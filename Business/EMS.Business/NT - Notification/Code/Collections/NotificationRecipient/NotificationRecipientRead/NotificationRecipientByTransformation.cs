using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Condesus.EMS.Business.NT.Collections.NotificationRecipientRead
{
    internal class NotificationRecipientByTransformation : ICollectionItems
    {
        private PA.Entities.CalculateOfTransformation _CalculateOfTransformation;

        internal NotificationRecipientByTransformation(PA.Entities.CalculateOfTransformation calculateOfTransformation)
        {
            _CalculateOfTransformation = calculateOfTransformation;
        }

        public IEnumerable<System.Data.Common.DbDataRecord> getItems()
        {
            DataAccess.NT.Notifications _dbNotifications = new Condesus.EMS.DataAccess.NT.Notifications();

            return _dbNotifications.NotificationRecipients_ReadByTransformation(_CalculateOfTransformation.IdTransformation);
        }

    }

}
