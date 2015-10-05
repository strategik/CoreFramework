using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategik.Definitions.Sites
{
    public class STKSiteProperties
    {
        public DateTime ContentLastModifiedDate { get; set; }
        public uint Lcid { get; set; }
        public String Owner { get; set; }
        public String SharingCapability { get; set; }
        public String Status { get; set; }
        public long StorageUseage { get; set; }
        public long MaxStorageQuota { get; set; }
        public long StorageWarning { get; set; }
        public String Template { get; set; }
        public int TimeZoneId { get; set; }
        public String Title { get; set; }
        public int WebsCount { get; set; }

        public String Url { get; set; }
    }
}
