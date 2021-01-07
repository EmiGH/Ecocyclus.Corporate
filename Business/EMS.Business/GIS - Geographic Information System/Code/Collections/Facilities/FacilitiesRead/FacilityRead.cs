using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.GIS.Collections.FacilitiesRead
{
    internal class FacilityRead : ICollectionItems
    {
        private Credential _Credential;

        internal FacilityRead(Credential credential)
        {
            _Credential = credential;
        }

        public IEnumerable<System.Data.Common.DbDataRecord> getItems()
        {
            DataAccess.GIS.GeographicInformationSystem _dbGeographicInformationSystem = new Condesus.EMS.DataAccess.GIS.GeographicInformationSystem();

            return _dbGeographicInformationSystem.Sites_ReadRoot(_Credential.CurrentLanguage.IdLanguage);
        }

    }

}
