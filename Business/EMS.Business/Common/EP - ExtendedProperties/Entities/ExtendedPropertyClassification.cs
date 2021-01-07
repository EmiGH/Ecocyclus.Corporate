using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.EP.Entities
{
    public class ExtendedPropertyClassification
    {
        #region Internal Properties    
        private Credential _Credential;
            private Int64 _IdExtendedPropertyClassification;
            private Dictionary<Int64,ExtendedProperty> _ExtendedProperties;            
            private Entities.ExtendedPropertyClassification_LG _LanguageOption; //Objeto con los datos dependientes del idioma actual elegido por el usuario
            private Collections.ExtendedPropertyClassifications_LG _LanguagesOptions;
        #endregion

        #region External Properties

            #region Extended Properties

                public ExtendedProperty ExtendedProperty(Int64 idExtendedProperty)
                {
                    return new Collections.ExtendedProperties(_Credential).Item(idExtendedProperty);
                }
                //grupo de propiedades extendidas que pertenecen a esta clasificacion
                public Dictionary<Int64, ExtendedProperty> ExtendedProperties
                {
                    get 
                    {
                        if (_ExtendedProperties == null)
                        {
                            _ExtendedProperties = new Collections.ExtendedProperties(_Credential).ItemsByClassification(_IdExtendedPropertyClassification);
                        }
                        return _ExtendedProperties;
                    }

                }
                public ExtendedProperty ExtendedPropertiesAdd(String name, String description)
                {
                    return new Collections.ExtendedProperties(_Credential).Add(_IdExtendedPropertyClassification, name, description);
                }
                public void ExtendedPropertiesModify(Int64 idExtendedProperty, String name, String description)
                {
                    new Collections.ExtendedProperties(_Credential).Modify(idExtendedProperty, _IdExtendedPropertyClassification, name, description);
                }
                public void ExtendedPropertiesRemove(Int64 idExtendedProperty)
                {
                    new Collections.ExtendedProperties(_Credential).Remove(idExtendedProperty);
                }

            #endregion

            public Collections.ExtendedPropertyClassifications_LG LanguagesOptions
            {
                get
                {
                    if (_LanguagesOptions == null)
                    {
                        //Carga la coleccion de lenguages de ese pais
                        _LanguagesOptions = new Collections.ExtendedPropertyClassifications_LG(_IdExtendedPropertyClassification,_Credential);
                    }
                    return _LanguagesOptions;
                }
            }
            public ExtendedPropertyClassification_LG LanguageOption
            {
                get { return _LanguageOption; }
            }
            public Int64 IdExtendedPropertyClassification
            {
                get { return _IdExtendedPropertyClassification; }
            }
        #endregion

            internal ExtendedPropertyClassification(Int64 idExtendedPropertiesClassification, String name, String description, String idLanguage, Credential credential)
        {
            _Credential = credential;
            _IdExtendedPropertyClassification = idExtendedPropertiesClassification;            
            //Carga el nombre para el lenguage seleccionado
            _LanguageOption = new ExtendedPropertyClassification_LG(idLanguage, name, description);

        }

        public void Modify(String name, String description)
        {
            //Realiza las validaciones de autorizacion 
            new Security.Authority(_Credential).Authorize(Common.Security.ConfigurationPF, 0, _Credential.User.IdPerson, Common.Permissions.Manage);
            new EP.Collections.ExtendedPropertyClassifications(_Credential).Modify(_IdExtendedPropertyClassification, name, description);
        }

    }
}
