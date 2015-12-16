using Strategik.Definitions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategik.Definitions.Pages
{
    /// <summary>
    /// A pointer to a set of folders and files that will be copied to the style library
    /// </summary>
    public class STKStyleLibraryAssets: STKDefinitionBase
    {
        public STKPageLocationType LocationType { get; set; }
        public String FolderPath { get; set; }
    }
}
