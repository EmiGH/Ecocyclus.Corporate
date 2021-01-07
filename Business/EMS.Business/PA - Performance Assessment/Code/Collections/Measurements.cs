using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PA.Collections
{
    internal class Measurements
    {
        #region Internal Properties
            private Credential _Credential;
        #endregion

        internal Measurements(Credential credential)
        {
            _Credential = credential;
        }

        #region Read Functions
        /// <summary>
        /// Devuelve la mediciones para un activity, cuando se le pasa un process
        /// </summary>
        /// <param name="activity"></param>
        /// <param name="process"></param>
        /// <returns></returns>
        internal Dictionary<Int64, Entities.Measurement> Items(PA.Entities.AccountingActivity activity, PF.Entities.ProcessGroupProcess process)
        {
            //Coleccion para devolver los ExtendedProperty
            Dictionary<Int64, Entities.Measurement> _items = new Dictionary<Int64, Entities.Measurement>();

            //Objeto de data layer para acceder a datos
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            //Traigo los datos de la base
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.Measurements_ReadByActivityAndProcess(activity.IdActivity, process.IdProcess, _Credential.CurrentLanguage.IdLanguage);

            Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdMeasurement", _Credential).Filter();

            foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
            {
                //Declara e instancia  
                Entities.Measurement _item = new MeasurementFactory().CreateMeasurement(Convert.ToInt64(_dbRecord["IdMeasurement"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurementDevice"], 0)),  Convert.ToInt64(_dbRecord["IdIndicator"]), Convert.ToInt64(_dbRecord["TimeUnitFrequency"]), Convert.ToInt32(_dbRecord["Frequency"]), Convert.ToInt64(_dbRecord["IdMeasurementUnit"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToBoolean(_dbRecord["IsCumulative"]), Convert.ToBoolean(_dbRecord["IsRegressive"]), Convert.ToBoolean(_dbRecord["IsRelevant"]), Convert.ToString(_dbRecord["IdLanguage"]), _Credential, Convert.ToString(_dbRecord["Source"]), Convert.ToString(_dbRecord["frequencyAtSource"]), Convert.ToDecimal(Common.Common.CastNullValues(_dbRecord["uncertainty"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdQuality"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMethodology"], 0)));
                //Lo agrego a la coleccion
                _items.Add(_item.IdMeasurement, _item);
            }
            return _items;
        }
        internal Entities.Measurement Item(Int64 idMeasurement)
        {
            //Objeto de data layer para acceder a datos
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            Entities.Measurement _measurement = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.Measurements_ReadById(idMeasurement, _Credential.CurrentLanguage.IdLanguage);

            Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdMeasurement", _Credential).Filter();
            
            foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
            {
                _measurement = new MeasurementFactory().CreateMeasurement(Convert.ToInt64(_dbRecord["IdMeasurement"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurementDevice"], 0)),  Convert.ToInt64(_dbRecord["IdIndicator"]), Convert.ToInt64(_dbRecord["TimeUnitFrequency"]), Convert.ToInt32(_dbRecord["Frequency"]), Convert.ToInt64(_dbRecord["IdMeasurementUnit"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToBoolean(_dbRecord["IsCumulative"]), Convert.ToBoolean(_dbRecord["IsRegressive"]), Convert.ToBoolean(_dbRecord["IsRelevant"]), Convert.ToString(_dbRecord["IdLanguage"]), _Credential, Convert.ToString(_dbRecord["Source"]), Convert.ToString(_dbRecord["frequencyAtSource"]), Convert.ToDecimal(Common.Common.CastNullValues(_dbRecord["uncertainty"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdQuality"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMethodology"], 0)));
            }
            return _measurement;
        }
        internal Dictionary<Int64, Entities.Measurement> Items()
        {
            //Coleccion para devolver los ExtendedProperty
            Dictionary<Int64, Entities.Measurement> _items = new Dictionary<Int64, Entities.Measurement>();

            //Objeto de data layer para acceder a datos
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            //Traigo los datos de la base
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.Measurements_ReadAll(_Credential.CurrentLanguage.IdLanguage);

            Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdMeasurement", _Credential).Filter();

            foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
            {
                //Declara e instancia  
                Entities.Measurement _item = new MeasurementFactory().CreateMeasurement(Convert.ToInt64(_dbRecord["IdMeasurement"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurementDevice"], 0)), Convert.ToInt64(_dbRecord["IdIndicator"]), Convert.ToInt64(_dbRecord["TimeUnitFrequency"]), Convert.ToInt32(_dbRecord["Frequency"]), Convert.ToInt64(_dbRecord["IdMeasurementUnit"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToBoolean(_dbRecord["IsCumulative"]), Convert.ToBoolean(_dbRecord["IsRegressive"]), Convert.ToBoolean(_dbRecord["IsRelevant"]), Convert.ToString(_dbRecord["IdLanguage"]), _Credential, Convert.ToString(_dbRecord["Source"]), Convert.ToString(_dbRecord["frequencyAtSource"]), Convert.ToDecimal(Common.Common.CastNullValues(_dbRecord["uncertainty"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdQuality"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMethodology"], 0)));
                //Lo agrego a la coleccion
                _items.Add(_item.IdMeasurement, _item);
            }
            return _items;
        }
        internal Dictionary<Int64, Entities.Measurement> Items(Entities.Indicator indicator)
        {
            Dictionary<Int64, Entities.Measurement> _items = new Dictionary<Int64, Entities.Measurement>();

            //Objeto de data layer para acceder a datos
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            //Traigo los datos de la base
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.Measurements_ReadByIndicator(indicator.IdIndicator, _Credential.CurrentLanguage.IdLanguage);

            Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdMeasurement", _Credential).Filter();

            foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
            {
                //Declara e instancia  
                Entities.Measurement _item = new MeasurementFactory().CreateMeasurement(Convert.ToInt64(_dbRecord["IdMeasurement"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurementDevice"], 0)),  Convert.ToInt64(_dbRecord["IdIndicator"]), Convert.ToInt64(_dbRecord["TimeUnitFrequency"]), Convert.ToInt32(_dbRecord["Frequency"]), Convert.ToInt64(_dbRecord["IdMeasurementUnit"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToBoolean(_dbRecord["IsCumulative"]), Convert.ToBoolean(_dbRecord["IsRegressive"]), Convert.ToBoolean(_dbRecord["IsRelevant"]), Convert.ToString(_dbRecord["IdLanguage"]), _Credential, Convert.ToString(_dbRecord["Source"]), Convert.ToString(_dbRecord["frequencyAtSource"]), Convert.ToDecimal(Common.Common.CastNullValues(_dbRecord["uncertainty"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdQuality"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMethodology"], 0)));
                //Lo agrego a la coleccion
                _items.Add(_item.IdMeasurement, _item);
            }
            return _items;
        }
        internal Dictionary<Int64, Entities.Measurement> Items(GIS.Entities.Facility facility, Entities.AccountingScope accountingScope, Entities.AccountingActivity accountingActivity)
        {
            Dictionary<Int64, Entities.Measurement> _items = new Dictionary<Int64, Entities.Measurement>();

            //Objeto de data layer para acceder a datos
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            //Traigo los datos de la base
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.Measurements_ReadByFacilityForScope(accountingScope.IdScope, facility.IdFacility,  accountingActivity.IdActivity,_Credential.CurrentLanguage.IdLanguage);

            Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdMeasurement", _Credential).Filter();
        
            foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
            {
                //Declara e instancia  
                Entities.Measurement _item = new MeasurementFactory().CreateMeasurement(Convert.ToInt64(_dbRecord["IdMeasurement"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurementDevice"], 0)),  Convert.ToInt64(_dbRecord["IdIndicator"]), Convert.ToInt64(_dbRecord["TimeUnitFrequency"]), Convert.ToInt32(_dbRecord["Frequency"]), Convert.ToInt64(_dbRecord["IdMeasurementUnit"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToBoolean(_dbRecord["IsCumulative"]), Convert.ToBoolean(_dbRecord["IsRegressive"]), Convert.ToBoolean(_dbRecord["IsRelevant"]), Convert.ToString(_dbRecord["IdLanguage"]), _Credential, Convert.ToString(_dbRecord["Source"]), Convert.ToString(_dbRecord["frequencyAtSource"]), Convert.ToDecimal(Common.Common.CastNullValues(_dbRecord["uncertainty"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdQuality"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMethodology"], 0)));
                //Lo agrego a la coleccion
                _items.Add(_item.IdMeasurement, _item);
            }
            return _items;
        }
        internal Double OperateValue(Entities.CalculateOfTransformation transformation, Entities.Measurement measurement, DateTime startDate, DateTime endDate)
        {
            try
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                Double _operateValue = Double.MinValue;

                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.Measurements_ReadOperateValue(measurement.IdMeasurement, measurement.Indicator.IsCumulative, startDate, endDate);

                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    System.TimeSpan diffResult = endDate.Subtract(startDate);
                    //Si la diferencia de minutos pedidos, noes = a las traidas tira una exception
                    if (Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["Minut"], 0)) >= diffResult.TotalMinutes)
                    {
                        _operateValue = Convert.ToDouble(_dbRecord["Value"]);
                    }
                    else
                    {
                        //le pongo minvalue parque no encontro todos los valores para las fechas pedias
                        throw new Exception("No hay valores o no estan todos los valores de la medicion para las Fechas Pedidas " + startDate + " - " + endDate);
                    }
                }
                return _operateValue;
            }
            catch (Exception ex)
            {
                //DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new DataAccess.PA.PerformanceAssessments();
                //_dbPerformanceAssessments.CalculateOfTransformationErrors_Create(transformation.IdTransformation, false, ex.Message);
                return Double.MinValue;
            }
        }
        #endregion

        #region Write Functions
        internal Entities.Measurement Add(Entities.MeasurementDevice measurementDevice, List<Entities.ParameterGroup> parametersGroups, 
            Entities.Indicator indicator, String name, String description, PF.Entities.TimeUnit timeUnitFrequency, Int32 frequency, 
            PA.Entities.MeasurementUnit measurementUnit, Boolean isRegressive, Boolean isRelevant, String source, 
            String frequencyAtSource, Decimal uncertainty, Entities.Quality quality, Entities.Methodology methodology)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                    Int64 _idMeasurementDevice = measurementDevice == null ? 0 : measurementDevice.IdMeasurementDevice;
                    Int64 _idQuality = quality == null ? 0 : quality.IdQuality;
                    Int64 _idMethodology = methodology == null ? 0 : methodology.IdMethodology;
                    //Ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                    Int64 _idMeasurement = _dbPerformanceAssessments.Measurements_Create(_idMeasurementDevice, indicator.IdIndicator, timeUnitFrequency.IdTimeUnit, frequency, measurementUnit.IdMeasurementUnit, isRegressive, isRelevant, source, frequencyAtSource, uncertainty, _idQuality, _idMethodology);

                    foreach (Entities.ParameterGroup _pg in parametersGroups)
                    {
                        _dbPerformanceAssessments.ParameterGroups_Create(_idMeasurement, _pg.IdParameterGroup, _pg.Indicator.IdIndicator);
                    }

                    _dbPerformanceAssessments.Measurements_LG_Create(_idMeasurement, _Credential.DefaultLanguage.IdLanguage, name, description);
                    //Log
                    DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();                    
                    _dbLog.Create("PA_Measurements", "Measurements", "Add", "IdMeasurement=" + _idMeasurement, _Credential.User.IdPerson);
                    //Devuelvo el objeto creado
                    return new MeasurementFactory().CreateMeasurement(_idMeasurement, _idMeasurementDevice, indicator.IdIndicator, timeUnitFrequency.IdTimeUnit, frequency, measurementUnit.IdMeasurementUnit, name, description, indicator.IsCumulative, isRegressive, isRelevant, _Credential.DefaultLanguage.IdLanguage, _Credential, source, frequencyAtSource, uncertainty, _idQuality, _idMethodology);
                    
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
            internal void Remove(Entities.Measurement measurement )
            {
                try
                {
                    measurement.Remove();
                    //Objeto de data layer para acceder a datos
                    DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                    //borra la relacion con los exel
                    _dbPerformanceAssessments.ConfigurationExcelFiles_DeleteRelationshipByMeasurement(measurement.IdMeasurement);
                    //borra las relaciones que tiene como operador de los calculos
                    _dbPerformanceAssessments.CalculateOfTransformationParameterMeasurements_DeleteByMeasurement(measurement.IdMeasurement);
                    //borra la relacion con parameter group
                    _dbPerformanceAssessments.ParameterGroups_DeleteByMeasurement(measurement.IdMeasurement);

                    _dbPerformanceAssessments.Measurements_LG_Delete(measurement.IdMeasurement);
                    //Borrar de la base de datos
                    _dbPerformanceAssessments.Measurements_Delete(measurement.IdMeasurement);
                    //Log
                    DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
                    _dbLog.Create("PA_Measurements", "Measurements", "Remove", "IdMeasurement=" + measurement.IdMeasurement, _Credential.User.IdPerson);
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
            internal void Modify(Entities.Measurement measurement, Entities.MeasurementDevice measurementDevice, List<Entities.ParameterGroup> parametersGroups,
            Entities.Indicator indicator, String name, String description, PF.Entities.TimeUnit timeUnitFrequency, Int32 frequency,
            PA.Entities.MeasurementUnit measurementUnit, Boolean isRegressive, Boolean isRelevant, String source,
            String frequencyAtSource, Decimal uncertainty, Entities.Quality quality, Entities.Methodology methodology)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();
                    Int64 _idMeasurementDevice = measurementDevice == null ? 0 : measurementDevice.IdMeasurementDevice;
                    Int64 _idQuality = quality == null ? 0 : quality.IdQuality;
                    Int64 _idMethodology = methodology == null ? 0 : methodology.IdMethodology;
                    //Modifico los datos de la base
                    _dbPerformanceAssessments.Measurements_Update(measurement.IdMeasurement, _idMeasurementDevice, indicator.IdIndicator, measurementUnit.IdMeasurementUnit, timeUnitFrequency.IdTimeUnit, frequency, isRelevant, source, frequencyAtSource, uncertainty, _idQuality, _idMethodology);

                    _dbPerformanceAssessments.Measurements_LG_Update(measurement.IdMeasurement, _Credential.DefaultLanguage.IdLanguage, name, description);

                    _dbPerformanceAssessments.ParameterGroups_DeleteByMeasurement(measurement.IdMeasurement);

                    foreach (Entities.ParameterGroup _pg in parametersGroups)
                    {
                        _dbPerformanceAssessments.ParameterGroups_Create(measurement.IdMeasurement, _pg.IdParameterGroup, _pg.Indicator.IdIndicator);
                    }

                    //Log
                    DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
                    _dbLog.Create("PA_Measurements", "Measurements", "Modify", "IdMeasurement=" + measurement.IdMeasurement, _Credential.User.IdPerson);
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
        #endregion
    }
}
