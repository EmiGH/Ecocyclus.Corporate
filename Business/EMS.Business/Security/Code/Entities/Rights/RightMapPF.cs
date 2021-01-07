using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Condesus.EMS.Business.Security.Entities
{
    public class RightMapPF : Right
    {
        #region Internal Properties
        private PF.Entities.MapPF _MapPF;
        #endregion

        #region External Properties
        public PF.Entities.MapPF MapPF
        {
            get { return _MapPF; }
        }
        #endregion
        internal RightMapPF(Permission permission, PF.Entities.MapPF mapPF)
            : base(permission)
        {
            _MapPF = mapPF;
        }
    }
}
