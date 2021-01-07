using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.PF.Entities
{
    //public class ProcessGroupNode : ProcessGroup
    //{
    //    #region Internal Properties
    //        private Int64 _IdProcessParent;
    //        private Dictionary<Int64, IA.Entities.Exception> _Exeptions;
    //        private Dictionary<Int64, PF.Entities.ProcessTask> _ProcessTask;
    //    #endregion 

    //    #region External Properties      
    //        public override Process Parent
    //        {
    //            //Llamar al factory para crear lo que corresponda               
    //            get { return new Collections.ProcessGroups(Credential).Item(_IdProcessParent); }
    //        }

    //        public override String State
    //        {
    //            get
    //            {
    //                if (DateTime.Now < StartDate)
    //                { return Condesus.EMS.Business.Common.Resources.ConstantMessage.ProcessState_Planned; }

    //                //Si la fecha actual esta dentro de las fechas planeada de inicio y fin de la tarea
    //                if ((DateTime.Now >= StartDate) && (DateTime.Now <= EndDate))
    //                {
    //                    //y si el % de completitud es 100%, entonces la tarea esta terminada.
    //                    if (Completed == 100)
    //                    {
    //                        return Condesus.EMS.Business.Common.Resources.ConstantMessage.ProcessState_Finished;
    //                    }
    //                    else
    //                    {
    //                        //sino esta trabajando.
    //                        return Condesus.EMS.Business.Common.Resources.ConstantMessage.ProcessState_Working;
    //                    }
    //                }
    //                //Si la fecha actual es mayor a la planificada como fecha final y no llego al 100% de completitud, quiere decir que esta vencida!!!
    //                if ((DateTime.Now > EndDate) && (Completed < 100))
    //                { return Condesus.EMS.Business.Common.Resources.ConstantMessage.ProcessState_OverDue; }

    //                return Condesus.EMS.Business.Common.Resources.ConstantMessage.ProcessState_Working;
    //            }
    //        }
    //        public override DateTime StartDate
    //        {
    //            get 
    //            {
    //                DateTime _startDate = DateTime.MaxValue;
    //                Dictionary<Int64, ProcessGroupNode> _Nodes = ChildrenNodes;
    //                foreach (ProcessGroupNode _Node in _Nodes.Values)
    //                {
    //                    if (_Node.StartDate < _startDate) { _startDate = _Node.StartDate; }
    //                }
    //                Dictionary<Int64, ProcessTask> _tasks = ChildrenTask;                    
    //                foreach (ProcessTask _task in _tasks.Values)
    //                {
    //                    if (_task.StartDate < _startDate) { _startDate = _task.StartDate; }                        
    //                }
    //                return _startDate;
    //            }
    //        }
    //        public override DateTime EndDate
    //        {
    //            get 
    //            {
    //                DateTime _endDate = DateTime.MinValue;
    //                Dictionary<Int64, ProcessGroupNode> _Nodes = ChildrenNodes;
    //                foreach (ProcessGroupNode _Node in _Nodes.Values)
    //                {
    //                    if (_Node.EndDate > _endDate) { _endDate = _Node.EndDate; }
    //                }
    //                Dictionary<Int64, ProcessTask> _tasks = ChildrenTask;
    //                foreach (ProcessTask _task in _tasks.Values)
    //                {
    //                    if (_task.EndDate > _endDate) { _endDate = _task.EndDate; }                        
    //                }
    //                return _endDate;
    //            }
    //        }
    //        public override int Completed
    //        {
    //            get 
    //            {     
    //                ///si o si el peso (Weight), no puede pasar el 100%
    //                Int32 _completed = 0;
    //                Int32 _countSisters = 0;
    //                Dictionary<Int64, ProcessGroupNode> _Nodes = ChildrenNodes;
    //                foreach (ProcessGroupNode _Node in _Nodes.Values)
    //                {
    //                    //_completed = _completed + (_Node.Completed * _Node.Weight) / 100;  
    //                    _completed = _completed + (_Node.Completed);
    //                    _countSisters = _countSisters + 1;
    //                }
    //                Dictionary<Int64, ProcessTask> _tasks = ChildrenTask;
    //                foreach (ProcessTask _task in _tasks.Values)
    //                {
    //                    //_completed = _completed + (_task.Completed * _task.Weight) / 100;
    //                    _completed = _completed + (_task.Completed);
    //                    _countSisters = _countSisters + 1;
    //                }
    //                if (_countSisters == 0) { _countSisters = 1; }
    //                _completed = _completed / _countSisters;
    //                if (_completed > 100) { _completed = 100; }
    //                return _completed;

    //            }
    //        }
    //        public override String Result
    //        {
    //            get 
    //            {
    //                String _result = "---";
    //                Int32 _percentageCompleted = 0;
    //                Dictionary<Int64, ProcessGroupNode> _Nodes = ChildrenNodes;
    //                foreach (ProcessGroupNode _Node in _Nodes.Values)
    //                {
    //                    //el resultado me importa si el proceso esta completo
    //                    if (_Node.Completed == 100 && _Node.Result == Common.Resources.ConstantMessage.ProcessResult_Ok) { _percentageCompleted = _percentageCompleted + _Node.Weight; }
    //                }
    //                Dictionary<Int64, ProcessTask> _tasks = ChildrenTask;
    //                foreach (ProcessTask _task in _tasks.Values)
    //                {
    //                    //el resultado me importa si el proceso esta completo
    //                    if (_task.Completed == 100 && _task.Result == Common.Resources.ConstantMessage.ProcessResult_Ok) { _percentageCompleted = _percentageCompleted + _task.Weight; }  
    //                }
    //                //si las que estan true superan el umbral pongo el resultado en true
    //                if (_percentageCompleted >= Threshold) { _result = Common.Resources.ConstantMessage.ProcessResult_Ok; }
    //                else { if (this.Completed == 100) { _result = Common.Resources.ConstantMessage.ProcessResult_NotOk; } }
    //                return _result;
    //            }
    //        }

    //        public override Dictionary<Int64, IA.Entities.Exception>  Exceptions
    //        {
    //            get 
    //            {
                    
    //                Dictionary<Int64, ProcessGroupNode> _Nodes = new Dictionary<Int64, ProcessGroupNode>();
    //                Dictionary<Int64, ProcessTask> _tasks = new Dictionary<Int64, ProcessTask>();
    //                if (_Exeptions == null)
    //                {
    //                    _Exeptions = new Dictionary<long, Condesus.EMS.Business.IA.Entities.Exception>();
    //                    _Nodes = ChildrenNodes;
    //                     foreach (ProcessGroupNode _Node in _Nodes.Values)
    //                     {
    //                         if (_Node.Exceptions != null)
    //                         {
    //                             foreach (IA.Entities.Exception _exeption in _Node.Exceptions.Values)
    //                             {
    //                                 _Exeptions.Add(_exeption.IdException, _exeption);
    //                             }
    //                         }
    //                     }
    //                    _tasks = ChildrenTask;
    //                    foreach (ProcessTask _task in _tasks.Values)
    //                    {
    //                        foreach (IA.Entities.Exception _exeption in _task.Exceptions.Values)
    //                        {
    //                            _Exeptions.Add(_exeption.IdException,_exeption);
    //                        }
    //                    }
    //                }
    //                return _Exeptions;
    //            }
    //        }

    //        #region External Properties
    //        #region Process Task
    //        #region Task Common
    //        public PF.Entities.ProcessTask ProcessTask(Int64 idProcess)
    //        {
    //            return new PF.Collections.ProcessTasks(Credential).Item(idProcess);
    //        }
    //        public Dictionary<Int64, ProcessTask> ChildrenTask
    //        {
    //            get
    //            {
    //                Dictionary<Int64, ProcessTask> _processTasks = new PF.Collections.ProcessTasks(Credential).Items(IdProcess);
    //                return _processTasks;
    //            }
    //        }
    //        #endregion

    //        #region Task Operation
    //        public PF.Entities.ProcessTask ProcessTasksAdd(Int16 weight, Int16 orderNumber, String title, String purpose, String description, PF.Entities.ProcessGroupNode processGrupNode, DateTime startDate, DateTime endDate, Int32 duration, Int32 interval, Int64 maxNumberExecutions, Boolean result, Int32 completed, PF.Entities.TimeUnit timeUnitDuration, PF.Entities.TimeUnit timeUnitInterval, String comment, String typeExecution, List<DS.Entities.Post> post, GIS.Entities.Site site, KC.Entities.Resource taskInstruction, List<NT.Entities.NotificationRecipient> notificationRecipients)
    //        {
    //            using (TransactionScope _transactionScope = new TransactionScope())
    //            {
    //                PF.Entities.ProcessTask _processTask = new PF.Collections.ProcessTasks(Credential).AddTask(weight, orderNumber, title, purpose, description, processGrupNode, startDate, endDate, duration, interval, maxNumberExecutions, result, completed, timeUnitDuration, timeUnitInterval, comment, typeExecution, post, site, taskInstruction, notificationRecipients);
    //                _transactionScope.Complete();
    //                return _processTask;
    //            }                 
                
    //        }
    //        public void ProcessTasksRemove(PF.Entities.ProcessTaskOperation processTaskOperation)
    //        {
    //            using (TransactionScope _transactionScope = new TransactionScope())
    //            {
    //                new PF.Collections.ProcessTasks(Credential).RemoveTask(processTaskOperation);
    //                if (_ProcessTask != null)
    //                {
    //                    _ProcessTask.Remove(processTaskOperation.IdProcess);
    //                }
    //                _transactionScope.Complete();
    //            }
    //        }            
    //        #endregion

    //        #region Task Calibration
    //        public PF.Entities.ProcessTask ProcessTasksAdd(Int16 weight, Int16 orderNumber, String title, String purpose, String description, PF.Entities.ProcessGroupNode processGrupNode, DateTime startDate, DateTime endDate, Int32 duration, Int32 interval, Int64 maxNumberExecutions, Boolean result, Int32 completed, PF.Entities.TimeUnit timeUnitDuration, PF.Entities.TimeUnit timeUnitInterval, PA.Entities.MeasurementDevice measurementDevice, String typeExecution, List<DS.Entities.Post> post, GIS.Entities.Site site, KC.Entities.Resource taskInstruction, List<NT.Entities.NotificationRecipient> notificationRecipients)
    //        {
    //            using (TransactionScope _transactionScope = new TransactionScope())
    //            {
    //                PF.Entities.ProcessTask _processTask = new PF.Collections.ProcessTasks(Credential).AddTask(weight, orderNumber, title, purpose, description, processGrupNode, startDate, endDate, duration, interval, maxNumberExecutions, result, completed, timeUnitDuration, timeUnitInterval, measurementDevice, typeExecution, post, site, taskInstruction, notificationRecipients);
    //                _transactionScope.Complete();
    //                return _processTask;
    //            }
    //        }
    //        public void ProcessTasksRemove(PF.Entities.ProcessTaskCalibration processTaskCalibration)
    //        {
    //            using (TransactionScope _transactionScope = new TransactionScope())
    //            {
    //                new PF.Collections.ProcessTasks(Credential).RemoveTask(processTaskCalibration);
    //                if (_ProcessTask != null)
    //                {
    //                    _ProcessTask.Remove(processTaskCalibration.IdProcess);
    //                }
    //                _transactionScope.Complete();
    //            }
    //        }     
    //        #endregion

    //        #region Task Measurement
    //        public PF.Entities.ProcessTask ProcessTasksAdd(PA.Entities.MeasurementDevice measurementDevice, PA.Entities.ParameterGroup parametersGroup, 
    //            PA.Entities.Indicator indicator, String measurementName, String measurementDescription, PF.Entities.TimeUnit timeUnitFrequency, 
    //            Int32 frequency, PA.Entities.MeasurementUnit measurementUnit, Boolean isCumulative, Boolean isRegressive, Boolean isRelevant, 
    //            Int16 weight, Int16 orderNumber, String title, String purpose, String description, PF.Entities.ProcessGroupNode processGrupNode, 
    //            DateTime startDate, DateTime endDate, Int32 duration, Int32 interval, Int64 maxNumberExecutions, Boolean result, Int32 completed, 
    //            PF.Entities.TimeUnit timeUnitDuration, PF.Entities.TimeUnit timeUnitInterval, String typeExecution, List<DS.Entities.Post> post, 
    //            GIS.Entities.Site site, KC.Entities.Resource taskInstruction, String measurementSource, String measurementFrequencyAtSource,
    //            Decimal measurementUncertainty, PA.Entities.Quality measurementQuality, PA.Entities.Methodology measurementMethodology,
    //            PA.Entities.AccountingScope accountingScope, PA.Entities.AccountingActivity accountingActivity, String reference,
    //            List<NT.Entities.NotificationRecipient> notificationRecipients)
    //        {
    //            using (TransactionScope _transactionScope = new TransactionScope())
    //            {
    //                PA.Entities.Measurement _measurement = new PA.Collections.Measurements(Credential).Add(measurementDevice, parametersGroup, 
    //                    indicator, measurementName, measurementDescription, timeUnitFrequency, frequency, measurementUnit, isCumulative, 
    //                    isRegressive, isRelevant, measurementSource, measurementFrequencyAtSource, measurementUncertainty, measurementQuality, 
    //                    measurementMethodology); 
                    
    //                PF.Entities.ProcessTask _processTask = new PF.Collections.ProcessTasks(Credential).AddTask(weight, orderNumber, title, purpose, 
    //                    description, processGrupNode, startDate, endDate, duration, interval, maxNumberExecutions, result, completed, 
    //                    timeUnitDuration, timeUnitInterval, _measurement, typeExecution, post, site, taskInstruction, accountingScope, 
    //                    accountingActivity, reference, notificationRecipients);

    //                _transactionScope.Complete();
    //                return _processTask;
    //            }
    //        }
    //        public void ProcessTasksRemove(PF.Entities.ProcessTaskMeasurement processTaskMeasurement)
    //        {
    //            using (TransactionScope _transactionScope = new TransactionScope())
    //            {
    //                new PF.Collections.ProcessTasks(Credential).RemoveTask(processTaskMeasurement);
    //                if (_ProcessTask != null)
    //                {
    //                    _ProcessTask.Remove(processTaskMeasurement.IdProcess);
    //                }
    //                _transactionScope.Complete();

    //            }
    //        }            
    //        #endregion

    //        //Dentro de un Node Exception solo pueden existir tareas del Tipo DataRecovery y Operativas.
    //        #region Task Data Recovery
    //        public PF.Entities.ProcessTask ProcessTasksAdd(Int16 weight, Int16 orderNumber, String title, String purpose, String description, PF.Entities.ProcessGroupNode processGrupNode, DateTime startDate, DateTime endDate, Int32 duration, Int32 interval, Int64 maxNumberExecutions, Boolean result, Int32 completed, PF.Entities.TimeUnit timeUnitDuration, PF.Entities.TimeUnit timeUnitInterval, String typeExecution, PF.Entities.ProcessTaskMeasurement processTaskMeasurement, List<DateTime> measurementDateToRecovery, List<DS.Entities.Post> post, GIS.Entities.Site site, KC.Entities.Resource taskInstruction, List<NT.Entities.NotificationRecipient> notificationRecipients)
    //        {
    //            //aca es donde se arma una transaccion y primero se graba la tarea de recovery
    //            //y luego se graban las fechas de medicion que se habilitan para recuperar.
    //            using (TransactionScope _transactionScope = new TransactionScope())
    //            {
    //                //1° grabo la tabla de la planificacion de la tarea de Recovery
    //                PF.Entities.ProcessTask _processTask = new PF.Collections.ProcessTasks(Credential).AddTask(weight, orderNumber, title, purpose, description, processGrupNode, startDate, endDate, duration, interval, maxNumberExecutions, result, completed, timeUnitDuration, timeUnitInterval, typeExecution, processTaskMeasurement, post, site, taskInstruction, notificationRecipients);

    //                //2° grabo la tabla con las fechas de medicion para recuperar.
    //                new PF.Collections.ProcessTasks(Credential).AddDataRecoveryMeasurement((PF.Entities.ProcessTaskDataRecovery)_processTask, measurementDateToRecovery);

    //                //actualiza la coleccion interna. (patron proxy)
    //                if (_ProcessTask != null)
    //                {
    //                    _ProcessTask.Add(_processTask.IdProcess, _processTask);
    //                }

    //                // Completar la transacción
    //                _transactionScope.Complete();

    //                return _processTask;
    //            }
    //        }
    //        public void ProcessTasksRemove(PF.Entities.ProcessTaskDataRecovery processTaskDataRecovery)
    //        {
    //            //aca es donde se arma una transaccion y primero se borra las mediciones a recuperar
    //            //y luego se borra la tarea de recovery.
    //            using (TransactionScope _transactionScope = new TransactionScope())
    //            {
    //                //1° borra las measurementeDate a recuperar.
    //                new PF.Collections.ProcessTasks(Credential).RemoveDataRecoveryMeasurement(processTaskDataRecovery);
    //                //2° borra la tarea
    //                new PF.Collections.ProcessTasks(Credential).RemoveTask(processTaskDataRecovery);

    //                if (_ProcessTask != null)
    //                {
    //                    _ProcessTask.Remove(processTaskDataRecovery.IdProcess);
    //                }

    //                // Completar la transacción
    //                _transactionScope.Complete();

    //            }
    //        }
       
    //        #endregion        
          
    //        #endregion
    //        #endregion
    //    #endregion

    //        internal ProcessGroupNode(Int64 idProcess, Int16 weight, Int16 orderNumber, String idLanguage, String title, String purpose, String description, Credential credential, Int16 threshold, 
    //                 Int64 idParentProcess)
    //            : base( idProcess,  weight, orderNumber, idLanguage, title, purpose, description, credential, threshold)
    //        {
    //            _IdProcessParent = idParentProcess;
    //        }

    //        /// <summary>
    //        /// borra sus depencias
    //        /// </summary>
    //        internal override void Remove()
    //        {
    //            //Borra las tareas
    //            foreach (ProcessTask _task in ChildrenTask.Values)
    //            {
    //                if (_task.GetType().Name == "ProcessTaskOperation") { this.ProcessTasksRemove((ProcessTaskOperation)_task); }
    //                if (_task.GetType().Name == "ProcessTaskCalibration") { this.ProcessTasksRemove((ProcessTaskCalibration)_task); }
    //                if (_task.GetType().Name == "ProcessTaskMeasurement") { this.ProcessTasksRemove((ProcessTaskMeasurement)_task); }
    //                if (_task.GetType().Name == "ProcessTaskDataRecovery") { this.ProcessTasksRemove((ProcessTaskDataRecovery)_task); }
    //            }

    //            //Borra las nodos
    //            foreach (ProcessGroupNode _node in ChildrenNodes.Values)
    //            {
    //                this.Remove(_node);
    //            }

    //            base.Remove();
    //        }

    //        public void Modify(Int16 weight, Int16 orderNumber, String title, String purpose, String description, Int16 threshold, Entities.Process parentProcess)
    //        {
    //            new Collections.ProcessGroupNodes(Credential).Modify(this,  weight,  orderNumber,  title,  purpose,  description,  threshold,  parentProcess);
    //        }

    //}
}
