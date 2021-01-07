using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PA.Entities
{
    public class FormulaParameter
    {
        #region Internal Properties
        private Int64 _IdFormula;
        private Int64 _PositionParameter;
        private Int64 _IdIndicator;
        private Indicator _Indicator;
        private Int64 _IdMeasurementUnit;
        private String _ParameterName;
        private MeasurementUnit _MeasurementUnit;
        private Credential _Credential;
        #endregion

        #region External Properties
        public Int64 IdFormula
        {
           get{return _IdFormula;}
        }
        public Int64 PositionParameter
        {
            get { return _PositionParameter; }
        }
        public String ParameterName
        {
            get { return _ParameterName; }
        } 
        public Indicator Indicator
        {
            get
            {
                if (_Indicator == null)
                { _Indicator = new Collections.Indicators(_Credential).Item(_IdIndicator); }
                return _Indicator;
            }           
        }
        public MeasurementUnit MeasurementUnit
        {
            get
            {
                if (_MeasurementUnit == null) { _MeasurementUnit = new Collections.MeasurementUnits(_Credential).Item(_IdMeasurementUnit); }
                return _MeasurementUnit;
            }
        }
        #endregion

        internal FormulaParameter(Int64 idFormula, Int64 positionParameter, Int64 idIndicator, Int64 idMeasurementUnit, String parameterName, Credential credential)
        {
            _IdFormula = idFormula;
            _PositionParameter = positionParameter;
            _IdIndicator = idIndicator;
            _IdMeasurementUnit = idMeasurementUnit;
            _ParameterName = parameterName;
            _Credential = credential;
        }

        public void Modify(Int64 idIndicator, Int64 idMeasurementUnit, String parameterName)
        {
            try
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                //Modifico los datos de la base
                _dbPerformanceAssessments.FormulaParameters_Update(_IdFormula, _PositionParameter, idIndicator, idMeasurementUnit, parameterName, _Credential.User.Person.IdPerson);
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
