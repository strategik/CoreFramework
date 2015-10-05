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
using Microsoft.SharePoint.Client.Taxonomy;
using OfficeDevPnP.Core.Utilities;
using Strategik.Definitions.Taxonomy;
using System;
using System.Collections.Generic;

namespace Strategik.CoreFramework.Helpers
{
    /// <summary>
    /// Extension and helper methods for working with O365 and SP2013 taxonomy
    /// </summary>
    public class STKTaxonomyHelper : STKHelperBase
    {
        #region Constructors

        public STKTaxonomyHelper(ClientContext clientContext)
            : base(clientContext)
        { }

        #endregion Constructors

        #region Read Taxonomy Definitions Methods

        /// <summary>
        /// Reads the taxonomy definitions from our of the current term store
        /// </summary>
        /// <remarks>
        /// Reverse engineering termstore information into definitions is usefull for
        /// working out what has changed / what will change in the target environment
        /// before a provisioning operation is run.
        /// </remarks>
        /// <returns>The definition of the current termstore</returns>
        public STKTaxonomy ReadTaxonomy()
        {
            STKTaxonomy taxonomy = new STKTaxonomy();

            // Get the termstore
            TermStore spTermStore = _site.GetDefaultSiteCollectionTermStore();

            // Load all the groups, termsets & terms with the properties we are interested in
            _clientContext.Load(spTermStore,
                        store => store.Name,
                        store => store.Groups.Include(
                            group => group.Name,
                            group => group.Id,
                            group => group.Description,
                            group => group.CreatedDate,
                            group => group.LastModifiedDate,
                            group => group.TermSets.Include(
                                termSet => termSet.Name,
                                termset => termset.Id,
                                termset => termset.Contact,
                                termset => termset.Description,
                                termSet => termSet.Terms.Include(
                                    term => term.Name,
                                    term => term.Id,
                                    term => term.Terms)
                            )
                        )
                );

            _clientContext.ExecuteQueryRetry();

            // Extract the metadata
            taxonomy.Groups = ReadGroups(spTermStore.Groups);

            return taxonomy;
        }

        /// <summary>
        /// Reads the group definitions for the taxonomy groups passed
        /// </summary>
        /// <returns></returns>
        public List<STKTermGroup> ReadGroups(TermGroupCollection spGroups)
        {
            List<STKTermGroup> groups = new List<STKTermGroup>();

            foreach (TermGroup spTermGroup in spGroups)
            {
                STKTermGroup termGroup = ReadGroups(spTermGroup);
                groups.Add(termGroup);
            }

            return groups;
        }

        public STKTermGroup ReadGroups(TermGroup spTermGroup)
        {
            STKTermGroup termGroup = new STKTermGroup();
            termGroup.UniqueId = spTermGroup.Id;
            termGroup.Name = spTermGroup.Name;
            termGroup.Description = spTermGroup.Description;

            termGroup.TermSets = ReadTermSets(spTermGroup.TermSets);
            return termGroup;
        }

        public List<STKTermSet> ReadTermSets(TermSetCollection spTermSets)
        {
            List<STKTermSet> termSets = new List<STKTermSet>();

            foreach (TermSet spTermSet in spTermSets)
            {
                STKTermSet termset = ReadTermSet(spTermSet);
                termSets.Add(termset);
            }

            return termSets;
        }

        public STKTermSet ReadTermSet(TermSet spTermSet)
        {
            STKTermSet termset = new STKTermSet();
            termset.Contact = spTermSet.Contact;
            termset.Description = spTermSet.Description;
            termset.DisplayName = spTermSet.Name;
            termset.Name = spTermSet.Name;
            termset.UniqueId = spTermSet.Id;

            // Load all the terms at once for processing later
            TermCollection allTerms = spTermSet.GetAllTerms();
            _clientContext.Load(allTerms);
            _clientContext.ExecuteQueryRetry();
            termset.Terms.AddRange(ReadTerms(allTerms));

            //foreach (Term spTerm in spTermSet.Terms)
            //{
            //    STKTerm term = ReadTermDefinition(spTerm);
            //    termset.Terms.Add(term);
            //}

            return termset;
        }

        public List<STKTerm> ReadTerms(TermCollection spTerms)
        {
            // We have all the terms in the termset (including the depricated ones)
            // for efficiency. We need to process them back into their hierachy
            // and throw the depricated ones away
            // The terms might come in any order so we need to save up the child terms
            // and them match them to their parents at the end

            Dictionary<Guid, STKTerm> termsDictionary = new Dictionary<Guid, STKTerm>();
            List<STKTerm> terms = new List<STKTerm>();
            List<STKTerm> childTerms = new List<STKTerm>();

            foreach (Term spTerm in spTerms)
            {
                if(spTerm.IsDeprecated == false)
                {
                    STKTerm term = new STKTerm();
                    term.UniqueId = spTerm.Id;
                    term.Label = spTerm.Name;

                    termsDictionary.Add(spTerm.Id, term);

                    if (spTerm.IsRoot)
                    {
                        terms.Add(term);   
                    }
                    else
                    {
                        term.ParentId = spTerm.Id;
                        childTerms.Add(term);
                    }
                }
            }

            // Process the child terms to the right place 
            // now all are in place
            foreach (STKTerm term in childTerms)
            {
                // what could go wrong - lol
                STKTerm parentTerm = termsDictionary[term.ParentId];
                parentTerm.Terms.Add(term);
            }

            return terms;   
        }

