using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Condesus.EMS.Business.Security.Entities
{
    public class RightConfigurationPF : Right
    {
        #region Internal Properties
        private PF.Entities.ConfigurationPF _ConfigurationPF;
        #endregion

        #region External Properties
        public PF.Entities.ConfigurationPF ConfigurationPF
        {
            get { return _ConfigurationPF; }
        }
        #endregion
        internal RightConfigurationPF(Permission permission, PF.Entities.ConfigurationPF configurationPF)
            : base(permission)
        {
            _ConfigurationPF = configurationPF;
        }
    }
}
