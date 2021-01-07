using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.DS
{
    internal class OrganizationsRelationships
    {
        internal OrganizationsRelationships()
        {
        }

        #region Write Functions

        internal void Create(Int64 idOrganization1, Int64 idOrganization2, Int64 idOrganizationRelationshipType, Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_OrganizationsRelationships_Create");
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization1);
            _db.AddInParameter(_dbCommand, "IdOrganizationRelated", DbType.Int64, idOrganization2);
            _db.AddInParameter(_dbCommand, "IdOrganizationRelationshipType", DbType.Int64, idOrganizationRelationshipType);
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void Delete(Int64 idOrganization1, Int64 idOrganization2, Int64 idOrganizationRelationshipType, Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_OrganizationsRelationships_Delete");
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization1);
            _db.AddInParameter(_dbCommand, "IdOrganizationRelated", DbType.Int64, idOrganization2);
            _db.AddInParameter(_dbCommand, "IdOrganizationRelationshipType", DbType.Int64, idOrganizationRelationshipType);
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        internal void Delete(Int64 idOrganization)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_OrganizationsRelationships_DeletebyOrganization");
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        #endregion

        #region Read Functions

        internal IEnumerable<DbDataRecord> GetAllByIdOrganization(Int64 idOrganization)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_OrganizationsRelationships_ReadAllByOrganization");
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);

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
        internal IEnumerable<DbDataRecord> ReadById(Int64 idOrganization1, Int64 idOrganization2, Int64 idOrganizationRelationshipType)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_OrganizationsRelationships_ReadById");
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization1);
            _db.AddInParameter(_dbCommand, "IdOrganizationRelated", DbType.Int64, idOrganization2);
            _db.AddInParameter(_dbCommand, "IdOrganizationRelationshipType", DbType.Int64, idOrganizationRelationshipType);

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
