using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Condesus.EMS.Business.GIS.Collections.FacilitiesRead
{
    internal class FacilityByFacilityType : ICollectionItems
    {
        private GIS.Entities.FacilityType _FacilityType;

        internal FacilityByFacilityType(GIS.Entities.FacilityType facilityType)
        {
            _FacilityType = facilityType;
        }

        public IEnumerable<System.Data.Common.DbDataRecord> getItems()
        {
            DataAccess.GIS.GeographicInformationSystem _dbGeographicInformationSystem = new Condesus.EMS.DataAccess.GIS.GeographicInformationSystem();

            return _dbGeographicInformationSystem.Sites_ReadByFacilityType(_FacilityType.IdFacilityType, _FacilityType.Credential.CurrentLanguage.IdLanguage);
        }

    }
}
