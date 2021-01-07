using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.PA.Entities
{
    public class IndicatorClassification 
    {
        #region Internal Properties
        private Credential _Credential;
        private Int64 _IdIndicatorClassification;
        private Int64 _IdParentIndicatorClassification;
        private IndicatorClassification _ParentIndicatorClassification;
        private Dictionary<Int64, PA.Entities.Indicator> _Indicators;
        private Entities.IndicatorClassification_LG _LanguageOption; //Objeto con los datos dependientes del idioma actual elegido por el usuario
        private Collections.IndicatorClassifications_LG _LanguagesOptions; //Coleccion con los datos dependientes del idioma actual elegido por el usuario
        private Dictionary<Int64, Entities.IndicatorClassification> _Children; //Coleccion de hijas
        #endregion

        #region External Properties
        internal Credential Credential
        {
            get { return _Credential; }
        }
        public Int64 IdIndicatorClassification
        {
            get { return _IdIndicatorClassification; }
        }
        public Int64 IdParentIndicatorClassification
        {
            get { return _IdParentIndicatorClassification; }
        }
        public IndicatorClassification ParentIndicatorClassification
        {
            get
            {
                if (_ParentIndicatorClassification == null)
                { _ParentIndicatorClassification = new Collections.IndicatorClassifications(_Credential).Item(_IdParentIndicatorClassification); }
                return _ParentIndicatorClassification;
            }
        }

        public IndicatorClassification_LG LanguageOption
        {
            get { return _LanguageOption; }
        }
        public Collections.IndicatorClassifications_LG LanguagesOptions
        {
            get
            {
                if (_LanguagesOptions == null)
                {
                    //Carga la coleccion de lenguages de es posicion
                    _LanguagesOptions = new Collections.IndicatorClassifications_LG(this,_Credential);
                }

                return _LanguagesOptions;
            }
        }

        public Dictionary<Int64, Entities.IndicatorClassification> ChildrenClassifications
        {
            get
            {
                if (_Children == null)
                {
                    _Children = new Collections.IndicatorClassifications(this, _Credential).Items();
                }

                return _Children;
            }
        }
        /// <summary>
        /// Borra las clasificaciones hijas 
        /// </summary>
        internal void Remove()
        {
            Collections.IndicatorClassifications _indicatorClassifications = new PA.Collections.IndicatorClassifications(_Credential);
            foreach (IndicatorClassification _indicatorClassification in ChildrenClassifications.Values)
            {
                _indicatorClassifications.Remove(_indicatorClassification);
            }
        }
        public Dictionary<Int64, PA.Entities.Indicator> ChildrenElements
        {
            get
            {
                if (_Indicators == null)
                { _Indicators = new PA.Collections.Indicators(this).Items(); }
                return _Indicators;
            }
        }
        public PA.Entities.Indicator Indicator(Int64 idIndicator)
        {
            return new PA.Collections.Indicators(_Credential).Item(idIndicator);
        }
        #endregion

        internal IndicatorClassification(Int64 idIndicatorClassification, Int64 idParentIndicatorClassification, String name, String description, Credential credential)
        {
            _Credential = credential;
            _IdIndicatorClassification = idIndicatorClassification;
            _IdParentIndicatorClassification = idParentIndicatorClassification;
            //Carga el nombre para el lenguage seleccionado
            _LanguageOption = new IndicatorClassification_LG(name, description, _Credential.CurrentLanguage.IdLanguage);
        }

        /// <summary>
        /// Modifica el parent (para el drug&drop)
        /// </summary>
        /// <param name="parent"></param>
        public void Move(IndicatorClassification parent)
        {            
            //Manejo de la transaccion
            using (TransactionScope _transactionScope = new TransactionScope())
            {
                new Collections.IndicatorClassifications(parent, _Credential).Modify(this);
                _transactionScope.Complete();
            }
        }
        /// <summary>
        /// modifica solo el nombre y description
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        public void Modify(String name, String description)
        {
            //Manejo de la transaccion
            using (TransactionScope _transactionScope = new TransactionScope())
            {
                new Collections.IndicatorClassifications(_Credential).Modify(this, name, description);
                _transactionScope.Complete();
            }
        }
      
    }
}
