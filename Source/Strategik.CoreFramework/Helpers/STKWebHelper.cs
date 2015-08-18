﻿#region License

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

        #region Provisioning

        public void Provision(STKWeb web, STKProvisioningConfiguration config)
        {
            if (web == null) throw new ArgumentNullException("web");
            if (config == null) config = new STKProvisioningConfiguration();
            
            STKPnPHelper pnpHelper = null;
            if (config.UsePnP)
            {
                pnpHelper = new STKPnPHelper(_clientContext);
                //pnpHelper.Provision() //TODO - features - one level up?
            }
        
            // Deactivate web features
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
            if (config.UsePnP && config.UsePnpForSiteColumns)
            {
                pnpHelper.Provision(web.SiteColumns, config);
            }
            else
            {
               // siteColumnHelper.EnsureSiteColumns(web.SiteColumns, config);
            }

            if (config.UsePnP && config.UsePnPForContentTypes)
            {
                pnpHelper.Provision(web.ContentTypes, config);
            }
            else
            {
               // contentTypeHelper.EnsureContentTypes(web.ContentTypes, config);
            }

            // Ensure lists & libraries
            if (config.UsePnP && config.UsePnPForLists)
            {
                pnpHelper.Provision(web.Lists, config);
            }
            else
            {
                STKListsHelper listHelper = new STKListsHelper(_clientContext);
                listHelper.EnsureLists(web.Lists, config);
            }

            STKPageHelper pageHelper = new STKPageHelper(_clientContext);
            // Ensure pages
          //  if (config.UsePnP && config.UsePnPForPublishingPages)
          //  {
                  //PNP doesnt do our pages yet  
           // }
          //  else
          //  {
                pageHelper.EnsurePages(web.PublishingPages, config);
                pageHelper.EnsurePages(web.WebPartPages, config);
                pageHelper.EnsurePages(web.WikiPages, config);
           // }

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
            childWebHelper.Provision(subWeb, config);
           
        }

        #endregion Provisioning

        #region Read Definitions

        public List<STKField> GetSiteColumns()
        {
            return _web.ReadSiteColumns();
        }

        //public List<STKContentType> ReadContenttypes()
        //{
        //    return _web.ReadContentTypes();
        //}

        public List<STKList> ReadLists()
        {
            return _web.ReadLists();
        }

        #endregion Read Definitions

        #endregion Methods
    }
}