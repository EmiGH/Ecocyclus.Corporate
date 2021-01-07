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

namespace Condesus.EMS.Business.GIS.Collections
{
    public class Addresses
    {
        #region Internal properties
        private Credential _Credential;
        private ICollectionItems _Datasource;
        #endregion

        internal Addresses(Credential credential)
        {
            _Credential = credential;
            _Datasource = new AddressesRead.AddressAll(credential);
        }

        internal Addresses(Entities.Site site)
        {
            _Credential = site.Credential;
            _Datasource = new AddressesRead.AddressBySite(site);
        }

        internal Addresses(Entities.GeographicArea geographicArea)
        {
            _Credential = geographicArea.Credential;
            _Datasource = new AddressesRead.AddresByGeographicArea(geographicArea);
        }

        internal Addresses(DS.Entities.Person person)
        {
            _Credential = person.Credential;
            _Datasource = new AddressesRead.AddressByPerson(person);
        }

        #region Read Functions
        /// <summary>
        /// Retorna Addresses
        /// </summary>
        /// <returns></returns>
        internal Dictionary<Int64, Entities.Address> Items()
        {
            Dictionary<Int64, Entities.Address> _items = new Dictionary<Int64, Entities.Address>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _Datasource.getItems();

            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                Entities.Address _Address = new AddressFactory().CreateAddress(Convert.ToInt64(_dbRecord["IdAddress"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdFacility"], 0)), Convert.ToInt64(_dbRecord["IdGeographicArea"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdPerson"], 0)), Convert.ToString(_dbRecord["Coordinate"]), Convert.ToString(_dbRecord["Street"]), Convert.ToString(_dbRecord["Number"]), Convert.ToString(_dbRecord["Floor"]), Convert.ToString(_dbRecord["Department"]), Convert.ToString(_dbRecord["PostCode"]), _Credential);
                _items.Add(_Address.IdAddress, _Address);
            }
            return _items;
        }
        /// <summary>
        /// Retorna AddresPerson
        /// </summary>
        /// <returns></returns>
        internal Dictionary<Int64, Entities.AddressPerson> AddressPerson()
        {
            Dictionary<Int64, Entities.AddressPerson> _items = new Dictionary<Int64, Entities.AddressPerson>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _Datasource.getItems();

            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                Entities.AddressPerson _AddressPerson = new Entities.AddressPerson(Convert.ToInt64(_dbRecord["IdAddress"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdPerson"], 0)), Convert.ToInt64(_dbRecord["IdGeographicArea"]), Convert.ToString(_dbRecord["Coordinate"]), Convert.ToString(_dbRecord["Street"]), Convert.ToString(_dbRecord["Number"]), Convert.ToString(_dbRecord["Floor"]), Convert.ToString(_dbRecord["Department"]), Convert.ToString(_dbRecord["PostCode"]), _Credential);
                _items.Add(_AddressPerson.IdAddress, _AddressPerson);
            }
            return _items;
        }
        /// <summary>
        /// Retorna AddresFacilities
        /// </summary>
        /// <returns></returns>
        internal Dictionary<Int64, Entities.AddressFacility> AddressFacility()
        {
            Dictionary<Int64, Entities.AddressFacility> _items = new Dictionary<Int64, Entities.AddressFacility>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _Datasource.getItems();

            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                Entities.AddressFacility _AddressFacility = new Entities.AddressFacility(Convert.ToInt64(_dbRecord["IdAddress"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdFacility"], 0)), Convert.ToInt64(_dbRecord["IdGeographicArea"]), Convert.ToString(_dbRecord["Coordinate"]), Convert.ToString(_dbRecord["Street"]), Convert.ToString(_dbRecord["Number"]), Convert.ToString(_dbRecord["Floor"]), Convert.ToString(_dbRecord["Department"]), Convert.ToString(_dbRecord["PostCode"]), _Credential);
                _items.Add(_AddressFacility.IdAddress, _AddressFacility);
            }
            return _items;
        }


        /// <summary>
        /// Retorna ForumForums por ID
        /// </summary>
        /// <param name="IdMessage"></param>
        /// <returns></returns>
        internal Entities.Address Item(Int64 IdAddress)
        {
            DataAccess.GIS.GeographicInformationSystem _dbGeographicInformationSystem = new Condesus.EMS.DataAccess.GIS.GeographicInformationSystem();

            Entities.Address _Address = null;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbGeographicInformationSystem.Addresses_ReadById(IdAddress);

            //si no trae nada retorno 0 para que no de error
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _Address = new AddressFactory().CreateAddress(Convert.ToInt64(_dbRecord["IdAddress"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdFacility"], 0)), Convert.ToInt64(_dbRecord["IdGeographicArea"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdPerson"], 0)), Convert.ToString(_dbRecord["Coordinate"]), Convert.ToString(_dbRecord["Street"]), Convert.ToString(_dbRecord["Number"]), Convert.ToString(_dbRecord["Floor"]), Convert.ToString(_dbRecord["Department"]), Convert.ToString(_dbRecord["PostCode"]), _Credential);
            }
            return _Address;
        }

        #endregion


        #region Write Functions
        //Crea AddressFacility
        internal Entities.Address Add(Entities.Site site, Entities.GeographicArea geographicArea, String coordinate, 
            String street, String number, String floor, String department, String postCode)
        {
            //Objeto de data layer para acceder a datos
            DataAccess.GIS.GeographicInformationSystem _dbGeographicInformationSystem = new Condesus.EMS.DataAccess.GIS.GeographicInformationSystem();

            Int64 _idPerson = 0;
            //ejecuta el insert y devuelve el identificador que le asigno en la base de datos
            Int64 _idAddress = _dbGeographicInformationSystem.Addresses_Create(site.IdFacility, geographicArea.IdGeographicArea, _idPerson, coordinate, street , number,floor,department,postCode);
            //crea el objeto 
            Entities.Address _Address = new Entities.AddressFacility(_idAddress, site.IdFacility, geographicArea.IdGeographicArea, coordinate, street,number,floor,department,postCode, _Credential);

            DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
            _dbLog.Create("GIS_Addresses", "Addresses", "Add", "IdAddress=" + _idAddress, _Credential.User.IdPerson);

            return _Address;

        }
        //Crea AddressPerson
        internal Entities.Address Add(DS.Entities.Person person, Entities.GeographicArea geographicArea, String coordinate, 
            String street, String number, String floor, String department, String postCode)
        {
            //Objeto de data layer para acceder a datos
            DataAccess.GIS.GeographicInformationSystem _dbGeographicInformationSystem = new Condesus.EMS.DataAccess.GIS.GeographicInformationSystem();

            Int64 _idFacility = 0;
            //ejecuta el insert y devuelve el identificador que le asigno en la base de datos
            Int64 _idAddress = _dbGeographicInformationSystem.Addresses_Create(_idFacility, geographicArea.IdGeographicArea, person.IdPerson, coordinate, street, number, floor, department, postCode);
            //crea el objeto 
            Entities.Address _Address = new Entities.AddressPerson(_idAddress, person.IdPerson, geographicArea.IdGeographicArea, coordinate, street, number, floor, department, postCode, _Credential);

            DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
            _dbLog.Create("GIS_Addresses", "Addresses", "Add", "IdAddress=" + _idAddress, _Credential.User.IdPerson);

            return _Address;

        }
        internal void Remove(Entities.Address address)
        {
            DataAccess.GIS.GeographicInformationSystem _dbGeographicInformationSystem = new Condesus.EMS.DataAccess.GIS.GeographicInformationSystem();

            //Borra dependencias 
            address.Remove();

            _dbGeographicInformationSystem.Addresses_Delete(address.IdAddress);

            DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
            _dbLog.Create("GIS_Addresses", "Addresses", "Delete", "IdAddress=" + address.IdAddress, _Credential.User.IdPerson);

        }
        internal void Modify(Entities.Address address, Entities.Site site, Entities.GeographicArea geographicArea, String coordinate, 
            String street, String number, String floor, String department, String postCode)
        {
            DataAccess.GIS.GeographicInformationSystem _dbGeographicInformationSystem = new Condesus.EMS.DataAccess.GIS.GeographicInformationSystem();

            Int64 _idPerson = 0;

            _dbGeographicInformationSystem.Addresses_Update(address.IdAddress, site.IdFacility, geographicArea.IdGeographicArea, _idPerson, coordinate, street,number,floor,department,postCode);
            DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
            _dbLog.Create("GIS_Addresses", "Addresses", "Update", "IdAddress=" + address.IdAddress, _Credential.User.IdPerson);

        }

        internal void Modify(Entities.Address address, DS.Entities.Person person, Entities.GeographicArea geographicArea, String coordinate, 
        String street, String number, String floor, String department, String postCode)
        {
            DataAccess.GIS.GeographicInformationSystem _dbGeographicInformationSystem = new Condesus.EMS.DataAccess.GIS.GeographicInformationSystem();
            
            Int64 _idFacility = 0;

            _dbGeographicInformationSystem.Addresses_Update(address.IdAddress, _idFacility, geographicArea.IdGeographicArea, person.IdPerson, coordinate, street, number, floor, department, postCode);
            DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
            _dbLog.Create("GIS_Addresses", "Addresses", "Update", "IdAddress=" + address.IdAddress, _Credential.User.IdPerson);

        }

        #endregion

    }
}
