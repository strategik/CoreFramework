using Strategik.Definitions.Security;
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
            AllowMembershipRequests = false,
            AllowUsersToEditMembership = false,
            AutoAcceptRequestsToJoinOrLeave = false,
            Description = "Custom Group1 Description",
            MembershipRequestsEmail = "group1@strategik.com.au",
            Name = "Custom Group 1",
            OnlyAllowMembersToViewMembership = false,
            Owner = @"dev\strategikspdev" // This user must exist on the target system
        };



        public static List<STKGroup> AllCustomGroups()
        {
            List<STKGroup> groups = new List<STKGroup>();
            groups.Add(CustomGroup1);
            return groups;
        }

        #endregion
    }
}
