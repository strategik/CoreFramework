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

#endregion License

using Strategik.Definitions.ContentTypes;
using Strategik.Definitions.Lists;
using Strategik.Definitions.Shared;
using System;
using System.Collections.Generic;

namespace Strategik.Definitions.EventHandlers
{
    public class STKListItemEventHandler : STKSharePointEventHandler
    {
        public int ListId { get; set; }

        public string ListName { get; set; }

        public STKList TargetList { get; set; }

        public List<string> ListNames { get; set; }

        public STKContentType ContentType { get; set; }

        public string AssemblyName { get; set; }

        public string ClassName { get; set; }

        public bool AllEvents { get; set; }

        public STKRegistrationType RegistrationType { get; set; }

        //  public List<SPEventReceiverType> EventReceiverTypes { get; set; }
        /// <summary>
        /// Prevents an exception being raised during provisioning if the targeted
        /// list does not exist in the current context.
        /// </summary>
        public bool IgnoreIfListDoesNotExist { get; set; }

        public STKListItemEventHandler()
        {
            AllEvents = true;
            // EventReceiverTypes = new List<SPEventReceiverType>();
            ListNames = new List<string>();
        }

        /// <summary>
        /// Checks if the assembly name has been set
        /// </summary>
        /// <returns>true if the assembly name has been explicitly set.</returns>
        public bool AssemblyNameIsSet()
        {
            return (String.IsNullOrEmpty(AssemblyName) ? false : true);
        }
    }
}