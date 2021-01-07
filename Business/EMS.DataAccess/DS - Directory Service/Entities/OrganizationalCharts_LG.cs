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
    internal class OrganizationalCharts_LG
    {
        internal OrganizationalCharts_LG() 
        {
        }

        #region Write Functions

        internal void Create(Int64 IdOrganizationalChart, Int64 IdOrganization, String idLanguage, String name, String Description)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_OrganizationalCharts_LG_Create");
            _db.AddInParameter(_dbCommand, "IdOrganizationalChart", DbType.Int64, IdOrganizationalChart);
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, IdOrganization);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
            _db.AddInParameter(_dbCommand, "name", DbType.String, name);
            _db.AddInParameter(_dbCommand, "Description", DbType.String, Description);
            
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void Delete(Int64 IdOrganizationalChart, Int64 IdOrganization, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_OrganizationalCharts_LG_Delete");
            _db.AddInParameter(_dbCommand, "IdOrganizationalChart", DbType.Int64, IdOrganizationalChart);
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, IdOrganization);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
        
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void Delete(Int64 IdOrganizationalChart, Int64 IdOrganization)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_OrganizationalCharts_LG_DeleteByOrganizationalChart");
            _db.AddInParameter(_dbCommand, "IdOrganizationalChart", DbType.Int64, IdOrganizationalChart);
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, IdOrganization);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void Update(Int64 IdOrganizationalChart, Int64 IdOrganization, String idLanguage, String name, String Description)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_OrganizationalCharts_LG_Update");
            _db.AddInParameter(_dbCommand, "IdOrganizationalChart", DbType.Int64, IdOrganizationalChart);
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, IdOrganization);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
            _db.AddInParameter(_dbCommand, "name", DbType.String, name);
            _db.AddInParameter(_dbCommand, "Description", DbType.String, Description);
           
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        #endregion

        #region Read Functions
        internal IEnumerable<DbDataRecord> ReadAll(Int64 IdOrganizationalChart, Int64 IdOrganization)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_OrganizationalCharts_LG_ReadAll");
            _db.AddInParameter(_dbCommand, "IdOrganizationalChart", DbType.Int64, IdOrganizationalChart);
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, IdOrganization);
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
        internal IEnumerable<DbDataRecord> ReadById(Int64 IdOrganizationalChart, Int64 IdOrganization, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_OrganizationalCharts_LG_ReadById");
            _db.AddInParameter(_dbCommand, "IdOrganizationalChart", DbType.Int64, IdOrganizationalChart);
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, IdOrganization);
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
