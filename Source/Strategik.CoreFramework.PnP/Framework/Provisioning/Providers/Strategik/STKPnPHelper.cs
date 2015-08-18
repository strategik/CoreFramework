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

using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core.Framework.Provisioning.Model;
using OfficeDevPnP.Core.Utilities;
using Strategik.Definitions.Configuration;
using Strategik.Definitions.ContentTypes;
using Strategik.Definitions.Features;
using Strategik.Definitions.Fields;
using Strategik.Definitions.Lists;
using Strategik.Definitions.Pages;
using Strategik.Definitions.Taxonomy;
using Strategik.Definitions.UserInterface;
using System;
using System.Collections.Generic;
using Field = Microsoft.SharePoint.Client.Field;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Strategik
{
    /// <summary>
    /// Helper classs to provision Strategik definitions using the PnP framework
    /// </summary>
    /// <remarks>
    /// Takes Strategik definitions objects and converts them to their corresponding
    /// PnP template definitions so that they can be deployed using the
    /// PnP code (rather than ours) saving us heaps of work.
    ///
    /// Thanks!
    /// </remarks>
    public class STKPnPHelper
    {
        #region Data

        protected ClientContext _clientContext;
        protected Web _web;
        protected Site _site;

        #endregion Data

        #region Constructor

        public STKPnPHelper(ClientContext clientContext)
        {
            if (clientContext == null) throw new ArgumentNullException("clientContext");

            _clientContext = clientContext;
            _clientContext.Load(_clientContext.Web);
            _clientContext.Load(_clientContext.Site);

            _clientContext.ExecuteQueryRetry();

            _web = _clientContext.Web;
            _site = _clientContext.Site;

            Log.Info(STKConstants.LOGGING_SOURCE, "Initialised helper {0}. Target web is {1} located at {2}", this.GetType().Name, _web.Title, _web.Url);
        }

        #endregion Constructor

        #region Provisioning Methods

        #region Taxonomy
        /// <summary>
        /// Provision a Strategik Taxonomy definition
        /// </summary>
        /// <remarks>
        /// Converts the taxonomy to to a PnP template then perfroms the provisioning
        /// using the core PnP code
        /// </remarks>
        /// <param name="taxonomy">The definition of the taxonomy</param>
        public void Provision(STKTaxonomy taxonomy, STKProvisioningConfiguration config = null)
        {
            ProvisioningTemplate template = new ProvisioningTemplate();
            template.TermGroups.AddRange(taxonomy.GeneratePnPTemplates());
            _web.ApplyProvisioningTemplate(template);
        }

        #endregion

        #region Fields

        public void Provision(List<STKField> fields, STKProvisioningConfiguration config = null)
        {
            if (fields == null) throw new ArgumentNullException("fields");
            if (config == null) config = new STKProvisioningConfiguration();

            // An inefficient "one at at time" provisioning of new site columns
            foreach (STKField field in fields)
            {
                Provision(field, config);
            }
        }

        /// <summary>
        /// Provision a Strategik Site Column Definition
        /// </summary>
        /// <remarks>
        /// Converts the taxonomy to to a PnP template then performs the provisioning
        /// using the core PnP code
        /// </remarks>
        /// <param name="field">The definition of the site column</param>
        public void Provision(STKField field, STKProvisioningConfiguration config = null)
        {
            if (field == null) throw new ArgumentNullException("field");
            if (config == null) config = new STKProvisioningConfiguration();

            // Check we have a valid definition 
            field.Validate();

            // Taxonomy columns can leave a hidden field behind - if we dont try and
            // delete it any attempt to provision the field again will fail
            if (field.SharePointType == STKFieldType.TaxonomyFieldType)
            {
                _web.STKCleanupTaxonomyHiddenField((STKTaxonomyField)field);
            }

            // Generate the PnP template
            ProvisioningTemplate template = new ProvisioningTemplate();
            template.SiteFields.Add(field.GeneratePnPTemplate());

            // provision the field
            _web.ApplyProvisioningTemplate(template);

            // If the site column is a taxonomy column then we need to
            // wire it up to its termset
            if (field.SharePointType == STKFieldType.TaxonomyFieldType)
            {
                try
                {
                    Field spSiteColumn = _web.Fields.GetById(field.UniqueId);
                    _clientContext.ExecuteQueryRetry();
                    STKTaxonomyField taxonomyField = field as STKTaxonomyField;
                    _web.STKWireUpTaxonomyField(spSiteColumn, taxonomyField);
                }
                catch
                {
                    // oops
                }
            }
        }

        #endregion

        #region Content Types

        /// <summary>
        /// Provision a collection of content types
        /// </summary>
        /// <param name="contentTypes"></param>
        public void Provision(List<STKContentType> contentTypes, STKProvisioningConfiguration config = null)
        {
            if (contentTypes == null) throw new ArgumentNullException("contentTypes");
            if (config == null) config = new STKProvisioningConfiguration();

            foreach (STKContentType contentType in contentTypes)
            {
                Provision(contentType);
            }
        }

        /// <summary>
        /// Provision a Strategik Content Type definition
        /// </summary>
        /// <remarks>
        /// Converts the content type definition to to a PnP template then performs the provisioning
        /// using the core PnP code.
        ///
        /// Our content type definitions can contain both links to existing site columns and the
        /// definitions of new site columns to be provisioned "on the fly". We extract and new site
        /// columns and provision them first, beofre attempting to provision the content type.
        /// </remarks>
        /// <param name="contentType"></param>
        public void Provision(STKContentType contentType, STKProvisioningConfiguration config = null)
        {
            if (contentType == null) throw new ArgumentNullException("contentType");
            if (config == null) config = new STKProvisioningConfiguration();

            // Check we have a valid definition
            contentType.Validate();

            // Provision any new site columns defined in the content type
            Provision(contentType.SiteColumns);

            // Generate the PnP template
            ProvisioningTemplate template = new ProvisioningTemplate();
            template.ContentTypes.Add(contentType.GeneratePnPTemplate());

            // Provision the content type
            _web.ApplyProvisioningTemplate(template);
        }

        #endregion

        #region Lists & Libraries

        /// <summary>
        /// Lists and Libraries
        /// </summary>
        /// <param name="list"></param>
        public void Provision(STKList list, STKProvisioningConfiguration config = null)
        {
            if (list == null) throw new ArgumentNullException("list");
            if (config == null) config = new STKProvisioningConfiguration();

            // Check we have a valid definition
            list.Validate();

            // Provision any site columns embedded in the list definition
            Provision(list.SiteColumns);

            // Provision any Content types embedded in the list definition
            Provision(list.ContentTypes);

            // Generate the PnP template
            ProvisioningTemplate template = new ProvisioningTemplate();
            template.Lists.Add(list.GeneratePnPTemplate());

            // Provision the list
            _web.ApplyProvisioningTemplate(template);
        }

        public void Provision(List<STKList> lists, STKProvisioningConfiguration config = null) 
        {
            if (lists == null) throw new ArgumentNullException("lists");
            if (config == null) config = new STKProvisioningConfiguration();

            foreach (STKList list in lists) 
            {
                Provision(list, config);
            }
        }

        #endregion

        #region Pages

        /// <summary>
        /// Pages
        /// </summary>
        /// <param name="page"></param>
        public void Provision(STKPage page, STKProvisioningConfiguration config = null)
        {
            // Provision the list
            ProvisioningTemplate template = new ProvisioningTemplate();
            template.Pages.Add(page.GeneratePnPTemplate());
            _web.ApplyProvisioningTemplate(template);
        }

        #endregion

        #region Custom Actions

        /// <summary>
        /// Custom actions
        /// </summary>
        /// <param name="customAction"></param>
        public void Provision(STKCustomActions customAction, STKProvisioningConfiguration config = null)
        {
            // Generate the PnP template
            ProvisioningTemplate template = new ProvisioningTemplate();
            template.CustomActions = customAction.GeneratePnPTemplate();
            _web.ApplyProvisioningTemplate(template);
        }

        #endregion

        #region Composed Looks

        // Composed looks
        public void Provision(STKComposedLook composedLook)
        {
            // Generate the PnP template
            ProvisioningTemplate template = new ProvisioningTemplate();
            template.ComposedLook = composedLook.GeneratePnPTemplate();
            _web.ApplyProvisioningTemplate(template);
        }

        #endregion

        #region Features

        // Features
        public void Provision(STKFeatures features, STKProvisioningConfiguration config = null)
        {
            // Generate the PnP template
            ProvisioningTemplate template = new ProvisioningTemplate();

            foreach (STKFeature siteFeature in features.SiteFeatures)
            {
                template.Features.SiteFeatures.Add(siteFeature.GeneratePnPTemplate());
            }

            foreach (STKFeature webFeature in features.WebFeatures)
            {
                template.Features.WebFeatures.Add(webFeature.GeneratePnPTemplate());
            }

            _web.ApplyProvisioningTemplate(template);
        }

        #endregion

        // Files
        // TODO: Understand how this works in the engine

        #endregion Provisioning Methods

        #region Read Methods

        public List<STKField> ReadSiteColumns()
        {
            return _web.ReadSiteColumns();
        }

        //public List<STKContentType> ReadContentTypes()
        //{
        //    return _web.ReadContentTypes();
        //}

        public List<STKList> ReadLists()
        {
            return _web.ReadLists();
        }


        #endregion
    }
}