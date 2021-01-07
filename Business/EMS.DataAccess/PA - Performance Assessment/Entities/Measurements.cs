using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.PA.Entities
{
    public class Measurements
    {
        internal Measurements()
        { }

        #region Public Porperties

        public enum AggregateType
        { Sum, SumCummulative, Avg, StdDev, StdDevP, Var, VarP }
        public enum GroupingType
        { Hour, Day, Month, Year }

        #endregion

        #region Read Functions

        internal IEnumerable<DbDataRecord> ReadAll(String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Measurements_ReadAll");
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
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
        internal IEnumerable<DbDataRecord> ReadByIndicator(Int64 idIndicator, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Measurements_ReadByIndicator");
            _db.AddInParameter(_dbCommand, "IdIndicator", DbType.Int64, idIndicator);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
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
        internal IEnumerable<DbDataRecord> ReadById(Int64 idMeasurement, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Measurements_ReadById");
            _db.AddInParameter(_dbCommand, "IdMeasurement", DbType.Int64, idMeasurement);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
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

        internal MeasurementStatistics ReadStatistics(Int64 idMeasurement, DateTime? startDate, DateTime? endDate)
        {
            Database _db = DatabaseFactory.CreateDatabase();
            MeasurementStatistics _statistics;

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Measurements_Statistics");
            _db.AddInParameter(_dbCommand, "IdMeasurement", DbType.Int64, idMeasurement);
            _db.AddInParameter(_dbCommand, "StartDate", DbType.DateTime, startDate); 
            _db.AddInParameter(_dbCommand, "EndDate", DbType.DateTime, endDate);
        
            _db.AddOutParameter(_dbCommand, "FirstDate", DbType.DateTime, 8);
            _db.AddOutParameter(_dbCommand, "LastDate", DbType.DateTime, 8);
            _db.AddOutParameter(_dbCommand, "FirstValue", DbType.Double, 20);
            _db.AddOutParameter(_dbCommand, "LastValue", DbType.Double, 20);
            _db.AddOutParameter(_dbCommand, "AvgValue", DbType.Double, 20);
            _db.AddOutParameter(_dbCommand, "SumValue", DbType.Double, 20);
            _db.AddOutParameter(_dbCommand, "MaxValue", DbType.Double, 20);
            _db.AddOutParameter(_dbCommand, "MinValue", DbType.Double, 20);
            _db.AddOutParameter(_dbCommand, "CountValue", DbType.Double, 18);
            _db.AddOutParameter(_dbCommand, "StdDevValue", DbType.Double, 20);
            _db.AddOutParameter(_dbCommand, "StdDevPValue", DbType.Double, 20);
            _db.AddOutParameter(_dbCommand, "VarValue", DbType.Double, 20);
            _db.AddOutParameter(_dbCommand, "VarPValue", DbType.Double, 20);

            _db.ExecuteNonQuery(_dbCommand);

            _statistics = new MeasurementStatistics();
            _statistics.FirstDate = Convert.ToDateTime(Common.Common.CastNullValues(_db.GetParameterValue(_dbCommand, "FirstDate"), DateTime.MinValue));
            _statistics.LastDate = Convert.ToDateTime(Common.Common.CastNullValues(_db.GetParameterValue(_dbCommand, "LastDate"), DateTime.MinValue));
            _statistics.FirstValue = Convert.ToDouble(Common.Common.CastNullValues(_db.GetParameterValue(_dbCommand, "FirstValue"),0.0));
            _statistics.LastValue = Convert.ToDouble(Common.Common.CastNullValues(_db.GetParameterValue(_dbCommand, "LastValue"),0.0));
            _statistics.MaxValue = Convert.ToDouble(Common.Common.CastNullValues(_db.GetParameterValue(_dbCommand, "MaxValue"),0.0));
            _statistics.MinValue = Convert.ToDouble(Common.Common.CastNullValues(_db.GetParameterValue(_dbCommand, "MinValue"),0.0));
            _statistics.AvgValue = Convert.ToDouble(Common.Common.CastNullValues(_db.GetParameterValue(_dbCommand, "AvgValue"),0.0));
            _statistics.CountValue = Convert.ToDouble(Common.Common.CastNullValues(_db.GetParameterValue(_dbCommand, "CountValue"), 0));
            _statistics.SumValue = Convert.ToDouble(Common.Common.CastNullValues(_db.GetParameterValue(_dbCommand, "SumValue"), 0.0));
            _statistics.StdDevValue = Convert.ToDouble(Common.Common.CastNullValues(_db.GetParameterValue(_dbCommand, "StdDevValue"),0.0));
            _statistics.StdDevPValue = Convert.ToDouble(Common.Common.CastNullValues(_db.GetParameterValue(_dbCommand, "StdDevPValue"),0.0));
            _statistics.VarValue = Convert.ToDouble(Common.Common.CastNullValues(_db.GetParameterValue(_dbCommand, "VarValue"),0.0));
            _statistics.VarPValue = Convert.ToDouble(Common.Common.CastNullValues(_db.GetParameterValue(_dbCommand, "VarPValue"),0.0));

            return _statistics;
        }
        internal IEnumerable<DbDataRecord> ReadSeries(Int64 idMeasurement, DateTime? startDate, DateTime? endDate)
        {
            Database _db = DatabaseFactory.CreateDatabase();
            DbCommand _dbCommand;

            _dbCommand = _db.GetStoredProcCommand("PA_Measurements_Series");
            _db.AddInParameter(_dbCommand, "IdMeasurement", DbType.Int64, idMeasurement);
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
        internal IEnumerable<DbDataRecord> ReadSeries(Int64 idMeasurement, DateTime? startDate, DateTime? endDate, GroupingType groupingType, AggregateType aggregateType)
        {
            Database _db = DatabaseFactory.CreateDatabase();
            DbCommand _dbCommand;
            String _dbProcedureName="";

            switch (groupingType)
            {
                case GroupingType.Hour:
                    switch (aggregateType)
                    {
                        case AggregateType.Avg:
                            _dbProcedureName = "PA_Measurements_SeriesByHourAvg";
                            break;
                        case AggregateType.Sum:
                            _dbProcedureName = "PA_Measurements_SeriesByHourSum";
                            break;
                        case AggregateType.SumCummulative:
                            _dbProcedureName = "PA_Measurements_SeriesByHourSumCummulative";
                            break;
                        case AggregateType.StdDev:
                            _dbProcedureName = "PA_Measurements_SeriesByHourStdDev";
                            break;
                        case AggregateType.StdDevP:
                            _dbProcedureName = "PA_Measurements_SeriesByHourStdDevP";
                            break;
                        case AggregateType.Var:
                            _dbProcedureName = "PA_Measurements_SeriesByHourVar";
                            break;
                        case AggregateType.VarP:
                            _dbProcedureName = "PA_Measurements_SeriesByHourVarP";
                            break;
                    }
                    break;
                case GroupingType.Day:
                    switch (aggregateType)
                    {
                        case AggregateType.Avg:
                            _dbProcedureName = "PA_Measurements_SeriesByDayAvg";
                            break;
                        case AggregateType.Sum:
                            _dbProcedureName = "PA_Measurements_SeriesByDaySum";
                            break;
                        case AggregateType.SumCummulative:
                            _dbProcedureName = "PA_Measurements_SeriesByDaySumCummulative";
                            break;
                        case AggregateType.StdDev:
                            _dbProcedureName = "PA_Measurements_SeriesByDayStdDev";
                            break;
                        case AggregateType.StdDevP:
                            _dbProcedureName = "PA_Measurements_SeriesByDayStdDevP";
                            break;
                        case AggregateType.Var:
                            _dbProcedureName = "PA_Measurements_SeriesByDayVar";
                            break;
                        case AggregateType.VarP:
                            _dbProcedureName = "PA_Measurements_SeriesByDayVarP";
                            break;
                    }
                    break;
                case GroupingType.Month:
                    switch (aggregateType)
                    {
                        case AggregateType.Avg:
                            _dbProcedureName = "PA_Measurements_SeriesByMonthAvg";
                            break;
                        case AggregateType.Sum:
                            _dbProcedureName = "PA_Measurements_SeriesByMonthSum";
                            break;
                        case AggregateType.SumCummulative:
                            _dbProcedureName = "PA_Measurements_SeriesByMonthSumCummulative";
                            break;
                        case AggregateType.StdDev:
                            _dbProcedureName = "PA_Measurements_SeriesByMonthStdDev";
                            break;
                        case AggregateType.StdDevP:
                            _dbProcedureName = "PA_Measurements_SeriesByMonthStdDevP";
                            break;
                        case AggregateType.Var:
                            _dbProcedureName = "PA_Measurements_SeriesByMonthVar";
                            break;
                        case AggregateType.VarP:
                            _dbProcedureName = "PA_Measurements_SeriesByMonthVarP";
                            break;
                    }
                    break;
                case GroupingType.Year:
                    switch (aggregateType)
                    {
                        case AggregateType.Avg:
                            _dbProcedureName = "PA_Measurements_SeriesByYearAvg";
                            break;
                        case AggregateType.Sum:
                            _dbProcedureName = "PA_Measurements_SeriesByYearSum";
                            break;
                        case AggregateType.SumCummulative:
                            _dbProcedureName = "PA_Measurements_SeriesByYearSumCummulative";
                            break;
                        case AggregateType.StdDev:
                            _dbProcedureName = "PA_Measurements_SeriesByYearStdDev";
                            break;
                        case AggregateType.StdDevP:
                            _dbProcedureName = "PA_Measurements_SeriesByYearStdDevP";
                            break;
                        case AggregateType.Var:
                            _dbProcedureName = "PA_Measurements_SeriesByYearVar";
                            break;
                        case AggregateType.VarP:
                            _dbProcedureName = "PA_Measurements_SeriesByYearVarP";
                            break;
                    }
                    break;
            }
           
            _dbCommand = _db.GetStoredProcCommand(_dbProcedureName);
            _db.AddInParameter(_dbCommand, "IdMeasurement", DbType.Int64, idMeasurement);
            _db.AddInParameter(_dbCommand, "StartDate", DbType.DateTime, startDate);
            _db.AddInParameter(_dbCommand, "EndDate", DbType.DateTime, endDate);

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
        internal IEnumerable<DbDataRecord> ReadMaxMinDate(Int64 idMeasurement)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Measurements_ReadMaxMinDate");
            _db.AddInParameter(_dbCommand, "IdMeasurement", DbType.Int64, idMeasurement);
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
        internal IEnumerable<DbDataRecord> ReadOperateValue(Int64 idMeasurement, Boolean isCumulative, DateTime startDate, DateTime endDate)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Measurements_ReadOperateValue");
            _db.AddInParameter(_dbCommand, "IdMeasurement", DbType.Int64, idMeasurement);
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
        internal IEnumerable<DbDataRecord> ReadByActivityAndProcess(Int64 IdActivity, Int64 IdProcess, String IdLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Measurements_ReadByActivityAndProcess");
            _db.AddInParameter(_dbCommand, "IdActivity", DbType.Int64, IdActivity);
            _db.AddInParameter(_dbCommand, "IdProcess ", DbType.Int64, IdProcess);
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

        internal IEnumerable<DbDataRecord> ReadByFacilityForScope(Int64 idScope, Int64 IdFacility, Int64 IdActivity, String IdLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Measurements_ReadByFacilityForScope");
            _db.AddInParameter(_dbCommand, "idScope", DbType.Int64, idScope);
            _db.AddInParameter(_dbCommand, "IdFacility", DbType.Int64, IdFacility);
            _db.AddInParameter(_dbCommand, "IdActivity", DbType.Int64, IdActivity);
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

        #endregion


        #region Write Functions


        internal Int64 Create(Int64 idDevice, Int64 idIndicator, Int64 idTimeUnitFrequency, Int32 frequency, 
            Int64 idMeasurementUnit, Boolean isRegressive, Boolean isRelevant, String source, String frequencyAtSource, 
            Decimal uncertainty, Int64 idQuality, Int64 idMethodology)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Measurements_Create");
            _db.AddInParameter(_dbCommand, "IdMeasurementDevice", DbType.Int64, Common.Common.CastValueToNull(idDevice, 0));
            _db.AddInParameter(_dbCommand, "IdIndicator", DbType.Int64, idIndicator);
            _db.AddInParameter(_dbCommand, "IdTimeUnitFrequency", DbType.Int64, idTimeUnitFrequency);
            _db.AddInParameter(_dbCommand, "Frequency", DbType.Int64, frequency);
            _db.AddInParameter(_dbCommand, "IdMeasurementUnit", DbType.Int64, idMeasurementUnit);
            _db.AddInParameter(_dbCommand, "IsRegressive", DbType.Boolean, isRegressive);
            _db.AddInParameter(_dbCommand, "IsRelevant", DbType.Boolean, isRelevant);
            _db.AddInParameter(_dbCommand, "Source", DbType.String, source);
            _db.AddInParameter(_dbCommand, "FrequencyAtSource", DbType.String, frequencyAtSource);
            _db.AddInParameter(_dbCommand, "Uncertainty", DbType.Decimal, Common.Common.CastValueToNull(uncertainty, 0));

            _db.AddInParameter(_dbCommand, "IdQuality", DbType.Int64, Common.Common.CastValueToNull(idQuality, 0));
            _db.AddInParameter(_dbCommand, "IdMethodology", DbType.Int64, Common.Common.CastValueToNull(idMethodology, 0));
            //Parámetro de salida
            _db.AddOutParameter(_dbCommand, "IdMeasurement", DbType.Int64, 18);
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdMeasurement"));
        }
        internal void Delete(Int64 idMeasurement)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Measurements_Delete");
            _db.AddInParameter(_dbCommand, "IdMeasurement", DbType.Int64, idMeasurement);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void Update(Int64 idMeasurement, Int64 idMeasurementDevice, Int64 idIndicator, Int64 idMeasurementUnit, 
            Int64 idTimeUnitFrequency, Int32 frequency, Boolean isRelevant, String source, String frequencyAtSource,
            Decimal uncertainty, Int64 idQuality, Int64 idMethodology)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Measurements_Update");
            _db.AddInParameter(_dbCommand, "IdMeasurement", DbType.Int64, idMeasurement);
            _db.AddInParameter(_dbCommand, "IdMeasurementDevice", DbType.Int64, Common.Common.CastValueToNull(idMeasurementDevice, 0));
            _db.AddInParameter(_dbCommand, "IdIndicator", DbType.Int64, idIndicator);
            _db.AddInParameter(_dbCommand, "IdTimeUnitFrequency", DbType.Int64, idTimeUnitFrequency);
            _db.AddInParameter(_dbCommand, "Frequency", DbType.Int64, frequency);
            _db.AddInParameter(_dbCommand, "IdMeasurementUnit", DbType.Int64, idMeasurementUnit);
            _db.AddInParameter(_dbCommand, "IsRelevant", DbType.Boolean, isRelevant);
            _db.AddInParameter(_dbCommand, "Source", DbType.String, source);
            _db.AddInParameter(_dbCommand, "FrequencyAtSource", DbType.String, frequencyAtSource);
            _db.AddInParameter(_dbCommand, "Uncertainty", DbType.Decimal, Common.Common.CastValueToNull(uncertainty, 0));

            _db.AddInParameter(_dbCommand, "IdQuality", DbType.Int64, Common.Common.CastValueToNull(idQuality, 0));
            _db.AddInParameter(_dbCommand, "IdMethodology", DbType.Int64, Common.Common.CastValueToNull(idMethodology, 0));

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        #endregion
    }

    public struct MeasurementStatistics
    {
        private DateTime _FirstDate;
        private DateTime _LastDate;
        private Double _FirstValue;
        private Double _LastValue;
        private Double _AvgValue;
        private Double _CountValue;
        private Double _SumValue;
        private Double _MaxValue;
        private Double _MinValue;
        private Double _StdDevValue;
        private Double _StdDevPValue;
        private Double _VarValue;
        private Double _VarPValue;

        public DateTime FirstDate
        {
            get { return _FirstDate; }
            set { _FirstDate = value; }
        }
        public DateTime LastDate
        {
            get { return _LastDate; }
            set { _LastDate = value; }
        }
        public Double FirstValue
        {
            get { return _FirstValue; }
            set { _FirstValue = value; }
        }
        public Double LastValue
        {
            get { return _LastValue; }
            set { _LastValue = value; }
        }
        public Double SumValue
        {
            get { return _SumValue; }
            set { _SumValue = value; }
        }
        public Double AvgValue
        {
            get { return _AvgValue; }
            set { _AvgValue = value; }
        }
        public Double CountValue
        {
            get { return _CountValue; }
            set { _CountValue = value; }
        }
        public Double MaxValue
        {
            get { return _MaxValue; }
            set { _MaxValue = value; }
        }
        public Double MinValue
        {
            get { return _MinValue; }
            set { _MinValue = value; }
        }
        public Double StdDevValue
        {
            get { return _StdDevValue; }
            set { _StdDevValue = value; }
        }
        public Double StdDevPValue
        {
            get { return _StdDevPValue; }
            set { _StdDevPValue = value; }
        }
        public Double VarValue
        {
            get { return _VarValue; }
            set { _VarValue = value; }
        }
        public Double VarPValue
        {
            get { return _VarPValue; }
            set { _VarPValue = value; }
        }

    }
    
}
