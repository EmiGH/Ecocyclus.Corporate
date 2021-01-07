using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Condesus.EMS.Business.Security.Entities
{
    public class RightConfigurationIA : Right
    {
        #region Internal Properties
        private IA.Entities.ConfigurationIA _ConfigurationIA;
        #endregion

        #region External Properties
        public IA.Entities.ConfigurationIA ConfigurationIA
        {
            get { return _ConfigurationIA; }
        }
        #endregion

        internal RightConfigurationIA(Permission permission, IA.Entities.ConfigurationIA configurationIA)
            : base(permission)
        {
            _ConfigurationIA = configurationIA;
        }
    }
}
