using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.PA.Entities
{
    public class ConstantClassification
    {
        #region Internal Region
        private Credential _Credential; 
        private Int64 _IdConstantClassification;
        private Int64 _IdParentConstantClassification;
        private ConstantClassification _Parent;
        private Dictionary<Int64, ConstantClassification> _Childrens;        
        private ConstantClassification_LG _LanguageOption;//puntero a lenguages
        private Dictionary<String, Entities.ConstantClassification_LG> _LanguagesOptions;//puntero a lenguajes
        private Dictionary<Int64, PA.Entities.Constant> _Constants;
        #endregion

        #region External Region
        internal Credential Credential
        {
            get { return _Credential; }
        }
        public Int64 IdConstantClassification
        {
            get { return _IdConstantClassification; }
        }
        public Int64 IdParentConstantClassification
        {
            get { return _IdParentConstantClassification; }
        }
        public ConstantClassification Parent
        {
            get
            {
                if (_Parent == null)
                { _Parent = new Collections.ConstantClassifications(this).Item(_IdParentConstantClassification); }
                return _Parent;
            }
        }

        public Dictionary<Int64, ConstantClassification> Childrens
        {
            get
            {
                if (_Childrens == null)
                { _Childrens = new Collections.ConstantClassifications(this).Items(); }
                return _Childrens;
            }
        }        

        #region LanguageOptions
        public ConstantClassification_LG LanguageOption
        {
            get
            {
                return _LanguageOption;
            }
        }
        public Dictionary<String, Entities.ConstantClassification_LG> LanguagesOptions
        {
            get
            {
                if (_LanguagesOptions == null)
                {
                    //Carga la coleccion de lenguages de es posicion
                    _LanguagesOptions = new Collections.ConstantClassifications_LG(this).Items();
                }

                return _LanguagesOptions;
            }
        }
        public ConstantClassification_LG LanguageCreate(DS.Entities.Language language, String name, String description)
        {
            return new Collections.ConstantClassifications_LG(this).Create(language, name, description);
        }
        public void LanguageModify(DS.Entities.Language language, String name, String description)
        {
            new Collections.ConstantClassifications_LG(this).Update(language, name, description);
        }
        public void LanguageRemove(DS.Entities.Language language)
        {
            new Collections.ConstantClassifications_LG(this).Delete(language);
        }    
        #endregion

        #region Constants
        public Dictionary<Int64, PA.Entities.Constant> Constants
        {
            get
            {
                if (_Constants == null)
                { _Constants = new PA.Collections.Constants(this).Items(); }
                return _Constants;
            }
        }
        public PA.Entities.Constant Constant(Int64 idConstant)
        {
            return new PA.Collections.Constants(this).Item(idConstant);
        }
        public PA.Entities.Constant ConstantAdd(String symbol, Double value, MeasurementUnit measurementUnit, String name, String description, PA.Entities.ConstantClassification constsantClassification)
        {
            return new PA.Collections.Constants(this).Create(symbol, value, measurementUnit, name, description, this);
        }
        public void Remove(Entities.Constant constant)
        {
            new PA.Collections.Constants(this).Delete(constant);
        }
        #endregion

        

        //Borra dependencias
        internal void Remove()
        {
            foreach (ConstantClassification _item in Childrens.Values)
            {
                new Collections.ConstantClassifications(this).Delete(_item);
            }
            foreach (Constant _item in Constants.Values)
            {
                Remove(_item);                
            }
            new Collections.ConstantClassifications_LG(this).Delete();
        }

        #endregion

        internal ConstantClassification(Int64 idConstantClassification, Int64 idParentConstantClassification, String idLanguage, String name, String description, Credential credential)
        {
            _IdConstantClassification = idConstantClassification;
            _IdParentConstantClassification = idParentConstantClassification;
            _Credential = credential;
            _LanguageOption = new ConstantClassification_LG(idLanguage, name, description);
        }

        public void Modify(ConstantClassification parent, String name, String description)
        {
            using (TransactionScope _TransactionScope = new TransactionScope())
            {
                new Collections.ConstantClassifications(this).Update(this, parent, name, description);
                _TransactionScope.Complete();
            }
        }
    }
}
