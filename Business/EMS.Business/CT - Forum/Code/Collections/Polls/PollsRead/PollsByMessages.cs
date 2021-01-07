using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Condesus.EMS.Business.CT.Collections.PollsRead
{
    public class PollsByMessages : ICollectionItems
    {
        private Entities.Message _message;

        internal PollsByMessages(Entities.Message message)
        {
            _message = message;
        }

        public IEnumerable<System.Data.Common.DbDataRecord> getItems()
        {
            DataAccess.CT.CollaborationTools _dbCollaborationTools = new Condesus.EMS.DataAccess.CT.CollaborationTools();

            return _dbCollaborationTools.ForumPolls_ReadByMessage(_message.IdMessage);
        } 
    }
}
