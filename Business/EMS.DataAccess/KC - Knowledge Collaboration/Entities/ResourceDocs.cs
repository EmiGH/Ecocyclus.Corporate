using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.KC.Entities
{
    internal class ResourceDocs
    {
        internal ResourceDocs() { }

        //#region Read Functions

        ////Trae todos los resourcesfiles para un resource
        //internal IEnumerable<DbDataRecord> ReadAll(Int64 idOrganization, Int64 idResource, String idLanguage)
        //{
        //    Database _db = DatabaseFactory.CreateDatabase();

        //    DbCommand _dbCommand = _db.GetStoredProcCommand("KC_ResourceFiles_ReadAll");
        //    _db.AddInParameter(_dbCommand, "idOrganization", DbType.Int64, idOrganization);
        //    _db.AddInParameter(_dbCommand, "idResource", DbType.Int64, idResource);
        //    _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
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

        ////trae el resource pedido
        //internal IEnumerable<DbDataRecord> ReadById(Int64 idResourceFile, Int64 idResource, Int64 idOrganization, String idLanguage)
        //{
        //    Database _db = DatabaseFactory.CreateDatabase();

        //    DbCommand _dbCommand = _db.GetStoredProcCommand("KC_ResourceFiles_ReadById");
        //    _db.AddInParameter(_dbCommand, "idResourceFile", DbType.Int64, idResourceFile); 
        //    _db.AddInParameter(_dbCommand, "IdResource", DbType.Int64, idResource);
        //    _db.AddInParameter(_dbCommand, "idOrganization", DbType.Int64, idOrganization);
        //    _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);

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

        //#endregion

        #region Write Functions

        internal void Create(Int64 idResourceFile, Int64 idResource, String docType, String docSize, Int64 idFile)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("KC_ResourceDocs_Create");
            _db.AddInParameter(_dbCommand, "idResourceFile", DbType.Int64, idResourceFile);
            _db.AddInParameter(_dbCommand, "idResource", DbType.Int64, idResource);
            _db.AddInParameter(_dbCommand, "docType", DbType.String, docType);
            _db.AddInParameter(_dbCommand, "docSize", DbType.String, docSize);
            _db.AddInParameter(_dbCommand, "idFile", DbType.Int64, idFile);

            _dbCommand.CommandTimeout = 0;
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

        }

        internal void Delete(Int64 idResourceFile, Int64 idResource)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("KC_ResourceDocs_Delete");
            _db.AddInParameter(_dbCommand, "idResource", DbType.Int64, idResource);
            _db.AddInParameter(_dbCommand, "idResourceFile", DbType.Int64, idResourceFile);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        internal void Delete(Int64 idResource)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("KC_ResourceDocs_DeleteByResource");
            _db.AddInParameter(_dbCommand, "idResource", DbType.Int64, idResource);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        internal void Update(Int64 idResourceFile, Int64 idResource, String docType, String docSize, Int64 idFile)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("KC_ResourceDocs_Update");
            _db.AddInParameter(_dbCommand, "idResourceFile", DbType.Int64, idResourceFile);
            _db.AddInParameter(_dbCommand, "idResource", DbType.Int64, idResource);
            _db.AddInParameter(_dbCommand, "docType", DbType.String, docType);
            _db.AddInParameter(_dbCommand, "docSize", DbType.String, docSize);
            _db.AddInParameter(_dbCommand, "idFile", DbType.Int64, idFile);

            _dbCommand.CommandTimeout = 0;
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        #endregion
    }
}
