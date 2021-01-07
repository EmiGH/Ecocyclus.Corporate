using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.PA.Entities
{
    internal class CalculateOfTransformations
    {
        internal CalculateOfTransformations() { }

        #region Read Functions
        //Trae todos los resources 
        internal IEnumerable<DbDataRecord> ReadAll(String IdLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculateOfTransformations_ReadAll");
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, IdLanguage);
            _dbCommand.CommandTimeout = 0;
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
        internal IEnumerable<DbDataRecord> ReadAllRoots(String IdLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculateOfTransformations_ReadAllRoots");
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, IdLanguage);
            _dbCommand.CommandTimeout = 0;
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
      
        internal IEnumerable<DbDataRecord> ReadWhitErrors(String IdLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculateOfTransformations_ReadWhitErrors");
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, IdLanguage);
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
        //trae el resource pedido
        internal IEnumerable<DbDataRecord> ReadById(Int64 IdTransformation, String IdLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculateOfTransformations_ReadById");
            _db.AddInParameter(_dbCommand, "IdTransformation", DbType.Int64, IdTransformation);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, IdLanguage);

            _dbCommand.CommandTimeout = 0;
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
        internal IEnumerable<DbDataRecord> ReadTransformationParameter(Int64 IdTransformation, String IdLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculateOfTransformations_ReadTransformationParameter");
            _db.AddInParameter(_dbCommand, "IdTransformation", DbType.Int64, IdTransformation);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, IdLanguage);

            _dbCommand.CommandTimeout = 0;
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
        internal IEnumerable<DbDataRecord> ReadByMeasurement(Int64 IdMeasurement, String IdLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculateOfTransformations_ReadByMeasurement");
            _db.AddInParameter(_dbCommand, "IdMeasurement", DbType.Int64, IdMeasurement);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, IdLanguage);
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
        internal IEnumerable<DbDataRecord> ReadByMeasurementAsParameter(Int64 IdMeasurement, String IdLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculateOfTransformations_ReadByMeasurementAsParameter");
            _db.AddInParameter(_dbCommand, "IdMeasurement", DbType.Int64, IdMeasurement);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, IdLanguage);
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
        internal IEnumerable<DbDataRecord> ReadByIndicator(Int64 IdIndicator, String IdLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculateOfTransformations_ReadByIndicator");
            _db.AddInParameter(_dbCommand, "IdIndicator", DbType.Int64, IdIndicator);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, IdLanguage);
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
        internal IEnumerable<DbDataRecord> ReadByTransformationAsParameter(Int64 IdTransformation, String IdLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculateOfTransformations_ReadByTransformationAsParameter");
            _db.AddInParameter(_dbCommand, "IdTransformation", DbType.Int64, IdTransformation);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, IdLanguage);
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
        internal IEnumerable<DbDataRecord> ReadByConstant(Int64 IdConstant, String IdLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculateOfTransformations_ReadBycConstant");
            _db.AddInParameter(_dbCommand, "IdConstant", DbType.Int64, IdConstant);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, IdLanguage);
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
        internal IEnumerable<DbDataRecord> ReadByProcess(Int64 IdProcess, String IdLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculateOfTransformations_ReadByProcess");
            _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, IdProcess);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, IdLanguage);
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
        internal IEnumerable<DbDataRecord> ReadByTransformation(Int64 IdTransformationTransformer, String IdLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculateOfTransformations_ReadByTransformation");
            _db.AddInParameter(_dbCommand, "IdTransformationTransformer", DbType.Int64, IdTransformationTransformer);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, IdLanguage);
            _dbCommand.CommandTimeout = 0;
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
        internal IEnumerable<DbDataRecord> ReadMaxMinDate(Int64 IdTransformation)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculateOfTransformations_ReadMaxMinDate");
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
        internal IEnumerable<DbDataRecord> ReadOperateValue(Int64 IdTransformation, Boolean isCumulative, DateTime startDate, DateTime endDate)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculateOfTransformations_ReadOperateValue");
            _db.AddInParameter(_dbCommand, "IdTransformation", DbType.Int64, IdTransformation);
            _db.AddInParameter(_dbCommand, "IsCumulative", DbType.Boolean, isCumulative);
            _db.AddInParameter(_dbCommand, "startDate", DbType.DateTime, startDate);
            _db.AddInParameter(_dbCommand, "endDate", DbType.DateTime, endDate);
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

        internal IEnumerable<DbDataRecord> WasteReadValuesAndYears(Int64 idTransformation, DateTime startDate, DateTime endDate)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculateOfTransformationWaste_ReadValuesAndYears");
            _db.AddInParameter(_dbCommand, "IdTransformation", DbType.Int64, idTransformation);
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
        internal IEnumerable<DbDataRecord> WasteReadValuesAndYearsForMonth(Int64 idTransformation, DateTime startDate, DateTime endDate)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculateOfTransformationWaste_ReadValuesAndYearsForMonth");
            _db.AddInParameter(_dbCommand, "IdTransformation", DbType.Int64, idTransformation);
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
        internal IEnumerable<DbDataRecord> WasteReadYears(Int64 idTransformation, DateTime date)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculateOfTransformationWaste_ReadYears");
            _db.AddInParameter(_dbCommand, "IdTransformation", DbType.Int64, idTransformation);
            _db.AddInParameter(_dbCommand, "Date", DbType.DateTime, date);
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
        internal Int64 Create(Int64 IdMeasurementTransformer, Int64 IdTransformationTransformer, Int64 IdIndicator, String Formula, Int64 IdMeasurementUnit, Int64 IdProcess, Int64 IdMeasurementOrigin, Int64 IdActivity)
        {
            Database _db = DatabaseFactory.CreateDatabase();
            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculateOfTransformations_Create");            
            _db.AddInParameter(_dbCommand, "IdMeasurementTransformer", DbType.Int64, Common.Common.CastValueToNull(IdMeasurementTransformer, 0));
            _db.AddInParameter(_dbCommand, "IdTransformationTransformer", DbType.Int64, Common.Common.CastValueToNull(IdTransformationTransformer, 0));
            _db.AddInParameter(_dbCommand, "IdIndicator", DbType.Int64, IdIndicator);
            _db.AddInParameter(_dbCommand, "Formula", DbType.String, Formula);
            _db.AddInParameter(_dbCommand, "IdMeasurementUnit", DbType.Int64, IdMeasurementUnit);
            _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, IdProcess);
            _db.AddInParameter(_dbCommand, "IdMeasurementOrigin", DbType.Int64, IdMeasurementOrigin);
            _db.AddInParameter(_dbCommand, "IdActivity", DbType.Int64, Common.Common.CastValueToNull(IdActivity, 0));

            _db.AddOutParameter(_dbCommand, "IdTransformation", DbType.Int64, 18);

            _db.ExecuteNonQuery(_dbCommand);
            //Retorna el identificador
            return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdTransformation"));
        }

        internal void Delete(Int64 IdTransformation)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculateOfTransformations_Delete");
            _db.AddInParameter(_dbCommand, "IdTransformation", DbType.Int64, IdTransformation);
            _db.ExecuteNonQuery(_dbCommand);
        }

        internal void Update(Int64 IdTransformation, Int64 IdMeasurementTransformer, Int64 IdTransformationTransformer, Int64 IdIndicator, String Formula, Int64 IdMeasurementUnit, Int64 IdProcess, Int64 IdMeasurementOrigin, Int64 IdActivity)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculateOfTransformations_Update");
            _db.AddInParameter(_dbCommand, "IdTransformation", DbType.Int64, IdTransformation);
            _db.AddInParameter(_dbCommand, "IdMeasurementTransformer", DbType.Int64, Common.Common.CastValueToNull(IdMeasurementTransformer, 0));
            _db.AddInParameter(_dbCommand, "IdTransformationTransformer", DbType.Int64, Common.Common.CastValueToNull(IdTransformationTransformer, 0));
            _db.AddInParameter(_dbCommand, "IdIndicator", DbType.Int64, IdIndicator);
            _db.AddInParameter(_dbCommand, "Formula", DbType.String, Formula);
            _db.AddInParameter(_dbCommand, "IdMeasurementUnit", DbType.Int64, IdMeasurementUnit);
            _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, IdProcess);
            _db.AddInParameter(_dbCommand, "IdMeasurementOrigin", DbType.Int64, IdMeasurementOrigin);
            _db.AddInParameter(_dbCommand, "IdActivity", DbType.Int64, Common.Common.CastValueToNull(IdActivity, 0));

            _db.ExecuteNonQuery(_dbCommand);
        }
        #endregion


    }
}
