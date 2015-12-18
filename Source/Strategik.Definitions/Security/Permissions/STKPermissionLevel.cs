using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategik.Definitions.Security.Permissions
{
    public class STKPermissionLevel
    {
        public List<STKPermission> Permissions { get; set; }

        public STKPermissionLevel()
        {
            Permissions = new List<STKPermission>();
        }
    }
}
