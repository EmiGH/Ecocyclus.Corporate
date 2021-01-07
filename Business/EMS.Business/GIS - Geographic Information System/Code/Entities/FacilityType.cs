using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.GIS.Entities
{
    public class FacilityType
    {
        #region Internal Region
        private Int64 _IdFacilityType;        
        private Credential _Credential;
        private FacilityType_LG _LanguageOption;//puntero a lenguages
        private Dictionary<String, Entities.FacilityType_LG> _LanguagesOptions;//puntero a lenguajes
        private String _IconName;
        #endregion

        #region External Region
        public String IconName
        {
            get { return _IconName; }
        }
        public Int64 IdFacilityType
        {
            get { return _IdFacilityType; }
        }
        internal Credential Credential
        {
            get { return _Credential; }
        }

        #region LanguageOptions
        public FacilityType_LG LanguageOption
        {
            get
            {
                return _LanguageOption;
            }
        }
        public Dictionary<String, Entities.FacilityType_LG> LanguagesOptions
        {
            get
            {
                if (_LanguagesOptions == null)
                {
                    //Carga la coleccion de lenguages de es posicion
                    _LanguagesOptions = new Collections.FacilityTypes_LG(this).Items();
                }

                return _LanguagesOptions;
            }
        }
        public FacilityType_LG LanguageCreate(DS.Entities.Language language, String name, String description)
        {
            return new Collections.FacilityTypes_LG(this).Create(language, name, description);
        }
        public void LanguageRemove(DS.Entities.Language language)
        {
            new Collections.FacilityTypes_LG(this).Delete(language);
        }
        public void LanguageModify(DS.Entities.Language language, String name, String description)
        {
            new Collections.FacilityTypes_LG(this).Update(language, name, description);
        }
        #endregion

        //Borra dependencias
        internal void Remove()
        {
            new Collections.FacilityTypes_LG(this).Delete();
        }

        #region Facilities
        /// <summary>
        /// Trae las facility q pertenecen a este type
        /// </summary>
        public Dictionary<Int64, Entities.Site> Facilities
        {
            get { return new Collections.Facilities(this).Items(); }
        }
        /// <summary>
        /// Trae los site (sin jerarquia) q pertenecen a este type
        /// </summary>
        public Dictionary<Int64, Entities.Site> Sites
        {
            get { return new Collections.Facilities(this).SitesByFacilityType(this); }
        }
        /// <summary>
        /// Trae los site (sin jerarquia) q pertenecen a este type
        /// </summary>
        public Dictionary<Int64, Entities.Sector> SectorsBySite(Site site)
        {
            return new Collections.Facilities(this).SectorBySiteAndFacilityType(site, this); 
        }
        /// <summary>
        /// devuelve los facilityes que estan en una medicion para el process dado
        /// </summary>
        /// <param name="process"></param>
        /// <returns></returns>
        public Dictionary<Int64, Entities.Site> FacilitiesInMeasurements(PF.Entities.ProcessGroupProcess process)
        {
            return new Collections.Facilities(this, process).Items(); 
        }
        #endregion

        #endregion

        internal FacilityType(Int64 idFacilityType, String iconName, String idLanguage, String name, String description, Credential credential)
        {
            _IdFacilityType = idFacilityType;            
            _Credential = credential;
            _IconName = iconName;
            _LanguageOption = new FacilityType_LG(idLanguage, name, description);
        }

        public void Modify(String iconName, String name, String description)
        {
            using (TransactionScope _TransactionScope = new TransactionScope())
            {
                new Collections.FacilityTypes(this.Credential).Update(this, iconName, name, description);
                _TransactionScope.Complete();
            }
        }
    }
}
