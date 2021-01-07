using System;
using System.Collections.Generic;
using System.Text;
using ciloci.FormulaEngine;

namespace Condesus.EMS.Business.Common
{
    internal class Common
    {
        internal static T CastNullValues<T>(object value, T defaultValue)
        {
            if (value == DBNull.Value) return defaultValue;
            return (T)Convert.ChangeType(value, typeof(T));
        }
        internal static T CastValueToNull<T>(object value, T defaultValue)
        {
            if (value == (Object)0) return defaultValue;
            return (T)Convert.ChangeType(value, typeof(T));
        }
        /// <summary>
        /// Para Byte[]
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static object CastNullValues(object value)
        {
            if (value == DBNull.Value) 
            { Byte[] _byte = null; 
                return _byte; 
            }
            return value;
        }
        /// <summary>
        /// Calcula la fecha de fin de una tarea/ejecucion en base al tipo unidad seleccionada y la duracion de la tarea.
        /// </summary>
        /// <param name="idTimeUnit">Identificador del tipo de unidad de tiempo seleccionada</param>
        /// <param name="startDate">Fecha de inicio de la tarea</param>
        /// <param name="duration">Duracion de esa tarea, relacionado con el idTimeUnit</param>
        /// <returns>Una fecha de Fin</returns>
        internal static DateTime CalculateEndDate(Int64 idTimeUnit, DateTime startDate, Int64 duration)
        {
            DateTime _endDate = startDate;

            switch (idTimeUnit)
            {
                case 1:
                    _endDate = startDate.AddYears(Convert.ToInt16(duration));
                    break;

                case 2:
                    _endDate = startDate.AddMonths(Convert.ToInt16(duration));
                    break;

                case 3:
                    _endDate = startDate.AddDays(duration);
                    break;

                case 4:
                    //_return startDate.Addw(duration);
                    break;

                case 5:
                    _endDate = startDate.AddHours(duration);
                    break;

                case 6:
                    _endDate = startDate.AddMinutes(duration);
                    break;

                case 7:
                    _endDate = startDate.AddSeconds(duration);
                    break;
            }

            return _endDate;
        }

        /// <summary>
        /// Generates a valid datetime from a specific string format
        /// </summary>
        /// <param name="Value">The date in string format  (yyyymmddhhmmss)</param>
        /// <returns>A valid datetime</returns>
        internal static DateTime ConstructDateTime(String Value)
        {
            return new DateTime(Int32.Parse(Value.Substring(0, 4)), Int32.Parse(Value.Substring(4, 2)), Int32.Parse(Value.Substring(6, 2)), Int32.Parse(Value.Substring(9, 2)), Int32.Parse(Value.Substring(12, 2)), Int32.Parse(Value.Substring(15, 2)));
        }

    }

    public class ConstantsApplicabilities
    {
        public const int Address = 0;
        public const int Telephone = 1;
        public const int Email = 2;
        public const int Url = 3;
        public const int Messenger = 4;
    }

    internal class Constants
    {
        internal static Int16 ErrorDataBaseDuplicatedKey = 2627;
        internal static Int32 ErrorNoMeasurementforCalculate = 52629;
        internal static Int32 ErrorDateRangeForCalculate = 52628;
        internal static Int16 ErrorDataBaseDeleteReferenceConstraints = 547;
        internal static Int32 ErrorDataBaseNotLastPatternMeasurementUnit = 50000;

        internal static Int16 TimeUnitYear = 1;
        internal static Int16 TimeUnitMonth = 2;
        internal static Int16 TimeUnitDay = 3;
        internal static Int16 TimeUnitHour = 5;
        internal static Int16 TimeUnitMinute = 6;
        internal static Int16 TimeUnitSecond = 7;

        internal const Int16 ExceptionTypeTaskOverdue = 1;
        internal const Int16 ExceptionTypeMeasurementOutofRange = 2;
        internal const Int16 ExceptionTypeDocumentExpiration = 3;
        internal const Int16 ExceptionTypeExecutionNotOK = 4;
        internal const Int16 ExceptionTypeResourceExpiration = 5;

        internal static Int64 ContactTypeAddress = 0;
        internal static Int64 ContactTypeTelephone = 1;
        internal static Int64 ContactTypeEmail = 2;
        internal static Int64 ContactTypeUrl = 3;
        internal static Int64 ContactTypeMessenger = 4;

        internal static Int64 ExceptionStateOpen = 1;
        internal static Int64 ExceptionStateTreat = 2;
        internal static Int64 ExceptionStateClose = 3;

