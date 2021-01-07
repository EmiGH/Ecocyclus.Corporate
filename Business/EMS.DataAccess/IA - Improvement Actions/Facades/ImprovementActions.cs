using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.IA
{
    public class ImprovementActions
    {
       # region Public Properties

        # region Exceptions
       
            #region Read Functions

        public IEnumerable<DbDataRecord> Exceptions_ReadByExcecution(Int64 idExecution, Int64 idProcess)
            {
                return new Entities.Exceptions().ReadByExcecution(idExecution, idProcess);
            }
        public IEnumerable<DbDataRecord> Exceptions_Read(Int64 idException)
            {
                return new Entities.Exceptions().Read(idException);
            }
        public IEnumerable<DbDataRecord> Exceptions_ReadByTask(Int64 idProcess)
            {
                return new Entities.Exceptions().ReadByTask(idProcess);
            }
        public IEnumerable<DbDataRecord> Exceptions_ReadByTaskOutOfRange(Int64 idProcess)
        {
            return new Entities.Exceptions().ReadByTaskOutOfRange(idProcess);
        }
        public IEnumerable<DbDataRecord> Exceptions_ReadByMeasurement(Int64 IdExecutionMeasurement)
        {
            return new Entities.Exceptions().ReadByMeasurement(IdExecutionMeasurement);
        }
        public IEnumerable<DbDataRecord> Exceptions_ReadByResource(Int64 idResource)
        {
            return new Entities.Exceptions().ReadByResource(idResource);
        }
        public IEnumerable<DbDataRecord> Exceptions_ReadByResourceVersion(Int64 idResource, Int64 idResourceFile)
        {
            return new Entities.Exceptions().ReadByResourceVersion(idResource, idResourceFile);
        }
        public IEnumerable<DbDataRecord> Exceptions_ReadForNotification()
            {
                return new Entities.Exceptions().ReadForNotification();
            }
        public IEnumerable<DbDataRecord> Exceptions_ReadExceptions(Int64 idProcess)
        {
            return new Entities.Exceptions().ReadExceptions(idProcess);
        }
            #endregion

            #region Write Functions

        public Int64 Exceptions_Create(Int64 idProcess, Int64 idExecution, Double measureValue, DateTime measureDate, Int16 idExceptionType, String comment, Int64 idLogPerson)
        {
            return new Entities.Exceptions().Create(idProcess, idExecution,measureValue, measureDate, idExceptionType, comment, idLogPerson);
        }
        public void Exceptions_DeleteExcutionMeasurementExceptions(Int64 idProcess, Int64 idException, Int64 IdExecutionMeasurement)
        {
            new Entities.Exceptions().DeleteExcutionMeasurementExceptions(idProcess, idException, IdExecutionMeasurement);
        }
        public Int64 Exceptions_Create(Int64 idProcess, Int64 idExecution, Int16 idExceptionType, String comment, Int64 idLogPerson)
            {
                return new Entities.Exceptions().Create(idProcess, idExecution, idExceptionType, comment, idLogPerson);
            }

        public void Exceptions_Create(Int64 idProcess, Int64 idException)
            {
                new Entities.Exceptions().Create(idProcess, idException);
            }
        public void Exceptions_Delete(Int64 idProcess, Int64 idException, Int64 idExecution)
        {
            new Entities.Exceptions().Delete(idProcess, idException, idExecution);
        }
        public void Exceptions_Delete(Int64 idException)
        {
            new Entities.Exceptions().Delete(idException);
        }
        public void Exceptions_Update(Int64 idException, Boolean notify)
        {
            new Entities.Exceptions().Update(idException, notify);
        }
        public void Exceptions_ExceptionAutomaticAlert(Int64 idLogPerson)
        {
            new Entities.Exceptions().ExceptionAutomaticAlert(idLogPerson);
        }
        public void Exceptions_DeleteResourceVersionExceptions(Int64 idException)
        {
            new Entities.Exceptions().DeleteResourceVersionExceptions(idException);
        }
            #endregion
        
        #endregion

        # region ExceptionStates
            
                #region Read Functions

                public IEnumerable<DbDataRecord> ExceptionStates_ReadAll(String idLanguage)
                {
                    return new Entities.ExceptionStates().ReadAll(idLanguage);
                }

                public IEnumerable<DbDataRecord> ExceptionStates_ReadById(Int64 idExceptionState, String idLanguage)
                {
                    return new Entities.ExceptionStates().ReadById(idExceptionState, idLanguage);
                }

                #endregion

                #region Write Functions

                public void ExceptionStates_ModifyState(Int64 IdException, Int64 IdExceptionState, String Comment, DateTime ExceptionDate, Int64 idLogPerson)
                {
                    new Entities.ExceptionStates().ModifyState(IdException, IdExceptionState, Comment, ExceptionDate, idLogPerson);
                }
                #endregion
            
        #endregion

        # region ExceptionStates

        #region Read Functions

        #endregion

        #region Write Functions

                public void ExceptionHistories_Delete(Int64 IdException)
        {
            new Entities.ExceptionHistories().Delete(IdException);
        }
        #endregion

        #endregion

        # region ExceptionStates_LG
                    #region Write Functions

                    public void ExceptionStates_LG_Create(Int64 idExceptionState, String idLanguage, String name, Int64 idLogPerson)
                    {
                        new Entities.ExceptionStates_LG().Create(idExceptionState, idLanguage, name, idLogPerson);
                    }
                    public void ExceptionStates_LG_Delete(Int64 idExceptionState, String idLanguage, Int64 idLogPerson)
                    {
                        new Entities.ExceptionStates_LG().Delete(idExceptionState, idLanguage, idLogPerson);
                    }
                    public void ExceptionStates_LG_Update(Int64 idExceptionState, String idLanguage, String name, Int64 idLogPerson)
                    {
                        new Entities.ExceptionStates_LG().Update(idExceptionState, idLanguage, name, idLogPerson);
                    }
                    #endregion

                    #region Read Functions
                    public IEnumerable<DbDataRecord> ExceptionStates_LG_ReadAll(Int64 idExceptionState)
                    {
                        return new Entities.ExceptionStates_LG().ReadAll(idExceptionState);
                    }

                    public IEnumerable<DbDataRecord> ExceptionStates_LG_ReadById(Int64 idExceptionType, String idLanguage)
                    {
                        return new Entities.ExceptionStates_LG().ReadById(idExceptionType, idLanguage);
                    }
                    #endregion

        #endregion

        # region ExceptionTypes
        
                        #region Read Functions
                        public IEnumerable<DbDataRecord> ExceptionTypes_ReadAll(String idLanguage)
                        {
                            return new Entities.ExceptionTypes().ReadAll(idLanguage);
                        }
                        public IEnumerable<DbDataRecord> ExceptionTypes_ReadById(Int64 idExceptionType, String idLanguage)
                        {
                            return new Entities.ExceptionTypes().ReadById(idExceptionType, idLanguage);
                        }
                        #endregion
                    
        #endregion

        # region ExceptionTypes_LG
                        
                            #region Write Functions
                            public void ExceptionTypes_LG_Create(Int64 IdExceptionType, String idLanguage, String name, String Description, Int64 idLogPerson)
                            {
                                new Entities.ExceptionTypes_LG().Create(IdExceptionType, idLanguage, name, Description, idLogPerson);
                            }
                            public void ExceptionTypes_LG_Delete(Int64 IdExceptionType, String idLanguage, Int64 idLogPerson)
                            {
                                new Entities.ExceptionTypes_LG().Delete(IdExceptionType, idLanguage, idLogPerson);
                            }
                            public void ExceptionTypes_LG_Update(Int64 IdExceptionType, String idLanguage, String name, String Description, Int64 idLogPerson)
                            {
                                new Entities.ExceptionTypes_LG().Update(IdExceptionType, idLanguage, name, Description, idLogPerson);
                            }
                            #endregion

                            #region Read Functions
                            public IEnumerable<DbDataRecord> ExceptionTypes_LG_ReadAll(Int64 idExceptionType)
                            {
                                return new Entities.ExceptionTypes_LG().ReadAll(idExceptionType);
                            }
                            public IEnumerable<DbDataRecord> ExceptionTypes_LG_ReadById(Int64 idExceptionType, String idLanguage)
                            {
                                return new Entities.ExceptionTypes_LG().ReadById(idExceptionType, idLanguage);
                            }
                            #endregion
                        
        #endregion

        # region ProjectClassifications

        #region Read Functions
        public IEnumerable<DbDataRecord> ProjectClassifications_ReadAll(String idLanguage)
        {
            return new Entities.ProjectClassifications().ReadAll(idLanguage);
        }
        public IEnumerable<DbDataRecord> ProjectClassifications_ReadById(Int64 idProjectClassification, String idLanguage)
        {
            return new Entities.ProjectClassifications().ReadById(idProjectClassification, idLanguage);
        }
        public IEnumerable<DbDataRecord> ProjectClassifications_ReadRoot(String idLanguage)
        {
            return new Entities.ProjectClassifications().ReadRoot(idLanguage);
        }
        public IEnumerable<DbDataRecord> ProjectClassifications_ReadByParent(Int64 idParentProjectClassification, String idLanguage)
        {
            return new Entities.ProjectClassifications().ReadByParent(idParentProjectClassification, idLanguage);
        }
        public IEnumerable<DbDataRecord> ProjectClassifications_ReadByProject(Int64 idProject, String idLanguage)
        {
            return new Entities.ProjectClassifications().ReadByProject(idProject, idLanguage);
        }
        #endregion

        #region Write Functions
        public Int64 ProjectClassifications_Create(Int64 idParentProjectClassification)
        {
            return new Entities.ProjectClassifications().Create(idParentProjectClassification);
        }
        public void ProjectClassifications_Delete(Int64 idProjectClassification)
        {
            new Entities.ProjectClassifications().Delete(idProjectClassification);
        }
        public void ProjectClassifications_Update(Int64 idProjectClassification, Int64 idParentProjectClassification)
        {
            new Entities.ProjectClassifications().Update(idProjectClassification, idParentProjectClassification);
        }
        public void ProjectClassifications_DeleteRelationships(Int64 idProjectClassification)
        {
            //TODO: hacer lo SP de la tabla de relacion con projects
        }
        #endregion

        #endregion

        # region ProjectClassifications_LG

            #region Write Functions
            public void ProjectClassifications_LG_Create(Int64 idProjectClassification, String idLanguage, String name, String Description)
            {
                new Entities.ProjectClassifications_LG().Create(idProjectClassification, idLanguage, name, Description);
            }
            public void ProjectClassifications_LG_Delete(Int64 idProjectClassification, String idLanguage)
            {
                new Entities.ProjectClassifications_LG().Delete(idProjectClassification, idLanguage);
            }
            public void ProjectClassifications_LG_Delete(Int64 idProjectClassification)
            {
                new Entities.ProjectClassifications_LG().Delete(idProjectClassification);
            }
            public void ProjectClassifications_LG_Update(Int64 idProjectClassification, String idLanguage, String name, String Description)
            {
                new Entities.ProjectClassifications_LG().Update(idProjectClassification, idLanguage, name, Description);
            }
            #endregion

            #region Read Functions
                                    public IEnumerable<DbDataRecord> ProjectClassifications_LG_ReadAll(Int64 idProjectClassification)
                                    {
                                        return new Entities.ProjectClassifications_LG().ReadAll(idProjectClassification);
                                    }
                                    public IEnumerable<DbDataRecord> ProjectClassifications_LG_ReadById(Int64 idProjectClassification, String idLanguage)
                                    {
                                        return new Entities.ProjectClassifications_LG().ReadById(idProjectClassification, idLanguage);
                                    }
                                    #endregion
                                

        #endregion

        # region Projects
 
        #region Read Functions
        public IEnumerable<DbDataRecord> Projects_ReadById(Int64 idProject)
        {
            return new Entities.Projects().ReadById(idProject);
        }
        public IEnumerable<DbDataRecord> Projects_ReadAll(Int64 idProjectClassification)
        {
            return new Entities.Projects().ReadAll(idProjectClassification);
        }
        #endregion

        #endregion

       #endregion

        public ImprovementActions()
        { 
        }
    }
}
