using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace Condesus.EMS.Business.PF.Entities
{
    public class ProcessTaskMeasurement : ProcessTask
    {
        #region Internal Properties
            private Int64 _IdMeasurement;
            private PA.Entities.Measurement _Measurement;
            private PF.Entities.ProcessTaskExecution _ProcessTaskExecution;
            private PA.Entities.AccountingActivity _AccountingActivity;
            private PA.Entities.AccountingScope _AccountingScope;
            private Int64 _IdActivity;
            private Int64 _IdScope;
            private String _Reference;
            private Boolean _MeasurementStatus;
        #endregion

        #region External Properties
            public override ProcessGroupProcess Parent
            {
                get
                {
                    return new PF.Collections.ProcessGroupProcesses(Credential).Item(_IdProcessParent);
                }
            }
            public Boolean MeasurementStatus
            {
                get { return _MeasurementStatus; }
            }
            public String Reference
            {
                get { return _Reference; }
            }
            public PA.Entities.AccountingScope AccountingScope
            {
                get
                {
                    if (_AccountingScope == null)
                    { _AccountingScope = new PA.Collections.AccountingScopes(this).Item(_IdScope); }
                    return _AccountingScope;
                }
            }
            public PA.Entities.AccountingActivity AccountingActivity
            {
                get
                {
                    if (_AccountingActivity == null)
                    { _AccountingActivity = new PA.Collections.AccountingActivities(this).Item(_IdActivity); }
                    return _AccountingActivity;
                }
            }
            public PA.Entities.Measurement Measurement
        {
            get
            {
                if (_Measurement == null)
                {
                    _Measurement = new PA.Collections.Measurements(Credential).Item(_IdMeasurement);
                }
                return _Measurement;
            }
        }
        public PF.Entities.ProcessTaskExecution MeasurementExecution(Int64 idExecution)
        {
            if (_ProcessTaskExecution == null)
            { _ProcessTaskExecution = new Collections.ProcessTaskExecutions(Credential).Item(IdProcess, idExecution); }
            return _ProcessTaskExecution;                
        }
        public void AddMeasurementExecution()
        {
            throw new System.NotImplementedException();
        }
        internal void PlanMeasurementExecution()
        {
            throw new System.NotImplementedException();
        }
        #endregion

        #region Executions Measurement
        public void ProcessTaskExecutionsAddTVP(DS.Entities.Post post, String comment, Byte[] attachment, DataTable dtTVPValues, PA.Entities.MeasurementDevice measuremenDevice, PA.Entities.MeasurementUnit measuremenUnit, Boolean skipValidation, Boolean chargeNotice)
        {
            new PF.Collections.ProcessTaskExecutions(Credential).AddExecutionTVP(this, post, comment, attachment, dtTVPValues, measuremenDevice, measuremenUnit, skipValidation, chargeNotice);
        }
        public void ProcessTaskExecutionsAddTVP(DS.Entities.Post post, PF.Entities.ProcessTaskExecution processTaskExecution, String comment, Byte[] attachment, DataTable dtTVPValues, PA.Entities.MeasurementDevice measuremenDevice, PA.Entities.MeasurementUnit measuremenUnit, Boolean skipValidation, Boolean chargeNotice)
        {
            new PF.Collections.ProcessTaskExecutions(Credential).AddExecutionTVP(this, post, processTaskExecution, comment, attachment, dtTVPValues, measuremenDevice, measuremenUnit, skipValidation, chargeNotice);
        }
        
        public PF.Entities.ProcessTaskExecution ProcessTaskExecutionsAdd(DS.Entities.Post post, DateTime date, String comment, Byte[] attachment, Double measureValue, DateTime measureDate, PA.Entities.MeasurementDevice measuremenDevice, PA.Entities.MeasurementUnit measuremenUnit, Boolean chargeNotice)
        {
            return new PF.Collections.ProcessTaskExecutions(Credential).AddExecution(this, post, date, comment, attachment, measureValue, measureDate, measuremenDevice, measuremenUnit, chargeNotice);
        }
        
        public void ProcessTaskExecutionsAdd(DS.Entities.Post post, PF.Entities.ProcessTaskExecution processTaskExecution, ref Int64 idFile, String fileName, String fileStream, Byte[] fileStreamBinary, String comment, Byte[] attachment, ref Decimal measureValue, ref DateTime measureDate, PA.Entities.MeasurementDevice measuremenDevice, PA.Entities.MeasurementUnit measuremenUnit, Boolean skipValidation, Boolean chargeNotice)
        {
            new PF.Collections.ProcessTaskExecutions(Credential).AddExecution(this, post, processTaskExecution, fileName, fileStream, fileStreamBinary, comment,attachment, measuremenDevice, measuremenUnit, skipValidation, chargeNotice);
        }
        //con excell
        public void ProcessTaskExecutionsAdd(DS.Entities.Post post, String fileName, String fileStream, Byte[] fileStreamBinary, String comment, Byte[] attachment, PA.Entities.MeasurementUnit measurementUnit, Boolean skipValidation, Boolean chargeNotice)
        {                                                        
            new PF.Collections.ProcessTaskExecutions(Credential).AddExecution(this, post, fileName, fileStream, fileStreamBinary, comment, attachment, measurementUnit, skipValidation, chargeNotice);
        }

        internal void Remove(PF.Entities.ProcessTaskExecutionMeasurement processTaskExecutionMeasurement)
        {
            new PF.Collections.ProcessTaskExecutions(Credential).RemoveExecution(processTaskExecutionMeasurement);
        }
        internal List<Int64> Exceptions()
        {
            //Objeto de data layer para acceder a datos
            DataAccess.IA.ImprovementActions _dbImprovementActions = new Condesus.EMS.DataAccess.IA.ImprovementActions();
            List<Int64> _oItems = new List<Int64>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbImprovementActions.Exceptions_ReadExceptions(IdProcess);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _oItems.Add(Convert.ToInt64(_dbRecord["IdException"]));
            }
            return _oItems;
        }
        //
        //public Boolean ValidateMeasurementDateExecution(PF.Entities.ProcessTaskExecution processTaskExecution, String fileStream, DataTable dtTVPMeasurements)
        //{
        //    //Hace solo la validacion de las fechas de medicion, si hay huecos, entonces retorna un falso
        //    //y en front end se debe mostrar el mensaje, para que luego el usuario diga si quiere insertar de todas formas o no.
        //    return new PF.Collections.ProcessTaskExecutions(Credential).ValidateMeasurementDateExecution(IdProcess, processTaskExecution.IdExecution, fileStream, false);
        //}
       
        #endregion

        /// <summary>
        /// Borra sus dependencias
        /// </summary>
        internal override void Remove()
        {
            //Borra las calibration execution
            foreach (ProcessTaskExecution _processTaskExecution in this.ProcessTaskExecutions.Values)
            {
                if (_processTaskExecution.GetType().Name == "ProcessTaskExecution")
                {
                    this.Remove(_processTaskExecution);
                }
                else
                {
                    this.Remove((ProcessTaskExecutionMeasurement)_processTaskExecution);
                }
            }
            

            base.Remove();
        }

        internal ProcessTaskMeasurement(Int64 idProcess, Int16 weight, Int16 orderNumber, String idLanguage, String title, String purpose, String description, Credential credential, Int64 idParentProcess, DateTime startDate, DateTime endDate, Int32 duration, Int32 interval, Int64 maxNumberExecutions, Boolean result, Int32 completed, Int64 timeUnitDuration, Int64 timeUnitInterval, String typeExecution, Int64 idFacility,Int64 idTaskInstruction, Boolean ExecutionStatus,
                                Int64 idMeasurement, Int64 idScope, Int64 idActivity, String reference, Boolean MeasurementStatus, Int64 timeUnitAdvanceNotice, Int16 advanceNotice)
            : base(idProcess, weight, orderNumber, idLanguage, title, purpose, description, credential, idParentProcess, startDate, endDate, duration, interval, maxNumberExecutions, result, completed, timeUnitDuration, timeUnitInterval, typeExecution, idFacility, idTaskInstruction, ExecutionStatus, timeUnitAdvanceNotice, advanceNotice)
        {
            _IdMeasurement = idMeasurement;
            _IdScope = idScope;
            _IdActivity = idActivity;
            _Reference = reference;
            _MeasurementStatus = MeasurementStatus;
        }

        public void Modify(PA.Entities.MeasurementDevice measurementDevice, List<PA.Entities.ParameterGroup> parametersGroups, 
            PA.Entities.Indicator indicator, String measurementName, String measurementDescription, PF.Entities.TimeUnit timeUnitFrequency, 
            Int32 frequency, PA.Entities.MeasurementUnit measurementUnit, Boolean isRegressive, Boolean isRelevant, 
            Int16 weight, Int16 orderNumber, String title, String purpose, String description, PF.Entities.ProcessGroupProcess process, 
            DateTime startDate, DateTime endDate, Int32 duration, Int32 interval, Int64 maxNumberExecutions, Boolean result, Int32 completed, 
            PF.Entities.TimeUnit timeUnitDuration, PF.Entities.TimeUnit timeUnitInterval, String typeExecution, List<DS.Entities.Post> post,
            GIS.Entities.Site site, KC.Entities.Resource taskInstruction, String measurementSource, String measurementFrequencyAtSource,
            Decimal measurementUncertainty, PA.Entities.Quality measurementQuality, PA.Entities.Methodology measurementMethodology,
            PA.Entities.AccountingScope accountingScope, PA.Entities.AccountingActivity accountingActivity, String reference, List<NT.Entities.NotificationRecipient> notificationRecipients, PF.Entities.TimeUnit timeUnitAdvanceNotice, Int16 advanceNotice)
        {
            using (TransactionScope _transactionScope = new TransactionScope())
            {
                this.Measurement.Modify(measurementDevice, parametersGroups, indicator, measurementName, measurementDescription, timeUnitFrequency, frequency, measurementUnit, isRegressive, isRelevant, measurementSource, measurementFrequencyAtSource, measurementUncertainty, measurementQuality, measurementMethodology); 
                new Collections.ProcessTasks(Credential).ModifyTask(this, weight, orderNumber, title, purpose, description, process, startDate, endDate, duration, interval, maxNumberExecutions, result, completed, timeUnitDuration, timeUnitInterval, this.Measurement, typeExecution, post, site, taskInstruction,accountingScope, accountingActivity, reference, notificationRecipients, timeUnitAdvanceNotice, advanceNotice);
                _transactionScope.Complete();
            }
  
        }
    }
}
