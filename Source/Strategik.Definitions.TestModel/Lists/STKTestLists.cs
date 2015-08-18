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
            lists.Add(new STKProjectTaskList());
            lists.Add(new STKProjectTaskList());

            // Libraries
            lists.Add(new STKPictureLibrary());
            lists.Add(new STKDocumentLibrary());

            // A Custom list
            STKList customList = new STKList("STKCustomList") 
            {
                Url = "STKCustomList"
            };

            lists.Add(customList);

            return lists;
        }

        #endregion
    }
}
