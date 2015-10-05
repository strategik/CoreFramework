using Strategik.Definitions.O365.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategik.Definitions.O365.Taxonomy
{
    public class STKTermGroupDelta : STKO365DeltaBase
    {
        public List<STKTermSetDelta> TermSetsDelta { get; set; }

        public STKTermGroupDelta(STKTermGroup termGroup)
            : base(termGroup)
        {
            TermSetsDelta = new List<STKTermSetDelta>();
        }
    }
}
