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

using Strategik.Definitions.Lists;
using Strategik.Definitions.Shared;
using System;
using System.Xml;

namespace Strategik.Definitions.Fields
{
    /// <summary>
    /// Defines a new lokkup field to be provisioned programatically
    /// </summary>
    /// <remarks>
    /// Lookup fields must be provisioned programatically "in situ" by
    /// grabbing the id of the list we are looking up to at runtime.
    /// </remarks>
    public class STKLookupField : STKField
    {
        #region Data

        public STKList LookupListDefinition { get; set; } // will always use this in preference

        public String LookupListName { get; set; }

        public String LookupFieldName { get; set; }

        public Guid LookupWebId { get; set; }

        public Guid LookupListId { get; set; }

        public bool AllowMultipleValues { get; set; }

        public String LookupListRelativeUrl { get; set; }

        #endregion Data

        #region Constructor

        public STKLookupField()
        {
            base.SharePointType = STKFieldType.Lookup;
        }

        #endregion Constructor

        #region Base class overrides - Custom attributes for CAML

        protected override void AddCustomFieldAttributes(XmlWriter xmlWriter)
        {
            xmlWriter.WriteAttributeString(STKDefinitionConstants.ListUrl, LookupListRelativeUrl);
            xmlWriter.WriteAttributeString(STKDefinitionConstants.ShowField, LookupFieldName);
        }

        #endregion Base class overrides - Custom attributes for CAML
    }
}