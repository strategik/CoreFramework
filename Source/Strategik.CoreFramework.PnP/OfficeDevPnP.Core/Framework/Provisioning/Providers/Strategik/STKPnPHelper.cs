
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
using System.Text;
using System.Threading.Tasks;
using OfficeDevPnP.Core.Framework.Provisioning.Providers.Strategik;
using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core.Utilities;
using Strategik.Definitions.O365.Taxonomy;
using OfficeDevPnP.Core.Framework.Provisioning.Model;
using Strategik.Definitions.O365.Fields;
using Microsoft.SharePoint.Client.Taxonomy;
using Strategik.Definitions.O365.ContentTypes;
using Strategik.Definitions.O365.UserInterface;
using Strategik.Definitions.O365.Lists;
using Strategik.Definitions.O365.Libraries;
using Strategik.Definitions.O365.Pages;
using Strategik.Definitions.O365.Features;

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

        #endregion

        #region Constructor

        public STKPnPHelper(ClientContext clientContext)
        {
            if (clientContext == null) throw new ArgumentNullException("Client Context");

            _clientContext = clientContext;
            _clientContext.Load(_clientContext.Web);
            _clientContext.Load(_clientContext.Site);

            _clientContext.ExecuteQueryRetry();

            _web = _clientContext.Web;
            _site = _clientContext.Site;

            Log.Info(STKConstants.LOGGING_SOURCE, "Initialised helper {0}. Target web is {1} located at {2}", this.GetType().Name, _web.Title, _web.Url);

        }

        #endregion

        #region Provisioning Methods

        /// <summary>
        /// Provision a Strategik Taxonomy definition
        /// </summary>
        /// <remarks>
        /// Converts the taxonomy to to a PnP template then perfroms the provisioning 
        /// using the core PnP code
        /// </remarks>
        /// <param name="taxonomy">The definition of the taxonomy</param>
        public void Provision(STKTaxonomy taxonomy) 
        {
            ProvisioningTemplate template = new ProvisioningTemplate();
            template.TermGroups.AddRange(taxonomy.GeneratePnPTemplates());
            _web.ApplyProvisioningTemplate(template);
        }

        public void Provision(List<STKField> fields)
        {
            // An inefficient "one at at time" provisioning of new site columns
            foreach (STKField field in fields) 
            {
                Provision(field);
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
        public void Provision(STKField field) 
        {
            // Taxonomy columns can leave a hidden field behind - if we dont try and
            // delete it any attempt to provision the field again will fail
            if (field.SharePointType == STKFieldType.TaxonomyFieldType)
            {
                _web.CleanupTaxonomyHiddenField((STKTaxonomyField)field);
            }

            // Generate the PnP template
            ProvisioningTemplate template = new ProvisioningTemplate();
            template.SiteFields.Add(field.GeneratePnPTemplate());
            _web.ApplyProvisioningTemplate(template);

            // If the site column is a taxonomy column then we need to 
            // wire it up to its termset
            if (field.SharePointType == STKFieldType.TaxonomyFieldType)
            {
                try
                {
                    Microsoft.SharePoint.Client.Field spSiteColumn = _web.Fields.GetById(field.UniqueId);
                    _clientContext.ExecuteQueryRetry();
                    STKTaxonomyField taxonomyField = field as STKTaxonomyField;
                    _web.WireUpTaxonomyField(spSiteColumn, taxonomyField);
                }
                catch 
                {
                    // oops
                }
                
            }
        }

        /// <summary>
        /// Provision a collection of content types
        /// </summary>
        /// <param name="contentTypes"></param>
        public void Provision(List<STKContentType> contentTypes) 
        {
            foreach (STKContentType contentType in contentTypes) 
            {
                Provision(contentType);
            }
        }

        /// <summary>
        /// Provision a Strategik Site Column Definition
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
        public void Provision(STKContentType contentType) 
        {
            // Provision any new site columns defined in the content type
            Provision(contentType.SiteColumns);

            // Generate the PnP template
            ProvisioningTemplate template = new ProvisioningTemplate();
            template.ContentTypes.Add(contentType.GeneratePnPTemplate());
            _web.ApplyProvisioningTemplate(template);
        }

        /// <summary>
        /// Lists and Libraries
        /// </summary>
        /// <param name="list"></param>
        public void Provision(STKList list)
        {
            // Provision any site columns embedded in the list definition
            Provision(list.SiteColumns);

            // Provision any Content types embedded in the list definition
            Provision(list.ContentTypes);

            // Provision the list
            ProvisioningTemplate template = new ProvisioningTemplate();
            template.Lists.Add(list.GeneratePnPTemplate());
            _web.ApplyProvisioningTemplate(template);
        }

        /// <summary>
        /// Pages
        /// </summary>
        /// <param name="page"></param>
        public void Provision(STKPage page) 
        {
            // Provision the list
            ProvisioningTemplate template = new ProvisioningTemplate();
            template.Pages.Add(page.GeneratePnPTemplate());
            _web.ApplyProvisioningTemplate(template);
        }

        /// <summary>
        /// Custom actions
        /// </summary>
        /// <param name="customAction"></param>
        public void Provision(STKCustomActions customAction) 
        {
            // Generate the PnP template
            ProvisioningTemplate template = new ProvisioningTemplate();
            template.CustomActions = customAction.GeneratePnPTemplate();
            _web.ApplyProvisioningTemplate(template);
        }

        // Composed looks
        public void Provision(STKComposedLook composedLook) 
        {
            // Generate the PnP template
            ProvisioningTemplate template = new ProvisioningTemplate();
            template.ComposedLook = composedLook.GeneratePnPTemplate();
            _web.ApplyProvisioningTemplate(template);
        }

        // Features
        public void Provision(STKFeatures features) 
        {
            // Generate the PnP template
            ProvisioningTemplate template = new ProvisioningTemplate();

            foreach(STKFeature siteFeature in features.SiteFeatures)
            {
                template.Features.SiteFeatures.Add(siteFeature.GeneratePnPTemplate());
            }

            foreach (STKFeature webFeature in features.WebFeatures)
            {
                template.Features.WebFeatures.Add(webFeature.GeneratePnPTemplate());
            }

            _web.ApplyProvisioningTemplate(template);
        }

        // Files
        // TODO: Understand how this works in the engine

        #endregion
    }
}
