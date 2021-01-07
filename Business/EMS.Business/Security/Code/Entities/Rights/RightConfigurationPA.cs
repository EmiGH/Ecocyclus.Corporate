using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Condesus.EMS.Business.Security.Entities
{
    public class RightConfigurationPA : Right
    {
        #region Internal Properties
        private PA.Entities.ConfigurationPA _ConfigurationPA;
        #endregion

        #region External Properties
        public PA.Entities.ConfigurationPA ConfigurationPA
        {
            get { return _ConfigurationPA; }
        }
        #endregion
        internal RightConfigurationPA(Permission permission, PA.Entities.ConfigurationPA configurationPA)
            : base(permission)
        {
            _ConfigurationPA = configurationPA;
        }
    }
}
