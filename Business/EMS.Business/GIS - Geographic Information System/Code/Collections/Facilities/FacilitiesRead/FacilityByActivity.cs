using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Condesus.EMS.Business.GIS.Collections.FacilitiesRead
{
    internal class FacilityByActivity : ICollectionItems
    {
        private PA.Entities.AccountingActivity _AccountingActivity;
        private DS.Entities.Organization _Organization;

        internal FacilityByActivity(PA.Entities.AccountingActivity accountingActivity, DS.Entities.Organization organization)
        {
            _Organization = organization;
            _AccountingActivity = accountingActivity;
        }

        public IEnumerable<System.Data.Common.DbDataRecord> getItems()
        {
            DataAccess.GIS.GeographicInformationSystem _dbGeographicInformationSystem = new Condesus.EMS.DataAccess.GIS.GeographicInformationSystem();

            return _dbGeographicInformationSystem.Sites_ReadByActivityForOrganization(_AccountingActivity.IdActivity, _Organization.IdOrganization, _Organization.Credential.CurrentLanguage.IdLanguage);
        }

    }
}