using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.DS.Collections
{
    internal class ContactURLs
    {
        #region Internal Properties
            private Credential _Credential;            
            private Entities.Person _Person;
            private Entities.Organization _Organization;
        #endregion

            internal ContactURLs(Entities.Person person)
        {
            _Credential = person.Credential;
            _Organization = person.Organization;
            _Person = person;
        }
        internal ContactURLs(Entities.Organization organization)
        {
            _Organization = organization;
            _Credential = organization.Credential;
        }

        #region Organization
            #region Write Functions
        /// <summary>
        /// Alta para el conctactURL de una Organization
        /// </summary>
        /// <param name="url"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="contactType"></param>
        /// <returns></returns>
        internal Entities.ContactURL AddByOrganization(String url, String name, String description, Entities.ContactType contactType)
                {
                    try
                    {
                        //Objeto de data layer para acceder a datos
                        DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                        //ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                        Int64 _idContactURL = _dbDirectoryServices.ContactURLs_AddByOrganization(url, name, description, _Credential.DefaultLanguage.IdLanguage, _Organization.IdOrganization, contactType.IdContactType, _Credential.User.Person.IdPerson);
                        //Devuelvo el objeto direccion creado
                        return new Entities.ContactURL(_idContactURL, _Credential.DefaultLanguage.IdLanguage, url, name, description, contactType.IdContactType, _Credential);
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
                internal void ModifyByOrganization(Entities.ContactURL contactURL, String url, String name, String description, Entities.ContactType contactType)
                {
                    try
                    {
                        //Objeto de data layer para acceder a datos
                        DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                        //Modifico los datos de la base
                        _dbDirectoryServices.ContactURLs_Update(contactURL.IdContactURL, url, name, description, _Credential.DefaultLanguage.IdLanguage, _Organization.IdOrganization, contactType.IdContactType, _Credential.User.Person.IdPerson);
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
                internal void RemoveByOrganization(Entities.ContactURL contactURL)
                {
                    try
                    {
                        //Objeto de data layer para acceder a datos
                        DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                        //Borrar de la base de datos
                        _dbDirectoryServices.ContactURLs_RemoveByOrganization(contactURL.IdContactURL, _Organization.IdOrganization, _Credential.User.Person.IdPerson);
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
                //internal void RemoveByOrganization()
                //{
                //    try
                //    {
                //        //Objeto de data layer para acceder a datos
                //        DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                //        //Borrar de la base de datos
                //        _dbDirectoryServices.ContactURLs_RemoveByOrganization(_Organization.IdOrganization);
                //    }
                //    catch (SqlException ex)
                //    {
                //        if (ex.Number == Common.Constants.ErrorDataBaseDeleteReferenceConstraints)
                //        {
                //            throw new Exception(Common.Resources.Errors.RemoveDependency, ex);
                //        }
                //        throw ex;
                //    }
                //}
            #endregion
        #endregion

        #region People
            #region Write Functions
        /// <summary>
        /// Alta para el conctactURL de una person
        /// </summary>
        /// <param name="url"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="contactType"></param>
        /// <returns></returns>
                internal Entities.ContactURL AddByPerson(Entities.ContactType contactType, String url, String name, String description)
                {
                    try
                    {
                        //Objeto de data layer para acceder a datos
                        DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                        //ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                        Int64 _idContactURL = _dbDirectoryServices.ContactURLs_AddByPerson(url, name, description, _Credential.DefaultLanguage.IdLanguage, _Person.IdPerson, contactType.IdContactType, _Organization.IdOrganization, _Credential.User.Person.IdPerson);
                        //Devuelvo el objeto direccion creado
                        return new Entities.ContactURL(_idContactURL, _Credential.DefaultLanguage.IdLanguage, url, name, description, contactType.IdContactType, _Credential);
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
                internal void ModifyByPerson(Entities.ContactURL contactURL, Entities.ContactType contactType, String url, String name, String description)
                {
                    try
                    {
                        //Objeto de data layer para acceder a datos
                        DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                        //Modifico los datos de la base
                        _dbDirectoryServices.ContactURLs_Update(_Person.IdPerson, contactURL.IdContactURL, url, name, description, _Credential.DefaultLanguage.IdLanguage, _Organization.IdOrganization, contactType.IdContactType, _Credential.User.Person.IdPerson);
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
                internal void RemoveByPerson(Entities.ContactURL contactURL)
                {
                    try
                    {
                        //Objeto de data layer para acceder a datos
                        DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                        //Borrar de la base de datos
                        _dbDirectoryServices.ContactURLs_RemoveByPerson(contactURL.IdContactURL, _Person.IdPerson, _Credential.CurrentLanguage.IdLanguage, _Organization.IdOrganization, _Credential.User.Person.IdPerson);
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
            internal Entities.ContactURL Item(Int64 idContactURL)
            {
                //Objeto de data layer para acceder a datos
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                IEnumerable<System.Data.Common.DbDataRecord> _record;
                if (_Person == null)
                {
                    _record = _dbDirectoryServices.ContactURLs_ReadById(idContactURL, _Organization.IdOrganization, _Credential.CurrentLanguage.IdLanguage);
                }
                else
                {
                    _record = _dbDirectoryServices.ContactURLs_ReadByIdPerson(idContactURL, _Person.IdPerson, _Organization.IdOrganization, _Credential.CurrentLanguage.IdLanguage);
                }
                Entities.ContactURL _contactURL = null;
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    if (_contactURL == null)
                    {
                        _contactURL = new Entities.ContactURL(Convert.ToInt64(_dbRecord["IdContactURL"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["URL"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(_dbRecord["IdContactType"]),_Credential);
                        if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                        {
                            return _contactURL;
                        }
                    }
                    else
                    {
                        return new Entities.ContactURL(Convert.ToInt64(_dbRecord["IdContactURL"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["URL"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(_dbRecord["IdContactType"]),_Credential);
                    }
                }
                return _contactURL;
            }
            internal Dictionary<Int64, Entities.ContactURL> Items()
            {

                //Coleccion para devolver las direcciones
                Dictionary<Int64, Entities.ContactURL> _oItems = new Dictionary<Int64, Entities.ContactURL>();

                //Objeto de data layer para acceder a datos
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                //Si esto esta en true, no debe insertar en la coleccion. (esto es cuando ya esta insertado el idioma elegido en la coleccion)
                Boolean _oInsert = true;

                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record;
                if (_Person == null)
                {
                    _record = _dbDirectoryServices.ContactURLs_GetByOrganization(_Organization.IdOrganization, _Credential.CurrentLanguage.IdLanguage);
                }
                else
                {
                    _record = _dbDirectoryServices.ContactURLs_GetByPerson(_Person.IdPerson, _Organization.IdOrganization, _Credential.CurrentLanguage.IdLanguage);
                } 
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdContactURL"])))
                    {
                        ///Si el que agregue es el default lo elimino porque el que voy a agregar es el del idioma en uso por el usuario
                        if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                        {
                            _oItems.Remove(Convert.ToInt64(_dbRecord["IdContactURL"]));
                        }
                        else
                        {
                            //No debe insertar en la coleccion ya que existe el idioma correcto.
                            _oInsert = false;
                        }

                        //Solo inserta si es necesario.
                        if (_oInsert)
                        {
                            //Declara e instancia un pais 
                            Entities.ContactURL _oContactURL = new Entities.ContactURL(Convert.ToInt64(_dbRecord["IdContactURL"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["URL"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(_dbRecord["IdContactType"]),_Credential);
                            //Lo agrego a la coleccion
                            _oItems.Add(_oContactURL.IdContactURL, _oContactURL);
                        }
                        _oInsert = true;
                    }
                    else
                    {
                        //Declara e instancia una direccion 
                        Entities.ContactURL _oContactURL = new Entities.ContactURL(Convert.ToInt64(_dbRecord["IdContactURL"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["URL"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(_dbRecord["IdContactType"]),_Credential);
                        //Lo agrego a la coleccion
                        _oItems.Add(_oContactURL.IdContactURL, _oContactURL);
                    }
                }
                return _oItems;
            }
        #endregion       
    }
}
