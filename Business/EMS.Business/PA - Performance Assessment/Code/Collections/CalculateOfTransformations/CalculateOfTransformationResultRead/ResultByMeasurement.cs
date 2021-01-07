using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PA.Collections.CalculationOfTransformationRead
{
    internal class ResultByMeasurement : ICollectionItems
    {
        private Entities.Measurement _Measurement;
        private Entities.CalculateOfTransformation _Transformation;

        internal ResultByMeasurement(Entities.Measurement measurement, Entities.CalculateOfTransformation transformation)
        {
            _Measurement = measurement;
            _Transformation = transformation;
        }

        public IEnumerable<System.Data.Common.DbDataRecord> getItems()
        {
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            return _dbPerformanceAssessments.CalculateOfTransformationMeasurementResults_TansformValues(_Measurement.IdMeasurement,_Transformation.IdTransformation);
        }
    }
}
