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

using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core.Framework.Provisioning.Providers.Strategik;
using Strategik.Definitions.Configuration;
using Strategik.Definitions.ContentTypes;
using Strategik.Definitions.Fields;
using Strategik.Definitions.Lists;
using Strategik.Definitions.Sites;
using System;
using System.Collections.Generic;

namespace Strategik.CoreFramework.Helpers
{
    /// <summary>
    /// Helper to work with Webs
    /// </summary>
    public class STKWebHelper : STKHelperBase
    {
        #region Constructor

        public STKWebHelper(ClientContext context)
            : base(context)
        { }

        #endregion Constructor

        #region Methods

        #region Ensure Web

        public void EnsureWeb(STKWeb web)
        {
            EnsureWeb(web, new STKProvisioningConfiguration());
        }

        public void EnsureWeb(STKWeb web, STKProvisioningConfiguration config)
        {
            if (web == null) throw new ArgumentNullException("web");
            if (config == null) config = new STKProvisioningConfiguration();

            // Deactivate web features - Should be in an helper
            foreach (Guid featureId in web.FeaturesToDeactivate)
            {
                _web.DeactivateFeature(featureId);
            }

            // Activate web features
            foreach (Guid featureId in web.FeaturesToActivate)
            {
                _web.ActivateFeature(featureId);
            }

            STKContentTypeHelper contentTypeHelper = new STKContentTypeHelper(_clientContext);
            STKSiteColumnHelper siteColumnHelper = new STKSiteColumnHelper(_clientContext);

            // Ensure Site Columns & Content Types
            siteColumnHelper.EnsureSiteColumns(web.SiteColumns, config);
            contentTypeHelper.EnsureContentTypes(web.ContentTypes, config);

            STKListHelper listHelper = new STKListHelper(_clientContext);
            listHelper.EnsureLists(web.Lists, config);

            STKPageHelper pageHelper = new STKPageHelper(_clientContext);
            pageHelper.EnsurePages(web.PublishingPages, config);
            pageHelper.EnsurePages(web.WebPartPages, config);
            pageHelper.EnsurePages(web.WikiPages, config);
         
            // Ensure Content - TODO

            // Recursively provision subwebs
            foreach (STKWeb subWeb in web.SubWebs) 
            {
                ProvisionSubWeb(subWeb, config);
            }

        }

        private void ProvisionSubWeb(STKWeb subWeb, STKProvisioningConfiguration config) 
        {
            if(subWeb.LeafUrl == null) throw new ArgumentNullException("subWeb", "Leaf Url must be specified to provision subweb");
            Web spSubWeb = _clientContext.Web.GetWeb(subWeb.LeafUrl);
            _clientContext.ExecuteQueryRetry();

            if (spSubWeb == null) 
            {
                WebCreationInformation wci = new WebCreationInformation()
                {
                    Description = subWeb.Description,
                    Language = subWeb.Language,
                    Title = subWeb.Title,
                    Url = subWeb.LeafUrl,
                    UseSamePermissionsAsParentSite = subWeb.UseSamePermissionsAsParent,
                    WebTemplate = subWeb.template
                };

                _clientContext.Web.Webs.Add(wci);
                _clientContext.ExecuteQueryRetry();

                spSubWeb = _clientContext.Web.GetWeb(subWeb.LeafUrl);
                _clientContext.ExecuteQueryRetry();
            }

            ClientContext childContext = new ClientContext(spSubWeb.Url);
            childContext.Credentials = _clientContext.Credentials;
            STKWebHelper childWebHelper = new STKWebHelper(childContext);
            childWebHelper.EnsureWeb(subWeb, config);
           
        }

        #endregion

        #region Read Web

        public STKWeb ReadWeb()
        {
            // Delegated to the PnPHelper
            STKPnPHelper ppHelper = new STKPnPHelper(_clientContext);
            return ppHelper.ReadWeb();
        }

        #endregion

        #endregion Methods
    }
}