        internal static String TypeResourceFile = "ResourceVersion";
        internal static String TypeResourceCatalog = "ResourceCatalog";

        internal static String FormulaStoredProcedureNamePrefix = "PA_CalculationFormula_"; //Identifica como prefijo a todos los SP's que se pueden usar en las formulas.

        //Gadgetss
        internal const String Gadget_BuyerProjectSummary = "BuyerProjectSummary";
        internal const String Gadget_ExceptionsSummary = "ExceptionsSummary";
        internal const String Gadget_NewsSummary = "NewsSummary";
        internal const String Gadget_ProjectSummary = "ProjectSummary";
        internal const String Gadget_TasksSummary = "TasksSummary";

    }

    internal class Permissions
    {
        //Tipos de permisos
        internal const int View = 1;
        internal const int Manage = 2;
    }

    internal class RolesTypes
    {
        //Tipos de permisos
        internal const Int64 Operator = 4;
    }

    public class ClassName
    {
        public const String CalculateOfTransformation = "CalculationOfTransformation";
        public const String Measurement = "Measurement";
        public const String Constant = "Constant";
    }
    public class Security
    {
        //Mapas
        public const String MapDS = "MapDS";
        public const String MapIA = "MapIA";
        public const String MapKC = "MapKC";
        public const String MapRM = "MapRM";
        public const String MapPA = "MapPA";
        public const String MapPF = "MapPF";
        //Calssificaciones
        public const String IndicatorClassification = "IndicatorClassification";
        public const String OrganizationClassification = "OrganizationClassification";
        public const String ProcessClassification = "ProcessClassification";
        public const String ProjectClassification = "ProjectClassification";
        public const String ResourceClassification = "ResourceClassification";
        public const String RiskClassification = "RiskClassification";
        //Elementos
        public const String Indicator = "Indicator";
        public const String Organization = "Organization";
        public const String OrganizationalChart = "OrganizationalChart";
        public const String Project = "Project"; 
        public const String Process = "Process";
        public const String Task = "Task";        
        public const String Resource = "Resource";
        public const String ActionPlan = "ActionPlan";
        public const String Exception = "Exception";
        public const String Calculation = "Calculation";
        public const String CollaborationTools = "CollaborationTools";
        public const String Potential = "Potential";
        public const String Risk = "Risk";
        //internal const String RisksPotentials = "RisksPotentials";
        //Objetos que dan soporte a los elementos
        public const String ConfigurationKC = "ConfigurationKC";
        public const String ConfigurationDS = "ConfigurationDS";
        public const String ConfigurationIA = "ConfigurationIA";
        public const String ConfigurationRM = "ConfigurationRM";
        public const String ConfigurationPA = "ConfigurationPA";
        public const String ConfigurationPF = "ConfigurationPF";     
  
    }
    //filtro de lenguages
    internal class FilterLanguages
    {
    #region Internal Properties
        Business.Security.Credential _Credential;
        IEnumerable<System.Data.Common.DbDataRecord> _recordCurrent;
        String _idKey;
    #endregion

        internal FilterLanguages(IEnumerable<System.Data.Common.DbDataRecord> recordCurrent, String idKey, Business.Security.Credential credential) 
        {
            _recordCurrent = recordCurrent;
            _idKey = idKey;
            _Credential = credential;
        }

        internal Dictionary<Object, System.Data.Common.DbDataRecord> Filter()
        {
            Dictionary<Object, System.Data.Common.DbDataRecord> _filterReturns = new Dictionary<Object, System.Data.Common.DbDataRecord>();

            //Si esto esta en true, no debe insertar en la coleccion. (esto es cuando ya esta insertado el idioma elegido en la coleccion)
            Boolean _bInsert = true;

            foreach (System.Data.Common.DbDataRecord _dbRecord in _recordCurrent)
            {
                if (_filterReturns.ContainsKey(_dbRecord[_idKey]))
                {
                    ///Si el que agregue es el default lo elimino porque el que voy a agregar es el del idioma en uso por el usuario
                    if ((Convert.ToString(_dbRecord["IdLanguage"])).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                    {
                        _filterReturns.Remove(_dbRecord[_idKey]);
                    }
                    else
                    {
                        //No debe insertar en la coleccion ya que existe el idioma correcto.
                        _bInsert = false;
                    }
                }
                //Solo inserta si es necesario.
                if (_bInsert)
                {
                    //Lo agrego a la coleccion
                    _filterReturns.Add(_dbRecord[_idKey], _dbRecord);
                }
                _bInsert = true;
            }

            return _filterReturns;
        }

        
    }

   
    public class Formulator
    {
        private FormulaEngine _FE;
        internal const String OperateBase = "base";

