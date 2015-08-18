using Microsoft.SharePoint.Client.Taxonomy;
using Strategik.Definitions.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using OfficeDevPnP.Core.Framework.Provisioning.Model;
using OfficeDevPnP.Core.Framework.Provisioning.ObjectHandlers;
using Strategik.Definitions.ContentTypes;
using Strategik.Definitions.Lists;
using Strategik.Definitions.Pages;
using Strategik.Definitions.Taxonomy;
using TermGroup = Microsoft.SharePoint.Client.Taxonomy.TermGroup;
using TermSet = Microsoft.SharePoint.Client.Taxonomy.TermSet;

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

            // Convert the templates returns to STKFields
            foreach (OfficeDevPnP.Core.Framework.Provisioning.Model.Field field in template.SiteFields)
            {
                stkSiteColumns.Add(field.GenerateStrategikDefinition());
            }
            
            return stkSiteColumns;
        }

        // Content Types
        //public static List<STKContentType> ReadContentTypes(this Web web)
        //{
        //    List<STKContentType> stkContentTypes = new List<STKContentType>();

        //    // Generate the PnP templates for site columns
        //    ProvisioningTemplate template = new ProvisioningTemplate();
        //    ProvisioningTemplateCreationInformation createInfo = new ProvisioningTemplateCreationInformation(web);
        //    ObjectContentType contentTypeTemplateGenerator = new ObjectContentType();
        //    template = contentTypeTemplateGenerator.ExtractObjects(web, template, createInfo);

        //    // Convert the templates returns to STKFields
        //    foreach (OfficeDevPnP.Core.Framework.Provisioning.Model.ContentType contentType in template.ContentTypes)
        //    {
        //        stkContentTypes.Add(contentType.GenerateStrategikDefinition());
        //    }

        //    return stkContentTypes;
        //}

        // Lists
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