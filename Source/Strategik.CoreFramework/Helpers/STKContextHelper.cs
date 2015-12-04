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
using Strategik.CoreFramework.Configuration;
using System;

namespace Strategik.CoreFramework.Helpers
{
    /// <summary>
    /// Extension and helper methods for authenticating and establishing context with O365 and SP2013
    /// </summary>
    public static class STKContextHelper
    {
        private static STConfiguration _devConfig;

        //TODO: Remove credentials
        public static STConfiguration DevConfiguration
        {
            get
            {
                if (_devConfig == null)
                {
                    _devConfig = new STConfiguration()
                    {
                        O365 = true,
                        Username = "",
                        Password = "",
                        ServiceUri = new Uri("https://strategik365.sharepoint.com "),
                        LoginUri = new Uri("https://login.microsoft.com")
                    };
                }

                return _devConfig;
            }
        }

        public static ClientContext GetClientContext(bool isDev)
        {
            ClientContext clientContext = null;

            if (isDev)
            {
                clientContext = new ClientContext(DevConfiguration.ServiceUri);
                SharePointOnlineCredentials credentials = new SharePointOnlineCredentials(DevConfiguration.Username, DevConfiguration.GetSecurePasswordString());
                clientContext.Credentials = credentials;
            }

            return clientContext;
        }
    }
}