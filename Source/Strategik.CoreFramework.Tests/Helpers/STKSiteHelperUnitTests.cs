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

namespace Strategik.CoreFramework.Tests.Helpers
{
    [TestClass]
    public class STKSiteHelperUnitTests
    {
        [TestMethod]
        [TestCategory(STKTestConstants.CoreFramework_Helpers)]
        public void TestSiteHelperConnect()
        {
            using (ClientContext context = STKUnitTestHelper.GetTestContext())
            {
                context.Load(context.Web);
                context.ExecuteQueryRetry();
                String title = context.Web.Title;

                Assert.AreEqual(title, "Core Framework Unit Tests");
            }
        }

        [TestMethod]
        [TestCategory(STKTestConstants.CoreFramework_Helpers)]
        public void TestEnsureSite()
        {
            Assert.Fail(STKTestConstants.TestNotImplemented);
        }

        [TestMethod]
        [TestCategory(STKTestConstants.CoreFramework_Helpers)]
        public void TestReadSite()
        {
            Assert.Fail(STKTestConstants.TestNotImplemented);
        }
    }
}
