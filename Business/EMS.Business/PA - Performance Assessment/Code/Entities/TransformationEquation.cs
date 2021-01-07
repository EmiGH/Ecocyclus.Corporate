using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Transactions;
using System.Data;
using System.Reflection;

namespace Condesus.EMS.Business.PA.Entities
{
    //public class TransformationEquation
    //{
    //    protected String _CommandName = "";


    //    public TransformationEquation(String commandName)
    //    {
    //        _CommandName = commandName;
    //    }
    //    public TransformationEquation Create(String commandName)
    //    {
    //        return new TransformationEquation(commandName);
    //    }


    //    /// <summary>
    //    /// Ejecuta el metodo por reflexion
    //    /// </summary>
    //    /// <param name="param"></param>
    //    /// <returns></returns>
    //    public Object Execute(Dictionary<String, CalculationOfTransformationParameter> parameters)
    //    {
    //        Type _type = this.GetType();
    //        MethodInfo _method = _type.GetMethod(_CommandName);
    //        object[] _args = new object[] { parameters };
    //        try
    //        {
    //            return _method.Invoke(this, _args);
    //        }

    //        catch (Exception ex)
    //        {
    //            // TODO: Add logging functionality
    //            throw ex.InnerException;
    //        }
    //    }

    //    #region LinearTransformation
    //    /// <summary>
    //    /// Transformacion Lineal R=ax+b, idFuncion = 1, Parametros a,x,b, 
    //    /// </summary>
    //    /// <param name="parameters"></param>
    //    public void LinearTransformation(Dictionary<String, CalculationOfTransformationParameter> parameters)
    //    {
    //        try
    //        {
    //            //Asignacion de parametros y valores por default
    //            IOperand _base = null;
    //            IOperand _factorEmision = null;
    //            Int64 _idTransformation = 0;

    //            _idTransformation = parameters["Base"].IdTransformation;

    //            _base = parameters["Base"].Operand;
    //            _factorEmision = parameters["FactorEmision"].Operand;
                

    //            //Validaciones para la ejecucion del calculo
    //            // 1- Valida si hay valores para transformar
    //            ValidationValuesExistTransforming(_base, _idTransformation);

    //            //Armado de serie de datos
                              
    //            // 2- inserta los datos
    //            List<TransformationValues> _listTransformationValues = CreateDataSeries(_base, _factorEmision, _idTransformation);
                                

    //            //Ejecucion de la ecuacion
    //            foreach (TransformationValues _item in _listTransformationValues)
    //            {
    //                Decimal _result = EquationOfLinearTransformation(_item.FactorEmisionValue, _item.MeasurementValue, 0);

    //                //Graba el resultado
    //                new Collections.CalculateOfTransformationResults(this).Add(_item.IdTransformation, _item.IdMeasurement, _result, _item.MeasurementDate, _item.MeasurementStartDate, _item.MeasurementEndDate);
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            switch (ex.Message)
    //            {
    //                case "No hay Valores":
    //                    break;
    //                case "No hay Valor para el Factor de emision":
    //                    //Aca hay que guardar el error para que se enteren q fallo
    //                    break;
    //                default:
    //                    break;
    //            }
    //        }
    //    }

    //    private List<TransformationValues> CreateDataSeries(IOperand _base, IOperand _factorEmision, Int64 idTransformation)
    //    {
    //        //Declara la lista de datos
    //        List<TransformationValues> _listTransformationValues = new List<TransformationValues>();

    //        foreach (System.Data.Common.DbDataRecord _dbRecord in _base.TransformValues(idTransformation))
    //        {
    //            // 2- Valida que exista factor de emision para la fecha de la medicion
    //            ValidationValuesExistOperand(_factorEmision, Convert.ToDateTime(_dbRecord["MeasureDate"]));

    //            //Crea el objeto con todos los datos y lo mete en la coleccion
    //            TransformationValues _transformationValues = new TransformationValues(Convert.ToInt64(_dbRecord["IdTransformation"]), Convert.ToInt64(_dbRecord["IdMeasurement"]), Convert.ToDecimal(_dbRecord["MeasureValue"]), Convert.ToDateTime(_dbRecord["MeasureDate"]), _factorEmision.OperateValue(Convert.ToDateTime(_dbRecord["MeasureDate"])), Convert.ToDateTime(_dbRecord["MeasureStartDate"]), Convert.ToDateTime(_dbRecord["MeasureEndDate"]));

