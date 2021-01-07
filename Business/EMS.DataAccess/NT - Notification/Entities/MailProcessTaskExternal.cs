using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.NT.Entities
{
    internal class MailProcessTaskExternal
    {
        internal MailProcessTaskExternal() { }

        #region Read Functions

        internal IEnumerable<DbDataRecord> ReadByProcess(Int64 IdProcess)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("NT_MailProcessTaskExternal_ReadByProcess");
            _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, IdProcess);

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

        internal void Create(Int64 idProcess, String email)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("NT_MailProcessTaskExternal_Create");
            _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess); 
            _db.AddInParameter(_dbCommand, "Email", DbType.String, email);
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

        }

        internal void Delete(Int64 idProcess)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("NT_MailProcessTaskExternal_DeleteByProcess");
            _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        internal void Delete(Int64 idProcess, String email)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("NT_MailProcessTaskExternal_Delete");
            _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);
            _db.AddInParameter(_dbCommand, "Email", DbType.String, email);
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        internal void Update(Int64 idProcess, String email)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("NT_MailProcessTaskExternal_Update");
            _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);
            _db.AddInParameter(_dbCommand, "Email", DbType.String, email);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        #endregion
    }
}
