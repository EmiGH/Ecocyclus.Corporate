using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.PA.Entities
{
    internal class ParameterGroups_LG
    {
        internal ParameterGroups_LG()
        { }

        #region Read Functions
        internal IEnumerable<DbDataRecord> ReadAll(Int64 idParameterGroup)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_ParameterGroups_LG_ReadAll");
            _db.AddInParameter(_dbCommand, "IdParameterGroup", DbType.Int64, idParameterGroup);
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
        internal IEnumerable<DbDataRecord> ReadById(Int64 idParameterGroup, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_ParameterGroups_LG_ReadById");
            _db.AddInParameter(_dbCommand, "IdParameterGroup", DbType.Int64, idParameterGroup);
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
        internal void Create(Int64 idIndicator, Int64 idParameterGroup, String idLanguage, String name, String Description, Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_ParameterGroups_LG_Create");
            _db.AddInParameter(_dbCommand, "IdIndicator", DbType.Int64, idIndicator);
            _db.AddInParameter(_dbCommand, "idParameterGroup", DbType.Int64, idParameterGroup);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
            _db.AddInParameter(_dbCommand, "Name", DbType.String, name);
            _db.AddInParameter(_dbCommand, "Description", DbType.String, Description);
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void Delete(Int64 idParameterGroup, String idLanguage, Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_ParameterGroups_LG_Delete");
            _db.AddInParameter(_dbCommand, "IdParameterGroup", DbType.String, idParameterGroup);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void Update(Int64 idIndicator, Int64 idParameterGroup, String idLanguage, String name, String Description, Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_ParameterGroups_LG_Update");
            _db.AddInParameter(_dbCommand, "IdIndicator", DbType.Int64, idIndicator);
            _db.AddInParameter(_dbCommand, "IdParameterGroup", DbType.Int64, idParameterGroup);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
            _db.AddInParameter(_dbCommand, "Name", DbType.String, name);
            _db.AddInParameter(_dbCommand, "Description", DbType.String, Description);
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        #endregion
    }
}
