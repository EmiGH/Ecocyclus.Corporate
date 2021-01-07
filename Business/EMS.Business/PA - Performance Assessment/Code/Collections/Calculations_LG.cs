using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PA.Collections
{
    public class Calculations_LG
    {
        #region Internal Properties
            private Credential _Credential;
            private Int64 _IdCalculation;
        #endregion

            internal Calculations_LG(Int64 idCalculation, Credential credential)
        {
            _Credential = credential;
            _IdCalculation = idCalculation;
        }

        #region Read Functions
            public Entities.Calculation_LG Item(String idLanguage)
            {
                //Acceso a datos para la opción de idioma
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                Entities.Calculation_LG _item = null;
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.Calculations_LG_ReadById(_IdCalculation, idLanguage);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    _item = new Entities.Calculation_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]));
                }
                return _item;
            }
            public Dictionary<String, Entities.Calculation_LG> Items()
            {
                //Acceso a datos para la opción de idioma
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                Dictionary<String, Entities.Calculation_LG> _calculations_LG = new Dictionary<String, Entities.Calculation_LG>();
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.Calculations_LG_ReadAll(_IdCalculation);

                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    Entities.Calculation_LG _calculation_LG = new Entities.Calculation_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["name"]), Convert.ToString(_dbRecord["Description"]));
                    _calculations_LG.Add(Convert.ToString(_dbRecord["IdLanguage"]), _calculation_LG);
                }

                return _calculations_LG;
            }
        #endregion

        #region Write Functions
            public Entities.Calculation_LG Add(String idLanguage, String name, String description)
            {
                try
                {
                    //Acceso a datos para la opción de idioma
                    DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                    //ejecuta el insert a la base y devuelve el ID que le asigno en la bd            
                    _dbPerformanceAssessments.Calculations_LG_Create(_IdCalculation, idLanguage, name, description, _Credential.User.Person.IdPerson);
                    return new Entities.Calculation_LG(idLanguage,name, description);
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
            public Entities.Calculation_LG Modify(String idLanguage,String name, String description)
            {
                try
                {
                    //Acceso a datos para la opción de idioma
                    DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();
                    
                    _dbPerformanceAssessments.Calculations_LG_Update(_IdCalculation, idLanguage, name, description, _Credential.User.Person.IdPerson);
                    return new Entities.Calculation_LG(idLanguage,name, description);
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

                    _dbPerformanceAssessments.Calculations_LG_Delete(_IdCalculation, idLanguage, _Credential.User.Person.IdPerson);
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
