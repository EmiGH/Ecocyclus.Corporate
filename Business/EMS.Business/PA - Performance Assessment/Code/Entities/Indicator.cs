using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.PA.Entities

{
    public class Indicator : IExtendedProperty
    {
        #region Internal Properties
            private Credential _Credential;
            private Int64 _IdIndicator;
            private Int64 _IdMagnitud;
            private Boolean _IsCumulative;
            //private Int64 _IdClassification;
            private IndicatorClassification _Classification;
            private Magnitud _Magnitud;
            private Dictionary<Int64, Entities.Measurement> _Measurements;
            private Entities.Indicator_LG _LanguageOption; //Objeto con los datos dependientes del idioma actual elegido por el usuario
            private Collections.Indicators_LG _LanguagesOptions;
            private Dictionary<Int64, Entities.ParameterGroup> _ParameterGroups;
            private Dictionary<Int64, Formula> _Formulas;
        #endregion

        #region External Properties
            internal Credential Credential
            {
                get { return _Credential; }
            }
            public Int64 IdIndicator
            {
                get { return _IdIndicator; }
            }
            public Boolean IsCumulative
            {
                get { return _IsCumulative; }
            }
            public Collections.Indicators_LG LanguagesOptions
            {
                get
                {
                    if (_LanguagesOptions == null)
                    {
                        //Carga la coleccion de lenguages de ese pais
                        _LanguagesOptions = new Collections.Indicators_LG(this, _Credential);
                    }
                    return _LanguagesOptions;
                }
            }
            public Indicator_LG LanguageOption
            {
                get { return _LanguageOption; }
            }
            public IndicatorClassification Classification(Int64 idIndicatorClassification)
            {
                if (_Classification == null)
                {
                    _Classification = new Collections.IndicatorClassifications(_Credential).Item(idIndicatorClassification); 
                }
                return _Classification;
            }
            public Dictionary<Int64, IndicatorClassification> Classifications
            {
                get
                {
                    return new Collections.IndicatorClassifications(_Credential).Items(this);
                }
            }
            public Magnitud Magnitud
            {
                get
                {
                    if (_Magnitud == null)
                    {
                        _Magnitud = new Collections.Magnitudes(_Credential).Item(_IdMagnitud);
                    }
                    return _Magnitud;
                }
            }
            public Dictionary<Int64, Entities.Measurement> Measurements
            {
                get
                {
                    if (_Measurements == null)
                    {
                        _Measurements = new Collections.Measurements(_Credential).Items(this);
                    }
                    return _Measurements;
                }
            }

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

            #region CalulateOfTransformation
            internal Dictionary<Int64, CalculateOfTransformation> Transformations
            {
                get { return new Collections.CalculateOfTransformations(this).Items(); }
            }
            #endregion

            #region Formulas
            public Dictionary<Int64, Formula> Formulas
            {
                get
                {
                    if (_Formulas == null)
                    { _Formulas = new PA.Collections.Formulas(_Credential).ItemsByIndicator(_IdIndicator); }
                    return _Formulas;
                }
            }
            #endregion

        #endregion

            internal Indicator(Int64 idIndicator, Int64 idMagnitud, Boolean IsCumulative, String name, String description, String scope, String limitation, String definition, String idLanguage, Credential credential)
        {
            _Credential = credential;
            _IdIndicator = idIndicator;
            _IdMagnitud = idMagnitud;
            _IsCumulative = IsCumulative;
            //Carga el nombre para el lenguage seleccionado
            _LanguageOption = new Indicator_LG(idLanguage, name, description, scope, limitation,definition);
        }

        public void Modify(Entities.Magnitud magnitud, String name, String description, String scope, String limitation, String definition, Dictionary<Int64, PA.Entities.IndicatorClassification> indicatorClassifications)
            {
                //Realiza las validaciones de autorizacion 
                new Security.Authority(_Credential).Authorize(Common.Security.ConfigurationPA, 0, _Credential.User.IdPerson, Common.Permissions.Manage);
                //aca es donde se arma una transaccion y primero se modifica el indicador,
                //borra las asociaciones de indicadores y por ultimo agrega las relaciones nuevas.
                using (TransactionScope _transactionScope = new TransactionScope())
                {
                    //Hace el update sobre el indicador.
                    new PA.Collections.Indicators(_Credential).Modify(this, magnitud,name, description, scope, limitation, definition, indicatorClassifications);
                    
                    _transactionScope.Complete();
                }

            }


        #region ParameterGroups
            /// <summary>
            /// Lista de Grupos de parametros de este indicador
            /// </summary>
            /// <returns></returns>
            public Dictionary<Int64, Entities.ParameterGroup> ParameterGroups
            {
                get
                {
                    if (_ParameterGroups == null)
                    { _ParameterGroups = new PA.Collections.ParameterGroups(this, _Credential).Items(); }
                    return _ParameterGroups;
                }
            }
            /// <summary>
            /// Grupos de parametros de este indicador
            /// </summary>
            /// <param name="idParameterGroup"></param>
            /// <returns></returns>
            public Entities.ParameterGroup ParameterGroup(Int64 idParameterGroup)
            {
                return new PA.Collections.ParameterGroups(this,_Credential).Item(idParameterGroup);
            }
            /// <summary>
            /// Alta de un grupo de parametros
            /// </summary>
            /// <param name="name"></param>
            /// <param name="description"></param>
            /// <returns></returns>
            public Entities.ParameterGroup ParameterGroupAdd(String name, String description)
            {
                return new PA.Collections.ParameterGroups(this,_Credential).Add(name, description);
            }
            /// <summary>
            /// Borra un grupo de parametros
            /// </summary>
            /// <param name="idParameterGroup"></param>
            public void Remove(Entities.ParameterGroup parameterGroup)
            {
                new PA.Collections.ParameterGroups(this,_Credential).Remove(parameterGroup);
            }
            /// <summary>
            /// Borra todos los parameterGroup de este indicator
            /// </summary>
            public void Remove()
            {
                foreach (ParameterGroup _ParameterGroup in ParameterGroups.Values)
                {
                    new PA.Collections.ParameterGroups(this, _Credential).Remove(_ParameterGroup);    
                }
                //Borra los extended properties
                foreach (EP.Entities.ExtendedPropertyValue _extendedPropertyValue in ExtendedPropertyValues)
                {
                    this.Remove(_extendedPropertyValue);
                }

                
            }
           
        #endregion

  
    }
}
