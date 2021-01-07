using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.KC.Collections
{
    public class ResourceTypes_LG
    {
        #region Internal Properties
            private Credential _Credential;
            private Entities.ResourceType _ResourceType; 
        #endregion

            internal ResourceTypes_LG(Entities.ResourceType resourceType, Credential credential)
        {
            _Credential = credential;
            _ResourceType = resourceType;
        }

        #region Read Functions
            public Entities.ResourceType_LG Item(DS.Entities.Language language)
            {
                //Acceso a datos para la opción de idioma
                DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();

                Entities.ResourceType_LG _item = null;
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbKnowledgeCollaboration.ResourceTypes_LG_ReadById(_ResourceType.IdResourceType, language.IdLanguage);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    _item = new Entities.ResourceType_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]),Convert.ToString(_dbRecord["Description"]));
                }
                return _item;
            }
            public Dictionary<String, Entities.ResourceType_LG> Items()
            {
                //Acceso a datos para la opción de idioma
                DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();

                Dictionary<String, Entities.ResourceType_LG> _oResourceTypes_LG = new Dictionary<String, Entities.ResourceType_LG>();
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbKnowledgeCollaboration.ResourceTypes_LG_ReadAll(_ResourceType.IdResourceType);

                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    Entities.ResourceType_LG _oResourceType_LG = new Entities.ResourceType_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]));
                    _oResourceTypes_LG.Add(Convert.ToString(_dbRecord["IdLanguage"]), _oResourceType_LG);
                }

                return _oResourceTypes_LG;
            }
            #endregion

        #region Write Functions
            public Entities.ResourceType_LG Add(DS.Entities.Language language, String name, String description)
            {
                try
                {
                    //Acceso a datos para la opción de idioma
                    DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();

                    //ejecuta el insert a la base y devuelve el ID que le asigno en la bd            
                    _dbKnowledgeCollaboration.ResourceTypes_LG_Create(_ResourceType.IdResourceType, language.IdLanguage, name, description, _Credential.User.Person.IdPerson);
                    return new Entities.ResourceType_LG(language.IdLanguage, name, description);
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
            public Entities.ResourceType_LG Modify(DS.Entities.Language language, String name, String description)
            {
                try
                {
                    //Acceso a datos para la opción de idioma
                    DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();

                    _dbKnowledgeCollaboration.ResourceTypes_LG_Update(_ResourceType.IdResourceType, language.IdLanguage, name, description, _Credential.User.Person.IdPerson);
                    return new Entities.ResourceType_LG(language.IdLanguage, name, description);
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
            public void Remove(DS.Entities.Language language)
            {
                //Check to verify that the language option to be deleted is not default language
                if (_Credential.DefaultLanguage.IdLanguage == language.IdLanguage)
                {
                    throw new Exception(Common.Resources.Errors.RemoveDefaultCulture);
                }
                try
                {
                    //Acceso a datos para la opción de idioma
                    DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();

                    _dbKnowledgeCollaboration.ResourceTypes_LG_Delete(_ResourceType.IdResourceType, language.IdLanguage, _Credential.User.Person.IdPerson);
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
