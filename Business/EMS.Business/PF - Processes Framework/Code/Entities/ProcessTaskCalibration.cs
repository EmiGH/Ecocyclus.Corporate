using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Transactions;
using System.Data.SqlClient;

namespace Condesus.EMS.Business.PF.Entities
{
    public class ProcessTaskCalibration : ProcessTask
    {
        #region Internal Properties
            private Int64 _IdMeasurementDevice;
            private  PA.Entities.MeasurementDevice _MeasurementDevice;
        #endregion

        #region External Properties
            public override ProcessGroupProcess Parent
            {
                get
                {
                    return new PF.Collections.ProcessGroupProcesses(Credential).Item(_IdProcessParent);
                }
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
     

            #region Executions Calibration
            public PF.Entities.ProcessTaskExecution ProcessTaskExecutionsAdd(DS.Entities.Post post, DateTime date, String comment, Byte[] attachment, DateTime validationStart, DateTime validationEnd, PA.Entities.MeasurementDevice measuremenDevice)
            {
                return new PF.Collections.ProcessTaskExecutions(Credential).AddExecution(this, post, date, comment, attachment, validationStart, validationEnd, measuremenDevice);
            }
            public PF.Entities.ProcessTaskExecution ProcessTaskExecutionsAdd(DS.Entities.Post post, PF.Entities.ProcessTaskExecution processTaskExecution, String comment, Byte[] attachment, DateTime validationStart, DateTime validationEnd, PA.Entities.MeasurementDevice measuremenDevice)
            {
                return new PF.Collections.ProcessTaskExecutions(Credential).AddExecution(this, post, processTaskExecution, comment, attachment, validationStart, validationEnd, measuremenDevice);
            }
            public PF.Entities.ProcessTaskExecution ProcessTaskExecutionsAdd(DS.Entities.Post post, PF.Entities.ProcessTaskExecution processTaskExecution, ref Int64 idFile, String fileName, Byte[] fileStream, String comment, DateTime validationStart, DateTime validationEnd, PA.Entities.MeasurementDevice measuremenDevice)
            {
                return new PF.Collections.ProcessTaskExecutions(Credential).AddExecution(ref idFile, this, post, processTaskExecution, fileName, fileStream, validationStart, validationEnd, measuremenDevice);
            }
            public void Remove(PF.Entities.ProcessTaskExecutionCalibration processTaskExecutionCalibration)
            {
                new PF.Collections.ProcessTaskExecutions(Credential).RemoveExecution(processTaskExecutionCalibration);
            }
            #endregion

            /// <summary>
            /// Borra sus dependencias
            /// </summary>
            internal override void Remove()
            {
                //Borra las calibration execution
                foreach (ProcessTaskExecution _processTaskExecution in this.ProcessTaskExecutions.Values)
                {
                    if (_processTaskExecution.GetType().Name == "ProcessTaskExecutionCalibration")
                    {
                        this.Remove((ProcessTaskExecutionCalibration)_processTaskExecution);
                    }
                    else
                    {
                        this.Remove(_processTaskExecution);
                    }
                }

                base.Remove();   
            }
        #endregion

            internal ProcessTaskCalibration(Int64 idProcess, Int16 weight, Int16 orderNumber, String idLanguage, String title, String purpose, String description, Credential credential, Int64 idParentProcess, DateTime startDate, DateTime endDate, Int32 duration, Int32 interval, Int64 maxNumberExecutions, Boolean result, Int32 completed, Int64 timeUnitDuration, Int64 timeUnitInterval, String typeExecution, Int64 idFacility,Int64 idTaskInstruction, Boolean ExecutionStatus,
                                        Int64 idMeasurementDevice, Int64 timeUnitAdvanceNotice, Int16 advanceNotice)
            : base(idProcess, weight, orderNumber, idLanguage, title, purpose, description, credential, idParentProcess, startDate, endDate, duration, interval, maxNumberExecutions, result, completed, timeUnitDuration, timeUnitInterval, typeExecution, idFacility, idTaskInstruction, ExecutionStatus, timeUnitAdvanceNotice, advanceNotice)
        {
            _IdMeasurementDevice = idMeasurementDevice;
        }

            public void Modify(Int16 weight, Int16 orderNumber, String title, String purpose, String description, PF.Entities.ProcessGroupProcess process, DateTime startDate, DateTime endDate, Int32 duration, Int32 interval, Int64 maxNumberExecutions, Boolean result, Int32 completed, PF.Entities.TimeUnit timeUnitDuration, PF.Entities.TimeUnit timeUnitInterval, PA.Entities.MeasurementDevice measurementDevice, String typeExecution, List<DS.Entities.Post> post, GIS.Entities.Site site, KC.Entities.Resource taskInstruction, List<NT.Entities.NotificationRecipient> notificationRecipients, PF.Entities.TimeUnit timeUnitAdvanceNotice, Int16 advanceNotice)
            {
                using (TransactionScope _transactionScope = new TransactionScope())
                {
                    new Collections.ProcessTasks(Credential).ModifyTask(this, weight, orderNumber, title, purpose, description, process, startDate, endDate, duration, interval, maxNumberExecutions, result, completed, timeUnitDuration, timeUnitInterval, measurementDevice, typeExecution, post, site, taskInstruction, notificationRecipients, timeUnitAdvanceNotice, advanceNotice);
                    _transactionScope.Complete();
                }

            }
    }
}
