using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PA.Collections
{
    public class CalculationScenarioTypes_LG
    {
        #region Internal Properties
            private Credential _Credential;
            private Int64 _IdScenarioType; 
        #endregion

        internal CalculationScenarioTypes_LG(Int64 idScenarioType, Credential credential)
        {
            _Credential = credential;
            _IdScenarioType = idScenarioType;
        }

        #region Read Functions
            public Entities.CalculationScenarioType_LG Item(String idLanguage)
            {
                //Acceso a datos para la opción de idioma
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                Entities.CalculationScenarioType_LG _item = null;
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.CalculationScenarioTypes_LG_ReadById(_IdScenarioType, idLanguage);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    _item = new Entities.CalculationScenarioType_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]));
                }
                return _item;
            }
            public Dictionary<String, Entities.CalculationScenarioType_LG> Items()
            {
                //Acceso a datos para la opción de idioma
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                Dictionary<String, Entities.CalculationScenarioType_LG> _oParticipationTypes_LG = new Dictionary<String, Entities.CalculationScenarioType_LG>();
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.CalculationScenarioTypes_LG_ReadAll(_IdScenarioType);

                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    Entities.CalculationScenarioType_LG _oCalculationScenarioType_LG = new Entities.CalculationScenarioType_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]));
                    _oParticipationTypes_LG.Add(Convert.ToString(_dbRecord["IdLanguage"]), _oCalculationScenarioType_LG);
                }

                return _oParticipationTypes_LG;
            }
            #endregion

        #region Write Functions
            public Entities.CalculationScenarioType_LG Add(String idLanguage, String name, String description)
            {
                try
                {
                    //Acceso a datos para la opción de idioma
                    DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                    //ejecuta el insert a la base y devuelve el ID que le asigno en la bd            
                    _dbPerformanceAssessments.CalculationScenarioTypes_LG_Create(_IdScenarioType, idLanguage, name, description, _Credential.User.IdPerson);
                    return new Entities.CalculationScenarioType_LG(idLanguage, name, description);
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
            public Entities.CalculationScenarioType_LG Modify(String idLanguage, String name, String description)
            {
                try
                {
                    //Acceso a datos para la opción de idioma
                    DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                    _dbPerformanceAssessments.CalculationScenarioTypes_LG_Update(_IdScenarioType, idLanguage, name, description, _Credential.User.IdPerson);
                    return new Entities.CalculationScenarioType_LG(idLanguage, name, description);
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
                    DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();
                    
                    _dbPerformanceAssessments.CalculationScenarioTypes_LG_Delete(_IdScenarioType, idLanguage, _Credential.User.IdPerson);
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
