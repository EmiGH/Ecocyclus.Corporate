using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Condesus.EMS.Business
{
    /// <summary>
    /// esta interface la tienen que implementar los objetos que tienen que notificar
    /// </summary>
 
    public interface INotificationReported
    {
        void ChangeStatusNotification(Int64 IdError);

        List<NT.Entities.NotificationMessage> NotificationMessages { get; }
    }
}
