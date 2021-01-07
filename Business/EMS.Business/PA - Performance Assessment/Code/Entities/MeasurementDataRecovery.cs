using System;
using System.Collections.Generic;
using System.Text;

namespace Condesus.EMS.Business.PA.Entities
{
    public class MeasurementDataRecovery
    {
        private DateTime _StartDate;

        public DateTime StartDate
        {
            get { return _StartDate; }
        }
        private DateTime _EndDate;

        public DateTime EndDate
        {
            get { return _EndDate; }
        }
        private decimal _Value;

        public decimal Value
        {
            get { return _Value; }
        }
        private long _IdMeasurement;

        public Measurement Measurement
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }

        public PF.Entities.ProcessTaskMeasurement TaskMeasurement
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }

        internal MeasurementDataRecovery()
        {
            throw new System.NotImplementedException();
        }

        public void AddDataRecovery()
        {
            throw new System.NotImplementedException();
        }
    }
}
