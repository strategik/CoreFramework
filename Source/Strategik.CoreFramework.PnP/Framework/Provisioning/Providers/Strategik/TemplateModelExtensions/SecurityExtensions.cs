
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
// Author:  Dr Adrian Colquhoun
//
#endregion


using Strategik.Definitions.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeDevPnP.Core.Framework.Provisioning.Model
{
    public static partial class SecurityExtensions
    {
        public static STKGroup GenerateStrategikDefinition(this SiteGroup group)
        {
            STKGroup stkGroup = new STKGroup()
            {
                AllowMembershipRequests = group.AllowRequestToJoinLeave,
                AutoAcceptRequestsToJoinOrLeave = group.AutoAcceptRequestToJoinLeave,
                AllowUsersToEditMembership = group.AllowMembersEditMembership,
                Description = group.Description,
                MembershipRequestsEmail = group.RequestToJoinLeaveEmailSetting,
                Name = group.Title,
                OnlyAllowMembersToViewMembership = group.OnlyAllowMembersViewMembership,
                Owner = group.Owner
            };

            foreach (User user in group.Members)
            {
                STKUser stkUser = new STKUser()
                {
                    Group = stkGroup,
                    Name = user.Name
                };

                stkGroup.Users.Add(stkUser);
            }
        

            return stkGroup;
        }

    }
}
