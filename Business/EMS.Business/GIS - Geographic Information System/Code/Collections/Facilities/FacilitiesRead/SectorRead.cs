using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Condesus.EMS.Business.GIS.Collections.FacilitiesRead
{
    internal class SectorRead : ICollectionItems
    {
        private Entities.Site _Site;

        internal SectorRead(Entities.Site site)
        {
            _Site = site;
        }

        public IEnumerable<System.Data.Common.DbDataRecord> getItems()
        {
            DataAccess.GIS.GeographicInformationSystem _dbGeographicInformationSystem = new Condesus.EMS.DataAccess.GIS.GeographicInformationSystem();

            return _dbGeographicInformationSystem.Sites_ReadByParent(_Site.IdFacility, _Site.Credential.CurrentLanguage.IdLanguage);
        }

    }
}
