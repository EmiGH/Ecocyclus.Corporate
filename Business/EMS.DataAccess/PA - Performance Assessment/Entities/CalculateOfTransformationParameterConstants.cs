using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.PA.Entities
{
    internal class CalculateOfTransformationParameterConstants
    {
        internal CalculateOfTransformationParameterConstants() { }
    
        #region Write Functions
        internal void Create(String IdParameter, Int64 IdTransformation, Int64 IdConstantOperand)
        {
            Database _db = DatabaseFactory.CreateDatabase();
            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculateOfTransformationParameterConstants_Create");
            _db.AddInParameter(_dbCommand, "IdParameter", DbType.String, IdParameter);
            _db.AddInParameter(_dbCommand, "IdTransformation", DbType.Int64, IdTransformation);
            _db.AddInParameter(_dbCommand, "IdConstantOperand", DbType.Int64, IdConstantOperand);
            _db.ExecuteNonQuery(_dbCommand);
        }

        internal void Delete(String IdParameter, Int64 IdTransformation)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculateOfTransformationParameterConstants_Delete");
            _db.AddInParameter(_dbCommand, "IdParameter", DbType.String, IdParameter);
            _db.AddInParameter(_dbCommand, "IdTransformation", DbType.Int64, IdTransformation);
            _db.ExecuteNonQuery(_dbCommand);
        }

        internal void Delete(Int64 IdTransformation)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculateOfTransformationParameterConstants_DeleteByTransformation");
            _db.AddInParameter(_dbCommand, "IdTransformation", DbType.Int64, IdTransformation);
            _db.ExecuteNonQuery(_dbCommand);
        }

        internal void Update(String IdParameter, Int64 IdTransformation, Int64 IdConstantOperand)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculateOfTransformationParameterConstants_Update");
            _db.AddInParameter(_dbCommand, "IdParameter", DbType.String, IdParameter);
            _db.AddInParameter(_dbCommand, "IdTransformation", DbType.Int64, IdTransformation);
            _db.AddInParameter(_dbCommand, "IdConstantOperand", DbType.Int64, IdConstantOperand);
            _db.ExecuteNonQuery(_dbCommand);
        }
        #endregion


    }
}
