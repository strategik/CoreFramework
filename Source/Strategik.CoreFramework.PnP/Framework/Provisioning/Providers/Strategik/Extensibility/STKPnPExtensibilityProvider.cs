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
            if (configurationData.Equals("ReadGroups"))
            {
                pnpTemplate = ReadGroups(web, pnpTemplate);
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

        protected ProvisioningTemplate ReadGroups(Web web, ProvisioningTemplate template)
        {
            // We have cloned the PnP read groups functionality here to add in the out of the box groups
            // and to ensure we read groups when processing a sub web
            // if this is a sub site then we're not creating security entities as by default security is inherited from the root site
            // Eseentially we are reading the site security again here for now

            Log.Debug(LogSource, "Read groups extension called");
            web.Context.Load(web, w => w.HasUniqueRoleAssignments, w => w.Title);

            var ownerGroup = web.AssociatedOwnerGroup;
            var memberGroup = web.AssociatedMemberGroup;
            var visitorGroup = web.AssociatedVisitorGroup;
            web.Context.ExecuteQueryRetry();

            if (!ownerGroup.ServerObjectIsNull.Value)
            {
                web.Context.Load(ownerGroup, o => o.Id, o => o.Users, o => o.Title);
            }
            if (!memberGroup.ServerObjectIsNull.Value)
            {
                web.Context.Load(memberGroup, o => o.Id, o => o.Users, o => o.Title);
            }
            if (!visitorGroup.ServerObjectIsNull.Value)
            {
                web.Context.Load(visitorGroup, o => o.Id, o => o.Users, o => o.Title);
            }

            web.Context.ExecuteQueryRetry();

            List<int> associatedGroupIds = new List<int>();
            var owners = new List<User>();
            var members = new List<User>();
            var visitors = new List<User>();
            if (!ownerGroup.ServerObjectIsNull.Value)
            {
                //associatedGroupIds.Add(ownerGroup.Id);  - we will include this group
                foreach (var member in ownerGroup.Users)
                {
                    owners.Add(new User() { Name = member.LoginName });
                }
            }
            if (!memberGroup.ServerObjectIsNull.Value)
            {
                // associatedGroupIds.Add(memberGroup.Id); - we will include this group
                foreach (var member in memberGroup.Users)
                {
                    members.Add(new User() { Name = member.LoginName });
                }
            }
            if (!visitorGroup.ServerObjectIsNull.Value)
            {
                // associatedGroupIds.Add(visitorGroup.Id); - we will include this group
                foreach (var member in visitorGroup.Users)
                {
                    visitors.Add(new User() { Name = member.LoginName });
                }
            }
            var siteSecurity = new SiteSecurity();
            siteSecurity.AdditionalOwners.AddRange(owners);
            siteSecurity.AdditionalMembers.AddRange(members);
            siteSecurity.AdditionalVisitors.AddRange(visitors);

            var query = from user in web.SiteUsers
                        where user.IsSiteAdmin
                        select user;
            var allUsers = web.Context.LoadQuery(query);

            web.Context.ExecuteQueryRetry();

            var admins = new List<User>();
            foreach (var member in allUsers)
            {
                admins.Add(new User() { Name = member.LoginName });
            }
            siteSecurity.AdditionalAdministrators.AddRange(admins);

            // include all the site groups

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

                foreach (var group in web.SiteGroups.AsEnumerable().Where(o => !associatedGroupIds.Contains(o.Id)))
                {
                    
                    var siteGroup = new SiteGroup()
                    {
                        //Title = group.Title.Replace(web.Title, "{sitename}"),
                        Title = group.Title,
                        AllowMembersEditMembership = group.AllowMembersEditMembership,
                        AutoAcceptRequestToJoinLeave = group.AutoAcceptRequestToJoinLeave,
                        AllowRequestToJoinLeave = group.AllowRequestToJoinLeave,
                        Description = group.Description,
                        OnlyAllowMembersViewMembership = group.OnlyAllowMembersViewMembership,
                        //    Owner = ReplaceGroupTokens(web, group.Owner.LoginName),
                        Owner = group.Owner.LoginName,
                        RequestToJoinLeaveEmailSetting = group.RequestToJoinLeaveEmailSetting
                    };

                    foreach (var member in group.Users)
                    {
                        siteGroup.Members.Add(new User() { Name = member.LoginName });
                    }
                    siteSecurity.SiteGroups.Add(siteGroup);
                }
            

            var webRoleDefinitions = web.Context.LoadQuery(web.RoleDefinitions.Include(r => r.Name, r => r.Description, r => r.BasePermissions, r => r.RoleTypeKind));
            web.Context.ExecuteQueryRetry();

            if (web.HasUniqueRoleAssignments)
            {
                var permissionKeys = Enum.GetNames(typeof(PermissionKind));

                foreach (var webRoleDefinition in webRoleDefinitions)
                {
                    if (webRoleDefinition.RoleTypeKind == RoleType.None)
                    {
                       
                        var modelRoleDefinitions = new OfficeDevPnP.Core.Framework.Provisioning.Model.RoleDefinition();

                        modelRoleDefinitions.Description = webRoleDefinition.Description;
                        modelRoleDefinitions.Name = webRoleDefinition.Name;
                        var permissions = new List<PermissionKind>();

                        foreach (var permissionKey in permissionKeys)
                        {
                            var permissionKind = (PermissionKind)Enum.Parse(typeof(PermissionKind), permissionKey);
                            if (webRoleDefinition.BasePermissions.Has(permissionKind))
                            {
                                modelRoleDefinitions.Permissions.Add(permissionKind);
                            }
                        }
                        siteSecurity.SiteSecurityPermissions.RoleDefinitions.Add(modelRoleDefinitions);
                    }
                    else
                    {
                       
                    }
                }

                var webRoleAssignments = web.Context.LoadQuery(web.RoleAssignments.Include(
                    r => r.RoleDefinitionBindings.Include(
                        rd => rd.Name,
                        rd => rd.RoleTypeKind),
                    r => r.Member.LoginName));

                web.Context.ExecuteQueryRetry();

                foreach (var webRoleAssignment in webRoleAssignments)
                {
                    if (webRoleAssignment.Member.LoginName != "Excel Services Viewers")
                    {
                        foreach (var roleDefinition in webRoleAssignment.RoleDefinitionBindings)
                        {
                            if (roleDefinition.RoleTypeKind != RoleType.Guest)
                            {
                                var modelRoleAssignment = new OfficeDevPnP.Core.Framework.Provisioning.Model.RoleAssignment();
                                modelRoleAssignment.RoleDefinition = roleDefinition.Name;
                                modelRoleAssignment.Principal = webRoleAssignment.Member.LoginName;
                                siteSecurity.SiteSecurityPermissions.RoleAssignments.Add(modelRoleAssignment);
                            }
                        }
                    }
                }

                template.Security = siteSecurity;
            }

            //TODO: Check we dont need this
            //// If a base template is specified then use that one to "cleanup" the generated template model
            //if (creationInfo.BaseTemplate != null)
            //{
            //    template = CleanupEntities(template, creationInfo.BaseTemplate);

            //}
        
            return template;
        }

    private string ReplaceGroupTokens(Web web, string loginName)
    {
        if (!web.AssociatedOwnerGroup.ServerObjectIsNull.Value)
        {
            loginName = loginName.Replace(web.AssociatedOwnerGroup.Title, "{associatedownergroup}");
        }
        if (!web.AssociatedMemberGroup.ServerObjectIsNull.Value)
        {
            loginName = loginName.Replace(web.AssociatedMemberGroup.Title, "{associatedmembergroup}");
        }
        if (!web.AssociatedVisitorGroup.ServerObjectIsNull.Value)
        {
            loginName = loginName.Replace(web.AssociatedVisitorGroup.Title, "{associatedvisitorgroup}");
        }
        return loginName;
    }
    #endregion

}
}
