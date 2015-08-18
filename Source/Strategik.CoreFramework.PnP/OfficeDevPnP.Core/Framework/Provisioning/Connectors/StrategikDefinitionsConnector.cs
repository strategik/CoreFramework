using Strategik.Definitions.O365.Solutions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeDevPnP.Core.Framework.Provisioning.Connectors
{
    public class StrategikDefinitionsConnector: FileConnectorBase
    {
        #region Data

        private STKO365Solution _solution;

        #endregion

        #region Properties

        public STKO365Solution Solution { get {return _solution;}}

        #endregion

        #region Constructors

        public StrategikDefinitionsConnector() 
            : base()
        {

        }

        public StrategikDefinitionsConnector(STKO365Solution solution)
            : base()
        {
            if (solution == null) throw new ArgumentNullException("Solution not specified");
            if (solution.IsValid() == false) throw new ArgumentException("The specified solution is invalid");

            _solution = solution;
        }

        #endregion

        #region Base class overides

        public override List<string> GetFiles()
        {
            throw new NotImplementedException();
        }

        public override List<string> GetFiles(string container)
        {
            throw new NotImplementedException();
        }

        public override string GetFile(string fileName)
        {
            throw new NotImplementedException();
        }

        public override string GetFile(string fileName, string container)
        {
            throw new NotImplementedException();
        }

        public override System.IO.Stream GetFileStream(string fileName)
        {
            throw new NotImplementedException();
        }

        public override System.IO.Stream GetFileStream(string fileName, string container)
        {
            throw new NotImplementedException();
        }

        public override void SaveFileStream(string fileName, System.IO.Stream stream)
        {
            throw new NotImplementedException();
        }

        public override void SaveFileStream(string fileName, string container, System.IO.Stream stream)
        {
            throw new NotImplementedException();
        }

        public override void DeleteFile(string fileName)
        {
            throw new NotImplementedException();
        }

        public override void DeleteFile(string fileName, string container)
        {
            throw new NotImplementedException();
        }

        public override string GetFilenamePart(string fileName)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Helper Methods



        #endregion

       
    }
}
