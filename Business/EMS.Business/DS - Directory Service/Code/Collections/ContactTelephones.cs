using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.DS.Collections
{
    //internal class ContactTelephones
    //{
    //    #region Internal Properties
    //        private Credential _Credential;
    //        private Entities.Person _Person;
    //        private Entities.Organization _Organization;
    //    #endregion

    //    internal ContactTelephones(Entities.Person person)
    //    {
    //        _Credential = person.Credential;
    //        _Organization = person.Organization;
    //        _Person = person;
    //    }
    //    internal ContactTelephones(Entities.Organization organization)
    //    {
    //        _Organization = organization;
    //        _Credential = organization.Credential;
    //    }

    //    #region Organizations
    //        #region Write Functions
    //            internal Entities.ContactTelephone AddByOrganization(String areaCode, String number, String extension, Entities.Country country, Entities.ContactType contactType)
    //            {
    //                try
    //                {
    //                    //Objeto de data layer para acceder a datos
    //                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

    //                    //ejecuta el insert y devuelve el identificador que le asigno en la base de datos
    //                    Int64 _idContactTelephone = _dbDirectoryServices.ContactTelephones_AddByOrganization(areaCode, number, extension, country.IdCountry, _Organization.IdOrganization, contactType.IdContactType, _Credential.User.Person.IdPerson);
    //                    //Devuelvo el objeto telefono creado
    //                    return new Entities.ContactTelephone(_idContactTelephone, areaCode, number, extension, country.IdCountry, contactType.IdContactType,_Credential);
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
    //            internal void RemoveByOrganization(Entities.ContactTelephone contactTelephone)
    //            {
    //                try
    //                {
    //                    //Objeto de data layer para acceder a datos
    //                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();
             
    //                    //Borrar de la base de datos
    //                    _dbDirectoryServices.ContactTelephones_RemoveByOrganization(contactTelephone.IdContactTelephone, _Organization.IdOrganization, _Credential.User.Person.IdPerson);
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
    //            internal void ModifyByOrganization(Entities.ContactTelephone contactTelephone, String areaCode, String number, String extension, Entities.Country country, Entities.ContactType contactType)
    //            {
    //                try
    //                {
    //                    //Objeto de data layer para acceder a datos
    //                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

    //                    //Modifico los datos de la base
    //                    _dbDirectoryServices.ContactTelephones_Update(contactTelephone.IdContactTelephone, areaCode, number, extension, country.IdCountry, contactType.IdContactType, _Organization.IdOrganization, _Credential.User.Person.IdPerson);
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

    //    #region People
    //        #region Write Functions
    //            internal Entities.ContactTelephone AddByPerson(String areaCode, String number, String extension, Entities.Country country, Entities.ContactType contactType)
    //            {
    //                try
    //                {
    //                    //Objeto de data layer para acceder a datos
    //                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

    //                    //ejecuta el insert y devuelve el identificador que le asigno en la base de datos
    //                    Int64 _idContactTelephone = _dbDirectoryServices.ContactTelephones_AddByPerson(areaCode, number, extension, country.IdCountry, _Person.IdPerson, contactType.IdContactType, _Organization.IdOrganization, _Credential.User.Person.IdPerson);
    //                    //Devuelvo el objeto telefono creado
    //                    return new Entities.ContactTelephone(_idContactTelephone, areaCode, number, extension, country.IdCountry, contactType.IdContactType, _Credential);
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
    //            internal void RemoveByPerson(Entities.ContactTelephone contactTelephone)
    //            {
    //                try
    //                {
    //                    //Objeto de data layer para acceder a datos
    //                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

    //                    //Borrar de la base de datos
    //                    _dbDirectoryServices.ContactTelephones_RemoveByPerson(contactTelephone.IdContactTelephone, _Person.IdPerson, _Organization.IdOrganization, _Credential.User.Person.IdPerson);
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
    //            internal void ModifyByPerson(Entities.ContactTelephone contactTelephone, String areaCode, String number, String extension, Entities.Country country, Entities.ContactType contactType)
    //            {
    //                try
    //                {
    //                    //Objeto de data layer para acceder a datos
    //                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

    //                    //Modifico los datos de la base
    //                    _dbDirectoryServices.ContactTelephones_Update(contactTelephone.IdContactTelephone, areaCode, number, extension, country.IdCountry, contactType.IdContactType, _Organization.IdOrganization, _Credential.User.Person.IdPerson);
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

    //    #region Common Read Functions
    //        internal Entities.ContactTelephone Item(Int64 idContactTelephone)
    //        {
    //            //Objeto de data layer para acceder a datos
    //            DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

    //            Entities.ContactTelephone _item = null;
    //            IEnumerable<System.Data.Common.DbDataRecord> _record;
    //            if (_Person == null)
    //            {
    //                _record = _dbDirectoryServices.ContactTelephones_ReadById(idContactTelephone, _Organization.IdOrganization);
    //            }
    //            else
    //            {
    //                _record = _dbDirectoryServices.ContactTelephones_ReadByIdPerson(idContactTelephone, _Organization.IdOrganization, _Person.IdPerson);
    //            } 
    //            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
    //            {
    //                _item = new Entities.ContactTelephone(Convert.ToInt64(_dbRecord["IdContactTelephone"]),
    //                    Convert.ToString(_dbRecord["AreaCode"]),
    //                    Convert.ToString(_dbRecord["Number"]),
    //                    Convert.ToString(_dbRecord["Extension"]),
    //                    Convert.ToInt64(_dbRecord["IdCountry"]),
    //                    Convert.ToInt64(_dbRecord["IdContactType"])
    //                    ,_Credential);
    //            }
    //            return _item;
    //        }
    //        internal Dictionary<Int64, Entities.ContactTelephone> Items()
    //        {
    //            //Coleccion para devolver los telefonos
    //            Dictionary<Int64, Entities.ContactTelephone> _oItems = new Dictionary<Int64, Entities.ContactTelephone>();

    //            //Objeto de data layer para acceder a datos
    //            DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

    //            IEnumerable<System.Data.Common.DbDataRecord> _record;
    //            if (_Person == null)
    //            {
    //                _record = _dbDirectoryServices.ContactTelephones_GetByOrganization(_Organization.IdOrganization);
    //            }
    //            else
    //            {
    //                _record = _dbDirectoryServices.ContactTelephones_GetByPerson(_Person.IdPerson, _Organization.IdOrganization);
    //            }
    //            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
    //            {
    //                //Declara e instancia un telefono 
    //                Entities.ContactTelephone _oContactTelephone = new Entities.ContactTelephone(
    //                    Convert.ToInt64(_dbRecord["IdContactTelephone"]),
    //                    Convert.ToString(_dbRecord["AreaCode"]),
    //                    Convert.ToString(_dbRecord["Number"]),
    //                    Convert.ToString(_dbRecord["Extension"]),
    //                    Convert.ToInt64(_dbRecord["IdCountry"]),
    //                    Convert.ToInt64(_dbRecord["IdContactType"]),_Credential);

    //                //Lo agrego a la coleccion
    //                _oItems.Add(_oContactTelephone.IdContactTelephone, _oContactTelephone);
    //            }
    //            return _oItems;
    //        }
    //    #endregion

    //}
}
