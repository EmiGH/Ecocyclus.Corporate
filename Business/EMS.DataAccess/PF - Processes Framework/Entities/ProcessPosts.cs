using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.PF.Entities
{
    internal class ProcessPosts
    {
        internal ProcessPosts() { }

        #region Read Function
            internal IEnumerable<DbDataRecord> ReadAll(Int64 idProcess, Int64 idPerson)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessPosts_ReadAll");
                _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);
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
            internal IEnumerable<DbDataRecord> ReadById(Int64 idProcess, Int64 idPerson, Int64 idPosition, Int64 idFunctionalArea, Int64 idGeographicArea)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessPosts_ReadById");
                _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);
                _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, idPerson);
                _db.AddInParameter(_dbCommand, "IdPosition", DbType.Int64, idPosition);
                _db.AddInParameter(_dbCommand, "IdFunctionalArea", DbType.Int64, idFunctionalArea);
                _db.AddInParameter(_dbCommand, "IdGeographicArea", DbType.Int64, idGeographicArea);

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
            internal void Create(Int64 idProcess, Int64 idPerson, Int64 idPosition, Int64 idFunctionalArea, Int64 idGeographicArea, DateTime startDate, DateTime endDate, Int64 idPermission, Int64 idLogPerson)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessPosts_Create");
                _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);
                _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, idPerson);
                _db.AddInParameter(_dbCommand, "IdPosition", DbType.Int64, idPosition);
                _db.AddInParameter(_dbCommand, "IdFunctionalArea", DbType.Int64, idFunctionalArea);
                _db.AddInParameter(_dbCommand, "IdGeographicArea", DbType.Int64, idGeographicArea);
                _db.AddInParameter(_dbCommand, "StartDate", DbType.DateTime, startDate);
                _db.AddInParameter(_dbCommand, "EndDate", DbType.DateTime, endDate);
                _db.AddInParameter(_dbCommand, "IdPermission", DbType.Int64, idPermission);

                _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }
            internal void Delete(Int64 idProcess, Int64 idPerson, Int64 idPosition, Int64 idFunctionalArea, Int64 idGeographicArea, Int64 idLogPerson)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessPosts_Delete");
                _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);
                _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, idPerson);
                _db.AddInParameter(_dbCommand, "IdPosition", DbType.Int64, idPosition);
                _db.AddInParameter(_dbCommand, "IdFunctionalArea", DbType.Int64, idFunctionalArea);
                _db.AddInParameter(_dbCommand, "IdGeographicArea", DbType.Int64, idGeographicArea);
                _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }
            internal void Update(Int64 idProcess, Int64 idPerson, Int64 idPosition, Int64 idFunctionalArea, Int64 idGeographicArea, DateTime startDate, DateTime endDate, Int64 idPermission, Int64 idLogPerson)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessPosts_Update");
                _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);
                _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, idPerson);
                _db.AddInParameter(_dbCommand, "IdPosition", DbType.Int64, idPosition);
                _db.AddInParameter(_dbCommand, "IdFunctionalArea", DbType.Int64, idFunctionalArea);
                _db.AddInParameter(_dbCommand, "IdGeographicArea", DbType.Int64, idGeographicArea);
                _db.AddInParameter(_dbCommand, "StartDate", DbType.DateTime, startDate);
                _db.AddInParameter(_dbCommand, "EndDate", DbType.DateTime, endDate);
                _db.AddInParameter(_dbCommand, "IdPermission", DbType.Int64, idPermission);

                _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }

        #endregion
    }
}