        public STKTerm ReadTerm(Term spTerm)
        {
            STKTerm term = new STKTerm();

            term.Name = spTerm.Name;
            term.UniqueId = spTerm.Id;
            //   ClientResult<string> result = spTerm.GetDefaultLabel(1033);
            //  _clientContext.ExecuteQueryRetry();
            //  term.Label = result.Value;

            term.Label = term.Name; // need to have another look at this - probably shouldnt populate it

            // We cant assume our child terms are loaded here
            _clientContext.Load(spTerm.Terms);
            _clientContext.ExecuteQueryRetry();

            foreach (Term spChildTerm in spTerm.Terms)
            {
                STKTerm childTerm = ReadTerm(spChildTerm); // recursion
                term.Terms.Add(childTerm);
            }

            return term;
        }

        #endregion Read Taxonomy Definitions Methods

        #region Ensure Taxonomy Methods

        public void EnsureTaxonomy(STKTaxonomy termstore)
        {
            if (termstore == null) throw new ArgumentNullException("Termstore definition");
            termstore.Validate();
            EnsureTermGroups(termstore.Groups);
        }

        public void EnsureTermGroups(List<STKTermGroup> termGroups)
        {
            foreach (STKTermGroup termGroup in termGroups)
            {
                EnsureTermGroup(termGroup);
            }
        }

        public void EnsureTermGroup(STKTermGroup termGroup)
        {
          
            TermGroup spTermGroup = null;
            bool hasDifferentId = false;
            bool exists = _site.Exists(termGroup, out hasDifferentId, out spTermGroup);

          

            // Use the Microsoft P&P extensions to make sure the group exists
            if (spTermGroup == null) // do some damage - provision the new group
            {
                spTermGroup = _site.EnsureTermGroup(termGroup.Name, termGroup.UniqueId, termGroup.Description);
            }

            if (spTermGroup != null)
            {
                // Cascade the creation of any termsets defined in the group (if we have a group)
                EnsureTermSets(termGroup.TermSets, spTermGroup);
            }
            
        }

        public void EnsureTermSets(List<STKTermSet> termsets, TermGroup spTermGroup)
        {
            foreach (STKTermSet termset in termsets)
            {
                
                TermSet spTermSet = null;
                bool hasDifferentId = false;
                bool exists = _site.Exists(termset, out hasDifferentId, out spTermSet);

                

                
                spTermSet = spTermGroup.EnsureTermSet(termset.Name, termset.UniqueId, 1033, termset.Description, null, termset.Contact, termset.Owner);
                

                if (spTermSet != null)
                {
                    // todo = fix
                    EnsureTerms(termset, spTermSet);
                }    
            }
        }

        public void EnsureTerms(STKTermSet stkTermSet, TermSet spTermset)
        {
           

            foreach (STKTerm stkTerm in stkTermSet.Terms)
            {
                Log.Debug(STKConstants.LoggingSource, "Processing term " + stkTerm.Name);

                
                Term spTerm = spTermset.GetTerm(stkTerm.UniqueId);
                _clientContext.ExecuteQueryRetry();

                if (spTerm == null || spTerm.ServerObjectIsNull.Value == true) // This term doesnt exist so we need to recreate
                {
                    spTerm = _site.AddTermToTermset(spTermset.Id, stkTerm.Label, stkTerm.UniqueId);
                }

                foreach (STKTerm skChildTerm in stkTerm.Terms)
                {
                    EnsureChildTerm(skChildTerm, spTerm);
                    
                }

                
            }

            
        }

        private void EnsureChildTerm(STKTerm stkTerm, Term spParentTerm)
        {
            // Check if the child term already exists here
            Term spChildTerm = spParentTerm.TermSet.GetTerm(stkTerm.UniqueId);
            _clientContext.ExecuteQueryRetry();

            if (spChildTerm == null || spChildTerm.ServerObjectIsNull.Value == true)
            {
                spChildTerm = spParentTerm.AddTermToTerm(stkTerm.Lcid, stkTerm.Label, stkTerm.UniqueId, _clientContext);
            }

            foreach (STKTerm stkChildTerm in stkTerm.Terms)
            {
                EnsureChildTerm(stkChildTerm, spChildTerm); // recursion   
            }
        }

        //set term set to work for site navigation
        // termSet.SetCustomProperty("_Sys_NHi janetav_IsNavigationTermSet", "True");

        #endregion Ensure Taxonomy Methods

        #region Test & Debug Methods

        public static void DumpTaxonomyToConsole(ClientContext currentContext)
        {
            Console.WriteLine("");
            Console.WriteLine("=== Dumping current taxonomy ===");
            Console.WriteLine("");

            TaxonomySession taxonomySession = TaxonomySession.GetTaxonomySession(currentContext);

            // Load all the termstores
            currentContext.Load(taxonomySession, ts => ts.TermStores.Include(
                store => store.Name,
                store => store.Groups.Include(
                    group => group.Name
                    )
                )
             );

            currentContext.ExecuteQuery();

            if (taxonomySession != null)
            {
                TermStoreCollection allTermStores = taxonomySession.TermStores;
                foreach (TermStore ts in allTermStores)
                {
                    Console.WriteLine("Current context contains termstore {0}" + ts.Name);

                    foreach (TermGroup tg in ts.Groups)
                    {
                        Console.WriteLine("");
                        Console.WriteLine("Found Term Group {0}", tg.Name);
                    }
                }

                TermStore defaultTermStore = taxonomySession.GetDefaultSiteCollectionTermStore();
                currentContext.Load(defaultTermStore,
                    dts => dts.Name,
                    dts => dts.IsOnline
                    );
                currentContext.ExecuteQuery();

                Console.WriteLine("");
                Console.WriteLine("The default term store is " + defaultTermStore.Name + " is online = " + defaultTermStore.IsOnline);
            }
        }

        #endregion Test & Debug Methods
    }
}