using Strategik.Definitions.Fields;
using Strategik.Definitions.TestModel.Taxonomy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategik.Definitions.TestModel.SiteColumns
{
    public static class STKTestSiteColumns
    {
        #region Constants

        public static Guid boolFieldId = new Guid("{16C954BF-9424-47BE-B4BC-54CC787A4940}");
        public static Guid choiceFieldId = new Guid("{8419FC7B-B785-4418-9F45-F4CD0A2C4F06}");
        public static Guid dateFieldId = new Guid("{370CD753-1D4F-49F6-8097-C1FA0AF1F221}");
        public static Guid guidFieldId = new Guid("{E710C771-8FA9-4407-9F0A-EB51E1004968}");
        public static Guid linkFieldId = new Guid("{9AF2C635-51C6-4BA7-AA0B-0118DFE71230}");
        public static Guid numberFieldId = new Guid("{FD0A3BF8-34A8-4180-AEC1-0E2CA75BD8C3}");
        public static Guid textFieldId = new Guid("{311C58D1-45EC-4093-9103-BA3884C92919}");
        public static Guid richTextFieldId = new Guid("{437C5DCE-8F28-4D63-B787-BE74F0CC9F16}");
        public static Guid userFieldId = new Guid("{B240F2DC-BF5E-454A-8B33-BCBF00ACBDAF}");
        public static Guid taxonomyFieldId = new Guid("{747F2C20-D97B-484A-9C41-C211ECF629E5}");

        #endregion

        #region Definitions

        public static STKField BoolSiteColumn()
        {
            STKBooleanField boolSiteColumn = new STKBooleanField()
            {
                IsSiteColumn = true,
                UniqueId = boolFieldId,
                GroupName = "Test_SiteColumns"
            };

            boolSiteColumn.SetAllNameFields("Test_BoolSiteColumn");
            return boolSiteColumn;
        }

        public static STKField ChoiceSiteColumn()
        {
            STKChoiceField choiceSiteColumn = new STKChoiceField()
            {
                IsSiteColumn = true,
                UniqueId = choiceFieldId,
                GroupName = "Test_SiteColumns"
            };

            choiceSiteColumn.SetAllNameFields("Test_ChoiceSiteColumn");
            choiceSiteColumn.Choices.Add("Tom");
            choiceSiteColumn.Choices.Add("Dick");
            choiceSiteColumn.Choices.Add("Harry");

            return choiceSiteColumn;
        }

        public static STKField DateSiteColumn()
        {
            STKDateField dateSiteColumn = new STKDateField()
            {
                IsSiteColumn = true,
                UniqueId = dateFieldId,
                GroupName = "Test_SiteColumns"
            };

            dateSiteColumn.SetAllNameFields("Test_DateSiteColumn");
            dateSiteColumn.IsDateOnly = true;

            return dateSiteColumn;
        }

        public static STKField GuidSiteColumn()
        {
            STKGuidField guidSiteColumn = new STKGuidField()
            {
                IsSiteColumn = true,
                UniqueId = guidFieldId,
                GroupName = "Test_SiteColumns"
            };

            guidSiteColumn.SetAllNameFields("Test_GuidSiteColumn");
            return guidSiteColumn;
        }

        public static STKField LinkSiteColumn()
        {
            STKUrlField linkSiteColumn = new STKUrlField()
            {
                IsSiteColumn = true,
                UniqueId = linkFieldId,
                GroupName = "Test_SiteColumns"
            };

            linkSiteColumn.SetAllNameFields("Test_LinkSiteColumn");
            return linkSiteColumn;
        }

        public static STKField NumberSiteColumn()
        {
            STKNumberField numberSiteColumn = new STKNumberField()
            {
                IsSiteColumn = true,
                UniqueId = numberFieldId,
                GroupName = "Test_SiteColumns"
            };

            numberSiteColumn.SetAllNameFields("Test_NumberSiteColumn");
            numberSiteColumn.DecimalPlaces = 2;
            numberSiteColumn.DefaultValue = "1.00";
            numberSiteColumn.Min = 0;
            numberSiteColumn.Max = 100;

            return numberSiteColumn;
        }

        public static STKField TextSiteColumn()
        {
            STKTextField textSiteColumn = new STKTextField()
            {
                IsSiteColumn = true,
                UniqueId = textFieldId,
                GroupName = "Test_SiteColumns"
            };

            textSiteColumn.SetAllNameFields("Test_TextSiteColumn");
            return textSiteColumn;
        }

        public static STKField RichTextSiteColumn()
        {
            STKRichTextField richTextSiteColumn = new STKRichTextField()
            {
                IsSiteColumn = true,
                UniqueId = richTextFieldId,
                GroupName = "Test_SiteColumns"
            };

            richTextSiteColumn.SetAllNameFields("Test_RichTextSiteColumn");
            richTextSiteColumn.Mode = RichTextMode.FullHtml;
            return richTextSiteColumn;
        }

        public static STKField UserSiteColumn()
        {
            STKUserField userSiteColumn = new STKUserField()
            {
                IsSiteColumn = true,
                UniqueId = userFieldId,
                GroupName = "Test_SiteColumns"
            };

            userSiteColumn.SetAllNameFields("Test_UserSiteColumn");
            return userSiteColumn;
        }

        public static STKField TaxonomySiteColumn()
        {
            Guid termsetId = STKTestTaxonomy.TestTermSet2_Id;
            Guid termGroupId = STKTestTaxonomy.TestTermGroup1_Id;
            STKTaxonomyField taxonomySiteColumn = new STKTaxonomyField()
            {
                IsSiteColumn = true,
                UniqueId = taxonomyFieldId,
                TermsetId = termsetId,
                TermGroupId = termGroupId,
                GroupName = "Test_SiteColumns",
            };

            taxonomySiteColumn.SetAllNameFields("Test_TaxonomySiteColumn");
            return taxonomySiteColumn;
        }

        public static List<STKField> AllSiteColumns()
        {
            List<STKField> siteColumns = new List<STKField>();

            siteColumns.Add(BoolSiteColumn());
            siteColumns.Add(ChoiceSiteColumn());
            siteColumns.Add(DateSiteColumn());
            siteColumns.Add(GuidSiteColumn());
            siteColumns.Add(LinkSiteColumn());
            siteColumns.Add(NumberSiteColumn());
            siteColumns.Add(TextSiteColumn());
            siteColumns.Add(RichTextSiteColumn());
            siteColumns.Add(UserSiteColumn());
            siteColumns.Add(TaxonomySiteColumn());

            return siteColumns;
        }

        #endregion
    }
}
