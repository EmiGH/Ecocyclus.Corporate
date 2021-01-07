using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.DS.Collections
{
    internal class ContactTypes
    {
        #region Internal Properties
            private Credential _Credential;
            private Int64 _Applicability; //Tipo de tipo de contactos
        #endregion

        internal ContactTypes(Int64 applicability, Credential credential)
        {
            _Credential = credential;
            _Applicability = applicability;
        }
       
        #region Read Functions
            internal Entities.ContactType Item(Int64 idContactType)
            {

                //Objeto de data layer para acceder a datos
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                Entities.ContactType _contactType = null;
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.ContactTypes_ReadById(idContactType, _Credential.CurrentLanguage.IdLanguage);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    if (_contactType == null)
                    {
                        _contactType = new Entities.ContactType(Convert.ToInt64(_dbRecord["IdContactType"]), _Credential.CurrentLanguage.IdLanguage,_Applicability, Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), _Credential);
                        if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                        {
                            return _contactType;
                        }
                    }
                    else
                    {
                        return new Entities.ContactType(Convert.ToInt64(_dbRecord["IdContactType"]), _Credential.CurrentLanguage.IdLanguage, _Applicability, Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]),_Credential);
                    }
                }
                return _contactType; 
            }
            internal Dictionary<Int64, Entities.ContactType> Items()
            {
                //Coleccion para devolver los ContactType
                Dictionary<Int64, Entities.ContactType> _oItems = new Dictionary<Int64, Entities.ContactType>();

                //Objeto de data layer para acceder a datos
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.ContactTypes_ReadAll(_Credential.CurrentLanguage.IdLanguage, _Applicability);

                //Si esto esta en true, no debe insertar en la coleccion. (esto es cuando ya esta insertado el idioma elegido en la coleccion)
                Boolean _oInsert = true;

                //busca si hay mas de un id de contacto igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdContactType"])))
                    {
                        ///Si el que agregue es el default lo elimino porque el que voy a agregar es el del idioma en uso por el usuario
                        if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                        {
                            _oItems.Remove(Convert.ToInt64(_dbRecord["IdContactType"]));
                        }
                        else
                        {
                            //No debe insertar en la coleccion ya que existe el idioma correcto.
                            _oInsert = false;
                        }
                        //Solo inserta si es necesario.
                        if (_oInsert)
                        {
                            //Declara e instancia un ContactType 
                            Entities.ContactType _oContactType = new Entities.ContactType(Convert.ToInt64(_dbRecord["IdContactType"]), _Credential.CurrentLanguage.IdLanguage, _Applicability, Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), _Credential);
                            //Lo agrego a la coleccion
                            _oItems.Add(_oContactType.IdContactType , _oContactType );
                        }
                        _oInsert = true;
                    }
                    else
                    {   
                        //Declara e instancia un ContactType 
                        Entities.ContactType _oContactType = new Entities.ContactType(Convert.ToInt64(_dbRecord["IdContactType"]), _Credential.CurrentLanguage.IdLanguage, _Applicability, Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), _Credential);
                        //Lo agrego a la coleccion
                        _oItems.Add(_oContactType.IdContactType , _oContactType );
                    }
                }
                return _oItems;
            }
            internal Dictionary<Int64, Entities.ContactType> ItemsByApplicability()
            {

                //Coleccion para devolver los países
                Dictionary<Int64, Entities.ContactType> _oItems = new Dictionary<Int64, Entities.ContactType>();

                //Objeto de data layer para acceder a datos
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.ContactTypes_ReadAll(_Credential.CurrentLanguage.IdLanguage, _Applicability);

                //Si esto esta en true, no debe insertar en la coleccion. (esto es cuando ya esta insertado el idioma elegido en la coleccion)
                Boolean _oInsert = true;

                //busca si hay mas de un id de contacto igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdContactType"])))
                    {
                        ///Si el que agregue es el default lo elimino porque el que voy a agregar es el del idioma en uso por el usuario
                        if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                        {
                            _oItems.Remove(Convert.ToInt64(_dbRecord["IdContactType"]));
                        }
                        else
                        {
                            //No debe insertar en la coleccion ya que existe el idioma correcto.
                            _oInsert = false;
                        }

                        //Solo inserta si es necesario.
                        if (_oInsert)
                        {
                            //Declara e instancia un ContactType 
                            Entities.ContactType _oContactType = new Entities.ContactType(Convert.ToInt64(_dbRecord["IdContactType"]), _Credential.CurrentLanguage.IdLanguage, _Applicability, Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), _Credential);
                            //Lo agrego a la coleccion
                            _oItems.Add(_oContactType.IdContactType, _oContactType);
                        }
                        _oInsert = true;
                    }
                    else
                    {
                        //Declara e instancia un ContactType 
                        Entities.ContactType _oContactType = new Entities.ContactType(Convert.ToInt64(_dbRecord["IdContactType"]), _Credential.CurrentLanguage.IdLanguage, _Applicability, Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), _Credential);
                        //Lo agrego a la coleccion
                        _oItems.Add(_oContactType.IdContactType, _oContactType);
                    }
                }
                return _oItems;
            }
        #endregion

        #region Write Functions
            internal Entities.ContactType Add(String name, String description)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                    //Ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                    Int64 _idContactType = _dbDirectoryServices.ContactTypes_Create(_Applicability, _Credential.DefaultLanguage.IdLanguage, name, description, _Credential.User.Person.IdPerson);
                    //Devuelvo el objeto país creado
                    return new Entities.ContactType(_idContactType, _Credential.DefaultLanguage.IdLanguage, _Applicability, name, description, _Credential);
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
            internal void Remove(Int64 idContactType)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                    //Borrar de la base de datos
                    _dbDirectoryServices.ContactTypes_Delete(idContactType, _Credential.User.Person.IdPerson);
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
            internal void Modify(Int64 idContactType, String name, String description)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                    //Modifico los datos de la base
                    _dbDirectoryServices.ContactTypes_Update(idContactType, _Applicability, _Credential.DefaultLanguage.IdLanguage, name, description, _Credential.User.Person.IdPerson);
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
    }
}
