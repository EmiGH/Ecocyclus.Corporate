using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Condesus.EMS.Business.DS.Collections.TelephoneRead
{
    internal class TelephoneByPerson : ICollectionRead
    {
        private DS.Entities.Person _Person;

        internal TelephoneByPerson(DS.Entities.Person person)
        {
            _Person = person;
        }

        public IEnumerable<System.Data.Common.DbDataRecord> getItems()
        {
            DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

            return _dbDirectoryServices.TelephonePeople_ReadAll(_Person.IdPerson);
        }
        public IEnumerable<System.Data.Common.DbDataRecord> getItem(Int64 IdTelephone)
        {
            DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

            return _dbDirectoryServices.TelephonePeople_ReadById(IdTelephone, _Person.IdPerson);
        }

        internal Entities.Telephone Add(DS.Entities.Person person, String areaCode, String number, String extension, String internationalCode, String reason)
        {
            //Objeto de data layer para acceder a datos
            DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();
            //ejecuta el insert y devuelve el identificador que le asigno en la base de datos
            Int64 _idTelephone = _dbDirectoryServices.Telephones_Create(areaCode, number, extension, internationalCode);
            //alta del lg
            _dbDirectoryServices.TelephonePeople_Create(_idTelephone, person.IdPerson, person.Organization.IdOrganization, reason);
            //crea el objeto 
            Entities.Telephone _Telephone = new Entities.Telephone(_idTelephone, areaCode, number, extension, internationalCode, reason, person.Credential);

            DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
            _dbLog.Create("DS_Telephones", "Telephones", "Add", "IdTelephone=" + _idTelephone, person.Credential.User.IdPerson);

            return _Telephone;

        }

        internal void Modify(Entities.Telephone telephone, DS.Entities.Person person, String areaCode, String number, String extension, String internationalCode, String reason)
        {
            //Objeto de data layer para acceder a datos
            DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();
            //ejecuta el insert y devuelve el identificador que le asigno en la base de datos
            _dbDirectoryServices.Telephones_Update(telephone.IdTelephone, areaCode, number, extension, internationalCode);
            //alta del lg
            _dbDirectoryServices.TelephonePeople_Update(telephone.IdTelephone, person.IdPerson, person.Organization.IdOrganization, reason);

            DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
            _dbLog.Create("DS_Telephones", "Telephones", "Add", "IdTelephone=" + telephone.IdTelephone, person.Credential.User.IdPerson);

        }

    }
}
