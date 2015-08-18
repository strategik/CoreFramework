
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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Strategik.Definitions.ContentTypes;
using Strategik.Definitions.Fields;

namespace OfficeDevPnP.Core.Framework.Provisioning.Model
{
    public static partial class FieldExtensions
    {
        #region Generate strategik definition from corresponding PnP template object

        public static STKField GenerateStrategikDefinition(this Field field)
        {
            STKField stkField = null;
            XElement fieldElement = XElement.Parse(field.SchemaXml);
            String fieldType = fieldElement.Attribute("Type").Value;

            // Boolean
            if (fieldType.Equals("boolean", StringComparison.InvariantCultureIgnoreCase))
            {
                stkField = GenerateBooleanField(fieldElement);
            }
            // Choice
            else if (fieldType.Equals("choice", StringComparison.InvariantCultureIgnoreCase))
            {
                stkField = GenerateChoiceField(fieldElement);
            }
            // DateTime
            else if (fieldType.Equals("datetime", StringComparison.InvariantCultureIgnoreCase))
            {
                stkField = GenerateDateField(fieldElement);
            }
            else 
                // unpack other field types here as required
            {
                stkField = new STKField();
            }



            // Set the common properties
            SetCommonProperties(stkField, fieldElement);

            return stkField;
        }

        public static STKFieldLink GenerateStrategikDefinition(this FieldRef fieldRef)
        {
            STKFieldLink stkFieldLink = new STKFieldLink()
            {
                Name = fieldRef.Name,
                DisplayName = fieldRef.DisplayName,
                IsHidden = fieldRef.Hidden,
                IsRequired = fieldRef.Required,
                SiteColumnId = fieldRef.Id
            };

            return stkFieldLink;
        }

        #endregion

        #region Helper Methods

        public static STKBooleanField GenerateBooleanField(XElement fieldElement)
        {
            STKBooleanField stkBooleanField = new STKBooleanField()
            {

            };


            return stkBooleanField;
        }

        public static STKChoiceField GenerateChoiceField(XElement fieldElement)
        {
            STKChoiceField stkChoiceField = new STKChoiceField()
            {};

            // Fill in choice
            if (fieldElement.Attribute("FillinChoice") != null)
            {
                stkChoiceField.AllowFillInChoice = Boolean.Parse(fieldElement.Attribute("FillinChoice").Value);
            }

            // Format
            if (fieldElement.Attribute("Format") != null)
            {
                stkChoiceField.Format = fieldElement.Attribute("Format").Value;
            }

            // Choices
            XElement choicesElement = fieldElement.Element("Choices");
            foreach (XElement choiceElement in choicesElement.Elements())
            {
                stkChoiceField.Choices.Add(choiceElement.Value);   
            }

            return stkChoiceField;
        }

        public static STKDateField GenerateDateField(XElement fieldElement)
        {
            STKDateField stkDateField = new STKDateField()
            {

            };

            //// Format
            //if (fieldElement.Attribute("Format") != null)
            //{
            //    stkDateField.Format = fieldElement.Attribute("Format").Value;
            //}

            return stkDateField;
        }

        public static STKGuidField GenerateGuidField(XElement fieldElement)
        {
            STKGuidField stkGuidField = new STKGuidField()
            {
                
            };

            return stkGuidField;
        }

        public static STKHyperlinkField GenerateHyperlinkField(XElement fieldElement)
        {
            STKHyperlinkField stkHyperlinkField = new STKHyperlinkField();
            return stkHyperlinkField;
        }

        public static STKLookupField GenerateLookupField(XElement fieldElement)
        {
            STKLookupField stkLookupField = new STKLookupField();

            // Format
            if (fieldElement.Attribute("List") != null)
            {
                stkLookupField.LookupListId = new Guid(fieldElement.Attribute("List").Value); // check this
            }

            // Format
            if (fieldElement.Attribute("ShowField") != null)
            {
                stkLookupField.LookupFieldName = fieldElement.Attribute("Format").Value;
            }

            return stkLookupField;
        }

        public static STKMultiChoiceField GenerateMultiChoiceField(XElement fieldElement)
        {
            STKMultiChoiceField stkMultiChoiceField = new STKMultiChoiceField();

            // Choices
            XElement choicesElement = fieldElement.Element("Choices");
            foreach (XElement choiceElement in choicesElement.Elements())
            {
                stkMultiChoiceField.Choices.Add(choiceElement.Value);
            }

            return stkMultiChoiceField;
        }

        public static STKNumberField GenerateNumberField(XElement fieldElement)
        {
            STKNumberField stkNumberField = new STKNumberField();

            // Commas
            if (fieldElement.Attribute("Commas") != null)
            {
                stkNumberField.UseCommas = Boolean.Parse(fieldElement.Attribute("Commas").Value);
            }

            // Decimal Places
            if (fieldElement.Attribute("Decimals") != null)
            {
                stkNumberField.DecimalPlaces = Int32.Parse(fieldElement.Attribute("Decimals").Value);
            }

            // Max
            if (fieldElement.Attribute("Max") != null)
            {
                stkNumberField.Max = Int32.Parse(fieldElement.Attribute("Max").Value);
            }

            // Min
            if (fieldElement.Attribute("Min") != null)
            {
                stkNumberField.Min = Int32.Parse(fieldElement.Attribute("Min").Value);
            }

            return stkNumberField;
        }

