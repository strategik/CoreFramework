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
using Strategik.Definitions.Pages;
using System;
using System.Collections.Generic;

namespace Strategik.CoreFramework.Helpers
{
    /// <summary>
    /// A helper for manipulating the various different types of pages we have
    /// </summary>
    public class STKPageHelper : STKHelperBase
    {
        #region Constructor

        public STKPageHelper(ClientContext clientContext)
            : base(clientContext)
        {
            if (clientContext == null) throw new ArgumentNullException("Client Context");
        }

        #endregion Constructor

        #region Implementation Methods

        public void EnsurePages(List<STKPublishingPage> pages, STKProvisioningConfiguration config = null)
        {
            foreach (STKPublishingPage page in pages)
            {
                _web.AddPublishingPage(page.Name, page.PageLayout, page.PageTitle, page.PublishOnProvisioning);
            }
        }

        public void EnsurePages(List<STKWebPartPage> pages, STKProvisioningConfiguration config = null)
        {
            foreach (STKWebPartPage page in pages)
            {
                //TODO
            }
        }

        public void EnsurePages(List<STKWikiPage> pages, STKProvisioningConfiguration config = null)
        {
            foreach (STKWikiPage page in pages)
            {
                //TODO
            }
        }

        #endregion Implementation Methods
    }
}