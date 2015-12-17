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
        public String Contents { get; set; }
        public byte[] FileBytes { get; set; }
        public String FileName { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
        public String Version { get; set; }
        public String LinkingUrl { get; set; }
        public int MajorVersion { get; set; }
        public int MinorVersion { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        public Dictionary<String, Object> ListItem { get; set; }
    }
}
