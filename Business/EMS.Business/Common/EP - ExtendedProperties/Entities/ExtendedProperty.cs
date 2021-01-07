using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.EP.Entities
{
    public class ExtendedProperty
    {
        #region Internal Properties   
            private Credential _Credential;
            private Int64 _IdExtendedProperty;
            private Int64 _IdExtendedPropertyClassification;
            private Entities.ExtendedProperty_LG _LanguageOption; //Objeto con los datos dependientes del idioma actual elegido por el usuario
            private Collections.ExtendedProperties_LG _LanguagesOptions;
        #endregion

        #region External Properties
            internal Credential Credential
            {
                get { return _Credential; }
            }
            public Int64 IdExtendedProperty
            {
                get { return _IdExtendedProperty; }
            }
            public Collections.ExtendedProperties_LG LanguagesOptions
            {
                get
                {
                    if (_LanguagesOptions == null)
                    {
                        //Carga la coleccion de lenguages de ese pais
                        _LanguagesOptions = new Collections.ExtendedProperties_LG(_IdExtendedProperty, _Credential);
                    }
                    return _LanguagesOptions;
                }
            }
            public ExtendedProperty_LG  LanguageOption
            {
                get { return _LanguageOption; }
            }
            public ExtendedPropertyClassification ExtendedPropertyClassification
            {
                get { return new Collections.ExtendedPropertyClassifications(_Credential).Item( _IdExtendedPropertyClassification); }
            }
        #endregion

            internal ExtendedProperty(Int64 idExtendedProperty, Int64 idExtendedPropertyClassification, String name, String description, String idLanguage, Credential credential)
        {
            _Credential = credential;
            _IdExtendedProperty = idExtendedProperty;
            _IdExtendedPropertyClassification = idExtendedPropertyClassification;         
            //Carga el nombre para el lenguage seleccionado
            _LanguageOption = new ExtendedProperty_LG(idLanguage, name, description);
        }
    }
}
