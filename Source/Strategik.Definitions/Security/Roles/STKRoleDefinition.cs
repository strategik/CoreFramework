using Strategik.Definitions.Security.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategik.Definitions.Security.Roles
{
    public class STKRoleDefinition
    {
        public String Name { get; set; }
        public String Description { get; set; }
        public STKPermissionLevel PermissionLevel { get; set; }

        public STKRoleDefinition()
        {
            PermissionLevel = new STKPermissionLevel();
        }
    }
}
