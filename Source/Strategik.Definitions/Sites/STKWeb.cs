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

using Strategik.Definitions.Base;
using Strategik.Definitions.ContentTypes;
using Strategik.Definitions.Fields;
using Strategik.Definitions.Lists;
using Strategik.Definitions.Pages;
using Strategik.Definitions.Security;
using System;
using System.Collections.Generic;

namespace Strategik.Definitions.Sites
{
    /// <summary>
    /// Defines a web.
    /// </summary>
    public class STKWeb : STKDefinitionBase
    {
        #region Properties

        public List<STKGroup> SecurityGroups { get; set; }
        public List<STKField> SiteColumns { get; set; }
        public List<STKContentType> ContentTypes { get; set; }
        public List<STKList> Lists { get; set; }
        public List<STKPublishingPage> PublishingPages { get; set; }
        public List<STKWebPartPage> WebPartPages { get; set; }
        public List<STKWikiPage> WikiPages { get; set; }
        public bool UseManagedNavigation { get; set; }
        public bool EnsureNavigationTermset { get; set; }
        public bool HasSiteMailbox { get; set; }
        public List<Guid> FeaturesToActivate { get; set; }
        public List<Guid> FeaturesToDeactivate { get; set; }
        public List<STKWeb> SubWebs { get; set; }
        public String LeafUrl { get; set; }
        public int Language { get; set; }
        public bool UseSamePermissionsAsParent { get; set; }
        public String template { get; set; }

        #endregion Properties

        #region Constructors

        public STKWeb()
        {
            SiteColumns = new List<STKField>();
            ContentTypes = new List<STKContentType>();
            Lists = new List<STKList>();
            PublishingPages = new List<STKPublishingPage>();
            WebPartPages = new List<STKWebPartPage>();
            WikiPages = new List<STKWikiPage>();
            FeaturesToActivate = new List<Guid>();
            FeaturesToDeactivate = new List<Guid>();
            SubWebs = new List<STKWeb>();
            SecurityGroups = new List<STKGroup>();
        }

        #endregion Constructors
    }
}