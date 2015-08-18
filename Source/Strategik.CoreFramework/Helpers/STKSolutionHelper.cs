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
using Strategik.Definitions.Sites;
using Strategik.Definitions.Features;
using System;
using System.Diagnostics;
using Strategik.Definitions.Solutions;

namespace Strategik.CoreFramework.Helpers
{
    /// <summary>
    /// Helper to manage Strategik Solutions.
    /// </summary>
    /// <remarks>
    ///  Use this helper to install, unisntall and upgrade solutions in Office 365 Tenants of SharePoint 2013.
    /// </remarks>
    public class STKSolutionHelper 
    {
        #region Data

        private STKSolution _solution;

        #endregion Data

        #region Constructor

        public STKSolutionHelper(STKSolution solution)
        {
            Debug.WriteLine(STKConstants.LoggingSource, "Creating Solution helper");

             solution.Validate();
            _solution = solution;

            Debug.WriteLine(STKConstants.LoggingSource, "Solution helper created");
        }

        #endregion Constructor

        #region Methods

        public virtual void InstallSolution(String adminUrl, String sharePointURL, String userName, String password)
        {
            STKTenantHelper helper = new STKTenantHelper(adminUrl, sharePointURL, userName, password);
            helper.Provision(_solution);
        }

        public virtual void UninstallSolution()
        {
            //TODO:
        }

        #endregion Methods
    }
}