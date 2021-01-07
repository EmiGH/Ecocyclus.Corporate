using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.PA.Entities
{
    public class Quality
    {
        #region Internal Region
        private Int64 _IdQuality;        
        private Credential _Credential;
        private Quality_LG _LanguageOption;//puntero a lenguages
        private Dictionary<String, Entities.Quality_LG> _LanguagesOptions;//puntero a lenguajes

        #endregion

        #region External Region
        public Int64 IdQuality
        {
            get { return _IdQuality; }
        }
        internal Credential Credential
        {
            get { return _Credential; }
        }

        #region LanguageOptions
        public Quality_LG LanguageOption
        {
            get
            {
                return _LanguageOption;
            }
        }
        public Dictionary<String, Entities.Quality_LG> LanguagesOptions
        {
            get
            {
                if (_LanguagesOptions == null)
                {
                    //Carga la coleccion de lenguages de es posicion
                    _LanguagesOptions = new Collections.Qualities_LG(this).Items();
                }

                return _LanguagesOptions;
            }
        }
        public Quality_LG LanguageCreate(DS.Entities.Language language, String name, String description)
        {
            return new Collections.Qualities_LG(this).Create(language, name, description);
        }
        public void LanguageRemove(DS.Entities.Language language)
        {
            new Collections.Qualities_LG(this).Delete(language);
        }
        public void LanguageModify(DS.Entities.Language language, String name, String description)
        {
            new Collections.Qualities_LG(this).Update(language, name, description);
        }
        #endregion

        //Borra dependencias
        internal void Remove()
        {
            new Collections.Qualities_LG(this).Delete();
        }

        #endregion

        internal Quality(Int64 idQuality, String idLanguage, String name, String description, Credential credential)
        {
            _IdQuality = idQuality;            
            _Credential = credential;
            _LanguageOption = new Quality_LG(idLanguage, name, description);
        }

        public void Modify(String name, String description)
        {
            using (TransactionScope _TransactionScope = new TransactionScope())
            {
                new Collections.Qualities(this.Credential).Update(this, name, description);
                _TransactionScope.Complete();
            }
        }
    }
}
