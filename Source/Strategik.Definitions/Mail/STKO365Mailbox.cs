using Strategik.Definitions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategik.Definitions.Mail
{
    /// <summary>
    /// Defines customisations that we make to an Office 365 Mailbox
    /// </summary>
    public class STKO365Mailbox: STKDefinitionBase
    {
        public List<STKMailFolder> CustomFolders { get; set; }

        public STKO365Mailbox()
        {

        }
    }
}
