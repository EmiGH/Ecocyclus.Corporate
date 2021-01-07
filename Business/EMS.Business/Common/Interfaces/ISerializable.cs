using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Condesus.EMS.Business
{
    public interface ISerializable
    {
        
        PA.Entities.MeasurementPoint TotalMeasurement(ref DateTime? firstDateSeries);

        List<PA.Entities.MeasurementPoint> Series();
        List<PA.Entities.MeasurementPoint> Series(DateTime? startDate, DateTime? endDate);
        DateTime MaxDate();
        DateTime Mindate();

    }
}
