using OfficeDevPnP.Core.Framework.Provisioning.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategik.CoreFramework.PnP.Framework.Provisioning.Providers.Strategik.Model
{
    public class STKPnPRoleAssignment: RoleAssignment
    {
        public bool IsSharePointGroup { get; set; }
        public bool IsADGroup { get; set; }
        public bool IsUser { get; set; }
    }
}
