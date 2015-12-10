
#region License

//
// Copyright (c) 2015 Strategik Pty Ltd,
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
//
// Author:  Dr Adrian Colquhoun
//

#endregion License

using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core.Diagnostics;
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
        private const String LogSource = "CoreFramework.STKSiteColumnHelper";

        #region Constructor

        public STKSiteColumnHelper(ClientContext clientContext)
            : base(clientContext)
        { }

        #endregion

        #region Ensure Site Columns

        public void EnsureSiteColumns(List<STKField> siteColumns, STKProvisioningConfiguration config = null)
        {
            if (siteColumns == null) throw new ArgumentNullException("siteColumns");
            if (config == null) config = new STKProvisioningConfiguration();

            foreach (STKField siteColumn in siteColumns)
            {
                EnsureSiteColumn(siteColumn, config);
            }
        }

        public void EnsureSiteColumn(STKField siteColumn, STKProvisioningConfiguration config = null)
        {
            if (siteColumn == null) throw new ArgumentNullException("siteColumn");
            if (config == null) config = new STKProvisioningConfiguration();
          
            siteColumn.Validate();

            Log.Info("Provisioning site column {0}", siteColumn.Name);
            STKPnPHelper pnpHelper = new STKPnPHelper(_clientContext);
            pnpHelper.Provision(siteColumn, config);
        }

        #region old code to remove
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
