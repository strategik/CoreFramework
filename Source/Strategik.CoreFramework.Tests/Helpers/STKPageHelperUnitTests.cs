using System;
using System.Linq;
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
                STKFolder rootFolder = helper.GetMasterPages(false, new List<String> {"Strategik"});
                Assert.IsTrue(rootFolder.Files.Count > 0);
                STKFolder strategikFolder = rootFolder.Folders.Where(f => f.Name == "Strategik").SingleOrDefault();
                Assert.IsNotNull(strategikFolder);
                Assert.IsTrue(strategikFolder.Files.Count == 9);
          
                STKFolder rootFolderWithData = helper.GetMasterPages(true, new List<String> { "Strategik" });
                Assert.IsTrue(rootFolder.Files.Count == rootFolderWithData.Files.Count);
            }
        }

        [TestMethod]
        [TestCategory(STKTestConstants.CoreFramework_Helpers)]
        public void TestReadStyleLibraryAssets()
        {
            using (ClientContext context = STKTestsConfig.CreateClientContext())
            {
                STKPageHelper helper = new STKPageHelper(context);
                STKFolder rootFolder = helper.GetStyleLibraryAssets(false, new List<String> { "Strategik" });
 
                STKFolder strategikFolder = rootFolder.Folders.Where(f => f.Name == "Strategik").SingleOrDefault();
                Assert.IsNotNull(strategikFolder);
          
                STKFolder rootFolderWithData = helper.GetStyleLibraryAssets(true, new List<String> { "Strategik" });
                Assert.IsTrue(rootFolder.Files.Count == rootFolderWithData.Files.Count);
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
