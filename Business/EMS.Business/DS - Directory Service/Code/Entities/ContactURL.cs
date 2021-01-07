using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.DS.Entities
{
    public class ContactURL
    {
        #region Internal Properties
            private Credential _Credential;
            private Int64 _IdContactURL;//Identificador de la url de contacto
            private Int64 _IdContactType;//Identificador del tipo de contacto
            private String _URL;//Direccion url

            private Entities.ContactURL_LG _LanguageOption; //Objeto con los datos dependientes del idioma actual elegido por el usuario
            private Collections.ContactURLs_LG _LanguagesOptions;
            private Entities.ContactType _ContactType;            
        #endregion

        #region External Properties
            public Int64 IdContactURL
            {
                get { return _IdContactURL; }            
            }
            public String URL
            {
                get { return _URL; }            
            }
            public Entities.ContactType ContactType
            {
                get
                {
                    Collections.ApplicabilityContactTypes oApplicabilityContactTypes = new Condesus.EMS.Business.DS.Collections.ApplicabilityContactTypes(_Credential);
                    if (_ContactType == null)
                    {
                        _ContactType = new Collections.ContactTypes(Common.Constants.ContactTypeUrl, _Credential).Item(_IdContactType);
                    }
                    return _ContactType;
                }
            }
            public ContactURL_LG LanguageOption
            {
                get { return _LanguageOption; }
            }
            public Collections.ContactURLs_LG LanguagesOptions
            {
                get
                {
                    if (_LanguagesOptions == null)
                    {
                        //Carga la coleccion de lenguages de ese pais
                        _LanguagesOptions = new Collections.ContactURLs_LG(_IdContactURL,_Credential);
                    }
                    return _LanguagesOptions;
                }
            }
        #endregion

            internal ContactURL(Int64 idContactURL, String idLanguage, String url, String name, String description, Int64 idContactType, Credential credential)
        {
            _Credential = credential;
            _IdContactURL = idContactURL;
            _URL = url;
            _IdContactType = idContactType;

            _LanguageOption = new ContactURL_LG(idLanguage, name, description);
        }

      
    }
}
