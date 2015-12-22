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
using Microsoft.SharePoint.Client.Search.Administration;
using Microsoft.SharePoint.Client.Search.Query;
using Strategik.Definitions.Search;
using Strategik.CoreFramework.Configuration;
using System;

namespace Strategik.CoreFramework.Helpers
{
    /// <summary>
    /// Extension and Helper methods to work with O365 Search features
    /// </summary>
    public class STKSearchHelper : STKHelperBase
    {
        #region Constructor

        public STKSearchHelper(ClientContext clientContext)
            : base(clientContext)
        { }

        public STKSearchHelper(STKAuthenticationHelper authHelper)
            : base(authHelper)
        { }

        public STKSearchHelper()
            : base (new STKAuthenticationHelper())
        { }

        #endregion

        #region Import and Export Methods

        public STKSearchConfig ExportTenantSearchConfig()
        {
            STKSearchConfig searchConfiguration = new STKSearchConfig();
            searchConfiguration.XML = _clientContext.ExportSearchSettingsXML(SearchObjectLevel.SPSiteSubscription);
            return searchConfiguration;
        }

        public STKSearchConfig ExportSiteSearchConfig()
        {
            STKSearchConfig searchConfiguration = new STKSearchConfig();
            searchConfiguration.XML = _clientContext.ExportSearchSettingsXML(SearchObjectLevel.SPSite);
            return searchConfiguration;
        }

        #endregion Import and Export Methods

        #region Debug methods

        public static void PingSearch(STConfiguration sTConfiguration, ClientContext devContext)
        {
            Console.WriteLine("");
            Console.WriteLine("Executing a keyword search for the term 'SharePoint'");

            KeywordQuery keywordQuery = new KeywordQuery(devContext);
            keywordQuery.QueryText = "filetype:pptx";
            // keywordQuery.RowLimit = 1; // just get the first search result - add this line to see the counts change

            SearchExecutor searchExecutor = new SearchExecutor(devContext);

            ClientResult<ResultTableCollection> results = searchExecutor.ExecuteQuery(keywordQuery);
            devContext.ExecuteQuery();

            Console.WriteLine("");
            Console.WriteLine("The keyword search returned " + results.Value[0].RowCount + " results");

            ResultTable resultTable = results.Value[0];

            Console.WriteLine("");
            Console.WriteLine("There are " + resultTable.TotalRows + " results in the search index that match the query submitted");
            Console.WriteLine("There are " + resultTable.TotalRowsIncludingDuplicates + " Results including duplicates");
        }

        #endregion Debug methods
    }
}