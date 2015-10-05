using System.Collections.Generic;

namespace OfficeDevPnP.Core.Framework.Provisioning.Model
{
    public class DataRow
    {
        #region Private members

        private Dictionary<string, string> _values = new Dictionary<string, string>();

        #endregion Private members

        #region public members

        public Dictionary<string, string> Values
        {
            get { return _values; }
            private set { _values = value; }
        }

        #endregion public members

        #region constructors

        public DataRow()
        {
        }

        public DataRow(Dictionary<string, string> values)
        {
            foreach (var key in values.Keys)
            {
                Values.Add(key, values[key]);
            }
        }

        #endregion constructors
    }
}