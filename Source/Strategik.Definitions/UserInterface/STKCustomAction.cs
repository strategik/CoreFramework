using Strategik.Definitions.Base;
using System;

namespace Strategik.Definitions.UserInterface
{
    public class STKCustomAction : STKDefinitionBase
    {
        #region Properties

        public bool DeleteIfPresent { get; set; }
        public String CommandUIExtension { get; set; }

        public bool Enabled { get; set; }

        public String Group { get; set; }

        public String ImageUrl { get; set; }

        public string Location { get; set; }

        public string RegistrationId { get; set; }

        public STKCustomActionRegistrationType RegistrationType { get; set; }

        public string ScriptBlock { get; set; }

        public string ScriptSource { get; set; }

        public int Sequence { get; set; }

        public string Url { get; set; }

        #endregion Properties
    }
}