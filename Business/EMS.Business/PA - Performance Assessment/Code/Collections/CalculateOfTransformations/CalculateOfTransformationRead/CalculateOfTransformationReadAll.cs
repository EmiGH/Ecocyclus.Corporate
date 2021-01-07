using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PA.Collections.CalculateOfTransformationRead
{
    internal class CalculateOfTransformationReadAll : ICollectionItems
    {
        private Transformations _Transformations;

        internal CalculateOfTransformationReadAll(Transformations transformations)
        {
            _Transformations = transformations;
        }

        public IEnumerable<System.Data.Common.DbDataRecord> getItems()
        {
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            return _dbPerformanceAssessments.CalculateOfTransformations_ReadAll(_Transformations.Credential.CurrentLanguage.IdLanguage);
        }
    }
}
