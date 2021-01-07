using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.GIS
{
    public class GeographicInformationSystem
    {
        # region Public Properties

        #region FacilityTypes
        #region Read Functions
        public IEnumerable<DbDataRecord> FacilityTypes_ReadById(Int64 idFacilityType, String idLanguage)
        {
            return new Entities.FacilityTypes().ReadById(idFacilityType, idLanguage);
        }
        public IEnumerable<DbDataRecord> FacilityTypes_ReadAll(String idLanguage)
        {
            return new Entities.FacilityTypes().ReadAll(idLanguage);
        }
        public IEnumerable<DbDataRecord> FacilityTypes_ReadByProcessWhitMeasurements(Int64 IdProcess, String idLanguage)
        {
            return new Entities.FacilityTypes().ReadByProcessWhitMeasurements(IdProcess, idLanguage);
        }
        #endregion

        #region Write Functions
        public Int64 FacilityTypes_Create(String IconName)
        {
            return new Entities.FacilityTypes().Create(IconName);
        }
        public void FacilityTypes_Update(Int64 idFacilityType,String IconName)
        {
             new Entities.FacilityTypes().Update(idFacilityType, IconName);
        }
        public void FacilityTypes_Delete(Int64 idFacilityType)
        {
            new Entities.FacilityTypes().Delete(idFacilityType);
        }
        #endregion
        #endregion

        #region FacilityTypes_LG
        #region Read Functions
        public IEnumerable<DbDataRecord> FacilityTypes_LG_ReadById(Int64 idFacilityType, String idLanguage)
        {
            return new Entities.FacilityTypes_LG().ReadById(idFacilityType, idLanguage);
        }
        public IEnumerable<DbDataRecord> FacilityTypes_LG_ReadAll(Int64 idFacilityType)
        {
            return new Entities.FacilityTypes_LG().ReadAll(idFacilityType);
        }
        #endregion

        #region Write Functions
        public void FacilityTypes_LG_Create(Int64 idFacilityType, String idLanguage, String name, String description)
        {
            new Entities.FacilityTypes_LG().Create(idFacilityType, idLanguage, name, description);
        }
        public void FacilityTypes_LG_Delete(Int64 idFacilityType, String idLanguage)
        {
            new Entities.FacilityTypes_LG().Delete(idFacilityType, idLanguage);
        }
        public void FacilityTypes_LG_Delete(Int64 idFacilityType)
        {
            new Entities.FacilityTypes_LG().Delete(idFacilityType);
        }
        public void FacilityTypes_LG_Update(Int64 idFacilityType, String idLanguage, String name, String description)
        {
            new Entities.FacilityTypes_LG().Update(idFacilityType, idLanguage, name, description);
        }
        #endregion
        #endregion

        # region GeographicAreas

        #region Write Functions GeographicAreas

        public Int64 GeographicAreas_Create(Int64 idParentGeographicArea, String coordinate, String isoCode, Int64 IdOrganization, String Layer)
        {
            return new GeographicAreas().Create(idParentGeographicArea, coordinate, isoCode, IdOrganization, Layer);
        }
        public void GeographicAreas_Delete(Int64 idGeographicArea)
        {
            new GeographicAreas().Delete(idGeographicArea);
        }
        public void GeographicAreas_Update(Int64 idGeographicArea, Int64 idParentGeographicArea, String coordinate, String isoCode, Int64 IdOrganization, String Layer)
        {
            new GeographicAreas().Update(idGeographicArea, idParentGeographicArea, coordinate, isoCode, IdOrganization, Layer);
        }
        public void GeographicAreas_Update(Int64 IdOrganization)
        {
            new GeographicAreas().Update(IdOrganization);
        }

        #endregion

        #region Read Functions

        public IEnumerable<DbDataRecord> GeographicAreas_ReadAll(String idLanguage)
        {
            return new GeographicAreas().ReadAll(idLanguage);
        }
        public IEnumerable<DbDataRecord> GeographicAreas_ReadById(Int64 idGeographicArea, String idLanguage)
        {
            return new GeographicAreas().ReadById(idGeographicArea, idLanguage);
        }
        public IEnumerable<DbDataRecord> GeographicAreas_ReadByParent(Int64 idParentGeographicArea, String idLanguage)
        {
            return new GeographicAreas().ReadByParent(idParentGeographicArea, idLanguage);
        }
        public IEnumerable<DbDataRecord> GeographicAreas_ReadRoot(String idLanguage)
        {
            return new GeographicAreas().ReadRoot(idLanguage);
        }

        #endregion
        #endregion

        # region GeographicAreas_LG


        #region Write Functions

        public void GeographicAreas_LG_Create(Int64 idGeographicArea, String idLanguage, String name, String description)
        {
            new GeographicAreas_LG().Create(idGeographicArea, idLanguage, name, description);
        }
        public void GeographicAreas_LG_Delete(Int64 idGeographicArea, String idLanguage)
        {
            new GeographicAreas_LG().Delete(idGeographicArea, idLanguage);
        }
        public void GeographicAreas_LG_DeleteByGeographicArea(Int64 idGeographicArea)
        {
            new GeographicAreas_LG().DeleteByGeographicArea(idGeographicArea);
        }
        public void GeographicAreas_LG_Update(Int64 idGeographicArea, String idLanguage, String name, String description)
        {
            new GeographicAreas_LG().Update(idGeographicArea, idLanguage, name, description);
        }

        #endregion

        #region Read Functions

        public IEnumerable<DbDataRecord> GeographicAreas_LG_ReadAll(Int64 idGeographicArea)
        {
            return new GeographicAreas_LG().ReadAll(idGeographicArea);
        }
        public IEnumerable<DbDataRecord> GeographicAreas_LG_ReadById(Int64 idGeographicArea, String idLanguage)
        {
            return new GeographicAreas_LG().ReadById(idGeographicArea, idLanguage);
        }

        #endregion

        #endregion

        # region Sites

        #region Write Functions Sites

        public Int64 Sites_Create(Int64 idParentFacility, Int64 idOrganization, String coordinate, Int64 idResourcePicture, Int64 idGeographicArea, Int64 idFacilityType, Boolean active)
        {
            return new Sites().Create(idParentFacility, idOrganization, coordinate, idResourcePicture, idGeographicArea, idFacilityType, active);
        }
        public void Sites_Delete(Int64 idFacility)
        {
            new Sites().Delete(idFacility);
        }
        public void Sites_Update(Int64 idFacility, Int64 idParentFacility, Int64 idOrganization, String coordinate, Int64 idResourcePicture, Int64 idGeographicArea, Int64 idFacilityType, Boolean active)
        {
            new Sites().Update(idFacility, idParentFacility, coordinate, idOrganization, idResourcePicture, idGeographicArea, idFacilityType, active);
        }

        #endregion

        #region Read Functions

        public IEnumerable<DbDataRecord> Sites_ReadAll(String idLanguage)
        {
            return new Sites().ReadAll(idLanguage);
        }
        public IEnumerable<DbDataRecord> Sites_ReadById(Int64 idFacility, String idLanguage)
        {
            return new Sites().ReadById(idFacility, idLanguage);
        }
        public IEnumerable<DbDataRecord> Sites_ReadByProcess(Int64 idProcess, String idLanguage)
        {
            return new Sites().ReadByProcess(idProcess, idLanguage);
        }
        public IEnumerable<DbDataRecord> Sites_ReadByProcessWhitMeasurements(Int64 IdFacilityType, Int64 idProcess, String idLanguage)
        {
            return new Sites().ReadByProcessWhitMeasurements(IdFacilityType, idProcess, idLanguage);
        }
        public IEnumerable<DbDataRecord> Sites_ReadByParent(Int64 idParentFacility, String idLanguage)
        {
            return new Sites().ReadByParent(idParentFacility, idLanguage);
        }
        public IEnumerable<DbDataRecord> Sites_ReadRoot(String idLanguage)
        {
            return new Sites().ReadRoot(idLanguage);
        }
        public IEnumerable<DbDataRecord> Sites_ReadByOrganization(Int64 idOrganization, String idLanguage)
        {
            return new Sites().ReadByOrganization(idOrganization, idLanguage);
        }
        public IEnumerable<DbDataRecord> Sites_ReadByFacilityType(Int64 idFacilityType, String idLanguage)
        {
            return new Sites().ReadByFacilityType(idFacilityType, idLanguage);
        }
        public IEnumerable<DbDataRecord> Sites_ReadSectorBySiteAndFacilityType(Int64 idSite, Int64 idFacilityType, String idLanguage)
        {
            return new Sites().ReadSectorBySiteAndFacilityType(idSite, idFacilityType, idLanguage);
        }
        public IEnumerable<DbDataRecord> Sites_ReadPlaneByFacilityType(Int64 idFacilityType, String idLanguage)
        {
            return new Sites().ReadPlaneByFacilityType(idFacilityType, idLanguage);
        }
        public IEnumerable<DbDataRecord> Sites_ReadByActivityForOrganization(Int64 IdActivity, Int64 idOrganization, String idLanguage)
        {
            return new Sites().ReadByActivityForOrganization(IdActivity, idOrganization, idLanguage);
        }
        public IEnumerable<DbDataRecord> Sites_ReadTotalMeasurementResultByIndicator(Int64 idScope, Int64 idFacility, Int64 idActivity, Int64 idIndicatorColumnGas, DateTime startDate, DateTime endDate)
        {
            return new Sites().ReadTotalMeasurementResultByIndicator(idScope, idFacility, idActivity, idIndicatorColumnGas, startDate, endDate);
        }
        public IEnumerable<DbDataRecord> Sites_FacilitiesForDashboard(Int64 IdPerson, String idLanguage)
        {
            return new Sites().FacilitiesForDashboard(IdPerson , idLanguage);
        }
        #endregion
        #endregion

        # region Sites_LG


        #region Write Functions

        public void Sites_LG_Create(Int64 idFacility, String idLanguage, String name, String description)
        {
            new Sites_LG().Create(idFacility, idLanguage, name, description);
        }
        public void Sites_LG_Delete(Int64 idFacility, String idLanguage)
        {
            new Sites_LG().Delete(idFacility, idLanguage);
        }
        public void Sites_LG_DeleteByFacility(Int64 idFacility)
        {
            new Sites_LG().DeleteByFacility(idFacility);
        }
        public void Sites_LG_Update(Int64 idFacility, String idLanguage, String name, String description)
        {
            new Sites_LG().Update(idFacility, idLanguage, name, description);
        }

        #endregion

        #region Read Functions

        public IEnumerable<DbDataRecord> Sites_LG_ReadAll(Int64 idFacility)
        {
            return new Sites_LG().ReadAll(idFacility);
        }
        public IEnumerable<DbDataRecord> Sites_LG_ReadById(Int64 idFacility, String idLanguage)
        {
            return new Sites_LG().ReadById(idFacility, idLanguage);
        }

        #endregion

        #endregion

        # region Addresses

        #region Write Functions Addresses

        public Int64 Addresses_Create(Int64 idFacility, Int64 idGeographicArea, Int64 idPerson, String coordinate,
             String street, String number, String floor, String department, String postCode)
        {
            return new Addresses().Create(idFacility, idGeographicArea, idPerson, coordinate, street, number, floor, department, postCode);
        }
        public void Addresses_Delete(Int64 idAddress)
        {
            new Addresses().Delete(idAddress);
        }
        public void Addresses_Update(Int64 idAddress, Int64 idFacility, Int64 idGeographicArea, Int64 idPerson, String coordinate,
             String street, String number, String floor, String department, String postCode)
        {
            new Addresses().Update(idAddress, idFacility, idGeographicArea, idPerson, coordinate, street, number, floor, department, postCode);
        }

        #endregion

        #region Read Functions

        public IEnumerable<DbDataRecord> Addresses_ReadAll()
        {
            return new Addresses().ReadAll();
        }
        public IEnumerable<DbDataRecord> Addresses_ReadById(Int64 idAddress)
        {
            return new Addresses().ReadById(idAddress);
        }
        public IEnumerable<DbDataRecord> Addresses_ReadByFacility(Int64 idFacility)
        {
            return new Addresses().ReadByFacility(idFacility);
        }
        public IEnumerable<DbDataRecord> Addresses_ReadByGeographicArea(Int64 idGeographicArea)
        {
            return new Addresses().ReadByGeographicArea(idGeographicArea);
        }
        public IEnumerable<DbDataRecord> Addresses_ReadByPerson(Int64 IdPerson)
        {
            return new Addresses().ReadByPerson(IdPerson);
        }

        #endregion
        #endregion

        #endregion

        public GeographicInformationSystem()
        {
        }

    }
}
