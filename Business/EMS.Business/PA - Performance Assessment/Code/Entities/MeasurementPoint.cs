using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PA.Entities
{
    public class MeasurementPoint
    {
        private DateTime _MeasureDate;
        private Double _MeasureValue;
        private DateTime _StartDate;
        private DateTime _EndDate;
        private Int64 _IdExecutionMeasurement;
        private Credential _Credential;
        private Int64 _IdExecution;
        private Int64 _IdProcess;

        public Int64 IdProcess
        {
            get { return _IdProcess; }
        }
        public Int64 IdExecution
        {
            get { return _IdExecution; }
        }
        public Int64 IdExecutionMeasurement
        {
            get { return _IdExecutionMeasurement; }
        }
        public DateTime MeasureDate
        {
            get { return _MeasureDate; }
        }
        public Double MeasureValue
        {
            get { return _MeasureValue; }
        }
        public DateTime StartDate
        {
            get { return _StartDate; }
        }
        public DateTime EndDate
        {
            get { return _EndDate; }
        }
        public Dictionary<Int64, IA.Entities.Exception> Exceptions
        {
            get { return new IA.Collections.Exceptions(_Credential).ItemsByMeasurement(_IdExecutionMeasurement); }
        }
        public Boolean Sing
        {
            get 
            {
                if (this.Exceptions != null)
                {
                    if (this.Exceptions.Count > 0)
                    {
                        foreach (IA.Entities.Exception _exc in this.Exceptions.Values)
                        {
                            if (_exc.ExceptionState.IdExceptionState != Common.Constants.ExceptionStateClose)
                            {
                                return true;
                            }
                        }
                    }
                }
                return false;
                
            }
        }

        internal MeasurementPoint(DateTime measureDate, Double measureValue, DateTime startDate, DateTime endDate, Int64 idExecutionMeasurement, Credential credential, Int64 idExecution, Int64 IdProcess)
        {
            _MeasureDate = measureDate;
            _MeasureValue = measureValue;
            _StartDate = startDate;
            _EndDate = endDate;
            _IdExecutionMeasurement = idExecutionMeasurement;
            _Credential = credential;
            _IdExecution = idExecution;
            _IdProcess = IdProcess;
        }
    }
}
