using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.DS.Entities
{
    public class ApplicabilityContactType
    {
        #region Internal Properties
            private Credential _Credential;
            private Int64 _IdApplicabilityContactType;   
            private Entities.ApplicabilityContactType_LG _LanguageOption; //Objeto con los datos dependientes del idioma actual elegido por el usuario               
            private Collections.ApplicabilityContactTypes_LG _LanguagesOptions;
            private Dictionary<Int64, ContactType> _ContactTypes; 
        #endregion

        #region External Properties
            public Int64 IdApplicabilityContactType
            {
                get { return _IdApplicabilityContactType; }
            }
            public ApplicabilityContactType_LG LanguageOption
            {
                get { return _LanguageOption; }
            }
            public Collections.ApplicabilityContactTypes_LG LanguagesOptions
            {
                get
                {
                    if (_LanguagesOptions == null)
                    {
                        _LanguagesOptions = new Collections.ApplicabilityContactTypes_LG(_IdApplicabilityContactType, _Credential);
                    }
                    return _LanguagesOptions;
                }
            }
            public Dictionary<Int64, ContactType> ContactTypes
            {
                get 
                {
                    if (_ContactTypes == null)
                    { _ContactTypes = new Collections.ContactTypes(this._IdApplicabilityContactType, _Credential).ItemsByApplicability(); }
                    return _ContactTypes;
                }
            }

        #endregion

        internal ApplicabilityContactType(Int64 idApplicabilityContactType, String name, Credential credential)
        {
            _Credential = credential;
            _IdApplicabilityContactType = idApplicabilityContactType;
            //Carga el nombre para el lenguage seleccionado
            _LanguageOption = new ApplicabilityContactType_LG(_Credential.DefaultLanguage.IdLanguage, name);
        }
    }
}
