#region License

//
// Copyright (c) 2015 Strategik Pty Ltd,
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
//
// Author:  Dr Adrian Colquhoun
//

#endregion License

using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core.Diagnostics;
using OfficeDevPnP.Core.Framework.Provisioning.Model;
using OfficeDevPnP.Core.Framework.Provisioning.ObjectHandlers;
using Strategik.CoreFramework.PnP.Framework.Provisioning.Providers.Strategik.Model;
using Strategik.Definitions.Configuration;
using Strategik.Definitions.ContentTypes;
using Strategik.Definitions.Features;
using Strategik.Definitions.Fields;
using Strategik.Definitions.Lists;
using Strategik.Definitions.Pages;
using Strategik.Definitions.Security;
using Strategik.Definitions.Security.Permissions;
using Strategik.Definitions.Security.Principals;
using Strategik.Definitions.Security.Roles;
using Strategik.Definitions.Sites;
using Strategik.Definitions.Taxonomy;
using Strategik.Definitions.UserInterface;
using System;
using System.Collections.Generic;
using Field = Microsoft.SharePoint.Client.Field;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Strategik
{
    /// <summary>
    /// Helper classs to provision Strategik definitions using the PnP framework
    /// </summary>
    /// <remarks>
    /// Takes Strategik definitions objects and converts them to their corresponding
    /// PnP template definitions so that they can be deployed using the
    /// PnP code (rather than ours) saving us heaps of work.
    ///
    /// Thanks!
    /// </remarks>
    public class STKPnPHelper
    {

        #region Data

        private const String LogSource = "CoreFramework.PnPHelper";

        protected ClientContext _clientContext;
        protected Web _web;
        protected Site _site;
        
        #endregion Data

        #region Constructor

        public STKPnPHelper(ClientContext clientContext)
        {
            if (clientContext == null) throw new ArgumentNullException("clientContext");

            _clientContext = clientContext;
            _clientContext.Load(_clientContext.Web);
            _clientContext.Load(_clientContext.Site);

            _clientContext.ExecuteQueryRetry();

            _web = _clientContext.Web;
            _site = _clientContext.Site;

            Log.Debug(LogSource, "Initialised PnP helper {0}. Target web is {1} located at {2}", this.GetType().Name, _web.Title, _web.Url);
        }

        #endregion Constructor

        #region Provisioning Methods

        #region Taxonomy
        /// <summary>
        /// Provision a Strategik Taxonomy definition
        /// </summary>
        /// <remarks>
        /// Converts the taxonomy to to a PnP template then perfroms the provisioning
        /// using the core PnP code
        /// </remarks>
        /// <param name="taxonomy">The definition of the taxonomy</param>
        public void Provision(STKTaxonomy taxonomy, STKProvisioningConfiguration config = null)
        {
            Log.Debug(LogSource, "Starting provisioning for taxonomy {0}", taxonomy.UniqueId);

            ProvisioningTemplate template = new STKPnPTemplate();
            template.TermGroups.AddRange(taxonomy.GeneratePnPTemplates());
           
            _web.ApplyProvisioningTemplate(template);

            Log.Debug(LogSource, "Provisioning for taxonomy {0} complete", taxonomy.UniqueId);
        }

        #endregion

        #region Fields

        public void Provision(List<STKField> fields, STKProvisioningConfiguration config = null)
        {
            if (fields == null) throw new ArgumentNullException("fields");
            if (config == null) config = new STKProvisioningConfiguration();

           // ProvisioningTemplate allSiteColumnsTemplate = new ProvisioningTemplate();
        

            foreach (STKField field in fields)
            {
                Provision(field, config);
            }
        }

        /// <summary>
        /// Provision a Strategik Site Column Definition
        /// </summary>
        /// <remarks>
        /// Converts the taxonomy to to a PnP template then performs the provisioning
        /// using the core PnP code
        /// </remarks>
        /// <param name="field">The definition of the site column</param>
        public void Provision(STKField field, STKProvisioningConfiguration config = null)
        {
            if (field == null) throw new ArgumentNullException("field");
            if (config == null) config = new STKProvisioningConfiguration();

            Log.Debug(LogSource, "Starting Provisioning {0} field {1}", field.GetType().Name, field.Name);

            if (field.IsBuiltInSiteColumn == true)
            {
                Log.Debug(LogSource, "Field {0} is a built in site column - skipping provisioning", field.Name);
                return;
            }

            // Check we have a valid definition 
            field.Validate();

            // Taxonomy columns can leave a hidden field behind - if we dont try and
            // delete it any attempt to provision the field again will fail
            if (field.SharePointType == STKFieldType.TaxonomyFieldType)
            {
                _web.STKCleanupTaxonomyHiddenField((STKTaxonomyField)field);
            }

            ProvisioningTemplate template = new STKPnPTemplate();
            
            template.SiteFields.Add(field.GeneratePnPTemplate());

            _web.ApplyProvisioningTemplate(template);
            

            // TODO - needs to be factored elsewhere / repated for multiple columns route
            // If the site column is a taxonomy column then we need to
            // wire it up to its termset
            if (field.SharePointType == STKFieldType.TaxonomyFieldType)
            {
                try
                {
                    Field spSiteColumn = _web.Fields.GetById(field.UniqueId);
                    _clientContext.ExecuteQueryRetry();
                    STKTaxonomyField taxonomyField = field as STKTaxonomyField;
                    _web.STKWireUpTaxonomyField(spSiteColumn, taxonomyField);
                }
                catch
                {
                    // oops
                }
            }

            Log.Debug(LogSource, "Provisioning {0} field {1} complete", field.GetType().Name, field.Name);
        }

        #endregion

        #region Content Types

        /// <summary>
        /// Provision a collection of content types
        /// </summary>
        /// <param name="contentTypes"></param>
        public void Provision(List<STKContentType> contentTypes, STKProvisioningConfiguration config = null)
        {
            if (contentTypes == null) throw new ArgumentNullException("contentTypes");
            if (config == null) config = new STKProvisioningConfiguration();

            foreach (STKContentType contentType in contentTypes)
            {
                Provision(contentType);
            }
        }

        /// <summary>
        /// Provision a Strategik Content Type definition
        /// </summary>
        /// <remarks>
        /// Converts the content type definition to to a PnP template then performs the provisioning
        /// using the core PnP code.
        ///
        /// Our content type definitions can contain both links to existing site columns and the
        /// definitions of new site columns to be provisioned "on the fly". We extract and new site
        /// columns and provision them first, beofre attempting to provision the content type.
        /// </remarks>
        /// <param name="contentType"></param>
        public void Provision(STKContentType contentType, STKProvisioningConfiguration config = null)
        {
            if (contentType == null) throw new ArgumentNullException("contentType");
            if (config == null) config = new STKProvisioningConfiguration();

            // Check we have a valid definition
            contentType.Validate();

            Log.Debug(LogSource, "Starting provisioning for content type {0}", contentType.Name);

            // Provision any new site columns defined in the content type
            Provision(contentType.SiteColumns);

            // Generate the PnP template
            ProvisioningTemplate template = new STKPnPTemplate();
            template.ContentTypes.Add(contentType.GeneratePnPTemplate());

            // Provision the content type
            _web.ApplyProvisioningTemplate(template);

            Log.Debug(LogSource, "Provisioning of content type {0} compelete", contentType.Name);
        }

        #endregion

        #region Lists & Libraries

        /// <summary>
        /// Lists and Libraries
        /// </summary>
        /// <param name="list"></param>
        public void Provision(STKList list, STKProvisioningConfiguration config = null)
        {
            if (list == null) throw new ArgumentNullException("list");
            if (config == null) config = new STKProvisioningConfiguration();

            // Check we have a valid definition
            list.Validate();

            Log.Debug(LogSource, "Starting provisioning for list {0}", list.Name);

            // Check if this list already exists and adjust accordingly
            if (_web.ListExists(list.Title))
            {
                // Remove any default data from the list definition - otherwise the same rows will be added again by the PnP
                if(list.Items.Count > 0) {
                    list.Items = new List<STKListItem>();
                }
            }

            // Provision any site columns embedded in the list definition
            Provision(list.SiteColumns);

            // Provision any Content types embedded in the list definition
            Provision(list.ContentTypes);

            // Generate the PnP template
            ProvisioningTemplate template = new STKPnPTemplate();
            template.Lists.Add(list.GeneratePnPTemplate());

            // Template Creation settings
            ProvisioningTemplateApplyingInformation info = new ProvisioningTemplateApplyingInformation();

            #region Expermental
            //info.MessagesDelegate = delegate(string message, ProvisioningMessageType messageType) 
            //{
            //    Console.WriteLine("wtf " + message + " with message type " + messageType);
            //};

            //info.ProgressDelegate = delegate (string message, int step, int total)
            //{
            //    Console.WriteLine("wtf " + message + " step " + step +  " of " + total);
            //};
            #endregion

            // Provision the list
            _web.ApplyProvisioningTemplate(template, info);

            Log.Debug(LogSource, "Provisioning for list {0} complete", list.Name);
        }

        public void Provision(List<STKList> lists, STKProvisioningConfiguration config = null) 
        {
            if (lists == null) throw new ArgumentNullException("lists");
            if (config == null) config = new STKProvisioningConfiguration();

            foreach (STKList list in lists) 
            {
                Provision(list, config);
            }
        }

        #endregion

        #region Pages

        /// <summary>
        /// Pages
        /// </summary>
        /// <param name="page"></param>
        public void Provision(STKPage page, STKProvisioningConfiguration config = null)
        {
            // Provision the list
            ProvisioningTemplate template = new STKPnPTemplate();
            template.Pages.Add(page.GeneratePnPTemplate());
        
            _web.ApplyProvisioningTemplate(template);
        }

        #endregion

        #region Custom Actions

        /// <summary>
        /// Custom actions
        /// </summary>
        /// <param name="customAction"></param>
        public void Provision(STKCustomActions customAction, STKProvisioningConfiguration config = null)
        {
            // Generate the PnP template
            ProvisioningTemplate template = new STKPnPTemplate();
            template.CustomActions = customAction.GeneratePnPTemplate();

            _web.ApplyProvisioningTemplate(template);
        }

        #endregion

        #region Composed Looks

        // Composed looks
        public void Provision(STKComposedLook composedLook)
        {
            // Generate the PnP template
            ProvisioningTemplate template = new STKPnPTemplate();
            template.ComposedLook = composedLook.GeneratePnPTemplate();

            _web.ApplyProvisioningTemplate(template);
        }

        #endregion

        #region Features

        // Features
        public void Provision(STKFeatures features, STKProvisioningConfiguration config = null)
        {
            // Generate the PnP template
            ProvisioningTemplate template = new STKPnPTemplate();

            foreach (STKFeature siteFeature in features.SiteFeatures)
            {
                template.Features.SiteFeatures.Add(siteFeature.GeneratePnPTemplate());
            }

            foreach (STKFeature webFeature in features.WebFeatures)
            {
                template.Features.WebFeatures.Add(webFeature.GeneratePnPTemplate());
            }

            
            _web.ApplyProvisioningTemplate(template);
        }

        #endregion

        #region Groups

        public void Provision(List<STKGroup> groups, STKProvisioningConfiguration config = null)
        {

            if (groups == null) throw new ArgumentNullException("groups");
            if (config == null) config = new STKProvisioningConfiguration();

            foreach (STKGroup group in groups)
            {
                Provision(group, config);
            }
        }

        public void Provision(STKGroup group, STKProvisioningConfiguration config = null)
        {

            if (group == null) throw new ArgumentNullException("group");
            if (config == null) config = new STKProvisioningConfiguration();

            // Check we have a valid definition 
            group.Validate();

            
            ProvisioningTemplate template = new STKPnPTemplate();
            template.Security.SiteGroups.Add(group.GeneratePnPTemplate());

            _web.ApplyProvisioningTemplate(template);

        }

        public List<STKGroup> ReadGroups()
        {
            return _web.ReadGroups();
        }

        #endregion

        #region Role Definitions

        public void Provision(List<STKRoleDefinition> roleDefinitions, STKProvisioningConfiguration config = null)
        {

            if (roleDefinitions == null) throw new ArgumentNullException("roleDefinitions");
            if (config == null) config = new STKProvisioningConfiguration();

            foreach (STKRoleDefinition roleDefinition in roleDefinitions)
            {
                Provision(roleDefinition, config);
            }
        }

        public void Provision(STKRoleDefinition roleDefinition, STKProvisioningConfiguration config = null)
        {

            if (roleDefinition == null) throw new ArgumentNullException("roleDefinition");
            if (config == null) config = new STKProvisioningConfiguration();

            // Check we have a valid definition 
            roleDefinition.Validate();


            ProvisioningTemplate template = new STKPnPTemplate();
            template.Security.SiteSecurityPermissions.RoleDefinitions.Add(roleDefinition.GeneratePnPTemplate());

            _web.ApplyProvisioningTemplate(template);

        }

        public List<STKRoleDefinition> ReadRoleDefinitions()
        {
            return _web.ReadRoleDefinitions();
        }

        #endregion

        #region Role Assignments


        public void Provision(List<STKRoleAssignment> roleAssignments, STKProvisioningConfiguration config = null)
        {

            if (roleAssignments == null) throw new ArgumentNullException("roleAssignments");
            if (config == null) config = new STKProvisioningConfiguration();

            foreach (STKRoleAssignment roleAssignment in roleAssignments)
            {
                Provision(roleAssignment, config);
            }
        }

        public void Provision(STKRoleAssignment roleAssignment, STKProvisioningConfiguration config = null)
        {

            if (roleAssignment == null) throw new ArgumentNullException("roleAssignment");
            if (config == null) config = new STKProvisioningConfiguration();

            // Check we have a valid definition 
            roleAssignment.Validate();


            ProvisioningTemplate template = new STKPnPTemplate();
            template.Security.SiteSecurityPermissions.RoleAssignments.Add(roleAssignment.GeneratePnPTemplate());

            _web.ApplyProvisioningTemplate(template);

        }

        public List<STKRoleAssignment> ReadRoleAssignments()
        {
            return _web.ReadRoleAssignments();
        }

        #endregion

        // Files
        // TODO: Understand how this works in the engine

        #endregion Provisioning Methods

        #region Read Definition Methods

        public List<STKField> ReadSiteColumns()
        {
            return _web.ReadSiteColumns();
        }

        public List<STKContentType> ReadContentTypes()
        {
            return _web.ReadContentTypes();
        }

        public List<STKList> ReadLists()
        {
            return _web.ReadLists();
        }

        public STKWeb ReadWeb()
        {
            return _web.ReadWeb();
        }

        #endregion
    }
}