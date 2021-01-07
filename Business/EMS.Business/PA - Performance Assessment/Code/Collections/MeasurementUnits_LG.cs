using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PA.Collections
{
    public class MeasurementUnits_LG
    {
        #region Internal Properties
            private Credential _Credential;
            private Int64 _IdMeasurementUnit;
        #endregion

        internal MeasurementUnits_LG(Int64 idMeasurementUnit, Credential credential)
        {
            _Credential = credential;
            _IdMeasurementUnit = idMeasurementUnit;
        }

        #region Read Functions
            public Entities.MeasurementUnit_LG Item(String idLanguage)
            {
                //Acceso a datos para la opción de idioma
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                Entities.MeasurementUnit_LG _item = null;
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.MeasurementUnits_LG_ReadById(_IdMeasurementUnit, idLanguage);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    _item = new Entities.MeasurementUnit_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]));
                }
                return _item;
            }
            public Dictionary<String, Entities.MeasurementUnit_LG> Items()
        {
            //Acceso a datos para la opción de idioma
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            Dictionary<String, Entities.MeasurementUnit_LG> _measurementUnits_LG = new Dictionary<String, Entities.MeasurementUnit_LG>();
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.MeasurementUnits_LG_ReadAll(_IdMeasurementUnit);

            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                Entities.MeasurementUnit_LG _measurementUnit_LG = new Entities.MeasurementUnit_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]));
                _measurementUnits_LG.Add(Convert.ToString(_dbRecord["IdLanguage"]), _measurementUnit_LG);
            }

            return _measurementUnits_LG;
        }
        #endregion

        #region Write Functions
            public Entities.MeasurementUnit_LG Add(String idLanguage, String name, String description)
        {
            try
            {
                //Acceso a datos para la opción de idioma
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                //ejecuta el insert a la base y devuelve el ID que le asigno en la bd            
                _dbPerformanceAssessments.MeasurementUnits_LG_Create(_IdMeasurementUnit, idLanguage, name, description, _Credential.User.Person.IdPerson);
                return new Entities.MeasurementUnit_LG(idLanguage, name, description);
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
            public Entities.MeasurementUnit_LG Modify(String idLanguage, String name, String description)
        {
            try
            {
                //Acceso a datos para la opción de idioma
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                _dbPerformanceAssessments.MeasurementUnits_LG_Update(_IdMeasurementUnit, idLanguage, name, description, _Credential.User.Person.IdPerson);
                return new Entities.MeasurementUnit_LG(idLanguage, name, description);
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
            if (_Credential.CurrentLanguage.IdLanguage == idLanguage)
            {
                throw new Exception(Common.Resources.Errors.RemoveDefaultCulture);
            }
            try
            {
                //Acceso a datos para la opción de idioma
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                _dbPerformanceAssessments.MeasurementUnits_LG_Delete(_IdMeasurementUnit, idLanguage, _Credential.User.Person.IdPerson);
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
