using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.IA.Entities
{
    internal class Projects
    {
        internal Projects()
        {
           
        }

        #region Read Functions

        internal IEnumerable<DbDataRecord> ReadById(Int64 idProject)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("IA_Projects_ReadAll");
            _db.AddInParameter(_dbCommand, "IdClassification", DbType.Int64, idProject);
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

        internal IEnumerable<DbDataRecord> ReadAll(Int64 idProjectClassification)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("IA_Projects_ReadAll");
            _db.AddInParameter(_dbCommand, "IdClassification", DbType.Int64, idProjectClassification);
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