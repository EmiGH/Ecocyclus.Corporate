using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.PA.Collections
{
    public class Indicators_LG
    {
        #region Internal Properties
            private Credential _Credential;
            private Entities.Indicator _Indicator;
        #endregion

        internal Indicators_LG(Entities.Indicator indicator, Credential credential)
        {
            _Credential = credential;
            _Indicator = indicator;
        }

        #region Read Functions
        public Entities.Indicator_LG Item(DS.Entities.Language language)
        {
            //Acceso a datos para la opción de idioma
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            Entities.Indicator_LG _item = null;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.Indicators_LG_ReadById(_Indicator.IdIndicator, language.IdLanguage);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _item = new Entities.Indicator_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["Scope"]), Convert.ToString(_dbRecord["Limitation"]), Convert.ToString(_dbRecord["Definition"]));
            }
            return _item;
        }
        public Dictionary<String, Entities.Indicator_LG> Items()
        {
            //Acceso a datos para la opción de idioma
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            Dictionary<String, Entities.Indicator_LG> _indicators_LG = new Dictionary<String, Entities.Indicator_LG>();
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.Indicators_LG_ReadAll(_Indicator.IdIndicator);

            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                Entities.Indicator_LG _indicator_LG = new Entities.Indicator_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["Scope"]), Convert.ToString(_dbRecord["Limitation"]), Convert.ToString(_dbRecord["Definition"]));
                _indicators_LG.Add(Convert.ToString(_dbRecord["IdLanguage"]), _indicator_LG);
            }

            return _indicators_LG;
        }
        #endregion

        #region Write Functions
        public Entities.Indicator_LG Add(DS.Entities.Language language, String name, String description, String scope, String limitation, String definition)
        {
            try
            {
                //Acceso a datos para la opción de idioma
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();
                DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();

                using (TransactionScope _transactionScope = new TransactionScope())
                {
                    //ejecuta el insert a la base y devuelve el ID que le asigno en la bd            
                    _dbPerformanceAssessments.Indicators_LG_Create(_Indicator.IdIndicator, language.IdLanguage, name, description, scope,limitation,definition);

                    _dbLog.Create("PA_Indicators_LG", "Indicators_LG", "Add", "IdIndicator=" + _Indicator.IdIndicator + " and IdLanguage='" + language.IdLanguage + "'", _Credential.User.IdPerson);

                    _transactionScope.Complete();
                }
                return new Entities.Indicator_LG(language.IdLanguage, name, description, scope,limitation,definition);
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
        public Entities.Indicator_LG Modify(DS.Entities.Language language, String name, String description, String scope, String limitation, String definition)
        {
            try
            {
                //Acceso a datos para la opción de idioma
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();
                DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
                using (TransactionScope _transactionScope = new TransactionScope())
                {
                    _dbPerformanceAssessments.Indicators_LG_Update(_Indicator.IdIndicator, language.IdLanguage, name, description, scope,limitation,definition);

                    _dbLog.Create("PA_Indicators_LG", "Indicators_LG", "Add", "IdIndicator=" + _Indicator.IdIndicator + " and IdLanguage='" + language.IdLanguage + "'", _Credential.User.IdPerson);

                    _transactionScope.Complete();
                }
                return new Entities.Indicator_LG(language.IdLanguage, name, description, scope, limitation,definition);
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
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();
                DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
                using (TransactionScope _transactionScope = new TransactionScope())
                {
                    _dbPerformanceAssessments.Indicators_LG_Delete(_Indicator.IdIndicator, language.IdLanguage);

                    _dbLog.Create("PA_Indicators_LG", "Indicators_LG", "Add", "IdIndicator=" + _Indicator.IdIndicator + " and IdLanguage='" + language.IdLanguage + "'", _Credential.User.IdPerson);

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
