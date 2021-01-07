using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.PA.Entities
{
    public class AccountingScenario
    {
        #region Internal Region
        private Int64 _IdScenario;        
        private Credential _Credential;
        private AccountingScenario_LG _LanguageOption;//puntero a lenguages
        private Dictionary<String, Entities.AccountingScenario_LG> _LanguagesOptions;//puntero a lenguajes

        #endregion

        #region External Region
        public Int64 IdScenario
        {
            get { return _IdScenario; }
        }
        internal Credential Credential
        {
            get { return _Credential; }
        }

        #region LanguageOptions
        public AccountingScenario_LG LanguageOption
        {
            get
            {
                return _LanguageOption;
            }
        }
        public Dictionary<String, Entities.AccountingScenario_LG> LanguagesOptions
        {
            get
            {
                if (_LanguagesOptions == null)
                {
                    //Carga la coleccion de lenguages de es posicion
                    _LanguagesOptions = new Collections.AccountingScenarios_LG(this).Items();
                }

                return _LanguagesOptions;
            }
        }
        public AccountingScenario_LG LanguageCreate(DS.Entities.Language language, String name, String description)
        {
            return new Collections.AccountingScenarios_LG(this).Create(language, name, description);
        }
        public void LanguageRemove(DS.Entities.Language language)
        {
            new Collections.AccountingScenarios_LG(this).Delete(language);
        }
        public void LanguageModify(DS.Entities.Language language, String name, String description)
        {
            new Collections.AccountingScenarios_LG(this).Update(language, name, description);
        }
        #endregion

        //Borra dependencias
        internal void Remove()
        {
            new Collections.AccountingScenarios_LG(this).Delete();
        }

        #endregion

        internal AccountingScenario(Int64 idScenario, String idLanguage, String name, String description, Credential credential)
        {
            _IdScenario = idScenario;            
            _Credential = credential;
            _LanguageOption = new AccountingScenario_LG(idLanguage, name, description);
        }

        public void Modify(String name, String description)
        {
            using (TransactionScope _TransactionScope = new TransactionScope())
            {
                new Collections.AccountingScenarios(this.Credential).Update(this, name, description);
                _TransactionScope.Complete();
            }
        }
    }
}
