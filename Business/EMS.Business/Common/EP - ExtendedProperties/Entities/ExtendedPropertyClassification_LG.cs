using System;
using System.Collections.Generic;
using System.Text;

namespace Condesus.EMS.Business.EP.Entities
{
    public class ExtendedPropertyClassification_LG
    {
        #region Internal Properties
            private String _IdLanguage;
            private String _Name;
            private String _Description;
        #endregion

        #region External Properties
            public String Name
            {
                get { return _Name; }
            }

            public String Description
            {
                get { return _Description; }
            }
            public DS.Entities.Language Language
            {
                get { return Condesus.EMS.Business.DS.Collections.Languages.Options()[_IdLanguage]; }
            }
        #endregion

            internal ExtendedPropertyClassification_LG(String idLanguage, String name, String description)
        {
            _IdLanguage = idLanguage;
            _Name = name;
            _Description = description;
        }
    }
}
