using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;



namespace Condesus.EMS.DataAccess.PF.Entities
{
    internal class ProcessGroupProcessForums
    {
              /// <summary>
        /// Constructor de un PF_ProcessGroupProcessForums
        /// </summary>
        internal ProcessGroupProcessForums()
        {}

        #region Read Functions   
        internal IEnumerable<DbDataRecord> ReadByProcess(Int64 idProcess, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessGroupProcessForums_ReadByProcess");
            _db.AddInParameter(_dbCommand, "idProcess", DbType.Int64, idProcess);
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
        internal IEnumerable<DbDataRecord> ReadByForum(Int64 idForum)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessGroupProcessForums_ReadByForum");
            _db.AddInParameter(_dbCommand, "idForum", DbType.Int64, idForum);

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


        internal void Create(Int64 idProcess,Int64 idForum)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessGroupProcessForums_Create");
            _db.AddInParameter(_dbCommand, "idProcess", DbType.Int64, idProcess);
            _db.AddInParameter(_dbCommand, "idForum", DbType.Int64, idForum);
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

        }
  
       
        internal void Delete(Int64 idProcess, Int64 idForum)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessGroupProcessForums_Delete");
            _db.AddInParameter(_dbCommand, "idProcess", DbType.Int64, idProcess);
            _db.AddInParameter(_dbCommand, "idForum", DbType.Int64, idForum);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
       
        #endregion
 
    }
}
