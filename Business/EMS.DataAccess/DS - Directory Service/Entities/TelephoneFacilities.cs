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
    internal class TelephoneFacilities
    {
        internal TelephoneFacilities()
        {
        }

        #region Write Functions Addresses

        internal void Create(Int64 idTelephone, Int64 idFacility, String reason)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_TelephoneFacilities_Create");
            _db.AddInParameter(_dbCommand, "IdTelephone", DbType.Int64, idTelephone);
            _db.AddInParameter(_dbCommand, "IdFacility", DbType.Int64, idFacility);
            _db.AddInParameter(_dbCommand, "Reason", DbType.String, reason);
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

        }
        internal void Delete(Int64 idTelephone, Int64 idFacility)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_TelephoneFacilities_Delete");
            _db.AddInParameter(_dbCommand, "IdTelephone", DbType.Int64, idTelephone);
            _db.AddInParameter(_dbCommand, "IdFacility", DbType.Int64, idFacility);
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void DeleteByTelephone(Int64 idTelephone)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_TelephoneFacilities_DeleteByTelephone");
            _db.AddInParameter(_dbCommand, "IdTelephone", DbType.Int64, idTelephone);
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void Update(Int64 idTelephone, Int64 idFacility, String reason)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_TelephoneFacilities_Update");
            _db.AddInParameter(_dbCommand, "IdTelephone", DbType.Int64, idTelephone);
            _db.AddInParameter(_dbCommand, "IdFacility", DbType.Int64, idFacility);
            _db.AddInParameter(_dbCommand, "Reason", DbType.String, reason);
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        #endregion

        #region Read Functions

        internal IEnumerable<DbDataRecord> ReadAll(Int64 idFacility)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_TelephoneFacilities_ReadAll");
            _db.AddInParameter(_dbCommand, "IdFacility", DbType.Int64, idFacility);
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
        internal IEnumerable<DbDataRecord> ReadById(Int64 idTelephone, Int64 idFacility)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_TelephoneFacilities_ReadById");
            _db.AddInParameter(_dbCommand, "IdTelephone", DbType.Int64, idTelephone);
            _db.AddInParameter(_dbCommand, "IdFacility", DbType.Int64, idFacility);
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
