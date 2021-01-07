using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.KC.Entities
{
    internal class Catalogues
    {
        internal Catalogues() { }

        #region Read Functions 

        //Trae todos los resourcesfiles para un resource
        internal IEnumerable<DbDataRecord> ReadAll(Int64 idResource)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("KC_ResourceCatalogues_ReadAll");
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

            DbCommand _dbCommand = _db.GetStoredProcCommand("KC_ResourceCatalogues_ReadById");
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

            DbCommand _dbCommand = _db.GetStoredProcCommand("KC_ResourceFiles_ReadByCurrentFile");
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
        #endregion

        #region Write Functions

        internal Int64 Create(Int64 idResource, DateTime timeStamp, Int64 idPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("KC_ResourceCatalogues_Create");
            _db.AddInParameter(_dbCommand, "idResource", DbType.Int64, idResource);
            _db.AddInParameter(_dbCommand, "timeStamp", DbType.DateTime, timeStamp);
            _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, idPerson);
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

            DbCommand _dbCommand = _db.GetStoredProcCommand("KC_ResourceCatalogues_Delete");
            _db.AddInParameter(_dbCommand, "idResource", DbType.Int64, idResource);
            _db.AddInParameter(_dbCommand, "idResourceFile", DbType.Int64, idResourceFile);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        internal void Delete(Int64 idResource)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("KC_ResourceCatalogues_DeleteByResource");
            _db.AddInParameter(_dbCommand, "idResource", DbType.Int64, idResource);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        internal void Update(Int64 idResourceFile, Int64 idResource, DateTime timeStamp, Int64 idPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("KC_ResourceCatalogues_Update");
            _db.AddInParameter(_dbCommand, "idResourceFile", DbType.Int64, idResourceFile);
            _db.AddInParameter(_dbCommand, "idResource", DbType.Int64, idResource);
            _db.AddInParameter(_dbCommand, "timeStamp", DbType.DateTime, timeStamp);
            _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, idPerson);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        #endregion
    }
}
