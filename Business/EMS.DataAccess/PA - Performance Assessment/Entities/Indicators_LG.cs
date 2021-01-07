using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.PA.Entities
{
    internal class Indicators_LG
    {
        internal Indicators_LG()
        { }

        #region Read Functions
        internal IEnumerable<DbDataRecord> ReadAll(Int64 idIndicator)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Indicators_LG_ReadAll");
            _db.AddInParameter(_dbCommand, "IdIndicator", DbType.Int64, idIndicator);
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
        internal IEnumerable<DbDataRecord> ReadById(Int64 idIndicator, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Indicators_LG_ReadById");
            _db.AddInParameter(_dbCommand, "IdIndicator", DbType.Int64, idIndicator);
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
        internal void Create(Int64 idIndicator, String idLanguage, String name, String Description, String scope, String limitation, String definition)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Indicators_LG_Create");
            _db.AddInParameter(_dbCommand, "idIndicator", DbType.Int64, idIndicator);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
            _db.AddInParameter(_dbCommand, "Name", DbType.String, name);
            _db.AddInParameter(_dbCommand, "Description", DbType.String, Description);
            _db.AddInParameter(_dbCommand, "Scope", DbType.String, scope);
            _db.AddInParameter(_dbCommand, "Limitation", DbType.String, limitation);
            _db.AddInParameter(_dbCommand, "Definition", DbType.String, definition);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void Delete(Int64 idIndicator, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Indicators_LG_Delete");
            _db.AddInParameter(_dbCommand, "IdIndicator", DbType.Int64, idIndicator);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void Delete(Int64 idIndicator)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Indicators_LG_DeleteByIndicator");
            _db.AddInParameter(_dbCommand, "IdIndicator", DbType.Int64, idIndicator);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void Update(Int64 idIndicator, String idLanguage, String name, String Description, String scope, String limitation, String definition)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Indicators_LG_Update");
            _db.AddInParameter(_dbCommand, "IdIndicator", DbType.Int64, idIndicator);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
            _db.AddInParameter(_dbCommand, "Name", DbType.String, name);
            _db.AddInParameter(_dbCommand, "Description", DbType.String, Description);
            _db.AddInParameter(_dbCommand, "Scope", DbType.String, scope);
            _db.AddInParameter(_dbCommand, "Limitation", DbType.String, limitation);
            _db.AddInParameter(_dbCommand, "Definition", DbType.String, definition);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        #endregion
    }
}
