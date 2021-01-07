using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.DS.Entities
{
    // public class GeographicArea
    //{

    //    #region Internal Properties     
    //        private Credential _Credential;
    //        private Int64 _IdGeographicArea;//Id del area geografica
    //        private Int64 _IdOrganization;//ID de la organizacion a la que pertenece
    //        private Int64 _IdParentGeographicArea; //Id del area geografica padre
    //        private Organization _Organization;
    //        private Entities.GeographicArea_LG _LanguageOption; //Objeto con los datos dependientes del idioma actual elegido por el usuario
    //        private Collections.GeographicAreas_LG _LanguagesOptions; //Opciones de Idioma
    //        private Dictionary<Int64, GeographicArea> _Children; //Areas geograficas hijas
    //    #endregion

    //    #region External Properties
    //        public Int64 IdGeographicArea
    //        {
    //            get { return _IdGeographicArea ; }
    //        }
    //        public Int64 IdOrganization
    //        {
    //            get { return _IdOrganization; }
    //        }
    //        public Organization Organization
    //        {
    //            get
    //            {
    //                if (_Organization == null)
    //                { _Organization = new Collections.Organizations(_Credential).Item(_IdOrganization); }
    //                return _Organization;
    //            }
    //        }
    //        public Int64 IdParentGeographicArea
    //        {
    //            get { return _IdParentGeographicArea; }
    //        }
    //        public GeographicArea_LG LanguageOption
    //        {
    //            get { return _LanguageOption; }
    //        }
    //        public Collections.GeographicAreas_LG LanguagesOptions
    //        {
    //            get
    //            {
    //                if (_LanguagesOptions == null) 
    //                {
    //                    _LanguagesOptions = new Collections.GeographicAreas_LG(_IdGeographicArea, _IdOrganization, _Credential);
    //                }
    //                return _LanguagesOptions;
    //            }
    //        }
    //        public Dictionary<Int64,GeographicArea> Children
    //        {
    //            get
    //            {
    //                if (_Children == null)
    //                {
    //                    _Children = new Collections.GeographicAreas(this,Organization).Items();
    //                }
    //                return _Children;
    //            }
    //        }
    //     /// <summary>
    //     /// Borra sus dependencias
    //     /// </summary>
    //        internal void Remove()
    //        {
    //            foreach (GeographicArea _geographicArea in Children.Values)
    //            {
    //                new Collections.GeographicAreas(Organization).Remove(_geographicArea);
    //            }
    //        }
    //    #endregion

    //    internal GeographicArea(Int64 idGeographicArea, Int64 idOrganization, Int64 idParentGeographicArea, String name, String description, Credential credential)
    //    {
    //        _Credential = credential;
    //        _IdGeographicArea = idGeographicArea;
    //        _IdOrganization = idOrganization;
    //        _IdParentGeographicArea = idParentGeographicArea;
    //        //Carga el nombre para el lenguage seleccionado
    //        _LanguageOption = new GeographicArea_LG(_Credential.CurrentLanguage.IdLanguage , name, description);
    //    }
    //}
}
