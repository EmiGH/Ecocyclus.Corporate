using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PA.Collections.CalculateOfTransformationRead
{
    internal class CalculateOfTransformationByTransformation : ICollectionItems
    {
        private Entities.CalculateOfTransformation _Transformation;

        internal CalculateOfTransformationByTransformation(Entities.CalculateOfTransformation transformation)
        {
            _Transformation = transformation;
        }

        public IEnumerable<System.Data.Common.DbDataRecord> getItems()
        {
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            return _dbPerformanceAssessments.CalculateOfTransformations_ReadByTransformation(_Transformation.IdTransformation, _Transformation.Credential.CurrentLanguage.IdLanguage);
        }
    }
}
