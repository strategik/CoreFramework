
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

using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.Online.SharePoint.TenantManagement;
using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core.Entities;
using Strategik.Definitions.Configuration;
using Strategik.Definitions.Sites;
using Strategik.Definitions.Solutions;
using Strategik.Definitions.Taxonomy;
using Strategik.Definitions.Tenant;
using Strategik.CoreFramework.Configuration;
using Strategik.CoreFramework.Enumerations;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Strategik.CoreFramework.Helpers
{
    /// <summary>
    /// Helper class for manipulating O365 tenant
    /// </summary>
    /// <remarks>
    /// Use this helper for tenant level operations susch as creating or deleting site collections, retreiving lists
    /// of the available site collections in a tenancy and so on.
    /// To execute code at the tenant level the user id and password or app credentials supplied must have tenant admin permissions.
    /// </remarks>
    public class STKTenantHelper
    {
        #region Data

        protected Office365Tenant _office365Tenant;
        protected Tenant _tenant;
        protected String _adminUrl;
        protected string _sharePointUrl;
        protected ClientContext _context;
        protected STKAuthenticationHelper _authHelper;
        protected List<String> _siteUrls;
        protected List<STKSiteProperties> _siteProperties;

        #endregion Data

        #region Constructor

        public STKTenantHelper(String adminUrl, String sharePointUrl, string userName, string password)
        {
            _authHelper = new STKAuthenticationHelper(adminUrl, sharePointUrl, userName, password, STKTarget.Office_365);
            _adminUrl = adminUrl;
            _sharePointUrl = sharePointUrl;
            Initialise(false);
        }


        public STKTenantHelper(String adminUrl, String sharePointUrl, ClientContext adminContext, bool loadSiteCollections)
        {
            _adminUrl = adminUrl;
            _sharePointUrl = sharePointUrl;
            _context = adminContext;
            Initialise(loadSiteCollections);
        }

        public STKTenantHelper(STKAuthenticationHelper authHelper)
        {
            if (authHelper == null) throw new ArgumentNullException("authHelper");
            Initialise(false);
        }

        #endregion Constructor

        #region Public Methods

        /// <summary>
        /// Extension point 
        /// </summary>
        /// <param name="site"></param>
        /// <param name="config"></param>
        protected virtual void BeforeCreateSite(STKSite site, STKProvisioningConfiguration config)
        {
            // Extend this class and do whatever you want here
        }

        /// <summary>
        /// Create the specified site collection or updates its contents if it already exists
        /// </summary>
        /// <param name="site">Strategik site definition</param>
        /// <param name="config">Configuration options</param>
        public void CreateSite(STKSite site, STKProvisioningConfiguration config = null)
        {
           
            bool provisionSite = false;

            // Apply the configuration
            if (config == null) config = new STKProvisioningConfiguration();
            ApplyConfiguration(site, config);

            BeforeCreateSite(site, config);

            // Validate the updated site definition
            site.Validate();

            String fullUrl = _sharePointUrl + site.TenantRelativeURL;

            // Check if this site already exists in the tenant
            if (SiteExists(site))
            {
                if(config.DeleteExistingSites)
                {
                    DeleteSite(site, false); // bye bye data !
                    provisionSite = true; // we will need to recreate it
                }
            }
            else
            {
                provisionSite = true; // It doesnt exist to create it
            }
           
            // Provision the site
            if (provisionSite) 
            {
                // The site does not exist so create it
                SiteEntity siteEntity = new SiteEntity()
                {
                    Title = (String.IsNullOrEmpty(site.DisplayName)) ? site.Name : site.DisplayName,
                    Description = site.Description,
                    Lcid = site.Lcid,
                    SiteOwnerLogin = site.SiteOwnerLogin,
                    Template = site.Template,
                    TimeZoneId = site.TimezoneId,
                    Url = fullUrl
                };

                Guid siteId = _tenant.CreateSiteCollection(siteEntity, true, true);
                site.UniqueId = siteId;
            }

            // Update the site with our definition (if we have one)
            if (config.EnsureSite && site.RootWeb != null)
            {
                ClientContext context = _authHelper.GetClientContext(fullUrl);
                STKSiteHelper siteHelper = new STKSiteHelper(context);
                siteHelper.EnsureSite(site, config);
            }

            AfterCreateSite(site, config);
        }

        protected virtual void AfterCreateSite(STKSite site, STKProvisioningConfiguration config)
        {
            // Extend this class and do whatever you want here
        }

        public void EnsureTenantCustomisations(STKTenantCustomisations tenantCustomisations, STKProvisioningConfiguration config = null) 
        {
            if (tenantCustomisations == null) throw new ArgumentNullException("tennantCustomisations");
            if (config == null) config = new STKProvisioningConfiguration();
            // TODO:
        }
        
        public void EnsureTaxonomy(STKTaxonomy taxonomy, STKProvisioningConfiguration config) 
        {
            if (taxonomy == null) throw new ArgumentNullException("taxonomy");
            if (config == null) config = new STKProvisioningConfiguration();

             STKTaxonomyHelper taxonomyHelper = new STKTaxonomyHelper(_context);
             taxonomyHelper.EnsureTaxonomy(taxonomy);
        }
      
        public bool SiteExists(STKSite site) 
        {
            bool exists = false;

            String fullUrl = _sharePointUrl + site.TenantRelativeURL;
            String status = "Active";
            try
            {
                exists = (_tenant.CheckIfSiteExists(fullUrl, status)) ? true : false;
            }
            catch
            {
                exists = false;
            }

            return exists;
        }

        public Site GetSharePointSite(STKSite site) 
        {
            if (site == null) throw new ArgumentNullException("site");
            Site spSite = null;

            String fullUrl = _sharePointUrl + site.TenantRelativeURL;
            String status = "Active";

            if (_tenant.CheckIfSiteExists(fullUrl, status))
            {
                spSite = _tenant.GetSiteByUrl(fullUrl);
            }

            return spSite;
        }

        #endregion Public Methods

        #region Provisioning

        protected virtual void BeforeInstallSolution(STKSolution solution, STKProvisioningConfiguration config)
        {
        }

        /// <summary>
        /// Provisions a Strategik solution to an Office 365 tennant
        /// </summary>
        /// <remarks>
        /// Progressively provisions solution level artefacts, taxonomy, site
        /// collections, sites and the artefacts that they contain. Uses a mixture
        /// of hand crafted code and the PnP provisioning engine (where it is possible to do so)
        /// </remarks>
        /// <param name="solution">The definition of the solution to provision</param>
        /// <param name="config">Configuration to apply during provisioning</param>
        /// <returns>True if the solution is provisioned successfully</returns>
        public void Install(STKSolution solution, STKProvisioningConfiguration config = null) 
        {
            if (config == null) config = new STKProvisioningConfiguration();

            BeforeInstallSolution(solution, config);

            // Ensures we have been passed a valid solution definition
            solution.Validate();

            // Apply any tennant wide customisations
            if (solution.HasTennantCustomisations())
            {
                EnsureTenantCustomisations(solution.TennantCustomisations, config);
            }

            // Apply any global taxonomy
            if (solution.HasTaxonomy())
            {
                foreach (STKTaxonomy taxonomy in solution.Taxonomy)
                {
                    EnsureTaxonomy(taxonomy, config);
                }  
            }

            config.Solution = solution; // As we go down the tree we might need this !

            // Create each site collection specified in the solution
            foreach (STKSite site in solution.Sites)
            {
                CreateSite(site, config);
            }

            AfterInstallSolution(solution, config);
        }

        protected virtual void AfterInstallSolution(STKSolution solution, STKProvisioningConfiguration config)
        {
        }

        #endregion

        #region Permissions Checks

        #endregion

        #region Delete Sites

        public void DeleteSite(STKSite site, bool useRecycleBin)
        {
            String fullUrl = _sharePointUrl + site.TenantRelativeURL;

            if (SiteExists(site))
            {
                _tenant.DeleteSiteCollection(fullUrl, useRecycleBin);
            }
        }

        #endregion

        #region Get Site Properties

        public List<STKSiteProperties> GetAllSitesProperties()
        {
            return _siteProperties;
        }

        #endregion

        #region Implementation

        private void Initialise(bool loadSiteCollections) 
        {
            if (_tenant == null) LoadO365Tenant(loadSiteCollections);
        }

        private void LoadO365Tenant(bool loadSiteCollections)
        {
            if (_context == null)
            {
                _context = _authHelper.GetAdminContext();
            }
            _office365Tenant = new Office365Tenant(_context);
            _context.Load(_office365Tenant);
            _context.ExecuteQueryRetry();

            _tenant = new Tenant(_context);
            _context.Load(_tenant);
            _context.ExecuteQueryRetry();

            if (loadSiteCollections)
            {
                LoadSiteCollections();
            }
        }

        private void LoadSiteCollections()
        {
            _siteUrls = new List<string>();
            _siteProperties = new List<STKSiteProperties>();

            String rootSiteUrl = _tenant.RootSiteUrl;
            SPOSitePropertiesEnumerable spp = null;

            int total = 0;
            int current = 0;

            // Iterate site collections
            while (spp == null || spp.Count > 0)
            {
                spp = _tenant.GetSiteProperties(current, true);
                _context.Load(spp);
                _context.ExecuteQuery();

                total += spp.Count;

                foreach (SiteProperties sp in spp)
                {
                    Console.WriteLine(sp.Url + " last modifed on " + sp.LastContentModifiedDate.ToUniversalTime() +
                                      " Represents a site collection within a tenant, including a top-level website and all its subsites.");

                    _siteUrls.Add(sp.Url);

                    _siteProperties.Add(new STKSiteProperties()
                    {
                        ContentLastModifiedDate = sp.LastContentModifiedDate,
                        Lcid = sp.Lcid,
                        MaxStorageQuota = sp.StorageMaximumLevel,
                        Owner = sp.Owner,
                        SharingCapability = sp.SharingCapability.ToString(),
                        Status = sp.Status,
                        StorageUseage = sp.StorageUsage,
                        StorageWarning = sp.StorageWarningLevel,
                        Template = sp.Template,
                        TimeZoneId = sp.TimeZoneId,
                        Title = sp.Title,
                        Url = sp.Url
                    });

                    // Update progress
                    current++;

                    
                }
            }
        }

        private void ApplyConfiguration(STKSite site, STKProvisioningConfiguration config)
        {
            // Overide various aspects of the definition with items specified in the configuration
            if (!String.IsNullOrEmpty(config.PrimarySiteCollectionAdministrator)) site.SiteOwnerLogin = config.PrimarySiteCollectionAdministrator;
            if (!String.IsNullOrEmpty(config.TenantRelativeUrl)) site.TenantRelativeURL = config.TenantRelativeUrl;
            if (config.Locale.HasValue) site.Lcid = config.Locale.Value;
            if (config.TimeZone.HasValue) site.TimezoneId = config.TimeZone.Value;
        }

        #endregion Implementation
    }
}
