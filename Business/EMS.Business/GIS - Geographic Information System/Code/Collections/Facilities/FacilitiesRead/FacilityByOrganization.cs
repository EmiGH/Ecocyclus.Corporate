using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Condesus.EMS.Business.GIS.Collections.FacilitiesRead
{
    internal class FacilityByOrganization : ICollectionItems
    {
        private DS.Entities.Organization _Organization;

        internal FacilityByOrganization(DS.Entities.Organization organization)
        {
            _Organization = organization;
        }

        public IEnumerable<System.Data.Common.DbDataRecord> getItems()
        {
            DataAccess.GIS.GeographicInformationSystem _dbGeographicInformationSystem = new Condesus.EMS.DataAccess.GIS.GeographicInformationSystem();

            return _dbGeographicInformationSystem.Sites_ReadByOrganization(_Organization.IdOrganization, _Organization.Credential.CurrentLanguage.IdLanguage);
        }

    }
}
