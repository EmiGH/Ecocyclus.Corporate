using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Condesus.EMS.Business.GIS.Collections.AddressesRead
{
    
    internal class AddressBySite : ICollectionItems
    {
        private Entities.Site _Site;

        internal AddressBySite(Entities.Site site)
        {
            _Site = site;
        }

        public IEnumerable<System.Data.Common.DbDataRecord> getItems()
        {
            DataAccess.GIS.GeographicInformationSystem _dbGeographicInformationSystem = new Condesus.EMS.DataAccess.GIS.GeographicInformationSystem();

            return _dbGeographicInformationSystem.Addresses_ReadByFacility(_Site.IdFacility);
        }

    }

}
