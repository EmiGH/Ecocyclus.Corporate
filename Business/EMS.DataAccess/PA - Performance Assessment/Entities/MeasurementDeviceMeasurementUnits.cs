using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.PA.Entities
{
    internal class MeasurementDeviceMeasurementUnits
    {
        internal MeasurementDeviceMeasurementUnits()
        { }

        #region Read Functions
        internal IEnumerable<DbDataRecord> ReadAll(Int64 idMeasurementDevice, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_MeasurementDeviceMeasurementUnits_ReadAll");
            _db.AddInParameter(_dbCommand, "IdMeasurementDevice", DbType.Int64, idMeasurementDevice);
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
        internal void Create(Int64 idMeasurementDevice, Int64 idMeasurementUnit, Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_MeasurementDeviceMeasurementUnits_Create");
            _db.AddInParameter(_dbCommand, "IdMeasurementDevice", DbType.Int64, idMeasurementDevice);
            _db.AddInParameter(_dbCommand, "IdMeasurementUnit", DbType.Int64, idMeasurementUnit);
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);
           
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }


        internal void Delete(Int64 idMeasurementDevice, Int64 idMeasurementUnit, Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_MeasurementDeviceMeasurementUnits_Delete");
            _db.AddInParameter(_dbCommand, "IdMeasurementDevice", DbType.Int64, idMeasurementDevice);
            _db.AddInParameter(_dbCommand, "IdMeasurementUnit", DbType.Int64, idMeasurementUnit);
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void Delete(Int64 idMeasurementDevice, Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_MeasurementDeviceMeasurementUnits_DeleteByDevice");
            _db.AddInParameter(_dbCommand, "IdMeasurementDevice", DbType.Int64, idMeasurementDevice);
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        #endregion
    }
}
