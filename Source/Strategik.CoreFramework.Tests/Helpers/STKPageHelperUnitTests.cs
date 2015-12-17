using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.SharePoint.Client;
using Strategik.CoreFramework.Tests.Infrastructure;
using Strategik.CoreFramework.Helpers;
using Strategik.Definitions.Files;
using System.Collections.Generic;

namespace Strategik.CoreFramework.Tests.Helpers
{
    [TestClass]
    public class STKPageHelperUnitTests
    {

        [TestMethod]
        [TestCategory(STKTestConstants.CoreFramework_Helpers)]
        public void TestReadMastepages()
        {
            using (ClientContext context = STKTestsConfig.CreateClientContext())
            {
                STKPageHelper helper = new STKPageHelper(context);
                List<STKFile> pages = helper.GetMasterPages(false);
                Assert.IsTrue(pages.Count > 0);
                List<STKFile> pagesWithData = helper.GetMasterPages(false);
                Assert.IsTrue(pages.Count == pagesWithData.Count);
            }
        }

        [TestMethod]
        [TestCategory(STKTestConstants.CoreFramework_Helpers)]
        public void TestReadPageLayouts()
        {
            using (ClientContext context = STKTestsConfig.CreateClientContext())
            {
                STKPageHelper helper = new STKPageHelper(context);
                List<STKFile> pages = helper.GetPageLayouts(false);
                Assert.IsTrue(pages.Count > 0);
                List<STKFile> pagesWithData = helper.GetPageLayouts(false);
                Assert.IsTrue(pages.Count == pagesWithData.Count);
            }
        }
    }
}
