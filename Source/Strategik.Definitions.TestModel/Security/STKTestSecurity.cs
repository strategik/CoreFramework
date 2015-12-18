using Strategik.Definitions.Security;
using Strategik.Definitions.Security.Principals;
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



        public static List<STKGroup> AllCustomGroups()
        {
            List<STKGroup> groups = new List<STKGroup>();
            groups.Add(CustomGroup1);
            return groups;
        }

        #endregion
    }
}
