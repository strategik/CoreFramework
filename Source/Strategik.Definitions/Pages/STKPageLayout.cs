using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategik.Definitions.Pages
{
    /// <summary>
    /// A custom page layout to deploy to Office 365
    /// </summary>
    public class STKPageLayout: STKPage
    {
        public bool IsHTMLLayout { get; set; }
        public String AssociatedContentTypeId { get; set; }
    }
}
