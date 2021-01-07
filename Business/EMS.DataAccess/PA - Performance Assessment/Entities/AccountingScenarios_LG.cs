using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.PA.Entities
{
    class AccountingScenarios_LG
    {
        internal AccountingScenarios_LG()
        { }

        #region Read Functions
        internal IEnumerable<DbDataRecord> ReadById(Int64 idScenario, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_AccountingScenarios_LG_ReadByID");
            _db.AddInParameter(_dbCommand, "IdScenario", DbType.Int64, idScenario);
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
        internal IEnumerable<DbDataRecord> ReadAll(Int64 idScenario)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_AccountingScenarios_LG_ReadAll");
            _db.AddInParameter(_dbCommand, "idScenario", DbType.Int64, idScenario);
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

        internal void Create(Int64 idScenario, String idLanguage, String name, String description)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_AccountingScenarios_LG_Create");
            _db.AddInParameter(_dbCommand, "IdScenario", DbType.Int64, idScenario);
            _db.AddInParameter(_dbCommand, "idLanguage", DbType.String, idLanguage);
            _db.AddInParameter(_dbCommand, "name", DbType.String, name);
            _db.AddInParameter(_dbCommand, "description", DbType.String, description);
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        internal void Delete(Int64 idScenario, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_AccountingScenarios_LG_Delete");
            _db.AddInParameter(_dbCommand, "IdScenario", DbType.Int64, idScenario);
            _db.AddInParameter(_dbCommand, "idLanguage", DbType.String, idLanguage);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        internal void Delete(Int64 idScenario)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_AccountingScenarios_LG_DeleteByAccountingScenario");
            _db.AddInParameter(_dbCommand, "IdScenario", DbType.Int64, idScenario);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        internal void Update(Int64 idScenario, String idLanguage, String name, String description)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_AccountingScenarios_LG_Update");
            _db.AddInParameter(_dbCommand, "IdScenario", DbType.Int64, idScenario);
            _db.AddInParameter(_dbCommand, "idLanguage", DbType.String, idLanguage);
            _db.AddInParameter(_dbCommand, "name", DbType.String, name);
            _db.AddInParameter(_dbCommand, "description", DbType.String, description);
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        #endregion
    }
}
