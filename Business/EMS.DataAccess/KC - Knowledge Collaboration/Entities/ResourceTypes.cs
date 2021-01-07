using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.KC.Entities
{
    internal class ResourceTypes
    {
        internal ResourceTypes() { }

        #region Read Functions

        //Trae todos los types
        internal IEnumerable<DbDataRecord> ReadAll(String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("KC_ResourceTypes_ReadAll");
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
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

        //Trae el type pedido
        internal IEnumerable<DbDataRecord> ReadRoot(String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("KC_ResourceTypes_ReadRoot");
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);

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

        internal IEnumerable<DbDataRecord> ReadByParent(Int64 idParentResourceType, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("KC_ResourceTypes_ReadByParent");
            _db.AddInParameter(_dbCommand, "idParentResourceType", DbType.Int64, idParentResourceType);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);

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

        internal IEnumerable<DbDataRecord> ReadById(Int64 idResourceType, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("KC_ResourceTypes_ReadById");
            _db.AddInParameter(_dbCommand, "idResourceType", DbType.Int64, idResourceType);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);

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

        internal Int64 Create(Int64 idParentResourceType, Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("KC_ResourceTypes_Create");
            _db.AddInParameter(_dbCommand, "idParentResourceType", DbType.Int64, Common.Common.CastValueToNull(idParentResourceType, DBNull.Value));
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);
            //Parámetro de salida
            _db.AddOutParameter(_dbCommand, "IdResourceType", DbType.Int64, 18);
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdResourceType"));
        }

        internal void Delete(Int64 IdResourceType, Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("KC_ResourceTypes_Delete");
            _db.AddInParameter(_dbCommand, "IdResourceType", DbType.Int64, IdResourceType);
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        internal void Update(Int64 idResourceType, Int64 idParentResourceType, Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("KC_ResourceTypes_Update");
            _db.AddInParameter(_dbCommand, "idResourceType", DbType.Int64, idResourceType);
            _db.AddInParameter(_dbCommand, "idParentResourceType", DbType.Int64, Common.Common.CastValueToNull(idParentResourceType, DBNull.Value));
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        #endregion

    }
}
