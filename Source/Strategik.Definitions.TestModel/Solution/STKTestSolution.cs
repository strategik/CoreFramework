using Strategik.Definitions.Sites;
using Strategik.Definitions.Solutions;
using Strategik.Definitions.Taxonomy;
using Strategik.Definitions.Tenant;
using Strategik.Definitions.ContentTypes;
using Strategik.Definitions.Features;
using Strategik.Definitions.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Strategik.Definitions.TestModel.Taxonomy;
using Strategik.Definitions.TestModel.Sites;

namespace Strategik.Definitions.TestModel.Solutions
{
    /// <summary>
    /// A Demonstration solution that can be used for testing (including our Unit Tests)
    /// </summary>
    /// <remarks>
    /// Illustrates how a solution can be defined in code.
    /// 
    /// A solution is a collection of site collections, sites, taxonomy, lists
    /// libraries, content types (and so on) that need to be bundled up 
    /// and deployed togather to deliver some business functionality.
    /// 
    /// Just like the good old days ...
    /// 
    /// This solution can be deployed to Office 365 (or SharePoint 2013)
    /// using the "best practice" remote provisioning approach via the 
    /// Strategik Core Framework. 
    /// 
    /// Wherever possible we delegate the provisioning to the Office Dev PnP 
    /// provisioning engine.
    /// 
    /// In this scenario we extend the STKSolution class. We could also have
    /// instancitated this class and set its properties - either
    /// approach is valid depending on your particular scenario.
    /// </remarks>
    public static class STKTestSolution
    {
        #region Constants

        public static Guid TestSolutionId = new Guid("{71382191-ACD9-4C31-8AF0-F86591D75B89}");
        
        #endregion Constants

        #region Define Solution

        public static STKSolution GetTestSolution()
        {
            STKSolution solution = new STKSolution();

            solution.UniqueId = TestSolutionId;
            solution.Name = "StrategikTestSolution";
            solution.DisplayName = "Strategik Test Solution";
            solution.MajorVersion = 1;
            solution.MinorVersion = 0;
        
            solution.Taxonomy.Add(STKTestTaxonomy.GetTestTaxonomy());
            solution.Sites.AddRange(STKTestSites.GetTestSites());

            return solution;
        }

        #endregion
    }
}
