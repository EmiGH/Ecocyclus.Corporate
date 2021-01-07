using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.KC.Entities
{
    internal class ResourceVersions
    {
        internal ResourceVersions() { }

        #region Read Functions 

        //Trae todos los resourcesfiles para un resource
        internal IEnumerable<DbDataRecord> ReadAll(Int64 idResource)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("KC_ResourceVersions_ReadAll");
            _db.AddInParameter(_dbCommand, "idResource", DbType.Int64, idResource);
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

        //trae el resource pedido
        internal IEnumerable<DbDataRecord> ReadById(Int64 idResourceFile, Int64 idResource)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("KC_ResourceVersions_ReadById");
            _db.AddInParameter(_dbCommand, "idResourceFile", DbType.Int64, idResourceFile); 
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

        internal IEnumerable<DbDataRecord> ReadById(Int64 idResource)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("KC_ResourceVersions_ReadByCurrentFile");
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

        internal IEnumerable<DbDataRecord> ReadByException(Int64 idException)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("KC_ResourceVersions_ReadByException");
            _db.AddInParameter(_dbCommand, "IdException", DbType.Int64, idException);

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

        internal Int64 Create(Int64 idResource, DateTime timeStamp, DateTime validFrom, DateTime validThrough, String version, Int64 idPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("KC_ResourceVersions_Create");
            _db.AddInParameter(_dbCommand, "idResource", DbType.Int64, idResource);
            _db.AddInParameter(_dbCommand, "version", DbType.String, version);
            _db.AddInParameter(_dbCommand, "timeStamp", DbType.DateTime, timeStamp);
            _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, idPerson);
            _db.AddInParameter(_dbCommand, "validFrom", DbType.DateTime, validFrom);
            _db.AddInParameter(_dbCommand, "validThrough", DbType.DateTime, validThrough);            
            //Parámetro de salida
            _db.AddOutParameter(_dbCommand, "IdResourceFile", DbType.Int64, 18);
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdResourceFile"));
        }

        internal void Delete(Int64 idResourceFile, Int64 idResource)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("KC_ResourceVersions_Delete");
            _db.AddInParameter(_dbCommand, "idResource", DbType.Int64, idResource);
            _db.AddInParameter(_dbCommand, "idResourceFile", DbType.Int64, idResourceFile);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        internal void Delete(Int64 idResource)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("KC_ResourceVersions_DeleteByResource");
            _db.AddInParameter(_dbCommand, "idResource", DbType.Int64, idResource);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        internal void Update(Int64 idResourceFile, Int64 idResource, DateTime timeStamp, DateTime validFrom, DateTime validThrough, String version, Int64 idPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("KC_ResourceVersions_Update");
            _db.AddInParameter(_dbCommand, "idResourceFile", DbType.Int64, idResourceFile);
            _db.AddInParameter(_dbCommand, "idResource", DbType.Int64, idResource);
            _db.AddInParameter(_dbCommand, "version", DbType.String, version);
            _db.AddInParameter(_dbCommand, "timeStamp", DbType.DateTime, timeStamp);
            _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, idPerson);
            _db.AddInParameter(_dbCommand, "validFrom", DbType.DateTime, validFrom);
            _db.AddInParameter(_dbCommand, "validThrough", DbType.DateTime, validThrough);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        #endregion
    }
}
