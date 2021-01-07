using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.KC.Entities
{
    internal class ResourceHistoryStates 
    {
        internal ResourceHistoryStates() { }

        #region Read Functions

        //Trae todos los States
        internal IEnumerable<DbDataRecord> ReadAll(String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("KC_ResourceHistoryStates_ReadAll");
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

        //Trae el type pedido
        internal IEnumerable<DbDataRecord> ReadById(Int64 idResourceState, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("KC_ResourceHistoryStates_ReadById");
            _db.AddInParameter(_dbCommand, "IdResourceFileState", DbType.Int64, idResourceState);
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

        internal Int64 Create(Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("KC_ResourceHistoryStates_Create");
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);
            //Parámetro de salida
            _db.AddOutParameter(_dbCommand, "IdResourceFileState", DbType.Int64, 18);
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdResourceFileState"));
        }

        internal void Delete(Int64 idResourceState, Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("KC_ResourceHistoryStates_Delete");
            _db.AddInParameter(_dbCommand, "IdResourceFileState", DbType.Int64, idResourceState);
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        #endregion

    }
}
