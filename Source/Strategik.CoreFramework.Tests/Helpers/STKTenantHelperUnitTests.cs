using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Strategik.CoreFramework.Helpers;
using Strategik.CoreFramework.Tests.Infrastructure;
using Strategik.Definitions.Sites;
using Strategik.Definitions.TestModel.Sites;
using Strategik.Definitions.Configuration;

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
        [TestCategory(STKTestConstants.CoreFramework_Helpers)]
        public void TestCreateSite()
        {
            STKTenantHelper helper = new STKTenantHelper(STKTestsConfig.TenantUrl, STKTestsConfig.DevSiteUrl, STKTestsConfig.CreateTenantClientContext(), false);
            STKSite site = STKTestSites.GetUnitTestSite();
            site.SiteOwnerLogin = STKTestsConfig.UserName;
            STKProvisioningConfiguration config = new STKProvisioningConfiguration() { DeleteExistingSite = true, EnsureSite = false };

            // Create it (force delete if already present / dont provision all the artefacts)
            helper.CreateSite(site, config);
            Assert.IsTrue(helper.SiteExists(site));

            // Delete it
            helper.DeleteSite(site, false);
            Assert.IsFalse(helper.SiteExists(site));
        }
    }
}
