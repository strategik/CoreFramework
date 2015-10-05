using OfficeDevPnP.Core.Framework.Provisioning.Connectors;
using OfficeDevPnP.Core.Framework.Provisioning.Model;
using Strategik.Definitions.O365.Solutions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Strategik
{
    /// <summary>
    /// Provides P&P provisioning templates from Strategik definitions
    /// </summary>
    /// <remarks>
    /// Strategik provide a set of c# class definitions for O365 objects.
    /// This class allows those definitions to be converted into
    /// the P&P provisioning template format so that they can be provisioned (on
    /// premise or in Office 365) using the P&P core provisioning engine.
    /// </remarks>
    public abstract class STKTemplateProvider: TemplateProviderBase
    {
        #region Constructors

        public STKTemplateProvider()
            :base()
        {}

        public STKTemplateProvider(FileConnectorBase connector)
            :base(connector)
        {}

        #endregion

        #region Base class overrides

        public override List<ProvisioningTemplate> GetTemplates()
        {
            STKPnPFormatter formatter = new STKPnPFormatter();
            formatter.Initialize(this);
            return (this.GetTemplates(formatter));
        }

        public override List<ProvisioningTemplate> GetTemplates(ITemplateFormatter formatter)
        {
            List<ProvisioningTemplate> templates = new List<ProvisioningTemplate>();

            // Retrieve the solution
            STKO365Solution solution = ((StrategikDefinitionsConnector)this.Connector).Solution;
            templates.AddRange(((STKPnPFormatter)formatter).ToProvisioningTemplate(solution)); 
            return (templates);
        }

        public override ProvisioningTemplate GetTemplate(string uri)
        {
            throw new NotImplementedException();
        }

        public override ProvisioningTemplate GetTemplate(string uri, string identifier)
        {
            throw new NotImplementedException();
        }

        public override ProvisioningTemplate GetTemplate(string uri, ITemplateFormatter formatter)
        {
            throw new NotImplementedException();
        }

        public override ProvisioningTemplate GetTemplate(string uri, string identifier, ITemplateFormatter formatter)
        {
            throw new NotImplementedException();
        }


        #region TODO - Save templates

        public override void Save(Model.ProvisioningTemplate template)
        {
            throw new NotImplementedException();
        }

        public override void Save(Model.ProvisioningTemplate template, ITemplateFormatter formatter)
        {
            throw new NotImplementedException();
        }

        public override void SaveAs(Model.ProvisioningTemplate template, string uri)
        {
            throw new NotImplementedException();
        }

        public override void SaveAs(Model.ProvisioningTemplate template, string uri, ITemplateFormatter formatter)
        {
            throw new NotImplementedException();
        }

        public override void Delete(string uri)
        {
            throw new NotImplementedException();
        }

        #endregion

        #endregion

        #region Helper Methods



        #endregion
    }
}
