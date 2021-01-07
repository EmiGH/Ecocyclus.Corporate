using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PA.Collections
{
    internal class ParameterRanges
    {
        #region Internal Properties
            private Credential _Credential;
            private Entities.Parameter _Parameter;
        #endregion

            //TODO DAM: verificar con ruben si puedo sacar este constructor, el tema son los validate
            internal ParameterRanges(Credential credential)
            {
                _Credential = credential;
            }

            internal ParameterRanges(Entities.Parameter parameter, Credential credential)
            {
                _Credential = credential;
                _Parameter = parameter;
            }

            #region Read Functions
                internal Dictionary<Int64, Entities.ParameterRange> Items()
                {
                    //Coleccion para devolver las direcciones
                    Dictionary<Int64, Entities.ParameterRange> _oItems = new Dictionary<Int64, Entities.ParameterRange>();

                    //Objeto de data layer para acceder a datos
                    DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                    //Traigo los datos de la base (o por persona o por organizacion)
                    IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.ParameterRanges_ReadAll(_Parameter.IdParameter);
                    
                    foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                    {
                        //Declara e instancia una direccion 
                        Entities.ParameterRange _parameterRange = new Entities.ParameterRange(Convert.ToInt64(_dbRecord["IdParameterRange"]),
                            Convert.ToInt64(_dbRecord["IdParameter"]),
                            Convert.ToDouble(_dbRecord["LowValue"]),
                            Convert.ToDouble(_dbRecord["HighValue"]), 
                            _Credential);

                        //Lo agrego a la coleccion
                        _oItems.Add(_parameterRange.IdParameterRange, _parameterRange);
                    }
                    return _oItems;
                }
                internal Entities.ParameterRange Item(Int64 idParameterRange)
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                    IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.ParameterRanges_ReadById(_Parameter.IdParameter, idParameterRange);
                    System.Collections.IEnumerator _enum = _record.GetEnumerator();
                    if (_enum.MoveNext())
                    {
                        System.Data.Common.DbDataRecord _dbRecord = (System.Data.Common.DbDataRecord)_enum.Current;

                        return new Entities.ParameterRange(Convert.ToInt64(_dbRecord["IdParameterRange"]),
                            Convert.ToInt64(_dbRecord["IdParameter"]),
                            Convert.ToDouble(_dbRecord["LowValue"]),
                            Convert.ToDouble(_dbRecord["HighValue"]), 
                            _Credential);
                    }

                    return null;
                }
            #endregion

            #region Write Functions
                internal Entities.ParameterRange Add(Double lowValue, Double highValue)
                {
                    try
                    {
                        //Objeto de data layer para acceder a datos
                        DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                        if(ValidateNewRange(_Parameter.IdParameter, lowValue, highValue))
                        {
                            //ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                            Int64 _idParameterRange = _dbPerformanceAssessments.ParameterRanges_Add(_Parameter.IdParameter, lowValue, highValue);
                            //Devuelvo el objeto direccion creado
                            return new Entities.ParameterRange(_idParameterRange, _Parameter.IdParameter, lowValue, highValue, _Credential);
                        }
                        else
                        {
                            throw new Exception(Common.Resources.Errors.IncorrectParameterRange);
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
                internal void Remove(Entities.ParameterRange parameterRange)
                {
                    try
                    {
                        //Objeto de data layer para acceder a datos
                        DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                        //Borrar de la base de datos
                        _dbPerformanceAssessments.ParameterRanges_Remove(parameterRange.IdParameterRange);
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
                internal void Remove()
                {
                    try
                    {
                        //Objeto de data layer para acceder a datos
                        DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                        //Borrar de la base de datos
                        _dbPerformanceAssessments.ParameterRanges_RemoveByParameter(_Parameter.IdParameter);
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
                internal void Modify(Entities.ParameterRange parameterRange, Double lowValue, Double highValue)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                    if (ValidateNewRange(_Parameter.IdParameter, lowValue, highValue))
                    {
                        //Modifico los datos de la base
                        _dbPerformanceAssessments.ParameterRanges_Update(parameterRange.IdParameterRange, _Parameter.IdParameter, lowValue, highValue);
                    }
                    else
                    {
                        throw new Exception(Common.Resources.Errors.IncorrectParameterRange);
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
            #endregion

            internal Entities.Parameter ValidateValueRange(Entities.ParameterGroup parameterGroup, Double value)
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                //ejecuta la validacion y obtiene el idParameter, para luego retornar el parametro que lo contiene.
                Int64 _idParameter = _dbPerformanceAssessments.ParameterRanges_Validate(parameterGroup.IdParameterGroup, value);
                //Devuelvo el objeto parametro creado
                return new Collections.Parameters(_Credential).Item(_idParameter);
            }
            private Entities.Parameter ValidateValueRange(Int64 idParameter, Double value)
            {
                Entities.Parameter _parameter = new PA.Collections.Parameters(_Credential).Item(idParameter);

                //Objeto de data layer para acceder a datos
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                //ejecuta la validacion y obtiene el idParameter, para luego retornar el parametro que lo contiene.
                Int64 _idParameter = _dbPerformanceAssessments.ParameterRanges_Validate( _parameter.ParameterGroup.IdParameterGroup, value);
                //Devuelvo el objeto parametro creado
                return new Collections.Parameters( _Credential).Item(_idParameter);
            }
            private Entities.Parameter ValidateValueMinRange(Int64 idParameter, Double lowValue, Double highValue)
            {
                //Este metodo se usa para validar que el rango ingresado si es mayor al que ya existe tambien lo valide.
                Entities.Parameter _parameter = new PA.Collections.Parameters(_Credential).Item(idParameter);

                //Objeto de data layer para acceder a datos
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                //ejecuta la validacion y obtiene el idParameter, para luego retornar el parametro que lo contiene.
                Int64 _idParameter = _dbPerformanceAssessments.ParameterRanges_ValidateMinRange( _parameter.ParameterGroup.IdParameterGroup, lowValue, highValue);
                //Devuelvo el objeto parametro creado
                return new Collections.Parameters(_Credential).Item(_idParameter);
            }
            private Boolean ValidateNewRange(Int64 idParameter, Double lowValue, Double highValue)
            {
                //Realiza la validacion de que los valores no existan actualmente en el rango
                Entities.Parameter _parameter = ValidateValueRange(idParameter, lowValue);
                //si el valor menor no existe, entonces pregunto por el mayor.
                if (_parameter == null)
                {
                    _parameter = ValidateValueRange(idParameter, highValue);
                    //si tampoco existe, entonces grabo
                    if (_parameter == null)
                    {
                        //Ahora debe validar que el rango ingresado, no este siendo utilizado por un rango mas chico.
                        _parameter = ValidateValueMinRange(idParameter, lowValue, highValue);
                        if (_parameter == null)
                        {
                            //Deja grabarlo
                            return true;
                        }
                    }
                }
                //Si llego aca, entonces no puede grabar porque ya existe el valor en el rango.
                return false;
            }

    }
}
