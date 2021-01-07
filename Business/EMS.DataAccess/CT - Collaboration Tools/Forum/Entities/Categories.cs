
// Generated by Pnyx Generation tool at :04/05/2009 17:17:45
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.CT.Entities
{
    internal class ForumCategories
    {
        internal ForumCategories() { }

        #region Read Functions
        //Trae todos los resources 
        internal IEnumerable<DbDataRecord> ReadAll(String IdLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("CT_ForumCategories_ReadAll");
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
        //trae el resource pedido
        internal IEnumerable<DbDataRecord> ReadById(Int64 IdCategory,String IdLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("CT_ForumCategories_ReadById");
            _db.AddInParameter(_dbCommand, "IdCategory", DbType.Int64, IdCategory);
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
        #region FK's
        #endregion
        #region Write Functions
        #endregion


    }
}

