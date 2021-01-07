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
    internal class Sites
    {
        internal Sites()
        {
        }

        #region Write Functions Facilities

        internal Int64 Create(Int64 idParentFacility, Int64 IdOrganization, String coordinate, Int64 idResourcePicture, Int64 idGeographicArea, 
            Int64 idFacilityType, Boolean active)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("GIS_Facilities_Create");
            _db.AddInParameter(_dbCommand, "IdParentFacility", DbType.Int64, Common.Common.CastValueToNull(idParentFacility, DBNull.Value));
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, IdOrganization);
            _db.AddInParameter(_dbCommand, "Coordinate", DbType.String, coordinate);
            _db.AddInParameter(_dbCommand, "IdResourcePicture", DbType.Int64, Common.Common.CastValueToNull(idResourcePicture, 0));
            _db.AddInParameter(_dbCommand, "IdFacilityType", DbType.Int64, Common.Common.CastValueToNull(idFacilityType, 0));
            _db.AddInParameter(_dbCommand, "IdGeographicArea", DbType.Int64, Common.Common.CastValueToNull(idGeographicArea, 0));
            _db.AddInParameter(_dbCommand, "Active", DbType.Boolean, active);

            //Parámetro de salida
            _db.AddOutParameter(_dbCommand, "IdFacility", DbType.Int64, 18);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdFacility"));
        }
        internal void Delete(Int64 idFacility)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("GIS_Facilities_Delete");
            _db.AddInParameter(_dbCommand, "IdFacility", DbType.String, idFacility);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void Update(Int64 idFacility, Int64 idParentFacility, String coordinate, Int64 IdOrganization, Int64 idResourcePicture, Int64 idGeographicArea, Int64 idFacilityType, Boolean active)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("GIS_Facilities_Update");
            _db.AddInParameter(_dbCommand, "IdFacility", DbType.Int64, idFacility);
            _db.AddInParameter(_dbCommand, "IdParentFacility", DbType.Int64, Common.Common.CastValueToNull(idParentFacility, DBNull.Value));
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, IdOrganization);
            _db.AddInParameter(_dbCommand, "Coordinate", DbType.String, coordinate);
            _db.AddInParameter(_dbCommand, "IdResourcePicture", DbType.Int64, Common.Common.CastValueToNull(idResourcePicture, 0));
            _db.AddInParameter(_dbCommand, "IdFacilityType", DbType.Int64, Common.Common.CastValueToNull(idFacilityType, 0));
            _db.AddInParameter(_dbCommand, "IdGeographicArea", DbType.Int64, Common.Common.CastValueToNull(idGeographicArea, 0));
            _db.AddInParameter(_dbCommand, "Active", DbType.Boolean, active);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        #endregion

        #region Read Functions

        internal IEnumerable<DbDataRecord> ReadAll(String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("GIS_Facilities_ReadAll");
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
        internal IEnumerable<DbDataRecord> ReadById(Int64 idFacility, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("GIS_Facilities_ReadById");
            _db.AddInParameter(_dbCommand, "IdFacility", DbType.Int64, idFacility);
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
        internal IEnumerable<DbDataRecord> ReadByProcess(Int64 idProcess, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("GIS_Facilities_ReadByProcess");
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
        internal IEnumerable<DbDataRecord> ReadByProcessWhitMeasurements(Int64 IdFacilityType, Int64 idProcess, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("GIS_Facilities_ReadByProcessWhitMeasurements");
            _db.AddInParameter(_dbCommand, "IdFacilityType", DbType.Int64, IdFacilityType);
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

        internal IEnumerable<DbDataRecord> ReadByParent(Int64 idParent, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("GIS_Facilities_ReadByParent");
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

            DbCommand _dbCommand = _db.GetStoredProcCommand("GIS_Facilities_ReadRoot");
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
        internal IEnumerable<DbDataRecord> ReadByOrganization(Int64 idOrganization, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("GIS_Facilities_ReadByOrganization");
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);
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
        internal IEnumerable<DbDataRecord> ReadByFacilityType(Int64 idFacilityType, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("GIS_Facilities_ReadByFacilityType");
            _db.AddInParameter(_dbCommand, "IdFacilityType", DbType.Int64, idFacilityType);
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
        internal IEnumerable<DbDataRecord> ReadPlaneByFacilityType(Int64 idFacilityType, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("GIS_Facilities_ReadPlaneByFacilityType");
            _db.AddInParameter(_dbCommand, "IdFacilityType", DbType.Int64, idFacilityType);
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
        internal IEnumerable<DbDataRecord> ReadSectorBySiteAndFacilityType(Int64 idSite, Int64 idFacilityType, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("GIS_Facilities_ReadSectorBySiteAndFacilityType");
            _db.AddInParameter(_dbCommand, "IdSite", DbType.Int64, idSite);
            _db.AddInParameter(_dbCommand, "IdFacilityType", DbType.Int64, idFacilityType); 
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
        internal IEnumerable<DbDataRecord> ReadByActivityForOrganization(Int64 IdActivity,Int64 idOrganization, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("GIS_Facilities_ReadByActivityForOrganization");
            _db.AddInParameter(_dbCommand, "IdActivity", DbType.Int64, IdActivity);
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);
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
        internal IEnumerable<DbDataRecord> ReadTotalMeasurementResultByIndicator(Int64 idScope, Int64 idFacility, Int64 idActivity, Int64 idIndicatorColumnGas, DateTime startDate, DateTime endDate)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("GIS_Facilities_ReadTotalMeasurementResultByIndicator");
            _db.AddInParameter(_dbCommand, "IdScope", DbType.Int64, idScope);
            _db.AddInParameter(_dbCommand, "IdFacility", DbType.Int64, idFacility);
            _db.AddInParameter(_dbCommand, "IdActivity", DbType.Int64, idActivity);
            _db.AddInParameter(_dbCommand, "IdIndicatorColumnGas", DbType.Int64, idIndicatorColumnGas);
            _db.AddInParameter(_dbCommand, "StartDate", DbType.DateTime, startDate);
            _db.AddInParameter(_dbCommand, "EndDate", DbType.DateTime, endDate);

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
        internal IEnumerable<DbDataRecord> FacilitiesForDashboard(Int64 idPerson, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("GIS_Facilities_ReadForDashboard");
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
            _db.AddInParameter(_dbCommand, "idPerson", DbType.Int64, idPerson);
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
