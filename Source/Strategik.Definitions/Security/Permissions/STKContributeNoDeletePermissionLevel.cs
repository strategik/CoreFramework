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
    /// Can contribute to a site but not delete items
    /// </summary>
    public class STKContributeNoDeletePermissionLevel: STKPermissionLevel
    {
        public STKContributeNoDeletePermissionLevel()
        {
            Define();
        }

        private void Define()
        {
            // List Permissions
            base.Permissions.Add(STKPermission.AddListItems);
            base.Permissions.Add(STKPermission.EditListItems);
        //    base.Permissions.Add(STKPermission.DeleteListItems);
            base.Permissions.Add(STKPermission.ViewListItems);
            base.Permissions.Add(STKPermission.OpenItems);
            base.Permissions.Add(STKPermission.ViewVersions);
            base.Permissions.Add(STKPermission.CreateAlerts);
            base.Permissions.Add(STKPermission.ViewApplicationPages);

            // Site Permissions
            base.Permissions.Add(STKPermission.BrowseDirectories);
            base.Permissions.Add(STKPermission.CreateSSCSite);
            base.Permissions.Add(STKPermission.ViewPages);
            base.Permissions.Add(STKPermission.UseRemoteAPIs);
            base.Permissions.Add(STKPermission.UseClientIntegration);
            base.Permissions.Add(STKPermission.Open);
            base.Permissions.Add(STKPermission.EditMyUserInfo);
            base.Permissions.Add(STKPermission.BrowseUserInfo);

            //Personal Permissions
            base.Permissions.Add(STKPermission.ManagePersonalViews);
            base.Permissions.Add(STKPermission.AddDelPrivateWebParts);
            base.Permissions.Add(STKPermission.UpdatePersonalWebParts);
        }
    }
}
