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

namespace Strategik.Definitions.Solutions
{
    /// <summary>
    /// Used to set configuration properties to be applied when a solution is provisioned
    /// </summary>
    public class STKSolutionConfiguration
    {
        #region Properties

        /// <summary>
        /// Automatically provision a "dev" suffix
        /// </summary>
        public bool IsDev { get; set; }

        /// <summary>
        /// Automatically provision a "UAT" suffix
        /// </summary>
        public bool IsUAT { get; set; }

        /// <summary>
        /// Custom suffix for provisioned items
        /// </summary>
        public String CustomSuffix { get; set; }

        public String TenantUrl { get; set; }

        public String TenantAdminUrl { get; set; }

        public String ProvisioningUserName { get; set; }

        public String ProvisioningPassword { get; set; }

        #endregion
    }
}