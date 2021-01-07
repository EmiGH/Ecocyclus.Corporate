using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Condesus.EMS.Business.DS.Collections.PostRead
{
    
    internal class PostByJobTitle: ICollectionItems
    {
        private Entities.JobTitle _JobTitle;

        internal PostByJobTitle(Entities.JobTitle jobTitle)
        {
            _JobTitle = jobTitle;
        }

        public IEnumerable<System.Data.Common.DbDataRecord> getItems()
        {
            DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

            return _dbDirectoryServices.Posts_ReadByJobTitle(_JobTitle.IdPosition, _JobTitle.IdGeographicArea, _JobTitle.IdFunctionalArea, _JobTitle.IdOrganization);
        }
    }
}
