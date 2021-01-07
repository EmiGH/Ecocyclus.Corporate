using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.CT

{
    public class CollaborationTools : IModule
    {
        #region Internal Properties
        private Credential _Credential;
        
        #endregion

        #region External Properties
        public String ModuleName
        {
            get { return "CT"; }
        }

        #region MAP
        public Entities.MapCT Map
        {
            get { return new Entities.MapCT(_Credential); }
        }
        #endregion

        #region CONFIGURATION
        public Entities.ConfigurationCT Configuration
        {
            get { return new Entities.ConfigurationCT(_Credential); }
        }
        #endregion
        #endregion
        internal CollaborationTools(Credential credential)
            {
                _Credential = credential;
            }
       
  

    }
}
