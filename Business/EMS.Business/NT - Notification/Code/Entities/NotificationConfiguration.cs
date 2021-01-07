using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Condesus.EMS.Business.NT.Entities
{
    public class NotificationConfiguration
    {
        #region Internal Properties
        private String _EmailSender;
        private String _Host;
        private String _UserName;
        private String _Password;
        private Double _Interval;
        #endregion

        #region External Properties
        public String EmailSender
        {
            get { return _EmailSender; }
        }
        public String Host
        {
            get { return _Host; }
        }
        public String UserName
        {
            get { return _UserName; }
        }
        public String Password
        {
            get { return _Password; }
        }
        public Double Interval
        {
            get { return _Interval; }
        }
        #endregion

        internal NotificationConfiguration(String emailSender, String host, String userName, String password, Double interval)
        {
            _EmailSender = emailSender;
            _Host = host;
            _UserName = userName;
            _Password = password;
            _Interval = interval;
        }

        public void Modify(String emailSender, String host, String userName, String password, Double interval)
        {
            new Collections.NotificationConfigurations().Modify(emailSender, host, userName, password, interval);
        }

    }
}
