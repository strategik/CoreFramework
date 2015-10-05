using Newtonsoft.Json;
using OfficeDevPnP.Core.Framework.Provisioning.Model;
using System;
using System.IO;
using System.Text;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Json
{
    public class JsonPnPFormatter : ITemplateFormatter
    {
        private TemplateProviderBase _provider;

        public void Initialize(TemplateProviderBase provider)
        {
            this._provider = provider;
        }

        public bool IsValid(Stream template)
        {
            // We do not provide JSON validation capabilities
            return (true);
        }

        public Stream ToFormattedTemplate(ProvisioningTemplate template)
        {
            String jsonString = JsonConvert.SerializeObject(template);
            Byte[] jsonBytes = Encoding.Unicode.GetBytes(jsonString);
            MemoryStream jsonStream = new MemoryStream(jsonBytes);
            jsonStream.Position = 0;

            return (jsonStream);
        }

        public ProvisioningTemplate ToProvisioningTemplate(Stream template)
        {
            return (this.ToProvisioningTemplate(template, null));
        }

        public ProvisioningTemplate ToProvisioningTemplate(Stream template, string identifier)
        {
            StreamReader sr = new StreamReader(template, Encoding.Unicode);
            String jsonString = sr.ReadToEnd();
            ProvisioningTemplate result = JsonConvert.DeserializeObject<ProvisioningTemplate>(jsonString);
            return (result);
        }
    }
}