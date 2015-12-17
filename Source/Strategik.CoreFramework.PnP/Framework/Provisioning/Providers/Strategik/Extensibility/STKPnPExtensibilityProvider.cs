using OfficeDevPnP.Core.Framework.Provisioning.Extensibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core.Framework.Provisioning.Model;
using OfficeDevPnP.Core.Diagnostics;
using Strategik.CoreFramework.PnP.Framework.Provisioning.Providers.Strategik.Model;

namespace Strategik.CoreFramework.PnP.Framework.Provisioning.Providers.Strategik.Extensibility
{
    /// <summary>
    /// Called after the Pnp provisioing engine completes its work
    /// </summary>
    public class STKPnPExtensbilityProvider : IProvisioningExtensibilityProvider
    {
        const string LogSource = "Strategik.CoreFramework.PnP.STKPnPExtensibilityProvider";
        public void ProcessRequest(ClientContext ctx, ProvisioningTemplate template, string configurationData)
        {
            STKPnPTemplate stkTemplate = template as STKPnPTemplate;
            Log.Debug(LogSource, "Provisioning provider called " + configurationData);
        }
    }
}
