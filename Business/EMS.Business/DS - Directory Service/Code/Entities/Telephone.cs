using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.DS.Entities
{
    public class Telephone
    {
             #region Internal Properties
            private Credential _Credential;
            private Int64 _IdTelephone;
            private String _AreaCode;
            private String _Number;
            private String _Extension;
            private String _InternationalCode;
            private String _Reason;
        #endregion

        #region External Properties
            public Int64 IdTelephone
            {
                get { return _IdTelephone; }
            }
            public String AreaCode
            {
                get { return _AreaCode; }
            }
            public String Number
            {
                get { return _Number; }
            }
            public String Extension
            {
                get { return _Extension; }
            }
            public String InternationalCode
            {
                get { return _InternationalCode; }
            }
            public String Reason
            {
                get { return _Reason; }
            }
            
        #endregion

            internal Telephone(Int64 idTelephone, String areaCode, String number, String extension, String internationalCode, String reason, Credential credential) 
        {
            _Credential = credential;
            _IdTelephone = idTelephone;
            _AreaCode = areaCode;
            _Number = number;
            _Extension = extension;
            _InternationalCode = internationalCode;
            _Reason = reason;
        }

            public void Modify(GIS.Entities.Site facility, String areaCode, String number, String extension, String internationalCode, String reason)
            {
                new Collections.Telephones(_Credential).Modify(this, facility, areaCode, number, extension, internationalCode, reason);
            }

            public void Modify(DS.Entities.Person person, String areaCode, String number, String extension, String internationalCode, String reason)
            {
                new Collections.Telephones(_Credential).Modify(this, person, areaCode, number, extension, internationalCode, reason);
            }

    }
}
