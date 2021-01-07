using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PA.Entities
{
    public class Parameter
    {
        #region Internal Properties
            private Credential _Credential;
            private Int64 _IdParameter;
            private Int64 _IdParameterGroup;
            private String _Sign;
            private Boolean _RaiseException;
            private ParameterGroup _ParameterGroup;
            private Dictionary<Int64, ParameterRange> _ParameterRanges; 
            private Dictionary<Int64, Indicator> _Indicators;
            private Entities.Parameter_LG _LanguageOption; //Objeto con los datos dependientes del idioma actual elegido por el usuario
            private Collections.Parameters_LG _LanguagesOptions;
        #endregion

        #region External Properties
            public Int64 IdParameter
            {
                get { return _IdParameter; }
            }
            public String Sign
            {
                get { return _Sign; }
            }
            public Boolean RaiseException
            {
                get { return _RaiseException; }
            }

            public Collections.Parameters_LG LanguagesOptions
            {
                get
                {
                    if (_LanguagesOptions == null)
                    {
                        //Carga la coleccion de lenguages de ese pais
                        _LanguagesOptions = new Collections.Parameters_LG(_IdParameter, _Credential);
                    }
                    return _LanguagesOptions;
                }
            }
            public Parameter_LG  LanguageOption
            {
                get { return _LanguageOption; }
            }
            public ParameterGroup ParameterGroup
            {
                get
                { 
                    if (_ParameterGroup == null)
                    {_ParameterGroup = new Collections.ParameterGroups(_Credential).Item(_IdParameterGroup); }
                    return _ParameterGroup;
                }
            }

            #region Parameter Ranges
                /// <summary>
                /// Devuelve un parameterRange que pertenecen a este parametro
                /// </summary>
                /// <param name="idParameterRange"></param>
                /// <returns></returns>
                public ParameterRange ParameterRange(Int64 idParameterRange)
                {
                    return new Collections.ParameterRanges(this, _Credential).Item(idParameterRange);
                }
                /// <summary>
                /// Devuelve un diccionario de parametersRange que pertenecen a este parametro
                /// </summary>
                public Dictionary<Int64, ParameterRange> ParameterRanges
                {
                    get
                    {
                        if (_ParameterRanges  == null) { _ParameterRanges  = new Collections.ParameterRanges(this, _Credential).Items(); }
                        return _ParameterRanges;
                    }
                }
                /// <summary>
                /// Alta de un parameterrange
                /// </summary>
                /// <param name="lowValue"></param>
                /// <param name="HighValue"></param>
                /// <returns></returns>
                public ParameterRange ParameterRangeAdd(Double lowValue, Double HighValue)
                {
                    return new Collections.ParameterRanges(this, _Credential).Add(lowValue, HighValue);
                }               
                /// <summary>
                /// Borrra el parameter range enviado
                /// </summary>
                /// <param name="parameterRange"></param>
                public void Remove(ParameterRange parameterRange)
                {
                    new Collections.ParameterRanges(this, _Credential).Remove(parameterRange);
                }
                /// <summary>
                /// Borra todas los parameterrange de este parametro
                /// </summary>
                public void Remove()
                {
                    new Collections.ParameterRanges(this, _Credential).Remove();
                }
            #endregion
        #endregion

        internal Parameter(Int64 idParameter, Int64 idParameterGroup, String description, String sign, Boolean raiseException, String idLanguage, Credential credential)
        {
            _Credential = credential;
            _IdParameter = idParameter;
            _IdParameterGroup = idParameterGroup;
            _Sign = sign;
            _RaiseException = raiseException;
            //Carga el nombre para el lenguage seleccionado
            _LanguageOption = new Parameter_LG(idLanguage, description);
        }

        public void Modify(String description, String sign, Boolean raiseException)
        {
            new Collections.Parameters(ParameterGroup,_Credential).Modify(this, description,sign,raiseException);
        }
    }
}
