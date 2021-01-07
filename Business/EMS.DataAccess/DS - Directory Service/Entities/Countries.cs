using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.DS
{
    internal class Countries
    {
        internal Countries()
        {
        }

        #region Write Functions
            
        
        internal Int64 Create(String idLanguage, String alpha, String name, String internationalCode, Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_Countries_Create");
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
            _db.AddInParameter(_dbCommand, "Alpha", DbType.String, alpha);
            _db.AddInParameter(_dbCommand, "name", DbType.String, name);
            _db.AddInParameter(_dbCommand, "InternationalCode", DbType.String, internationalCode);
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);
            //Parámetro de salida
            _db.AddOutParameter(_dbCommand, "IdCountry", DbType.Int64, 18);
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdCountry"));
        }
        internal void Delete(Int64 idCountry, Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_Countries_Delete");
            _db.AddInParameter(_dbCommand, "IdCountry", DbType.String, idCountry);
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void Update(Int64 idCountry, String idLanguage, String alpha, String name, String internationalCode, Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_Countries_Update");
            _db.AddInParameter(_dbCommand, "IdCountry", DbType.String, idCountry);
            _db.AddInParameter(_dbCommand, "Alpha", DbType.String, alpha);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
            _db.AddInParameter(_dbCommand, "name", DbType.String, name);
            _db.AddInParameter(_dbCommand, "InternationalCode", DbType.String, internationalCode);
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        #endregion

        #region Read Functions
        
        internal IEnumerable<DbDataRecord> ReadAll(String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_Countries_ReadAll");
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
        internal IEnumerable<DbDataRecord> ReadById(Int64 idCountry, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_Countries_ReadById");
            _db.AddInParameter(_dbCommand, "@IdCountry", DbType.Int64, idCountry);
            _db.AddInParameter(_dbCommand, "@IdLanguage", DbType.String, idLanguage);

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
