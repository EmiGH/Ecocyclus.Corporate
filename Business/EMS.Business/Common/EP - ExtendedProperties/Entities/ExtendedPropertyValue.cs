using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.EP.Entities
{
    public class ExtendedPropertyValue
    {
        #region Internal Properties
            private Credential _Credential;
            private Int64 _IdExtendedProperty;
            private String _Value;     
        #endregion

        #region External Properties
            public String Value
            {
                get { return _Value; }
            }
            
            public EP.Entities.ExtendedProperty ExtendedProperty
            {
                get
                {
                    return new EP.Collections.ExtendedProperties(_Credential).Item(_IdExtendedProperty);
                }
            }
        #endregion

            internal ExtendedPropertyValue(Int64 idExtendedProperty, String value, Credential credential)
        {
            _Credential = credential;
            _IdExtendedProperty = idExtendedProperty;
            _Value = value;
        }

    }
}
