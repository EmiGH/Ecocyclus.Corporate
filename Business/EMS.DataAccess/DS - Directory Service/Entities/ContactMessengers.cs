using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.DS
{
    internal class ContactMessengers
    {
        internal ContactMessengers()
        {
        }

        #region Organizations

        #region Write Functions

        internal Int64 AddByOrganization(String provider, String application, String data, Int64 idOrganization, Int64 idContactType, Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_ContactMessengers_CreateByOrganization");
            _db.AddInParameter(_dbCommand, "Provider", DbType.String, provider);
            _db.AddInParameter(_dbCommand, "Application", DbType.String, application);
            _db.AddInParameter(_dbCommand, "Data", DbType.String, data);
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);
            _db.AddInParameter(_dbCommand, "IdContactType", DbType.Int64, idContactType);
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

            //Parámetro de salida
            _db.AddOutParameter(_dbCommand, "IdContactMessenger", DbType.Int64, 18);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdContactMessenger"));
        }

       internal void Update(Int64 idContactMessenger, String provider, String application, String data, Int64 idContactType, Int64 idOrganization, Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_ContactMessengers_UpdateByOrganization");
            _db.AddInParameter(_dbCommand, "IdContactMessenger", DbType.Int64, idContactMessenger);
            _db.AddInParameter(_dbCommand, "Provider", DbType.String, provider);
            _db.AddInParameter(_dbCommand, "Application", DbType.String, application);
            _db.AddInParameter(_dbCommand, "Data", DbType.String, data);
            _db.AddInParameter(_dbCommand, "IdContactType", DbType.Int64, idContactType);
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        internal void RemoveByOrganization(Int64 idContactMessenger, Int64 idOrganization, Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_ContactMessengers_DeleteByOrganization");
            _db.AddInParameter(_dbCommand, "IdContactMessenger", DbType.Int64, idContactMessenger);
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        #endregion

        #region Read Functions

        internal IEnumerable<DbDataRecord> ReadById(Int64 idContactMessenger, Int64 idOrganization)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("[DS_ContactMessengers_ReadByOrganization]");
            _db.AddInParameter(_dbCommand, "IdContactMessenger", DbType.Int64, idContactMessenger);
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);
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

        internal IEnumerable<DbDataRecord> GetByOrganization(Int64 idOrganization)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_ContactMessengers_ReadAllByOrganization");
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);

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

        #endregion

        #region People

        #region Write Functions

        internal Int64 AddByPerson(String provider, String application, String data, Int64 idPerson, Int64 idContactType, Int64 idOrganization, Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_ContactMessengers_CreateByPerson");
            _db.AddInParameter(_dbCommand, "Provider", DbType.String, provider);
            _db.AddInParameter(_dbCommand, "Application", DbType.String, application);
            _db.AddInParameter(_dbCommand, "Data", DbType.String, data);
            _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, idPerson);
            _db.AddInParameter(_dbCommand, "IdContactType", DbType.Int64, idContactType);
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);

            //Parámetro de salida
            _db.AddOutParameter(_dbCommand, "IdContactMessenger", DbType.Int64, 18);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdContactMessenger"));
        }
        internal void Update(Int64 idPerson, Int64 idContactMessenger, String provider, String application, String data, Int64 idContactType, Int64 idOrganization, Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_ContactMessengers_UpdateByPerson");
            _db.AddInParameter(_dbCommand, "IdContactMessenger", DbType.Int64, idContactMessenger);
            _db.AddInParameter(_dbCommand, "Provider", DbType.String, provider);
            _db.AddInParameter(_dbCommand, "Application", DbType.String, application);
            _db.AddInParameter(_dbCommand, "Data", DbType.String, data);
            _db.AddInParameter(_dbCommand, "IdContactType", DbType.Int64, idContactType);
            _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, idPerson);
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void RemoveByPerson(Int64 idContactMessenger, Int64 idPerson, Int64 idOrganization, Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_ContactMessengers_DeleteByPerson");
            _db.AddInParameter(_dbCommand, "IdContactMessenger", DbType.Int64, idContactMessenger);
            _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, idPerson);
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        #endregion

        #region Read Functions

        internal IEnumerable<DbDataRecord> ReadByIdPerson(Int64 idContactMessenger, Int64 idOrganization, Int64 idPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("[DS_ContactMessengers_ReadByPerson]");
            _db.AddInParameter(_dbCommand, "IdContactMessenger", DbType.Int64, idContactMessenger);
            _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, idPerson);
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);

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
        internal IEnumerable<DbDataRecord> GetByPerson(Int64 idPerson, Int64 idOrganization)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_ContactMessengers_ReadAllByPerson");
            _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, idPerson);
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);

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
        #endregion
    }
}
