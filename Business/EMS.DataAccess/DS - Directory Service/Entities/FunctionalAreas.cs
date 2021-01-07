using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.DS
{
    internal class FunctionalAreas
    {
        internal FunctionalAreas()
        {
        }

        #region Write Functions

        internal Int64 Create(Int64 idOrganization, Int64 idParentFunctionalArea, String idLanguage, String mnemo, String name, Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_FunctionalAreas_Create");
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);
            _db.AddInParameter(_dbCommand, "IdParentFunctionalArea", DbType.Int64, Common.Common.CastValueToNull(idParentFunctionalArea, DBNull.Value));
            _db.AddInParameter(_dbCommand, "name", DbType.String, name);
            _db.AddInParameter(_dbCommand, "Mnemo", DbType.String, mnemo);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

            //Parámetro de salida
            _db.AddOutParameter(_dbCommand, "IdFunctionalArea", DbType.Int64, 18);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdFunctionalArea"));
        }
        internal void Delete(Int64 idFunctionalArea, Int64 idOrganization, Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_FunctionalAreas_Delete");
            _db.AddInParameter(_dbCommand, "IdFunctionalArea", DbType.String, idFunctionalArea);
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.String, idOrganization);
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void Update(Int64 idFunctionalArea, Int64 idParentFunctionalArea, Int64 idOrganization, String idLanguage, String name, String mnemo, Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_FunctionalAreas_Update");
            _db.AddInParameter(_dbCommand, "IdFunctionalArea", DbType.Int64, idFunctionalArea);
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.String, idOrganization);
            _db.AddInParameter(_dbCommand, "IdParentFunctionalArea", DbType.Int64, Common.Common.CastValueToNull(idParentFunctionalArea, DBNull.Value));
            _db.AddInParameter(_dbCommand, "Mnemo", DbType.String, mnemo);
            _db.AddInParameter(_dbCommand, "name", DbType.String, name);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        #endregion

        #region Read Functions

        internal IEnumerable<DbDataRecord> GetRoot(Int64 idOrganization, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_FunctionalAreas_ReadRoot");
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
        internal IEnumerable<DbDataRecord> GetByParent(Int64 idParentFunctionalArea, Int64 idOrganization, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_FunctionalAreas_ReadByParent");
            _db.AddInParameter(_dbCommand, "IdParentFunctionalArea", DbType.Int64, idParentFunctionalArea);
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
        internal IEnumerable<DbDataRecord> ReadAll(Int64 idOrganization, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_FunctionalAreas_ReadAll");
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
        internal IEnumerable<DbDataRecord> ReadById(Int64 idFunctionalArea, Int64 idOrganization, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_FunctionalAreas_ReadById");
            _db.AddInParameter(_dbCommand, "IdFunctionalArea", DbType.Int64, idFunctionalArea);
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

        #endregion

    }
}