        public static STKRichTextField GenerateRichTextField(XElement fieldElement)
        {
            STKRichTextField stkRichTextField = new STKRichTextField();

            // Rich Text
            if (fieldElement.Attribute("RichText") != null)
            {
                stkRichTextField.Mode = (RichTextMode) Enum.Parse(typeof(RichTextMode), fieldElement.Attribute("RichText").Value);
            }

            // Append Only
            if (fieldElement.Attribute("AppendOnly") != null)
            {
                stkRichTextField.AppendOnly = Boolean.Parse(fieldElement.Attribute("AppendOnly").Value);
            }

            return stkRichTextField;
        }

        public static STKTaxonomyField GenerateTaxonomyField(XElement fieldElement)
        {
            STKTaxonomyField stkTaxonomyField = new STKTaxonomyField();

            //TODO: See what gets returned

            return stkTaxonomyField;
        }

        public static STKTextField GenerateTextField(XElement fieldElement)
        {
            STKTextField stkTextField = new STKTextField();

            // Number of lines
            if (fieldElement.Attribute("NumLines") != null)
            {
                stkTextField.Lines = Int32.Parse(fieldElement.Attribute("NumLines").Value);
            }

            // Max Length
            if (fieldElement.Attribute("MaxLength") != null)
            {
                stkTextField.MaxLength = Int32.Parse(fieldElement.Attribute("MaxLength").Value);
            }

            // Strip Whitepace
            if (fieldElement.Attribute("StripWS") != null)
            {
                stkTextField.StripWhitespace = Boolean.Parse(fieldElement.Attribute("StripWS").Value);
            }

            return stkTextField;
        }

        public static STKUserField GenerateUserField(XElement fieldElement)
        {
            STKUserField stkUserField = new STKUserField();
            return stkUserField;
        }

        public static STKField SetCommonProperties(STKField stkField, XElement fieldElement)
        {
             // Id (required)
            String idString = fieldElement.Attribute("ID").Value;
            stkField.UniqueId = new Guid(idString);

            // Group
            if (fieldElement.Attribute("Group") != null)
            {
                stkField.GroupName = fieldElement.Attribute("Group").Value;
            }

            // Description
            if (fieldElement.Attribute("Description") != null)
            {
                stkField.Description = fieldElement.Attribute("Description").Value;
            }

            // Display Name
            if (fieldElement.Attribute("DisplayName") != null)
            {
                stkField.DisplayName = fieldElement.Attribute("DisplayName").Value;
            }

            // Hidden
            if (fieldElement.Attribute("Hidden") != null)
            {
                stkField.Hidden = Boolean.Parse(fieldElement.Attribute("Hidden").Value);
            }
            
            // Name (required)
            stkField.Name = fieldElement.Attribute("Name").Value;
            
            // Read Only
            if (fieldElement.Attribute("ReadOnly") != null)
            {
                stkField.ReadOnly = Boolean.Parse(fieldElement.Attribute("ReadOnly").Value);
            }

            // Show in Display Form
            if (fieldElement.Attribute("ShowInDisplayForm") != null)
            {
                stkField.ShowInDisplayForm = Boolean.Parse(fieldElement.Attribute("ShowInDisplayForm").Value);
            }

            // Show in Edit Form
            if (fieldElement.Attribute("ShowInEditForm") != null)
            {
                stkField.ShowInEditForm = Boolean.Parse(fieldElement.Attribute("ShowInEditForm").Value);
            }

            // Show in New Form
            if (fieldElement.Attribute("ShowInNewForm") != null)
            {
                stkField.ShowInNewForm = Boolean.Parse(fieldElement.Attribute("ShowInNewForm").Value);
            }

            // Show in List settings
            if (fieldElement.Attribute("ShowInListSettings") != null)
            {
                stkField.ShowInListSettings = Boolean.Parse(fieldElement.Attribute("ShowInListSettings").Value);
            }

            // Show in Version History settings
            if (fieldElement.Attribute("ShowInVersionHistory") != null)
            {
                stkField.ShowInVersionHistory = Boolean.Parse(fieldElement.Attribute("ShowInVersionHistory").Value);
            }

             // Filterable
            if (fieldElement.Attribute("Filterable") != null)
            {
                stkField.Filterable = Boolean.Parse(fieldElement.Attribute("Filterable").Value);
            }

            // Required
            if (fieldElement.Attribute("Required") != null)
            {
                stkField.Required = Boolean.Parse(fieldElement.Attribute("Required").Value);
            }

            // Sortable
            if (fieldElement.Attribute("Sortable") != null)
            {
                stkField.Sortable = Boolean.Parse(fieldElement.Attribute("Sortable").Value);
            }

            // Title
            if (fieldElement.Attribute("Title") != null)
            {
                stkField.Title = fieldElement.Attribute("Title").Value;
            }

            return stkField;
        }

        #endregion
    }
}
