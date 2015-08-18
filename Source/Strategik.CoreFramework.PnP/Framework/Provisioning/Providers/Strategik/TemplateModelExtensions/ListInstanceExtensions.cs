
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
//
// Author:  Dr Adrian Colquhoun
//
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using Strategik.Definitions.ContentTypes;
using Strategik.Definitions.Libraries;
using Strategik.Definitions.Lists;

namespace OfficeDevPnP.Core.Framework.Provisioning.Model
{
    public static partial class ListInstanceExtensions
    {
        #region Generate strategik definition from corresponding PnP template object

        public static STKList GenerateStrategikDefinition(this ListInstance listInstance)
        {
            STKList stkList = null;

            switch (listInstance.TemplateType)
            {
                case (int)STKListType.Announcements:
                {
                    stkList = new STKAnnouncementsList(listInstance.Title);
                    break;
                }

                case (int)STKListType.Contacts:
                {
                    stkList = new STKContactList(listInstance.Title);
                    break;
                }

                case (int)STKListType.DiscussionBoard:
                {
                    stkList = new STKDiscussionForum(listInstance.Title);
                    break;
                }

                case (int)STKListType.Events:
                {
                    stkList = new STKEventsList(listInstance.Title);
                    break;
                }

                case (int)STKListType.IssueTracking:
                {
                    stkList = new STKIssueTrackingList(listInstance.Title);
                    break;
                }

                case (int)STKListType.Links:
                {
                    stkList = new STKLinksList(listInstance.Title);
                    break;
                }

                case (int) STKListType.GanttTasks:
                {
                    stkList = new STKProjectTaskList(listInstance.Title);
                    break;
                }

                case (int)STKListType.Tasks:
                {
                    stkList = new STKTaskList(listInstance.Title);
                    break;
                }

                // Libraries
                case (int) STKListType.PictureLibrary:
                {
                    stkList = new STKPictureLibrary(listInstance.Title);
                    break;
                }

                case (int)STKListType.DocumentLibrary:
                {
                    stkList = new STKDocumentLibrary(listInstance.Title);
                    break;
                }

                default:
                {
                    stkList = new STKList(listInstance.Title);
                    break;
                }

            }

            // Set the common properties
            stkList.AllowContentTypes = listInstance.ContentTypesEnabled;
            stkList.AllowFolders = listInstance.EnableFolderCreation;
            stkList.ListType = (STKListType)listInstance.TemplateType;
            stkList.Description = listInstance.Description;
            stkList.DocumentTemplate = listInstance.DocumentTemplate;
            stkList.DraftVersionVisibility = listInstance.DraftVersionVisibility;
            stkList.EnableAttachments = listInstance.EnableAttachments;
            stkList.EnableMinorVersions = listInstance.EnableMinorVersions;
            stkList.Hidden = listInstance.Hidden;
            stkList.MaxVersionLimit = listInstance.MaxVersionLimit;
            //stkList.MinorVersions = listInstance.MinorVersionLimit;
            stkList.EnableVersioning = listInstance.EnableVersioning;
            stkList.EnableModeration = listInstance.EnableModeration;
            stkList.OnQuickLaunch = listInstance.OnQuickLaunch;
            stkList.Url = listInstance.Url;
            
            // Views
            foreach (View view in listInstance.Views)
            {
                stkList.Views.Add(view.GenerateStrategikDefinition());
            }

            // Columns
            foreach (Field field in listInstance.Fields)
            {
                stkList.Fields.Add(field.GenerateStrategikDefinition());
            }

            // Column Refs
            foreach (FieldRef fieldRef in listInstance.FieldRefs)
            {
                stkList.FieldLinks.Add(fieldRef.GenerateStrategikDefinition());
            }

            // Content Types
            foreach (ContentTypeBinding contentTypeBinding in listInstance.ContentTypeBindings)
            {
                STKContentType stkContentType = new STKContentType()
                {
                    SharePointContentTypeId = contentTypeBinding.ContentTypeId
                };
                //TODO: This needs some work
                stkList.ContentTypes.Add(stkContentType);
            }

            return stkList;
        }

        #endregion
    }
}
