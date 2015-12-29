
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

using Strategik.Definitions.Fields;
using System;
using System.Collections.Generic;


namespace Strategik.Definitions.ContentTypes
{
    public static partial class STKContentTypeExtensions
    {
        #region Content Type Validation

        public static bool IsValid(this STKContentType contentType)
        {
            try
            {
                contentType.Validate();
                return true;
            }
            catch 
            {
                return false;
            }
        }

        public static void Validate(this STKContentType contentType)
        {
            if (contentType == null) throw new ArgumentNullException("contentType");
            if (String.IsNullOrEmpty(contentType.SharePointContentTypeId)) throw new Exception("SharePoint ContentTypeId is empty " + contentType.Name);
            if (String.IsNullOrEmpty(contentType.Name)) throw new Exception("Field name is empty " + contentType.UniqueId);
            if (String.IsNullOrEmpty(contentType.GroupName)) throw new Exception("Group name is empty " + contentType.Name);

            foreach (STKField field in contentType.SiteColumns)
            {
                field.Validate();
            }

            foreach (STKFieldLink fieldLink in contentType.SiteColumnLinks)
            {
                fieldLink.Validate();
            }
        }

        #endregion Content Type Validation

        #region Field Link Validation

        public static bool IsValid(this STKFieldLink fieldLink)
        {
            try
            {
                fieldLink.Validate();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void Validate(this STKFieldLink fieldLink)
        {
            if (fieldLink == null) throw new ArgumentNullException("fieldLink");
            if (String.IsNullOrEmpty(fieldLink.DisplayName)) throw new Exception("FieldLink Display Name is empty " + fieldLink.SiteColumnId);

            if (fieldLink.IsBuiltInSiteColumnLink == false && fieldLink.ContentType == null) throw new Exception("Parent content type is not set for fieldlink");
        }

        #endregion
    }
}
