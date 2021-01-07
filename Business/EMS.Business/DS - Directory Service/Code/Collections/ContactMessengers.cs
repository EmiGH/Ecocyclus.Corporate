using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.DS.Collections
{
    internal class ContactMessengers
    {
        #region Internal Properties
            private Credential _Credential;
            private Entities.Person _Person;
            private Entities.Organization _Organization;
        #endregion

        internal ContactMessengers(Entities.Person person)
        {
            _Credential = person.Credential;
            _Organization = person.Organization;
            _Person = person;
        }
        internal ContactMessengers(Entities.Organization organization)
        {
            _Organization = organization;
            _Credential = organization.Credential;
        }

        #region Organizations
            #region Write Functions
        internal Entities.ContactMessenger AddByOrganization(String provider, String application, String data, Entities.ContactType contactType)
                {
                    try
                    {
                        //Objeto de data layer para acceder a datos
                        DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                        //ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                        Int64 _idContactMessenger = _dbDirectoryServices.ContactMessengers_AddByOrganization(provider, application, data, _Organization.IdOrganization, contactType.IdContactType, _Credential.User.Person.IdPerson);
                        //Devuelvo el objeto email creado
                        return new Entities.ContactMessenger(_idContactMessenger, provider, application, data, contactType.IdContactType,_Credential);
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Number == Common.Constants.ErrorDataBaseDuplicatedKey)
                        {
                            throw new Exception(Common.Resources.Errors.DuplicatedDataRecord, ex);
                        }
                        throw ex;
                    }
                }
        internal void RemoveByOrganization(Entities.ContactMessenger contactMessenger)
                {
                    try
                    {
                        //Objeto de data layer para acceder a datos
                        DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                        //Borrar de la base de datos
                        _dbDirectoryServices.ContactMessengers_RemoveByOrganization(contactMessenger.IdContactMessenger, _Organization.IdOrganization, _Credential.User.Person.IdPerson);
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Number == Common.Constants.ErrorDataBaseDeleteReferenceConstraints)
                        {
                            throw new Exception(Common.Resources.Errors.RemoveDependency, ex);
                        }
                        throw ex;
                    }
                }
                internal void ModifyByOrganization(Entities.ContactMessenger contactMessenger, String provider, String application, String data, Entities.ContactType contactType)
                {
                    try
                    {
                        //Objeto de data layer para acceder a datos
                        DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                        //Modifico los datos de la base
                        _dbDirectoryServices.ContactMessengers_Update(contactMessenger.IdContactMessenger, provider, application, data, contactType.IdContactType, _Organization.IdOrganization, _Credential.User.Person.IdPerson);
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Number == Common.Constants.ErrorDataBaseDuplicatedKey)
                        {
                            throw new Exception(Common.Resources.Errors.DuplicatedDataRecord, ex);
                        }
                        throw ex;
                    }
                }
            #endregion
        #endregion

        #region People
            #region Write Functions
                internal Entities.ContactMessenger AddByPerson(String provider, String application, String data, Entities.ContactType contactType)
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();
                    try
                    {
                        //ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                        Int64 _idContactMessenger = _dbDirectoryServices.ContactMessengers_AddByPerson(provider, application, data, _Person.IdPerson, contactType.IdContactType, _Organization.IdOrganization, _Credential.User.Person.IdPerson);
                        //Devuelvo el objeto email creado
                        return new Entities.ContactMessenger(_idContactMessenger, provider, application, data, contactType.IdContactType, _Credential);
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Number == Common.Constants.ErrorDataBaseDuplicatedKey)
                        {
                            throw new Exception(Common.Resources.Errors.DuplicatedDataRecord, ex);
                        }
                        throw ex;
                    }
                }
                internal void RemoveByPerson(Entities.ContactMessenger contactMessenger)
                {
                    try
                    {
                        //Objeto de data layer para acceder a datos
                        DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                        //Borrar de la base de datos
                        _dbDirectoryServices.ContactMessengers_RemoveByPerson(contactMessenger.IdContactMessenger, _Person.IdPerson, _Organization.IdOrganization, _Credential.User.Person.IdPerson);
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Number == Common.Constants.ErrorDataBaseDeleteReferenceConstraints)
                        {
                            throw new Exception(Common.Resources.Errors.RemoveDependency, ex);
                        }
                        throw ex;
                    }
                }
                internal void ModifyByPerson(Entities.ContactMessenger contactMessenger, String provider, String application, String data, Entities.ContactType contactType)
                {
                    try
                    {
                        //Objeto de data layer para acceder a datos
                        DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                        //Modifico los datos de la base
                        _dbDirectoryServices.ContactMessengers_Update(_Person.IdPerson, contactMessenger.IdContactMessenger, provider, application, data, contactType.IdContactType, _Organization.IdOrganization, _Credential.User.Person.IdPerson);
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Number == Common.Constants.ErrorDataBaseDuplicatedKey)
                        {
                            throw new Exception(Common.Resources.Errors.DuplicatedDataRecord, ex);
                        }
                        throw ex;
                    }
                }
            #endregion
        #endregion

        #region Common Read Functions
            internal Dictionary<Int64, Entities.ContactMessenger> Items()
            {
                //Coleccion para devolver los emails
                Dictionary<Int64, Entities.ContactMessenger> _oItems = new Dictionary<Int64, Entities.ContactMessenger>();

                //Objeto de data layer para acceder a datos
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                IEnumerable<System.Data.Common.DbDataRecord> _record;
                if (_Person == null)
                {
                    _record = _dbDirectoryServices.ContactMessengers_GetByOrganization(_Organization.IdOrganization);
                }
                else
                {
                    _record = _dbDirectoryServices.ContactMessengers_GetByPerson(_Person.IdPerson, _Organization.IdOrganization);
                }
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    //Declara e instancia un mensajero
                    Entities.ContactMessenger _oContactMessenger = new Entities.ContactMessenger(Convert.ToInt64(_dbRecord["IdContactMessenger"]),
                        Convert.ToString(_dbRecord["Provider"]),
                        Convert.ToString(_dbRecord["Application"]),
                        Convert.ToString(_dbRecord["Data"]),
                        Convert.ToInt64(_dbRecord["IdContactType"]),_Credential);

                    //Lo agrego a la coleccion
                    _oItems.Add(_oContactMessenger.IdContactMessenger, _oContactMessenger);
                }
                return _oItems;
            }
            internal Entities.ContactMessenger Item(Int64 idContactMessenger)
            {
                //Objeto de data layer para acceder a datos
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                Entities.ContactMessenger _item = null;
                IEnumerable<System.Data.Common.DbDataRecord> _record;
                if (_Person == null)
                {
                    _record = _dbDirectoryServices.ContactMessengers_ReadById(idContactMessenger, _Organization.IdOrganization);
                }
                else
                {
                    _record = _dbDirectoryServices.ContactMessengers_ReadByIdPerson(idContactMessenger, _Organization.IdOrganization, _Person.IdPerson);
                } 
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    _item= new Entities.ContactMessenger(Convert.ToInt64(_dbRecord["IdContactMessenger"]),
                                Convert.ToString(_dbRecord["Provider"]),
                                Convert.ToString(_dbRecord["Application"]),
                                Convert.ToString(_dbRecord["Data"]),
                                Convert.ToInt64(_dbRecord["IdContactType"]),_Credential);
                }
                return _item;
            }
        #endregion
    }
}
