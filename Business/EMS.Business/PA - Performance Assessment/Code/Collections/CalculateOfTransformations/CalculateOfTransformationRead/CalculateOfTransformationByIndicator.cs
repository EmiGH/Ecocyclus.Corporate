using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PA.Collections.CalculateOfTransformationRead
{
    internal class CalculateOfTransformationByIndicator : ICollectionItems
    {
        private Entities.Indicator _Indicator;

        internal CalculateOfTransformationByIndicator(Entities.Indicator indicator)
        {
            _Indicator = indicator;
        }

        public IEnumerable<System.Data.Common.DbDataRecord> getItems()
        {
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            return _dbPerformanceAssessments.CalculateOfTransformations_ReadByIndicator(_Indicator.IdIndicator, _Indicator.Credential.CurrentLanguage.IdLanguage);
        }
    }
}
