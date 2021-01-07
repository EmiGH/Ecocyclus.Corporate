using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.NT.Entities
{
    public abstract class NotificationRecipient
    {
        #region Internal Properties
        #endregion

        #region External Properties
        public abstract String Email {get;}
        #endregion

        public NotificationRecipient()
        {
        }

    }
}