        public Formulator()
        {
            //instancia la dll
            _FE = new FormulaEngine();
            //Alta de custom functions
            _FE.FunctionLibrary.AddFunction(EF_DRILLINGCH4);
            _FE.FunctionLibrary.AddFunction(EF_DRILLINGC2H6);
            _FE.FunctionLibrary.AddFunction(EF_DRILLINGC3H8);
            _FE.FunctionLibrary.AddFunction(EF_DRILLINGC4H10);
            _FE.FunctionLibrary.AddFunction(EF_DRILLINGH2S);
            _FE.FunctionLibrary.AddFunction(EF_DRILLINGCO2);
            _FE.FunctionLibrary.AddFunction(WASTE);
            _FE.FunctionLibrary.AddFunction(WASTEMONTH);
        }

        //Valida q la formula este correcta
        public void EvaluateFormula(String formula)
        {
            try
            {
                
                String _formula = formula.ToLower();

                if (!_formula.Contains(OperateBase)) { throw new Exception(Resources.Errors.CalculateOfTransformationOperatorBase); }

                _FE.Evaluate(_formula);

                //throw new Exception(Resources.Errors.CalculateOfTransformationFormulaCorrect);
            }
            catch (Exception e)
            {
                if (e.Message == Resources.Errors.CalculateOfTransformationOperatorBase)
                {
                    throw new Exception(Resources.Errors.CalculateOfTransformationOperatorBase, e); 
                }
                else
                {
                    throw new Exception(Resources.Errors.CalculateOfTransformationFormulaIncorrect, e);
                }
            }

        }
        public Double Execute(String formula)
        {
            //instancia la dll
            //FormulaEngine FE = new FormulaEngine();
            //ejecuta el metodo de creacion de la formula
            Formula F = _FE.CreateFormula(formula);
            //devuelve el resultado del tipo object
            object _Result = F.Evaluate();
            //retorna el resultado convertido a decimal
            return Convert.ToDouble(_Result);
        }

