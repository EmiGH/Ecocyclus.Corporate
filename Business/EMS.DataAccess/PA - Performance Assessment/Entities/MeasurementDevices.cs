using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.PA.Entities
{
    internal class MeasurementDevices
    {
        internal MeasurementDevices()
        { }

        #region Write Functions

        internal Int64 Add(Int64 idDeviceType, String reference, String serialNumber, String brand, String model, String calibrationPeriodicity, String maintenance, DateTime? installationDate, Int64 idResourcePicture, Int64 idFacility, Double upperLimit, Double lowerLimit, Double uncertainty, Int64 idLogPerson)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_MeasurementDevices_Create");
                _db.AddInParameter(_dbCommand, "IdMeasurementDeviceType", DbType.Int64, idDeviceType);
                _db.AddInParameter(_dbCommand, "Reference", DbType.String, reference);
                _db.AddInParameter(_dbCommand, "SerialNumber", DbType.String, serialNumber);
                _db.AddInParameter(_dbCommand, "Brand", DbType.String, brand);
                _db.AddInParameter(_dbCommand, "Model", DbType.String, model);
                _db.AddInParameter(_dbCommand, "CalibrationPeriodicity", DbType.String, calibrationPeriodicity);
                _db.AddInParameter(_dbCommand, "Maintenance", DbType.String, maintenance);
                _db.AddInParameter(_dbCommand, "InstallationDate", DbType.DateTime, installationDate);
                _db.AddInParameter(_dbCommand, "IdResourcePicture", DbType.Int64, Common.Common.CastValueToNull(idResourcePicture, 0));
                _db.AddInParameter(_dbCommand, "IdFacility", DbType.Int64, Common.Common.CastValueToNull(idFacility, 0));
                _db.AddInParameter(_dbCommand, "UpperLimit", DbType.Double, Common.Common.CastValueToNull(upperLimit, 0));
                _db.AddInParameter(_dbCommand, "LowerLimit", DbType.Double, Common.Common.CastValueToNull(lowerLimit, 0));
                _db.AddInParameter(_dbCommand, "Uncertainty", DbType.Double, Common.Common.CastValueToNull(uncertainty, 0));
                _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

                //Parámetro de salida
                _db.AddOutParameter(_dbCommand, "IdMeasurementDevice", DbType.Int64, 18);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);

                //Retorna el identificador
                return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdMeasurementDevice"));
            }
        internal void Remove(Int64 idDevice,Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_MeasurementDevices_Delete");
            _db.AddInParameter(_dbCommand, "IdMeasurementDevice", DbType.Int64, idDevice);
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void Update(Int64 idDevice, Int64 idDeviceType, String reference, String serialNumber, String brand, String model, String calibrationPeriodicity, String maintenance, DateTime? installationDate, Int64 idResourcePicture, Int64 idFacility, Double upperLimit, Double lowerLimit, Double uncertainty, Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_MeasurementDevices_Update");
            _db.AddInParameter(_dbCommand, "IdMeasurementDevice", DbType.Int64, idDevice);
            _db.AddInParameter(_dbCommand, "IdMeasurementDeviceType", DbType.Int64, idDeviceType);
            _db.AddInParameter(_dbCommand, "Reference", DbType.String, reference);
            _db.AddInParameter(_dbCommand, "SerialNumber", DbType.String, serialNumber);
            _db.AddInParameter(_dbCommand, "Brand", DbType.String, brand);
            _db.AddInParameter(_dbCommand, "Model", DbType.String, model);
            _db.AddInParameter(_dbCommand, "CalibrationPeriodicity", DbType.String, calibrationPeriodicity);
            _db.AddInParameter(_dbCommand, "Maintenance", DbType.String, maintenance);
            _db.AddInParameter(_dbCommand, "InstallationDate", DbType.DateTime, installationDate);
            _db.AddInParameter(_dbCommand, "IdResourcePicture", DbType.Int64, Common.Common.CastValueToNull(idResourcePicture, 0));
            _db.AddInParameter(_dbCommand, "IdFacility", DbType.Int64, Common.Common.CastValueToNull(idFacility, 0));
            _db.AddInParameter(_dbCommand, "UpperLimit", DbType.Double, Common.Common.CastValueToNull(upperLimit, 0));
            _db.AddInParameter(_dbCommand, "LowerLimit", DbType.Double, Common.Common.CastValueToNull(lowerLimit, 0));
            _db.AddInParameter(_dbCommand, "Uncertainty", DbType.Double, Common.Common.CastValueToNull(uncertainty, 0));
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
  
        #endregion

        #region Read Functions
            internal DateTime? ReadCurrentCalibrationStartDate(Int64 idDevice)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("[PA_MeasurementDevices_CurrentCalibrationStartDate]");
                _db.AddInParameter(_dbCommand, "IdMeasurementDevice", DbType.Int64, idDevice);

                //Parámetro de salida
                _db.AddOutParameter(_dbCommand, "CalibrationStartDate", DbType.DateTime, 23);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);

                //Retorna el identificador
                if (_db.GetParameterValue(_dbCommand, "CalibrationStartDate") == DBNull.Value)
                    { return null; }

                return Convert.ToDateTime( _db.GetParameterValue(_dbCommand, "CalibrationStartDate"));

            }
            internal DateTime? ReadCurrentCalibrationEndDate(Int64 idDevice)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("[PA_MeasurementDevices_CurrentCalibrationEndDate]");
                _db.AddInParameter(_dbCommand, "IdMeasurementDevice", DbType.Int64, idDevice);

                //Parámetro de salida
                _db.AddOutParameter(_dbCommand, "CalibrationEndDate", DbType.DateTime, 23);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);

                //Retorna el identificador
                if (_db.GetParameterValue(_dbCommand, "CalibrationEndDate") == DBNull.Value)
                    { return null; }
                
                return Convert.ToDateTime(_db.GetParameterValue(_dbCommand, "CalibrationEndDate"));

            }
            internal IEnumerable<DbDataRecord> ReadById(Int64 idDevice)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("[PA_MeasurementDevices_ReadById]");
                _db.AddInParameter(_dbCommand, "IdMeasurementDevice", DbType.Int64, idDevice);
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
            internal IEnumerable<DbDataRecord> ReadAll(Int64 idMeasurementDeviceType)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_MeasurementDevices_ReadByDeviceType");
                _db.AddInParameter(_dbCommand, "idMeasurementDeviceType", DbType.Int64, idMeasurementDeviceType);
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
            internal IEnumerable<DbDataRecord> ReadAll()
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_MeasurementDevices_ReadAll");
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
