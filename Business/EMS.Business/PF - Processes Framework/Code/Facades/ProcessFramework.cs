using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PF
{
    public class ProcessFramework : IModule
    {
         #region Internal Properties
            private Credential _Credential;              
        #endregion

         #region External Properties

            public String ModuleName
            {
                get { return "PF"; }
            }

            #region MAP
            public Entities.MapPF Map
            {
                get { return new Entities.MapPF(_Credential); }
            }
            #endregion

            #region CONFIGURATION
            public Entities.ConfigurationPF Configuration
            {
                get { return new Entities.ConfigurationPF(_Credential); }
            }
            #endregion

            #endregion

            internal ProcessFramework(Credential credential)
        { _Credential = credential; }
    }
}
