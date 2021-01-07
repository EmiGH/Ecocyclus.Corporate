using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Condesus.EMS.Business.Security.Entities
{
   
    public class RightMapKC : Right
    {
        #region Internal Properties
        private KC.Entities.MapKC _MapKC;
        
        #endregion

        #region External Properties
        public KC.Entities.MapKC MapKC
        {
            get { return _MapKC; }
        }
       
        #endregion
        internal RightMapKC(Permission permission, KC.Entities.MapKC mapKC)
            : base(permission)
        {
            _MapKC = mapKC;
        }
    }
}
