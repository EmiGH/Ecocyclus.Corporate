using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PA.Entities
{
    public class MeasurementDeviceType
    {
        #region Internal Properties 
        private Credential _Credential;
        private Int64 _IdMeasurementDeviceType;
        private Dictionary<Int64, MeasurementDevice> _MeasurementDevice;
        private Entities.MeasurementDeviceType_LG _LanguageOption; //Objeto con los datos dependientes del idioma actual elegido por el usuario
        private Collections.MeasurementDeviceTypes_LG _LanguagesOptions;
        
        #endregion

        #region External Properties
     
        public Int64 IdMeasurementDeviceType
        {
            get { return _IdMeasurementDeviceType; }
        }
        public Collections.MeasurementDeviceTypes_LG LanguagesOptions
        {
            get
            {
                if (_LanguagesOptions == null)
                {
                    //Carga la coleccion de lenguages de ese pais
                    _LanguagesOptions = new Collections.MeasurementDeviceTypes_LG(_IdMeasurementDeviceType, _Credential);
                }
                return _LanguagesOptions;
            }
        }
        public MeasurementDeviceType_LG  LanguageOption
        {
            get { return _LanguageOption; }
        }

        #region MeasurementDevice
        public MeasurementDevice MeasurementDevice(Int64 idMeasurementDevice)
        {
            return new Collections.MeasurementDevices(_Credential).Item(idMeasurementDevice);
        }
        public Dictionary<Int64, MeasurementDevice> MeasurementDevices
        {
            get
            {
                if (_MeasurementDevice == null) { _MeasurementDevice = new Collections.MeasurementDevices(_Credential).Items(_IdMeasurementDeviceType); }
                return _MeasurementDevice;
            }
        }
        public MeasurementDevice MeasurementDeviceAdd(String reference, String serialNumber, String brand, String model, String calibrationPeriodicity, String maintenance, DateTime? installationDate, Dictionary<Int64, Entities.MeasurementUnit> measurementUnits, KC.Entities.ResourceCatalog resourcePicture, GIS.Entities.Site site, Double upperLimit, Double lowerLimit, Double uncertainty)
        {
            return new Collections.MeasurementDevices(_Credential).Add(reference, serialNumber, brand, model, calibrationPeriodicity, maintenance, installationDate, _IdMeasurementDeviceType, measurementUnits, resourcePicture, site,upperLimit,lowerLimit,uncertainty);
        }
        public void MeasurementDeviceModify(Int64 idMeasurementDevice, String reference, String serialNumber, String brand, String model, String calibrationPeriodicity, String maintenance, DateTime? installationDate, Dictionary<Int64, Entities.MeasurementUnit> measurementUnits, KC.Entities.ResourceCatalog resourcePicture, GIS.Entities.Site site, Double upperLimit, Double lowerLimit, Double uncertainty)
        {
            new Collections.MeasurementDevices(_Credential).Modify(idMeasurementDevice, reference, serialNumber, brand, model, calibrationPeriodicity, maintenance, installationDate, _IdMeasurementDeviceType, measurementUnits, resourcePicture, site,upperLimit,lowerLimit,uncertainty);
        }
        public void MeasurementDeviceRemove(Int64 idMeasurementDevice)
        {
            new Collections.MeasurementDevices(_Credential).Remove(idMeasurementDevice);
        }
        #endregion
      
        /// <summary>
        /// Borra sus dependencias
        /// </summary>
        internal void Remove()
        {
            foreach (MeasurementDevice _measurementDevice in this.MeasurementDevices.Values)
            {
                this.MeasurementDeviceRemove(_measurementDevice.IdMeasurementDevice);
            }
        }

        #endregion
            
        internal MeasurementDeviceType(Int64 idMeasurementDeviceType, String name, String description, String idLanguage, Credential credential)
        {
            _Credential = credential;
            _IdMeasurementDeviceType = idMeasurementDeviceType;       
            //Carga el nombre para el lenguage seleccionado
            _LanguageOption = new MeasurementDeviceType_LG(idLanguage, name, description);
        }

        public void Modify(String name, String description)
        {
            //Realiza las validaciones de autorizacion 
            new Security.Authority(_Credential).Authorize(Common.Security.ConfigurationPA, 0, _Credential.User.IdPerson, Common.Permissions.Manage);
            new Collections.MeasurementDeviceTypes(_Credential).Modify(_IdMeasurementDeviceType, name, description);
        }

    }
}
