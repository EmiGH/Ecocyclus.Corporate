using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Condesus.EMS.Business.Security.Entities
{
    public abstract class Right
    {
        #region Internal Properties
        private Permission _Permission;
        #endregion
        #region External Properties
        public Permission Permission
        {
            get { return _Permission; }
        }
        #endregion

        internal Right(Permission permission) 
        {
            _Permission = permission;
        }
    }
}
