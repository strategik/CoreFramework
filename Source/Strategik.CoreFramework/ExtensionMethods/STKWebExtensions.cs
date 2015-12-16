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

using System;
using Strategik.Definitions.Fields;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using OfficeDevPnP.Core.Framework.Provisioning.ObjectHandlers;
using Strategik.Definitions.Taxonomy;
using OfficeDevPnP.Core.Diagnostics;
using OfficeDevPnP.Core;
using Strategik.Definitions.Files;

namespace Microsoft.SharePoint.Client
{

    public static partial class STKWebExtensions
    {
        // Copied from PnP
        internal const String LogSoure = "Strategik.CoreFramework.STKWebExtensions";
        internal const string THEMES_DIRECTORY = "/_catalogs/theme/15/{0}";
        internal const string MASTERPAGE_SEATTLE = "/_catalogs/masterpage/seattle.master";
        internal const string MASTERPAGE_DIRECTORY = "/_catalogs/masterpage/{0}";
        internal const string MASTERPAGE_CONTENT_TYPE = "0x01010500B45822D4B60B7B40A2BFCC0995839404";
        internal const string PAGE_LAYOUT_CONTENT_TYPE = "0x01010007FF3E057FA8AB4AA42FCB67B453FFC100E214EEE741181F4E9F7ACC43278EE811";
        internal const string HTMLPAGE_LAYOUT_CONTENT_TYPE = "0x01010007FF3E057FA8AB4AA42FCB67B453FFC100E214EEE741181F4E9F7ACC43278EE8110003D357F861E29844953D5CAA1D4D8A3B001EC1BD45392B7A458874C52A24C9F70B";


        public static void DeployPageLayout(this Web web, STKFile file, string title, string description, string associatedContentTypeID, string folderHierarchy = "")
        {
            web.DeployMasterPageGalleryItem(file, title, description, associatedContentTypeID, PAGE_LAYOUT_CONTENT_TYPE, folderHierarchy);
        }

        public static void DeployHtmlPageLayout(this Web web, STKFile file, string title, string description, string associatedContentTypeID, string folderHierarchy = "")
        {
            web.DeployMasterPageGalleryItem(file, title, description, associatedContentTypeID, HTMLPAGE_LAYOUT_CONTENT_TYPE, folderHierarchy);
        }

        // A clone of the PnP version so we can deploy resources embedded within our assemblies
        private static void DeployMasterPageGalleryItem(this Web web, STKFile file, string title, string description, string associatedContentTypeID, string itemContentTypeId, string folderHierarchy = "")
        {
            
            // Get the path to the file which we are about to deploy
            List masterPageGallery = web.GetCatalog((int)ListTemplateType.MasterPageCatalog);
            Folder rootFolder = masterPageGallery.RootFolder;
            web.Context.Load(masterPageGallery);
            web.Context.Load(rootFolder);
            web.Context.ExecuteQueryRetry();

            // Create folder structure inside master page gallery, if does not exists
            // For e.g.: _catalogs/masterpage/contoso/
            web.EnsureFolder(rootFolder, folderHierarchy);

           
            // Use CSOM to upload the file in
            FileCreationInformation newFile = new FileCreationInformation();
            newFile.Content = file.FileBytes;
            newFile.Url = UrlUtility.Combine(rootFolder.ServerRelativeUrl, folderHierarchy, file.FileName);
            newFile.Overwrite = true;

            File uploadFile = rootFolder.Files.Add(newFile);
            web.Context.Load(uploadFile);
            web.Context.ExecuteQueryRetry();

            // Check out the file if needed
            if (masterPageGallery.ForceCheckout || masterPageGallery.EnableVersioning)
            {
                if (uploadFile.CheckOutType == CheckOutType.None)
                {
                    uploadFile.CheckOut();
                }
            }

            // Get content type for ID to assign associated content type information
            ContentType associatedCt = web.GetContentTypeById(associatedContentTypeID);

            var listItem = uploadFile.ListItemAllFields;
            listItem["Title"] = title;
            listItem["MasterPageDescription"] = description;
            // set the item as page layout
            listItem["ContentTypeId"] = itemContentTypeId;
            // Set the associated content type ID property
            listItem["PublishingAssociatedContentType"] = string.Format(";#{0};#{1};#", associatedCt.Name, associatedCt.Id);
            listItem["UIVersion"] = Convert.ToString(15); //TODO
            listItem.Update();

            // Check in the page layout if needed
            if (masterPageGallery.ForceCheckout || masterPageGallery.EnableVersioning)
            {
                uploadFile.CheckIn(string.Empty, CheckinType.MajorCheckIn);
                if (masterPageGallery.EnableModeration)
                {
                    listItem.File.Publish(string.Empty);
                }
            }

            web.Context.ExecuteQueryRetry();
        }

        public static void DeployMasterPage(this Web web, STKFile file, string title, string description, string uiVersion = "15", string defaultCSSFile = "", string folderPath = "")
        {
        
            // Get the path to the file which we are about to deploy
            List masterPageGallery = web.GetCatalog((int)ListTemplateType.MasterPageCatalog);
            Folder rootFolder = masterPageGallery.RootFolder;
            web.Context.Load(masterPageGallery);
            web.Context.Load(rootFolder);
            web.Context.ExecuteQueryRetry();

            // Create folder if does not exists
            if (!String.IsNullOrEmpty(folderPath))
            {
                web.EnsureFolder(rootFolder, folderPath);
            }

            // Use CSOM to upload the file in
            FileCreationInformation newFile = new FileCreationInformation();
            newFile.Content = file.FileBytes;
            newFile.Url = UrlUtility.Combine(rootFolder.ServerRelativeUrl, folderPath, file.FileName);
            newFile.Overwrite = true;

            File uploadFile = rootFolder.Files.Add(newFile);
            web.Context.Load(uploadFile);
            web.Context.ExecuteQueryRetry();


            var listItem = uploadFile.ListItemAllFields;
            if (masterPageGallery.ForceCheckout || masterPageGallery.EnableVersioning)
            {
                if (uploadFile.CheckOutType == CheckOutType.None)
                {
                    uploadFile.CheckOut();
                }
            }

            listItem["Title"] = title;
            listItem["MasterPageDescription"] = description;
            // Set content type as master page
            listItem["ContentTypeId"] = MASTERPAGE_CONTENT_TYPE;
            listItem["UIVersion"] = uiVersion;
            listItem.Update();
            if (masterPageGallery.ForceCheckout || masterPageGallery.EnableVersioning)
            {
                uploadFile.CheckIn(string.Empty, CheckinType.MajorCheckIn);
                if (masterPageGallery.EnableModeration)
                {
                    listItem.File.Publish(string.Empty);
                }
            }
            web.Context.Load(listItem);
            web.Context.ExecuteQueryRetry();

        }

    }
}