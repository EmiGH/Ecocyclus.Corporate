using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.PA.Entities
{
    public class AccountingScope
    {
        #region Internal Region
        private Int64 _IdScope;        
        private Credential _Credential;
        private AccountingScope_LG _LanguageOption;//puntero a lenguages
        private Dictionary<String, Entities.AccountingScope_LG> _LanguagesOptions;//puntero a lenguajes

        #endregion

        #region External Region
        public Int64 IdScope
        {
            get { return _IdScope; }
        }
        internal Credential Credential
        {
            get { return _Credential; }
        }

        #region LanguageOptions
        public AccountingScope_LG LanguageOption
        {
            get
            {
                return _LanguageOption;
            }
        }
        public Dictionary<String, Entities.AccountingScope_LG> LanguagesOptions
        {
            get
            {
                if (_LanguagesOptions == null)
                {
                    //Carga la coleccion de lenguages de es posicion
                    _LanguagesOptions = new Collections.AccountingScopes_LG(this).Items();
                }

                return _LanguagesOptions;
            }
        }
        public AccountingScope_LG LanguageCreate(DS.Entities.Language language, String name, String description)
        {
            return new Collections.AccountingScopes_LG(this).Create(language, name, description);
        }
        public void LanguageRemove(DS.Entities.Language language)
        {
            new Collections.AccountingScopes_LG(this).Delete(language);
        }
        public void LanguageModify(DS.Entities.Language language, String name, String description)
        {
            new Collections.AccountingScopes_LG(this).Update(language, name, description);
        }
        #endregion

        //Borra dependencias
        internal void Remove()
        {
            new Collections.AccountingScopes_LG(this).Delete();
        }

        #endregion

        internal AccountingScope(Int64 idScope, String idLanguage, String name, String description, Credential credential)
        {
            _IdScope = idScope;            
            _Credential = credential;
            _LanguageOption = new AccountingScope_LG(idLanguage, name, description);
        }

        public void Modify(String name, String description)
        {
            using (TransactionScope _TransactionScope = new TransactionScope())
            {
                new Collections.AccountingScopes(this.Credential).Update(this, name, description);
                _TransactionScope.Complete();
            }
        }
    }
}
