using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.IA.Entities
{
    public class ExceptionResourceVersion : Exception
    {
        private KC.Entities.Version _AssociateAchive;
        private List<NT.Entities.NotificationMessage> _NotificationMessages;
        
        internal ExceptionResourceVersion(Int64 idException, Int64 idExceptionType, Int64 idExceptionState, String comment, DateTime exceptionDate, Credential credential) 
        : base (idException, idExceptionType, idExceptionState, comment, exceptionDate, credential)
        {

        }

        #region version 
        public KC.Entities.Version AssociateAchive
        {
            get
            {
                if (_AssociateAchive == null)
                { _AssociateAchive = new KC.Collections.Versions(this).Item(this); }
                return _AssociateAchive;
            }
        }
        #endregion

        public override List<NT.Entities.NotificationMessage> NotificationMessages
        {
            get
            {
                _NotificationMessages = new List<Condesus.EMS.Business.NT.Entities.NotificationMessage>();

                NT.Entities.NotificationMessage _NotificationMessage = new NT.Entities.NotificationMessageDocumentExpiration(this);

                _NotificationMessages.Add(_NotificationMessage);

                return _NotificationMessages;
                
            }
        }

    }
}
