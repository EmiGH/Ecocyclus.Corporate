using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PA.Collections.AccountingScopesRead
{
    internal class AccountingScopesRoot:ICollectionItems
    {
        private Credential _Credential;

        internal AccountingScopesRoot(Credential credential)
        {
            _Credential = credential;
        }

        public IEnumerable<System.Data.Common.DbDataRecord> getItems()
        {
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            return _dbPerformanceAssessments.AccountingScopes_ReadAll(_Credential.CurrentLanguage.IdLanguage);
        }
    }
}
