using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PF.Entities
{
    public class ParticipationType
    {
        #region Internal Properties
            private Credential _Credential;
            private Int64 _IdParticipationType;            
            private ParticipationType_LG _LanguageOption;
            private Collections.ParticipationTypes_LG _LanguagesOptions;           
        #endregion

        #region External Properties
            public Int64 IdParticipationType
            {
                get { return _IdParticipationType; }
            }
            
            public ParticipationType_LG LanguageOption
            {
                get { return _LanguageOption; }             
            }
            public Collections.ParticipationTypes_LG LanguagesOptions
            {
                get 
                {
                    if (_LanguagesOptions == null)
                    { _LanguagesOptions = new Collections.ParticipationTypes_LG(_IdParticipationType, _Credential); }
                    return _LanguagesOptions; 
                }
            }

        #endregion

            internal ParticipationType(Int64 idParticipationType, String name, Credential credential) 
        {
            _Credential = credential;
            _IdParticipationType = idParticipationType;
            _LanguageOption = new ParticipationType_LG(_Credential.CurrentLanguage.IdLanguage, name);
        }

            public void Modify(String name)
            {
                //Realiza las validaciones de autorizacion 
                new Security.Authority(_Credential).Authorize(Common.Security.ConfigurationPF, 0, _Credential.User.IdPerson, Common.Permissions.Manage);

                new PF.Collections.ParticipationTypes(_Credential).Modify(_IdParticipationType, name);
            }

    }
}
