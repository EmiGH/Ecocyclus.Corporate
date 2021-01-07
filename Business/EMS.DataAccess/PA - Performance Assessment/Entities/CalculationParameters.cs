using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.PA.Entities
{
    internal class CalculationParameters
    {
        internal CalculationParameters()
        { }


        #region Read Functions
        internal IEnumerable<DbDataRecord> ReadAll(Int64 idCalculation)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculationParameters_ReadAll");
                _db.AddInParameter(_dbCommand, "idCalculation", DbType.String, idCalculation);
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
        #endregion

        #region Write Functions
            internal void Create(Int64 idCalculation, Int64 positionParameter, Int64 IdMeasurementParameter, String parameterName)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculationParameters_Create");
                _db.AddInParameter(_dbCommand, "idCalculation", DbType.Int64, idCalculation);
                _db.AddInParameter(_dbCommand, "positionParameter", DbType.Int64, positionParameter);
                _db.AddInParameter(_dbCommand, "IdMeasurementParameter", DbType.Int64, IdMeasurementParameter);
                _db.AddInParameter(_dbCommand, "parameterName", DbType.String, parameterName);
                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }
            internal void Delete(Int64 idCalculation, Int64 positionParameter, Int64 idLogPerson)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculationParameters_Delete");
                _db.AddInParameter(_dbCommand, "idCalculation", DbType.Int64, idCalculation);
                _db.AddInParameter(_dbCommand, "positionParameter", DbType.Int64, positionParameter);
                _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }
            internal void Delete(Int64 idCalculation)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculationParameters_DeleteByCalculation");
                _db.AddInParameter(_dbCommand, "idCalculation", DbType.Int64, idCalculation);
                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }

            internal void Update(Int64 idCalculation, Int64 positionParameter, Int64 IdMeasurementParameter, String parameterName, Int64 idLogPerson)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculationParameters_Update");
                _db.AddInParameter(_dbCommand, "idCalculation", DbType.Int64, idCalculation);
                _db.AddInParameter(_dbCommand, "positionParameter", DbType.Int64, positionParameter);
                _db.AddInParameter(_dbCommand, "IdMeasurementParameter", DbType.Int64, IdMeasurementParameter);
                _db.AddInParameter(_dbCommand, "parameterName", DbType.String, parameterName);
                _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }
        
        #endregion
    }
}
