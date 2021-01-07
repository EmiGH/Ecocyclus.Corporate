using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.NT
{
    public class Notifications
    {
        private List<Entities.NotificationMessage> _NotificationMessages;
        private Credential _Credential;        

        internal Notifications(Credential credential) 
        {
            _Credential = credential;
        }

        /// <summary>
        /// //devuelve todas las notificaciones que tiene que hacer
        /// </summary>
        public List<Entities.NotificationMessage> NotificationMessages
        {
            get
            {
                //Agregar todos las busquedas de objetos que tienen notificaciones

                //Todas las excepciones
                //Notificaciones de excepciones OverDue
                //Notificaciones de excepciones de vencimiento de calibración de equipo 
                //Notificaciones de excepciones de vencimiento de archivo de recurso en vigor
                //Notificaciones de excepciones de rango marcado de medición
                _NotificationMessages = new List<Condesus.EMS.Business.NT.Entities.NotificationMessage>();

                IA.Collections.Exceptions _exceptions = new Condesus.EMS.Business.IA.Collections.Exceptions(_Credential);

                foreach (IA.Entities.Exception _exception in _exceptions.ItemsForNotification().Values)
                {
                    foreach (Entities.NotificationMessage _messages in _exception.NotificationMessages)
                    {
                        _NotificationMessages.Add(_messages);
                    }
                }
                                
                

                //Notificaciones de tareas working
                //Notificaciones de alta o baja de operador en tarea

                //Notifica todos lo errores de calculo
                //Dictionary<Int64, PA.Entities.CalculateOfTransformation> _transformations = new PA.Transformations(_Credential).CalculateOfTransformationErrors;
                //foreach (PA.Entities.CalculateOfTransformation _transformation in _transformations.Values)
                //{
                //    foreach (Entities.NotificationMessage _messages in _transformation.NotificationMessages)
                //    {
                //        _NotificationMessages.Add(_messages);                        
                //    }
                //}

                return _NotificationMessages;
            }
        }

        #region Notification Configuration

        public Entities.NotificationConfiguration NotificationConfiguration()
        {
            return new Collections.NotificationConfigurations().Item();
        }

        #region Write Functions
       
        public Entities.NotificationConfiguration NotificationConfigurationAdd(String emailSender, String host, String userName, String password, Double interval)
        {
            return new Collections.NotificationConfigurations().Add(emailSender, host, userName, password, interval);
        }

        /// <summary>
        /// notifica las tareas antes de que se ejecuten
        /// </summary>
        public void AdvanceNotifications()
        {
            foreach (PF.Entities.ProcessGroupProcess _process in new PF.Collections.ProcessGroupProcesses(_Credential).Items().Values)
            {
                foreach (PF.Entities.ProcessTask _task in _process.ChildrenTaskAdvanceNotice.Values)
                {
                    if (_task.ProcessTaskNextExecutionNotifice() != null)
                    {
                        PF.Entities.TimeUnit _time = new PF.Collections.TimeUnits(_Credential).Item(_task.TimeUnitAdvanceNotice);

                        Int64 _IdTimeUnit = _time == null ? 0 : _time.IdTimeUnit;

                        switch (_IdTimeUnit)
                        {
                            case 1:
                                if (!_task.ProcessTaskNextExecutionNotifice().Notify)
                                {
                                    if (DateTime.Now.AddYears(_task.AdvanceNotice) > _task.ProcessTaskNextExecutionNotifice().Date)
                                    {
                                        if (AdvanceNotificationSendMail(_task))
                                        {
                                            new PF.Collections.ProcessTaskExecutions(_Credential).ChangeStatusNotify(_task.IdProcess, _task.ProcessTaskNextExecutionNotifice().IdExecution);
                                        }
                                    }
                                }
                                break;
                            case 2:
                                if (!_task.ProcessTaskNextExecutionNotifice().Notify)
                                {
                                    if (DateTime.Now.AddMonths(_task.AdvanceNotice) > _task.ProcessTaskNextExecutionNotifice().Date)
                                    {
                                        if (AdvanceNotificationSendMail(_task))
                                        {
                                            new PF.Collections.ProcessTaskExecutions(_Credential).ChangeStatusNotify(_task.IdProcess, _task.ProcessTaskNextExecutionNotifice().IdExecution);
                                        }
                                    }
                                }
                                break;
                            case 3:
                                if (!_task.ProcessTaskNextExecutionNotifice().Notify)
                                {
                                    if (DateTime.Now.AddDays(_task.AdvanceNotice) > _task.ProcessTaskNextExecutionNotifice().Date)
                                    {
                                        if (AdvanceNotificationSendMail(_task))
                                        {
                                            new PF.Collections.ProcessTaskExecutions(_Credential).ChangeStatusNotify(_task.IdProcess, _task.ProcessTaskNextExecutionNotifice().IdExecution);
                                        }
                                    }
                                }
                                break;
                            case 5:
                                if (!_task.ProcessTaskNextExecutionNotifice().Notify)
                                {
                                    if (DateTime.Now.AddHours(_task.AdvanceNotice) > _task.ProcessTaskNextExecutionNotifice().Date)
                                    {
                                        if (AdvanceNotificationSendMail(_task))
                                        {
                                            new PF.Collections.ProcessTaskExecutions(_Credential).ChangeStatusNotify(_task.IdProcess, _task.ProcessTaskNextExecutionNotifice().IdExecution);
                                        }
                                    }
                                }
                                break;
                            case 6:
                                if (!_task.ProcessTaskNextExecutionNotifice().Notify)
                                {
                                    if (DateTime.Now.AddMinutes(_task.AdvanceNotice) > _task.ProcessTaskNextExecutionNotifice().Date)
                                    {
                                        if (AdvanceNotificationSendMail(_task))
                                        {
                                            new PF.Collections.ProcessTaskExecutions(_Credential).ChangeStatusNotify(_task.IdProcess, _task.ProcessTaskNextExecutionNotifice().IdExecution);
                                        }
                                    }
                                }
                                break;

                            default:
                                break;
                        }
                    }
                }
            }
        }
        private Boolean AdvanceNotificationSendMail(PF.Entities.ProcessTask task)
        {
           
            NT.Entities.NotificationConfiguration _NotifConfig = new Condesus.EMS.Business.NT.Collections.NotificationConfigurations().Item();
                //prepara los email
                String _To = null;
                String _to = null;

                foreach (DS.Entities.Post _post in task.ExecutionPermissions())
                {
                    foreach (DS.Entities.ContactEmail _mail in _post.Person.ContactEmails().Values)
                    {
                        _to = _to + _mail.Email + ", ";
                    }
                }
                //foreach(NT.Entities.NotificationRecipient _item in task.NotificationRecipient)
                //{
                //    _to = _to + _item.Email + ", ";
                //}

                //si es distinto de null le saco la ultima coma
                _To = _to != null ? _to.Substring(0, _to.Length - 2) : "";
                
                //Si quedo vacio lo envio al remitente               
                _To = _To == "" ? _NotifConfig.EmailSender : _To;

                

            NT.Entities.Email _email = new Condesus.EMS.Business.NT.Entities.Email();
            return  _email.CreateMail(_NotifConfig.EmailSender, _To, Common.Resources.ConstantMessage.Subject_NotificationMessageAdvanceNotify, task.LanguageOption.Title + " " + Common.Resources.ConstantMessage.Body_NotificationMessageAdvanceNotify + ": " + task.ProcessTaskNextExecutionNotifice().Date);
             
        }
        #endregion


        #endregion
    }
}
