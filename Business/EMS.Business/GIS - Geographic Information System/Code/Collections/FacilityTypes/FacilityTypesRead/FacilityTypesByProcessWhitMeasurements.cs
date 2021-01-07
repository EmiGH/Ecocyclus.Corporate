using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.GIS.Collections.FacilityTypesRead
{
    internal class FacilityTypesByProcessWhitMeasurements : ICollectionItems
    {
        private PF.Entities.ProcessGroupProcess _Process;

        internal FacilityTypesByProcessWhitMeasurements(PF.Entities.ProcessGroupProcess process)
        {
            _Process = process;
        }

        public IEnumerable<System.Data.Common.DbDataRecord> getItems()
        {
            DataAccess.GIS.GeographicInformationSystem _dbGeographicInformationSystem = new Condesus.EMS.DataAccess.GIS.GeographicInformationSystem();

            return _dbGeographicInformationSystem.FacilityTypes_ReadByProcessWhitMeasurements(_Process.IdProcess, _Process.Credential.CurrentLanguage.IdLanguage);
        }
    }
}
