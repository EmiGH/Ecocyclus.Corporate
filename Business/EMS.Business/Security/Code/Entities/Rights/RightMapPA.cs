using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Condesus.EMS.Business.Security.Entities
{
    public class RightMapPA : Right
    {
        #region Internal Properties
        private PA.Entities.MapPA _MapPA;
      
        #endregion

        #region External Properties
        public PA.Entities.MapPA MapPA
        {
            get { return _MapPA; }
        }
     
        #endregion
        internal RightMapPA(Permission permission, PA.Entities.MapPA mapPA)
            : base(permission)
        {
            _MapPA = mapPA;
        }
    }
}
