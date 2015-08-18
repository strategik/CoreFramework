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

        #region PnP Switches
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
