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
using Strategik.Definitions.Features;
using System;
using System.Collections.Generic;

namespace Strategik.Definitions.Sites
{
    /// <summary>
    /// This class defines a sharepoint site collection that we want to provision programatically
    /// </summary>
    /// <remarks>
    /// http://johnlivingstontech.blogspot.com.au/2011/01/sharepoint-spregionalsettingsglobaltime.html
    /// http://praveenbattula.blogspot.com.au/2013/05/language-local-ids-lcid-numbers-in.html
    /// </remarks>
    public class STKSite : STKDefinitionBase
    {
        #region Properties
        public bool AllowCreateDeclarativeWorkflow { get; set; }
        public bool AllowDesigner { get; set; }
        public bool AllowMasterPageEditing { get; set; }
        public bool AllowRevertFromTemplate { get; set; }
        public bool AllowSaveDeclarativeWorkflowAsTemplate { get; set; }
        public bool AllowSavePublishDeclarativeWorkflow { get; set; }
        public bool AllowSelfServiceUpgrade { get; set; }
        public bool AllowSelfServiceUpgradeEvaluation { get; set; }
        public bool ReadOnly { get; set; }

        public STKWeb RootWeb { get; set; }

        //   public List<STKSubWeb> SubWebs { get; set; }
        public List<STKSandboxSolution> SandboxSolutions { get; set; }

        public String TenantRelativeURL { get; set; }

        public uint Lcid { get; set; }

        public String SiteOwnerLogin { get; set; }
        public String SecondaryContact { get; set; }
        public string Template { get; set; }

        public int TimezoneId { get; set; }

        public List<Guid> SiteFeaturesToActivate { get; set; }

        public List<Guid> SiteFeaturesToDeactivate { get; set; }

        public List<Guid> RootWebFeaturesToActivate { get; set; }

        public List<Guid> RootWebFeaturesToDeactivate { get; set; }

        public bool IsEmbedded { get; set; }

        #endregion Properties

        #region Constructors

        public STKSite()
        {
            Template = "STS#0";
            Lcid = 1033; // US (Australia gives a not supported exception)
            TimezoneId = 18; // brisvegas
            //  SubWebs = new List<STKSubWeb>();
            SandboxSolutions = new List<STKSandboxSolution>();
            SiteFeaturesToActivate = new List<Guid>();
            SiteFeaturesToDeactivate = new List<Guid>();
            RootWebFeaturesToActivate = new List<Guid>();
            RootWebFeaturesToDeactivate = new List<Guid>();
        }

        #endregion Constructors

        #region Methods

        public bool IsValidDefinition()
        {
            bool isValid = (RootWeb != null) ? true : false;
            return isValid;
        }

        #endregion Methods
    }
}