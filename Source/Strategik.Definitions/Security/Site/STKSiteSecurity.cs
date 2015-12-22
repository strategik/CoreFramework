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

using Strategik.Definitions.Security.Principals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategik.Definitions.Security.Site
{
    public class STKSiteSecurity
    {
        #region

        private List<STKUser> _additionalAdministrators = new List<STKUser>();
        private List<STKUser> _additionalOwners = new List<STKUser>();
        private List<STKUser> _additionalMembers = new List<STKUser>();
        private List<STKUser> _additionalVisitors = new List<STKUser>();
        private List<STKGroup> _siteGroups = new List<STKGroup>();
        private STKSiteSecurityPermission _permissions = new STKSiteSecurityPermission();

        #endregion

        #region Public Members

        /// <summary>
        /// A Collection of users that are associated as site collection adminsitrators
        /// </summary>
        public List<STKUser> AdditionalAdministrators
        {
            get { return _additionalAdministrators; }
            private set { _additionalAdministrators = value; }
        }

        /// <summary>
        /// A Collection of users that are associated to the sites owners group
        /// </summary>
        public List<STKUser> AdditionalOwners
        {
            get { return _additionalOwners; }
            private set { _additionalOwners = value; }
        }

        /// <summary>
        /// A Collection of users that are associated to the sites members group
        /// </summary>
        public List<STKUser> AdditionalMembers
        {
            get { return _additionalMembers; }
            private set { _additionalMembers = value; }
        }

        /// <summary>
        /// A Collection of users taht are associated to the sites visitors group
        /// </summary>
        public List<STKUser> AdditionalVisitors
        {
            get { return _additionalVisitors; }
            private set { _additionalVisitors = value; }
        }

        /// <summary>
        /// List of additional Groups for the Site
        /// </summary>
        public List<STKGroup> SiteGroups
        {
            get { return _siteGroups; }
            private set { _siteGroups = value; }
        }

        /// <summary>
        /// List of Site Security Permissions for the Site
        /// </summary>
        public STKSiteSecurityPermission SiteSecurityPermissions
        {
            get { return _permissions; }
            private set { _permissions = value; }
        }

        #endregion
    }
}
