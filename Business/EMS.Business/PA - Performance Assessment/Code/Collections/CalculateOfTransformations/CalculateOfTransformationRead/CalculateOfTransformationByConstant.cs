using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PA.Collections.CalculateOfTransformationRead
{
    internal class CalculateOfTransformationByConstant : ICollectionItems
    {
        private Entities.Constant _Constant;

        internal CalculateOfTransformationByConstant(Entities.Constant constant)
        {
            _Constant = constant;
        }

        public IEnumerable<System.Data.Common.DbDataRecord> getItems()
        {
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            return _dbPerformanceAssessments.CalculateOfTransformations_ReadByConstant(_Constant.IdConstant, _Constant.Credential.CurrentLanguage.IdLanguage);
        }
    }
}
