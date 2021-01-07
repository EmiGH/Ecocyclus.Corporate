using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Condesus.EMS.Business.GIS.Collections.FacilitiesRead
{
    internal class FacilityByDashboard : ICollectionItems
    {
        private DS.Entities.Dashboard _Dashboard;

        internal FacilityByDashboard(DS.Entities.Dashboard dashboard)
        {
            _Dashboard = dashboard;
        }

        public IEnumerable<System.Data.Common.DbDataRecord> getItems()
        {
            DataAccess.GIS.GeographicInformationSystem _dbGeographicInformationSystem = new Condesus.EMS.DataAccess.GIS.GeographicInformationSystem();

            return _dbGeographicInformationSystem.Sites_FacilitiesForDashboard(_Dashboard.Credential.User.IdPerson, _Dashboard.Credential.CurrentLanguage.IdLanguage);
        }

    }
}
