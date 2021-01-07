using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.DS.Entities
{
    public class ContactType
    {
        #region Internal Properties
            private Credential _Credential;
            private Int64 _IdContactType; //Identificador del tipo de contacto
            private Int64 _Applicability; //Tipo de uso del tipo de contacto                      
            private Entities.ContactType_LG _LanguageOption; //Objeto con los datos dependientes del idioma actual elegido por el usuario
            private Collections.ContactTypes_LG _LanguagesOptions; //Objeto que contiene todas las opciones de idioma
        #endregion

        #region External Properties
            public Collections.ContactTypes_LG LanguagesOptions
            {
                get
                {
                    if (_LanguagesOptions == null)
                    {
                        //Carga la coleccion de lenguages de ese tipo de contacto
                        _LanguagesOptions = new Collections.ContactTypes_LG(_IdContactType, _Credential);
                    }
                    return _LanguagesOptions;
                }
            }
            public ContactType_LG LanguageOption
            {
                get { return _LanguageOption; }
            }
            public Int64 IdContactType
            {
                get { return _IdContactType; }
            }
            public Int64 Applicability
            {
                get { return _Applicability; }
            }
        #endregion

        internal ContactType(Int64 idContactType, String idLanguage, Int64 applicability, String name, String description, Credential credential)
        {
            _Credential = credential;
            //Aca directamente se asigna el valor a las propiedades sin pasar por la DB
            _IdContactType = idContactType;
            _Applicability = applicability;         
            //Carga el nombre para el lenguage seleccionado
            _LanguageOption = new ContactType_LG(idLanguage, name, description);
        }

    }

}
