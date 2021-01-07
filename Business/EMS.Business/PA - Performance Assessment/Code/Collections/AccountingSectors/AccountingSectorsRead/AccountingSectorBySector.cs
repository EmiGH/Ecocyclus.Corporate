using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PA.Collections.AccountingSectorsRead
{
    internal class AccountingSectorBySector : ICollectionItems
    {
        private Entities.AccountingSector _AccountingSector;

        internal AccountingSectorBySector(Entities.AccountingSector accountingSector)
        {
            _AccountingSector = accountingSector;
        }

        public IEnumerable<System.Data.Common.DbDataRecord> getItems()
        {
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            return _dbPerformanceAssessments.AccountingSectors_ReadBySector(_AccountingSector.IdSector, _AccountingSector.Credential.CurrentLanguage.IdLanguage);
        }
     }
}
