using Strategik.Definitions.O365.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategik.Definitions.O365.Taxonomy
{
    public class STKTermDelta : STKO365DeltaBase
    {
        public List<STKTermDelta> ChildTermDeltas { get; set; }

        public STKTermDelta(STKTerm term)
            :base(term)
        {
            ChildTermDeltas = new List<STKTermDelta>();
        }
    }
}
