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

using Microsoft.SharePoint.Client.Taxonomy;
using Strategik.Definitions.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using OfficeDevPnP.Core.Framework.Provisioning.Model;
using Strategik.Definitions.ContentTypes;
using Strategik.Definitions.Lists;
using Strategik.Definitions.Pages;
using Strategik.Definitions.Taxonomy;
using TermGroup = Microsoft.SharePoint.Client.Taxonomy.TermGroup;
using TermSet = Microsoft.SharePoint.Client.Taxonomy.TermSet;
using OfficeDevPnP.Core.Framework.Provisioning.ObjectHandlers;
using Strategik.Definitions.Security;
using Strategik.Definitions.Sites;

namespace Microsoft.SharePoint.Client
{
    public static partial class STKWebExtensions
    {
        #region Taxonomy

        public static void STKWireUpTaxonomyField(this Web web, Field field, STKTaxonomyField taxonomyField)
        {
            web.STKWireUpTaxonomyField(field, taxonomyField.TermsetId, taxonomyField.TermGroupId, taxonomyField.AllowMultiSelect);
        }

        public static void STKWireUpTaxonomyField(this Web web, Field field, Guid mmsTermSetId, Guid mmsTermGroupId, bool multiValue = false)
        {
            TermStore termStore = GetDefaultTermStore(web);

            if (termStore == null)
                throw new NullReferenceException("The default term store is not available.");

            if (mmsTermSetId == null)
                throw new ArgumentNullException("mmsTermSetId", "The MMS term set id is not specified.");

            if (mmsTermGroupId == null)
                throw new ArgumentNullException("mmsTermGroupId", "The MMS term group id is not specified.");

            // get the term group and term set
            TermGroup termGroup = termStore.Groups.GetById(mmsTermGroupId);
            TermSet termSet = termGroup.TermSets.GetById(mmsTermSetId);
            web.Context.Load(termStore);
            web.Context.Load(termSet);
            web.Context.ExecuteQueryRetry();

            web.WireUpTaxonomyField(field, termSet, multiValue);
        }

        public static void STKCleanupTaxonomyHiddenField(this Web web, STKTaxonomyField stkSiteColumn)
        {
            // if the Guid is empty then we'll have no issue
            try
            {
                FieldCollection _fields = web.Fields;
                web.Context.Load(_fields, fc => fc.Include(f => f.Id, f => f.InternalName, f => f.Hidden));
                web.Context.ExecuteQueryRetry();
                var _field = _fields.FirstOrDefault(f => f.InternalName.Equals(stkSiteColumn.UniqueId));
                // if the field does not exist we assume the possiblity that it was created earlier then deleted and the hidden field was left behind
                // if the field does exist then return and let the calling process exception out when attempting to create it
                // this does not appear to be an issue with lists, just site columns, but it doesnt hurt to check
                // if (_field == null)
                // {
                // The hidden field format is the id of the field itself with hyphens removed and the first character replaced
                // with a random character, so get everything to the right of the first character and remove hyphens
                var _hiddenField = stkSiteColumn.UniqueId.ToString().Replace("-", "").Substring(1);
                _field = _fields.FirstOrDefault(f => f.InternalName.EndsWith(_hiddenField));
                if (_field != null)
                {
                    if (_field.Hidden)
                    {
                        // just in case the field itself is hidden, make sure it is not because depending on the current CU hidden fields may not be deletable
                        _field.Hidden = false;
                        _field.Update();
                    }
                    _field.DeleteObject();
                    web.Context.ExecuteQueryRetry();
                }
                // }
            }
            catch { }
        }

        private static TermStore GetDefaultTermStore(Web web)
        {
            TermStore termStore = null;
            TaxonomySession taxonomySession = TaxonomySession.GetTaxonomySession(web.Context);
            web.Context.Load(taxonomySession,
                ts => ts.TermStores.Include(
                    store => store.Name,
                    store => store.Groups.Include(
                        group => group.Name
                        )
                    )
                );
            web.Context.ExecuteQueryRetry();
            if (taxonomySession != null)
            {
                termStore = taxonomySession.GetDefaultSiteCollectionTermStore();
            }

            return termStore;
        }

        #endregion Taxonomy


        #region Read Defintions

