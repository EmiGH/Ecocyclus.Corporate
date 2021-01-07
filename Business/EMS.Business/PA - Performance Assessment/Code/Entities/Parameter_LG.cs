using System;
using System.Collections.Generic;
using System.Text;

namespace Condesus.EMS.Business.PA.Entities
{
    public class Parameter_LG
    {
         #region Internal Properties
            private String _IdLanguage;
            private String _Description;
        #endregion

        #region External Properties
            public String Description
            {
                get { return _Description; }
            }
            public DS.Entities.Language Language
            {
                get { return Condesus.EMS.Business.DS.Collections.Languages.Options()[_IdLanguage]; }
            }
        #endregion

            internal Parameter_LG(String idLanguage, String description)
        {
            _IdLanguage = idLanguage;
            _Description = description;
        }
    }
}
