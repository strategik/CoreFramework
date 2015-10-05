
using System;

namespace Strategik.Definitions.Taxonomy
{
    public static partial class STKTaxonomyExtensions
    {
        #region Validation

        //TODO: Complete taxonomy model validation
        public static bool IsValid(this STKTaxonomy taxonomy)
        {
            try
            {
                taxonomy.Validate();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void Validate(this STKTaxonomy taxonomy)
        {
            if (taxonomy == null) throw new ArgumentNullException("taxonomy");
            if (String.IsNullOrEmpty(taxonomy.Name)) throw new Exception("No value specified for taxonomy name");
            // TODO: Title

            foreach (STKTermGroup group in taxonomy.Groups)
            {
                group.Validate();
            }
        }

        public static bool IsValid(this STKTermGroup group)
        {
            try
            {
                group.Validate();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void Validate(this STKTermGroup group)
        {
            if (group == null) throw new ArgumentNullException("group");
            if (String.IsNullOrEmpty(group.Name)) throw new Exception("group name must be specified"); //

        }

        public static bool IsValid(this STKTermSet termset)
        {
            try
            {
                termset.Validate();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void Validate(this STKTermSet termset)
        {
            //
        }

        public static bool IsValid(this STKTerm term)
        {
            try
            {
                term.Validate();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void Validate(this STKTerm term)
        {
            //
        }

        #endregion 
    }
}