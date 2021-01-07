using System;
using System.Collections.Generic;
using System.Text;

namespace Condesus.EMS.Business.PF.Entities
{
    public class Process_LG
    {
        #region Internal Properties
            private String _IdLanguage;//El código ISO del lenguaje
            private String _Title;
            private String _Description;
            private String _Purpose;
        #endregion

        #region External Properties
            public String Description
            {
                get { return _Description; }
            }
            public String Title
            {
                get { return _Title; }
            }
            public String Purpose
            {
                get { return _Purpose; }
            }           
            public DS.Entities.Language Language
            {
                get { return Condesus.EMS.Business.DS.Collections.Languages.Options()[_IdLanguage]; }
            }
        #endregion

        public Process_LG(String idLanguage, String title, String purpose, String description)
        {
            _IdLanguage = idLanguage;
            _Title = title;
            _Purpose = purpose;
            _Description = description;
        }

    }
}
