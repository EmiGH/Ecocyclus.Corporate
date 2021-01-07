using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.PF.Entities
{
    internal class ProcessTaskMeasurements
    {
        internal ProcessTaskMeasurements() { }

        internal IEnumerable<DbDataRecord> ReadByFacility(Int64 IdProcess, Int64 IdFacility, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();
                                                             
            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskMeasurements_ReadByFacility");
            _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, IdProcess);
            _db.AddInParameter(_dbCommand, "IdFacility", DbType.Int64, IdFacility);
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
        internal IEnumerable<DbDataRecord> ReadByProcessWhitOutFacility(Int64 IdProcess, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskMeasurements_ReadByProcessWhitOutFacility");
            _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, IdProcess);
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

        #region Write Functions
        internal void Create(Int64 idProcess, Int64 idMeasurement, Int64 idScope, Int64 idActivity, String Reference)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskMeasurements_Create");
            _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);
            _db.AddInParameter(_dbCommand, "IdMeasurement", DbType.Int64, idMeasurement);
            _db.AddInParameter(_dbCommand, "IdScope", DbType.Int64, Common.Common.CastValueToNull(idScope, 0));
            _db.AddInParameter(_dbCommand, "IdActivity", DbType.Int64, Common.Common.CastValueToNull(idActivity, 0));
            _db.AddInParameter(_dbCommand, "Reference", DbType.String, Reference);
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void Delete(Int64 idProcess)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskMeasurements_Delete");
            _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void Update(Int64 idProcess, Int64 idMeasurement, Int64 idScope, Int64 idActivity, String Reference)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskMeasurements_Update");
            _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);
            _db.AddInParameter(_dbCommand, "IdMeasurement", DbType.Int64, idMeasurement);
            _db.AddInParameter(_dbCommand, "IdScope", DbType.Int64, Common.Common.CastValueToNull(idScope, 0));
            _db.AddInParameter(_dbCommand, "IdActivity", DbType.Int64, Common.Common.CastValueToNull(idActivity, 0));
            _db.AddInParameter(_dbCommand, "Reference", DbType.String, Reference);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void Update(Int64 idProcess, Boolean Status)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskMeasurements_UpdateStatus");
            _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);
            _db.AddInParameter(_dbCommand, "Status", DbType.Boolean, Status);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        #endregion
    }
}