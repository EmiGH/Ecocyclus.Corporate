using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.PA.Collections
{
    public class IndicatorClassifications_LG
    {
        #region Internal Properties
        private Credential _Credential;
        private Entities.IndicatorClassification _IndicatorClassification; //El identificador del Indicator classification
        #endregion

        internal IndicatorClassifications_LG(Entities.IndicatorClassification indicatorClassification, Credential credential)
        {
            _Credential = credential;
            _IndicatorClassification = indicatorClassification;            
        }

        #region Read Functions
        public Entities.IndicatorClassification_LG Item(DS.Entities.Language language)
        {
            //Acceso a datos para la opción de idioma
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            Entities.IndicatorClassification_LG _IndicatorClassification_LG = null;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.IndicatorClassifications_LG_ReadById(_IndicatorClassification.IdIndicatorClassification, language.IdLanguage);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_IndicatorClassification_LG == null)
                {
                    _IndicatorClassification_LG = new Entities.IndicatorClassification_LG(Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["IdLanguage"]));
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                    {
                        return _IndicatorClassification_LG;
                    }
                }
                else
                {
                    return new Entities.IndicatorClassification_LG(Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["IdLanguage"]));
                }
            }
            return _IndicatorClassification_LG;

        }
        public Dictionary<String, Entities.IndicatorClassification_LG> Items()
        {
            //Acceso a datos para la opción de idioma
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            Dictionary<String, Entities.IndicatorClassification_LG> _IndicatorClassifications_LG = new Dictionary<String, Entities.IndicatorClassification_LG>();
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.IndicatorClassifications_LG_ReadAll(_IndicatorClassification.IdIndicatorClassification);

            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                Entities.IndicatorClassification_LG _IndicatorClassification_LG = new Entities.IndicatorClassification_LG(Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["IdLanguage"]));
                _IndicatorClassifications_LG.Add(Convert.ToString(_dbRecord["IdLanguage"]), _IndicatorClassification_LG);
            }

            return _IndicatorClassifications_LG;
        }
        #endregion
        
        #region Write Functions
        public Entities.IndicatorClassification_LG Add(DS.Entities.Language language, String name, String description)
        {
            try
            {
                //Acceso a datos para la opción de idioma
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();
                DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();

                using (TransactionScope _transactionScope = new TransactionScope())
                {
                    //ejecuta el insert a la base y devuelve el ID que le asigno en la bd            
                    _dbPerformanceAssessments.IndicatorClassifications_LG_Create(_IndicatorClassification.IdIndicatorClassification, language.IdLanguage, name, description);
                    //log
                    _dbLog.Create("PA_IndicatorClassifications_LG", "IndicatorClassifications_LG", "Add", "IdIndicatorClassification=" + _IndicatorClassification.IdIndicatorClassification + " and IdLanguage='" + language.IdLanguage + "'", _Credential.User.IdPerson);

                    _transactionScope.Complete();
                }

                return new Entities.IndicatorClassification_LG(name, description, language.IdLanguage);
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
        public Entities.IndicatorClassification_LG Modify(DS.Entities.Language language, String name, String description)
        {
            try
            {
                //Acceso a datos para la opción de idioma
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();
                DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();

                using (TransactionScope _transactionScope = new TransactionScope())
                {
                    _dbPerformanceAssessments.IndicatorClassifications_LG_Update(_IndicatorClassification.IdIndicatorClassification, language.IdLanguage, name, description);
                    //log
                    _dbLog.Create("PA_IndicatorClassifications_LG", "IndicatorClassifications_LG", "Modify", "IdIndicatorClassification=" + _IndicatorClassification.IdIndicatorClassification + " and IdLanguage='" + language.IdLanguage + "'", _Credential.User.IdPerson);

                    _transactionScope.Complete();
                }

                return new Entities.IndicatorClassification_LG(name, description, language.IdLanguage);
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
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();
                DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();

                using (TransactionScope _transactionScope = new TransactionScope())
                {
                    _dbPerformanceAssessments.IndicatorClassifications_LG_Delete(_IndicatorClassification.IdIndicatorClassification, language.IdLanguage);
                    //log
                    _dbLog.Create("PA_IndicatorClassifications_LG", "IndicatorClassifications_LG", "Remove", "IdIndicatorClassification=" + _IndicatorClassification.IdIndicatorClassification + " and IdLanguage='" + language.IdLanguage + "'", _Credential.User.IdPerson);

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
