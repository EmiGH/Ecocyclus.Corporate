using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PA.Entities
{
    public class MeasurementUnit
    {
        #region Internal Properties         
            private Credential _Credential;
            private Int64 _IdMeasurementUnit;
            private Int64 _Numerator;
            private Int64 _Denominator;
            private Int64 _Exponent;
            private Decimal _Constant;
            private Boolean _IsPattern;
            private Int64 _IdMagnitud;
            private Magnitud _Magnitud;            
            private Entities.MeasurementUnit_LG _LanguageOption; //Objeto con los datos dependientes del idioma actual elegido por el usuario
            private Collections.MeasurementUnits_LG _LanguagesOptions;
        #endregion

        #region External Properties
            public Int64 IdMeasurementUnit
            {
                get { return _IdMeasurementUnit; }
            }
            public Int64 Numerator
            {
                get { return _Numerator; }
            }
            public Int64 Denominator
            {
                get { return _Denominator; }
            }
            public Int64 Exponent
            {
                get { return _Exponent; }
            }
            public Decimal Constant
            {
                get { return _Constant; }
            }
            public Boolean IsPattern
            {
                get { return _IsPattern; }
            }
            public Magnitud Magnitud
            {
                get 
                { 
                    if(_Magnitud == null)
                    { _Magnitud = new Collections.Magnitudes(_Credential).Item(_IdMagnitud); }
                    return _Magnitud;
                }
            }
            public Collections.MeasurementUnits_LG LanguagesOptions
            {
                get
                {
                    if (_LanguagesOptions == null)
                    {
                        //Carga la coleccion de lenguages de ese pais
                        _LanguagesOptions = new Collections.MeasurementUnits_LG(_IdMeasurementUnit,_Credential);
                    }
                    return _LanguagesOptions;
                }
            }
            public MeasurementUnit_LG  LanguageOption
            {
                get { return _LanguageOption; }
            }           
        #endregion

            internal MeasurementUnit(Int64 idMeasurementUnit, Int64 numerator, Int64 denominator, Int64 exponent, Decimal constant, Boolean isPattern, Int64 idMagnitud, String idLanguage, String name, String description, Credential credential)
            {
                _Credential = credential;
                _IdMeasurementUnit = idMeasurementUnit;
                _Numerator = numerator;
                _Denominator = denominator;
                _Exponent = exponent;
                _Constant = constant;
                _IsPattern = isPattern;
                _IdMagnitud = idMagnitud;
                //Carga el nombre para el lenguage seleccionado
                _LanguageOption = new MeasurementUnit_LG(idLanguage, name, description);
            }
    }
}
