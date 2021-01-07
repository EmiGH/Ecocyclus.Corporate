using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Condesus.EMS.Business.Security.Entities
{
    public class RightProcess: Right
    {
        #region Internal Properties
        private PF.Entities.ProcessGroupProcess _Process;
        #endregion

        #region External Properties
        public PF.Entities.ProcessGroupProcess Process
        {
            get { return _Process; }
        }
        #endregion
        internal RightProcess(Permission permission, PF.Entities.ProcessGroupProcess Process)
            : base(permission)
        {
            _Process = Process;
        }
    }
}
