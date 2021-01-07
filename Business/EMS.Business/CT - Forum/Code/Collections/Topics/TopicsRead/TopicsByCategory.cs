using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.CT.Collections.TopicsRead
{
    internal class TopicsByCategory : ICollectionItems
    {
        private Entities.Category _Category;

        internal TopicsByCategory(Entities.Category category)
        {
            _Category = category;
        }

        public IEnumerable<System.Data.Common.DbDataRecord> getItems()
        {
            DataAccess.CT.CollaborationTools _dbCollaborationTools = new Condesus.EMS.DataAccess.CT.CollaborationTools();

            return _dbCollaborationTools.ForumTopics_ReadByCategory(_Category.IdCategory);
        }
    }
}
