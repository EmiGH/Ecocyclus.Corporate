using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;

namespace Condesus.EMS.Business.CT.Collections.MessagesRead
{
    internal class MessagesByTopic : ICollectionItems
    {
        private CT.Entities.Topic _Topic;

        internal MessagesByTopic(CT.Entities.Topic topic)
        {
            _Topic = topic;
        }

        public IEnumerable<System.Data.Common.DbDataRecord> getItems()
        {
            DataAccess.CT.CollaborationTools _dbCollaborationTools = new Condesus.EMS.DataAccess.CT.CollaborationTools();

            return _dbCollaborationTools.ForumMessages_ReadByTopic(_Topic.IdTopic);
        }
    }
}
