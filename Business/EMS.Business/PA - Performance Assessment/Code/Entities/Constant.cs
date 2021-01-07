using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.PA.Entities
{
    public class Constant : IOperand, ISerializable
    {
        #region Internal Region
        private Int64 _IdConstant;
        private String _Symbol;
        private Double _Value;        
        private Int64 _IdMeasurementUnit;
        private Int64 _IdConstantClassification;
        private Credential _Credential;
        private Constant_LG _LanguageOption;//puntero a lenguages
        private ConstantClassification _Classification;
        private Dictionary<String, Entities.Constant_LG> _LanguagesOptions;//puntero a lenguajes

        #endregion

        #region External Region
        public Int64 IdConstant
        {
            get { return _IdConstant; }
            
        }
        public String Symbol
        {
            get { return _Symbol; }
        }
        public Double Value
        {
            get { return _Value; }
        }
        public MeasurementUnit MeasurementUnit
        {
            get { return new Collections.MeasurementUnits(_Credential).Item(_IdMeasurementUnit); }
        }
        internal Credential Credential
        {
            get { return _Credential; }
        }

        #region LanguageOptions
        public Constant_LG LanguageOption
        {
            get
            {
                return _LanguageOption;
            }
        }
        public Dictionary<String, Entities.Constant_LG> LanguagesOptions
        {
            get
            {
                if (_LanguagesOptions == null)
                {
                    //Carga la coleccion de lenguages de es posicion
                    _LanguagesOptions = new Collections.Constants_LG(this).Items();
                }

                return _LanguagesOptions;
            }
        }
        public Constant_LG LanguageCreate(DS.Entities.Language language, String name, String description)
        {
            return new Collections.Constants_LG(this).Create(language, name, description);
        }
        public void LanguageRemove(DS.Entities.Language language)
        {
            new Collections.Constants_LG(this).Delete(language);
        }
        public void LanguageModify(DS.Entities.Language language, String name, String description)
        {
            new Collections.Constants_LG(this).Update(language, name, description);
        }
        #endregion

        #region Classification
        public ConstantClassification ConstantClassification
        {
            get
            {
                if (_Classification == null)
                {
                    _Classification = new Collections.ConstantClassifications(this).Item(_IdConstantClassification);
                }
                return _Classification;
            }
        }
        #endregion

        #region ISerializable
        public DateTime MaxDate()
        {
            return DateTime.Now;             
        }
        public DateTime Mindate()
        {
            return DateTime.Now; 
        }
        public MeasurementPoint TotalMeasurement(ref DateTime? firstDateSeries)
        {
          return null;            
        }
        public List<MeasurementPoint> Series()
        {
            return Series(null, null);
        }
        public List<MeasurementPoint> Series(DateTime? startDate, DateTime? endDate)
        {

            MeasurementPoint _point = null;
            List<MeasurementPoint> _points = new List<MeasurementPoint>();

            DateTime _date = startDate == null ? DateTime.Now : Convert.ToDateTime(startDate);
            DateTime _startDate = startDate == null ? DateTime.Now : Convert.ToDateTime(startDate);
            DateTime _endDate = endDate == null ? DateTime.Now : Convert.ToDateTime(endDate);

            _point = new MeasurementPoint(_date, Convert.ToDouble(this.Value), _startDate, _endDate, 0, Credential, 0, 0);
                _points.Add(_point);

            
            return _points;
        }
        #endregion
        //Borra dependencias
        internal void Remove()
        {
            new Collections.Constants_LG(this).Delete();
        }

        #endregion

        #region iOperand
        public Int64 IdObject
        {
            get { return IdConstant; }
        }
        public String ClassName
        {
            get { return "Constant"; }
        }
        public String Name
        {
            get { return _LanguageOption.Name; }
        }
        public String Description
        {
            get { return _LanguageOption.Description; }
        }
        /// <summary>
        /// Devuleve el valor para hacer la operacion en para la fecha solocitada
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public Double OperateValue(Entities.CalculateOfTransformation transformation, DateTime startDate, DateTime endDate)
        {
            return _Value;
        }
        #endregion

        internal Constant(Int64 idConstant, String symbol, Double value, Int64 idMessurementUnit, Int64 idConstantClassification, String idLanguage, String name, String description, Credential credential)
        {
            _IdConstant = idConstant;
            _Symbol = symbol;
            _Value = value;
            _IdMeasurementUnit = idMessurementUnit;
            _IdConstantClassification = idConstantClassification;
            _Credential = credential;
            _LanguageOption = new Constant_LG(idLanguage, name, description);
        }

        public void Modify(String symbol, Double value, MeasurementUnit measurementUnit, String name, String description, PA.Entities.ConstantClassification constantClassification)
        {
            using (TransactionScope _TransactionScope = new TransactionScope())
            {
                new Collections.Constants(this).Update(this,symbol,value,measurementUnit, name, description,constantClassification);
                //borra la transformaciones donde participa
                foreach (CalculateOfTransformation _calculate in new Collections.CalculateOfTransformations(this).Items().Values)
                {
                    new Collections.CalculateOfTransformationResults(_calculate).Remove(_calculate);
                }

                _TransactionScope.Complete();
            }
        }
    }
}
