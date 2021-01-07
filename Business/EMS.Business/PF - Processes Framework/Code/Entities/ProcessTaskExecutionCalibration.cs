using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PF.Entities
{
    public class ProcessTaskExecutionCalibration:ProcessTaskExecution
    {
        #region Internal Properties
            private DateTime _ValidationStart;
            private DateTime _ValidationEnd;
            private Int64 _IdMeasurementDevice;
            private PA.Entities.MeasurementDevice _MeasurementDevice;
        #endregion

        #region External Properties
            public DateTime ValidationStart
            {
                get { return _ValidationStart; }
            }
            public DateTime ValidationEnd
            {
                get { return _ValidationEnd; }
            }
            public PA.Entities.MeasurementDevice MeasurementDevice
            {
                get
                {
                    if (_MeasurementDevice == null)
                    {
                        _MeasurementDevice = new PA.Collections.MeasurementDevices(Credential).Item(_IdMeasurementDevice);
                    }
                    return _MeasurementDevice;
                }
            }            
        #endregion

            internal ProcessTaskExecutionCalibration(Int64 idExecution, Int64 idProcess, Int64 idPerson, Int64 idOrganization,
                Int64 idGeographicArea, Int64 idFunctionalArea, Int64 idPosition, DateTime date, String comment, Byte[] attachment, Boolean AdvanceNotify, DateTime validationStart, 
                DateTime validationEnd, Int64 idMeasurementDevice, Credential credential)
                                        : base(idExecution, idProcess, idPerson, idOrganization, idGeographicArea, idFunctionalArea, idPosition, date, comment, attachment, AdvanceNotify, credential)
        {
            _ValidationStart = validationStart;
            _ValidationEnd = validationEnd;
            _IdMeasurementDevice = idMeasurementDevice;
        }

        public void ModifyState()
        {
            throw new System.NotImplementedException();
        }
    }
}
