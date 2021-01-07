using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PA.Collections.IndicatorsRead
{
    internal class IndicatorsByIndicatorClassification : ICollectionItems
    {
        private Entities.IndicatorClassification _IndicatorClassification;

        internal IndicatorsByIndicatorClassification(Entities.IndicatorClassification indicatorClassification)
        {
            _IndicatorClassification = indicatorClassification;
        }

        public IEnumerable<System.Data.Common.DbDataRecord> getItems()
        {
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            return _dbPerformanceAssessments.Indicators_ReadByClassification(_IndicatorClassification.IdIndicatorClassification, _IndicatorClassification.Credential.CurrentLanguage.IdLanguage);
        }
    }
}
