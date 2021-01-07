using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.PA.Entities
{
    public class AccountingActivity
    {
        #region Internal Region
        private Int64 _IdActivity;
        private Int64 _IdParentActivity;
        private AccountingActivity _Parent;
        private Dictionary<Int64, AccountingActivity> _Childrens;
        private Credential _Credential;
        private AccountingActivity_LG _LanguageOption;//puntero a lenguages
        private Dictionary<String, Entities.AccountingActivity_LG> _LanguagesOptions;//puntero a lenguajes

        #endregion

        #region External Region
        public Int64 IdActivity
        {
            get { return _IdActivity; }
        }
        public Int64 IdParentActivity
        {
            get { return _IdParentActivity; }
        }
        public AccountingActivity Parent
        {
            get
            {
                if (_Parent == null)
                { _Parent = new Collections.AccountingActivities(this).Item(_IdParentActivity); }
                return _Parent;
            }
        }
        public Dictionary<Int64, AccountingActivity> Childrens
        {
            get
            {
                if (_Childrens == null)
                { _Childrens = new Collections.AccountingActivities(this).Items(); }
                return _Childrens;
            }
        }
        public Dictionary<Int64, Measurement> Measurements (PF.Entities.ProcessGroupProcess process)
        {
            return new Collections.Measurements(_Credential).Items(this , process);            
        }

        internal Credential Credential
        {
            get { return _Credential; }
        }

        //Reporte
        public Dictionary<Int64, GIS.Entities.Facility> Facilities (DS.Entities.Organization organization)
        {
            return new GIS.Collections.Facilities(this, organization).Facility();
        }

        #region LanguageOptions
        public AccountingActivity_LG LanguageOption
        {
            get
            {
                return _LanguageOption;
            }
        }
        public Dictionary<String, Entities.AccountingActivity_LG> LanguagesOptions
        {
            get
            {
                if (_LanguagesOptions == null)
                {
                    //Carga la coleccion de lenguages de es posicion
                    _LanguagesOptions = new Collections.AccountingActivities_LG(this).Items();
                }

                return _LanguagesOptions;
            }
        }
        public AccountingActivity_LG LanguageCreate(DS.Entities.Language language, String name, String description)
        {
            return new Collections.AccountingActivities_LG(this).Create(language, name, description);
        }
        public void LanguageRemove(DS.Entities.Language language)
        {
            new Collections.AccountingActivities_LG(this).Delete(language);
        }
        public void LanguageModify(DS.Entities.Language language, String name, String description)
        {
            new Collections.AccountingActivities_LG(this).Update(language, name, description);
        }
        #endregion
            
        //Reporte
        public Decimal ReadTotalMeasurementResultByIndicator(Entities.AccountingScope scope, Entities.Indicator indicatorColumnGas, DateTime? startDate, DateTime? endDate)
        {
            return new Collections.AccountingActivities(this).ReadTotalMeasurementResultByIndicator(scope, this, indicatorColumnGas, startDate, endDate);
        }
        #endregion

        //Borra dependencias
        internal void Remove()
        {

            foreach (AccountingActivity _child in Childrens.Values)
            {
                new Collections.AccountingActivities(this).Delete(_child);
            }

            new Collections.AccountingActivities_LG(this).Delete();
        }


        internal AccountingActivity(Int64 idActivity, Int64 idParentActivity, String idLanguage, String name, String description, Credential credential)
        {
            _IdActivity = idActivity;
            _IdParentActivity = idParentActivity;
            _Credential = credential;
            _LanguageOption = new AccountingActivity_LG(idLanguage, name, description);
        }

        public void Modify(AccountingActivity parent, String name, String description)
        {
            using (TransactionScope _TransactionScope = new TransactionScope())
            {
                new Collections.AccountingActivities(this).Update(this, parent, name, description);
                _TransactionScope.Complete();
            }
        }
    }
}

