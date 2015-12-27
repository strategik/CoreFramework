using OfficeDevPnP.Core.Framework.Provisioning.Connectors;
using OfficeDevPnP.Core.Framework.Provisioning.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Strategik.CoreFramework.PnP.Framework.Provisioning.Providers.Strategik.Model
{
    /// <summary>
    /// Extend the PnP Provisioning Template an plug in an extsnibility provide for provisioning operations
    /// </summary>
    public class STKPnPTemplate: ProvisioningTemplate
    {
        #region Data

        private string _configuration;


        #endregion

        #region Constructors

        public STKPnPTemplate(String configuration = null): base()
        {
            _configuration = configuration;
            Define();
        }


        public STKPnPTemplate(FileConnectorBase connector, String configuration= null)
            : base(connector)
        {
            _configuration = configuration;
            Define();
        }


        private void Define()
        {
            if (String.IsNullOrEmpty(_configuration)) _configuration = "None"; 

            Provider provider = new Provider()
            {
                Configuration = _configuration,
                Assembly = Assembly.GetExecutingAssembly().FullName,
                Type = "Strategik.CoreFramework.PnP.Framework.Provisioning.Providers.Strategik.Extensibility.STKPnPExtensbilityProvider",
                Enabled = true
            };

            base.Providers.Add(provider);
        }

        #endregion
    }
}
