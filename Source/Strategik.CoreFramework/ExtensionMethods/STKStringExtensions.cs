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

using System.Security;

namespace System
{
    public static class StringExtentions
    {
        #region Secure Password

        /// <summary>
        /// Returns the <see cref="SecureString"/> for use of passwords.
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static SecureString GetSecureString(this string password)
        {
            SecureString securePassWord = new SecureString();
            foreach (char c in password.ToCharArray()) securePassWord.AppendChar(c);

            return securePassWord;
        }

        /// <summary>
        /// Strips the quotes at the begin and end of the string.
        /// </summary>
        /// <param name="s"></param>
        /// <returns>Returns stripped string, without quotes at start and end.</returns>
        public static string StripQuotes(this string s)
        {
            if (s.EndsWith("\"") && s.StartsWith("\""))
            {
                return s.Substring(1, s.Length - 2);
            }
            else
            {
                return s;
            }
        }

        #endregion Secure Password
    }
}