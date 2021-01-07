using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.GIS.Entities
{
    public abstract class Site : IGoegraphicData
    {
        #region Internal Properties
        private Credential _Credential;
        private Int64 _IdFacility;//Id del area geografica
        private Int64 _IdOrganization;//ID de la organizacion a la que pertenece
        private String _Coordinate;
        private Int64 _IdResourcePicture;
        private Entities.Site_LG _LanguageOption; //Objeto con los datos dependientes del idioma actual elegido por el usuario
        private Collections.Sites_LG _LanguagesOptions; //Opciones de Idioma
        private Dictionary<Int64, Address> _Addresses;
        private Dictionary<Int64, Sector> _Sectors;
        private Dictionary<Int64, DS.Entities.Telephone> _Telephones;
        private KC.Entities.ResourceCatalog _ResourcePicture;
        private DS.Entities.Organization _Organization;
        private Int64 _IdFacilityType;
        private Int64 _IdGeographicArea;
        private Boolean _Active;
        #endregion

        #region External Properties
        internal Credential Credential
        {
            get { return _Credential; }
        }
        public Boolean Active
        {
            get { return _Active; }
        }
        public Int64 IdGeographicArea
        {
            get { return _IdGeographicArea; }
        }
        public Int64 IdFacilityType
        {
            get { return _IdFacilityType; }
        }
        public Int64 IdFacility
        {
            get { return _IdFacility; }
        }
        public Int64 IdOrganization
        {
            get { return _IdOrganization; }
        }
        public String Coordinate
        {
            get { return _Coordinate; }
        }
        public Site_LG LanguageOption
        {
            get { return _LanguageOption; }
        }
        public Collections.Sites_LG LanguagesOptions
        {
            get
            {
                if (_LanguagesOptions == null)
                {
                    _LanguagesOptions = new Collections.Sites_LG(this, _Credential);
                }
                return _LanguagesOptions;
            }
        }
        public DS.Entities.Organization Organization
        {
            get
            {
                if (_Organization == null)
                { _Organization = new DS.Collections.Organizations(_Credential).Item(_IdOrganization); }
                return _Organization;
            }
        }

        #region Addresses
        public Dictionary<Int64, Address> Addresses
        {
            get
            {
                if (_Addresses == null)
                { _Addresses = new Collections.Addresses(this).Items(); }
                return _Addresses;
            }
        }
        public AddressFacility Address(Int64 idAddress)
        {
            return (AddressFacility)new Collections.Addresses(this).Item(idAddress);
        }
        public Address AddressesAdd(GeographicArea geographicArea, String coordinate, 
        String street, String number, String floor, String department, String postCode)
        {
            using (TransactionScope _transactionScope = new TransactionScope())
            {
                Entities.Address _Address = new Collections.Addresses(this).Add(this, geographicArea, coordinate, street, number, floor, department, postCode);
                _transactionScope.Complete();
                return _Address;
            }
        }
        public void Remove(Entities.Address address)
        {
            using (TransactionScope _transactionScope = new TransactionScope())
            {
                new Collections.Addresses(this).Remove(address);
                _transactionScope.Complete();
            }
        }
        #endregion

        #region Telephnes
        public Dictionary<Int64, DS.Entities.Telephone> Telephones
        {
            get
            {
                if (_Telephones == null)
                { _Telephones = new DS.Collections.Telephones(this).Items(); }
                return _Telephones;
            }
        }
        public DS.Entities.Telephone Telephone(Int64 idTelephone)
        {
            return new DS.Collections.Telephones(this).Item(idTelephone);
        }
        public DS.Entities.Telephone TelephoneAdd(String areaCode, String number, String extension, String internationalCode, String reason)
        {
            using (TransactionScope _transactionScope = new TransactionScope())
            {
                DS.Entities.Telephone _Telephone = new DS.Collections.Telephones(this).Add(this, areaCode, number, extension, internationalCode, reason);
                _transactionScope.Complete();
                return _Telephone;
            }
        }
        public void Remove(DS.Entities.Telephone telephone)
        {
            using (TransactionScope _transactionScope = new TransactionScope())
            {
                new DS.Collections.Telephones(this).Remove(telephone);
                _transactionScope.Complete();
            }
        }
        #endregion

        #region FacilityType Sectors
        public Dictionary<Int64, FacilityType> FacilityTypeSectors
        {
            get
            {
                Dictionary<Int64, FacilityType> _facilityType = new Dictionary<long, FacilityType>();
                foreach (Sector _sector in Sectors.Values)
                {
                    if (!_facilityType.ContainsKey(_sector.FacilityType.IdFacilityType))
                    {
                        _facilityType.Add(_sector.FacilityType.IdFacilityType, _sector.FacilityType);
                    }
                }
                return _facilityType;
            }
        }
        #endregion 

        #region Sectors
        public Dictionary<Int64, Sector> Sectors
        {
            get
            {
                if (_Sectors == null)
                { _Sectors = new Collections.Facilities(this).Sectors(); }
                return _Sectors;
            }
        }
        public Sector Sector(Int64 idFacility)
        {
            return (Sector)new Collections.Facilities(this).Item(idFacility);
        }
        public Sector SectorAdd(String coordinate, String name, String description, KC.Entities.ResourceCatalog resourcePicture, Entities.FacilityType facilityType, Boolean active)
        {
            using (TransactionScope _transactionScope = new TransactionScope())
            {
                Sector _Sector = new Collections.Facilities(this).Add(this, coordinate, name, description, resourcePicture, facilityType, active);
                _transactionScope.Complete();
                return _Sector;
            }
        }
        public void Remove(Sector sector)
        {
            using (TransactionScope _transactionScope = new TransactionScope())
            {
                new Collections.Facilities(this).Remove(sector);
                _transactionScope.Complete();
            }
        }
        #endregion

        #region Resources
        /// <summary>
        /// Retorna la coleccion de Imagenes que tiene asociado. (a travez de CatalogDoc)
        /// Key = IdResourceFile
        /// </summary>
        public Dictionary<Int64, Condesus.EMS.Business.KC.Entities.CatalogDoc> Pictures
        {
            get
            {
                Dictionary<Int64, Condesus.EMS.Business.KC.Entities.CatalogDoc> _pictures = new Dictionary<Int64, Condesus.EMS.Business.KC.Entities.CatalogDoc>();
                //Si el proyecto no tiene ningun ResourcePicture Asociado...entrega vacio.
                if (this.ResourcePicture != null)
                {
                    foreach (Condesus.EMS.Business.KC.Entities.Catalog _catalog in this.ResourcePicture.Catalogues.Values)
                    {
                        if (_catalog.GetType().Name == "CatalogDoc")
                        {
                            //Lo castea a tipo Doc
                            Condesus.EMS.Business.KC.Entities.CatalogDoc _catalogDoc = (Condesus.EMS.Business.KC.Entities.CatalogDoc)_catalog;

                            //Solo nos quedamos con los que son tipo image
                            if (_catalogDoc.DocType.Contains("image"))
                            {
                                _pictures.Add(_catalogDoc.IdResourceFile, _catalogDoc);
                            }
                        }
                    }
                }
                return _pictures;
            }
        }

        public KC.Entities.ResourceCatalog ResourcePicture
        {
            get
            {
                if(_ResourcePicture==null)
                { _ResourcePicture = (KC.Entities.ResourceCatalog)new KC.Collections.Resources(_Credential).Item(Convert.ToInt64(_IdResourcePicture));}
                return _ResourcePicture;
            }
        }
        #endregion

        #region ProcessTaskMeasurement
        public Dictionary<Int64, PF.Entities.ProcessTask> TasksByProcess(PF.Entities.ProcessGroupProcess process)
        {
            return new PF.Collections.ProcessTasks(_Credential).Items(this, process);           
        }
        #endregion

        /// <summary>
        /// Borra sus dependencias
        /// </summary>
        internal void Remove()
        {
            foreach (Sector _Sector in this.Sectors.Values)
            {
                new Collections.Facilities(this).Remove(_Sector);
            }
            foreach (AddressFacility _AddressFacility in this.Addresses.Values)
            {
                new Collections.Addresses(this).Remove(_AddressFacility);
            }
            foreach (DS.Entities.Telephone _Telephone in this.Telephones.Values)
            {
                new DS.Collections.Telephones(this).Remove(_Telephone);
            }
            //Borra todas las opciones de lenguage
            new Collections.Sites_LG(this, _Credential).Remove();
        }
        #endregion

        internal Site(Int64 idFacility, Int64 idOrganization, String coordinate, String name, String description, Int64 IdResourcePicture, Credential credential, Int64 idFacilityType, Int64 idGeographicArea, Boolean active)
        {
            _Credential = credential;
            _Active = active;
            _IdFacility = idFacility;
            _IdOrganization = idOrganization;
            _Coordinate = coordinate;
            _IdResourcePicture = IdResourcePicture;
            _IdFacilityType = idFacilityType;
            _IdGeographicArea = idGeographicArea;
            //Carga el nombre para el lenguage seleccionado
            _LanguageOption = new Site_LG(_Credential.CurrentLanguage.IdLanguage, name, description);
        }

    }
}
