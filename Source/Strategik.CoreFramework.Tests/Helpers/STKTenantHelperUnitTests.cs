using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Strategik.CoreFramework.Helpers;
using Strategik.CoreFramework.Tests.Infrastructure;
using Strategik.Definitions.Sites;
using Strategik.Definitions.TestModel.Sites;
using Strategik.Definitions.Configuration;
using OfficeDevPnP.Core.Diagnostics;

namespace Strategik.CoreFramework.Tests.Helpers
{
    /// <summary>
    /// Unit Tests for the Tenant Helper - Needs an O365 instance to run against
    /// </summary>
    [TestClass]
    public class STKTenantHelperUnitTests
    {
        public STKTenantHelperUnitTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        [TestMethod]
        [TestCategory(STKTestConstants.CoreFramework_Helpers_TenantHelper)]
        public void TestCreateSite()
        {
            STKTenantHelper helper = new STKTenantHelper(STKTestsConfig.TenantUrl, STKTestsConfig.DevSiteUrl, STKTestsConfig.CreateTenantClientContext(), false);
            STKSite site = STKTestSites.GetUnitTestSite();
            site.SiteOwnerLogin = STKTestsConfig.UserName;
            STKProvisioningConfiguration config = new STKProvisioningConfiguration() { DeleteExistingSites = true, EnsureSite = false };

            // Create it (force delete if already present / dont provision all the artefacts)
            helper.CreateSite(site, config);
            Assert.IsTrue(helper.SiteExists(site));

            // Delete it
            helper.DeleteSite(site, false);
            Assert.IsFalse(helper.SiteExists(site));
        }

        [TestMethod]
        [TestCategory(STKTestConstants.CoreFramework_Helpers_TenantHelper)]
        public void TestCreateAndEnsureSite()
        {
            //STKTenantHelper helper = new STKTenantHelper(
            //    STKTestsConfig.TenantUrl, 
            //    STKTestsConfig.DevSiteUrl, 
            //    STKTestsConfig.CreateTenantClientContext(), 
            //    false);

            // Need to pass the password here so we can create the context to provision the site later
            STKTenantHelper helper = new STKTenantHelper(
                STKTestsConfig.TenantUrl,
                STKTestsConfig.DevSiteUrl,
                STKTestsConfig.UserName,
                STKTestsConfig.oPassword);

            STKSite site = STKTestSites.GetUnitTestSite();
            site.TenantRelativeURL += "AndEnsureSite";
            site.SiteOwnerLogin = STKTestsConfig.UserName;
            STKProvisioningConfiguration config = new STKProvisioningConfiguration() { DeleteExistingSites = false, EnsureSite = true };

            // Create it (force delete if already present / dont provision all the artefacts)
            helper.CreateSite(site, config);
            Assert.IsTrue(helper.SiteExists(site));

            // Delete it
            //helper.DeleteSite(site, false);
            //Assert.IsFalse(helper.SiteExists(site));
        }

        [TestMethod]
        [TestCategory(STKTestConstants.CoreFramework_Helpers_TenantHelper)]
        public void TestLogging()
        {
            Log.Info("Test Source", "Information test message");
            Log.Debug("Test Source", "Debug test message");

            Log.LogLevel = LogLevel.Information;

            Log.Error("Test Source", "Information test message 2");

        }
    }
}
