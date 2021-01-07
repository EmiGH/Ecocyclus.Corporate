using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.KC
{
    public class KnowledgeCollaboration : IModule
    {
        #region Internal Properties
            private Credential _Credential;
        #endregion

        #region External Properties
            public String ModuleName
            {
                get { return "KC"; }
            }

                #region MAP
                public Entities.MapKC Map
                {                    
                    get 
                    {
                        //valida permisos de view
                        //if (!new Security.Authority(_Credential).Authorize(Common.Security.MapKC, _Credential.User.IdPerson, Common.Permissions.View))
                        //{ throw new UnauthorizedAccessException(Common.Resources.Errors.AuthenticationFailed); }
                        return new Entities.MapKC(_Credential); 
                    }
                }
                #endregion

                #region CONFIGURATION
                public Entities.ConfigurationKC Configuration
                {
                    get 
                    {
                        //valida permisos de view                     
                        //if (!new Security.Authority(_Credential).Authorize(Common.Security.ConfigurationKC, _Credential.User.IdPerson, Common.Permissions.View))
                        //{ throw new UnauthorizedAccessException(Common.Resources.Errors.AuthenticationFailed); }
                        return new Entities.ConfigurationKC(_Credential); 
                    }
                }
                #endregion

        #endregion

        internal KnowledgeCollaboration(Credential credential)
        {
            _Credential = credential;
        }

    }
}
