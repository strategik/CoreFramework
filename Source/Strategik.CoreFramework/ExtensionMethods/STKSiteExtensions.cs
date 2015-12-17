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

using Microsoft.SharePoint.Client.Publishing;
using Strategik.Definitions.Features;
using System;
using System.IO;
using System.Reflection;

namespace Microsoft.SharePoint.Client
{
    /// <summary>
    /// A set of extension methods for the Site object
    /// </summary>
    public static partial class STKSiteExtensions
    {
        #region Install a sandboxed solution

        /// <summary>
        /// This extension method installs a sandboxed solution
        /// </summary>
        /// <param name="site">the site</param>
        /// <param name="solution">Our sandbox solution defintion</param>
        public static void InstallSandboxSolution(this Site site, STKSandboxSolution solution)
        {
            string fileName = solution.FileName;

            Web rootWeb = site.RootWeb;
            //var sourceFileName = Path.GetFileName(sourceFilePath);
            Stream stream = GetSolutionStream(solution);

            // Get the root folder of the root web
            Folder rootFolder = rootWeb.RootFolder;
            rootWeb.Context.Load(rootFolder, f => f.ServerRelativeUrl);
            rootWeb.Context.ExecuteQueryRetry();

            rootFolder.UploadFile(fileName, stream, true);

            var packageInfo = new DesignPackageInfo()
            {
                PackageName = fileName,
                PackageGuid = solution.SolutionId,
                MajorVersion = solution.MajorVersion,
                MinorVersion = solution.MinorVersion,
            };

            try // try and unisntall in already present
            {
                DesignPackage.UnInstall(site.Context, site, packageInfo);
                site.Context.ExecuteQueryRetry();
            }
            catch { }

            var packageServerRelativeUrl = UrlUtility.Combine(rootWeb.RootFolder.ServerRelativeUrl, fileName);

            // NOTE: The lines below (in OfficeDev PnP) wipe/clear all items in the composed looks aka design catalog (_catalogs/design, list template 124).
            // The solution package should be loaded into the solutions catalog (_catalogs/solutions, list template 121).

            DesignPackage.Install(site.Context, site, packageInfo, packageServerRelativeUrl);
            site.Context.ExecuteQueryRetry();

            // Remove package from rootfolder
            var uploadedSolutionFile = rootFolder.Files.GetByUrl(fileName);
            uploadedSolutionFile.DeleteObject();
            site.Context.ExecuteQueryRetry();
        }

        /// <summary>
        /// A helper method to load the sandbox soltuion to install (from somewhere)
        /// </summary>
        /// <param name="solution">Our solution definition</param>
        /// <returns>The sandbox solution as a stream</returns>
        private static Stream GetSolutionStream(STKSandboxSolution solution)
        {
            Stream stream = null;

            //TODO: Only solutions embedded in an assembly currently implemented
            switch (solution.LocationType)
            {
                case STKSolutionLocationType.Disk:
                    throw new NotImplementedException("TODO: Load sandboxed solutions from disk");

                case STKSolutionLocationType.Assembly:
                    {
                        Assembly assembly = Assembly.Load(solution.Location); // TODO Check this - move to file helper

                        foreach (String resource in assembly.GetManifestResourceNames())
                        {
                            Console.WriteLine(resource);
                        }

                        stream = assembly.GetManifestResourceStream(solution.Path);
                        break;
                    }

                case STKSolutionLocationType.Url:
                    throw new NotImplementedException("TODO: Load sandboxed solutions from a URL endpoint");

                default:
                    break;
            }

            return stream;
        }

        #endregion Install a sandboxed solution
    }
}