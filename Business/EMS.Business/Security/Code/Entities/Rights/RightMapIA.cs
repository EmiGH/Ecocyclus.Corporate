using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Condesus.EMS.Business.Security.Entities
{
    public class RightMapIA : Right
    {
        #region Internal Properties
        private IA.Entities.MapIA _MapIA;
        #endregion

        #region External Properties
        public IA.Entities.MapIA MapIA
        {
            get { return _MapIA; }
        }
      
        #endregion
        internal RightMapIA(Permission permission, IA.Entities.MapIA mapIA)
            : base(permission)
        {
            _MapIA = mapIA;
        }
    }
}
