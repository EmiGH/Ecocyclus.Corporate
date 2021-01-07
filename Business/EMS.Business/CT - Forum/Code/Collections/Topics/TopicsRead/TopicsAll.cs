using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.CT.Collections.TopicsRead
{
   internal class TopicsAll : ICollectionItems
    {
        private Credential _Credential;

        internal TopicsAll(Credential credential)
        {
            _Credential = credential;
        }

        public IEnumerable<System.Data.Common.DbDataRecord> getItems()
        {
            DataAccess.CT.CollaborationTools _dbCollaborationTools = new Condesus.EMS.DataAccess.CT.CollaborationTools();

            return _dbCollaborationTools.ForumTopics_ReadAll();
        }
    }
}
