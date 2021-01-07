using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Condesus.EMS.Business.Security.Entities
{
    public class RightOrganization: Right
    {
        #region Internal Properties
        private DS.Entities.Organization _Organization;
        #endregion

        #region External Properties
        public DS.Entities.Organization Organization
        {
            get { return _Organization; }
        }
        
        #endregion
        internal RightOrganization(Permission permission, DS.Entities.Organization organization)
            : base(permission)
        {
            _Organization = organization;
        }
    }
}
