#region License
//
// Copyright (c) 2015 Strategik Pty Ltd,
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
//
//
// Author:  Dr Adrian Colquhoun
//
#endregion License

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


        public STKGroup()
        {
            Users = new List<STKUser>();
        }
    }       
}
