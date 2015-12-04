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


using Strategik.Definitions.Base;
using Strategik.Definitions.Sites;
using Strategik.Definitions.Taxonomy;
using Strategik.Definitions.Tenant;
using System;
using System.Collections.Generic;

namespace Strategik.Definitions.Solutions
{
    /// <summary>
    /// Defines a Strategik solution
    /// </summary>
    /// <remarks>
    /// Solutions are deployed to Office 365 using the Core Framework.
    /// 
    /// Solutions may have other solutions (aka dependencies) embedded within them. 
    /// Embedded solutions are always applied (in order) first.
    /// </remarks>
    public class STKSolution: STKDefinitionBase
    {
        #region Constants

        public const String KEY = "StrategikSolution";

        #endregion Constants

        #region Properties

        /// <summary>
        /// Any Configufiguration to applied while deploying this solution
        /// </summary>
        public STKSolutionConfiguration O365Configuration { get; set; }

        /// <summary>
        /// Tenant wide customisations included in this solution (Office 365)
        /// </summary>
        public STKTenantCustomisations TennantCustomisations { get; set; }

        /// <summary>
        /// The taxonomies to be deployed by this solution
        /// </summary>
        public List<STKTaxonomy> Taxonomy { get; set; }

        /// <summary>
        /// The Sites and subsites to be deployed in this solution
        /// </summary>
        public List<STKSite> Sites { get; set; }

        #endregion Properties

        #region Constructor

        public STKSolution()
        {
            Taxonomy = new List<STKTaxonomy>();
            Sites = new List<STKSite>();
        }


        #endregion Constructor
    }
}