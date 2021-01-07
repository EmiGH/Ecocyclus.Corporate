using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.PA.Entities
{
    public class ConfigurationAsociationMeasurementExcelFile
    {
        #region Internal Properties
        private Entities.Measurement _Measurement;
        private String _IndexValue;
        private String _IndexDate;
        #endregion
        #region External Properties
        public Entities.Measurement Measurement
        {
            get { return _Measurement; }
        }
        public String IndexValue
        {
            get { return _IndexValue; }
        }
        public String IndexDate
        {
            get { return _IndexDate; }
        }
        #endregion

        public ConfigurationAsociationMeasurementExcelFile(Entities.Measurement measrement, String IndexValue, String IndexDate)
        {
            _Measurement = measrement;
            _IndexValue = IndexValue;
            _IndexDate = IndexDate;
        }


    }
}
