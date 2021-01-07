using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Data.SqlClient;

namespace Condesus.EMS.Business.IA.Collections
{
    public class ExceptionTypes_LG
    {
        #region Internal Properties
        private Credential _Credential;
            private Int64 _IdExceptionType; //El identificador de la ExceptionType
        #endregion

            internal ExceptionTypes_LG(Int64 idExceptionType, Credential credential)
        {
            _Credential = credential;
            _IdExceptionType = idExceptionType;
        }


            #region Read Functions
            public Entities.ExceptionType_LG Item(String idLanguage)
            {
                //Acceso a datos para la opción de idioma
                Condesus.EMS.DataAccess.IA.ImprovementActions _dbImprovementActions = new Condesus.EMS.DataAccess.IA.ImprovementActions();

                Entities.ExceptionType_LG _item = null;
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbImprovementActions.ExceptionTypes_LG_ReadById(_IdExceptionType, idLanguage);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    _item = new Entities.ExceptionType_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]));
                }
                return _item;
            }
            public Dictionary<String, Entities.ExceptionType_LG> Items()
            {
                //Acceso a datos para la opción de idioma
                Condesus.EMS.DataAccess.IA.ImprovementActions _dbImprovementActions = new Condesus.EMS.DataAccess.IA.ImprovementActions();

                Dictionary<String, Entities.ExceptionType_LG> _oExceptionTypes_LG = new Dictionary<String, Entities.ExceptionType_LG>();
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbImprovementActions.ExceptionTypes_LG_ReadAll(_IdExceptionType);

                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    Entities.ExceptionType_LG _oExceptionType_LG = new Entities.ExceptionType_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]));
                    _oExceptionTypes_LG.Add(Convert.ToString(_dbRecord["IdLanguage"]), _oExceptionType_LG);
                }

                return _oExceptionTypes_LG;
            }
            #endregion

            #region Write Functions
            public Entities.ExceptionType_LG Add(String idLanguage, String name, String description)
            {
                try
                {
                    //Acceso a datos para la opción de idioma
                    Condesus.EMS.DataAccess.IA.ImprovementActions _dbImprovementActions = new Condesus.EMS.DataAccess.IA.ImprovementActions();

                    //ejecuta el insert a la base y devuelve el ID que le asigno en la bd            
                    _dbImprovementActions.ExceptionTypes_LG_Create(_IdExceptionType, idLanguage, name, description, _Credential.User.Person.IdPerson);
                    return new Entities.ExceptionType_LG(idLanguage, name, description);
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
            public Entities.ExceptionType_LG Modify(String idLanguage, String name, String description)
            {
                try
                {
                    Condesus.EMS.DataAccess.IA.ImprovementActions _dbImprovementActions = new Condesus.EMS.DataAccess.IA.ImprovementActions();

                    _dbImprovementActions.ExceptionTypes_LG_Update(_IdExceptionType, idLanguage, name, description, _Credential.User.Person.IdPerson);
                    return new Entities.ExceptionType_LG(idLanguage, name,description);
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
                    Condesus.EMS.DataAccess.IA.ImprovementActions _dbImprovementActions = new Condesus.EMS.DataAccess.IA.ImprovementActions();

                    _dbImprovementActions.ExceptionTypes_LG_Delete(_IdExceptionType, idLanguage, _Credential.User.Person.IdPerson);
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
