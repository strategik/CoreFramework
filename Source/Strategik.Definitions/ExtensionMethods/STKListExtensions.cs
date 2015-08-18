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

using Strategik.Definitions.ContentTypes;
using Strategik.Definitions.Fields;
using System;

namespace Strategik.Definitions.Lists
{
    public static partial class STKListExtensions
    {
        #region Validation
        public static bool IsValid(this STKList list)
        {
            try
            {
                list.Validate();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void Validate(this STKList list)
        {
            if (list == null) throw new Exception("List is null");
            if (list.UniqueId == Guid.Empty) throw new Exception("List unique is empty + field " + list.Name);
            if (String.IsNullOrEmpty(list.Name)) throw new Exception("List name is empty " + list.UniqueId);

            foreach(STKField field in list.Fields)
            {
                field.Validate();
            }

            foreach (STKFieldLink fieldLink in list.FieldLinks)
            {
                fieldLink.Validate();
            }

            foreach (STKContentType contentType in list.ContentTypes)
            {
                contentType.Validate();
            }
        }

    
        #endregion

        #region Helper Methods

        public static bool HasContentTypes(this STKList list)
        {
            return (list.ContentTypes.Count > 0);
        }

        public static bool HasCustomFields(this STKList list)
        {
            return list.CustomFields.Count > 0;
        }

        public static bool HasCustomListViews(this STKList list)
        {
            return list.Views.Count > 0;
        }

        public static bool HasDefaultData(this STKList list)
        {
            return false; // TODO: ???
        }

        public static bool HasDescription(this STKList list)
        {
            return (String.IsNullOrEmpty(list.Description)) ? false : true;
        }

        public static bool HasDisplayName(this STKList list)
        {
            return (!String.IsNullOrEmpty(list.DisplayName));
        }

        public static bool HasEventHandlers(this STKList list)
        {
            return list.EventHandlers.Count > 0;
        }

        public static bool HasFolders(this STKList list)
        {
            return list.Folders.Count > 0;
        }

        public static bool HasSiteColumns(this STKList list)
        {
            return list.SiteColumns.Count > 0;
        }

        #endregion Helper Methods
    }
}