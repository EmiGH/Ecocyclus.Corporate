using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.PF.Entities
{
    internal class ProcessResources
    {
        internal ProcessResources()
        {
        }

        #region Read Function
        internal IEnumerable<DbDataRecord> ReadAll(Int64 idProcess)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessResources_ReadAll");
            _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);
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
        internal IEnumerable<DbDataRecord> ReadById(Int64 idProcess, Int64 idResource)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessResources_ReadById");
            _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);
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
        internal void Create(Int64 idResource, Int64 idProcess, String comment, Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessResources_Create");
            _db.AddInParameter(_dbCommand, "IdResource", DbType.Int64, idResource);
            _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);
            _db.AddInParameter(_dbCommand, "Comment", DbType.String, comment);
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void Delete(Int64 idResource, Int64 idProcess, Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessResources_Delete");
            _db.AddInParameter(_dbCommand, "IdResource", DbType.String, idResource);
            _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void Delete(Int64 idProcess)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessResources_DeleteByProcess");
            _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void Update(Int64 idResource, Int64 idProcess, String comment, Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessResources_Update");
            _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);
            _db.AddInParameter(_dbCommand, "IdResource", DbType.Int64, idResource);
            _db.AddInParameter(_dbCommand, "Comment", DbType.String, comment);
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        #endregion

    }
}
