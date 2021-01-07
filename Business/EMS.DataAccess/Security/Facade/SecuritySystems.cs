using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;


namespace Condesus.EMS.DataAccess.Security
{
    public class SecuritySystems
    {
        # region Public Properties

        #region Permissions
   
        #region Read Functions

        public IEnumerable<DbDataRecord> Permissions_ReadAll(String idLanguage)
        {
            return new Entities.Permissions().ReadAll(idLanguage);
        }
        public IEnumerable<DbDataRecord> Permissions_ReadById(Int64 idPermission, String idLanguage)
        {
            return new Entities.Permissions().ReadById(idPermission, idLanguage);
        }

        #endregion
        #endregion

        #region Permissions_LG

        #region Write Functions

        public void Permissions_LG_Create(Int64 idPermission, String idLanguage, String name, String description, Int64 idLogPerson)
        {
            new Entities.Permissions_LG().Create( idPermission,  idLanguage,  name,  description,  idLogPerson);
        }
        public void Permissions_LG_Delete(Int64 idPermission, String idLanguage, Int64 idLogPerson)
        {
            new Entities.Permissions_LG().Delete( idPermission,  idLanguage,  idLogPerson);
        }

        public void Permissions_LG_Delete(Int64 idPermission, Int64 idLogPerson)
        {
            new Entities.Permissions_LG().Delete(idPermission, idLogPerson);
        }

        public void Permissions_LG_Update(Int64 idPermission, String idLanguage, String name, String description, Int64 idLogPerson)
        {
            new Entities.Permissions_LG().Update( idPermission,  idLanguage,  name,  description,  idLogPerson);
        }
        #endregion

        #region Read Functions


        public IEnumerable<DbDataRecord> Permissions_LG_ReadAll(Int64 idPermission)
        {
            return new Entities.Permissions_LG().ReadAll(idPermission);
        }
        public IEnumerable<DbDataRecord> Permissions_LG_ReadById(Int64 idPermission, String idLanguage)
        {
            return new Entities.Permissions_LG().ReadById(idPermission, idLanguage);
        }
        #endregion

        #endregion

        #region Rights

        #region Write Functions

        public void Rights_Create(Int64 IdOrganization, Int64 IdPerson, Int64 idPermission, String ClassName, Int64 IdObject)
        {
            new Entities.Rights().Create(IdOrganization, IdPerson, idPermission, ClassName, IdObject);
        }
        public void Rights_Create(Int64 IdOrganization, Int64 IdGeographicArea, Int64 IdFunctionalArea, Int64 IdPosition, Int64 idPermission, String ClassName, Int64 IdObject)
        {
            new Entities.Rights().Create(IdOrganization, IdGeographicArea, IdFunctionalArea, IdPosition, idPermission, ClassName, IdObject);
        }
     
        public void Rights_DeleteJobTitles(Int64 IdOrganization, Int64 IdGeographicArea, Int64 IdFunctionalArea, Int64 IdPosition)
        {
            new Entities.Rights().DeleteJobTitles(IdOrganization, IdGeographicArea, IdFunctionalArea, IdPosition);
        }
        public void Rights_DeleteJobTitles(Int64 IdOrganization, Int64 IdGeographicArea, Int64 IdFunctionalArea, Int64 IdPosition, String ClassName, Int64 IdObject)
        {
            new Entities.Rights().DeleteJobTitles( IdOrganization,  IdGeographicArea,  IdFunctionalArea,  IdPosition,  ClassName,  IdObject);
        }
        public void Rights_DeleteJobTitles(Int64 IdObject, String ClassName)
        {
            new Entities.Rights().DeleteJobTitles(IdObject, ClassName);
        }


        public void Rights_DeletePeople(Int64 IdObject, String ClassName)
        {
            new Entities.Rights().DeletePeople(IdObject, ClassName);
        }
      
