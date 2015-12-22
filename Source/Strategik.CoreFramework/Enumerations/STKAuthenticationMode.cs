using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategik.CoreFramework.Enumerations
{
    /// <summary>
    /// Used to let our Authenticatio Helper class know what type of authentication to use
    /// </summary>
    public enum STKAuthenticationMode
    {
        AppOnly, // app only - ACS or Azure AD
        ClientAuthFlow, // user + app context
        OnlineCredentials // username and password
    }
}
