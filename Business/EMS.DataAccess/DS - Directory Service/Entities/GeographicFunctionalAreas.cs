using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.DS
{
    internal class GeographicFunctionalAreas
    {
        internal GeographicFunctionalAreas()
        {
        }

        #region Read Functions
 
        internal IEnumerable<DbDataRecord> ReadAll(Int64 idOrganization)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_GeographicFunctionalAreas_ReadAll");
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
        internal IEnumerable<DbDataRecord> ReadById(Int64 idFunctionalArea, Int64 idGeographicArea, Int64 idOrganization)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_GeographicFunctionalAreas_ReadById");
            _db.AddInParameter(_dbCommand, "IdFunctionalArea", DbType.Int64, idFunctionalArea);
            _db.AddInParameter(_dbCommand, "IdGeographicArea", DbType.Int64, idGeographicArea);
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
        internal IEnumerable<DbDataRecord> GetByParent(Int64 idParentFunctionalArea, Int64 idParentGeographicArea, Int64 idOrganization)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_GeographicFunctionalAreas_ReadByParent");
            _db.AddInParameter(_dbCommand, "IdParentFunctionalArea", DbType.Int64, idParentFunctionalArea);
            _db.AddInParameter(_dbCommand, "IdParentGeographicArea", DbType.Int64, idParentGeographicArea);
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
        internal IEnumerable<DbDataRecord> GetRoot(Int64 idOrganization)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_GeographicFunctionalAreas_ReadRoot");
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

        #endregion

        #region Write Functions

        internal void Delete(Int64 idFunctionalArea, Int64 idGeographicArea, Int64 idOrganization, Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_GeographicFunctionalAreas_Delete");
            _db.AddInParameter(_dbCommand, "IdFunctionalArea", DbType.Int64, idFunctionalArea);
            _db.AddInParameter(_dbCommand, "IdGeographicArea", DbType.Int64, idGeographicArea);
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void Create(Int64 idFunctionalArea, Int64 idGeographicArea, Int64 idOrganization, Int64 idParentFunctionalArea, Int64 idParentGeographicArea, Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_GeographicFunctionalAreas_Create");
            _db.AddInParameter(_dbCommand, "IdFunctionalArea", DbType.Int64, idFunctionalArea);
            _db.AddInParameter(_dbCommand, "IdGeographicArea", DbType.Int64, idGeographicArea);
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);
            _db.AddInParameter(_dbCommand, "IdParentFunctionalArea", DbType.String, Common.Common.CastValueToNull(idParentFunctionalArea, DBNull.Value));
            _db.AddInParameter(_dbCommand, "IdParentGeographicArea", DbType.String, Common.Common.CastValueToNull(idParentGeographicArea, DBNull.Value));

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

        }
        internal void Update(Int64 idFunctionalArea, Int64 idGeographicArea, Int64 idOrganization, Int64 idParentFunctionalArea, Int64 idParentGeographicArea, Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_GeographicFunctionalAreas_Update");
            _db.AddInParameter(_dbCommand, "IdFunctionalArea", DbType.Int64, idFunctionalArea);
            _db.AddInParameter(_dbCommand, "IdGeographicArea", DbType.Int64, idGeographicArea);
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);
            _db.AddInParameter(_dbCommand, "IdParentFunctionalArea", DbType.String, Common.Common.CastValueToNull(idParentFunctionalArea, DBNull.Value));
            _db.AddInParameter(_dbCommand, "IdParentGeographicArea", DbType.String, Common.Common.CastValueToNull(idParentGeographicArea, DBNull.Value));

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

        }

        #endregion

    }
}
