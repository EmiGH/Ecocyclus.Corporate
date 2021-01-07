using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.DS.Entities
{
    internal class OrganizationalCharts
    {
        internal OrganizationalCharts()
        {
        }

        #region Write Functions 

        internal Int64 Create(Int64 idOrganization)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_OrganizationalCharts_Create");
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);

            //Parámetro de salida
            _db.AddOutParameter(_dbCommand, "IdOrganizationalChart", DbType.Int64, 18);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdOrganizationalChart"));
        }
        internal void Delete(Int64 idOrganization, Int64 idOrganizationalChart)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_OrganizationalCharts_Delete");
            _db.AddInParameter(_dbCommand, "IdOrganizationalChart", DbType.String, idOrganizationalChart);
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
  
        #endregion
  
        #region Read Functions

        internal IEnumerable<DbDataRecord> ReadAll(Int64 idOrganization, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_OrganizationalCharts_ReadAll");
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
        internal IEnumerable<DbDataRecord> ReadById(Int64 idOrganizationalChart, Int64 idOrganization, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_OrganizationalCharts_ReadById");
            _db.AddInParameter(_dbCommand, "IdOrganizationalChart", DbType.Int64, idOrganizationalChart);
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
        internal IEnumerable<DbDataRecord> ReadByJobTitle(Int64 idGeographicArea, Int64 idFunctionalArea, Int64 idPosition, Int64 idOrganization)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_OrganizationalChartJobTitles_ReadByJobTitle");
            _db.AddInParameter(_dbCommand, "idGeographicArea", DbType.Int64, idGeographicArea);
            _db.AddInParameter(_dbCommand, "idFunctionalArea", DbType.Int64, idFunctionalArea);
            _db.AddInParameter(_dbCommand, "idPosition", DbType.Int64, idPosition);
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
    }
}
