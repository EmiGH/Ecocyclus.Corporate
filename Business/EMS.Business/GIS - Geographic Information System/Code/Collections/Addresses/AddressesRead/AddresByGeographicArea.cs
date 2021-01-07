using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Condesus.EMS.Business.GIS.Collections.AddressesRead
{
    internal class AddresByGeographicArea : ICollectionItems
    {
        private Entities.GeographicArea _GeographicArea;

        internal AddresByGeographicArea(Entities.GeographicArea geographicArea)
        {
            _GeographicArea = geographicArea;
        }

        public IEnumerable<System.Data.Common.DbDataRecord> getItems()
        {
            DataAccess.GIS.GeographicInformationSystem _dbGeographicInformationSystem = new Condesus.EMS.DataAccess.GIS.GeographicInformationSystem();

            return _dbGeographicInformationSystem.Addresses_ReadByGeographicArea(_GeographicArea.IdGeographicArea);
        }

    }

}
