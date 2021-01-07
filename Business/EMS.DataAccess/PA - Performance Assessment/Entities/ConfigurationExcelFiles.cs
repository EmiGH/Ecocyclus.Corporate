using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.PA.Entities
{
    internal class ConfigurationExcelFiles
    {
        internal ConfigurationExcelFiles()
        { }

        #region Read Functions
        internal IEnumerable<DbDataRecord> ReadById(Int64 IdExcelFile)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_ConfigurationExcelFiles_ReadById");
                _db.AddInParameter(_dbCommand, "IdExcelFile", DbType.Int64, IdExcelFile);

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
        internal IEnumerable<DbDataRecord> ReadByPerson(Int64 IdPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_ConfigurationExcelFiles_ReadByPerson");
            _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, IdPerson);

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
            internal IEnumerable<DbDataRecord> ReadAll()
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_ConfigurationExcelFiles_ReadAll");
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

            internal Int64 Create(String Name, String StartIndexOfDataRows, String StartIndexOfDataCols, Boolean IsDataRows, String IndexStartDate, String IndexEndDate)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_ConfigurationExcelFiles_Create");
                _db.AddInParameter(_dbCommand, "Name", DbType.String, Name);
                _db.AddInParameter(_dbCommand, "StartIndexOfDataRows", DbType.String, StartIndexOfDataRows);
                _db.AddInParameter(_dbCommand, "StartIndexOfDataCols", DbType.String, StartIndexOfDataCols);
                _db.AddInParameter(_dbCommand, "IsDataRows", DbType.Boolean, IsDataRows);
                _db.AddInParameter(_dbCommand, "IndexStartDate", DbType.String, IndexStartDate);
                _db.AddInParameter(_dbCommand, "IndexEndDate", DbType.String, IndexEndDate);
                //Parámetro de salida
                _db.AddOutParameter(_dbCommand, "IdExcelFile", DbType.Int64, 18);
                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);

                //Retorna el identificador
                return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdExcelFile"));
            }


            internal void Delete(Int64 IdExcelFile)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_ConfigurationExcelFiles_Delete");
                _db.AddInParameter(_dbCommand, "IdExcelFile", DbType.Int64, IdExcelFile);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }


            internal void Update(Int64 IdExcelFile, String Name, String StartIndexOfDataRows, String StartIndexOfDataCols, Boolean IsDataRows, String IndexStartDate, String IndexEndDate)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_ConfigurationExcelFiles_Update");
                _db.AddInParameter(_dbCommand, "IdExcelFile", DbType.Int64, IdExcelFile);
                _db.AddInParameter(_dbCommand, "Name", DbType.String, Name);
                _db.AddInParameter(_dbCommand, "StartIndexOfDataRows", DbType.String, StartIndexOfDataRows);
                _db.AddInParameter(_dbCommand, "StartIndexOfDataCols", DbType.String, StartIndexOfDataCols);
                _db.AddInParameter(_dbCommand, "IsDataRows", DbType.Boolean, IsDataRows);
                _db.AddInParameter(_dbCommand, "IndexStartDate", DbType.String, IndexStartDate);
                _db.AddInParameter(_dbCommand, "IndexEndDate", DbType.String, IndexEndDate);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }
        #endregion


        #region ConfigurationExcelFilesMeasurements
            internal IEnumerable<DbDataRecord> ReadByFile(Int64 IdExcelFile)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_ConfigurationExcelFilesMeasurements_ReadByFile");
                _db.AddInParameter(_dbCommand, "IdExcelFile", DbType.Int64, IdExcelFile);

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
            internal void CreateRelationship(Int64 IdExcelFile, Int64 IdMeasurement, String IndexValue , String IndexDate )
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_ConfigurationExcelFilesMeasurements_Create");
                _db.AddInParameter(_dbCommand, "IdExcelFile", DbType.Int64, IdExcelFile);
                _db.AddInParameter(_dbCommand, "IdMeasurement", DbType.Int64, IdMeasurement);
                _db.AddInParameter(_dbCommand, "IndexValue", DbType.String, IndexValue);
                _db.AddInParameter(_dbCommand, "IndexDate", DbType.String, IndexDate);
                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }
            internal void DeleteRelationship(Int64 IdExcelFile)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_ConfigurationExcelFilesMeasurements_Delete");
                _db.AddInParameter(_dbCommand, "IdExcelFile", DbType.Int64, IdExcelFile);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }
            internal void DeleteRelationshipByMeasurement(Int64 IdMeasurement)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_ConfigurationExcelFilesMeasurements_DeleteByMeasurement");
                _db.AddInParameter(_dbCommand, "IdMeasurement", DbType.Int64, IdMeasurement);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }
        #endregion

    }
}