    //            _listTransformationValues.Add(_transformationValues);
    //        }

    //        return _listTransformationValues;

    //    }

    //    #region Validations
    //    /// <summary>
    //    /// Valida si hay valores para transformar
    //    /// </summary>
    //    /// <param name="x"></param>
    //    /// <returns></returns>
    //    private Boolean ValidationValuesExistTransforming(IOperand x, Int64 idTransformation)
    //    {
    //        foreach (System.Data.Common.DbDataRecord _dbRecord in x.TransformValues(idTransformation))
    //        {
    //            return true;
    //        }
    //        throw new Exception("No hay Valores");
    //    }

    //    private Decimal ValidationValuesExistOperand(IOperand factorEmision, DateTime date)
    //    {
    //        Decimal _value = factorEmision.OperateValue(date);
    //        //si el valor es 0, entonces no hay valor para esa fecha y no se puede operar
    //        if (_value == 0){ throw new Exception("No hay Valor para el Factor de emision"); }
    //        return _value;

    //    }
    //    #endregion

    //    #region Equations
    //    /// <summary>
    //    /// ejecuta la equacion de transformacion lineal
    //    /// </summary>
    //    /// <param name="a"></param>
    //    /// <param name="x"></param>
    //    /// <param name="b"></param>
    //    /// <returns></returns>
    //    private Decimal EquationOfLinearTransformation(Decimal a, Decimal x, Decimal b)
    //    {
    //        Decimal _R = a * x + b;
    //        return _R;
    //    }
    //    #endregion
    //    #endregion

    //}

    ///// <summary>
    ///// Esta Clase se usa para armar la serie de datos que usa la equacion
    ///// </summary>
    //internal class TransformationValues
    //{
    //    private Int64 _IdTransformation;
    //    private Int64 _IdMeasurement;
    //    private Decimal _MeasurementValue;
    //    private DateTime _MeasurementDate; 
    //    private Decimal _FactorEmisionValue;
    //    private DateTime _MeasurementStartDate;
    //    private DateTime _MeasurementEndDate;
        
    //    public Int64 IdTransformation
    //    {
    //        get { return _IdTransformation; }
    //        set { _IdTransformation = value; }
    //    }
    //    public Int64 IdMeasurement
    //    {
    //        get { return _IdMeasurement; }
    //        set { _IdMeasurement = value; }
    //    }
    //    public Decimal MeasurementValue
    //    {
    //        get { return _MeasurementValue; }
    //        set { _MeasurementValue = value; }
    //    }
    //    public DateTime MeasurementDate
    //    {
    //        get { return _MeasurementDate; }
    //        set { _MeasurementDate = value; }
    //    }
    //    public Decimal FactorEmisionValue
    //    {
    //        get { return _FactorEmisionValue; }
    //        set { _FactorEmisionValue = value; }
    //    }
    //    public DateTime MeasurementStartDate
    //    {
    //        get { return _MeasurementStartDate; }
    //        set { _MeasurementStartDate = value; }
    //    }
    //    public DateTime MeasurementEndDate
    //    {
    //        get { return _MeasurementEndDate; }
    //        set { _MeasurementEndDate = value; }
    //    }
        
    //    internal TransformationValues(Int64 idTransformation, Int64 idMeasurement, Decimal measurementValue, DateTime measurementDate, Decimal FactorEmisionValue, DateTime measurementStartDate, DateTime measurementEndDate)
    //    {
    //        _IdTransformation = idTransformation;
    //        _IdMeasurement = idMeasurement;
    //        _MeasurementValue = measurementValue;
    //        _MeasurementDate = measurementDate;
    //        _FactorEmisionValue = FactorEmisionValue;
    //        _MeasurementStartDate = measurementStartDate;
    //        _MeasurementEndDate = measurementEndDate;
    //    }
    //}
}
