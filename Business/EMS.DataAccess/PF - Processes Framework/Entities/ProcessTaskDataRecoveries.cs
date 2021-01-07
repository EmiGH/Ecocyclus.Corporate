using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.PF.Entities
{
    internal class ProcessTaskDataRecoveries
    {
        internal ProcessTaskDataRecoveries() { }

        #region Write Functions
        internal void Create(Int64 idProcess, Int64 idTaskParent)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskDataRecoveries_Create");
            _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);
            _db.AddInParameter(_dbCommand, "IdTaskParent", DbType.Int64, idTaskParent);
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

        }
        internal void Delete(Int64 idProcess)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskDataRecoveries_Delete");
            _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        #endregion

    }
}