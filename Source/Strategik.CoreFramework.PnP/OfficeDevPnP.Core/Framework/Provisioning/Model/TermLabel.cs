using System;

namespace OfficeDevPnP.Core.Framework.Provisioning.Model
{
    public class TermLabel : IEquatable<TermLabel>
    {
        #region Public Members

        public int Language { get; set; }

        public bool IsDefaultForLanguage { get; set; }

        public string Value { get; set; }

        #endregion Public Members



        #region Comparison code

        public override int GetHashCode()
        {
            return (String.Format("{0}|{1}",
                this.Language,
                this.Value).GetHashCode());
        }

        public override bool Equals(object obj)
        {
            if (!(obj is TermLabel))
            {
                return (false);
            }
            return (Equals((TermLabel)obj));
        }

        public bool Equals(TermLabel other)
        {
            return (this.Language == other.Language &&
                this.Value == other.Value);
        }

        #endregion Comparison code
    }
}