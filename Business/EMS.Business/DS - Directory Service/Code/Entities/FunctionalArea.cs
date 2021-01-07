using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.DS.Entities
{
    public class FunctionalArea
    {
        #region Internal Properties
            private Credential _Credential;      
            private Int64 _IdFunctionalArea;
            private Int64 _IdOrganization;     
            private String _Mnemo;
            private Int64 _IdParentFunctionalArea; //Id del area funcional padre
            private Entities.FunctionalArea_LG _LanguageOption; //Objeto con los datos dependientes del idioma actual elegido por el usuario
            private Entities.Organization _Organization;
            private Collections.FunctionalAreas_LG _LanguagesOptions; //Coleccion con los datos dependientes del idioma actual elegido por el usuario
            private Dictionary<Int64, Entities.FunctionalArea> _Children; //Coleccion de hijas
        #endregion

        #region External Properties
            internal Credential Credential
            {
                get { return _Credential; }
            }
            public Int64 IdFunctionalArea
            {
                get { return _IdFunctionalArea; }
            }
            public Int64 IdOrganization
            {
                get { return _IdOrganization; }
            }
            public Int64 IdParentFunctionalArea
            {
                get { return _IdParentFunctionalArea; }
            }
            public String Mnemo
            {
                get { return _Mnemo; }
            }
            public FunctionalArea_LG LanguageOption
            {
                get { return _LanguageOption; }
            }
            public Collections.FunctionalAreas_LG  LanguagesOptions
            {
                get
                {
                    if (_LanguagesOptions == null)
                    {
                        //Carga la coleccion de lenguages de es posicion
                        _LanguagesOptions = new Collections.FunctionalAreas_LG(_IdFunctionalArea, _IdOrganization, _Credential);
                    }

                    return _LanguagesOptions;
                }
            }
            public Dictionary<Int64, Entities.FunctionalArea> Children
            {
                get
                {
                    if (_Children == null)
                    {
                        _Children = new Collections.FunctionalAreas(this, Organization).Items();
                    }

                    return _Children;
                }
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

            /// <summary>
            /// borra sus functional areas hijas, NOOO relaciones con otros objetos
            /// </summary>
            internal void Remove()
            {
                foreach (FunctionalArea _FunctionalAreaChild in Children.Values)
                {
                    new Collections.FunctionalAreas(Organization).Remove(_FunctionalAreaChild);
                }
            }
        #endregion

        internal FunctionalArea(Int64 idFunctionalArea, Int64 idOrganization, Int64 idParentFunctionalArea, String name, String mnemo, Credential credential) 
        {
            _Credential = credential;
            _IdFunctionalArea = idFunctionalArea;
            _IdOrganization = idOrganization;
            _Mnemo = mnemo;
            _IdParentFunctionalArea = idParentFunctionalArea;
            //Carga el nombre para el lenguage seleccionado
            _LanguageOption = new FunctionalArea_LG(name, _Credential.CurrentLanguage.IdLanguage); 
        }

    }
}
