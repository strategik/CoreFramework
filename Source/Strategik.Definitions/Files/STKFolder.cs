using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategik.Definitions.Files
{
    public class STKFolder
    {
        public List<STKFolder> Folders { get; set; }
        public List<STKFile> Files { get; set; }

        public STKFolder()
        {
            Folders = new List<STKFolder>();
            Files = new List<STKFile>();
        }
    }
}
