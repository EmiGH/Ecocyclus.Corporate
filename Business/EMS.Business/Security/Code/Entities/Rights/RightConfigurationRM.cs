using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Condesus.EMS.Business.Security.Entities
{  
    public class RightConfigurationRM : Right
    {
        #region Internal Properties
        private RM.Entities.ConfigurationRM _ConfigurationRM;
        #endregion

        #region External Properties
        public RM.Entities.ConfigurationRM ConfigurationRM
        {
            get { return _ConfigurationRM; }
        }
        #endregion
        internal RightConfigurationRM(Permission permission, RM.Entities.ConfigurationRM configurationRM)
            : base(permission)
        {
            _ConfigurationRM = configurationRM;
        }
    }
}
