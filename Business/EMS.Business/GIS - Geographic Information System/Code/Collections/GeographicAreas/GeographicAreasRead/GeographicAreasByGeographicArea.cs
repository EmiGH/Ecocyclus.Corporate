using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.GIS.Collections.GeographicAreasRead
{
    internal class GeographicAreasByGeographicArea : ICollectionItems
    {
        private Entities.GeographicArea _GeographicArea; 

        internal GeographicAreasByGeographicArea(Entities.GeographicArea geographicArea)
        {
            _GeographicArea = geographicArea;
        }

        public IEnumerable<System.Data.Common.DbDataRecord> getItems()
        {
            DataAccess.GIS.GeographicInformationSystem _dbGeographicInformationSystem = new Condesus.EMS.DataAccess.GIS.GeographicInformationSystem();

            return _dbGeographicInformationSystem.GeographicAreas_ReadByParent(_GeographicArea.IdGeographicArea, _GeographicArea.Credential.CurrentLanguage.IdLanguage);
        }

    }
}
