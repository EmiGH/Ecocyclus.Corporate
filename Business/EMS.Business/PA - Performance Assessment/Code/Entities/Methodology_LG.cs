using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PA.Entities
{
    public class Methodology_LG
    {
        #region Internal Region
        private String _IdLanguage;
        private String _MethodName;
        private String _MethodType;
        private String _Description;
        #endregion

        #region External Region

        public DS.Entities.Language Language
        {
            get { return Condesus.EMS.Business.DS.Collections.Languages.Options()[_IdLanguage]; }
        }
        public String MethodName
        {
            get { return _MethodName; }
        }

        public String MethodType
        {
            get { return _MethodType; }
        }

        public String Description
        {
            get { return _Description; }
        }
        #endregion

        #region Constructor
        internal Methodology_LG(String idlanguage, String methodName, String methodType, String description)
        {
            _IdLanguage = idlanguage;
            _MethodName = methodName;
            _MethodType = methodType;
            _Description = description;
        }
        #endregion
    }
        
}
