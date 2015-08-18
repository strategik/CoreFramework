using Strategik.Definitions.Features;
using Strategik.Definitions.Sites;
using Strategik.Definitions.TestModel.Content_Types;
using Strategik.Definitions.TestModel.Lists;
using Strategik.Definitions.TestModel.SiteColumns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategik.Definitions.TestModel.Sites
{
    public static class STKTestSites
    {
        #region Constants

        public static Guid TestSiteId = new Guid("{FCA960D0-CE1F-453C-A9A2-12F867BFE623}");
        public static Guid PnPTestSiteId = new Guid("{0E1CBFF0-3DC5-4010-8131-5A08ECCF5B5B}");

        public static Guid TestRootWebId = new Guid("{9E28CFEA-B4C8-4C38-A738-2C518667B438}");
        public static Guid TestSubWeb1Id = new Guid("{A0D74BBD-A54A-4333-95E2-06D1143AE10A}");
        public static Guid TestSubWeb2Id = new Guid("{FFF1E6BE-C243-4881-9E0B-B50A53DA6CB8}");

        #endregion

        #region Definitions

        public static List<STKSite> GetTestSites() 
        {
            List<STKSite> sites = new List<STKSite>();
            sites.Add(GetUnitTestSite());
            return sites;
        }

        public static STKSite GetUnitTestSite()
        {
            STKSite site = new STKSite()
            {
                DisplayName = "Test",
                Name = "Test",
                UniqueId = TestSiteId,
                SiteOwnerLogin = "STKCoreTestsCommon.UserName", //TODO
                TenantRelativeURL = "/sites/STKCoreFrameworkUnitTests"
            };

            // Set up features - enable publishing and disable MDS
            site.SiteFeaturesToActivate.Add(STKFeatures.PublishingSiteFeature);
            site.RootWebFeaturesToDeactivate.Add(STKFeatures.MDSFeature);
            site.RootWebFeaturesToActivate.Add(STKFeatures.PublishingWebFeature);

            // Add the BindTuning branding solution to be provisioned
            STKSandboxSolution bindTuningSolution = new STKSandboxSolution()
            {
                Activate = true,
                FileName = "RACQSPC.SPO2013.wsp",
                Location = "Strategik.O365.CoreFramework.Tests",
                Path = "Strategik.O365.CoreFramework.Tests.Solutions.RACQSPC.SPO2013.wsp",
                LocationType = STKSolutionLocationType.Assembly,
                MajorVersion = 1,
                MinorVersion = 0,
                SolutionId = new Guid("{d3d18a76-4e21-4fba-892e-f2e002f3ea4f}")
            };

            site.Solutions.Add(bindTuningSolution);

            STKWeb rootWeb = new STKWeb()
            {
                Name = "RootWebTest",
                DisplayName = "Root Web Test",
            };

            // Test Site Columns
            rootWeb.SiteColumns.AddRange(STKTestSiteColumns.AllSiteColumns());

            // Test Content Type
            rootWeb.ContentTypes.Add(STKTestContentTypes.ItemContentType());
            rootWeb.ContentTypes.Add(STKTestContentTypes.DocumentContentType());

            return site;
        }

        public static STKSite GetPnPIntegrationTestSite()
        {
            STKSite site = new STKSite()
            {
                DisplayName = "PnP Test",
                Name = "PnPTest",
                UniqueId = PnPTestSiteId,
                SiteOwnerLogin = "STKCoreTestsCommon.UserName", //TODO
                TenantRelativeURL = "/sites/STKPnPIntegrationTests",
            };

            // Set up features - enable publishing and disable MDS
       
            STKWeb rootWeb = new STKWeb()
            {
                Name = "PnPRootWebTest",
                DisplayName = "PnP Root Web Test",
            };

            // Test Site Columns
            rootWeb.SiteColumns.AddRange(STKTestSiteColumns.AllSiteColumns());

            // Test Content Type
            rootWeb.ContentTypes.Add(STKTestContentTypes.ItemContentType());
            rootWeb.ContentTypes.Add(STKTestContentTypes.DocumentContentType());

            // Test Lists and Librbaries
            rootWeb.Lists.AddRange(STKTestLists.AllListsAndLibraries());

            return site;
        }

        #endregion
    }
}
