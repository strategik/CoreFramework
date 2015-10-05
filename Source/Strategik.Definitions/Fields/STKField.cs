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

using Strategik.Definitions.Base;
using Strategik.Definitions.Shared;
using System;
using System.IO;
using System.Xml;

namespace Strategik.Definitions.Fields
{
    /// <summary>
    /// Base class for defining fields in a sharepoint list or as site columns
    /// </summary>
    public class STKField : STKDefinitionBase
    {
        #region Properties

        public bool AddToDefaultView { get; set; }

        public String AuthoringInfo { get; set; }

        public String DefaultValue { get; set; }

        // Used to indicate that a site column should be deleted when the feature that installed it is deactivated
        public bool DeleteSiteColumnOnDeactivation { get; set; }

        public String FieldClassName { get; set; }

        public bool Filterable { get; set; }

        public String GroupName { get; set; }

        public bool Hidden { get; set; }

        // used to indicate that this class represents a built in SharePoint site column
        public bool IsBuiltInSiteColumn { get; set; }

        public bool IsCustomFieldType { get; set; }

        // Used to explicity indicate that this field should be defined as a site column
        public bool IsSiteColumn { get; set; }

        public bool ReadOnly { get; set; }

        public bool Required { get; set; }

        /// <summary>
        /// Not generated
        /// </summary>
        public String SchemaXml { get; set; }

        public STKFieldType SharePointType { get; set; }

        public bool ShowInDisplayForm { get; set; }

        public bool ShowInEditForm { get; set; }

        public bool ShowInListSettings { get; set; }

        public bool ShowInNewForm { get; set; }

        public bool ShowInVersionHistory { get; set; }

        public bool ShowInViewForms { get; set; }

        // Defines the scope of a site column - ignored when the column is being added to a list directly
        public STKScope SiteColumnScope { get; set; }

        public bool Sortable { get; set; }

        /// <summary>
        /// String representation of a Guid representing this field
        /// </summary>
        public String StaticName { get; set; }

        public String JSLink { get; set; }

        #endregion Properties

        #region Constructors

        public STKField()
        {
            UniqueId = Guid.NewGuid();
            SharePointType = STKFieldType.Text;
            IsSiteColumn = false;
            ShowInNewForm = true;
            ShowInDisplayForm = true;
            ShowInListSettings = true;
            ShowInVersionHistory = true;
            ShowInViewForms = true;
            ShowInEditForm = true;
            SiteColumnScope = STKScope.Site;
        }

