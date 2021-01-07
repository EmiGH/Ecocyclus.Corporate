using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.DS
{
    internal class TelephonePeople
    {
        internal TelephonePeople()
        {
        }

        #region Write Functions Addresses

        internal void Create(Int64 idTelephone, Int64 idPerson, Int64 IdOrganization, String reason)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_TelephonePeople_Create");
            _db.AddInParameter(_dbCommand, "IdTelephone", DbType.Int64, idTelephone);
            _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, idPerson);
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, IdOrganization);
            _db.AddInParameter(_dbCommand, "Reason", DbType.String, reason);
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

        }
        internal void Delete(Int64 idTelephone, Int64 idPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_TelephonePeople_Delete");
            _db.AddInParameter(_dbCommand, "IdTelephone", DbType.Int64, idTelephone);
            _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, idPerson);
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void DeleteByTelephone(Int64 idTelephone)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_TelephonePeople_DeleteByTelephone");
            _db.AddInParameter(_dbCommand, "IdTelephone", DbType.Int64, idTelephone);
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void Update(Int64 idTelephone, Int64 idPerson, Int64 IdOrganization, String reason)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_TelephonePeople_Update");
            _db.AddInParameter(_dbCommand, "IdTelephone", DbType.Int64, idTelephone);
            _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, idPerson);
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, IdOrganization);
            _db.AddInParameter(_dbCommand, "Reason", DbType.String, reason);
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        #endregion

        #region Read Functions

        internal IEnumerable<DbDataRecord> ReadAll(Int64 idPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_TelephonePeople_ReadAll");
            _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, idPerson);
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
        internal IEnumerable<DbDataRecord> ReadById(Int64 idTelephone, Int64 idPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_TelephonePeople_ReadById");
            _db.AddInParameter(_dbCommand, "IdTelephone", DbType.Int64, idTelephone);
            _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, idPerson);
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
