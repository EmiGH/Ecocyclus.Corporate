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
    internal class Sites_LG
    {
        internal Sites_LG()
        {
        }

        #region Write Functions

             internal void Create(Int64 idFacility, String idLanguage, String name, String description)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("GIS_Facilities_LG_Create");
            _db.AddInParameter(_dbCommand, "IdFacility", DbType.Int64, idFacility);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
            _db.AddInParameter(_dbCommand, "name", DbType.String, name); 
            _db.AddInParameter(_dbCommand, "description", DbType.String, description);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand); 
        }
        internal void Delete(Int64 idFacility, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("GIS_Facilities_LG_Delete");
            _db.AddInParameter(_dbCommand, "IdFacility", DbType.Int64, idFacility);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void DeleteByFacility(Int64 idFacility)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("GIS_Facilities_LG_DeleteByFacility");
            _db.AddInParameter(_dbCommand, "IdFacility", DbType.Int64, idFacility);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void Update(Int64 idFacility, String idLanguage, String name, String description)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("GIS_Facilities_LG_Update");
            _db.AddInParameter(_dbCommand, "IdFacility", DbType.Int64, idFacility);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
            _db.AddInParameter(_dbCommand, "name", DbType.String, name); 
            _db.AddInParameter(_dbCommand, "description", DbType.String, description);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        #endregion

        #region Read Functions

        internal IEnumerable<DbDataRecord> ReadAll(Int64 idFacility)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("GIS_Facilities_LG_ReadAll");
            _db.AddInParameter(_dbCommand, "IdFacility", DbType.Int64, idFacility);

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

            DbCommand _dbCommand = _db.GetStoredProcCommand("GIS_Facilities_LG_ReadById");
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

        #endregion

    }
}
