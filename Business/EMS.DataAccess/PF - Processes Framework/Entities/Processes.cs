using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.PF.Entities
{
    internal class Processes
    {
        internal Processes() { }

        #region Read Functions
        internal IEnumerable<DbDataRecord> ReadById(Int64 idProcess, String idLanguage)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_Processes_ReadById");
                _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);

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
        internal IEnumerable<DbDataRecord> ReadByResource(Int64 idResource)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_Processes_ReadByResource");
            _db.AddInParameter(_dbCommand, "idResource", DbType.Int64, idResource);

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

        #region Read Functions
        internal Int64 Create(Int16 weight, Int32 orderNumber)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_Processes_Create");
            _db.AddInParameter(_dbCommand, "Weight", DbType.Int16, weight);
            _db.AddInParameter(_dbCommand, "OrderNumber", DbType.Int16, orderNumber);

            //Parámetro de salida
            _db.AddOutParameter(_dbCommand, "IdProcess", DbType.Int64, 18);
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
            //Retorna el identificador
            return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdProcess"));

        }
        internal void Update(Int64 idProcess, Int16 weight, Int32 orderNumber)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_Processes_Update");
            _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);
            _db.AddInParameter(_dbCommand, "Weight", DbType.Int16, weight);
            _db.AddInParameter(_dbCommand, "OrderNumber", DbType.Int16, orderNumber);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

        }
        internal void Delete(Int64 idProcess)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_Processes_Delete");
            _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        #endregion
    }
}
