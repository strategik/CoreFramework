
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
//
// Author:  Dr Adrian Colquhoun
//
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategik.Definitions.Sites
{
    public static partial class STKSiteExtensions
    {
        #region Validation

        public static bool IsValid(this STKSite site)
        {
            try
            {
                site.Validate();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void Validate(this STKSite site)
        {
            if (site == null) throw new ArgumentNullException("site");
          //  if (site.UniqueId == Guid.Empty) throw new Exception("Site Unique Id is empty");
            if (String.IsNullOrEmpty(site.Name)) throw new Exception("Site Name is empty");
            //   if (site.RootWeb == null) throw new Exception("Root web is null");

            // We require a site template, owner and tennant relative URL as a minimum
            //site.Template
            if (site.RootWeb != null)
            {
                site.RootWeb.Validate();
            }
        }

        #endregion 
    }
}
