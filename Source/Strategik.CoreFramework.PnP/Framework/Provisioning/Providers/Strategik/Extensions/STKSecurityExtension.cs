
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

#endregion License

using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core.Framework.Provisioning.Model;
using Strategik.Definitions.Security.Permissions;
using Strategik.Definitions.Security.Principals;
using Strategik.Definitions.Security.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategik.Definitions.Security
{
    public static partial class STKSecurityExtension
    {
        public static SiteGroup GeneratePnPTemplate(this STKGroup group)
        {
            SiteGroup siteGroup = new SiteGroup()
            {
                AllowMembersEditMembership = group.AllowMembersEditMembership,
                AutoAcceptRequestToJoinLeave = group.AutoAcceptRequestToJoinLeave,
                Description = group.Description,
                OnlyAllowMembersViewMembership = group.OnlyAllowMembersViewMembership,
                AllowRequestToJoinLeave = group.AllowRequestToJoinLeave,
                RequestToJoinLeaveEmailSetting = group.RequestToJoinLeaveEmailSetting,
                Owner = group.Owner,
                Title = group.Title
            };

            return siteGroup;
        }

        public static OfficeDevPnP.Core.Framework.Provisioning.Model.RoleDefinition GeneratePnPTemplate(this STKRoleDefinition stkRoleDefinition)
        {
            OfficeDevPnP.Core.Framework.Provisioning.Model.RoleDefinition roleDefinition = new OfficeDevPnP.Core.Framework.Provisioning.Model.RoleDefinition()
            {
                Name = stkRoleDefinition.Name,
                Description = stkRoleDefinition.Description
            };

            foreach (STKPermission permission in stkRoleDefinition.PermissionLevel.Permissions)
            {
                PermissionKind pk = (PermissionKind)permission;
                roleDefinition.Permissions.Add(pk);
            }

            return roleDefinition;
        }

        public static OfficeDevPnP.Core.Framework.Provisioning.Model.RoleAssignment GeneratePnPTemplate(this STKRoleAssignment stkRoleAssignment)
        {
            OfficeDevPnP.Core.Framework.Provisioning.Model.RoleAssignment roleAssignment = new OfficeDevPnP.Core.Framework.Provisioning.Model.RoleAssignment()
            {
                Principal = stkRoleAssignment.Principal.ToString(),
                RoleDefinition = stkRoleAssignment.RoleDefinition.Name
            };
            
            return roleAssignment;
        }
    }
}
