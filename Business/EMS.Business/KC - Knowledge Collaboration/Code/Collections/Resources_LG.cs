using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.KC.Collections
{
    public class Resources_LG
    {
        #region Internal Properties
            private Credential _Credential;
            private Entities.Resource _Resource;
        #endregion

            internal Resources_LG(Entities.Resource resource, Credential credential)
        {
            _Credential = credential;
            _Resource = resource;
        }

       #region Read Functions
        public Entities.Resource_LG Item(DS.Entities.Language language)
        {
           
            //Acceso a datos para la opción de idioma
            DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();

            Entities.Resource_LG _resource_LG = null;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbKnowledgeCollaboration.Resources_LG_ReadById(_Resource.IdResource,language.IdLanguage);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_resource_LG == null)
                {
                    _resource_LG = new Entities.Resource_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Title"]), Convert.ToString(_dbRecord["Description"]));
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                    {
                        return _resource_LG;
                    }
                }
                else
                {
                    return new Entities.Resource_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Title"]), Convert.ToString(_dbRecord["Description"]));
                }
            }
            return _resource_LG;

        }
        public Dictionary<String, Entities.Resource_LG> Items()
        {
            //Acceso a datos para la opción de idioma
            DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();

            Dictionary<String, Entities.Resource_LG> _Resources_LG = new Dictionary<String, Entities.Resource_LG>();
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbKnowledgeCollaboration.Resources_LG_ReadAll(_Resource.IdResource);

            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                Entities.Resource_LG _resource_LG = new Entities.Resource_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Title"]), Convert.ToString(_dbRecord["Description"]));
                _Resources_LG.Add(Convert.ToString(_dbRecord["IdLanguage"]), _resource_LG);
            }

            return _Resources_LG;
        }
        #endregion
        
        #region Write Functions
        public Entities.Resource_LG Add(DS.Entities.Language language, String title, String description)
        {
            try
            {
                //Acceso a datos para la opción de idioma
                DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();
                DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();

                using (TransactionScope _transactionScope = new TransactionScope())
                {

                    //ejecuta el insert a la base y devuelve el ID que le asigno en la bd            
                    _dbKnowledgeCollaboration.Resources_LG_Create(_Resource.IdResource, language.IdLanguage, title, description);
                    _dbLog.Create("KC_Resources_LG", "Resources_LG", "Add", "IdResource=" + _Resource.IdResource + " and IdLanguage='" + language.IdLanguage + "'", _Credential.User.IdPerson);

                    _transactionScope.Complete();
                }
                

                return new Entities.Resource_LG(language.IdLanguage, title, description);
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
        public Entities.Resource_LG Modify(DS.Entities.Language language, String title, String description)
        {
            try
            {
                //Acceso a datos para la opción de idioma
                DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();
                DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();

                using (TransactionScope _transactionScope = new TransactionScope())
                {

                    _dbKnowledgeCollaboration.Resources_LG_Update(_Resource.IdResource, language.IdLanguage, title, description);
                    _dbLog.Create("KC_Resources_LG", "Resources_LG", "Modify", "IdResource=" + _Resource.IdResource + " and IdLanguage='" + language.IdLanguage + "'", _Credential.User.IdPerson);

                    _transactionScope.Complete();
                }

                return new Entities.Resource_LG(language.IdLanguage, title, description);
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
            //controla que no se borre el lenguage default
            if (_Credential.DefaultLanguage.IdLanguage == language.IdLanguage)
            {
                throw new Exception(Common.Resources.Errors.RemoveDefaultCulture);
            }
            try
            {
                //Acceso a datos para la opción de idioma
                DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();
                DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();

                using (TransactionScope _transactionScope = new TransactionScope())
                {
                    _dbKnowledgeCollaboration.Resources_LG_Delete(_Resource.IdResource, language.IdLanguage);
                    _dbLog.Create("KC_Resources_LG", "Resources_LG", "Remove", "IdResource=" + _Resource.IdResource + " and IdLanguage='" + language.IdLanguage + "'", _Credential.User.IdPerson);

                    _transactionScope.Complete();
                }
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
