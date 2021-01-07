using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.IA.Entities
{
    internal class Exceptions
    {
        internal Exceptions()
        {
        }

        #region Read Functions

        /// <summary>
        /// El metodo <c>Get</c> obtiene un listado de todos las ExtendedProperties para una organizacion 
        /// </summary>
        internal IEnumerable<DbDataRecord> ReadByExcecution(Int64 idExecution, Int64 idProcess)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("IA_Exceptions_ReadByExecution");
            _db.AddInParameter(_dbCommand, "idExecution", DbType.Int64, idExecution);
            _db.AddInParameter(_dbCommand, "idProcess", DbType.Int64, idProcess);
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
        internal IEnumerable<DbDataRecord> Read(Int64 idException)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("IA_Exceptions_ReadById");
            _db.AddInParameter(_dbCommand, "IdException", DbType.Int64, idException);
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
        internal IEnumerable<DbDataRecord> ReadByTask(Int64 idProcess)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("IA_Exceptions_ReadByTask");
            _db.AddInParameter(_dbCommand, "idProcess", DbType.Int64, idProcess);
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
        internal IEnumerable<DbDataRecord> ReadByTaskOutOfRange(Int64 idProcess)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("IA_Exceptions_ReadByTaskOutOfRange");
            _db.AddInParameter(_dbCommand, "idProcess", DbType.Int64, idProcess);
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
        internal IEnumerable<DbDataRecord> ReadByMeasurement(Int64 IdExecutionMeasurement)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("IA_Exceptions_ReadByMeasurement");
            _db.AddInParameter(_dbCommand, "IdExecutionMeasurement", DbType.Int64, IdExecutionMeasurement);
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

        internal IEnumerable<DbDataRecord> ReadByResource(Int64 idResource)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("IA_Exceptions_ReadByResources");
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
        internal IEnumerable<DbDataRecord> ReadByResourceVersion(Int64 idResource, Int64 idResourceFile)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("IA_Exceptions_ReadByResourceVersions");
            _db.AddInParameter(_dbCommand, "IdResource", DbType.Int64, idResource);
            _db.AddInParameter(_dbCommand, "IdResourceFile", DbType.Int64, idResourceFile);
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
        internal IEnumerable<DbDataRecord> ReadForNotification()
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("IA_Exceptions_ReadForNotification");
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
        internal IEnumerable<DbDataRecord> ReadExceptions(Int64 idProcess)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskExcutionMeasurementExceptions_ReadExceptions");
            _db.AddInParameter(_dbCommand, "idProcess", DbType.Int64, idProcess);

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

        //Add por medicion fuera de rango
        internal Int64 Create(Int64 idProcess, Int64 idExecution, Double measureValue, DateTime measureDate,  Int16 idExceptionType, String comment, Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskExcutionMeasurementExceptions_Create");
            _db.AddInParameter(_dbCommand, "idProcess", DbType.Int64, idProcess);
            _db.AddInParameter(_dbCommand, "idExecution", DbType.Int64, idExecution);
            _db.AddInParameter(_dbCommand, "Value", DbType.Double, measureValue);
            _db.AddInParameter(_dbCommand, "Date", DbType.DateTime, measureDate);
            _db.AddInParameter(_dbCommand, "idExceptionType", DbType.Int64, idExceptionType);
            _db.AddInParameter(_dbCommand, "comment", DbType.String, comment);
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);
            //Parámetro de salida
            _db.AddOutParameter(_dbCommand, "IdException", DbType.Int64, 18);
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdException"));
        }
        internal void DeleteExcutionMeasurementExceptions(Int64 idProcess, Int64 idExecution, Int64 IdExecutionMeasurement)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskExcutionMeasurementExceptions_Delete");
            _db.AddInParameter(_dbCommand, "idProcess", DbType.Int64, idProcess);
            _db.AddInParameter(_dbCommand, "idExecution", DbType.Int64, idExecution);
            _db.AddInParameter(_dbCommand, "IdExecutionMeasurement", DbType.Int64, IdExecutionMeasurement);
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

        }

        //add por tarea over due
        internal Int64 Create(Int64 idProcess, Int64 idExecution, Int16 idExceptionType, String comment, Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskExecutionExceptions_Create");
            _db.AddInParameter(_dbCommand, "idProcess", DbType.Int64, idProcess);
            _db.AddInParameter(_dbCommand, "idExecution", DbType.Int64, idExecution);
            _db.AddInParameter(_dbCommand, "idExceptionType", DbType.Int64, idExceptionType);
            _db.AddInParameter(_dbCommand, "comment", DbType.String, comment);
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);
            //Parámetro de salida
            _db.AddOutParameter(_dbCommand, "IdException", DbType.Int64, 18);
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdException"));
        }

        internal void Create(Int64 idProcess, Int64 idException)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("IA_ExceptionProcessGroupExceptions_Create");
            _db.AddInParameter(_dbCommand, "idProcess", DbType.Int64, idProcess);
            _db.AddInParameter(_dbCommand, "idException", DbType.Int64, idException);            
            //Parámetro de salida
           _db.ExecuteNonQuery(_dbCommand);

        }

        internal void Delete(Int64 idProcess, Int64 idException, Int64 idExecution)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskExecutionExceptions_Delete");
            _db.AddInParameter(_dbCommand, "idProcess", DbType.Int64, idProcess);
            _db.AddInParameter(_dbCommand, "idException", DbType.Int64, idException);
            _db.AddInParameter(_dbCommand, "IdExecution", DbType.Int64, idExecution);
            //Parámetro de salida
            _db.ExecuteNonQuery(_dbCommand);

        }

        internal void Delete(Int64 idException)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("IA_Exceptions_Delete");
            _db.AddInParameter(_dbCommand, "idException", DbType.Int64, idException);
            //Parámetro de salida
            _db.ExecuteNonQuery(_dbCommand);

        }

        internal void Update(Int64 idException, Boolean notify)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("IA_Exceptions_Update");
            _db.AddInParameter(_dbCommand, "idException", DbType.Int64, idException);
            _db.AddInParameter(_dbCommand, "Notify", DbType.Boolean, notify);
            //Parámetro de salida
            _db.ExecuteNonQuery(_dbCommand);

        }

        /// <summary>
        /// este sp lo ejecuta el servicio, y alerta de task OVERDUE
        /// </summary>
        internal void ExceptionAutomaticAlert(Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("IA_ExceptionAutomaticAlert_Create");
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);
            //Parámetro de salida
            _dbCommand.CommandTimeout = 0;
            _db.ExecuteNonQuery(_dbCommand);

        }


        internal void DeleteResourceVersionExceptions(Int64 idException)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("KC_ResourceVersionExceptions_Delete");
            _db.AddInParameter(_dbCommand, "idException", DbType.Int64, idException);
            //Parámetro de salida
            _db.ExecuteNonQuery(_dbCommand);

        }
        #endregion
    }
}
