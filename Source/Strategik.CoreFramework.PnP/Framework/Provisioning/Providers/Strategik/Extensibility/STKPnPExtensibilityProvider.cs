using OfficeDevPnP.Core.Framework.Provisioning.Extensibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core.Framework.Provisioning.Model;
using OfficeDevPnP.Core.Diagnostics;
using Strategik.CoreFramework.PnP.Framework.Provisioning.Providers.Strategik.Model;
using User = OfficeDevPnP.Core.Framework.Provisioning.Model.User;
using Microsoft.SharePoint.Client.Utilities;

namespace Strategik.CoreFramework.PnP.Framework.Provisioning.Providers.Strategik.Extensibility
{
    /// <summary>
    /// Called after the PnP provisioning engine completes its work
    /// </summary>
    public class STKPnPExtensbilityProvider : IProvisioningExtensibilityProvider, ISTKExtratctionExtensibilityProvider
    {
        const string LogSource = "Strategik.CoreFramework.PnP.STKPnPExtensibilityProvider";

        // An extension to extratct extar information into the template / manipulate the template if required
        public ProvisioningTemplate ExtractTemplate(Web web, ProvisioningTemplate pnpTemplate, string configurationData)
        {
            if (String.IsNullOrEmpty(configurationData)) { configurationData = "No configuration data supplied"; }
            Log.Debug(LogSource, "Extraction extension called " + configurationData);

            // Extract default group information if reading groups
            if (configurationData.Equals("ReadGroups") || configurationData.Equals("ReadPermissionLevels"))
            {
                pnpTemplate = ReadSecurityInfo(web, pnpTemplate);
            }

            return pnpTemplate;
        }

        // Will be called automatically after provisioning operations
        public void ProcessRequest(ClientContext ctx, ProvisioningTemplate template, string configurationData)
        {
            STKPnPTemplate stkTemplate = template as STKPnPTemplate;
            Log.Debug(LogSource, "Provisioning provider called " + configurationData);
        }


        #region Implementation

        protected ProvisioningTemplate ReadSecurityInfo(Web web, ProvisioningTemplate template)
        {
            // Replace the PnP read security info implementation for now as we want all the available info
            // to get a full picture of the security model currently applied to the site
            Log.Debug(LogSource, "Read Security info extension called");

            // Holds all the security related information
            SiteSecurity siteSecurity = new SiteSecurity();
            // Get the default groups
            AddDefaultGroupsToSiteSecurity(web, siteSecurity);
            // Get any addtional site collection administrators
            AddAdditionalAdministratorsToSiteSecurity(web, siteSecurity);
            // Get any custom groups
            AddAdditionalGroupsToSiteSecurity(web, siteSecurity);
            // Get all the role Definitions
            AddRoleDefinitionsToSiteSecurity(web, siteSecurity);
            // Get all the role Assingments
            AddRoleAssignmentsToSiteSecurity(web, siteSecurity);

            template.Security = siteSecurity;

            return template;
        }

        protected void AddRoleAssignmentsToSiteSecurity(Web web, SiteSecurity siteSecurity)
        {
            IEnumerable<Microsoft.SharePoint.Client.RoleAssignment> spRoleAssignments = web.Context.LoadQuery(web.RoleAssignments.Include(
                r => r.RoleDefinitionBindings.Include(
                    rd => rd.Name,
                    rd => rd.RoleTypeKind),
                r => r.Member.LoginName));

            web.Context.ExecuteQueryRetry();

            foreach (Microsoft.SharePoint.Client.RoleAssignment spRoleAssignment in spRoleAssignments)
            {
                foreach (var roleDefinition in spRoleAssignment.RoleDefinitionBindings)
                {
                    if (roleDefinition.RoleTypeKind != RoleType.Guest)
                    {
                        STKPnPRoleAssignment roleAssignment = new STKPnPRoleAssignment();
                        roleAssignment.RoleDefinition = roleDefinition.Name;
                        roleAssignment.Principal = spRoleAssignment.Member.LoginName;

                        if (spRoleAssignment.Member.PrincipalType == PrincipalType.SharePointGroup)
                        {
                            roleAssignment.IsSharePointGroup = true;
                        }
                        else if (spRoleAssignment.Member.PrincipalType == PrincipalType.SecurityGroup)
                        {
                            roleAssignment.IsADGroup = true;
                        }
                        else if (spRoleAssignment.Member.PrincipalType == PrincipalType.User)
                        {
                            roleAssignment.IsUser = true;
                        }

                        siteSecurity.SiteSecurityPermissions.RoleAssignments.Add(roleAssignment);
                    }
                }
            }
        }

