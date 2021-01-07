using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PA.Collections.AccountingActivitiesRead
{
     internal class AccountingActivityByActivity : ICollectionItems
    {
         private Entities.AccountingActivity _AccountingActivity;

        internal AccountingActivityByActivity(Entities.AccountingActivity accountingActivity)
        {
            _AccountingActivity = accountingActivity;
        }

        public IEnumerable<System.Data.Common.DbDataRecord> getItems()
        {
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            return _dbPerformanceAssessments.AccountingActivities_ReadByActivity(_AccountingActivity.IdActivity, _AccountingActivity.Credential.CurrentLanguage.IdLanguage);
        }
    }
}
