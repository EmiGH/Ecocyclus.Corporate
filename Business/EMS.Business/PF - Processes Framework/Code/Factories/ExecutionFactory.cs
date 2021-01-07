using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PF.Factories
{
    internal class ExecutionFactory
    {
        private ExecutionFactory() { }

        //el factory contiene un metodo publico y estatico que puede ser utilizado por sin necesidad de instanciar la clase
        static public PF.Entities.ProcessTaskExecution CreateExecution(Int64 idProcess, Int64 idExecution, Int64 idOrganization, 
            Int64 idPerson, Int64 idPosition, Int64 idFunctionalArea, Int64 idGeographicArea,
            DateTime date, String comment, Byte[] attachment, Boolean AdvanceNotify, Credential credential, DateTime validationStart,
            DateTime validationEnd, Int64 idMeasurementDevice, Double measureValue, DateTime measureDate, 
            DateTime timeStamp, Int64 idMeasurementUnit, DateTime startDate, DateTime endDate, String type)
        {
            //Entities.ProcessTask _processTask = new Collections.ProcessTasks(credential).Item(idProcess);
            //DS.Entities.Post _post;

            //if (idOrganization == 0)
            //{
            //    _post = null;
            //}
            //else
            //{
            ////    DS.Entities.Organization _organization = new DS.Collections.Organizations(credential).Item(idOrganization);
            ////    DS.Entities.JobTitle _jobTitle = new DS.Collections.JobTitles(_organization).Item(idPosition, idGeographicArea, idFunctionalArea);
            ////    DS.Entities.Person _person = new DS.Collections.People(_organization).Item(idPerson);
            ////    _post = new DS.Collections.Posts(_jobTitle).Item(_person);
            //}
            if (type == "Calibration") { return new PF.Entities.ProcessTaskExecutionCalibration(idExecution, idProcess, idPerson, idOrganization, idGeographicArea, idFunctionalArea, idPosition, date, comment, attachment, AdvanceNotify, validationStart, validationEnd, idMeasurementDevice, credential); }

            if (type == "Measurement") { return new PF.Entities.ProcessTaskExecutionMeasurement(idExecution, idProcess, idPerson, idOrganization, idGeographicArea, idFunctionalArea, idPosition, date, comment, attachment, measureValue, measureDate, timeStamp, idMeasurementDevice, idMeasurementUnit, startDate, endDate, AdvanceNotify, credential); }

            if (type == "Execution") { return new PF.Entities.ProcessTaskExecution(idExecution, idProcess, idPerson, idOrganization, idGeographicArea, idFunctionalArea, idPosition, date, comment, attachment, AdvanceNotify, credential); }
            
            return null;
        }
    }
}
