using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.CT.Collections.TopicsRead
{
    internal class TopicsByForum : ICollectionItems
    {
        private Entities.Forum _Forum;

        internal TopicsByForum(Entities.Forum forum)
        {
            _Forum = forum;
        }

        public IEnumerable<System.Data.Common.DbDataRecord> getItems()
        {
            DataAccess.CT.CollaborationTools _dbCollaborationTools = new Condesus.EMS.DataAccess.CT.CollaborationTools();

            return _dbCollaborationTools.ForumTopics_ReadByForum(_Forum.IdForum);
        }
    }
}
