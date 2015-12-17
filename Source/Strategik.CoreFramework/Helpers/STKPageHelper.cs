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
using Strategik.Definitions.Configuration;
using Strategik.Definitions.Files;
using Strategik.Definitions.Pages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Strategik.CoreFramework.Helpers
{
    /// <summary>
    /// A helper for manipulating the various different types of pages we have
    /// </summary>
    public class STKPageHelper : STKHelperBase
    {
        #region Constructor

        public STKPageHelper(ClientContext clientContext)
            : base(clientContext)
        {
            if (clientContext == null) throw new ArgumentNullException("Client Context");
        }

        #endregion Constructor

        #region Implementation Methods

        public void EnsurePages(List<STKPublishingPage> pages, STKProvisioningConfiguration config = null)
        {
            foreach (STKPublishingPage page in pages)
            {
                _web.AddPublishingPage(page.Name, page.PageLayout, page.PageTitle, page.PublishOnProvisioning);
            }
        }

        public void EnsurePages(List<STKWebPartPage> pages, STKProvisioningConfiguration config = null)
        {
            foreach (STKWebPartPage page in pages)
            {
                //TODO
            }
        }

        public void EnsurePages(List<STKWikiPage> pages, STKProvisioningConfiguration config = null)
        {
            foreach (STKWikiPage page in pages)
            {
                //TODO
            }
        }

        public void EnsureMasterPages(List<STKMasterPage> masterPages, STKProvisioningConfiguration config = null)
        {
            foreach (STKMasterPage masterPage in masterPages)
            {
                STKFile file = STKFileHelper.GetFile(masterPage);
                if (file != null) {
                    _clientContext.Web.DeployMasterPage(file, masterPage.Title, masterPage.Description, "15", "", masterPage.FolderPath);
                }
            }
        }

        public void EnsurePageLayouts(List<STKPageLayout> pagesLayouts, STKProvisioningConfiguration config = null)
        {
            foreach (STKPageLayout pageLayout in pagesLayouts)
            {
                STKFile file = STKFileHelper.GetFile(pageLayout);
                if (file != null)
                {
                    if (pageLayout.IsHTMLLayout)
                    {
                        _clientContext.Web.DeployHtmlPageLayout(file, pageLayout.Title, pageLayout.Description, pageLayout.FolderPath);
                    }
                    else
                    {
                        _clientContext.Web.DeployPageLayout(file, pageLayout.Title, pageLayout.Description, pageLayout.FolderPath);
                    }
                }
            }
        }

        public void EnsureStyleLibraryAssets(List<STKStyleLibraryAssets> assetLocations, STKProvisioningConfiguration config = null)
        {
            foreach (STKStyleLibraryAssets assetLocation in assetLocations)
            {
                STKFolder assetFolder = STKFileHelper.GetAssets(assetLocation);
                DeployAssetsFolder(assetFolder);
            }
        }

        private void DeployAssetsFolder(STKFolder assetFolder)
        {
            throw new NotImplementedException(); // TODO
        }

        /// <summary>
        /// Downloads a list of the masterpages in the current context 
        /// </summary>
        /// <param name="downloadFiles">downloads the files</param>
        /// <returns></returns>
        public STKFolder GetMasterPages(bool downloadFiles, List<String> folderMatch = null)
        {
            List masterPageGallery = _web.GetMasterpageGallery();
            STKFileHelper fileHelper = new STKFileHelper(_clientContext);
            STKFolder rootFolder = fileHelper.GetFolder(masterPageGallery.RootFolder, downloadFiles, true, folderMatch);
            return rootFolder;
        }

        public STKFolder GetStyleLibraryAssets(bool downloadFiles, List<String> folderMatch = null)
        {
            List styleLibrary = _web.GetStyleLibrary();
            STKFileHelper fileHelper = new STKFileHelper(_clientContext);
            STKFolder rootFolder = fileHelper.GetFolder(styleLibrary.RootFolder, downloadFiles, true, folderMatch);
            return rootFolder;
        }

        /// <summary>
        /// Downloads a list of the page layotus in the current context
        /// </summary>
        /// <param name="downloadFiles">downlaods the files</param>
        /// <returns></returns>
        public List<STKFile> GetPageLayouts(bool downloadFiles)
        {
            List<STKFile> pages = new List<STKFile>();


            return pages;
        }

        /// <summary>
        /// Copy masterpage gallery contents from current context to target
        /// </summary>
        /// <param name="targetContext">Where we are copying to</param>
        /// <param name="includeSubFolders">true is subfolders need to be copied</param>
        /// <param name="overwriteTarget">overwite any identical files found on the target</param>
        /// <param name="folderMatch">Limits the subfolders included if specified</param>

        public void SyncMasterPageGallery(ClientContext targetContext, bool includeSubFolders, bool overwriteTarget, List<String> folderMatch = null)
        {
            STKFolder sourceRootFolder = GetMasterPages(true, folderMatch);

            STKPageHelper targetPageHelper = new STKPageHelper(targetContext);
            STKFolder targetRootFolder = GetMasterPages(false, folderMatch);

            foreach (STKFile file in sourceRootFolder.Files)
            {
                STKFile existingFile = targetRootFolder.Files.Where(f => f.FileName.ToLower() == file.FileName.ToLower()).SingleOrDefault();
                if (existingFile == null || overwriteTarget) // it aint there or we dont care
                {
                    
                }
            }


        }

        #endregion 
    }
}