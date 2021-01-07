using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.KC.Entities
{
    public class ResourceHistoryState
    {
        #region Internal Region
        private Int64 _IdResourceFileState;
        private Credential _Credential;
        private ResourceHistoryState_LG _LanguageOption;
        private Collections.ResourceHistoryStates_LG _LanguagesOptions;
        #endregion

        #region External Reagion
        public Int64 IdResourceFileState
        {
            get { return _IdResourceFileState; }
        }
        public ResourceHistoryState_LG LanguageOption
        {
            get { return _LanguageOption; }
        }
        public Collections.ResourceHistoryStates_LG LanguagesOptions
        {
            get
            {
                if (_LanguagesOptions == null)
                { _LanguagesOptions = new Collections.ResourceHistoryStates_LG(_IdResourceFileState, _Credential); }
                return _LanguagesOptions;
            }
        }
        #endregion

        internal ResourceHistoryState(Int64 idResourceFileState, String name, String description, Credential credential)
        {
            _IdResourceFileState = idResourceFileState;
            _Credential = credential;
            _LanguageOption = new ResourceHistoryState_LG(_Credential.CurrentLanguage.IdLanguage, name, description);
        }

        public void Modify(String name, String description)
        {
            //Realiza las validaciones de autorizacion 
            new Security.Authority(_Credential).Authorize(Common.Security.ConfigurationKC, 0, _Credential.User.IdPerson, Common.Permissions.Manage);
            new KC.Collections.ResourceHistoryStates(_Credential).Modify(_IdResourceFileState, name, description);
        }

    }
}
