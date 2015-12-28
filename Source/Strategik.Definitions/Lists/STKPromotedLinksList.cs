using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategik.Definitions.Lists
{
    public class STKPromotedLinksList: STKList
    {
        #region Constructors

        public STKPromotedLinksList()
            : this("STKPromotedLinksList")
        {}

        public STKPromotedLinksList(String listName)
            : base(listName)
        {
             base.ListType = STKListType.PromotedLinks;
            base.TemplateFeatureId = new Guid("192EFA95-E50C-475e-87AB-361CEDE5DD7F");
        }

        #endregion
    }
}
