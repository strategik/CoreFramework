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

using System;
using System.Security;

namespace Strategik.CoreFramework.Configuration
{
    /// <summary>
    /// Holds Office 365 or SP2013 configuration data
    /// </summary>
    public class STConfiguration
    {
        public bool O365 { get; set; }

        public bool SP2013 { get; set; }

        public String Username { get; set; }

        public String Password { get; set; }

        public Uri ServiceUri { get; set; }

        public Uri LoginUri { get; set; }

        public SecureString GetSecurePasswordString()
        {
            SecureString secureString = new SecureString();

            if (String.IsNullOrEmpty(Password) == false)
            {
                for (int i = 0; i < Password.Length; i++)
                {
                    secureString.AppendChar(Password[i]);
                }
            }

            return secureString;
        }
    }
}