
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
#endregion

using System;
using System.Collections.Generic;
using Microsoft.SharePoint.Client;
using Strategik.Definitions.Fields;

namespace Strategik.CoreFramework.Helpers
{
    public class STKFieldHelper: STKHelperBase
    {
        #region Constructor

        public STKFieldHelper(ClientContext clientContext)
            : base(clientContext)
        { }

        #endregion Constructor

        #region Utilites

        public static void Validate(List<STKField> fields)
        {
            List<Guid> idChecks = new List<Guid>();

            foreach (STKField field in fields)
            {
                field.Validate();

                if (idChecks.Contains(field.UniqueId))
                {
                    throw new Exception("A duplicate UniqueId " + field.UniqueId + "was detetcted");
                }
                else
                {
                    idChecks.Add(field.UniqueId);
                }
            }
        }

        #endregion
    }
}
