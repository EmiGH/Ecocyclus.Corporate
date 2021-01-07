using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PF.Entities
{
    public class TimeUnit
    {
        #region Internal Properties
        private Credential _Credential;
        private Int64 _IdTimeUnit;
        private Int64 _Numerator;
        private Int64 _Exponent;
        private Int64 _Denominator;
        private Boolean _IsPattern;
        private Entities.TimeUnit_LG _LanguageOption; //Objeto con los datos dependientes del idioma actual elegido por el usuario
        private Collections.TimeUnits_LG _LanguagesOptions;
        #endregion

        #region External Properties
        public Int64 IdTimeUnit
        {
            get { return _IdTimeUnit; }
        }
        public Int64 Numerator
        {
            get { return _Numerator; }
        }
        public Int64 Exponent
        {
            get { return _Exponent; }
        }
        public Int64 Denominator
        {
            get { return _Denominator; }
        }
        public Boolean IsPattern
        {
            get { return _IsPattern; }
        }

        public Collections.TimeUnits_LG LanguagesOptions
        {
            get
            {
                if (_LanguagesOptions == null)
                {
                    //Carga la coleccion de lenguages de ese pais
                    _LanguagesOptions = new Collections.TimeUnits_LG(_IdTimeUnit, _Credential);
                }
                return _LanguagesOptions;
            }
        }
        public TimeUnit_LG LanguageOption
        {
            get { return _LanguageOption; }
        }

            #region Calulation function
                internal Int64 CalulateTimeUnitPattern(Int64 value)
                {
                    //Retorna el valor pasado por parametro convertido a la unidad patron.
                    return (value * (_Denominator * _Exponent));
                }
            #endregion
        #endregion

        internal TimeUnit(Int64 idTimeUnit, Int64 numerator, Int64 exponent, Int64 denominator, Boolean isPattern, String name, String idLanguage, Credential credential)
        {
            _Credential = credential;
            _IdTimeUnit = idTimeUnit;
            _Numerator = numerator;
            _Exponent = exponent;
            _Denominator = denominator;
            _IsPattern = isPattern;
            //Carga el nombre para el lenguage seleccionado
            _LanguageOption = new TimeUnit_LG(idLanguage, name);
        }
    }
}
