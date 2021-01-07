using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.DS
{
    internal class ContactEmails
    {
        internal ContactEmails()
        {
        }

        #region Organizations

        #region Write Functions

        internal Int64 AddByOrganization(String email, Int64 idOrganization, Int64 idContactType, Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_ContactEmails_CreateByOrganization");
            _db.AddInParameter(_dbCommand, "Email", DbType.String, email);
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);
            _db.AddInParameter(_dbCommand, "IdContactType", DbType.Int64, idContactType);
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

            //Parámetro de salida
            _db.AddOutParameter(_dbCommand, "IdContactEmail", DbType.Int64, 18);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdContactEmail"));
        }
        internal void Update(Int64 idContactEmail, String email, Int64 idContactType, Int64 idOrganization, Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_ContactEmails_UpdateByOrganization");
            _db.AddInParameter(_dbCommand, "IdContactEmail", DbType.Int64, idContactEmail);
            _db.AddInParameter(_dbCommand, "Email", DbType.String, email);
            _db.AddInParameter(_dbCommand, "IdContactType", DbType.Int64, idContactType);
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void RemoveByOrganization(Int64 idContactEmail,Int64 idOrganization,Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_ContactEmails_DeleteByOrganization");
            _db.AddInParameter(_dbCommand, "IdContactEmail", DbType.Int64, idContactEmail);
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        
        #endregion

        #region Read Functions

        internal IEnumerable<DbDataRecord> ReadById(Int64 idContactEmail, Int64 idOrganization)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("[DS_ContactEmails_ReadByOrganization]");
            _db.AddInParameter(_dbCommand, "IdContactEmail", DbType.Int64, idContactEmail);
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

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_ContactEmails_ReadAllByOrganization");
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

        internal Int64 AddByPerson(String email, Int64 idPerson, Int64 idContactType, Int64 idOrganization, Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_ContactEmails_CreateByPerson");
            _db.AddInParameter(_dbCommand, "Email", DbType.String, email);
            _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, idPerson);
            _db.AddInParameter(_dbCommand, "IdContactType", DbType.Int64, idContactType);
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);

            //Parámetro de salida
            _db.AddOutParameter(_dbCommand, "IdContactEmail", DbType.Int64, 18);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdContactEmail"));
        }
        internal void Update(Int64 idPerson, Int64 idContactEmail, String email, Int64 idContactType, Int64 idOrganization, Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_ContactEmails_UpdateByPerson");
            _db.AddInParameter(_dbCommand, "IdContactEmail", DbType.Int64, idContactEmail);
            _db.AddInParameter(_dbCommand, "Email", DbType.String, email);
            _db.AddInParameter(_dbCommand, "IdContactType", DbType.Int64, idContactType);
            _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, idPerson);
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void RemoveByPerson(Int64 idContactEmail, Int64 idPerson, Int64 idOrganization, Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_ContactEmails_DeleteByPerson");
            _db.AddInParameter(_dbCommand, "IdContactEmail", DbType.Int64, idContactEmail);
            _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, idPerson);
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        
        #endregion

        #region Read Functions

        internal IEnumerable<DbDataRecord> ReadByIdPerson(Int64 idContactEmail, Int64 idOrganization, Int64 idPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("[DS_ContactEmails_ReadByPerson]");
            _db.AddInParameter(_dbCommand, "IdContactEmail", DbType.Int64, idContactEmail);
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

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_ContactEmails_ReadAllByPerson");
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
