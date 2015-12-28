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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.SharePoint.Client;
using Strategik.CoreFramework.Helpers;
using Strategik.Definitions.Lists;
using System.Collections.Generic;
using Strategik.Definitions.TestModel.Lists;
using Strategik.CoreFramework.Tests.Infrastructure;

namespace Strategik.CoreFramework.Tests.Helpers
{
    [TestClass]
    public class STKListHelperUnitTests
    {
        [TestMethod]
        [TestCategory(STKTestConstants.CoreFramework_Helpers_ListHelper)]
        public void TestListHelperConnect()
        {
            // Check we are testing the the correct site
            using (ClientContext context = STKTestsConfig.CreateClientContext())
            {
                context.Load(context.Web);
                context.ExecuteQueryRetry();
                String title = context.Web.Title;

                Assert.AreEqual(title, "Core Framework Unit Tests");
            }
        }

        [TestMethod]
        [TestCategory(STKTestConstants.CoreFramework_Helpers_ListHelper)]
        public void TestEnsureLists()
        {
            // Should execute without exception
            using (ClientContext context = STKTestsConfig.CreateClientContext())
            {
                STKListHelper helper = new STKListHelper(context);
                List<STKList> allListsAndLibraries = STKTestLists.AllListsAndLibraries();
                helper.EnsureLists(allListsAndLibraries);
            }
        }

        [TestMethod]
        [TestCategory(STKTestConstants.CoreFramework_Helpers_ListHelper)]
        public void TestReadLists()
        {
            // Make sure the lists we need have been provisioned
           

            using (ClientContext context = STKTestsConfig.CreateClientContext())
            {
                STKListHelper helper = new STKListHelper(context);
                List<STKList> lists = helper.ReadLists(true);
            }
        }

        [TestMethod]
        [TestCategory(STKTestConstants.CoreFramework_Helpers_ListHelper)]
        public void TestEnsureAndReadLists()
        {
            TestEnsureLists();
            TestReadLists();
        }

        [TestMethod]
        [TestCategory(STKTestConstants.CoreFramework_Helpers_ListHelper)]
        public void TestReadSharePointList()
        {
            Assert.Fail(STKTestConstants.TestNotImplemented);
        }


        [TestMethod]
        [TestCategory(STKTestConstants.CoreFramework_Helpers_ListHelper)]
        public void TestProvisionTaskList()
        {
            using (ClientContext context = STKTestsConfig.CreateClientContext())
            {
                STKListHelper helper = new STKListHelper(context);
                helper.EnsureList(new STKTaskList());
            }
        }

        [TestMethod]
        [TestCategory(STKTestConstants.CoreFramework_Helpers_ListHelper)]
        public void TestProvisionCustomisedTaskList()
        {
            using (ClientContext context = STKTestsConfig.CreateClientContext())
            {
                STKListHelper helper = new STKListHelper(context);
                STKTaskList taskList = new STKTaskList()
                {
                    Title = "My Demo Task List",
                    Url = "demoTasks",
                    EnableVersioning = true,
                };

                helper.EnsureList(taskList);
            }
        }


        [TestMethod]
        [TestCategory(STKTestConstants.CoreFramework_Helpers_ListHelper)]
        public void TestProvisionPromotedLinksList()
        {
            using (ClientContext context = STKTestsConfig.CreateClientContext())
            {
                STKListHelper helper = new STKListHelper(context);
                helper.EnsureList(new STKPromotedLinksList());
            }
        }
    }
}
