using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.DS.Collections
{
    internal class ContactEmails
    {
        #region Internal Properties
            private Credential _Credential;
            private Entities.Person _Person;
            private Entities.Organization _Organization;
        #endregion

        internal ContactEmails(Entities.Person person)
        {
            _Credential =  person.Credential;
            _Organization = person.Organization;
            _Person = person;
        }
        internal ContactEmails(Entities.Organization organization)
        {
            _Organization = organization;
            _Credential = organization.Credential;
        }

        #region Organizations
            #region Write Functions
        internal Entities.ContactEmail AddByOrganization(String email, Entities.ContactType contactType)
                {
                    try
                    {
                        //Objeto de data layer para acceder a datos
                        DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                        //ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                        Int64 _idContactEmail = _dbDirectoryServices.ContactEmails_AddByOrganization(email, _Organization.IdOrganization, contactType.IdContactType, _Credential.User.Person.IdPerson);
                        //Devuelvo el objeto email creado
                        return new Entities.ContactEmail(_idContactEmail, email, contactType.IdContactType, _Credential);
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
        internal void RemoveByOrganization(Entities.ContactEmail contactEmail)
                {
                    try
                    {
                        //Objeto de data layer para acceder a datos
                        DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                        //Borrar de la base de datos
                        _dbDirectoryServices.ContactEmails_RemoveByOrganization(contactEmail.IdContactEmail, _Organization.IdOrganization, _Credential.User.Person.IdPerson);
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
                internal void ModifyByOrganization(Entities.ContactEmail contactEmail, String email, Entities.ContactType contactType)
                {
                    try
                    {
                        //Objeto de data layer para acceder a datos
                        DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                        //Modifico los datos de la base
                        _dbDirectoryServices.ContactEmails_Update(contactEmail.IdContactEmail, email, contactType.IdContactType, _Organization.IdOrganization, _Credential.User.Person.IdPerson);
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
                internal Entities.ContactEmail AddByPerson(String email, Entities.ContactType contactType)
                {
                    try
                    {
                        //Objeto de data layer para acceder a datos
                        DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                        //ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                        Int64 _idContactEmail = _dbDirectoryServices.ContactEmails_AddByPerson(email, _Person.IdPerson, contactType.IdContactType, _Organization.IdOrganization, _Credential.User.Person.IdPerson);
                        //Devuelvo el objeto email creado
                        return new Entities.ContactEmail(_idContactEmail, email, contactType.IdContactType, _Credential);
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
                internal void ModifyByPerson(Entities.ContactEmail contactEmail, String email, Entities.ContactType contactType)
                {
                    try
                    {
                        //Objeto de data layer para acceder a datos
                        DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                        //Modifico los datos de la base
                        _dbDirectoryServices.ContactEmails_Update(contactEmail.IdContactEmail, email, contactType.IdContactType, _Organization.IdOrganization, _Credential.User.Person.IdPerson);
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
                internal void RemoveByPerson(Entities.ContactEmail contactEmail)
                {
                    try
                    {
                        //Objeto de data layer para acceder a datos
                        DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                        //Borrar de la base de datos
                        _dbDirectoryServices.ContactEmails_RemoveByPerson(contactEmail.IdContactEmail, _Person.IdPerson, _Organization.IdOrganization, _Credential.User.Person.IdPerson);
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
            #endregion       
        #endregion

        #region Common Read Functions
            internal Dictionary<Int64, Entities.ContactEmail> Items()
            {
                //Coleccion para devolver los emails
                Dictionary<Int64, Entities.ContactEmail> _oItems = new Dictionary<Int64, Entities.ContactEmail>();

                //Objeto de data layer para acceder a datos
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                IEnumerable<System.Data.Common.DbDataRecord> _record;
                if (_Person == null)
                {
                    _record = _dbDirectoryServices.ContactEmails_GetByOrganization(_Organization.IdOrganization);
                }
                else
                {
                    _record = _dbDirectoryServices.ContactEmails_GetByPerson(_Person.IdPerson, _Organization.IdOrganization);
                }
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    //Declara e instancia un email
                    Entities.ContactEmail _oContactEmail = new Entities.ContactEmail(Convert.ToInt64(_dbRecord["IdContactEmail"]),
                        Convert.ToString(_dbRecord["Email"]),
                        Convert.ToInt64(_dbRecord["IdContactType"]),_Credential);

                    //Lo agrego a la coleccion
                    _oItems.Add(_oContactEmail.IdContactEmail, _oContactEmail);
                }
                return _oItems;
            }
            internal Entities.ContactEmail Item(Int64 idContactEmail)
            {
                //Objeto de data layer para acceder a datos
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                Entities.ContactEmail _item = null;
                IEnumerable<System.Data.Common.DbDataRecord> _record;
                if (_Person == null)
                {
                    _record = _dbDirectoryServices.ContactEmails_ReadById(idContactEmail, _Organization.IdOrganization);
                }
                else
                {
                    _record = _dbDirectoryServices.ContactEmails_ReadByIdPerson(idContactEmail, _Organization.IdOrganization, _Person.IdPerson);
                } 
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    _item = new Entities.ContactEmail(Convert.ToInt64(_dbRecord["IdContactEmail"]),
                        Convert.ToString(_dbRecord["Email"]),
                        Convert.ToInt64(_dbRecord["IdContactType"]),_Credential);
                }
                return _item;
            }
        #endregion
    }
}
