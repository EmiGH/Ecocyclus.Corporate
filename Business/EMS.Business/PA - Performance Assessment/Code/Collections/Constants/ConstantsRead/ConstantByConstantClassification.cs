using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PA.Collections.ConstantsRead
{
    internal class ConstantByConstantClassification:ICollectionItems
    {
        private Entities.ConstantClassification _ConstantClassification;

        internal ConstantByConstantClassification(Entities.ConstantClassification constantClassification)
        {
            _ConstantClassification = constantClassification;
        }

        public IEnumerable<System.Data.Common.DbDataRecord> getItems()
        {
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            return _dbPerformanceAssessments.Constants_ReadByConstantClassification(_ConstantClassification.IdConstantClassification,_ConstantClassification.LanguageOption.Language.IdLanguage);
        }
    }
}
