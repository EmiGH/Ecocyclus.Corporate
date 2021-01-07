using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.IA.Entities
{
    public class ExceptionType
    {
        #region Internal Properties
        private Int64 _IdExceptionType;
        private Credential _Credential;    
        private ExceptionType_LG _LanguageOption;
        #endregion

        #region external Properties
        public Int64 IdExceptionType
        {
            get { return _IdExceptionType; }
        }
        public ExceptionType_LG LanguageOption
        {
            get { return _LanguageOption; }
        }
        #endregion
        internal ExceptionType(Int64 idExceptiontype, String idLanguage, String name, String description, Credential credential)
        {
            _Credential = credential;
            _IdExceptionType = idExceptiontype;
            _LanguageOption = new ExceptionType_LG(idLanguage, name, description);
        }


    }
}
