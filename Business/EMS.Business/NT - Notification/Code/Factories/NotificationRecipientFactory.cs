using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.NT
{
    internal class NotificationRecipientFactory
    {
        internal NotificationRecipientFactory() { }

        internal Entities.NotificationRecipient CreateNotificationRecipient(String  Email,Int64 IdPerson,Int64 IdOrganization, Int64 IdContacEmail, Credential credential)
        {
            if (IdPerson == 0)
            { return new Entities.NotificationRecipientEmail(Email); }
            else
            { return new Entities.NotificationRecipientPerson(IdPerson, IdOrganization, IdContacEmail, credential); }
        }
    }
}
