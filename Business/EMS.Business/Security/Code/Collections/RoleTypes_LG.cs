using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.Security.Collections
{
    //public class RoleTypes_LG
    //{
    //     #region Internal Properties
    //        private Credential _Credential;
    //        private Int64 _IdRoleType; 
    //    #endregion

    //        internal RoleTypes_LG(Int64 idRoleType, Credential credential)
    //    {
    //        _Credential = credential;
    //        _IdRoleType = idRoleType;
    //    }

    //    #region Read Functions
    //        public Entities.RoleType_LG Item(String idLanguage)
    //        {
    //            //Acceso a datos para la opción de idioma
    //            DataAccess.Security.SecuritySystems _dbSecuritySystems = new Condesus.EMS.DataAccess.Security.SecuritySystems();

    //            Entities.RoleType_LG _item = null;
    //            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSecuritySystems.RoleTypes_LG_ReadById(_IdRoleType, idLanguage);
    //            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
    //            {
    //                _item = new Entities.RoleType_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]));
    //            }
    //            return _item;
    //        }
    //        public Dictionary<String, Entities.RoleType_LG> Items()
    //        {
    //            //Acceso a datos para la opción de idioma
    //            DataAccess.Security.SecuritySystems _dbSecuritySystems = new Condesus.EMS.DataAccess.Security.SecuritySystems();

    //            Dictionary<String, Entities.RoleType_LG> _oRoleTypes_LG = new Dictionary<String, Entities.RoleType_LG>();
    //            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSecuritySystems.RoleTypes_LG_ReadAll(_IdRoleType);

    //            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
    //            {
    //                Entities.RoleType_LG _oRoleType_LG = new Entities.RoleType_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]));
    //                _oRoleTypes_LG.Add(Convert.ToString(_dbRecord["IdLanguage"]), _oRoleType_LG);
    //            }

    //            return _oRoleTypes_LG;

    //        }
    //        #endregion

    //    #region Write Functions
    //        public Entities.RoleType_LG Add(String idLanguage, String name)
    //        {
    //            try
    //            {
    //                //Acceso a datos para la opción de idioma
    //                DataAccess.Security.SecuritySystems _dbSecuritySystems = new Condesus.EMS.DataAccess.Security.SecuritySystems();

    //                //ejecuta el insert a la base y devuelve el ID que le asigno en la bd            
    //                _dbSecuritySystems.RoleTypes_LG_Create(_IdRoleType, idLanguage, name);
    //                return new Entities.RoleType_LG(idLanguage, name);
    //            }
    //            catch (SqlException ex)
    //            {
    //                if (ex.Number == Common.Constants.ErrorDataBaseDuplicatedKey)
    //                {
    //                    throw new Exception(Common.Resources.Errors.DuplicatedDataRecord, ex);
    //                }
    //                throw ex;
    //            }
    //        }
    //        public Entities.RoleType_LG Modify(String idLanguage, String name)
    //        {
    //            try
    //            {
    //                //Acceso a datos para la opción de idioma
    //                DataAccess.Security.SecuritySystems _dbSecuritySystems = new Condesus.EMS.DataAccess.Security.SecuritySystems();

    //                _dbSecuritySystems.RoleTypes_LG_Update(_IdRoleType, idLanguage, name);
    //                return new Entities.RoleType_LG(idLanguage, name);
    //            }
    //            catch (SqlException ex)
    //            {
    //                if (ex.Number == Common.Constants.ErrorDataBaseDuplicatedKey)
    //                {
    //                    throw new Exception(Common.Resources.Errors.DuplicatedDataRecord, ex);
    //                }
    //                throw ex;
    //            }
    //        }
    //        public void Remove(String idLanguage)
    //        {
    //            //Check to verify that the language option to be deleted is not default language
    //            if (_Credential.DefaultLanguage.IdLanguage == idLanguage)
    //            {
    //                throw new Exception(Common.Resources.Errors.RemoveDefaultCulture);
    //            }
    //            try
    //            {
    //                //Acceso a datos para la opción de idioma
    //                DataAccess.Security.SecuritySystems _dbSecuritySystems = new Condesus.EMS.DataAccess.Security.SecuritySystems();

    //                _dbSecuritySystems.RoleTypes_LG_Delete(_IdRoleType, idLanguage);
    //            }
    //            catch (SqlException ex)
    //            {
    //                if (ex.Number == Common.Constants.ErrorDataBaseDeleteReferenceConstraints)
    //                {
    //                    throw new Exception(Common.Resources.Errors.RemoveDependency, ex);
    //                }
    //                throw ex;
    //            }
    //    }
    //    #endregion
    //}
}
