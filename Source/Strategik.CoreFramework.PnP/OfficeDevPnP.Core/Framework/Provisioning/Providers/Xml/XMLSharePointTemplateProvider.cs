using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core.Framework.Provisioning.Connectors;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Xml
{
    public class XMLSharePointTemplateProvider : XMLTemplateProvider
    {
        public XMLSharePointTemplateProvider(ClientRuntimeContext cc, string connectionString, string container) :
            base(new SharePointConnector(cc, connectionString, container))
        {
        }
    }
}