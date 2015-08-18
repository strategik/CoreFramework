
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Strategik.Definitions.ContentTypes;

namespace OfficeDevPnP.Core.Framework.Provisioning.Model
{
    public static partial class ContentTypeExtensions
    {
        #region Generate strategik definition from corresponding PnP template object

        public static STKContentType GenerateStrategikDefinition(this ContentType contentType)
        {
            STKContentType stkContentType = new STKContentType()
            {
               SharePointContentTypeId = contentType.Id,
               Description = contentType.Description,
               DocumentTemplate =  contentType.DocumentTemplate,
               GroupName =  contentType.Group,
               Hidden =  contentType.Hidden,
               Name = contentType.Name,
               ReadOnly = contentType.ReadOnly,
               Sealed = contentType.Sealed
            };

            foreach (FieldRef fieldRef in contentType.FieldRefs)
            {
                STKFieldLink stkFieldFieldLink = new STKFieldLink()
                {
                    Name = fieldRef.Name,
                    DisplayName = fieldRef.DisplayName,
                    IsHidden = fieldRef.Hidden,
                    IsRequired = fieldRef.Required,
                    SiteColumnId = fieldRef.Id
                };

                stkContentType.SiteColumnLinks.Add(stkFieldFieldLink);
            }
            
            return stkContentType;
        }

        #endregion
    }
}
