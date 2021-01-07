using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PA.Entities
{
    public class AccountingScope_LG
    {
        #region Internal Region
        private String _IdLanguage;
        private String _Name;
        private String _Description;
        #endregion

        #region External Region

        public DS.Entities.Language Language
        {
            get { return Condesus.EMS.Business.DS.Collections.Languages.Options()[_IdLanguage]; }
        }
        public String Name
        {
            get { return _Name; }
        }
        public String Description
        {
            get { return _Description; }
        }
        #endregion
        #region Constructor
        internal AccountingScope_LG( String idlanguage, String name, String description)
        {
            _IdLanguage = idlanguage;
            _Name = name;
            _Description = description;
        }

       
        #endregion
    }
}
