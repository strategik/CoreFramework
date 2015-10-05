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

using System;
using System.Collections.Generic;

namespace Strategik.Definitions.Features
{
    // http://social.technet.microsoft.com/wiki/contents/articles/14423.sharepoint-2013-existing-features-guid.aspx
    public class STKFeatures
    {
        #region Constants

        #region Site Features

        public readonly static Guid PublishingSiteFeature = new Guid("{f6924d36-2fa8-4f0b-b16d-06b7250180fa}");

        #endregion Site Features

        #region Web Features

        public readonly static Guid MDSFeature = new Guid("{87294c72-f260-42f3-a41b-981a2ffce37a}");
        public readonly static Guid PublishingWebFeature = new Guid("94c94ca6-b32f-4da9-a9e3-1f3d343d7ecb");

        #endregion Web Features

        #endregion Constants

        #region Properties

        public List<STKFeature> SiteFeatures { get; set; }

        public List<STKFeature> WebFeatures { get; set; }

        #endregion Properties

        #region Constructor

        public STKFeatures()
        {
            SiteFeatures = new List<STKFeature>();
            WebFeatures = new List<STKFeature>();
        }

        #endregion Constructor
    }
}