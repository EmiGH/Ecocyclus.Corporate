using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.PF.Entities
{
    internal class Processes_LG
    {
        internal Processes_LG()
        {
        }
        #region Read Functions
            internal IEnumerable<DbDataRecord> ReadAll(Int64 idProcess)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_Processes_LG_ReadAll");
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
            internal IEnumerable<DbDataRecord> ReadById(Int64 idProcess, String idLanguage)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_Processes_LG_ReadById");
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
        #endregion

        #region Write Functions
            internal void Create(Int64 idProcess, String idLanguage, String title, String purpose, String description)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_Processes_LG_Create");
                _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);
                _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
                _db.AddInParameter(_dbCommand, "Title", DbType.String, title);
                _db.AddInParameter(_dbCommand, "Purpose", DbType.String, purpose);
                _db.AddInParameter(_dbCommand, "Description", DbType.String, description);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }
            internal void Delete(Int64 idProcess, String idLanguage)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_Processes_LG_Delete");
                _db.AddInParameter(_dbCommand, "IdProcess", DbType.String, idProcess);
                _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }
            internal void Delete(Int64 idProcess)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_Processes_LG_DeleteByProcess");
                _db.AddInParameter(_dbCommand, "IdProcess", DbType.String, idProcess);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }
            internal void Update(Int64 idProcess, String idLanguage, String title, String purpose, String description)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_Processes_LG_Update");
                _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);
                _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
                _db.AddInParameter(_dbCommand, "Title", DbType.String, title);
                _db.AddInParameter(_dbCommand, "Purpose", DbType.String, purpose);
                _db.AddInParameter(_dbCommand, "Description", DbType.String, description);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }
        #endregion

    }
}
