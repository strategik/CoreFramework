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

using Strategik.Definitions.Security.Roles;
using System;

namespace Strategik.Definitions.Security
{
    /// <summary>
    /// Defines a permission set that we want to add
    /// </summary>
    public class STKRole : STKSecurityDefinition
    {
        #region Properties

        public bool IsBuiltInRole { get; set; }

        public String SharePointRoleName { get; set; }

        public STKRole CustomRoleDefinition { get; set; }

        #endregion Properties

        #region Static Helper Properties

        public static STKRole Reader
        {
            get
            {
                return new STKReaderRole();
            }
        }

        public static STKRole Contributor
        {
            get
            {
                return new STKContributorRole();
            }
        }

        public static STKContributorNoDeleteRole ContributorNoDelete
        {
            get
            {
                return new STKContributorNoDeleteRole();
            }
        }

        public static STKRole Designer
        {
            get
            {
                return new STKDesignerRole();
            }
        }

        public static STKRole Administrator
        {
            get
            {
                return new STKAdministratorRole();
            }
        }

        public static STKRole Guest
        {
            get
            {
                return new STKGuestRole();
            }
        }

        #endregion Static Helper Properties

        #region Methods

        #region GetRoleName

        public String GetRoleName()
        {
            return (IsBuiltInRole) ? SharePointRoleName : CustomRoleDefinition.SharePointRoleName;
        }

        #endregion GetRoleName

        #endregion Methods
    }
}