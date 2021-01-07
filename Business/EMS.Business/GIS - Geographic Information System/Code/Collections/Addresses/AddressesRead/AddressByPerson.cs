using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Condesus.EMS.Business.GIS.Collections.AddressesRead
{
    internal class AddressByPerson : ICollectionItems
    {
        private DS.Entities.Person _Person;

        internal AddressByPerson(DS.Entities.Person person)
        {
            _Person = person;
        }

        public IEnumerable<System.Data.Common.DbDataRecord> getItems()
        {
            DataAccess.GIS.GeographicInformationSystem _dbGeographicInformationSystem = new Condesus.EMS.DataAccess.GIS.GeographicInformationSystem();

            return _dbGeographicInformationSystem.Addresses_ReadByPerson(_Person.IdPerson);
        }

    }

}
