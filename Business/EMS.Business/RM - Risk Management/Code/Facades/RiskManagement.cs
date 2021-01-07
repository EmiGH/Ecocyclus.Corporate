using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.RM
{
    public class RiskManagement : IModule
    {
        #region Internal Properties
            private Credential _Credential;
        #endregion

        #region External Properties

            public String ModuleName
            {
                get { return "RM"; }
            }

                #region MAP
                public Entities.MapRM Map
                {
                    get { return new Entities.MapRM(_Credential); }
                }
                #endregion

                #region CONFIGURATION
                public Entities.ConfigurationRM Configuration
                {
                    get { return new Entities.ConfigurationRM(_Credential); }
                }
                #endregion

        #endregion

        internal RiskManagement(Credential credential)
        {
            _Credential = credential;
        }

    }

}
