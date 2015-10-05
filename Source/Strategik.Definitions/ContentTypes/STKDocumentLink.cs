using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategik.Definitions.ContentTypes
{
    public class STKDocumentLink: STKContentType
    {
        #region Constructors

        public STKDocumentLink()
        {
            base.SharePointContentTypeId = "0x01010A";
            base.IsBuiltInContentType = true;
        }

        #endregion
    }
}
