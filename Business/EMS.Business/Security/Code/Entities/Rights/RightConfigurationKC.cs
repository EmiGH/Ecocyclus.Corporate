using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Condesus.EMS.Business.Security.Entities
{
    public class RightConfigurationKC : Right
    {
        #region Internal Properties
        private KC.Entities.ConfigurationKC _ConfigurationKC;
        #endregion

        #region External Properties
        public KC.Entities.ConfigurationKC ConfigurationKC
        {
            get { return _ConfigurationKC; }
        }
        #endregion
        internal RightConfigurationKC(Permission permission, KC.Entities.ConfigurationKC configurationKC)
            : base(permission)
        {
            _ConfigurationKC = configurationKC;
        }
    }
}
