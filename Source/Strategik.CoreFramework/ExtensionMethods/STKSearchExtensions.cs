﻿#region License

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

using Microsoft.SharePoint.Client.Search.Administration;
using Microsoft.SharePoint.Client.Search.Portability;
using System;

namespace Microsoft.SharePoint.Client
{
    public static partial class STKSearchExtensions
    {
        /// <summary>
        /// Export the search settings from the current context
        /// </summary>
        /// <param name="context"></param>
        /// <param name="searchSettingsExportLevel"></param>
        /// <returns></returns>
        public static String ExportSearchSettingsXML(this ClientContext context, SearchObjectLevel searchSettingsExportLevel)
        {
            String searchSettingsXML = String.Empty;

            SearchConfigurationPortability sconfig = new SearchConfigurationPortability(context);
            SearchObjectOwner owner = new SearchObjectOwner(context, searchSettingsExportLevel);

            ClientResult<string> configresults = sconfig.ExportSearchConfiguration(owner);
            context.ExecuteQuery();

            if (configresults.Value != null)
            {
                searchSettingsXML = configresults.Value;
            }
            else
            {
                throw new Exception("No search settings to export.");
            }

            return searchSettingsXML;
        }
    }
}