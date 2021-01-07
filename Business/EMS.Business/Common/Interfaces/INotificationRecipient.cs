using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Condesus.EMS.Business
{
    public interface INotificationRecipient
    {
        #region Notifocation
        
      

        //Retorna todos los destinatarios de email
         List<NT.Entities.NotificationRecipient> NotificationRecipient { get;}
        //Alta de email externo
         NT.Entities.NotificationRecipientEmail NotificationRecipientAdd(String email);
        //Baja de email externo
         void Remove(NT.Entities.NotificationRecipientEmail notificationRecipientEmail);
        //Alta de email de contacto
         NT.Entities.NotificationRecipientPerson NotificationRecipientPersonAdd(DS.Entities.Person person, DS.Entities.ContactEmail contactEmail);
        //Baja de email de contacto
         void Remove(NT.Entities.NotificationRecipientPerson notificationRecipientPerson);
        #endregion

    }
}
