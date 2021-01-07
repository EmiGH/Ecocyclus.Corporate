using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Transactions;
using System.Reflection;
using System.Data.SqlClient;
using ciloci.FormulaEngine;


namespace Condesus.EMS.Business.PA.Entities
{
    public class CalculateOfTransformation : ITransformer, IOperand, INotificationRecipient, INotificationReported, ISerializable
    {
        #region Internal Properties
            private Credential _Credential;
            private Int64 _IdTransformation;
            private ITransformer _BaseTransformer;
            private Int64 _IdIndicator;
            private String _Formula;
            private Int64 _IdMeasurementUnit;
            private MeasurementUnit _MeasurementUnit;
            private Entities.Indicator _Indicator;
            private Entities.CalculateOfTransformation_LG _LanguageOption; //Objeto con los datos dependientes del idioma actual elegido por el usuario
            private Dictionary<String, CalculateOfTransformation_LG> _LanguagesOptions;
            private Dictionary<String, Entities.CalculateOfTransformationParameter> _Parameters;
            private Int64 _IdProcess;
            private PF.Entities.ProcessGroupProcess _ProcessGroupProcess;
            private Int64 _IdMeasurementOrigin;
            private PA.Entities.Measurement _MeasurementOrigin;
            private List<NT.Entities.NotificationMessage> _NotificationMessages;
            private Int64 _IdActivity;
            private AccountingActivity _Activity;
        #endregion

        #region External Properties
            
