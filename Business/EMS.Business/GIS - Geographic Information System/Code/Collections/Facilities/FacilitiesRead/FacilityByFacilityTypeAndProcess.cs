using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Condesus.EMS.Business.GIS.Collections.FacilitiesRead
{
    internal class FacilityByFacilityTypeAndProcess : ICollectionItems
    {
        private GIS.Entities.FacilityType _FacilityType;
        private PF.Entities.ProcessGroupProcess _Process;

        internal FacilityByFacilityTypeAndProcess(GIS.Entities.FacilityType facilityType, PF.Entities.ProcessGroupProcess process)
        {
            _FacilityType = facilityType;
            _Process = process;
        }

        public IEnumerable<System.Data.Common.DbDataRecord> getItems()
        {
            DataAccess.GIS.GeographicInformationSystem _dbGeographicInformationSystem = new Condesus.EMS.DataAccess.GIS.GeographicInformationSystem();

            return _dbGeographicInformationSystem.Sites_ReadByProcessWhitMeasurements(_FacilityType.IdFacilityType, _Process.IdProcess, _FacilityType.Credential.CurrentLanguage.IdLanguage);
        }

    }
}

