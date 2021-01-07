using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.PA.Entities
{
    internal class Qualities_LG
    {
        internal Qualities_LG()
        { }

        #region Read Functions
        internal IEnumerable<DbDataRecord> ReadById(Int64 idQuality, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Qualities_LG_ReadByID");
            _db.AddInParameter(_dbCommand, "IdQuality", DbType.Int64, idQuality);
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
        internal IEnumerable<DbDataRecord> ReadAll(Int64 idQuality)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Qualities_LG_ReadAll");
            _db.AddInParameter(_dbCommand, "IdQuality", DbType.Int64, idQuality);
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

        internal void Create(Int64 idQuality, String idLanguage, String name, String description)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Qualities_LG_Create");
            _db.AddInParameter(_dbCommand, "IdQuality", DbType.Int64, idQuality);
            _db.AddInParameter(_dbCommand, "idLanguage", DbType.String, idLanguage);
            _db.AddInParameter(_dbCommand, "name", DbType.String, name);
            _db.AddInParameter(_dbCommand, "description", DbType.String, description);
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        internal void Delete(Int64 idQuality, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Qualities_LG_Delete");
            _db.AddInParameter(_dbCommand, "IdQuality", DbType.Int64, idQuality);
            _db.AddInParameter(_dbCommand, "idLanguage", DbType.String, idLanguage);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        internal void Delete(Int64 idQuality)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Qualities_LG_DeleteByQuality");
            _db.AddInParameter(_dbCommand, "IdQuality", DbType.Int64, idQuality);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        internal void Update(Int64 idQuality, String idLanguage, String name, String description)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Qualities_LG_Update");
            _db.AddInParameter(_dbCommand, "IdQuality", DbType.Int64, idQuality);
            _db.AddInParameter(_dbCommand, "idLanguage", DbType.String, idLanguage);
            _db.AddInParameter(_dbCommand, "name", DbType.String, name);
            _db.AddInParameter(_dbCommand, "description", DbType.String, description);
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        #endregion
    }
}