            internal Credential Credential
            {
                get { return _Credential; }
            }
            public Int64 IdTransformation
            {
                get { return _IdTransformation; }
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
            public AccountingActivity Activity
            {
                get
                {
                    if (_Activity == null)
                    {
                        _Activity = new Collections.AccountingActivities(_Credential).Item(_IdActivity);
                    }
                    return _Activity;
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
            public String Formula
            {
                get { return _Formula; }
            }
            public ITransformer BaseTransformer
            {
                get
                {
                    return _BaseTransformer;
                }
            }
            public PF.Entities.ProcessGroupProcess ProcessGroupProcess
            {
                get
                {
                    if (_ProcessGroupProcess == null)
                    { _ProcessGroupProcess = new PF.Collections.ProcessGroupProcesses(this.Credential).Item(_IdProcess); }
                    return _ProcessGroupProcess;
                }
            }

            public Entities.Measurement MeasurementOrigin
            {
                get
                {
                    if (_MeasurementOrigin == null)
                    { _MeasurementOrigin = new Collections.Measurements(this.Credential).Item(_IdMeasurementOrigin); }
                    return _MeasurementOrigin;
                }
            }

            #region LanguageOptions
                public CalculateOfTransformation_LG LanguageOption
                {
                    get
                    {
                        return _LanguageOption;
                    }
                }
                public Dictionary<String, Entities.CalculateOfTransformation_LG> LanguagesOptions
                {
                    get
                    {
                        if (_LanguagesOptions == null)
                        {
                            //Carga la coleccion de lenguages de es posicion
                            _LanguagesOptions = new Collections.CalculateOfTransformations_LG(this).Items();
                        }

                        return _LanguagesOptions;
                    }
                }
                public CalculateOfTransformation_LG LanguageCreate(DS.Entities.Language language, String name, String description)
                {
                    return new Collections.CalculateOfTransformations_LG(this).Create(language, name, description);
                }
                public void LanguageRemove(DS.Entities.Language language)
                {
                    new Collections.CalculateOfTransformations_LG(this).Delete(language);
                }
                public void LanguageModify(DS.Entities.Language language, String name, String description)
                {
                    new Collections.CalculateOfTransformations_LG(this).Update(language, name, description);
                }
            #endregion

            #region Parameters
            public Dictionary<String, CalculateOfTransformationParameter> Parameters
            {
                get
                {
                    if (_Parameters == null)
                    {
                        _Parameters = new Collections.CalculateOfTransformationParameters(this).Items();
                    }
                    return _Parameters;
                }
            }
            public CalculateOfTransformationParameter Parameter(String idParameter)
            {
                return new Collections.CalculateOfTransformationParameters(this).Item(idParameter);
            }
            internal CalculateOfTransformationParameter ParameterAdd(String idParameter, IOperand operand)
            {
                using (TransactionScope _TransactionScope = new TransactionScope())
                {
                    CalculateOfTransformationParameter _calculateOfTransformationParameter = new Collections.CalculateOfTransformationParameters(this).Add(idParameter, operand);
                    _TransactionScope.Complete();
                    return _calculateOfTransformationParameter;
                }
            }
            internal void Remove(CalculateOfTransformationParameter parameter)
            {
                using (TransactionScope _TransactionScope = new TransactionScope())
                {
                    new Collections.CalculateOfTransformationParameters(this).Remove(parameter);
                    _TransactionScope.Complete();
                }
            }

            public Dictionary<Int64, CalculateOfTransformation> TransformationsParameters
            {
                get { return new Collections.CalculateOfTransformations(this).ItemsTransformationParameter(this); }
            }
            #endregion

            #region Results
                public Decimal ResultTransformationByIndicator(Indicator indicator, DateTime? startDate, DateTime? endDate)
            {
                
                return new Collections.CalculateOfTransformationResults(this).ResultTransformationByIndicator(this, indicator, startDate, endDate);
                
            }
            #endregion
        #endregion

             


            #region iOperand
            public Int64 IdObject
            {
                get { return IdTransformation; }
            }
            public String ClassName 
            {
                get { return Common.ClassName.CalculateOfTransformation;}            
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
            public Double OperateValue(Entities.CalculateOfTransformation transformation, DateTime startDate, DateTime endDate)
            {
                return new Collections.CalculateOfTransformations(this).ReadOperateValue(this, startDate, endDate);
            }
        #endregion

            #region ITransformer
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
            public CalculateOfTransformation TransformationAdd(Entities.Indicator indicatorTransformation, MeasurementUnit measurementUnit, String formula, String name, String description, Entities.AccountingActivity activity, Dictionary<String, IOperand> operands, List<NT.Entities.NotificationRecipient> notificationRecipients)
            {
                using (TransactionScope _TransactionScope = new TransactionScope())
                {
                    CalculateOfTransformation _calculateOfTransformation = new Collections.CalculateOfTransformations(this).Add(indicatorTransformation, measurementUnit, this, formula, name, description, this.ProcessGroupProcess, MeasurementOrigin, activity, operands, notificationRecipients);
                    _TransactionScope.Complete();
                    return _calculateOfTransformation;
                }                
            }
            public void Remove(CalculateOfTransformation calculationOfTransformation)
            {
                using (TransactionScope _TransactionScope = new TransactionScope())
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
            #endregion

            #region INotificationReported
            public void ChangeStatusNotification(Int64 idError)
            {
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new DataAccess.PA.PerformanceAssessments();

                _dbPerformanceAssessments.CalculateOfTransformationErrors_Update(idError, this.IdTransformation, true);
            }

            public List<NT.Entities.NotificationMessage> NotificationMessages
            {
                get 
                {
                    DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new DataAccess.PA.PerformanceAssessments();

                    _NotificationMessages = new List<Condesus.EMS.Business.NT.Entities.NotificationMessage>();

                    IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.CalculateOfTransformationErrors_ReadAll(this._IdTransformation);

                    foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                    {
                        NT.Entities.NotificationMessageTransformationError _NotificationMessageTransformationError = new NT.Entities.NotificationMessageTransformationError(this, Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(_dbRecord["IdError"]), Convert.ToDateTime(_dbRecord["ErrorDate"]));

                         _NotificationMessages.Add(_NotificationMessageTransformationError);
                    }
                    return _NotificationMessages; 
                }
            }

            #endregion

            #region INotificationRecipient
            public List<NT.Entities.NotificationRecipient> NotificationRecipient
            {
                get
                {
                    return new NT.Collections.NotificationRecipients(this).Items();
                }
            }

            public NT.Entities.NotificationRecipientEmail NotificationRecipientAdd(String email)
            {
                return new NT.Collections.NotificationRecipients(this).Add(this, email);
            }
            public void Remove(NT.Entities.NotificationRecipientEmail notificationRecipientEmail)
            {
                new NT.Collections.NotificationRecipients(this).Remove(this, notificationRecipientEmail);
            }
            public NT.Entities.NotificationRecipientPerson NotificationRecipientPersonAdd(DS.Entities.Person person, DS.Entities.ContactEmail contactEmail)
            {
                return new NT.Collections.NotificationRecipients(this).Add(this, person, contactEmail);
            }
            public void Remove(NT.Entities.NotificationRecipientPerson notificationRecipientPerson)
            {
                new NT.Collections.NotificationRecipients(this).Remove(this, notificationRecipientPerson.Person, notificationRecipientPerson.ConctactEmail);
            }
            public void NotificationRecipientRemove()
            {
                new NT.Collections.NotificationRecipients(this).Remove(this);
            }
            #endregion

            #region ISerializable
            public MeasurementPoint TotalMeasurement(ref DateTime? firstDateSeries)
            {
                //Ruben Toca aca
                //if (this.MeasurementOrigin.GetType().Name == "MeasurementExtensive")
                if (this.Indicator.IsCumulative)
                {
                    Double _total = 0;
                    DateTime _dateMeasurement = DateTime.Today;
                    DateTime _Startdate = DateTime.MaxValue;
                    DateTime _Enddate = DateTime.MinValue;
                    List<MeasurementPoint> _measureemntPoints = Series();

                    //si es acumulativa retorna la suma de todos los valores, sin filtro de fechas
                    if (_measureemntPoints.Count > 0)
                    {
                        //cuando es Acumulativa, debo mostrar la fecha inicio y fin....
                        foreach (MeasurementPoint _measurementPoint in _measureemntPoints)
                        {
                            //Se guarda la primer fecha en caso de ser cumulative.
                            if (firstDateSeries == null)
                            { firstDateSeries = _measurementPoint.MeasureDate; }

                            _total += _measurementPoint.MeasureValue;
                            _dateMeasurement = _measurementPoint.MeasureDate;   //Se queda con la ultima fecha medida.
                            if (_Startdate > _measurementPoint.StartDate)
                            { _Startdate = _measurementPoint.StartDate; }
                            if (_Enddate < _measurementPoint.EndDate)
                            {_Enddate = _measurementPoint.EndDate;}
                        }

                        return new MeasurementPoint(_dateMeasurement, _total, _Startdate, _Enddate, 0, Credential, 0 , 0);
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    Double _total = 0;
                    DateTime _dateMeasurement = DateTime.Today;
                    DateTime _Startdate = DateTime.Today;
                    DateTime _Enddate = DateTime.Today;
                    List<MeasurementPoint> _measureemntPoints = Series();
                    //si es intensiva devuelve el ultimo valor cargado
                    if (_measureemntPoints.Count > 0)
                    {
                        foreach (MeasurementPoint _measurementPoint in _measureemntPoints)
                        {
                            _total = _measurementPoint.MeasureValue;
                            _dateMeasurement = _measurementPoint.MeasureDate;   //Se queda con la ultima fecha medida.
                            _Startdate = _measurementPoint.StartDate;
                            _Enddate = _measurementPoint.EndDate;
                        }
                        return new MeasurementPoint(_dateMeasurement, _total, _Startdate, _Enddate, 0, Credential, 0, 0);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
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

                IEnumerable<System.Data.Common.DbDataRecord> _record = null;

                switch (this.BaseTransformer.ClassName)
                {
                    case Common.ClassName.Measurement:
                        _record = _dbPerformanceAssessment.CalculateOfTransformationMeasurementResults_ReadSeries(this.IdTransformation, startDate, endDate);        
                        break;
                    case Common.ClassName.CalculateOfTransformation:
                        _record = _dbPerformanceAssessment.CalculateOfTransformationTransformationResults_ReadSeries(this.IdTransformation, startDate, endDate);        
                        break;
                    default:
                        break;
                }

                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    _point = new MeasurementPoint(Convert.ToDateTime(_dbRecord["TransformationDate"]), Convert.ToDouble(_dbRecord["TransformationValue"]), Convert.ToDateTime(_dbRecord["StartDate"]), Convert.ToDateTime(_dbRecord["EndDate"]), 0, _Credential, 0, 0);
                    _points.Add(_point);

                }
                //carga los valores de maximo y minino date
                IEnumerable<System.Data.Common.DbDataRecord> _record1 = _dbPerformanceAssessment.CalculateOfTransformations_ReadMaxMinDate(_IdTransformation);
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
            #endregion

            /// <summary>
        /// Borra dependencias
        /// </summary>
            internal void Remove()
            {
                //Borra sus transformaciones
                foreach (CalculateOfTransformation _calculateOfTransformation in this.Transformations.Values)
                {
                    Remove(_calculateOfTransformation);
                }
                //Borra los parametros
                foreach (CalculateOfTransformationParameter _calculateOfTransformationParameter in this.Parameters.Values)
                {
                    Remove(_calculateOfTransformationParameter);
                }
                
                //Borra los emails
                NotificationRecipientRemove();
                //Borra LG
                new Collections.CalculateOfTransformations_LG(this).Delete();
                //Borra los resultados
                new Collections.CalculateOfTransformationResults(this).Remove(this);
                //Borra los Errores
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new DataAccess.PA.PerformanceAssessments();
                _dbPerformanceAssessments.CalculateOfTransformationErrors_Delete(this.IdTransformation);
            }

            internal CalculateOfTransformation(Int64 idTransformation, ITransformer baseTransformer, Int64 idIndicator, Int64 idMeasurementUnit, Int64 idProcess, Int64 idMeasurementOrigin, Int64 idActivity, String formula, String name, String description, String idLanguage, Credential credential)
        {
            _Credential = credential;
            _IdTransformation = idTransformation;
            _BaseTransformer = baseTransformer;
            _IdIndicator = idIndicator;
            _Formula = formula;
            _IdMeasurementUnit = idMeasurementUnit;
            _IdProcess = idProcess;
            _IdMeasurementOrigin = idMeasurementOrigin;
            _IdActivity = idActivity;
            //Carga el nombre para el lenguage seleccionado
            _LanguageOption = new CalculateOfTransformation_LG(idLanguage, name, description);
        }

            public void Modify(Entities.Indicator indicator, Entities.MeasurementUnit measurementUnit, String formula, String name, String description, Entities.AccountingActivity activity, Dictionary<String, IOperand> operands, List<NT.Entities.NotificationRecipient> notificationRecipients)
            {
                //aca es donde se arma una transaccion y primero se modifica el indicador,
                //borra las asociaciones de indicadores y por ultimo agrega las relaciones nuevas.
                using (TransactionScope _transactionScope = new TransactionScope())
                {
                    //Hace el update sobre el indicador.
                    new PA.Collections.CalculateOfTransformations(this).Modify(this, indicator, measurementUnit, this.BaseTransformer, formula, name, description, this.ProcessGroupProcess, this.MeasurementOrigin, activity, operands, notificationRecipients);

                    //borra los resultados de las transformaciones donde es parametro
                    foreach (CalculateOfTransformation _calculate in new Collections.CalculateOfTransformations(this).ItemsAsParameter(this).Values)
                    {
                        new Collections.CalculateOfTransformationResults(_calculate).Remove(_calculate);
                    }

                    RemoveResults();                                        

                    _transactionScope.Complete();                    
                }

            }
        /// <summary>
        /// Borra los resultados hijos
        /// </summary>
            internal void RemoveResults()
            {
                //borra los resultados de las transformaciones que tiene como hija
                foreach (CalculateOfTransformation _calculate in this.Transformations.Values)
                {
                    RemoveResultsChildrens(_calculate);
                }
                new Collections.CalculateOfTransformationResults(this).Remove(this);
            }
            private void RemoveResultsChildrens(CalculateOfTransformation calculate)
            {
                //borra los resultados de las transformaciones que tiene como hija
                foreach (CalculateOfTransformation _calculate in calculate.Transformations.Values)
                {
                    RemoveResultsChildrens(_calculate);
                }
                new Collections.CalculateOfTransformationResults(this).Remove(this);
            }
            
        /// <summary>
        /// Metodo que realiza la ejecucion del calculo por reflexion
        /// </summary>
        public void Execute()
        {
            try
            {
                //1° - Busca si hay valores para transformar
                IEnumerable<System.Data.Common.DbDataRecord> _record = this.BaseTransformer.TransformValues(this);

                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    Boolean _calculate = true;
                    
                    //falta crear variables independientes y validar la existencia de los valores
                    //Valida la formula
                    Common.Formulator _formulator = new Common.Formulator();

                    //carga las variablaes
                    Double _value = Convert.ToDouble(_dbRecord["Value"]);
                    DateTime _date = Convert.ToDateTime(_dbRecord["Date"]);
                    DateTime _startDate = Convert.ToDateTime(_dbRecord["StartDate"]);
                    DateTime _endDate = Convert.ToDateTime(_dbRecord["EndDate"]);

                    //Carga variables
                    String _formula = this.Formula.ToLower();
                    if (_formula.Contains("waste"))
                    {
                        int _MeasureMes = _date.Month;
                        int _MeasureDia = _date.Day; ;
                        int _MeasureAno = _date.Year;
                        int _MeasureHora = _date.Hour;
                        int _MeasureMin = _date.Minute;
                        int _MeasureSeg = _date.Second;
                        int _StartMes = _startDate.Month;
                        int _StartDia = _startDate.Day;
                        int _StartAno = _startDate.Year;
                        int _StartHora = _startDate.Hour;
                        int _StartMin = _startDate.Minute;
                        int _StartSeg = _startDate.Second;
                        int _EndMes = _endDate.Month;
                        int _EndDia = _endDate.Day;
                        int _EndAno = _endDate.Year;
                        int _EndHora = _endDate.Hour;
                        int _EndMin = _endDate.Minute;
                        int _EndSeg = _endDate.Second;

                        _formula = _formula.Replace(")", "," + this.BaseTransformer.IdObject + "," +
                            _MeasureMes + "," + _MeasureDia + "," + _MeasureAno + "," + _MeasureHora + "," + _MeasureMin + "," + _MeasureSeg + "," +
                            _StartMes + "," + _StartDia + "," + _StartAno + "," + _StartHora + "," + _StartMin + "," + _StartSeg + "," +
                            _EndMes + "," + _EndDia + "," + _EndAno + "," + _EndHora + "," + _EndMin + "," + _EndSeg + ")");
                    }

                    //valida si la formula es correcta
                    //_formulator.EvaluateFormula(_formula);
                
                    //Carga la variable a transformar, la "base" es el baseTransformer                    
                    _formula = _formula.Replace(Common.Formulator.OperateBase.ToLower(), _value.ToString());

                    _formula = ReplaceIntrincicFuctions(_formula);

                    //Carga el resto de los parametros
                    foreach (CalculateOfTransformationParameter _parameter in this.Parameters.Values)
                    {
                        Double _operateValue = _parameter.Operand.OperateValue(this, _startDate, _endDate);
                        //si viene minValue no tiene que ejecutar
                        if (_operateValue == Double.MinValue) { _calculate = false; }

                        _formula = _formula.Replace(_parameter.IdParameter.ToLower(), Convert.ToString(_operateValue));
                    }

                    //Valida que despues de los reemplazos a la formula no le hayan quedado letras y sean solo numeros
                    for (int i = 97; i < 123; i++)
                    {
                        String _letter = Char.ConvertFromUtf32(i);
                        //Si la formula contiene una letra genera una exepcion
                        if (_formula.Contains(_letter))
                        {
                            throw new Exception(Common.Resources.Errors.CalculateOfTransformationUndeclaredParameter + ": " + _letter);
                        }
                    }

                    if (_calculate)
                    {
                        //Ejecuta la formula
                        Double _result = _formulator.Execute(_formula);

                        //Graba el resultado
                        new Collections.CalculateOfTransformationResults(BaseTransformer, this).Add(this, this.BaseTransformer, _result, _date, _startDate, _endDate);
                    }
                }
            }

            catch (SqlException ex)
            {
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new DataAccess.PA.PerformanceAssessments();

                _dbPerformanceAssessments.CalculateOfTransformationErrors_Create(this.IdTransformation, false, ex.Message);
            }
            catch (System.DivideByZeroException ex)
            {
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new DataAccess.PA.PerformanceAssessments();

                _dbPerformanceAssessments.CalculateOfTransformationErrors_Create(this.IdTransformation, false, "Divide By Zero Exception - " + ex.Message);
            }

            catch (Exception ex)
            {
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new DataAccess.PA.PerformanceAssessments();
                _dbPerformanceAssessments.CalculateOfTransformationErrors_Create(this.IdTransformation, false, ex.Message);
            }
           
        }

        private String ReplaceIntrincicFuctions(String formula)
        {
            //reemplaza todas las funciones intrinsecas, las pasa a mayuscula para que no se reemplazen cuando reemplazo los operadores
            formula = formula.Replace("abs", "ABS");
            formula = formula.Replace("acos", "ACOS");
            formula = formula.Replace("asin", "ASIN");
            formula = formula.Replace("atan", "ATAN");
            formula = formula.Replace("atan2", "ATAN2");
            formula = formula.Replace("bigmull", "BIGMULL");
            formula = formula.Replace("ceiling", "CEILING");
            formula = formula.Replace("cos", "COS");
            formula = formula.Replace("cosh", "COSH");
            formula = formula.Replace("divrem", "DIVREM");
            formula = formula.Replace("exp", "EXP");
            formula = formula.Replace("floor", "FLOOR");
            formula = formula.Replace("ieeeremainder", "IEEEREMAINDER");
            formula = formula.Replace("log", "LOG");
            formula = formula.Replace("log10", "LOG10");
            formula = formula.Replace("max", "MAX");
            formula = formula.Replace("min", "MIN");
            formula = formula.Replace("pow", "POW");
            formula = formula.Replace("round", "ROUND");
            formula = formula.Replace("sing", "SIGN");
            formula = formula.Replace("sin", "SIN");
            formula = formula.Replace("sinh", "SINH");
            formula = formula.Replace("sqrt", "SQRT");
            formula = formula.Replace("tan", "TAN");
            formula = formula.Replace("tanh", "TANH");
            formula = formula.Replace("truncate", "TRUNCATE");
            formula = formula.Replace("if", "IF");

            //CUSTOM FUNCTIONS
            formula = formula.Replace("ef_drillingch4", "EF_DRILLINGCH4");
            formula = formula.Replace("ef_drillingc2h6", "EF_DRILLINGC2H6");
            formula = formula.Replace("ef_drillingc3h8", "EF_DRILLINGC3H8");
            formula = formula.Replace("ef_drillingc4h10", "EF_DRILLINGC4H10");
            formula = formula.Replace("ef_drillingh2s", "EF_DRILLINGH2S");
            formula = formula.Replace("ef_drillingco2", "EF_DRILLINGCO2");
            formula = formula.Replace("wastemonth", "WASTEMONTH");
            formula = formula.Replace("waste", "WASTE");            
            return formula;
        }

        #region Custom functions
      
        #endregion
    }
}
