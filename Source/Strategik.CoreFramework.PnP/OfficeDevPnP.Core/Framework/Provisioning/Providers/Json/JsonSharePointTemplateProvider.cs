using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core.Framework.Provisioning.Connectors;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Json
{
    public class JsonSharePointTemplateProvider : JsonTemplateProvider
    {
        public JsonSharePointTemplateProvider()
            : base()
        {
        }

        public JsonSharePointTemplateProvider(ClientRuntimeContext cc, string connectionString, string container) :
            base(new SharePointConnector(cc, connectionString, container))
        {
        }
    }
}