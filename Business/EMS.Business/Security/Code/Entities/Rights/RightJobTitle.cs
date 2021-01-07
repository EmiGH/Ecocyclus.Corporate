using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Condesus.EMS.Business.Security.Entities
{
    public class RightJobTitle : Right
    {
        #region Internal Properties
        private DS.Entities.JobTitle _JobTitle;
        #endregion

        #region External Properties
        public DS.Entities.JobTitle JobTitle
        {
            get { return _JobTitle; }
        }
        #endregion
        internal RightJobTitle(Permission permission, DS.Entities.JobTitle jobTitle)
            : base(permission)
        {
            _JobTitle = jobTitle;
        }
    }
}
