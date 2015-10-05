using OfficeDevPnP.Core.Framework.Provisioning.Connectors;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Json
{
    public class JsonAzureStorageTemplateProvider : JsonTemplateProvider
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public JsonAzureStorageTemplateProvider()
            : base()
        {
        }

        public JsonAzureStorageTemplateProvider(string connectionString, string container) :
            base(new AzureStorageConnector(connectionString, container))
        {
        }
    }
}