using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.IA.Entities
{
    internal class ExceptionStates
    {
        internal ExceptionStates()
        {
        }

        #region Read Functions

       
        internal IEnumerable<DbDataRecord> ReadAll(String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("IA_ExceptionStates_ReadAll");        
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

        internal IEnumerable<DbDataRecord> ReadById(Int64 idExceptionState, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("IA_ExceptionStates_ReadById");
            _db.AddInParameter(_dbCommand, "idExceptionState", DbType.Int64, idExceptionState);
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

        internal void ModifyState(Int64 IdException, Int64 IdExceptionState, String Comment, DateTime ExceptionDate, Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("IA_ExceptionHistories_Create");
            _db.AddInParameter(_dbCommand, "IdException", DbType.Int64, IdException);
            _db.AddInParameter(_dbCommand, "IdExceptionState", DbType.Int64, IdExceptionState);
            _db.AddInParameter(_dbCommand, "ExceptionDate", DbType.DateTime, ExceptionDate);
            _db.AddInParameter(_dbCommand, "Comment", DbType.String, Comment);
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        
        }
        #endregion
    }
}
