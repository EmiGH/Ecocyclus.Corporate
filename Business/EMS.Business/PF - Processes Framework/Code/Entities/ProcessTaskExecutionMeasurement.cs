using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PF.Entities
{
    public class ProcessTaskExecutionMeasurement:ProcessTaskExecution
    {
        #region Internal Properties
        private Double _MeasureValue;
            private DateTime _MeasureDate;
            private DateTime _TimeStamp;
            private Int64 _IdMeasurementDevice;
            private Int64 _IdMeasurementUnit;
            private DateTime _StartDate;
            private DateTime _EndDate;

            private PA.Entities.MeasurementDevice _MeasurementDevice;
            private PA.Entities.MeasurementUnit _MeasurementUnit;
        #endregion

        #region External Properties
            public Double MeasureValue
            {
                get { return _MeasureValue; }
            }
            public DateTime MeasureDate
            {
                get { return _MeasureDate; }
            }
            public DateTime TimeStamp
            {
                get { return _TimeStamp; }
            }
            public DateTime MeasureStartDate
            {
                get { return _StartDate; }
            }
            public DateTime MeasureEndDate
            {
                get { return _EndDate; }
            }
            public List<PA.Entities.ProcessTaskMeasurementExecutionValue> Values
            {
                get
                {
                    throw new System.NotImplementedException();
                }

            }
            public void AddExecutionValue()
            {
                throw new System.NotImplementedException();
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
            public PA.Entities.MeasurementUnit MeasurementUnit
            {
                get
                {
                    if (_MeasurementUnit == null)
                    {
                        _MeasurementUnit = new PA.Collections.MeasurementUnits(Credential).Item(_IdMeasurementUnit);
                    }
                    return _MeasurementUnit;
                }
            }
        #endregion

            internal ProcessTaskExecutionMeasurement(Int64 idExecution, Int64 idProcess, Int64 idPerson, Int64 idOrganization, Int64 idGeographicArea,
                Int64 idFunctionalArea, Int64 idPosition, DateTime date, String comment, Byte[] attachment, Double measureValue, DateTime measureDate, 
                DateTime timeStamp, Int64 idMeasurementDevice, Int64 idMeasurementUnit, DateTime startDate, DateTime endDate, Boolean advanceNotify, Credential credential)
                : base(idExecution, idProcess, idPerson, idOrganization, idGeographicArea, idFunctionalArea, idPosition, date, comment, attachment, advanceNotify, credential)
        {
            _MeasureValue = measureValue;
            _MeasureDate = measureDate;
            _TimeStamp = timeStamp;
            _IdMeasurementDevice = idMeasurementDevice;
            _IdMeasurementUnit = idMeasurementUnit;
            _StartDate = startDate;
            _EndDate = endDate;
        }

            /// <summary>
            /// Borra sus dependencias
            /// </summary>
            internal void Remove()
            {
                IA.Collections.Exceptions _Exceptions = new IA.Collections.Exceptions(Credential);
                //borra excepciones de medicion
                IEnumerable<System.Data.Common.DbDataRecord> _record = _Exceptions.ItemsByExecutionsOutOfRange(this.ProcessTask.IdProcess);
                 foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                 {

                     _Exceptions.Remove(Convert.ToInt64(_dbRecord["idProcess"]), Convert.ToInt64(_dbRecord["IdExecution"]), Convert.ToInt64(_dbRecord["idExecutionMeasurement"]));

                 }

                base.Remove();
            }
    }
}
