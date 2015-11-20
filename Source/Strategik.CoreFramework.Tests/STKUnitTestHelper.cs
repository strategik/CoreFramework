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
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Strategik.CoreFramework.Tests
{
        public static class STKUnitTestHelper
        {
            //
            // The properties we need for system testing
            // read form an app config file (not commited to source control)
            // see app.config.sample for the required format
            //
            public static String oUserName = @"*";
            private static String oPassword = "*";
            private static String oTestSiteUrl = "*";
            private static String oTenantUrl = "*";
            private static String oAdminUserName = "*";
            private static String oAdminPassword = "*";

            public static ClientContext GetTestContext()
            {
                ClientContext context = new ClientContext(oTestSiteUrl);
                context.Credentials = new SharePointOnlineCredentials(oUserName, GetSecurePassword(oPassword));
                return context;
            }

            private static SecureString GetSecurePassword(String input)
            {
                if (string.IsNullOrEmpty(input))
                    throw new ArgumentException("Input string is empty and cannot be made into a SecureString", "input");

                var secureString = new SecureString();
                foreach (char c in input.ToCharArray())
                    secureString.AppendChar(c);

                return secureString;
            }
        }
    }


