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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.SharePoint.Client;
using Strategik.CoreFramework.Helpers;
using Strategik.Definitions.Security;
using Strategik.Definitions.TestModel.Security;
using Strategik.CoreFramework.Tests.Infrastructure;
using Strategik.Definitions.Security.Principals;
using Strategik.Definitions.Security.Permissions;
using Strategik.Definitions.Security.Roles;

namespace Strategik.CoreFramework.Tests.Helpers
{
    [TestClass]
    public class STKSecurityHelperUnitTests
    {
        [TestMethod]
        [TestCategory(STKTestConstants.CoreFramework_Helpers_SecurityHelper)]
        public void TestEnsureSecurityGroups()
        {
            using (ClientContext context = STKTestsConfig.CreateClientContext())
            {
                STKSecurityHelper helper = new STKSecurityHelper(context);
                List<STKGroup> allCustomGroups = STKTestSecurity.AllCustomGroups();

                allCustomGroups[0].Owner = STKTestsConfig.UserName;

                //foreach (STKGroup group in allCustomGroups)
                //{
                //    group.Owner = STKTestsConfig.UserName;
                //}

                helper.EnsureGroups(allCustomGroups);
            }
        }

        [TestMethod]
        [TestCategory(STKTestConstants.CoreFramework_Helpers_SecurityHelper)]
        public void TestReadSecurityGroups()
        {
            using (ClientContext context = STKTestsConfig.CreateClientContext())
            {
                STKSecurityHelper helper = new STKSecurityHelper(context);
                List<STKGroup> allSiteGroups = helper.ReadGroups();
            }
        }


        [TestMethod]
        [TestCategory(STKTestConstants.CoreFramework_Helpers_SecurityHelper)]
        public void TestEnsureRoleDefinitions()
        {
            using (ClientContext context = STKTestsConfig.CreateClientContext())
            {
                STKSecurityHelper helper = new STKSecurityHelper(context);
                List<STKRoleDefinition> allCustomRoleDefinitions = STKTestSecurity.AllCustomRoleDefinitions();
                helper.EnsureRoleDefinitions(allCustomRoleDefinitions);
            }
        }

        [TestMethod]
        [TestCategory(STKTestConstants.CoreFramework_Helpers_SecurityHelper)]
        public void TestReadRoleDefinitions()
        {
            using (ClientContext context = STKTestsConfig.CreateClientContext())
            {
                STKSecurityHelper helper = new STKSecurityHelper(context);
                List<STKRoleDefinition> allCustomRoleDefinitions = helper.ReadRoleDefinitions();
            }
        }

        [TestMethod]
        [TestCategory(STKTestConstants.CoreFramework_Helpers_SecurityHelper)]
        public void TestReadRoleAssignments()
        {
            using (ClientContext context = STKTestsConfig.CreateClientContext())
            {
                STKSecurityHelper helper = new STKSecurityHelper(context);
                List<STKRoleAssignment> allPermissionLevels = helper.ReadRoleAssignments();
            }
        }


        [TestMethod]
        [TestCategory(STKTestConstants.CoreFramework_Helpers)]
        public void TestCreateCustomAdministratorRole()
        {
            using (ClientContext context = STKTestsConfig.CreateClientContext())
            {
                //STKSecurityHelper helper = new STKSecurityHelper(context);
                //STKGroup adminRole = new STKGroup()
                //{
                //    Name = "Custom Admin Role",
                    
                //};

                //STKRoleAssignment adminRoleAssignment = new STKRoleAssignment()
                //{
                //    Group = adminRole
                //};

                //adminRoleAssignment.RoleDefinitions.Add()

                //adminRole.RoleAssigments.Add();

            }
        }


    }
}
