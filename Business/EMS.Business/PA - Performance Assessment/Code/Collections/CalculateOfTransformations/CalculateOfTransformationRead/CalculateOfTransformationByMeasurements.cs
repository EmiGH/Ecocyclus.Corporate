using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PA.Collections.CalculateOfTransformationRead
{
    internal class CalculateOfTransformationByMeasurements : ICollectionItems
    {
        private Entities.Measurement _Measurement;

        internal CalculateOfTransformationByMeasurements(Entities.Measurement measurement)
        {
            _Measurement = measurement;
        }

        public IEnumerable<System.Data.Common.DbDataRecord> getItems()
        {
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            return _dbPerformanceAssessments.CalculateOfTransformations_ReadByMeasurement(_Measurement.IdMeasurement, _Measurement.Credential.CurrentLanguage.IdLanguage);
        }
    }
}
