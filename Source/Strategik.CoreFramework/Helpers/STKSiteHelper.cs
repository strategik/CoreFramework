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
using Strategik.Definitions.Sites;
using Strategik.Definitions.Features;
using System;
using System.Linq;
using Strategik.CoreFramework.Configuration;
using Strategik.Definitions.Configuration;
using System.Collections.Generic;
using OfficeDevPnP.Core.Diagnostics;
using System.Threading;

namespace Strategik.CoreFramework.Helpers
{
    /// <summary>
    /// Helper methods for working with Site Collections in SharePoint Online
    /// </summary>
    public class STKSiteHelper : STKHelperBase
    {
        private const String LogSource = "CoreFramework.STKSiteHelper";

        #region Constructors

        /// <summary>
        /// Constructs the helper
        /// </summary>
        /// <param name="context">The context for the site collection to work against</param>
        public STKSiteHelper(ClientContext context)
            : base(context)
        { }

        public STKSiteHelper(STKAuthenticationHelper authHelper)
            : base(authHelper)
        { }

        public STKSiteHelper()
            : base(new STKAuthenticationHelper())
        { }

        #endregion

        #region Methods

        #region Ensure Site

        protected virtual void BeforeEnsureSite(STKSite site, STKProvisioningConfiguration config)
        {
            Log.Debug(LogSource, "Before EnsureSite");
        }

        /// <summary>
        /// Provision the Site
        /// </summary>
        /// <remarks>
        /// First provisions the Site (i.e. the site collection level artefacts)
        /// Then calls the web helper with the root web to fill out any web scoped
        /// artefacts, create an sub sites required and so on. 
        /// Applies insert semantics - its something is not present it is added, nothing is 
        /// deleted or overwriten.
        /// 
        /// The site must already exist. To create a site use the tenant helper class
        /// </remarks>
        /// <param name="site"></param>
        public void EnsureSite(STKSite site, STKProvisioningConfiguration config)
        {
            
            if (site == null) throw new ArgumentNullException("site");
            if (config == null) config = new STKProvisioningConfiguration();

            site.Validate();
            Log.Debug(LogSource, "Starting EnsureSite() with site {0}", site.Name);

            BeforeEnsureSite(site, config);

            DeactivateSiteFeatures(site.SiteFeaturesToDeactivate);
            ActivateSiteFeatures(site.SiteFeaturesToActivate);
            InstallSandboxedSolutions(site.SandboxSolutions);
            
            // Provision the root web (and then down to subWebs)
            if (site.RootWeb != null)
            {
                Log.Debug(LogSource, "Root Web detected, attempting to ensure root web features");
                STKWebHelper webHelper = GetWebHelper(_clientContext);
                webHelper.EnsureWeb(site.RootWeb, config);
            }

            AfterEnsureSite(site, config);

            Log.Debug(LogSource, "EnsureSite() complete");
        }

        public void EnsureSite(STKSite site) 
        {
            STKProvisioningConfiguration config = new STKProvisioningConfiguration();
            EnsureSite(site, config);
        }

        protected virtual STKWebHelper GetWebHelper(ClientContext context)
        {
            return new STKWebHelper(_clientContext);
        }

        protected virtual void AfterEnsureSite(STKSite site, STKProvisioningConfiguration config)
        {
            Log.Debug(LogSource, "After EnsureSite");
        }

        #endregion

        #region Read Site

        public STKSite ReadSite() 
        {
            _clientContext.Load(_site.Owner, o => o.LoginName);
            _clientContext.Load(_site.SecondaryContact);

            _clientContext.ExecuteQueryRetry();

            STKSite site = new STKSite() 
            {
                AllowCreateDeclarativeWorkflow = _site.AllowCreateDeclarativeWorkflow,
                AllowDesigner = _site.AllowDesigner,
                AllowMasterPageEditing = _site.AllowMasterPageEditing,
                AllowRevertFromTemplate = _site.AllowRevertFromTemplate,
                AllowSaveDeclarativeWorkflowAsTemplate = _site.AllowSaveDeclarativeWorkflowAsTemplate,
                AllowSavePublishDeclarativeWorkflow = _site.AllowSavePublishDeclarativeWorkflow,
                AllowSelfServiceUpgrade = _site.AllowSelfServiceUpgrade,
                ReadOnly = _site.ReadOnly,
                SiteOwnerLogin = _site.Owner.LoginName,
            };

            if (_site.SecondaryContact != null && _site.SecondaryContact.ServerObjectIsNull == false) site.SecondaryContact = _site.SecondaryContact.LoginName;

            return site;
        }

        #endregion

        #endregion Methods

        #region Implementation Methods

        protected void DeactivateSiteFeatures(List<Guid> siteFeaturesToDeactivate)
        {
            // Deactivate and site scoped features requested
            foreach (Guid featureToDeactivate in siteFeaturesToDeactivate)
            {
                Log.Debug(LogSource, "Deactivating site feature " + featureToDeactivate);
                _site.DeactivateFeature(featureToDeactivate);
            }
        }

        protected void ActivateSiteFeatures(List<Guid> siteFeaturesToActivate)
        {
            // Deactivate and site scoped features requested
            foreach (Guid featureToActivate in siteFeaturesToActivate)
            {
                try
                {

                    Log.Debug(LogSource, "Activating site feature " + featureToActivate);
                    _site.ActivateFeature(featureToActivate);
                }
                catch (Exception e)
                {
                    Log.Debug(LogSource, "Unexpected error activating site feature " + featureToActivate + " error message is " + e.Message);
                    int retryCount = 0;
                    while (retryCount < 3)
                    { // an ugly hack
                      //
                      // We seem to get timeouts here on the publishing feature especiallly
                      // Wait a while them try again - think the feature is activating in the
                      // background
                      //
                        Thread.Sleep(10000);
                        retryCount++;

                        try
                        {
                            Log.Debug(LogSource, "Activating site feature " + featureToActivate + " retry count is " + retryCount);
                            _site.ActivateFeature(featureToActivate);
                            break; // we have success
                        }
                        catch (Exception ex)
                        {
                            Log.Debug(LogSource, "Unexpected error activating site feature " + featureToActivate + " error message is " + e.Message + " retry count is " + retryCount);
                            if (retryCount == 3) throw ex; // Give up
                        }
                    }
                }
               // FeatureCollection features = _site.Features;
               // features.Context.Load(features);
               // features.Context.ExecuteQueryRetry();

               // features.Add(featureToActivate, true, FeatureDefinitionScope.Farm);
               // features.Context.ExecuteQueryRetry();

               // features.Context.Load(features);
               // features.Context.ExecuteQueryRetry();

               //// Block until the feature is activated - otherwise we will have problems with any dependent features e.g. publishing web
               // while (_site.IsFeatureActive(featureToActivate) == false)
               // {
               //     Log.Debug(LogSource, "Feature " + featureToActivate + " is still activating - pausing for 5 seconds");
               //     Thread.Sleep(5000);
               // }
            }
        }

        protected void InstallSandboxedSolutions(List<STKSandboxSolution> solutions)
        {
            // Activate any Sandboxed solutions
            foreach (STKSandboxSolution solution in solutions)
            {
                Log.Debug(LogSource, "Installing Sandbox solution " + solution.FileName);
                _site.InstallSandboxSolution(solution);
            }
        }

        #endregion
    }
}