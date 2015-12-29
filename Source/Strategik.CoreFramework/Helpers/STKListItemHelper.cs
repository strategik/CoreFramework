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

using Microsoft.SharePoint.Client;
using Strategik.Definitions.Lists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategik.CoreFramework.Helpers
{
    /// <summary>
    /// Helper class for working with a specific list instance
    /// </summary>
    public class STKListItemHelper: STKHelperBase
    {
        #region Constants

        private const String LogSource = "CoreFramework.STKListItemHelper";
        
        #endregion

        #region Data

        private STKList _list; // The definition of our list
        private List _spList; // The instance of our list

        #endregion

        #region Constructors

        public STKListItemHelper(ClientContext context, STKList list, List spList = null)
            :base(context)
        {
            Initialise(list, spList);
        }

        public STKListItemHelper(STKAuthenticationHelper authHelper, STKList list, List spList = null)
            : base(authHelper)
        {
            Initialise(list, spList);
        }

        public STKListItemHelper(STKList list, List spList = null)
            : base(new STKAuthenticationHelper())
        {
            Initialise(list, spList);
        }

        private void Initialise(STKList list, List spList)
        {
            if (list == null) throw new ArgumentNullException("list");
            _list = list;

            if (spList == null)
            {
                STKListHelper listHelper = new STKListHelper(_clientContext);
                _spList = listHelper.GetSharePointList(_list);
            }
            else
            {
                _list = list;
            }
        }

        #endregion

        #region Methods

        public ListItem GetListItem(int id)
        {
            ListItem item = _spList.GetItemById(id);
            _clientContext.ExecuteQueryRetry();
            return item;
        }

        #endregion
    }
}
