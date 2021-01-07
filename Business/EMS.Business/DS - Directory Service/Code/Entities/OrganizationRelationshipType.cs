using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.DS.Entities
{
    public class OrganizationRelationshipType
    {
        #region Internal Properties
            private Credential _Credential;
            private Int64 _IdOrganizationRelationshipType;//Identificador del tipo derelacion de la organizacion
            private Entities.OrganizationRelationshipType_LG _LanguageOption; //Objeto con los datos dependientes del idioma actual elegido por el usuario
            private Collections.OrganizationsRelationshipsTypes_LG _LanguagesOptions;
        #endregion

        #region External Properties
            public Int64 IdOrganizationRelationshipType
            {
                get { return _IdOrganizationRelationshipType; }
            }
            public OrganizationRelationshipType_LG LanguageOption
            {
                get { return _LanguageOption; }
            }
            public Collections.OrganizationsRelationshipsTypes_LG LanguagesOptions
            {
                get
                {
                    if (_LanguagesOptions == null)
                    {
                        //Carga la coleccion de lenguages de ese tipo de contacto
                        _LanguagesOptions = new Collections.OrganizationsRelationshipsTypes_LG(_IdOrganizationRelationshipType, _Credential);
                    }
                    return _LanguagesOptions;
                }
            }
        #endregion

        internal OrganizationRelationshipType(Int64 idOrganizationRelationshipType, String idLanguage, String name, String description, Credential credential)
        {
            _Credential = credential;
            //Aca directamente se asigna el valor a las propiedades sin pasar por la DB
            _IdOrganizationRelationshipType = idOrganizationRelationshipType;

            _LanguageOption = new OrganizationRelationshipType_LG(idLanguage, name, description);
        }

    }
}
