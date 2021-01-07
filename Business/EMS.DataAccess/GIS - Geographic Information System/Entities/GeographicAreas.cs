using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.GIS
{
    internal class GeographicAreas
    {
        internal GeographicAreas()
        {
        }

        #region Write Functions GeographicAreas

        internal Int64 Create(Int64 idParentGeographicArea, String coordinate, String isoCode, Int64 IdOrganization, String Layer)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("GIS_GeographicAreas_Create");
            _db.AddInParameter(_dbCommand, "IdParentGeographicArea", DbType.Int64, Common.Common.CastValueToNull(idParentGeographicArea, DBNull.Value));
            _db.AddInParameter(_dbCommand, "Coordinate", DbType.String, coordinate);
            _db.AddInParameter(_dbCommand, "ISOCode", DbType.String, isoCode);
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, Common.Common.CastValueToNull(IdOrganization, DBNull.Value));
            _db.AddInParameter(_dbCommand, "Layer", DbType.String, Layer);

            //Parámetro de salida
            _db.AddOutParameter(_dbCommand, "IdGeographicArea", DbType.Int64, 18);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdGeographicArea"));
        }
        internal void Delete(Int64 idGeographicArea)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("GIS_GeographicAreas_Delete");
            _db.AddInParameter(_dbCommand, "IdGeographicArea", DbType.String, idGeographicArea);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void Update(Int64 idGeographicArea, Int64 idParentGeographicArea, String coordinate, String isoCode, Int64 IdOrganization, String Layer)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("GIS_GeographicAreas_Update");
            _db.AddInParameter(_dbCommand, "IdGeographicArea", DbType.Int64, idGeographicArea);
            _db.AddInParameter(_dbCommand, "IdParentGeographicArea", DbType.Int64, Common.Common.CastValueToNull(idParentGeographicArea, DBNull.Value));
            _db.AddInParameter(_dbCommand, "Coordinate", DbType.String, coordinate);
            _db.AddInParameter(_dbCommand, "ISOCode", DbType.String, isoCode);
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, Common.Common.CastValueToNull(IdOrganization, DBNull.Value));
            _db.AddInParameter(_dbCommand, "Layer", DbType.String, Layer);
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        internal void Update(Int64 IdOrganization)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("GIS_GeographicAreas_UpdateRemoveOrganization");
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, IdOrganization);
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        #endregion

        #region Read Functions

        internal IEnumerable<DbDataRecord> ReadAll(String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("GIS_GeographicAreas_ReadAll");
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
        internal IEnumerable<DbDataRecord> ReadById(Int64 idGeographicArea, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("GIS_GeographicAreas_ReadById");
            _db.AddInParameter(_dbCommand, "IdGeographicArea", DbType.Int64, idGeographicArea);
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
        internal IEnumerable<DbDataRecord> ReadByParent(Int64 idParent, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("GIS_GeographicAreas_ReadByParent");
            _db.AddInParameter(_dbCommand, "IdParent", DbType.Int64, Common.Common.CastValueToNull(idParent, DBNull.Value));
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
        internal IEnumerable<DbDataRecord> ReadRoot(String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("GIS_GeographicAreas_ReadRoot");
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
    }
}
