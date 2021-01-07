using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PA.Collections
{
    internal class CalculateOfTransformationResults
    {
        #region Internal Properties
        private ICollectionItems _Datasource;
        private Credential _Credential;
        #endregion

        internal CalculateOfTransformationResults(ITransformer baseTransformer, Entities.CalculateOfTransformation transformation)
        {
            _Credential = transformation.Credential;
            switch (baseTransformer.ClassName)
            {
                case Common.ClassName.Measurement:
                    Entities.Measurement _measurement = new Collections.Measurements(transformation.Credential).Item(baseTransformer.IdObject);
                    _Datasource = new CalculationOfTransformationRead.ResultByMeasurement(_measurement, transformation);
                    break;
                case Common.ClassName.CalculateOfTransformation:
                    Entities.CalculateOfTransformation _transformationTransformer = new Collections.CalculateOfTransformations(transformation.Credential).Item(baseTransformer.IdObject);
                    _Datasource = new CalculationOfTransformationRead.ResultByTransformation(_transformationTransformer, transformation);
                    break;
                default:
                    break;
            }
        }

        internal CalculateOfTransformationResults(Entities.CalculateOfTransformation transformation)
        {
            _Credential = transformation.Credential;            
        }

        internal CalculateOfTransformationResults(Entities.Measurement measurement)
        {
            _Credential = measurement.Credential;
        }

        #region Read Functions
        internal IEnumerable<System.Data.Common.DbDataRecord> TransformValues()
        {
            IEnumerable<System.Data.Common.DbDataRecord> _records = _Datasource.getItems();

            return _records;
        }

        internal Double ResultTransformationByIndicator(Entities.Measurement measurement, Entities.Indicator indicator, DateTime? startDate, DateTime? endDate)
        {
            //Objeto de data layer para acceder a datos
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new DataAccess.PA.PerformanceAssessments();

            Double _result = 0;
            DateTime _startdate = startDate == null ? (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue : (DateTime)startDate;
            DateTime _endDate = endDate == null ? (DateTime)System.Data.SqlTypes.SqlDateTime.MaxValue : (DateTime)endDate;

            IEnumerable<System.Data.Common.DbDataRecord> _records = _dbPerformanceAssessments.CalculateOfTransformationMeasurementResults_ReadByIndicator(measurement.IdMeasurement, indicator.IdIndicator, _startdate, _endDate);

            foreach (System.Data.Common.DbDataRecord _dbRecord in _records)
            {
                _result = Convert.ToDouble(Common.Common.CastNullValues(_dbRecord["Result"], 0.0));
            }
            
            return _result;
        }

        internal Decimal ResultTransformationByIndicator(Entities.CalculateOfTransformation calculateOfTransformation, Entities.Indicator indicator, DateTime? startDate, DateTime? endDate)
        {
            //Objeto de data layer para acceder a datos
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new DataAccess.PA.PerformanceAssessments();

            Decimal _result = 0;
            DateTime _startdate = startDate == null ? (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue : (DateTime)startDate;
            DateTime _endDate = endDate == null ? (DateTime)System.Data.SqlTypes.SqlDateTime.MaxValue : (DateTime)endDate;

            IEnumerable<System.Data.Common.DbDataRecord> _records = _dbPerformanceAssessments.CalculateOfTransformationTransformationResults_ReadByIndicator(calculateOfTransformation.IdTransformation, indicator.IdIndicator, _startdate, _endDate);

            foreach (System.Data.Common.DbDataRecord _dbRecord in _records)
            {
                _result = Convert.ToDecimal(Common.Common.CastNullValues(_dbRecord["Result"],0));
            }

            return _result;
        }

        #endregion
        #region Write Functions
        internal void Add(Entities.CalculateOfTransformation transformation, ITransformer baseTransformer, Double transformationValue, DateTime transformationDate, DateTime startDate, DateTime endDate)
        {
            try
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new DataAccess.PA.PerformanceAssessments();

                Double _minuteValue = 0;

                if (transformation.Indicator.IsCumulative)
                {
                    //saca la diferencia de horas
                    TimeSpan _difTime = endDate.Subtract(startDate);
                    //lo pasa a minutos
                    Double _minutes = Convert.ToDouble(_difTime.TotalMinutes);
                    //saca el valor del minuto
                    _minuteValue = transformationValue / _minutes;
                }
                else
                {
                    _minuteValue = transformationValue;
                }
                //Construye el objeto base correspondiente y Ejecuta el insert 
                switch (baseTransformer.ClassName)
                {
                    case Common.ClassName.Measurement:
                        _dbPerformanceAssessments.CalculateOfTransformationMeasurementResults_Create(transformation.IdTransformation, baseTransformer.IdObject, transformationValue, transformationDate, startDate, endDate, _minuteValue);
                        break;
                    case Common.ClassName.CalculateOfTransformation:
                        _dbPerformanceAssessments.CalculateOfTransformationTransformationResults_Create(transformation.IdTransformation, baseTransformer.IdObject, transformationValue, transformationDate, startDate, endDate, _minuteValue);
                        break;
                    default:
                        break;
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
        internal void Remove(Entities.CalculateOfTransformation transformation)
        {
            try
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new DataAccess.PA.PerformanceAssessments();

                //Borra las que dependen de ella
                //borra la transformaciones donde participa
                foreach (PA.Entities.CalculateOfTransformation _calculate in transformation.Transformations.Values)
                {
                    this.Remove(_calculate);
                }
                
                foreach (PA.Entities.CalculateOfTransformation _calculate in new Collections.CalculateOfTransformations(_Credential).ItemsAsParameter(transformation).Values)
                {
                    this.Remove(_calculate);
                }


                //Borra las ejecuciones
                _dbPerformanceAssessments.CalculateOfTransformationMeasurementResults_Delete(transformation.IdTransformation);
                _dbPerformanceAssessments.CalculateOfTransformationTransformationResults_Delete(transformation.IdTransformation);
                
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

    }
}
