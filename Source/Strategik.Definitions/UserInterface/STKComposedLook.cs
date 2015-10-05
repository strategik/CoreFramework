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

#endregion License

namespace Strategik.Definitions.UserInterface
{
    public class STKComposedLook
    {
        #region Properties

        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the ColorFile
        /// </summary>
        public string ColorFile { get; set; }

        /// <summary>
        /// Gets or sets the FontFile
        /// </summary>
        public string FontFile { get; set; }

        /// <summary>
        /// Gets or sets the Background Image
        /// </summary>
        public string BackgroundFile { get; set; }

        /// <summary>
        /// Gets or sets the MasterPage for the Composed Look
        /// </summary>
        public string MasterPage { get; set; }

        /// <summary>
        /// Gets or sets the Site Logo
        /// </summary>
        public string SiteLogo { get; set; }

        /// <summary>
        /// Gets or sets the AlternateCSS
        /// </summary>
        public string AlternateCSS { get; set; }

        /// <summary>
        /// Gets or sets the Version of the ComposedLook.
        /// </summary>
        public int Version { get; set; }

        #endregion Properties
    }
}