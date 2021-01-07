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
    internal class DashBoard
    {
        internal DashBoard() { }

        #region Read
        //Trae todos los types
        internal IEnumerable<DbDataRecord> ReadByPerson(Int64 idPerson, Int64 idOrganization, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_Dashboards_ReadByPerson");
            _db.AddInParameter(_dbCommand, "idPerson", DbType.Int64, idPerson);
            _db.AddInParameter(_dbCommand, "idOrganization", DbType.Int64, idOrganization);
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

        #region Write
        internal void Create(Int64 idPerson, Int64 idOrganization, String idGadget, String configuration)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_Dashboards_Create");
            //Parámetro de salida
            _db.AddInParameter(_dbCommand, "idPerson", DbType.Int64, idPerson);
            _db.AddInParameter(_dbCommand, "idOrganization", DbType.Int64, idOrganization); 
            _db.AddInParameter(_dbCommand, "IdGadget", DbType.String, idGadget);
            _db.AddInParameter(_dbCommand, "configuration", DbType.String, configuration);
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

        }
        internal void Delete(Int64 idPerson, Int64 idOrganization)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_Dashboards_DeleteByPerson");
            _db.AddInParameter(_dbCommand, "idPerson", DbType.Int64, idPerson);
            _db.AddInParameter(_dbCommand, "idOrganization", DbType.Int64, idOrganization);
 
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void Delete(Int64 idPerson, Int64 idOrganization, String idGadget)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_Dashboards_Delete");
            _db.AddInParameter(_dbCommand, "idPerson", DbType.Int64, idPerson);
            _db.AddInParameter(_dbCommand, "idOrganization", DbType.Int64, idOrganization);
            _db.AddInParameter(_dbCommand, "IdGadget", DbType.String, idGadget);
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        #endregion

    }
}
