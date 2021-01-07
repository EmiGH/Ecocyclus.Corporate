using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Data.SqlClient;

namespace Condesus.EMS.Business.KC.Collections
{
    public class ResourceHistoryStates_LG
    {
        #region Internal Properties
            private Credential _Credential;
            private Int64 _IdResourceState; 
        #endregion

            internal ResourceHistoryStates_LG(Int64 idResourceState, Credential credential)
        {
            _Credential = credential;
            _IdResourceState = idResourceState;
        }

        #region Read Functions
            public Entities.ResourceHistoryState_LG Item(String idLanguage)
            {

                //Acceso a datos para la opción de idioma
                DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();

                Entities.ResourceHistoryState_LG _item = null;
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbKnowledgeCollaboration.ResourceHistoryStates_LG_ReadById(_IdResourceState, idLanguage);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    _item = new Entities.ResourceHistoryState_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]));
                }
                return _item;
            }
            public Dictionary<String, Entities.ResourceHistoryState_LG> Items()
            {

                //Acceso a datos para la opción de idioma
                DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();

                Dictionary<String, Entities.ResourceHistoryState_LG> _oResourceHistoryStates_LG = new Dictionary<String, Entities.ResourceHistoryState_LG>();
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbKnowledgeCollaboration.ResourceHistoryStates_LG_ReadAll(_IdResourceState);

                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    Entities.ResourceHistoryState_LG _oResourceHistoryState_LG = new Entities.ResourceHistoryState_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]));
                    _oResourceHistoryStates_LG.Add(Convert.ToString(_dbRecord["IdLanguage"]), _oResourceHistoryState_LG);
                }

                return _oResourceHistoryStates_LG;
            }
            #endregion

        #region Write Functions
            public Entities.ResourceHistoryState_LG Add(String idLanguage, String name, String description)
            {
                try
                {
                    //Acceso a datos para la opción de idioma
                    DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();

                    //ejecuta el insert a la base y devuelve el ID que le asigno en la bd            
                    _dbKnowledgeCollaboration.ResourceHistoryStates_LG_Create(_IdResourceState, idLanguage, name, description, _Credential.User.Person.IdPerson);
                    return new Entities.ResourceHistoryState_LG(idLanguage, name, description);
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
            public Entities.ResourceHistoryState_LG Modify(String idLanguage, String name, String description)
            {
                try
                {
                    //Acceso a datos para la opción de idioma
                    DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();

                    _dbKnowledgeCollaboration.ResourceHistoryStates_LG_Update(_IdResourceState, idLanguage, name, description, _Credential.User.Person.IdPerson);
                    return new Entities.ResourceHistoryState_LG(idLanguage, name, description);
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
                    DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();

                    _dbKnowledgeCollaboration.ResourceHistoryStates_LG_Delete(_IdResourceState, idLanguage, _Credential.User.Person.IdPerson);
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
