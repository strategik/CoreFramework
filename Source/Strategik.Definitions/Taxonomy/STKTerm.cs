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
using System;
using System.Collections.Generic;

namespace Strategik.Definitions.Taxonomy
{
    /// <summary>
    /// The definition of a term
    /// </summary>
    public class STKTerm : STKDefinitionBase
    {
        #region Properties

        public int CustomSortOrder { get; set; }

        public bool IsAvailableForTagging { get; set; }

        public String Label { get; set; }

        public int Lcid { get; set; } //??

        public Dictionary<string, string> Properties { get; set; }

        public List<STKTerm> Terms { get; set; }

        public Guid ParentId { get; set; }

        #endregion Properties

        #region Constructors

        public STKTerm()
        {
            Terms = new List<STKTerm>();
            Properties = new Dictionary<string, string>();
            Lcid = 1033; // probably should fix this
        }

        #endregion Constructors
    }
}