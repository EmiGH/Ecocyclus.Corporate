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
    internal class Addresses
    {
                internal Addresses()
        {
        }

        #region Write Functions Addresses
		
        internal Int64 Create(Int64 idFacility, Int64 idGeographicArea, Int64 idPerson, String coordinate, 
            String street, String number, String floor, String department, String postCode)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("GIS_Addresses_Create");
            _db.AddInParameter(_dbCommand, "IdFacility", DbType.Int64, Common.Common.CastValueToNull(idFacility, DBNull.Value));
            _db.AddInParameter(_dbCommand, "IdGeographicArea", DbType.Int64, idGeographicArea);
            _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, Common.Common.CastValueToNull(idPerson, DBNull.Value));
            _db.AddInParameter(_dbCommand, "Coordinate", DbType.String, coordinate);
            _db.AddInParameter(_dbCommand, "Street", DbType.String, street);
            _db.AddInParameter(_dbCommand, "Number", DbType.String, number);
            _db.AddInParameter(_dbCommand, "Floor", DbType.String, floor);
            _db.AddInParameter(_dbCommand, "Department", DbType.String, department);
            _db.AddInParameter(_dbCommand, "PostCode", DbType.String, postCode);
            
            //Parámetro de salida
            _db.AddOutParameter(_dbCommand, "IdAddress", DbType.Int64, 18);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdAddress"));
        }
        internal void Delete(Int64 idAddress)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("GIS_Addresses_Delete");
            _db.AddInParameter(_dbCommand, "IdAddress", DbType.String, idAddress);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void Update(Int64 idAddress, Int64 idFacility, Int64 idGeographicArea, Int64 idPerson, String coordinate, 
            String street, String number, String floor, String department, String postCode)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("GIS_Addresses_Update");
            _db.AddInParameter(_dbCommand, "IdAddress", DbType.Int64, idAddress);
            _db.AddInParameter(_dbCommand, "IdFacility", DbType.Int64, Common.Common.CastValueToNull(idFacility, DBNull.Value));
            _db.AddInParameter(_dbCommand, "IdGeographicArea", DbType.Int64, idGeographicArea);
            _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, Common.Common.CastValueToNull(idPerson, DBNull.Value));
            _db.AddInParameter(_dbCommand, "Coordinate", DbType.String, coordinate);
            _db.AddInParameter(_dbCommand, "Street", DbType.String, street);
            _db.AddInParameter(_dbCommand, "Number", DbType.String, number);
            _db.AddInParameter(_dbCommand, "Floor", DbType.String, floor);
            _db.AddInParameter(_dbCommand, "Department", DbType.String, department);
            _db.AddInParameter(_dbCommand, "PostCode", DbType.String, postCode);
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        #endregion

        #region Read Functions

        internal IEnumerable<DbDataRecord> ReadAll()
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("GIS_Addresses_ReadAll");
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
        internal IEnumerable<DbDataRecord> ReadById(Int64 idAddress)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("GIS_Addresses_ReadById");
            _db.AddInParameter(_dbCommand, "IdAddress", DbType.Int64, idAddress);

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
        internal IEnumerable<DbDataRecord> ReadByFacility(Int64 idFacility)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("GIS_Addresses_ReadByFacility");
            _db.AddInParameter(_dbCommand, "idFacility", DbType.Int64, idFacility);

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
        internal IEnumerable<DbDataRecord> ReadByGeographicArea(Int64 IdGeographicArea)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("GIS_Addresses_ReadByGeographicArea");
            _db.AddInParameter(_dbCommand, "IdGeographicArea", DbType.String, IdGeographicArea);

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

        internal IEnumerable<DbDataRecord> ReadByPerson(Int64 IdPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("GIS_Addresses_ReadByPerson");
            _db.AddInParameter(_dbCommand, "IdPerson", DbType.String, IdPerson);

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
