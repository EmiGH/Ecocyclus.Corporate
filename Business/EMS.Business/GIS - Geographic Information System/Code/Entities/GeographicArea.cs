using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.GIS.Entities
{
    public class GeographicArea : IGoegraphicData
    {
        #region Internal Properties
        private Credential _Credential;
        private Int64 _IdGeographicArea;//Id del area geografica
        private Int64 _IdParentGeographicArea; //Id del area geografica padre
        private String _Coordinate;
        private String _ISOCode;
        private Entities.GeographicArea_LG _LanguageOption; //Objeto con los datos dependientes del idioma actual elegido por el usuario
        private Collections.GeographicAreas_LG _LanguagesOptions; //Opciones de Idioma
        private Dictionary<Int64, GeographicArea> _Children; //Areas geograficas hijas
        private Dictionary<Int64, Address> _Addresses;
        private Int64 _IdOrganization;
        private DS.Entities.Organization _Organization;
        private String _Layer;
        #endregion

        #region External Properties
        internal Credential Credential
        {
            get { return _Credential; }
        }
        public Int64 IdGeographicArea
        {
            get { return _IdGeographicArea; }
        }
        public String Coordinate
        {
            get { return _Coordinate; }
        }
        public String ISOCode
        {
            get {return _ISOCode;}
        }
        public Int64 IdOrganization
        {
            get { return _IdOrganization; }
        }
        public String Layer
        {
            get { return _Layer; }
        }
        public Int64 IdParentGeographicArea
        {
            get { return _IdParentGeographicArea; }
        }
        public GeographicArea_LG LanguageOption
        {
            get { return _LanguageOption; }
        }
        public DS.Entities.Organization Government
        {
            get
            {
                if (_Organization == null)
                { _Organization = new DS.Collections.Organizations(_Credential).Item(_IdOrganization); }
                return _Organization;
            }
        }
        public Collections.GeographicAreas_LG LanguagesOptions
        {
            get
            {
                if (_LanguagesOptions == null)
                {
                    _LanguagesOptions = new Collections.GeographicAreas_LG(this , _Credential);
                }
                return _LanguagesOptions;
            }
        }
        public Dictionary<Int64, GeographicArea> Children
        {
            get
            {
                if (_Children == null)
                {
                    _Children = new Collections.GeographicAreas(this).Items();
                }
                return _Children;
            }
        }

        public Dictionary<Int64, Address> Addresses
        {
            get
            {
                if (_Addresses == null)
                { _Addresses = new Collections.Addresses(this).Items(); }
                return _Addresses;
            }
        }
        public GeographicArea ParentGeographicArea
        {
            get { return new Collections.GeographicAreas(this).Item(_IdParentGeographicArea); }
        }
        public Dictionary<Int64, PF.Entities.ProcessGroupProcess> ProcessGroupProcess()
        {
            return new PF.Collections.ProcessGroupProcesses(_Credential).Items(this); 
        }
        public Dictionary<Int64, PF.Entities.ProcessGroupProcess> ProcessGroupProcess(PF.Entities.ProcessClassification clasification)
        {
            return new PF.Collections.ProcessGroupProcesses(_Credential).Items(clasification, this); 
        }

        /// <summary>
        /// Borra sus dependencias
        /// </summary>
        internal void Remove()
        {
            foreach (GeographicArea _geographicArea in Children.Values)
            {
                new Collections.GeographicAreas(this).Remove(_geographicArea);
            }
            //Borra todos los lenguages
            new Collections.GeographicAreas_LG(this, _Credential).Remove();
        }
        #endregion

        internal GeographicArea(Int64 idGeographicArea, Int64 idParentGeographicArea, String coordinate, Int64 IdOrganization, String ISOCode, String layer, String name, String description, Credential credential)
        {
            _Credential = credential;
            _IdGeographicArea = idGeographicArea;
            _IdParentGeographicArea = idParentGeographicArea;
            _Coordinate = coordinate;
            _ISOCode = ISOCode;
            _IdOrganization = IdOrganization;
            _Layer = layer;
            //Carga el nombre para el lenguage seleccionado
            _LanguageOption = new GeographicArea_LG(_Credential.CurrentLanguage.IdLanguage, name, description);
        }

        public void Modify(Entities.GeographicArea parentGeographicArea, String coordinate, String isoCode, DS.Entities.Organization organization, String name, String description, String layer)
        {
            using (TransactionScope _transactionScope = new TransactionScope(new TransactionScopeOption(), new TimeSpan(0, 10, 0)))
            {
                new Collections.GeographicAreas(this).Modify(this, parentGeographicArea, coordinate, isoCode, organization, name, description, layer);
                _transactionScope.Complete();
            }
        }
    }
}
