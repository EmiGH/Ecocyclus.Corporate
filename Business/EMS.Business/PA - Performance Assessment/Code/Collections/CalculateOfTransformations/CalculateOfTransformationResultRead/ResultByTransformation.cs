using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PA.Collections.CalculationOfTransformationRead
{
    internal class ResultByTransformation : ICollectionItems
    {
        private Entities.CalculateOfTransformation _TransformationTransformer;
        private Entities.CalculateOfTransformation _Transformation;

        internal ResultByTransformation(Entities.CalculateOfTransformation transformationTransformer, Entities.CalculateOfTransformation transformation)
        {
            _TransformationTransformer=transformationTransformer;
            _Transformation = transformation;
        }

        public IEnumerable<System.Data.Common.DbDataRecord> getItems()
        {
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            return _dbPerformanceAssessments.CalculateOfTransformationTransformationResults_TansformValues(_TransformationTransformer.IdTransformation, _Transformation.IdTransformation);
        }
    }
}
