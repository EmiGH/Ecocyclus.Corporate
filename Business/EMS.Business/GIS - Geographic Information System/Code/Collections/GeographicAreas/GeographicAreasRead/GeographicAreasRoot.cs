using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.GIS.Collections.GeographicAreasRead
{
    internal class GeographicAreasRoot : ICollectionItems
    {
        private Credential _Credential;

        internal GeographicAreasRoot(Credential credential)
        {
            _Credential= credential;
        }

        public IEnumerable<System.Data.Common.DbDataRecord> getItems()
        {
            DataAccess.GIS.GeographicInformationSystem _dbGeographicInformationSystem = new Condesus.EMS.DataAccess.GIS.GeographicInformationSystem();

            return _dbGeographicInformationSystem.GeographicAreas_ReadRoot(_Credential.CurrentLanguage.IdLanguage);
        }

    }

}
