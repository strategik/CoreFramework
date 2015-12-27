using Strategik.Definitions.Security;
using Strategik.Definitions.Security.Permissions;
using Strategik.Definitions.Security.Principals;
using Strategik.Definitions.Security.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategik.Definitions.TestModel.Security
{
    public static class STKTestSecurity
    {
        #region Definitions

        public static STKGroup CustomGroup1 = new STKGroup()
        {
            AllowRequestToJoinLeave = false,
            AllowMembersEditMembership = false,
            AutoAcceptRequestToJoinLeave = false,
            Description = "Custom Group1 Description",
            RequestToJoinLeaveEmailSetting = "group1@strategik.com.au",
            Title = "Custom Group 1",
            OnlyAllowMembersViewMembership = false,
            Owner = @"dev\strategikspdev" // This user must exist on the target system
        };

        public static STKGroup CustomGroup2 = new STKGroup()
        {
            AllowRequestToJoinLeave = false,
            AllowMembersEditMembership = false,
            AutoAcceptRequestToJoinLeave = false,
            Description = "Custom Group2 Description",
            RequestToJoinLeaveEmailSetting = "group2@strategik.com.au",
            Title = "Custom Group 2",
            OnlyAllowMembersViewMembership = false,
            Owner = "Custom Group 1" // This user must exist on the target system
        };


        public static List<STKGroup> AllCustomGroups()
        {
            List<STKGroup> groups = new List<STKGroup>();
            groups.Add(CustomGroup1);
            groups.Add(CustomGroup2);
            return groups;
        }

        public static List<STKRoleDefinition> AllCustomRoleDefinitions()
        {
            List<STKRoleDefinition> roleDefinitions = new List<STKRoleDefinition>();

            STKRoleDefinition roleDefinition = new STKRoleDefinition()
            {
                Name = "Custom Permission Level 1",
                Description = "A custom permission level for unit testing"
            };

            roleDefinition.PermissionLevel.Permissions.Add(STKPermission.ApproveItems);
            roleDefinition.PermissionLevel.Permissions.Add(STKPermission.CreateAlerts);
            roleDefinitions.Add(roleDefinition);

            return roleDefinitions;
        }

        #endregion
    }
}
