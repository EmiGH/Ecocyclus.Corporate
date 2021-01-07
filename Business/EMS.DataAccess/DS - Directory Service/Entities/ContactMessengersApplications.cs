using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.DS
{
    internal class ContactMessengersApplications
    {
        internal ContactMessengersApplications()
        {
        }

        #region Read Functions

        internal IEnumerable<DbDataRecord> ReadAll(String Provider)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_ContactMessengersApplications_ReadAllByProvider");
            _db.AddInParameter(_dbCommand, "Provider", DbType.String, Provider);

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
        internal IEnumerable<DbDataRecord> ReadById(String Provider, String Application)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("[DS_ContactMessengersApplications_ReadById]");
            _db.AddInParameter(_dbCommand, "Provider", DbType.String, Provider);
            _db.AddInParameter(_dbCommand, "Application", DbType.String, Application);
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

        internal void Create(String Provider, String Application, Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_ContactMessengersApplications_Create");
            _db.AddInParameter(_dbCommand, "Provider", DbType.String, Provider);
            _db.AddInParameter(_dbCommand, "Application", DbType.String, Application);
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        internal void Delete(String Provider, String Application, Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_ContactMessengersApplications_Delete");
            _db.AddInParameter(_dbCommand, "Provider", DbType.String, Provider);
            _db.AddInParameter(_dbCommand, "Application", DbType.String, Application);
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        #endregion
    }
}
