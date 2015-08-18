using Strategik.Definitions.Taxonomy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategik.Definitions.TestModel.Taxonomy
{
    public static class STKTestTaxonomy
    {
        #region Constants

        public static Guid TestTermGroup1_Id = new Guid("{CE1A8C88-FB34-4941-B535-580228A8149A}");
        public static String TestTermGroup1_Name = "Test_TermGroup1";

        public static Guid TestTermGroup2_Id = new Guid("{F0561A08-C398-44FE-88FA-95118A7F640E}");
        public static String TestTermGroup2_Name = "Test_TermGroup2";

        public static Guid TestTermSet1_Id = new Guid("{BFAE5448-7AEB-4659-85BA-A09F9BD992C7}");
        public static String TestTermSet1_Name = "Test_TermSet1";

        public static Guid TestTermSet2_Id = new Guid("{5065AD0C-DB81-4BB5-9830-CC74CE4FD080}");
        public static String TestTermSet2_Name = "Test_TermSet2";

        public static Guid TestTermSet3_Id = new Guid("{69770B9A-A281-4C58-8182-A4A3E5FEE01B}");
        public static String TestTermSet3_Name = "Test_TermSet3";

        public static Guid TestTerm1_Id = new Guid("{B3663C0E-187C-4BF0-A244-A86F8E313451}");
        public static Guid TestTerm2_Id = new Guid("{24BDE259-0EBD-455C-B3EA-07FD7406EC27}");
        public static Guid TestTerm3_Id = new Guid("{D9DED53D-0C19-4273-BA9E-5D38EB16AD6E}");
        public static Guid TestTerm4_Id = new Guid("{E8D32B9B-B9D3-437F-B9E6-39CA3474885E}");
        public static Guid TestTerm5_Id = new Guid("{5B3C6B9D-0816-4E8E-85F4-2FBB3EFC4892}");
        public static Guid TestTerm6_Id = new Guid("{48EC7A74-1006-4834-AE17-51B7FF72D462}");
        public static Guid TestTerm7_Id = new Guid("{5EE6C8CD-A84C-4361-A81E-8CCD306EB012}");
        public static Guid TestTerm8_Id = new Guid("{722413CA-8357-4B8D-8D43-D0A41FFFF666}");
        public static Guid TestTerm9_Id = new Guid("{4981B7DF-4A9C-45D9-8255-E957613A6033}");
        public static Guid TestTerm10_Id = new Guid("{75014797-94B9-4C61-A3F9-4CAB123C6CF0}");
        public static Guid TestTerm11_Id = new Guid("{AC0356EE-7316-4B5A-956F-D04401082901}");
        public static Guid TestTerm12_Id = new Guid("{409CDDEE-DE05-4D63-A5CD-B4B11E0DF39E}");
        public static Guid TestTerm13_Id = new Guid("{2E097E44-67DD-4435-9A17-594BC7E1FE00}");
        public static Guid TestTerm14_Id = new Guid("{8CC1F273-9847-43AD-B0A6-7C6DA10F13C7}");
        public static Guid TestTerm15_Id = new Guid("{8237731A-5A98-474D-9D25-DFB603052B6A}");

        #endregion

        #region Definitions
        public static STKTaxonomy GetTestTaxonomy()
        {
            STKTaxonomy taxonomy = new STKTaxonomy();
            taxonomy.Groups.AddRange(GetTestTermGroups());
            return taxonomy;
        }
        public static List<STKTermGroup> GetTestTermGroups()
        {
            List<STKTermGroup> termGroups = new List<STKTermGroup>();
            STKTermGroup group1 = new STKTermGroup()
            {
                UniqueId = TestTermGroup1_Id,
                Name = TestTermGroup1_Name
            };

            group1.TermSets.AddRange(GetGroup1TermSets());
            termGroups.Add(group1);

            STKTermGroup group2 = new STKTermGroup()
            {
                UniqueId = TestTermGroup2_Id,
                Name = TestTermGroup2_Name
            };

            group2.TermSets.AddRange(GetGroup2TermSets());
            termGroups.Add(group2);

            return termGroups;
        }
        private static List<STKTermSet> GetGroup2TermSets()
        {
            List<STKTermSet> termSets = new List<STKTermSet>();

            STKTermSet termSet = new STKTermSet()
            {
                UniqueId = TestTermSet3_Id,
                Name = TestTermSet3_Name
            };

            termSet.Terms.Add(new STKTerm() { UniqueId = TestTerm1_Id, Name = "Term1", Label = "Term 1" });
            termSet.Terms.Add(new STKTerm() { UniqueId = TestTerm2_Id, Name = "Term2", Label = "Term 2" });
            termSet.Terms.Add(new STKTerm() { UniqueId = TestTerm3_Id, Name = "Term3", Label = "Term 3" });

            termSets.Add(termSet);

            return termSets;
        }
        private static List<STKTermSet> GetGroup1TermSets()
        {
            List<STKTermSet> termSets = new List<STKTermSet>();

            STKTermSet termSet = new STKTermSet()
            {
                UniqueId = TestTermSet1_Id,
                Name = TestTermSet1_Name
            };

            termSet.Terms.Add(new STKTerm() { UniqueId = TestTerm4_Id, Name = "Term4", Label = "Term 4" });
            termSet.Terms.Add(new STKTerm() { UniqueId = TestTerm5_Id, Name = "Term 5", Label = "Term 5" });
            termSet.Terms.Add(new STKTerm() { UniqueId = TestTerm6_Id, Name = "Term 6", Label = "Term 6" });
            termSets.Add(termSet);

            STKTermSet termSet1 = new STKTermSet()
            {
                UniqueId = TestTermSet2_Id,
                Name = TestTermSet2_Name
            };

            termSet1.Terms.Add(new STKTerm() { UniqueId = TestTerm7_Id, Name = "Term7", Label = "Term 7" });
            termSet1.Terms.Add(new STKTerm() { UniqueId = TestTerm8_Id, Name = "Term8", Label = "Term 8" });
            termSet1.Terms.Add(new STKTerm() { UniqueId = TestTerm9_Id, Name = "Term9", Label = "Term 9" });

            // Build some termset hierachy
            STKTerm termWithChildren = new STKTerm() { UniqueId = TestTerm10_Id, Name = "Term10", Label = "Term 10" };
            STKTerm childTerm1 = new STKTerm() { UniqueId = TestTerm11_Id, Name = "Term11", Label = "Term 11" };
            STKTerm childTerm2 = new STKTerm() { UniqueId = TestTerm12_Id, Name = "Term12", Label = "Term 12" };
            childTerm2.Terms.Add(new STKTerm() { UniqueId = TestTerm13_Id, Name = "Term13", Label = "Term 13" });
            childTerm2.Terms.Add(new STKTerm() { UniqueId = TestTerm14_Id, Name = "Term 14", Label = "Term 14" });
            childTerm2.Terms.Add(new STKTerm() { UniqueId = TestTerm15_Id, Name = "Term 15", Label = "Term 15" });
            termWithChildren.Terms.Add(childTerm2);
            termWithChildren.Terms.Add(childTerm1);
            termSet1.Terms.Add(termWithChildren);
            termSets.Add(termSet1);

            return termSets;
        }

        #endregion

    }
}
