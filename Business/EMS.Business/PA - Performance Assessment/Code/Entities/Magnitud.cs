using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PA.Entities
{
    public class Magnitud
    {
        #region Internal Properties
        private Credential _Credential;
        private Int64 _IdMagnitud;
        private Dictionary<Int64, MeasurementUnit> _MeasurementUnits;
        private Entities.Magnitud_LG _LanguageOption; //Objeto con los datos dependientes del idioma actual elegido por el usuario
        private Collections.Magnitudes_LG _LanguagesOptions;
        #endregion

        #region External Properties
            public Int64 IdMagnitud
            {
                get { return _IdMagnitud; }
            }
            public Collections.Magnitudes_LG LanguagesOptions
            {
                get
                {
                    if (_LanguagesOptions == null)
                    {
                        //Carga la coleccion de lenguages de ese pais
                        _LanguagesOptions = new Collections.Magnitudes_LG(_IdMagnitud, _Credential);
                    }
                    return _LanguagesOptions;
                }
            }
            public Magnitud_LG LanguageOption
        {
            get { return _LanguageOption; }
        }

        #region MeasurementUnits
            public MeasurementUnit MeasurementUnit(Int64 idMeasurementUnit)
            {
                return new Collections.MeasurementUnits(_Credential).Item(idMeasurementUnit);
            }
            public Dictionary<Int64, MeasurementUnit> MeasurementUnits
            {
                get
                {
                    if (_MeasurementUnits == null) { _MeasurementUnits = new Collections.MeasurementUnits(_Credential).Items(_IdMagnitud); }
                    return _MeasurementUnits;
                }
            }
            public MeasurementUnit MeasurementUnitAdd(Int64 numerator, Int64 denominator, Int64 exponent, Decimal constant, Boolean isPattern, String name, String description)
            {
                return new Collections.MeasurementUnits(_Credential).Add(numerator ,denominator, exponent, constant,isPattern,_IdMagnitud,name,description);
            }
            public void MeasurementUnitModify(Int64 idMeasurementUnit, Int64 numerator, Int64 denominator, Int64 exponent, Decimal constant, Boolean isPattern, String name, String decription)
            {
                new Collections.MeasurementUnits(_Credential).Modify(idMeasurementUnit, numerator,denominator, exponent, constant, isPattern, _IdMagnitud, name, decription);
            }
            public void MeasurementUnitRemove(Int64 idMeasurementUnit)
        {
            new Collections.MeasurementUnits(_Credential).Remove(idMeasurementUnit);
        }
        #endregion

        #endregion

        internal Magnitud(Int64 idMagnitud, String name, String idLanguage, Credential credential)
        {
            _Credential = credential;
            _IdMagnitud = idMagnitud;         
            //Carga el nombre para el lenguage seleccionado
            _LanguageOption = new Magnitud_LG(idLanguage, name);
        }

        public void Modify(String name)
        {
            //Realiza las validaciones de autorizacion 
            new Security.Authority(_Credential).Authorize(Common.Security.ConfigurationPA, 0, _Credential.User.IdPerson, Common.Permissions.Manage);
            new Collections.Magnitudes(_Credential).Modify(_IdMagnitud, name);
        }

        /// <summary>
        /// Borra dependencias
        /// </summary>
        internal void Remove()
        {
            foreach (MeasurementUnit _item in this.MeasurementUnits.Values)
            {
                new Collections.MeasurementUnits(_Credential).Remove(this);
            }
        }
 
    }
}
