using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Condesus.EMS.Business.NT.Entities
{
    public abstract class NotificationMessage
    {
        #region Internal Properties
    
        #endregion

        #region External Properties
        public abstract INotificationReported NotificationReported
        {get;}
        public abstract String To
        {get; }
        public String From
        { 
            get 
            { 
                return new Collections.NotificationConfigurations().Item().EmailSender; 
            }
        }
        public abstract String Subject
        { get; }
        public abstract String Body
        { get; }
        public abstract Int64 IdError
        { get; }
        
        #endregion

        public NotificationMessage()
        {
           
        }
    }
}
