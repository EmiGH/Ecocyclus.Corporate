using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Condesus.EMS.Business.Security.Entities
{
    public class RightMapDS : Right
    {
        #region Internal Properties
        private DS.Entities.MapDS _MapDS;
        #endregion

        #region External Properties
        public DS.Entities.MapDS MapDS
        {
            get { return _MapDS; }
        }
        #endregion
        internal RightMapDS(Permission permission, DS.Entities.MapDS mapDS)
            : base(permission)
        {
            _MapDS = mapDS;
        }
    }
}
