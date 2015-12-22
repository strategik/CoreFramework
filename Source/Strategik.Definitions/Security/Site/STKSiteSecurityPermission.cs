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

using Strategik.Definitions.Security.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategik.Definitions.Security.Site
{
    public class STKSiteSecurityPermission
    {
        #region Private Members

        private List<STKRoleDefinition> _roleDefinitions = new List<STKRoleDefinition>();
        private List<STKRoleAssignment> _roleAssignments = new List<STKRoleAssignment>();

        #endregion

        #region Public Members

        /// <summary>
        /// List of Role Definitions for the Site
        /// </summary>
        public List<STKRoleDefinition> RoleDefinitions
        {
            get { return this._roleDefinitions; }
            private set { this._roleDefinitions = value; }
        }

        /// <summary>
        /// List of Role Assignments for the Site
        /// </summary>
        public List<STKRoleAssignment> RoleAssignments
        {
            get { return this._roleAssignments; }
            private set { this._roleAssignments = value; }
        }

        #endregion
    }
}
