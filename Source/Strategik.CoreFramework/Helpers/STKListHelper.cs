using Microsoft.SharePoint.Client;
using Strategik.Definitions.ContentTypes;
using Strategik.Definitions.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategik.CoreFramework.Helpers
{
    public class STKListHelper: STKHelperBase
    {
        #region Data

        private List _spList;

        #endregion

        #region Constructor

        public STKListHelper(ClientContext context, List spList)
            : base(context)
        {
            if (spList == null) throw new ArgumentNullException("spList");
            _spList = spList;
        }

        #endregion


        #region Methods

        public void WriteDataAsContentType(STKItem item, STKData data)
        {

        }

        public void WriteDataAsListItem(STKData data)
        {

        }


        #endregion
    }
}