        public void Rights_DeletePeople(Int64 IdOrganization, Int64 IdPerson, String ClassName, Int64 IdObject)
        {
            new Entities.Rights().DeletePeople(IdOrganization, IdPerson,  ClassName,  IdObject);
        }
        public void Rights_DeletePeople(Int64 IdOrganization, Int64 IdPerson)
        {
            new Entities.Rights().DeletePeople(IdOrganization, IdPerson);
        }
        #endregion


        #region Read Functions
        public IEnumerable<DbDataRecord> Rights_ReadPermission(Int64 IdOrganization, Int64 IdGeographicArea, Int64 IdFunctionalArea, Int64 IdPosition, Int64 IdPerson, String ClassName, Int64 IdObject)
        {
            return new Entities.Rights().ReadPermission( IdOrganization,  IdGeographicArea,  IdFunctionalArea,  IdPosition,  IdPerson,  ClassName,  IdObject);
        }

        public IEnumerable<DbDataRecord> Rights_ReadPersonByObject(String ClassName, Int64 IdObject)
        {
            return new Entities.Rights().ReadPersonByObject(ClassName, IdObject);
        }

        public IEnumerable<DbDataRecord> Rights_ReadJobTitleByObject(String ClassName, Int64 IdObject)
        {
            return new Entities.Rights().ReadJobTitleByObject(ClassName, IdObject);
        }

        public IEnumerable<DbDataRecord> Rights_ReadByJobTitleAndClassName(Int64 IdOrganization, Int64 IdGeographicArea, Int64 IdFunctionalArea, Int64 IdPosition, String ClassName)
        {
            return new Entities.Rights().ReadByJobTitleAndClassName(IdOrganization, IdGeographicArea, IdFunctionalArea, IdPosition, ClassName);
        }
        public IEnumerable<DbDataRecord> Rights_ReadByJobTitle(Int64 IdOrganization, Int64 IdGeographicArea, Int64 IdFunctionalArea, Int64 IdPosition)
        {
            return new Entities.Rights().ReadByJobTitle(IdOrganization, IdGeographicArea, IdFunctionalArea, IdPosition);
        }

        public IEnumerable<DbDataRecord> Rights_ReadByPersonAndClassName(Int64 IdOrganization, Int64 IdPerson, String ClassName)
        {
            return new Entities.Rights().ReadByPersonAndClassName(IdOrganization, IdPerson, ClassName);
        }

        public IEnumerable<DbDataRecord> Rights_ReadByPersonAndObject(Int64 IdOrganization, Int64 IdPerson, String ClassName, Int64 IdObject)
        {
            return new Entities.Rights().ReadByPersonAndObject(IdOrganization, IdPerson, ClassName, IdObject);
        }
        public IEnumerable<DbDataRecord> Rights_ReadByJobTitleAndObject(Int64 IdOrganization, Int64 IdGeographicArea, Int64 IdFunctionalArea, Int64 IdPosition, String ClassName, Int64 IdObject)
        {
            return new Entities.Rights().ReadByJobTitleAndObject(IdOrganization, IdGeographicArea, IdFunctionalArea, IdPosition, ClassName, IdObject);
        }
        #endregion
        #endregion

        #region Authority
        
        public Boolean Authenticate(String username, String password, String ipAddress)
        {
            return new Entities.Authority().Authenticate(username, password, ipAddress);
        }
        public IEnumerable<DbDataRecord> Authorize(String className, Int64 idObject, Int64 idPerson, String idLanguage)
        {
            return new Entities.Authority().Authorize(className, idObject, idPerson, idLanguage);
        }
        public IEnumerable<DbDataRecord> IsAdmin(Int64 IdOrganization, Int64 IdGeographicArea, Int64 IdFunctionalArea, Int64 IdPosition, Int64 IdPerson, Int64 IdPermission)
        {
            return new Entities.Authority().IsAdmin(IdOrganization, IdGeographicArea, IdFunctionalArea, IdPosition, IdPerson, IdPermission);
        }
        #endregion

        #endregion

        public SecuritySystems() 
        { }

    }
}
