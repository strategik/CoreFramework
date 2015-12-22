using Strategik.Definitions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategik.Definitions.Mail
{
    public class STKMailFolder
    {
        public String Name { get; set; }

        public List<STKMailFolder> SubFolders { get; set; }

        public STKMailFolder()
        {
            SubFolders = new List<STKMailFolder>();
        }

        public bool HasSubFolders()
        {
            return SubFolders.Count > 0;
        }
    }
}
