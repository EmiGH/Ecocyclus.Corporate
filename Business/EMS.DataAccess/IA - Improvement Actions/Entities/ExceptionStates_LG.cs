using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.IA.Entities
{
    internal class ExceptionStates_LG
    {
        internal ExceptionStates_LG()
        {
        }

        #region Write Functions

        internal void Create(Int64 idExceptionState, String idLanguage, String name, Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("IA_ExceptionStates_LG_Create");
            _db.AddInParameter(_dbCommand, "idExceptionState", DbType.Int64, idExceptionState);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
            _db.AddInParameter(_dbCommand, "Name", DbType.String, name);
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

     
        internal void Delete(Int64 idExceptionState, String idLanguage, Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("IA_ExceptionStates_LG_Delete");
            _db.AddInParameter(_dbCommand, "idExceptionState", DbType.String, idExceptionState);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }


        internal void Update(Int64 idExceptionState, String idLanguage, String name, Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("IA_ExceptionStates_LG_Update");
            _db.AddInParameter(_dbCommand, "idExceptionState", DbType.Int64, idExceptionState);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
            _db.AddInParameter(_dbCommand, "Name", DbType.String, name);
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        #endregion

        #region Read Functions


        internal IEnumerable<DbDataRecord> ReadAll(Int64 idExceptionState)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("IA_ExceptionStates_LG_ReadAll");
            _db.AddInParameter(_dbCommand, "idExceptionState", DbType.Int64, idExceptionState);
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

            DbCommand _dbCommand = _db.GetStoredProcCommand("IA_ExceptionStates_LG_ReadById");
            _db.AddInParameter(_dbCommand, "IdExceptionState", DbType.Int64, idExceptionState);
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
    }
}
