
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


using Microsoft.SharePoint.Client;
using Strategik.CoreFramework.PnP.Framework.Provisioning.Providers.Strategik.Model;
using Strategik.Definitions.Security;
using Strategik.Definitions.Security.Permissions;
using Strategik.Definitions.Security.Principals;
using Strategik.Definitions.Security.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeDevPnP.Core.Framework.Provisioning.Model
{
    /// <summary>
    /// Extension methods to convert PnP security model objects to Strategik objects
    /// </summary>
    public static partial class SecurityExtensions
    {
        #region Site Group

        public static STKGroup GenerateStrategikDefinition(this SiteGroup group)
        {
            STKGroup stkGroup = new STKGroup()
            {
                AllowRequestToJoinLeave = group.AllowRequestToJoinLeave,
                AutoAcceptRequestToJoinLeave = group.AutoAcceptRequestToJoinLeave,
                AllowMembersEditMembership = group.AllowMembersEditMembership,
                Description = group.Description,
                RequestToJoinLeaveEmailSetting = group.RequestToJoinLeaveEmailSetting,
                Title = group.Title,
                OnlyAllowMembersViewMembership = group.OnlyAllowMembersViewMembership,
                Owner = group.Owner
            };

            foreach (User user in group.Members)
            {
                STKUser stkUser = new STKUser()
                {
                    Name = user.Name
                };

                stkGroup.Users.Add(stkUser);
            }
        

            return stkGroup;
        }

        #endregion

        #region RoleDefinition

        public static STKRoleDefinition GenerateStrategikDefinition(this RoleDefinition roleDefinition)
        {
            STKRoleDefinition stkRoleDefinition = new STKRoleDefinition()
            {
                Name = roleDefinition.Name,
                Description = roleDefinition.Description

            };

            STKPermissionLevel stkPermissionLevel = new STKPermissionLevel();

            foreach (PermissionKind permission in roleDefinition.Permissions)
            {
                STKPermission stkPermission = (STKPermission)permission; // these enums currently correspond
                stkPermissionLevel.Permissions.Add(stkPermission);
            }

            stkRoleDefinition.PermissionLevel = stkPermissionLevel;

            return stkRoleDefinition;
        }

        #endregion

        #region Role Assignment

        public static STKRoleAssignment GenerateStrategikDefinition(this RoleAssignment roleAssignment)
        {
            STKPnPRoleAssignment stkPnPRoleAssigment = roleAssignment as STKPnPRoleAssignment;
            STKPrincipal stkPrincipal = null;

            if (stkPnPRoleAssigment != null)
            {
                if (stkPnPRoleAssigment.IsADGroup)
                {
                    stkPrincipal = new STKADGroup() { Name = stkPnPRoleAssigment.Principal };
                }

                if (stkPnPRoleAssigment.IsSharePointGroup)
                {
                    stkPrincipal = new STKGroup() { Title = stkPnPRoleAssigment.Principal };
                }

                if (stkPnPRoleAssigment.IsUser)
                {
                    stkPrincipal = new STKUser() { Name = stkPnPRoleAssigment.Principal };
                }
            }

            STKRoleDefinition stkRoleDefintion = new STKRoleDefinition()
            {
                Name = roleAssignment.RoleDefinition
            };

            STKRoleAssignment stkRoleAssignment = new STKRoleAssignment()
            {
                RoleDefinition = stkRoleDefintion
            };

            return stkRoleAssignment;
        }


        #endregion
    }
}
