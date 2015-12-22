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
//
// Author:  Dr Adrian Colquhoun
//
#endregion License

using System;
using System.Collections.Generic;

namespace Strategik.Definitions.Base
{
    /// <summary>
    ///  Base definition class to factor our common properties
    /// </summary>
    public class STKDefinitionBase
    {
        #region Shared Properties
        /// <summary>
        /// A description of the item.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// The display name (if supported) - will appear in the UI
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// An identifier for this definition
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The internal name for the item
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The title of the item, takes precidence over display name if set
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// A unique identifer for the item - does not change between installations or versions
        /// </summary>
        public Guid UniqueId { get; set; }
        /// <summary>
        /// Allows us to explicitly track the major version of this definition
        /// </summary>
        public int MajorVersion { get; set; }
        /// <summary>
        /// Allows us to explicitly track the minor version of this definition
        /// </summary>
        public int MinorVersion { get; set; }
        /// <summary>
        /// Allows us to store a set or abritary properties within any definition
        /// </summary>
        public Dictionary<String, String> STKProperties { get; set; }

        #endregion Shared Properties

        #region Constructor

        public STKDefinitionBase()
        {
            STKProperties = new Dictionary<String, String>();
        }

        #endregion
    }
}