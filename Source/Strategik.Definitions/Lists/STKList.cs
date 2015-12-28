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
using Strategik.Definitions.ContentTypes;
using Strategik.Definitions.Events;
using Strategik.Definitions.Fields;
using Strategik.Definitions.Security;
using Strategik.Definitions.Security.Roles;
using Strategik.Definitions.Workflows;
using System;
using System.Collections.Generic;

namespace Strategik.Definitions.Lists
{
    /// <summary>
    /// Defines a list
    /// </summary>
    public class STKList : STKDefinitionBase
    {
        #region Constants

        public const String ID_Field = "ID";
        public const String Title_Field = "Title";
        public const String Author_Field = "Author";
        public const String Editor_Field = "Editor";
        public const String Created_Field = "Created";
        public const String Modified_Field = "Modified";
        public const String ContentType_Field = "ContentType";



        #endregion

        #region static data

        // The fields we need to know for reading list data
        public List<String> _builtInFieldsRead;

        // The fields we need to know for writing list data
        public List<String> _builtInFieldsWrite;

        #endregion


        #region Properties
        public bool AllowContentTypes { get; set; }
        public bool AllowDeletion { get; set; }
        public bool AllowEveryoneViewItems { get; set; }
        public bool AllowFolders { get; set; }
        public bool AllowMultiResponses { get; set; }
        public bool AllowRssFeeds { get; set; }
        public bool BreakRoleInheritence { get; set; }
        public bool CanReceiveEmail { get; set; }
        public List<STKContentType> ContentTypes { get; set; }
        public bool CopyExistingRoles { get; set; }
        public List<STKField> CustomFields { get; set; }
        public STKContentType DefaultContentType { get; set; }
        public String DocumentTemplate { get; set; }
        public int DraftVersionVisibility { get; set; }
        public String EmailAlias { get; set; }
        public bool EmailEnabled { get; set; }
        public bool EnableAttachments { get; set; }
        public bool EnableContentTypes { get; set; }
        public bool EnableEmailNotifications { get; set; }
        public bool EnableFolders { get; set; }
        public bool EnableMinorVersions { get; set; }
        public bool EnableModeration { get; set; }
        public bool EnableVersioning { get; set; }
        public List<STKListItemEventReceiver> EventReceivers { get; set; }
        public List<STKField> Fields { get; set; }
        public List<STKFieldLink> FieldLinks { get; set; }
        public List<STKFolderContentType> Folders { get; set; }
        public bool Hidden { get; set; }
        public List<STKListItem> Items { get; set; }
        public STKListType ListType { get; set; }
        public int MaxVersionLimit { get; set; }
        public bool MinorVersions { get; set; }
        public bool OnQuickLaunch { get; set; }
        public bool RemoveExistingContentTypes{ get; set; }
        public bool RemoveExistingViews { get; set; }
        public bool RequireCheckout { get; set; }
        public bool RequireContentApproval { get; set; }
        public bool RetainDefaultContentType { get; set; }
        public List<STKRoleAssignment> RoleAssignments { get; set; }
        public List<STKField> SiteColumns { get; set; }
        public String Url { get; set; }
        public String LeafUrl { get; set; }
        public List<STKListView> Views { get; set; }
        public List<STKWorkflow> Workflows { get; set; }

        public Guid TemplateFeatureId { get; set; }

        #endregion Properties

        #region Constructors

        public STKList(String listName)
            : this(listName, STKListType.GenericList)
        { }

        public STKList(string listName, STKListType sharePointListType)
        {
            UniqueId = Guid.Empty;
            Name = listName;
            Title = listName;
            Description = listName;
            ListType = STKListType.GenericList;
            ContentTypes = new List<STKContentType>();
            Fields = new List<STKField>();
            FieldLinks = new List<STKFieldLink>();
            SiteColumns = new List<STKField>();
            Views = new List<STKListView>();
            EventReceivers = new List<STKListItemEventReceiver>();
            Workflows = new List<STKWorkflow>();
            OnQuickLaunch = true;
            EnableVersioning = false;
            EnableContentTypes = false;
            EnableEmailNotifications = false;
            RoleAssignments = new List<STKRoleAssignment>();
            this.CustomFields = new List<STKField>();
            Folders = new List<STKFolderContentType>();
            Items = new List<STKListItem>();
            TemplateFeatureId = Guid.Empty;

            _builtInFieldsRead = new List<String>();
            _builtInFieldsRead.Add(ID_Field);
            _builtInFieldsRead.Add(Title_Field);
            _builtInFieldsRead.Add(Author_Field);
            _builtInFieldsRead.Add(Editor_Field);
            _builtInFieldsRead.Add(Created_Field);
            _builtInFieldsRead.Add(Modified_Field);
            _builtInFieldsRead.Add(ContentType_Field);


            _builtInFieldsWrite = new List<String>();
            _builtInFieldsWrite.Add(Title_Field);
        }

        #endregion Constructors

    }
}