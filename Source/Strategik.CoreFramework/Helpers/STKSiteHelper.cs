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
using Strategik.CoreFramework.Configuration;
using Strategik.Definitions.Configuration;

namespace Strategik.CoreFramework.Helpers
{
    public class STKSiteHelper : STKHelperBase
    {
        #region Constructors

        public STKSiteHelper(ClientContext context)
            : base(context)
        { }

        #endregion Constructors

        #region Methods

        #region Provision

        /// <summary>
        /// Provision the Site
        /// </summary>
        /// <remarks>
        /// First provisions the Site (i.e. the site collection level artefacts)
        /// Then calls the web helper with the root web to fill out any web scoped
        /// artefacts, create an sub sites required and so on. 
        /// Applies insert semantics - its something is not present it is added, nothing is 
        /// deleted or overwriten.
        /// </remarks>
        /// <param name="site"></param>
        public void Provision(STKSite site, STKProvisioningConfiguration config)
        {
            if (site == null) throw new ArgumentNullException("site");
            if (config == null) config = new STKProvisioningConfiguration();

            // Deactivate and site scoped features requested
            foreach (Guid featureToDeactivate in site.SiteFeaturesToDeactivate)
            {
                _site.DeactivateFeature(featureToDeactivate);
            }

            //Activate any site scoped features request
            foreach (Guid featureToActivate in site.SiteFeaturesToActivate)
            {
                _site.ActivateFeature(featureToActivate);
            }

            // Activate any Sandboxed solutions
            foreach (STKSandboxSolution solution in site.Solutions)
            {
                _site.InstallSandboxSolution(solution);
            }

            // Provision the root web (and then down to subWebs)
            if (site.RootWeb != null)
            {
                STKWebHelper webHelper = new STKWebHelper(_clientContext);
                webHelper.Provision(site.RootWeb, config);
            }
        }

        #endregion

        #region Read Definition

        public STKSite ReadDefinition() 
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
    }
}