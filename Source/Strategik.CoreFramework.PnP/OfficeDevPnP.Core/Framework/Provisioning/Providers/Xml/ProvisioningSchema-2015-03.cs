﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by xsd, Version=4.0.30319.18020.
// 

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Xml.V201503 {
    /// <remarks/>
    [GeneratedCode("xsd", "4.0.30319.18020")]
    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType=true, Namespace="http://schemas.dev.office.com/PnP/2015/03/ProvisioningSchema")]
    [XmlRoot(Namespace="http://schemas.dev.office.com/PnP/2015/03/ProvisioningSchema", IsNullable=false)]
    public partial class SharePointProvisioningTemplate {
        
        private string sitePolicyField;
        
        private PropertyBagEntry[] propertyBagEntriesField;
        
        private SharePointProvisioningTemplateSecurity securityField;
        
        private SharePointProvisioningTemplateSiteFields siteFieldsField;
        
        private SharePointProvisioningTemplateContentTypes contentTypesField;
        
        private ListInstance[] listsField;
        
        private SharePointProvisioningTemplateFeatures featuresField;
        
        private SharePointProvisioningTemplateCustomActions customActionsField;
        
        private File[] filesField;
        
        private ComposedLook composedLookField;
        
        private Provider[] providersField;
        
        private string idField;
        
        private string versionField;
        
        /// <remarks/>
        public string SitePolicy {
            get {
                return this.sitePolicyField;
            }
            set {
                this.sitePolicyField = value;
            }
        }
        
        /// <remarks/>
        [XmlArrayItem(IsNullable=false)]
        public PropertyBagEntry[] PropertyBagEntries {
            get {
                return this.propertyBagEntriesField;
            }
            set {
                this.propertyBagEntriesField = value;
            }
        }
        
        /// <remarks/>
        public SharePointProvisioningTemplateSecurity Security {
            get {
                return this.securityField;
            }
            set {
                this.securityField = value;
            }
        }
        
        /// <remarks/>
        public SharePointProvisioningTemplateSiteFields SiteFields {
            get {
                return this.siteFieldsField;
            }
            set {
                this.siteFieldsField = value;
            }
        }
        
        /// <remarks/>
        public SharePointProvisioningTemplateContentTypes ContentTypes {
            get {
                return this.contentTypesField;
            }
            set {
                this.contentTypesField = value;
            }
        }
        
        /// <remarks/>
        [XmlArrayItem(IsNullable=false)]
        public ListInstance[] Lists {
            get {
                return this.listsField;
            }
            set {
                this.listsField = value;
            }
        }
        
        /// <remarks/>
        public SharePointProvisioningTemplateFeatures Features {
            get {
                return this.featuresField;
            }
            set {
                this.featuresField = value;
            }
        }
        
        /// <remarks/>
        public SharePointProvisioningTemplateCustomActions CustomActions {
            get {
                return this.customActionsField;
            }
            set {
                this.customActionsField = value;
            }
        }
        
        /// <remarks/>
        [XmlArrayItem(IsNullable=false)]
        public File[] Files {
            get {
                return this.filesField;
            }
            set {
                this.filesField = value;
            }
        }
        
        /// <remarks/>
        public ComposedLook ComposedLook {
            get {
                return this.composedLookField;
            }
            set {
                this.composedLookField = value;
            }
        }
        
        /// <remarks/>
        [XmlArrayItem(IsNullable=false)]
        public Provider[] Providers {
            get {
                return this.providersField;
            }
            set {
                this.providersField = value;
            }
        }
        
        /// <remarks/>
        [XmlAttribute(DataType="ID")]
        public string ID {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        [XmlAttribute()]
        public string Version {
            get {
                return this.versionField;
            }
            set {
                this.versionField = value;
            }
        }
    }
    
    /// <remarks/>
    [GeneratedCode("xsd", "4.0.30319.18020")]
    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(Namespace="http://schemas.dev.office.com/PnP/2015/03/ProvisioningSchema")]
    public partial class PropertyBagEntry {
        
        private string keyField;
        
        private string valueField;
        
        /// <remarks/>
        [XmlAttribute()]
        public string Key {
            get {
                return this.keyField;
            }
            set {
                this.keyField = value;
            }
        }
        
        /// <remarks/>
        [XmlAttribute()]
        public string Value {
            get {
                return this.valueField;
            }
            set {
                this.valueField = value;
            }
        }
    }
    
    /// <remarks/>
    [GeneratedCode("xsd", "4.0.30319.18020")]
    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(Namespace="http://schemas.dev.office.com/PnP/2015/03/ProvisioningSchema")]
    public partial class Provider {
        
        private XmlNode configurationField;
        
        private bool enabledField;
        
        private string assemblyField;
        
        private string typeField;
        
        public Provider() {
            this.enabledField = false;
        }
        
        /// <remarks/>
        public XmlNode Configuration {
            get {
                return this.configurationField;
            }
            set {
                this.configurationField = value;
            }
        }
        
        /// <remarks/>
        [XmlAttribute()]
        [DefaultValue(false)]
        public bool Enabled {
            get {
                return this.enabledField;
            }
            set {
                this.enabledField = value;
            }
        }
        
        /// <remarks/>
        [XmlAttribute()]
        public string Assembly {
            get {
                return this.assemblyField;
            }
            set {
                this.assemblyField = value;
            }
        }
        
        /// <remarks/>
        [XmlAttribute()]
        public string Type {
            get {
                return this.typeField;
            }
            set {
                this.typeField = value;
            }
        }
    }
    
    /// <remarks/>
    [GeneratedCode("xsd", "4.0.30319.18020")]
    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(Namespace="http://schemas.dev.office.com/PnP/2015/03/ProvisioningSchema")]
    public partial class ComposedLook {
        
        private string nameField;
        
        private string colorFileField;
        
        private string fontFileField;
        
        private string backgroundFileField;
        
        private string masterPageField;
        
        private string siteLogoField;
        
        private string alternateCSSField;
        
        private int versionField;
        
        private bool versionFieldSpecified;
        
        /// <remarks/>
        [XmlAttribute()]
        public string Name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        [XmlAttribute()]
        public string ColorFile {
            get {
                return this.colorFileField;
            }
            set {
                this.colorFileField = value;
            }
        }
        
        /// <remarks/>
        [XmlAttribute()]
        public string FontFile {
            get {
                return this.fontFileField;
            }
            set {
                this.fontFileField = value;
            }
        }
        
        /// <remarks/>
        [XmlAttribute()]
        public string BackgroundFile {
            get {
                return this.backgroundFileField;
            }
            set {
                this.backgroundFileField = value;
            }
        }
        
        /// <remarks/>
        [XmlAttribute()]
        public string MasterPage {
            get {
                return this.masterPageField;
            }
            set {
                this.masterPageField = value;
            }
        }
        
        /// <remarks/>
        [XmlAttribute()]
        public string SiteLogo {
            get {
                return this.siteLogoField;
            }
            set {
                this.siteLogoField = value;
            }
        }
        
        /// <remarks/>
        [XmlAttribute()]
        public string AlternateCSS {
            get {
                return this.alternateCSSField;
            }
            set {
                this.alternateCSSField = value;
            }
        }
        
        /// <remarks/>
        [XmlAttribute()]
        public int Version {
            get {
                return this.versionField;
            }
            set {
                this.versionField = value;
            }
        }
        
        /// <remarks/>
        [XmlIgnore()]
        public bool VersionSpecified {
            get {
                return this.versionFieldSpecified;
            }
            set {
                this.versionFieldSpecified = value;
            }
        }
    }
    
    /// <remarks/>
    [GeneratedCode("xsd", "4.0.30319.18020")]
    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(Namespace="http://schemas.dev.office.com/PnP/2015/03/ProvisioningSchema")]
    public partial class File {
        
        private string srcField;
        
        private string folderField;
        
        private bool overwriteField;
        
        public File() {
            this.overwriteField = false;
        }
        
        /// <remarks/>
        [XmlAttribute()]
        public string Src {
            get {
                return this.srcField;
            }
            set {
                this.srcField = value;
            }
        }
        
        /// <remarks/>
        [XmlAttribute()]
        public string Folder {
            get {
                return this.folderField;
            }
            set {
                this.folderField = value;
            }
        }
        
        /// <remarks/>
        [XmlAttribute()]
        [DefaultValue(false)]
        public bool Overwrite {
            get {
                return this.overwriteField;
            }
            set {
                this.overwriteField = value;
            }
        }
    }
    
    /// <remarks/>
    [GeneratedCode("xsd", "4.0.30319.18020")]
    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(Namespace="http://schemas.dev.office.com/PnP/2015/03/ProvisioningSchema")]
    public partial class CustomAction {
        
        private string nameField;
        
        private string descriptionField;
        
        private string groupField;
        
        private string locationField;
        
        private string titleField;
        
        private int sequenceField;
        
        private bool sequenceFieldSpecified;
        
        private int rightsField;
        
        private bool rightsFieldSpecified;
        
        private string urlField;
        
        private bool enabledField;
        
        private string scriptBlockField;
        
        private string imageUrlField;
        
        private string scriptSrcField;
        
        public CustomAction() {
            this.enabledField = true;
        }
        
        /// <remarks/>
        [XmlAttribute()]
        public string Name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        [XmlAttribute()]
        public string Description {
            get {
                return this.descriptionField;
            }
            set {
                this.descriptionField = value;
            }
        }
        
        /// <remarks/>
        [XmlAttribute()]
        public string Group {
            get {
                return this.groupField;
            }
            set {
                this.groupField = value;
            }
        }
        
        /// <remarks/>
        [XmlAttribute()]
        public string Location {
            get {
                return this.locationField;
            }
            set {
                this.locationField = value;
            }
        }
        
        /// <remarks/>
        [XmlAttribute()]
        public string Title {
            get {
                return this.titleField;
            }
            set {
                this.titleField = value;
            }
        }
        
        /// <remarks/>
        [XmlAttribute()]
        public int Sequence {
            get {
                return this.sequenceField;
            }
            set {
                this.sequenceField = value;
            }
        }
        
        /// <remarks/>
        [XmlIgnore()]
        public bool SequenceSpecified {
            get {
                return this.sequenceFieldSpecified;
            }
            set {
                this.sequenceFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [XmlAttribute()]
        public int Rights {
            get {
                return this.rightsField;
            }
            set {
                this.rightsField = value;
            }
        }
        
        /// <remarks/>
        [XmlIgnore()]
        public bool RightsSpecified {
            get {
                return this.rightsFieldSpecified;
            }
            set {
                this.rightsFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [XmlAttribute()]
        public string Url {
            get {
                return this.urlField;
            }
            set {
                this.urlField = value;
            }
        }
        
        /// <remarks/>
        [XmlAttribute()]
        [DefaultValue(true)]
        public bool Enabled {
            get {
                return this.enabledField;
            }
            set {
                this.enabledField = value;
            }
        }
        
        /// <remarks/>
        [XmlAttribute()]
        public string ScriptBlock {
            get {
                return this.scriptBlockField;
            }
            set {
                this.scriptBlockField = value;
            }
        }
        
        /// <remarks/>
        [XmlAttribute()]
        public string ImageUrl {
            get {
                return this.imageUrlField;
            }
            set {
                this.imageUrlField = value;
            }
        }
        
        /// <remarks/>
        [XmlAttribute()]
        public string ScriptSrc {
            get {
                return this.scriptSrcField;
            }
            set {
                this.scriptSrcField = value;
            }
        }
    }
    
    /// <remarks/>
    [GeneratedCode("xsd", "4.0.30319.18020")]
    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(Namespace="http://schemas.dev.office.com/PnP/2015/03/ProvisioningSchema")]
    public partial class Feature {
        
        private string idField;
        
        private bool deactivateField;
        
        private string descriptionField;
        
        public Feature() {
            this.deactivateField = false;
        }
        
        /// <remarks/>
        [XmlAttribute()]
        public string ID {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        [XmlAttribute()]
        [DefaultValue(false)]
        public bool Deactivate {
            get {
                return this.deactivateField;
            }
            set {
                this.deactivateField = value;
            }
        }
        
        /// <remarks/>
        [XmlAttribute()]
        public string Description {
            get {
                return this.descriptionField;
            }
            set {
                this.descriptionField = value;
            }
        }
    }
    
    /// <remarks/>
    [GeneratedCode("xsd", "4.0.30319.18020")]
    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(Namespace="http://schemas.dev.office.com/PnP/2015/03/ProvisioningSchema")]
    public partial class FieldRef {
        
        private string idField;
        
        /// <remarks/>
        [XmlAttribute()]
        public string ID {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
    }
    
    /// <remarks/>
    [GeneratedCode("xsd", "4.0.30319.18020")]
    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(Namespace="http://schemas.dev.office.com/PnP/2015/03/ProvisioningSchema")]
    public partial class ContentTypeBinding {
        
        private string contentTypeIDField;
        
        private bool defaultField;
        
        public ContentTypeBinding() {
            this.defaultField = false;
        }
        
        /// <remarks/>
        [XmlAttribute()]
        public string ContentTypeID {
            get {
                return this.contentTypeIDField;
            }
            set {
                this.contentTypeIDField = value;
            }
        }
        
        /// <remarks/>
        [XmlAttribute()]
        [DefaultValue(false)]
        public bool Default {
            get {
                return this.defaultField;
            }
            set {
                this.defaultField = value;
            }
        }
    }
    
    /// <remarks/>
    [GeneratedCode("xsd", "4.0.30319.18020")]
    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(Namespace="http://schemas.dev.office.com/PnP/2015/03/ProvisioningSchema")]
    public partial class ListInstance {
        
        private ContentTypeBinding[] contentTypeBindingsField;
        
        private ListInstanceViews viewsField;
        
        private ListInstanceFields fieldsField;
        
        private FieldRef[] fieldRefsField;
        
        private string titleField;
        
        private string descriptionField;
        
        private string documentTemplateField;
        
        private bool onQuickLaunchField;
        
        private int templateTypeField;
        
        private string urlField;
        
        private bool enableVersioningField;
        
        private int minorVersionLimitField;
        
        private bool minorVersionLimitFieldSpecified;
        
        private int maxVersionLimitField;
        
        private bool maxVersionLimitFieldSpecified;
        
        private bool removeDefaultContentTypeField;
        
        private bool contentTypesEnabledField;
        
        private bool hiddenField;
        
        public ListInstance() {
            this.onQuickLaunchField = false;
            this.enableVersioningField = false;
            this.removeDefaultContentTypeField = false;
            this.contentTypesEnabledField = false;
            this.hiddenField = false;
        }
        
        /// <remarks/>
        [XmlArrayItem(IsNullable=false)]
        public ContentTypeBinding[] ContentTypeBindings {
            get {
                return this.contentTypeBindingsField;
            }
            set {
                this.contentTypeBindingsField = value;
            }
        }
        
        /// <remarks/>
        public ListInstanceViews Views {
            get {
                return this.viewsField;
            }
            set {
                this.viewsField = value;
            }
        }
        
        /// <remarks/>
        public ListInstanceFields Fields {
            get {
                return this.fieldsField;
            }
            set {
                this.fieldsField = value;
            }
        }
        
        /// <remarks/>
        [XmlArrayItem(IsNullable=false)]
        public FieldRef[] FieldRefs {
            get {
                return this.fieldRefsField;
            }
            set {
                this.fieldRefsField = value;
            }
        }
        
        /// <remarks/>
        [XmlAttribute()]
        public string Title {
            get {
                return this.titleField;
            }
            set {
                this.titleField = value;
            }
        }
        
        /// <remarks/>
        [XmlAttribute()]
        public string Description {
            get {
                return this.descriptionField;
            }
            set {
                this.descriptionField = value;
            }
        }
        
        /// <remarks/>
        [XmlAttribute()]
        public string DocumentTemplate {
            get {
                return this.documentTemplateField;
            }
            set {
                this.documentTemplateField = value;
            }
        }
        
        /// <remarks/>
        [XmlAttribute()]
        [DefaultValue(false)]
        public bool OnQuickLaunch {
            get {
                return this.onQuickLaunchField;
            }
            set {
                this.onQuickLaunchField = value;
            }
        }
        
        /// <remarks/>
        [XmlAttribute()]
        public int TemplateType {
            get {
                return this.templateTypeField;
            }
            set {
                this.templateTypeField = value;
            }
        }
        
        /// <remarks/>
        [XmlAttribute()]
        public string Url {
            get {
                return this.urlField;
            }
            set {
                this.urlField = value;
            }
        }
        
        /// <remarks/>
        [XmlAttribute()]
        [DefaultValue(false)]
        public bool EnableVersioning {
            get {
                return this.enableVersioningField;
            }
            set {
                this.enableVersioningField = value;
            }
        }
        
        /// <remarks/>
        [XmlAttribute()]
        public int MinorVersionLimit {
            get {
                return this.minorVersionLimitField;
            }
            set {
                this.minorVersionLimitField = value;
            }
        }
        
        /// <remarks/>
        [XmlIgnore()]
        public bool MinorVersionLimitSpecified {
            get {
                return this.minorVersionLimitFieldSpecified;
            }
            set {
                this.minorVersionLimitFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [XmlAttribute()]
        public int MaxVersionLimit {
            get {
                return this.maxVersionLimitField;
            }
            set {
                this.maxVersionLimitField = value;
            }
        }
        
        /// <remarks/>
        [XmlIgnore()]
        public bool MaxVersionLimitSpecified {
            get {
                return this.maxVersionLimitFieldSpecified;
            }
            set {
                this.maxVersionLimitFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [XmlAttribute()]
        [DefaultValue(false)]
        public bool RemoveDefaultContentType {
            get {
                return this.removeDefaultContentTypeField;
            }
            set {
                this.removeDefaultContentTypeField = value;
            }
        }
        
        /// <remarks/>
        [XmlAttribute()]
        [DefaultValue(false)]
        public bool ContentTypesEnabled {
            get {
                return this.contentTypesEnabledField;
            }
            set {
                this.contentTypesEnabledField = value;
            }
        }
        
        /// <remarks/>
        [XmlAttribute()]
        [DefaultValue(false)]
        public bool Hidden {
            get {
                return this.hiddenField;
            }
            set {
                this.hiddenField = value;
            }
        }
    }
    
    /// <remarks/>
    [GeneratedCode("xsd", "4.0.30319.18020")]
    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType=true, Namespace="http://schemas.dev.office.com/PnP/2015/03/ProvisioningSchema")]
    public partial class ListInstanceViews {
        
        private XmlElement[] anyField;
        
        /// <remarks/>
        [XmlAnyElement()]
        public XmlElement[] Any {
            get {
                return this.anyField;
            }
            set {
                this.anyField = value;
            }
        }
    }
    
    /// <remarks/>
    [GeneratedCode("xsd", "4.0.30319.18020")]
    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType=true, Namespace="http://schemas.dev.office.com/PnP/2015/03/ProvisioningSchema")]
    public partial class ListInstanceFields {
        
        private XmlElement[] anyField;
        
        /// <remarks/>
        [XmlAnyElement()]
        public XmlElement[] Any {
            get {
                return this.anyField;
            }
            set {
                this.anyField = value;
            }
        }
    }
    
    /// <remarks/>
    [GeneratedCode("xsd", "4.0.30319.18020")]
    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(Namespace="http://schemas.dev.office.com/PnP/2015/03/ProvisioningSchema")]
    public partial class User {
        
        private string nameField;
        
        /// <remarks/>
        [XmlAttribute()]
        public string Name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
    }
    
    /// <remarks/>
    [GeneratedCode("xsd", "4.0.30319.18020")]
    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType=true, Namespace="http://schemas.dev.office.com/PnP/2015/03/ProvisioningSchema")]
    public partial class SharePointProvisioningTemplateSecurity {
        
        private User[] additionalAdministratorsField;
        
        private User[] additionalOwnersField;
        
        private User[] additionalMembersField;
        
        private User[] additionalVisitorsField;
        
        /// <remarks/>
        [XmlArrayItem(IsNullable=false)]
        public User[] AdditionalAdministrators {
            get {
                return this.additionalAdministratorsField;
            }
            set {
                this.additionalAdministratorsField = value;
            }
        }
        
        /// <remarks/>
        [XmlArrayItem(IsNullable=false)]
        public User[] AdditionalOwners {
            get {
                return this.additionalOwnersField;
            }
            set {
                this.additionalOwnersField = value;
            }
        }
        
        /// <remarks/>
        [XmlArrayItem(IsNullable=false)]
        public User[] AdditionalMembers {
            get {
                return this.additionalMembersField;
            }
            set {
                this.additionalMembersField = value;
            }
        }
        
        /// <remarks/>
        [XmlArrayItem(IsNullable=false)]
        public User[] AdditionalVisitors {
            get {
                return this.additionalVisitorsField;
            }
            set {
                this.additionalVisitorsField = value;
            }
        }
    }
    
    /// <remarks/>
    [GeneratedCode("xsd", "4.0.30319.18020")]
    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType=true, Namespace="http://schemas.dev.office.com/PnP/2015/03/ProvisioningSchema")]
    public partial class SharePointProvisioningTemplateSiteFields {
        
        private XmlElement[] anyField;
        
        /// <remarks/>
        [XmlAnyElement()]
        public XmlElement[] Any {
            get {
                return this.anyField;
            }
            set {
                this.anyField = value;
            }
        }
    }
    
    /// <remarks/>
    [GeneratedCode("xsd", "4.0.30319.18020")]
    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType=true, Namespace="http://schemas.dev.office.com/PnP/2015/03/ProvisioningSchema")]
    public partial class SharePointProvisioningTemplateContentTypes {
        
        private XmlElement[] anyField;
        
        /// <remarks/>
        [XmlAnyElement()]
        public XmlElement[] Any {
            get {
                return this.anyField;
            }
            set {
                this.anyField = value;
            }
        }
    }
    
    /// <remarks/>
    [GeneratedCode("xsd", "4.0.30319.18020")]
    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType=true, Namespace="http://schemas.dev.office.com/PnP/2015/03/ProvisioningSchema")]
    public partial class SharePointProvisioningTemplateFeatures {
        
        private Feature[] siteFeaturesField;
        
        private Feature[] webFeaturesField;
        
        /// <remarks/>
        [XmlArrayItem(IsNullable=false)]
        public Feature[] SiteFeatures {
            get {
                return this.siteFeaturesField;
            }
            set {
                this.siteFeaturesField = value;
            }
        }
        
        /// <remarks/>
        [XmlArrayItem(IsNullable=false)]
        public Feature[] WebFeatures {
            get {
                return this.webFeaturesField;
            }
            set {
                this.webFeaturesField = value;
            }
        }
    }
    
    /// <remarks/>
    [GeneratedCode("xsd", "4.0.30319.18020")]
    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType=true, Namespace="http://schemas.dev.office.com/PnP/2015/03/ProvisioningSchema")]
    public partial class SharePointProvisioningTemplateCustomActions {
        
        private CustomAction[] siteCustomActionsField;
        
        private CustomAction[] webCustomActionsField;
        
        /// <remarks/>
        [XmlArrayItem(IsNullable=false)]
        public CustomAction[] SiteCustomActions {
            get {
                return this.siteCustomActionsField;
            }
            set {
                this.siteCustomActionsField = value;
            }
        }
        
        /// <remarks/>
        [XmlArrayItem(IsNullable=false)]
        public CustomAction[] WebCustomActions {
            get {
                return this.webCustomActionsField;
            }
            set {
                this.webCustomActionsField = value;
            }
        }
    }
}