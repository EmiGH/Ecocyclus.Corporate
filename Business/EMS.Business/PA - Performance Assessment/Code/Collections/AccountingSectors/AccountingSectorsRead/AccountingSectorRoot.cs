﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PA.Collections.AccountingSectorsRead
{
    internal class AccountingSectorRoot:ICollectionItems 
    {
        private Credential _Credential;

        internal AccountingSectorRoot(Credential credential)
        {
            _Credential = credential;
        }

        public IEnumerable<System.Data.Common.DbDataRecord> getItems()
        {
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            return _dbPerformanceAssessments.AccountingSectors_ReadRoot(_Credential.CurrentLanguage.IdLanguage);
        }
    }
}
