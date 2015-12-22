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
using OfficeDevPnP.Core.Diagnostics;
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
        private const String LogSource = "CoreFramework.STKWebHelper";

        #region Constructor

        public STKWebHelper(ClientContext context)
            : base(context)
        { }


        public STKWebHelper(STKAuthenticationHelper authHelper)
            : base(authHelper)
        { }

        public STKWebHelper()
            : base(new STKAuthenticationHelper())
        { }

        #endregion 

        #region Methods

        #region Ensure Web

        protected virtual void BeforeEnsureWeb(STKWeb web, STKProvisioningConfiguration config) { }

        public void EnsureWeb(STKWeb web)
        {
            EnsureWeb(web, new STKProvisioningConfiguration());
        }

        public void EnsureWeb(STKWeb web, STKProvisioningConfiguration config)
        {
            if (web == null) throw new ArgumentNullException("web");
            if (config == null) config = new STKProvisioningConfiguration();
            web.Validate();

            Log.Debug(LogSource, "Starting EnsureWeb() for web {0}", web.Name);

            BeforeEnsureWeb(web, config);

            // Deactivate web features - Should be in an helper
            foreach (Guid featureId in web.FeaturesToDeactivate)
            {
                Log.Debug(LogSource, "Deactivating web feature {0}", featureId);
                _web.DeactivateFeature(featureId);
            }

            // Activate web features
            foreach (Guid featureId in web.FeaturesToActivate)
            {
                Log.Debug(LogSource, "Activating web feature {0}", featureId);
                _web.ActivateFeature(featureId);
            }

            STKContentTypeHelper contentTypeHelper = new STKContentTypeHelper(_clientContext);
            STKSiteColumnHelper siteColumnHelper = new STKSiteColumnHelper(_clientContext);

            // Ensure Site Columns & Content Types
            siteColumnHelper.EnsureSiteColumns(web.SiteColumns, config);
            contentTypeHelper.EnsureContentTypes(web.ContentTypes, config);

            // Ensure Lists
            STKListHelper listHelper = new STKListHelper(_clientContext);
            listHelper.EnsureLists(web.Lists, config);

            // Ensure Pages
            STKPageHelper pageHelper = new STKPageHelper(_clientContext);
            pageHelper.EnsureMasterPages(web.MasterPages, config);
            pageHelper.EnsurePageLayouts(web.PageLayouts, config);
            pageHelper.EnsureStyleLibraryAssets(web.StyleLibraryAssetLocations, config);
            pageHelper.EnsurePages(web.PublishingPages, config);
            pageHelper.EnsurePages(web.WebPartPages, config);
            pageHelper.EnsurePages(web.WikiPages, config);
         
            // Ensure Content - TODO

            // Recursively provision subwebs
            foreach (STKWeb subWeb in web.SubWebs) 
            {
                Log.Debug(LogSource, "Found subweb {0}, attemptng to provision", subWeb.Name);
                ProvisionSubWeb(subWeb, config);
            }

            AfterEnsureWeb(web, config);
            Log.Debug(LogSource, "EnsureWeb() complete for web {0}", web.Name);
        }

        protected virtual void AfterEnsureWeb(STKWeb web, STKProvisioningConfiguration config) { }

        protected virtual void BeforeProvisionSubWeb(STKWeb subWeb, STKProvisioningConfiguration config) { }

        protected void ProvisionSubWeb(STKWeb subWeb, STKProvisioningConfiguration config) 
        {
            Log.Debug(LogSource, "Provisioning subweb {0}", subWeb.Name);

            BeforeProvisionSubWeb(subWeb, config);

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

                Log.Debug(LogSource, "Subweb {0} does not exist, attempting to create with title = {1}, Description = {2}, Language = {3}, Url = {4}, UseSamePermissionsAsParentSite = {5}, WebTemplate = {6}",
                                              subWeb.Name, subWeb.Title, subWeb.Description, subWeb.Language, subWeb.LeafUrl, subWeb.UseSamePermissionsAsParent, subWeb.template);

                _clientContext.Web.Webs.Add(wci);
                _clientContext.ExecuteQueryRetry();

                spSubWeb = _clientContext.Web.GetWeb(subWeb.LeafUrl); // why do we do this??
                _clientContext.ExecuteQueryRetry();

                Log.Debug(LogSource, "Subweb {0} created succeffully", spSubWeb.Url);
            }
            else
            {
                Log.Debug(LogSource,"Subweb {0} already exists, skipping provisioning", subWeb.Name);
            }

            ClientContext childContext = new ClientContext(spSubWeb.Url);
            childContext.Credentials = _clientContext.Credentials;
            STKWebHelper childWebHelper = new STKWebHelper(childContext);
            childWebHelper.EnsureWeb(subWeb, config);

            AfterProvisionSubWeb(subWeb, config);
        }

        protected virtual void AfterProvisionSubWeb(STKWeb subWeb, STKProvisioningConfiguration config)
        { }

        #endregion

        #region Read Web

        public STKWeb ReadWeb()
        {
            // Delegated to the PnPHelper
            STKPnPHelper pnpHelper = new STKPnPHelper(_clientContext);
            return pnpHelper.ReadWeb();
        }

        #endregion

        #endregion Methods
    }
}