#region License

//
// Copyright (c) 2015 Strategik Pty Ltd,
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

#endregion License

using System;
using Microsoft.SharePoint.Client;

using Strategik.Definitions.Configuration;
using Strategik.Definitions.Libraries;
using Strategik.Definitions.Lists;
using System.Collections.Generic;
using OfficeDevPnP.Core.Framework.Provisioning.Providers.Strategik;
using Strategik.Definitions.Fields;
using OfficeDevPnP.Core.Diagnostics;
using Strategik.Definitions.Events;
using Microsoft.SharePoint.Client.EventReceivers;

namespace Strategik.CoreFramework.Helpers
{
    public class STKListHelper : STKHelperBase
    {
        private const String LogSource = "CoreFramework.STKListHelper";

        #region Constructor

        public STKListHelper(ClientContext context)
            : base(context)
        { }

        #endregion Constructor

        #region Ensure Lists Methods

        public void EnsureLists(List<STKList> lists, STKProvisioningConfiguration config = null)
        {
            if (lists == null) throw new ArgumentNullException("lists");
            if (config == null) config = new STKProvisioningConfiguration();

            foreach (STKList list in lists)
            {
                EnsureList(list, config);
            }        
        }

        public void EnsureList(STKList list, STKProvisioningConfiguration config = null)
        {
            if (list == null) throw new ArgumentNullException("list"); 
            if (config == null) config = new STKProvisioningConfiguration();
            list.Validate();

            Log.Debug(LogSource, "Starting EnsureList() for list {0}", list.Name);
            STKPnPHelper pnpHelper = new STKPnPHelper(_clientContext);
            pnpHelper.Provision(list, config);

            // Apply the display name fix if required
            FixUpDisplayName(list);

            // Register any remote event receivers defined for the current list
            if(list.HasEventReceivers()) RegisterRemoteEventReceivers(list, config);


            // Add any data defined in the template (experimental)
            //if(list.Items.Count > 0)
            //{
            //    AddListData(list, list.Items);
            //}

            Log.Debug(LogSource, "EnsureList for List {0} complete", list.Name);        
        }

        public void RegisterRemoteEventReceivers(STKList list, STKProvisioningConfiguration config)
        {
            if (list.HasEventReceivers())
            {
                // Note - remote event receivers registered in this way are called with no context token
                if (_clientContext.Web.ListExists(list.Title))
                {
                    List targetList = _clientContext.Web.GetListByTitle(list.Title);

                    foreach (STKListItemEventReceiver receiver in list.EventReceivers)
                    {
                        EventReceiverSynchronization sync = (receiver.Synchronous) ? EventReceiverSynchronization.Synchronous : EventReceiverSynchronization.Asynchronous;

                        foreach (STKEventReceiverType eventReceiverType in receiver.EventReceiverTypes)
                        {
                            EventReceiverType csomReceiverType = (EventReceiverType)Enum.Parse(typeof(EventReceiverType), eventReceiverType.ToString());
                            targetList.AddRemoteEventReceiver(receiver.Name, receiver.Url, csomReceiverType, sync, true);
                        }
                    }
                }
            }
        }

        public void AddListData(STKList list, List<STKListItem> data)
        {
            if (list == null) throw new ArgumentNullException("list");
            if (data == null) throw new ArgumentNullException("data");
            if (data.Count == 0) return; // No data to add

            List spList =  _web.GetListByTitle(list.Title);
            if(spList != null)
            {
                foreach(STKListItem item in data)
                {
                    ListItemCreationInformation ci = new ListItemCreationInformation();
                    ListItem spListItem = spList.AddItem(ci);

                    foreach(String fieldName in item.Values.Keys)
                    {
                        spListItem[fieldName] = item.Values[fieldName];
                    }

                    spListItem.Update();
                    // _clientContext.ExecuteQueryRetry();
                }

                _clientContext.ExecuteQueryRetry();
            }
        }

        private void FixUpDisplayName(STKList list)
        {
            if( list != null && 
                String.IsNullOrEmpty(list.DisplayName) == false 
                && list.DisplayName.Equals(list.Name, StringComparison.InvariantCultureIgnoreCase) == false)
            {
                // We have a display name set that isnt the same as the list name
                // A common requirement when we want spaces in the list name but not in its URL
                List spList = GetSharePointList(list);
                if(spList != null)
                {
                    if (spList.Title.Equals(list.DisplayName, StringComparison.InvariantCulture) == false)
                    {
                        spList.Title = list.DisplayName;
                        spList.Update();
                        _clientContext.ExecuteQueryRetry();
                    }
                }
            }
        }

        #endregion

        #region Read Lists Methods

        public List<STKList> ReadLists(bool includeData)
        {
            STKPnPHelper stkPnPHelper = new STKPnPHelper(_clientContext);
            List<STKList> lists = stkPnPHelper.ReadLists();

            if (includeData) // PnP doesnt do this so we have to do it ourselves
            {
                foreach(STKList list in lists)
                {
                    AddListData(list);
                }
            }

            return lists;
        }

