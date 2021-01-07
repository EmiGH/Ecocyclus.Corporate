using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.PF.Entities
{
    internal class ProcessTaskExecutions
    {
        internal ProcessTaskExecutions() { }

        #region Common Read Function
            internal IEnumerable<DbDataRecord> ReadAll(Int64 idProcess)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskExecutions_ReadAll");
                _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);
                _dbCommand.CommandTimeout = 0;
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
            internal IEnumerable<DbDataRecord> ReadAllOnlyExecution(Int64 idProcess)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskExecutions_ReadAllOnly");
                _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);
                _dbCommand.CommandTimeout = 0;
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
            internal IEnumerable<DbDataRecord> ReadById(Int64 idProcess, Int64 idExecution)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskExecutions_ReadById");
                _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);
                _db.AddInParameter(_dbCommand, "IdExecution", DbType.Int64, idExecution);
                _dbCommand.CommandTimeout = 0;
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
            internal IEnumerable<DbDataRecord> ReadFirstExecution(Int64 idProcess)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskExecutions_ReadFirstExecution");
                _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);
                _dbCommand.CommandTimeout = 0;
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
            internal IEnumerable<DbDataRecord> ReadLastExecution(Int64 idProcess)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskExecutions_ReadLastExecution");
                _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);
                _dbCommand.CommandTimeout = 0;
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
            internal IEnumerable<DbDataRecord> ReadLastExecutionMeasurement(Int64 idProcess)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskExecutions_ReadLastExecutionMeasurement");
                _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);
                _dbCommand.CommandTimeout = 0;
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
            internal IEnumerable<DbDataRecord> ReadNowExecution(Int64 idProcess, Int64 timeUnit, Int64 duration)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskExecutions_ReadExecutionNow");
                _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);
                _db.AddInParameter(_dbCommand, "TimeUnit", DbType.Int64, timeUnit);
                _db.AddInParameter(_dbCommand, "Duration", DbType.Int64, duration);
                _dbCommand.CommandTimeout = 0;
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
            internal IEnumerable<DbDataRecord> ReadNextExecution(Int64 idProcess, DateTime startDate, DateTime endDate)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskExecutions_ReadExecutionNext");
                _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);
                _db.AddInParameter(_dbCommand, "StartDate", DbType.DateTime, startDate); 
                _db.AddInParameter(_dbCommand, "EndDate", DbType.DateTime, endDate);
                _dbCommand.CommandTimeout = 0;
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
            internal IEnumerable<DbDataRecord> ReadExecutionNextNotifice(Int64 idProcess)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskExecutions_ReadExecutionNextNotifice");
                _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);
                _dbCommand.CommandTimeout = 0;
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
            internal IEnumerable<DbDataRecord> ReadByPlanned(Int64 idPerson)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskExecutions_ReadByPlanned");
                _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, idPerson);
                _dbCommand.CommandTimeout = 0;
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
            internal IEnumerable<DbDataRecord> ReadByPlanned(DateTime plannedDate, Int64 idPerson)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskExecutions_ReadByPlannedDate");
                _db.AddInParameter(_dbCommand, "PlannedDate", DbType.DateTime, plannedDate);
                _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, idPerson);
                _dbCommand.CommandTimeout = 0;
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
            internal IEnumerable<DbDataRecord> ReadByWorking(Int64 idPerson)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskExecutions_ReadByWorking");
                _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, idPerson);
                _dbCommand.CommandTimeout = 0;
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
            internal IEnumerable<DbDataRecord> ReadByFinished(Int64 idPerson)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskExecutions_ReadByFinished");
                _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, idPerson);
                _dbCommand.CommandTimeout = 0;
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
            internal IEnumerable<DbDataRecord> ReadByOverdue(Int64 idPerson)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskExecutions_ReadByOverDue");
                _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, idPerson);
                _dbCommand.CommandTimeout = 0;
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

        #region Write Execution Functions
            internal void Create(Int64 idProcess, DateTime startDate, DateTime endDate, Int32 interval, Int64 timeUnitInterval, String typeExecution, Boolean result)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskExecutions_CreateAllByDate");
                _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);
                _db.AddInParameter(_dbCommand, "StartDate", DbType.DateTime, startDate);
                _db.AddInParameter(_dbCommand, "EndDate", DbType.DateTime, endDate);
                _db.AddInParameter(_dbCommand, "Interval", DbType.Int16, interval);
                _db.AddInParameter(_dbCommand, "TimeUnitInterval", DbType.Int64, timeUnitInterval);
                _db.AddInParameter(_dbCommand, "TypeExecution", DbType.String, typeExecution);
                _db.AddInParameter(_dbCommand, "Result", DbType.Boolean, result);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);

            }
            internal void Delete(Int64 idProcess)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskExecutions_DeleteAllByDate");
                _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }
            internal void Update(Int64 idProcess, DateTime startDate, DateTime endDate, Int32 interval, Int64 timeUnitInterval, String typeExecution)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskExecutions_UpdateAllByDate");
                _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);
                _db.AddInParameter(_dbCommand, "StartDate", DbType.DateTime, startDate);
                _db.AddInParameter(_dbCommand, "EndDate", DbType.DateTime, endDate);
                _db.AddInParameter(_dbCommand, "Interval", DbType.Int16, interval);
                _db.AddInParameter(_dbCommand, "TimeUnitInterval", DbType.Int64, timeUnitInterval);
                _db.AddInParameter(_dbCommand, "TypeExecution", DbType.String, typeExecution);


                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }
            internal void Update(Int64 idProcess, Int64 IdExecution, Boolean AdvanceNotify)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskExecutions_UpdateNotify");
                _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);
                _db.AddInParameter(_dbCommand, "IdExecution", DbType.Int64, IdExecution);
                _db.AddInParameter(_dbCommand, "AdvanceNotify", DbType.Boolean, AdvanceNotify);


                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }
        #endregion

        #region Write Process Task Execution Calibration
        internal Int64 CreateExecution(Int64 idProcess, Int64 idOrganization, Int64 idPerson, Int64 idPosition, Int64 idFunctionalArea, 
            Int64 idGeographicArea, DateTime date, String comment, Byte[] attachment, DateTime validationStart, DateTime validationEnd, Int64 idMeasurementDevice, Int64 idLogPerson)
        {
            //Este metodo termina insertando primero en la tabla de PF_ProcessTaskExecutions y despues en PF_ProcessTaskExecutionCalibrations
            //Hace todo en un solo paso...
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskExecutionCalibrations_CreateBoth");
            _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);
            _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, idPerson);
            _db.AddInParameter(_dbCommand, "IdPosition", DbType.Int64, idPosition);
            _db.AddInParameter(_dbCommand, "IdFunctionalArea", DbType.Int64, idFunctionalArea);
            _db.AddInParameter(_dbCommand, "IdGeographicArea", DbType.Int64, idGeographicArea);
            _db.AddInParameter(_dbCommand, "Date", DbType.DateTime, date);
            _db.AddInParameter(_dbCommand, "Comment", DbType.String, comment);
            _db.AddInParameter(_dbCommand, "Attachment", DbType.Binary, Common.Common.CastValueToNull(attachment)); 
            _db.AddInParameter(_dbCommand, "ValidationStart", DbType.DateTime, validationStart);
            _db.AddInParameter(_dbCommand, "ValidationEnd", DbType.DateTime, validationEnd);

            _db.AddInParameter(_dbCommand, "IdMeasurementDevice", DbType.Int64, Common.Common.CastValueToNull(idMeasurementDevice, 0));

            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

            //Parámetro de salida
            _db.AddOutParameter(_dbCommand, "IdExecution", DbType.Int64, 18);
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdExecution"));
        }
        internal void CreateExecution(Int64 idProcess, Int64 idExecution, Int64 idOrganization, Int64 idPerson, Int64 idPosition,
            Int64 idFunctionalArea, Int64 idGeographicArea, String comment, Byte[] attachment, DateTime validationStart, DateTime validationEnd, Int64 idMeasurementDevice, Int64 idLogPerson)
        {
            //Este metodo termina insertando sobre la tabla PF_ProcessTaskExecutionCalibrations
            //Para lo cual ya debe existir el idExecution, por eso se pasa por parametros...
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskExecutionCalibrations_Create");
            _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);
            _db.AddInParameter(_dbCommand, "IdExecution", DbType.Int64, idExecution);
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);
            _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, idPerson);
            _db.AddInParameter(_dbCommand, "IdPosition", DbType.Int64, idPosition);
            _db.AddInParameter(_dbCommand, "IdFunctionalArea", DbType.Int64, idFunctionalArea);
            _db.AddInParameter(_dbCommand, "IdGeographicArea", DbType.Int64, idGeographicArea);
            _db.AddInParameter(_dbCommand, "Comment", DbType.String, comment);
            _db.AddInParameter(_dbCommand, "Attachment", DbType.Binary, Common.Common.CastValueToNull(attachment)); 

            _db.AddInParameter(_dbCommand, "ValidationStart", DbType.DateTime, validationStart);
            _db.AddInParameter(_dbCommand, "ValidationEnd", DbType.DateTime, validationEnd);

            _db.AddInParameter(_dbCommand, "IdMeasurementDevice", DbType.Int64, Common.Common.CastValueToNull(idMeasurementDevice, 0));

            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal Int64 CreateExecution(Int64 idProcess, Int64 idExecution, Int64 idOrganization, Int64 idPerson, Int64 idPosition,
            Int64 idFunctionalArea, Int64 idGeographicArea, String comment, Byte[] attachment, String fileName, Byte[] fileStream, DateTime validationStart, DateTime validationEnd, Int64 idMeasurementDevice, Int64 idLogPerson)
        {
            //Este metodo termina insertando sobre la tabla PF_ProcessTaskExecutionCalibrations y PF_ProcessTaskExecutionFileAttachs
            //Para lo cual ya debe existir el idExecution, por eso se pasa por parametros...
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskExecutionCalibrationFileAttachs_Create");
            _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);
            _db.AddInParameter(_dbCommand, "IdExecution", DbType.Int64, idExecution);
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);
            _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, idPerson);
            _db.AddInParameter(_dbCommand, "IdPosition", DbType.Int64, idPosition);
            _db.AddInParameter(_dbCommand, "IdFunctionalArea", DbType.Int64, idFunctionalArea);
            _db.AddInParameter(_dbCommand, "IdGeographicArea", DbType.Int64, idGeographicArea);
            _db.AddInParameter(_dbCommand, "Comment", DbType.String, comment);
            _db.AddInParameter(_dbCommand, "Attachment", DbType.Binary, Common.Common.CastValueToNull(attachment)); 

            _db.AddInParameter(_dbCommand, "FileName", DbType.String, fileName);
            _db.AddInParameter(_dbCommand, "FileStream", DbType.Binary, fileStream);

            _db.AddInParameter(_dbCommand, "ValidationStart", DbType.DateTime, validationStart);
            _db.AddInParameter(_dbCommand, "ValidationEnd", DbType.DateTime, validationEnd);

            _db.AddInParameter(_dbCommand, "IdMeasurementDevice", DbType.Int64, Common.Common.CastValueToNull(idMeasurementDevice, 0));

            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

            //Parámetro de salida
            _db.AddOutParameter(_dbCommand, "IdFile", DbType.Int64, 18);

            _dbCommand.CommandTimeout = 0;
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdFile"));
        }
        internal void CreateExecution(ref Int64 idExecution, ref Int64 idFile, Int64 idProcess, Int64 idOrganization, Int64 idPerson,
            Int64 idPosition, Int64 idFunctionalArea, Int64 idGeographicArea, DateTime date, String comment, Byte[] attachment, String fileName, Byte[] fileStream, 
            DateTime validationStart, DateTime validationEnd, Int64 idMeasurementDevice, Int64 idLogPerson)
        {
            //Este metodo termina insertando sobre la tabla PF_ProcessTaskExecutions, en la PF_ProcessTaskExecutionCalibrations y por ultimo en PF_ProcessTaskExecutionFileAttachs
            //Por eso retorna lso 2 Id's idExecution y el segundo idFile como referencia.
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskExecutionCalibrationFileAttachs_CreateBoth");
            _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);
            _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, idPerson);
            _db.AddInParameter(_dbCommand, "IdPosition", DbType.Int64, idPosition);
            _db.AddInParameter(_dbCommand, "IdFunctionalArea", DbType.Int64, idFunctionalArea);
            _db.AddInParameter(_dbCommand, "IdGeographicArea", DbType.Int64, idGeographicArea);
            _db.AddInParameter(_dbCommand, "Date", DbType.DateTime, date);
            _db.AddInParameter(_dbCommand, "Comment", DbType.String, comment);
            _db.AddInParameter(_dbCommand, "Attachment", DbType.Binary, Common.Common.CastValueToNull(attachment)); 

            _db.AddInParameter(_dbCommand, "FileName", DbType.String, fileName);
            _db.AddInParameter(_dbCommand, "FileStream", DbType.Binary, fileStream);

            _db.AddInParameter(_dbCommand, "ValidationStart", DbType.DateTime, validationStart);
            _db.AddInParameter(_dbCommand, "ValidationEnd", DbType.DateTime, validationEnd);

            _db.AddInParameter(_dbCommand, "IdMeasurementDevice", DbType.Int64, Common.Common.CastValueToNull(idMeasurementDevice, 0));

            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

            //Parámetro de salida
            _db.AddOutParameter(_dbCommand, "IdFile", DbType.Int64, 18);
            _db.AddOutParameter(_dbCommand, "IdExecution", DbType.Int64, 18);

            _dbCommand.CommandTimeout = 0;
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna los identificadores como referencia...
            idExecution = Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdExecution"));
            idFile = Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdFile"));
        }
        internal void DeleteExecutionCalibration(Int64 idProcess, Int64 idExecution)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskExecutionCalibrations_Delete");
            _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);
            _db.AddInParameter(_dbCommand, "IdExecution", DbType.Int64, idExecution);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
    #endregion

        #region Write Process Task Execution Measurement
        internal Int64 CreateExecution(Int64 idProcess, Int64 idOrganization, Int64 idPerson, Int64 idPosition, Int64 idFunctionalArea,
            Int64 idGeographicArea, DateTime date, String comment, Byte[] attachment, Double measureValue, DateTime measureDate, ref DateTime timeStamp, Int64 idMeasurementDevice, Int64 idMeasurementUnit, DateTime startDate, DateTime endDate, Int64 idLogPerson)
            {
                //Este metodo termina insertando primero en la tabla PF_ProcessTaskExecutions y despues en PF_ProcessTaskExecutionMeasurements
                //Hace todo en un solo paso.
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskExecutionMeasurements_CreateBoth");
                _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);
                _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);
                _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, idPerson);
                _db.AddInParameter(_dbCommand, "IdPosition", DbType.Int64, idPosition);
                _db.AddInParameter(_dbCommand, "IdFunctionalArea", DbType.Int64, idFunctionalArea);
                _db.AddInParameter(_dbCommand, "IdGeographicArea", DbType.Int64, idGeographicArea);
                _db.AddInParameter(_dbCommand, "Date", DbType.DateTime, date);
                _db.AddInParameter(_dbCommand, "Comment", DbType.String, comment);
                _db.AddInParameter(_dbCommand, "Attachment", DbType.Binary, Common.Common.CastValueToNull(attachment)); 

                _db.AddInParameter(_dbCommand, "MeasureValue", DbType.Double, measureValue);
                _db.AddInParameter(_dbCommand, "MeasureDate", DbType.DateTime, measureDate);

                _db.AddInParameter(_dbCommand, "IdMeasurementDevice", DbType.Int64, Common.Common.CastValueToNull(idMeasurementDevice, 0));
                _db.AddInParameter(_dbCommand, "IdMeasurementUnit", DbType.Int64, Common.Common.CastValueToNull(idMeasurementUnit, 0));

                _db.AddInParameter(_dbCommand, "StartDate", DbType.DateTime, startDate);
                _db.AddInParameter(_dbCommand, "EndDate", DbType.DateTime, endDate);


                _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

                //Parámetro de salida
                _db.AddOutParameter(_dbCommand, "IdExecution", DbType.Int64, 18);
                _db.AddOutParameter(_dbCommand, "TimeStamp", DbType.DateTime, 23);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);

                //Retorna el identificador
                timeStamp = Convert.ToDateTime(_db.GetParameterValue(_dbCommand, "TimeStamp"));
                return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdExecution"));
            }
        internal Int64 CreateExecution(Int64 idProcess, Int64 idExecution, Int64 idOrganization, Int64 idPerson, Int64 idPosition,
            Int64 idFunctionalArea, Int64 idGeographicArea, String comment, Byte[] attachment, Double measureValue, DateTime measureDate, 
            ref DateTime timeStamp, Int64 idMeasurementDevice, Int64 idMeasurementUnit, DateTime startDate, DateTime endDate, Int64 idLogPerson)
            {
                //Este metodo inserta solamente en PF_ProcessTaskExecutionMeasurements
                //Para ello necesita ya tener cargada la ejecucion, por eso recibe como parametro el idExecution.
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskExecutionMeasurements_Create");
                _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);
                _db.AddInParameter(_dbCommand, "IdExecution", DbType.Int64, idExecution);
                _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);
                _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, idPerson);
                _db.AddInParameter(_dbCommand, "IdPosition", DbType.Int64, idPosition);
                _db.AddInParameter(_dbCommand, "IdFunctionalArea", DbType.Int64, idFunctionalArea);
                _db.AddInParameter(_dbCommand, "IdGeographicArea", DbType.Int64, idGeographicArea);
                _db.AddInParameter(_dbCommand, "Comment", DbType.String, comment);
                _db.AddInParameter(_dbCommand, "Attachment", DbType.Binary, Common.Common.CastValueToNull(attachment)); 

                _db.AddInParameter(_dbCommand, "MeasureValue", DbType.Decimal, measureValue);
                _db.AddInParameter(_dbCommand, "MeasureDate", DbType.DateTime, measureDate);
                
                _db.AddInParameter(_dbCommand, "IdMeasurementDevice", DbType.Int64, Common.Common.CastValueToNull(idMeasurementDevice, 0));
                _db.AddInParameter(_dbCommand, "IdMeasurementUnit", DbType.Int64, Common.Common.CastValueToNull(idMeasurementUnit, 0));

                _db.AddInParameter(_dbCommand, "StartDate", DbType.DateTime, startDate);
                _db.AddInParameter(_dbCommand, "EndDate", DbType.DateTime, endDate);

                
                _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

                _db.AddOutParameter(_dbCommand, "IdExecutionMeasurement", DbType.Int64, 18);

                _dbCommand.CommandTimeout = 0;
                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
                return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdExecutionMeasurement"));

            }
        internal void CreateExecutionForCalculate(Int64 idProcess, Int64 idExecution, Double measureValue, DateTime measureDate, Int64 idMeasurementUnit, Int64 idMeasurementDevice, DateTime startDate, DateTime endDate, Double minuteValue)
            {
                //Este metodo inserta solamente en PF_ProcessTaskExecutionMeasurements
                //Para ello necesita ya tener cargada la ejecucion, por eso recibe como parametro el idExecution.
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskExecutionMeasurementForCalculates_Create");
                _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);
                _db.AddInParameter(_dbCommand, "IdMeasurementDevice", DbType.Int64, Common.Common.CastValueToNull(idMeasurementDevice, 0));
                _db.AddInParameter(_dbCommand, "IdMeasurementUnit", DbType.Int64, idMeasurementUnit);
     
                _db.AddInParameter(_dbCommand, "MeasureValue", DbType.Double, measureValue);
                _db.AddInParameter(_dbCommand, "MeasureDate", DbType.DateTime, measureDate);

                _db.AddInParameter(_dbCommand, "StartDate", DbType.DateTime, startDate);
                _db.AddInParameter(_dbCommand, "EndDate", DbType.DateTime, endDate);
                _db.AddInParameter(_dbCommand, "MinuteValue", DbType.Double, minuteValue);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);

            }
        internal void UpdateExecutionForCalculate(Int64 idProcess, Double measureValue, DateTime measureDate)
            {
                //Este metodo inserta solamente en PF_ProcessTaskExecutionMeasurements
                //Para ello necesita ya tener cargada la ejecucion, por eso recibe como parametro el idExecution.
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskExecutionMeasurementForCalculates_Update");
                _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);
                
                _db.AddInParameter(_dbCommand, "MeasureValue", DbType.Double, measureValue);
                _db.AddInParameter(_dbCommand, "MeasureDate", DbType.DateTime, measureDate);
              
                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);

            }

        internal void UpdateMeasurementExecution(DataTable dtTVPMeasurements)
        {
            Database _db = DatabaseFactory.CreateDatabase();
                                                                           
            SqlCommand _dbCommand = (SqlCommand)_db.GetStoredProcCommand("PF_ProcessTaskExecutionMeasurementDT_Update");

            //Arma el parametro de tipo tabla
            SqlParameter _tvpParam = _dbCommand.Parameters.AddWithValue("@dtMeasurement", dtTVPMeasurements);
            //Indica que es de tipo Structured, para que lo identifique el SQL como Table.
            _tvpParam.SqlDbType = SqlDbType.Structured;

            _dbCommand.CommandTimeout = 0;
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void UpdateMeasurementExecutionForCalculate(DataTable dtTVPMeasurements)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            SqlCommand _dbCommand = (SqlCommand)_db.GetStoredProcCommand("PF_ProcessTaskExecutionMeasurementForCalculatesDT_Update");

            //Arma el parametro de tipo tabla
            SqlParameter _tvpParam = _dbCommand.Parameters.AddWithValue("@dtMeasurement", dtTVPMeasurements);
            //Indica que es de tipo Structured, para que lo identifique el SQL como Table.
            _tvpParam.SqlDbType = SqlDbType.Structured;

            _dbCommand.CommandTimeout = 0;
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

            //internal void CreateExecutionForCalculate(Int64 idProcess, String fileName, String fileStream, Int64 idMeasurementDevice, Int64 idMeasurementUnit, DateTime startDate, DateTime endDate)
            //{
            //    //con archivo
            //    //Este metodo finalmente inserta en PF_ProcessTaskExecutionMeasurements y en PF_ProcessTaskExecutionFileAttachs
            //    //Para ello necesita ya tener cargada la ejecucion, por eso recibe como parametro el idExecution.
            //    Database _db = DatabaseFactory.CreateDatabase();

            //    DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskExecutionMeasurementForCalculatesFileAttachs_Create");
            //    _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);

            //    _db.AddInParameter(_dbCommand, "FileName", DbType.String, fileName);
            //    _db.AddInParameter(_dbCommand, "FileStream", DbType.String, fileStream);

            //    _db.AddInParameter(_dbCommand, "IdMeasurementDevice", DbType.Int64, Common.Common.CastValueToNull(idMeasurementDevice, 0));
            //    _db.AddInParameter(_dbCommand, "IdMeasurementUnit", DbType.Int64, Common.Common.CastValueToNull(idMeasurementUnit, 0));

            //    _db.AddInParameter(_dbCommand, "StartDate", DbType.DateTime, startDate);
            //    _db.AddInParameter(_dbCommand, "EndDate", DbType.DateTime, endDate);

            //    _dbCommand.CommandTimeout = 0;
            //    //Ejecuta el comando
            //    _db.ExecuteNonQuery(_dbCommand);
            //}
            internal void CreateExecutionForCalculateCumulative(Int64 idProcess, DataTable dtTVPMeasurements, Int64 idMeasurementDevice, Int64 idMeasurementUnit)
            {
                //con archivo
                //Este metodo finalmente inserta en PF_ProcessTaskExecutionMeasurements y en PF_ProcessTaskExecutionFileAttachs
                //Para ello necesita ya tener cargada la ejecucion, por eso recibe como parametro el idExecution.
                Database _db = DatabaseFactory.CreateDatabase();

                SqlCommand _dbCommand = (SqlCommand)_db.GetStoredProcCommand("PF_ProcessTaskExecutionMeasurementForCalculatesTVP_Cumulative_Create");
                _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);

                //Arma el parametro de tipo tabla
                SqlParameter _tvpParam = _dbCommand.Parameters.AddWithValue("@tvpMeasurement", dtTVPMeasurements);
                //Indica que es de tipo Structured, para que lo identifique el SQL como Table.
                _tvpParam.SqlDbType = SqlDbType.Structured;

                _db.AddInParameter(_dbCommand, "IdMeasurementDevice", DbType.Int64, Common.Common.CastValueToNull(idMeasurementDevice, 0));
                _db.AddInParameter(_dbCommand, "IdMeasurementUnit", DbType.Int64, idMeasurementUnit);

                _dbCommand.CommandTimeout = 0;
                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }
            internal void CreateExecutionForCalculateNotCumulative(Int64 idProcess, DataTable dtTVPMeasurements, Int64 idMeasurementDevice, Int64 idMeasurementUnit)
            {
                //con archivo
                //Este metodo finalmente inserta en PF_ProcessTaskExecutionMeasurements y en PF_ProcessTaskExecutionFileAttachs
                //Para ello necesita ya tener cargada la ejecucion, por eso recibe como parametro el idExecution.
                Database _db = DatabaseFactory.CreateDatabase();

                SqlCommand _dbCommand = (SqlCommand)_db.GetStoredProcCommand("PF_ProcessTaskExecutionMeasurementForCalculatesTVP_NotCumulative_Create");
                _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);

                //Arma el parametro de tipo tabla
                SqlParameter _tvpParam = _dbCommand.Parameters.AddWithValue("@tvpMeasurement", dtTVPMeasurements);
                //Indica que es de tipo Structured, para que lo identifique el SQL como Table.
                _tvpParam.SqlDbType = SqlDbType.Structured;

                _db.AddInParameter(_dbCommand, "IdMeasurementDevice", DbType.Int64, Common.Common.CastValueToNull(idMeasurementDevice, 0));
                _db.AddInParameter(_dbCommand, "IdMeasurementUnit", DbType.Int64, idMeasurementUnit);

                _dbCommand.CommandTimeout = 0;
                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }

            //internal void UpdateExecutionForCalculate(Int64 idProcess, String fileName, String fileStream)
            //{
            //    //update con archivo
            //    //Este metodo finalmente inserta en PF_ProcessTaskExecutionMeasurements y en PF_ProcessTaskExecutionFileAttachs
            //    //Para ello necesita ya tener cargada la ejecucion, por eso recibe como parametro el idExecution.
            //    Database _db = DatabaseFactory.CreateDatabase();

            //    DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskExecutionMeasurementForCalculatesFileAttachs_Update");
            //    _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);

            //    _db.AddInParameter(_dbCommand, "FileName", DbType.String, fileName);
            //    _db.AddInParameter(_dbCommand, "FileStream", DbType.String, fileStream);

            //    _dbCommand.CommandTimeout = 0;
            //    //Ejecuta el comando
            //    _db.ExecuteNonQuery(_dbCommand);
            //}
            internal void UpdateExecutionForCalculate(Int64 idProcess, DataTable dtTVPMeasurements)
            {
                //update con archivo
                //Este metodo finalmente inserta en PF_ProcessTaskExecutionMeasurements y en PF_ProcessTaskExecutionFileAttachs
                //Para ello necesita ya tener cargada la ejecucion, por eso recibe como parametro el idExecution.
                Database _db = DatabaseFactory.CreateDatabase();

                SqlCommand _dbCommand = (SqlCommand)_db.GetStoredProcCommand("PF_ProcessTaskExecutionMeasurementForCalculatesTVP_Update");
                _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);

                //Arma el parametro de tipo tabla
                SqlParameter _tvpParam = _dbCommand.Parameters.AddWithValue("@tvpMeasurement", dtTVPMeasurements);
                //Indica que es de tipo Structured, para que lo identifique el SQL como Table.
                _tvpParam.SqlDbType = SqlDbType.Structured;

                _dbCommand.CommandTimeout = 0;
                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }
            internal void DeleteExecutionForCalculate(Int64 idProcess)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                SqlCommand _dbCommand = (SqlCommand)_db.GetStoredProcCommand("PF_ProcessTaskExecutionMeasurementForCalculates_Delete");
                _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }

            //New
            internal void CreateExecutionTVPForCalculate(Int64 idProcess, DataTable dtTVPMeasurements, Int64 idMeasurementDevice, Int64 idMeasurementUnit, Boolean isCumulative)
            {
                //Este metodo finalmente inserta en PF_ProcessTaskExecutionMeasurements y en PF_ProcessTaskExecutionFileAttachs
                //Para ello necesita ya tener cargada la ejecucion, por eso recibe como parametro el idExecution.
                Database _db = DatabaseFactory.CreateDatabase();

                SqlCommand _dbCommand = (SqlCommand)_db.GetStoredProcCommand("PF_ProcessTaskExecutionMeasurementForCalculatesTVP_Create");
                _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);
                //Arma el parametro de tipo tabla
                SqlParameter _tvpParam = _dbCommand.Parameters.AddWithValue("@tvpMeasurement", dtTVPMeasurements);
                //Indica que es de tipo Structured, para que lo identifique el SQL como Table.
                _tvpParam.SqlDbType = SqlDbType.Structured;
                
                _db.AddInParameter(_dbCommand, "IdMeasurementDevice", DbType.Int64, Common.Common.CastValueToNull(idMeasurementDevice, 0));
                _db.AddInParameter(_dbCommand, "IdMeasurementUnit", DbType.Int64, Common.Common.CastValueToNull(idMeasurementUnit, 0));

                _db.AddInParameter(_dbCommand, "IsCumulative", DbType.Boolean, isCumulative);

                _dbCommand.CommandTimeout = 0;
                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);

            }
            internal Int64 CreateExecutionTVP(Int64 idProcess, Int64 idExecution, Int64 idOrganization, Int64 idPerson, Int64 idPosition,
                   Int64 idFunctionalArea, Int64 idGeographicArea, String comment, Byte[] attachment,
                   DataTable dtTVPMeasurements, Int64 idMeasurementDevice, Int64 idMeasurementUnit, Int64 idLogPerson)
            {
                //Este metodo finalmente inserta en PF_ProcessTaskExecutionMeasurements y en PF_ProcessTaskExecutionFileAttachs
                //Para ello necesita ya tener cargada la ejecucion, por eso recibe como parametro el idExecution.
                Database _db = DatabaseFactory.CreateDatabase();

                SqlCommand _dbCommand = (SqlCommand)_db.GetStoredProcCommand("PF_ProcessTaskExecutionMeasurementTVP_Create");
                _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);
                _db.AddInParameter(_dbCommand, "IdExecution", DbType.Int64, idExecution);
                _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);
                _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, idPerson);
                _db.AddInParameter(_dbCommand, "IdPosition", DbType.Int64, idPosition);
                _db.AddInParameter(_dbCommand, "IdFunctionalArea", DbType.Int64, idFunctionalArea);
                _db.AddInParameter(_dbCommand, "IdGeographicArea", DbType.Int64, idGeographicArea);
                _db.AddInParameter(_dbCommand, "Comment", DbType.String, comment);
                _db.AddInParameter(_dbCommand, "Attachment", DbType.Binary, Common.Common.CastValueToNull(attachment));
                //Arma el parametro de tipo tabla
                SqlParameter _tvpParam = _dbCommand.Parameters.AddWithValue("@tvpMeasurement", dtTVPMeasurements);
                //Indica que es de tipo Structured, para que lo identifique el SQL como Table.
                _tvpParam.SqlDbType = SqlDbType.Structured;

                _db.AddInParameter(_dbCommand, "IdMeasurementDevice", DbType.Int64, Common.Common.CastValueToNull(idMeasurementDevice, 0));
                _db.AddInParameter(_dbCommand, "IdMeasurementUnit", DbType.Int64, Common.Common.CastValueToNull(idMeasurementUnit, 0));
                _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

                //Parámetro de salida
                _db.AddOutParameter(_dbCommand, "IdExecutionMeasurement", DbType.Int64, 18);

                _dbCommand.CommandTimeout = 0;
                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);

                //Retorna el identificador                
                return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdExecutionMeasurement"));
            }
            internal Int64 CreateExecution(Int64 idProcess, Int64 idExecution, Int64 idOrganization, Int64 idPerson, Int64 idPosition,
                Int64 idFunctionalArea, Int64 idGeographicArea, String comment, Byte[] attachment,
                DataTable dtTVPMeasurements, Int64 idMeasurementDevice, Int64 idMeasurementUnit, Int64 idLogPerson)
            {
                //Este metodo finalmente inserta en PF_ProcessTaskExecutionMeasurements y en PF_ProcessTaskExecutionFileAttachs
                //Para ello necesita ya tener cargada la ejecucion, por eso recibe como parametro el idExecution.
                Database _db = DatabaseFactory.CreateDatabase();

                SqlCommand _dbCommand = (SqlCommand)_db.GetStoredProcCommand("PF_ProcessTaskExecutionMeasurementTVP_Create");
                _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);
                _db.AddInParameter(_dbCommand, "IdExecution", DbType.Int64, idExecution);
                _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);
                _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, idPerson);
                _db.AddInParameter(_dbCommand, "IdPosition", DbType.Int64, idPosition);
                _db.AddInParameter(_dbCommand, "IdFunctionalArea", DbType.Int64, idFunctionalArea);
                _db.AddInParameter(_dbCommand, "IdGeographicArea", DbType.Int64, idGeographicArea);
                _db.AddInParameter(_dbCommand, "Comment", DbType.String, comment);
                _db.AddInParameter(_dbCommand, "Attachment", DbType.Binary, Common.Common.CastValueToNull(attachment)); 
                //Arma el parametro de tipo tabla
                SqlParameter _tvpParam = _dbCommand.Parameters.AddWithValue("@tvpMeasurement", dtTVPMeasurements);
                //Indica que es de tipo Structured, para que lo identifique el SQL como Table.
                _tvpParam.SqlDbType = SqlDbType.Structured;

                //_db.AddOutParameter(_dbCommand, "MeasureValue", DbType.Double, 18);
                //_db.AddOutParameter(_dbCommand, "MeasureDate", DbType.DateTime, 23);

                _db.AddInParameter(_dbCommand, "IdMeasurementDevice", DbType.Int64, Common.Common.CastValueToNull(idMeasurementDevice, 0));
                _db.AddInParameter(_dbCommand, "IdMeasurementUnit", DbType.Int64, Common.Common.CastValueToNull(idMeasurementUnit, 0));
                _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

                //Parámetro de salida
                _db.AddOutParameter(_dbCommand, "IdExecutionMeasurement", DbType.Int64,18);

                _dbCommand.CommandTimeout = 0;
                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);

                //Retorna el identificador
                //timeStamp = Convert.ToDateTime(_db.GetParameterValue(_dbCommand, "TimeStamp"));
                //ref DateTime timeStamp, 
                return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdExecutionMeasurement"));
            }
  
            internal void CreateExecution(ref Int64 idExecution, ref Int64 idFile, Int64 idProcess, Int64 idOrganization, Int64 idPerson,
                Int64 idPosition, Int64 idFunctionalArea, Int64 idGeographicArea, DateTime date, String comment, Byte[] attachment,
                String fileName, String fileStream, Byte[] fileStreamBinary, Int64 idMeasurementDevice, Int64 idMeasurementUnit, Int64 idLogPerson)
            {
                //Este metodo crea e inserta en PF_ProcessTaskExecutions, 
                //en PF_ProcessTaskExecutionMeasurements y en PF_ProcessTaskExecutionFileAttachs , y en PF_ProcessTaskExecutionMeasurementsforCalculate
                //Por eso retorna lso 2 Id's idExecution y el segundo idFile como referencia.
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskExecutionMeasurementFileAttachs_CreateBoth");
                _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);
                _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);                
                _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, idPerson);
                _db.AddInParameter(_dbCommand, "IdPosition", DbType.Int64, idPosition);
                _db.AddInParameter(_dbCommand, "IdFunctionalArea", DbType.Int64, idFunctionalArea);
                _db.AddInParameter(_dbCommand, "IdGeographicArea", DbType.Int64, idGeographicArea);
                _db.AddInParameter(_dbCommand, "Date", DbType.DateTime, date);
                _db.AddInParameter(_dbCommand, "Comment", DbType.String, comment);
                _db.AddInParameter(_dbCommand, "Attachment", DbType.Binary, Common.Common.CastValueToNull(attachment)); 

                _db.AddInParameter(_dbCommand, "FileName", DbType.String, fileName);
                _db.AddInParameter(_dbCommand, "FileStream", DbType.String, fileStream);
                _db.AddInParameter(_dbCommand, "FileStreamBinary", DbType.Binary, fileStreamBinary);

                //_db.AddInParameter(_dbCommand, "MeasureValue", DbType.Double, measureValue);
                //_db.AddInParameter(_dbCommand, "MeasureDate", DbType.DateTime, measureDate);
                //_db.AddInParameter(_dbCommand, "TimeStamp", DbType.DateTime, timeStamp);

                _db.AddInParameter(_dbCommand, "IdMeasurementDevice", DbType.Int64, Common.Common.CastValueToNull(idMeasurementDevice, 0));
                _db.AddInParameter(_dbCommand, "IdMeasurementUnit", DbType.Int64, Common.Common.CastValueToNull(idMeasurementUnit, 0));

                //_db.AddInParameter(_dbCommand, "StartDate", DbType.DateTime, startDate);
                //_db.AddInParameter(_dbCommand, "EndDate", DbType.DateTime, endDate);


                _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

                //Parámetro de salida
                _db.AddOutParameter(_dbCommand, "IdFile", DbType.Int64, 18);
                _db.AddOutParameter(_dbCommand, "IdExecution", DbType.Int64, 18);
                //_db.AddOutParameter(_dbCommand, "TimeStamp", DbType.DateTime, 23);

                _dbCommand.CommandTimeout = 0;
                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);

                //Retorna los identificadores como referencia...
                idExecution = Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdExecution"));
                idFile = Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdFile"));
                //timeStamp = Convert.ToDateTime(_db.GetParameterValue(_dbCommand, "TimeStamp"));
            }
            internal void DeleteExecutionMeasurement(Int64 idProcess, Int64 idExecution)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskExecutionMeasurements_Delete");
                _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);
                _db.AddInParameter(_dbCommand, "IdExecution", DbType.Int64, idExecution);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }
            internal void ResetExecution(Int64 idProcess, Int64 idExecution)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskExecutions_ResetExecution");
                _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);
                _db.AddInParameter(_dbCommand, "IdExecution", DbType.Int64, idExecution);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }
        #endregion

        #region Write Process Task Execution General
            internal Int64 CreateExecution(Int64 idProcess, Int64 idOrganization, Int64 idPerson, Int64 idPosition, Int64 idFunctionalArea, Int64 idGeographicArea, DateTime date, String comment, Boolean result, Int64 idLogPerson, Byte[] Attachment)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskExecutions_Create");
                _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);
                _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);
                _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, idPerson);
                _db.AddInParameter(_dbCommand, "IdPosition", DbType.Int64, idPosition);
                _db.AddInParameter(_dbCommand, "IdFunctionalArea", DbType.Int64, idFunctionalArea);
                _db.AddInParameter(_dbCommand, "IdGeographicArea", DbType.Int64, idGeographicArea);
                _db.AddInParameter(_dbCommand, "Date", DbType.DateTime, date);
                _db.AddInParameter(_dbCommand, "Comment", DbType.String, comment);
                _db.AddInParameter(_dbCommand, "Result", DbType.Boolean, result);
                _db.AddInParameter(_dbCommand, "Attachment", DbType.Binary, Common.Common.CastValueToNull(Attachment));

                //Parámetro de salida
                _db.AddOutParameter(_dbCommand, "IdExecution", DbType.Int64, 18);
                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);

                //Retorna el identificador
                return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdExecution"));
            }
            internal void DeleteExecution(Int64 idProcess, Int64 idExecution)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskExecutions_Delete");
                _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);
                _db.AddInParameter(_dbCommand, "IdExecution", DbType.Int64, idExecution);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }
            internal void UpdateExecution(Int64 idProcess, Int64 idExecution, Int64 idOrganization, Int64 idPerson, Int64 idPosition, Int64 idFunctionalArea, Int64 idGeographicArea, String comment, Boolean result, Int64 idLogPerson, Byte[] Attachment)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskExecutions_Update");
                _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);
                _db.AddInParameter(_dbCommand, "IdExecution", DbType.Int64, idExecution);
                _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);
                _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, idPerson);
                _db.AddInParameter(_dbCommand, "IdPosition", DbType.Int64, idPosition);
                _db.AddInParameter(_dbCommand, "IdFunctionalArea", DbType.Int64, idFunctionalArea);
                _db.AddInParameter(_dbCommand, "IdGeographicArea", DbType.Int64, idGeographicArea);
                _db.AddInParameter(_dbCommand, "Comment", DbType.String, comment);
                _db.AddInParameter(_dbCommand, "Result", DbType.Boolean, result);
                _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);
                _db.AddInParameter(_dbCommand, "Attachment", DbType.Binary, Common.Common.CastValueToNull(Attachment));

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }
        #endregion

        #region Write Process Task Execution File Attach
            internal Int64 CreateExecutionFileAttach(Int64 idProcess, Int64 idExecution, String fileName, Byte[] fileStreamBinary, Int64 idLogPerson)
            {
                //Este metodo finalmente inserta en PF_ProcessTaskExecutionMeasurements y en PF_ProcessTaskExecutionFileAttachs
                //Para ello necesita ya tener cargada la ejecucion, por eso recibe como parametro el idExecution.
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskExecutionFileAttachs_CreateOnly");
                _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);
                _db.AddInParameter(_dbCommand, "IdExecution", DbType.Int64, idExecution);
                _db.AddInParameter(_dbCommand, "FileName", DbType.String, fileName);
                _db.AddInParameter(_dbCommand, "FileStreamBinary", DbType.Binary, fileStreamBinary);
                _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

                //Parámetro de salida
                _db.AddOutParameter(_dbCommand, "IdFile", DbType.Int64, 18);

                _dbCommand.CommandTimeout = 0;
                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);

                //Retorna el identificador
                return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdFile"));
            }
            internal void DeleteExecutionFileAttach(Int64 idProcess, Int64 idExecution)
            {
                //Este metodo finalmente inserta en PF_ProcessTaskExecutionMeasurements y en PF_ProcessTaskExecutionFileAttachs
                //Para ello necesita ya tener cargada la ejecucion, por eso recibe como parametro el idExecution.
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskExecutionFileAttachs_Delete");
                _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);
                _db.AddInParameter(_dbCommand, "IdExecution", DbType.Int64, idExecution);

                _dbCommand.CommandTimeout = 0;
                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }
        #endregion

    }
}
