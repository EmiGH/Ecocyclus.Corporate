using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Condesus.EMS.Business.Security.Entities
{
    public class Permission
    {
        #region Internal Properties
        private Int64 _IdPermission;
        private Credential _Credential;
        private Permission_LG _LanguageOption;
        private Dictionary<String, Permission_LG> _LanguagesOptions;        
        #endregion

        #region External Properties
        internal Credential Credential
        {
            get { return _Credential; }
        }
        public Int64 IdPermission
        {
            get { return _IdPermission; }
        }
        #region Languages Options
        public Permission_LG LanguageOption
        {
            get { return _LanguageOption; }
        }
        public Dictionary<String, Permission_LG> LanguagesOptions
        {
            get
            {
                if (_LanguagesOptions == null)
                { _LanguagesOptions = new Collections.Permissions_LG(_IdPermission, _Credential).Items(); }
                return _LanguagesOptions;
            }
        }
        

        #endregion
        
        #endregion

        internal Permission(Int64 idPermission, String name, String description, Credential credential)
        {
            _IdPermission = idPermission;
            _Credential = credential;
            _LanguageOption = new Permission_LG(_Credential.CurrentLanguage.IdLanguage, name, description);
        }

    }
}
