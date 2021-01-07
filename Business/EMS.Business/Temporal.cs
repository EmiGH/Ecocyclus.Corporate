using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business
{
    public class Temporal
    {
        //private Credential _Credential;
        public Temporal()
        { }

        public enum AggregateType
        { Sum, Avg, StdDev, StdDevP, Var, VarP }
        public enum GroupingType
        { Hour, Day, Month, Year }

        public static List<PA.Entities.MeasurementPoint> Series()
        {
            return Series(null, null);
        }
        public static List<PA.Entities.MeasurementPoint> Series(DateTime? startDate, DateTime? endDate)
        {
             Credential _Credential = null;
            PA.Entities.MeasurementPoint _point = null;
            List<PA.Entities.MeasurementPoint> _points = new List<PA.Entities.MeasurementPoint>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = DataAccess.Temporal.ReadSeries(startDate, endDate);

            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _point = new PA.Entities.MeasurementPoint(Convert.ToDateTime(_dbRecord["MeasureDate"]), Convert.ToDouble(_dbRecord["MeasureValue"]), Convert.ToDateTime(_dbRecord["StartDate"]), Convert.ToDateTime(_dbRecord["EndDate"]), 0, _Credential, 0, 0);
                _points.Add(_point);

            }
            return _points;
        }
        public static List<PA.Entities.MeasurementPoint> Series(DateTime? startDate, DateTime? endDate, GroupingType groupingType, AggregateType aggregateType)
        {
            Credential _Credential = null;
            PA.Entities.MeasurementPoint _point = null;
            List<PA.Entities.MeasurementPoint> _points = new List<PA.Entities.MeasurementPoint>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = DataAccess.Temporal.ReadSeries(startDate, endDate, TranslateGrouping(groupingType), TranslateAggregate(aggregateType));

            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _point = new PA.Entities.MeasurementPoint(Common.Common.ConstructDateTime(Convert.ToString(_dbRecord["MeasureDate"])), Convert.ToDouble(Common.Common.CastNullValues(_dbRecord["MeasureValue"], 0.0)), Convert.ToDateTime(_dbRecord["StartDate"]), Convert.ToDateTime(_dbRecord["EndDate"]), 0 , _Credential, 0,0);
                _points.Add(_point);

            }
            return _points;
        }

        private static DataAccess.PA.Entities.Measurements.GroupingType TranslateGrouping(GroupingType grouping)
        {
            if (grouping == GroupingType.Hour) return DataAccess.PA.Entities.Measurements.GroupingType.Hour;
            if (grouping == GroupingType.Day) return DataAccess.PA.Entities.Measurements.GroupingType.Day;
            if (grouping == GroupingType.Month) return DataAccess.PA.Entities.Measurements.GroupingType.Month;
            if (grouping == GroupingType.Year) return DataAccess.PA.Entities.Measurements.GroupingType.Year;

            return Condesus.EMS.DataAccess.PA.Entities.Measurements.GroupingType.Day;

        }
        private static DataAccess.PA.Entities.Measurements.AggregateType TranslateAggregate(AggregateType aggregate)
        {
            if (aggregate == AggregateType.Avg) return DataAccess.PA.Entities.Measurements.AggregateType.Avg;
            if (aggregate == AggregateType.StdDev) return DataAccess.PA.Entities.Measurements.AggregateType.StdDev;
            if (aggregate == AggregateType.StdDevP) return DataAccess.PA.Entities.Measurements.AggregateType.StdDevP;
            if (aggregate == AggregateType.Sum) return DataAccess.PA.Entities.Measurements.AggregateType.Sum;
            if (aggregate == AggregateType.Var) return DataAccess.PA.Entities.Measurements.AggregateType.Var;
            if (aggregate == AggregateType.VarP) return DataAccess.PA.Entities.Measurements.AggregateType.VarP;

            return Condesus.EMS.DataAccess.PA.Entities.Measurements.AggregateType.Avg;
        }

        public static PA.Entities.MeasurementStatistics Statistics(DateTime? startDate, DateTime? endDate)
        {
            return new PA.Entities.MeasurementStatistics(DataAccess.Temporal.ReadStatistics(startDate, endDate));
        }


    }
}