        public STKField(String id, String name, String displayName, STKFieldType type, String groupName, string staticName)
            : this()
        {
            UniqueId = new Guid(id);
            Name = name;
            DisplayName = displayName;
            SharePointType = type;
            GroupName = groupName;
            StaticName = staticName;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Fields are provisioned by generating a CAML fragment as this apprears to allow options
        /// whihc are not currently available through the Sharepoint object model, e.g, defining the guid for the field
        /// </summary>
        /// <returns>XML Formated Filed</returns>
        public string GetProvisioningXML()
        {
            StringWriter sw = new StringWriter();
            XmlWriter xmlWriter = XmlWriter.Create(sw);

            // <Field
            xmlWriter.WriteStartElement(STKDefinitionConstants.FieldElement);

            xmlWriter.WriteAttributeString(STKDefinitionConstants.IdAttribute, UniqueId.ToString());
            xmlWriter.WriteAttributeString(STKDefinitionConstants.NameAttribute, Name);

            if (IsCustomFieldType == false)
            {
                xmlWriter.WriteAttributeString(STKDefinitionConstants.TypeAttribute, SharePointType.ToString());
            }
            else
            {
                xmlWriter.WriteAttributeString(STKDefinitionConstants.TypeAttribute, FieldClassName);
            }

            // Required
            if (Required)
            {
                xmlWriter.WriteAttributeString(STKDefinitionConstants.RequiredAttribute, STKDefinitionConstants.TRUE);
            }
            else
            {
                xmlWriter.WriteAttributeString(STKDefinitionConstants.RequiredAttribute, STKDefinitionConstants.FALSE);
            }

            // ReadOnly
            if (ReadOnly)
            {
                xmlWriter.WriteAttributeString(STKDefinitionConstants.ReadonlyAttribute, STKDefinitionConstants.TRUE);
            }
            else
            {
                xmlWriter.WriteAttributeString(STKDefinitionConstants.ReadonlyAttribute, STKDefinitionConstants.FALSE);
            }

            // Display Name
            if (string.IsNullOrEmpty(DisplayName) == false)
            {
                xmlWriter.WriteAttributeString(STKDefinitionConstants.DisplaynameAttribute, DisplayName);
            }
            // Group Name
            if (string.IsNullOrEmpty(GroupName) == false)
            {
                xmlWriter.WriteAttributeString(STKDefinitionConstants.GroupnameAttribute, GroupName);
            }
            //Static Name
            if (String.IsNullOrEmpty(StaticName) == false)
            {
                xmlWriter.WriteAttributeString(STKDefinitionConstants.StaticnameAttribute, StaticName);
            }

            //Authoring Info
            if (String.IsNullOrEmpty(AuthoringInfo) == false)
            {
                xmlWriter.WriteAttributeString(STKDefinitionConstants.AuthoringinfoAttribute, AuthoringInfo);
            }

            // Filterable

            if (Filterable)
            {
                xmlWriter.WriteAttributeString(STKDefinitionConstants.FilterableAttribute, STKDefinitionConstants.TRUE);
            }

            // Hidden

            if (Hidden)
            {
                xmlWriter.WriteAttributeString(STKDefinitionConstants.HiddenAttribute, STKDefinitionConstants.TRUE);
            }

            // Sortable

            if (Sortable)
            {
                xmlWriter.WriteAttributeString(STKDefinitionConstants.SortableAttribute, STKDefinitionConstants.TRUE);
            }
            // ShowInNewForm
            if (ShowInNewForm)
            {
                xmlWriter.WriteAttributeString(STKDefinitionConstants.ShowinnewformAttribute, STKDefinitionConstants.TRUE);
            }
            else
            {
                xmlWriter.WriteAttributeString(STKDefinitionConstants.ShowinnewformAttribute, STKDefinitionConstants.FALSE);
            }
            // ShowInDisplayForm
            if (ShowInDisplayForm)
            {
                xmlWriter.WriteAttributeString(STKDefinitionConstants.ShowindisplayformAttribute, STKDefinitionConstants.TRUE);
            }
            else
            {
                xmlWriter.WriteAttributeString(STKDefinitionConstants.ShowindisplayformAttribute, STKDefinitionConstants.FALSE);
            }
            // ShowInEditForm
            if (ShowInEditForm)
            {
                xmlWriter.WriteAttributeString(STKDefinitionConstants.ShowineditformAttribute, STKDefinitionConstants.TRUE);
            }
            else
            {
                xmlWriter.WriteAttributeString(STKDefinitionConstants.ShowineditformAttribute, STKDefinitionConstants.FALSE);
            }
            // ShowInListSettings
            if (ShowInListSettings)
            {
                xmlWriter.WriteAttributeString(STKDefinitionConstants.ShowinlistsettingsAttribute, STKDefinitionConstants.TRUE);
            }
            else
            {
                xmlWriter.WriteAttributeString(STKDefinitionConstants.ShowinlistsettingsAttribute, STKDefinitionConstants.FALSE);
            }
            // ShowInVersionHistory
            if (ShowInVersionHistory)
            {
                xmlWriter.WriteAttributeString(STKDefinitionConstants.ShowinversionhistoryAttribute, STKDefinitionConstants.TRUE);
            }
            else
            {
                xmlWriter.WriteAttributeString(STKDefinitionConstants.ShowinversionhistoryAttribute, STKDefinitionConstants.FALSE);
            }
            // ShowInViewForms
            if (ShowInViewForms)
            {
                xmlWriter.WriteAttributeString(STKDefinitionConstants.ShowinviewformsAttribute, STKDefinitionConstants.TRUE);
            }
            else
            {
                xmlWriter.WriteAttributeString(STKDefinitionConstants.ShowinnewformAttribute, STKDefinitionConstants.FALSE);
            }

            AddCustomFieldAttributes(xmlWriter);

            // Default Value
            if (String.IsNullOrEmpty(DefaultValue) == false)
            {
                xmlWriter.WriteElementString(STKDefinitionConstants.DefaultElement, DefaultValue);
            }

            // JSLink
            if (String.IsNullOrEmpty(JSLink) == false)
            {
                xmlWriter.WriteElementString(STKDefinitionConstants.JSLinkAttribute, JSLink);
            }

            xmlWriter.WriteEndElement(); // End of field element
            xmlWriter.Flush();
            return sw.ToString();
        }

        /// <summary>
        /// Sets a single name for both the title and internal name for this field
        /// </summary>
        /// <param name="name"></param>
        public void SetAllNameFields(string name)
        {
            Name = name;
            DisplayName = name;
            StaticName = name;
        }

        protected virtual void AddCustomFieldAttributes(XmlWriter xmlWriter)
        {
            ;
            // base classes add their custom attributed here
        }

        #endregion Methods
    }
}