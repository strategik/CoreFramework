using Strategik.Definitions.Security.Principals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategik.Definitions.Security.Site
{
    public class STKSiteSecurity
    {
        #region

        private List<STKUser> _additionalAdministrators = new List<STKUser>();
        private List<STKUser> _additionalOwners = new List<STKUser>();
        private List<STKUser> _additionalMembers = new List<STKUser>();
        private List<STKUser> _additionalVisitors = new List<STKUser>();
        private List<STKGroup> _siteGroups = new List<STKGroup>();
        private STKSiteSecurityPermission _permissions = new STKSiteSecurityPermission();

        #endregion

        #region Public Members

        /// <summary>
        /// A Collection of users that are associated as site collection adminsitrators
        /// </summary>
        public List<STKUser> AdditionalAdministrators
        {
            get { return _additionalAdministrators; }
            private set { _additionalAdministrators = value; }
        }

        /// <summary>
        /// A Collection of users that are associated to the sites owners group
        /// </summary>
        public List<STKUser> AdditionalOwners
        {
            get { return _additionalOwners; }
            private set { _additionalOwners = value; }
        }

        /// <summary>
        /// A Collection of users that are associated to the sites members group
        /// </summary>
        public List<STKUser> AdditionalMembers
        {
            get { return _additionalMembers; }
            private set { _additionalMembers = value; }
        }

        /// <summary>
        /// A Collection of users taht are associated to the sites visitors group
        /// </summary>
        public List<STKUser> AdditionalVisitors
        {
            get { return _additionalVisitors; }
            private set { _additionalVisitors = value; }
        }

        /// <summary>
        /// List of additional Groups for the Site
        /// </summary>
        public List<STKGroup> SiteGroups
        {
            get { return _siteGroups; }
            private set { _siteGroups = value; }
        }

        /// <summary>
        /// List of Site Security Permissions for the Site
        /// </summary>
        public STKSiteSecurityPermission SiteSecurityPermissions
        {
            get { return _permissions; }
            private set { _permissions = value; }
        }

        #endregion
    }
}
