using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Condesus.EMS.Business;
using Condesus.EMS.DataAccess;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.DS.Collections
{
    internal class Telephones
    {
        #region Internal properties
        private Credential _Credential;
        private ICollectionRead _Datasource;
        #endregion

        internal Telephones(Credential credential) 
        {
            _Credential = credential;
            _Datasource = null;
        }
        internal Telephones(GIS.Entities.Site site)
        {
            _Credential = site.Credential;
            _Datasource = new TelephoneRead.TelephoneByFacility(site);
        }
        internal Telephones(DS.Entities.Person person)
        {
            _Credential = person.Credential;
            _Datasource = new TelephoneRead.TelephoneByPerson(person);
        }

        #region Read Functions
        /// <summary>
        /// Retorna telephones
        /// </summary>
        /// <returns></returns>
        internal Dictionary<Int64, Entities.Telephone> Items()
        {
            Dictionary<Int64, Entities.Telephone> _items = new Dictionary<Int64, Entities.Telephone>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _Datasource.getItems();

            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                Entities.Telephone _Telephone = new Entities.Telephone(Convert.ToInt64(_dbRecord["IdTelephone"]), Convert.ToString(_dbRecord["AreaCode"]), Convert.ToString(_dbRecord["Number"]), Convert.ToString(_dbRecord["Extension"]), Convert.ToString(_dbRecord["InternationalCode"]), Convert.ToString(_dbRecord["Reason"]), _Credential);
                _items.Add(_Telephone.IdTelephone, _Telephone);
            }
            return _items;
        }


        /// <summary>
        /// Retorna TelephoneTelephones por ID
        /// </summary>
        /// <param name="IdMessage"></param>
        /// <returns></returns>
        internal Entities.Telephone Item(Int64 IdTelephone)
        {
            DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

            Entities.Telephone _Telephone = null;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _Datasource.getItem(IdTelephone);

            //si no trae nada retorno 0 para que no de error
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _Telephone = new Entities.Telephone(Convert.ToInt64(_dbRecord["IdTelephone"]), Convert.ToString(_dbRecord["AreaCode"]), Convert.ToString(_dbRecord["Number"]), Convert.ToString(_dbRecord["Extension"]), Convert.ToString(_dbRecord["InternationalCode"]), Convert.ToString(_dbRecord["Reason"]), _Credential);
            }
            return _Telephone;
        }

        

        #endregion


        #region Write Functions
        //Crea TelephoneTelephones
        internal Entities.Telephone Add(GIS.Entities.Site facility, String areaCode, String number, String extension, String internationalCode, String reason)
        {
            return new TelephoneRead.TelephoneByFacility(facility).Add(facility, areaCode, number, extension, internationalCode, reason);
        }
        internal Entities.Telephone Add(DS.Entities.Person person, String areaCode, String number, String extension, String internationalCode, String reason)
        {
            return new TelephoneRead.TelephoneByPerson(person).Add(person, areaCode, number, extension, internationalCode, reason);
        }
        internal void Remove(Entities.Telephone Telephone)
        {
            DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();
            //Borra tablas referenciadas 
            _dbDirectoryServices.TelephoneFacilities_DeleteByTelephone(Telephone.IdTelephone);
            _dbDirectoryServices.TelephonePeople_DeleteByTelephone(Telephone.IdTelephone);

            _dbDirectoryServices.Telephones_Delete(Telephone.IdTelephone);

            DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
            _dbLog.Create("CT_TelephoneTelephones", "Telephones", "Delete", "IdTelephone=" + Telephone.IdTelephone, _Credential.User.IdPerson);

        }
        internal void Modify(Entities.Telephone telephone, GIS.Entities.Site facility, String areaCode, String number, String extension, String internationalCode, String reason)
        {
            new TelephoneRead.TelephoneByFacility(facility).Modify(telephone, facility, areaCode, number, extension, internationalCode, reason);
        }
        internal void Modify(Entities.Telephone telephone, DS.Entities.Person person, String areaCode, String number, String extension, String internationalCode, String reason)
        {
            new TelephoneRead.TelephoneByPerson(person).Modify(telephone, person, areaCode, number, extension, internationalCode, reason);
        }
        #endregion
    }
}
