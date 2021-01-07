using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.DS.Collections
{   
    internal class Users
    {
        #region Internal Properties
            private Credential _Credential;
            private Int64 _IdOrganization;  //Identificador de organización
        #endregion


        internal Users(Credential credential) 
        {
            _Credential = credential;
        }
       
        internal Users(Entities.Organization Organization, Credential credential)
        {
            _Credential = credential;
            _IdOrganization = Organization.IdOrganization;
        }

        #region Read Functions
            internal Entities.User Item(String username)
            {
                //Objeto de data layer para acceder a datos
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                Entities.User _item = null;
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.Users_ReadById(username);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    _item = new Entities.User(Convert.ToInt64(_dbRecord["IdPerson"]), Convert.ToInt64(_dbRecord["IdOrganization"]), Convert.ToString(_dbRecord["Username"]), Convert.ToString(_dbRecord["Password"]), Common.Common.CastNullValues(Convert.ToString(_dbRecord["LastIP"]), "0"), Common.Common.CastNullValues<DateTime>(_dbRecord["LastLogin"], DateTime.MinValue), Convert.ToBoolean(_dbRecord["Active"]), Convert.ToBoolean(_dbRecord["ChangePasswordOnNextLogin"]), Convert.ToBoolean(_dbRecord["CannotChangePassword"]), Convert.ToBoolean(_dbRecord["PasswordNeverExpires"]), _Credential, Convert.ToBoolean(_dbRecord["ViewGlobalMenu"]));
                }
                return _item;
            }
            internal Entities.User Item(Int64 idPerson)
            {
                //Objeto de data layer para acceder a datos
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                Entities.User _item = null;
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.Users_GetByPerson(idPerson, _IdOrganization);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    _item = new Entities.User(Convert.ToInt64(_dbRecord["IdPerson"]), _IdOrganization, Convert.ToString(_dbRecord["Username"]), Convert.ToString(_dbRecord["Password"]), Convert.ToString(_dbRecord["LastIP"]), Common.Common.CastNullValues<DateTime>(_dbRecord["LastLogin"], DateTime.MinValue), Convert.ToBoolean(_dbRecord["Active"]), Convert.ToBoolean(_dbRecord["ChangePasswordOnNextLogin"]), Convert.ToBoolean(_dbRecord["CannotChangePassword"]), Convert.ToBoolean(_dbRecord["PasswordNeverExpires"]), _Credential, Convert.ToBoolean(_dbRecord["ViewGlobalMenu"]));
                }
                return _item;
            }
            internal Dictionary<String, Entities.User> Items()
            {
                //Coleccion para devolver los usuarios
                Dictionary<String, Entities.User> _oItems = new Dictionary<String, Entities.User>();

                //Objeto de data layer para acceder a datos
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.Users_ReadAll(_IdOrganization);

                //busca si hay mas de un id User igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    //Declara e instancia un rol
                    Entities.User _oUser = new Entities.User(Convert.ToInt64(_dbRecord["IdPerson"]), _IdOrganization, Convert.ToString(_dbRecord["Username"]), Convert.ToString(_dbRecord["Password"]), Convert.ToString(_dbRecord["LastIP"]), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["LastLogin"], DateTime.MinValue)), Convert.ToBoolean(_dbRecord["Active"]), Convert.ToBoolean(_dbRecord["ChangePasswordOnNextLogin"]), Convert.ToBoolean(_dbRecord["CannotChangePassword"]), Convert.ToBoolean(_dbRecord["PasswordNeverExpires"]), _Credential, Convert.ToBoolean(_dbRecord["ViewGlobalMenu"]));
                 
                    //Lo agrego a la coleccion
                    _oItems.Add(_oUser.Username, _oUser);
                }
                return _oItems;
            }
        #endregion

        #region Write Functions
            internal Entities.User Add(Int64 idPerson, String username, String password, Boolean active, Boolean changePasswordOnNextLogin, Boolean cannotChangePassword, Boolean passwordNeverExpires, Boolean ViewGlobalMenu)
            {
                //Hasheo la clave
                String _oHashPassword = Condesus.EMS.Business.Security.Cryptography.Hash(password);

                //Validate password policy
                Condesus.EMS.Business.Security.Authority.AuthorizePassword(username, _oHashPassword);
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();
                    //ejecuta el insert
                    _dbDirectoryServices.Users_Create(idPerson, username, _oHashPassword, active, _IdOrganization, changePasswordOnNextLogin, cannotChangePassword, passwordNeverExpires, _Credential.User.Person.IdPerson, ViewGlobalMenu);

                    //Devuelvo el objeto usuario creado
                    return new Entities.User(idPerson, _IdOrganization, username, _oHashPassword, "", DateTime.MinValue, active, changePasswordOnNextLogin, cannotChangePassword, passwordNeverExpires, _Credential, ViewGlobalMenu);
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
            internal void Remove(Entities.User user)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();
                   
                    //Borrar de la base de datos
                    _dbDirectoryServices.Users_Delete(user.Username, _Credential.User.Person.IdPerson);
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
            internal void Modify(Int64 idPerson, String username, Boolean active, Boolean changePasswordOnNextLogin, Boolean cannotChangePassword, Boolean passwordNeverExpires, Boolean ViewGlobalMenu)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                    //Modifico los datos de la base
                    _dbDirectoryServices.Users_Update(idPerson, username, active, _IdOrganization, changePasswordOnNextLogin, cannotChangePassword, passwordNeverExpires, _Credential.User.Person.IdPerson, ViewGlobalMenu);
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
            internal void Modify(String username,String password)
            {
                //Hasheo la clave
                String _oHashPassword = Condesus.EMS.Business.Security.Cryptography.Hash(password);

                //Validate password policy
                Condesus.EMS.Business.Security.Authority.AuthorizePassword(username, _oHashPassword);
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                    //Modifico los datos de la base
                    _dbDirectoryServices.Users_Update(username, _oHashPassword, _Credential.User.Person.IdPerson);
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
            internal void ResetPassword(String username, String oldPassword, String newPassword)
            { 
                
            }
        #endregion

    }
}
