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

using Strategik.Definitions.Features;
using System;

namespace Strategik.Definitions.Solutions
{
    /// <summary>
    /// Defines a solution asset to be deployed with the solution
    /// </summary>
    /// <remarks>
    /// Solution assets are javascript files, css files, fonts, masterpages and so on.
    /// 
    /// Use this class to specify assets to be deplyed solution wide or to a particular web.
    /// 
    /// Assets can sourced from disk, embedded into an assembly or form a URL
    /// </remarks>
    public class STKSolutionAsset
    {
        #region Properties

        /// <summary>
        /// Set to true to deploy all the assets found at the specified location
        /// </summary>
        public bool IsFolder { get; set; }

        /// <summary>
        /// Set to true to recursively deploy all assets detected
        /// </summary>
        public bool Recursive { get; set; }

        /// <summary>
        /// Specify the filename (for a single asset)
        /// </summary>
        public String FileName { get; set; }

        /// <summary>
        /// The location to source the assets
        /// </summary>
        public String Location { get; set; }

        /// <summary>
        /// The location type
        /// </summary>
        /// <remarks>
        /// File system, URL, embedded in an assembly etc
        /// </remarks>
        public STKSolutionLocationType LocationType { get; set; }

        // TODO: Check code !!!
        public string Path { get; set; }

        #endregion Properties
    }
}