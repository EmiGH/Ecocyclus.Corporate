using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.DS.Collections
{
    public class ApplicabilityContactTypes_LG
    {
        #region Internal Properties
            private Credential _Credential;
            private Int64 _IdApplicabilityContactType; //El identificador del tipo de nombramiento
        #endregion

        internal ApplicabilityContactTypes_LG(Int64 idApplicabilityContactType, Credential credential)
        {
            _Credential = credential;
            _IdApplicabilityContactType = idApplicabilityContactType;
        }

        #region Read Functions
            public Entities.ApplicabilityContactType_LG Item(String idLanguage)
            {
                //Acceso a datos para la opción de idioma
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();
                //Condesus.EMS.DataAccess.DS.ApplicabilityContactTypes_LG _dbApplicabilityContactTypes_LG = _dbDirectoryServices.ApplicabilityContactTypes_LG;

                Entities.ApplicabilityContactType_LG _item = null;
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.ApplicabilityContactTypes_LG_ReadById(_IdApplicabilityContactType, idLanguage);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    _item = new Entities.ApplicabilityContactType_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]));
                }
                return _item;
            }
            public Dictionary<String, Entities.ApplicabilityContactType_LG> Items()
            {
                //Acceso a datos para la opción de idioma
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();
                //Condesus.EMS.DataAccess.DS.ApplicabilityContactTypes_LG _dbApplicabilityContactTypes_LG = _dbDirectoryServices.ApplicabilityContactTypes_LG;

                Dictionary<String, Entities.ApplicabilityContactType_LG> _oApplicabilityContactTypes_LG = new Dictionary<String, Entities.ApplicabilityContactType_LG>();
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.ApplicabilityContactTypes_LG_ReadAll(_IdApplicabilityContactType);

                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    Entities.ApplicabilityContactType_LG _oApplicabilityContactType_LG = new Entities.ApplicabilityContactType_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]));
                    _oApplicabilityContactTypes_LG.Add(Convert.ToString(_dbRecord["IdLanguage"]), _oApplicabilityContactType_LG);
                }

                return _oApplicabilityContactTypes_LG;
            }
        #endregion

        #region Write Functions
            public Entities.ApplicabilityContactType_LG Add(String idLanguage, String name)
            {
                try
                {
                    //Acceso a datos para la opción de idioma
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();
                    //Condesus.EMS.DataAccess.DS.ApplicabilityContactTypes_LG _dbApplicabilityContactTypes_LG = _dbDirectoryServices.ApplicabilityContactTypes_LG;

                    //ejecuta el insert a la base y devuelve el ID que le asigno en la bd            
                    _dbDirectoryServices.ApplicabilityContactTypes_LG_Create(_IdApplicabilityContactType, idLanguage, name, _Credential.User.Person.IdPerson);
                    return new Entities.ApplicabilityContactType_LG(idLanguage, name);
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
            public Entities.ApplicabilityContactType_LG Modify(String idLanguage, String name)
            {
                try
                {
                    //Acceso a datos para la opción de idioma
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();
                    //Condesus.EMS.DataAccess.DS.ApplicabilityContactTypes_LG _dbApplicabilityContactTypes_LG = _dbDirectoryServices.ApplicabilityContactTypes_LG;

                    _dbDirectoryServices.ApplicabilityContactTypes_LG_Update(_IdApplicabilityContactType, idLanguage, name, _Credential.User.Person.IdPerson);
                    return new Entities.ApplicabilityContactType_LG(idLanguage, name);
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
                    //Condesus.EMS.DataAccess.DS.ApplicabilityContactTypes_LG _dbApplicabilityContactTypes_LG = _dbDirectoryServices.ApplicabilityContactTypes_LG;

                    _dbDirectoryServices.ApplicabilityContactTypes_LG_Delete(_IdApplicabilityContactType, idLanguage, _Credential.User.Person.IdPerson);
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
