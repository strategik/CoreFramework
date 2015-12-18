using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategik.Definitions.Security.Permissions
{
    public class STKFullControlPermissionLevel: STKPermissionLevel
    {
        public STKFullControlPermissionLevel()
        {
            base.Permissions.Add(STKPermission.FullMask);
        }
    }
}
