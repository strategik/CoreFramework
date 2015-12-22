
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
using Strategik.CoreFramework.Enumerations;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Strategik.CoreFramework.Helpers
{
    /// <summary>
    /// Helper class for authenticating to Office 365 and providing a context to work against
    /// </summary>
    /// <remarks>
    /// Provides a Client Context for Office 365 to our helper clasess. The client Context is 
    /// username / password, SharePoint provider hosted app (app only or app + user context 
    /// or Windows Azure AD application (app only or app + user context) depending on how we 
    /// configure this class. 
    /// 
    /// Reads authentication details from app / web.config for convenience so that we 
    /// dont have to hard code credentials into our application.
    /// 
    /// Based on code from the PnP unit test projects.
    /// </remarks>
    public class STKAuthenticationHelper
    {

        #region Keys - Used to read from app / web.config

        public const String STK_AppId = "STK_AppId";
        public const String STK_AppSecret = "STK_AppSecret";
        public const String STK_Realm = "STK_Realm";
        public const String STK_TenantUrl = "STK_SPOTenantUrl";
        public const String STK_SharePointOnlineUrl = "STK_SPOSharePointUrl";
        public const String STK_UserName = "STK_SPOUserName";
        public const String STK_Password = "STK_SPOPassword";
       
        #endregion

        #region Data

        private static String _appId;
        private static String _appSecret;
        private static String _realm;
        private static String _tenantUrl;
        private static String _sharePointOnlineUrl;
        private static String _userName;
        private static String _password;
        
        private ClientContext _context;
        private ClientContext _adminContext;

        private STKTarget _target;
        private STKAuthenticationMode _authenticationMode;
        private STKAppType _appType;
        private ICredentials _credentials;
        private bool _hasAdminUrl;

        #endregion

        #region Constructor

        static STKAuthenticationHelper ()
	    {
            // Preload any specified configuration values from the app.config / web.config

            _realm = ConfigurationManager.AppSettings[STK_Realm];
            _appId = ConfigurationManager.AppSettings[STK_AppId];
            _appSecret = ConfigurationManager.AppSettings[STK_AppSecret];
            _tenantUrl = ConfigurationManager.AppSettings[STK_TenantUrl];
            _sharePointOnlineUrl = ConfigurationManager.AppSettings[STK_SharePointOnlineUrl];
            _userName = ConfigurationManager.AppSettings[STK_UserName];
            _password = ConfigurationManager.AppSettings[STK_Password];
	    }

        /// <summary>
        /// Looks for for our default app id and secret in the config file and attempts app only auth 
        /// </summary>
        public STKAuthenticationHelper()
            :this(String.Empty, String.Empty, String.Empty, null, STKTarget.Office_365)
        {
            // Default is provider hosted app - client Auth flow
            _appType = STKAppType.ProviderHosted;
            _authenticationMode = STKAuthenticationMode.ClientAuthFlow;
        } // Looks for the appid and secret in app config

        /// <summary>
        /// Create an app only or client authenticated client context.
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <param name="?"></param>
        /// <param name="?"></param>
        /// <param name="credentials"></param>
        /// <param name="target"></param>
        public STKAuthenticationHelper(String appId, String appSecret, String realm, ICredentials credentials, STKTarget target)
        {
            _credentials = credentials;
            _target = target;
            
            if(credentials != null)
            {
                _authenticationMode = STKAuthenticationMode.ClientAuthFlow;
            }
            else
            {
                _authenticationMode = STKAuthenticationMode.AppOnly;
            }

            InitialiseOAuth(appId, appSecret, realm);
        }

        /// <summary>
        /// Creates cleint context based on the uRL's, user ids and passwords passed
        /// </summary>
        /// <param name="adminUrl"></param>
        /// <param name="sharePointUrl"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        public STKAuthenticationHelper(String adminUrl, String sharePointUrl, string userName, string password, STKTarget target, String domain = null)
        {
            _target = target;
            InitialiseUserIdAndPassword(adminUrl,sharePointUrl, userName, password, domain);
        }

        #endregion

        #region Public Methods

        public bool IsInitialised() 
        {
            //TODO:
            return true;
        }

        public ClientContext GetClientContext() 
        {
            return _context;
        }

        public ClientContext GetClientContext(String url)
        {
            ClientContext context = null;

            switch (_authenticationMode)
	        {
		         case STKAuthenticationMode.AppOnly:
                 break;
                
                case STKAuthenticationMode.ClientAuthFlow:
                break;

                case STKAuthenticationMode.OnlineCredentials:
                    {
                        context = new ClientContext(url);
                        SharePointOnlineCredentials credentials = new SharePointOnlineCredentials(_userName, _password.GetSecureString());
                        context.Credentials = credentials;
                        break;
                    }
                
                default:
                break;
            }
            return context;

        }

        public ClientContext GetAdminContext() 
        {
            if(_hasAdminUrl)
            {
               return _adminContext;
            }
            else
            {
                throw new Exception("No valid admin url detected");
            }
        }


        #endregion

        #region Initialisation

        private void InitialiseOAuth(String appId, String appSecret, String realm)
        {
            _appId = ValidateParameter(STK_AppId, _appId, appId);
            _appSecret = ValidateParameter(STK_AppSecret, _appSecret, appSecret);
            _realm = ValidateParameter(STK_Realm, _realm, realm);

        }

        private void InitialiseUserIdAndPassword(string adminUrl,string sharePointUrl,string userName,string password, string domain)
        {
 	        
            if(_target == STKTarget.Office_365)
            {
                _tenantUrl = ValidateParameter(STK_TenantUrl, _tenantUrl, adminUrl, false);
                ValidateTenantUrl(_tenantUrl);

                _sharePointOnlineUrl = ValidateParameter(STK_SharePointOnlineUrl, _sharePointOnlineUrl, sharePointUrl);
                _userName = ValidateParameter(STK_UserName, _userName, userName);
                _password = ValidateParameter(STK_Password, _password, password);
                _authenticationMode = STKAuthenticationMode.OnlineCredentials;

                InitialiseTenantContext();
            }
            else
            {
                //_onPremDeveloperSiteUrl= ValidateParameter(STK_OnPremDevSiteUrl, _onPremDeveloperSiteUrl, sharePointUrl);
                //_onPremDomain = ValidateParameter(STK_OnPremDomain, _onPremDomain, domain);
                //_onPremUserId = ValidateParameter(STK_OnPremUserId, _onPremUserId, userName);
                //_onPremPassword = ValidateParameter(STK_OnPremPassword, _onPremPassword, password);

                //_authenticationMode = STKAuthenticationMode.OnPremiseCredentials;
            }
        }

        private void ValidateTenantUrl(string adminUrl)
        {
            // If we have a tenant URL specified then make sure it is in the right format
            if (!String.IsNullOrEmpty(_tenantUrl)) 
            {
                Regex regex = new Regex(@"((\w+:\/\/)[-a-zA-Z0-9:@;?&=\/%\+\.\*!'\(\),\$_\{\}\^~\[\]`#|]+)");
                if (!regex.IsMatch(adminUrl.Trim()) || adminUrl.Contains("_layouts"))
                {
                    throw new Exception("Please enter a valid tenant admin URL, like https://company-admin.sharepoint.com.");
                }

            }
        }

        private void InitialiseTenantContext()
        {
 	        _hasAdminUrl = true;

            if(_authenticationMode == STKAuthenticationMode.OnlineCredentials)
            {
                _adminContext = new ClientContext(_tenantUrl);
                _adminContext.AuthenticationMode = ClientAuthenticationMode.Default;
                SharePointOnlineCredentials credentials = new SharePointOnlineCredentials(_userName, _password.GetSecureString());
                _adminContext.Credentials = credentials;
            }
            //TODO:
        }

        private String ValidateParameter(String parameterName, String appConfigValue, String parameterValue, bool required = true)
        {
            String validatedParameter = String.Empty;

            if (String.IsNullOrEmpty(parameterValue)) 
            {
                if(String.IsNullOrEmpty(appConfigValue) && required)
                {
                    // No value passed in the constuctor or app.config
                    throw new ArgumentNullException(parameterName);
                }
                else
                {
                    validatedParameter = appConfigValue;
                }
            }
            else
            {
                validatedParameter = parameterValue;
            }

            return validatedParameter;
        }

        #endregion
    }
}
