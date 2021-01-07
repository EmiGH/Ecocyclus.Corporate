using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Data.SqlClient;

namespace Condesus.EMS.Business.IA.Collections
{
    public class ExceptionStates_LG
    {
         #region Internal Properties
            private Credential _Credential;
            private Int64 _IdExceptionState; //El identificador de la ExceptionType
        #endregion

            internal ExceptionStates_LG(Int64 idExceptionState, Credential credential)
        {
            _Credential = credential;
            _IdExceptionState = idExceptionState;
        }

        #region Read Functions
            public Entities.ExceptionState_LG Item(String idLanguage)
            {
                //Acceso a datos para la opción de idioma
                Condesus.EMS.DataAccess.IA.ImprovementActions _dbImprovementActions = new Condesus.EMS.DataAccess.IA.ImprovementActions();

                Entities.ExceptionState_LG _item = null;
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbImprovementActions.ExceptionStates_LG_ReadById(_IdExceptionState, idLanguage);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    _item = new Entities.ExceptionState_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]));
                }
                return _item;
            }
            public Dictionary<String, Entities.ExceptionState_LG> Items()
            {
                Condesus.EMS.DataAccess.IA.ImprovementActions _dbImprovementActions = new Condesus.EMS.DataAccess.IA.ImprovementActions();

                Dictionary<String, Entities.ExceptionState_LG> _oExceptionStates_LG = new Dictionary<String, Entities.ExceptionState_LG>();
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbImprovementActions.ExceptionStates_LG_ReadAll(_IdExceptionState);

                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    Entities.ExceptionState_LG _oExceptionState_LG = new Entities.ExceptionState_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]));
                    _oExceptionStates_LG.Add(Convert.ToString(_dbRecord["IdLanguage"]), _oExceptionState_LG);
                }

                return _oExceptionStates_LG;
            }
            #endregion

        #region Write Functions
            public Entities.ExceptionState_LG Add(String idLanguage, String name)
            {
                try
                {
                    //Acceso a datos para la opción de idioma
                    Condesus.EMS.DataAccess.IA.ImprovementActions _dbImprovementActions = new Condesus.EMS.DataAccess.IA.ImprovementActions();

                    //ejecuta el insert a la base y devuelve el ID que le asigno en la bd            
                    _dbImprovementActions.ExceptionStates_LG_Create(_IdExceptionState, idLanguage, name, _Credential.User.Person.IdPerson);
                    return new Entities.ExceptionState_LG(idLanguage, name);
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
            public Entities.ExceptionState_LG Modify(String idLanguage, String name)
            {
                try
                {
                    //Acceso a datos para la opción de idioma
                    Condesus.EMS.DataAccess.IA.ImprovementActions _dbImprovementActions = new Condesus.EMS.DataAccess.IA.ImprovementActions();

                    _dbImprovementActions.ExceptionStates_LG_Update(_IdExceptionState, idLanguage, name, _Credential.User.Person.IdPerson);
                    return new Entities.ExceptionState_LG(idLanguage, name);
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

                    _dbImprovementActions.ExceptionStates_LG_Delete(_IdExceptionState, idLanguage, _Credential.User.Person.IdPerson);
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
