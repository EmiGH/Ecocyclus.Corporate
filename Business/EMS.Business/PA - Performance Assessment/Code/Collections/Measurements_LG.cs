using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PA.Collections
{
    public class Measurements_LG
    {
        #region Internal Properties
            private Credential _Credential;
            private Entities.Measurement _Measurement;
        #endregion

        internal Measurements_LG(Entities.Measurement measurement)
        {
            _Credential = measurement.Credential;
            _Measurement = measurement;
        }

        #region Read Functions
            public Entities.Measurement_LG Item(String idLanguage)
            {
                //Acceso a datos para la opción de idioma
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                Entities.Measurement_LG _item = null;
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.Measurements_LG_ReadById(_Measurement.IdMeasurement, idLanguage);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    _item = new Entities.Measurement_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]));
                }
                return _item;
            }
            public Dictionary<String, Entities.Measurement_LG> Items()
        {
            //Acceso a datos para la opción de idioma
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            Dictionary<String, Entities.Measurement_LG> _measurements_LG = new Dictionary<String, Entities.Measurement_LG>();
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.Measurements_LG_ReadAll(_Measurement.IdMeasurement);

            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                Entities.Measurement_LG _measurement_LG = new Entities.Measurement_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]));
                _measurements_LG.Add(Convert.ToString(_dbRecord["IdLanguage"]), _measurement_LG);
            }

            return _measurements_LG;
        }
        #endregion

        #region Write Functions
            public Entities.Measurement_LG Add(String idLanguage, String name, String description)
            {
                try
                {
                    //Acceso a datos para la opción de idioma
                    DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                    //ejecuta el insert a la base y devuelve el ID que le asigno en la bd            
                    _dbPerformanceAssessments.Measurements_LG_Create(_Measurement.IdMeasurement, idLanguage, name, description);
                    return new Entities.Measurement_LG(idLanguage, name, description);
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
            public Entities.Measurement_LG Modify(String idLanguage, String name, String description)
            {
                try
                {
                    //Acceso a datos para la opción de idioma
                    DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                    _dbPerformanceAssessments.Measurements_LG_Update(_Measurement.IdMeasurement, idLanguage, name, description);
                    return new Entities.Measurement_LG(idLanguage, name, description);
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

                _dbPerformanceAssessments.Measurements_LG_Delete(_Measurement.IdMeasurement, idLanguage);
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
            public void Remove()
            {
                try
                {
                    //Acceso a datos para la opción de idioma
                    DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                    _dbPerformanceAssessments.Measurements_LG_Delete(_Measurement.IdMeasurement);
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
