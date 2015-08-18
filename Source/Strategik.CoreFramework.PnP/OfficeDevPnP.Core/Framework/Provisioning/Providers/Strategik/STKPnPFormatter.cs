using OfficeDevPnP.Core.Framework.Provisioning.Model;
using OfficeDevPnP.Core.Utilities;
using Strategik.Definitions.O365.ContentTypes;
using Strategik.Definitions.O365.Fields;
using Strategik.Definitions.O365.Lists;
using Strategik.Definitions.O365.Sites;
using Strategik.Definitions.O365.Solutions;
using Strategik.Definitions.O365.Taxonomy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Strategik
{
    public class STKPnPFormatter: ITemplateFormatter
    {
        #region ITemplate Formatter Implementation

        public void Initialize(TemplateProviderBase provider)
        {
            throw new NotImplementedException();
        }

        public bool IsValid(Stream template)
        {
            throw new NotImplementedException();
        }

        public Stream ToFormattedTemplate(Model.ProvisioningTemplate template)
        {
            throw new NotImplementedException();
        }

        public ProvisioningTemplate ToProvisioningTemplate(System.IO.Stream template)
        {
            throw new NotImplementedException();
        }

        public ProvisioningTemplate ToProvisioningTemplate(System.IO.Stream template, string identifier)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Strategik template conversions

        /// <summary>
        /// Generates all the P&P templates greaquied to deploy a strategik solution
        /// </summary>
        /// <param name="solution"></param>
        /// <returns></returns>
        public List<ProvisioningTemplate> ToProvisioningTemplate(STKO365Solution solution)
        {
            Log.Debug(STKConstants.LOGGING_SOURCE, "Generating PnP template from Strategik solution definition");
            
            List<ProvisioningTemplate> templates = new List<ProvisioningTemplate>();
            
            // global template
            Log.Debug(STKConstants.LOGGING_SOURCE, "Generating global template");
            ProvisioningTemplate pnpTemplate = new ProvisioningTemplate();
            
            pnpTemplate.Id = solution.UniqueId.ToString();
            pnpTemplate.PropertyBagEntries.Add(new PropertyBagEntry() { Key = solution.GenerateTag() });

            pnpTemplate.TermGroups.AddRange(GenerateTermGroupTemplates(solution.Taxonomy, pnpTemplate));
            
            templates.Add(pnpTemplate);

            // Return a provisioning template per site collection
            foreach (STKSite site in solution.Sites) 
            {
                Log.Debug(STKConstants.LOGGING_SOURCE, "Generating template for site {0}", site.Name);
                ProvisioningTemplate siteTemplate = new ProvisioningTemplate();

              //  siteTemplate.ComposedLook = site.RootWeb.ComposedLook;

                pnpTemplate.SiteFields.AddRange(GenerateSiteColumnTemplates(site.RootWeb.SiteColumns, siteTemplate));
                pnpTemplate.ContentTypes.AddRange(GenerateContentTypeTemplates(site.RootWeb.ContentTypes, siteTemplate));

                //template.CustomActions; TODO
                //template.Features; TODO
                //template.Files; TODO

                pnpTemplate.Lists.AddRange(GenerateListProvisioningTemplates(site.RootWeb.Lists, pnpTemplate));
                templates.Add(pnpTemplate);

                //rootWebTemplate.Pages;
                //rootWebTemplate.Security;
            }

            //sub webs ??

            return templates;
        }

        /// <summary>
        /// Convert Strategik taxonomy definitions to pnp TermGroups
        /// </summary>
        /// <param name="taxonomies"></param>
        /// <returns></returns>
        private List<TermGroup> GenerateTermGroupTemplates(List<STKTaxonomy> taxonomies, ProvisioningTemplate pnpTemplate)
        {
            List<TermGroup> termGroups = new List<TermGroup>();

            foreach (STKTaxonomy taxonomy in taxonomies) 
            {
                termGroups.AddRange(taxonomy.GeneratePnPTemplates());
            }

            return termGroups;
        }

       
        private List<ContentType> GenerateContentTypeTemplates(List<STKContentType> contentTypes, ProvisioningTemplate template)
        {
            List<ContentType> contentTypeTemplates = new List<ContentType>();

            foreach (STKContentType contentType in contentTypes)
            {
                ContentType contentTypeTemplate = new ContentType()
                {
                    Description = contentType.Description,
                    //DocumentTemplate
                    Group = contentType.GroupName,
                    Hidden = contentType.Hidden,
                    Id = contentType.SharePointContentTypeId,
                    Name = contentType.Name,
                    Overwrite = contentType.OverwriteIfPresent,
                    ReadOnly = contentType.ReadOnly,
                    Sealed = contentType.Sealed
                };
                
                foreach(STKFieldLink fieldLink in contentType.SiteColumnLinks )
                {
                    FieldRef fieldRef = new FieldRef()
                    {
                        DisplayName = fieldLink.DisplayName,
                        Hidden = fieldLink.IsHidden,
                        Id = fieldLink.SiteColumnId,
                        Required = fieldLink.IsRequired
                    };

                    contentTypeTemplate.FieldRefs.Add(fieldRef);
                }

                //loop the site columns - add to template and add to link if required.
                
            }

            return contentTypeTemplates;
        }

        private List<Field> GenerateSiteColumnTemplates(List<STKField> siteColumns, ProvisioningTemplate template)
        {
            List<Field> fieldTemplates = new List<Field>();

            foreach(STKField siteColumn in siteColumns)
            {
                Field fieldTemplate = new Field();
                fieldTemplate.SchemaXml = siteColumn.GetProvisioningXML();
            }

            return fieldTemplates;
        }


        #endregion

        #region Helper Methods


        private List<ListInstance> GenerateListProvisioningTemplates(List<STKList> stkLists, ProvisioningTemplate template)
        {
            List<ListInstance> lists = new List<ListInstance>();

            foreach (STKList list in stkLists)
            {
                
                    
                ListInstance listInstance = new ListInstance() 
                { 
                    ContentTypesEnabled = list.EnableContentTypes,
                    //DataRows
                    Description = list.Description,
                   // DocumentTemplate = list.
                  // DraftVersionVisibility = list.
                    EnableAttachments = list.EnableAttachments,
                  EnableFolderCreation = list.AllowFolders,
                //  EnableMinorVersions = 
                EnableVersioning = list.EnableVersioning,
                EnableModeration = list.EnableModeration,
              //  FieldRefs = list.
                Hidden = list.Hidden,
              //  MaxVersionLimit = list.M
                 OnQuickLaunch = list.OnQuickLaunch,
                 RemoveExistingContentTypes = list.RemoveExistingContentTypes,
                 TemplateType = list.SharePointListId,
                 Title = list.Title,
                 Url = list.Url
                 //Views
                };

                listInstance.Fields.AddRange(GenerateFieldTemplates(list.Fields));
                lists.Add(listInstance);
                
            }
            return lists;
        }

        private List<Field> GenerateFieldTemplates(List<STKField> fields)
        {
            throw new NotImplementedException();
        }


        #endregion
    }
}
