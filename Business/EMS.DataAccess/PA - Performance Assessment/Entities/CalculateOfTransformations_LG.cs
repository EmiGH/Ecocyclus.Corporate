using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.PA.Entities
{
    internal class CalculateOfTransformations_LG
    {
        internal CalculateOfTransformations_LG() { }

        #region Read Functions
        //Trae todos los resources 
        internal IEnumerable<DbDataRecord> ReadAll(Int64 IdTransformation)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculateOfTransformations_LG_ReadAll");
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.Int64, IdTransformation);
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
        //trae el resource pedido
        internal IEnumerable<DbDataRecord> ReadById(Int64 IdTransformation, String IdLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculateOfTransformations_LG_ReadById");
            _db.AddInParameter(_dbCommand, "IdTransformation", DbType.Int64, IdTransformation);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, IdLanguage);
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
        internal void Create(Int64 IdTransformation, String IdLanguage, String Name, String Description)
        {
            Database _db = DatabaseFactory.CreateDatabase();
            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculateOfTransformations_LG_Create");
            _db.AddInParameter(_dbCommand, "IdTransformation", DbType.Int64, IdTransformation);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, IdLanguage);
            _db.AddInParameter(_dbCommand, "Name", DbType.String, Name);
            _db.AddInParameter(_dbCommand, "Description", DbType.String, Description);
            _db.ExecuteNonQuery(_dbCommand);
        }

        internal void Delete(Int64 IdTransformation, String IdLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculateOfTransformations_LG_Delete");
            _db.AddInParameter(_dbCommand, "IdTransformation", DbType.Int64, IdTransformation);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, IdLanguage);
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void Delete(Int64 IdTransformation)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculateOfTransformations_LG_DeleteByCalculateOfTransformations");
            _db.AddInParameter(_dbCommand, "IdTransformation", DbType.Int64, IdTransformation);
            _db.ExecuteNonQuery(_dbCommand);
        }

        internal void Update(Int64 IdTransformation, String IdLanguage, String Name, String Description)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculateOfTransformations_LG_Update");
            _db.AddInParameter(_dbCommand, "IdTransformation", DbType.Int64, IdTransformation);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, IdLanguage);
            _db.AddInParameter(_dbCommand, "Name", DbType.String, Name);
            _db.AddInParameter(_dbCommand, "Description", DbType.String, Description);
            _db.ExecuteNonQuery(_dbCommand);
        }
        #endregion


    }
}