        #region Custom Formulas
        [FixedArgumentFormulaFunction(1, new OperandType[] { OperandType.Double })]
        private void EF_DRILLINGCH4(ciloci.FormulaEngine.Argument[] args, FunctionResult result, FormulaEngine engine)
        {
            // Get the value of the first argument as a double

            Double _Profundidad = args[0].ValueAsDouble;
            // Get the value of the second argument as a double

            Double Presion_del_reservorio_kpa = (1.422 * _Profundidad) * 6.894757;

            Double Temp_del_reservorio_GC = ((80 + 1.3 * (_Profundidad / 0.3048) / 100) - 32) / 1.8;

            Double _result = 0;

            if (Presion_del_reservorio_kpa < 5000)
            {
                if (Temp_del_reservorio_GC < 5) { _result = (Convert.ToDouble("0.05")); }
                if (Temp_del_reservorio_GC > 5 & Temp_del_reservorio_GC < 60) { _result = (Convert.ToDouble("0.83")); }
                if (Temp_del_reservorio_GC > 60 & Temp_del_reservorio_GC < 100) { _result = (Convert.ToDouble("1.33")); }
                if (Temp_del_reservorio_GC > 100) { _result = (Convert.ToDouble("1.68")); }
            }
            if (Presion_del_reservorio_kpa > 5000 & Presion_del_reservorio_kpa < 6000)
            {
                if (Temp_del_reservorio_GC < 5) { _result = (Convert.ToDouble("0.04")); }
                if (Temp_del_reservorio_GC > 5 & Temp_del_reservorio_GC < 60) { _result = (Convert.ToDouble("0.75")); }
                if (Temp_del_reservorio_GC > 60 & Temp_del_reservorio_GC < 100) { _result = (Convert.ToDouble("1.18")); }
                if (Temp_del_reservorio_GC > 100) { _result = (Convert.ToDouble("1.50")); }
            }
            if (Presion_del_reservorio_kpa > 6000)
            {
                if (Temp_del_reservorio_GC < 5) { _result = (Convert.ToDouble("0.04")); }
                if (Temp_del_reservorio_GC > 5 & Temp_del_reservorio_GC < 60) { _result = (Convert.ToDouble("0.68")); }
                if (Temp_del_reservorio_GC > 60 & Temp_del_reservorio_GC < 100) { _result = (Convert.ToDouble("1.24")); }
                if (Temp_del_reservorio_GC > 100) { _result = (Convert.ToDouble("1.50")); }
            }

            result.SetValue(_result);
        }
        [FixedArgumentFormulaFunction(1, new OperandType[] { OperandType.Double })]
        private void EF_DRILLINGC2H6(ciloci.FormulaEngine.Argument[] args, FunctionResult result, FormulaEngine engine)
        {
            // Get the value of the first argument as a double

            Double _Profundidad = args[0].ValueAsDouble;
            // Get the value of the second argument as a double

            Double Presion_del_reservorio_kpa = (1.422 * _Profundidad) * 6.894757;

            Double Temp_del_reservorio_GC = ((80 + 1.3 * (_Profundidad / 0.3048) / 100) - 32) / 1.8;

            Double _result = 0;

            if (Presion_del_reservorio_kpa < 5000)
            {
                if (Temp_del_reservorio_GC < 5) { _result = (Convert.ToDouble("0.10")); }
                if (Temp_del_reservorio_GC > 5 & Temp_del_reservorio_GC < 60) { _result = (Convert.ToDouble("1.12")); }
                if (Temp_del_reservorio_GC > 60 & Temp_del_reservorio_GC < 100) { _result = (Convert.ToDouble("1.27")); }
                if (Temp_del_reservorio_GC > 100) { _result = (Convert.ToDouble("1.37")); }
            }
            if (Presion_del_reservorio_kpa > 5000 & Presion_del_reservorio_kpa < 6000)
            {
                if (Temp_del_reservorio_GC < 5) { _result = (Convert.ToDouble("0.08")); }
                if (Temp_del_reservorio_GC > 5 & Temp_del_reservorio_GC < 60) { _result = (Convert.ToDouble("1.03")); }
                if (Temp_del_reservorio_GC > 60 & Temp_del_reservorio_GC < 100) { _result = (Convert.ToDouble("1.20")); }
                if (Temp_del_reservorio_GC > 100) { _result = (Convert.ToDouble("1.34")); }
            }
            if (Presion_del_reservorio_kpa > 6000)
            {
                if (Temp_del_reservorio_GC < 5) { _result = (Convert.ToDouble("0.07")); }
                if (Temp_del_reservorio_GC > 5 & Temp_del_reservorio_GC < 60) { _result = (Convert.ToDouble("0.96")); }
                if (Temp_del_reservorio_GC > 60 & Temp_del_reservorio_GC < 100) { _result = (Convert.ToDouble("1.18")); }
                if (Temp_del_reservorio_GC > 100) { _result = (Convert.ToDouble("1.34")); }
            }

            result.SetValue(_result);
        }
        [FixedArgumentFormulaFunction(1, new OperandType[] { OperandType.Double })]
        private void EF_DRILLINGC3H8(ciloci.FormulaEngine.Argument[] args, FunctionResult result, FormulaEngine engine)
        {
            // Get the value of the first argument as a double

            Double _Profundidad = args[0].ValueAsDouble;
            // Get the value of the second argument as a double

            Double Presion_del_reservorio_kpa = (1.422 * _Profundidad) * 6.894757;

            Double Temp_del_reservorio_GC = ((80 + 1.3 * (_Profundidad / 0.3048) / 100) - 32) / 1.8;

            Double _result = 0;

            if (Presion_del_reservorio_kpa < 5000)
            {
                if (Temp_del_reservorio_GC < 5) { _result = (Convert.ToDouble("0.15")); }
                if (Temp_del_reservorio_GC > 5 & Temp_del_reservorio_GC < 60) { _result = (Convert.ToDouble("0.70")); }
                if (Temp_del_reservorio_GC > 60 & Temp_del_reservorio_GC < 100) { _result = (Convert.ToDouble("0.72")); }
                if (Temp_del_reservorio_GC > 100) { _result = (Convert.ToDouble("0.77")); }
            }
            if (Presion_del_reservorio_kpa > 5000 & Presion_del_reservorio_kpa < 6000)
            {
                if (Temp_del_reservorio_GC < 5) { _result = (Convert.ToDouble("0.11")); }
                if (Temp_del_reservorio_GC > 5 & Temp_del_reservorio_GC < 60) { _result = (Convert.ToDouble("0.64")); }
                if (Temp_del_reservorio_GC > 60 & Temp_del_reservorio_GC < 100) { _result = (Convert.ToDouble("0.60")); }
                if (Temp_del_reservorio_GC > 100) { _result = (Convert.ToDouble("0.69")); }
            }
            if (Presion_del_reservorio_kpa > 6000)
            {
                if (Temp_del_reservorio_GC < 5) { _result = (Convert.ToDouble("0.10")); }
                if (Temp_del_reservorio_GC > 5 & Temp_del_reservorio_GC < 60) { _result = (Convert.ToDouble("0.61")); }
                if (Temp_del_reservorio_GC > 60 & Temp_del_reservorio_GC < 100) { _result = (Convert.ToDouble("0.64")); }
                if (Temp_del_reservorio_GC > 100) { _result = (Convert.ToDouble("0.69")); }
            }

            result.SetValue(_result);
        }
        [FixedArgumentFormulaFunction(1, new OperandType[] { OperandType.Double })]
        private void EF_DRILLINGC4H10(ciloci.FormulaEngine.Argument[] args, FunctionResult result, FormulaEngine engine)
        {
            // Get the value of the first argument as a double

            Double _Profundidad = args[0].ValueAsDouble;
            // Get the value of the second argument as a double

            Double Presion_del_reservorio_kpa = (1.422 * _Profundidad) * 6.894757;

            Double Temp_del_reservorio_GC = ((80 + 1.3 * (_Profundidad / 0.3048) / 100) - 32) / 1.8;

            Double _result = 0;

            if (Presion_del_reservorio_kpa < 5000)
            {
                if (Temp_del_reservorio_GC < 5) { _result = (Convert.ToDouble("0.18")); }
                if (Temp_del_reservorio_GC > 5 & Temp_del_reservorio_GC < 60) { _result = (Convert.ToDouble("0.42")); }
                if (Temp_del_reservorio_GC > 60 & Temp_del_reservorio_GC < 100) { _result = (Convert.ToDouble("0.42")); }
                if (Temp_del_reservorio_GC > 100) { _result = (Convert.ToDouble("0.42")); }
            }
            if (Presion_del_reservorio_kpa > 5000 & Presion_del_reservorio_kpa < 6000)
            {
                if (Temp_del_reservorio_GC < 5) { _result = (Convert.ToDouble("0.16")); }
                if (Temp_del_reservorio_GC > 5 & Temp_del_reservorio_GC < 60) { _result = (Convert.ToDouble("0.42")); }
                if (Temp_del_reservorio_GC > 60 & Temp_del_reservorio_GC < 100) { _result = (Convert.ToDouble("0.42")); }
                if (Temp_del_reservorio_GC > 100) { _result = (Convert.ToDouble("0.42")); }
            }
            if (Presion_del_reservorio_kpa > 6000)
            {
                if (Temp_del_reservorio_GC < 5) { _result = (Convert.ToDouble("0.15")); }
                if (Temp_del_reservorio_GC > 5 & Temp_del_reservorio_GC < 60) { _result = (Convert.ToDouble("0.42")); }
                if (Temp_del_reservorio_GC > 60 & Temp_del_reservorio_GC < 100) { _result = (Convert.ToDouble("0.42")); }
                if (Temp_del_reservorio_GC > 100) { _result = (Convert.ToDouble("0.42")); }
            }

            result.SetValue(_result);
        }
        [FixedArgumentFormulaFunction(1, new OperandType[] { OperandType.Double })]
        private void EF_DRILLINGH2S(ciloci.FormulaEngine.Argument[] args, FunctionResult result, FormulaEngine engine)
        {
            // Get the value of the first argument as a double

            Double _Profundidad = args[0].ValueAsDouble;
            // Get the value of the second argument as a double

            Double Presion_del_reservorio_kpa = (1.422 * _Profundidad) * 6.894757;

            Double Temp_del_reservorio_GC = ((80 + 1.3 * (_Profundidad / 0.3048) / 100) - 32) / 1.8;

            Double _result = 0;

            if (Presion_del_reservorio_kpa < 5000)
            {
                if (Temp_del_reservorio_GC < 5) { _result = (Convert.ToDouble("5.28")); }
                if (Temp_del_reservorio_GC > 5 & Temp_del_reservorio_GC < 60) { _result = (Convert.ToDouble("63.14")); }
                if (Temp_del_reservorio_GC > 60 & Temp_del_reservorio_GC < 100) { _result = (Convert.ToDouble("63.14")); }
                if (Temp_del_reservorio_GC > 100) { _result = (Convert.ToDouble("63.14")); }
            }
            if (Presion_del_reservorio_kpa > 5000 & Presion_del_reservorio_kpa < 6000)
            {
                if (Temp_del_reservorio_GC < 5) { _result = (Convert.ToDouble("4.81")); }
                if (Temp_del_reservorio_GC > 5 & Temp_del_reservorio_GC < 60) { _result = (Convert.ToDouble("66.29")); }
                if (Temp_del_reservorio_GC > 60 & Temp_del_reservorio_GC < 100) { _result = (Convert.ToDouble("66.29")); }
                if (Temp_del_reservorio_GC > 100) { _result = (Convert.ToDouble("70.32")); }
            }
            if (Presion_del_reservorio_kpa > 6000)
            {
                if (Temp_del_reservorio_GC < 5) { _result = (Convert.ToDouble("4.67")); }
                if (Temp_del_reservorio_GC > 5 & Temp_del_reservorio_GC < 60) { _result = (Convert.ToDouble("70.32")); }
                if (Temp_del_reservorio_GC > 60 & Temp_del_reservorio_GC < 100) { _result = (Convert.ToDouble("70.32")); }
                if (Temp_del_reservorio_GC > 100) { _result = (Convert.ToDouble("70.32")); }
            }

            result.SetValue(_result);
        }
        [FixedArgumentFormulaFunction(1, new OperandType[] { OperandType.Double })]
        private void EF_DRILLINGCO2(ciloci.FormulaEngine.Argument[] args, FunctionResult result, FormulaEngine engine)
        {
            // Get the value of the first argument as a double

            Double _Profundidad = args[0].ValueAsDouble;
            // Get the value of the second argument as a double

            Double Presion_del_reservorio_kpa = (1.422 * _Profundidad) * 6.894757;

            Double Temp_del_reservorio_GC = ((80 + 1.3 * (_Profundidad / 0.3048) / 100) - 32) / 1.8;

            Double _result = 0;

            if (Presion_del_reservorio_kpa < 5000)
            {
                if (Temp_del_reservorio_GC < 5) { _result = (Convert.ToDouble("1.69")); }
                if (Temp_del_reservorio_GC > 5 & Temp_del_reservorio_GC < 60) { _result = (Convert.ToDouble("37.73")); }
                if (Temp_del_reservorio_GC > 60 & Temp_del_reservorio_GC < 100) { _result = (Convert.ToDouble("49.70")); }
                if (Temp_del_reservorio_GC > 100) { _result = (Convert.ToDouble("54.10")); }
            }
            if (Presion_del_reservorio_kpa > 5000 & Presion_del_reservorio_kpa < 6000)
            {
                if (Temp_del_reservorio_GC < 5) { _result = (Convert.ToDouble("1.20")); }
                if (Temp_del_reservorio_GC > 5 & Temp_del_reservorio_GC < 60) { _result = (Convert.ToDouble("33.27")); }
                if (Temp_del_reservorio_GC > 60 & Temp_del_reservorio_GC < 100) { _result = (Convert.ToDouble("45.73")); }
                if (Temp_del_reservorio_GC > 100) { _result = (Convert.ToDouble("48.54")); }
            }
            if (Presion_del_reservorio_kpa > 6000)
            {
                if (Temp_del_reservorio_GC < 5) { _result = (Convert.ToDouble("0.86")); }
                if (Temp_del_reservorio_GC > 5 & Temp_del_reservorio_GC < 60) { _result = (Convert.ToDouble("29.73")); }
                if (Temp_del_reservorio_GC > 60 & Temp_del_reservorio_GC < 100) { _result = (Convert.ToDouble("42.19")); }
                if (Temp_del_reservorio_GC > 100) { _result = (Convert.ToDouble("48.54")); }
            }

            result.SetValue(_result);
        }

