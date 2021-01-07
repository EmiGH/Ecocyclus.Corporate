using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.NT.Entities
{
    internal class NotificationConfigurations
    {
        internal NotificationConfigurations() { }

        #region Read Functions

        internal IEnumerable<DbDataRecord> Read()
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("NT_NotificationConfigurations_Read");
           
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

        internal void Create(String emailSender, String host, String userName, String password, Double interval)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("NT_NotificationConfigurations_Create");
            _db.AddInParameter(_dbCommand, "EmailSender", DbType.String, emailSender);
            _db.AddInParameter(_dbCommand, "Host", DbType.String, host);
            _db.AddInParameter(_dbCommand, "UserName", DbType.String, userName);
            _db.AddInParameter(_dbCommand, "Password", DbType.String, password);
            _db.AddInParameter(_dbCommand, "Interval", DbType.Double, interval);
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

        }
        internal void Update(String emailSender, String host, String userName, String password, Double interval)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("NT_NotificationConfigurations_Update");
            _db.AddInParameter(_dbCommand, "EmailSender", DbType.String, emailSender);
            _db.AddInParameter(_dbCommand, "Host", DbType.String, host);
            _db.AddInParameter(_dbCommand, "UserName", DbType.String, userName);
            _db.AddInParameter(_dbCommand, "Password", DbType.String, password);
            _db.AddInParameter(_dbCommand, "Interval", DbType.Double, interval);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        #endregion
    }
}
