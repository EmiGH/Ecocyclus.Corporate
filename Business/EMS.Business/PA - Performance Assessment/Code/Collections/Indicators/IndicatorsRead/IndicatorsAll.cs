﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PA.Collections.IndicatorsRead
{
    internal class IndicatorsAll : ICollectionItems
    {
        private Credential _Credential;

        internal IndicatorsAll(Credential credential)
        {
            _Credential = credential;
        }

        public IEnumerable<System.Data.Common.DbDataRecord> getItems()
        {
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            return _dbPerformanceAssessments.Indicators_ReadAll(_Credential.CurrentLanguage.IdLanguage);
        }
    }
}
