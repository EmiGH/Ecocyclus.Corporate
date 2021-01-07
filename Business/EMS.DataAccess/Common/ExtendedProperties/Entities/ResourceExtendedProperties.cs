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
    internal class ResourceExtendedProperties
    {
        internal ResourceExtendedProperties()
        {
        }

        #region Read Function
        internal IEnumerable<DbDataRecord> ReadAll(Int64 idResource)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("EP_ResourceExtendedProperties_ReadAll");
            _db.AddInParameter(_dbCommand, "IdResource", DbType.Int64, idResource);
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
        internal IEnumerable<DbDataRecord> ReadById(Int64 idResource, Int64 idExtendedProperty)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("EP_ResourceExtendedProperties_ReadById");
            _db.AddInParameter(_dbCommand, "IdResource", DbType.Int64, idResource);
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
        internal void Create(Int64 idExtendedProperty, Int64 idResource, String value)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("EP_ResourceExtendedProperties_Create");
            _db.AddInParameter(_dbCommand, "IdExtendedProperty", DbType.Int64, idExtendedProperty);
            _db.AddInParameter(_dbCommand, "IdResource", DbType.Int64, idResource);
            _db.AddInParameter(_dbCommand, "Value", DbType.String, value);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void Delete(Int64 idExtendedProperty, Int64 idResource)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("EP_ResourceExtendedProperties_Delete");
            _db.AddInParameter(_dbCommand, "IdExtendedProperty", DbType.String, idExtendedProperty);
            _db.AddInParameter(_dbCommand, "IdResource", DbType.Int64, idResource);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void Delete(Int64 idResource)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("EP_ResourceExtendedProperties_DeleteByResource");
            _db.AddInParameter(_dbCommand, "IdResource", DbType.Int64, idResource);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void Update(Int64 idExtendedProperty, Int64 idResource, String value)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("EP_ResourceExtendedProperties_Update");
            _db.AddInParameter(_dbCommand, "IdResource", DbType.Int64, idResource);
            _db.AddInParameter(_dbCommand, "IdExtendedProperty", DbType.Int64, idExtendedProperty);
            _db.AddInParameter(_dbCommand, "Value", DbType.String, value);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        #endregion

    }
}