        private void AddListData(STKList list)
        {
            List spList = GetSharePointList(list);
            if(spList != null)
            {
                CamlQuery query = CamlQuery.CreateAllItemsQuery();
                ListItemCollection allListItems = spList.GetItems(query);
                _clientContext.Load(allListItems);
                _clientContext.ExecuteQueryRetry(); // Loads all list items but wil be missing Content type

                if(allListItems.Count > 0) // we found some data
                {
                    // A trick to load all the Content Types in a single server call
                    foreach (ListItem item in allListItems)
                    {
                        _clientContext.Load(item, i => i.ContentType);
                    }
                    _clientContext.ExecuteQueryRetry();


                    // Now prcess the data retrived
                    foreach (ListItem item in allListItems)
                    {

                        // Read the data into our template
                        STKListItem stkListItem = new STKListItem();
                        
                        // Process the built in fields on our list
                        foreach(String fieldName in list._builtInFieldsRead)
                        { 
                            String key = fieldName;
                            Object value = null;

                            if (key.ToLower().StartsWith("contenttype"))
                            {
                                value = item.ContentType;
                            }
                            else
                            {
                                value = item[key];
                            }
                            stkListItem.Values.Add(key, value);
                        }

                        // Process the custom fields on our list
                        foreach (STKField field in list.Fields) // The custom fields read into the template
                        {
                            String key = field.Name;
                            Object value = item[key];
                            stkListItem.Values.Add(key, value);
                        }

                        // TODO: - Check site columns

                    }
                }
            }
        }

        public List GetSharePointList(STKList list)
        {
            // try the name
            List spList = _clientContext.Web.GetListByTitle(list.Name);

            if (spList == null)
            {
                spList = _clientContext.Web.GetListByTitle(list.DisplayName);
            }

            // TODO - add URL lookup for the list

            return spList;
        }

        #endregion Methods

        #region Exists

        public bool Exists(STKList list)
        {
            if (list == null) throw new ArgumentNullException("list");
            list.Validate();
            return _clientContext.Web.ListExists(list.Name);
        } 

        #endregion

        #region Utilities 

        private ListTemplateType GetListTemplateType(STKList list)
        {
            ListTemplateType templateType = ListTemplateType.GenericList;

            switch (list.ListType)
            {
                case STKListType.AdminTasks:
                    templateType = ListTemplateType.AdminTasks;
                    break;

                case STKListType.Agenda:
                    templateType = ListTemplateType.Agenda;
                    break;

                case STKListType.Announcements:
                    templateType = ListTemplateType.Announcements;
                    break;

                case STKListType.Categories:
                    templateType = ListTemplateType.Categories;
                    break;

                case STKListType.Comments:
                    templateType = ListTemplateType.Comments;
                    break;

                case STKListType.Contacts:
                    templateType = ListTemplateType.Contacts;
                    break;

                case STKListType.CustomGrid:
                    templateType = ListTemplateType.CustomGrid;
                    break;

                case STKListType.DataConnectionLibrary:
                    templateType = ListTemplateType.DataConnectionLibrary;
                    break;

                case STKListType.DataSources:
                    templateType = ListTemplateType.DataSources;
                    break;

                case STKListType.Decision:
                    templateType = ListTemplateType.Decision;
                    break;

                case STKListType.DiscussionBoard:
                    templateType = ListTemplateType.DiscussionBoard;
                    break;

                case STKListType.DocumentLibrary:
                    templateType = ListTemplateType.DocumentLibrary;
                    break;

                case STKListType.Events:
                    templateType = ListTemplateType.Events;
                    break;

                case STKListType.GanttTasks:
                    templateType = ListTemplateType.GanttTasks;
                    break;

                case STKListType.GenericList:
                    templateType = ListTemplateType.GenericList;
                    break;

                case STKListType.HomePageLibrary:
                    templateType = ListTemplateType.HomePageLibrary;
                    break;

                case STKListType.IssueTracking:
                    templateType = ListTemplateType.IssueTracking;
                    break;

                case STKListType.Links:
                    templateType = ListTemplateType.Links;
                    break;

                case STKListType.ListTemplateCatalog:
                    templateType = ListTemplateType.ListTemplateCatalog;
                    break;

                case STKListType.MasterPageCatalog:
                    templateType = ListTemplateType.MasterPageCatalog;
                    break;

                case STKListType.MeetingObjective:
                    templateType = ListTemplateType.MeetingObjective;
                    break;

                case STKListType.Meetings:
                    templateType = ListTemplateType.Meetings;
                    break;

                case STKListType.MeetingUser:
                    templateType = ListTemplateType.MeetingUser;
                    break;

                case STKListType.NoCodeWorkflows:
                    templateType = ListTemplateType.NoCodeWorkflows;
                    break;

                case STKListType.PictureLibrary:
                    templateType = ListTemplateType.NoCodeWorkflows;
                    break;

                case STKListType.Posts:
                    templateType = ListTemplateType.Posts;
                    break;

                case STKListType.Survey:
                    templateType = ListTemplateType.Survey;
                    break;

                case STKListType.Tasks:
                    templateType = ListTemplateType.Tasks;
                    break;

                case STKListType.TextBox:
                    templateType = ListTemplateType.TextBox;
                    break;

                case STKListType.ThingsToBring:
                    templateType = ListTemplateType.ThingsToBring;
                    break;

                case STKListType.UserInformation:
                    templateType = ListTemplateType.UserInformation;
                    break;

                case STKListType.WebPageLibrary:
                    templateType = ListTemplateType.WebPageLibrary;
                    break;

                case STKListType.WebPartCatalog:
                    templateType = ListTemplateType.WebPartCatalog;
                    break;

                case STKListType.WebTemplateCatalog:
                    templateType = ListTemplateType.WebTemplateCatalog;
                    break;

                case STKListType.WorkflowHistory:
                    templateType = ListTemplateType.WorkflowHistory;
                    break;

                case STKListType.WorkflowProcess:
                    templateType = ListTemplateType.WorkflowProcess;
                    break;

                case STKListType.XMLForm:
                    templateType = ListTemplateType.XMLForm;
                    break;

                case STKListType.CustomList:
                    templateType = ListTemplateType.GenericList;
                    break;

                default:
                    break;
            }

            return templateType;
        }

        #endregion
    }
}