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

#endregion License

using OfficeDevPnP.Core.Framework.Provisioning.Model;
using System.Collections.Generic;

namespace Strategik.Definitions.Taxonomy
{
    /// <summary>
    /// Extension methods to convert Strategik definitions into their corresponding PnP templates.
    /// </summary>
    public static partial class STKTaxonomyExtensions
    {
        #region Generate PnP templates from corresponding Stategik object definitions

        public static List<TermGroup> GeneratePnPTemplates(this STKTaxonomy taxonomy)
        {
            List<TermGroup> termGroupTemplates = new List<TermGroup>();

            foreach (STKTermGroup termGroup in taxonomy.Groups)
            {
                termGroupTemplates.Add(termGroup.GeneratePnPTemplate());
            }

            return termGroupTemplates;
        }

        public static TermGroup GeneratePnPTemplate(this STKTermGroup group)
        {
            TermGroup termGroupTemplate = new TermGroup()
            {
                Id = group.UniqueId,
                Name = group.Name
            };

            foreach (STKTermSet termSet in group.TermSets)
            {
                termGroupTemplate.TermSets.Add(termSet.GeneratePnPTemplate());
            }

            return termGroupTemplate;
        }

        public static TermSet GeneratePnPTemplate(this STKTermSet termSet)
        {
            TermSet termSetTemplate = new TermSet()
            {
                Id = termSet.UniqueId,
                Name = termSet.Name,
                IsAvailableForTagging = termSet.IsAvailableForTagging,
                IsOpenForTermCreation = termSet.IsOpen,
                Language = termSet.Lcid,
                Description = termSet.Description,
                Owner = termSet.Owner,
            };

            foreach (string key in termSet.Properties.Keys)
            {
                termSetTemplate.Properties.Add(key, termSet.Properties[key]);
            }

            foreach (STKTerm term in termSet.Terms)
            {
                termSetTemplate.Terms.Add(term.GeneratePnPTemplate());
            }

            return termSetTemplate;
        }

        public static Term GeneratePnPTemplate(this STKTerm term)
        {
            Term termTemplate = new Term()
            {
                Id = term.UniqueId,
                Name = term.Name,
                Description = term.Description,
                CustomSortOrder = term.CustomSortOrder,
                IsAvailableForTagging = term.IsAvailableForTagging,
                Language = term.Lcid,
            };

            termTemplate.Labels.Add(new TermLabel() { IsDefaultForLanguage = true, Value = term.Label, Language = term.Lcid }); //TODO - fix this

            foreach (string key in term.Properties.Keys)
            {
                termTemplate.Properties.Add(key, term.Properties[key]);
            }

            foreach (STKTerm childTerm in term.Terms)
            {
                termTemplate.Terms.Add(childTerm.GeneratePnPTemplate());
            }

            return termTemplate;
        }

        #endregion Generate PnP templates from corresponding Stategik object definitions

    }
}