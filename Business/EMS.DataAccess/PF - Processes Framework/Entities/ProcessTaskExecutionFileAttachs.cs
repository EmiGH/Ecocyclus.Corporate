using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.PF.Entities
{
    internal class ProcessTaskExecutionFileAttachs
    {
        internal ProcessTaskExecutionFileAttachs() { }

        #region Read Function
            internal IEnumerable<DbDataRecord> ReadAll(Int64 idProcess, Int64 idExecution)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskExecutionFileAttachs_ReadAll");
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
            internal IEnumerable<DbDataRecord> ReadById(Int64 idProcess, Int64 idExecution, Int64 idFile)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskExecutionFileAttachs_ReadById");
                _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);
                _db.AddInParameter(_dbCommand, "IdExecution", DbType.Int64, idExecution);
                _db.AddInParameter(_dbCommand, "IdFile", DbType.Int64, idFile);
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

        #region Write Process Task Execution File Attachs
            internal Int64 CreateExecution(Int64 idProcess, Int64 idExecution, String fileName, String fileStream, Int64 idLogPerson)
            {
                //Este metodo finalmente inserta en la tabla KC_FileAttachs y despues en PF_ProcessTaskExecutionFileAttachs.
                //La ejecucion ya debe existir, por eso se recibe como parametro.
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskExecutionFileAttachs_Create");
                _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);
                _db.AddInParameter(_dbCommand, "IdExecution", DbType.Int64, idExecution);
                _db.AddInParameter(_dbCommand, "FileName", DbType.String, fileName);
                _db.AddInParameter(_dbCommand, "FileStream", DbType.String, fileStream);
                _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

                //Parámetro de salida
                _db.AddOutParameter(_dbCommand, "IdFile", DbType.Int64, 18);
                _dbCommand.CommandTimeout = 0;
                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);

                //Retorna el identificador
                return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdFile"));
            }
            internal void CreateExecution(ref Int64 idExecution, ref Int64 idFile, Int64 idProcess, Int64 idPerson, Int64 idPosition, Int64 idFunctionalArea, Int64 idGeographicArea, DateTime date, String comment, String fileName, String fileStream, Int64 idLogPerson)
            {
                //Este metodo finalmente inserta en la tabla PF_ProcessTaskExecutions, en KC_FileAttachs y por ultimo en PF_ProcessTaskExecutionFileAttachs
                //hace todo en un solo paso. Crea la ejecucion (general) y le Asocia un archivo a la misma.
                //Por eso retorna lso 2 Id's idExecution y el segundo idFile como referencia.
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskExecutionFileAttachs_CreateBoth");
                _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);
                _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, idPerson);
                _db.AddInParameter(_dbCommand, "IdPosition", DbType.Int64, idPosition);
                _db.AddInParameter(_dbCommand, "IdFunctionalArea", DbType.Int64, idFunctionalArea);
                _db.AddInParameter(_dbCommand, "IdGeographicArea", DbType.Int64, idGeographicArea);
                _db.AddInParameter(_dbCommand, "Date", DbType.DateTime, date);
                _db.AddInParameter(_dbCommand, "Comment", DbType.String, comment);

                _db.AddInParameter(_dbCommand, "FileName", DbType.String, fileName);
                _db.AddInParameter(_dbCommand, "FileStream", DbType.String, fileStream);
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
            internal void DeleteExecution(Int64 idProcess, Int64 idExecution, Int64 idFile, Int64 idLogPerson)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskExecutionFileAttachs_Delete");
                _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);
                _db.AddInParameter(_dbCommand, "IdExecution", DbType.Int64, idExecution);
                //_db.AddInParameter(_dbCommand, "IdFile", DbType.Int64, idFile);
                //_db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }
        #endregion
    }
}
