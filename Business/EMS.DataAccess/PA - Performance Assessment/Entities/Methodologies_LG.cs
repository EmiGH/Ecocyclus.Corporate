using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.PA.Entities
{
    internal class Methodologies_LG
    {
        internal Methodologies_LG()
        { }

        #region Read Functions
        internal IEnumerable<DbDataRecord> ReadById(Int64 idMethodology, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Methodologies_LG_ReadByID");
            _db.AddInParameter(_dbCommand, "IdMethodology", DbType.Int64, idMethodology);
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
        internal IEnumerable<DbDataRecord> ReadAll(Int64 idMethodology)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Methodologies_LG_ReadAll");
            _db.AddInParameter(_dbCommand, "idMethodology", DbType.Int64, idMethodology);
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

        internal void Create(Int64 idMethodology, String idLanguage, String methodName, String methodType, String description)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Methodologies_LG_Create");
            _db.AddInParameter(_dbCommand, "IdMethodology", DbType.Int64, idMethodology);
            _db.AddInParameter(_dbCommand, "idLanguage", DbType.String, idLanguage);
            _db.AddInParameter(_dbCommand, "MethodName", DbType.String, methodName);
            _db.AddInParameter(_dbCommand, "MethodType", DbType.String, methodType);
            _db.AddInParameter(_dbCommand, "description", DbType.String, description);
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        internal void Delete(Int64 idMethodology, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Methodologies_LG_Delete");
            _db.AddInParameter(_dbCommand, "IdMethodology", DbType.Int64, idMethodology);
            _db.AddInParameter(_dbCommand, "idLanguage", DbType.String, idLanguage);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        internal void Delete(Int64 idMethodology)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Methodologies_LG_DeleteByMethodology");
            _db.AddInParameter(_dbCommand, "IdMethodology", DbType.Int64, idMethodology);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        internal void Update(Int64 idMethodology, String idLanguage, String methodName, String methodType, String description)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Methodologies_LG_Update");
            _db.AddInParameter(_dbCommand, "IdMethodology", DbType.Int64, idMethodology);
            _db.AddInParameter(_dbCommand, "idLanguage", DbType.String, idLanguage);
            _db.AddInParameter(_dbCommand, "MethodName", DbType.String, methodName);
            _db.AddInParameter(_dbCommand, "MethodType", DbType.String, methodType);
            _db.AddInParameter(_dbCommand, "description", DbType.String, description);
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        #endregion

    }
}
