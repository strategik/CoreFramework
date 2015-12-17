using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategik.Definitions.Files
{
    /// <summary>
    /// Utility class for wrapping a file that we read from or write to Office 365
    /// </summary>
    public class STKFile
    {
        public Byte[] FileBytes { get; set; }
        public String FileName { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
        public String Version { get; set; }
    }
}
