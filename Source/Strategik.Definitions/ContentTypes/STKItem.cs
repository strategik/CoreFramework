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

using Strategik.Definitions.Fields;
using System;

namespace Strategik.Definitions.ContentTypes
{
    /// <summary>
    /// Class which represents SharePoint's Built-in "Item" Content Type.
    /// </summary>
    public class STKItem : STKContentType
    {
        #region Fields

        // Item - these fields will always be present for any content type on a list
        public STKNumberField IdColumn;
        public STKDateField CreatedColumn;
        public STKDateField LastUpdatedColumn;
        public STKUserField EditorColumn;
        public STKUserField AuthorColumn;
        public STKTextField TitleColumn;
        public STKTextField ContentTypeColumn;

        #endregion

        #region Constructor

        public STKItem()
        {
            Define();
        }

        private void Define()
        {
            base.SharePointContentTypeId = "0x01";
            base.IsBuiltInContentType = true;
            base.IsItemContentType = true;
            STKContentType definingType = new STKItem();

            // Initialise the default columns
            IdColumn = new STKNumberField()
            {
                ContentType = definingType,
                StaticName = "ID",
                IsBuiltInSiteColumn = true,
                ReadOnly = true
            };
            base.SiteColumns.Add(IdColumn);

            CreatedColumn = new STKDateField()
            {
                ContentType =definingType,
                StaticName = "Created",
                IsBuiltInSiteColumn = true,
                ReadOnly = true
            };
            base.SiteColumns.Add(CreatedColumn);

            LastUpdatedColumn = new STKDateField()
            {
                ContentType = definingType,
                StaticName = "LastUpdated",
                IsBuiltInSiteColumn = true,
                ReadOnly = true
            };
            base.SiteColumns.Add(LastUpdatedColumn);

            EditorColumn = new STKUserField()
            {
                ContentType = definingType,
                StaticName = "Editor",
                IsBuiltInSiteColumn = true,
                ReadOnly = true
            };
            base.SiteColumns.Add(EditorColumn);

            AuthorColumn = new STKUserField()
            {
                ContentType = definingType,
                StaticName = "Author",
                IsBuiltInSiteColumn = true,
                ReadOnly = true
            };
            base.SiteColumns.Add(AuthorColumn);

            TitleColumn = new STKTextField()
            {
                ContentType = definingType,
                StaticName = "Title",
                IsBuiltInSiteColumn = true,
            };
            base.SiteColumns.Add(TitleColumn);

            ContentTypeColumn = new STKTextField()
            {
                ContentType = definingType,
                StaticName = "ContentType",
                IsBuiltInSiteColumn = true,
            };
            base.SiteColumns.Add(ContentTypeColumn);

        }

        #endregion Constructor
    }
}