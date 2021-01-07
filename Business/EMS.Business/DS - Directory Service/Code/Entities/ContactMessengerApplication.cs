using System;
using System.Collections.Generic;
using System.Text;

namespace Condesus.EMS.Business.DS.Entities
{
    public class ContactMessengerApplication
    {
        #region Internal Properties
            private String _Provider;
            private String _Application;        
        #endregion

        #region External Properties
            public String Provider
            {
                get { return _Provider; }
            }
            public String Application
            {
                get { return _Application; }
            }
        #endregion

        internal ContactMessengerApplication(String provider, String application)
        {
            _Provider = provider;
            _Application = application;                    
        }

    }
}
