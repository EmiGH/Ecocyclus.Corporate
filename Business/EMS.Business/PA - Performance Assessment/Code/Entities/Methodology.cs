using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Transactions;
using Condesus.EMS.Business.KC.Entities;

namespace Condesus.EMS.Business.PA.Entities
{
    public class Methodology
    {
        #region Internal Region
        private Int64 _IdMethodology;        
        private Int64 _IdResource;        
        private Credential _Credential;
        private Resource _Resource;
        private Methodology_LG _LanguageOption;//puntero a lenguages
        private Dictionary<String, Entities.Methodology_LG> _LanguagesOptions;//puntero a lenguajes

        #endregion

        #region External Region
        public Int64 IdMethodology
        {
            get { return _IdMethodology; }
        }

        public Int64 IdResource
        {
            get { return _IdResource; }
        }

        internal Credential Credential
        {
            get { return _Credential; }
        }

        public Resource Resource
        {
            get
            {
                if (_Resource == null)
                { _Resource = new KC.Collections.Resources(Credential).Item(_IdResource); }
                return _Resource;
            }
        }


        #region LanguageOptions
        public Methodology_LG LanguageOption
        {
            get
            {
                return _LanguageOption;
            }
        }
        public Dictionary<String, Entities.Methodology_LG> LanguagesOptions
        {
            get
            {
                if (_LanguagesOptions == null)
                {
                    //Carga la coleccion de lenguages de es posicion
                    _LanguagesOptions = new Collections.Methodologies_LG(this).Items();
                }

                return _LanguagesOptions;
            }
        }
        public Methodology_LG LanguageCreate(DS.Entities.Language language, String methodName, String methodType, String description)
        {
            return new Collections.Methodologies_LG(this).Create(language, methodName,methodType, description);
        }
        public void LanguageRemove(DS.Entities.Language language)
        {
            new Collections.Methodologies_LG(this).Delete(language);
        }
        public void LanguageModify(DS.Entities.Language language, String methodName, String methodType, String description)
        {
            new Collections.Methodologies_LG(this).Update(language, methodName,methodType, description);
        }
        #endregion

        //Borra dependencias
        internal void Remove()
        {
            new Collections.Methodologies_LG(this).Delete();
        }

        #endregion

        internal Methodology(Int64 idMethodology,Int64 idResource, String idLanguage, String methodName, String methodType, String description, Credential credential)
        {
            _IdMethodology = idMethodology;
            _IdResource = idResource;
            _Credential = credential;
            _LanguageOption = new Methodology_LG(idLanguage, methodName, methodType, description);
        }

        public void Modify(Resource resource, String methodName,String methodType, String description)
        {
            using (TransactionScope _TransactionScope = new TransactionScope())
            {
                new Collections.Methodologies(this.Credential).Update(this,resource, methodName,methodType, description);
                _TransactionScope.Complete();
            }
        }
    }
}
