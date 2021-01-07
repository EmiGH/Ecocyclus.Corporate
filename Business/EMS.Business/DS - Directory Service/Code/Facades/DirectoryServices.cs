using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Data;

namespace Condesus.EMS.Business.DS
{
    public class DirectoryServices : IModule
    {
        #region Internal Properties
            private Credential _Credential;
        #endregion

        #region External Properties
            public String ModuleName
            { 
                get { return "DS"; } 
            }

            #region MAP
            public Entities.MapDS Map
            {
                get { return new Entities.MapDS(_Credential); }
            }
            #endregion

            #region CONFIGURATION
            public Entities.ConfigurationDS Configuration
            {
                get { return new Entities.ConfigurationDS(_Credential); }
            }
            #endregion

            #region Languages
                public static Dictionary<String, Entities.Language> LanguagesOptions()
                {
                    return Collections.Languages.Options();
                }        
            #endregion

        #endregion

        internal DirectoryServices(Credential credential)
        {
            _Credential = credential;
        }
    }
}
