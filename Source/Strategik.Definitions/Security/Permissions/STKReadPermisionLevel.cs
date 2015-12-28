using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategik.Definitions.Security.Permissions
{
    public class STKReadPermisionLevel: STKPermissionLevel
    {
        public STKReadPermisionLevel()
        {
            Define();
        }

        private void Define()
        {
            // List Permissions
            base.Permissions.Add(STKPermission.ViewListItems);
            base.Permissions.Add(STKPermission.OpenItems);
            base.Permissions.Add(STKPermission.ViewVersions);
            base.Permissions.Add(STKPermission.CreateAlerts);
            base.Permissions.Add(STKPermission.ViewApplicationPages);

            // Site Permissions
            base.Permissions.Add(STKPermission.CreateSSCSite);
            base.Permissions.Add(STKPermission.ViewPages);
            base.Permissions.Add(STKPermission.BrowseUserInfo);
            base.Permissions.Add(STKPermission.UseRemoteAPIs);
            base.Permissions.Add(STKPermission.UseClientIntegration);
            base.Permissions.Add(STKPermission.Open);

            //Personal Permissions
           
        }
    }
}
