using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.IA.Entities
{
    internal class ExceptionHistories
    {
        internal ExceptionHistories()
        {
        }

        #region Read Functions

     
        #endregion

        #region Write Functions


     

        internal void Delete(Int64 idException)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("IA_ExceptionHistories_DeleteByException");
            _db.AddInParameter(_dbCommand, "idException", DbType.Int64, idException);
            //Parámetro de salida
            _db.ExecuteNonQuery(_dbCommand);

        }

       



        #endregion
    }
}
