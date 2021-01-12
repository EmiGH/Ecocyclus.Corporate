using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.Log
{
    public class Log
    {
        public Log() { }

        #region Read Functions
        public Boolean GenericExist(String tableName, String rowFilter)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("GenericExist");
            _db.AddInParameter(_dbCommand, "TableName", DbType.String, tableName);
            _db.AddInParameter(_dbCommand, "RowFilter", DbType.String, rowFilter);
            SqlDataReader _Exist = (SqlDataReader)_db.ExecuteReader(_dbCommand);

            while (_Exist.Read())
            {
                return _Exist.GetBoolean(0);
            }
            return false;
            
        }
        //public IEnumerable<DbDataRecord> GenericExist(String tableName, String rowFilter)
        //{
        //    Database _db = DatabaseFactory.CreateDatabase();

        //    DbCommand _dbCommand = _db.GetStoredProcCommand("GenericExist");
        //    _db.AddInParameter(_dbCommand, "TableName", DbType.String, tableName);
        //    _db.AddInParameter(_dbCommand, "RowFilter", DbType.String, rowFilter);
        //    SqlDataReader _reader = (SqlDataReader)_db.ExecuteReader(_dbCommand);

        //    try
        //    {
        //        foreach (DbDataRecord _record in _reader)
        //        {
        //            yield return _record;
        //        }
        //    }
        //    finally
        //    {
        //        _reader.Close();
        //    }
        //}
        #endregion

        #region Write Functions
        public void Create(String TableName, String ObjectName, String OperationCode, String RowFilter, Int64 IdPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("Log_Create");
            _db.AddInParameter(_dbCommand, "TableName", DbType.String, TableName);
            _db.AddInParameter(_dbCommand, "ObjectName", DbType.String, ObjectName);
            _db.AddInParameter(_dbCommand, "OperationCode", DbType.String, OperationCode);
            _db.AddInParameter(_dbCommand, "RowFilter", DbType.String, RowFilter);
            _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, IdPerson);
            //Parámetro de salida
            _db.ExecuteNonQuery(_dbCommand);

        }
        #endregion
    }
}
