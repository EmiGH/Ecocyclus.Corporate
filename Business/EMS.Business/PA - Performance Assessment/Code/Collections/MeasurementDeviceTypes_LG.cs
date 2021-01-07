using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PA.Collections
{
    public class MeasurementDeviceTypes_LG
    {
        #region Internal Properties
            private Credential _Credential;
            private Int64 _IdMeasurementDeviceType;
        #endregion

            internal MeasurementDeviceTypes_LG(Int64 idMeasurementDeviceType, Credential credential)
        {
            _Credential = credential;
            _IdMeasurementDeviceType = idMeasurementDeviceType;
        }

        #region Read Functions
            public Entities.MeasurementDeviceType_LG Item(String idLanguage)
        {
            //Acceso a datos para la opción de idioma
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            Entities.MeasurementDeviceType_LG _item = null;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.MeasurementDeviceTypes_LG_ReadById(_IdMeasurementDeviceType, idLanguage);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _item = new Entities.MeasurementDeviceType_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]));
            }
            return _item;
        }
            public Dictionary<String, Entities.MeasurementDeviceType_LG> Items()
        {
            //Acceso a datos para la opción de idioma
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            Dictionary<String, Entities.MeasurementDeviceType_LG> _measurementDeviceTypes_LG = new Dictionary<String, Entities.MeasurementDeviceType_LG>();
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.MeasurementDeviceTypes_LG_ReadAll(_IdMeasurementDeviceType);

            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                Entities.MeasurementDeviceType_LG _measurementDeviceType_LG = new Entities.MeasurementDeviceType_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]));
                _measurementDeviceTypes_LG.Add(Convert.ToString(_dbRecord["IdLanguage"]), _measurementDeviceType_LG);
            }

            return _measurementDeviceTypes_LG;
        }
        #endregion

        #region Write Functions
            public Entities.MeasurementDeviceType_LG Add(String idLanguage, String name, String description)
        {
            try
            {
                //Acceso a datos para la opción de idioma
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                //ejecuta el insert a la base y devuelve el ID que le asigno en la bd            
                _dbPerformanceAssessments.MeasurementDeviceTypes_LG_Create(_IdMeasurementDeviceType, idLanguage, name, description, _Credential.User.Person.IdPerson);
                return new Entities.MeasurementDeviceType_LG(idLanguage, name, description);
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
            public Entities.MeasurementDeviceType_LG Modify(String idLanguage, String name, String description)
        {
            try
            {
                //Acceso a datos para la opción de idioma
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                _dbPerformanceAssessments.MeasurementDeviceTypes_LG_Update(_IdMeasurementDeviceType, idLanguage, name, description, _Credential.User.Person.IdPerson);
                return new Entities.MeasurementDeviceType_LG(idLanguage, name, description);
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

                _dbPerformanceAssessments.MeasurementDeviceTypes_LG_Delete(_IdMeasurementDeviceType, idLanguage, _Credential.User.Person.IdPerson);
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
