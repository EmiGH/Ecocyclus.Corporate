using System;
using System.Collections.Generic;
using System.Text;

namespace Condesus.EMS.Business.PA.Entities
{
    public class Indicator_LG
    {
        #region Internal Properties
        private String _IdLanguage;
        private String _Name;
        private String _Description;
        private String _Scope;
        private String _Limitation;
        private String _Definition;
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
        public String Scope
        {
            get { return _Scope; }
        }
        public String Limitation
        {
            get { return _Limitation; }
        }
        public String Definition
        {
            get { return _Definition; }
        }
        public DS.Entities.Language Language
        {
            get { return Condesus.EMS.Business.DS.Collections.Languages.Options()[_IdLanguage]; }
        }
        #endregion

        internal Indicator_LG(String idLanguage, String name, String description, String scope, String limitation, String definition)
        {
            _IdLanguage = idLanguage;
            _Name = name;
            _Description = description;
            _Scope = scope;
            _Limitation = limitation;
            _Definition = definition;
        }
    }
}
