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

using Strategik.Definitions.Sites;
using System;

namespace Strategik.Definitions.Solutions
{
    /// <summary>
    /// Methods to extnd the functionality of our solution definition class
    /// </summary>
    public static partial class STKSolutionExtensions
    {
        #region Validation

        public static bool IsValid(this STKSolution solution)
        {
            try
            {
                Validate(solution);
                return true;
            }
            catch 
            {
                return false;
            }
        }


        /// <summary>
        /// Validates a Strategik solution
        /// </summary>
        /// <param name="solution"></param>
        public static void Validate(this STKSolution solution)
        {
            if (solution == null) throw new ArgumentNullException("solution");
            if (solution.UniqueId == Guid.Empty) throw new Exception("Solution Id is empty " + solution.Name);
            if (String.IsNullOrEmpty(solution.Name)) throw new Exception("Solution Name is empty " + solution.UniqueId);

            // TODO: Complete solution validation
            foreach(STKSite site in solution.Sites)
            {
                site.Validate();
            }
        }

        #endregion Validation

        #region Tags

        public static String GenerateTag(this STKSolution solution)
        {
            String tag = String.Format("{0} - {1}:{2}. Id = {3}. Tagged at {4}",
                solution.Name,
                solution.MajorVersion,
                solution.MinorVersion,
                solution.UniqueId,
                DateTime.Now
                );

            return tag;
        }

        #endregion Tags

        #region Has? methods

        public static bool HasTennantCustomisations(this STKSolution solution) 
        {
            return (solution.TennantCustomisations == null) ? false : true;
        }

        public static bool HasTaxonomy(this STKSolution solution) 
        {
            return (solution.Taxonomy == null) ? false : true;
        }

        #endregion
    }
}