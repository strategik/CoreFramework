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

namespace Strategik.Definitions.Features
{
    /// <summary>
    /// A sandbox solution that we want to unstall in our models
    /// </summary>
    public class STKSandboxSolution
    {
        #region Properties

        public bool Activate { get; set; }

        public Guid SolutionId { get; set; }

        public String FileName { get; set; }

        public String Location { get; set; }

        public int MajorVersion { get; set; }

        public int MinorVersion { get; set; }

        public STKSolutionLocationType LocationType { get; set; }

        public string Path { get; set; }

        #endregion Properties

        #region Constructor

        public STKSandboxSolution()
        {
            MajorVersion = 1;
            MinorVersion = 0;
            LocationType = STKSolutionLocationType.Disk;
        }

        #endregion Constructor
    }

    public enum STKSolutionLocationType
    {
        Disk,
        Assembly,
        Url
    }
}