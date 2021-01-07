using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Transactions;
using System.Data.SqlClient;
using System.Data;

namespace Condesus.EMS.Business.PA.Entities
{
    public class Calculation : IExtendedProperty
    {
        #region Internal Properties
        private Int64 _IdCalculation;    
        private Int64 _IdFormula;
        private DateTime _CreationDate;
        private Int64 _IdMeasurementUnit;
        private MeasurementUnit _MeasurementUnit;
        private Int64 _IdTimeUnitFrequency;
        private Int32 _Frequency;
        private Decimal _LastResult;        
        private DateTime _DateLastResult;
        private Boolean _IsRelevant;
        private Credential _Credential;
        private Dictionary<Int64, CalculationParameter> _CalculationParameters;
        private Calculation_LG _LanguageOption;
        private Collections.Calculations_LG _LanguagesOptions;
        private Formula _Formula;
        private Dictionary<Int64, PF.Entities.ProcessGroupProcess> _AssociatedProcess;
        #endregion
        
        #region External Properties
        internal Credential Credential
        {
            get { return _Credential; }
        }
        public Int64 IdCalculation
        {
            get { return _IdCalculation; }        
        }
        public Int64 IdTimeUnitFrequency
        {
            get { return _IdTimeUnitFrequency; }
        }
        public Int32 Frequency
        {
            get { return _Frequency; }
        }
        public Decimal LastResult
        {
            get { return _LastResult; }
        }
        public DateTime DateLastResult
        {
            get { return _DateLastResult; }
        }
        public Boolean IsRelevant
        {
            get { return _IsRelevant; }
        }
        public Formula Formula
        {
           get
           {
               if (_Formula == null)
               { _Formula = new Collections.Formulas(_Credential).Item(_IdFormula); }
               return _Formula;
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
        public DateTime CreationDate
        {
            get { return _CreationDate; }
        }

        public Calculation_LG LanguageOption
        {
            get{return _LanguageOption;}
        }
        public Collections.Calculations_LG LanguagesOptions
        {
            get
            {
                if (_LanguagesOptions == null)
                { _LanguagesOptions = new Collections.Calculations_LG(_IdCalculation, _Credential); }
                return _LanguagesOptions;
            }
        }

        #region TimeUnitFrequency
        public PF.Entities.TimeUnit TimeUnitFrequency
        {
            get { return new PF.Collections.TimeUnits(_Credential).Item(_IdTimeUnitFrequency); }
        }
        #endregion

        #region Calculations parameters
        public PA.Entities.CalculationParameter CalculationParameter(Int64 positionParameter, Int64 idMeasurementParameter, String parameterName)
        {
            return new PA.Entities.CalculationParameter(_IdCalculation, positionParameter, idMeasurementParameter, parameterName, _Credential);
        }
        public Dictionary<Int64, CalculationParameter> CalculationParameters
        {
            get
            {
                if (_CalculationParameters == null)
                { _CalculationParameters = new Collections.CalculationParameters(_Credential).Items(_IdCalculation); }
                return _CalculationParameters;
            }
        }
        public void CalculationParametersAdd(Int64 positionParameters, PA.Entities.Measurement measurementParameter, String parameterName)
        {
            new PA.Collections.CalculationParameters(_Credential).Add(this, positionParameters, measurementParameter, parameterName);
        }                
        public void CalculationParameterRemove(Int64 positionParameter)
        {
            new PA.Collections.CalculationParameters(_Credential).Remove(_IdCalculation, positionParameter);
        }
        #endregion

        #region Projects
        public Dictionary<Int64, PF.Entities.ProcessGroupProcess> AssociatedProcess
        {
            get 
            {
                if (_AssociatedProcess == null)
                { _AssociatedProcess = new PF.Collections.ProcessGroupProcesses(_Credential).ItemsByCalcualtion(this);}
                return _AssociatedProcess;
            }
        }
        #endregion

        #region Calculation Estimated
            public PA.Entities.CalculationEstimated CalculationEstimated(Int64 idEstimated)
            {
                return new PA.Collections.CalculationEstimates(_Credential).Item(_IdCalculation, idEstimated);
            }
            public List<PA.Entities.CalculationEstimated> CalculationEstimates
                {
                    get
                    {
                        { return new PA.Collections.CalculationEstimates(_Credential).ItemsByCalculation(this); }
                    }
                }
            public PA.Entities.CalculationEstimated CalculationEstimatesAdd(DateTime startDate, DateTime endDate, PA.Entities.CalculationScenarioType calculationScenarioType, Decimal value)
            {
                return new PA.Collections.CalculationEstimates(_Credential).Add(this, startDate, endDate, calculationScenarioType, value);
            }
            public void CalculationEstimatesRemove(Int64 idEstimated)
            {
                new PA.Collections.CalculationEstimates(_Credential).Remove(this, idEstimated);
            }
            public Decimal Estimate(DateTime currentCampaignStartDate, PA.Entities.CalculationScenarioType calculationScenarioType)
            {
                Decimal _result = 0;

                List<PA.Entities.CalculationEstimated> _calculationEstimatedByScenarioType = new PA.Collections.CalculationEstimates(_Credential).ItemsByScenarioType(this, calculationScenarioType);
                //Recorre todas las estimaciones para el escenario indicado.
                //Debe ir sumando todas las estimaciones.
                foreach (PA.Entities.CalculationEstimated _calculationEstimated in _calculationEstimatedByScenarioType)
                {
                    DateTime _startDateReal;
                    DateTime _endDateReal;
                    Int32 _daysCampaign;
                    Int32 _daysEstimated;
                    Decimal _estimatedByDay;
                    
                    //Obtiene la cantidad de dias de la Estimacion.
                    _daysCampaign = _calculationEstimated.EndDate.Subtract(_calculationEstimated.StartDate).Days;
                    _estimatedByDay = _calculationEstimated.Value / _daysCampaign;

                    //Ahora va a obtener la cantidad de dias que realmente impactan en esta estimacion.
                    //Si la fecha de inicio de la estimacion es mas actual que la del periodo, tomo la fecha de la estimacion, sino la del periodo.
                    if (_calculationEstimated.StartDate >= currentCampaignStartDate)
                        { _startDateReal = _calculationEstimated.StartDate; }
                    else
                        { _startDateReal = currentCampaignStartDate; }

                    //Si la fecha fin de la estimacion es mas grande que la actual, tomo la fecha de hoy, sino la de la estimacion.
                    if (_calculationEstimated.EndDate >= DateTime.Now)
                        { _endDateReal = DateTime.Now; }
                    else
                        { _endDateReal = _calculationEstimated.EndDate; }

                    //Para obtener la cantidad de dias, uso el subtract y pregunto por la cantidad de dias. (el famoso TimeSpan...)
                    _daysEstimated = _endDateReal.Subtract(_startDateReal).Days;
                    //La diferencia en dias siempre tiene que ser positiva...sino es que no hay fechas a tener en cuenta.
                    if (_daysEstimated >= 0)
                    {
                        //Ahora que tengo cuantos dias realmente se deben aplicar, multiplico el valor dia con la cantidad de dias.
                        _result += _estimatedByDay * _daysEstimated;
                    }
                    else
                    {
                        _result += 0;
                    }
                }
                //Retorna el resultado.
                return _result;
            }
        #endregion

        #region Calculation Certificated
            public PA.Entities.CalculationCertificated CalculationCertificated(Int64 idCertificated)
            {
                return new PA.Collections.CalculationCertificates(_Credential).Item(_IdCalculation, idCertificated);
            }
            public List<PA.Entities.CalculationCertificated> CalculationCertificates
            {
                get
                {
                    { return new PA.Collections.CalculationCertificates(_Credential).ItemsByCalculation(this); }
                }
            }
            public PA.Entities.CalculationCertificated CalculationCertificatesAdd(DateTime startDate, DateTime endDate, Decimal value, Int64 idOrganizationDOE)
            {
                return new PA.Collections.CalculationCertificates(_Credential).Add(this, startDate, endDate, value, idOrganizationDOE);
            }
            public void CalculationCertificatesRemove(Int64 idCertificated)
            {
                new PA.Collections.CalculationCertificates(_Credential).Remove(this, idCertificated);
            }
        #endregion

        #region Extended Properties
            private List<EP.Entities.ExtendedPropertyValue> _ExtendedPropertyValue; //puntero a extended properties          
            public List<EP.Entities.ExtendedPropertyValue> ExtendedPropertyValues
            {
                get
                {
                    if (_ExtendedPropertyValue == null)
                    { _ExtendedPropertyValue = new EP.Collections.ExtendedPropertyValues(this).Items(); }
                    return _ExtendedPropertyValue;
                }
            }
            public EP.Entities.ExtendedPropertyValue ExtendedPropertyValue(Int64 idExtendedProperty)
            {
                return new EP.Collections.ExtendedPropertyValues(this).Item(idExtendedProperty);
            }
            public void ExtendedPropertyValueAdd(EP.Entities.ExtendedProperty extendedProperty, String value)
            {
                new EP.Collections.ExtendedPropertyValues(this).Add(extendedProperty, value);
            }
            public void Remove(EP.Entities.ExtendedPropertyValue extendedPropertyValue)
            {
                new EP.Collections.ExtendedPropertyValues(this).Remove(extendedPropertyValue);
            }
            public void ExtendedPropertyValueModify(EP.Entities.ExtendedPropertyValue extendedPropertyValue, String value)
            {
                new EP.Collections.ExtendedPropertyValues(this).Modify(extendedPropertyValue, value);
            }
            #endregion

        #endregion

        internal Calculation(Int64 idCalculation, Int64 idFormula, DateTime creationDate, Int64 idMeasurementUnit, Int64 idTimeUnitFrequency, Int32 frequency, Decimal lastResult, DateTime dateLastResult, String name, String description, Boolean isRelevant, Credential credential)
        {
            _IdCalculation = idCalculation;
            _IdFormula = idFormula;
            _CreationDate = creationDate;
            _IdMeasurementUnit = idMeasurementUnit;
            _Frequency = frequency;
            _IdTimeUnitFrequency = idTimeUnitFrequency;
            _LastResult = lastResult;
            _DateLastResult = dateLastResult;
            _IsRelevant = isRelevant;
            _Credential = credential;
            _LanguageOption = new Calculation_LG(_Credential.CurrentLanguage.IdLanguage, name, description);
        }

        public void Modify(MeasurementUnit measurementUnit, String name, String description, Int16 frequency, Int64 idTimeUnitFrequency, Dictionary<Int64, PF.Entities.ProcessGroupProcess> processGroupProcesses, Boolean isRelevant)
        {
            new Collections.Calculations(_Credential).Modify(_IdCalculation, measurementUnit, name, description, frequency, idTimeUnitFrequency, processGroupProcesses, isRelevant);
        }

        //public void Modify(MeasurementUnit MeasurementUnit, String name, String description, Int16 Frequency, Int64 IdTimeUnitFrequency)
        //{
        //    //Check for permission
        //    //Condesus.EMS.Business.Security.Authority.Authorize("PerformanceAssessment", "Indicators", "Write");
        //    try
        //    {
        //        using (TransactionScope _transactionScope = new TransactionScope())
        //        {
        //            //Objeto de data layer para acceder a datos
        //            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();
        //            DataAccess.PA.Entities.Calculations _dbCalculations = _dbPerformanceAssessments.Calculations;
        //            DataAccess.PA.Entities.Calculations_LG _dbCalculations_LG = _dbPerformanceAssessments.Calculations_LG;
        //            DataAccess.PA.Entities.CalculationParameters _dbCalculationParameters = _dbPerformanceAssessments.CalculationParameters;

        //            //Modifico los datos de la base
        //            _dbCalculations.Update(_IdCalculation, _IdOrganization,MeasurementUnit.IdMeasurementUnit, Frequency, IdTimeUnitFrequency, _Credential.User.Person.IdPerson);

        //            _dbCalculations_LG.Update(_IdCalculation, _IdOrganization, _Credential.DefaultLanguage.IdLanguage, name, description, _Credential.User.IdPerson);

        //            // Completar la transacción
        //            _transactionScope.Complete();
        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        if (ex.Number == Common.Constants.ErrorDataBaseDuplicatedKey)
        //        {
        //            throw new Exception(Common.Resources.Errors.DuplicatedDataRecord, ex);
        //        }
        //        throw ex;
        //    }
        //}

  
        public Decimal Calculate(DateTime startDate, DateTime endDate)
        {
            try
            {
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();
                

                Dictionary<String, Int64> _parameters = new Dictionary<String, Int64>();

                foreach (CalculationParameter oCalculationParameter in CalculationParameters.Values)
                {
                    _parameters.Add(oCalculationParameter.ParameterName, oCalculationParameter.Measurement.IdMeasurement);
                }

                Decimal _result = 0;
                //Estos 2 parametros son opcionales y siempre van despues del resto de los parametros y 
                //en este orden start, end..
                //IEnumerable<System.Data.Common.DbDataRecord> _record = _dbCalculations.Calculate(Formula.SchemaSP.Name, _parameters, startDate, endDate);
                //foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                //{
                //    _result = Convert.ToDecimal(_dbRecord["Result"]);
                //}

                _result = _dbPerformanceAssessments.Calculations_Calculate(Formula.SchemaSP.Name, _parameters, startDate, endDate);

                //graba el resultado en la tabla historica de resultados
                //_dbCalculations.CreateHistoryResult(_IdCalculation, _IdOrganization, DateTime.Now, _result, _Credential.User.IdPerson);

                //Ruben, cuando es por rango de fecha, no debe actualizar el resultado.
                //Graba el ultimo resultado
                //_dbCalculations.Update(_IdCalculation, _IdOrganization, _result, DateTime.Now, _Credential.User.IdPerson);

                return _result;
            }
            catch (SqlException ex)
            {
                if (ex.Number == Common.Constants.ErrorNoMeasurementforCalculate)
                {
                    throw new Exception(ex.Message, ex);
                }
                throw ex;
            }
        }
        public Decimal Calculate()
        {
            try
            {
                //Este metodo se encarga de ejecutar el calculo para todas las fechas que existen en la base. sin filtro de fecha.
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();                

                Dictionary<String, Int64> _parameters = new Dictionary<String, Int64>();

                foreach (CalculationParameter oCalculationParameter in CalculationParameters.Values)
                {
                    _parameters.Add(oCalculationParameter.ParameterName, oCalculationParameter.Measurement.IdMeasurement);
                }

                Decimal _result = 0;
                //IEnumerable<System.Data.Common.DbDataRecord> _record = _dbCalculations.Calculate(Formula.SchemaSP.Name, _parameters);
                //foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                //{
                //    _result = Convert.ToDecimal(_dbRecord["Result"]);
                //}

                _result = _dbPerformanceAssessments.Calculations_Calculate(Formula.SchemaSP.Name, _parameters);

                //graba el resultado en la tabla historica de resultados
                //_dbCalculations.CreateHistoryResult(_IdCalculation, _IdOrganization, DateTime.Now, _result, _Credential.User.IdPerson);

                //Graba el ultimo resultado
                _dbPerformanceAssessments.Calculations_Update(_IdCalculation, _result, DateTime.Now, _Credential.User.IdPerson);

                return _result;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 50000)
                {
                    if (Convert.ToInt32(ex.Message.Split('|')[0]) == Common.Constants.ErrorNoMeasurementforCalculate)
                    {
                        Int64 _idMeasurement = Convert.ToInt64(ex.Message.Split('|')[1]);
                        String _dateRange = ex.Message.Split('|')[2];
                        String _message = Common.Resources.Errors.NoDataMeasurement + _idMeasurement.ToString() + Common.Resources.Errors.MeasurementRange + _dateRange;
                        throw new Exception(_message, ex);
                    }
                    if (Convert.ToInt32(ex.Message.Split('|')[0]) == Common.Constants.ErrorDateRangeForCalculate)
                    {
                        String _message = Common.Resources.Errors.CalculateDataOutOffRange;
                        throw new Exception(_message, ex);
                    }
                }
                throw ex;
            }
        }

        #region Forecasted Series
        public List<CalculationPoint> SeriesForecasted(PA.Entities.CalculationScenarioType calculationScenarioType)
        {
            return SeriesForecasted(null, null, calculationScenarioType);
        }
        public List<CalculationPoint> SeriesForecasted(DateTime? startDate, DateTime? endDate, PA.Entities.CalculationScenarioType calculationScenarioType)
        {
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessment = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();            

            CalculationPoint _point = null;
            List<CalculationPoint> _points = new List<CalculationPoint>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessment.Calculations_ReadSeriesForecasted(_IdCalculation, startDate, endDate, calculationScenarioType.IdScenarioType);

            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _point = new CalculationPoint(Convert.ToDateTime(_dbRecord["StartDate"]), Convert.ToDecimal(_dbRecord["Result"]));
                _points.Add(_point);

            }
            return _points;
        }
        #endregion

