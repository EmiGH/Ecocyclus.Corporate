using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.NT.Entities
{
    internal class MailTransformationExternal
    {
        internal MailTransformationExternal() { }

        #region Read Functions

        internal IEnumerable<DbDataRecord> ReadByTransformation(Int64 IdTransformation)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("NT_MailTransformationExternal_ReadByTransformation");
            _db.AddInParameter(_dbCommand, "IdTransformation", DbType.Int64, IdTransformation);

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

        internal void Create(Int64 IdTransformation, String email)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("NT_MailTransformationExternal_Create");
            _db.AddInParameter(_dbCommand, "IdTransformation", DbType.Int64, IdTransformation); 
            _db.AddInParameter(_dbCommand, "Email", DbType.String, email);
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

        }

        internal void Delete(Int64 IdTransformation)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("NT_MailTransformationExternal_DeleteByTransformation");
            _db.AddInParameter(_dbCommand, "IdTransformation", DbType.Int64, IdTransformation);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        internal void Delete(Int64 IdTransformation, String email)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("NT_MailTransformationExternal_Delete");
            _db.AddInParameter(_dbCommand, "IdTransformation", DbType.Int64, IdTransformation);
            _db.AddInParameter(_dbCommand, "Email", DbType.String, email);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        internal void Update(Int64 IdTransformation, String email)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("NT_MailTransformationExternal_Update");
            _db.AddInParameter(_dbCommand, "IdTransformation", DbType.Int64, IdTransformation);
            _db.AddInParameter(_dbCommand, "Email", DbType.String, email);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        #endregion
    }
}
