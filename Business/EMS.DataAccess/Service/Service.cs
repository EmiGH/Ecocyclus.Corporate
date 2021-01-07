using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.Entities
{
    public class Service
    {
        public Service()
        {
            
        }

        /// <summary>
        /// Cambia un flag en PF_ProcessTasks y PF_ProcessTaskMeasurements 
        /// </summary>
        public void OverdueInTaskAndMeasurements()
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessTaskExceptions_forService");

            _db.ExecuteNonQuery(_dbCommand);

        }
    }
}
