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
using OfficeDevPnP.Core.Diagnostics;
using Strategik.Definitions.ContentTypes;
using Strategik.Definitions.Fields;
using Strategik.Definitions.Sites;
using Strategik.Definitions.Features;
using System;
using System.Collections.Generic;

namespace Strategik.CoreFramework.Helpers
{
    /// <summary>
    /// Base class to factor out common helper code
    /// </summary>
    /// <remarks>
    /// Helpers are provisioned with a client context. From that context the target 
    /// Web and Site ojects are extracted. Functionality in helper methods is executed against that
    /// context.
    /// </remarks>
    public class STKHelperBase
    {
        #region Data

        protected ClientContext _clientContext;
        protected Web _web;
        protected Site _site;

        #endregion Data

        #region Constructor

        public STKHelperBase(ClientContext clientContext)
        {
            if (clientContext == null) throw new ArgumentNullException("Client Context");

            _clientContext = clientContext;
            _clientContext.Load(_clientContext.Web);
            _clientContext.Load(_clientContext.Site);

            _clientContext.ExecuteQueryRetry();

            _web = _clientContext.Web;
            _site = _clientContext.Site;

            Log.Info(STKConstants.LoggingSource, "Initialised helper {0}. Target web is {1} located at {2}", this.GetType().Name, _web.Title, _web.Url);
        }

        #endregion Constructor
    }
}