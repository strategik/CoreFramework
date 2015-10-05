
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

using Strategik.Definitions.Solutions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategik.Definitions.Configuration
{
    /// <summary>
    /// Allows configuration to be passed to provisioning operations
    /// </summary>
    public class STKProvisioningConfiguration
    {
        #region Properties

        #region PnP Switches - redundant
        public bool UsePnP { get; set; }
        public bool UsePnPForTaxonomy { get; set; }
        public bool UsePnPForSecurity { get; set; }
        public bool UsePnpForSiteColumns { get; set; }
        public bool UsePnPForLists { get; set; }
        public bool UsePnPForContentTypes { get; set; }
        public bool UsePnPForSitePages { get; set; }
        public bool UsePnPForwebPartPages { get; set; }
        public bool UsePnPForPublishingPages { get; set; }

        #endregion

        public List<String> Messages { get; set; }
        public bool WhatIf { get; set; }
        public Exception ProvisioningException { get; set; }
        public STKSolution Solution { get; set; }

        #endregion

        #region Constructors

        public STKProvisioningConfiguration()
            : this(null)
        {}

        public STKProvisioningConfiguration(STKSolution solution)
        {
            Solution = solution;
            WhatIf = false;
            Messages = new List<String>();

            // Use the PnP code by default for content types, site columns, lists and libraries
            UsePnP = true;
            UsePnpForSiteColumns = true;
            UsePnPForContentTypes = true;
            UsePnPForLists = true;

        }

        #endregion
    }
}
