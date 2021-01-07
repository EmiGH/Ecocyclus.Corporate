using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PA.Collections.ConstantClassificationsRead
{
    internal class ConstantClassificationRoot : ICollectionItems
    {
        private Entities.ConfigurationPA _ConfigurationPA;

        internal ConstantClassificationRoot(Entities.ConfigurationPA configurationPA)
        {
            _ConfigurationPA = configurationPA;
        }

        public IEnumerable<System.Data.Common.DbDataRecord> getItems()
        {
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            return _dbPerformanceAssessments.ConstantClassifications_ReadRoot(_ConfigurationPA.Credential.CurrentLanguage.IdLanguage);
        }
    }
}
