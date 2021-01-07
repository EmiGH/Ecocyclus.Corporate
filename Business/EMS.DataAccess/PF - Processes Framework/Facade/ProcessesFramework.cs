using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.DataAccess.PF.Entities;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.PF
{
    public class ProcessesFramework
    {
        # region Public Properties

        # region ParticipationTypes

        #region Read Functions

        public IEnumerable<DbDataRecord> ParticipationTypes_ReadAll(String idLanguage)
        {
            return new Entities.ParticipationTypes().ReadAll(idLanguage);
        }
        public IEnumerable<DbDataRecord> ParticipationTypes_ReadById(Int64 idParticipationType, String idLanguage)
        {
            return new Entities.ParticipationTypes().ReadById(idParticipationType, idLanguage);
        }

        #endregion

        #region Write Functions

        public Int64 ParticipationTypes_Create(Int64 idLogPerson)
        {
            return new Entities.ParticipationTypes().Create(idLogPerson);
        }
        public void ParticipationTypes_Delete(Int64 idParticipationType, Int64 idLogPerson)
        {
            new Entities.ParticipationTypes().Delete(idParticipationType, idLogPerson);
        }

        #endregion
        #endregion

        # region ParticipationTypes_LG

        #region Write Functions

        public void ParticipationTypes_LG_Create(Int64 idParticipationType, String idLanguage, String name, Int64 idLogPerson)
        {
            new Entities.ParticipationTypes_LG().Create(idParticipationType, idLanguage, name, idLogPerson);
        }
        public void ParticipationTypes_LG_Delete(Int64 idParticipationType, String idLanguage, Int64 idLogPerson)
        {
            new Entities.ParticipationTypes_LG().Delete(idParticipationType, idLanguage, idLogPerson);
        }

        public void ParticipationTypes_LG_Delete(Int64 idParticipationType, Int64 idLogPerson)
        {
            new Entities.ParticipationTypes_LG().Delete(idParticipationType, idLogPerson);
        }
        public void ParticipationTypes_LG_Update(Int64 idParticipationType, String idLanguage, String name, Int64 idLogPerson)
        {
            new Entities.ParticipationTypes_LG().Update(idParticipationType, idLanguage, name, idLogPerson);
        }
        #endregion

        #region Read Functions
        public IEnumerable<DbDataRecord> ParticipationTypes_LG_ReadAll(Int64 idParticipationType)
        {
            return new Entities.ParticipationTypes_LG().ReadAll(idParticipationType);
        }

        public IEnumerable<DbDataRecord> ParticipationTypes_LG_ReadById(Int64 idParticipationType, String idLanguage)
        {
            return new Entities.ParticipationTypes_LG().ReadById(idParticipationType, idLanguage);
        }
        #endregion
        #endregion

        # region ProcessClassifications

        #region Read Functions

        public IEnumerable<DbDataRecord> ProcessClassifications_ReadAll(String idLanguage)
        {
            return new ProcessClassifications().ReadAll(idLanguage);
        }
        public IEnumerable<DbDataRecord> ProcessClassifications_ReadById(Int64 idProcessClassification, String idLanguage)
        {
            return new ProcessClassifications().ReadById(idProcessClassification, idLanguage);
        }
        public IEnumerable<DbDataRecord> ProcessClassifications_ReadRoot(String idLanguage)
        {
            return new ProcessClassifications().ReadRoot(idLanguage);
        }
        public IEnumerable<DbDataRecord> ProcessClassifications_ReadByParent(Int64 idParentProcessClassifications, String idLanguage)
        {
            return new ProcessClassifications().ReadByParent(idParentProcessClassifications, idLanguage);
        }
        public IEnumerable<DbDataRecord> ProcessClassifications_ReadByProcess(Int64 idParentProcessClassifications, String idLanguage)
        {
            return new ProcessClassifications().ReadByProcess(idParentProcessClassifications, idLanguage);
        }
        #endregion

        #region Write Functions
        public Int64 ProcessClassifications_Create(Int64 idParentProcessClassification)
        {
            return new ProcessClassifications().Create(idParentProcessClassification);
        }
        public void ProcessClassifications_Delete(Int64 idProcessClassification)
        {
            new ProcessClassifications().Delete(idProcessClassification);
        }
        public void ProcessClassifications_Update(Int64 idProcessClassification, Int64 idParentProcessClassification)
        {
            new ProcessClassifications().Update(idProcessClassification, idParentProcessClassification);
        }
        #endregion

        #region ProjectClassification related with Projects
        public void ProcessClassifications_DeleteRaltionship(Int64 idProcessCalssification)
        {
            new Entities.ProcessClassifications().DeleteRaltionship(idProcessCalssification);
        }
        public void ProcessClassifications_DeleteScenarioTypes(Int64 idProcessCalssification)
        {
            new Entities.ProcessClassifications().DeleteScenarioTypes(idProcessCalssification);
        }
        #endregion
        #endregion

        # region ProcessClassifications_LG

        #region Write Functions
        public void ProcessClassifications_LG_Create(Int64 idProcessClassification, String idLanguage, String name, String Description)
        {
            new ProcessClassifications_LG().Create(idProcessClassification, idLanguage, name, Description);
        }
        public void ProcessClassifications_LG_Delete(Int64 idProcessClassification, String idLanguage)
        {
            new ProcessClassifications_LG().Delete(idProcessClassification, idLanguage);
        }
        public void ProcessClassifications_LG_Delete(Int64 idProcessClassification)
        {
            new ProcessClassifications_LG().Delete(idProcessClassification);
        }
        public void ProcessClassifications_LG_Update(Int64 idProcessClassification, String idLanguage, String name, String Description)
        {
            new ProcessClassifications_LG().Update(idProcessClassification, idLanguage, name, Description);
        }
        #endregion

        #region Read Functions
        public IEnumerable<DbDataRecord> ProcessClassifications_LG_ReadAll(Int64 idProcessClassification)
        {
            return new ProcessClassifications_LG().ReadAll(idProcessClassification);
        }
        public IEnumerable<DbDataRecord> ProcessClassifications_LG_ReadById(Int64 idProcessClassification, String idLanguage)
        {
            return new ProcessClassifications_LG().ReadById(idProcessClassification, idLanguage);
        }
        #endregion
        #endregion

        # region Processes
        #region Read Functions
        public IEnumerable<DbDataRecord> Processes_ReadById(Int64 idProcess, String idLanguage)
        {
            return new Processes().ReadById(idProcess, idLanguage);
        }
        public IEnumerable<DbDataRecord> Processes_ReadByResource(Int64 idResource)
        {
            return new Processes().ReadByResource(idResource);
        }

        #endregion
        #region Write Functions
        public Int64 Processes_Create(Int16 weight, Int32 orderNumber)
        {
            return new Processes().Create(weight, orderNumber);
        }
        public void Processes_Update(Int64 idProcess, Int16 weight, Int32 orderNumber)
        {
            new Processes().Update(idProcess, weight, orderNumber);
        }
        public void Processes_Delete(Int64 idProcess)
        {
            new Processes().Delete(idProcess);
        }
        #endregion
        #endregion

        # region Processes_LG
        #region Read Functions
        public IEnumerable<DbDataRecord> Processes_LG_ReadAll(Int64 idProcess)
            {
                return new Processes_LG().ReadAll(idProcess);
            }
        public IEnumerable<DbDataRecord> Processes_LG_ReadById(Int64 idProcess, String idLanguage)
            {
                return new Processes_LG().ReadById(idProcess, idLanguage);
            }
        #endregion
        #region Write Functions
        public void Processes_LG_Create(Int64 idProcess, String idLanguage, String title, String purpose, String description)
            {
                new Processes_LG().Create(idProcess, idLanguage, title,purpose, description);
            }
        public void Processes_LG_Delete(Int64 idProcess, String idLanguage)
            {
                new Processes_LG().Delete(idProcess, idLanguage);
            }
        public void Processes_LG_Delete(Int64 idProcess)
        {
            new Processes_LG().Delete(idProcess);
        }
        public void Processes_LG_Update(Int64 idProcess, String idLanguage, String title, String purpose, String description)
            {
                new Processes_LG().Update(idProcess, idLanguage, title, purpose, description);        
            }
        #endregion        
        #endregion

        # region ProcessGroupExceptions
        #region Read Functions
        public IEnumerable<DbDataRecord> ProcessGroupExceptions_ReadById(Int64 idProcess, String idLanguage)
            {
                return new ProcessGroupExceptions().ReadById(idProcess, idLanguage);
            }
        public IEnumerable<DbDataRecord> ProcessGroupExceptions_ReadAssociatedTask(Int64 idProcess)
            {
                return new ProcessGroupExceptions().ReadAssociatedTask(idProcess);
            }
        public IEnumerable<DbDataRecord> ProcessGroupExceptions_ReadByException(Int64 idException, String idLanguage)
            {
                return new ProcessGroupExceptions().ReadByException(idException, idLanguage);
            }
        #endregion 
        #region Write Function
            //// ADD Exception
        public void ProcessGroupExceptions_Create(Int64 idProcess)
            {
                new ProcessGroupExceptions().Create(idProcess);           
            }
        public void ProcessGroupExceptions_Delete(Int64 idProcess)
        {
            new ProcessGroupExceptions().Delete(idProcess);
        }
        #endregion
        #endregion

        //# region ProcessGroupNodes
        //#region Read Functions
        //public IEnumerable<DbDataRecord> ProcessGroupNodes_ReadById(Int64 idProcess, String idLanguage)
        //    {
        //        return new ProcessGroupNodes().ReadById(idProcess, idLanguage);
        //    }
        //public IEnumerable<DbDataRecord> ProcessGroupNodes_ReadByParent(Int64 IdParentProcess, String idLanguage)
        //{
        //    return new ProcessGroupNodes().ReadByParent(IdParentProcess, idLanguage);        
        //}
  
        //#endregion 
        //#region Write Functions
        //public void ProcessGroupNodes_Create(Int64 idProcess,Int64 idParentProcess)
        //    {
        //        new ProcessGroupNodes().Create(idProcess,idParentProcess);
        //    }
        //public void ProcessGroupNodes_Update(Int64 idProcess, Int64 idParentProcess)
        //    {
        //       new ProcessGroupNodes().Update(idProcess,idParentProcess);
        //    }
        //public void ProcessGroupNodes_Delete(Int64 idProcess)
        //    {
        //        new ProcessGroupNodes().Delete(idProcess);
        //    }
        //    #endregion
        //#endregion

        # region ProcessGroupProcesses

        #region Read Functions
        public IEnumerable<DbDataRecord> ProcessGroupProcessForums_ReadByProcess(Int64 idProcess, String idLanguage)
        {
            return new ProcessGroupProcessForums().ReadByProcess(idProcess, idLanguage);
        }
        public IEnumerable<DbDataRecord> ProcessGroupProcessForums_ReadByForum(Int64 idForum)
        {
            return new ProcessGroupProcessForums().ReadByForum(idForum);
        }
        #endregion

        #region Write Functions
        public void ProcessGroupProcessForums_Create(Int64 idProcess, Int64 idForum)
        {
            new ProcessGroupProcessForums().Create(idProcess, idForum);
        }
        public void ProcessGroupProcessForums_Delete(Int64 idProcess, Int64 idForum)
        {
            new ProcessGroupProcessForums().Delete(idProcess, idForum);
        }
        #endregion

        #endregion

        # region ProcessGroupProcesses

        #region Read Functions
        public IEnumerable<DbDataRecord> ProcessGroupProcesses_ReadById(Int64 idProcess, String idLanguage)
        {
            return new ProcessGroupProcesses().ReadById(idProcess, idLanguage);
        }
        public IEnumerable<DbDataRecord> ProcessGroupProcesses_ReadAll(String idLanguage)
        {
            return new ProcessGroupProcesses().ReadAll(idLanguage);      
        }
        public IEnumerable<DbDataRecord> ProcessGroupProcesses_ReadByPerson(String idLanguage, Int64 idPerson, String className)
        {
            return new ProcessGroupProcesses().ReadByPerson(idLanguage,idPerson,className);      
        }
        public IEnumerable<DbDataRecord> ProcessGroupProcesses_ReadByPersonAndRoleType(Int64 idPerson, Int64 idRoleType)
        {
            return new ProcessGroupProcesses().ReadByPersonAndRoleType(idPerson, idRoleType);      
        }
        public IEnumerable<DbDataRecord> ProcessGroupProcesses_ReadRoot(String idLanguage, String className, Int64 idPerson)
        {
            return new ProcessGroupProcesses().ReadRoot(idLanguage, className, idPerson);
        }
        public IEnumerable<DbDataRecord> ProcessGroupProcesses_ReadByClassification(Int64 idClassification, String idLanguage, String className, Int64 idPerson)
        {
            return new ProcessGroupProcesses().ReadByClassification(idClassification,idLanguage,className,idPerson);
        }
        public IEnumerable<DbDataRecord> ProcessGroupProcesses_ReadByGeographicArea(Int64 IdGeographicArea, String idLanguage, String className, Int64 idPerson)
        {
            return new ProcessGroupProcesses().ReadByGeographicArea(IdGeographicArea, idLanguage, className, idPerson);
        }
        public IEnumerable<DbDataRecord> ProcessGroupProcesses_ReadByGeographicAreaLayerAndProcessType(Int64 IdGeographicAreaParent, Int64 IdProcessClassification, String idLanguage, String className, Int64 idPerson)
        {
            return new ProcessGroupProcesses().ReadByGeographicAreaLayerAndProcessType(IdGeographicAreaParent, IdProcessClassification, idLanguage, className, idPerson);
        }
        public IEnumerable<DbDataRecord> ProcessGroupProcesses_ReadByClassificationAndGeographicArea(Int64 idClassification, Int64 IdGeographicArea, String idLanguage, String className, Int64 idPerson)
        {
            return new ProcessGroupProcesses().ReadByClassificationAndGeographicArea(idClassification, IdGeographicArea, idLanguage, className, idPerson);
        }
        public IEnumerable<DbDataRecord> ProcessGroupProcesses_ReadByOrganization(Int64 IdOrganization, String idLanguage, String className, Int64 idPerson)
        {
            return new ProcessGroupProcesses().ReadByOrganization(IdOrganization, idLanguage, className, idPerson);
        }
        public IEnumerable<DbDataRecord> ProcessGroupProcesses_ReadProcessParticipationsByProcess(Int64 idProcess, String idLanguage, String className, Int64 idPerson)
        {
            return new ProcessGroupProcesses().ReadProcessParticipationsByProcess(idProcess, idLanguage, className, idPerson);
        }
        public IEnumerable<DbDataRecord> ProcessGroupProcesses_ReadByClassificationAndOrganization(Int64 idClassification, Int64 IdOrganization, String idLanguage, String className, Int64 idPerson)
        {
            return new ProcessGroupProcesses().ReadByClassificationAndOrganization(idClassification, IdOrganization, idLanguage, className, idPerson);
        }
        public IEnumerable<DbDataRecord> ProcessGroupProcesses_ClassificationsByProject(Int64 idProcess, String idLanguage)
        {
            return new ProcessGroupProcesses().ClassificationsByProject(idProcess, idLanguage);
        }
        public IEnumerable<DbDataRecord> ProcessGroupProcesses_ReadByCalculations(Int64 idCalculation)
        {
            return new ProcessGroupProcesses().ReadByCalculations(idCalculation);
        }
        public IEnumerable<DbDataRecord> ProcessGroupProcesses_ReadByTitle(String Title, String className, Int64 idPerson)
        {
            return new ProcessGroupProcesses().ReadByTitle(Title, className, idPerson);
        }
        public IEnumerable<DbDataRecord> ProcessGroupProcesses_ReadByFacility(String idLanguage, Int64 idPerson, Int64 IdFacility)
        {
            return new ProcessGroupProcesses().ReadByFacility(idLanguage,idPerson, IdFacility);
        }
        
        #endregion 

        #region Write ProcessGroupProcesses
        public void ProcessGroupProcesses_Create(Int64 idProcess,String identification, DateTime currentCampaignStartDate,
                Int64 idResourcePicture, String Coordinate, Int64 idGeographicArea, Int64 idOrganization, String TwitterUser, String FacebookUser)
                {
                    new ProcessGroupProcesses().Create( idProcess,  identification,  currentCampaignStartDate,  idResourcePicture,
                 Coordinate, idGeographicArea, idOrganization, TwitterUser, FacebookUser);
                }      
        public void ProcessGroupProcesses_Update(Int64 idProcess, String identification, DateTime currentCampaignStartDate,
            Int64 idResourcePicture, String Coordinate, Int64 idGeographicArea, Int64 idOrganization, String TwitterUser, String FacebookUser)
                {
                    new ProcessGroupProcesses().Update( idProcess,  identification,  currentCampaignStartDate,  idResourcePicture,
                        Coordinate, idGeographicArea, idOrganization, TwitterUser, FacebookUser);
                }
        public void ProcessGroupProcesses_Delete(Int64 idProcess)
        {
            new ProcessGroupProcesses().Delete(idProcess);
        }
        #endregion  

        #region Projects related with Calculation
        public void ProcessGroupProcesses_DeleteRelationshipCalculation(Int64 idProcess)
        {
            new ProcessGroupProcesses().DeleteRelationshipCalculation(idProcess);
        }
        #endregion

        #region Projects related with ProjectClassification
        public void ProcessGroupProcesses_Create(Int64 idProcess, Int64 idProcessClassification)
            {
                new ProcessGroupProcesses().Create(idProcess, idProcessClassification);
            }
        public void ProcessGroupProcesses_Delete(Int64 idProcess, Int64 idProcessClassification)
            {
                new ProcessGroupProcesses().Delete(idProcess, idProcessClassification);
            }
        //public void ProcessGroupProcesses_Delete(Int64 idProcess)
        //    {
        //        new ProcessGroupProcesses().Delete(idProcess);
        //    }
        #endregion

        #endregion

        # region ProcessGroups
        #region Read Function
        public IEnumerable<DbDataRecord> ProcessGroups_ReadProjectType(Int64 idProcess)
            {
                return new ProcessGroups().ReadProjectType(idProcess);
            }
        #region Write Function
        public void ProcessGroups_Create(Int64 idProcess, Int16 threshold)
        {
            new ProcessGroups().Create(idProcess, threshold);
        }
        public void ProcessGroups_Update(Int64 idProcess, Int16 threshold)
        {
            new ProcessGroups().Update(idProcess, threshold);
        }
        public void ProcessGroups_Delete(Int64 idProcess)
        {
            new ProcessGroups().Delete(idProcess);
        }
        #endregion
         #endregion
         #endregion

        # region ProcessParticipations
        #region Read Functions
        //Trae todos los types
        public IEnumerable<DbDataRecord> ProcessParticipations_ReadByProcess(Int64 idProcess)
        {
            return new ProcessParticipations().ReadByProcess(idProcess);
        }
        public IEnumerable<DbDataRecord> ProcessParticipations_ReadByOrganization(Int64 idOrganization)
        {
            return new ProcessParticipations().ReadByOrganization(idOrganization);
        }
        public IEnumerable<DbDataRecord> ProcessParticipations_ReadById(Int64 idProcess, Int64 idOrganization, Int64 IdParticipationType)
        {
            return new ProcessParticipations().ReadById(idProcess, idOrganization, IdParticipationType);
        }
        #endregion

        #region Write Functions
        public void ProcessParticipations_Create(Int64 idProcess, Int64 idOrganization, Int64 IdParticipationType, String Comment, Int64 idLogPerson)
        {
            new ProcessParticipations().Create(idProcess, idOrganization, IdParticipationType, Comment, idLogPerson);
        }
        public void ProcessParticipations_Delete(Int64 idProcess, Int64 idOrganization, Int64 idParticipationType, Int64 idLogPerson)
        {
            new ProcessParticipations().Delete(idProcess, idOrganization, idParticipationType, idLogPerson);
        }
        //public void ProcessParticipations_Delete(Int64 idProcess, Int64 idLogPerson)
        //{
        //    new ProcessParticipations().Delete(idProcess, idLogPerson);
        //}
        public void ProcessParticipations_DeleteByOrganization(Int64 idOrganization)
        {
            new ProcessParticipations().DeleteByOrganization(idOrganization);
        }
        public void ProcessParticipations_DeleteByProcess(Int64 idProcess)
        {
            new ProcessParticipations().DeleteByProcess(idProcess);
        }
        public void ProcessParticipations_Update(Int64 idProcess, Int64 idOrganization, Int64 IdParticipationType, String Comment, Int64 idLogPerson)
        {
            new ProcessParticipations().Update(idProcess, idOrganization, IdParticipationType, Comment, idLogPerson);
        }
        #endregion
        #endregion

        # region ProcessPosts
        #region Read Function
        public IEnumerable<DbDataRecord> ProcessPosts_ReadAll(Int64 idProcess, Int64 idPerson)
            {
                return new ProcessPosts().ReadAll(idProcess, idPerson);
            }
        public IEnumerable<DbDataRecord> ProcessPosts_ReadById(Int64 idProcess, Int64 idPerson, Int64 idPosition, Int64 idFunctionalArea, Int64 idGeographicArea)
            {
                return new ProcessPosts().ReadById(idProcess, idPerson, idPosition, idFunctionalArea, idGeographicArea);
            }
        #endregion

        #region Write Process Task Execution File Attachs
        public void ProcessPosts_Create(Int64 idProcess, Int64 idPerson, Int64 idPosition, Int64 idFunctionalArea, Int64 idGeographicArea, DateTime startDate, DateTime endDate, Int64 idPermission, Int64 idLogPerson)
            {
                new ProcessPosts().Create(idProcess, idPerson, idPosition, idFunctionalArea, idGeographicArea, startDate, endDate, idPermission, idLogPerson);
            }
        public void ProcessPosts_Delete(Int64 idProcess, Int64 idPerson, Int64 idPosition, Int64 idFunctionalArea, Int64 idGeographicArea, Int64 idLogPerson)
            {
                new ProcessPosts().Delete(idProcess, idProcess, idPosition, idFunctionalArea, idGeographicArea, idLogPerson);
            }
        public void ProcessPosts_Update(Int64 idProcess, Int64 idPerson, Int64 idPosition, Int64 idFunctionalArea, Int64 idGeographicArea, DateTime startDate, DateTime endDate, Int64 idPermission, Int64 idLogPerson)
            {
                new ProcessPosts().Update(idProcess, idPerson, idPosition, idFunctionalArea, idGeographicArea, startDate, endDate, idPermission, idLogPerson);
            }

        #endregion
        #endregion  

        # region ProcessResources
        #region Read Function
            public IEnumerable<DbDataRecord> ProcessResources_ReadAll(Int64 idProcess)
        {
            return new ProcessResources().ReadAll(idProcess);
        }
            public IEnumerable<DbDataRecord> ProcessResources_ReadById(Int64 idProcess, Int64 idResource)
        {
            return new ProcessResources().ReadById(idProcess, idResource);
        }
        #endregion
        #region Write Functions
            public void ProcessResources_Create(Int64 idResource, Int64 idProcess, String comment, Int64 idLogPerson)
        {
            new ProcessResources().Create(idResource, idProcess, comment, idLogPerson);
        }
            public void ProcessResources_Delete(Int64 idResource, Int64 idProcess, Int64 idLogPerson)
            {
                new ProcessResources().Delete(idResource, idProcess, idLogPerson);
            }
            public void ProcessResources_Delete(Int64 idProcess)
            {
                new ProcessResources().Delete(idProcess);
            }
            public void ProcessResources_Update(Int64 idResource, Int64 idProcess, String comment, Int64 idLogPerson)
        {
            new ProcessResources().Update(idResource, idProcess, comment, idLogPerson);
        }
        #endregion
        #endregion

        # region ProcessTaskExecutionFileAttachs
        #region Read Function
            public IEnumerable<DbDataRecord> ProcessTaskExecutionFileAttachs_ReadAll(Int64 idProcess, Int64 idExecution)
            {
                return new ProcessTaskExecutionFileAttachs().ReadAll(idProcess, idExecution);
            }
            public IEnumerable<DbDataRecord> ProcessTaskExecutionFileAttachs_ReadById(Int64 idProcess, Int64 idExecution, Int64 idFile)
            {
                return new ProcessTaskExecutionFileAttachs().ReadById(idProcess, idExecution, idFile);
            }
        #endregion

        #region Write Process Task Execution File Attachs
            public Int64 ProcessTaskExecutionFileAttachs_CreateExecution(Int64 idProcess, Int64 idExecution, String fileName, String fileStream, Int64 idLogPerson)
            {
                return new ProcessTaskExecutionFileAttachs().CreateExecution(idProcess, idExecution, fileName, fileStream, idLogPerson);
            }
            public void ProcessTaskExecutionFileAttachs_CreateExecution(ref Int64 idExecution, ref Int64 idFile, Int64 idProcess, Int64 idPerson, Int64 idPosition, Int64 idFunctionalArea, Int64 idGeographicArea, DateTime date, String comment, String fileName, String fileStream, Int64 idLogPerson)
            {
                new ProcessTaskExecutionFileAttachs().CreateExecution(ref idExecution,ref idFile, idProcess, idPerson, idPosition, idFunctionalArea, idGeographicArea, date, comment, fileName, fileStream, idLogPerson);
            }
            public void ProcessTaskExecutionFileAttachs_DeleteExecution(Int64 idProcess, Int64 idExecution, Int64 idFile, Int64 idLogPerson)
            {
                new ProcessTaskExecutionFileAttachs().DeleteExecution(idProcess, idExecution, idFile, idLogPerson);
            }
        #endregion
            #endregion

        # region ProcessTaskExecutions
        #region Common Read Function
            public IEnumerable<DbDataRecord> ProcessTaskExecutions_ReadAll(Int64 idProcess)
            {
                return new ProcessTaskExecutions().ReadAll(idProcess);
            }
            public IEnumerable<DbDataRecord> ProcessTaskExecutions_ReadAllOnlyExecution(Int64 idProcess)
            {
                return new ProcessTaskExecutions().ReadAllOnlyExecution(idProcess);
            }
            public IEnumerable<DbDataRecord> ProcessTaskExecutions_ReadById(Int64 idProcess, Int64 idExecution)
            {
                return new ProcessTaskExecutions().ReadById(idProcess, idExecution);
            }
            public IEnumerable<DbDataRecord> ProcessTaskExecutions_ReadFirstExecution(Int64 idProcess)
            {
                return new ProcessTaskExecutions().ReadFirstExecution(idProcess);
            }
            public IEnumerable<DbDataRecord> ProcessTaskExecutions_ReadLastExecution(Int64 idProcess)
            {
                return new ProcessTaskExecutions().ReadLastExecution(idProcess);
            }
            public IEnumerable<DbDataRecord> ProcessTaskExecutions_ReadLastExecutionMeasurement(Int64 idProcess)
            {
                return new ProcessTaskExecutions().ReadLastExecutionMeasurement(idProcess);
            }
            public IEnumerable<DbDataRecord> ProcessTaskExecutions_ReadNowExecution(Int64 idProcess, Int64 timeUnit, Int64 duration)
            {
                return new ProcessTaskExecutions().ReadNowExecution(idProcess, timeUnit, duration);
            }
            public IEnumerable<DbDataRecord> ProcessTaskExecutions_ReadNextExecution(Int64 idProcess, DateTime startDate, DateTime endDate)
            {
                return new ProcessTaskExecutions().ReadNextExecution(idProcess, startDate, endDate);
            }
            public IEnumerable<DbDataRecord> ProcessTaskExecutions_ReadExecutionNextNotifice(Int64 idProcess)
            {
                return new ProcessTaskExecutions().ReadExecutionNextNotifice(idProcess);
            }
            public IEnumerable<DbDataRecord> ProcessTaskExecutions_ReadByPlanned(Int64 idPerson)
            {
                return new ProcessTaskExecutions().ReadByPlanned(idPerson);
            }
            public IEnumerable<DbDataRecord> ProcessTaskExecutions_ReadByPlanned(DateTime plannedDate, Int64 idPerson)
            {
                return new ProcessTaskExecutions().ReadByPlanned(plannedDate, idPerson);
            }
            public IEnumerable<DbDataRecord> ProcessTaskExecutions_ReadByWorking(Int64 idPerson)
            {
                return new ProcessTaskExecutions().ReadByWorking(idPerson);
            }
            public IEnumerable<DbDataRecord> ProcessTaskExecutions_ReadByFinished(Int64 idPerson)
            {
                return new ProcessTaskExecutions().ReadByFinished(idPerson);
            }
            public IEnumerable<DbDataRecord> ProcessTaskExecutions_ReadByOverdue(Int64 idPerson)
            {
                return new ProcessTaskExecutions().ReadByOverdue(idPerson);
            }
          
        #endregion

        #region Process Task Execution Functions
            public void ProcessTaskExecutionsAll_Create(Int64 idProcess, DateTime starDate, DateTime endDate, Int32 interval, Int64 timeUnitInterval, String typeExecution, Boolean result)
            {
                new Entities.ProcessTaskExecutions().Create(idProcess, starDate, endDate, interval, timeUnitInterval, typeExecution, result);
            }
            public void ProcessTaskExecutionsAll_Delete(Int64 idProcess)
            {
                new Entities.ProcessTaskExecutions().Delete(idProcess);
            }
            public void ProcessTaskExecutionsAll_Update(Int64 idProcess, DateTime starDate, DateTime endDate, Int32 interval, Int64 timeUnitInterval, String typeExecution)
            {
                new Entities.ProcessTaskExecutions().Update(idProcess, starDate, endDate, interval, timeUnitInterval, typeExecution);
            }
            public void ProcessTaskExecutions_Update(Int64 idProcess, Int64 IdExecution, Boolean AdvanceNotify)
            {
                new Entities.ProcessTaskExecutions().Update(idProcess, IdExecution, AdvanceNotify);
            }
            #endregion

        #region Write Process Task Execution Calibration
            public Int64 ProcessTaskExecutions_CreateExecution(Int64 idProcess, Int64 idOrganization, Int64 idPerson, Int64 idPosition, Int64 idFunctionalArea, Int64 idGeographicArea, DateTime date, String comment, Byte[] attachment, DateTime validationStart, DateTime validationEnd, Int64 idMeasurementDevice, Int64 idLogPerson)
            {
                return new ProcessTaskExecutions().CreateExecution(idProcess, idOrganization,  idPerson,  idPosition,  idFunctionalArea,  idGeographicArea,  date,  comment, attachment,  validationStart,  validationEnd,  idMeasurementDevice,  idLogPerson);
            }
            public void ProcessTaskExecutions_CreateExecution(Int64 idProcess, Int64 idExecution, Int64 idOrganization, Int64 idPerson, Int64 idPosition, Int64 idFunctionalArea, Int64 idGeographicArea, String comment, Byte[] attachment, DateTime validationStart, DateTime validationEnd, Int64 idMeasurementDevice, Int64 idLogPerson)
            {
                 new ProcessTaskExecutions().CreateExecution(idProcess,  idExecution, idOrganization, idPerson,  idPosition,  idFunctionalArea,  idGeographicArea,  comment, attachment, validationStart,  validationEnd,  idMeasurementDevice,  idLogPerson);
            }
            public Int64 ProcessTaskExecutions_CreateExecution(Int64 idProcess, Int64 idExecution, Int64 idOrganization, Int64 idPerson, Int64 idPosition, Int64 idFunctionalArea, Int64 idGeographicArea, String comment, Byte[] attachment, String fileName, Byte[] fileStream, DateTime validationStart, DateTime validationEnd, Int64 idMeasurementDevice, Int64 idLogPerson)
            {
                return new ProcessTaskExecutions().CreateExecution( idProcess,  idExecution, idOrganization,  idPerson,  idPosition,  idFunctionalArea,  idGeographicArea,  comment, attachment, fileName,  fileStream,  validationStart,  validationEnd,  idMeasurementDevice,  idLogPerson);
            }
            public void ProcessTaskExecutions_CreateExecution(ref Int64 idExecution, ref Int64 idFile, Int64 idProcess, Int64 idOrganization, Int64 idPerson, Int64 idPosition, Int64 idFunctionalArea, Int64 idGeographicArea, DateTime date, String comment, Byte[] attachment, String fileName, Byte[] fileStream, DateTime validationStart, DateTime validationEnd, Int64 idMeasurementDevice, Int64 idLogPerson)
            {
                 new ProcessTaskExecutions().CreateExecution(ref  idExecution, ref  idFile,  idProcess, idOrganization,  idPerson,  idPosition,  idFunctionalArea,  idGeographicArea,  date,  comment, attachment, fileName,  fileStream,  validationStart,  validationEnd,  idMeasurementDevice,  idLogPerson);
            }
            public void ProcessTaskExecutions_DeleteExecutionCalibration(Int64 idProcess, Int64 idExecution)
            {
                 new ProcessTaskExecutions().DeleteExecutionCalibration( idProcess,  idExecution);
            }
        #endregion

        #region Write Process Task Execution Measurement
            public Int64 ProcessTaskExecutions_CreateExecutionTVP(Int64 idProcess, Int64 idExecution, Int64 idOrganization, Int64 idPerson, Int64 idPosition,
                   Int64 idFunctionalArea, Int64 idGeographicArea, String comment, Byte[] attachment,
                   DataTable dtTVPMeasurements, Int64 idMeasurementDevice, Int64 idMeasurementUnit, Int64 idLogPerson)
            {
                return new ProcessTaskExecutions().CreateExecutionTVP(idProcess, idExecution, idOrganization, idPerson, idPosition,
                   idFunctionalArea, idGeographicArea, comment, attachment, dtTVPMeasurements, idMeasurementDevice, idMeasurementUnit, idLogPerson);
            }
            public void ProcessTaskExecutions_CreateExecutionTVPForCalculate(Int64 idProcess, DataTable dtTVPMeasurements, Int64 idMeasurementDevice, Int64 idMeasurementUnit, Boolean isCumulative)
            {
                new ProcessTaskExecutions().CreateExecutionTVPForCalculate(idProcess, dtTVPMeasurements, idMeasurementDevice, idMeasurementUnit, isCumulative);
            }
            public Int64 ProcessTaskExecutions_CreateExecution(Int64 idProcess, Int64 idOrganization, Int64 idPerson, Int64 idPosition, Int64 idFunctionalArea, Int64 idGeographicArea, DateTime date, String comment, Byte[] attachment, Double measureValue, DateTime measureDate, ref DateTime timeStamp, Int64 idMeasurementDevice, Int64 idMeasurementUnit, DateTime startDate, DateTime endDate, Int64 idLogPerson)
            {
                return new ProcessTaskExecutions().CreateExecution( idProcess, idOrganization,  idPerson,  idPosition,  idFunctionalArea,  idGeographicArea,  date,  comment, attachment,  measureValue,  measureDate, ref  timeStamp,  idMeasurementDevice, idMeasurementUnit, startDate, endDate, idLogPerson);
            }
            public Int64 ProcessTaskExecutions_CreateExecution(Int64 idProcess, Int64 idExecution, Int64 idOrganization, Int64 idPerson, Int64 idPosition, Int64 idFunctionalArea, Int64 idGeographicArea, String comment, Byte[] attachment, Double measureValue, DateTime measureDate, ref DateTime timeStamp, Int64 idMeasurementDevice, Int64 idMeasurementUnit, DateTime startDate, DateTime endDate, Int64 idLogPerson)
            {
                return new ProcessTaskExecutions().CreateExecution( idProcess,  idExecution, idOrganization,  idPerson,  idPosition,  idFunctionalArea,  idGeographicArea,  comment, attachment, measureValue,  measureDate, ref  timeStamp,  idMeasurementDevice,  idMeasurementUnit,  startDate,  endDate,  idLogPerson);
            }
            public void ProcessTaskExecutions_CreateExecutionForCalculate(Int64 idProcess, Int64 idExecution, Double measureValue, DateTime measureDate, Int64 idMeasurementUnit, Int64 idMeasurementDevice, DateTime startDate, DateTime endDate, Double minuteValue)
            {
                new ProcessTaskExecutions().CreateExecutionForCalculate(idProcess, idExecution, measureValue, measureDate, idMeasurementUnit, idMeasurementDevice, startDate, endDate, minuteValue);
            }
            public void ProcessTaskExecutions_UpdateExecutionForCalculate(Int64 idProcess, Double measureValue, DateTime measureDate)
            {
                new ProcessTaskExecutions().UpdateExecutionForCalculate(idProcess, measureValue, measureDate);
            }
            public void ProcessTaskExecutions_CreateExecutionForCalculateCumulative(Int64 idProcess, DataTable dtTVPMeasurements, Int64 idMeasurementDevice, Int64 idMeasurementUnit)
            {
                new ProcessTaskExecutions().CreateExecutionForCalculateCumulative(idProcess, dtTVPMeasurements, idMeasurementDevice, idMeasurementUnit);
            }
            public void ProcessTaskExecutions_CreateExecutionForCalculateNotCumulative(Int64 idProcess, DataTable dtTVPMeasurements, Int64 idMeasurementDevice, Int64 idMeasurementUnit)
            {
                new ProcessTaskExecutions().CreateExecutionForCalculateNotCumulative(idProcess, dtTVPMeasurements, idMeasurementDevice, idMeasurementUnit);
            }
            public void ProcessTaskExecutions_DeleteExecutionForCalculate(Int64 idProcess)
            {
                new ProcessTaskExecutions().DeleteExecutionForCalculate(idProcess);
            }
            public void ProcessTaskExecutions_UpdateMeasurementExecution(DataTable dtTVPMeasurements)
            {
                new ProcessTaskExecutions().UpdateMeasurementExecution(dtTVPMeasurements);
            }
            public void ProcessTaskExecutions_UpdateMeasurementExecutionForCalculate(DataTable dtTVPMeasurements)
            {
                new ProcessTaskExecutions().UpdateMeasurementExecutionForCalculate(dtTVPMeasurements);
            }
            //public void ProcessTaskExecutions_CreateExecution(ref Int64 idExecution, ref Int64 idFile, Int64 idProcess, Int64 idOrganization, Int64 idPerson, Int64 idPosition, Int64 idFunctionalArea, Int64 idGeographicArea, DateTime date, String comment, String fileName, String fileStream, Byte[] fileStreamBinary, Int64 idMeasurementDevice, Int64 idMeasurementUnit, Int64 idLogPerson)
            //{
            //    new ProcessTaskExecutions().CreateExecution(idExecution, idFile, idProcess, idOrganization, idPerson, idPosition, idFunctionalArea, idGeographicArea, date, comment, fileName, fileStream, fileStreamBinary, idMeasurementDevice, idMeasurementUnit, idLogPerson);
            //}
            //public void ProcessTaskExecutions_CreateExecutionForCalculate(Int64 idProcess, String fileName, String fileStream, Int64 idMeasurementDevice, Int64 idMeasurementUnit, DateTime startDate, DateTime endDate)
            //{
            //    new ProcessTaskExecutions().CreateExecutionForCalculate(idProcess, fileName, fileStream, idMeasurementDevice, idMeasurementUnit, startDate, endDate);
            //}
            //public void ProcessTaskExecutions_UpdateExecutionForCalculate(Int64 idProcess, String fileName, String fileStream)
            //{
            //    new ProcessTaskExecutions().UpdateExecutionForCalculate(idProcess, fileName, fileStream);
            //}
            public void ProcessTaskExecutions_UpdateExecutionForCalculate(Int64 idProcess, DataTable dtTVPMeasurements)
            {
                new ProcessTaskExecutions().UpdateExecutionForCalculate(idProcess, dtTVPMeasurements);
            }
            public Int64 ProcessTaskExecutions_CreateExecution(Int64 idProcess, Int64 idExecution, Int64 idOrganization, Int64 idPerson, Int64 idPosition, Int64 idFunctionalArea, Int64 idGeographicArea, String comment, Byte[] attachment, DataTable dtTVPMeasurements, Int64 idMeasurementDevice, Int64 idMeasurementUnit, Int64 idLogPerson)
            {
                return new ProcessTaskExecutions().CreateExecution(idProcess, idExecution, idOrganization, idPerson, idPosition, idFunctionalArea, idGeographicArea, comment, attachment, dtTVPMeasurements, idMeasurementDevice, idMeasurementUnit, idLogPerson);
            }
            //public Int64 ProcessTaskExecutions_CreateExecution(Int64 idProcess, Int64 idExecution, Int64 idOrganization, Int64 idPerson, Int64 idPosition, Int64 idFunctionalArea, Int64 idGeographicArea, String comment, String fileName, String fileStream, Byte[] fileStreamBinary, ref Decimal measureValue, ref DateTime measureDate, ref DateTime timeStamp, Int64 idMeasurementDevice, Int64 idMeasurementUnit, DateTime startDate, DateTime endDate, Int64 idLogPerson)
            //{
            //    return new ProcessTaskExecutions().CreateExecution(idProcess, idExecution, idOrganization, idPerson, idPosition, idFunctionalArea, idGeographicArea, comment, fileName, fileStream, fileStreamBinary, ref  measureValue, ref  measureDate, ref  timeStamp, idMeasurementDevice, idMeasurementUnit, startDate, endDate, idLogPerson);
            //}
            //public void ProcessTaskExecutions_CreateExecution(ref Int64 idExecution, Int64 idProcess, Int64 idOrganization, Int64 idPerson, Int64 idPosition, Int64 idFunctionalArea, Int64 idGeographicArea, DateTime date, String comment, DataTable dtTVPMeasurements, Int64 idMeasurementDevice, Int64 idMeasurementUnit, Int64 idLogPerson)
            //{
            //    new ProcessTaskExecutions().CreateExecution(ref  idExecution, idProcess, idOrganization, idPerson, idPosition, idFunctionalArea, idGeographicArea, date, comment, dtTVPMeasurements, idMeasurementDevice, idMeasurementUnit, idLogPerson);
            //}
            //public void ProcessTaskExecutions_CreateExecution(ref Int64 idExecution, ref Int64 idFile, Int64 idProcess, Int64 idOrganization, Int64 idPerson, Int64 idPosition, Int64 idFunctionalArea, Int64 idGeographicArea, DateTime date, String comment, String fileName, String fileStream, Byte[] fileStreamBinary, Decimal measureValue, DateTime measureDate, ref DateTime timeStamp, Int64 idMeasurementDevice, Int64 idMeasurementUnit, DateTime startDate, DateTime endDate, Int64 idLogPerson)
            //{
            //    new ProcessTaskExecutions().CreateExecution(ref  idExecution, ref  idFile, idProcess, idOrganization, idPerson, idPosition, idFunctionalArea, idGeographicArea, date, comment, fileName, fileStream, fileStreamBinary, measureValue, measureDate, ref  timeStamp, idMeasurementDevice, idMeasurementUnit, startDate, endDate, idLogPerson);
            //}
            public void ProcessTaskExecutions_DeleteExecutionMeasurement(Int64 idProcess, Int64 idExecution)
            {
                 new ProcessTaskExecutions().DeleteExecutionMeasurement( idProcess,  idExecution);
            }
            public void ProcessTaskExecutions_ResetExecution(Int64 idProcess, Int64 idExecution)
            {
                new ProcessTaskExecutions().ResetExecution(idProcess, idExecution);
            }
        #endregion

        #region File Attach in Process Task Execution
            public Int64 ProcessTaskExecutions_CreateExecutionFileAttach(Int64 idProcess, Int64 idExecution, String fileName, Byte[] fileStreamBinary, Int64 idLogPerson)
            {
                return new ProcessTaskExecutions().CreateExecutionFileAttach(idProcess, idExecution, fileName, fileStreamBinary, idLogPerson);
            }
            public void ProcessTaskExecutions_DeleteExecutionFileAttach(Int64 idProcess, Int64 idExecution)
            {
                new ProcessTaskExecutions().DeleteExecutionFileAttach(idProcess, idExecution);
            }
        #endregion

        #region Write Process Task Execution General
            public Int64 ProcessTaskExecutions_CreateExecution(Int64 idProcess, Int64 idOrganization, Int64 idPerson, Int64 idPosition, Int64 idFunctionalArea, Int64 idGeographicArea, DateTime date, String comment, Boolean result, Byte[] attachment, Int64 idLogPerson)
            {
                return new ProcessTaskExecutions().CreateExecution( idProcess, idOrganization,  idPerson,  idPosition,  idFunctionalArea,  idGeographicArea,  date,  comment,  result,   idLogPerson, attachment);
            }
            public void ProcessTaskExecutions_DeleteExecution(Int64 idProcess, Int64 idExecution)
            {
                new ProcessTaskExecutions().DeleteExecution(idProcess, idExecution);
            }
            public void ProcessTaskExecutions_UpdateExecution(Int64 idProcess, Int64 idExecution, Int64 idOrganization, Int64 idPerson, Int64 idPosition, Int64 idFunctionalArea, Int64 idGeographicArea, String comment, Boolean result, Byte[] attachment, Int64 idLogPerson)
            {
                 new ProcessTaskExecutions().UpdateExecution( idProcess,  idExecution, idOrganization,  idPerson,  idPosition,  idFunctionalArea,  idGeographicArea,  comment,  result,  idLogPerson, attachment);
            }
        #endregion

        #endregion

        # region ProcessTaskPermissions
        #region Read Functions
            public IEnumerable<DbDataRecord> ProcessTaskPermissions_ReadById(Int64 idProcess, Int64 IdOrganizationPost, Int64 IdGeographicArea, Int64 IdFunctionalArea, Int64 IdPosition, Int64 IdPerson)
        {
                return new ProcessTaskPermissions().ReadById( idProcess,  IdOrganizationPost,  IdGeographicArea,  IdFunctionalArea,  IdPosition,  IdPerson);
        }
            public IEnumerable<DbDataRecord> ProcessTaskPermissions_ReadByProcess(Int64 idProcess)
        {
            return new ProcessTaskPermissions().ReadByProcess(idProcess);
        }
            public IEnumerable<DbDataRecord> ProcessTaskPermissions_ReadByPost(Int64 IdOrganizationPost, Int64 IdGeographicArea, Int64 IdFunctionalArea, Int64 IdPosition, Int64 IdPerson)
            {
                return new ProcessTaskPermissions().ReadByPost(IdOrganizationPost, IdGeographicArea, IdFunctionalArea, IdPosition, IdPerson);
            }
        #endregion

        #region Write Functions
            public void ProcessTaskPermissions_Create(Int64 idProcess, Int64 IdOrganizationPost, Int64 IdGeographicArea, Int64 IdFunctionalArea, Int64 IdPosition, Int64 IdPerson)
        {
            new ProcessTaskPermissions().Create(idProcess,  IdOrganizationPost,  IdGeographicArea,  IdFunctionalArea,  IdPosition,  IdPerson);
        }
            public void ProcessTaskPermissions_Delete(Int64 idProcess)
        {
            new ProcessTaskPermissions().Delete(idProcess);
        }
            public void ProcessTaskPermissions_Delete(Int64 IdProcess, Int64 IdOrganizationPost, Int64 IdGeographicArea, Int64 IdFunctionalArea, Int64 IdPosition, Int64 IdPerson)
        {
            new ProcessTaskPermissions().Delete(IdProcess, IdOrganizationPost, IdGeographicArea, IdFunctionalArea, IdPosition, IdPerson);
        }
        public void ProcessTaskPermissions_Delete(Int64 IdOrganization, Int64 IdGeographicArea, Int64 IdFunctionalArea, Int64 IdPosition, Int64 IdPerson)
        {
            new ProcessTaskPermissions().Delete(IdOrganization, IdGeographicArea, IdFunctionalArea, IdPosition, IdPerson);
        }
        #endregion
            #endregion

        # region ProcessTasks
        #region Common Read Function
            public IEnumerable<DbDataRecord> ProcessTasks_ReadByMeasurement(Int64 idMeasurement, String idLanguage)
            {
                return new ProcessTasks().ReadByMeasurement(idMeasurement, idLanguage);
            }
            public IEnumerable<DbDataRecord> ProcessTasks_ReadTaskOperation(Int64 idProcess, String idLanguage)
            {
                return new ProcessTasks().ReadTaskOperation(idProcess, idLanguage);
            }
            public IEnumerable<DbDataRecord> ProcessTasks_ReadTaskCalibration(Int64 idProcess, String idLanguage)
            {
                return new ProcessTasks().ReadTaskCalibration(idProcess, idLanguage);
            }
            public IEnumerable<DbDataRecord> ProcessTasks_ReadByMeasurementDevice(Int64 idMeasurementDevice, String idLanguage)
            {
                return new ProcessTasks().ReadByMeasurementDevice(idMeasurementDevice, idLanguage);
            }
            public IEnumerable<DbDataRecord> ProcessTasks_ReadByParent(Int64 idParentProcess, String idLanguage)
            {
                return new ProcessTasks().ReadByParent(idParentProcess, idLanguage);
            }
            public IEnumerable<DbDataRecord> ProcessTasks_ReadByParentAdvanceNotice(Int64 idParentProcess, String idLanguage)
            {
                return new ProcessTasks().ReadByParentAdvanceNotice(idParentProcess, idLanguage);
            }
            public IEnumerable<DbDataRecord> ProcessTasks_ReadById(Int64 idProcess, String idLanguage)
            {
                return new ProcessTasks().ReadById(idProcess, idLanguage);
            }
            public IEnumerable<DbDataRecord> ProcessTasks_ReadByException(Int64 idException)
            {
                return new ProcessTasks().ReadByException(idException);
            }        
        #endregion

        #region Write Functions
            public void ProcessTasks_Create(Int64 idProcess, Int64 idParentProcess, DateTime startDate, DateTime endDate, Int32 duration, Int32 interval,
                Int64 maxNumberExecutions, Boolean result, Int32 completed, Int64 timeUnitDuration, Int64 timeUnitInterval, String typeExecution,
                Int64 idFacility, Int64 idTaskInstruction, Int64 timeUnitAdvanceNotice, Int16 advanceNotice)
            {
                new Entities.ProcessTasks().Create(idProcess, idParentProcess, startDate, endDate,duration,interval, maxNumberExecutions, result, completed, timeUnitDuration, timeUnitInterval, typeExecution, idFacility, idTaskInstruction, timeUnitAdvanceNotice, advanceNotice);
            }
            public void ProcessTasks_Update(Int64 idProcess, Int64 idParentProcess, DateTime startDate, DateTime endDate, Int32 duration, Int32 interval,
                Int64 maxNumberExecutions, Boolean result, Int32 completed, Int64 timeUnitDuration, Int64 timeUnitInterval, String typeExecution,
                Int64 idFacility, Int64 idTaskInstruction, Int64 timeUnitAdvanceNotice, Int16 advanceNotice)
            {
                new Entities.ProcessTasks().Update(idProcess, idParentProcess, startDate, endDate, duration, interval, maxNumberExecutions, result, completed, timeUnitDuration, timeUnitInterval, typeExecution, idFacility, idTaskInstruction, timeUnitAdvanceNotice, advanceNotice);
            }
            public void ProcessTasks_Delete(Int64 idProcess)
            {
                new ProcessTasks().Delete(idProcess);
            }
            #endregion

        #region ProcessTaskOperation
        public void ProcessTaskOperation_Create(Int64 idProcess, String comment)
            {
                new ProcessTaskOperations().Create(idProcess, comment);
            }
        public void ProcessTaskOperation_Delete(Int64 idProcess)
            {
                new ProcessTaskOperations().Delete(idProcess);
            }
        public void ProcessTaskOperation_Update(Int64 idProcess, String comment)
            {
                new ProcessTaskOperations().Update(idProcess, comment);
            }
        #endregion

        #region ProcessTaskCalibrations
        public void ProcessTaskCalibrations_Create(Int64 idProcess, Int64 idMeasurementDevice)
            {
                new ProcessTaskCalibrations().Create(idProcess, idMeasurementDevice);
            }
        public void ProcessTaskCalibrations_Delete(Int64 idProcess)
            {
                new ProcessTaskCalibrations().Delete(idProcess);
            }
        public void ProcessTaskCalibrations_Update(Int64 idProcess, Int64 idMeasurementDevice)
            {
                new ProcessTaskCalibrations().Update(idProcess, idMeasurementDevice);
            }
        #endregion

        #region ProcessTaskMeasurements
        public IEnumerable<DbDataRecord> ProcessTaskMeasurements_ReadByFacility(Int64 IdProcess, Int64 IdFacility, String idLanguage)
        {
            return new ProcessTaskMeasurements().ReadByFacility(IdProcess, IdFacility, idLanguage);
        }
        public IEnumerable<DbDataRecord> ProcessTaskMeasurements_ReadByProcessWhitOutFacility(Int64 IdProcess, String idLanguage)
        {
            return new ProcessTaskMeasurements().ReadByProcessWhitOutFacility(IdProcess, idLanguage);
        }
        public void ProcessTaskMeasurements_Create(Int64 idProcess, Int64 idMeasurement, Int64 idScope, Int64 idActivity, String Reference)
            {
                new ProcessTaskMeasurements().Create(idProcess, idMeasurement, idScope, idActivity, Reference);
            }
        public void ProcessTaskMeasurements_Delete(Int64 idProcess)
            {
                new ProcessTaskMeasurements().Delete(idProcess);
            }
        public void ProcessTaskMeasurements_Update(Int64 idProcess, Int64 idMeasurement, Int64 idScope, Int64 idActivity, String Reference)
            {
                new ProcessTaskMeasurements().Update(idProcess, idMeasurement, idScope, idActivity, Reference);
            }
        public void ProcessTaskMeasurements_Update(Int64 idProcess, Boolean Status)
        {
            new ProcessTaskMeasurements().Update(idProcess, Status);
        }
        #endregion

        #region ProcessTaskDataRecoveries
        public void ProcessTaskDataRecoveries_Create(Int64 idProcess, Int64 idTaskParent)
            {
                new ProcessTaskDataRecoveries().Create(idProcess, idTaskParent);
            }
        public void ProcessTaskDataRecoveries_Delete(Int64 idProcess)
            {
                new ProcessTaskDataRecoveries().Delete(idProcess);
            }
        #endregion

        #region Measurement to Recovery
            public void ProcessTasks_CreateTaskDataRecoveryMeasurement(Int64 idProcess, DateTime measurementDate, Int64 idLogPerson)
            {
                new ProcessTasks().CreateTaskDataRecoveryMeasurement(idProcess, measurementDate, idLogPerson);
            }
            public void ProcessTasks_DeleteTaskDataRecoveryMeasurement(Int64 idProcess, Int64 idLogPerson)
            {
                new ProcessTasks().DeleteTaskDataRecoveryMeasurement(idProcess, idLogPerson);
            }
            public IEnumerable<DbDataRecord> ProcessTasks_ReadByDataRecoveryMeasurement()
            {
                return new ProcessTasks().ReadByDataRecoveryMeasurement();
            }
            public IEnumerable<DbDataRecord> ProcessTasks_ReadByDataRecoveryMeasurement(Int64 idProcess)
            {
                return new ProcessTasks().ReadByDataRecoveryMeasurement(idProcess);
            }
        #endregion
        #endregion

        # region TimeUnits
        #region Read Functions
            public IEnumerable<DbDataRecord> TimeUnits_ReadAll(String idLanguage)
        {
            return new TimeUnits().ReadAll(idLanguage);
        }
            public IEnumerable<DbDataRecord> TimeUnits_ReadById(Int64 idTimeUnit, String idLanguage)
        {
            return new TimeUnits().ReadById(idTimeUnit, idLanguage);
        }

        #endregion
        #endregion

        # region TimeUnits_LG
        #region Write Functions
            public void TimeUnits_LG_Create(Int64 idTimeUnit, String idLanguage, String name, Int64 idLogPerson)
        {
            new TimeUnits_LG().Create(idTimeUnit, idLanguage, name, idLogPerson);
        }
            public void TimeUnits_LG_Delete(Int64 idTimeUnit, String idLanguage, Int64 idLogPerson)
        {
            new TimeUnits_LG().Delete(idTimeUnit, idLanguage, idLogPerson);
        }
            public void TimeUnits_LG_Update(Int64 idTimeUnit, String idLanguage, String name, Int64 idLogPerson)
        {
            new TimeUnits_LG().Update(idTimeUnit, idLanguage, name, idLogPerson);
        }
        #endregion

        #region Read Functions
            public IEnumerable<DbDataRecord> TimeUnits_LG_ReadAll(Int64 idTimeUnit)
        {
            return new TimeUnits_LG().ReadAll(idTimeUnit);
        }
            public IEnumerable<DbDataRecord> TimeUnits_LG_ReadById(Int64 idTimeUnit, String idLanguage)
        {
            return new TimeUnits_LG().ReadById(idTimeUnit, idLanguage);
        }
        #endregion
        #endregion

        #endregion
            /// <summary>
        /// Constructor del acceso a datos del mapa de procesos
        /// </summary>
        public ProcessesFramework()
        { 
        }

    
    }
}