        [FixedArgumentFormulaFunction(29, new OperandType[] { OperandType.Double, OperandType.Double, OperandType.Double, 
            OperandType.Double, OperandType.Double, OperandType.Double, OperandType.Double, OperandType.Double, OperandType.Double, OperandType.Double, OperandType.Double, 
            OperandType.Integer, OperandType.Integer, OperandType.Integer, OperandType.Integer, OperandType.Integer, OperandType.Integer, 
            OperandType.Integer, OperandType.Integer, OperandType.Integer, OperandType.Integer, OperandType.Integer, OperandType.Integer ,
            OperandType.Integer, OperandType.Integer, OperandType.Integer, OperandType.Integer, OperandType.Integer, OperandType.Integer })]
        private void WASTE(ciloci.FormulaEngine.Argument[] args, FunctionResult result, FormulaEngine engine)
        {
            
            // Get the value of the first argument as a double
            Double _base = args[0].ValueAsDouble; //Base
            Double _a = args[1].ValueAsDouble; // φ
            Double _b = args[2].ValueAsDouble; // f
            //Double _c = args[3].ValueAsDouble; // GWPCH4
            Double _d = args[3].ValueAsDouble; // OX
            Double _e = args[4].ValueAsDouble; // F
            Double _f = args[5].ValueAsDouble; // DOCf
            Double _g = args[6].ValueAsDouble; // MCF
            Double _h = args[7].ValueAsDouble; // DOCj
            Double _i = args[8].ValueAsDouble; // kj
            Double _j = args[9].ValueAsDouble; // e-kj
            Double _idTransformation = args[10].ValueAsDouble;
            int _MeasureMes = args[11].ValueAsInteger;
            int _MeasureDia = args[12].ValueAsInteger;
            int _MeasureAno = args[13].ValueAsInteger;
            int _MeasureHora = args[14].ValueAsInteger;
            int _MeasureMin = args[15].ValueAsInteger;
            int _MeasureSeg = args[16].ValueAsInteger;
            int _StartMes = args[17].ValueAsInteger;
            int _StartDia = args[18].ValueAsInteger;
            int _StartAno = args[19].ValueAsInteger;
            int _StartHora = args[20].ValueAsInteger;
            int _StartMin = args[21].ValueAsInteger;
            int _StartSeg = args[22].ValueAsInteger;
            int _EndMes = args[23].ValueAsInteger;
            int _EndDia = args[24].ValueAsInteger;
            int _EndAno = args[25].ValueAsInteger;
            int _EndHora = args[26].ValueAsInteger;
            int _EndMin = args[27].ValueAsInteger;
            int _EndSeg = args[28].ValueAsInteger;

            DateTime _MeasuredateBase = new DateTime(_MeasureAno, _MeasureMes, _MeasureDia, _MeasureHora, _MeasureMin, _MeasureSeg);
        

            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();
                        
            //trae un distinct de los años de las mediciones
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.CalculateOfTransformations_WasteReadYears(Convert.ToInt64(_idTransformation), _MeasuredateBase);

            int _x = 0;
            int _y = 0;
            Double _resultPartial = 0;
            //obtengo y exponete del total de años que hay pra atras          
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                //guardo la variable del exponente
                _y = _y + 1;
            }
            Int16 _yearAnt = 0;
            //recorro otra vez para q año por año se calcule el valor del periodo
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                Int16 _anoActual = Convert.ToInt16(_dbRecord["TimeYear"]);

