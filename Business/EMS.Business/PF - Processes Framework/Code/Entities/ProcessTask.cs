using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.PF.Entities
{
    public abstract class ProcessTask : Process, INotificationRecipient
    {
        #region Internal Proprties
            protected Int64 _IdProcessParent;
            private DateTime _StartDate;
            private DateTime _EndDate;
            private Int32 _Duration;
            private Int32 _Interval;
            private Int64 _MaxNumberExecution;
            private Boolean _Result;
            private String _ResultStr;
            private Int32 _Completed;
            private Int64 _TimeUnitDuration;
            private Int64 _TimeUnitInterval;
            private String _TypeExecution;
            private Int64 _IdFacility;
            private Dictionary<Int64, ProcessTaskExecution> _ProcessTaskExecutions;
            private Dictionary<Int64, IA.Entities.Exception> _ExceptionTask;
            private GIS.Entities.Site _Site;
            private Int64 _IdTaskInstruction;
            private Boolean _ExecutionStatus;
            private KC.Entities.Resource _TaskInstruction;
            private Int64 _TimeUnitAdvanceNotice;
            private Int16 _AdvanceNotice;
        #endregion

        #region External Properties
            public Int16 AdvanceNotice
            {
                get { return _AdvanceNotice; }
            }
            public override DateTime StartDate
            {
                get { return _StartDate; }
            }
            public override DateTime EndDate
            {
                get { return _EndDate; }
            }
            public Boolean ExecutionStatus
            {
                get { return _ExecutionStatus; }
            }
            public Int32 Duration
            {
                get { return _Duration; }
            }
            public Int32 Interval
            {
                get { return _Interval; }
            }
            public Int64 MaxNumberExecution
            {
                get { return _MaxNumberExecution; }
            }
            public override String Result
            {
                get 
                {
                    _ResultStr = "---";
                    if (this.Completed == 100)
                    {
                        if (_Result) { _ResultStr = Common.Resources.ConstantMessage.ProcessResult_Ok; } else { _ResultStr = Common.Resources.ConstantMessage.ProcessResult_NotOk; }
                    }
                    return _ResultStr; 
                }
            }
            public override Int32 Completed
            {
                get { return _Completed; }
            }
            public Int64 TimeUnitDuration
            {
                get { return _TimeUnitDuration; }
            }
            public Int64 TimeUnitAdvanceNotice
            {
                get { return _TimeUnitAdvanceNotice; }
            }
            public Int64 TimeUnitInterval
            {
                get { return _TimeUnitInterval; }
            }
            public String TypeExecution
            {
                get { return _TypeExecution; }
            }
            
            public override String State
            {
                get
                {
                    if (DateTime.Now < _StartDate)
                    { return Condesus.EMS.Business.Common.Resources.ConstantMessage.ProcessState_Planned; }

                    //Si la fecha actual esta dentro de las fechas planeada de inicio y fin de la tarea
                    if ((DateTime.Now >= _StartDate) && (DateTime.Now <= _EndDate))
                    {
                        //y si el % de completitud es 100%, entonces la tarea esta terminada.
                        if (_Completed == 100)
                        {
                            return Condesus.EMS.Business.Common.Resources.ConstantMessage.ProcessState_Finished;
                        }
                        else
                        {
                            //sino esta trabajando.
                            return Condesus.EMS.Business.Common.Resources.ConstantMessage.ProcessState_Working;
                        }
                    }
                    //Si la fecha actual es mayor a la planificada como fecha final y no llego al 100% de completitud, quiere decir que esta vencida!!!
                    if ((DateTime.Now > _EndDate) && (_Completed < 100) && (_TypeExecution != "Spontaneous"))
                    //if ((DateTime.Now > _EndDate) && (_Completed < 100))
                    { return Condesus.EMS.Business.Common.Resources.ConstantMessage.ProcessState_OverDue; }

                    return Condesus.EMS.Business.Common.Resources.ConstantMessage.ProcessState_Finished;
                }
            }
    
            public GIS.Entities.Site Site
            {
                get
                {
                    if (_Site == null)
                    { _Site = new GIS.Collections.Facilities(Credential).Item(_IdFacility); }
                    return _Site;
                }
            }

            public KC.Entities.Resource TaskInstruction
            {
                get
                {
                    if (_TaskInstruction == null)
                    { _TaskInstruction = new KC.Collections.Resources(Credential).Item(_IdTaskInstruction); }
                    return _TaskInstruction;
                }
            }
            

            #region Executions

                #region Executions Permissions
            /// <summary>
            /// Alta de un permiso para ejecutar la tarea
            /// </summary>
            /// <param name="post"></param>
            public void AddExecutionPermission(DS.Entities.Post post)
            {
                new Collections.ProcessTasks(Credential).AddExecutionPermission(this, post);
            }
            /// <summary>
            /// Baja de un permiso para ejecutar la tarea
            /// </summary>
            /// <param name="post"></param>
            public void RemoveExecutionPermission(DS.Entities.Post post)
            {
                new Collections.ProcessTasks(Credential).RemoveExecutionPermission(this, post);
            }
            /// <summary>
            /// Borra todos los permisos para esta tareaa
            /// </summary>
            public void RemoveExecutionPermission()
            {
                new Collections.ProcessTasks(Credential).RemoveExecutionPermission(this);
            }
            /// <summary>
            /// Devuelve todos los puestos q tienen permiso para ejecutar esta tarea
            /// </summary>
            /// <returns></returns>
            public List<DS.Entities.Post> ExecutionPermissions()
            {
                return new Collections.ProcessTasks(Credential).ReadExecutionPermissions(this);
            }
            public DS.Entities.Post ExecutionPermission(DS.Entities.Post post)
            {
                return new Collections.ProcessTasks(Credential).ReadExecutionPermission(this, post);
            }
            /// <summary>
            /// Valida si tiene permisos para una persona y devuelve el post pa el cual tiene permisos
            /// </summary>
            /// <param name="post"></param>
            /// <returns></returns>
            public DS.Entities.Post ExecutionPermission(DS.Entities.Person person)
            {
                foreach(DS.Entities.Post _post in ExecutionPermissions())
                {
                    if (_post.Person.IdPerson == person.IdPerson) { return _post; }
                }
                return null;
            }
            #endregion

                #region Executions Read
            public PF.Entities.ProcessTaskExecution ProcessTaskNextExecutionNotifice()
                    {
                        return new PF.Collections.ProcessTaskExecutions(Credential).NextExecutionNotifice(IdProcess);
                    }
                    public PF.Entities.ProcessTaskExecution ProcessTaskExecutionNow(DateTime startDate, DateTime endDate)
                    {
                        return new PF.Collections.ProcessTaskExecutions(Credential).NextExecution(IdProcess, startDate, endDate);
                    }
                    public PF.Entities.ProcessTaskExecution ProcessTaskExecutionNow()
                    {
                        return new PF.Collections.ProcessTaskExecutions(Credential).NowExecution(IdProcess);
                    }
                    public PF.Entities.ProcessTaskExecution ProcessTaskExecutionLast(Int64 idExecution)
                    {
                        return new PF.Collections.ProcessTaskExecutions(Credential).LastExecution(IdProcess, idExecution);
                    }
                    public PF.Entities.ProcessTaskExecution ProcessTaskExecutionLast()
                    {
                        return new PF.Collections.ProcessTaskExecutions(Credential).LastExecution(IdProcess);
                    }
                    public PF.Entities.ProcessTaskExecutionMeasurement ProcessTaskExecutionMeasurementLast()
                    {
                        return new PF.Collections.ProcessTaskExecutions(Credential).LastExecutionMeasurement(IdProcess);
                    }
                    public PF.Entities.ProcessTaskExecution ProcessTaskExecution(Int64 idExecution)
                    {
                        return new PF.Collections.ProcessTaskExecutions(Credential).Item(IdProcess, idExecution);
                    }
                    public Dictionary<Int64, ProcessTaskExecution> ProcessTaskExecutions
                    {
                        get
                        {
                            if (_ProcessTaskExecutions == null)
                            {
                                _ProcessTaskExecutions = new PF.Collections.ProcessTaskExecutions(Credential).Items(IdProcess);
                            }
                            return _ProcessTaskExecutions;
                        }
                    }
                    public Dictionary<Int64, ProcessTaskExecution> ProcessTaskExecutionsOnly
                    {
                        get
                        {
                            if (_ProcessTaskExecutions == null)
                            {
                                _ProcessTaskExecutions = new PF.Collections.ProcessTaskExecutions(Credential).ItemsOnlyExecution(IdProcess);
                            }
                            return _ProcessTaskExecutions;
                        }
                    }
                #endregion

                #region Executions Write
                    public PF.Entities.ProcessTaskExecution ProcessTaskExecutionsAdd(DS.Entities.Post post, DateTime date, String comment, Byte[] attachment, Boolean result)
                    {
                        return new PF.Collections.ProcessTaskExecutions(Credential).AddExecution(this, post, date, comment, attachment, result);
                    }
                    public void Remove(PF.Entities.ProcessTaskExecution processTaskExecution)
                    {
                        new PF.Collections.ProcessTaskExecutions(Credential).RemoveExecution(processTaskExecution);
                    }
                #endregion
                                
            #endregion

            #region Exceptions
                //mis exceptions
                public override Dictionary<Int64, IA.Entities.Exception> Exceptions
                {
                    get
                    {
                        if (_ExceptionTask == null)
                        { _ExceptionTask = new IA.Collections.Exceptions(Credential).ItemsByTask(IdProcess); }
                        return _ExceptionTask;
                    }
                }

              
            #endregion

            #region INotificationRecipient
                public List<NT.Entities.NotificationRecipient> NotificationRecipient
                {
                    get
                    {
                        return new NT.Collections.NotificationRecipients(this).Items();
                    }
                }

                public NT.Entities.NotificationRecipientEmail NotificationRecipientAdd(String email)
                {
                    return new NT.Collections.NotificationRecipients(this).Add(this, email);
                }
                public void Remove(NT.Entities.NotificationRecipientEmail notificationRecipientEmail)
                {
                    new NT.Collections.NotificationRecipients(this).Remove(this);
                }
                public NT.Entities.NotificationRecipientPerson NotificationRecipientPersonAdd(DS.Entities.Person person, DS.Entities.ContactEmail contactEmail)
                {
                    return new NT.Collections.NotificationRecipients(this).Add(this, person,contactEmail);
                }
                public void Remove(NT.Entities.NotificationRecipientPerson notificationRecipientPerson)
                {
                    new NT.Collections.NotificationRecipients(this).Remove(this);
                }
                public void NotificationRecipientRemove()
                {
                    new NT.Collections.NotificationRecipients(this).Remove(this);
                }
            #endregion

                /// <summary>
            /// Borra sus dependencias
            /// </summary>
            internal virtual void Remove()
            {
                //Borra los emails
                NotificationRecipientRemove();
                //Borra los permisos
                this.RemoveExecutionPermission();

                foreach (IA.Entities.Exception _exception in this.Exceptions.Values)
                {
                    new IA.Collections.Exceptions(Credential).Remove((IA.Entities.ExceptionProcessTask)_exception);
                }

                base.Remove();
            }
        #endregion

        internal ProcessTask(Int64 idProcess, Int16 weight, Int16 orderNumber, String idLanguage, String title, String purpose, String description, 
                Credential credential,Int64 idParentProcess, DateTime startDate, DateTime endDate, Int32 duration, Int32 interval, 
            Int64 maxNumberExecutions, Boolean result, Int32 completed, Int64 timeUnitDuration, Int64 timeUnitInterval, String typeExecution,
            Int64 idFacility, Int64 idTaskInstruction, Boolean ExecutionStatus, Int64 timeUnitAdvanceNotice, Int16 advanceNotice)
            : base(idProcess, weight, orderNumber, idLanguage, title, purpose, description, credential)
        {
            _IdProcessParent = idParentProcess;
            _StartDate = startDate;
            _EndDate = endDate;
            _Duration = duration;
            _Interval = interval;
            _MaxNumberExecution = maxNumberExecutions;
            _Result = result;
            _Completed = completed;
            _TimeUnitDuration = timeUnitDuration;
            _TimeUnitInterval = timeUnitInterval;
            _TypeExecution = typeExecution;
            _IdFacility = idFacility;
            _IdTaskInstruction = idTaskInstruction;
            _ExecutionStatus = ExecutionStatus;
            _TimeUnitAdvanceNotice = timeUnitAdvanceNotice;
            _AdvanceNotice = advanceNotice;
        }


    }
}
