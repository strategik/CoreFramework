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

namespace Strategik.Definitions.EventHandlers
{
    /// <summary>
    /// Our copy of SPEventReceiverType enumeration
    /// </summary>
    public enum STKEventReceiverType
    {
        // Summary:
        //     Invalid.
        InvalidReceiver = -1,

        //
        // Summary:
        //     An item is being added.
        ItemAdding = 1,

        //
        // Summary:
        //     An item is being updated.
        ItemUpdating = 2,

        //
        // Summary:
        //     An item is being deleted.
        ItemDeleting = 3,

        //
        // Summary:
        //     An item is being checked in.
        ItemCheckingIn = 4,

        //
        // Summary:
        //     An item is being checked out.
        ItemCheckingOut = 5,

        //
        // Summary:
        //     An item is being unchecked out.
        ItemUncheckingOut = 6,

        //
        // Summary:
        //     An attachment is being added to the item.
        ItemAttachmentAdding = 7,

        //
        // Summary:
        //     An attachment is being removed from the item.
        ItemAttachmentDeleting = 8,

        //
        // Summary:
        //     A file is being moved.
        ItemFileMoving = 9,

        //
        // Summary:
        //     A field is being added.
        FieldAdding = 101,

        //
        // Summary:
        //     A field is being updated.
        FieldUpdating = 102,

        //
        // Summary:
        //     A field is being removed.
        FieldDeleting = 103,

        ListAdding = 104,
        ListDeleting = 105,

        //
        // Summary:
        //     A site collection is being deleted.
        SiteDeleting = 201,

        //
        // Summary:
        //     A site is being deleted.
        WebDeleting = 202,

        //
        // Summary:
        //     A site is being moved.
        WebMoving = 203,

        WebAdding = 204,
        WorkflowStarting = 501,

        //
        // Summary:
        //     An item was added.
        ItemAdded = 10001,

        //
        // Summary:
        //     An item was updated.
        ItemUpdated = 10002,

        //
        // Summary:
        //     An item was deleted.
        ItemDeleted = 10003,

        //
        // Summary:
        //     An item was checked in.
        ItemCheckedIn = 10004,

        //
        // Summary:
        //     An item was checked out.
        ItemCheckedOut = 10005,

        //
        // Summary:
        //     An item was unchecked out.
        ItemUncheckedOut = 10006,

        //
        // Summary:
        //     An attachment was added to the item.
        ItemAttachmentAdded = 10007,

        //
        // Summary:
        //     An attachment was removed from the item.
        ItemAttachmentDeleted = 10008,

        //
        // Summary:
        //     A file was moved.
        ItemFileMoved = 10009,

        //
        // Summary:
        //     A file was converted.
        ItemFileConverted = 10010,

        //
        // Summary:
        //     A field was added.
        FieldAdded = 10101,

        //
        // Summary:
        //     A field was updated.
        FieldUpdated = 10102,

        //
        // Summary:
        //     A field was removed.
        FieldDeleted = 10103,

        ListAdded = 10104,
        ListDeleted = 10105,

        //
        // Summary:
        //     A site collection was deleted.
        SiteDeleted = 10201,

        //
        // Summary:
        //     A site was deleted.
        WebDeleted = 10202,

        //
        // Summary:
        //     A site was moved.
        WebMoved = 10203,

        WebProvisioned = 10204,
        WorkflowStarted = 10501,
        WorkflowPostponed = 10502,
        WorkflowCompleted = 10503,

        //
        // Summary:
        //     The list received an e-mail message.
        EmailReceived = 20000,

        //
        // Summary:
        //     The list received a context event.
        ContextEvent = 32766,
    }
}