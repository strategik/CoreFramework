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

using OfficeDevPnP.Core.Framework.Provisioning.Model;
using System;

namespace Strategik.Definitions.Pages
{
    public static partial class STKPageExtensions
    {
        public static Page GeneratePnPTemplate(this STKPage page)
        {
            Page pageTemplate = new Page()
            {
                Overwrite = page.OverwriteIfPresent,
                Url = page.Url,
                WelcomePage = page.IsWelcomePage
            };

            // Webpart pages
            STKWebPartPage webPartPage = page as STKWebPartPage;
            if (webPartPage != null)
            {
                // TODO Web parts
            }

            // Wiki Pages
            STKWikiPage wikiPage = page as STKWikiPage;
            if (wikiPage != null)
            {
                // TODO Wiki page layout
            }

            // No publishing page support in PnP?
            STKPublishingPage publishingPage = page as STKPublishingPage;
            if (publishingPage != null)
            {
                throw new NotImplementedException("Publishing pages not supported by the PnP template framework");
            }

            return pageTemplate;
        }
    }
}