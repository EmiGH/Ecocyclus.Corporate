using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.PA.Entities
{
    internal class Parameters
    {
        internal Parameters()
        { }

        #region Read Functions
            internal IEnumerable<DbDataRecord> ReadAll(Int64 idParameterGroup, String idLanguage)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Parameters_ReadAll");
                _db.AddInParameter(_dbCommand, "IdParameterGroup", DbType.Int64, idParameterGroup);
                _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
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
            internal IEnumerable<DbDataRecord> ReadById(Int64 idParameter, String idLanguage)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Parameters_ReadById");
                _db.AddInParameter(_dbCommand, "IdParameter", DbType.Int64, idParameter);
                _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
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
            internal Int64 Create(Int64 idParameterGroup, Int64 idIndicator, String description, String sign, Boolean raiseException, String idLanguage, Int64 idLogPerson)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Parameters_Create");
                _db.AddInParameter(_dbCommand, "IdParameterGroup", DbType.Int64, idParameterGroup);
                _db.AddInParameter(_dbCommand, "IdIndicator", DbType.Int64, idIndicator);
                _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
                _db.AddInParameter(_dbCommand, "Description", DbType.String, description);
                _db.AddInParameter(_dbCommand, "Sign", DbType.String, sign);
                _db.AddInParameter(_dbCommand, "RaiseException", DbType.Boolean, raiseException);
                _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);
                //Parámetro de salida
                _db.AddOutParameter(_dbCommand, "IdParameter", DbType.Int64, 18);
                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);

                //Retorna el identificador
                return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdParameter"));
            }
            internal void Delete(Int64 idParameter, Int64 idLogPerson)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Parameters_Delete");
                _db.AddInParameter(_dbCommand, "IdParameter", DbType.Int64, idParameter);
                _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }
            internal void Update(Int64 idParameter, Int64 idParameterGroup, Int64 idIndicator, String idLanguage, String Description, String sign, Boolean raiseException, Int64 idLogPerson)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Parameters_Update");
                _db.AddInParameter(_dbCommand, "IdParameter", DbType.Int64, idParameter);
                _db.AddInParameter(_dbCommand, "IdParameterGroup", DbType.Int64, idParameterGroup);
                _db.AddInParameter(_dbCommand, "IdIndicator", DbType.Int64, idIndicator);
                _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
                _db.AddInParameter(_dbCommand, "Description", DbType.String, Description);
                _db.AddInParameter(_dbCommand, "Sign", DbType.String, sign);
                _db.AddInParameter(_dbCommand, "RaiseException", DbType.Boolean, raiseException);
                _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }
        #endregion
    }
}
