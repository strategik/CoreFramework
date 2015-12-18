using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategik.Definitions.Security.Principals
{
    public class STKGroup: STKPrincipal
    {
        public String Title { get; set; }

        /// <summary>
        /// The Description of the Site Group
        /// </summary>
        public String Description { get; set; }

        /// <summary>
        /// The Owner of the Site Group
        /// </summary>
        public String Owner { get; set; }

        public List<STKUser> Users { get; set; }

        /// <summary>
        /// Defines whether the members can edit membership of the Site Group
        /// </summary>
        public Boolean AllowMembersEditMembership { get; set; }

        /// <summary>
        /// Defines whether to allow requests to join or leave the Site Group
        /// </summary>
        public Boolean AllowRequestToJoinLeave { get; set; }

        /// <summary>
        /// Defines whether to auto-accept requests to join or leave the Site Group
        /// </summary>
        public Boolean AutoAcceptRequestToJoinLeave { get; set; }

        /// <summary>
        /// Defines whether to allow members only to view the membership of the Site Group
        /// </summary>
        public Boolean OnlyAllowMembersViewMembership { get; set; }

        /// <summary>
        /// Defines the email address used for membership requests to join or leave will be sent for the Site Group
        /// </summary>
        public String RequestToJoinLeaveEmailSetting { get; set; }

    }
}
