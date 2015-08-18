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
using System.Xml;

namespace Strategik.Definitions.Fields
{
    /// <summary>
    /// This class encapsulates a SharePoint field of type "Number".
    /// The Number field type requires the number of decimal places to be set.
    /// If 0 decimal places is required use <code>SPNumberFormats.NoDecimal</code> and the field type will be be set to <code>SPFieldType.Integer</code>
    /// This prevents the need for an IntegerField class - are there any issues with this?
    /// </summary>
    public class STKNumberField : STKField
    {
        public bool? UseCommas { get; set; }

        public int? DecimalPlaces { get; set; }

        public int? Max { get; set; }

        public int? Min { get; set; }

        public STKNumberField()
        {
            base.SharePointType = STKFieldType.Number;
        }

        protected override void AddCustomFieldAttributes(XmlWriter xmlWriter)
        {
            base.AddCustomFieldAttributes(xmlWriter);

            if (UseCommas.HasValue && UseCommas.Value == true)
            {
                xmlWriter.WriteAttributeString(STKDefinitionConstants.UsecommasAttribute, STKDefinitionConstants.TRUE);
            }

            if (DecimalPlaces.HasValue)
            {
                // Don't set integer as there is no such type in UI
                //if (DecimalPlaces == SPNumberFormatTypes.NoDecimal)
                //{
                //    base.Type = SPFieldType.Integer.ToString();
                //}
                //int i = 0; //TODO
                // int i = (int)Enum.Parse(typeof(SPNumberFormatTypes), Enum.GetName(typeof(SPNumberFormatTypes), DecimalPlaces));
                xmlWriter.WriteAttributeString(STKDefinitionConstants.DecimalsAttribute, DecimalPlaces.ToString());
            }

            if (Max.HasValue)
            {
                xmlWriter.WriteAttributeString(STKDefinitionConstants.MaxvalueAttribute, Max.Value.ToString());
            }

            if (Min.HasValue)
            {
                xmlWriter.WriteAttributeString(STKDefinitionConstants.MinvalueAttribute, Min.Value.ToString());
            }
        }
    }
}