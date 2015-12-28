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

namespace Strategik.Definitions.Security.Permissions
{
    /// <summary>
    /// A site owner but not allowed to create subsites
    /// </summary>
    public class STKSiteOwnerNoSubsitesPermissionLevel: STKPermissionLevel
    {
        public STKSiteOwnerNoSubsitesPermissionLevel()
        {
            Define();
        }

        private void Define()
        {
            // List Permissions
            base.Permissions.Add(STKPermission.ManageLists);
            //base.Permissions.Add(STKPermission.Ove) override list behaviours
            base.Permissions.Add(STKPermission.AddListItems);
            base.Permissions.Add(STKPermission.EditListItems);
            base.Permissions.Add(STKPermission.DeleteListItems);
            base.Permissions.Add(STKPermission.ViewListItems);
            base.Permissions.Add(STKPermission.ApproveItems);
            base.Permissions.Add(STKPermission.OpenItems);
            base.Permissions.Add(STKPermission.ViewVersions);
            base.Permissions.Add(STKPermission.DeleteVersions);
            base.Permissions.Add(STKPermission.CreateAlerts);
            base.Permissions.Add(STKPermission.ViewApplicationPages);

            // Site Permissions
            base.Permissions.Add(STKPermission.ManagePermissions);
            base.Permissions.Add(STKPermission.ViewUsageData);
           // base.Permissions.Add(STKPermission.ManageSubwebs); // Create sub sites???
            base.Permissions.Add(STKPermission.ManageWeb);
            base.Permissions.Add(STKPermission.AddAndCustomizePages);
            base.Permissions.Add(STKPermission.ApplyThemeAndBorder);
            base.Permissions.Add(STKPermission.ApplyStyleSheets);
            base.Permissions.Add(STKPermission.CreateGroups);
            base.Permissions.Add(STKPermission.BrowseDirectories);
            base.Permissions.Add(STKPermission.CreateSSCSite);
            base.Permissions.Add(STKPermission.ViewPages);
            base.Permissions.Add(STKPermission.EnumeratePermissions);
            base.Permissions.Add(STKPermission.BrowseUserInfo);
            base.Permissions.Add(STKPermission.ManageAlerts);
            base.Permissions.Add(STKPermission.UseRemoteAPIs);
            base.Permissions.Add(STKPermission.UseClientIntegration);
            base.Permissions.Add(STKPermission.Open);
            base.Permissions.Add(STKPermission.EditMyUserInfo);

            //Personal Permissions
            base.Permissions.Add(STKPermission.ManagePersonalViews);
            base.Permissions.Add(STKPermission.UpdatePersonalWebParts);
            base.Permissions.Add(STKPermission.UpdatePersonalWebParts);
        }
    }
}
