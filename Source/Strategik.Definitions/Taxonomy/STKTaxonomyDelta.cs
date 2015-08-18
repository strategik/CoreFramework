using Strategik.Definitions.O365.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategik.Definitions.O365.Taxonomy
{
    /// <summary>
    /// Records the changes made by a taxonomy operation
    /// </summary>
    public class STKTaxonomyDelta: STKO365DeltaBase
    {
        public List<STKTermGroupDelta> TermGroupsDelta { get; set; }

        public STKTaxonomyDelta(STKTaxonomy taxonomy)
            : base(taxonomy)
        {
            TermGroupsDelta = new List<STKTermGroupDelta>();
        }
    }
}
