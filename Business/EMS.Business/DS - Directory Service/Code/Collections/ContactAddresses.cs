using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.DS.Collections
{
    //internal class ContactAddresses
    //{
    //    #region Internal Properties
    //        private Credential _Credential;
    //        private Entities.Person _Person;
    //        private Entities.Organization _Organization;
    //    #endregion

    //    internal ContactAddresses(Entities.Person person)
    //    {
    //        _Credential = person.Credential;
    //        _Organization = person.Organization;
    //        _Person = person;
    //    }
    //    internal ContactAddresses(Entities.Organization organization)
    //    {
    //        _Organization = organization;
    //        _Credential = organization.Credential;
    //    }

    //    #region Organization
    //        #region Write Functions
    //    internal Entities.ContactAddress AddByOrganization(String street, String number, String floor, String apartment, String zipCode, String city, String state, Entities.Country country, Entities.ContactType contactType)
    //            {
    //                try
    //                {
    //                    //Objeto de data layer para acceder a datos
    //                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

    //                    //ejecuta el insert y devuelve el identificador que le asigno en la base de datos
    //                    Int64 _idContactAddress = _dbDirectoryServices.ContactAddresses_AddByOrganization(street, number, floor, apartment, zipCode, city, state, country.IdCountry, _Organization.IdOrganization, contactType.IdContactType, _Credential.User.Person.IdPerson);
    //                    //Devuelvo el objeto direccion creado
    //                    return new Entities.ContactAddress(_idContactAddress, street, number, floor, apartment, zipCode, city, state, country.IdCountry, contactType.IdContactType, _Credential);
    //                }
    //                catch (SqlException ex)
    //                {
    //                    if (ex.Number == Common.Constants.ErrorDataBaseDuplicatedKey)
    //                    {
    //                        throw new Exception(Common.Resources.Errors.DuplicatedDataRecord, ex);
    //                    }
    //                    throw ex;
    //                }
    //            }
    //    internal void RemoveByOrganization(Entities.ContactAddress contactAddress)
    //            {
    //                try
    //                {
    //                    //Objeto de data layer para acceder a datos
    //                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

    //                    //Borrar de la base de datos
    //                    _dbDirectoryServices.ContactAddresses_RemoveByOrganization(contactAddress.IdContactAddress, _Organization.IdOrganization, _Credential.User.Person.IdPerson);
    //                }
    //                catch (SqlException ex)
    //                {
    //                    if (ex.Number == Common.Constants.ErrorDataBaseDeleteReferenceConstraints)
    //                    {
    //                        throw new Exception(Common.Resources.Errors.RemoveDependency, ex);
    //                    }
    //                    throw ex;
    //                }
    //            }
    //    internal void ModifyByOrganization(Entities.ContactAddress contactAddress, String street, String number, String floor, String apartment, String zipCode, String city, String state, Entities.Country country, Entities.ContactType contactType)
    //            {
    //                try
    //                {
    //                    //Objeto de data layer para acceder a datos
    //                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

    //                    //Modifico los datos de la base
    //                    _dbDirectoryServices.ContactAddresses_Update(contactAddress.IdContactAddress, street, number, floor, apartment, zipCode, city, state, country.IdCountry, contactType.IdContactType, _Organization.IdOrganization, _Credential.User.Person.IdPerson);
    //                }
    //                catch (SqlException ex)
    //                {
    //                    if (ex.Number == Common.Constants.ErrorDataBaseDuplicatedKey)
    //                    {
    //                        throw new Exception(Common.Resources.Errors.DuplicatedDataRecord, ex);
    //                    }
    //                    throw ex;
    //                }
    //            }
    //        #endregion
    //    #endregion

    //    #region Person
    //        #region Write Functions
    //    internal Entities.ContactAddress AddByPerson(String street, String number, String floor, String apartment, String zipCode, String city, String state, Entities.Country country, Entities.ContactType contactType)
    //            {
    //                try
    //                {
    //                    //Objeto de data layer para acceder a datos
    //                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

    //                    //ejecuta el insert y devuelve el identificador que le asigno en la base de datos
    //                    Int64 _idContactAddress = _dbDirectoryServices.ContactAddresses_AddByPerson(_Organization.IdOrganization, street, number, floor, apartment, zipCode, city, state, country.IdCountry, _Person.IdPerson, contactType.IdContactType, _Credential.User.Person.IdPerson);
    //                    //Devuelvo el objeto direccion creado
    //                    return new Entities.ContactAddress(_idContactAddress, street, number, floor, apartment, zipCode, city, state, country.IdCountry, contactType.IdContactType, _Credential);
    //                }
    //                catch (SqlException ex)
    //                {
    //                    if (ex.Number == Common.Constants.ErrorDataBaseDuplicatedKey)
    //                    {
    //                        throw new Exception(Common.Resources.Errors.DuplicatedDataRecord, ex);
    //                    }
    //                    throw ex;
    //                }
    //            }
    //    internal void ModifyByPerson(Entities.ContactAddress contactAddress, String street, String number, String floor, String apartment, String zipCode, String city, String state, Entities.Country country, Entities.ContactType contactType)
    //            {
    //                try
    //                {
    //                    //Objeto de data layer para acceder a datos
    //                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

    //                    //Modifico los datos de la base
    //                    _dbDirectoryServices.ContactAddresses_Update(_Person.IdPerson, contactAddress.IdContactAddress, _Organization.IdOrganization, street, number, floor, apartment, zipCode, city, state, country.IdCountry, contactType.IdContactType, _Credential.User.Person.IdPerson);
    //                }
    //                catch (SqlException ex)
    //                {
    //                    if (ex.Number == Common.Constants.ErrorDataBaseDuplicatedKey)
    //                    {
    //                        throw new Exception(Common.Resources.Errors.DuplicatedDataRecord, ex);
    //                    }
    //                    throw ex;
    //                }
    //            }
    //            internal void RemoveByPerson(Entities.ContactAddress contactAddress)
    //            {
    //                try
    //                {
    //                    //Objeto de data layer para acceder a datos
    //                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

    //                    //Borrar de la base de datos
    //                    _dbDirectoryServices.ContactAddresses_RemoveByPerson(contactAddress.IdContactAddress, _Person.IdPerson, _Organization.IdOrganization, _Credential.User.Person.IdPerson);
    //                }
    //                catch (SqlException ex)
    //                {
    //                    if (ex.Number == Common.Constants.ErrorDataBaseDeleteReferenceConstraints)
    //                    {
    //                        throw new Exception(Common.Resources.Errors.RemoveDependency, ex);
    //                    }
    //                    throw ex;
    //                }
    //            }
    //        #endregion
    //    #endregion

    //    #region Common Read Functions
    //        internal Dictionary<Int64, Entities.ContactAddress> Items()
    //        {
    //            //Coleccion para devolver las direcciones
    //            Dictionary<Int64, Entities.ContactAddress> _oItems = new Dictionary<Int64, Entities.ContactAddress>();

    //            //Objeto de data layer para acceder a datos
    //            DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();
                
    //            //Traigo los datos de la base (o por persona o por organizacion)
    //            IEnumerable<System.Data.Common.DbDataRecord> _record;
    //            if (_Person == null)
    //            {
    //                _record = _dbDirectoryServices.ContactAddresses_GetByOrganization(_Organization.IdOrganization);
    //            }
    //            else
    //            {
    //                _record = _dbDirectoryServices.ContactAddresses_GetByPerson(_Person.IdPerson, _Organization.IdOrganization);
    //            }
    //            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
    //            {
    //                //Declara e instancia una direccion 
    //                Entities.ContactAddress _oContactAddress = new Entities.ContactAddress(Convert.ToInt64(_dbRecord["IdContactAddress"]),             
    //                    Convert.ToString(_dbRecord["Street"]),
    //                    Convert.ToString(_dbRecord["Number"]),
    //                    Convert.ToString(_dbRecord["Floor"]),
    //                    Convert.ToString(_dbRecord["Apartment"]),
    //                    Convert.ToString(_dbRecord["ZipCode"]),
    //                    Convert.ToString(_dbRecord["City"]),
    //                    Convert.ToString(_dbRecord["State"]),
    //                    Convert.ToInt64(_dbRecord["IdCountry"]),
    //                    Convert.ToInt64(_dbRecord["IdContactType"]),_Credential);

    //                //Lo agrego a la coleccion
    //                _oItems.Add(_oContactAddress.IdContactAddress, _oContactAddress);
    //            }
    //            return _oItems;
    //        }
    //        internal Entities.ContactAddress Item(Int64 idContactAddress)
    //        {

    //            //Objeto de data layer para acceder a datos
    //            DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();
    //            //DataAccess.DS.ContactAddresses _dbContactAddress = _dbDirectoryServices.ContactAddresses;

               
    //            IEnumerable<System.Data.Common.DbDataRecord> _record;
    //            if (_Person == null)
    //            {
    //                _record = _dbDirectoryServices.ContactAddresses_ReadById(idContactAddress, _Organization.IdOrganization);
    //            }
    //            else
    //            {
    //                _record = _dbDirectoryServices.ContactAddresses_ReadByIdPerson(idContactAddress, _Organization.IdOrganization, _Person.IdPerson);
    //            }

    //            System.Collections.IEnumerator _enum = _record.GetEnumerator();
    //            if (_enum.MoveNext())
    //            {
    //                System.Data.Common.DbDataRecord _dbRecord = (System.Data.Common.DbDataRecord)_enum.Current;

    //               return new Entities.ContactAddress(Convert.ToInt64(_dbRecord["IdContactAddress"]),                            
    //                        Convert.ToString(_dbRecord["Street"]),
    //                        Convert.ToString(_dbRecord["Number"]),
    //                        Convert.ToString(_dbRecord["Floor"]),
    //                        Convert.ToString(_dbRecord["Apartment"]),
    //                        Convert.ToString(_dbRecord["ZipCode"]),
    //                        Convert.ToString(_dbRecord["City"]),
    //                        Convert.ToString(_dbRecord["State"]),
    //                        Convert.ToInt64(_dbRecord["IdCountry"]),
    //                        Convert.ToInt64(_dbRecord["IdContactType"]),_Credential                            
    //                        );
    //            }
                
    //            return null;
    //        }
    //    #endregion
    //}
}
