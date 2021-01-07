using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.IA.Entities
{
    public class ExceptionProcessTask : Exception
    {

        private Int64 _IdExceptionState;
        private PF.Entities.ProcessTask _AssociateTask;
        private List<NT.Entities.NotificationMessage> _NotificationMessages;

        internal ExceptionProcessTask(Int64 idException, Int64 idExceptionType, Int64 idExceptionState, String comment, DateTime exceptionDate, Credential credential) 
            : base (idException, idExceptionType, idExceptionState, comment, exceptionDate, credential)
        {
            _IdExceptionState = idExceptionType;
        }

        #region Task
        public PF.Entities.ProcessTask AssociateTask
        {
            get
            {
                if (_AssociateTask == null)
                { _AssociateTask = new PF.Collections.ProcessTasks(Credential).Item(this); }
                return _AssociateTask;
            }
        }
        #endregion
        #region Execution
        public PF.Entities.ProcessTaskExecution AssociateExecution
        {
            get
            {
                return new PF.Collections.ProcessTaskExecutions(Credential).Item(this);

            }
        }
        #endregion

        public override List<NT.Entities.NotificationMessage> NotificationMessages
        {
            get
            {
                _NotificationMessages = new List<Condesus.EMS.Business.NT.Entities.NotificationMessage>();
                NT.Entities.NotificationMessage _NotificationMessage;

                if (_IdExceptionState == 1)
                {
                    _NotificationMessage = new NT.Entities.NotificationMessageTaskOverdue(this);
                }
                else
                {
                    _NotificationMessage = new NT.Entities.NotificationMessageValueOutOfRange(this);
                }

                _NotificationMessages.Add(_NotificationMessage);
                
                return _NotificationMessages;
            }
        }


        internal override void Remove()
        {

            //borra la excepcion
            
            base.Remove();
        }
    }
}
