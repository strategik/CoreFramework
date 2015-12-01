﻿#region License

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

#endregion License


namespace Strategik.Definitions.Events
{
    ///<summary>
    ///OurcopyofSPEventReceiverTypeenumeration
    ///</summary>
    public enum STKEventReceiverType
    {
        ItemAdding,
        ItemUpdating,
        ItemDeleting,
        ItemCheckingIn,
        ItemCheckingOut,
        ItemUncheckingOut,
        ItemAttachmentAdding,
        ItemAttachmentDeleting,
        ItemFileMoving,
        ItemVersionDeleting,
        FieldAdding,
        FieldUpdating,
        FieldDeleting,
        ListAdding,
        ListDeleting,
        SiteDeleting,
        WebDeleting,
        WebMoving,
        WebAdding,
        GroupAdding,
        GroupUpdating,
        GroupDeleting,
        GroupUserAdding,
        GroupUserDeleting,
        RoleDefinitionAdding,
        RoleDefinitionUpdating,
        RoleDefinitionDeleting,
        RoleAssignmentAdding,
        RoleAssignmentDeleting,
        InheritanceBreaking,
        InheritanceResetting,
        ItemAdded,
        ItemUpdated,
        ItemDeleted,
        ItemCheckedIn,
        ItemCheckedOut,
        ItemUncheckedOut,
        ItemAttachmentAdded,
        ItemAttachmentDeleted,
        ItemFileMoved,
        ItemFileConverted,
        ItemVersionDeleted,
        FieldAdded,
        FieldUpdated,
        FieldDeleted,
        ListAdded,
        ListDeleted,
        SiteDeleted,
        WebDeleted,
        WebMoved,
        WebProvisioned,
        WebRestored,
        GroupAdded,
        GroupUpdated,
        GroupDeleted,
        GroupUserAdded,
        GroupUserDeleted,
        RoleDefinitionAdded,
        RoleDefinitionUpdated,
        RoleDefinitionDeleted,
        RoleAssignmentAdded,
        RoleAssignmentDeleted,
        InheritanceBroken,
        InheritanceReset,
        EntityInstanceAdded,
        EntityInstanceUpdated,
        EntityInstanceDeleted,
        AppInstalled,
        AppUpgraded,
        AppUninstalling,
    }
}