using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PA.Entities
{
    public class MeasurementDevice
    {
        #region Internal Properties
        private Credential _Credential;
        private Int64 _IdMeasurementDevice;
        private String _Reference;        
        private String _SerialNumber;
        private String _Brand;
        private String _Model;
        private String _CalibrationPeriodicity;
        private String _Maintenance;
        private DateTime? _InstallationDate;        
        private Int64 _IdDeviceType;
        private Int64 _IdFacility;
        private Double _UpperLimit;
        private Double _LowerLimit;
        private Double _Uncertainty;
        private MeasurementDeviceType _DeviceType;    
        private Dictionary<Int64, MeasurementUnit> _MeasurementUnits;
        private KC.Entities.ResourceCatalog _ResourcePicture;
        private GIS.Entities.Site _Site;
        #endregion
        
        #region External Properties

        public Int64 IdMeasurementDevice
        {
            get { return _IdMeasurementDevice; }
        }
        public String Reference
        {
            get { return _Reference; }
        }
        public String SerialNumber
        {
            get { return _SerialNumber; }
        }
        public String Brand
        {
            get { return _Brand; }
        }
        public String Model
        {
            get { return _Model; }
        }
        public String CalibrationPeriodicity
        {
            get { return _CalibrationPeriodicity; }
        }
        public String Maintenance
        {
            get { return _Maintenance; }
        }
        public Double UpperLimit
        {
            get { return _UpperLimit; }
        }
        public Double LowerLimit
        {
            get { return _LowerLimit; }
        }
        public Double Uncertainty
        {
            get { return _Uncertainty; }
        }

        public DateTime? InstallationDate
        {
            get
            {
                return _InstallationDate;
            }
        }
        public DateTime? CalibrationStartDate()
        {
            return new Collections.MeasurementDevices(_Credential).CalibrationStartDate(_IdMeasurementDevice);
        }
        public DateTime? CalibrationEndDate()
        {
            return new Collections.MeasurementDevices(_Credential).CalibrationEndDate(_IdMeasurementDevice);
        }
        public MeasurementDeviceType DeviceType
        {
            get
            {
                if (_DeviceType == null)
                {
                    _DeviceType = new Collections.MeasurementDeviceTypes(_Credential).Item(_IdDeviceType);
                }
                return _DeviceType;
            }
        }
        public Dictionary<Int64, PF.Entities.ProcessTask> ProcessTask()
        {
            return new PF.Collections.ProcessTasks(_Credential).Items(this);
        }
        public Dictionary<Int64, MeasurementUnit> MeasurementUnits
        {
            get
            {
                if (_MeasurementUnits == null)
                { _MeasurementUnits = new Collections.MeasurementUnits(_Credential).ItemsByMeasurementDevice(_IdMeasurementDevice); }
                return _MeasurementUnits;
            }
        }
        public void MeasurementUnitAdd(Int64 idMeasurementUnit)
        {
            new Collections.MeasurementUnits(_Credential).AddByMeasurementDevice(_IdMeasurementDevice,idMeasurementUnit);
        }
        public void MeasurementUnitRemove(Int64 idMeasurementUnit)
        {
            new Collections.MeasurementUnits(_Credential).RemoveByMeasurementDevice(_IdMeasurementDevice, idMeasurementUnit);
        }

        public String FullName
        {
            get
            {
                return String.Concat(Brand, " ", Model, " (", SerialNumber, ")");
            }
        }

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
                return _ResourcePicture;
            }
        }
        #endregion

        public GIS.Entities.Site Site
        {
            get
            {
                if (_Site == null)
                { _Site = new GIS.Collections.Facilities(_Credential).Item(_IdFacility); }
                return _Site;
            }
        }
        #endregion

        internal MeasurementDevice(Int64 idMeasurementDevice, String reference, String serialNumber, String brand, String model, String calibrationPeriodicity, String maintenance, DateTime? installationDate, Int64 idDeviceType, KC.Entities.ResourceCatalog resourcePicture, Int64 idFacility, Double upperLimit, Double lowerLimit, Double uncertainty, Credential credential)
        {
            _Credential = credential;
            _IdMeasurementDevice = idMeasurementDevice;
            _Reference = reference;
            _SerialNumber = serialNumber;
            _Brand = brand;
            _Model = model;
            _CalibrationPeriodicity = calibrationPeriodicity;
            _Maintenance = maintenance;
            _InstallationDate = installationDate;
            _IdDeviceType = idDeviceType;
            _ResourcePicture = resourcePicture;
            _IdFacility = idFacility;
            _UpperLimit = upperLimit;
            _LowerLimit = lowerLimit;
            _Uncertainty = uncertainty;
        }
   
    }
}
