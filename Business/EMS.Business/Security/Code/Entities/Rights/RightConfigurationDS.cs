using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Condesus.EMS.Business.Security.Entities
{
    public class RightConfigurationDS : Right
    {
        #region Internal Properties
        private DS.Entities.ConfigurationDS _ConfigurationDS;
        #endregion

        #region External Properties
        public DS.Entities.ConfigurationDS ConfigurationDS
        {
            get { return _ConfigurationDS; }
        }
        #endregion
        internal RightConfigurationDS(Permission permission, DS.Entities.ConfigurationDS configurationDS)
            : base(permission)
        {
            _ConfigurationDS = configurationDS;
        }
    }
}
