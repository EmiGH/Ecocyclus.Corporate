using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.KC
{
    public class KnowledgeCollaboration
    {
        public KnowledgeCollaboration(){ }

        # region Public Properties

        # region ResourceClassifications
        #region Read Functions

        public IEnumerable<DbDataRecord> ResourceClassifications_ReadAll(String idLanguage)
        {
            return new Entities.ResourceClassifications().ReadAll(idLanguage);
        }
        public IEnumerable<DbDataRecord> ResourceClassifications_ReadById(Int64 idResourceClassification, String idLanguage)
        {
            return new Entities.ResourceClassifications().ReadById(idResourceClassification, idLanguage);
        }
        public IEnumerable<DbDataRecord> ResourceClassifications_ReadRoot(String idLanguage)
        {
            return new Entities.ResourceClassifications().ReadRoot(idLanguage);
        }
        public IEnumerable<DbDataRecord> ResourceClassifications_ReadByParent(Int64 idResourceClassificationsParent, String idLanguage)
        {
            return new Entities.ResourceClassifications().ReadByParent(idResourceClassificationsParent, idLanguage);
        }
        public IEnumerable<DbDataRecord> ResourceClassifications_ReadByResource(Int64 idResource, String idLanguage)
        {
            return new Entities.ResourceClassifications().ReadByResource(idResource, idLanguage);
        }
        #endregion

        #region Write Functions
        public Int64 ResourceClassifications_Create(Int64 idParentResourceClassification)
        {
            return new Entities.ResourceClassifications().Create(idParentResourceClassification);
        }
        public void ResourceClassifications_Delete(Int64 idResourceClassification)
        {
            new Entities.ResourceClassifications().Delete(idResourceClassification);
        }
        public void ResourceClassifications_Update(Int64 idResourceClassification, Int64 idParentResourceClassification)
        {
            new Entities.ResourceClassifications().Update(idResourceClassification, idParentResourceClassification);
        }
        public void ResourceClassifications_Create(Int64 idResource, Int64 idResourceClassification)
        {
            new Entities.Resources().Create(idResource, idResourceClassification);
        }
        public void ResourceClassifications_Delete(Int64 idResource, Int64 idResourceClassification)
        {
            new Entities.Resources().Delete(idResource, idResourceClassification);
        }
        public void ResourceClassifications_DeleteByResource(Int64 idResource)
        {
            new Entities.Resources().DeleteByResource(idResource);
        }
        public void ResourceClassifications_DeleteByClassification(Int64 idResourceClassification)
        {
            new Entities.Resources().DeleteByClassification(idResourceClassification);
        }
        #endregion
        #endregion

        # region ResourceClassifications_LG
        #region Read Functions

        public IEnumerable<DbDataRecord> ResourceClassifications_LG_ReadAll(Int64 idResourceClassification)
        {
            return new Entities.ResourceClassifications_LG().ReadAll(idResourceClassification);
        }

        public IEnumerable<DbDataRecord> ResourceClassifications_LG_ReadById(Int64 idResourceClassification, String idLanguage)
        {
            return new Entities.ResourceClassifications_LG().ReadById(idResourceClassification, idLanguage);
        }

        #endregion

        #region Write Functions

        public void ResourceClassifications_LG_Create(Int64 idResourceClassification, String idLanguage, String name, String Description)
        {
            new Entities.ResourceClassifications_LG().Create(idResourceClassification, idLanguage, name, Description);
        }

        public void ResourceClassifications_LG_Delete(Int64 idResourceClassification, String idLanguage)
        {
            new Entities.ResourceClassifications_LG().Delete(idResourceClassification, idLanguage);
        }

        public void ResourceClassifications_LG_Delete(Int64 idResourceClassification)
        {
            new Entities.ResourceClassifications_LG().Delete(idResourceClassification);
        }
        public void ResourceClassifications_LG_Update(Int64 idResourceClassification, String idLanguage, String name, String Description)
        {
            new Entities.ResourceClassifications_LG().Update(idResourceClassification, idLanguage, name, Description);
        }

        #endregion
        #endregion

        # region Catalogues
        #region Read Functions
        public IEnumerable<DbDataRecord> Catalogues_ReadAll(Int64 idResource)
        {
            return new Entities.Catalogues().ReadAll(idResource);
        }

        public IEnumerable<DbDataRecord> Catalogues_ReadById(Int64 idResourceFile, Int64 idResource)
        {
            return new Entities.Catalogues().ReadById(idResourceFile, idResource);
        }

        public IEnumerable<DbDataRecord> Catalogues_ReadById(Int64 idResource)
        {
            return new Entities.Catalogues().ReadById(idResource);
        }
        #endregion

        #region Write Functions
        public Int64 Catalogues_Create(Int64 idResource, DateTime timeStamp, Int64 idLogPerson)
        {
            return new Entities.Catalogues().Create(idResource, timeStamp, idLogPerson);
        }
        public void Catalogues_Delete(Int64 idResourceFile, Int64 idResource)
        {
            new Entities.Catalogues().Delete(idResourceFile, idResource);
        }
        public void Catalogues_Delete(Int64 idResource)
        {
            new Entities.Catalogues().Delete(idResource);
        }
        public void Catalogues_Update(Int64 idResourceFile, Int64 idResource, DateTime timeStamp, Int64 idLogPerson)
        {
            new Entities.Catalogues().Update(idResourceFile, idResource, timeStamp, idLogPerson);
        }
        #endregion
        #endregion

        # region FileAttaches
        #region Read Functions

        public IEnumerable<DbDataRecord> FileAttaches_ReadById(Int64 idFile)
        {
            return new Entities.FileAttaches().ReadById(idFile);
        }
        public IEnumerable<DbDataRecord> FileAttaches_ReadFileStream(Int64 idFile)
        {
            return new Entities.FileAttaches().ReadFileStream(idFile);
        }

        #endregion

        #region Write Functions

        public Int64 FileAttaches_Create(String fileName, Byte[] fileStream)
        {
            return new Entities.FileAttaches().Create(fileName, fileStream);
        }

        public void FileAttaches_Update(Int64 idFile, String fileName, Byte[] fileStream)
        {
            new Entities.FileAttaches().Update(idFile, fileName, fileStream);
        }

        public void FileAttaches_Delete(Int64 idFile)
        {
            new Entities.FileAttaches().Delete(idFile);
        }

        #endregion
        #endregion

        # region ResourceDocs
        #region Read Functions
        #endregion

        #region Write Functions

        public void ResourceDocs_Create(Int64 idResourceFile, Int64 idResource, String docType, String docSize, Int64 idFile)
        {
            new Entities.ResourceDocs().Create(idResourceFile, idResource, docType, docSize, idFile);
        }
        public void ResourceDocs_Delete(Int64 idResourceFile, Int64 idResource)
        {
            new Entities.ResourceDocs().Delete(idResourceFile, idResource);
        }
        public void ResourceDocs_Delete(Int64 idResource)
        {
            new Entities.ResourceDocs().Delete(idResource);
        }
        public void ResourceDocs_Update(Int64 idResourceFile, Int64 idResource, String docType, String docSize, Int64 idFile)
        {
            new Entities.ResourceDocs().Update(idResourceFile, idResource, docType, docSize, idFile);
        }

        #endregion
        #endregion

        # region ResourceVersionHistories
        #region Read Functions

        public IEnumerable<DbDataRecord> ResourceVersionHistories_ReadAll(Int64 idResource, String idLanguage)
        {
            return new Entities.ResourceVersionHistories().ReadAll(idResource, idLanguage);
        }

        public IEnumerable<DbDataRecord> ResourceVersionHistories_ReadById(Int64 idResourceFile, Int64 idResource, String idLanguage)
        {
            return new Entities.ResourceVersionHistories().ReadById(idResourceFile, idResource, idLanguage);
        }

        #endregion

        #region Write Functions

        public void ResourceVersionHistories_Create(Int64 idResourceFile, Int64 idResourceState, Int64 idResource, DateTime date, Int64 idGeographicArea, Int64 idFunctionalArea, Int64 idOrganizationPost, Int64 idPerson, Int64 idPosition)
        {
            new Entities.ResourceVersionHistories().Create(idResourceFile, idResourceState, idResource, date, idGeographicArea, idFunctionalArea, idOrganizationPost, idPerson, idPosition);
        }
        public void ResourceVersionHistories_Delete(Int64 idGeographicArea, Int64 idFunctionalArea, Int64 idOrganizationPost, Int64 idPerson, Int64 idPosition)
        {
            new Entities.ResourceVersionHistories().Delete(idGeographicArea, idFunctionalArea, idOrganizationPost, idPerson, idPosition);
        }
        public void ResourceVersionHistories_Delete(Int64 idResourceFile, Int64 idResource, Int64 idResourceState)
        {
            new Entities.ResourceVersionHistories().Delete(idResourceFile, idResource, idResourceState);
        }
        public void ResourceVersionHistories_Delete(Int64 idResourceFile, Int64 idResource)
        {
            new Entities.ResourceVersionHistories().Delete(idResourceFile, idResource);
        }
        public void ResourceVersionHistories_Delete(Int64 idResource)
        {
            new Entities.ResourceVersionHistories().Delete(idResource);
        }

        public void ResourceVersionHistories_Update(Int64 idResourceFile, Int64 idResourceState, Int64 idResource, DateTime date, Int64 idGeographicArea, Int64 idFunctionalArea, Int64 idOrganizationPost, Int64 idPerson, Int64 idPosition)
        {
            new Entities.ResourceVersionHistories().Update(idResourceFile, idResourceState, idResource, date, idGeographicArea, idFunctionalArea, idOrganizationPost, idPerson, idPosition);
        }

        #endregion
        #endregion

        # region ResourceVersions
        #region Read Functions

        public IEnumerable<DbDataRecord> ResourceVersions_ReadAll(Int64 idResource)
        {
            return new Entities.ResourceVersions().ReadAll(idResource);
        }

        public IEnumerable<DbDataRecord> ResourceVersions_ReadById(Int64 idResourceFile, Int64 idResource)
        {
            return new Entities.ResourceVersions().ReadById(idResourceFile, idResource);
        }

        public IEnumerable<DbDataRecord> ResourceVersions_ReadById(Int64 idResource)
        {
            return new Entities.ResourceVersions().ReadById(idResource);
        }
        public IEnumerable<DbDataRecord> ResourceVersions_ReadByException(Int64 idException)
        {
            return new Entities.ResourceVersions().ReadByException(idException);
        }

        #endregion

        #region Write Functions

        public Int64 ResourceVersions_Create(Int64 idResource, DateTime timeStamp, DateTime validFrom, DateTime validThrough, String version, Int64 idLogPerson)
        {
            return new Entities.ResourceVersions().Create(idResource, timeStamp, validFrom, validThrough, version, idLogPerson);
        }

        public void ResourceVersions_Delete(Int64 idResourceFile, Int64 idResource)
        {
            new Entities.ResourceVersions().Delete(idResourceFile, idResource);
        }

        public void ResourceVersions_Delete(Int64 idResource)
        {
            new Entities.ResourceVersions().Delete(idResource);
        }

        public void ResourceVersions_Update(Int64 idResourceFile, Int64 idResource, DateTime timeStamp, DateTime validFrom, DateTime validThrough, String version, Int64 idLogPerson)
        {
            new Entities.ResourceVersions().Update(idResourceFile, idResource, timeStamp, validFrom, validThrough, version, idLogPerson);
        }

        #endregion
        #endregion

        # region ResourceHistoryStates
        #region Read Functions
        public IEnumerable<DbDataRecord> ResourceHistoryStates_ReadAll(String idLanguage)
        {
            return new Entities.ResourceHistoryStates().ReadAll(idLanguage);
        }

        public IEnumerable<DbDataRecord> ResourceHistoryStates_ReadById(Int64 idResourceState, String idLanguage)
        {
            return new Entities.ResourceHistoryStates().ReadById(idResourceState, idLanguage);
        }
        #endregion

        #region Write Functions
        public Int64 ResourceHistoryStates_Create(Int64 idLogPerson)
        {
            return new Entities.ResourceHistoryStates().Create(idLogPerson);
        }
        public void ResourceHistoryStates_Delete(Int64 idResourceState, Int64 idLogPerson)
        {
            new Entities.ResourceHistoryStates().Delete(idResourceState, idLogPerson);
        }

        #endregion
        #endregion

        # region ResourceHistoryStates_LG
        #region Read Functions
        public IEnumerable<DbDataRecord> ResourceHistoryStates_LG_ReadAll(Int64 idResourceFileState)
        {
            return new Entities.ResourceHistoryStates_LG().ReadAll(idResourceFileState);
        }
        public IEnumerable<DbDataRecord> ResourceHistoryStates_LG_ReadById(Int64 idResourceFileState, String idLanguage)
        {
            return new Entities.ResourceHistoryStates_LG().ReadById(idResourceFileState, idLanguage);
        }

        #endregion

        #region Write Functions
        public void ResourceHistoryStates_LG_Create(Int64 idResourceFileState, String idLanguage, String name, String description, Int64 idLogPerson)
        {
            new Entities.ResourceHistoryStates_LG().Create(idResourceFileState, idLanguage, name, description, idLogPerson);
        }
        public void ResourceHistoryStates_LG_Delete(Int64 idResourceFileState, String idLanguage, Int64 idLogPerson)
        {
            new Entities.ResourceHistoryStates_LG().Delete(idResourceFileState, idLanguage, idLogPerson);
        }
        public void ResourceHistoryStates_LG_Delete(Int64 idResourceFileState, Int64 idLogPerson)
        {
            new Entities.ResourceHistoryStates_LG().Delete(idResourceFileState, idLogPerson);
        }
        public void ResourceHistoryStates_LG_Update(Int64 idResourceFileState, String idLanguage, String name, String description, Int64 idLogPerson)
        {
            new Entities.ResourceHistoryStates_LG().Update(idResourceFileState, idLanguage, name, description, idLogPerson);
        }

        #endregion
        #endregion

        # region ResourceUrls
        #region Read Functions
        #endregion

        #region Write Functions

        public void ResourceUrls_Create(Int64 idResourceFile, Int64 idResource, String url)
        {
            new Entities.ResourceUrls().Create(idResourceFile, idResource, url);
        }
        public void ResourceUrls_Delete(Int64 idResourceFile, Int64 idResource)
        {
            new Entities.ResourceUrls().Delete(idResourceFile, idResource);
        }
        public void ResourceUrls_Delete(Int64 idResource)
        {
            new Entities.ResourceUrls().Delete(idResource);
        }
        public void ResourceUrls_Update(Int64 idResourceFile, Int64 idResource, String url)
        {
            new Entities.ResourceUrls().Update(idResourceFile, idResource, url);
        }


        #endregion
        #endregion

        # region Resources
        #region Read Functions
        public IEnumerable<DbDataRecord> Resources_ReadAll(String idLanguage)
        {
            return new Entities.Resources().ReadAll(idLanguage);
        }
        public IEnumerable<DbDataRecord> Resources_ReadById(Int64 idResource, String idLanguage)
        {
            return new Entities.Resources().ReadById(idResource, idLanguage);
        }
        public IEnumerable<DbDataRecord> Resources_ReadByClassification(Int64 idResourceClassification, String idLanguage)
        {
            return new Entities.Resources().ReadByClassification(idResourceClassification, idLanguage);
        }
        public IEnumerable<DbDataRecord> Resources_ReadByType(Int64 idResourceType, String idLanguage)
        {
            return new Entities.Resources().ReadByType(idResourceType, idLanguage);
        }
        public IEnumerable<DbDataRecord> Resources_ReadRoot(String idLanguage)
        {
            return new Entities.Resources().ReadRoot(idLanguage);
        }
        public IEnumerable<DbDataRecord> ReadProcess(Int64 idResource)
        {
            return new Entities.Resources().ReadProcess(idResource);
        }
        #endregion

        #region Write Functions
        public Int64 Resources_Create(Int64 idResourceType, Int64 currentFile, String type)
        {
            return new Entities.Resources().Create(idResourceType, currentFile, type);
        }

        public void Resources_Delete(Int64 idResource)
        {
            new Entities.Resources().Delete(idResource);
        }

        public void Resources_Update(Int64 idResource, Int64 idResourceType)
        {
            new Entities.Resources().Update(idResource, idResourceType);
        }

        public void Resources_UpdateCurrentFile(Int64 idResource, Int64 currentFile)
        {
            new Entities.Resources().UpdateCurrentFile(idResource, currentFile);
        }

        public void Resources_Create(Int64 idResource, Int64 idResourceClassification)
        {
            new Entities.Resources().Create(idResource, idResourceClassification);
        }
        public void Resources_Delete(Int64 idResource, Int64 idResourceClassification)
        {
            new Entities.Resources().Delete(idResource, idResourceClassification);
        }
        public void Resources_DeleteByResource(Int64 idResource)
        {
            new Entities.Resources().DeleteByResource(idResource);
        }

        #endregion
        #endregion

        # region Resources_LG
        #region Read Functions
        public IEnumerable<DbDataRecord> Resources_LG_ReadAll(Int64 idResource)
        {
            return new Entities.Resources_LG().ReadAll(idResource);
        }
        public IEnumerable<DbDataRecord> Resources_LG_ReadById(Int64 idResource, String idLanguage)
        {
            return new Entities.Resources_LG().ReadById(idResource, idLanguage);
        }

        #endregion

        #region Write Functions
        public void Resources_LG_Create(Int64 idResource, String idLanguage, String title, String description)
        {
            new Entities.Resources_LG().Create(idResource, idLanguage, title, description);
        }

        public void Resources_LG_Delete(Int64 idResource, String idLanguage)
        {
            new Entities.Resources_LG().Delete(idResource, idLanguage);
        }

        public void Resources_LG_Delete(Int64 idResource)
        {
            new Entities.Resources_LG().Delete(idResource);
        }

        public void Resources_LG_Update(Int64 idResource, String idLanguage, String title, String description)
        {
            new Entities.Resources_LG().Update(idResource, idLanguage, title, description);
        }

        #endregion
        #endregion

        # region ResourceTypes
        #region Read Functions
        public IEnumerable<DbDataRecord> ResourceTypes_ReadAll(String idLanguage)
        {
            return new Entities.ResourceTypes().ReadAll(idLanguage);
        }

        public IEnumerable<DbDataRecord> ResourceTypes_ReadRoot(String idLanguage)
        {
            return new Entities.ResourceTypes().ReadRoot(idLanguage);
        }

        public IEnumerable<DbDataRecord> ResourceTypes_ReadByParent(Int64 idParentResourceType, String idLanguage)
        {
            return new Entities.ResourceTypes().ReadByParent(idParentResourceType, idLanguage);
        }

        public IEnumerable<DbDataRecord> ResourceTypes_ReadById(Int64 idResourceType, String idLanguage)
        {
            return new Entities.ResourceTypes().ReadById(idResourceType, idLanguage);
        }

        #endregion

        #region Write Functions
        public Int64 ResourceTypes_Create(Int64 idParentResourceType, Int64 idLogPerson)
        {
            return new Entities.ResourceTypes().Create(idParentResourceType, idLogPerson);
        }

        public void ResourceTypes_Delete(Int64 IdResourceType, Int64 idLogPerson)
        {
            new Entities.ResourceTypes().Delete(IdResourceType, idLogPerson);
        }

        public void ResourceTypes_Update(Int64 idResourceType, Int64 idParentResourceType, Int64 idLogPerson)
        {
            new Entities.ResourceTypes().Update(idResourceType, idParentResourceType, idLogPerson);
        }

        #endregion
        #endregion

        # region ResourceTypes_LG
        #region Read Functions
        public IEnumerable<DbDataRecord> ResourceTypes_LG_ReadAll(Int64 idResourceType)
        {
            return new Entities.ResourceTypes_LG().ReadAll(idResourceType);
        }

        public IEnumerable<DbDataRecord> ResourceTypes_LG_ReadById(Int64 idResourceType, String idLanguage)
        {
            return new Entities.ResourceTypes_LG().ReadById(idResourceType, idLanguage);
        }

        #endregion

        #region Write Functions
        public void ResourceTypes_LG_Create(Int64 idResourceType, String idLanguage, String name, String description, Int64 idLogPerson)
        {
            new Entities.ResourceTypes_LG().Create(idResourceType, idLanguage, name, description, idLogPerson);
        }

        public void ResourceTypes_LG_Delete(Int64 idResourceType, String idLanguage, Int64 idLogPerson)
        {
            new Entities.ResourceTypes_LG().Delete(idResourceType, idLanguage, idLogPerson);
        }

        public void ResourceTypes_LG_Delete(Int64 idResourceType, Int64 idLogPerson)
        {
            new Entities.ResourceTypes_LG().Delete(idResourceType, idLogPerson);
        }

        public void ResourceTypes_LG_Update(Int64 idResourceType, String idLanguage, String name, String description, Int64 idLogPerson)
        {
            new Entities.ResourceTypes_LG().Update(idResourceType, idLanguage, name, description, idLogPerson);
        }

        #endregion
        #endregion
       
        #endregion
     
    }
       
}
