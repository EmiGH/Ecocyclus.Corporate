using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.DS.Entities
{
    //public class Country
    //{
    //    #region Internal Properties
    //        private Credential _Credential;
    //        private Int64 _IdCountry; //Identificador del pais
    //        private String _Alpha; //Codigo alpha del pais
    //        private String _InternationalCode; //Codigo internacional telefónico
    //        private Entities.Country_LG _LanguageOption; //Objeto con los datos dependientes del idioma actual elegido por el usuario
    //        private Collections.Countries_LG _LanguagesOptions;
    //    #endregion

    //    #region External Properties
    //        public Collections.Countries_LG LanguagesOptions
    //        {
    //            get 
    //            {
    //                if (_LanguagesOptions == null)
    //                {
    //                    //Carga la coleccion de lenguages de ese pais
    //                    _LanguagesOptions = new Collections.Countries_LG(_IdCountry, _Credential);
    //                }
    //                return _LanguagesOptions;
    //            }
    //        }
    //        public Country_LG LanguageOption 
    //        {
    //            get { return _LanguageOption; }
    //        }
    //        public String Alpha
    //        {
    //            get { return _Alpha; }
    //        }
    //        public Int64 IdCountry
    //        {
    //            get { return _IdCountry; }
    //        }
    //        public String InternationalCode
    //        {
    //            get { return _InternationalCode; }
    //        }
    //    #endregion

    //    internal Country(Int64 IdCountry, String IdLanguage, String Alpha, String Name, String InternationalCode, Credential credential)
    //    {
    //        _Credential = credential;
    //        //Aca directamente se asigna el valor a las propiedades sin pasar por la DB
    //        _IdCountry = IdCountry;
    //        _Alpha = Alpha;                  
    //        _InternationalCode = InternationalCode; 
    //        //Carga el nombre para el lenguage seleccionado
    //        _LanguageOption = new Country_LG(IdLanguage, Name);            
    //    }
     //}
}
