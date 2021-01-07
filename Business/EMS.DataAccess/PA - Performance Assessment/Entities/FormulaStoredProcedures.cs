using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.PA.Entities
{
    internal class FormulaStoredProcedures
    {
        internal FormulaStoredProcedures()
        { }

        #region Read Functions
            internal IEnumerable<DbDataRecord> ReadAll(String storedProcedureName)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_FormulaStoredProcedures_GetAll");
                _db.AddInParameter(_dbCommand, "StoredProcedureName", DbType.String, storedProcedureName);
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
