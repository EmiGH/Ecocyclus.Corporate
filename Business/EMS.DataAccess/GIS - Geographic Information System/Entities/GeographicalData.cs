using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.GIS.Entities
{
    public class GeographicalData
    {
        public GeographicalData() { }

        #region Read Functions
        public  IEnumerable<DbDataRecord> ReadByID(String className, Int64 idObject)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("GIS_GeographicalData_ReadById");
            _db.AddInParameter(_dbCommand, "className", DbType.String, className);
            _db.AddInParameter(_dbCommand, "idObject", DbType.Int64, idObject);

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
        public IEnumerable<DbDataRecord> ReadAll()
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("GIS_GeographicalData_ReadAll");

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
        public IEnumerable<DbDataRecord> ReadByClass(String className)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("GIS_GeographicalData_ReadByClass");
            _db.AddInParameter(_dbCommand, "className", DbType.String, className);

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
