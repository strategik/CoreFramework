﻿
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

using OfficeDevPnP.Core.Framework.Provisioning.Model;
using Strategik.Definitions.O365.ContentTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategik.Definitions.O365.Fields
{
    public static partial class STKFieldExtensions
    {
        public static Field GeneratePnPTemplate(this STKField field)
        {
            Field fieldTemplate = new Field() 
            {
               SchemaXml = field.GetProvisioningXML()
            };

            return fieldTemplate;
        }
        public static FieldRef GeneratePnPFieldRefTemplate(this STKField field) 
        {
            FieldRef fieldRefTemplate = new FieldRef() 
            {
                DisplayName = field.DisplayName,
                Hidden = field.Hidden,
                Id = field.UniqueId,
                Required = field.Required
            };

            return fieldRefTemplate;
        }
        public static FieldRef GeneratePnPTemplate(this STKFieldLink fieldLink)
        {
            FieldRef fieldRefTemplate = new FieldRef()
            {
                DisplayName = fieldLink.DisplayName,
                Hidden = fieldLink.IsHidden,
                Id = fieldLink.SiteColumnId,
                Required = fieldLink.IsRequired
            };

            return fieldRefTemplate;
        }
    }
}