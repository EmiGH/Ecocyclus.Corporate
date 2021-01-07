using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Condesus.EMS.Business.Security;
using System.Data;

namespace Condesus.EMS.Business.PA
{
    public class PerformanceAssessments : IModule
    {

        #region Internal Properties
        private Credential _Credential;
        #endregion

        #region External Properties
        internal Credential Credential
        {
            get { return _Credential; }
        }
        public String ModuleName
        {
            get { return "PA"; }
        }

        #region MAP
        public Entities.MapPA Map
        {
            get { return new Entities.MapPA(this.Credential); }
        }
        #endregion

        #region CONFIGURATION
        public Entities.ConfigurationPA Configuration
        {
            get { return new Entities.ConfigurationPA(this.Credential); }
        }
        #endregion

        #endregion
      
        internal PerformanceAssessments(DS.Entities.User user)
        {
            _Credential = user.Credential;
        }
    }
}