                //arma las fechas del periodo con el año que se esta recorriendo
                DateTime _StartdateBase = new DateTime(_anoActual, _StartMes, _StartDia, _StartHora, _StartMin, _StartSeg);
                DateTime _EnddateBase = new DateTime(_anoActual, _EndMes, _EndDia, _EndHora, _EndMin, _EndSeg);
                //si el endate es menor que el start incremento el año
                if (_EnddateBase < _StartdateBase) { _EnddateBase = new DateTime(_anoActual + 1, _EndMes, _EndDia, _EndHora, _EndMin, _EndSeg); }
                //trae valores con años
                IEnumerable<System.Data.Common.DbDataRecord> _record2 = _dbPerformanceAssessments.CalculateOfTransformations_WasteReadValuesAndYears(Convert.ToInt64(_idTransformation), _StartdateBase, _EnddateBase);

                //calcula el potencial        
                
                foreach (System.Data.Common.DbDataRecord _dbRecord2 in _record2)
                {
                    Double _value = Convert.ToDouble(_dbRecord2["Value"]);
                    Int16 _year = Convert.ToInt16(_dbRecord2["TimeYear"]);

                    if (_year > _yearAnt)
                    {
                        if (_yearAnt == 0)
                        { _x = 1; }
                        else
                        { _x = _x + (_year - _yearAnt); }
                    }
                    _resultPartial = _resultPartial + (_a * (1 - _b) * (1 - _d) * _e * _f * _g * _value * (1 - _j) * Potential(_j, _y - _x));
                    //_resultPartial = _resultPartial + (_a * (1 - _b) * (1 - _d) * _e * _f * _g * _value * _i * (1 - _j) * Potential(_j, _y - _x));
                    _yearAnt = _year;

                }
            }                              

