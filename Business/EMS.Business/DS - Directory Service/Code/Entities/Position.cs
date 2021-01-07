using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.DS.Entities
{
    public class Position
    {
        #region Internal Properties
            private Credential _Credential;
            private Int64 _IdPosition;
            private Int64 _IdOrganization;
            private String _IdLanguage;
            private Organization _Organization;
            private Entities.Position_LG _LanguageOption; //Objeto con los datos dependientes del idioma actual elegido por el usuario
            private Collections.Positions_LG _LanguagesOptions; //Coleccion con los datos dependientes del idioma actual elegido por el usuario
        #endregion

        #region External Properties
            public Int64 IdPosition
            {
                get { return _IdPosition; }
            }
            public Int64 IdOrganization
            {
                get { return _IdOrganization; }
            }
            public Position_LG LanguageOption
            {
                get { return _LanguageOption; }
            }
            public Entities.Organization Organization
            {
                get
                {
                    if (_Organization == null)
                    {
                        _Organization = new Collections.Organizations(_Credential).Item(_IdOrganization);
                    }

                    return _Organization;
                }
            }
            public Collections.Positions_LG  LanguagesOptions
            {
                get
                {
                    if (_LanguagesOptions == null)
                    {
                        //Carga la coleccion de lenguages de es posicion
                        _LanguagesOptions = new Collections.Positions_LG(_IdPosition, _IdOrganization, _Credential);
                    }

                    return _LanguagesOptions;
                }
            }
        #endregion

        internal Position(Int64 idPosition, Int64 idOrganization, String name, String description, String idLanguage, Credential credential) 
        {
            _Credential = credential;
            _IdPosition = idPosition;
            _IdOrganization = idOrganization;
            _IdLanguage = idLanguage;
            //Carga el nombre para el lenguage seleccionado
            _LanguageOption = new Position_LG(name, description, idLanguage); 
        }
    }
}
