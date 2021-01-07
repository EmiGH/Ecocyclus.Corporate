using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.DS
{
    internal class Users
    {
        internal Users()
        {
        }

        #region Write Functions
        internal void Create(Int64 idPerson, String userName, String password, Boolean active, Int64 idOrganization, Boolean changePasswordOnNextLogin, Boolean cannotChangePassword, Boolean passwordNeverExpires, Int64 idLogPerson, Boolean ViewGlobalMenu)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("DS_PeopleUsers_Create");
                _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, idPerson);
                _db.AddInParameter(_dbCommand, "Username", DbType.String, userName);
                _db.AddInParameter(_dbCommand, "Password", DbType.String, password);
                _db.AddInParameter(_dbCommand, "Active", DbType.Boolean, active);
                _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);
                _db.AddInParameter(_dbCommand, "ChangePasswordOnNextLogin", DbType.Boolean, changePasswordOnNextLogin);
                _db.AddInParameter(_dbCommand, "CannotChangePassword", DbType.Boolean, cannotChangePassword);
                _db.AddInParameter(_dbCommand, "PasswordNeverExpires", DbType.Boolean, passwordNeverExpires);
                _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);
                _db.AddInParameter(_dbCommand, "ViewGlobalMenu", DbType.Boolean, ViewGlobalMenu);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }
            internal void Delete(String userName, Int64 idLogPerson)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("DS_PeopleUsers_Delete");
                _db.AddInParameter(_dbCommand, "UserName", DbType.String, userName);
                _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }
            internal void Update(Int64 idPerson, String userName, Boolean active, Int64 idOrganization, Boolean changePasswordOnNextLogin, Boolean cannotChangePassword, Boolean passwordNeverExpires, Int64 idLogPerson, Boolean ViewGlobalMenu)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("DS_PeopleUsers_Update");
                _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, idPerson);
                _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);
                _db.AddInParameter(_dbCommand, "UserName", DbType.String, userName);
                _db.AddInParameter(_dbCommand, "Active", DbType.Boolean, active);
                _db.AddInParameter(_dbCommand, "ChangePasswordOnNextLogin", DbType.Boolean, changePasswordOnNextLogin);
                _db.AddInParameter(_dbCommand, "CannotChangePassword", DbType.Boolean, cannotChangePassword);
                _db.AddInParameter(_dbCommand, "PasswordNeverExpires", DbType.Boolean, passwordNeverExpires);
                _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);
                _db.AddInParameter(_dbCommand, "ViewGlobalMenu", DbType.Boolean, ViewGlobalMenu);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }
            internal void Update(String userName, String password, Int64 idLogPerson)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("DS_PeopleUsers_UpdatePassword");
                _db.AddInParameter(_dbCommand, "userName", DbType.String, userName);
                _db.AddInParameter(_dbCommand, "password", DbType.String, password);
                _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }
            internal void ResetPassword(String Username, String NewPassword, Int64 idLogPerson)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("DS_PeopleUsers_ResetPassword");
                _db.AddInParameter(_dbCommand, "userName", DbType.String, Username);
                _db.AddInParameter(_dbCommand, "password", DbType.String, NewPassword);
                _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }
        #endregion

        #region Read Functions
            internal IEnumerable<DbDataRecord> ReadAll(Int64 idOrganization)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("DS_PeopleUsers_ReadAll");
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
            internal IEnumerable<DbDataRecord> ReadById(String userName)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("DS_PeopleUsers_ReadById");
                _db.AddInParameter(_dbCommand, "userName", DbType.String, userName);

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

                DbCommand _dbCommand = _db.GetStoredProcCommand("DS_PeopleUsers_ReadByPerson");
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
            internal IEnumerable<DbDataRecord> GetPasswordHistory(String userName)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("DS_PeopleUsers_ReadPasswordHistory");
                _db.AddInParameter(_dbCommand, "Username", DbType.String, userName);

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
