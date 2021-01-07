using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Data.SqlClient;
using System.Transactions;

namespace Condesus.EMS.Business.PF.Entities
{
    public class ProcessGroupException : ProcessGroup
    {
        #region Internal Properties
        private Dictionary<Int64, PF.Entities.ProcessTask> _ProcessTask;
        #endregion

        #region External Properties
        public override String State
        {
            get { return null; }
        }
        public override DateTime StartDate
        {
            get { return DateTime.MinValue; }
        }
        public override DateTime EndDate
        {
            get { return DateTime.MinValue; }
        }
        public override Int32 Completed
        {
            get { return 0; }
        }

        public Entities.ProcessTask associatedtask
        {
            get
            {
                return new Collections.ProcessGroupExceptions(Credential).AssociatedTask(IdProcess);
            }
        }
        public override Entities.ProcessGroupProcess Parent
        {
            get
            {
                return this.associatedtask.Parent;
            }
        }
        public override String Result
        {
            get
            { return null; }
        }

        public override Dictionary<Int64, IA.Entities.Exception> Exceptions
        {
            get
            {      //Este metodo no se utiliza desde aca, sino desde ProcessGroup.
                new ApplicationException("Method not supported by this class");
                return null;
            }
        }

        #region Process Task

        #region Task Data Recovery
        public PF.Entities.ProcessTask ProcessTasksAdd(Int16 weight, Int16 orderNumber, String title, String purpose, String description, DateTime startDate, DateTime endDate, Int32 duration, Int32 interval, Int64 maxNumberExecutions, Boolean result, Int32 completed, PF.Entities.TimeUnit timeUnitDuration, PF.Entities.TimeUnit timeUnitInterval, String typeExecution, PF.Entities.ProcessTaskMeasurement processTaskMeasurement, List<DateTime> measurementDateToRecovery, List<DS.Entities.Post> post, GIS.Entities.Site site, KC.Entities.Resource taskInstruction, List<NT.Entities.NotificationRecipient> notificationRecipients, PF.Entities.TimeUnit timeUnitAdvanceNotice, Int16 advanceNotice)
        {
            //aca es donde se arma una transaccion y primero se graba la tarea de recovery
            //y luego se graban las fechas de medicion que se habilitan para recuperar.
            using (TransactionScope _transactionScope = new TransactionScope())
            {
                //1° grabo la tabla de la planificacion de la tarea de Recovery
                PF.Entities.ProcessTask _processTask = new PF.Collections.ProcessTasks(Credential).AddTask(weight, orderNumber, title, purpose, description, this, startDate, endDate, duration, interval, maxNumberExecutions, result, completed, timeUnitDuration, timeUnitInterval, typeExecution, processTaskMeasurement, post, site, taskInstruction, notificationRecipients, timeUnitAdvanceNotice, advanceNotice);

                //2° grabo la tabla con las fechas de medicion para recuperar.
                new PF.Collections.ProcessTasks(Credential).AddDataRecoveryMeasurement((PF.Entities.ProcessTaskDataRecovery)_processTask, measurementDateToRecovery);

                //actualiza la coleccion interna. (patron proxy)
                if (_ProcessTask != null)
                {
                    _ProcessTask.Add(_processTask.IdProcess, _processTask);
                }

                // Completar la transacción
                _transactionScope.Complete();

                return _processTask;
            }
        }
        public void ProcessTasksRemove(PF.Entities.ProcessTaskDataRecovery processTaskDataRecovery)
        {
            //aca es donde se arma una transaccion y primero se borra las mediciones a recuperar
            //y luego se borra la tarea de recovery.
            using (TransactionScope _transactionScope = new TransactionScope())
            {
                //1° borra las measurementeDate a recuperar.
                new PF.Collections.ProcessTasks(Credential).RemoveDataRecoveryMeasurement(processTaskDataRecovery);
                //2° borra la tarea
                new PF.Collections.ProcessTasks(Credential).RemoveTask(processTaskDataRecovery);

                if (_ProcessTask != null)
                {
                    _ProcessTask.Remove(processTaskDataRecovery.IdProcess);
                }

                // Completar la transacción
                _transactionScope.Complete();

            }
        }

        #endregion

        #endregion


        #endregion  

        internal override void Remove()
        {
            //TODO Borrar las relaciones 
            //Borra las tareas
            foreach (ProcessTask _task in ChildrenTask.Values)
            {
                if (_task.GetType().Name == "ProcessTaskDataRecovery") { this.ProcessTasksRemove((ProcessTaskDataRecovery)_task); }
            }


            base.Remove();
        }

        internal ProcessGroupException(Int64 idProcess, Int16 weight, Int16 orderNumber, String idLanguage, String title, String purpose, String description, Int16 threshold, Credential credential)
            : base(idProcess, weight, orderNumber, idLanguage, title, purpose, description, credential, threshold)
        {
            
        }

        public void Modify(Int16 weight, Int16 orderNumber, String title, String purpose, String description, Int16 threshold)
        {
            using (TransactionScope _TransactionScope = new TransactionScope())
            {
                new Collections.ProcessGroupExceptions(Credential).Modify(this, weight, orderNumber, title, purpose, description, threshold);
                _TransactionScope.Complete();
            }
        }

    }
}