        protected void AddRoleDefinitionsToSiteSecurity(Web web, SiteSecurity siteSecurity)
        {
            IEnumerable<Microsoft.SharePoint.Client.RoleDefinition> spRoleDefinitions = web.Context.LoadQuery(web.RoleDefinitions.Include(r => r.Name, r => r.Description, r => r.BasePermissions, r => r.RoleTypeKind));
            web.Context.ExecuteQueryRetry();

            
            var permissionKeys = Enum.GetNames(typeof(PermissionKind));

            foreach (Microsoft.SharePoint.Client.RoleDefinition spRoleDefinition in spRoleDefinitions)
            {
                
               OfficeDevPnP.Core.Framework.Provisioning.Model.RoleDefinition roleDefinition
                                                = new OfficeDevPnP.Core.Framework.Provisioning.Model.RoleDefinition();
                roleDefinition.Description = spRoleDefinition.Description;
                roleDefinition.Name = spRoleDefinition.Name;
                List<PermissionKind> permissions = new List<PermissionKind>();

                foreach (var permissionKey in permissionKeys)
                {
                    var permissionKind = (PermissionKind)Enum.Parse(typeof(PermissionKind), permissionKey);
                    if (spRoleDefinition.BasePermissions.Has(permissionKind))
                    {
                        roleDefinition.Permissions.Add(permissionKind);
                    }
                }
                    
                siteSecurity.SiteSecurityPermissions.RoleDefinitions.Add(roleDefinition);
            }
        }

        protected void AddAdditionalGroupsToSiteSecurity(Web web, SiteSecurity siteSecurity)
        {
            //TODO: Check we are not duplicating groups
            web.Context.Load(web.SiteGroups,
                o => o.IncludeWithDefaultProperties(
                    gr => gr.Title,
                    gr => gr.AllowMembersEditMembership,
                    gr => gr.AutoAcceptRequestToJoinLeave,
                    gr => gr.AllowRequestToJoinLeave,
                    gr => gr.Description,
                    gr => gr.Users.Include(u => u.LoginName),
                    gr => gr.OnlyAllowMembersViewMembership,
                    gr => gr.Owner.LoginName,
                    gr => gr.RequestToJoinLeaveEmailSetting
                    ));

            web.Context.ExecuteQueryRetry();

            foreach (Group group in web.SiteGroups)
            {
                SiteGroup siteGroup = new SiteGroup()
                {
                    Title = group.Title,
                    AllowMembersEditMembership = group.AllowMembersEditMembership,
                    AutoAcceptRequestToJoinLeave = group.AutoAcceptRequestToJoinLeave,
                    AllowRequestToJoinLeave = group.AllowRequestToJoinLeave,
                    Description = group.Description,
                    OnlyAllowMembersViewMembership = group.OnlyAllowMembersViewMembership,
                    Owner = group.Owner.LoginName,
                    RequestToJoinLeaveEmailSetting = group.RequestToJoinLeaveEmailSetting
                };

                foreach (var member in group.Users)
                {
                    siteGroup.Members.Add(new User() { Name = member.LoginName });
                }
                siteSecurity.SiteGroups.Add(siteGroup);
            }
        }

        protected void AddAdditionalAdministratorsToSiteSecurity(Web web, SiteSecurity siteSecurity)
        {
            IQueryable<Microsoft.SharePoint.Client.User> query = from user in web.SiteUsers
                                                                 where user.IsSiteAdmin
                                                                 select user;

            IEnumerable<Microsoft.SharePoint.Client.User> allSiteAdminsitrators = web.Context.LoadQuery(query);
            web.Context.ExecuteQueryRetry();

            List<User> admins = new List<User>();
            foreach (var member in allSiteAdminsitrators)
            {
                admins.Add(new User() { Name = member.LoginName });
            }
            siteSecurity.AdditionalAdministrators.AddRange(admins);

        }

        protected void AddDefaultGroupsToSiteSecurity(Web web, SiteSecurity siteSecurity)
        {
            Group ownerGroup = web.AssociatedOwnerGroup;
            Group memberGroup = web.AssociatedMemberGroup;
            Group visitorGroup = web.AssociatedVisitorGroup;
            web.Context.ExecuteQueryRetry();

            if (!ownerGroup.ServerObjectIsNull.Value) { web.Context.Load(ownerGroup, o => o.Id, o => o.Users, o => o.Title); }
            if (!memberGroup.ServerObjectIsNull.Value) { web.Context.Load(memberGroup, o => o.Id, o => o.Users, o => o.Title); }
            if (!visitorGroup.ServerObjectIsNull.Value) { web.Context.Load(visitorGroup, o => o.Id, o => o.Users, o => o.Title); }
            web.Context.ExecuteQueryRetry();

            List<User> owners = new List<User>();
            List<User> members = new List<User>();
            List<User> visitors = new List<User>();

            if (!ownerGroup.ServerObjectIsNull.Value)
            {
                foreach (var member in ownerGroup.Users) { owners.Add(new User() { Name = member.LoginName }); }
            }

            if (!memberGroup.ServerObjectIsNull.Value)
            {
                foreach (var member in memberGroup.Users) { members.Add(new User() { Name = member.LoginName }); }
            }

            if (!visitorGroup.ServerObjectIsNull.Value)
            {
                foreach (var member in visitorGroup.Users) { visitors.Add(new User() { Name = member.LoginName }); }
            }

            siteSecurity.AdditionalOwners.AddRange(owners);
            siteSecurity.AdditionalMembers.AddRange(members);
            siteSecurity.AdditionalVisitors.AddRange(visitors);
        }

        #endregion

    }
}
