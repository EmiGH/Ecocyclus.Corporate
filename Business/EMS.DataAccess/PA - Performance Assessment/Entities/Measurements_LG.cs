using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.PA.Entities
{
    internal class Measurements_LG
    {
        internal Measurements_LG()
        { }

        #region Read Functions
            internal IEnumerable<DbDataRecord> ReadAll(Int64 idMeasurement)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Measurements_LG_ReadAll");
                _db.AddInParameter(_dbCommand, "IdMeasurement", DbType.Int64, idMeasurement);
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
            internal IEnumerable<DbDataRecord> ReadById(Int64 idMeasurement, String idLanguage)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Measurements_LG_ReadById");
                _db.AddInParameter(_dbCommand, "IdMeasurement", DbType.Int64, idMeasurement);
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
            internal void Create(Int64 idMeasurement, String idLanguage, String name, String Description)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Measurements_LG_Create");
                _db.AddInParameter(_dbCommand, "idMeasurement", DbType.Int64, idMeasurement);
                _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
                _db.AddInParameter(_dbCommand, "Name", DbType.String, name);
                _db.AddInParameter(_dbCommand, "Description", DbType.String, Description);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }
            internal void Delete(Int64 idMeasurement, String idLanguage)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Measurements_LG_Delete");
                _db.AddInParameter(_dbCommand, "IdMeasurement", DbType.String, idMeasurement);
                _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }
            internal void Delete(Int64 idMeasurement)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Measurements_LG_DeleteByMeasurement");
                _db.AddInParameter(_dbCommand, "IdMeasurement", DbType.String, idMeasurement);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }
            internal void Update(Int64 idMeasurement, String idLanguage, String name, String Description)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Measurements_LG_Update");
                _db.AddInParameter(_dbCommand, "IdMeasurement", DbType.Int64, idMeasurement);
                _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
                _db.AddInParameter(_dbCommand, "Name", DbType.String, name);
                _db.AddInParameter(_dbCommand, "Description", DbType.String, Description);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }
        #endregion
    }
}
