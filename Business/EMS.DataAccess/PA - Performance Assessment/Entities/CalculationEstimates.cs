using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.PA.Entities
{
    internal class CalculationEstimates
    {
        internal CalculationEstimates()
        { }

        #region Read Functions
            internal IEnumerable<DbDataRecord> ReadById(Int64 idCalculation, Int64 idEstimated)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculationUserEstimated_ReadById");
                _db.AddInParameter(_dbCommand, "idCalculation", DbType.Int64, idCalculation);
                _db.AddInParameter(_dbCommand, "IdEstimated", DbType.Int64, idEstimated);
                SqlDataReader _reader = (SqlDataReader)_db.ExecuteReader(_dbCommand);

                try
                {
                    foreach (DbDataRecord _record in _reader)
                    {
                        yield return _record;
                    }
                }
                finally
                {
                    _reader.Close();
                }
            }
            internal IEnumerable<DbDataRecord> ReadByCalculation(Int64 idCalculation)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculationUserEstimated_ReadAll");
                _db.AddInParameter(_dbCommand, "idCalculation", DbType.Int64, idCalculation);
                SqlDataReader _reader = (SqlDataReader)_db.ExecuteReader(_dbCommand);

                try
                {
                    foreach (DbDataRecord _record in _reader)
                    {
                        yield return _record;
                    }
                }
                finally
                {
                    _reader.Close();
                }
            }
            /// <summary>
            /// Trae todos los Estimados de un calculo y de un escenario determinado
            /// </summary>
            /// <param name="idScenarioType"></param>
            /// <returns></returns>
            internal IEnumerable<DbDataRecord> ReadByScenarioType(Int64 idCalculation, Int64 idScenarioType)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculationUserEstimated_ReadByScenarioType");
                _db.AddInParameter(_dbCommand, "idCalculation", DbType.Int64, idCalculation);
                _db.AddInParameter(_dbCommand, "IdScenarioType", DbType.Int64, idScenarioType);
                SqlDataReader _reader = (SqlDataReader)_db.ExecuteReader(_dbCommand);

                try
                {
                    foreach (DbDataRecord _record in _reader)
                    {
                        yield return _record;
                    }
                }
                finally
                {
                    _reader.Close();
                }
            }
            /// <summary>
            /// Verifica la existencia de uns Estimacion (dentro del mismo escenario y calculo) para la misma fecha.
            /// </summary>
            /// <param name="idCalculation"></param>
            /// <param name="idOrganization"></param>
            /// <param name="startDate"></param>
            /// <param name="endDate"></param>
            /// <param name="idScenarioType"></param>
            /// <returns></returns>
            internal Boolean Exists(Int64 idCalculation, DateTime startDate, DateTime endDate, Int64 idScenarioType)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculationUserEstimated_Exists");
                _db.AddInParameter(_dbCommand, "idCalculation", DbType.Int64, idCalculation);
                _db.AddInParameter(_dbCommand, "IdScenarioType", DbType.Int64, idScenarioType);
                _db.AddInParameter(_dbCommand, "StartDateNew", DbType.DateTime, startDate);
                _db.AddInParameter(_dbCommand, "EndDateNew", DbType.DateTime, endDate);

                //Parámetro de salida
                _db.AddOutParameter(_dbCommand, "Exists", DbType.Boolean, 18);
                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);

                //Retorna el identificador
                return Convert.ToBoolean(_db.GetParameterValue(_dbCommand, "Exists"));
            }

        #endregion

        #region Write Functions
            internal Int64 Create(Int64 idCalculation, DateTime startDate, DateTime endDate, Int64 idScenarioType, Decimal value, Int64 idLogPerson)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculationUserEstimated_Create");
                _db.AddInParameter(_dbCommand, "idCalculation", DbType.Int64, idCalculation);
                _db.AddInParameter(_dbCommand, "StartDate", DbType.DateTime, startDate);
                _db.AddInParameter(_dbCommand, "EndDate", DbType.DateTime, endDate);
                _db.AddInParameter(_dbCommand, "IdScenarioType", DbType.Int64, idScenarioType);
                _db.AddInParameter(_dbCommand, "Value", DbType.Decimal, value);
                _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);
                //Parámetro de salida
                _db.AddOutParameter(_dbCommand, "IdEstimated", DbType.Int64, 18);
                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);

                //Retorna el identificador
                return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdEstimated"));
            }
            internal void Delete(Int64 idCalculation, Int64 idEstimated, Int64 idLogPerson)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculationUserEstimated_Delete");
                _db.AddInParameter(_dbCommand, "idCalculation", DbType.Int64, idCalculation);
                _db.AddInParameter(_dbCommand, "idEstimated", DbType.Int64, idEstimated);
                _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }
            internal void Update(Int64 idCalculation, Int64 idEstimated, DateTime startDate, DateTime endDate, Int64 idScenarioType, Decimal value, Int64 idLogPerson)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculationUserEstimated_Update");
                _db.AddInParameter(_dbCommand, "idCalculation", DbType.Int64, idCalculation);
                _db.AddInParameter(_dbCommand, "idEstimated", DbType.Int64, idEstimated);
                _db.AddInParameter(_dbCommand, "StartDate", DbType.DateTime, startDate);
                _db.AddInParameter(_dbCommand, "EndDate", DbType.DateTime, endDate);
                _db.AddInParameter(_dbCommand, "IdScenarioType", DbType.Int64, idScenarioType);
                _db.AddInParameter(_dbCommand, "Value", DbType.Decimal, value);
                _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }
        #endregion

    }
}
