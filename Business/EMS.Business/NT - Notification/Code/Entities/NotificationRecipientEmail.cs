using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.NT.Entities
{
    public class NotificationRecipientEmail : NotificationRecipient
    {
        #region Internal Properties
        private String _Email;
        #endregion

        #region External Properties
        public override String Email
        {
            get { return _Email; }
        }
        #endregion

        public NotificationRecipientEmail(String email)
            : base()
        {
            _Email = email;
        }

    }
}
