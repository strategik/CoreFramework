using Microsoft.SharePoint.Client.Taxonomy;
using Strategik.Definitions.O365.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.SharePoint.Client
{
    public static partial class STKWebExtensions
    {
        #region Taxonomy

        public static void WireUpTaxonomyField(this Web web, Microsoft.SharePoint.Client.Field field, STKTaxonomyField taxonomyField)
        {
            web.WireUpTaxonomyField(field, taxonomyField.TermsetId, taxonomyField.TermGroupId, taxonomyField.AllowMultiSelect);
        }
        public static void WireUpTaxonomyField(this Web web, Microsoft.SharePoint.Client.Field field, Guid mmsTermSetId, Guid mmsTermGroupId, bool multiValue = false)
        {
            TermStore termStore = GetDefaultTermStore(web);

            if (termStore == null)
                throw new NullReferenceException("The default term store is not available.");

            if (mmsTermSetId == null)
                throw new ArgumentNullException("mmsTermSetId", "The MMS term set id is not specified.");

            if (mmsTermGroupId == null)
                throw new ArgumentNullException("mmsTermGroupId", "The MMS term group id is not specified.");

            // get the term group and term set
            Microsoft.SharePoint.Client.Taxonomy.TermGroup termGroup = termStore.Groups.GetById(mmsTermGroupId);
            Microsoft.SharePoint.Client.Taxonomy.TermSet termSet = termGroup.TermSets.GetById(mmsTermSetId);
            web.Context.Load(termStore);
            web.Context.Load(termSet);
            web.Context.ExecuteQueryRetry();

            web.WireUpTaxonomyField(field, termSet, multiValue);
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

        #endregion
    }
}
