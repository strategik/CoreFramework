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

#endregion License

using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core.Entities;
using OfficeDevPnP.Core.Framework.Provisioning.Providers.Strategik;
using Strategik.Definitions.Configuration;
using Strategik.Definitions.ContentTypes;
using Strategik.Definitions.Fields;
using Strategik.CoreFramework.Configuration;
using System;
using System.Collections.Generic;


namespace Strategik.CoreFramework.Helpers
{
    public class STKContentTypeHelper : STKHelperBase
    {
        #region Data

        private STKSiteColumnHelper _siteColumnHelper;

        #endregion

        #region Constructor

        public STKContentTypeHelper(ClientContext clientContext)
            : base(clientContext)
        {
            _siteColumnHelper = new STKSiteColumnHelper(clientContext);
        }

        #endregion Constructor

        // TODO: Implement provisioning sematics
        // TODO: Create complete unit test suite
        #region Ensure Content Types Methods

        public void EnsureContentTypes(List<STKContentType> contentTypes, STKProvisioningConfiguration config = null)
        {
            if (config == null) config = new STKProvisioningConfiguration();

            foreach (var contentType in contentTypes)
            {
                EnsureContentType(contentType, config);
            }
        }

        public void EnsureContentType(STKContentType contentType, STKProvisioningConfiguration config = null)
        {
            if (contentType == null) throw new ArgumentNullException("contentColumn");
            if (config == null) config = new STKProvisioningConfiguration();

            contentType.Validate();

            if (config.UsePnP && config.UsePnPForContentTypes)
            {
                STKPnPHelper pnpHelper = new STKPnPHelper(_clientContext);
                pnpHelper.Provision(contentType, config);
            }
            else
            {
                throw new NotImplementedException("Content Type provisioning is only implmented via the PnP code");
                #region old code - removed
                //ContentTypeCreationInformation createInfo = GenerateContentTypeCreationInfo(stkContentType);
                //ContentType spContentType = CreateContentTypeIfDoesNotExist(createInfo, stkContentType.SharePointContentTypeId);

                //EnsureContentTypeFields(stkContentType, spContentType);
                //EnsureContentTypeFieldLinks(stkContentType);

                //return spContentType;
                #endregion
            }
        }

        #region redundant code to remove

        //private void EnsureContentTypeFields(STKContentType stkContentType, ContentType spContentType)
        //{
        //    foreach (STKField stkSiteColumn in stkContentType.SiteColumns)
        //    {
        //        Field spField = _siteColumnHelper.EnsureSiteColumn(stkSiteColumn);
        //        _web.AddFieldToContentType(spContentType, spField, stkSiteColumn.Required, stkSiteColumn.Hidden);
        //    }
        //}

        //private void EnsureContentTypeFieldLinks(STKContentType stkContentType)
        //{
        //    foreach (STKFieldLink stkFieldLink in stkContentType.SiteColumnLinks)
        //    {
        //        _web.AddFieldToContentTypeById(stkContentType.SharePointContentTypeId, stkFieldLink.SiteColumnId.ToString(), stkFieldLink.IsRequired, stkFieldLink.IsHidden);
        //    }
        //}

        //private ContentTypeCreationInformation GenerateContentTypeCreationInfo(STKContentType contentType)
        //{
        //    // Create the content type defintion to a format we can execute agaisnt Office 365
        //    ContentTypeCreationInformation createInfo = new ContentTypeCreationInformation()
        //    {
        //        Id = contentType.SharePointContentTypeId,
        //        Name = contentType.Name,
        //        Group = contentType.GroupName,
        //        Description = contentType.Description
        //    };

        //    return createInfo;
        //}

        //private ContentType CreateContentTypeIfDoesNotExist(ContentTypeCreationInformation contentTypeCreateInfo, String contentTypeId)
        //{
        //    ContentType contentType = null;

        //    ContentTypeCollection contentTypes = _web.ContentTypes;
        //    _clientContext.Load(contentTypes);
        //    _clientContext.ExecuteQueryRetry();

        //    foreach (ContentType item in contentTypes)
        //    {
        //        if (item.StringId == contentTypeId)
        //        {
        //            contentType = item;
        //            break;
        //        }
        //    }

        //    if (contentType == null)
        //    {
        //        contentType = contentTypes.Add(contentTypeCreateInfo);
        //        _clientContext.ExecuteQueryRetry();
        //    }

        //    return contentType;
        //}

        //private void AddSiteColumnsToContentType(List<FieldCreationInformation> fields, String contentTypeId)
        //{
        //    ContentType contentType = _web.GetContentTypeById(contentTypeId);
        //    _clientContext.ExecuteQueryRetry();

        //    foreach (FieldCreationInformation fieldInfo in fields)
        //    {
        //        Field field = EnsureSiteColumn(fieldInfo);
        //        EnsureFieldLink(contentType, field);
        //    }
        //}

        //private Field EnsureSiteColumn(FieldCreationInformation fieldInfo)
        //{
        //    // Look up the field
        //    FieldCollection fields = _web.Fields;
        //    _clientContext.Load(fields);
        //    _clientContext.ExecuteQuery();

        //    foreach (var field in fields)
        //    {
        //        if (field.InternalName == fieldInfo.InternalName && field.Group == fieldInfo.Group) return field;
        //    }

