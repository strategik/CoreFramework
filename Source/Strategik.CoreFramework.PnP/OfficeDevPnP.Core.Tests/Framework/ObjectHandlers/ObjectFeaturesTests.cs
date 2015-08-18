using Microsoft.SharePoint.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OfficeDevPnP.Core.Framework.Provisioning.Model;
using OfficeDevPnP.Core.Framework.Provisioning.ObjectHandlers;
using System;
using System.Linq;
using Feature = OfficeDevPnP.Core.Framework.Provisioning.Model.Feature;

namespace OfficeDevPnP.Core.Tests.Framework.ObjectHandlers
{
    [TestClass]
    public class ObjectFeaturesTests
    {
        private Guid featureId = Guid.Parse("{87294c72-f260-42f3-a41b-981a2ffce37a}");

        [TestCleanup]
        public void CleanUp()
        {
            using (var ctx = TestCommon.CreateClientContext())
            {
                if (!ctx.Web.IsFeatureActive(featureId))
                {
                    ctx.Web.ActivateFeature(featureId);
                }
            }
        }

        [TestMethod]
        public void CanProvisionObjects()
        {
            var template = new ProvisioningTemplate();
            template.Features.WebFeatures.Add(
                new Feature() { Id = featureId, Deactivate = true });

            using (var ctx = TestCommon.CreateClientContext())
            {
                TokenParser.Initialize(ctx.Web, template);
                new ObjectFeatures().ProvisionObjects(ctx.Web, template, new ProvisioningTemplateApplyingInformation());

                var f = ctx.Web.IsFeatureActive(featureId);

                Assert.IsFalse(f);
            }
        }

        [TestMethod]
        public void CanCreateEntities()
        {
            using (var ctx = TestCommon.CreateClientContext())
            {
                // Load the base template which will be used for the comparison work
                var creationInfo = new ProvisioningTemplateCreationInformation(ctx.Web) { BaseTemplate = null };

                var template = new ProvisioningTemplate();
                template = new ObjectFeatures().ExtractObjects(ctx.Web, template, creationInfo);

                Assert.IsTrue(template.Features.SiteFeatures.Any());
                Assert.IsTrue(template.Features.WebFeatures.Any());
                Assert.IsInstanceOfType(template.Features, typeof(Features));
            }
        }
    }
}
