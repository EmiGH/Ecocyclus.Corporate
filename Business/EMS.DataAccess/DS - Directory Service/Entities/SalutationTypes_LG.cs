using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.DS
{
    internal class SalutationTypes_LG
    {
        internal SalutationTypes_LG()
        {            
        }

        #region Read Functions

        internal IEnumerable<DbDataRecord> ReadAll(Int64 idSalutationType)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_SalutationTypes_LG_ReadAll");
            _db.AddInParameter(_dbCommand, "IdSalutationTypes", DbType.Int64, idSalutationType);

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
        internal IEnumerable<DbDataRecord> ReadById(Int64 idSalutationType, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_SalutationTypes_LG_ReadById");
            _db.AddInParameter(_dbCommand, "IdSalutationTypes", DbType.Int64, idSalutationType);
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

        internal void Create(Int64 idSalutationTypes, String idLanguage, String name, String description, Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_SalutationTypes_LG_Create");
            _db.AddInParameter(_dbCommand, "IdSalutationType", DbType.Int64, idSalutationTypes);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
            _db.AddInParameter(_dbCommand, "Description", DbType.String, description);
            _db.AddInParameter(_dbCommand, "name", DbType.String, name);
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void Delete(Int64 idSalutationTypes, String idLanguage, Int64 idLogPerson)        
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_SalutationTypes_LG_Delete");
            _db.AddInParameter(_dbCommand, "IdSalutationType", DbType.Int64, idSalutationTypes);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void Update(Int64 idSalutationTypes, String idLanguage, String name, String description, Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_SalutationTypes_LG_Update");
            _db.AddInParameter(_dbCommand, "IdSalutationType", DbType.Int64, idSalutationTypes);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
            _db.AddInParameter(_dbCommand, "Description", DbType.String, description);
            _db.AddInParameter(_dbCommand, "name", DbType.String, name);
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        #endregion


    }
}
