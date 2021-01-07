using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Condesus.EMS.Business.NT.Entities
{
    public class NotificationMessageTransformationError : NotificationMessage
    {
        #region Internal Properties
        private PA.Entities.CalculateOfTransformation _CalculateOfTransformation;
        private String _To;
        private String _ErrorDescription;
        private Int64 _IdError;
        private DateTime _ErrorDate;
        #endregion

        public override INotificationReported NotificationReported
        {
            get { return _CalculateOfTransformation; }
        }
        public override String To
        { 
            get 
            { 
                String _to = null;
                foreach(NotificationRecipient _item in _CalculateOfTransformation.NotificationRecipient)
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
        { get { return Common.Resources.ConstantMessage.Subject_NotificationMessageTransformationError; } }

        public override String Body
        {
            get
            {
                return "Calculate of transformation Name: " + _CalculateOfTransformation.Name
                    + "\n\rDescription: " + _CalculateOfTransformation.Description
                    + "\n\rDate Error: " + this.ErrorDate
                    + "\n\rDescription Error: " + this._ErrorDescription;
            }
        }

        public override Int64 IdError
        {
            get { return _IdError; }
        }
        public DateTime ErrorDate
        {
            get { return _ErrorDate; }
        }

        internal NotificationMessageTransformationError(PA.Entities.CalculateOfTransformation calculateOfTransformation, String errorDescription, Int64 idError, DateTime errorDate)
            : base ()
        {
            _CalculateOfTransformation = calculateOfTransformation;
            _ErrorDescription = errorDescription;
            _IdError = idError;
            _ErrorDate = errorDate;
        }
    }
}
