using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.PA.Entities
{
    internal class ParameterRanges
    {
        internal ParameterRanges()
        { }

        #region Write Functions
        internal Int64 Add(Int64 idParameter, Double lowValue, Double highValue)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_ParameterRanges_Create");
                _db.AddInParameter(_dbCommand, "IdParameter", DbType.Int64, idParameter);
                _db.AddInParameter(_dbCommand, "LowValue", DbType.Double, lowValue);
                _db.AddInParameter(_dbCommand, "HighValue", DbType.Double, highValue);

                //Parámetro de salida
                _db.AddOutParameter(_dbCommand, "IdParameterRange", DbType.Int64, 18);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);

                //Retorna el identificador
                return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdParameterRange"));
            }
            internal void Remove(Int64 idParameterRange)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_ParameterRanges_Delete");
                _db.AddInParameter(_dbCommand, "IdParameterRange", DbType.Int64, idParameterRange);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }
            internal void RemoveByParameter(Int64 idParameter)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_ParameterRanges_DeleteByParameter");
                _db.AddInParameter(_dbCommand, "IdParameter", DbType.Int64, idParameter);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }
            internal void Update(Int64 idParameterRange, Int64 idParameter, Double lowValue, Double highValue)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_ParameterRanges_Update");
                _db.AddInParameter(_dbCommand, "idParameterRange", DbType.Int64, idParameterRange);
                _db.AddInParameter(_dbCommand, "IdParameter", DbType.Int64, idParameter);
                _db.AddInParameter(_dbCommand, "LowValue", DbType.Double, lowValue);
                _db.AddInParameter(_dbCommand, "HighValue", DbType.Double, highValue);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }
        #endregion

        #region Read Functions
            internal IEnumerable<DbDataRecord> ReadById(Int64 idParameter, Int64 idParameterRange)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("[PA_ParameterRanges_ReadById]");
                _db.AddInParameter(_dbCommand, "IdParameter", DbType.Int64, idParameter);
                _db.AddInParameter(_dbCommand, "idParameterRange", DbType.Int64, idParameterRange);
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
            internal IEnumerable<DbDataRecord> ReadAll(Int64 idParameter)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_ParameterRanges_ReadAll");
                _db.AddInParameter(_dbCommand, "IdParameter", DbType.Int64, idParameter);
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
            internal Int64 Validate(Int64 idParameterGroup, Double value)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_ParameterRanges_Validate");
                _db.AddInParameter(_dbCommand, "IdParameterGroup", DbType.Int64, idParameterGroup);
                _db.AddInParameter(_dbCommand, "Value", DbType.Double, value);

                //Parámetro de salida
                _db.AddOutParameter(_dbCommand, "IdParameter", DbType.Int64, 18);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);

                //Retorna el identificador
                return Convert.ToInt64(Common.Common.CastNullValues(_db.GetParameterValue(_dbCommand, "IdParameter"), 0));
            }
            internal Int64 ValidateMinRange(Int64 idParameterGroup, Double lowValue, Double highValue)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_ParameterRanges_ValidateMinRange");
                _db.AddInParameter(_dbCommand, "IdParameterGroup", DbType.Int64, idParameterGroup);
                _db.AddInParameter(_dbCommand, "LowValue", DbType.Double, lowValue);
                _db.AddInParameter(_dbCommand, "HighValue", DbType.Double, highValue);

                //Parámetro de salida
                _db.AddOutParameter(_dbCommand, "IdParameter", DbType.Int64, 18);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);

                //Retorna el identificador
                return Convert.ToInt64(Common.Common.CastNullValues(_db.GetParameterValue(_dbCommand, "IdParameter"), 0));
            }
        #endregion
    }
}
