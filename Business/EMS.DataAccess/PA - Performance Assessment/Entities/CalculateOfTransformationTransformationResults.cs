using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.PA.Entities
{
    internal class CalculateOfTransformationTransformationResults
    {
        internal CalculateOfTransformationTransformationResults() { }

        #region Read Functions
        //Trae todos los resources 
        internal IEnumerable<DbDataRecord> TansformValues(Int64 IdTransformationTransformer, Int64 IdTransformation)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculateOfTransformationTransformationResults_TransformValues");
            _db.AddInParameter(_dbCommand, "IdTransformationTransformer", DbType.Int64, IdTransformationTransformer);
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
        internal IEnumerable<DbDataRecord> ReadByIndicator(Int64 IdTransformation, Int64 IdIndicatorColumnGas, DateTime StartDate, DateTime EndDate)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculateOfTransformationTransformationResults_ReadByIndicator");
            _db.AddInParameter(_dbCommand, "IdTransformation", DbType.Int64, IdTransformation);
            _db.AddInParameter(_dbCommand, "IdIndicatorColumnGas", DbType.Int64, IdIndicatorColumnGas);
            _db.AddInParameter(_dbCommand, "StartDate", DbType.DateTime, StartDate);
            _db.AddInParameter(_dbCommand, "EndDate", DbType.DateTime, EndDate);
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
        internal IEnumerable<DbDataRecord> ReadSeries(Int64 IdTransformation, DateTime? startDate, DateTime? endDate)
        {
            Database _db = DatabaseFactory.CreateDatabase();
            DbCommand _dbCommand;

            _dbCommand = _db.GetStoredProcCommand("PA_CalculateOfTransformationTransformationResults_Series");
            _db.AddInParameter(_dbCommand, "IdTransformation", DbType.Int64, IdTransformation);
            _db.AddInParameter(_dbCommand, "StartDate", DbType.DateTime, startDate);
            _db.AddInParameter(_dbCommand, "EndDate", DbType.DateTime, endDate);

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
        internal void Create(Int64 IdTransformation, Int64 IdTransformationTransformer, Double TransformationValue, DateTime TransformationDate, DateTime StartDate, DateTime EndDate, Double minuteValue)
        {
            Database _db = DatabaseFactory.CreateDatabase();
            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculateOfTransformationTransformationResults_Create");

            _db.AddInParameter(_dbCommand, "IdTransformation", DbType.Int64, IdTransformation);
            _db.AddInParameter(_dbCommand, "IdTransformationTransformer", DbType.Int64, IdTransformationTransformer);
            _db.AddInParameter(_dbCommand, "TransformationValue", DbType.Double, TransformationValue);
            _db.AddInParameter(_dbCommand, "TransformationDate", DbType.DateTime, TransformationDate);
            _db.AddInParameter(_dbCommand, "StartDate", DbType.DateTime, StartDate);
            _db.AddInParameter(_dbCommand, "EndDate", DbType.DateTime, EndDate);
            _db.AddInParameter(_dbCommand, "minuteValue", DbType.Double, minuteValue);

            _db.ExecuteNonQuery(_dbCommand);
        }

        internal void Delete(Int64 IdTransformation)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculateOfTransformationTransformationResults_Delete");
            _db.AddInParameter(_dbCommand, "IdTransformation", DbType.Int64, IdTransformation);
            _db.ExecuteNonQuery(_dbCommand);
        }

 
        #endregion


    }
}