        #region Verificated Series
        public List<CalculationPoint> SeriesVerificated()
        {
            return SeriesVerificated(null, null);
        }
        public List<CalculationPoint> SeriesVerificated(DateTime? startDate, DateTime? endDate)
        {
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessment = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();            

            CalculationPoint _point = null;
            List<CalculationPoint> _points = new List<CalculationPoint>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessment.Calculations_ReadSeriesVerificated(_IdCalculation, startDate, endDate);

            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _point = new CalculationPoint(Convert.ToDateTime(_dbRecord["StartDate"]), Convert.ToDecimal(_dbRecord["Result"]));
                _points.Add(_point);

            }
            return _points;
        }
        #endregion

        #region Historical series
        public List<CalculationPoint> Series()
        {
            return Series(null, null);
        }
        public List<CalculationPoint> Series(DateTime? startDate, DateTime? endDate)
        {
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessment = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();            

            CalculationPoint _point = null;
            List<CalculationPoint> _points = new List<CalculationPoint>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessment.Calculations_ReadSeries(_IdCalculation, startDate, endDate);

            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _point = new CalculationPoint(Convert.ToDateTime(_dbRecord["TimeStamp"]), Convert.ToDecimal(_dbRecord["Result"]));
                _points.Add(_point);

            }
            return _points;
        }


        //public List<MeasurementPoint> Series(DateTime? startDate, DateTime? endDate, GroupingType groupingType, AggregateType aggregateType)
        //{
        //    DataAccess.PA.PerformanceAssessments _dbPerformanceAssessment = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();
        //    DataAccess.PA.Entities.Measurements _dbMeasurements = _dbPerformanceAssessment.Measurements;

        //    MeasurementPoint _point = null;
        //    List<MeasurementPoint> _points = new List<MeasurementPoint>();

        //    IEnumerable<System.Data.Common.DbDataRecord> _record = _dbMeasurements.ReadSeries(_IdOrganization, _IdMeasurement, startDate, endDate, TranslateGrouping(groupingType), TranslateAggregate(aggregateType));

        //    foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
        //    {
        //        _point = new MeasurementPoint(Common.Common.ConstructDateTime(Convert.ToString(_dbRecord["MeasureDate"])), Convert.ToDecimal(Common.Common.CastNullValues(_dbRecord["MeasureValue"], 0.0)));
        //        _points.Add(_point);

        //    }
        //    return _points;
        //}

        //private DataAccess.PA.Entities.Measurements.GroupingType TranslateGrouping(GroupingType grouping)
        //{
        //    if (grouping == GroupingType.Hour) return DataAccess.PA.Entities.Measurements.GroupingType.Hour;
        //    if (grouping == GroupingType.Day) return DataAccess.PA.Entities.Measurements.GroupingType.Day;
        //    if (grouping == GroupingType.Month) return DataAccess.PA.Entities.Measurements.GroupingType.Month;
        //    if (grouping == GroupingType.Year) return DataAccess.PA.Entities.Measurements.GroupingType.Year;

        //    return Condesus.EMS.DataAccess.PA.Entities.Measurements.GroupingType.Day;

        //}
        //private DataAccess.PA.Entities.Measurements.AggregateType TranslateAggregate(AggregateType aggregate)
        //{
        //    if (aggregate == AggregateType.Avg) return DataAccess.PA.Entities.Measurements.AggregateType.Avg;
        //    if (aggregate == AggregateType.StdDev) return DataAccess.PA.Entities.Measurements.AggregateType.StdDev;
        //    if (aggregate == AggregateType.StdDevP) return DataAccess.PA.Entities.Measurements.AggregateType.StdDevP;
        //    if (aggregate == AggregateType.Sum) return DataAccess.PA.Entities.Measurements.AggregateType.Sum;
        //    if (aggregate == AggregateType.SumCummulative) return DataAccess.PA.Entities.Measurements.AggregateType.SumCummulative;
        //    if (aggregate == AggregateType.Var) return DataAccess.PA.Entities.Measurements.AggregateType.Var;
        //    if (aggregate == AggregateType.VarP) return DataAccess.PA.Entities.Measurements.AggregateType.VarP;

        //    return Condesus.EMS.DataAccess.PA.Entities.Measurements.AggregateType.Avg;
        //}    

        #endregion

        //#region Security
        //public Int64 IdObject
        //{
        //    get { return _IdCalculation; }
        //}
        //public String Title
        //{
        //    get { return _LanguageOption.Name; }
        //}
        //public String ClassName
        //{
        //    get
        //    {
        //        return Common.Security.Calculation;
        //    }
        //}
        //public void SecurityPostAdd(DS.Entities.Post post, Security.Entities.Permission permission)
        //{
        //    new Security.Collections.Rights(_Credential).Add(this, post, permission);
        //}
        //public void SecurityJobTitleAdd(DS.Entities.JobTitle jobTitle, Security.Entities.Permission permission)
        //{
        //    new Security.Collections.Rights(_Credential).Add(this, jobTitle, permission);
        //}
        //public void SecurityPostRemove(DS.Entities.Post post, Security.Entities.Permission permission)
        //{
        //    new Security.Collections.Rights(_Credential).Remove(post, this, permission);
        //}
        //public void SecurityJobTitleRemove(DS.Entities.JobTitle jobTitle, Security.Entities.Permission permission)
        //{
        //    new Security.Collections.Rights(_Credential).Remove(jobTitle, this, permission);
        //}
        //public void SecurityPostModify(DS.Entities.Post post, Security.Entities.Permission permission)
        //{
        //    new Security.Collections.Rights(_Credential).Modify(this, post, permission);
        //}
        //public void SecurityJobTitleModify(DS.Entities.JobTitle jobTitle, Security.Entities.Permission permission)
        //{
        //    new Security.Collections.Rights(_Credential).Modify(this, jobTitle, permission);
        //}

        //#region Read
        ////ALL
        //public List<Security.Entities.RightPost> SecurityPosts()
        //{
        //    return new Security.Collections.Rights(_Credential).ReadPostByClassName(this);
        //}

        //public List<Security.Entities.RightJobTitle> SecurityJobTitles()
        //{
        //    return new Security.Collections.Rights(_Credential).ReadJobTitleByClassName(this);
        //}
        ////por ID
        //public Security.Entities.RightJobTitle ReadJobTitleByID(DS.Entities.JobTitle jobTitle, Security.Entities.Permission permission)
        //{
        //    //no accedo a la base, solo lo creo aca           
        //    return new Security.Entities.RightJobTitle(permission, jobTitle);
        //}

        //public Security.Entities.RightPost ReadPostByID(DS.Entities.Post post, Security.Entities.Permission permission)
        //{
        //    //no accedo a la base, solo lo creo aca
        //    return new Security.Entities.RightPost(permission, post);
        //}
        //#endregion

        //#endregion
    }
}
