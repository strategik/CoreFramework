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

namespace Strategik.CoreFramework.Helpers
{
    public class STKListsHelper : STKHelperBase
    {
        #region Constructor

        public STKListsHelper(ClientContext context)
            : base(context)
        { }

        #endregion Constructor

        #region Methods

        public List<List> EnsureLists(List<STKList> lists, STKProvisioningConfiguration config = null)
        {
            if (config == null) config = new STKProvisioningConfiguration();

            List<List> spLists = new List<List>();

            foreach (STKList list in lists)
            {
                spLists.Add(EnsureList(list, config));
            }

            return spLists;
        }

        public List EnsureList(STKList list, STKProvisioningConfiguration config = null)
        {
            if (list == null) throw new ArgumentNullException("list"); 
            if (config == null) config = new STKProvisioningConfiguration();
            List spList = null;

            list.Validate(); 

            if (config.UsePnP && config.UsePnPForLists)
            {
                STKPnPHelper pnpHelper = new STKPnPHelper(_clientContext);
                pnpHelper.Provision(list, config);

                // Apply the display name fix if required
                FixUpDisplayName(list);
            }
            else
            {
                #region Old Code
                ListTemplateType listTemplateType = GetListTemplateType(list);

                if (listTemplateType == ListTemplateType.DocumentLibrary)
                {
                    STKDocumentLibrary documentLibrary = list as STKDocumentLibrary;
                    spList = _clientContext.Web.CreateDocumentLibrary(documentLibrary.Name, documentLibrary.EnableVersioning);
                }
                else
                {
                    spList = _clientContext.Web.CreateList(listTemplateType, list.Name, list.EnableVersioning);
                }

                #endregion

                // Apply the rest of the list configuration required here
            }

            return spList;
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

        public List GetSharePointList(STKList list)
        {
            // try the name
            List spList = _clientContext.Web.GetListByTitle(list.Name);

            if (spList == null)
            {
                spList = _clientContext.Web.GetListByTitle(list.DisplayName);
            }

            // TODO - add URL llokup for the list

            return spList;
        }

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

        #endregion Methods
    }
}