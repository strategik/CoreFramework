using OfficeDevPnP.Core.Framework.Provisioning.Model;
using System;
using System.IO;
using System.Xml.Linq;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Xml
{
    /// <summary>
    /// Helper class that abstracts from any specific version of XMLPnPSchemaFormatter
    /// </summary>
    public class XMLPnPSchemaFormatter : ITemplateFormatter
    {
        private TemplateProviderBase _provider;

        public void Initialize(TemplateProviderBase provider)
        {
            this._provider = provider;
        }

        #region Static methods and properties

        /// <summary>
        /// Static property to retrieve an instance of the latest XMLPnPSchemaFormatter
        /// </summary>
        public static ITemplateFormatter LatestFormatter
        {
            get
            {
                return (new XMLPnPSchemaV201505Formatter());
            }
        }

        /// <summary>
        /// Static method to retrieve a specific XMLPnPSchemaFormatter instance
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        public static ITemplateFormatter GetSpecificFormatter(XMLPnPSchemaVersion version)
        {
            switch (version)
            {
                case XMLPnPSchemaVersion.V201503:
                    return (new XMLPnPSchemaV201503Formatter());

                case XMLPnPSchemaVersion.V201505:
                    return (new XMLPnPSchemaV201505Formatter());

                default:
                    return (new XMLPnPSchemaV201505Formatter());
            }
        }

        /// <summary>
        /// Static method to retrieve a specific XMLPnPSchemaFormatter instance
        /// </summary>
        /// <param name="namespaceUri"></param>
        /// <returns></returns>
        public static ITemplateFormatter GetSpecificFormatter(string namespaceUri)
        {
            switch (namespaceUri)
            {
                case XMLConstants.PROVISIONING_SCHEMA_NAMESPACE_2015_03:
                    return (new XMLPnPSchemaV201503Formatter());

                case XMLConstants.PROVISIONING_SCHEMA_NAMESPACE_2015_05:
                    return (new XMLPnPSchemaV201505Formatter());

                default:
                    return (new XMLPnPSchemaV201505Formatter());
            }
        }

        #endregion Static methods and properties

        #region Abstract methods implementation

        public bool IsValid(Stream template)
        {
            ITemplateFormatter formatter = this.GetSpecificFormatterInternal(ref template);
            formatter.Initialize(this._provider);
            return (formatter.IsValid(template));
        }

        public Stream ToFormattedTemplate(ProvisioningTemplate template)
        {
            ITemplateFormatter formatter = LatestFormatter;
            formatter.Initialize(this._provider);
            return (formatter.ToFormattedTemplate(template));
        }

        public ProvisioningTemplate ToProvisioningTemplate(Stream template)
        {
            return (this.ToProvisioningTemplate(template, null));
        }

        public ProvisioningTemplate ToProvisioningTemplate(Stream template, String identifier)
        {
            ITemplateFormatter formatter = this.GetSpecificFormatterInternal(ref template);
            formatter.Initialize(this._provider);
            return (formatter.ToProvisioningTemplate(template, identifier));
        }

        #endregion Abstract methods implementation

        #region Helper Methods

        private ITemplateFormatter GetSpecificFormatterInternal(ref Stream template)
        {
            if (template == null)
            {
                throw new ArgumentNullException("template");
            }

            // Create a copy of the source stream
            MemoryStream sourceStream = new MemoryStream();
            template.Position = 0;
            template.CopyTo(sourceStream);
            sourceStream.Position = 0;
            template = sourceStream;

            XDocument xml = XDocument.Load(template);
            template.Position = 0;

            String targetNamespaceUri = xml.Root.Name.NamespaceName;

            if (!String.IsNullOrEmpty(targetNamespaceUri))
            {
                return (GetSpecificFormatter(targetNamespaceUri));
            }
            else
            {
                return (LatestFormatter);
            }
        }

        #endregion Helper Methods
    }
}