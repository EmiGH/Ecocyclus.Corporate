using System;
using System.Collections.Generic;
using System.Text;

namespace Condesus.EMS.Business.PA.Entities
{
    public class Calculation_LG
    {
        #region Internal Properties
        private string _IdLanguage;
        private string _Name;
        private string _Description;
        #endregion

        #region External Properties
        public string Name
        {
            get { return _Name; }
        }

        public string Description
        {
            get { return _Description; }
        }
        public DS.Entities.Language Language
        {
            get { return Condesus.EMS.Business.DS.Collections.Languages.Options()[_IdLanguage]; }
        }
        #endregion

        internal Calculation_LG(String idLanguage, String name, String description)
        {
            _IdLanguage = idLanguage;
            _Name = name;
            _Description = description;
        }
    }
}
