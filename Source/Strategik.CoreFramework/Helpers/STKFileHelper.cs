using Microsoft.SharePoint.Client;
using Strategik.Definitions.Files;
using Strategik.Definitions.Pages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategik.CoreFramework.Helpers
{
    /// <summary>
    /// Helper for manipulating files
    /// </summary>
    public class STKFileHelper: STKHelperBase
    {

        #region Constructor

        public STKFileHelper(ClientContext context)
            : base(context)
        {}

        #endregion

        #region Static Methods
        public static STKFile GetFile(STKPage page)
        {
            STKFile file = new STKFile {FileName = page.FileName };

            switch (page.LocationType)
            {
                case STKPageLocationType.Disk:
                    {
                        file.FileBytes = LoadFromDisk(page);
                        break;
                    }

                case STKPageLocationType.Assembly:
                    {
                        file.FileBytes = LoadFromAssembly(page);
                        break;
                    }

                case STKPageLocationType.Url:
                    {
                        file.FileBytes = LoadFromUrl(page);
                        break;
                    }

                default:
                    break;
            }

            return file;
        }

        private static byte[] LoadFromUrl(STKPage page)
        {
            byte[] bytes = new byte[] { };



            return bytes;
        }

        private static byte[] LoadFromAssembly(STKPage page)
        {
            byte[] bytes = new byte[] { };



            return bytes;
        }

        private static byte[] LoadFromDisk(STKPage page)
        {
            byte[] bytes = new byte[] { };



            return bytes;
        }

        #endregion

        #region Get all the files and folders from a SharePoint folder

        public STKFolder GetFolder(Folder spFolder, bool downloadFile, bool includeSubFolders, List<String> folderMatch = null)
        {
            // Store this folders details
            STKFolder folder = new STKFolder()
            {
               Name = spFolder.Name,
               SharePointId = spFolder.UniqueId
            };

            // dont assume anything beneath us is already loaded
            _clientContext.Load(spFolder.Files);
            _clientContext.Load(spFolder.Folders);
            _clientContext.ExecuteQueryRetry();

            // recursively load our subfolders if required
            if (includeSubFolders)
            {
                foreach (Folder spSubFolder in spFolder.Folders)
                {
                    String name = spSubFolder.Name;
                    bool matchThisFolder = true;
                    if (folderMatch != null) // Selectively match the subfolders (first level only)
                    {
                        matchThisFolder = folderMatch.Contains(name);
                    }

                    if (matchThisFolder)
                    {
                        // Dont pass the matching pattern here - if a first level folder is matched we get everything under it
                        folder.Folders.Add(GetFolder(spSubFolder, downloadFile, includeSubFolders));
                    }
                }
            }

            // load all the files
            foreach (File spFile in spFolder.Files)
            {
                STKFile file = new STKFile()
                {
                    FileName = spFile.Name,
                    LinkingUrl = spFile.LinkingUrl,
                    Title = spFile.Title,
                    MajorVersion = spFile.MajorVersion,
                    MinorVersion = spFile.MinorVersion,
                    Created = spFile.TimeCreated,
                    LastModified = spFile.TimeLastModified,
                };

                if (downloadFile)
                {
                    file.FileBytes = DownloadFile(spFile);
                }

                // Get the details from the list item associated with this file
                // This will tell us what it is (e.g. a masterpage) etc
                ListItem spListItem = spFile.ListItemAllFields;
                _clientContext.Load(spListItem);
                _clientContext.ExecuteQueryRetry();
                file.ListItem = ProcessListItem(spListItem);

                folder.Files.Add(file);
            }

            return folder;
        }

        private Dictionary<String, Object> ProcessListItem(ListItem spListItem)
        {
            Dictionary<String, Object> listItem = new Dictionary<String, Object>();

            foreach(String key in spListItem.FieldValues.Keys)
            {
                Object value = null;
                spListItem.FieldValues.TryGetValue(key, out value);

                if (value != null)
                {
                    Debug.WriteLine(key + " has value " + value + " and type " + value.GetType().Name);

                    String stringValue = value as String;
                    if (String.IsNullOrEmpty(stringValue) == false)
                    {
                        listItem.Add(key, stringValue);
                    }
                }
            }

            return listItem;
        }

        private byte[] DownloadFile(File file)
        {
            byte[] data = new byte[0];
            FileInformation fileInformation = File.OpenBinaryDirect(_clientContext, file.ServerRelativeUrl);
            data = STKFileHelper.ReadFully(fileInformation.Stream); 
            return data ;
        }

        #endregion

        public static byte[] ReadFully(System.IO.Stream input)
        {
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())               
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }

        internal static STKFolder GetAssets(STKStyleLibraryAssets assetLocation)
        {
            STKFolder folder = new STKFolder();
            //TODO
            return folder;
        }
    }
}
