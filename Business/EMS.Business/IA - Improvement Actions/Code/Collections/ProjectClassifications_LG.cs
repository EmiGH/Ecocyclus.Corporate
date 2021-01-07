using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.IA.Collections
{
    public class ProjectClassifications_LG
    {
        #region Internal Properties
            private Credential _Credential;
            private Int64 _IdProjectClassification; //El identificador del Resource classification
        #endregion

        internal ProjectClassifications_LG(Int64 idProjectClassification, Credential credential)
        {
            _Credential = credential;
            _IdProjectClassification = idProjectClassification;           
        }

        #region Read Functions
        public Entities.ProjectClassification_LG Item(String idLanguage)
        {
            //Acceso a datos para la opción de idioma
            DataAccess.IA.ImprovementActions _dbImprovementActions = new Condesus.EMS.DataAccess.IA.ImprovementActions();

            Entities.ProjectClassification_LG _ProjectClassification_LG = null;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbImprovementActions.ProjectClassifications_LG_ReadById(_IdProjectClassification, idLanguage);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_ProjectClassification_LG == null)
                {
                    _ProjectClassification_LG = new Entities.ProjectClassification_LG(Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["IdLanguage"]));
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                    {
                        return _ProjectClassification_LG;
                    }
                }
                else
                {
                    return new Entities.ProjectClassification_LG(Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["IdLanguage"]));
                }
            }
            return _ProjectClassification_LG;

        }
        public Dictionary<String, Entities.ProjectClassification_LG> Items()
        {
            DataAccess.IA.ImprovementActions _dbImprovementActions = new Condesus.EMS.DataAccess.IA.ImprovementActions();

            Dictionary<String, Entities.ProjectClassification_LG> _ProjectClassifications_LG = new Dictionary<String, Entities.ProjectClassification_LG>();
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbImprovementActions.ProjectClassifications_LG_ReadAll(_IdProjectClassification);

            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                Entities.ProjectClassification_LG _ProjectClassification_LG = new Entities.ProjectClassification_LG(Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["IdLanguage"]));
                _ProjectClassifications_LG.Add(Convert.ToString(_dbRecord["IdLanguage"]), _ProjectClassification_LG);
            }

            return _ProjectClassifications_LG;
        }
        #endregion

        #region Write Functions
        public Entities.ProjectClassification_LG Add(String idLanguage, String name, String description)
        {
            try
            {
                //Acceso a datos para la opción de idioma
                DataAccess.IA.ImprovementActions _dbImprovementActions = new Condesus.EMS.DataAccess.IA.ImprovementActions();
                DataAccess.Log.Log _dbLog = new DataAccess.Log.Log();

                if (_dbLog.GenericExist("IA_ProjectClassifications_LG", "Name = '" + name + "'"))
                {
                    throw new Exception(Common.Resources.Errors.DuplicatedDataRecord);
                }             
 
                using (TransactionScope _transactionScope = new TransactionScope())
                {
                    //ejecuta el insert a la base y devuelve el ID que le asigno en la bd            
                    _dbImprovementActions.ProjectClassifications_LG_Create(_IdProjectClassification, idLanguage, name, description);

                    _dbLog.Create("IA_ProjectClassifications_LG", "ProjectClassifications_LG", "Add", "IdProjectClassification = " + _IdProjectClassification  + " AND idLanguage ='" + idLanguage + "'", _Credential.User.IdPerson);

                    _transactionScope.Complete();

                    return new Entities.ProjectClassification_LG(name, description, idLanguage);
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
        public Entities.ProjectClassification_LG Modify(String idLanguage, String name, String description)
        {
            try
            {
                DataAccess.IA.ImprovementActions _dbImprovementActions = new Condesus.EMS.DataAccess.IA.ImprovementActions();
                DataAccess.Log.Log _dbLog = new DataAccess.Log.Log();

                if (_dbLog.GenericExist("IA_ProjectClassifications_LG", "Name = '" + name + "'"))
                {
                    throw new Exception(Common.Resources.Errors.DuplicatedDataRecord);
                }

                using (TransactionScope _transactionScope = new TransactionScope())
                {
                    //ejecuta el insert a la base y devuelve el ID que le asigno en la bd            
                    _dbImprovementActions.ProjectClassifications_LG_Update(_IdProjectClassification, idLanguage, name, description);

                    _dbLog.Create("IA_ProjectClassifications_LG", "ProjectClassifications_LG", "Update", "IdProjectClassification = " + _IdProjectClassification + " AND idLanguage ='" + idLanguage + "'", _Credential.User.IdPerson);

                    _transactionScope.Complete();

                    return new Entities.ProjectClassification_LG(name, description, idLanguage);
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
        public void Remove(String idLanguage)
        {
            //controla que no se borre el lenguage default
            if (_Credential.DefaultLanguage.IdLanguage == idLanguage)
            {
                throw new Exception(Common.Resources.Errors.RemoveDefaultCulture);
            }
            try
            {
                DataAccess.IA.ImprovementActions _dbImprovementActions = new Condesus.EMS.DataAccess.IA.ImprovementActions();
                DataAccess.Log.Log _dbLog = new DataAccess.Log.Log();

                using (TransactionScope _transactionScope = new TransactionScope())
                {
                    //ejecuta el insert a la base y devuelve el ID que le asigno en la bd            
                    _dbImprovementActions.ProjectClassifications_LG_Delete(_IdProjectClassification, idLanguage);

                    _dbLog.Create("IA_ProjectClassifications_LG", "ProjectClassifications_LG", "Delete", "IdProjectClassification = " + _IdProjectClassification + " AND idLanguage ='" + idLanguage + "'", _Credential.User.IdPerson);

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
