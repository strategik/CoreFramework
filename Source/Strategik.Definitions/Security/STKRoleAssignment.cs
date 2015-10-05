using System.Collections.Generic;

namespace Strategik.Definitions.Security
{
    public class STKRoleAssignment : STKSecurityDefinition
    {
        #region Properties

        /// <summary>
        /// An existing user or group for the role assignment. Use a group wherever possible.
        /// </summary>
        public STKUser User { get; set; }

        /// <summary>
        /// A collection of role definitions (permissionsets) for this role asssignment
        /// </summary>
        public List<STKRole> RoleDefinitions { get; set; }

        #endregion Properties

        #region Constructor

        public STKRoleAssignment()
        {
            RoleDefinitions = new List<STKRole>();
        }

        #endregion Constructor

        #region Role Assignment Static Helper Methods

        public static STKRoleAssignment GetAdministratorRoleAssignment(STKGroup daGroupDefinition)
        {
            STKRoleAssignment roleAssignment = GetEmptyRoleAssignment(daGroupDefinition);
            roleAssignment.RoleDefinitions.Add(STKRole.Administrator);
            return roleAssignment;
        }

        public static STKRoleAssignment GetContributorRoleAssignment(STKGroup daGroupDefinition)
        {
            STKRoleAssignment roleAssignment = GetEmptyRoleAssignment(daGroupDefinition);
            roleAssignment.RoleDefinitions.Add(STKRole.Contributor);
            return roleAssignment;
        }

        public static STKRoleAssignment GetContributorNoDeleteRoleAssignment(STKGroup daGroupDefinition)
        {
            STKRoleAssignment roleAssignment = GetEmptyRoleAssignment(daGroupDefinition);
            roleAssignment.RoleDefinitions.Add(STKRole.Contributor);
            return roleAssignment;
        }

        public static STKRoleAssignment GetReaderRoleAssignment(STKGroup daGroupDefinition)
        {
            STKRoleAssignment roleAssignment = GetEmptyRoleAssignment(daGroupDefinition);
            roleAssignment.RoleDefinitions.Add(STKRole.Reader);
            return roleAssignment;
        }

        public static STKRoleAssignment GetGuestRoleAssignment(STKGroup daGroupDefinition)
        {
            STKRoleAssignment roleAssignment = GetEmptyRoleAssignment(daGroupDefinition);
            roleAssignment.RoleDefinitions.Add(STKRole.Guest);
            return roleAssignment;
        }

        private static STKRoleAssignment GetEmptyRoleAssignment(STKGroup daGroupDefinition)
        {
            STKUser daTargetGroupDefinition = new STKUser { Group = daGroupDefinition };
            STKRoleAssignment roleAssignment = new STKRoleAssignment { User = daTargetGroupDefinition };
            return roleAssignment;
        }

        #endregion Role Assignment Static Helper Methods
    }
}