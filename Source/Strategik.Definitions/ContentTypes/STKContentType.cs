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
//
// Author:  Dr Adrian Colquhoun
//
#endregion License

using Strategik.Definitions.Base;
using Strategik.Definitions.Fields;
using Strategik.Definitions.Lists;
using System;
using System.Collections.Generic;

namespace Strategik.Definitions.ContentTypes
{
    /// <summary>
    /// This class is used to define a SharePoint content types that we want to provision.
    /// </summary>
    /// <remarks>
    /// Content type provisioning is performed by extending existing content types. Definitions
    /// can reference existing site columns or provide definitions of new site columns
    /// to be provisioned "on the fly". 
    /// </remarks>
    public class STKContentType : STKDefinitionBase
    {
        #region Public Properties

        public string Scope { get; set; }
        public String DocumentTemplate { get; set; }
        public Boolean ProvisionAsUpdate { get; set; }
        public Boolean ProvisionAtRootSite { get; set; }
        public Boolean HideFromNewMenu { get; set; }
        public string GroupName { get; set; }
        public List<STKField> SiteColumns { get; set; }
        public List<STKFieldLink> SiteColumnLinks { get; set; }
        public List<EventHandler> EventHandlers { get; set; }
        public bool HideTitleField { get; set; }
        public STKContentType Parent { get; set; }
        public bool ReadOnly { get; set; }
        public bool Hidden { get; set; }
        public bool Sealed { get; set; }
        public bool IsBuiltInContentType { get; set; }
        public bool IsItemContentType { get; set; }
        public bool IsDocumentContentType { get; set; }
        public bool IsWorkflowTaskContentType { get; set; }
        public bool IsTaskContentType { get; set; }
        public bool IsContactContentType { get; set; }
        public String SharePointContentTypeId { get; set; }
        public bool IsFolderContentType { get; set; }
        public List<STKList> ListsToAddTo { get; set; }
        public bool CreateSearchScopeAtTenantLevel { get; set; }
        public bool CreateSearchScopeAtSiteLevel { get; set; }
        public bool OverwriteIfPresent { get; set; }
        public bool IsDefaultContentType { get; set; }

        #endregion Public Properties

        #region Constructor

        public STKContentType()
        {
            SiteColumns = new List<STKField>();
            SiteColumnLinks = new List<STKFieldLink>();
            EventHandlers = new List<EventHandler>();
            Description = string.Empty;
            Name = string.Empty;
            HideTitleField = false;
            ListsToAddTo = new List<STKList>();
        }

        #endregion Constructor
    }
}