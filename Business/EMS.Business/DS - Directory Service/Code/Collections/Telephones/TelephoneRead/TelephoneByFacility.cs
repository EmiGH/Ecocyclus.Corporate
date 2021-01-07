using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Condesus.EMS.Business.DS.Collections.TelephoneRead
{
    internal class TelephoneByFacility : ICollectionRead
    {
        private GIS.Entities.Site _Facility;

        internal TelephoneByFacility(GIS.Entities.Site site)
        {
            _Facility = site;
        }

        public IEnumerable<System.Data.Common.DbDataRecord> getItems()
        {
            DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

            return _dbDirectoryServices.TelephoneFacilities_ReadAll(_Facility.IdFacility);
        }

        public IEnumerable<System.Data.Common.DbDataRecord> getItem(Int64 IdTelephone)
        {
            DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

            return _dbDirectoryServices.TelephoneFacilities_ReadById(IdTelephone, _Facility.IdFacility);
        }

        internal Entities.Telephone Add(GIS.Entities.Site facility, String areaCode, String number, String extension, String internationalCode, String reason)
        {
            //Objeto de data layer para acceder a datos
            DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();
            //ejecuta el insert y devuelve el identificador que le asigno en la base de datos
            Int64 _idTelephone = _dbDirectoryServices.Telephones_Create(areaCode, number, extension, internationalCode);
            //alta del lg
            _dbDirectoryServices.TelephoneFacilities_Create(_idTelephone, facility.IdFacility, reason);
            //crea el objeto 
            Entities.Telephone _Telephone = new Entities.Telephone(_idTelephone, areaCode, number, extension, internationalCode, reason, facility.Credential);

            DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
            _dbLog.Create("DS_Telephones", "Telephones", "Add", "IdTelephone=" + _idTelephone, facility.Credential.User.IdPerson);

            return _Telephone;

        }

        internal void Modify(Entities.Telephone telephone, GIS.Entities.Site facility, String areaCode, String number, String extension, String internationalCode, String reason)
        {
            //Objeto de data layer para acceder a datos
            DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();
            //ejecuta el insert y devuelve el identificador que le asigno en la base de datos
            _dbDirectoryServices.Telephones_Update(telephone.IdTelephone, areaCode, number, extension, internationalCode);
            //alta del lg
            _dbDirectoryServices.TelephoneFacilities_Update(telephone.IdTelephone, facility.IdFacility, reason);
            //crea el objeto 
            
            DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
            _dbLog.Create("DS_Telephones", "Telephones", "Add", "IdTelephone=" + telephone.IdTelephone, facility.Credential.User.IdPerson);

        }

    }
}
