using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PA.Entities
{
    public class ParameterGroup : IExtendedProperty
    {
        #region Internal Properties   
        private Credential _Credential;
        private Int64 _IdParameterGroup;
        private Int64 _IdIndicator;
        private Indicator _Indicators;
        private Dictionary<Int64, Parameter> _Parameters;        
        private Entities.ParameterGroup_LG _LanguageOption; //Objeto con los datos dependientes del idioma actual elegido por el usuario
        private Collections.ParameterGroups_LG _LanguagesOptions;
        #endregion

        #region External Properties
        internal Credential Credential
        {
            get { return _Credential; }
        }
        public Int64 IdParameterGroup
        {
            get { return _IdParameterGroup; }
        }
        public Collections.ParameterGroups_LG LanguagesOptions
        {
            get
            {
                if (_LanguagesOptions == null)
                {
                    //Carga la coleccion de lenguages de ese pais
                    _LanguagesOptions = new Collections.ParameterGroups_LG(_IdIndicator , _IdParameterGroup, _Credential);
                }
                return _LanguagesOptions;
            }
        }
        public ParameterGroup_LG LanguageOption
        {
            get { return _LanguageOption; }
        }
        public Indicator Indicator
        {
            get
            {
                if (_Indicators == null)
                { _Indicators = new Collections.Indicators(_Credential).Item(_IdIndicator); }
                return _Indicators;
            }
        }

        #region Parameters
        /// <summary>
        /// Parametro de este grupo
        /// </summary>
        /// <param name="idParameter"></param>
        /// <returns></returns>
        public Parameter Parameter(Int64 idParameter)
        {
            return new Collections.Parameters(this, _Credential).Item(idParameter);
        }
        /// <summary>
        /// Lista de parametros del grupo
        /// </summary>
        public Dictionary<Int64, Parameter> Parameters
        {
            get
            {
                if (_Parameters == null) { _Parameters = new Collections.Parameters(this, _Credential).Items(); }
                return _Parameters;
            }
        }
        /// <summary>
        /// Alta de un parametro para este grupo
        /// </summary>
        /// <param name="decription"></param>
        /// <param name="sign"></param>
        /// <param name="raiseException"></param>
        /// <returns></returns>
        public Parameter ParameterAdd(String decription, String sign, Boolean raiseException)
        {
            return new Collections.Parameters(this, _Credential).Add(decription, sign, raiseException);
        }
        /// <summary>
        /// Borra un parametro de este grupo
        /// </summary>
        /// <param name="parameter"></param>
        public void Remove(Parameter parameter)
        {
            new Collections.Parameters(this, _Credential).Remove(parameter);
        }
        /// <summary>
        /// Borra todos los parametros para este grupo
        /// </summary>
        public void Remove()
        {
            Collections.Parameters _parameters = new Condesus.EMS.Business.PA.Collections.Parameters(this,_Credential);
            foreach (Parameter _parameter in Parameters.Values)
            {
                _parameters.Remove(_parameter);
            }
        }
        #endregion

        #region Extended Properties
        private List<EP.Entities.ExtendedPropertyValue> _ExtendedPropertyValue; //puntero a extended properties          
        public List<EP.Entities.ExtendedPropertyValue> ExtendedPropertyValues
        {
            get
            {
                if (_ExtendedPropertyValue == null)
                { _ExtendedPropertyValue = new EP.Collections.ExtendedPropertyValues(this).Items(); }
                return _ExtendedPropertyValue;
            }
        }
        public EP.Entities.ExtendedPropertyValue ExtendedPropertyValue(Int64 idExtendedProperty)
        {
            return new EP.Collections.ExtendedPropertyValues(this).Item(idExtendedProperty);
        }
        public void ExtendedPropertyValueAdd(EP.Entities.ExtendedProperty extendedProperty, String value)
        {
            new EP.Collections.ExtendedPropertyValues(this).Add(extendedProperty, value);
        }
        public void Remove(EP.Entities.ExtendedPropertyValue extendedPropertyValue)
        {
            new EP.Collections.ExtendedPropertyValues(this).Remove(extendedPropertyValue);
        }
        public void ExtendedPropertyValueModify(EP.Entities.ExtendedPropertyValue extendedPropertyValue, String value)
        {
            new EP.Collections.ExtendedPropertyValues(this).Modify(extendedPropertyValue, value);
        }
        #endregion
        #endregion

        internal ParameterGroup(Int64 idParameterGroup, Int64 idIndicator, String name, String description, String idLanguage, Credential credential)
        {
            _Credential = credential;
            _IdParameterGroup = idParameterGroup;
            _IdIndicator = idIndicator;         
            //Carga el nombre para el lenguage seleccionado
            _LanguageOption = new ParameterGroup_LG(idLanguage, name, description);
        }

        public void Modify(String name, String description)
        {
            new PA.Collections.ParameterGroups(_Credential).Modify(this, name, description);
        }
    }
}
