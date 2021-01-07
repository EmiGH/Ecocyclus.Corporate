using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.PF.Entities
{
    internal class ProcessGroupExceptions
    {
        internal ProcessGroupExceptions() { }

        #region Read Functions
            internal IEnumerable<DbDataRecord> ReadById(Int64 idProcess, String idLanguage)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessGroupExceptions_ReadById");
                _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);
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
            internal IEnumerable<DbDataRecord> ReadAssociatedTask(Int64 idProcess)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessGroupExceptions_ReadAssociatedTask");
                _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);

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
            internal IEnumerable<DbDataRecord> ReadByException(Int64 idException, String idLanguage)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessGroupProjects_ReadByException");
                _db.AddInParameter(_dbCommand, "idException", DbType.Int64, idException);
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

        #region Write Function
            //// ADD Exception
            internal void Create(Int64 idProcess)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessGroupExceptions_Create");
                _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);
                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);

            }
            internal void Delete(Int64 idProcess)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessGroupExceptions_Delete");
                _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }
        #endregion
    }
}
