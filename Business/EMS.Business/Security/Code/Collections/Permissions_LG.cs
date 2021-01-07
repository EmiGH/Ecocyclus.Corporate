using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.Security.Collections
{
    public class Permissions_LG
    {
         #region Internal Properties
            private Credential _Credential;
            private Int64 _IdPermission; 
        #endregion

            internal Permissions_LG(Int64 idPermission, Credential credential)
        {
            _Credential = credential;
            _IdPermission = idPermission;
        }

        #region Read Functions
            public Entities.Permission_LG Item(String idLanguage)
            {
                //Acceso a datos para la opción de idioma
                DataAccess.Security.SecuritySystems _dbSecuritySystems = new Condesus.EMS.DataAccess.Security.SecuritySystems();

                Entities.Permission_LG _item = null;
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSecuritySystems.Permissions_LG_ReadById(_IdPermission, idLanguage);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    _item = new Entities.Permission_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]),Convert.ToString(_dbRecord["Description"]));
                }
                return _item;
            }
            public Dictionary<String, Entities.Permission_LG> Items()
            {
                //Acceso a datos para la opción de idioma
                DataAccess.Security.SecuritySystems _dbSecuritySystems = new Condesus.EMS.DataAccess.Security.SecuritySystems();

                Dictionary<String, Entities.Permission_LG> _oPermissions_LG = new Dictionary<String, Entities.Permission_LG>();
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSecuritySystems.Permissions_LG_ReadAll(_IdPermission);

                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    Entities.Permission_LG _oPermission_LG = new Entities.Permission_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]));
                    _oPermissions_LG.Add(Convert.ToString(_dbRecord["IdLanguage"]), _oPermission_LG);
                }

                return _oPermissions_LG;
            }
            #endregion

        #region Write Functions
            public Entities.Permission_LG Add(String idLanguage, String name, String description)
            {
                try
                {
                    //Acceso a datos para la opción de idioma
                    DataAccess.Security.SecuritySystems _dbSecuritySystems = new Condesus.EMS.DataAccess.Security.SecuritySystems();

                    //ejecuta el insert a la base y devuelve el ID que le asigno en la bd            
                    _dbSecuritySystems.Permissions_LG_Create(_IdPermission, idLanguage, name, description, _Credential.User.Person.IdPerson);
                    return new Entities.Permission_LG(idLanguage, name, description);
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
            public Entities.Permission_LG Modify(String idLanguage, String name, String description)
            {
                try
                {
                    //Acceso a datos para la opción de idioma
                    DataAccess.Security.SecuritySystems _dbSecuritySystems = new Condesus.EMS.DataAccess.Security.SecuritySystems();

                    _dbSecuritySystems.Permissions_LG_Update(_IdPermission, idLanguage, name, description, _Credential.User.Person.IdPerson);
                    return new Entities.Permission_LG(idLanguage, name, description);
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
            public void Remove(String idLanguage)
            {
                //Check to verify that the language option to be deleted is not default language
                if (_Credential.DefaultLanguage.IdLanguage == idLanguage)
                {
                    throw new Exception(Common.Resources.Errors.RemoveDefaultCulture);
                }
                try
                {
                    //Acceso a datos para la opción de idioma
                    DataAccess.Security.SecuritySystems _dbSecuritySystems = new Condesus.EMS.DataAccess.Security.SecuritySystems();

                    _dbSecuritySystems.Permissions_LG_Delete(_IdPermission, idLanguage, _Credential.User.Person.IdPerson);
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
    }
}
