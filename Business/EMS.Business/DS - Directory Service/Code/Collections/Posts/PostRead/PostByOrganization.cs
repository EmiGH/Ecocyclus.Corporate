using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Condesus.EMS.Business.DS.Collections.PostRead
{
    
    internal class PostByOrganization : ICollectionItems
    {
        private Entities.Organization _Organization;

        internal PostByOrganization(Entities.Organization organization)
        {
            _Organization = organization;
        }

        public IEnumerable<System.Data.Common.DbDataRecord> getItems()
        {
            DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

            return _dbDirectoryServices.Posts_ReadAll(_Organization.IdOrganization);
        }
    }
}
