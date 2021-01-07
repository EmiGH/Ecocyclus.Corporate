using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Condesus.EMS.Business.NT.Entities
{    
    public class NotificationMessageDocumentExpiration : NotificationMessage
    {
        #region Internal Properties
        private IA.Entities.ExceptionResourceVersion _Exception;
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
                foreach (NotificationRecipient _item in _Exception.AssociateAchive.NotificationRecipient)
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
        { get { return Common.Resources.ConstantMessage.Subject_NotificationMessageDocumentExpiration; } }

        public override String Body
        {
            get
            {
                return "Exception Comment: " + _Exception.Comment
                    + "\n\rException Id: " + _Exception.IdException
                    + "\n\rResource Version Name: " + _Exception.AssociateAchive.ResourceVersion.LanguageOption.Title
                    + "\n\rDescription: " + _Exception.AssociateAchive.ResourceVersion.LanguageOption.Description
                    + "\n\rValid From: " + _Exception.AssociateAchive.ValidFrom
                    + "\n\rValid Through: " + _Exception.AssociateAchive.ValidThrough
                    + "\n\rVersion Number: " + _Exception.AssociateAchive.VersionNumber;
            }
        }

        public override Int64 IdError
        { get { return 0;} }

        internal NotificationMessageDocumentExpiration(IA.Entities.ExceptionResourceVersion exception)
            : base()
        {
            _Exception = exception;
        }
    }
}
