using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Condesus.EMS.Business.Security
{
    public class Security
    {
        #region Internal Properties
            private Credential _Credential;
        #endregion

        internal Security(Credential credential)
            { _Credential = credential; }

        #region External Methods

       
        #region Permissions
        public Dictionary<Int64, Entities.Permission> Permissions()
            {
                return new Collections.Permissions(_Credential).Items();
            }
            public Entities.Permission Permission(Int64 idPermission)
            {
                return new Collections.Permissions(_Credential).Item(idPermission);
            }
        #endregion

        #endregion

  


    }
}
