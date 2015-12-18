using Strategik.Definitions.Security.Principals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategik.Definitions.Security.Roles
{
    public class STKRoleAssignment
    {
        /// <summary>
        /// Defines the Role to which the assignment will apply
        /// </summary>
        public STKPrincipal Principal { get; set; }

        /// <summary>
        /// Defines the Role to which the assignment will apply
        /// </summary>
        public STKRoleDefinition RoleDefinition { get; set; }
    }
}
