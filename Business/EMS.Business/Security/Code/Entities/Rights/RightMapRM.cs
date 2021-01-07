using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Condesus.EMS.Business.Security.Entities
{
    public class RightMapRM : Right
    {
        #region Internal Properties
        private RM.Entities.MapRM _MapRM;
        #endregion

        #region External Properties
        public RM.Entities.MapRM MapRM
        {
            get { return _MapRM; }
        }
        #endregion
        internal RightMapRM(Permission permission, RM.Entities.MapRM mapRM)
            : base(permission)
        {
            _MapRM = mapRM;
        }
    }
}
