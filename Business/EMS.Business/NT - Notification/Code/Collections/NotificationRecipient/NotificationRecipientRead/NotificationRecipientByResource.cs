using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Condesus.EMS.Business.NT.Collections.NotificationRecipientRead
{
    internal class NotificationRecipientByResource : ICollectionItems
    {
        private KC.Entities.Version _Version;

        internal NotificationRecipientByResource(KC.Entities.Version version)
        {
            _Version = version;
        }

        public IEnumerable<System.Data.Common.DbDataRecord> getItems()
        {
            DataAccess.NT.Notifications _dbNotifications = new Condesus.EMS.DataAccess.NT.Notifications();

            return _dbNotifications.NotificationRecipients_ReadByProcess(_Version.IdResourceFile);
        }

    }

}
