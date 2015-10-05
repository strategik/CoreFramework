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

using Strategik.Definitions.Shared;
using System;
using System.Collections.Generic;
using System.Xml;

namespace Strategik.Definitions.Fields
{
    public class STKChoiceField : STKField
    {
        #region Properties

        public List<String> Choices { get; set; }

        public String Format { get; set; }

        public bool AllowFillInChoice { get; set; }

        #endregion Properties

        #region Constructor

        public STKChoiceField()
            : base()
        {
            base.SharePointType = STKFieldType.Choice;
            Choices = new List<string>();
            Format = "Dropdown";
        }

        public STKChoiceField(String id, String name, String displayName, STKFieldType type, String groupName, string staticName, List<string> choices, String format)
            : base(id, name, displayName, type, groupName, staticName)
        {
            Choices = choices;
            Format = format;
        }

        #endregion Constructor

        #region Custom Provisioning CAML

        protected override void AddCustomFieldAttributes(XmlWriter xmlWriter)
        {
            // Write the choice element specific CAML
            if (Choices.Count > 0)
            {
                xmlWriter.WriteAttributeString(STKDefinitionConstants.FillinchoiceAttribute, AllowFillInChoice.ToString());
                xmlWriter.WriteAttributeString(STKDefinitionConstants.FormatAttribute, Format.ToString());
                // source attribute required ?

                xmlWriter.WriteStartElement(STKDefinitionConstants.ChoicesElement);

                foreach (String choice in Choices)
                {
                    xmlWriter.WriteElementString(STKDefinitionConstants.ChoiceElement, choice);
                }

                xmlWriter.WriteEndElement();
            }
        }

        #endregion Custom Provisioning CAML
    }
}