using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Condesus.EMS.Business.NT.Collections.NotificationRecipientRead
{
    internal class NotificationRecipientByTask : ICollectionItems
    {
        private PF.Entities.ProcessTask _ProcessTask;

        internal NotificationRecipientByTask(PF.Entities.ProcessTask processTask)
        {
            _ProcessTask = processTask;
        }

        public IEnumerable<System.Data.Common.DbDataRecord> getItems()
        {
            DataAccess.NT.Notifications _dbNotifications = new Condesus.EMS.DataAccess.NT.Notifications();

            return _dbNotifications.NotificationRecipients_ReadByProcess(_ProcessTask.IdProcess);
        }

    }

}
