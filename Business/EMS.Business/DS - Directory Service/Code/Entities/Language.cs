using System;
using System.Collections.Generic;
using System.Text;

namespace Condesus.EMS.Business.DS.Entities
{
    public class Language
    {
        #region Internal Properties
            private String _IdLanguage;
            private String _Name;
            private Boolean _IsDefault;
            private Boolean _Enable;
        #endregion

        #region External Properties
            public String IdLanguage
            {
                get { return _IdLanguage; }
            }
            public String Name
            {
                get { return _Name; }
            }
            public Boolean IsDefault
            {
                get { return _IsDefault; }
            }
            public Boolean Enable
            {
                get { return _Enable; }
            }
        #endregion


        public Language(String idLanguage, String name, Boolean isDefault, Boolean enable)
        {
            _IdLanguage = idLanguage;
            _Name = name;
            _IsDefault = isDefault;
            _Enable = enable;
        }
    }
}
