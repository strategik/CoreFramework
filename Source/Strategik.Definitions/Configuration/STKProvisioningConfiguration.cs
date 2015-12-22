
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
    /// <remarks>
    /// We often wish to apply different provisioning configuration rules to the same logical
    /// definition. We specify provisioning overrides using this class.
    /// </remarks>
    public class STKProvisioningConfiguration
    {
        #region Properties

        #region Site Collection Overrides

        /// <summary>
        /// Set to true to delete existing site collections during provisioning
        /// </summary>
        public bool DeleteExistingSites { get; set; }

        /// <summary>
        /// Set to trur to skip the recycle bin on a site collection delete
        /// </summary>
        public bool ForceSiteDelete { get; set; }

        /// <summary>
        /// The primamry site colelction adminstrator to use during provisioning
        /// </summary>
        public String PrimarySiteCollectionAdministrator { get; set; }

        /// <summary>
        /// The secondary site collection administrator
        /// </summary>
        public String SecondarySiteCollectionAdminisitrator { get; set;}

        /// <summary>
        /// Override the tenant relative URL when provisioning
        /// </summary>
        public String TenantRelativeUrl { get; set; }

        /// <summary>
        /// Timezone
        /// </summary>
        public int? TimeZone { get; set; }

        /// <summary>
        /// Locale
        /// </summary>
        public uint? Locale { get; set; }

        /// <summary>
        /// Set to true to call EnsureSite() once a new site collection has been created - otherwise stops at empty site collections
        /// </summary>
        public bool EnsureSite { get; set; }

        public bool ContinueOnError { get; set; }

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="solution">the solution that this configuration applies to</param>
        public STKProvisioningConfiguration(STKSolution solution)
        {
            Solution = solution;
            WhatIf = false;
            Messages = new List<String>();

          
        }

        #endregion

        #region Has Property Checks

        public bool HasPrimarySiteCollectionAdministrator()
        {
            return String.IsNullOrEmpty(PrimarySiteCollectionAdministrator) ? false : true;
        }

        public bool HasSecondarySiteCollectionAdministrator()
        {
            return String.IsNullOrEmpty(SecondarySiteCollectionAdminisitrator) ? false : true;
        }

        public bool HasTenantRelativeUrl()
        {
            return String.IsNullOrEmpty(TenantRelativeUrl) ? false : true;
        }

        #endregion
    }
}
