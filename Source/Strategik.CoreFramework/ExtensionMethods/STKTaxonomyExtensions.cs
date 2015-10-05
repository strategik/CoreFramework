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

using Microsoft.SharePoint.Client.Taxonomy;
using Strategik.Definitions.Fields;
using Strategik.Definitions.Taxonomy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Microsoft.SharePoint.Client
{
    public static partial class TaxonomyExtensions
    {
        #region Contants from P&P

        private static readonly Regex InvalidNameRegex = new Regex("[;\"<>|&\\t]", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        #endregion Contants from P&P

        #region Extension methods

        // (Things that are not currently in P&P)

        public static Term AddTermToTerm(this Term term, int lcid, string termLabel, Guid termId, ClientContext _context)
        {
            var clientContext = _context;

            if (term.ServerObjectIsNull == true)
            {
                try
                {
                    clientContext.Load(term);
                    clientContext.ExecuteQueryRetry();
                }
                catch { }
            }
            Term subTerm = null;
            if (termId != Guid.Empty)
            {
                subTerm = term.Terms.GetById(termId);
            }
            else
            {
                subTerm = term.Terms.GetByName(NormalizeName(termLabel));
            }
            clientContext.Load(term);
            try
            {
                clientContext.ExecuteQueryRetry();
            }
            catch { }

            clientContext.Load(subTerm);
            try
            {
                clientContext.ExecuteQueryRetry();
            }
            catch { }
            if (subTerm.ServerObjectIsNull == null)
            {
                if (termId == Guid.Empty) termId = Guid.NewGuid();
                subTerm = term.CreateTerm(NormalizeName(termLabel), lcid, termId);
                clientContext.Load(subTerm);
                clientContext.ExecuteQueryRetry();
            }
            return subTerm;
        }

        public static void WireUpTaxonomyField(this Web web, Field field, STKTaxonomyField stkSiteColumn)
        {
            try
            {
                WireUpTaxonomyField(web, field, stkSiteColumn.TermsetId, stkSiteColumn.TermGroupId, stkSiteColumn.AllowMultiSelect);
            }
            catch { }
        }

        public static void CleanupTaxonomyHiddenField(this Web web, STKTaxonomyField stkSiteColumn)
        {
            // if the Guid is empty then we'll have no issue
            try
            {
                FieldCollection _fields = web.Fields;
                web.Context.Load(_fields, fc => fc.Include(f => f.Id, f => f.InternalName, f => f.Hidden));
                web.Context.ExecuteQueryRetry();
                var _field = _fields.FirstOrDefault(f => f.InternalName.Equals(stkSiteColumn.UniqueId));
                // if the field does not exist we assume the possiblity that it was created earlier then deleted and the hidden field was left behind
                // if the field does exist then return and let the calling process exception out when attempting to create it
                // this does not appear to be an issue with lists, just site columns, but it doesnt hurt to check
                // if (_field == null)
                // {
                // The hidden field format is the id of the field itself with hyphens removed and the first character replaced
                // with a random character, so get everything to the right of the first character and remove hyphens
                var _hiddenField = stkSiteColumn.UniqueId.ToString().Replace("-", "").Substring(1);
                _field = _fields.FirstOrDefault(f => f.InternalName.EndsWith(_hiddenField));
                if (_field != null)
                {
                    if (_field.Hidden)
                    {
                        // just in case the field itself is hidden, make sure it is not because depending on the current CU hidden fields may not be deletable
                        _field.Hidden = false;
                        _field.Update();
                    }
                    _field.DeleteObject();
                    web.Context.ExecuteQueryRetry();
                }
                // }
            }
            catch { }
        }

        public static void WireUpTaxonomyField(this Web web, Field field, Guid mmsTermSetId, Guid mmsTermGroupId, bool multiValue = false)
        {
            TermStore termStore = GetDefaultTermStore(web);

            if (termStore == null)
                throw new NullReferenceException("The default term store is not available.");

            if (mmsTermSetId == null)
                throw new ArgumentNullException("mmsTermSetId", "The MMS term set id is not specified.");

            if (mmsTermGroupId == null)
                throw new ArgumentNullException("mmsTermGroupId", "The MMS term group id is not specified.");

            // get the term group and term set
            TermGroup termGroup = termStore.Groups.GetById(mmsTermGroupId);
            TermSet termSet = termGroup.TermSets.GetById(mmsTermSetId);
            web.Context.Load(termStore);
            web.Context.Load(termSet);
            web.Context.ExecuteQueryRetry();

            web.WireUpTaxonomyField(field, termSet, multiValue);
        }

        private static TermStore GetDefaultTermStore(Web web)
        {
            TermStore termStore = null;
            TaxonomySession taxonomySession = TaxonomySession.GetTaxonomySession(web.Context);
            web.Context.Load(taxonomySession,
                ts => ts.TermStores.Include(
                    store => store.Name,
                    store => store.Groups.Include(
                        group => group.Name
                        )
                    )
                );
            web.Context.ExecuteQueryRetry();
            if (taxonomySession != null)
            {
                termStore = taxonomySession.GetDefaultSiteCollectionTermStore();
            }

            return termStore;
        }

        #region Exists

        public static bool Exists(this Site site, STKTermGroup stkTermGroup, out bool hasDifferentId, out TermGroup termGroup)
        {
            bool exists = false; // will be set to true the the group exists (id or name match)
            hasDifferentId = false; // will be set to true if the ids are different

            // based on the code in the ensure term group extension in the P&P
            TaxonomySession session = TaxonomySession.GetTaxonomySession(site.Context);
            var termStore = session.GetDefaultSiteCollectionTermStore();
            site.Context.Load(termStore, s => s.Name, s => s.Id);

            termGroup = null;
            String groupName = NormalizeName(stkTermGroup.Name);
            ValidateName(groupName, "groupName");

            // Find Group by id or Name
            IEnumerable<TermGroup> groups = site.Context.LoadQuery(termStore.Groups.Include(g => g.Name, g => g.Id, g => g.Description));
            site.Context.ExecuteQueryRetry();
            if (stkTermGroup.UniqueId != Guid.Empty)
            {
                termGroup = groups.FirstOrDefault(g => g.Id == stkTermGroup.UniqueId);
            }

            if (termGroup == null) // not found by id
            {
                termGroup = groups.FirstOrDefault(g => string.Equals(g.Name, groupName, StringComparison.OrdinalIgnoreCase));

                if (termGroup == null) // Not found
                {
                    // nothing to do here
                }
                else
                {
                    exists = true; // Found by name - has a different id
                    hasDifferentId = true;
                }
            }
            else
            {
                exists = true; // Found by id - done
            }

            return exists;
        }

        public static bool Exists(this Site site, STKTermSet stkTermSet, out bool hasDifferentId, out TermSet termSet)
        {
            bool exists = false; // will be set to true the the group exists (id or name match)
            hasDifferentId = false; // will be set to true if the ids are different

            // based on the code in the ensure term group extension in the P&P
            TaxonomySession session = TaxonomySession.GetTaxonomySession(site.Context);
            var termStore = session.GetDefaultSiteCollectionTermStore();
            site.Context.Load(termStore, s => s.Name, s => s.Id);

            termSet = null;
            String termSetName = NormalizeName(stkTermSet.Name);
            ValidateName(termSetName, "termSetName");

            // Find Group by id or Name
            termSet = termStore.GetTermSet(stkTermSet.UniqueId);
            site.Context.ExecuteQueryRetry();

            if (termSet != null)
            {
                exists = true;
            }
            else
            {
                TermSetCollection termSets = termStore.GetTermSetsByName(termSetName, 1033);
                site.Context.ExecuteQueryRetry();

                if (termSets.Count > 0)
                {
                    exists = true;
                    hasDifferentId = true;
                }
            }

            return exists;
        }

        #endregion Exists

        #region Methods copied from the P&P extensions

        private static void ValidateName(string name, string parameterName)
        {
            if (string.IsNullOrEmpty(name)) { throw new ArgumentNullException(parameterName); }

            if (name.Length > 255 || InvalidNameRegex.IsMatch(name))
            {
                throw new ArgumentException(string.Format("Invalid taxonomy name '{0}'.", new object[]
				{
					name
				}), parameterName);
            }
        }

        private static readonly Regex TrimSpacesRegex = new Regex("\\s+", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private static string NormalizeName(string name)
        {
            if (name == null)
                return (string)null;
            else
                return TrimSpacesRegex.Replace(name, " ").Replace('&', '＆').Replace('"', '＂');
        }

        #endregion Methods copied from the P&P extensions

        #endregion Extension methods
    }
}