using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.KC.Entities
{
    internal class FileAttaches
    {
        internal FileAttaches() { }

        #region Read Function
            internal IEnumerable<DbDataRecord> ReadById(Int64 idFile)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("KC_FileAttachs_ReadById");
                _db.AddInParameter(_dbCommand, "IdFile", DbType.Int64, idFile);
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
            internal IEnumerable<DbDataRecord> ReadFileStream(Int64 idFile)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("KC_FileAttachs_ReadFileStream");
                _db.AddInParameter(_dbCommand, "IdFile", DbType.Int64, idFile);
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
        
        #region Write Function
            internal Int64 Create(String fileName, Byte[] fileStream)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("KC_FileAttachs_Create");
                _db.AddInParameter(_dbCommand, "FileName", DbType.String, fileName);
                _db.AddInParameter(_dbCommand, "FileStream", DbType.Binary, fileStream);
                //Parámetro de salida
                _db.AddOutParameter(_dbCommand, "IdFile", DbType.Int64, 18);

                _dbCommand.CommandTimeout = 0;
                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);

                //Retorna el identificador
                return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdFile"));
            }
            internal void Update(Int64 idFile, String fileName, Byte[] fileStream)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("KC_FileAttachs_Update");
                _db.AddInParameter(_dbCommand, "IdFile", DbType.Int64, idFile);
                _db.AddInParameter(_dbCommand, "FileName", DbType.String, fileName);
                _db.AddInParameter(_dbCommand, "FileStream", DbType.Binary, fileStream);

                _dbCommand.CommandTimeout = 0;
                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);

            }
            internal void Delete(Int64 idFile)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("KC_FileAttachs_Delete");
                _db.AddInParameter(_dbCommand, "IdFile", DbType.Int64, idFile);

                _dbCommand.CommandTimeout = 0;
                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);

            }
            //internal Int64 Create(String fileName, String fileStream)
            //{
            //    Database _db = DatabaseFactory.CreateDatabase();

            //    DbCommand _dbCommand = _db.GetStoredProcCommand("KC_FileAttachs_Create");
            //    _db.AddInParameter(_dbCommand, "FileName", DbType.String, fileName);
            //    _db.AddInParameter(_dbCommand, "FileStream", DbType.String, fileStream);
            //    //Parámetro de salida
            //    _db.AddOutParameter(_dbCommand, "IdFile", DbType.Int64, 18);
            //    //Ejecuta el comando
            //    _db.ExecuteNonQuery(_dbCommand);

            //    //Retorna el identificador
            //    return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdFile"));
            //}
        #endregion
    }
}
