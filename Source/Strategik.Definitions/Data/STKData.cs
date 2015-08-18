using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategik.Definitions.Data
{
    public class STKData
    {
        public List<STKDataItem> Items { get; set; }

        public STKData()
        {
            Items = new List<STKDataItem>();
        }
    }
}
