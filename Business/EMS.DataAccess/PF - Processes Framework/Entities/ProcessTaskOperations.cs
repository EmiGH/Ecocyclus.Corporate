using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.PF.Entities
{
    internal class ProcessTaskOperations
    {
        internal ProcessTaskOperations() { }

        #region Write Functions
        internal void Create(Int64 idProcess, String comment)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskOperations_Create");
            _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);
            _db.AddInParameter(_dbCommand, "Comment", DbType.String, comment);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

        }
        internal void Delete(Int64 idProcess)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskOperations_Delete");
            _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void Update(Int64 idProcess, String comment)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskOperations_Update");
            _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);
            _db.AddInParameter(_dbCommand, "Comment", DbType.String, comment);


            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        #endregion
    }
}