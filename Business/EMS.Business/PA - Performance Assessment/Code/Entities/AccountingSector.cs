using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.PA.Entities
{
    public class AccountingSector
    {
        #region Internal Region
        private Int64 _IdSector;
        private Int64 _IdParentSector;
        private AccountingSector _Parent;
        private Dictionary<Int64, AccountingSector> _Childrens;
        private Credential _Credential;
        private AccountingSector_LG _LanguageOption;//puntero a lenguages
        private Dictionary<String, Entities.AccountingSector_LG> _LanguagesOptions;//puntero a lenguajes

        #endregion

        #region External Region
        public Int64 IdSector
        {
            get { return _IdSector; }
        }
        public Int64 IdParentSector
        {
            get { return _IdParentSector; }
        }
        public AccountingSector Parent
        {
            get
            {
                if (_Parent == null)
                { _Parent = new Collections.AccountingSectors(this).Item(_IdParentSector); }
                return _Parent;
            }
        }
        public Dictionary<Int64, AccountingSector> Childrens
        {
            get
            {
                if (_Childrens == null)
                { _Childrens = new Collections.AccountingSectors(this).Items(); }
                return _Childrens;
            }
        }
        internal Credential Credential
        {
            get { return _Credential; }
        }

        #region LanguageOptions
        public AccountingSector_LG LanguageOption
        {
            get
            {
                return _LanguageOption;
            }
        }
        public Dictionary<String, Entities.AccountingSector_LG> LanguagesOptions
        {
            get
            {
                if (_LanguagesOptions == null)
                {
                    //Carga la coleccion de lenguages de es posicion
                    _LanguagesOptions = new Collections.AccountingSectors_LG(this).Items();
                }

                return _LanguagesOptions;
            }
        }
        public AccountingSector_LG LanguageCreate(DS.Entities.Language language, String name, String description)
        {
            return new Collections.AccountingSectors_LG(this).Create(language, name, description);
        }
        public void LanguageRemove(DS.Entities.Language language)
        {
            new Collections.AccountingSectors_LG(this).Delete(language);
        }
        public void LanguageModify(DS.Entities.Language language, String name, String description)
        {
            new Collections.AccountingSectors_LG(this).Update(language, name, description);
        }
        #endregion

        //Borra dependencias
        internal void Remove()
        {
            new Collections.AccountingSectors_LG(this).Delete();
        }

        #endregion

        internal AccountingSector(Int64 idSector, Int64 idParentSector, String idLanguage, String name, String description, Credential credential)
        {
            _IdSector = idSector;
            _IdParentSector = idParentSector;
            _Credential = credential;
            _LanguageOption = new AccountingSector_LG(idLanguage, name, description);
        }

        public void Modify(AccountingSector parent, String name, String description)
        {
            using (TransactionScope _TransactionScope = new TransactionScope())
            {
                new Collections.AccountingSectors(this).Update(this, parent, name, description);
                _TransactionScope.Complete();
            }
        }
    }
}
