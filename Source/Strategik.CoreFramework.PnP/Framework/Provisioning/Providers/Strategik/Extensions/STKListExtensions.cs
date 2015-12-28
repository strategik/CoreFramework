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

#endregion License

using OfficeDevPnP.Core.Framework.Provisioning.Model;
using Strategik.Definitions.ContentTypes;
using Strategik.Definitions.Fields;
using System;

namespace Strategik.Definitions.Lists
{
    public static partial class STKListExtensions
    {
        public static ListInstance GeneratePnPTemplate(this STKList list)
        {
            ListInstance listInstanceTemplate = new ListInstance()
            {
                ContentTypesEnabled = list.EnableContentTypes,
                Description = list.Description,
                DocumentTemplate = list.DocumentTemplate,
                DraftVersionVisibility = list.DraftVersionVisibility,
                EnableAttachments = list.EnableAttachments,
                EnableFolderCreation = list.AllowFolders,
                EnableMinorVersions = list.EnableMinorVersions,
                EnableVersioning = list.EnableVersioning,
                EnableModeration = list.EnableModeration,
                Hidden = list.Hidden,
                MaxVersionLimit = list.MaxVersionLimit,
                OnQuickLaunch = list.OnQuickLaunch,
                RemoveExistingContentTypes = list.RemoveExistingContentTypes,
                TemplateType = (int)list.ListType,
                Title = list.Title,
                Url = (list.LeafUrl != null) ? list.LeafUrl : list.Title
            };

            if (list.TemplateFeatureId != Guid.Empty) // Required for lists susch as the Promoted links
            {
                listInstanceTemplate.TemplateFeatureID = list.TemplateFeatureId;
            }

            // Fields
            foreach (STKField field in list.Fields)
            {
                field.Validate();
                listInstanceTemplate.Fields.Add(field.GeneratePnPTemplate());
            }

            // Site columns
            foreach (STKField siteColumn in list.SiteColumns)
            {
                siteColumn.Validate();
                listInstanceTemplate.FieldRefs.Add(siteColumn.GeneratePnPFieldRefTemplate());
            }

            // Data
            foreach (STKListItem listItem in list.Items)
            {
                listInstanceTemplate.DataRows.Add(listItem.GeneratePnPTemplate());
            }

            // Views
            foreach (STKListView listView in list.Views)
            {
                listInstanceTemplate.Views.Add(listView.GeneratePnPTemplate());
            }

            // Content Types
            foreach (STKContentType contentType in list.ContentTypes)
            {
                listInstanceTemplate.ContentTypeBindings.Add(contentType.GenerContentTypeBindingPnPTemplate());
            }
            

            return listInstanceTemplate;
        }

        public static DataRow GeneratePnPTemplate(this STKListItem listItem)
        {
            DataRow dataRow = new DataRow();
            foreach (String key in listItem.Values.Keys)
            {
                dataRow.Values.Add(key, listItem.Values[key].ToString());
            }
            return dataRow;
        }

        public static View GeneratePnPTemplate(this STKListView listView)
        {
            View viewTemplate = new View()
            {
                //SchemaXml = listView.  //TODO - Generate the view schema from the information that we have
            };

            return viewTemplate;
        }
    }
}