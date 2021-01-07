using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.DS.Collections
{
    public class SalutationTypes_LG
    {
        #region Internal Properties
            private Credential _Credential;
            private Int64 _IdSalutationType; //El identificador del tipo de nombramiento           
        #endregion

        internal SalutationTypes_LG(Int64 idSalutationType, Credential credential)
        {
            _Credential = credential;
            _IdSalutationType = idSalutationType;
        }

        #region Read Functions
            public Entities.SalutationType_LG Item(String idLanguage)
            {
                //Acceso a datos para la opción de idioma
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                Entities.SalutationType_LG _item = null;
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.SalutationTypes_LG_ReadById(_IdSalutationType, idLanguage);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    _item = new Entities.SalutationType_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]));
                }
                return _item;

            }   
            public Dictionary<String, Entities.SalutationType_LG> Items()
            {
                //Acceso a datos para la opción de idioma
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                Dictionary<String, Entities.SalutationType_LG> _SalutationTypes_LG = new Dictionary<String, Entities.SalutationType_LG>();
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.SalutationTypes_LG_ReadAll(_IdSalutationType);

                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    Entities.SalutationType_LG _SalutationType_LG = new Entities.SalutationType_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]));
                    _SalutationTypes_LG.Add(Convert.ToString(_dbRecord["IdLanguage"]), _SalutationType_LG);
                }

                return _SalutationTypes_LG;
            }
        #endregion

        #region Write Functions
            public Entities.SalutationType_LG Add(String idLanguage, String name, String description)
            {
                try
                {
                    //Acceso a datos para la opción de idioma
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                    //ejecuta el insert a la base y devuelve el ID que le asigno en la bd            
                    _dbDirectoryServices.SalutationTypes_LG_Create(_IdSalutationType, idLanguage, name, description, _Credential.User.Person.IdPerson);
                    return new Entities.SalutationType_LG(idLanguage, name, description);
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
            public Entities.SalutationType_LG Modify(String idLanguage, String name, String description)
            {
                try
                {
                    //Acceso a datos para la opción de idioma
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                    _dbDirectoryServices.SalutationTypes_LG_Update(_IdSalutationType, idLanguage, name, description, _Credential.User.Person.IdPerson);
                    return new Entities.SalutationType_LG(idLanguage, name, description);
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

                if (_Credential.DefaultLanguage.IdLanguage == idLanguage)
                {
                    throw new Exception(Common.Resources.Errors.RemoveDefaultCulture);
                }
                try
                {
                    //Acceso a datos para la opción de idioma
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                    _dbDirectoryServices.SalutationTypes_LG_Delete(_IdSalutationType, idLanguage, _Credential.User.Person.IdPerson);
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
