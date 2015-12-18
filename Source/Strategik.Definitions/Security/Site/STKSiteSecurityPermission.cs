using Strategik.Definitions.Security.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategik.Definitions.Security.Site
{
    public class STKSiteSecurityPermission
    {
        #region Private Members

        private List<STKRoleDefinition> _roleDefinitions = new List<STKRoleDefinition>();
        private List<STKRoleAssignment> _roleAssignments = new List<STKRoleAssignment>();

        #endregion

        #region Public Members

        /// <summary>
        /// List of Role Definitions for the Site
        /// </summary>
        public List<STKRoleDefinition> RoleDefinitions
        {
            get { return this._roleDefinitions; }
            private set { this._roleDefinitions = value; }
        }

        /// <summary>
        /// List of Role Assignments for the Site
        /// </summary>
        public List<STKRoleAssignment> RoleAssignments
        {
            get { return this._roleAssignments; }
            private set { this._roleAssignments = value; }
        }

        #endregion
    }
}
