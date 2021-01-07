using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.Entities
{
    public class CalculateExtendedProperties
    {    
        internal CalculateExtendedProperties()
        {
        }

        #region Read Function
            internal IEnumerable<DbDataRecord> ReadAll(Int64 idCalculate)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("EP_CalculateExtendedProperties_ReadAll");
                _db.AddInParameter(_dbCommand, "IdCalculate", DbType.Int64, idCalculate);
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
            internal IEnumerable<DbDataRecord> ReadById(Int64 idCalculate, Int64 idExtendedProperty)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("EP_CalculateExtendedProperties_ReadById");
                _db.AddInParameter(_dbCommand, "IdCalculate", DbType.Int64, idCalculate);
                _db.AddInParameter(_dbCommand, "IdExtendedProperty", DbType.Int64, idExtendedProperty);
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
            internal void Create(Int64 idExtendedProperty, Int64 idCalculate, String value)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("EP_CalculateExtendedProperties_Create");
                _db.AddInParameter(_dbCommand, "IdExtendedProperty", DbType.Int64, idExtendedProperty);
                _db.AddInParameter(_dbCommand, "IdCalculate", DbType.Int64, idCalculate);
                _db.AddInParameter(_dbCommand, "Value", DbType.String, value);
               
                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }
            internal void Delete(Int64 idExtendedProperty, Int64 idCalculate)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("EP_CalculateExtendedProperties_Delete");
                _db.AddInParameter(_dbCommand, "IdExtendedProperty", DbType.String, idExtendedProperty);
                _db.AddInParameter(_dbCommand, "IdCalculate", DbType.Int64, idCalculate);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }
            internal void Delete(Int64 idCalculate)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("EP_CalculateExtendedProperties_DeleteByCalculate");
                _db.AddInParameter(_dbCommand, "IdCalculate", DbType.Int64, idCalculate);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }
            internal void Update(Int64 idExtendedProperty, Int64 idCalculate, String value)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("EP_CalculateExtendedProperties_Update");
                _db.AddInParameter(_dbCommand, "IdCalculate", DbType.Int64, idCalculate);
                _db.AddInParameter(_dbCommand, "IdExtendedProperty", DbType.Int64, idExtendedProperty);
                _db.AddInParameter(_dbCommand, "Value", DbType.String, value);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }
        #endregion

    }
}
