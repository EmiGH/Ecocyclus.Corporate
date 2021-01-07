using System;
using System.Collections.Generic;
using System.Text;

namespace Condesus.EMS.Business.PA.Entities
{
    public class ProcessTaskMeasurementExecutionValue
    {
        private decimal _Value;

        internal ProcessTaskMeasurementExecutionValue()
        {
            throw new System.NotImplementedException();
        }

        public decimal Value
        {
            get { return _Value; }
        }
    }
}
