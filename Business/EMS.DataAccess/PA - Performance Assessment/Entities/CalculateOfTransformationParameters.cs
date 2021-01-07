using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.PA.Entities
{
    internal class CalculateOfTransformationParameters
    {
        internal CalculateOfTransformationParameters() { }

        #region Read Functions
        internal IEnumerable<DbDataRecord> ReadAll(Int64 IdTransformation)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculateOfTransformationParameters_ReadAll");
            _db.AddInParameter(_dbCommand, "IdTransformation", DbType.Int64, IdTransformation);
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
        internal IEnumerable<DbDataRecord> ReadByID(String IdParameter, Int64 IdTransformation)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculateOfTransformationParameters_ReadByID");
            _db.AddInParameter(_dbCommand, "IdParameter", DbType.String, IdParameter);
            _db.AddInParameter(_dbCommand, "IdTransformation", DbType.Int64, IdTransformation);
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
