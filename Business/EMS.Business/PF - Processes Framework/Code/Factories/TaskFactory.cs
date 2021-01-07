using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PF.Factories
{
    internal class TaskFactory
    {
        private TaskFactory() { }

        //el factory contiene un metodo publico y estatico que puede ser utilizado por sin necesidad de instanciar la clase
        static public PF.Entities.ProcessTask CreateTask(Int64 idProcess, Int16 weight, Int16 orderNumber, String idLanguage, String title, String purpose, String description, Credential credential,
            Int64 idParentProcess, DateTime startDate, DateTime endDate, Int32 duration, Int32 interval, Int64 maxNumberExecutions, Boolean result, Int32 completed, Int64 timeUnitDuration, Int64 timeUnitInterval, String typeExecution, Int64 idFacility,Int64 idTaskInstruction, Boolean ExecutionStatus,
            String comment, Int64 idMeasurementDevice, Int64 idMeasurement, String type,
            Int64 idTaskParent, Int64 idScope, Int64 idActivity, String reference, Boolean MeasurementStatus, Int64 timeUnitAdvanceNotice, Int16 advanceNotice)
        {

            if (type == "Operation") { return new PF.Entities.ProcessTaskOperation(idProcess, weight, orderNumber, idLanguage, title, purpose, description, credential, idParentProcess, startDate, endDate, duration, interval, maxNumberExecutions, result, completed, timeUnitDuration, timeUnitInterval, typeExecution, idFacility, idTaskInstruction, ExecutionStatus,  comment, timeUnitAdvanceNotice, advanceNotice); }

            if (type == "Calibration") { return new PF.Entities.ProcessTaskCalibration(idProcess, weight, orderNumber, idLanguage, title, purpose, description, credential, idParentProcess, startDate, endDate, duration, interval, maxNumberExecutions, result, completed, timeUnitDuration, timeUnitInterval, typeExecution, idFacility, idTaskInstruction, ExecutionStatus, idMeasurementDevice, timeUnitAdvanceNotice, advanceNotice); }

            if (type == "Measurement") { return new PF.Entities.ProcessTaskMeasurement(idProcess, weight, orderNumber, idLanguage, title, purpose, description, credential, idParentProcess, startDate, endDate, duration, interval, maxNumberExecutions, result, completed, timeUnitDuration, timeUnitInterval, typeExecution, idFacility, idTaskInstruction, ExecutionStatus, idMeasurement, idScope , idActivity, reference, MeasurementStatus, timeUnitAdvanceNotice, advanceNotice); }

            if (type == "DataRecovery") { return new PF.Entities.ProcessTaskDataRecovery(idProcess, weight, orderNumber, idLanguage, title, purpose, description, credential, idParentProcess, startDate, endDate, duration, interval, maxNumberExecutions, result, completed, timeUnitDuration, timeUnitInterval, typeExecution, idFacility, idTaskInstruction, ExecutionStatus, idTaskParent, timeUnitAdvanceNotice, advanceNotice); }

            return null;
        }

    }
}
