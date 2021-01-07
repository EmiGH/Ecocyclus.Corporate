using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.PA.Collections
{
    internal class MeasurementDevices
    {
        #region Internal Properties
            private Credential _Credential;
        #endregion

        internal MeasurementDevices(Credential credential)
        {
            _Credential = credential;
        } 

        #region Read Functions

        internal Dictionary<Int64, Entities.MeasurementDevice> Items(Int64 idMeasurementDeviceType)
        {
            //Coleccion para devolver las direcciones
            Dictionary<Int64, Entities.MeasurementDevice> _oItems = new Dictionary<Int64, Entities.MeasurementDevice>();

            //Objeto de data layer para acceder a datos
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            //Traigo los datos de la base (o por persona o por organizacion)
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.MeasurementDevices_ReadAll(idMeasurementDeviceType);
            
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                DateTime? _installationDate;
                try 
                {	        
	                _installationDate = (DateTime)_dbRecord["InstallationDate"];
                }
                catch (InvalidCastException)
                {
                    _installationDate = null;
                }

                KC.Entities.ResourceCatalog _resourcecatalog = (KC.Entities.ResourceCatalog)new KC.Collections.Resources(_Credential).Item(Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdResourcePicture"], 0)));
                //Declara e instancia una direccion 
                Entities.MeasurementDevice _measurementDevice = new Entities.MeasurementDevice(Convert.ToInt64(_dbRecord["IdMeasurementDevice"]),                        
                    Convert.ToString(_dbRecord["Reference"]),
                    Convert.ToString(_dbRecord["SerialNumber"]),
                    Convert.ToString(_dbRecord["Brand"]),
                    Convert.ToString(_dbRecord["Model"]),
                    Convert.ToString(_dbRecord["CalibrationPeriodicity"]),
                    Convert.ToString(_dbRecord["Maintenance"]),
                    _installationDate,
                    Convert.ToInt64(_dbRecord["IdMeasurementDeviceType"]),
                    _resourcecatalog ,
                    Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdFacility"], 0)),
                    Convert.ToDouble(Common.Common.CastNullValues(_dbRecord["UpperLimit"], 0)),
                    Convert.ToDouble(Common.Common.CastNullValues(_dbRecord["LowerLimit"], 0)),
                    Convert.ToDouble(Common.Common.CastNullValues(_dbRecord["Uncertainty"], 0)),
                    _Credential);

                //Lo agrego a la coleccion
                _oItems.Add(_measurementDevice.IdMeasurementDevice, _measurementDevice);
            }
            return _oItems;
        }
        internal Dictionary<Int64, Entities.MeasurementDevice> Items()
        {
            //Coleccion para devolver las direcciones
            Dictionary<Int64, Entities.MeasurementDevice> _oItems = new Dictionary<Int64, Entities.MeasurementDevice>();

            //Objeto de data layer para acceder a datos
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            //Traigo los datos de la base (o por persona o por organizacion)
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.MeasurementDevices_ReadAll();

            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                DateTime? _installationDate;
                try
                {
                    _installationDate = (DateTime)_dbRecord["InstallationDate"];
                }
                catch (InvalidCastException)
                {
                    _installationDate = null;
                }

                KC.Entities.ResourceCatalog _resourcecatalog = (KC.Entities.ResourceCatalog)new KC.Collections.Resources(_Credential).Item(Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdResourcePicture"], 0)));
                //Declara e instancia una direccion 
                Entities.MeasurementDevice _measurementDevice = new Entities.MeasurementDevice(Convert.ToInt64(_dbRecord["IdMeasurementDevice"]),
                    Convert.ToString(_dbRecord["Reference"]),
                    Convert.ToString(_dbRecord["SerialNumber"]),
                    Convert.ToString(_dbRecord["Brand"]),
                    Convert.ToString(_dbRecord["Model"]),
                    Convert.ToString(_dbRecord["CalibrationPeriodicity"]),
                    Convert.ToString(_dbRecord["Maintenance"]),
                    _installationDate,
                    Convert.ToInt64(_dbRecord["IdMeasurementDeviceType"]), 
                    _resourcecatalog ,
                    Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdFacility"], 0)),
                    Convert.ToDouble(Common.Common.CastNullValues(_dbRecord["UpperLimit"], 0)),
                    Convert.ToDouble(Common.Common.CastNullValues(_dbRecord["LowerLimit"], 0)),
                    Convert.ToDouble(Common.Common.CastNullValues(_dbRecord["Uncertainty"], 0)),

                    _Credential);

                //Lo agrego a la coleccion
                _oItems.Add(_measurementDevice.IdMeasurementDevice, _measurementDevice);
            }
            return _oItems;
        }
        internal Entities.MeasurementDevice Item(Int64 idDevice)
        {
            //Objeto de data layer para acceder a datos
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.MeasurementDevices_ReadById(idDevice);
            System.Collections.IEnumerator _enum = _record.GetEnumerator();
            if (_enum.MoveNext())
            {
                System.Data.Common.DbDataRecord _dbRecord = (System.Data.Common.DbDataRecord)_enum.Current;

                DateTime? _installationDate;
                try
                {
                    _installationDate = (DateTime)_dbRecord["InstallationDate"];
                }
                catch (InvalidCastException)
                {
                    _installationDate = null;
                }

                KC.Entities.ResourceCatalog _resourcecatalog = (KC.Entities.ResourceCatalog)new KC.Collections.Resources(_Credential).Item(Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdResourcePicture"], 0)));

                return new Entities.MeasurementDevice(Convert.ToInt64(_dbRecord["IdMeasurementDevice"]),
                    Convert.ToString(_dbRecord["Reference"]),
                    Convert.ToString(_dbRecord["SerialNumber"]),
                    Convert.ToString(_dbRecord["Brand"]),
                    Convert.ToString(_dbRecord["Model"]),
                    Convert.ToString(_dbRecord["CalibrationPeriodicity"]),
                    Convert.ToString(_dbRecord["Maintenance"]),
                   _installationDate,
                    Convert.ToInt64(_dbRecord["IdMeasurementDeviceType"]),
                    _resourcecatalog ,
                    Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdFacility"], 0)),
                    Convert.ToDouble(Common.Common.CastNullValues(_dbRecord["UpperLimit"], 0)),
                    Convert.ToDouble(Common.Common.CastNullValues(_dbRecord["LowerLimit"], 0)),
                    Convert.ToDouble(Common.Common.CastNullValues(_dbRecord["Uncertainty"], 0)),
                    _Credential);
            }

            return null;
        }
        internal DateTime? CalibrationStartDate(Int64 idDevice)
        {
            //Objeto de data layer para acceder a datos
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            return _dbPerformanceAssessments.MeasurementDevices_ReadCurrentCalibrationStartDate(idDevice);
        }
        internal DateTime? CalibrationEndDate(Int64 idDevice)
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                return _dbPerformanceAssessments.MeasurementDevices_ReadCurrentCalibrationEndDate(idDevice);
            }

        #endregion

        #region Write Functions

        internal Entities.MeasurementDevice Add(String reference, String serialNumber, String brand, String model, String calibrationPeriodicity, String maintenance, DateTime? installationDate, Int64 idDeviceType, Dictionary<Int64, Entities.MeasurementUnit> measurementUnits, KC.Entities.ResourceCatalog resourcePicture, GIS.Entities.Site site, Double upperLimit, Double lowerLimit, Double uncertainty)
        {
            try
            {
                using (TransactionScope _transactionScope = new TransactionScope())
                {

                    //Objeto de data layer para acceder a datos
                    DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                    //validacio para los null
                    Int64 _IdResourceCatalog = resourcePicture == null ? 0 : resourcePicture.IdResource; 
                    Int64 _IdFacility = site == null ? 0 : site.IdFacility;

                    //ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                    Int64 _idDevice = _dbPerformanceAssessments.MeasurementDevices_Add(idDeviceType, reference, serialNumber, brand, model, calibrationPeriodicity, maintenance, installationDate, _IdResourceCatalog, _IdFacility,upperLimit,lowerLimit,uncertainty, _Credential.User.Person.IdPerson);

                    //Aca inserta todas las unidades de medida que tiene este equipo.
                    foreach (Condesus.EMS.Business.PA.Entities.MeasurementUnit _measurementUnit in measurementUnits.Values)
                    {
                        //Ejecuta el insert 1x1 de una unidad de medida a un equipo
                        _dbPerformanceAssessments.MeasurementDeviceMeasurementUnits_Create(_idDevice, _measurementUnit.IdMeasurementUnit, _Credential.User.Person.IdPerson);
                    }
                    _transactionScope.Complete();
                    //Devuelvo el objeto direccion creado
                    return new Entities.MeasurementDevice(_idDevice, reference, serialNumber, brand, model, calibrationPeriodicity, maintenance, installationDate, idDeviceType, resourcePicture, _IdFacility,upperLimit,lowerLimit,uncertainty, _Credential);
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == Common.Constants.ErrorDataBaseDuplicatedKey)
                {
                    throw new Exception(Common.Resources.Errors.DuplicatedDataRecord, ex);
                }
                throw ex;
            }
        }
        internal void Remove(Int64 idDevice)
        {
            try
            {
                using (TransactionScope _transactionScope = new TransactionScope())
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();


                    //1° Borrar de la base de datos las unidades de medidas relacionadas y despues las vuelve a insertar...
                    _dbPerformanceAssessments.MeasurementDeviceMeasurementUnits_Delete(idDevice, _Credential.User.Person.IdPerson);

                    //2° Borrar de la base de datos el device.
                    _dbPerformanceAssessments.MeasurementDevices_Remove(idDevice, _Credential.User.Person.IdPerson);
                    
                    _transactionScope.Complete();
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == Common.Constants.ErrorDataBaseDeleteReferenceConstraints)
                {
                    throw new Exception(Common.Resources.Errors.RemoveDependency, ex);
                }
                throw ex;
            }
        }
        internal void Modify(Int64 idDevice, String reference, String serialNumber, String brand, String model, String calibrationPeriodicity, String maintenance, DateTime? installationDate, Int64 idDeviceType, Dictionary<Int64, Entities.MeasurementUnit> measurementUnits, KC.Entities.ResourceCatalog resourcePicture, GIS.Entities.Site site, Double upperLimit, Double lowerLimit, Double uncertainty)
        {
            try
            {
                using (TransactionScope _transactionScope = new TransactionScope())
                {

                    //Objeto de data layer para acceder a datos
                    DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                    //validacio para los null
                    Int64 _IdResourceCatalog = resourcePicture == null ? 0 : resourcePicture.IdResource;
                    Int64 _IdFacility = site == null ? 0 : site.IdFacility;
  
                    //Modifico los datos de la base
                    _dbPerformanceAssessments.MeasurementDevices_Update(idDevice, idDeviceType, reference, serialNumber, brand, model, calibrationPeriodicity, maintenance, installationDate, _IdResourceCatalog, _IdFacility,upperLimit,lowerLimit,uncertainty, _Credential.User.Person.IdPerson);

                    //Borrar de la base de datos las unidades de medidas relacionadas y despues las vuelve a insertar...
                    _dbPerformanceAssessments.MeasurementDeviceMeasurementUnits_Delete(idDevice, _Credential.User.Person.IdPerson);
                    //Aca inserta todas las unidades de medida que tiene este equipo.
                    foreach (Condesus.EMS.Business.PA.Entities.MeasurementUnit _measurementUnit in measurementUnits.Values)
                    {
                        //Ejecuta el insert 1x1 de una unidad de medida a un equipo
                        _dbPerformanceAssessments.MeasurementDeviceMeasurementUnits_Create(idDevice, _measurementUnit.IdMeasurementUnit, _Credential.User.Person.IdPerson);
                    }

                    _transactionScope.Complete();
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == Common.Constants.ErrorDataBaseDuplicatedKey)
                {
                    throw new Exception(Common.Resources.Errors.DuplicatedDataRecord, ex);
                }
                throw ex;
            }
        }
        
        #endregion
    }
}
