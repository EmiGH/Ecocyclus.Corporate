using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.PA.Collections
{
    public class CalculationEstimates
    {
        #region Internal Properties
            private Credential _Credential;
        #endregion

        internal CalculationEstimates(Credential credential)
        {
            _Credential = credential;
        }

        #region Read Functions
            internal Entities.CalculationEstimated Item(Int64 idCalculation, Int64 idEstimated)
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                Entities.CalculationEstimated _calculationEstimates = null;
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.CalculationEstimates_ReadById(idCalculation, idEstimated);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    if (_calculationEstimates == null)
                    {
                        _calculationEstimates = new Entities.CalculationEstimated(Convert.ToInt64(_dbRecord["IdCalculation"]), Convert.ToInt64(_dbRecord["IdEstimated"]), Convert.ToDateTime(_dbRecord["StartDate"]), Convert.ToDateTime(_dbRecord["EndDate"]), Convert.ToInt64(_dbRecord["IdScenarioType"]), Convert.ToDecimal(_dbRecord["Value"]), _Credential);
                        return _calculationEstimates;
                    }
                }
                return _calculationEstimates;
            }
            internal List<Entities.CalculationEstimated> ItemsByCalculation(PA.Entities.Calculation calculation)
            {
                //Coleccion para devolver los Indicator
                List<Entities.CalculationEstimated> _oItems = new List<Entities.CalculationEstimated>();

                //Objeto de data layer para acceder a datos
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.CalculationEstimates_ReadByCalculation(calculation.IdCalculation);

                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    //Declara e instancia  
                    Entities.CalculationEstimated _calculationEstimated = new Entities.CalculationEstimated(Convert.ToInt64(_dbRecord["IdCalculation"]), Convert.ToInt64(_dbRecord["IdEstimated"]), Convert.ToDateTime(_dbRecord["StartDate"]), Convert.ToDateTime(_dbRecord["EndDate"]), Convert.ToInt64(_dbRecord["IdScenarioType"]), Convert.ToDecimal(_dbRecord["Value"]), _Credential);
                    //Lo agrego a la coleccion
                    _oItems.Add(_calculationEstimated);
                }
                return _oItems;
            }
            internal List<Entities.CalculationEstimated> ItemsByScenarioType(PA.Entities.Calculation calculation, PA.Entities.CalculationScenarioType calculationScenarioType)
            {
                //Coleccion para devolver los Indicator
                List<Entities.CalculationEstimated> _oItems = new List<Entities.CalculationEstimated>();

                //Objeto de data layer para acceder a datos
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.CalculationEstimates_ReadByScenarioType(calculation.IdCalculation, calculationScenarioType.IdScenarioType);

                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    //Declara e instancia  
                    Entities.CalculationEstimated _calculationEstimated = new Entities.CalculationEstimated(Convert.ToInt64(_dbRecord["IdCalculation"]), Convert.ToInt64(_dbRecord["IdEstimated"]), Convert.ToDateTime(_dbRecord["StartDate"]), Convert.ToDateTime(_dbRecord["EndDate"]), Convert.ToInt64(_dbRecord["IdScenarioType"]), Convert.ToDecimal(_dbRecord["Value"]), _Credential);
                    //Lo agrego a la coleccion
                    _oItems.Add(_calculationEstimated);
                }
                return _oItems;
            }

        #endregion

        #region Write Functions
            internal Entities.CalculationEstimated Add(Entities.Calculation calculation, DateTime startDate, DateTime endDate, PA.Entities.CalculationScenarioType calculationScenarioType, Decimal value)
            {
                try
                {
                    //cargo variables que se que no van a venir en el add, como el result y date del result
                    DateTime _dateLastResult = DateTime.MinValue;
                    //Objeto de data layer para acceder a datos
                    DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                    //Si no existe, la puede crear
                    if (!ValidateRageOfDateExists(calculation, startDate, endDate, calculationScenarioType))
                    {
                        //Ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                        Int64 _idEstimated = _dbPerformanceAssessments.CalculationEstimates_Create(calculation.IdCalculation, startDate, endDate, calculationScenarioType.IdScenarioType, value, _Credential.User.Person.IdPerson);
                        //Devuelvo el objeto creado
                        return new Entities.CalculationEstimated(calculation.IdCalculation, _idEstimated, startDate, endDate, calculationScenarioType.IdScenarioType, value, _Credential);
                    }
                    else
                    {
                        throw new Exception(Common.Resources.Errors.IncorrectRangeOfDate);
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
            internal void Remove(Entities.Calculation calculation, Int64 idEstimated)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();
                    
                    //Borrar de la base de datos el certificado...
                    _dbPerformanceAssessments.CalculationEstimates_Delete(calculation.IdCalculation, idEstimated, _Credential.User.Person.IdPerson);
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
            internal void Modify(Entities.Calculation calculation, Int64 idEstimated, DateTime startDate, DateTime endDate, PA.Entities.CalculationScenarioType calculationScenarioType, Decimal value)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                    //Modifico los datos de la base
                    _dbPerformanceAssessments.CalculationEstimates_Update(calculation.IdCalculation, idEstimated, startDate, endDate, calculationScenarioType.IdScenarioType, value, _Credential.User.Person.IdPerson);
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

        #region Internal Methods
            /// <summary>
            /// Realiza la verificacion del rango de fechas para el Rango de la estimacion. No se pueden pisar las fechas.
            /// </summary>
            /// <param name="calculation"></param>
            /// <param name="startDate"></param>
            /// <param name="endDate"></param>
            /// <param name="calculationScenarioType"></param>
            /// <returns></returns>
            private Boolean ValidateRageOfDateExists(Entities.Calculation calculation, DateTime startDate, DateTime endDate, PA.Entities.CalculationScenarioType calculationScenarioType)
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                //Ejecuta la validacion sobre la base para saber si ya existe o se pisan las fechas de la estimacion para el calculo y mismo escenario.
                return _dbPerformanceAssessments.CalculationEstimates_Exists(calculation.IdCalculation, startDate, endDate, calculationScenarioType.IdScenarioType);
            }
        #endregion

    }
}
