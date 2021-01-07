using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.DS.Entities
{
    public class SalutationType
    {
        #region Internal Properties
            private Credential _Credential;
            private Int64 _IdSalutationType;
            private Entities.SalutationType_LG _LanguageOption; //Objeto con los datos dependientes del idioma actual elegido por el usuario       
            private Collections.SalutationTypes_LG _LanguagesOptions;
        #endregion

        #region External Properties
            public Int64 IdSalutationType
            {
                get { return _IdSalutationType; }
            }
            public SalutationType_LG LanguageOption
            {
                get { return _LanguageOption; }
            }
            public Collections.SalutationTypes_LG LanguagesOptions
            {
                get
                {
                    if (_LanguagesOptions == null)
                    {
                        _LanguagesOptions = new Condesus.EMS.Business.DS.Collections.SalutationTypes_LG(_IdSalutationType, _Credential);
                    }
                    return _LanguagesOptions;
                }
            }
            
        #endregion

        internal SalutationType(Int64 idSalutationType, String idLanguage, String name, String description, Credential credential)
        {
            _Credential = credential;
            //Aca directamente se asigna el valor a las propiedades sin pasar por la DB
            _IdSalutationType = idSalutationType;
            
            //Carga el nombre para el lenguage seleccionado
            _LanguageOption = new SalutationType_LG(idLanguage, name, description);
        }
    }
}