        #region Site Columns
        /// <summary>
        /// Read the site columns definitions for the current web
        /// </summary>
        /// <param name="web"></param>
        /// <returns></returns>
        public static List<STKField> ReadSiteColumns(this Web web)
        {
            List<STKField> stkSiteColumns = new List<STKField>();

            // Generate the PnP templates for site columns
            ProvisioningTemplate template = new ProvisioningTemplate();
            ProvisioningTemplateCreationInformation createInfo = new ProvisioningTemplateCreationInformation(web);
            template = web.GetProvisioningTemplate(createInfo);

            // Convert the templates returned to STKFields
            foreach (OfficeDevPnP.Core.Framework.Provisioning.Model.Field field in template.SiteFields)
            {
                stkSiteColumns.Add(field.GenerateStrategikDefinition());
            }
            
            return stkSiteColumns;
        }

        #endregion

        #region  Content Types
        public static List<STKContentType> ReadContentTypes(this Web web)
        {
            List<STKContentType> stkContentTypes = new List<STKContentType>();

            // Generate the PnP templates for site columns
            ProvisioningTemplate template = new ProvisioningTemplate();
            ProvisioningTemplateCreationInformation createInfo = new ProvisioningTemplateCreationInformation(web);
            template = web.GetProvisioningTemplate(createInfo);

            // Convert the templates returns to STKFields
            foreach (OfficeDevPnP.Core.Framework.Provisioning.Model.ContentType contentType in template.ContentTypes)
            {
                stkContentTypes.Add(contentType.GenerateStrategikDefinition());
            }

            return stkContentTypes;
        }

        #endregion

        #region Lists
        public static List<STKList> ReadLists(this Web web)
        {
            List<STKList> stkLists = new List<STKList>();

            // Generate the PnP templates for site columns
            ProvisioningTemplate template = new ProvisioningTemplate();
            ProvisioningTemplateCreationInformation createInfo = new ProvisioningTemplateCreationInformation(web);
            template = web.GetProvisioningTemplate(createInfo);

            foreach (ListInstance list in template.Lists)
            {
                stkLists.Add(list.GenerateStrategikDefinition());
            }

            return stkLists;
        }

        #endregion

        #region Groups

        public static List<STKGroup> ReadGroups(this Web web)
        {
            List<STKGroup> stkGroups = new List<STKGroup>();

            // Generate the PnP templates for site columns
            ProvisioningTemplate template = new ProvisioningTemplate();
            ProvisioningTemplateCreationInformation createInfo = new ProvisioningTemplateCreationInformation(web);
            createInfo.IncludeSiteGroups = true;
            template = web.GetProvisioningTemplate(createInfo);

            foreach (SiteGroup group in template.Security.SiteGroups)
            {
                stkGroups.Add(group.GenerateStrategikDefinition());
            }

            return stkGroups;
        }

        #endregion

        #region Web

        public static STKWeb ReadWeb(this Web web)
        {
           

            // Generate the PnP templates for the web
            ProvisioningTemplate template = new ProvisioningTemplate();
            ProvisioningTemplateCreationInformation createInfo = new ProvisioningTemplateCreationInformation(web);
            createInfo.IncludeSiteGroups = true;

            template = web.GetProvisioningTemplate(createInfo);

            STKWeb stkWeb = template.GenerateStrategikDefinition(web, createInfo);

            // Add Groups
            foreach (SiteGroup group in template.Security.SiteGroups)
            {
                stkWeb.SecurityGroups.Add(group.GenerateStrategikDefinition());
            }

            // Add Site Columns
            foreach (OfficeDevPnP.Core.Framework.Provisioning.Model.Field field in template.SiteFields)
            {
                stkWeb.SiteColumns.Add(field.GenerateStrategikDefinition());
            }

            // Add Content Types
            foreach (OfficeDevPnP.Core.Framework.Provisioning.Model.ContentType contentType in template.ContentTypes)
            {
                stkWeb.ContentTypes.Add(contentType.GenerateStrategikDefinition());
            }

            // Add List Instances
            foreach (ListInstance list in template.Lists)
            {
                stkWeb.Lists.Add(list.GenerateStrategikDefinition());
            }

            return stkWeb;
        }

        #endregion

        //// Pages
        //public static List<STKPage> ReadPages(this Web web)
        //{
        //    List<STKPage> stkPages = new List<STKPage>();

        //    // Generate the PnP templates for site columns
        //    ProvisioningTemplate template = new ProvisioningTemplate();
        //    ProvisioningTemplateCreationInformation createInfo = new ProvisioningTemplateCreationInformation(web);
        //    ObjectPages pageTemplateGenerator = new ObjectPages();
        //    template = pageTemplateGenerator.ExtractObjects(web, template, createInfo);
        //    //
        //    foreach (Page page in template.Pages)
        //    {
        //        stkPages.Add(page.GenerateStrategikDefinition());
        //    }

        //    return stkPages;
        //}

        #endregion
    }
}