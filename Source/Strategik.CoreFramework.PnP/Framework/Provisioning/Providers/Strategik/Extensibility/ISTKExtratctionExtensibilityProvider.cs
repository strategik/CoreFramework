using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core.Framework.Provisioning.Model;
using Strategik.CoreFramework.PnP.Framework.Provisioning.Providers.Strategik.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategik.CoreFramework.PnP.Framework.Provisioning.Providers.Strategik.Extensibility
{
    /// <summary>
    /// An netrface for providers to extend the default template extraxtion in the PnP
    /// </summary>
    interface ISTKExtratctionExtensibilityProvider
    {
        ProvisioningTemplate ExtractTemplate(Web web, ProvisioningTemplate pnpTemplate, string configurationData);
    }
}
