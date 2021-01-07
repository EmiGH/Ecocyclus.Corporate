using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Condesus.EMS.Business.Security.Entities
{
    public abstract class RightEntity
    {
        #region Internal Properties
        private Permission _Permission;
        private ISecurity _Entity;
        #endregion
        #region External Properties
        public Permission Permission
        {
            get { return _Permission; }
        }
        public ISecurity Entity
        {
            get { return _Entity; }
        }
        #endregion

        internal RightEntity(ISecurity Entity, Permission permission)
        {
            _Permission = permission;
            _Entity = Entity;
        }
    }
}
