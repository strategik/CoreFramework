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

// Author: Dr Adrian Colquhoun

#endregion License

using Frenetik.Model.Phase1.Sites;
using Frenetik.Model.Phase1.Taxonomy;
using Strategik.Definitions.Solutions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frenetik.Model.Phase1.Solution
{
    /// <summary>
    /// Defines the Frenetik Information Management Solution
    /// </summary>
    public class FRIMSolution: STKSolution
    {
        public FRIMSolution()
        {
            base.UniqueId = new Guid("{5A8D8CAA-A138-4E14-84A4-1993866ADBD4}");
            base.Title = "Frenetik Information Management Solution";
            base.Name = "FRIM";
            base.DisplayName = "Frenetik Information Management Solution";
            base.MajorVersion = 1;
            base.MinorVersion = 0;
            base.Taxonomy.Add(FRIMTaxonomy.GetTaxonomy());
            base.Sites.AddRange(FRIMSites.GetSites());
        }
    }
}
