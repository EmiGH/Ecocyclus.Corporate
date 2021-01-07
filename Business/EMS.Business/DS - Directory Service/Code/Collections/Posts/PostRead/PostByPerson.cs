using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Condesus.EMS.Business.DS.Collections.PostRead
{
    
    internal class PostByPerson : ICollectionItems
    {
        private Entities.Person _Person;

        internal PostByPerson(Entities.Person person)
        {
            _Person = person;
        }

        public IEnumerable<System.Data.Common.DbDataRecord> getItems()
        {
            DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

            return _dbDirectoryServices.Posts_ReadAll(_Person.IdPerson, _Person.Organization.IdOrganization);
        }
    }
}
