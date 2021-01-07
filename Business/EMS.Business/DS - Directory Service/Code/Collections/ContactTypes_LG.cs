using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.DS.Collections
{
    public class ContactTypes_LG
    {
        #region Internal Properties
            private Credential _Credential;
            private Int64 _IdContactType; //El identificador del tipo de contacto         
        #endregion

        internal ContactTypes_LG(Int64 IdContactType,Credential credential)
        {
            _Credential = credential;
            _IdContactType = IdContactType;
        }

        #region Read Functions
            public Entities.ContactType_LG Item(String idLanguage)
            {
                //Acceso a datos para la opción de idioma
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                Entities.ContactType_LG _item = null;
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.ContactTypes_LG_ReadById(_IdContactType, idLanguage);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    _item= new Entities.ContactType_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]));
                }
                return _item;
            }
            public Dictionary<String, Entities.ContactType_LG> Items()
            {
                //Acceso a datos para la opción de idioma
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                Dictionary<String, Entities.ContactType_LG> _oContactTypes_LG = new Dictionary<String, Entities.ContactType_LG>();
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.ContactTypes_LG_ReadAll(_IdContactType);

                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    Entities.ContactType_LG _oContactType_LG = new Entities.ContactType_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]));
                    _oContactTypes_LG.Add(Convert.ToString(_dbRecord["IdLanguage"]), _oContactType_LG);
                }

                return _oContactTypes_LG;
            }
        #endregion

        #region Write Functions
            public Entities.ContactType_LG Add(String idLanguage, String name, String description)
            {
                try
                {
                    //Acceso a datos para la opción de idioma
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                    //ejecuta el insert a la base y devuelve el ID que le asigno en la bd            
                    _dbDirectoryServices.ContactTypes_LG_Create(_IdContactType, idLanguage, name, description, _Credential.User.Person.IdPerson);
                    return new Entities.ContactType_LG(idLanguage, name, description);
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
            public Entities.ContactType_LG Modify(String idLanguage, String name, String description)
            {
                try
                {
                    //Acceso a datos para la opción de idioma
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                    _dbDirectoryServices.ContactTypes_LG_Update(_IdContactType, idLanguage, name, description, _Credential.User.Person.IdPerson);
                    return new Entities.ContactType_LG(idLanguage, name, description);
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

                    _dbDirectoryServices.ContactTypes_LG_Delete(_IdContactType, idLanguage, _Credential.User.Person.IdPerson);
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
