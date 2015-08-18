using Strategik.Definitions.ContentTypes;
using Strategik.Definitions.TestModel.SiteColumns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategik.Definitions.TestModel.Content_Types
{
    public static class STKTestContentTypes
    {
        #region Constants

        public static Guid itemContentTypeId = new Guid("{D671416C-629F-457E-A59D-41011ED42A65}");
        public static String itemSharePointContentTypeId = "0x010071213BDF7C254F0099D479243B920A54";
        public static Guid documentContentTypeId = new Guid("{A7ECAE2E-8FF2-44AF-BF73-BCA3CB2F4719}");
        public static String documentSharePointContentTypeId = "0x0101007CBF43EA7B7F470CA27424A62DA0011D";

        #endregion

        #region Definitions

        public static List<STKContentType> GetContentTypes()
        {
            List<STKContentType> contentTypes = new List<STKContentType>();

            return contentTypes;
        }


        public static STKContentType ItemContentType()
        {
            STKContentType contentType = new STKContentType()
            {
                UniqueId = itemContentTypeId,
                SharePointContentTypeId = itemSharePointContentTypeId,
                GroupName = "Test_ContentTypes",
                Name = "TestItem"
            };

            contentType.SiteColumns.Add(STKTestSiteColumns.BoolSiteColumn());
            contentType.SiteColumns.Add(STKTestSiteColumns.RichTextSiteColumn());
            contentType.SiteColumns.Add(STKTestSiteColumns.DateSiteColumn());

            contentType.SiteColumnLinks.Add(new STKFieldLink() { SiteColumnId = STKTestSiteColumns.guidFieldId });
            contentType.SiteColumnLinks.Add(new STKFieldLink() { SiteColumnId = STKTestSiteColumns.textFieldId });

            return contentType;
        }

        public static STKContentType DocumentContentType()
        {
            STKContentType contentType = new STKContentType()
            {
                UniqueId = documentContentTypeId,
                SharePointContentTypeId = documentSharePointContentTypeId,
                GroupName = "Test_ContentTypes",
                Name = "TestDocument"
            };

            contentType.SiteColumns.Add(STKTestSiteColumns.BoolSiteColumn());
            contentType.SiteColumns.Add(STKTestSiteColumns.RichTextSiteColumn());
            contentType.SiteColumns.Add(STKTestSiteColumns.DateSiteColumn());
            contentType.SiteColumns.Add(STKTestSiteColumns.NumberSiteColumn());

            contentType.SiteColumnLinks.Add(new STKFieldLink() { SiteColumnId = STKTestSiteColumns.guidFieldId });
            contentType.SiteColumnLinks.Add(new STKFieldLink() { SiteColumnId = STKTestSiteColumns.textFieldId });

            return contentType;
        }

        #endregion
    }
}

