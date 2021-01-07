using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Condesus.EMS.Business.NT.Entities
{
    public class NotificationMessageValueOutOfRange : NotificationMessage
    {
        #region Internal Properties
        private IA.Entities.ExceptionProcessTask _Exception;
        private String _To;
        #endregion

        public override INotificationReported NotificationReported
        {
            get { return _Exception; }
        }
        public override String To
        { 
            get 
            { 
                String _to = null;
                foreach(NotificationRecipient _item in _Exception.AssociateTask.NotificationRecipient)
                {
                    _to = _to + _item.Email + ", ";
                }

                //si es distinto de null le saco la ultima coma
                _To = _to != null ? _to.Substring(0, _to.Length - 2) : "";
                
                //Si quedo vacio lo envio al remitente               
                _To = _To == "" ? base.From : _To;

                return _To; 
            } 
        }
        public override String Subject
        { get { return Common.Resources.ConstantMessage.Subject_NotificationMessageValueOutOfRange; } }

        public override String Body
        {
            get
            {
                return "Exception Comment: " + _Exception.Comment
                    + "\n\rException Id: " + _Exception.IdException
                    + "\n\rProcess Name: " + _Exception.AssociateTask.Parent.LanguageOption.Title
                    + "\n\rTask Name: " + _Exception.AssociateTask.LanguageOption.Title
                    + "\n\rDescription: " + _Exception.AssociateTask.LanguageOption.Description + " " + _Exception.AssociateExecution.Date;
            }
        }

        public override Int64 IdError
        { get { return 0; } }

        internal NotificationMessageValueOutOfRange(IA.Entities.ExceptionProcessTask exception)
            : base ()
        {
            _Exception = exception;
        }
    }
}
