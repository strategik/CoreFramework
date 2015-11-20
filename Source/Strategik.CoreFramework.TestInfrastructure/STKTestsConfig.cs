
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
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using OfficeDevPnP.Core.Utilities;

namespace Strategik.CoreFramework.Tests.Infrastructure
{
    // Adapated from PnP Unit Tests - TestCommon
    public static class STKTestsCommon
    {
        #region Constructor
        static STKTestsCommon()
        {
            // Read configuration data
            TenantUrl = ConfigurationManager.AppSettings["SPOTenantUrl"];
            DevSiteUrl = ConfigurationManager.AppSettings["SPODevSiteUrl"];

            if (string.IsNullOrEmpty(TenantUrl) || string.IsNullOrEmpty(DevSiteUrl))
            {
                throw new ConfigurationErrorsException("Tenant credentials in App.config are not set up.");
            }

            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["SPOCredentialManagerLabel"]))
            {
                Credentials = CredentialManager.GetSharePointOnlineCredential(ConfigurationManager.AppSettings["SPOCredentialManagerLabel"]);
            }
            else
            {
                if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["SPOUserName"]) &&
                    !String.IsNullOrEmpty(ConfigurationManager.AppSettings["SPOPassword"]))
                {
                    UserName = ConfigurationManager.AppSettings["SPOUserName"];
                    var password = ConfigurationManager.AppSettings["SPOPassword"];

                    Password = GetSecureString(password);
                    Credentials = new SharePointOnlineCredentials(UserName, Password);
                }
                else if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["OnPremUserName"]) &&
                         !String.IsNullOrEmpty(ConfigurationManager.AppSettings["OnPremDomain"]) &&
                         !String.IsNullOrEmpty(ConfigurationManager.AppSettings["OnPremPassword"]))
                {
                    Password = GetSecureString(ConfigurationManager.AppSettings["OnPremPassword"]);
                    Credentials = new NetworkCredential(ConfigurationManager.AppSettings["OnPremUserName"], Password, ConfigurationManager.AppSettings["OnPremDomain"]);
                }
                else if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["Realm"]) &&
                         !String.IsNullOrEmpty(ConfigurationManager.AppSettings["AppId"]) &&
                         !String.IsNullOrEmpty(ConfigurationManager.AppSettings["AppSecret"]))
                {
                    Realm = ConfigurationManager.AppSettings["Realm"];
                    AppId = ConfigurationManager.AppSettings["AppId"];
                    AppSecret = ConfigurationManager.AppSettings["AppSecret"];
                }
                else
                {
                    throw new ConfigurationErrorsException("Tenant credentials in App.config are not set up.");
                }
            }
        }
        #endregion

        #region Properties
        public static string TenantUrl { get; set; }
        public static string DevSiteUrl { get; set; }
        static string UserName { get; set; }
        static SecureString Password { get; set; }
        public static ICredentials Credentials { get; set; }
        static string Realm { get; set; }
        static string AppId { get; set; }
        static string AppSecret { get; set; }

        public static String AzureStorageKey
        {
            get
            {
                return ConfigurationManager.AppSettings["AzureStorageKey"];
            }
        }
        #endregion

        #region Methods
        public static ClientContext CreateClientContext()
        {
            return CreateContext(DevSiteUrl, Credentials);
        }

        public static ClientContext CreateTenantClientContext()
        {
            return CreateContext(TenantUrl, Credentials);
        }

        public static bool AppOnlyTesting()
        {
            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["Realm"]) &&
                !String.IsNullOrEmpty(ConfigurationManager.AppSettings["AppId"]) &&
                !String.IsNullOrEmpty(ConfigurationManager.AppSettings["AppSecret"]) &&
                String.IsNullOrEmpty(ConfigurationManager.AppSettings["SPOCredentialManagerLabel"]) &&
                String.IsNullOrEmpty(ConfigurationManager.AppSettings["SPOUserName"]) &&
                String.IsNullOrEmpty(ConfigurationManager.AppSettings["SPOPassword"]) &&
                String.IsNullOrEmpty(ConfigurationManager.AppSettings["OnPremUserName"]) &&
                String.IsNullOrEmpty(ConfigurationManager.AppSettings["OnPremDomain"]) &&
                String.IsNullOrEmpty(ConfigurationManager.AppSettings["OnPremPassword"]))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static ClientContext CreateContext(string contextUrl, ICredentials credentials)
        {
            ClientContext context;
            if (!String.IsNullOrEmpty(Realm) && !String.IsNullOrEmpty(AppId) && !String.IsNullOrEmpty(AppSecret))
            {
                OfficeDevPnP.Core.AuthenticationManager am = new OfficeDevPnP.Core.AuthenticationManager();
                context = am.GetAppOnlyAuthenticatedContext(contextUrl, Realm, AppId, AppSecret);
            }
            else
            {
                context = new ClientContext(contextUrl);
                context.Credentials = credentials;
            }
            return context;
        }

        private static SecureString GetSecureString(string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentException("Input string is empty and cannot be made into a SecureString", "input");

            var secureString = new SecureString();
            foreach (char c in input.ToCharArray())
                secureString.AppendChar(c);

            return secureString;
        }
        #endregion
    }
}
