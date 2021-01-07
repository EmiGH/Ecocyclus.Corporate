using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.DS
{
    public class DirectoryServices
    {
        # region Public Properties

        # region ApplicabilityContactTypes
        #region Read Functions
        public IEnumerable<DbDataRecord> ApplicabilityContactTypes_ReadAll(String idLanguage)
        {
            return new ApplicabilityContactTypes().ReadAll(idLanguage);
        }
        #endregion
        #endregion

        # region ApplicabilityContactTypes_LG
        #region Read Functions
        public IEnumerable<DbDataRecord> ApplicabilityContactTypes_LG_ReadAll(Int64 idApplicabilityContactTypes)
        {
            return new ApplicabilityContactTypes_LG().ReadAll(idApplicabilityContactTypes);
        }
        public IEnumerable<DbDataRecord> ApplicabilityContactTypes_LG_ReadById(Int64 idApplicabilityContactTypes, String idLanguage)
        {
            return new ApplicabilityContactTypes_LG().ReadById(idApplicabilityContactTypes, idLanguage);
        }
        #endregion

        #region Write Functions
        public void ApplicabilityContactTypes_LG_Create(Int64 idApplicabilityContactTypes, String idLanguage, String name, Int64 idLogPerson)
        {
            new ApplicabilityContactTypes_LG().Create(idApplicabilityContactTypes, idLanguage, name, idLogPerson);
        }
        public void ApplicabilityContactTypes_LG_Delete(Int64 idApplicabilityContactTypes, String idLanguage, Int64 idLogPerson)
        {
            new ApplicabilityContactTypes_LG().Delete(idApplicabilityContactTypes, idLanguage, idLogPerson);
        }
        public void ApplicabilityContactTypes_LG_Update(Int64 idApplicabilityContactTypes, String idLanguage, String name, Int64 idLogPerson)
        {
            new ApplicabilityContactTypes_LG().Update(idApplicabilityContactTypes, idLanguage, name, idLogPerson);
        }
        #endregion
        #endregion

        # region ContactEmails
        #region Organizations
        #region Write Functions
        public Int64 ContactEmails_AddByOrganization(String email, Int64 idOrganization, Int64 idContactType, Int64 idLogPerson)
        {
            return new ContactEmails().AddByOrganization( email,  idOrganization,  idContactType,  idLogPerson);
        }
        public void ContactEmails_Update(Int64 idContactEmail, String email, Int64 idContactType, Int64 idOrganization, Int64 idLogPerson)
        {
             new ContactEmails().Update(idContactEmail, email, idContactType, idOrganization, idLogPerson);
        }
        public void ContactEmails_RemoveByOrganization(Int64 idContactEmail, Int64 idOrganization, Int64 idLogPerson)
        {
             new ContactEmails().RemoveByOrganization(idContactEmail, idOrganization, idLogPerson);
        }        
        #endregion

        #region Read Functions

        public IEnumerable<DbDataRecord> ContactEmails_ReadById(Int64 idContactEmail, Int64 idOrganization)
        {
            return new ContactEmails().ReadById(idContactEmail, idOrganization);
        }
        public IEnumerable<DbDataRecord> ContactEmails_GetByOrganization(Int64 idOrganization)
        {
            return new ContactEmails().GetByOrganization(idOrganization);
        }
        #endregion
        #endregion 
        
        #region People
        #region Write Functions
        public Int64 ContactEmails_AddByPerson(String email, Int64 idPerson, Int64 idContactType, Int64 idOrganization, Int64 idLogPerson)
        {
            return new ContactEmails().AddByPerson(email, idPerson, idContactType, idOrganization, idLogPerson);
        }
        public void ContactEmails_Update(Int64 idPerson, Int64 idContactEmail, String email, Int64 idContactType, Int64 idOrganization, Int64 idLogPerson)
        {
             new ContactEmails().Update(idPerson, idContactEmail, email, idContactType, idOrganization, idLogPerson);
        }
        public void ContactEmails_RemoveByPerson(Int64 idContactEmail, Int64 idPerson, Int64 idOrganization, Int64 idLogPerson)
        {
             new ContactEmails().RemoveByPerson(idContactEmail, idPerson, idOrganization, idLogPerson);
        }
        #endregion

        #region Read Functions
        public IEnumerable<DbDataRecord> ContactEmails_ReadByIdPerson(Int64 idContactEmail, Int64 idOrganization, Int64 idPerson)
        {
            return new ContactEmails().ReadByIdPerson(idContactEmail, idOrganization, idPerson);
        }
        public IEnumerable<DbDataRecord> ContactEmails_GetByPerson(Int64 idPerson, Int64 idOrganization)
        {
            return new ContactEmails().GetByPerson(idPerson, idOrganization);
        }
        #endregion
        #endregion
        #endregion
        
        # region ContactMessengers
        #region Organizations
        #region Write Functions
        public Int64 ContactMessengers_AddByOrganization(String provider, String application, String data, Int64 idOrganization, Int64 idContactType, Int64 idLogPerson)
        {
            return new ContactMessengers().AddByOrganization( provider,  application,  data,  idOrganization,  idContactType,  idLogPerson);
        }
        public void ContactMessengers_Update(Int64 idContactMessenger, String provider, String application, String data, Int64 idContactType, Int64 idOrganization, Int64 idLogPerson)
        {
             new ContactMessengers().Update( idContactMessenger,  provider,  application,  data,  idContactType,  idOrganization,  idLogPerson);
        }
        public void ContactMessengers_RemoveByOrganization(Int64 idContactMessenger, Int64 idOrganization, Int64 idLogPerson)
        {
             new ContactMessengers().RemoveByOrganization(idContactMessenger, idOrganization, idLogPerson);
        }
        #endregion

        #region Read Functions
        public IEnumerable<DbDataRecord> ContactMessengers_ReadById(Int64 idContactMessenger, Int64 idOrganization)
        {
            return new ContactMessengers().ReadById(idContactMessenger, idOrganization);
        }
        public IEnumerable<DbDataRecord> ContactMessengers_GetByOrganization(Int64 idOrganization)
        {
            return new ContactMessengers().GetByOrganization(idOrganization);
        }
        #endregion
        #endregion

        #region People
        #region Write Functions
        public Int64 ContactMessengers_AddByPerson(String provider, String application, String data, Int64 idPerson, Int64 idContactType, Int64 idOrganization, Int64 idLogPerson)
        {
            return new ContactMessengers().AddByPerson( provider,  application,  data,  idPerson,  idContactType,  idOrganization,  idLogPerson);
        }
        public void ContactMessengers_Update(Int64 idPerson, Int64 idContactMessenger, String provider, String application, String data, Int64 idContactType, Int64 idOrganization, Int64 idLogPerson)
        {
             new ContactMessengers().Update( idPerson,  idContactMessenger,  provider,  application,  data,  idContactType,  idOrganization,  idLogPerson);
        }
        public void ContactMessengers_RemoveByPerson(Int64 idContactMessenger, Int64 idPerson, Int64 idOrganization, Int64 idLogPerson)
        {
             new ContactMessengers().RemoveByPerson( idContactMessenger,  idPerson,  idOrganization,  idLogPerson);
        }
        #endregion

        #region Read Functions
        public IEnumerable<DbDataRecord> ContactMessengers_ReadByIdPerson(Int64 idContactMessenger, Int64 idOrganization, Int64 idPerson)
        {
            return new ContactMessengers().ReadByIdPerson(idContactMessenger, idOrganization, idPerson);
        }
        public IEnumerable<DbDataRecord> ContactMessengers_GetByPerson(Int64 idPerson, Int64 idOrganization)
        {
            return new ContactMessengers().GetByPerson(idPerson, idOrganization);
        }
        #endregion
        #endregion
        #endregion
        
        # region ContactMessengersApplications
        #region Read Functions
        public IEnumerable<DbDataRecord> ContactMessengersApplications_ReadAll(String Provider)
        {
            return new ContactMessengersApplications().ReadAll(Provider);
        }
        public IEnumerable<DbDataRecord> ContactMessengersApplications_ReadById(String Provider, String Application)
        {
            return new ContactMessengersApplications().ReadById(Provider, Application);
        }
        #endregion

        #region Write Functions
        public void ContactMessengersApplications_Create(String Provider, String Application, Int64 idLogPerson)
        {
             new ContactMessengersApplications().Create(Provider, Application, idLogPerson);
        }
        public void ContactMessengersApplications_Delete(String Provider, String Application, Int64 idLogPerson)
        {
             new ContactMessengersApplications().Delete(Provider, Application, idLogPerson);
        }
        #endregion
        #endregion
        
        # region ContactMessengersProviders
        #region Read Functions
        public IEnumerable<DbDataRecord> ContactMessengersProviders_ReadAll()
        {
            return new ContactMessengersProviders().ReadAll();
        }
        public IEnumerable<DbDataRecord> ContactMessengersProviders_ReadById(String Provider)
        {
            return new ContactMessengersProviders().ReadById(Provider);
        }
        #endregion

        #region Write Functions
        public void ContactMessengersProviders_Create(String Provider, Int64 idLogPerson)
        {
            new ContactMessengersProviders().Create(Provider, idLogPerson);
        }
        public void ContactMessengersProviders_Delete(String Provider, Int64 idLogPerson)
        {
            new ContactMessengersProviders().Delete(Provider, idLogPerson);
        }
        #endregion
        #endregion
        
        # region ContactTypes
          

        #region Write Functions

        public Int64 ContactTypes_Create(Int64 applicability, String idLanguage, String name, String description, Int64 idLogPerson)
        {
           return new ContactTypes().Create( applicability,  idLanguage,  name,  description,  idLogPerson);
        }
        public void ContactTypes_Delete(Int64 idContactType, Int64 idLogPerson)
        {
            new ContactTypes().Delete( idContactType,  idLogPerson);
        }
        public void ContactTypes_Update(Int64 idContactType, Int64 applicability, String idLanguage, String name, String description, Int64 idLogPerson)
        {
            new ContactTypes().Update( idContactType,  applicability,  idLanguage,  name,  description,  idLogPerson);
        }

        #endregion

        #region Read Functions

        public IEnumerable<DbDataRecord> ContactTypes_ReadAll(String idLanguage)
        {
            return new ContactTypes().ReadAll(idLanguage);
        }
        public IEnumerable<DbDataRecord> ContactTypes_ReadAll(String idLanguage, Int64 applicability)
        {
            return new ContactTypes().ReadAll(idLanguage,  applicability);
        }
        public IEnumerable<DbDataRecord> ContactTypes_ReadById(Int64 idContactType, String idLanguage) 
        {
            return new ContactTypes().ReadById(idContactType,  idLanguage);
        }

        #endregion
        #endregion

        # region ContactTypes_LG
        #region Read Functions

        public IEnumerable<DbDataRecord> ContactTypes_LG_ReadAll(Int64 idContactType)
        {
            return new ContactTypes_LG().ReadAll(idContactType);
        }
        public IEnumerable<DbDataRecord> ContactTypes_LG_ReadById(Int64 idContactType, String idLanguage)
        {
            return new ContactTypes_LG().ReadById(idContactType, idLanguage);
        }

        #endregion

        #region Write Functions

        public void ContactTypes_LG_Create(Int64 idContactType, String idLanguage, String name, String description, Int64 idLogPerson)
        {
            new ContactTypes_LG().Create( idContactType,  idLanguage,  name,  description,  idLogPerson);
        }
        public void ContactTypes_LG_Delete(Int64 idContactType, String idLanguage, Int64 idLogPerson)
        {
            new ContactTypes_LG().Delete( idContactType,  idLanguage,  idLogPerson);
        }
        public void ContactTypes_LG_Update(Int64 idContactType, String idLanguage, String name, String description, Int64 idLogPerson)
        {
            new ContactTypes_LG().Update( idContactType,  idLanguage,  name,  description,  idLogPerson);
        }

        #endregion

        #endregion

        # region ContactURLs
      

        #region Organizations

        #region Write Functions

        public Int64 ContactURLs_AddByOrganization(String url, String name, String description, String idLanguage, Int64 idOrganization, Int64 idContactType, Int64 idLogPerson)
        {
            return new ContactURLs().AddByOrganization( url,  name,  description,  idLanguage,  idOrganization,  idContactType,  idLogPerson);
        }
        public void ContactURLs_Update(Int64 idContactURL, String url, String name, String description, String idLanguage, Int64 idOrganization, Int64 idContactType, Int64 idLogPerson)
        {
            new ContactURLs().Update( idContactURL,  url,  name,  description,  idLanguage,  idOrganization,  idContactType,  idLogPerson);
        }
        public void ContactURLs_RemoveByOrganization(Int64 idContactURL, Int64 idOrganization, Int64 idLogPerson)
        {
            new ContactURLs().RemoveByOrganization(idContactURL, idOrganization, idLogPerson);
        }
        public void ContactURLs_RemoveByOrganization(Int64 idOrganization)
        {
            new ContactURLs().RemoveByOrganization(idOrganization);
        }
        #endregion

        #region Read Functions

        public IEnumerable<DbDataRecord> ContactURLs_ReadById(Int64 idContactURL, Int64 idOrganization, String idLanguage)
        {
            return new ContactURLs().ReadById( idContactURL,  idOrganization,  idLanguage);
        }
        public IEnumerable<DbDataRecord> ContactURLs_GetByOrganization(Int64 idOrganization, String idLanguage)
        {
            return new ContactURLs().GetByOrganization( idOrganization,  idLanguage);

        }

        #endregion

        #endregion

        #region People

        #region Write Functions

        public Int64 ContactURLs_AddByPerson(String url, String name, String description, String idLanguage, Int64 idPerson, Int64 idContactType, Int64 idOrganization, Int64 idLogPerson)
        {
            return new ContactURLs().AddByPerson( url,  name,  description,  idLanguage,  idPerson,  idContactType,  idOrganization,  idLogPerson);
        }
        public void ContactURLs_Update(Int64 idPerson, Int64 idContactURL, String url, String name, String description, String idLanguage, Int64 idContactType, Int64 idOrganization, Int64 idLogPerson)
        {
            new ContactURLs().Update( idPerson,  idContactURL,  url,  name,  description,  idLanguage,  idContactType,  idOrganization,  idLogPerson);
        }
        public void ContactURLs_RemoveByPerson(Int64 idContactURL, Int64 idPerson, String idLanguage, Int64 idOrganization, Int64 idLogPerson)
        {
            new ContactURLs().RemoveByPerson( idContactURL,  idPerson,  idLanguage,  idOrganization,  idLogPerson);
        }
    
        #endregion

        #region Read Functions

        public IEnumerable<DbDataRecord> ContactURLs_ReadByIdPerson(Int64 idContactURL, Int64 idPerson, Int64 idOrganization, String idLanguage)
        {
            return new ContactURLs().ReadByIdPerson( idContactURL,  idPerson,  idOrganization,  idLanguage);
        }
        public IEnumerable<DbDataRecord> ContactURLs_GetByPerson(Int64 idPerson, Int64 idOrganization, String idLanguage)
        {
            return new ContactURLs().GetByPerson( idPerson,  idOrganization,  idLanguage);
        }

        #endregion

        #endregion
        #endregion

        # region ContactURLs_LG
     

        #region Write Functions

        public void ContactURLs_LG_Create(Int64 idContactURLs, String idLanguage, String name, String description, Int64 idLogPerson)
        {
            new ContactURLs_LG().Create( idContactURLs,  idLanguage,  name,  description,  idLogPerson);
        }
        public void ContactURLs_LG_Delete(Int64 idContactURLs, String idLanguage, Int64 idLogPerson)
        {
            new ContactURLs_LG().Delete( idContactURLs,  idLanguage,  idLogPerson); 
        }
        public void ContactURLs_LG_Update(Int64 idContactURLs, String idLanguage, String name, String description, Int64 idLogPerson)
        {
            new ContactURLs_LG().Update( idContactURLs,  idLanguage,  name,  description,  idLogPerson);
        }

        #endregion

        #region Read Functions

        public IEnumerable<DbDataRecord> ContactURLs_LG_ReadAll(Int64 idContactURLs)
        {
            return new ContactURLs_LG().ReadAll(idContactURLs);
        }
        public IEnumerable<DbDataRecord> ContactURLs_LG_ReadById(Int64 idContactURLs, String idLanguage)
        {
            return new ContactURLs_LG().ReadById(idContactURLs, idLanguage);
        }

        #endregion

        #endregion

        # region Countries
      
        #region Write Functions


        public Int64 Countries_Create(String idLanguage, String alpha, String name, String internationalCode, Int64 idLogPerson)
        {
            return new Countries().Create( idLanguage,  alpha,  name,  internationalCode,  idLogPerson);
        }
        public void Countries_Delete(Int64 idCountry, Int64 idLogPerson)
        {
            new Countries().Delete( idCountry,  idLogPerson);
        }
        public void Countries_Update(Int64 idCountry, String idLanguage, String alpha, String name, String internationalCode, Int64 idLogPerson)
        {
            new Countries().Update(idCountry, idLanguage, alpha, name, internationalCode, idLogPerson);
        }
        #endregion

        #region Read Functions

        public IEnumerable<DbDataRecord> Countries_ReadAll(String idLanguage)
        {
            return new Countries().ReadAll(idLanguage);
        }
        public IEnumerable<DbDataRecord> Countries_ReadById(Int64 idCountry, String idLanguage)
        {
            return new Countries().ReadById(idCountry, idLanguage);
        }
        #endregion
        #endregion

        # region Countries_LG
 
        #region Write Functions

       public void Countries_LG_Create(Int64 idCountry, String idLanguage, String name, Int64 idLogPerson)
        {
            new Countries_LG().Create( idCountry,  idLanguage,  name,  idLogPerson);
        }
       public void Countries_LG_Delete(Int64 idCountry, String idLanguage, Int64 idLogPerson)
        {
            new Countries_LG().Delete( idCountry,  idLanguage,  idLogPerson);
        }
       public void Countries_LG_Update(Int64 idCountry, String idLanguage, String name, Int64 idLogPerson)
        {
            new Countries_LG().Update( idCountry,  idLanguage,  name,  idLogPerson);
        }
        #endregion

        #region Read Functions
       public IEnumerable<DbDataRecord> Countries_LG_ReadAll(Int64 idCountry)
        {
            return new Countries_LG().ReadAll(idCountry);
        }
       public IEnumerable<DbDataRecord> Countries_LG_ReadById(Int64 idCountry, String idLanguage)
        {
            return new Countries_LG().ReadById(idCountry, idLanguage);
        }
        #endregion
        #endregion

        # region DashBoard

       #region Write Functions
       public void DashBoard_Create(Int64 idPerson, Int64 idOrganization, String idGadget, String configuration)
       {
           new Entities.DashBoard().Create(idPerson, idOrganization, idGadget, configuration);
       }
       public void DashBoard_Delete(Int64 idPerson, Int64 idOrganization)
       {
           new Entities.DashBoard().Delete(idPerson,idOrganization);
       }
       public void DashBoard_Delete(Int64 idPerson, Int64 idOrganization, String idGadget)
       {
           new Entities.DashBoard().Delete(idPerson,idOrganization,idGadget);
       }
       #endregion

       #region Read Functions

       public IEnumerable<DbDataRecord> DashBoard_ReadByPerson(Int64 idPerson, Int64 idOrganization, String idLanguage)
       {
           return new Entities.DashBoard().ReadByPerson(idPerson, idOrganization, idLanguage);
       }  
       #endregion
       #endregion

        # region FunctionalAreas
   
        #region Write Functions

       public Int64 FunctionalAreas_Create(Int64 idOrganization, Int64 idParentFunctionalArea, String idLanguage, String mnemo, String name, Int64 idLogPerson)
        {
               return new FunctionalAreas().Create( idOrganization,  idParentFunctionalArea,  idLanguage,  mnemo,  name,  idLogPerson);
        }
       public void FunctionalAreas_Delete(Int64 idFunctionalArea, Int64 idOrganization, Int64 idLogPerson)
        {
               new FunctionalAreas().Delete( idFunctionalArea,  idOrganization,  idLogPerson);         
        }
       public void FunctionalAreas_Update(Int64 idFunctionalArea, Int64 idParentFunctionalArea, Int64 idOrganization, String idLanguage, String name, String mnemo, Int64 idLogPerson)
        {
               new FunctionalAreas().Update( idFunctionalArea,  idParentFunctionalArea,  idOrganization,  idLanguage,  name,  mnemo,  idLogPerson); 
        }

        #endregion

        #region Read Functions

       public IEnumerable<DbDataRecord> FunctionalAreas_GetRoot(Int64 idOrganization, String idLanguage)
        {
            return new FunctionalAreas().GetRoot(idOrganization, idLanguage);
        }
       public IEnumerable<DbDataRecord> FunctionalAreas_GetByParent(Int64 idParentFunctionalArea, Int64 idOrganization, String idLanguage)
        {
           return new FunctionalAreas().GetByParent( idParentFunctionalArea,  idOrganization,  idLanguage);
        }
       public IEnumerable<DbDataRecord> FunctionalAreas_ReadAll(Int64 idOrganization, String idLanguage)
        {
            return new FunctionalAreas().ReadAll(idOrganization, idLanguage);
        }
       public IEnumerable<DbDataRecord> FunctionalAreas_ReadById(Int64 idFunctionalArea, Int64 idOrganization, String idLanguage)
        {
            return new FunctionalAreas().ReadById( idFunctionalArea,  idOrganization,  idLanguage);
        }

        #endregion

        #endregion

        # region FunctionalAreas_LG
   
        #region Write Functions

       public void FunctionalAreas_LG_Create(Int64 idFunctionalArea, Int64 idOrganization, String idLanguage, String name, Int64 idLogPerson)
        {
                new FunctionalAreas_LG().Create( idFunctionalArea,  idOrganization,  idLanguage,  name,  idLogPerson);
        }

       public void FunctionalAreas_LG_Delete(Int64 idFunctionalArea, Int64 idOrganization, String idLanguage, Int64 idLogPerson)
        {
                new FunctionalAreas_LG().Delete( idFunctionalArea,  idOrganization,  idLanguage,  idLogPerson);
        }

       public void FunctionalAreas_LG_Update(Int64 idFunctionalArea, Int64 idOrganization, String idLanguage, String name, Int64 idLogPerson)
        {
                new FunctionalAreas_LG().Update( idFunctionalArea,  idOrganization,  idLanguage,  name,  idLogPerson);
        }

        #endregion

        #region Read Functions

       public IEnumerable<DbDataRecord> FunctionalAreas_LG_ReadAll(Int64 idFunctionalArea, Int64 idOrganization)
        {
                return new FunctionalAreas_LG().ReadAll( idFunctionalArea,  idOrganization);
        }

       public IEnumerable<DbDataRecord> FunctionalAreas_LG_ReadById(Int64 idFunctionalArea, Int64 idOrganization, String idLanguage)
        {
                return new FunctionalAreas_LG().ReadById( idFunctionalArea,  idOrganization,  idLanguage);
        }

        #endregion

        #endregion

        # region FunctionalPositions

        #region Read Function

       public IEnumerable<DbDataRecord> FunctionalPositions_ReadAll(Int64 idOrganization)
        {
            return new FunctionalPositions().ReadAll(idOrganization);
        }
       public IEnumerable<DbDataRecord> FunctionalPositions_ReadById(Int64 idPosition, Int64 idFunctionalArea, Int64 idOrganization)
        {
            return new FunctionalPositions().ReadById( idPosition,  idFunctionalArea,  idOrganization);
        }
       public IEnumerable<DbDataRecord> FunctionalPositions_GetByParent(Int64 idPosition, Int64 idFunctionalArea, Int64 idOrganization)
        {
            return new FunctionalPositions().GetByParent( idPosition,  idFunctionalArea,  idOrganization);
        }
       public IEnumerable<DbDataRecord> FunctionalPositions_GetRoot(Int64 idOrganization)
        {
            return new FunctionalPositions().GetRoot(idOrganization);
        }

        #endregion

        #region Write Function

       public void FunctionalPositions_Delete(Int64 idPosition, Int64 idFunctionalArea, Int64 idOrganization, Int64 idLogPerson)
        {
            new FunctionalPositions().Delete( idPosition,  idFunctionalArea,  idOrganization,  idLogPerson);
        }
       public void FunctionalPositions_Create(Int64 idPosition, Int64 idFunctionalArea, Int64 idOrganization, Int64 idParentPosition, Int64 idParentFunctionalArea, Int64 idLogPerson)
        {
            new FunctionalPositions().Create( idPosition,  idFunctionalArea,  idOrganization,  idParentPosition,  idParentFunctionalArea,  idLogPerson);
        }
       public void FunctionalPositions_Update(Int64 idPosition, Int64 idFunctionalArea, Int64 idOrganization, Int64 idParentPosition, Int64 idParentFunctionalArea, Int64 idLogPerson)
        {
            new FunctionalPositions().Update( idPosition,  idFunctionalArea,  idOrganization,  idParentPosition,  idParentFunctionalArea,  idLogPerson);
        }

        #endregion

        #endregion

        # region GeographicAreas
   
        #region Write Functions GeographicAreas

       public Int64 GeographicAreas_Create(Int64 idOrganization, Int64 idParentGeographicArea, String idLanguage, String name, String description, Int64 idLogPerson)
        {
            return new GeographicAreas().Create( idOrganization,  idParentGeographicArea,  idLanguage,  name,  description,  idLogPerson);
        }
       public void GeographicAreas_Delete(Int64 idOrganization, Int64 idGeographicArea, Int64 idLogPerson)
        {
            new GeographicAreas().Delete( idOrganization,  idGeographicArea,  idLogPerson);
        }
       public void GeographicAreas_Update(Int64 idGeographicArea, Int64 idParentGeographicArea, Int64 idOrganization, String idLanguage, String name, String description, Int64 idLogPerson)
        {
            new GeographicAreas().Update( idGeographicArea,  idParentGeographicArea,  idOrganization,  idLanguage,  name,  description,  idLogPerson);
        }

        #endregion

        #region Write Functions GeographicAreas_Facilities

       public Int64 GeographicAreas_Create(Int64 idOrganization, Int64 idParentGeographicArea, String idLanguage, String mnemo, String name, String description, Int64 idResourcePicture, Int64 idLogPerson)
        {
            return new GeographicAreas().Create( idOrganization,  idParentGeographicArea,  idLanguage,  mnemo,  name,  description, idResourcePicture, idLogPerson);
        }
       public void GeographicAreas_Update(Int64 idGeographicArea, Int64 idParentGeographicArea, Int64 idOrganization, String idLanguage, String mnemo, String name, String description, Int64 idResourcePicture, Int64 idLogPerson)
        {
            new GeographicAreas().Update( idGeographicArea,  idParentGeographicArea,  idOrganization,  idLanguage,  mnemo,  name,  description, idResourcePicture, idLogPerson);
        }

        #endregion

        #region Read Functions

       public IEnumerable<DbDataRecord> GeographicAreas_ReadAll(Int64 idOrganization, String idLanguage)
        {
            return new GeographicAreas().ReadAll( idOrganization,  idLanguage);
        }
       public IEnumerable<DbDataRecord> GeographicAreas_ReadById(Int64 idGeographicArea, String idLanguage)
        {
            return new GeographicAreas().ReadById( idGeographicArea,  idLanguage);
        }
       public IEnumerable<DbDataRecord> GeographicAreas_GetByParent(Int64 idParentGeographicArea, Int64 idOrganization, String idLanguage)
        {
            return new GeographicAreas().GetByParent( idParentGeographicArea,  idOrganization,  idLanguage);
        }
       public IEnumerable<DbDataRecord> GeographicAreas_GetRoot(Int64 idOrganization, String idLanguage)
        {
            return new GeographicAreas().GetRoot(idOrganization, idLanguage);
        }

        #endregion
        #endregion

        # region GeographicAreas_LG_


        #region Write Functions

       public void GeographicAreas_LG_Create(Int64 idGeographicArea, Int64 idOrganization, String idLanguage, String description, String name, Int64 idLogPerson)
        {
            new GeographicAreas_LG().Create( idGeographicArea,  idOrganization,  idLanguage,  description,  name,  idLogPerson);
        }
       public void GeographicAreas_LG_Delete(Int64 idGeographicArea, Int64 idOrganization, String idLanguage, Int64 idLogPerson)
        {
            new GeographicAreas_LG().Delete( idGeographicArea,  idOrganization,  idLanguage,  idLogPerson);
        }
       public void GeographicAreas_LG_Update(Int64 idGeographicArea, Int64 idOrganization, String idLanguage, String description, String name, Int64 idLogPerson)
        {
            new GeographicAreas_LG().Update( idGeographicArea,  idOrganization,  idLanguage,  description,  name,  idLogPerson);
        }

        #endregion

        #region Read Functions

       public IEnumerable<DbDataRecord> GeographicAreas_LG_ReadAll(Int64 idGeographicArea, Int64 idOrganization)
        {
            return new  GeographicAreas_LG().ReadAll( idGeographicArea,  idOrganization);
        }
       public IEnumerable<DbDataRecord> GeographicAreas_LG_ReadById(Int64 idGeographicArea, Int64 idOrganization, String idLanguage)
        {
            return new GeographicAreas_LG().ReadById( idGeographicArea,  idOrganization,  idLanguage);
        }

        #endregion

        #endregion

        # region GeographicFunctionalAreas

        #region Read Functions

       public IEnumerable<DbDataRecord> GeographicFunctionalAreas_ReadAll(Int64 idOrganization)
        {
            return new GeographicFunctionalAreas().ReadAll(idOrganization);
        }
       public IEnumerable<DbDataRecord> GeographicFunctionalAreas_ReadById(Int64 idFunctionalArea, Int64 idGeographicArea, Int64 idOrganization)
        {
            return new GeographicFunctionalAreas().ReadById( idFunctionalArea,  idGeographicArea,  idOrganization);
        }
       public IEnumerable<DbDataRecord> GeographicFunctionalAreas_GetByParent(Int64 idParentFunctionalArea, Int64 idParentGeographicArea, Int64 idOrganization)
        {
            return new GeographicFunctionalAreas().GetByParent( idParentFunctionalArea,  idParentGeographicArea,  idOrganization);
        }
       public IEnumerable<DbDataRecord> GeographicFunctionalAreas_GetRoot(Int64 idOrganization)
        {
            return new GeographicFunctionalAreas().GetRoot(idOrganization);
        }

        #endregion

        #region Write Functions

       public void GeographicFunctionalAreas_Delete(Int64 idFunctionalArea, Int64 idGeographicArea, Int64 idOrganization, Int64 idLogPerson)
        {
            new GeographicFunctionalAreas().Delete( idFunctionalArea,  idGeographicArea,  idOrganization,  idLogPerson);
        }
       public void GeographicFunctionalAreas_Create(Int64 idFunctionalArea, Int64 idGeographicArea, Int64 idOrganization, Int64 idParentFunctionalArea, Int64 idParentGeographicArea, Int64 idLogPerson)
        {
            new GeographicFunctionalAreas().Create( idFunctionalArea,  idGeographicArea,  idOrganization,  idParentFunctionalArea,  idParentGeographicArea,  idLogPerson);
        }
       public void GeographicFunctionalAreas_Update(Int64 idFunctionalArea, Int64 idGeographicArea, Int64 idOrganization, Int64 idParentFunctionalArea, Int64 idParentGeographicArea, Int64 idLogPerson)
        {
            new GeographicFunctionalAreas().Update( idFunctionalArea,  idGeographicArea,  idOrganization,  idParentFunctionalArea,  idParentGeographicArea,  idLogPerson);
        }

        #endregion

        #endregion

        # region JobTitles
 

        #region Write Functions
            public void JobTitles_Delete(Int64 idGeographicArea, Int64 idPosition, Int64 idFunctionalArea, Int64 idLogPerson, Int64 idOrganization)
            {
                new JobTitles().Delete( idGeographicArea,  idPosition,  idFunctionalArea,  idLogPerson,  idOrganization);
            }
            public void JobTitles_Create(Int64 idGeographicArea, Int64 idPosition, Int64 idFunctionalArea, Int64 idLogPerson, Int64 idOrganization)
            {
                new JobTitles().Create( idGeographicArea,  idPosition,  idFunctionalArea,  idLogPerson,  idOrganization);
            }
            public void JobTitles_CreateRelationship(Int64 idOrganizationalChart, Int64 idOrganization, Int64 idGeographicArea, Int64 idPosition, Int64 idFunctionalArea, Int64 idGeographicAreaParent, Int64 idPositionParent, Int64 idFunctionalAreaParent)
            {
                new JobTitles().CreateRelationship(idOrganizationalChart,  idOrganization,  idGeographicArea,  idPosition,  idFunctionalArea,  idGeographicAreaParent,  idPositionParent,  idFunctionalAreaParent);
            }
            public void JobTitles_DeleteRelationship(Int64 idOrganizationalChart, Int64 idOrganization, Int64 idGeographicArea, Int64 idPosition, Int64 idFunctionalArea)
            {
                new JobTitles().DeleteRelationship(idOrganizationalChart, idOrganization, idGeographicArea, idPosition, idFunctionalArea);
            }
            public void JobTitles_DeleteRelationship(Int64 idGeographicArea, Int64 idPosition, Int64 idFunctionalArea)
            {
                new JobTitles().DeleteRelationship(idGeographicArea, idPosition, idFunctionalArea);
            }
            public void JobTitles_DeleteRelationship(Int64 idOrganizationalChart, Int64 idOrganization)
            {
                new JobTitles().DeleteRelationship(idOrganizationalChart, idOrganization);
            }
            public void JobTitles_UpdateRelationship(Int64 idOrganizationalChart, Int64 idOrganization, Int64 idGeographicArea, Int64 idPosition, Int64 idFunctionalArea, Int64 idGeographicAreaParent, Int64 idPositionParent, Int64 idFunctionalAreaParent)
            {
                new JobTitles().UpdateRelationship(idOrganizationalChart,  idOrganization,  idGeographicArea,  idPosition,  idFunctionalArea,  idGeographicAreaParent,  idPositionParent,  idFunctionalAreaParent);
            }
        #endregion

        #region Read Functions
       public IEnumerable<DbDataRecord> JobTitles_ReadRoot(Int64 idOrganizationalChart, Int64 idOrganization)
       {
           return new JobTitles().ReadRoot(idOrganizationalChart, idOrganization);
       }
       public IEnumerable<DbDataRecord> JobTitles_ReadByJobTitle(Int64 idOrganizationalChart, Int64 idOrganization, Int64 idGeographicAreaParent, Int64 idPositionParent, Int64 idFunctionalAreaParent)
        {
            return new JobTitles().ReadByJobTitle(idOrganizationalChart, idOrganization, idGeographicAreaParent, idPositionParent, idFunctionalAreaParent);
        }
       public IEnumerable<DbDataRecord> JobTitles_ReadParent(Int64 idOrganizationalChart, Int64 idOrganization, Int64 idGeographicArea, Int64 idPosition, Int64 idFunctionalArea)
       {
           return new JobTitles().ReadParent(idOrganizationalChart, idOrganization, idGeographicArea, idPosition, idFunctionalArea);
       }
       public IEnumerable<DbDataRecord> JobTitles_ReadAll(Int64 idOrganization)
        {
            return new JobTitles().ReadAll(idOrganization);
        }
       public IEnumerable<DbDataRecord> JobTitles_ReadById(Int64 idGeographicArea, Int64 idPosition, Int64 idFunctionalArea, Int64 idOrganization)
        {
            return new JobTitles().ReadById( idGeographicArea,  idPosition,  idFunctionalArea,  idOrganization);
        }
       public IEnumerable<DbDataRecord> JobTitles_GetByClassification(Int64 idClassification, Int64 idOrganization)
        {
            return new JobTitles().GetByClassification( idClassification,  idOrganization);
        }
              

        #endregion
        #endregion

        # region Languages
               
        #region Write Function

       public void Languages_Create(String idLanguage, String name, Boolean enable, Int64 idLogonPerson)
        {
            new Languages().Create( idLanguage,  name,  enable,  idLogonPerson);
        }
       public void Languages_Delete(String idLanguage, Int64 idLogonPerson)
        {
            new Languages().Delete( idLanguage,  idLogonPerson);
        }
       public void Languages_Update(String idLanguage, String name, Boolean enable, Int64 idLogonPerson)
        {
            new Languages().Update( idLanguage,  name,  enable,  idLogonPerson);
        }

        #endregion

        #region Read Function

       public IEnumerable<DbDataRecord> Languages_ReadAll()
        {
            return new Languages().ReadAll();
        }
       public IEnumerable<DbDataRecord> Languages_GetEnabled()
        {
            return new Languages().GetEnabled();
        }
       public IEnumerable<DbDataRecord> Languages_ReadById(String idLanguage)
        {
            return new Languages().ReadById(idLanguage);
        }
       public IEnumerable<DbDataRecord> Languages_GetDefault()
        {
            return new Languages().GetDefault();
        }

        #endregion
        #endregion
    
        # region OrganizationalCharts

        #region Write Functions GeographicAreas

       public Int64 OrganizationalCharts_Create(Int64 idOrganization)
        {
            return new Entities.OrganizationalCharts().Create(idOrganization);
        }
       public void OrganizationalCharts_Delete(Int64 idOrganization, Int64 idOrganizationalChart)
        {
            new Entities.OrganizationalCharts().Delete(idOrganization, idOrganizationalChart);
        }
  
        #endregion
  
        #region Read Functions

       public IEnumerable<DbDataRecord> OrganizationalCharts_ReadAll(Int64 idOrganization, String idLanguage)
        {
            return new Entities.OrganizationalCharts().ReadAll(idOrganization, idLanguage);
        }
       public IEnumerable<DbDataRecord> OrganizationalCharts_ReadById(Int64 idOrganizationalChart, Int64 idOrganization, String idLanguage)
        {
            return new Entities.OrganizationalCharts().ReadById(idOrganizationalChart, idOrganization, idLanguage);
        }
       public IEnumerable<DbDataRecord> OrganizationalCharts_ReadByJobTitle(Int64 idGeographicArea, Int64 idFunctionalArea, Int64 idPosition, Int64 idOrganization)
       {
           return new Entities.OrganizationalCharts().ReadByJobTitle(idGeographicArea, idFunctionalArea, idPosition, idOrganization);
       }
   
        #endregion
       #endregion

        # region OrganizationalCharts_LG

        #region Write Functions

       public void OrganizationalCharts_LG_Create(Int64 IdOrganizationalChart, Int64 IdOrganization, String idLanguage, String name, String Description)
        {
           new Entities.OrganizationalCharts_LG().Create( IdOrganizationalChart,  IdOrganization,  idLanguage,  name,  Description);
        }
       public void OrganizationalCharts_LG_Delete(Int64 IdOrganizationalChart, Int64 IdOrganization, String idLanguage)
        {
           new Entities.OrganizationalCharts_LG().Delete( IdOrganizationalChart,  IdOrganization,  idLanguage);
        }
       public void OrganizationalCharts_LG_Delete(Int64 IdOrganizationalChart, Int64 IdOrganization)
       {
           new Entities.OrganizationalCharts_LG().Delete(IdOrganizationalChart, IdOrganization);
       }
       public void OrganizationalCharts_LG_Update(Int64 IdOrganizationalChart, Int64 IdOrganization, String idLanguage, String name, String Description)
        {
           new Entities.OrganizationalCharts_LG().Update( IdOrganizationalChart,  IdOrganization,  idLanguage,  name,  Description);
        }
        #endregion

        #region Read Functions
       public IEnumerable<DbDataRecord> OrganizationalCharts_LG_ReadAll(Int64 IdOrganizationalChart, Int64 IdOrganization)
        {
           return new Entities.OrganizationalCharts_LG().ReadAll( IdOrganizationalChart,  IdOrganization);
        }
       public IEnumerable<DbDataRecord> OrganizationalCharts_LG_ReadById(Int64 IdOrganizationalChart, Int64 IdOrganization, String idLanguage)
        {
           return new Entities.OrganizationalCharts_LG().ReadById( IdOrganizationalChart,  IdOrganization,  idLanguage);
        }
        #endregion

       #endregion

        # region OrganizationClassifications

       #region Write Functions

       public Int64 OrganizationClassifications_Create(Int64 idParentOrganizationClassification)
        {
            return new OrganizationClassifications().Create(idParentOrganizationClassification);
        }
        public void OrganizationClassifications_Delete(Int64 idOrganizationClassification)
        {
            new OrganizationClassifications().Delete(idOrganizationClassification);
        }
        public void OrganizationClassifications_Update(Int64 idOrganizationClassification, Int64 idParentOrganizationClassification)
        {
            new OrganizationClassifications().Update( idOrganizationClassification, idParentOrganizationClassification);
        }

        #endregion

        #region Read Functions

        public IEnumerable<DbDataRecord> OrganizationClassifications_ReadAll(String idLanguage)
        {
            return new OrganizationClassifications().ReadAll( idLanguage);
        }
        public IEnumerable<DbDataRecord> OrganizationClassifications_ReadById(Int64 idOrganizationClassification, String idLanguage)
        {
            return new OrganizationClassifications().ReadById( idOrganizationClassification,  idLanguage);
        }
        public IEnumerable<DbDataRecord> OrganizationClassifications_ReadRoot(String idLanguage)
        {
            return new OrganizationClassifications().ReadRoot(idLanguage);
        }
        public IEnumerable<DbDataRecord> OrganizationClassifications_ReadByParent(Int64 idParentOrganizationClassification, String idLanguage)
        {
            return new OrganizationClassifications().ReadByParent( idParentOrganizationClassification,  idLanguage);
        }
        public IEnumerable<DbDataRecord> OrganizationClassifications_ReadByOrganization(Int64 idOrganization, String idLanguage)
        {
            return new OrganizationClassifications().ReadByOrganization( idOrganization,  idLanguage);
        }

        #endregion
        #endregion

        # region OrganizationClassifications_LG
 

        #region Write Functions

        public void OrganizationClassifications_LG_Create(Int64 idOrganizationClassification, String idLanguage, String name, String Description)
        {
            new OrganizationClassifications_LG().Create( idOrganizationClassification,  idLanguage,  name,  Description);
        }

        public void OrganizationClassifications_LG_Delete(Int64 idOrganizationClassification, String idLanguage)
        {
            new OrganizationClassifications_LG().Delete( idOrganizationClassification,  idLanguage);
        }

        public void OrganizationClassifications_LG_Delete(Int64 idOrganizationClassification)
        {
            new OrganizationClassifications_LG().Delete(idOrganizationClassification);
        }

        public void OrganizationClassifications_LG_Update(Int64 idOrganizationClassification, String idLanguage, String name, String Description)
        {
            new OrganizationClassifications_LG().Update( idOrganizationClassification,  idLanguage,  name,  Description);
        }

        #endregion

        #region Read Functions

        public IEnumerable<DbDataRecord> OrganizationClassifications_LG_ReadAll(Int64 idOrganizationClassification)
        {
            return new OrganizationClassifications_LG().ReadAll( idOrganizationClassification);
        }
        public IEnumerable<DbDataRecord> OrganizationClassifications_LG_ReadById(Int64 idOrganizationClassification, String idLanguage)
        {
            return new OrganizationClassifications_LG().ReadById( idOrganizationClassification,  idLanguage);
        }
        #endregion
        #endregion

        # region Organizations
               
        #region Read Functions

        public IEnumerable<DbDataRecord> Organizations_ReadAll()
        {
            return new Organizations().ReadAll();
        }

        public IEnumerable<DbDataRecord> Organizations_ReadById(Int64 idOrganization)
        {
            return new Organizations().ReadById(idOrganization);
        }

        public IEnumerable<DbDataRecord> Organizations_ReadRoot(String className, Int64 idPerson)
        {
            return new Organizations().ReadRoot(className, idPerson);
        }

        public IEnumerable<DbDataRecord> Organizations_ReadByClassification(Int64 idOrganizationClassification, String className, Int64 idPerson)
        {
            return new Organizations().ReadByClassification( idOrganizationClassification,  className,  idPerson);
        }


        public IEnumerable<DbDataRecord> Organizations_ReadByPerson(Int64 idPerson, String className)
        {
            return new Organizations().ReadByPerson(idPerson, className);
        }
        #endregion

        #region Write Functions

        public Int64 Organizations_Create(String name, String corporateName, String fiscalIdentification, Int64 idResourcePicture)
        {
            return  new Organizations().Create( name,  corporateName,  fiscalIdentification, idResourcePicture);
        }
        public void Organizations_Delete(Int64 idOrganization)
        {
            new Organizations().Delete( idOrganization);
        }
        public void Organizations_Delete()
        {
            new Organizations().Delete();
        }
        public void Organizations_Update(Int64 idOrganization, String name, String corporateName, String fiscalIdentification, Int64 idResourcePicture)
        {
            new Organizations().Update( idOrganization,  name,  corporateName,  fiscalIdentification, idResourcePicture);
        }


        #endregion

        #region OrganizationClassificationOrganizations

        public void Organizations_Create(Int64 idOrganization, Int64 idOrganizationClassification)
        {
            new Organizations().Create( idOrganization,  idOrganizationClassification);
        }
        public void Organizations_Delete(Int64 idOrganization, Int64 idOrganizationClassification)
        {
            new Organizations().Delete( idOrganization,  idOrganizationClassification);
        }
        public void Organizations_DeleteByOrganization(Int64 idOrganization)
        {
            new Organizations().DeleteByOrganization(idOrganization);
        }
        public void Organizations_DeleteByClassification(Int64 idOrganizationClassification)
        {
            new Organizations().DeleteByClassification(idOrganizationClassification);
        }
        #endregion


        #endregion

        # region OrganizationsRelationships
        
        #region Write Functions

        public void OrganizationsRelationships_Create(Int64 idOrganization1, Int64 idOrganization2, Int64 idOrganizationRelationshipType, Int64 idLogPerson)
        {
            new OrganizationsRelationships().Create( idOrganization1,  idOrganization2,  idOrganizationRelationshipType,  idLogPerson);
        }
        public void OrganizationsRelationships_Delete(Int64 idOrganization1, Int64 idOrganization2, Int64 idOrganizationRelationshipType, Int64 idLogPerson)
        {
            new OrganizationsRelationships().Delete( idOrganization1,  idOrganization2,  idOrganizationRelationshipType,  idLogPerson);
        }
        public void OrganizationsRelationships_Delete(Int64 idOrganization)
        {
            new OrganizationsRelationships().Delete(idOrganization);
        }

        #endregion

        #region Read Functions

        public IEnumerable<DbDataRecord> OrganizationsRelationships_GetAllByIdOrganization(Int64 idOrganization)
        {
               return new OrganizationsRelationships().GetAllByIdOrganization( idOrganization);
        }
        public IEnumerable<DbDataRecord> OrganizationsRelationships_ReadById(Int64 idOrganization1, Int64 idOrganization2, Int64 idOrganizationRelationshipType)
        {
               return new OrganizationsRelationships().ReadById( idOrganization1,  idOrganization2,  idOrganizationRelationshipType);
        }
        
        #endregion
        #endregion

        # region OrganizationsRelationshipsTypes
   
        #region Write Functions

        public Int64 OrganizationsRelationshipsTypes_Create(String idLanguage, String name, String description, Int64 idLogPerson)
        {
            return new OrganizationsRelationshipsTypes().Create( idLanguage,  name,  description,  idLogPerson);
        }
        public void OrganizationsRelationshipsTypes_Delete(Int64 idOrganizationRelationshipType, String idLanguage, Int64 idLogPerson)
        {
            new OrganizationsRelationshipsTypes().Delete( idOrganizationRelationshipType,  idLanguage,  idLogPerson);
        }
        public void OrganizationsRelationshipsTypes_Update(Int64 idOrganizationRelationshipType, String idLanguage, String name, String description, Int64 idLogPerson)
        {
            new OrganizationsRelationshipsTypes().Update( idOrganizationRelationshipType,  idLanguage,  name,  description,  idLogPerson);
        }

        #endregion

        #region Read Functions

        public IEnumerable<DbDataRecord> OrganizationsRelationshipsTypes_ReadAll(String idLanguage)
        {
            return new OrganizationsRelationshipsTypes().ReadAll(idLanguage);
        }
        public IEnumerable<DbDataRecord> OrganizationsRelationshipsTypes_ReadById(Int64 idOrganizationRelationshipType, String idLanguage)
        {
            return new OrganizationsRelationshipsTypes().ReadById( idOrganizationRelationshipType,  idLanguage);
        }

        #endregion
        #endregion

        # region OrganizationsRelationShipsTypes_LG

        #region Read Functions

        public IEnumerable<DbDataRecord> OrganizationsRelationShipsTypes_LG_ReadAll(Int64 idOrganizationRelationshipType)
        {
            return new OrganizationsRelationShipsTypes_LG().ReadAll( idOrganizationRelationshipType);
        }
        public IEnumerable<DbDataRecord> OrganizationsRelationShipsTypes_LG_ReadById(Int64 idOrganizationRelationshipType, String idLanguage)
        {
            return new OrganizationsRelationShipsTypes_LG().ReadById( idOrganizationRelationshipType,  idLanguage);
        }

        #endregion

        #region Write Functions

        public void OrganizationsRelationShipsTypes_LG_Create(Int64 idOrganizationRelationshipType, String idLanguage, String name, String description, Int64 idLogPerson)
        {
            new OrganizationsRelationShipsTypes_LG().Create( idOrganizationRelationshipType,  idLanguage,  name,  description,  idLogPerson);
        }
        public void OrganizationsRelationShipsTypes_LG_Delete(Int64 idOrganizationRelationshipType, String idLanguage, Int64 idLogPerson)
        {
            new OrganizationsRelationShipsTypes_LG().Delete( idOrganizationRelationshipType,  idLanguage,  idLogPerson);
        }
        public void OrganizationsRelationShipsTypes_LG_Update(Int64 idOrganizationRelationshipType, String idLanguage, String name, String description, Int64 idLogPerson)
        {
            new OrganizationsRelationShipsTypes_LG().Update( idOrganizationRelationshipType,  idLanguage,  name,  description,  idLogPerson);
        }

        #endregion
        #endregion

        # region People
 
        #region Write Functions

        public Int64 People_Create(Int64 idSalutationType, Int64 idOrganization, String lastName, String firstName, String posName, String nickName, Int64 idResourcePicture)
        {
            return new People().Create(idSalutationType, idOrganization, lastName, firstName, posName, nickName, idResourcePicture);
         }
        public void People_Delete(Int64 idPerson, Int64 idOrganization, Int64 idLogPerson)
        {
            new People().Delete( idPerson,  idOrganization,  idLogPerson);
        }
        public void People_Update(Int64 idPerson, Int64 idSalutationType, Int64 idOrganization, String lastName, String firstName, String posName, String nickName, Int64 idResourcePicture)
        {
            new People().Update(idPerson, idSalutationType, idOrganization, lastName, firstName, posName, nickName, idResourcePicture);
        }

        #endregion

        #region Read Functions

        public IEnumerable<DbDataRecord> People_ReadAll(Int64 idOrganization)
        {
            return new People().ReadAll(idOrganization);
        }
        public IEnumerable<DbDataRecord> People_ReadById(Int64 idPerson)
        {
            return new People().ReadById(idPerson);
        }
        public IEnumerable<DbDataRecord> People_Exists(Int64 idPerson, Int64 idOrganization)
        {
            return new People().Exists(idPerson, idOrganization);
        }
        #endregion
        #endregion

        # region Positions_
  
        #region Write Functions

        public Int64 Positions_Create(Int64 idOrganization, String idLanguage, String name, String description, Int64 idLogPerson)
        {
            return new Positions().Create( idOrganization,  idLanguage,  name,  description,  idLogPerson);
        }

        public void Positions_Delete(Int64 idPosition, Int64 idOrganization, Int64 idLogPerson)
        {
            new Positions().Delete( idPosition,  idOrganization,  idLogPerson);
        }
        public void Positions_Update(Int64 idPosition, Int64 idOrganization, String idLanguage, String name, String description, Int64 idLogPerson)
        {
            new Positions().Update( idPosition,  idOrganization,  idLanguage,  name,  description,  idLogPerson);
        }

        #endregion

        #region Read Functions

        public IEnumerable<DbDataRecord> Positions_ReadAll(Int64 idOrganization, String idLanguage)
        {
            return new Positions().ReadAll(idOrganization, idLanguage);
        }
        public IEnumerable<DbDataRecord> Positions_ReadById(Int64 idPosition, Int64 idOrganization, String idLanguage)
        {
            return new Positions().ReadById( idPosition,  idOrganization,  idLanguage);
        }

        #endregion
        #endregion

        # region Positions_LG


        #region Write Functions

        public void Positions_LG_Create(Int64 idPosition, Int64 idOrganization, String idLanguage, String description, String name, Int64 idLogPerson)
        {
            new Positions_LG().Create( idPosition,  idOrganization,  idLanguage,  description,  name,  idLogPerson);
        }
        public void Positions_LG_Delete(Int64 idPosition, Int64 idOrganization, String idLanguage, Int64 idLogPerson)
        {
            new Positions_LG().Delete( idPosition,  idOrganization,  idLanguage,  idLogPerson);
        }
        public void Positions_LG_Update(Int64 idPosition, Int64 idOrganization, String idLanguage, String name, String description, Int64 idLogPerson)
        {
            new Positions_LG().Update( idPosition,  idOrganization,  idLanguage,  name,  description,  idLogPerson);
        }

        #endregion

        #region Read Functions

        public IEnumerable<DbDataRecord> Positions_LG_ReadAll(Int64 idPosition, Int64 idOrganization)
        {
            return new Positions_LG().ReadAll(idPosition, idOrganization);
        }
        public IEnumerable<DbDataRecord> Positions_LG_ReadById(Int64 idPosition, Int64 idOrganization, String idLanguage)
        {
            return new Positions_LG().ReadById( idPosition,  idOrganization,  idLanguage);
        }

        #endregion
        #endregion

        # region Posts
 
        #region Read Functions

        public IEnumerable<DbDataRecord> Posts_ReadAll(Int64 idPerson, Int64 idOrganization)
        {
            return new Posts().ReadAll(idPerson, idOrganization);
        }
        public IEnumerable<DbDataRecord> Posts_ReadAll(Int64 idOrganization)
        {
            return new Posts().ReadAll(idOrganization);
        }
        public IEnumerable<DbDataRecord> Posts_ReadByJobTitle(Int64 idPosition, Int64 idGeographicArea, Int64 idFunctionalArea, Int64 idOrganization)
        {
            return new Posts().ReadByJobTitle( idPosition,  idGeographicArea,  idFunctionalArea,  idOrganization);
        }

        public IEnumerable<DbDataRecord> Posts_ReadById(Int64 idPosition, Int64 idGeographicArea, Int64 idFunctionalArea, Int64 idPerson, Int64 idOrganization)
        {
            return new Posts().ReadById( idPosition,  idGeographicArea,  idFunctionalArea,  idPerson,  idOrganization);
        }

        #endregion

        #region Write Functions

        public void Posts_Delete(Int64 idPosition, Int64 idGeographicArea, Int64 idFunctionalArea, Int64 idPerson, Int64 idOrganization, Int64 idLogPerson)
        {
            new Posts().Delete( idPosition,  idGeographicArea,  idFunctionalArea,  idPerson,  idOrganization,  idLogPerson);
        }

        public void Posts_Create(Int64 idPosition, Int64 idGeographicArea, Int64 idFunctionalArea, Int64 idPerson, Int64 idOrganization, DateTime startDate, DateTime endDate, Int64 idLogPerson)
        {
            new Posts().Create( idPosition,  idGeographicArea,  idFunctionalArea,  idPerson,  idOrganization,  startDate,  endDate,  idLogPerson);
        }

        public void Posts_Update(Int64 idPosition, Int64 idGeographicArea, Int64 idFunctionalArea, Int64 idPerson, Int64 idOrganization, DateTime startDate, DateTime endDate, Int64 idLogPerson)
        {
            new Posts().Update( idPosition,  idGeographicArea,  idFunctionalArea,  idPerson,  idOrganization,  startDate,  endDate,  idLogPerson);
        }
        #endregion
        #endregion

        # region Presences
 
        #region Read Functions

        public IEnumerable<DbDataRecord> Presences_ReadAll(Int64 idPosition, Int64 idGeographicArea, Int64 idFunctionalArea, Int64 idPerson, Int64 idOrganization)
        {
            return new Presences().ReadAll( idPosition,  idGeographicArea,  idFunctionalArea,  idPerson,  idOrganization);
        }
        public IEnumerable<DbDataRecord> Presences_ReadById(Int64 idPosition, Int64 idGeographicArea, Int64 idFunctionalArea, Int64 idPerson, Int64 idFacility, Int64 idOrganization)
        {
            return new Presences().ReadById( idPosition,  idGeographicArea,  idFunctionalArea,  idPerson,  idFacility,  idOrganization);
        }
       
        #endregion

        #region Write Functions

        public void Presences_Delete(Int64 idPosition, Int64 idGeographicArea, Int64 idFunctionalArea, Int64 idPerson, Int64 idFacility, Int64 idOrganization, Int64 idLogPerson)
        {
            new Presences().Delete( idPosition,  idGeographicArea,  idFunctionalArea,  idPerson,  idFacility,  idOrganization,  idLogPerson);
        }
        public void Presences_Create(Int64 idPosition, Int64 idGeographicArea, Int64 idFunctionalArea, Int64 idPerson, Int64 idFacility, Int64 idOrganization, Int64 idLogPerson)
        {
            new Presences().Create( idPosition,  idGeographicArea,  idFunctionalArea,  idPerson,  idFacility,  idOrganization,  idLogPerson);
        }
        
        #endregion
        #endregion

        # region Responsibilities
 
        #region Read Functions

        public IEnumerable<DbDataRecord> Responsibilities_ReadAll(Int64 idGeographicArea, Int64 idFunctionalArea, Int64 idPosition, Int64 idOrganization)
        {
            return new Responsibilities().ReadAll( idGeographicArea,  idFunctionalArea,  idPosition,  idOrganization);
        }
        public IEnumerable<DbDataRecord> Responsibilities_ReadById(Int64 idPosition, Int64 idGeographicArea, Int64 idFunctionalArea, Int64 idGeographicAreaResponsibility, Int64 idFunctionalAreaResponsibility, Int64 idOrganization)
        {
            return new Responsibilities().ReadById( idPosition,  idGeographicArea,  idFunctionalArea,  idGeographicAreaResponsibility,  idFunctionalAreaResponsibility,  idOrganization);
        }
       
        #endregion

        #region Write Functions

        public void Responsibilities_Delete(Int64 idFunctionalArea, Int64 idGeographicArea, Int64 idPosition, Int64 idFunctionalAreaResponsibility, Int64 idGeographicAreaResponsibility, Int64 idOrganization, Int64 idLogPerson)
        {
            new Responsibilities().Delete( idFunctionalArea,  idGeographicArea,  idPosition,  idFunctionalAreaResponsibility,  idGeographicAreaResponsibility,  idOrganization,  idLogPerson);
        }
        public void Responsibilities_DeleteByJobTitle(Int64 idFunctionalArea, Int64 idGeographicArea, Int64 idPosition, Int64 idOrganization, Int64 idLogPerson)
        {
            new Responsibilities().Delete(idFunctionalArea, idGeographicArea, idPosition, idOrganization, idLogPerson);
        }
        public void Responsibilities_Create(Int64 idFunctionalArea, Int64 idGeographicArea, Int64 idPosition, Int64 idFunctionalAreaResponsibility, Int64 idGeographicAreaResponsibility, Int64 idOrganization, Int64 idLogPerson)
        {
            new Responsibilities().Create( idFunctionalArea,  idGeographicArea,  idPosition,  idFunctionalAreaResponsibility,  idGeographicAreaResponsibility,  idOrganization,  idLogPerson);
        }

        #endregion
        #endregion   

        # region SalutationTypes

        #region Read Functions

        public IEnumerable<DbDataRecord> SalutationTypes_ReadAll(String idLanguage)
        {
            return new SalutationTypes().ReadAll(idLanguage);
        }
        public IEnumerable<DbDataRecord> SalutationTypes_ReadById(Int64 idSalutationType, String idLanguage)
        {
            return new SalutationTypes().ReadById( idSalutationType,  idLanguage); 
        }
        
        #endregion

        #region Write Functions

        public Int64 SalutationTypes_Create(String idLanguage, String name, String description, Int64 idLogPerson)
        {
            return new SalutationTypes().Create( idLanguage,  name,  description,  idLogPerson); 
        }
        public void SalutationTypes_Delete(Int64 idSalutationType, Int64 idLogPerson)
        {
            new SalutationTypes().Delete(idSalutationType,idLogPerson);
        }
        public void SalutationTypes_Update(Int64 idSalutationType, String idLanguage, String name, String description, Int64 idLogPerson)
        {
            new SalutationTypes().Update( idSalutationType,  idLanguage,  name,  description,  idLogPerson);
        }

        #endregion
        #endregion

        # region SalutationTypes_LG
      
        #region Read Functions

        public IEnumerable<DbDataRecord> SalutationTypes_LG_ReadAll(Int64 idSalutationType)
        {
            return new SalutationTypes_LG().ReadAll(idSalutationType);
        }
        public IEnumerable<DbDataRecord> SalutationTypes_LG_ReadById(Int64 idSalutationType, String idLanguage)
        {
            return new SalutationTypes_LG().ReadById( idSalutationType,  idLanguage);
        }

        #endregion

        #region Write Functions

        public void SalutationTypes_LG_Create(Int64 idSalutationTypes, String idLanguage, String name, String description, Int64 idLogPerson)
        {
            new SalutationTypes_LG().Create( idSalutationTypes,  idLanguage,  name,  description,  idLogPerson);
        }
        public void SalutationTypes_LG_Delete(Int64 idSalutationTypes, String idLanguage, Int64 idLogPerson)        
        {
            new SalutationTypes_LG().Delete( idSalutationTypes,  idLanguage,  idLogPerson);        
        }
        public void SalutationTypes_LG_Update(Int64 idSalutationTypes, String idLanguage, String name, String description, Int64 idLogPerson)
        {
            new SalutationTypes_LG().Update( idSalutationTypes,  idLanguage,  name,  description,  idLogPerson);
        }

        #endregion
        #endregion     

        #region Telephones

        #region Write Functions Addresses

        public Int64 Telephones_Create(String areaCode, String number, String extension, String internationalCode)
        {
            return new Telephones().Create(areaCode, number,extension,internationalCode);
        }
        public void Telephones_Delete(Int64 idTelephone)
        {
            new Telephones().Delete(idTelephone);
        }
        public void Telephones_Update(Int64 idTelephone, String areaCode, String number, String extension, String internationalCode)
        {
            new Telephones().Update(idTelephone,areaCode,number,extension, internationalCode);
        }

        #endregion

        #region Read Functions

        public IEnumerable<DbDataRecord> Telephones_ReadAll()
        {
            return new Telephones().ReadAll();
        }
        public IEnumerable<DbDataRecord> Telephones_ReadById(Int64 idTelephone)
        {
            return new Telephones().ReadById(idTelephone);
        }

        #endregion
        #endregion

        #region TelephoneFacilities
        
        #region Write Functions Addresses

        public void TelephoneFacilities_Create(Int64 idTelephone, Int64 idFacility, String reason)
        {
            new TelephoneFacilities().Create(idTelephone,idFacility,reason);
        }
        public void TelephoneFacilities_Delete(Int64 idTelephone, Int64 idFacility)
        {
            new TelephoneFacilities().Delete(idTelephone,idFacility);
        }
        public void TelephoneFacilities_DeleteByTelephone(Int64 idTelephone)
        {
            new TelephoneFacilities().DeleteByTelephone(idTelephone);
        }
        public void TelephoneFacilities_Update(Int64 idTelephone, Int64 idFacility, String reason)
        {
            new TelephoneFacilities().Update(idTelephone,idFacility,reason);
        }


        #endregion

        #region Read Functions

        public IEnumerable<DbDataRecord> TelephoneFacilities_ReadAll(Int64 idFacility)
        {
           return new TelephoneFacilities().ReadAll(idFacility);
        }
        public IEnumerable<DbDataRecord> TelephoneFacilities_ReadById(Int64 idTelephone, Int64 idFacility)
        {
           return new TelephoneFacilities().ReadById(idTelephone, idFacility);
        }

        #endregion
        #endregion

        #region TelephonePeople

        #region Write Functions Addresses

        public void TelephonePeople_Create(Int64 idTelephone, Int64 idPerson, Int64 IdOrganization,String reason)
        {
            new TelephonePeople().Create(idTelephone, idPerson,IdOrganization ,reason);
        }
        public void TelephonePeople_Delete(Int64 idTelephone, Int64 idPerson)
        {
            new TelephonePeople().Delete(idTelephone, idPerson);
        }
        public void TelephonePeople_DeleteByTelephone(Int64 idTelephone)
        {
            new TelephonePeople().DeleteByTelephone(idTelephone);
        }
        public void TelephonePeople_Update(Int64 idTelephone, Int64 idPerson, Int64 IdOrganization,String reason)
        {
            new TelephonePeople().Update(idTelephone, idPerson, IdOrganization, reason);
        }


        #endregion

        #region Read Functions

        public IEnumerable<DbDataRecord> TelephonePeople_ReadAll(Int64 idPerson)
        {
            return new TelephonePeople().ReadAll(idPerson);
        }
        public IEnumerable<DbDataRecord> TelephonePeople_ReadById(Int64 idTelephone, Int64 idPerson)
        {
            return new TelephonePeople().ReadById(idTelephone, idPerson);
        }

        #endregion
        #endregion

        # region Users
 
        #region Write Functions
        public void Users_Create(Int64 idPerson, String userName, String password, Boolean active, Int64 idOrganization, Boolean changePasswordOnNextLogin, Boolean cannotChangePassword, Boolean passwordNeverExpires, Int64 idLogPerson, Boolean ViewGlobalMenu)
                {
                    new Users().Create( idPerson,  userName,  password,  active,  idOrganization,  changePasswordOnNextLogin,  cannotChangePassword,  passwordNeverExpires,  idLogPerson, ViewGlobalMenu);
                }
            public void Users_Delete(String userName, Int64 idLogPerson)
                {
                    new Users().Delete( userName,  idLogPerson);
                }
            public void Users_Update(Int64 idPerson, String userName, Boolean active, Int64 idOrganization, Boolean changePasswordOnNextLogin, Boolean cannotChangePassword, Boolean passwordNeverExpires, Int64 idLogPerson, Boolean ViewGlobalMenu)
                {
                    new Users().Update( idPerson,  userName,  active,  idOrganization,  changePasswordOnNextLogin,  cannotChangePassword,  passwordNeverExpires,  idLogPerson, ViewGlobalMenu);
                }
            public void Users_Update(String userName, String password, Int64 idLogPerson)
                {
                    new Users().Update( userName,  password,  idLogPerson);
                }
            public void Users_ResetPassword(String Username, String NewPassword, Int64 idLogPerson)
                {
                    new Users().ResetPassword( Username,  NewPassword,  idLogPerson);
                }
        #endregion

        #region Read Functions
            public IEnumerable<DbDataRecord> Users_ReadAll(Int64 idOrganization)
                {
                    return new Users().ReadAll(idOrganization);
                }
            public IEnumerable<DbDataRecord> Users_ReadById(String userName)
                {
                    return new Users().ReadById(userName);
                }
            public IEnumerable<DbDataRecord> Users_GetByPerson(Int64 idPerson, Int64 idOrganization)
                {
                    return new Users().GetByPerson( idPerson,  idOrganization);
                }
            public IEnumerable<DbDataRecord> Users_GetPasswordHistory(String userName)
            {
                return new Users().GetPasswordHistory(userName);
            }
        #endregion
        #endregion
        
        #endregion

        public DirectoryServices()
        { 
        }

    }

}
