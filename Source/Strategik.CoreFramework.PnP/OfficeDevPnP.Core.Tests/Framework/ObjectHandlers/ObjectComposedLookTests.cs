using Microsoft.VisualStudio.TestTools.UnitTesting;
using OfficeDevPnP.Core.Framework.Provisioning.Model;
using OfficeDevPnP.Core.Framework.Provisioning.ObjectHandlers;

namespace OfficeDevPnP.Core.Tests.Framework.ObjectHandlers
{
    [TestClass]
    public class ObjectComposedLookTests
    {
        [TestMethod]
        public void CanCreateComposedLooks()
        {
            using (var ctx = TestCommon.CreateClientContext())
            {
                var template = new ProvisioningTemplate();
                template = new ObjectComposedLook().ExtractObjects(ctx.Web, template, null);
                Assert.IsInstanceOfType(template.ComposedLook, typeof(ComposedLook));
            }
        }
    }
}