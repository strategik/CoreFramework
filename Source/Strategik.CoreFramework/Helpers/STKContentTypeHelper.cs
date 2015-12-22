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
using Strategik.Definitions.Configuration;
using Strategik.Definitions.ContentTypes;
using Strategik.Definitions.Fields;
using Strategik.CoreFramework.Configuration;
using System;
using System.Collections.Generic;
using OfficeDevPnP.Core.Framework.Provisioning.Providers.Strategik;
using OfficeDevPnP.Core.Diagnostics;

namespace Strategik.CoreFramework.Helpers
{
    public class STKContentTypeHelper : STKHelperBase
    {
        private const String LogSource = "Strategik.STKContentTypeHelper";

        #region Data

        private STKSiteColumnHelper _siteColumnHelper;

        #endregion

        #region Constructors

        public STKContentTypeHelper(ClientContext clientContext)
            : base(clientContext)
        {
            _siteColumnHelper = new STKSiteColumnHelper(clientContext);
        }

        public STKContentTypeHelper(STKAuthenticationHelper authHelper)
            :base(authHelper)
        {}

        public STKContentTypeHelper()
            :base(new STKAuthenticationHelper()) 
        {
            // Authentication details specificed in app / web.config
        }


        #endregion Constructor

        #region Ensure Content Types Methods

        public void EnsureContentTypes(List<STKContentType> contentTypes, STKProvisioningConfiguration config = null)
        {
            if (config == null) config = new STKProvisioningConfiguration();

            foreach (var contentType in contentTypes)
            {
                EnsureContentType(contentType, config);
            }
        }

        public void EnsureContentType(STKContentType contentType, STKProvisioningConfiguration config = null)
        {
            if (contentType == null) throw new ArgumentNullException("contentType");
            if (config == null) config = new STKProvisioningConfiguration();

            contentType.Validate();

            // If the content type exists we do not update it (for now)
            // as the PnP code appears to be throwing a duplicate content type exception
            ContentType spContentType = _web.GetContentTypeById(contentType.SharePointContentTypeId);
            if (spContentType != null)
            {
                Log.Info(LogSource, "Content type {0} already exists - no action taken", contentType.Name);
                return;
            }

            Log.Info(LogSource, "Provisioning content type {0}", contentType.Name);
            STKPnPHelper pnpHelper = new STKPnPHelper(_clientContext);
            pnpHelper.Provision(contentType, config);
        } 

        #endregion

        #region Read Content Types

        /// <summary>
        /// Reads the defintions for all the site columns in the current site
        /// </summary>
        /// <returns></returns>
        public List<STKContentType> ReadContentTypes()
        {
            STKPnPHelper stkPnPHelper = new STKPnPHelper(_clientContext);
            return stkPnPHelper.ReadContentTypes();
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Checks a list of STKContentType definiions to ensure there are no duplicated id's or SharePoint ids
        /// </summary>
        /// <remarks>
        /// Duplicated id's are a common cut and paste error when creating definitions.
        /// We cannot deploy two content types with the same SharePoint id
        /// </remarks>
        /// <param name="contentTypes"></param>
        public static void Validate(List<STKContentType> contentTypes)
        {
            List<Guid> idChecks = new List<Guid>();
            List<String> sharePointIdChecks = new List<String>();

            foreach (STKContentType contentType in contentTypes)
            {
                contentType.Validate();

                if (idChecks.Contains(contentType.UniqueId))
                {
                    throw new Exception("A duplicate id was detected " + contentType.UniqueId);
                }
                else
                {
                    idChecks.Add(contentType.UniqueId);
                }

                if (sharePointIdChecks.Contains(contentType.SharePointContentTypeId))
                {
                    throw new Exception("A duplicate SharePoint Id was detected " + contentType.SharePointContentTypeId);
                }
                else
                {
                    sharePointIdChecks.Add(contentType.SharePointContentTypeId);
                }
            }
        }

        #endregion
    }
}