using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.PA.Entities
{
    internal class CalculateOfTransformationParameterTransformations
    {
        internal CalculateOfTransformationParameterTransformations() { }

     
        #region Write Functions
        internal void Create(String IdParameter, Int64 IdTransformation, Int64 IdTransformationOperand)
        {
            Database _db = DatabaseFactory.CreateDatabase();
            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculateOfTransformationParameterTransformations_Create");
            _db.AddInParameter(_dbCommand, "IdParameter", DbType.String, IdParameter);
            _db.AddInParameter(_dbCommand, "IdTransformation", DbType.Int64, IdTransformation);
            _db.AddInParameter(_dbCommand, "IdTransformationOperand", DbType.Int64, IdTransformationOperand);
            _db.ExecuteNonQuery(_dbCommand);
        }

        internal void Delete(String IdParameter, Int64 IdTransformation)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculateOfTransformationParameterTransformations_Delete");
            _db.AddInParameter(_dbCommand, "IdParameter", DbType.String, IdParameter);
            _db.AddInParameter(_dbCommand, "IdTransformation", DbType.Int64, IdTransformation);
            _db.ExecuteNonQuery(_dbCommand);
        }

        internal void Delete(Int64 IdTransformation)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculateOfTransformationParameterTransformations_DeleteByTransformation");
            _db.AddInParameter(_dbCommand, "IdTransformation", DbType.Int64, IdTransformation);
            _db.ExecuteNonQuery(_dbCommand);
        }

        internal void DeleteAsParameter(Int64 IdTransformation)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculateOfTransformationParameterTransformations_DeleteAsParameter");
            _db.AddInParameter(_dbCommand, "IdTransformation", DbType.Int64, IdTransformation);
            _db.ExecuteNonQuery(_dbCommand);
        }

        internal void Update(String IdParameter, Int64 IdTransformation, Int64 IdTransformationOperand)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculateOfTransformationParameterTransformations_Update");
            _db.AddInParameter(_dbCommand, "IdParameter", DbType.String, IdParameter);
            _db.AddInParameter(_dbCommand, "IdTransformation", DbType.Int64, IdTransformation);
            _db.AddInParameter(_dbCommand, "IdTransformationOperand", DbType.Int64, IdTransformationOperand); 
            _db.ExecuteNonQuery(_dbCommand);
        }
        #endregion


    }
}
