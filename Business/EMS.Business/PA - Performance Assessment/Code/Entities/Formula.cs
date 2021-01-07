using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Transactions;
using System.Data.SqlClient;
using System.Data;

namespace Condesus.EMS.Business.PA.Entities
{
    public class Formula : IExtendedProperty
    {
        #region Internal Properties
        private Int64 _IdFormula;
        private DateTime _CreationDate;
        private String _LiteralFormula;
        private String _SchemaSP;
        private FormulaStoredProcedure _FormulaStoredProcedure;
        private Int64 _IdIndicator;
        private Indicator _Indicator;
        private Int64 _IdMeasurementUnit;
        private MeasurementUnit _MeasurementUnit;
        private Credential _Credential;
        private Formula_LG _LanguageOption;
        private Collections.Formulas_LG _LanguageOptions;
        private Dictionary<Int64, FormulaParameter> _FormulaParameters;
        private Dictionary<Int64, Calculation> _Calculations;
        private Int64 _IdResourceVersion;
        private KC.Entities.ResourceVersion _ResourceVersion;
        #endregion

        #region External Properties
        internal Credential Credential
        {
            get { return _Credential; }
        }
        public Int64 IdFormula
        {
           get{return _IdFormula;}
        }
        public DateTime CreationDate
        {
            get { return _CreationDate; }
        }
        public String LiteralFormula
        {
            get { return _LiteralFormula; }
        }
        public FormulaStoredProcedure SchemaSP
        {
            get 
            {
                if (_FormulaStoredProcedure == null)
                { _FormulaStoredProcedure = new FormulaStoredProcedure(_SchemaSP, _Credential); }
                return _FormulaStoredProcedure; 
            }
        }
        public Indicator Indicator
        {
            get
            {
                if (_Indicator == null)
                { _Indicator = new Collections.Indicators(_Credential).Item(_IdIndicator); }
                return _Indicator;
            }           
        }
        public MeasurementUnit MeasurementUnit
        {
            get
            {
                if (_MeasurementUnit == null) { _MeasurementUnit = new Collections.MeasurementUnits(_Credential).Item(_IdMeasurementUnit); }
                return _MeasurementUnit;
            }
        }

        public Formula_LG LanguageOption
        {
            get{return _LanguageOption;}
        }
        public Collections.Formulas_LG  LanguageOptions
        {
            get
            {
                if (_LanguageOptions == null)
                { _LanguageOptions = new Collections.Formulas_LG(_IdFormula, _Credential); }
                return _LanguageOptions;
            }           
        }
       
        #region Formula Parameters
        public PA.Entities.FormulaParameter FormulaParameter(Int64 PositionParameter, Int64 IdIndicator, Int64 IdMeasurementUnit, String ParameterName)
        {
            return new PA.Entities.FormulaParameter(_IdFormula, PositionParameter, IdIndicator, IdMeasurementUnit, ParameterName, _Credential);
        }
        public Dictionary<Int64, FormulaParameter> FormulaParameters
        {
            get 
            {
                if (_FormulaParameters == null)
                { _FormulaParameters = new Collections.FormulaParameters(_Credential).Items(_IdFormula); }
                return _FormulaParameters;
            }
        }

        public void FormulaParameterAdd(Int64 PositionParameter, PA.Entities.Indicator indicator, PA.Entities.MeasurementUnit MeasurementUnit, String ParameterName)
        {
            new PA.Collections.FormulaParameters(_Credential).Add(this, PositionParameter, indicator, MeasurementUnit, ParameterName);
        }
        public void FormulaParameterRemove(Int64 positionParameter)
        {
            new PA.Collections.FormulaParameters(_Credential).Remove(_IdFormula, positionParameter);
        }
        #endregion

        #region Calculations
                public Dictionary<Int64, Calculation> Calculations()
                {
                        if(_Calculations == null)
                        { _Calculations = new PA.Collections.Calculations(_Credential).ItemsByFormula(_IdFormula); }
                        return _Calculations;            
                }
                public Boolean ExistCalculation
                {
                    get
                    {                        
                        return new PA.Collections.Formulas(_Credential).HasCalculation(_IdFormula);
                    }
                }
            #endregion

        #region ResourceVersion
                public KC.Entities.ResourceVersion ResourceVersion
                {
                    get
                    {
                        if (_ResourceVersion == null)
                        {
                            _ResourceVersion = (KC.Entities.ResourceVersion) new KC.Collections.Resources(_Credential).Item(_IdResourceVersion);
                        }
                        return _ResourceVersion;
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

        internal Formula(Int64 idFormula, DateTime creationDate, String literalFormula, String schemaSP, Int64 idIndicator, Int64 idMeasurementUnit, String name, String description, Int64 idResourceVersion, Credential credential)
        {
            _IdFormula = idFormula;
            _CreationDate = creationDate;
            _LiteralFormula = literalFormula;
            _SchemaSP = schemaSP;
            _IdIndicator = idIndicator;
            _IdMeasurementUnit = idMeasurementUnit;
            _Credential = credential;
            _IdResourceVersion = idResourceVersion;
            _LanguageOption = new Formula_LG(_Credential.CurrentLanguage.IdLanguage, name, description);
        }

        public void Modify(String literalFormula, String schemaSP, Entities.Indicator indicator, Entities.MeasurementUnit measurementUnit, String name, String description, DataTable parameters, KC.Entities.ResourceVersion resourceVersion)
        {
            using (TransactionScope _TransactionScope = new TransactionScope())
            {
                new Collections.Formulas(_Credential).Modify(_IdFormula, literalFormula, schemaSP, indicator, measurementUnit, name, description, parameters, resourceVersion);
                _TransactionScope.Complete();
            }
        }

    }
}
