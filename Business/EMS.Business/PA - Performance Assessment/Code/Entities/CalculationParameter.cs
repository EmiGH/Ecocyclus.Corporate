using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PA.Entities
{
    public class CalculationParameter
    {
        #region Internal Properties
        private Int64 _IdCalculation;
        private Int64 _PositionParameter;
        private Int64 _IdMeasurementParameter;
        private String _parameterName;
        private Measurement _Measurement;
        private Calculation _Calculation;
        private Credential _Credential;
        #endregion

        #region External Properties
        public Int64 IdCalculation
        {
           get{return _IdCalculation;}
        }
        public Int64 PositionParameter
        {
            get { return _PositionParameter; }
        }
         public String ParameterName
        {
            get { return _parameterName; }
        } 
        public Measurement Measurement
        {
            get 
            {
                if (_Measurement == null) 
                {_Measurement = new Collections.Measurements(_Credential).Item(_IdMeasurementParameter);}
                return _Measurement;
            }
        }
        
        #endregion

        internal CalculationParameter(Int64 idCalculation, Int64 positionParameter, Int64 idMeasurementParameter, String parameterName, Credential credential)
        {
            _IdCalculation = idCalculation;
            _PositionParameter = positionParameter;
            _IdMeasurementParameter = idMeasurementParameter;
            _parameterName= parameterName;
            _Credential = credential;
        }

        public void Modify(Int64 idMeasurementParameter, String parameterName)
        {
            try
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                //Modifico los datos de la base
                _dbPerformanceAssessments.CalculationParameters_Update(_IdCalculation, _PositionParameter, idMeasurementParameter, parameterName, _Credential.User.Person.IdPerson);
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
    }
}
