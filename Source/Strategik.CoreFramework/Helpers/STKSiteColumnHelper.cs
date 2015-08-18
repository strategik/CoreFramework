using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core.Framework.Provisioning.Providers.Strategik;
using Strategik.CoreFramework.Helpers;
using Strategik.Definitions.Configuration;
using Strategik.Definitions.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategik.CoreFramework.Helpers
{
    public class STKSiteColumnHelper: STKHelperBase
    {
        #region Constructor

        public STKSiteColumnHelper(ClientContext clientContext)
            : base(clientContext)
        { }

        #endregion

        #region Ensure Site Columns

        public List<Field> EnsureSiteColumns(List<STKField> siteColumns, STKProvisioningConfiguration config = null)
        {
            if (siteColumns == null) throw new ArgumentNullException("siteColumns");
            if (config == null) config = new STKProvisioningConfiguration();

            List<Field> fields = new List<Field>();

            foreach (STKField siteColumn in siteColumns)
            {
                fields.Add(EnsureSiteColumn(siteColumn, config));
            }

            return fields;
        }

        public Field EnsureSiteColumn(STKField siteColumn, STKProvisioningConfiguration config = null)
        {
            if (siteColumn == null) throw new ArgumentNullException("siteColumn");
            if (config == null) config = new STKProvisioningConfiguration();
            Field field = null;

            siteColumn.Validate();

            if (config.UsePnP && config.UsePnpForSiteColumns)
            {
                STKPnPHelper pnpHelper = new STKPnPHelper(_clientContext);
                pnpHelper.Provision(siteColumn, config);
            }
            else
            {
                #region old code to remove
                throw new NotImplementedException("Provisioning of site columns is only implemented through the P&P framework");


                //// Look up the field
                //FieldCollection fields = _web.Fields;
                //_clientContext.Load(_web.Fields);
                //_clientContext.ExecuteQueryRetry();

                //foreach (var existingField in fields)
                //{
                //    if (existingField.Id == siteColumn.UniqueId) field = existingField;
                //}

                //if (field == null)
                //{
                //    if (siteColumn.SharePointType == STKFieldType.TaxonomyFieldType)
                //    {
                //        _web.CleanupTaxonomyHiddenField((STKTaxonomyField)siteColumn);
                //    }

                //    String xml = siteColumn.GetProvisioningXML();
                //    field = _web.CreateField(xml, true);

                //    // Make sure all the fields we want are loaded
                //    _clientContext.Load(field,
                //        f => f.Id,
                //        f => f.InternalName,
                //        f => f.Group);

                //    _clientContext.ExecuteQueryRetry();
                //}

                //// If the site column is a taxonomy column then we need to
                //// wire it up to its termset
                //if (siteColumn.SharePointType == STKFieldType.TaxonomyFieldType)
                //{
                //    _web.WireUpTaxonomyField(field, (STKTaxonomyField)siteColumn);
                //}
                #endregion
            }

            return field;
        }

        #endregion

        #region Read Site Columns

        /// <summary>
        /// Reads the defintions for all the site columsn in the current site
        /// </summary>
        /// <returns></returns>
        public List<STKField> ReadSiteColumns()
        {
            // Delegated to the PnPHelper
            STKPnPHelper ppHelper = new STKPnPHelper(_clientContext);
            return ppHelper.ReadSiteColumns();
        }


        #endregion
    }
}
