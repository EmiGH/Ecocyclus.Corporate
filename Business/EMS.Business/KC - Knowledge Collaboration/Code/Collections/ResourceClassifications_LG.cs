using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.KC.Collections
{
    public class ResourceClassifications_LG
    {
        #region Internal Properties
            private Credential _Credential;
            private Entities.ResourceClassification _ResourceClassification; //El identificador del Resource classification
        #endregion

        internal ResourceClassifications_LG(Entities.ResourceClassification resourceClassification, Credential credential)
        {
            _Credential = credential;
            _ResourceClassification = resourceClassification;           
        }

        #region Read Functions
        public Entities.ResourceClassification_LG Item(DS.Entities.Language language)
        {
            //Acceso a datos para la opción de idioma
            DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();
            
            Entities.ResourceClassification_LG _ResourceClassification_LG = null;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbKnowledgeCollaboration.ResourceClassifications_LG_ReadById(_ResourceClassification.IdResourceClassification, language.IdLanguage);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_ResourceClassification_LG == null)
                {
                    _ResourceClassification_LG = new Entities.ResourceClassification_LG(Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["IdLanguage"]));
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                    {
                        return _ResourceClassification_LG;
                    }
                }
                else
                {
                    return new Entities.ResourceClassification_LG(Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["IdLanguage"]));
                }
            }
            return _ResourceClassification_LG;

        }
        public Dictionary<String, Entities.ResourceClassification_LG> Items()
        {
            //Acceso a datos para la opción de idioma
            DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();

            Dictionary<String, Entities.ResourceClassification_LG> _ResourceClassifications_LG = new Dictionary<String, Entities.ResourceClassification_LG>();
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbKnowledgeCollaboration.ResourceClassifications_LG_ReadAll(_ResourceClassification.IdResourceClassification);

            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                Entities.ResourceClassification_LG _ResourceClassification_LG = new Entities.ResourceClassification_LG(Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["IdLanguage"]));
                _ResourceClassifications_LG.Add(Convert.ToString(_dbRecord["IdLanguage"]), _ResourceClassification_LG);
            }

            return _ResourceClassifications_LG;
        }
        #endregion

        #region Write Functions
        public Entities.ResourceClassification_LG Add(DS.Entities.Language language, String name, String description)
        {
            try
            {
                //Acceso a datos para la opción de idioma
                DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();
                DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();

                using (TransactionScope _transactionScope = new TransactionScope())
                {
                    //ejecuta el insert a la base y devuelve el ID que le asigno en la bd            
                    _dbKnowledgeCollaboration.ResourceClassifications_LG_Create(_ResourceClassification.IdResourceClassification, language.IdLanguage, name, description);

                    //Log
                    _dbLog.Create("KC_ResourceClassifications_LG", "ResourceClassifications_LG", "Add", "IdResourceClassification=" + _ResourceClassification.IdResourceClassification + " and IdLanguage= '" + language.IdLanguage + "'", _Credential.User.IdPerson);

                    _transactionScope.Complete();

                    return new Entities.ResourceClassification_LG(name, description, language.IdLanguage);
                }
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
        public Entities.ResourceClassification_LG Modify(DS.Entities.Language language, String name, String description)
        {
            try
            {
                //Acceso a datos para la opción de idioma
                DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();
                DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();

                using (TransactionScope _transactionScope = new TransactionScope())
                {

                    _dbKnowledgeCollaboration.ResourceClassifications_LG_Update(_ResourceClassification.IdResourceClassification, language.IdLanguage, name, description);

                    _dbLog.Create("KC_ResourceClassifications_LG", "ResourceClassifications_LG", "Update", "IdResourceClassification=" + _ResourceClassification.IdResourceClassification + " and IdLanguage= '" + language.IdLanguage + "'", _Credential.User.IdPerson);

                    _transactionScope.Complete();

                    return new Entities.ResourceClassification_LG(name, description, language.IdLanguage);

                }
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

                    _dbKnowledgeCollaboration.ResourceClassifications_LG_Delete(_ResourceClassification.IdResourceClassification, language.IdLanguage);

                    _dbLog.Create("KC_ResourceClassifications_LG", "ResourceClassifications_LG", "Add", "IdResourceClassification=" + _ResourceClassification.IdResourceClassification + " and IdLanguage= '" + language.IdLanguage + "'", _Credential.User.IdPerson);

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
