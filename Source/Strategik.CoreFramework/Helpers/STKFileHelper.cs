using Strategik.Definitions.Files;
using Strategik.Definitions.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategik.CoreFramework.Helpers
{
    /// <summary>
    /// Helper for manipulating files
    /// </summary>
    public static class STKFileHelper
    {
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

        internal static STKFolder GetAssets(STKStyleLibraryAssets assetLocation)
        {
            STKFolder folder = new STKFolder();
            //TODO
            return folder;
        }
    }
}
