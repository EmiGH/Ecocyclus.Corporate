using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.PA.Entities
{
    public class MeasurementIntensive : Measurement
    {
        public Boolean IsExtensive
        {
            get { return false; }
        }
        /// <summary>
        /// Retorna el total de las mediciones en caso de ser Acumulativa y su ultima fecha tomada o el ultimo valor medido y su ultima fecha.
        /// </summary>
        /// <returns>un <c>MeasurementPoint</c></returns>
        public override MeasurementPoint TotalMeasurement(ref DateTime? firstDateSeries)
        {
            Double _total = 0;
            DateTime _dateMeasurement = DateTime.Today;
            DateTime _Startdate = DateTime.Today;
            DateTime _Enddate = DateTime.Today;
            Int64 _idprocess = 0;
            Int64 _idExecution = 0;
            Int64 _idExecutionMeasurement = 0;

            List<MeasurementPoint> _measureemntPoints = Series();
            //si es intensiva devuelve el ultimo valor cargado
            if (_measureemntPoints.Count > 0)
            {
                foreach (MeasurementPoint _measurementPoint in _measureemntPoints)
                {
                    _total = _measurementPoint.MeasureValue;
                    _dateMeasurement = _measurementPoint.MeasureDate;   //Se queda con la ultima fecha medida.
                    _Startdate = _measurementPoint.StartDate;
                    _Enddate = _measurementPoint.EndDate;
                    _idprocess = _measurementPoint.IdProcess;
                    _idExecution = _measurementPoint.IdExecution;
                    _idExecutionMeasurement = _measurementPoint.IdExecutionMeasurement;
                }
                return new MeasurementPoint(_dateMeasurement, _total, _Startdate, _Enddate, _idExecutionMeasurement , Credential, _idExecution, _idprocess);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Devuleve el valor para hacer la operacion en para la fecha solocitada
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public override Double OperateValue(Entities.CalculateOfTransformation transformation, DateTime startDate, DateTime endDate)
        {
            return new Collections.Measurements(Credential).OperateValue(transformation, this, startDate, endDate);
        }


        internal MeasurementIntensive(Int64 idMeasurement, Int64 idDevice, Int64 idIndicator, Int64 idTimeUnitFrequency,
        Int32 frequency, Int64 idMeasurementUnit, String name, String description, Boolean isRegressive, Boolean isRelevant, String idLanguage,
        Credential credential, String source, String frequencyAtSource, Decimal uncertainty, Int64 idQuality, Int64 idMethodology)
            : base(idMeasurement, idDevice, idIndicator, idTimeUnitFrequency, frequency, idMeasurementUnit, name,
            description, isRegressive, isRelevant, idLanguage, credential, source, frequencyAtSource, uncertainty, idQuality, idMethodology)
        { }
    }
}
