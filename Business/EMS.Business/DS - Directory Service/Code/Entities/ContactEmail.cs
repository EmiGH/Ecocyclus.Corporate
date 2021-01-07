using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.DS.Entities
{
    public class ContactEmail
    {
        #region Internal Properties
            private Credential _Credential;
            private Int64 _IdContactEmail;
            private Int64 _IdContactType;
            private String _Email;  
            private Entities.ContactType _ContactType;
        #endregion

        #region External Properties
            public Int64 IdContactEmail
            {
                get { return _IdContactEmail; }            
            }
            public String Email
            {
                get { return _Email; }            
            }
            public Entities.ContactType ContactType
            {
                get
                {
                    Collections.ApplicabilityContactTypes oApplicabilityContactTypes = new Condesus.EMS.Business.DS.Collections.ApplicabilityContactTypes(_Credential);
                    if (_ContactType == null)
                    {
                        //_ContactType = new Collections.ContactTypes(oApplicabilityContactTypes.Item("Email").IdApplicabilityContactType, _Credential).Item(_IdContactType);
                        _ContactType = new Collections.ContactTypes(Common.Constants.ContactTypeEmail, _Credential).Item(_IdContactType);
                    }
                    return _ContactType;
                }
            }
        #endregion

        internal ContactEmail(Int64 idContactEmail, String email, Int64 idContactType, Credential credential)
        {
            _Credential = credential;
            _IdContactEmail = idContactEmail;
            _Email = email;
            _IdContactType = idContactType;            
        }
    }
}
