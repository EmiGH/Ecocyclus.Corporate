using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.PA.Entities
{
    public abstract class Measurement : ITransformer, IOperand, ISerializable
    {
        #region Internal Properties
            private Credential _Credential;
            private Int64 _IdDevice;
            private Int64 _IdMeasurement;
            private Int64 _IdIndicator;
            private Int64 _IdMeasurementUnit;
            private Int64 _TimeUnitFrequency;
            private Int32 _Frequency;
            private Boolean _IsRegressive;
            private Boolean _IsRelevant;
            private String _Source;
            private String _FrequencyAtSource;
            private Decimal _Uncertainty;
            private Int64 _IdQuality;
            private Quality _Quality;
            private Int64 _IdMethodology;
            private Methodology _Methodology;
            private Entities.Measurement_LG _LanguageOption; //Objeto con los datos dependientes del idioma actual elegido por el usuario
            private Collections.Measurements_LG _LanguagesOptions;
            private MeasurementDevice _Device;
            private Dictionary<Int64, Entities.ParameterGroup> _ParameterGroup;
            private Indicator _Indicator;
            private MeasurementUnit _MeasurementUnit;
            private PF.Entities.ProcessTask _ProcessTask;
        #endregion     

        #region External Properties
            internal Credential Credential
            {
                get { return _Credential; }
            }
            public Int64 TimeUnitFrequency
            {
                get { return _TimeUnitFrequency; }
            }
            public Int32 Frequency
            {
                get { return _Frequency; }
            }
            public Int64 IdMeasurement
            {
                get { return _IdMeasurement; } 
            }
            public Boolean IsRegressive
            {
                get { return _IsRegressive; }
            }
            public Boolean IsRelevant
            {
                get { return _IsRelevant; }
            }
            public MeasurementDevice Device
            {
                get
                {
                    if (_Device == null)
                    {
                        _Device = new Collections.MeasurementDevices(_Credential).Item(_IdDevice);
                    }
                    return _Device;
                }
            }
            public Dictionary<Int64, Entities.ParameterGroup> ParameterGroups
            {
                get
                {
                    if (_ParameterGroup == null)
                    {
                        _ParameterGroup = new Collections.ParameterGroups(_Credential).Items(this.IdMeasurement);
                    }
                    return _ParameterGroup;
                }
            }
            public Indicator Indicator
            {
                get
                {
                    if (_Indicator == null)
                    {
                        _Indicator = new Collections.Indicators(_Credential).Item(_IdIndicator);
                    }
                    return _Indicator;
                }
            }
            public MeasurementUnit MeasurementUnit
            {
                get
                {
                    if (_MeasurementUnit == null)
                    {
                        _MeasurementUnit = new Collections.MeasurementUnits(_Credential).Item(_IdMeasurementUnit);
                    }
                    return _MeasurementUnit;
                }
            }
            public String FrequencyAtSource
            {
                get { return _FrequencyAtSource; }
            }
            public String Source
            {
                get { return _Source; }
            }
            public Decimal Uncertainty
            {
                get { return _Uncertainty; }
            }
            public Quality Quality
            {
                get
                {
                    if (_Quality == null)
                    { _Quality = new Collections.Qualities(this).Item(_IdQuality); }
                    return _Quality;
                }
            }
            public Methodology Methodology
            {
                get
                {
                    if (_Methodology == null)
                    { _Methodology = new Collections.Methodologies(this).Item(_IdMethodology); }
                    return _Methodology;
                }
            }
            public enum AggregateType
            { Sum, SumCummulative, Avg, StdDev, StdDevP, Var, VarP}
            public enum GroupingType
            { Hour, Day, Month, Year }

            public PF.Entities.ProcessTask ProcessTask
            {
                get
                {
                    if (_ProcessTask == null)
                    { _ProcessTask = new PF.Collections.ProcessTasks(_Credential).Item(this); }
                    return _ProcessTask;
                }
            }

            //public Decimal TotalMeasurement()
            //{
            //    Decimal _total = 0;
            //    if (IsCumulative) //si es acumulativa retorna la suma de todos los valores, sin filtro de fechas
            //    {                    
            //        foreach (MeasurementPoint _measurementPoint in Series())
            //        {
            //            _total += _measurementPoint.MeasureValue;
            //        }
            //    }
            //    else //si es intensiva devuelve el ultimo valor cargado
            //    {                   
            //        foreach (MeasurementPoint _measurementPoint in Series())
            //        {
            //            _total = _measurementPoint.MeasureValue;
            //        }                    
            //    }
            //    return _total;
            //}
            /// <summary>
            /// Retorna el total de las mediciones en caso de ser Acumulativa y su ultima fecha tomada o el ultimo valor medido y su ultima fecha.
            /// </summary>
            /// <returns>un <c>MeasurementPoint</c></returns>
            public abstract MeasurementPoint TotalMeasurement(ref DateTime? firstDateSeries);
            private DateTime _Maxdate;
            private DateTime _Mindate;
            public DateTime MaxDate()
            {
                return _Maxdate;
            }
            public DateTime Mindate()
            {
                return _Mindate;
            }
            public List<MeasurementPoint> Series()
            {
                return Series(null, null);
            }
            public List<MeasurementPoint> Series(DateTime? startDate, DateTime? endDate)
            {
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessment = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                MeasurementPoint _point = null;
                List<MeasurementPoint> _points = new List<MeasurementPoint>();

                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessment.Measurements_ReadSeries(_IdMeasurement, startDate, endDate);
            
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    _point = new MeasurementPoint(Convert.ToDateTime(_dbRecord["MeasureDate"]), Convert.ToDouble(_dbRecord["MeasureValue"]), Convert.ToDateTime(_dbRecord["StartDate"]), Convert.ToDateTime(_dbRecord["EndDate"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdExecutionMeasurement"], 0)), _Credential, Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdExecution"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdProcess"], 0)));
                    _points.Add(_point);

                }
                //carga los valores de maximo y minino date
                IEnumerable<System.Data.Common.DbDataRecord> _record1 = _dbPerformanceAssessment.Measurements_ReadMaxMinDate(_IdMeasurement);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record1)
                {
                    try
                    {
                        _Maxdate = Convert.ToDateTime(_dbRecord["MaxDate"]);
                        _Mindate = Convert.ToDateTime(_dbRecord["MinDate"]);
                    }
                    catch (Exception ex)
                    {
                        _Maxdate = DateTime.MaxValue;
                        _Mindate = DateTime.MinValue;
                    }
                }
                return _points;
            }
            public List<MeasurementPoint> Series(DateTime? startDate, DateTime? endDate, GroupingType groupingType, AggregateType aggregateType)
            { 
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessment = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                MeasurementPoint _point = null;
                List<MeasurementPoint> _points = new List<MeasurementPoint>();

                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessment.Measurements_ReadSeries(_IdMeasurement, startDate, endDate, TranslateGrouping(groupingType),TranslateAggregate(aggregateType));
            
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    _point = new MeasurementPoint(Common.Common.ConstructDateTime(Convert.ToString(_dbRecord["MeasureDate"])), Convert.ToDouble(Common.Common.CastNullValues(_dbRecord["MeasureValue"], 0.0)), Convert.ToDateTime(_dbRecord["StartDate"]), Convert.ToDateTime(_dbRecord["EndDate"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdExecutionMeasurement"], 0)), _Credential, Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdExecution"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdProcess"], 0)));
                    _points.Add(_point);

                }
                return _points;
            }

            private DataAccess.PA.Entities.Measurements.GroupingType TranslateGrouping(GroupingType grouping)
            {
                if (grouping == GroupingType.Hour) return DataAccess.PA.Entities.Measurements.GroupingType.Hour;
                if (grouping == GroupingType.Day) return DataAccess.PA.Entities.Measurements.GroupingType.Day;
                if (grouping == GroupingType.Month) return DataAccess.PA.Entities.Measurements.GroupingType.Month;
                if (grouping == GroupingType.Year) return DataAccess.PA.Entities.Measurements.GroupingType.Year;
                
                return Condesus.EMS.DataAccess.PA.Entities.Measurements.GroupingType.Day;

            }
            private DataAccess.PA.Entities.Measurements.AggregateType TranslateAggregate(AggregateType aggregate)
            {
                if (aggregate == AggregateType.Avg) return DataAccess.PA.Entities.Measurements.AggregateType.Avg;
                if (aggregate == AggregateType.StdDev) return DataAccess.PA.Entities.Measurements.AggregateType.StdDev;
                if (aggregate == AggregateType.StdDevP) return DataAccess.PA.Entities.Measurements.AggregateType.StdDevP;
                if (aggregate == AggregateType.Sum) return DataAccess.PA.Entities.Measurements.AggregateType.Sum;
                if (aggregate == AggregateType.SumCummulative) return DataAccess.PA.Entities.Measurements.AggregateType.SumCummulative;
                if (aggregate == AggregateType.Var) return DataAccess.PA.Entities.Measurements.AggregateType.Var;
                if (aggregate == AggregateType.VarP) return DataAccess.PA.Entities.Measurements.AggregateType.VarP;

                return Condesus.EMS.DataAccess.PA.Entities.Measurements.AggregateType.Avg;
            }    

            public MeasurementStatistics Statistics(DateTime? startDate, DateTime? endDate)
            {
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessment = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                return new MeasurementStatistics(_dbPerformanceAssessment.Measurements_ReadStatistics(_IdMeasurement, startDate, endDate));
            }
            
            public Collections.Measurements_LG LanguagesOptions
            {
                get
                {
                    if (_LanguagesOptions == null)
                    {
                        //Carga la coleccion de lenguages de ese pais
                        _LanguagesOptions = new Collections.Measurements_LG(this);
                    }
                    return _LanguagesOptions;
                }
            }
            public Entities.Measurement_LG LanguageOption
            {
                get { return _LanguageOption; }
            }

            public Double ResultTransformationByIndicator(Indicator indicator, DateTime? startDate, DateTime? endDate)
            {
                return new Collections.CalculateOfTransformationResults(this).ResultTransformationByIndicator(this, indicator, startDate, endDate);
            }
        #endregion

            #region iOperand
            public Int64 IdObject
            {
                get { return IdMeasurement; }
            }
            public String ClassName
            {
                get { return Common.ClassName.Measurement; }
            }
            public String Name
            {
                get { return _LanguageOption.Name; }
            }
            public String Description
            {
                get { return _LanguageOption.Description; }
            }
            /// <summary>
            /// Devuleve el valor para hacer la operacion , para la fecha solocitada
            /// </summary>
            /// <param name="date"></param>
            /// <returns></returns>
            public virtual Double OperateValue(Entities.CalculateOfTransformation transformation, DateTime startDate, DateTime endDate)
            {
                return new Collections.Measurements(this.Credential).OperateValue(transformation, this, startDate, endDate);
            }
            #endregion

            #region ITransformmer
            /// <summary>
            /// Devuelve los valores que hay que transformar, si viene vacio es porque no hay nada para calcular
            /// </summary>
            /// <returns></returns>
            public IEnumerable<System.Data.Common.DbDataRecord> TransformValues(CalculateOfTransformation transformation)
            {
                return new Collections.CalculateOfTransformationResults(this, transformation).TransformValues();
            }
            public CalculateOfTransformation Transformation(Int64 idTransformation)
            {
                return new Collections.CalculateOfTransformations(this).Item(idTransformation);
            }
            public Dictionary<Int64, CalculateOfTransformation> Transformations
            {
                get
                {
                    return new Collections.CalculateOfTransformations(this).Items();
                }
            }
            public Dictionary<Int64, CalculateOfTransformation> TransformationsAsParameter
            {
                get
                {
                    return new Collections.CalculateOfTransformations(this).ItemsAsParameter(this);
                }
            }
            public CalculateOfTransformation TransformationAdd(Entities.Indicator indicatorTransformation, MeasurementUnit measurementUnit, String formula, String name, String description, Entities.AccountingActivity activity, Dictionary<String, IOperand> operands, List<NT.Entities.NotificationRecipient> notificationRecipients)
            {
                using (TransactionScope _TransactionScope = new TransactionScope())
                {
                    CalculateOfTransformation _calculateOfTransformation = new Collections.CalculateOfTransformations(this).Add(indicatorTransformation, measurementUnit, this, formula, name, description, this.ProcessTask.Parent, this, activity, operands, notificationRecipients);
                    _TransactionScope.Complete();
                    return _calculateOfTransformation;
                }
            }
            public void Remove(CalculateOfTransformation calculationOfTransformation)
            {
                using (TransactionScope _TransactionScope = new TransactionScope(new TransactionScopeOption(), new TimeSpan(0, 10, 0)))
                {
                    new Collections.CalculateOfTransformations(this).Remove(calculationOfTransformation);
                    _TransactionScope.Complete();
                }
            }
            //Valida q la formula este correcta
            public void EvaluateFormula(String formula)
            {
                new Common.Formulator().EvaluateFormula(formula);
            }

            public void UpdateDataSeries(DataTable dtDataSerie)
            {
                new PF.Collections.ProcessTaskExecutions(Credential).UpdateDataSeries(dtDataSerie);

                //borra la transformaciones donde participa
                foreach (CalculateOfTransformation _calculate in this.Transformations.Values)
                {
                    _calculate.RemoveResults();
                }
                //borra la transformaciones donde participa
                foreach (CalculateOfTransformation _calculate in this.TransformationsAsParameter.Values)
                {
                    _calculate.RemoveResults();
                }
            }
            
            #endregion

        /// <summary>
        /// Borra dependencias
        /// </summary>
            internal void Remove()
            {

                foreach (CalculateOfTransformation _calculateOfTransformation in this.Transformations.Values)
                {
                    this.Remove(_calculateOfTransformation);
                }
                
            }

            internal Measurement(Int64 idMeasurement, Int64 idDevice, Int64 idIndicator, Int64 idTimeUnitFrequency, 
            Int32 frequency, Int64 idMeasurementUnit, String name, String description, Boolean isRegressive,
            Boolean isRelevant, String idLanguage, Credential credential, String source, String frequencyAtSource,
            Decimal uncertainty, Int64 idQuality, Int64 idMethodology)
        {
            _Credential = credential;
            _IdMeasurement = idMeasurement;
            _IdDevice = idDevice;
            _IdIndicator = idIndicator;
            _TimeUnitFrequency = idTimeUnitFrequency;
            _Frequency = frequency;
            _IdMeasurementUnit = idMeasurementUnit;
            _IsRegressive = isRegressive;
            _IsRelevant = isRelevant;
            _Source = source;
            _FrequencyAtSource= frequencyAtSource;
            _Uncertainty = uncertainty;
            _IdQuality= idQuality;
            _IdMethodology= idMethodology;
            _LanguageOption = new Measurement_LG(idLanguage, name, description);

        }

            public void Modify(Entities.MeasurementDevice measurementDevice, List<PA.Entities.ParameterGroup> parametersGroups,
            Entities.Indicator indicator, String name, String description, PF.Entities.TimeUnit timeUnitFrequency, Int32 frequency,
            PA.Entities.MeasurementUnit measurementUnit, Boolean isRegressive, Boolean isRelevant, String source,
            String frequencyAtSource, Decimal uncertainty, Entities.Quality quality, Entities.Methodology methodology)
        {
            using (TransactionScope _TransactionScope = new TransactionScope())
            {
                new Collections.Measurements(_Credential).Modify(this, measurementDevice, parametersGroups, indicator, name, description,
                    timeUnitFrequency, frequency, measurementUnit, isRegressive, isRelevant, source, frequencyAtSource,
                    uncertainty, quality, methodology);
                _TransactionScope.Complete();
            }
        }
    }
    
    public class MeasurementStatistics
    {
        private DateTime _FirstDate;
        private DateTime _LastDate;
        private Double _FirstValue;
        private Double _LastValue;
        private Double _AvgValue;
        private Double _CountValue;
        private Double _SumValue;
        private Double _MaxValue;
        private Double _MinValue;
        private Double _StdDevValue;
        private Double _StdDevPValue;
        private Double _VarValue;
        private Double _VarPValue;

        public DateTime FirstDate
        {
            get { return _FirstDate; }
        }
        public DateTime LastDate
        {
            get { return _LastDate; }
        }
        public Double FirstValue
        {
            get { return _FirstValue; }
        }
        public Double LastValue
        {
            get { return _LastValue; }
        }
        public Double SumValue
        {
            get { return _SumValue; }
        }
        public Double AvgValue
        {
            get { return _AvgValue; }
        }
        public Double CountValue
        {
            get { return _CountValue; }
        }
        public Double MaxValue
        {
            get { return _MaxValue; }
        }
        public Double MinValue
        {
            get { return _MinValue; }
        }
        public Double StdDevValue
        {
            get { return _StdDevValue; }
        }
        public Double StdDevPValue
        {
            get { return _StdDevPValue; }
        }
        public Double VarValue
        {
            get { return _VarValue; }
        }
        public Double VarPValue
        {
            get { return _VarPValue; }
        }

        internal MeasurementStatistics(DataAccess.PA.Entities.MeasurementStatistics statistics)
        {
            _FirstDate = statistics.FirstDate;
            _LastDate = statistics.LastDate;
            _FirstValue = statistics.FirstValue;
            _LastValue = statistics.LastValue;
            _AvgValue = statistics.AvgValue;
            _CountValue = statistics.CountValue;
            _SumValue = statistics.SumValue;
            _MaxValue = statistics.MaxValue;
            _MinValue = statistics.MinValue;
            _StdDevValue = statistics.StdDevValue;
            _StdDevPValue = statistics.StdDevPValue;
            _VarValue = statistics.VarValue;
            _VarPValue = statistics.VarPValue;

        }

    }
}
