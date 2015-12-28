
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
    /// Match the fine grained permissions available via CSOM
    /// </summary>
    public enum STKPermission
    {
        EmptyMask = 0,
        ViewListItems = 1,
        AddListItems = 2,
        EditListItems = 3,
        DeleteListItems = 4,
        ApproveItems = 5,
        OpenItems = 6,
        ViewVersions = 7,
        DeleteVersions = 8,
        CancelCheckout = 9,
        ManagePersonalViews = 10,
        ManageLists = 12,
        ViewApplicationPages = 13,
        AnonymousSearchAccessList = 14,
        Open = 17,
        ViewPages = 18,
        AddAndCustomizePages = 19,
        ApplyThemeAndBorder = 20,
        ApplyStyleSheets = 21,
        ViewUsageData = 22,
        CreateSSCSite = 23,
        ManageSubwebs = 24,
        CreateGroups = 25,
        ManagePermissions = 26,
        BrowseDirectories = 27,
        BrowseUserInfo = 28,
        AddDelPrivateWebParts = 29,
        UpdatePersonalWebParts = 30,
        ManageWeb = 31,
        AnonymousSearchAccessWebLists = 32,
        UseClientIntegration = 37,
        UseRemoteAPIs = 38,
        ManageAlerts = 39,
        CreateAlerts = 40,
        EditMyUserInfo = 41,
        EnumeratePermissions = 63,
        FullMask = 65
    }
}
