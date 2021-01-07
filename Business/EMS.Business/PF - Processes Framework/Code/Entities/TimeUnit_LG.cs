using System;
using System.Collections.Generic;
using System.Text;

namespace Condesus.EMS.Business.PF.Entities
{
    public class TimeUnit_LG
    {
        #region Internal Properties
        private String _IdLanguage;
        private String _Name;
        #endregion

        #region External Properties
        public String Name
        {
            get { return _Name; }
        }

        public DS.Entities.Language Language
        {
            get { return Condesus.EMS.Business.DS.Collections.Languages.Options()[_IdLanguage]; }
        }
        #endregion

        internal TimeUnit_LG(String idLanguage, String name)
        {
            _IdLanguage = idLanguage;
            _Name = name;
        }
    }
}