            result.SetValue(_resultPartial);
        }

        private Double Potential(Double b, Double exp)
        {
            int _contador = 0;
            Double _acum = 1;
            while (++_contador <= exp)
            {
                _acum = _acum * b;
            }
            return _acum;

        }

        [FixedArgumentFormulaFunction(29, new OperandType[] { OperandType.Double, OperandType.Double, OperandType.Double, 
            OperandType.Double, OperandType.Double, OperandType.Double, OperandType.Double, OperandType.Double, OperandType.Double, OperandType.Double, OperandType.Double, 
            OperandType.Integer, OperandType.Integer, OperandType.Integer, OperandType.Integer, OperandType.Integer, OperandType.Integer, 
            OperandType.Integer, OperandType.Integer, OperandType.Integer, OperandType.Integer, OperandType.Integer, OperandType.Integer ,
            OperandType.Integer, OperandType.Integer, OperandType.Integer, OperandType.Integer, OperandType.Integer, OperandType.Integer })]
        private void WASTEMONTH(ciloci.FormulaEngine.Argument[] args, FunctionResult result, FormulaEngine engine)
        {

            // Get the value of the first argument as a double
            Double _base = args[0].ValueAsDouble; //Base
            Double _a = args[1].ValueAsDouble; // φ
            Double _b = args[2].ValueAsDouble; // f
            //Double _c = args[3].ValueAsDouble; // GWPCH4
            Double _d = args[3].ValueAsDouble; // OX
            Double _e = args[4].ValueAsDouble; // F
            Double _f = args[5].ValueAsDouble; // DOCf
            Double _g = args[6].ValueAsDouble; // MCF
            Double _h = args[7].ValueAsDouble; // DOCj
            Double _i = args[8].ValueAsDouble; // kj
            Double _j = args[9].ValueAsDouble; // e-kj
            Double _idTransformation = args[10].ValueAsDouble;
            int _MeasureMes = args[11].ValueAsInteger;
            int _MeasureDia = args[12].ValueAsInteger;
            int _MeasureAno = args[13].ValueAsInteger;
            int _MeasureHora = args[14].ValueAsInteger;
            int _MeasureMin = args[15].ValueAsInteger;
            int _MeasureSeg = args[16].ValueAsInteger;
            int _StartMes = args[17].ValueAsInteger;
            int _StartDia = args[18].ValueAsInteger;
            int _StartAno = args[19].ValueAsInteger;
            int _StartHora = args[20].ValueAsInteger;
            int _StartMin = args[21].ValueAsInteger;
            int _StartSeg = args[22].ValueAsInteger;
            int _EndMes = args[23].ValueAsInteger;
            int _EndDia = args[24].ValueAsInteger;
            int _EndAno = args[25].ValueAsInteger;
            int _EndHora = args[26].ValueAsInteger;
            int _EndMin = args[27].ValueAsInteger;
            int _EndSeg = args[28].ValueAsInteger;

            DateTime _MeasuredateBase = new DateTime(_MeasureAno, _MeasureMes, _MeasureDia, _MeasureHora, _MeasureMin, _MeasureSeg);


            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            //trae un distinct de los años de las mediciones
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.CalculateOfTransformations_WasteReadYears(Convert.ToInt64(_idTransformation), _MeasuredateBase);

            int _x = 0;
            int _y = 0;
            Double _resultPartial = 0;
            //obtengo y exponete del total de años que hay pra atras          
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                //guardo la variable del exponente
                _y = _y + 1;
            }
            Int16 _yearAnt = 0;
            //recorro otra vez para q año por año se calcule el valor del periodo
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                Int16 _anoActual = Convert.ToInt16(_dbRecord["TimeYear"]);

