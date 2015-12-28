
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

#endregion License

using Strategik.Definitions.Fields;
using Strategik.Definitions.Libraries;
using Strategik.Definitions.Lists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategik.Definitions.TestModel.Lists
{
    public static class STKTestLists
    {
        #region Constants


        #endregion

        #region Definitions

        /// <summary>
        /// Adds all the SharePoint lists we know about (for testing)
        /// </summary>
        /// <returns></returns>
        public static List<STKList> AllListsAndLibraries()
        {
            List<STKList> lists = new List<STKList>();

            // Out of the box lists
            lists.Add(new STKAnnouncementsList());
            lists.Add(new STKContactList());
            lists.Add(new STKDiscussionForum());
            lists.Add(new STKEventsList());
            lists.Add(new STKIssueTrackingList());
            lists.Add(new STKLinksList());
            lists.Add(new STKTaskList());
            lists.Add(new STKProjectTaskList());

            // Promoted Links - requires special provisioning
            lists.Add(new STKPromotedLinksList());

            // Libraries
            lists.Add(new STKPictureLibrary());
            lists.Add(new STKDocumentLibrary());

            // A Custom list
            STKList customList = new STKList("STKCustomList") 
            {
                Url = "STKCustomList",
            };

            STKChoiceField deadlyChoices = new STKChoiceField()
            {
                UniqueId = new Guid("{3F497377-2978-441F-BF84-F6A140CC0CFC}"),
                Name = "deadlyChoices",
                DisplayName = "Deadly Choices",
                StaticName = "deadlyChoices",
                AddToDefaultView = true
            };

            deadlyChoices.Choices.Add("Sugar");
            deadlyChoices.Choices.Add("Alcohol");
            deadlyChoices.Choices.Add("Pork Pie");

            customList.Fields.Add(deadlyChoices);

            STKListItem item1 = new STKListItem();
            item1.Values.Add(STKList.Title_Field, "A test title");
            item1.Values.Add("deadlyChoices", "Sugar");
            customList.Items.Add(item1);

            lists.Add(customList);

            return lists;
        }

        #endregion
    }
}
