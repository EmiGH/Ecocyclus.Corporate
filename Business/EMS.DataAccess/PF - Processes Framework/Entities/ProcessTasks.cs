using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.PF.Entities
{
    internal class ProcessTasks
    {
        internal ProcessTasks() { }

        #region Common Read Function
            internal IEnumerable<DbDataRecord> ReadByMeasurement(Int64 idMeasurement, String idLanguage)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTasks_ReadByMeasurement");
                _db.AddInParameter(_dbCommand, "IdMeasurement", DbType.Int64, idMeasurement);
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
            internal IEnumerable<DbDataRecord> ReadTaskCalibration(Int64 IdProcess, String idLanguage)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTasks_ReadTaskCalibration");
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
            internal IEnumerable<DbDataRecord> ReadTaskOperation(Int64 IdProcess, String idLanguage)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTasks_ReadTaskOperation");
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
            internal IEnumerable<DbDataRecord> ReadByMeasurementDevice(Int64 idMeasurementDevice, String idLanguage)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTasks_ReadByMeasurementDevice");
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
            internal IEnumerable<DbDataRecord> ReadByParent(Int64 idParentProcess, String idLanguage)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTasks_ReadByParent");
                _db.AddInParameter(_dbCommand, "IdParentProcess", DbType.Int64, idParentProcess);
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
            internal IEnumerable<DbDataRecord> ReadByParentAdvanceNotice(Int64 idParentProcess, String idLanguage)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTasks_ReadByParentAdvanceNotice");
                _db.AddInParameter(_dbCommand, "IdParentProcess", DbType.Int64, idParentProcess);
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
            internal IEnumerable<DbDataRecord> ReadById(Int64 idProcess, String idLanguage)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTasks_ReadById");
                _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);
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

            internal IEnumerable<DbDataRecord> ReadByException(Int64 idException)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTasks_ReadByException");
                _db.AddInParameter(_dbCommand, "idException", DbType.Int64, idException);
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
            internal void Create(Int64 idProcess, Int64 idParentProcess, DateTime startDate, DateTime endDate, Int32 duration, Int32 interval,
                Int64 maxNumberExecutions, Boolean result, Int32 completed, Int64 timeUnitDuration, Int64 timeUnitInterval, String typeExecution,
                Int64 idFacility, Int64 idTaskInstruction, Int64 timeUnitAdvanceNotice, Int16 advanceNotice)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTasks_Create");
                _db.AddInParameter(_dbCommand, "idProcess", DbType.Int64, idProcess);
                _db.AddInParameter(_dbCommand, "IdParentProcess", DbType.Int64, idParentProcess);
                _db.AddInParameter(_dbCommand, "StartDate", DbType.DateTime, startDate);
                _db.AddInParameter(_dbCommand, "EndDate", DbType.DateTime, endDate);
                _db.AddInParameter(_dbCommand, "Duration", DbType.Int32, duration);
                _db.AddInParameter(_dbCommand, "Interval", DbType.Int32, interval);
                _db.AddInParameter(_dbCommand, "MaxNumberExecutions", DbType.Int64, maxNumberExecutions);
                _db.AddInParameter(_dbCommand, "Result", DbType.Boolean, result);
                _db.AddInParameter(_dbCommand, "Completed", DbType.Int32, completed);
                _db.AddInParameter(_dbCommand, "TimeUnitDuration", DbType.Int64, timeUnitDuration);
                _db.AddInParameter(_dbCommand, "TimeUnitInterval", DbType.Int64, timeUnitInterval);
                _db.AddInParameter(_dbCommand, "TypeExecution", DbType.String, typeExecution);
                _db.AddInParameter(_dbCommand, "IdFacility", DbType.Int64, Common.Common.CastValueToNull(idFacility, 0));
                _db.AddInParameter(_dbCommand, "idTaskInstruction", DbType.Int64, Common.Common.CastValueToNull(idTaskInstruction, 0));
                _db.AddInParameter(_dbCommand, "timeUnitAdvanceNotice", DbType.Int64, Common.Common.CastValueToNull(timeUnitAdvanceNotice, 0));
                _db.AddInParameter(_dbCommand, "advanceNotice", DbType.Int16, advanceNotice);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }
            internal void Update(Int64 idProcess, Int64 idParentProcess, DateTime startDate, DateTime endDate, Int32 duration, Int32 interval,
                Int64 maxNumberExecutions, Boolean result, Int32 completed, Int64 timeUnitDuration, Int64 timeUnitInterval, String typeExecution,
                Int64 idFacility, Int64 idTaskInstruction, Int64 timeUnitAdvanceNotice, Int16 advanceNotice)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTasks_Update");
                _db.AddInParameter(_dbCommand, "idProcess", DbType.Int64, idProcess);
                _db.AddInParameter(_dbCommand, "IdParentProcess", DbType.Int64, idParentProcess);
                _db.AddInParameter(_dbCommand, "StartDate", DbType.DateTime, startDate);
                _db.AddInParameter(_dbCommand, "EndDate", DbType.DateTime, endDate);
                _db.AddInParameter(_dbCommand, "Duration", DbType.Int32, duration);
                _db.AddInParameter(_dbCommand, "Interval", DbType.Int32, interval);
                _db.AddInParameter(_dbCommand, "MaxNumberExecutions", DbType.Int64, maxNumberExecutions);
                _db.AddInParameter(_dbCommand, "Result", DbType.Boolean, result);
                _db.AddInParameter(_dbCommand, "Completed", DbType.Int32, completed);
                _db.AddInParameter(_dbCommand, "TimeUnitDuration", DbType.Int64, timeUnitDuration);
                _db.AddInParameter(_dbCommand, "TimeUnitInterval", DbType.Int64, timeUnitInterval);
                _db.AddInParameter(_dbCommand, "TypeExecution", DbType.String, typeExecution);
                _db.AddInParameter(_dbCommand, "IdFacility", DbType.Int64, Common.Common.CastValueToNull(idFacility, 0));
                _db.AddInParameter(_dbCommand, "idTaskInstruction", DbType.Int64, Common.Common.CastValueToNull(idTaskInstruction, 0));
                _db.AddInParameter(_dbCommand, "timeUnitAdvanceNotice", DbType.Int64, Common.Common.CastValueToNull(timeUnitAdvanceNotice, 0));
                _db.AddInParameter(_dbCommand, "advanceNotice", DbType.Int16, advanceNotice);
                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }            
            internal void Delete(Int64 idProcess)
                {
                    Database _db = DatabaseFactory.CreateDatabase();

                    DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTasks_Delete");
                    _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);

                    //Ejecuta el comando
                    _db.ExecuteNonQuery(_dbCommand);
                }
            #endregion
  
     

        

     



        #region Measurement to Recovery
            internal void CreateTaskDataRecoveryMeasurement(Int64 idProcess, DateTime measurementDate, Int64 idLogPerson)
            {
                //Se guardan cuales son las mediciones que se desean recuperar.
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskDataRecoveryMeasurements_Create");
                _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);
                _db.AddInParameter(_dbCommand, "MeasurementDate", DbType.DateTime, measurementDate);

                _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }
            internal void DeleteTaskDataRecoveryMeasurement(Int64 idProcess, Int64 idLogPerson)
            {
                //Se borra todo.
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskDataRecoveryMeasurements_Delete");
                _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);
                _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }
            /// <summary>
            /// Retorna todas las tareas de Recovery para una medicion determinada.
            /// </summary>
            /// <param name="idOrganization"></param>
            /// <param name="idMeasurementRecovery"></param>
            /// <param name="idLanguage"></param>
            /// <returns></returns>
            internal IEnumerable<DbDataRecord> ReadByDataRecoveryMeasurement()
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskDataRecoveryMeasurements_ReadByMeasurement");
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
            /// <summary>
            /// Retorna todas las fechas de medicion que se deben recuperar, para una tarea de Recovery.
            /// </summary>
            /// <param name="idOrganization"></param>
            /// <param name="idProcess"></param>
            /// <param name="idMeasurementRecovery"></param>
            /// <param name="idLanguage"></param>
            /// <returns></returns>
            internal IEnumerable<DbDataRecord> ReadByDataRecoveryMeasurement(Int64 idProcess)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskDataRecoveryMeasurements_ReadById");
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
        #endregion

    }
}
