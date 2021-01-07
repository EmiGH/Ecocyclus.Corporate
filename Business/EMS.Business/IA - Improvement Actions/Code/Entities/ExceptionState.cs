using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.IA.Entities
{
    public abstract class ExceptionState
    {
        #region Internal Properties
        private Credential _Credential;            
        private Int64 _IdExceptionState;
        private ExceptionState_LG _LanguageOption;
        private Collections.ExceptionStates_LG _LanguagesOptions;
        #endregion

        #region External Properties
        public Int64 IdExceptionState
        {
            get { return _IdExceptionState; }
        }
        public ExceptionState_LG LanguageOption
        {
            get { return _LanguageOption; }
        }
        public Collections.ExceptionStates_LG LanguagesOptions
        {
            get
            {
                if (_LanguagesOptions == null)
                {
                    //Carga la coleccion de lenguages de ese pais
                    _LanguagesOptions = new Collections.ExceptionStates_LG(_IdExceptionState, _Credential);
                }
                return _LanguagesOptions;
            }
        }

        internal Credential Credential
        {
            get { return _Credential; }
        }
        #endregion

        internal ExceptionState(Int64 idExceptionState, String idLanguage, String name, Credential credential)
        {
            _Credential = credential;
            _IdExceptionState = idExceptionState;
            _LanguageOption = new ExceptionState_LG(idLanguage, name);
        }

        internal abstract ExceptionState Treat(Int64 idException, String comment);

        internal abstract ExceptionState Close(Int64 idException, String comment);
    }
}