        //    // Create it if it doesn't already exist
        //    // TODO - check the same name different group scenario - is this an error
        //    Field newField = _web.CreateField(fieldInfo);
        //    _clientContext.ExecuteQueryRetry();
        //    return newField;
        //}

        //private void EnsureFieldLink(ContentType contentType, Field field)
        //{
        //    FieldLinkCollection fieldLinks = contentType.FieldLinks;
        //    _clientContext.Load(fieldLinks);
        //    _clientContext.ExecuteQueryRetry();

        //    foreach (FieldLink fieldLink in fieldLinks)
        //    {
        //        if (fieldLink.Name == field.InternalName) return; // check this is the right property match
        //    }

        //    // Create the link and add it
        //    // ref does nt
        //    FieldLinkCreationInformation link = new FieldLinkCreationInformation();
        //    link.Field = field;
        //    contentType.FieldLinks.Add(link);
        //    contentType.Update(true);
        //    _clientContext.ExecuteQueryRetry();
        //}

        #endregion Ensure Content Types Methods

        //#region Ensure Site Columns

        //public List<Field> EnsureSiteColumns(List<STKField> siteColumns, STKProvisioningConfiguration config = null)
        //{
        //    if (siteColumns == null) throw new ArgumentNullException("siteColumns");
        //    if (config == null) config = new STKProvisioningConfiguration();

        //    List<Field> fields = new List<Field>();

        //    foreach (STKField siteColumn in siteColumns)
        //    {
        //        fields.Add(EnsureSiteColumn(siteColumn, config));
        //    }

        //    return fields;
        //}

        //public Field EnsureSiteColumn(STKField siteColumn, STKProvisioningConfiguration config = null)
        //{
        //    if (siteColumn == null) throw new ArgumentNullException("siteColumn");
        //    if (config == null) config = new STKProvisioningConfiguration();
        //    Field field = null;

        //    siteColumn.Validate();

        //    if (config.UsePnP && config.UsePnpForSiteColumns)
        //    {
        //        STKPnPHelper pnpHelper = new STKPnPHelper(_clientContext);
        //        pnpHelper.Provision(siteColumn, config);
        //    }
        //    else
        //    {
        //        #region old code



        //        // Look up the field
        //        FieldCollection fields = _web.Fields;
        //        _clientContext.Load(_web.Fields);
        //        _clientContext.ExecuteQueryRetry();

        //        foreach (var existingField in fields)
        //        {
        //            if (existingField.Id == siteColumn.UniqueId) field = existingField;
        //        }

        //        if (field == null)
        //        {
        //            if (siteColumn.SharePointType == STKFieldType.TaxonomyFieldType)
        //            {
        //                _web.CleanupTaxonomyHiddenField((STKTaxonomyField)siteColumn);
        //            }

        //            String xml = siteColumn.GetProvisioningXML();
        //            field = _web.CreateField(xml, true);

        //            // Make sure all the fields we want are loaded
        //            _clientContext.Load(field,
        //                f => f.Id,
        //                f => f.InternalName,
        //                f => f.Group);

        //            _clientContext.ExecuteQueryRetry();
        //        }

        //        // If the site column is a taxonomy column then we need to
        //        // wire it up to its termset
        //        if (siteColumn.SharePointType == STKFieldType.TaxonomyFieldType)
        //        {
        //            _web.WireUpTaxonomyField(field, (STKTaxonomyField)siteColumn);
        //        }
        //        #endregion
        //    }

        //    return field;
        //}

        //#endregion

        #region Read Content Types


        #endregion

        //#region Read Site Columns

        ///// <summary>
        ///// Reads the defintions for all the site columsn in the current site
        ///// </summary>
        ///// <returns></returns>
        //public List<STKField> ReadSiteColumns()
        //{
        //    // Delegated to the PnPHelper
        //    STKPnPHelper ppHelper = new STKPnPHelper(_clientContext);
        //    return ppHelper.ReadSiteColumns();
        //}


        //#endregion

        #endregion

        #region Read Content Types

        /// <summary>
        /// Reads the defintions for all the site columsn in the current site
        /// </summary>
        /// <returns></returns>
        public List<STKContentType> ReadContentTypes()
        {
            return null;
            // Delegated to the PnPHelper
            //STKPnPHelper stkPnPHelper = new STKPnPHelper(_clientContext);
            //return stkPnPHelper.ReadContentTypes();
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Checks a list of STKContentType definiions to ensure there are no duplicated id's or SharePoint ids
        /// </summary>
        /// <remarks>
        /// Duplicated id's are a common cut and paste error when creating definitions.
        /// We cannot deploy two content types with the same SharePoint id
        /// </remarks>
        /// <param name="contentTypes"></param>
        public static void Validate(List<STKContentType> contentTypes)
        {
            List<Guid> idChecks = new List<Guid>();
            List<String> sharePointIdChecks = new List<String>();

            foreach (STKContentType contentType in contentTypes)
            {
                contentType.Validate();

                if (idChecks.Contains(contentType.UniqueId))
                {
                    throw new Exception("A duplicate id was detected " + contentType.UniqueId);
                }
                else
                {
                    idChecks.Add(contentType.UniqueId);
                }

                if (sharePointIdChecks.Contains(contentType.SharePointContentTypeId))
                {
                    throw new Exception("A duplicate SharePoint Id was detected " + contentType.SharePointContentTypeId);
                }
                else
                {
                    sharePointIdChecks.Add(contentType.SharePointContentTypeId);
                }
            }
        }

        #endregion
    }
}