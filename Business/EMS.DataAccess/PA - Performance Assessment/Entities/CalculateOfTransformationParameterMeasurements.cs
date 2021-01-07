using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.PA.Entities
{
    internal class CalculateOfTransformationParameterMeasurements
    {
        internal CalculateOfTransformationParameterMeasurements() { }

        #region Write Functions
        internal void Create(String IdParameter, Int64 IdTransformation, Int64 IdMeasurementOperand)
        {
            Database _db = DatabaseFactory.CreateDatabase();
            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculateOfTransformationParameterMeasurements_Create");
            _db.AddInParameter(_dbCommand, "IdParameter", DbType.String, IdParameter);
            _db.AddInParameter(_dbCommand, "IdTransformation", DbType.Int64, IdTransformation);
            _db.AddInParameter(_dbCommand, "IdMeasurementOperand", DbType.Int64, IdMeasurementOperand);
            _db.ExecuteNonQuery(_dbCommand);
        }

        internal void Delete(String IdParameter, Int64 IdTransformation)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculateOfTransformationParameterMeasurements_Delete");
            _db.AddInParameter(_dbCommand, "IdParameter", DbType.String, IdParameter);
            _db.AddInParameter(_dbCommand, "IdTransformation", DbType.Int64, IdTransformation);
            _db.ExecuteNonQuery(_dbCommand);
        }

        internal void Delete(Int64 IdTransformation)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculateOfTransformationParameterMeasurements_DeleteByTransformation");
            _db.AddInParameter(_dbCommand, "IdTransformation", DbType.Int64, IdTransformation);
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void DeleteByMeasurement(Int64 IdMeasurementOperand)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculateOfTransformationParameterMeasurements_DeleteByMeasurement");
            _db.AddInParameter(_dbCommand, "IdMeasurementOperand", DbType.Int64, IdMeasurementOperand);
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void Update(String IdParameter, Int64 IdTransformation, Int64 IdMeasurementOperand)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculateOfTransformationParameterMeasurements_Update");
            _db.AddInParameter(_dbCommand, "IdParameter", DbType.String, IdParameter);
            _db.AddInParameter(_dbCommand, "IdTransformation", DbType.Int64, IdTransformation);
            _db.AddInParameter(_dbCommand, "IdMeasurementOperand", DbType.Int64, IdMeasurementOperand); 
            _db.ExecuteNonQuery(_dbCommand);
        }
        #endregion


    }
}
