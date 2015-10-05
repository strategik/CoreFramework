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
using Newtonsoft.Json.Linq;
using Strategik.CoreFramework.Configuration;
using System;
using System.Net;

namespace Strategik.CoreFramework.Helpers
{
    /// <summary>
    /// Helper class for making O365 SharePoint REST api calls
    /// </summary>
    public static class STKRestApiHelper
    {
        /// <summary>
        /// a simple connect test to SharePoint online
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public static String PingApi(STConfiguration config, String listTitle)
        {
            String response = null;

            using (WebClient webClient = new WebClient())
            {
                SharePointOnlineCredentials credentials = new SharePointOnlineCredentials(config.Username, config.GetSecurePasswordString());
                webClient.Credentials = credentials;
                webClient.Headers.Add("X-FORMS_BASED_AUTH_ACCEPTED", "f");
                webClient.Headers.Add(HttpRequestHeader.ContentType, "application/json;odata=verbose");
                webClient.Headers.Add(HttpRequestHeader.Accept, "application/json;odata=verbose");

                Uri apiEndPoint = new Uri(config.ServiceUri, String.Format("/_api/web/lists/getbytitle('{0}')/itemcount", listTitle));

                String result = webClient.DownloadString(apiEndPoint);
                JToken jtoken = JToken.Parse(result);
                JToken list = jtoken["d"];

                response = list["ItemCount"].ToString();
            }

            return response;
        }
    }
}