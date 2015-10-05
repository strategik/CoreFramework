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

        public static List<STKContentType> GetAllContentTypes()
        {
            List<STKContentType> contentTypes = new List<STKContentType>();
            contentTypes.Add(ItemContentType());
            contentTypes.Add(DocumentContentType());
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

