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
using Strategik.Definitions.Shared;
using System;
using System.Collections.Generic;

namespace Strategik.Definitions.Lists
{
    /// <summary>
    /// Defines a view for a list
    /// </summary>
    public class STKListView
    {
        #region Data

        public Guid Id { get; set; }

        public String ViewName { get; set; }

        public List<String> InternalFieldNames { get; set; }

        public List<STKField> Fields { get; set; }

        public String Filter { get; set; }

        public uint PageSize { get; set; }

        public bool IsDefaultView { get; set; }

        public STKViewType ViewType { get; set; }

        public bool IsPersonalView { get; set; }

        public String InlineEdit { get; set; }

        public String ViewData { get; set; }

        public STKViewStyle ViewStyle { get; set; }

        #endregion Data

        #region Constructor

        public STKListView(String viewName)
        {
            this.ViewName = viewName;
            InternalFieldNames = new List<String>();
            Fields = new List<STKField>();
            Filter = String.Empty;
            PageSize = 25;
            IsDefaultView = false;
            IsPersonalView = false;
            ViewType = STKViewType.Html;
            ViewStyle = STKViewStyle.Default;
            InlineEdit = "false";
        }

        #endregion Constructor

        #region Methods

        public bool HasFilter()
        {
            return (String.IsNullOrEmpty(Filter)) ? false : true;
        }

        public List<String> GetAllInternalFieldNames()
        {
            List<string> allInternalFieldNames = new List<string>();
            allInternalFieldNames.AddRange(InternalFieldNames.ToArray());

            foreach (STKField field in Fields)
            {
                allInternalFieldNames.Add(field.StaticName);
            }

            return allInternalFieldNames;
        }

        #endregion Methods
    }
}