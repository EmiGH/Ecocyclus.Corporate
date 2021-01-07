using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.PF.Entities
{
    internal class ProcessTaskPermissions
    {
        internal ProcessTaskPermissions() { }

        #region Read Functions

        internal IEnumerable<DbDataRecord> ReadById(Int64 idProcess, Int64 IdOrganizationPost, Int64 IdGeographicArea, Int64 IdFunctionalArea, Int64 IdPosition, Int64 IdPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();
     
            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskPermissions_ReadById");
            _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, IdOrganizationPost);
            _db.AddInParameter(_dbCommand, "IdGeographicArea", DbType.Int64, IdGeographicArea);
            _db.AddInParameter(_dbCommand, "IdFunctionalArea", DbType.Int64, IdFunctionalArea);
            _db.AddInParameter(_dbCommand, "IdPosition", DbType.Int64, IdPosition);
            _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, IdPerson);
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

        internal IEnumerable<DbDataRecord> ReadByProcess(Int64 idProcess)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskPermissions_ReadByProcess");
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
        internal IEnumerable<DbDataRecord> ReadByPost(Int64 IdOrganizationPost, Int64 IdGeographicArea, Int64 IdFunctionalArea, Int64 IdPosition, Int64 IdPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskPermissions_ReadByPost");
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, IdOrganizationPost);
            _db.AddInParameter(_dbCommand, "IdGeographicArea", DbType.Int64, IdGeographicArea);
            _db.AddInParameter(_dbCommand, "IdFunctionalArea", DbType.Int64, IdFunctionalArea);
            _db.AddInParameter(_dbCommand, "IdPosition", DbType.Int64, IdPosition);
            _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, IdPerson);
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

        internal void Create(Int64 idProcess, Int64 IdOrganizationPost, Int64 IdGeographicArea, Int64 IdFunctionalArea, Int64 IdPosition, Int64 IdPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskPermissions_Create");
            _db.AddInParameter(_dbCommand, "idProcess", DbType.Int64, idProcess);
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, IdOrganizationPost);
            _db.AddInParameter(_dbCommand, "IdGeographicArea", DbType.Int64, IdGeographicArea);
            _db.AddInParameter(_dbCommand, "IdFunctionalArea", DbType.Int64, IdFunctionalArea);
            _db.AddInParameter(_dbCommand, "IdPosition", DbType.Int64, IdPosition);
            _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, IdPerson);
            
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
            
        }

        internal void Delete(Int64 idProcess)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskPermissions_DeleteByProcess");
            _db.AddInParameter(_dbCommand, "idProcess", DbType.Int64, idProcess);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

        }
        internal void Delete(Int64 IdProcess, Int64 IdOrganizationPost, Int64 IdGeographicArea, Int64 IdFunctionalArea, Int64 IdPosition, Int64 IdPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskPermissions_Delete");
            _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, IdProcess);
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, IdOrganizationPost);
            _db.AddInParameter(_dbCommand, "IdGeographicArea", DbType.Int64, IdGeographicArea);
            _db.AddInParameter(_dbCommand, "IdFunctionalArea", DbType.Int64, IdFunctionalArea);
            _db.AddInParameter(_dbCommand, "IdPosition", DbType.Int64, IdPosition);
            _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, IdPerson);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

        }
        internal void Delete(Int64 IdOrganization, Int64 IdGeographicArea, Int64 IdFunctionalArea, Int64 IdPosition, Int64 IdPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskPermissions_DeleteByPost");
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, IdOrganization);
            _db.AddInParameter(_dbCommand, "IdGeographicArea", DbType.Int64, IdGeographicArea);
            _db.AddInParameter(_dbCommand, "IdFunctionalArea", DbType.Int64, IdFunctionalArea);
            _db.AddInParameter(_dbCommand, "IdPosition", DbType.Int64, IdPosition);
            _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, IdPerson);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

        }
        #endregion
    }
}
