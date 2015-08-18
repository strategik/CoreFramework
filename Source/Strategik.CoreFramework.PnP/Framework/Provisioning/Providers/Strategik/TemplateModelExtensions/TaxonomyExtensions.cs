
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
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Strategik.Definitions.Taxonomy;

namespace OfficeDevPnP.Core.Framework.Provisioning.Model
{
    public static partial class TaxonomyExtensions
    {
         #region Generate strategik definition from corresponding PnP template object

        public static STKTermGroup GenerateStrategikDefinition(this TermGroup termGroup)
        {
            STKTermGroup stkTermGroup = new STKTermGroup()
            {
                UniqueId = termGroup.Id,
                DisplayName = termGroup.Name,
                Description = termGroup.Description,
                Title = termGroup.Name,
            };

            foreach (TermSet termSet in termGroup.TermSets)
            {
                stkTermGroup.TermSets.Add(termSet.GenerateStrategikDefinition());
            }

            return stkTermGroup;
        }

        public static STKTermSet GenerateStrategikDefinition(this TermSet termSet)
        {
            STKTermSet stkTermSet = new STKTermSet()
            {
                Title = termSet.Name,
                Contact = termSet.Owner,
                Description = termSet.Description,
                DisplayName = termSet.Name,
                UniqueId = termSet.Id,
                IsAvailableForTagging = termSet.IsAvailableForTagging,
                IsOpen = termSet.IsOpenForTermCreation,
                Properties = termSet.Properties,
            };

            if (termSet.Language != null)
            {
                stkTermSet.Lcid = (int) termSet.Language;
            }

            foreach (Term term in termSet.Terms)
            {
                stkTermSet.Terms.Add(term.GenerateStrategikDefinition());
            }

            return stkTermSet;
        }

        public static STKTerm GenerateStrategikDefinition(this Term term)
        {
            STKTerm stkTerm = new STKTerm()
            {
                UniqueId = term.Id,
                Name = term.Name,
                CustomSortOrder = term.CustomSortOrder,
                Description = term.Description,
                IsAvailableForTagging = term.IsAvailableForTagging,
                Title = term.Name,
            };

            foreach(Term childTerm in term.Terms)
            {
                stkTerm.Terms.Add(childTerm.GenerateStrategikDefinition());
            }

            return stkTerm;
        }

        #endregion
    }
}
