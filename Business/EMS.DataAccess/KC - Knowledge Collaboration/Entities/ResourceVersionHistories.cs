using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.KC.Entities
{
    internal class ResourceVersionHistories
    {
        internal ResourceVersionHistories() { }

        #region Read Functions

        //Trae todos los resourcesfiles para un resource
        internal IEnumerable<DbDataRecord> ReadAll(Int64 idResource, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("KC_ResourceVersionHistories_ReadAll");
            _db.AddInParameter(_dbCommand, "idResource", DbType.Int64, idResource);
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

        //trae el resource pedido
        internal IEnumerable<DbDataRecord> ReadById(Int64 idResourceFile, Int64 idResource, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("KC_ResourceVersionHistories_ReadById");
            _db.AddInParameter(_dbCommand, "idResourceFile", DbType.Int64, idResourceFile);
            _db.AddInParameter(_dbCommand, "IdResource", DbType.Int64, idResource);
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

        internal void Create(Int64 idResourceFile, Int64 idResourceState, Int64 idResource, DateTime date, Int64 idGeographicArea, Int64 idFunctionalArea, Int64 idOrganizationPost, Int64 idPerson, Int64 idPosition)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("KC_ResourceVersionHistories_Create");
            _db.AddInParameter(_dbCommand, "idResourceFile", DbType.Int64, idResourceFile);
            _db.AddInParameter(_dbCommand, "IdResourceFileState", DbType.Int64, idResourceState);
            _db.AddInParameter(_dbCommand, "idResource", DbType.Int64, idResource);
            _db.AddInParameter(_dbCommand, "date", DbType.DateTime, date);
            _db.AddInParameter(_dbCommand, "idGeographicArea", DbType.Int64, idGeographicArea);
            _db.AddInParameter(_dbCommand, "idFunctionalArea", DbType.Int64, idFunctionalArea);
            _db.AddInParameter(_dbCommand, "idPerson", DbType.Int64, idPerson);
            _db.AddInParameter(_dbCommand, "idOrganizationPost", DbType.Int64, idOrganizationPost);
            _db.AddInParameter(_dbCommand, "idPosition", DbType.Int64, idPosition);
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void Delete(Int64 idGeographicArea, Int64 idFunctionalArea, Int64 idOrganizationPost, Int64 idPerson, Int64 idPosition)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("KC_ResourceVersionHistories_DeleteByPost");
            _db.AddInParameter(_dbCommand, "idGeographicArea", DbType.Int64, idGeographicArea);
            _db.AddInParameter(_dbCommand, "idFunctionalArea", DbType.Int64, idFunctionalArea);
            _db.AddInParameter(_dbCommand, "idPerson", DbType.Int64, idPerson);
            _db.AddInParameter(_dbCommand, "idOrganizationPost", DbType.Int64, idOrganizationPost);
            _db.AddInParameter(_dbCommand, "idPosition", DbType.Int64, idPosition);
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void Delete(Int64 idResourceFile, Int64 idResource, Int64 idResourceState)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("KC_ResourceVersionHistories_Delete");
            _db.AddInParameter(_dbCommand, "idResource", DbType.Int64, idResource);
            _db.AddInParameter(_dbCommand, "idResourceFile", DbType.Int64, idResourceFile);
            _db.AddInParameter(_dbCommand, "idResourceState", DbType.Int64, idResourceState);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        
        internal void Delete(Int64 idResourceFile, Int64 idResource)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("KC_ResourceVersionHistories_DeleteByResourceFile");
            _db.AddInParameter(_dbCommand, "idResource", DbType.Int64, idResource);
            _db.AddInParameter(_dbCommand, "idResourceFile", DbType.Int64, idResourceFile);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        internal void Delete(Int64 idResource)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("KC_ResourceVersionHistories_DeleteByResource");
            _db.AddInParameter(_dbCommand, "idResource", DbType.Int64, idResource);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        internal void Update(Int64 idResourceFile, Int64 idResourceState, Int64 idResource, DateTime date, Int64 idGeographicArea, Int64 idFunctionalArea, Int64 idOrganizationPost, Int64 idPerson, Int64 idPosition)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("KC_ResourceVersionHistories_Update");
            _db.AddInParameter(_dbCommand, "idResourceFile", DbType.Int64, idResourceFile);
            _db.AddInParameter(_dbCommand, "IdResourceFileState", DbType.Int64, idResourceState);
            _db.AddInParameter(_dbCommand, "idResource", DbType.Int64, idResource);
            _db.AddInParameter(_dbCommand, "date", DbType.DateTime, date);
            _db.AddInParameter(_dbCommand, "idGeographicArea", DbType.Int64, idGeographicArea);
            _db.AddInParameter(_dbCommand, "idFunctionalArea", DbType.Int64, idFunctionalArea);
            _db.AddInParameter(_dbCommand, "idPerson", DbType.Int64, idPerson);
            _db.AddInParameter(_dbCommand, "idOrganizationPost", DbType.Int64, idOrganizationPost);
            _db.AddInParameter(_dbCommand, "idPosition", DbType.Int64, idPosition);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        #endregion
    }
}
