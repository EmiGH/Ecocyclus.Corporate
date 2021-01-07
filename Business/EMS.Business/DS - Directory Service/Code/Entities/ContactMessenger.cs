using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.DS.Entities
{
    public class ContactMessenger
    {
        #region Internal Properties
            private Credential _Credential;
            private Int64 _IdContactMessenger;
            private Int64 _IdContactType;
            private String _Provider;
            private String _Application;
            private String _Data;            
            private Entities.ContactType _ContactType;
        #endregion

        #region External Properties
            public Int64 IdContactMessenger
            {
                get { return _IdContactMessenger; }
            }
            public String Provider
            {
                get { return _Provider; }
            }
            public String Application
            {
                get { return _Application; }
            }
            public String Data
            {
                get { return _Data; }
            }
            public Entities.ContactType ContactType
            {
                get
                {
                    Collections.ApplicabilityContactTypes oApplicabilityContactTypes = new Condesus.EMS.Business.DS.Collections.ApplicabilityContactTypes(_Credential);

                    if (_ContactType == null)
                    {
                        //_ContactType = new Collections.ContactTypes(oApplicabilityContactTypes.Item("Direccion").IdApplicabilityContactType,_Credential).Item(_IdContactType);
                        _ContactType = new Collections.ContactTypes(Common.Constants.ContactTypeMessenger, _Credential).Item(_IdContactType);
                    }
                    return _ContactType;
                }
            }
        #endregion

        internal ContactMessenger(Int64 idContactMessenger, String provider, String application, String data, Int64 idContactType, Credential credential)
        {
            _Credential = credential;
            _IdContactMessenger = idContactMessenger;
            _Provider = provider;
            _Application = application;
            _Data = data;
            _IdContactType = idContactType;
        }
    }
}
