using System;
using System.Collections.Generic;
using System.Text;

namespace Condesus.EMS.Business.DS.Entities
{
    public class ContactMessengerProvider
    {
        #region Internal Properties
            private String _Provider;             
        #endregion

        #region External Properties
            public String Provider
            {
                get { return _Provider; }
            }
        #endregion

        internal ContactMessengerProvider(String provider)
        {
            _Provider = provider;            
        }

    }
}