                //arma las fechas del periodo con el año que se esta recorriendo
                DateTime _StartdateBase = new DateTime(_anoActual, _StartMes, _StartDia, _StartHora, _StartMin, _StartSeg);
                DateTime _EnddateBase = new DateTime(_anoActual, _EndMes, _EndDia, _EndHora, _EndMin, _EndSeg);
                //si el endate es menor que el start incremento el año
                if (_EnddateBase < _StartdateBase) { _EnddateBase = new DateTime(_anoActual + 1, _EndMes, _EndDia, _EndHora, _EndMin, _EndSeg); }
                //trae valores con años
                IEnumerable<System.Data.Common.DbDataRecord> _record2 = _dbPerformanceAssessments.CalculateOfTransformations_WasteReadValuesAndYearsForMonth(Convert.ToInt64(_idTransformation), _StartdateBase, _EnddateBase);

                //calcula el potencial        

                foreach (System.Data.Common.DbDataRecord _dbRecord2 in _record2)
                {
                    Double _value = Convert.ToDouble(_dbRecord2["Value"]);
                    Int16 _year = Convert.ToInt16(_dbRecord2["TimeYear"]);

                    if (_year > _yearAnt)
                    {
                        if (_yearAnt == 0)
                        { _x = 1; }
                        else
                        { _x = _x + (_year - _yearAnt); }
                    }
                    _resultPartial = _resultPartial + (_a * (1 - _b) * (1 - _d) * _e * _f * _g * _value * (1 - _j) * Potential(_j, _y - _x));
                    _yearAnt = _year;

                }
            }

            result.SetValue(_resultPartial);
        }      
       
        #endregion 
    }
}
