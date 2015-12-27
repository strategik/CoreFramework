
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;
using Strategik.Definitions.Security;
using Strategik.Definitions.Configuration;
using OfficeDevPnP.Core.Framework.Provisioning.Providers.Strategik;
using Strategik.Definitions.Security.Principals;
using Strategik.Definitions.Security.Permissions;
using Strategik.Definitions.Security.Roles;

namespace Strategik.CoreFramework.Helpers
{
    public class STKSecurityHelper: STKHelperBase
    {
        #region Constructors
        public STKSecurityHelper(ClientContext clientContext) 
            : base(clientContext)
        { }

        public STKSecurityHelper(STKAuthenticationHelper authHelper)
            : base(authHelper)
        { }

        public STKSecurityHelper()
            : base(new STKAuthenticationHelper())
        { }

        #endregion

        #region Ensure Groups Methods

        public void EnsureGroups(List<STKGroup> groups, STKProvisioningConfiguration config = null)
        {
            if (groups == null) throw new ArgumentNullException("groups");
            if (config == null) config = new STKProvisioningConfiguration();

            foreach (STKGroup group in groups)
            {
                EnsureGroup(group, config);
            }
        }

        public void EnsureGroup(STKGroup group, STKProvisioningConfiguration config = null)
        {
            if (group == null) throw new ArgumentNullException("group");
            if (config == null) config = new STKProvisioningConfiguration();


            STKPnPHelper pnpHelper = new STKPnPHelper(_clientContext);
            pnpHelper.Provision(group, config);
        }

        #endregion

        #region Read Groups Methods

        public List<STKGroup> ReadGroups()
        {
           STKPnPHelper pnpHelper = new STKPnPHelper(_clientContext);
           return pnpHelper.ReadGroups();
        }

        #endregion

        #region Read Role Definitions

        public List<STKRoleDefinition> ReadRoleDefinitions()
        {
            STKPnPHelper pnpHelper = new STKPnPHelper(_clientContext);
            return pnpHelper.ReadRoleDefinitions();
        }

        #endregion

        #region Ensure Role Definitions

        public void EnsureRoleDefinitions(List<STKRoleDefinition> roleDefinitions, STKProvisioningConfiguration config = null)
        {
            if (roleDefinitions == null) throw new ArgumentNullException("roleDefinitions");
            if (config == null) config = new STKProvisioningConfiguration();

            foreach (STKRoleDefinition roleDefinition in roleDefinitions)
            {
                EnsureRoleDefinition(roleDefinition);
            }
        }

        public void EnsureRoleDefinition(STKRoleDefinition roleDefinition, STKProvisioningConfiguration config = null)
        {
            if (roleDefinition == null) throw new ArgumentNullException("roleDefinition");
            if (config == null) config = new STKProvisioningConfiguration();


            STKPnPHelper pnpHelper = new STKPnPHelper(_clientContext);
            pnpHelper.Provision(roleDefinition, config);
        }

        #endregion

        #region Read Role Assignments

        public List<STKRoleAssignment> ReadRoleAssignments()
        {
            STKPnPHelper pnpHelper = new STKPnPHelper(_clientContext);
            return pnpHelper.ReadRoleAssignments();
        }

        #endregion

        #region Ensure Role Assignments

        public void EnsureRoleAssignments(List<STKRoleAssignment> roleAssignments, STKProvisioningConfiguration config = null)
        {
            if (roleAssignments == null) throw new ArgumentNullException("roleAssignments");
            if (config == null) config = new STKProvisioningConfiguration();

            foreach (STKRoleAssignment roleAssignment in roleAssignments)
            {
                EnsureRoleAssignment(roleAssignment);
            }
        }

        public void EnsureRoleAssignment(STKRoleAssignment roleAssignment, STKProvisioningConfiguration config = null)
        {
            if (roleAssignment == null) throw new ArgumentNullException("roleAssignment");
            if (config == null) config = new STKProvisioningConfiguration();


            STKPnPHelper pnpHelper = new STKPnPHelper(_clientContext);
            pnpHelper.Provision(roleAssignment, config);
        }


        #endregion
    }
}
