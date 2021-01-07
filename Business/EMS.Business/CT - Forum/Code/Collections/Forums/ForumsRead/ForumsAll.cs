using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.CT.Collections.ForumsRead
{
     internal class ForumsAll : ICollectionItems
    {
        private Credential _Credential;

        internal ForumsAll(Credential credential)
        {
            _Credential = credential;
        }

        public IEnumerable<System.Data.Common.DbDataRecord> getItems()
        {
            DataAccess.CT.CollaborationTools _dbCollaborationTools = new Condesus.EMS.DataAccess.CT.CollaborationTools();

            return _dbCollaborationTools.ForumForums_ReadAll(_Credential.CurrentLanguage.IdLanguage);
        }
    }
}
