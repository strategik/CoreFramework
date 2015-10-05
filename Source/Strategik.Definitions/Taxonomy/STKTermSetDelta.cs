using Strategik.Definitions.O365.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategik.Definitions.O365.Taxonomy
{
    public class STKTermSetDelta : STKO365DeltaBase
    {
        public List<STKTermDelta> TermsDelta { get; set; }

        public STKTermSetDelta(STKTermSet termSet)
            :base(termSet)
        {
            TermsDelta = new List<STKTermDelta>();
        }
    }
}
