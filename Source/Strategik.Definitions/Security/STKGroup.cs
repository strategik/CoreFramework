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

using Strategik.Definitions.Shared;
using System;
using System.Collections.Generic;

namespace Strategik.Definitions.Security
{
    /// <summary>
    /// Defines a sharepoint group
    /// </summary>
    public class STKGroup
    {
        #region Properties

        public Guid Id { get; private set; }

        public String Owner { get; private set; }

        public String Name { get; private set; }

        public String Description { get; private set; }

        public STKScope Scope { get; private set; }

        public List<STKRoleAssignment> RoleAssigments { get; private set; }

        public List<STKUser> Users { get; private set; }

        #endregion Properties

        #region Constructors

        public STKGroup()
        {
            Id = Guid.NewGuid();
            Name = Id.ToString(); // a unique but meaningless name
            Description = Name + " group description";
            Scope = STKScope.RootWeb;
            RoleAssigments = new List<STKRoleAssignment>();
            Users = new List<STKUser>();
        }

        #endregion Constructors

        #region Structure Methods

        public void SetGroupStructure(Guid id, String name, String owner, String description, STKScope scope)
        {
            if (id != Guid.Empty) Id = id;
            if (!String.IsNullOrEmpty(name)) Name = name;
            if (!String.IsNullOrEmpty(owner)) Owner = owner; // set to null to default to current user
            if (!String.IsNullOrEmpty(description)) Description = description;
            Scope = scope;
        }

        public void AddRoleAssignments(List<STKRoleAssignment> dARoleAssignmentDefintions)
        {
            foreach (STKRoleAssignment dARoleAssignmentDefinition in dARoleAssignmentDefintions)
            {
                RoleAssigments.Add(dARoleAssignmentDefinition);
            }
        }

        #endregion Structure Methods



        #region Static Helper Properties

        public static STKGroup SiteCollectionAdministrators
        {
            get
            {
                return new STKGroup { Name = "Root Site Owners" };
            }
        }

        #endregion Static Helper Properties
    }
}