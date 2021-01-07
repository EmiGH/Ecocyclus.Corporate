using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.DS.Collections
{
    public class ContactURLs_LG
    {
        #region Internal Properties
            private Credential _Credential;
            private Int64 _IdContactURLs_LG; //El identificador del tipo de contacto         
        #endregion

        internal ContactURLs_LG(Int64 IdContactURLs,Credential credential)
        {
            _Credential = credential;
            _IdContactURLs_LG = IdContactURLs;
        }

        #region Read Functions
            public Entities.ContactURL_LG Item(String idLanguage)
            {
                //Acceso a datos para la opción de idioma
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                Entities.ContactURL_LG _item = null;
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.ContactURLs_LG_ReadById(_IdContactURLs_LG, idLanguage);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    _item = new Entities.ContactURL_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]));
                }
                return _item;
            }
            public Dictionary<String, Entities.ContactURL_LG> Items()
            {
                //Acceso a datos para la opción de idioma
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                Dictionary<String, Entities.ContactURL_LG> _oContactURL_LG = new Dictionary<String, Entities.ContactURL_LG>();
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.ContactURLs_LG_ReadAll(_IdContactURLs_LG);

                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    Entities.ContactURL_LG _oContactType_LG = new Entities.ContactURL_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]));
                    _oContactURL_LG.Add(Convert.ToString(_dbRecord["IdLanguage"]), _oContactType_LG);
                }

                return _oContactURL_LG;
            }
        #endregion

        #region Write Functions
            public Entities.ContactURL_LG Add(String idLanguage, String name, String description)
            {
                try
                {
                    //Acceso a datos para la opción de idioma
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                    //ejecuta el insert a la base y devuelve el ID que le asigno en la bd            
                    _dbDirectoryServices.ContactURLs_LG_Create(_IdContactURLs_LG, idLanguage, name, description, _Credential.User.Person.IdPerson);
                    return new Entities.ContactURL_LG(idLanguage, name, description);
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
            public Entities.ContactURL_LG Modify(String idLanguage, String name, String description)
            {
                try
                {
                    //Acceso a datos para la opción de idioma
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                    _dbDirectoryServices.ContactURLs_LG_Update(_IdContactURLs_LG, idLanguage, name, description, _Credential.User.Person.IdPerson);
                    return new Entities.ContactURL_LG(idLanguage, name, description);
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

                    _dbDirectoryServices.ContactURLs_LG_Delete(_IdContactURLs_LG, idLanguage, _Credential.User.Person.IdPerson);
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
