using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PA.Collections.CalculateOfTransformationRead
{
    internal class CalculateOfTransformationByProcess : ICollectionItems
    {
        private PF.Entities.ProcessGroupProcess _ProcessGroupProcess;

        internal CalculateOfTransformationByProcess(PF.Entities.ProcessGroupProcess processGroupProcess)
        {
            _ProcessGroupProcess = processGroupProcess;
        }

        public IEnumerable<System.Data.Common.DbDataRecord> getItems()
        {
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            return _dbPerformanceAssessments.CalculateOfTransformations_ReadByProcess(_ProcessGroupProcess.IdProcess, _ProcessGroupProcess.Credential.CurrentLanguage.IdLanguage);
        }
    }
}
