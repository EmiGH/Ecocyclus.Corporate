using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess
{
    public class Temporal
    {
        public Temporal()
        { }

        public static PA.Entities.MeasurementStatistics ReadStatistics(DateTime? startDate, DateTime? endDate)
        {
            Database _db = DatabaseFactory.CreateDatabase();
            PA.Entities.MeasurementStatistics _statistics;

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Calculation_Hydroelectric_Statistics");
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
            _db.AddOutParameter(_dbCommand, "CountValue", DbType.Int64, 18);
            _db.AddOutParameter(_dbCommand, "StdDevValue", DbType.Double, 20);
            _db.AddOutParameter(_dbCommand, "StdDevPValue", DbType.Double, 20);
            _db.AddOutParameter(_dbCommand, "VarValue", DbType.Double, 20);
            _db.AddOutParameter(_dbCommand, "VarPValue", DbType.Double, 20);

            _db.ExecuteNonQuery(_dbCommand);

            _statistics = new PA.Entities.MeasurementStatistics();
            _statistics.FirstDate = Convert.ToDateTime(Common.Common.CastNullValues(_db.GetParameterValue(_dbCommand, "FirstDate"), DateTime.MinValue));
            _statistics.LastDate = Convert.ToDateTime(Common.Common.CastNullValues(_db.GetParameterValue(_dbCommand, "LastDate"), DateTime.MinValue));
            _statistics.FirstValue = Convert.ToDouble(Common.Common.CastNullValues(_db.GetParameterValue(_dbCommand, "FirstValue"), 0.0));
            _statistics.LastValue = Convert.ToDouble(Common.Common.CastNullValues(_db.GetParameterValue(_dbCommand, "LastValue"), 0.0));
            _statistics.MaxValue = Convert.ToDouble(Common.Common.CastNullValues(_db.GetParameterValue(_dbCommand, "MaxValue"), 0.0));
            _statistics.MinValue = Convert.ToDouble(Common.Common.CastNullValues(_db.GetParameterValue(_dbCommand, "MinValue"), 0.0));
            _statistics.AvgValue = Convert.ToDouble(Common.Common.CastNullValues(_db.GetParameterValue(_dbCommand, "AvgValue"), 0.0));
            _statistics.CountValue = Convert.ToInt64(Common.Common.CastNullValues(_db.GetParameterValue(_dbCommand, "CountValue"), 0));
            _statistics.SumValue = Convert.ToInt64(Common.Common.CastNullValues(_db.GetParameterValue(_dbCommand, "SumValue"), 0));
            _statistics.StdDevValue = Convert.ToDouble(Common.Common.CastNullValues(_db.GetParameterValue(_dbCommand, "StdDevValue"), 0.0));
            _statistics.StdDevPValue = Convert.ToDouble(Common.Common.CastNullValues(_db.GetParameterValue(_dbCommand, "StdDevPValue"), 0.0));
            _statistics.VarValue = Convert.ToDouble(Common.Common.CastNullValues(_db.GetParameterValue(_dbCommand, "VarValue"), 0.0));
            _statistics.VarPValue = Convert.ToDouble(Common.Common.CastNullValues(_db.GetParameterValue(_dbCommand, "VarPValue"), 0.0));

            return _statistics;
        }
        public static IEnumerable<DbDataRecord> ReadSeries(DateTime? startDate, DateTime? endDate)
        {
            Database _db = DatabaseFactory.CreateDatabase();
            DbCommand _dbCommand;

            _dbCommand = _db.GetStoredProcCommand("PA_Calculation_Hydroelectric");
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
        public static IEnumerable<DbDataRecord> ReadSeries(DateTime? startDate, DateTime? endDate, PA.Entities.Measurements.GroupingType groupingType, PA.Entities.Measurements.AggregateType aggregateType)
        {
            Database _db = DatabaseFactory.CreateDatabase();
            DbCommand _dbCommand;
            String _dbProcedureName = "";

            switch (groupingType)
            {
                case PA.Entities.Measurements.GroupingType.Hour:
                    switch (aggregateType)
                    {
                        case PA.Entities.Measurements.AggregateType.Avg:
                            _dbProcedureName = "PA_Calculation_HydroelectricByHourAvg";
                            break;
                        case PA.Entities.Measurements.AggregateType.Sum:
                            _dbProcedureName = "PA_Calculation_HydroelectricByHourSum";
                            break;
                        case PA.Entities.Measurements.AggregateType.StdDev:
                            _dbProcedureName = "PA_Calculation_HydroelectricByHourStdDev";
                            break;
                        case PA.Entities.Measurements.AggregateType.StdDevP:
                            _dbProcedureName = "PA_Calculation_HydroelectricByHourStdDevP";
                            break;
                        case PA.Entities.Measurements.AggregateType.Var:
                            _dbProcedureName = "PA_Calculation_HydroelectricByHourVar";
                            break;
                        case PA.Entities.Measurements.AggregateType.VarP:
                            _dbProcedureName = "PA_Calculation_HydroelectricByHourVarP";
                            break;
                    }
                    break;
                case PA.Entities.Measurements.GroupingType.Day:
                    switch (aggregateType)
                    {
                        case PA.Entities.Measurements.AggregateType.Avg:
                            _dbProcedureName = "PA_Calculation_HydroelectricByDayAvg";
                            break;
                        case PA.Entities.Measurements.AggregateType.Sum:
                            _dbProcedureName = "PA_Calculation_HydroelectricByDaySum";
                            break;
                        case PA.Entities.Measurements.AggregateType.StdDev:
                            _dbProcedureName = "PA_Calculation_HydroelectricByDayStdDev";
                            break;
                        case PA.Entities.Measurements.AggregateType.StdDevP:
                            _dbProcedureName = "PA_Calculation_HydroelectricByDayStdDevP";
                            break;
                        case PA.Entities.Measurements.AggregateType.Var:
                            _dbProcedureName = "PA_Calculation_HydroelectricByDayVar";
                            break;
                        case PA.Entities.Measurements.AggregateType.VarP:
                            _dbProcedureName = "PA_Calculation_HydroelectricByDayVarP";
                            break;
                    }
                    break;
                case PA.Entities.Measurements.GroupingType.Month:
                    switch (aggregateType)
                    {
                        case PA.Entities.Measurements.AggregateType.Avg:
                            _dbProcedureName = "PA_Calculation_HydroelectricByMonthAvg";
                            break;
                        case PA.Entities.Measurements.AggregateType.Sum:
                            _dbProcedureName = "PA_Calculation_HydroelectricByMonthSum";
                            break;
                        case PA.Entities.Measurements.AggregateType.StdDev:
                            _dbProcedureName = "PA_Calculation_HydroelectricByMonthStdDev";
                            break;
                        case PA.Entities.Measurements.AggregateType.StdDevP:
                            _dbProcedureName = "PA_Calculation_HydroelectricByMonthStdDevP";
                            break;
                        case PA.Entities.Measurements.AggregateType.Var:
                            _dbProcedureName = "PA_Calculation_HydroelectricByMonthVar";
                            break;
                        case PA.Entities.Measurements.AggregateType.VarP:
                            _dbProcedureName = "PA_Calculation_HydroelectricByMonthVarP";
                            break;
                    }
                    break;
                case PA.Entities.Measurements.GroupingType.Year:
                    switch (aggregateType)
                    {
                        case PA.Entities.Measurements.AggregateType.Avg:
                            _dbProcedureName = "PA_Calculation_HydroelectricByYearAvg";
                            break;
                        case PA.Entities.Measurements.AggregateType.Sum:
                            _dbProcedureName = "PA_Calculation_HydroelectricByYearSum";
                            break;
                        case PA.Entities.Measurements.AggregateType.StdDev:
                            _dbProcedureName = "PA_Calculation_HydroelectricByYearStdDev";
                            break;
                        case PA.Entities.Measurements.AggregateType.StdDevP:
                            _dbProcedureName = "PA_Calculation_HydroelectricByYearStdDevP";
                            break;
                        case PA.Entities.Measurements.AggregateType.Var:
                            _dbProcedureName = "PA_Calculation_HydroelectricByYearVar";
                            break;
                        case PA.Entities.Measurements.AggregateType.VarP:
                            _dbProcedureName = "PA_Calculation_HydroelectricByYearVarP";
                            break;
                    }
                    break;
            }

            _dbCommand = _db.GetStoredProcCommand(_dbProcedureName);
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

        public static void ExecWithTableParams(DataTable dtTableValueParameter)
        {
            Database _db = DatabaseFactory.CreateDatabase();
            
            SqlConnection _connection = new SqlConnection(_db.ConnectionStringWithoutCredentials);
            _connection.Open();
            // Configure the SqlCommand and SqlParameter.
            SqlCommand _insertCommand = new SqlCommand("usp_InsertMeasurementFromFile", _connection);
            _insertCommand.CommandType = CommandType.StoredProcedure;
            SqlParameter _tvpParam = _insertCommand.Parameters.AddWithValue("@tvp", dtTableValueParameter);
            _tvpParam.SqlDbType = SqlDbType.Structured;

            // Execute the command.
            _insertCommand.ExecuteNonQuery();

            _connection.Close();
        }
        public static void ExecWithTableParams2(DataTable dtTableValueParameter)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            SqlCommand _sqlCommand = (SqlCommand)_db.GetStoredProcCommand("usp_InsertMeasurementFromFile");

            //Configura el type
            _sqlCommand.CommandType = CommandType.StoredProcedure;
            //Arma el parametro de tipo tabla
            SqlParameter _tvpParam = _sqlCommand.Parameters.AddWithValue("@tvp", dtTableValueParameter);
            //Indica que es de tipo Structured, para que lo identifique el SQL como Table.
            _tvpParam.SqlDbType = SqlDbType.Structured;

            // Execute the command.
            _db.ExecuteNonQuery(_sqlCommand);
        }
    }
}
