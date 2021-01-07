using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.PA
{
    public class PerformanceAssessments
    {
        # region Public Properties

        #region Methodologies
        #region Read Functions
        public IEnumerable<DbDataRecord> Methodologies_ReadById(Int64 idMethodology, String idLanguage)
        {
            return new Entities.Methodologies().ReadById(idMethodology, idLanguage);
        }
        public IEnumerable<DbDataRecord> Methodologies_ReadAll(String idLanguage)
        {
            return new Entities.Methodologies().ReadAll(idLanguage);
        }
        #endregion

        #region Write Functions
        public Int64 Methodologies_Create(Int64 idResource)
        {
            return new Entities.Methodologies().Create(idResource);
        }
        public void Methodologies_Delete(Int64 idMethodology)
        {
            new Entities.Methodologies().Delete(idMethodology);
        }

        public void Methodologies_Update(Int64 idMethodology, Int64 idResource)
        {
            new Entities.Methodologies().Update(idMethodology,idResource);
        }

        #endregion
        #endregion
        #region Methodologies_LG
        #region Read Functions
        public IEnumerable<DbDataRecord> Methodologies_LG_ReadById(Int64 idMethodology, String idLanguage)
        {
            return new Entities.Methodologies_LG().ReadById(idMethodology, idLanguage);
        }
        public IEnumerable<DbDataRecord> Methodologies_LG_ReadAll(Int64 idMethodology)
        {
            return new Entities.Methodologies_LG().ReadAll(idMethodology);
        }
        #endregion

        #region Write Functions
        public void Methodologies_LG_Create(Int64 idMethodology, String idLanguage, String methodName, String methodType, String description)
        {
            new Entities.Methodologies_LG().Create(idMethodology, idLanguage, methodName,methodType, description);
        }
        public void Methodologies_LG_Delete(Int64 idMethodology, String idLanguage)
        {
            new Entities.Methodologies_LG().Delete(idMethodology, idLanguage);
        }
        public void Methodologies_LG_Delete(Int64 idMethodology)
        {
            new Entities.Methodologies_LG().Delete(idMethodology);
        }
        public void Methodologies_LG_Update(Int64 idMethodology, String idLanguage, String methodName, String methodType, String description)
        {
            new Entities.Methodologies_LG().Update(idMethodology, idLanguage, methodName, methodType, description);
        }
        #endregion
        #endregion

        #region Qualities
        #region Read Functions
        public IEnumerable<DbDataRecord> Qualities_ReadById(Int64 idQuality, String idLanguage)
        {
            return new Entities.Qualities().ReadById(idQuality, idLanguage);
        }
        public IEnumerable<DbDataRecord> Qualities_ReadAll(String idLanguage)
        {
            return new Entities.Qualities().ReadAll(idLanguage);
        }
        #endregion

        #region Write Functions
        public Int64 Qualities_Create()
        {
            return new Entities.Qualities().Create();
        }
        public void Qualities_Delete(Int64 idQuality)
        {
            new Entities.Qualities().Delete(idQuality);
        }
        #endregion
        #endregion
        #region Qualities_LG
        #region Read Functions
        public IEnumerable<DbDataRecord> Qualities_LG_ReadById(Int64 idQuality, String idLanguage)
        {
            return new Entities.Qualities_LG().ReadById(idQuality, idLanguage);
        }
        public IEnumerable<DbDataRecord> Qualities_LG_ReadAll(Int64 idQuality)
        {
            return new Entities.Qualities_LG().ReadAll(idQuality);
        }
        #endregion

        #region Write Functions
        public void Qualities_LG_Create(Int64 idQuality, String idLanguage, String name, String description)
        {
            new Entities.Qualities_LG().Create(idQuality, idLanguage, name, description);
        }
        public void Qualities_LG_Delete(Int64 idQuality, String idLanguage)
        {
            new Entities.Qualities_LG().Delete(idQuality, idLanguage);
        }
        public void Qualities_LG_Delete(Int64 idQuality)
        {
            new Entities.Qualities_LG().Delete(idQuality);
        }
        public void Qualities_LG_Update(Int64 idQuality, String idLanguage, String name, String description)
        {
            new Entities.Qualities_LG().Update(idQuality, idLanguage, name, description);
        }
        #endregion
        #endregion

        #region ConstantClassifications
        #region Read Functions
        public IEnumerable<DbDataRecord> ConstantClassifications_ReadById(Int64 idConstantClassification, String idLanguage)
        {
            return new Entities.ConstantClassifications().ReadById(idConstantClassification, idLanguage);
        }
        public IEnumerable<DbDataRecord> ConstantClassifications_ReadByConstantClassification(Int64 idConstantClassification, String idLanguage)
        {
            return new Entities.ConstantClassifications().ReadByConstantClassification(idConstantClassification, idLanguage);
        }
        public IEnumerable<DbDataRecord> ConstantClassifications_ReadRoot(String idLanguage)
        {
            return new Entities.ConstantClassifications().ReadRoot(idLanguage);
        }
        #endregion

        #region Write Functions
        public Int64 ConstantClassifications_Create(Int64 idParentConstantClassification)
        {
            return new Entities.ConstantClassifications().Create(idParentConstantClassification);
        }
        public void ConstantClassifications_Delete(Int64 idConstantClassification)
        {
            new Entities.ConstantClassifications().Delete(idConstantClassification);
        }
        public void ConstantClassifications_Update(Int64 idConstant, Int64 idParentConstantClassification)
        {
            new Entities.ConstantClassifications().Update(idConstant, idParentConstantClassification);
        }
        #endregion
        #endregion
        #region ConstantClassifications_LG
        #region Read Functions
        public IEnumerable<DbDataRecord> ConstantClassifications_LG_ReadById(Int64 idConstantClassification, String idLanguage)
        {
            return new Entities.ConstantClassifications_LG().ReadById(idConstantClassification, idLanguage);
        }
        public IEnumerable<DbDataRecord> ConstantClassifications_LG_ReadAll(Int64 idConstantClassification)
        {
            return new Entities.ConstantClassifications_LG().ReadAll(idConstantClassification);
        }
        #endregion

        #region Write Functions
        public void ConstantClassifications_LG_Create(Int64 idConstantClassification, String idLanguage, String name, String description)
        {
            new Entities.ConstantClassifications_LG().Create(idConstantClassification, idLanguage, name, description);
        }
        public void ConstantClassifications_LG_Delete(Int64 idConstantClassification, String idLanguage)
        {
            new Entities.ConstantClassifications_LG().Delete(idConstantClassification, idLanguage);
        }
        public void ConstantClassifications_LG_Delete(Int64 idConstantClassification)
        {
            new Entities.ConstantClassifications_LG().Delete(idConstantClassification);
        }
        public void ConstantClassifications_LG_Update(Int64 idConstantClassification, String idLanguage, String name, String description)
        {
            new Entities.ConstantClassifications_LG().Update(idConstantClassification, idLanguage, name, description);
        }
        #endregion
        #endregion
        #region Constants
        #region Read Functions
        public IEnumerable<DbDataRecord> Constants_ReadById(Int64 idConstant, String idLanguage)
        {
            return new Entities.Constants().ReadById(idConstant, idLanguage);
        }
        public IEnumerable<DbDataRecord> Constants_ReadByConstantClassification(Int64 idConstantClassification, String idLanguage)
        {
            return new Entities.Constants().ReadByConstantClassification(idConstantClassification, idLanguage);
        }
        #endregion

        #region Write Functions
        public Int64 Constants_Create(String symbol, Double value, Int64 idMeasurementUnit, Int64 idConstantClassification)
        {
            return new Entities.Constants().Create(symbol, value, idMeasurementUnit, idConstantClassification);
        }
        public void Constants_Delete(Int64 idConstant)
        {
            new Entities.Constants().Delete(idConstant);
        }
        public void Constants_Update(Int64 idConstant, String symbol, Double value, Int64 idMeasurementUnit, Int64 idConstantClassification)
        {
            new Entities.Constants().Update(idConstant, symbol, value, idMeasurementUnit, idConstantClassification);
        }
        #endregion
        #endregion
        #region Constants_LG
        #region Read Functions
        public IEnumerable<DbDataRecord> Constants_LG_ReadById(Int64 idConstant, String idLanguage)
        {
            return new Entities.Constants_LG().ReadById(idConstant, idLanguage);
        }
        public IEnumerable<DbDataRecord> Constants_LG_ReadAll(Int64 idConstant)
        {
            return new Entities.Constants_LG().ReadAll(idConstant);
        }
        #endregion

        #region Write Functions
        public void Constants_LG_Create(Int64 idConstant, String idLanguage, String name, String description)
        {
            new Entities.Constants_LG().Create(idConstant, idLanguage, name, description);
        }
        public void Constants_LG_Delete(Int64 idConstant, String idLanguage)
        {
            new Entities.Constants_LG().Delete(idConstant, idLanguage);
        }
        public void Constants_LG_Delete(Int64 idConstant)
        {
            new Entities.Constants_LG().Delete(idConstant);
        }
        public void Constants_LG_Update(Int64 idConstant, String idLanguage, String name, String description)
        {
            new Entities.Constants_LG().Update(idConstant, idLanguage, name, description);
        }
        #endregion
        #endregion

        #region AccountingScenarios
        #region Read Functions
        public IEnumerable<DbDataRecord> AccountingScenarios_ReadById(Int64 idScenario, String idLanguage)
        {
            return new Entities.AccountingScenarios().ReadById(idScenario, idLanguage);
        }
        public IEnumerable<DbDataRecord> AccountingScenarios_ReadAll(String idLanguage)
        {
            return new Entities.AccountingScenarios().ReadAll(idLanguage);
        }
        #endregion

        #region Write Functions
        public Int64 AccountingScenarios_Create()
        {
            return new Entities.AccountingScenarios().Create();
        }
        public void AccountingScenarios_Delete(Int64 idScenario)
        {
            new Entities.AccountingScenarios().Delete(idScenario);
        }
        #endregion
        #endregion
        #region AccountingScenarios_LG
        #region Read Functions
        public IEnumerable<DbDataRecord> AccountingScenarios_LG_ReadById(Int64 idScenario, String idLanguage)
        {
            return new Entities.AccountingScenarios_LG().ReadById(idScenario, idLanguage);
        }
        public IEnumerable<DbDataRecord> AccountingScenarios_LG_ReadAll(Int64 idScenario)
        {
            return new Entities.AccountingScenarios_LG().ReadAll(idScenario);
        }
        #endregion

        #region Write Functions
        public void AccountingScenarios_LG_Create(Int64 idScenario, String idLanguage, String name, String description)
        {
            new Entities.AccountingScenarios_LG().Create(idScenario, idLanguage, name, description);
        }
        public void AccountingScenarios_LG_Delete(Int64 idScenario, String idLanguage)
        {
            new Entities.AccountingScenarios_LG().Delete(idScenario, idLanguage);
        }
        public void AccountingScenarios_LG_Delete(Int64 idScenario)
        {
            new Entities.AccountingScenarios_LG().Delete(idScenario);
        }
        public void AccountingScenarios_LG_Update(Int64 idScenario, String idLanguage, String name, String description)
        {
            new Entities.AccountingScenarios_LG().Update(idScenario, idLanguage, name, description);
        }
        #endregion
        #endregion
        #region AccountingScopes
        #region Read Functions
        public IEnumerable<DbDataRecord> AccountingScopes_ReadById(Int64 idScope, String idLanguage)
        {
            return new Entities.AccountingScopes().ReadById(idScope, idLanguage);
        }
        public IEnumerable<DbDataRecord> AccountingScopes_ReadAll(String idLanguage)
        {
            return new Entities.AccountingScopes().ReadAll(idLanguage);
        }
        #endregion

        #region Write Functions
        public Int64 AccountingScopes_Create()
        {
            return new Entities.AccountingScopes().Create();
        }
        public void AccountingScopes_Delete(Int64 idScope)
        {
            new Entities.AccountingScopes().Delete(idScope);
        }
        #endregion
        #endregion
        #region AccountingScopes_LG
        #region Read Functions
        public IEnumerable<DbDataRecord> AccountingScopes_LG_ReadById(Int64 idScope, String idLanguage)
        {
            return new Entities.AccountingScopes_LG().ReadById(idScope, idLanguage);
        }
        public IEnumerable<DbDataRecord> AccountingScopes_LG_ReadAll(Int64 idScope)
        {
            return new Entities.AccountingScopes_LG().ReadAll(idScope);
        }
        #endregion

        #region Write Functions
        public void AccountingScopes_LG_Create(Int64 idScope, String idLanguage, String name, String description)
        {
            new Entities.AccountingScopes_LG().Create(idScope, idLanguage, name, description);
        }
        public void AccountingScopes_LG_Delete(Int64 idScope, String idLanguage)
        {
            new Entities.AccountingScopes_LG().Delete(idScope, idLanguage);
        }
        public void AccountingScopes_LG_Delete(Int64 idScope)
        {
            new Entities.AccountingScopes_LG().Delete(idScope);
        }
        public void AccountingScopes_LG_Update(Int64 idScope, String idLanguage, String name, String description)
        {
            new Entities.AccountingScopes_LG().Update(idScope, idLanguage, name, description);
        }
        #endregion
        #endregion
        #region AccountingSectors

        #region Read Functions
        public IEnumerable<DbDataRecord> AccountingSectors_ReadById(Int64 idSector, String idLanguage)
        {
            return new Entities.AccountingSectors().ReadById(idSector, idLanguage);
        }
        public IEnumerable<DbDataRecord> AccountingSectors_ReadRoot(String idLanguage)
        {
            return new Entities.AccountingSectors().ReadRoot(idLanguage);
        }
        public IEnumerable<DbDataRecord> AccountingSectors_ReadBySector(Int64 idParentSector, String idLanguage)
        {
            return new Entities.AccountingSectors().ReadBySector(idParentSector, idLanguage);
        }
        #endregion

        #region Write Functions
        public Int64 AccountingSectors_Create(Int64 idParentSector)
        {
            return new Entities.AccountingSectors().Create(idParentSector);
        }
        public void AccountingSectors_Delete(Int64 idSector)
        {
            new Entities.AccountingSectors().Delete(idSector);
        }
        public void AccountingSectors_Update(Int64 idSector, Int64 idParentSector)
        {
            new Entities.AccountingSectors().Update(idSector, idParentSector);
        }
        #endregion
        #endregion
        #region AccountingSectors_LG
        #region Read Functions
        public IEnumerable<DbDataRecord> AccountingSectors_LG_ReadById(Int64 idSector, String idLanguage)
        {
            return new Entities.AccountingSectors_LG().ReadById(idSector, idLanguage);
        }
        public IEnumerable<DbDataRecord> AccountingSectors_LG_ReadAll(Int64 idSector)
        {
            return new Entities.AccountingSectors_LG().ReadAll(idSector);
        }
        #endregion

        #region Write Functions
        public void AccountingSectors_LG_Create(Int64 idSector, String idLanguage, String name, String description)
        {
            new Entities.AccountingSectors_LG().Create(idSector, idLanguage, name, description);
        }
        public void AccountingSectors_LG_Delete(Int64 idSector, String idLanguage)
        {
            new Entities.AccountingSectors_LG().Delete(idSector, idLanguage);
        }
        public void AccountingSectors_LG_Delete(Int64 idSector)
        {
            new Entities.AccountingSectors_LG().Delete(idSector);
        }
        public void AccountingSectors_LG_Update(Int64 idSector, String idLanguage, String name, String description)
        {
            new Entities.AccountingSectors_LG().Update(idSector, idLanguage, name, description);
        }
        #endregion
        #endregion
        #region AccountingActivities

        #region Read Functions
        public IEnumerable<DbDataRecord> AccountingActivities_ReadById(Int64 idActivity, String idLanguage)
        {
            return new Entities.AccountingActivities().ReadById(idActivity, idLanguage);
        }
        public IEnumerable<DbDataRecord> AccountingActivities_ReadRoot(String idLanguage)
        {
            return new Entities.AccountingActivities().ReadRoot(idLanguage);
        }
        public IEnumerable<DbDataRecord> AccountingActivities_ReadByActivity(Int64 idParentActivity, String idLanguage)
        {
            return new Entities.AccountingActivities().ReadByActivity(idParentActivity, idLanguage);
        }
        public IEnumerable<DbDataRecord> AccountingActivities_ReadTotalMeasurementResultByIndicator(Int64 idScope, Int64 idActivity, Int64 idIndicatorColumnGas, DateTime startDate, DateTime endDate)
        {
            return new Entities.AccountingActivities().ReadTotalMeasurementResultByIndicator(idScope, idActivity, idIndicatorColumnGas, startDate, endDate);
        }        
        #endregion

        #region Write Functions
        public Int64 AccountingActivities_Create(Int64 idParentActivity)
        {
            return new Entities.AccountingActivities().Create(idParentActivity);
        }
        public void AccountingActivities_Delete(Int64 idActivity)
        {
            new Entities.AccountingActivities().Delete(idActivity);
        }
        public void AccountingActivities_Update(Int64 idActivity, Int64 idParentActivity)
        {
            new Entities.AccountingActivities().Update(idActivity, idParentActivity);
        }
        #endregion
        #endregion
        #region AccountingActivities_LG
        #region Read Functions
        public IEnumerable<DbDataRecord> AccountingActivities_LG_ReadById(Int64 idActivity, String idLanguage)
        {
            return new Entities.AccountingActivities_LG().ReadById(idActivity, idLanguage);
        }
        public IEnumerable<DbDataRecord> AccountingActivities_LG_ReadAll(Int64 idActivity)
        {
            return new Entities.AccountingActivities_LG().ReadAll(idActivity);
        }
        #endregion

        #region Write Functions
        public void AccountingActivities_LG_Create(Int64 idActivity, String idLanguage, String name, String description)
        {
            new Entities.AccountingActivities_LG().Create(idActivity, idLanguage, name, description);
        }
        public void AccountingActivities_LG_Delete(Int64 idActivity, String idLanguage)
        {
            new Entities.AccountingActivities_LG().Delete(idActivity, idLanguage);
        }
        public void AccountingActivities_LG_Delete(Int64 idActivity)
        {
            new Entities.AccountingActivities_LG().Delete(idActivity);
        }
        public void AccountingActivities_LG_Update(Int64 idActivity, String idLanguage, String name, String description)
        {
            new Entities.AccountingActivities_LG().Update(idActivity, idLanguage, name, description);
        }
        #endregion
        #endregion

        # region CalculationCertificates

        #region Read Functions
        public IEnumerable<DbDataRecord> CalculationCertificates_ReadById(Int64 idCalculation, Int64 idCertificated)
        {
            return new Entities.CalculationCertificates().ReadById(idCalculation, idCertificated);
        }

        public IEnumerable<DbDataRecord> CalculationCertificates_ReadByCalculation(Int64 idCalculation)
        {
            return new Entities.CalculationCertificates().ReadByCalculation(idCalculation);
        }
        #endregion

        #region Write Functions
        public Int64 CalculationCertificates_Create(Int64 idCalculation, DateTime startDate, DateTime endDate, Decimal value, Int64 idOrganizationDOE, Int64 idLogPerson)
        {
            return new Entities.CalculationCertificates().Create(idCalculation, startDate, endDate, value, idOrganizationDOE, idLogPerson);
        }
        public void CalculationCertificates_Delete(Int64 idCalculation, Int64 idCertificated, Int64 idLogPerson)
        {
            new Entities.CalculationCertificates().Delete(idCalculation, idCertificated, idLogPerson);
        }
        public void CalculationCertificates_Update(Int64 idCalculation, Int64 idCertificated, DateTime startDate, DateTime endDate, Decimal value, Int64 idOrganizationDOE, Int64 idLogPerson)
        {
            new Entities.CalculationCertificates().Update(idCalculation, idCertificated, startDate, endDate, value, idOrganizationDOE, idLogPerson);
        }
        #endregion
        #endregion
        # region CalculationEstimates

        #region Read Functions
        public IEnumerable<DbDataRecord> CalculationEstimates_ReadById(Int64 idCalculation, Int64 idEstimated)
        {
            return new Entities.CalculationEstimates().ReadById(idCalculation, idEstimated);
        }
        public IEnumerable<DbDataRecord> CalculationEstimates_ReadByCalculation(Int64 idCalculation)
        {
            return new Entities.CalculationEstimates().ReadByCalculation(idCalculation);
        }

        public IEnumerable<DbDataRecord> CalculationEstimates_ReadByScenarioType(Int64 idCalculation, Int64 idScenarioType)
        {
            return new Entities.CalculationEstimates().ReadByScenarioType(idCalculation, idScenarioType);
        }
        public Boolean CalculationEstimates_Exists(Int64 idCalculation, DateTime startDate, DateTime endDate, Int64 idScenarioType)
        {
            return new Entities.CalculationEstimates().Exists(idCalculation, startDate, endDate, idScenarioType);
        }

        #endregion

        #region Write Functions
        public Int64 CalculationEstimates_Create(Int64 idCalculation, DateTime startDate, DateTime endDate, Int64 idScenarioType, Decimal value, Int64 idLogPerson)
        {
            return new Entities.CalculationEstimates().Create(idCalculation, startDate, endDate, idScenarioType, value, idLogPerson);
        }
        public void CalculationEstimates_Delete(Int64 idCalculation, Int64 idEstimated, Int64 idLogPerson)
        {
            new Entities.CalculationEstimates().Delete(idCalculation, idEstimated, idLogPerson);
        }
        public void CalculationEstimates_Update(Int64 idCalculation, Int64 idEstimated, DateTime startDate, DateTime endDate, Int64 idScenarioType, Decimal value, Int64 idLogPerson)
        {
            new Entities.CalculationEstimates().Update(idCalculation, idEstimated, startDate, endDate, idScenarioType, value, idLogPerson);
        }
        #endregion

        #endregion
        # region CalculationParameters

        #region Read Functions
        public IEnumerable<DbDataRecord> CalculationParameters_ReadAll(Int64 idCalculation)
        {
            return new Entities.CalculationParameters().ReadAll(idCalculation);
        }
        #endregion

        #region Write Functions
        public void CalculationParameters_Create(Int64 idCalculation, Int64 positionParameter, Int64 IdMeasurementParameter, String parameterName)
        {
            new Entities.CalculationParameters().Create(idCalculation, positionParameter, IdMeasurementParameter, parameterName);
        }
        public void CalculationParameters_Delete(Int64 idCalculation, Int64 positionParameter, Int64 idLogPerson)
        {
            new Entities.CalculationParameters().Delete(idCalculation, positionParameter, idLogPerson);
        }
        public void CalculationParameters_Delete(Int64 idCalculation)
        {
            new Entities.CalculationParameters().Delete(idCalculation);
        }

        public void CalculationParameters_Update(Int64 idCalculation, Int64 positionParameter, Int64 IdMeasurementParameter, String parameterName, Int64 idLogPerson)
        {
            new Entities.CalculationParameters().Update(idCalculation, positionParameter, IdMeasurementParameter, parameterName, idLogPerson);
        }

        #endregion
        #endregion
        # region Calculations

        #region Read Functions
        public IEnumerable<DbDataRecord> Calculations_ReadById(Int64 idCalculation, String idLanguage)
        {
            return new Entities.Calculations().ReadById(idCalculation, idLanguage);
        }
        public IEnumerable<DbDataRecord> Calculations_ReadAll(String idLanguage)
        {
            return new Entities.Calculations().ReadAll(idLanguage);
        }
        public IEnumerable<DbDataRecord> Calculations_ReadByProcess(Int64 idProcess, String idLanguage)
        {
            return new Entities.Calculations().ReadByProcess(idProcess, idLanguage);
        }
        public IEnumerable<DbDataRecord> Calculations_ReadByProcessIndicator(Int64 idProcess, Int64 indicator, String idLanguage)
        {
            return new Entities.Calculations().ReadByProcessIndicator(idProcess, indicator, idLanguage);
        }
        public IEnumerable<DbDataRecord> Calculations_ReadByFormula(Int64 idFormula, String idLanguage)
        {
            return new Entities.Calculations().ReadByFormula(idFormula, idLanguage);
        }

        public IEnumerable<DbDataRecord> Calculations_ReadSeries(Int64 idCalculation, DateTime? startDate, DateTime? endDate)
        {
            return new Entities.Calculations().ReadSeries(idCalculation, startDate, endDate);
        }
        public IEnumerable<DbDataRecord> Calculations_ReadSeriesForecasted(Int64 idCalculation, DateTime? startDate, DateTime? endDate, Int64 idScenarioType)
        {
            return new Entities.Calculations().ReadSeriesForecasted(idCalculation, startDate, endDate, idScenarioType);
        }
        public IEnumerable<DbDataRecord> Calculations_ReadSeriesVerificated(Int64 idCalculation, DateTime? startDate, DateTime? endDate)
        {
            return new Entities.Calculations().ReadSeriesVerificated(idCalculation, startDate, endDate);
        }
        #endregion

        #region Write Functions
        public Int64 Calculations_Create(Int64 idFormula, DateTime creationDate, Int64 idMeasurementUnit, Int16 Frequency, Int64 IdTimeUnitFrequency, Boolean isRelevant, Int64 idLogPerson)
        {
            return new Entities.Calculations().Create(idFormula, creationDate, idMeasurementUnit, Frequency, IdTimeUnitFrequency, isRelevant, idLogPerson);
        }
        public void Calculations_Delete(Int64 idCalculation, Int64 idLogPerson)
        {
            new Entities.Calculations().Delete(idCalculation, idLogPerson);
        }
        public void Calculations_Update(Int64 idCalculation, Int64 idMeasurementUnit, Int16 Frequency, Int64 IdTimeUnitFrequency, Boolean isRelevant, Int64 idLogPerson)
        {
            new Entities.Calculations().Update(idCalculation, idMeasurementUnit, Frequency, IdTimeUnitFrequency, isRelevant, idLogPerson);
        }
        public void Calculations_Update(Int64 idCalculation, Decimal lastResult, DateTime dateLastResult, Int64 idLogPerson)
        {
            new Entities.Calculations().Update(idCalculation, lastResult, dateLastResult, idLogPerson);
        }
        public void Calculations_CreateHistoryResult(Int64 idCalculation, DateTime timeStamp, Decimal result, Int64 idLogPerson)
        {
            new Entities.Calculations().CreateHistoryResult(idCalculation, timeStamp, result, idLogPerson);
        }
        public void Calculations_DeleteHistoryResult(Int64 idCalculation)
        {
            new Entities.Calculations().DeleteHistoryResult(idCalculation);
        }

        #region AssociatedProjets
        public void Calculations_CreateCalculationProcessGroupPrjects(Int64 idCalculation, Int64 idProcess)
        {
            new Entities.Calculations().CreateCalculationProcessGroupPrjects(idCalculation, idProcess);
        }
        public void Calculations_DeleteCalculationProcessGroupPrjects(Int64 idCalculation, Int64 idProcess)
        {
            new Entities.Calculations().DeleteCalculationProcessGroupPrjects(idCalculation, idProcess);
        }
        public void Calculations_DeleteCalculationProcessGroupPrjects(Int64 idCalculation)
        {
            new Entities.Calculations().DeleteCalculationProcessGroupPrjects(idCalculation);
        }
        #endregion
        #endregion

        #region Calculate Functions


        public Decimal Calculations_Calculate(String spName, Dictionary<String, Int64> parameters, DateTime startDate, DateTime endDate)
        {
            return new Entities.Calculations().Calculate(spName, parameters, startDate, endDate);
        }

        public Decimal Calculations_Calculate(String spName, Dictionary<String, Int64> parameters)
        {
            return new Entities.Calculations().Calculate(spName, parameters);
        }
        #endregion
        #endregion
        # region Calculations_LG

        #region Read Functions
        public IEnumerable<DbDataRecord> Calculations_LG_ReadAll(Int64 idCalculation)
        {
            return new Entities.Calculations_LG().ReadAll(idCalculation);
        }
        public IEnumerable<DbDataRecord> Calculations_LG_ReadById(Int64 idCalculation, String idLanguage)
        {
            return new Entities.Calculations_LG().ReadById(idCalculation, idLanguage);
        }
        #endregion

        #region Write Functions
        public void Calculations_LG_Create(Int64 idCalculation, String idLanguage, String name, String Description, Int64 idLogPerson)
        {
            new Entities.Calculations_LG().Create(idCalculation, idLanguage, name, Description, idLogPerson);
        }
        public void Calculations_LG_Delete(Int64 idCalculation, String idLanguage, Int64 idLogPerson)
        {
            new Entities.Calculations_LG().Delete(idCalculation, idLanguage, idLogPerson);
        }
        public void Calculations_LG_Delete(Int64 idCalculation)
        {
            new Entities.Calculations_LG().Delete(idCalculation);
        }
        public void Calculations_LG_Update(Int64 idCalculation, String idLanguage, String name, String Description, Int64 idLogPerson)
        {
            new Entities.Calculations_LG().Update(idCalculation, idLanguage, name, Description, idLogPerson);
        }
        #endregion
        #endregion
        # region CalculationScenarioTypes

        #region Read Functions

        public IEnumerable<DbDataRecord> CalculationScenarioTypes_ReadAll(String idLanguage)
        {
            return new Entities.CalculationScenarioTypes().ReadAll(idLanguage);
        }

        public IEnumerable<DbDataRecord> CalculationScenarioTypes_ReadById(Int64 idScenarioType, String idLanguage)
        {
            return new Entities.CalculationScenarioTypes().ReadById(idScenarioType, idLanguage);
        }
        public IEnumerable<DbDataRecord> CalculationScenarioTypes_ReadByClassification(Int64 idProcessClassification, String idLanguage)
        {
            return new Entities.CalculationScenarioTypes().ReadByClassification(idProcessClassification, idLanguage);
        }
        #endregion

        #region Write Functions
        public Int64 CalculationScenarioTypes_Create(Int64 idLogPerson)
        {
            return new Entities.CalculationScenarioTypes().Create(idLogPerson);
        }
        public void CalculationScenarioTypes_Delete(Int64 idScenarioType, Int64 idLogPerson)
        {
            new Entities.CalculationScenarioTypes().Delete(idScenarioType, idLogPerson);
        }
        #endregion

        #endregion
        # region CalculationScenarioTypes_LG

        #region Write Functions
        public void CalculationScenarioTypes_LG_Create(Int64 idScenarioType, String idLanguage, String name, String description, Int64 idLogPerson)
        {
            new Entities.CalculationScenarioTypes_LG().Create(idScenarioType, idLanguage, name, description, idLogPerson);
        }
        public void CalculationScenarioTypes_LG_Delete(Int64 idScenarioType, String idLanguage, Int64 idLogPerson)
        {
            new Entities.CalculationScenarioTypes_LG().Delete(idScenarioType, idLanguage, idLogPerson);
        }
        public void CalculationScenarioTypes_LG_Delete(Int64 idScenarioType, Int64 idLogPerson)
        {
            new Entities.CalculationScenarioTypes_LG().Delete(idScenarioType, idLogPerson);
        }
        public void CalculationScenarioTypes_LG_Update(Int64 idScenarioType, String idLanguage, String name, String description, Int64 idLogPerson)
        {
            new Entities.CalculationScenarioTypes_LG().Update(idScenarioType, idLanguage, name, description, idLogPerson);
        }
        #endregion

        #region Read Functions
        public IEnumerable<DbDataRecord> CalculationScenarioTypes_LG_ReadAll(Int64 idScenarioType)
        {
            return new Entities.CalculationScenarioTypes_LG().ReadAll(idScenarioType);
        }
        public IEnumerable<DbDataRecord> CalculationScenarioTypes_LG_ReadById(Int64 idScenarioType, String idLanguage)
        {
            return new Entities.CalculationScenarioTypes_LG().ReadById(idScenarioType, idLanguage);
        }
        #endregion

        #endregion
        # region CalculationScenarioTypesProcessClassification

        #region Read Functions

        public IEnumerable<DbDataRecord> CalculationScenarioTypesProcessClassification_ReadAll(Int64 idProcessClassification)
        {
            return new Entities.CalculationScenarioTypesProcessClassification().ReadAll(idProcessClassification);
        }
        public IEnumerable<DbDataRecord> CalculationScenarioTypesProcessClassification_ReadById(Int64 idProcessClassification, Int64 idScenarioType)
        {
            return new Entities.CalculationScenarioTypesProcessClassification().ReadById(idProcessClassification, idScenarioType);
        }
        public IEnumerable<DbDataRecord> CalculationScenarioTypesProcessClassification_ReadByType(Int64 idScenarioType)
        {
            return new Entities.CalculationScenarioTypesProcessClassification().ReadByType(idScenarioType);
        }

        #endregion

        #region Write Functions
        public void CalculationScenarioTypesProcessClassification_Create(Int64 idProcessClassification, Int64 idScenarioType, Int64 idLogPerson)
        {
            new Entities.CalculationScenarioTypesProcessClassification().Create(idProcessClassification, idScenarioType, idLogPerson);
        }
        public void CalculationScenarioTypesProcessClassification_Delete(Int64 idProcessClassification, Int64 idScenarioType, Int64 idLogPerson)
        {
            new Entities.CalculationScenarioTypesProcessClassification().Delete(idProcessClassification, idScenarioType, idLogPerson);
        }
        public void CalculationScenarioTypesProcessClassification_Delete(Int64 idProcessClassification, Int64 idLogPerson)
        {
            new Entities.CalculationScenarioTypesProcessClassification().Delete(idProcessClassification, idLogPerson);
        }
        public void CalculationScenarioTypesProcessClassification_DeleteRelatedClassification(Int64 idScenarioType, Int64 idLogPerson)
        {
            new Entities.CalculationScenarioTypesProcessClassification().DeleteRelatedClassification(idScenarioType, idLogPerson);
        }
        #endregion
        #endregion

        #region CalculateOfTransformations
        #region Read Functions
        public IEnumerable<DbDataRecord> CalculateOfTransformations_ReadById(Int64 idTransformation, String idLanguage)
        {
            return new Entities.CalculateOfTransformations().ReadById(idTransformation, idLanguage);
        }
        public IEnumerable<DbDataRecord> CalculateOfTransformations_ReadTransformationParameter(Int64 idTransformation, String idLanguage)
        {
            return new Entities.CalculateOfTransformations().ReadTransformationParameter(idTransformation, idLanguage);
        }
        public IEnumerable<DbDataRecord> CalculateOfTransformations_ReadAll(String idLanguage)
        {
            return new Entities.CalculateOfTransformations().ReadAll(idLanguage);
        }
        public IEnumerable<DbDataRecord> CalculateOfTransformations_ReadAllRoots(String idLanguage)
        {
            return new Entities.CalculateOfTransformations().ReadAllRoots(idLanguage);
        }
        public IEnumerable<DbDataRecord> CalculateOfTransformations_ReadWhitErrors(String idLanguage)
        {
            return new Entities.CalculateOfTransformations().ReadWhitErrors(idLanguage);
        }       
        public IEnumerable<DbDataRecord> CalculateOfTransformations_ReadByMeasurement(Int64 idMeasurement, String idLanguage)
        {
            return new Entities.CalculateOfTransformations().ReadByMeasurement(idMeasurement, idLanguage);
        }
        public IEnumerable<DbDataRecord> CalculateOfTransformations_ReadByMeasurementAsParameter(Int64 idMeasurement, String idLanguage)
        {
            return new Entities.CalculateOfTransformations().ReadByMeasurementAsParameter(idMeasurement, idLanguage);
        }
        public IEnumerable<DbDataRecord> CalculateOfTransformations_ReadByIndicator(Int64 IdIndicator, String idLanguage)
        {
            return new Entities.CalculateOfTransformations().ReadByIndicator(IdIndicator, idLanguage);
        }
        public IEnumerable<DbDataRecord> CalculateOfTransformations_ReadByConstant(Int64 IdConstant, String idLanguage)
        {
            return new Entities.CalculateOfTransformations().ReadByConstant(IdConstant, idLanguage);
        }
        public IEnumerable<DbDataRecord> CalculateOfTransformations_ReadByTransformation(Int64 idTransformation, String idLanguage)
        {
            return new Entities.CalculateOfTransformations().ReadByTransformation(idTransformation, idLanguage);
        }
        public IEnumerable<DbDataRecord> CalculateOfTransformations_ReadByTransformationAsParameter(Int64 idTransformation, String idLanguage)
        {
            return new Entities.CalculateOfTransformations().ReadByTransformationAsParameter(idTransformation, idLanguage);
        }
        public IEnumerable<DbDataRecord> CalculateOfTransformations_ReadByProcess(Int64 idProcess, String idLanguage)
        {
            return new Entities.CalculateOfTransformations().ReadByProcess(idProcess, idLanguage);
        }
        public IEnumerable<DbDataRecord> CalculateOfTransformations_ReadMaxMinDate(Int64 IdTransformation)
        {
            return new Entities.CalculateOfTransformations().ReadMaxMinDate(IdTransformation);
        }
        public IEnumerable<DbDataRecord> CalculateOfTransformations_ReadOperateValue(Int64 IdTransformation, Boolean isCumulative, DateTime startDate, DateTime endDate)
        {
            return new Entities.CalculateOfTransformations().ReadOperateValue(IdTransformation, isCumulative, startDate, endDate);
        }

        public IEnumerable<DbDataRecord> CalculateOfTransformations_WasteReadYears(Int64 idTransformation, DateTime date)
        {
            return new Entities.CalculateOfTransformations().WasteReadYears(idTransformation, date);
        }
        public IEnumerable<DbDataRecord> CalculateOfTransformations_WasteReadValuesAndYears(Int64 idTransformation, DateTime startDate, DateTime endDate)
        {
            return new Entities.CalculateOfTransformations().WasteReadValuesAndYears(idTransformation , startDate, endDate);
        }
        public IEnumerable<DbDataRecord> CalculateOfTransformations_WasteReadValuesAndYearsForMonth(Int64 idTransformation, DateTime startDate, DateTime endDate)
        {
            return new Entities.CalculateOfTransformations().WasteReadValuesAndYearsForMonth(idTransformation, startDate, endDate);
        }
        #endregion

        #region Write Functions
        public Int64 CalculateOfTransformations_Create(Int64 IdMeasurementTransformer, Int64 IdTransformationTransformer, Int64 idIndicator, String formula, Int64 idMeasurementUnit, Int64 IdProcess, Int64 IdMeasurementOrigin, Int64 IdActivity)
        {
            return new Entities.CalculateOfTransformations().Create(IdMeasurementTransformer,IdTransformationTransformer, idIndicator, formula, idMeasurementUnit, IdProcess, IdMeasurementOrigin, IdActivity);
        }
        public void CalculateOfTransformations_Delete(Int64 idTransformation)
        {
            new Entities.CalculateOfTransformations().Delete(idTransformation);          
        }
        public void CalculateOfTransformations_Update(Int64 idTransformation, Int64 IdMeasurementTransformer, Int64 IdTransformationTransformer, Int64 idIndicator, String formula, Int64 idMeasurementUnit, Int64 IdProcess, Int64 IdMeasurementOrigin, Int64 IdActivity)
        {
            new Entities.CalculateOfTransformations().Update(idTransformation, IdMeasurementTransformer, IdTransformationTransformer, idIndicator, formula, idMeasurementUnit, IdProcess, IdMeasurementOrigin, IdActivity);
        }
        #endregion
        #endregion
        #region CalculateOfTransformationErrors
        #region Read Functions
        public IEnumerable<DbDataRecord> CalculateOfTransformationErrors_ReadAll(Int64 IdTransformation)
        {
            return new Entities.CalculateOfTransformationErrors().ReadAll(IdTransformation);
        }
        #endregion

        #region Write Functions
        public void CalculateOfTransformationErrors_Create(Int64 IdTransformation, Boolean Reported, String Description)
        {
            new Entities.CalculateOfTransformationErrors().Create(IdTransformation, Reported, Description);
        }
        public void CalculateOfTransformationErrors_Update(Int64 IdError, Int64 IdTransformation, Boolean Reported)
        {
            new Entities.CalculateOfTransformationErrors().Update(IdError,  IdTransformation, Reported);
        }
        public void CalculateOfTransformationErrors_Delete(Int64 IdTransformation)
        {
            new Entities.CalculateOfTransformationErrors().Delete(IdTransformation);
        }
        public void CalculateOfTransformationErrors_DeleteAll()
        {
            new Entities.CalculateOfTransformationErrors().DeleteAll();
        }
        #endregion
        #endregion
        #region CalculateOfTransformations_LG
        #region Read Functions
        public IEnumerable<DbDataRecord> CalculateOfTransformations_LG_ReadById(Int64 idTransformation, String idLanguage)
        {
            return new Entities.CalculateOfTransformations_LG().ReadById(idTransformation, idLanguage);
        }
        public IEnumerable<DbDataRecord> CalculateOfTransformations_LG_ReadAll(Int64 idTransformation)
        {
            return new Entities.CalculateOfTransformations_LG().ReadAll(idTransformation);
        }
        #endregion

        #region Write Functions
        public void CalculateOfTransformations_LG_Create(Int64 idTransformation, String idLanguage, String name, String description)
        {
            new Entities.CalculateOfTransformations_LG().Create(idTransformation, idLanguage, name, description);
        }
        public void CalculateOfTransformations_LG_Delete(Int64 idTransformation, String idLanguage)
        {
            new Entities.CalculateOfTransformations_LG().Delete(idTransformation, idLanguage);
        }
        public void CalculateOfTransformations_LG_Delete(Int64 idTransformation)
        {
            new Entities.CalculateOfTransformations_LG().Delete(idTransformation);
        }
        public void CalculateOfTransformations_LG_Update(Int64 idTransformation, String idLanguage, String name, String description)
        {
            new Entities.CalculateOfTransformations_LG().Update(idTransformation, idLanguage, name, description);
        }
        #endregion
        #endregion
        #region CalculateOfTransformationParameters
        #region Read Functions
        public IEnumerable<DbDataRecord> CalculateOfTransformationParameters_ReadById(String idParameter, Int64 idTransformation)
        {
            return new Entities.CalculateOfTransformationParameters().ReadByID(idParameter, idTransformation);
        }
        public IEnumerable<DbDataRecord> CalculateOfTransformationParameters_ReadAll(Int64 idTransformation)
        {
            return new Entities.CalculateOfTransformationParameters().ReadAll(idTransformation);
        }
        #endregion
        #endregion
        #region CalculateOfTransformationParameterConstants
        #region Write Functions
        public void CalculateOfTransformationParameterConstants_Create(String idParameter, Int64 idTransformation, Int64 idConstantOperand)
        {
            new Entities.CalculateOfTransformationParameterConstants().Create(idParameter, idTransformation, idConstantOperand);
        }
        public void CalculateOfTransformationParameterConstants_Delete(String idParameter, Int64 idTransformation)
        {
            new Entities.CalculateOfTransformationParameterConstants().Delete(idParameter, idTransformation);
        }
        public void CalculateOfTransformationParameterConstants_Delete(Int64 idTransformation)
        {
            new Entities.CalculateOfTransformationParameterConstants().Delete(idTransformation);
        }
        public void CalculateOfTransformationParameterConstants_Update(String idParameter, Int64 idTransformation, Int64 idConstantOperand)
        {
            new Entities.CalculateOfTransformationParameterConstants().Update(idParameter, idTransformation, idConstantOperand);
        }
        #endregion
        #endregion
        #region CalculateOfTransformationParameterMeasurements  
        #region Write Functions
        public void CalculateOfTransformationParameterMeasurements_Create(String idParameter, Int64 idTransformation, Int64 idMeasurementOperand)
        {
            new Entities.CalculateOfTransformationParameterMeasurements().Create(idParameter, idTransformation, idMeasurementOperand);
        }
        public void CalculateOfTransformationParameterMeasurements_Delete(String idParameter, Int64 idTransformation)
        {
            new Entities.CalculateOfTransformationParameterMeasurements().Delete(idParameter, idTransformation);
        }
        public void CalculateOfTransformationParameterMeasurements_Delete(Int64 idTransformation)
        {
            new Entities.CalculateOfTransformationParameterMeasurements().Delete(idTransformation);
        }
        public void CalculateOfTransformationParameterMeasurements_DeleteByMeasurement(Int64 IdMeasurementOperand)
        {
            new Entities.CalculateOfTransformationParameterMeasurements().DeleteByMeasurement(IdMeasurementOperand);
        }
        public void CalculateOfTransformationParameterMeasurements_Update(String idParameter, Int64 idTransformation, Int64 idMeasurementOperand)
        {
            new Entities.CalculateOfTransformationParameterMeasurements().Update(idParameter, idTransformation, idMeasurementOperand);
        }
        #endregion
        #endregion
        #region CalculateOfTransformationParameterTransformations  
        #region Write Functions
        public void CalculateOfTransformationParameterTransformations_Create(String idParameter, Int64 idTransformation, Int64 idTransformationOperand)
        {
            new Entities.CalculateOfTransformationParameterTransformations().Create(idParameter, idTransformation, idTransformationOperand);
        }
        public void CalculateOfTransformationParameterTransformations_Delete(String idParameter, Int64 idTransformation)
        {
            new Entities.CalculateOfTransformationParameterTransformations().Delete(idParameter, idTransformation);
        }
        public void CalculateOfTransformationParameterTransformations_Delete(Int64 idTransformation)
        {
            new Entities.CalculateOfTransformationParameterTransformations().Delete(idTransformation);
        }
        public void CalculateOfTransformationParameterTransformations_DeleteAsParameter(Int64 idTransformation)
        {
            new Entities.CalculateOfTransformationParameterTransformations().DeleteAsParameter(idTransformation);
        }
        public void CalculateOfTransformationParameterTransformations_Update(String idParameter, Int64 idTransformation, Int64 idTransformationOperand)
        {
            new Entities.CalculateOfTransformationParameterTransformations().Update(idParameter, idTransformation, idTransformationOperand);
        }
        #endregion
        #endregion
        #region CalculateOfTransformationMeasurementResults
        #region Read Functions
        public IEnumerable<DbDataRecord> CalculateOfTransformationMeasurementResults_ReadSeries(Int64 IdTransformation, DateTime? startDate, DateTime? endDate)
        {
            return new Entities.CalculateOfTransformationMeasurementResults().ReadSeries(IdTransformation, startDate, endDate);
        }
        public IEnumerable<DbDataRecord> CalculateOfTransformationMeasurementResults_TansformValues(Int64 IdMeasurement, Int64 IdTransformation)
        {
            return new Entities.CalculateOfTransformationMeasurementResults().TansformValues(IdMeasurement, IdTransformation);
        }
        public IEnumerable<DbDataRecord> CalculateOfTransformationMeasurementResults_ReadByIndicator(Int64 IdMeasurement, Int64 IdIndicatorColumnGas, DateTime StartDate, DateTime EndDate)
        {
            return new Entities.CalculateOfTransformationMeasurementResults().ReadByIndicator(IdMeasurement, IdIndicatorColumnGas, StartDate, EndDate);
        }
        #endregion
        #region Write Functions
        public void CalculateOfTransformationMeasurementResults_Create(Int64 IdTransformation, Int64 IdMeasurement, Double TransformationValue, DateTime TransformationDate, DateTime StartDate, DateTime EndDate, Double minuteValue)
        {
            new Entities.CalculateOfTransformationMeasurementResults().Create(IdTransformation, IdMeasurement, TransformationValue, TransformationDate, StartDate, EndDate, minuteValue);
        }
        public void CalculateOfTransformationMeasurementResults_Delete(Int64 IdTransformation)
        {
            new Entities.CalculateOfTransformationMeasurementResults().Delete(IdTransformation);
        }
        #endregion
        #endregion
        #region CalculateOfTransformationTransformationResults
        #region Read Functions
        public IEnumerable<DbDataRecord> CalculateOfTransformationTransformationResults_ReadSeries(Int64 IdTransformation, DateTime? startDate, DateTime? endDate)
        {
            return new Entities.CalculateOfTransformationTransformationResults().ReadSeries(IdTransformation, startDate, endDate);
        }
        public IEnumerable<DbDataRecord> CalculateOfTransformationTransformationResults_TansformValues(Int64 IdTransformationTransformer, Int64 IdTransformation)
        {
            return new Entities.CalculateOfTransformationTransformationResults().TansformValues(IdTransformationTransformer, IdTransformation);
        }
        public IEnumerable<DbDataRecord> CalculateOfTransformationTransformationResults_ReadByIndicator(Int64 IdTransformation, Int64 IdIndicatorColumnGas, DateTime StartDate, DateTime EndDate)
        {
            return new Entities.CalculateOfTransformationTransformationResults().ReadByIndicator(IdTransformation, IdIndicatorColumnGas, StartDate, EndDate);
        }
      
        #endregion
        #region Write Functions
        public void CalculateOfTransformationTransformationResults_Create(Int64 IdTransformation, Int64 IdTransformationTransformer, Double TransformationValue, DateTime TransformationDate, DateTime StartDate, DateTime EndDate, Double minuteValue)
        {
            new Entities.CalculateOfTransformationTransformationResults().Create(IdTransformation, IdTransformationTransformer, TransformationValue, TransformationDate, StartDate, EndDate, minuteValue);
        }
        public void CalculateOfTransformationTransformationResults_Delete(Int64 IdTransformation)
        {
            new Entities.CalculateOfTransformationTransformationResults().Delete(IdTransformation);
        }
        #endregion
        #endregion

        # region FormulaParameters

        #region Read Functions
        public IEnumerable<DbDataRecord> FormulaParameters_ReadAll(Int64 idFormula)
        {
            return new Entities.FormulaParameters().ReadAll(idFormula);
        }
        #endregion

        #region Write Functions
        public void FormulaParameters_Create(Int64 idFormula, Int64 positionParameter, Int64 idIndicator, Int64 idMeasurementUnit, String parameterName)
        {
            new Entities.FormulaParameters().Create(idFormula, positionParameter, idIndicator, idMeasurementUnit, parameterName);
        }
        public void FormulaParameters_Delete(Int64 idFormula, Int64 positionParameter, Int64 idLogPerson)
        {
            new Entities.FormulaParameters().Delete(idFormula, positionParameter, idLogPerson);
        }
        public void FormulaParameters_Update(Int64 idFormula, Int64 positionParameter, Int64 idIndicator, Int64 idMeasurementUnit, String parameterName, Int64 idLogPerson)
        {
            new Entities.FormulaParameters().Update(idFormula, positionParameter, idIndicator, idMeasurementUnit, parameterName, idLogPerson);
        }

        #endregion
        #endregion
        # region Formulas

        #region Read Functions
        public IEnumerable<DbDataRecord> Formulas_ReadAll(String idLanguage)
        {
            return new Entities.Formulas().ReadAll(idLanguage);
        }

        public IEnumerable<DbDataRecord> Formulas_ReadByIndicator(Int64 IdIndicator, String idLanguage)
        {
            return new Entities.Formulas().ReadByIndicator(IdIndicator, idLanguage);
        }
        public IEnumerable<DbDataRecord> Formulas_ReadById(Int64 idFormula, String idLanguage)
        {
            return new Entities.Formulas().ReadById(idFormula, idLanguage);
        }

        public IEnumerable<DbDataRecord> Formulas_HasCalculation(Int64 idFormula)
        {
            return new Entities.Formulas().HasCalculation(idFormula);
        }
        #endregion

        #region Write Functions
        public Int64 Formulas_Create(DateTime creationDate, String literalFormula, String schemaSP, Int64 idIndicator, Int64 idMeasurementUnit, Int64 idResourceVersion, Int64 idLogPerson)
        {
            return new Entities.Formulas().Create(creationDate, literalFormula, schemaSP, idIndicator, idMeasurementUnit, idResourceVersion, idLogPerson);
        }
        public void Formulas_Delete(Int64 idFormula, Int64 idLogPerson)
        {
            new Entities.Formulas().Delete(idFormula, idLogPerson);
        }
        public void Formulas_Update(Int64 idFormula, String literalFormula, Int64 idResourceVersion, Int64 idLogPerson)
        {
            new Entities.Formulas().Update(idFormula, literalFormula, idResourceVersion, idLogPerson);
        }

        #endregion
        #endregion
        # region Formulas_LG

        #region Read Functions
        public IEnumerable<DbDataRecord> Formulas_LG_ReadAll(Int64 idFormula)
        {
            return new Entities.Formulas_LG().ReadAll(idFormula);
        }
        public IEnumerable<DbDataRecord> Formulas_LG_ReadById(Int64 idFormula, String idLanguage)
        {
            return new Entities.Formulas_LG().ReadById(idFormula, idLanguage);
        }
        #endregion

        #region Write Functions
        public void Formulas_LG_Create(Int64 idFormula, String idLanguage, String name, String Description, Int64 idLogPerson)
        {
            new Entities.Formulas_LG().Create(idFormula, idLanguage, name, Description, idLogPerson);
        }
        public void Formulas_LG_Delete(Int64 idFormula, String idLanguage, Int64 idLogPerson)
        {
            new Entities.Formulas_LG().Delete(idFormula, idLanguage, idLogPerson);
        }
        public void Formulas_LG_Update(Int64 idFormula, String idLanguage, String name, String Description, Int64 idLogPerson)
        {
            new Entities.Formulas_LG().Update(idFormula, idLanguage, name, Description, idLogPerson);
        }
        #endregion

        #endregion
        # region FormulaStoredProcedureParameters

        #region Read Functions
        public IEnumerable<DbDataRecord> FormulaStoredProcedureParameters_ReadAll(String storedProcedureName)
        {
            return new Entities.FormulaStoredProcedureParameters().ReadAll(storedProcedureName);
        }
        #endregion
        #endregion
        # region FormulaStoredProcedures

        #region Read Functions
        public IEnumerable<DbDataRecord> FormulaStoredProcedures_ReadAll(String storedProcedureName)
        {
            return new Entities.FormulaStoredProcedures().ReadAll(storedProcedureName);
        }
        #endregion
        #endregion

        # region IndicatorClassifications

        #region Read Functions
        public IEnumerable<DbDataRecord> IndicatorClassifications_ReadAll(String idLanguage)
        {
            return new Entities.IndicatorClassifications().ReadAll(idLanguage);
        }

        public IEnumerable<DbDataRecord> IndicatorClassifications_ReadById(Int64 idIndicatorClassification, String idLanguage)
        {
            return new Entities.IndicatorClassifications().ReadById(idIndicatorClassification, idLanguage);
        }

        public IEnumerable<DbDataRecord> IndicatorClassifications_ReadRoot(String idLanguage)
        {
            return new Entities.IndicatorClassifications().ReadRoot(idLanguage);
        }
        public IEnumerable<DbDataRecord> IndicatorClassifications_ReadByClassification(Int64 idIndicatorClassificationsParent, String idLanguage)
        {
            return new Entities.IndicatorClassifications().ReadByClassification(idIndicatorClassificationsParent, idLanguage);
        }
        public IEnumerable<DbDataRecord> IndicatorClassifications_ReadByIndicator(Int64 idIndicator, String idLanguage)
        {
            return new Entities.IndicatorClassifications().ReadByIndicator(idIndicator, idLanguage);
        }

        #endregion

        #region Write Functions

        public Int64 IndicatorClassifications_Create(Int64 idParentIndicatorClassification)
        {
            return new Entities.IndicatorClassifications().Create(idParentIndicatorClassification);
        }

        public void IndicatorClassifications_Delete(Int64 idIndicatorClassification)
        {
            new Entities.IndicatorClassifications().Delete(idIndicatorClassification);
        }

        public void IndicatorClassifications_Update(Int64 idIndicatorClassification, Int64 idParentIndicatorClassification)
        {
            new Entities.IndicatorClassifications().Update(idIndicatorClassification, idParentIndicatorClassification);
        }
        #endregion

        #endregion
        # region IndicatorClassifications_LG

        #region Write Functions

        public void IndicatorClassifications_LG_Create(Int64 idIndicatorClassification, String idLanguage, String name, String Description)
        {
            new Entities.IndicatorClassifications_LG().Create(idIndicatorClassification, idLanguage, name, Description);
        }

        public void IndicatorClassifications_LG_Delete(Int64 idIndicatorClassification, String idLanguage)
        {
            new Entities.IndicatorClassifications_LG().Delete(idIndicatorClassification, idLanguage);
        }

        public void IndicatorClassifications_LG_DeleteByClassification(Int64 idIndicatorClassification)
        {
            new Entities.IndicatorClassifications_LG().Delete(idIndicatorClassification);
        }

        public void IndicatorClassifications_LG_Update(Int64 idIndicatorClassification, String idLanguage, String name, String Description)
        {
            new Entities.IndicatorClassifications_LG().Update(idIndicatorClassification, idLanguage, name, Description);
        }
        #endregion

        #region Read Functions

        public IEnumerable<DbDataRecord> IndicatorClassifications_LG_ReadAll(Int64 idIndicatorClassification)
        {
            return new Entities.IndicatorClassifications_LG().ReadAll(idIndicatorClassification);
        }

        public IEnumerable<DbDataRecord> IndicatorClassifications_LG_ReadById(Int64 idIndicatorClassification, String idLanguage)
        {
            return new Entities.IndicatorClassifications_LG().ReadById(idIndicatorClassification, idLanguage);
        }
        #endregion
        #endregion
        # region Indicators

        #region Read Functions
        public IEnumerable<DbDataRecord> Indicators_ReadAll(String idLanguage)
        {
            return new Entities.Indicators().ReadAll(idLanguage);
        }
        public IEnumerable<DbDataRecord> Indicators_ReadByClassification(Int64 idIndicatorClassification, String idLanguage)
        {
            return new Entities.Indicators().ReadByClassification(idIndicatorClassification, idLanguage);
        }
        public IEnumerable<DbDataRecord> Indicators_ReadById(Int64 idIndicator, String idLanguage)
        {
            return new Entities.Indicators().ReadById(idIndicator, idLanguage);
        }
        public IEnumerable<DbDataRecord> Indicators_ReadRoot(String idLanguage)
        {
            return new Entities.Indicators().ReadRoot(idLanguage);
        }
        #endregion

        #region Write Functions
        public Int64 Indicators_Create(Int64 idMagnitud, Boolean IsCumulative, String name, String description, String idLanguage)
        {
            return new Entities.Indicators().Create(idMagnitud, IsCumulative, name, description, idLanguage);
        }
        public void Indicators_Delete(Int64 idIndicator)
        {
            new Entities.Indicators().Delete(idIndicator);
        }
        public void Indicators_Update(Int64 idIndicator, Int64 idMagnitud)
        {
            new Entities.Indicators().Update(idIndicator, idMagnitud);
        }

        #endregion

        #region IndicatorClassificationIndicator

        public void Indicators_Create(Int64 idIndicator, Int64 idIndicatorClassification)
        {
            new Entities.Indicators().Create(idIndicator, idIndicatorClassification);
        }
        public void Indicators_Delete(Int64 idIndicator, Int64 idIndicatorClassification)
        {
            new Entities.Indicators().Delete(idIndicator, idIndicatorClassification);
        }
        public void Indicators_DeleteByIndicator(Int64 idIndicator)
        {
            new Entities.Indicators().DeleteByIndicator(idIndicator);
        }
        public void Indicators_DeleteByClassification(Int64 idIndicatorClassification)
        {
            new Entities.Indicators().DeleteByClassification(idIndicatorClassification);
        }
        #endregion
        #endregion
        # region Indicators_LG

        #region Read Functions
        public IEnumerable<DbDataRecord> Indicators_LG_ReadAll(Int64 idIndicator)
        {
            return new Entities.Indicators_LG().ReadAll(idIndicator);
        }
        public IEnumerable<DbDataRecord> Indicators_LG_ReadById(Int64 idIndicator, String idLanguage)
        {
            return new Entities.Indicators_LG().ReadById(idIndicator, idLanguage);
        }
        #endregion

        #region Write Functions
        public void Indicators_LG_Create(Int64 idIndicator, String idLanguage, String name, String description, String scope, String limitation, String definition)
        {
            new Entities.Indicators_LG().Create(idIndicator, idLanguage, name, description, scope,limitation,definition);
        }
        public void Indicators_LG_Delete(Int64 idIndicator, String idLanguage)
        {
            new Entities.Indicators_LG().Delete(idIndicator, idLanguage);
        }
        public void Indicators_LG_Delete(Int64 idIndicator)
        {
            new Entities.Indicators_LG().Delete(idIndicator);
        }
        public void Indicators_LG_Update(Int64 idIndicator, String idLanguage, String name, String description, String scope, String limitation, String definition)
        {
            new Entities.Indicators_LG().Update(idIndicator, idLanguage, name, description, scope, limitation,definition);
        }
        #endregion
        #endregion    

        # region Magnitudes

        #region Read Functions
        public IEnumerable<DbDataRecord> Magnitudes_ReadAll(String idLanguage)
        {
            return new Entities.Magnitudes().ReadAll(idLanguage);
        }
        public IEnumerable<DbDataRecord> Magnitudes_ReadById(Int64 idMagnitud, String idLanguage)
        {
            return new Entities.Magnitudes().ReadById(idMagnitud, idLanguage);
        }
        #endregion

        #region Write Functions
        public Int64 Magnitudes_Create(String name, String idLanguage, Int64 idLogPerson)
        {
            return new Entities.Magnitudes().Create(name, idLanguage, idLogPerson);
        }
        public void Magnitudes_Delete(Int64 idMagnitud, Int64 idLogPerson)
        {
            new Entities.Magnitudes().Delete(idMagnitud, idLogPerson);
        }
        public void Magnitudes_Update(Int64 idMagnitud, String idLanguage, String name, Int64 idLogPerson)
        {
            new Entities.Magnitudes().Update(idMagnitud, idLanguage, name, idLogPerson);
        }
        #endregion
        #endregion
        # region Magnitudes_LG

        #region Read Functions
        public IEnumerable<DbDataRecord> Magnitudes_LG_ReadAll(Int64 idMagnitud)
        {
            return new Entities.Magnitudes_LG().ReadAll(idMagnitud);
        }
        public IEnumerable<DbDataRecord> Magnitudes_LG_ReadById(Int64 idMagnitud, String idLanguage)
        {
            return new Entities.Magnitudes_LG().ReadById(idMagnitud, idLanguage);
        }
        #endregion

        #region Write Functions
        public void Magnitudes_LG_Create(Int64 idMagnitud, String idLanguage, String name, Int64 idLogPerson)
        {
            new Entities.Magnitudes_LG().Create(idMagnitud, idLanguage, name, idLogPerson);
        }
        public void Magnitudes_LG_Delete(Int64 idMagnitud, String idLanguage, Int64 idLogPerson)
        {
            new Entities.Magnitudes_LG().Delete(idMagnitud, idLanguage, idLogPerson);
        }
        public void Magnitudes_LG_Update(Int64 idMagnitud, String idLanguage, String name, Int64 idLogPerson)
        {
            new Entities.Magnitudes_LG().Update(idMagnitud, idLanguage, name, idLogPerson);
        }
        #endregion
        #endregion

        # region MeasurementDeviceMeasurementUnits
        #region Read Functions
        public IEnumerable<DbDataRecord> MeasurementDeviceMeasurementUnits_ReadAll(Int64 idMeasurementDevice, String idLanguage)
        {
            return new Entities.MeasurementDeviceMeasurementUnits().ReadAll(idMeasurementDevice, idLanguage);
        }
        #endregion

        #region Write Functions
        public void MeasurementDeviceMeasurementUnits_Create(Int64 idMeasurementDevice, Int64 idMeasurementUnit, Int64 idLogPerson)
        {
            new Entities.MeasurementDeviceMeasurementUnits().Create(idMeasurementDevice, idMeasurementUnit, idLogPerson);
        }
        public void MeasurementDeviceMeasurementUnits_Delete(Int64 idMeasurementDevice, Int64 idMeasurementUnit, Int64 idLogPerson)
        {
            new Entities.MeasurementDeviceMeasurementUnits().Delete(idMeasurementDevice, idMeasurementUnit, idLogPerson);
        }
        public void MeasurementDeviceMeasurementUnits_Delete(Int64 idMeasurementDevice, Int64 idLogPerson)
        {
            new Entities.MeasurementDeviceMeasurementUnits().Delete(idMeasurementDevice, idLogPerson);
        }
        #endregion
        #endregion
        # region MeasurementDevices

        #region Write Functions

        public Int64 MeasurementDevices_Add(Int64 idDeviceType, String reference, String serialNumber, String brand, String model, String calibrationPeriodicity, String maintenance, DateTime? installationDate, Int64 idResourcePicture, Int64 idFacility, Double upperLimit, Double lowerLimit, Double uncertainty, Int64 idLogPerson)
        {
            return new Entities.MeasurementDevices().Add(idDeviceType, reference, serialNumber, brand, model, calibrationPeriodicity, maintenance, installationDate, idResourcePicture, idFacility,upperLimit,lowerLimit,uncertainty, idLogPerson);
        }
        public void MeasurementDevices_Remove(Int64 idDevice, Int64 idLogPerson)
        {
            new Entities.MeasurementDevices().Remove(idDevice, idLogPerson);
        }
        public void MeasurementDevices_Update(Int64 idDevice, Int64 idDeviceType, String reference, String serialNumber, String brand, String model, String calibrationPeriodicity, String maintenance, DateTime? installationDate, Int64 idResourcePicture, Int64 idFacility, Double upperLimit, Double lowerLimit, Double uncertainty, Int64 idLogPerson)
        {
            new Entities.MeasurementDevices().Update(idDevice, idDeviceType, reference, serialNumber, brand, model, calibrationPeriodicity, maintenance, installationDate, idResourcePicture, idFacility,upperLimit,lowerLimit,uncertainty, idLogPerson);
        }

        #endregion

        #region Read Functions
        public DateTime? MeasurementDevices_ReadCurrentCalibrationStartDate(Int64 idDevice)
        {
            return new Entities.MeasurementDevices().ReadCurrentCalibrationStartDate(idDevice);
        }
        public DateTime? MeasurementDevices_ReadCurrentCalibrationEndDate(Int64 idDevice)
        {
            return new Entities.MeasurementDevices().ReadCurrentCalibrationEndDate(idDevice);
        }
        public IEnumerable<DbDataRecord> MeasurementDevices_ReadById(Int64 idDevice)
        {
            return new Entities.MeasurementDevices().ReadById(idDevice);
        }
        public IEnumerable<DbDataRecord> MeasurementDevices_ReadAll(Int64 idMeasurementDeviceType)
        {
            return new Entities.MeasurementDevices().ReadAll(idMeasurementDeviceType);
        }
        public IEnumerable<DbDataRecord> MeasurementDevices_ReadAll()
        {
            return new Entities.MeasurementDevices().ReadAll();
        }
        #endregion
        #endregion
        # region MeasurementDeviceTypes

        #region Read Functions
        public IEnumerable<DbDataRecord> MeasurementDeviceTypes_ReadAll(String idLanguage)
        {
            return new Entities.MeasurementDeviceTypes().ReadAll(idLanguage);
        }
        public IEnumerable<DbDataRecord> MeasurementDeviceTypes_ReadById(Int64 idMeasurementDeviceType, String idLanguage)
        {
            return new Entities.MeasurementDeviceTypes().ReadById(idMeasurementDeviceType, idLanguage);
        }
        #endregion

        #region Write Functions
        public Int64 MeasurementDeviceTypes_Create(String name, String description, String idLanguage, Int64 idLogPerson)
        {
            return new Entities.MeasurementDeviceTypes().Create(name, description, idLanguage, idLogPerson);
        }

        public void MeasurementDeviceTypes_Delete(Int64 idMeasurementDeviceType, Int64 idLogPerson)
        {
            new Entities.MeasurementDeviceTypes().Delete(idMeasurementDeviceType, idLogPerson);
        }

        public void MeasurementDeviceTypes_Update(Int64 idMeasurementDeviceType, String idLanguage, String name, String Description, Int64 idLogPerson)
        {
            new Entities.MeasurementDeviceTypes().Update(idMeasurementDeviceType, idLanguage, name, Description, idLogPerson);
        }
        #endregion
        #endregion
        # region MeasurementDeviceTypes_LG

        #region Read Functions
        public IEnumerable<DbDataRecord> MeasurementDeviceTypes_LG_ReadAll(Int64 idMeasurementDeviceType)
        {
            return new Entities.MeasurementDeviceTypes_LG().ReadAll(idMeasurementDeviceType);
        }
        public IEnumerable<DbDataRecord> MeasurementDeviceTypes_LG_ReadById(Int64 idMeasurementDeviceType, String idLanguage)
        {
            return new Entities.MeasurementDeviceTypes_LG().ReadById(idMeasurementDeviceType, idLanguage);
        }
        #endregion

        #region Write Functions
        public void MeasurementDeviceTypes_LG_Create(Int64 idMeasurementDeviceType, String idLanguage, String name, String description, Int64 idLogPerson)
        {
            new Entities.MeasurementDeviceTypes_LG().Create(idMeasurementDeviceType, idLanguage, name, description, idLogPerson);
        }
        public void MeasurementDeviceTypes_LG_Delete(Int64 idMeasurementDeviceType, String idLanguage, Int64 idLogPerson)
        {
            new Entities.MeasurementDeviceTypes_LG().Delete(idMeasurementDeviceType, idLanguage, idLogPerson);
        }
        public void MeasurementDeviceTypes_LG_Update(Int64 idMeasurementDeviceType, String idLanguage, String name, String description, Int64 idLogPerson)
        {
            new Entities.MeasurementDeviceTypes_LG().Update(idMeasurementDeviceType, idLanguage, name, description, idLogPerson);
        }
        #endregion
        #endregion
        # region Measurements

        #region Read Functions

        public IEnumerable<DbDataRecord> Measurements_ReadAll(String idLanguage)
        {
            return new Entities.Measurements().ReadAll(idLanguage);
        }
        public IEnumerable<DbDataRecord> Measurements_ReadByIndicator(Int64 idIndicator, String idLanguage)
        {
            return new Entities.Measurements().ReadByIndicator(idIndicator, idLanguage);
        }
        public IEnumerable<DbDataRecord> Measurements_ReadById(Int64 idMeasurement, String idLanguage)
        {
            return new Entities.Measurements().ReadById(idMeasurement, idLanguage);
        }

        public Entities.MeasurementStatistics Measurements_ReadStatistics(Int64 idMeasurement, DateTime? startDate, DateTime? endDate)
        {
            return new Entities.Measurements().ReadStatistics(idMeasurement, startDate, endDate);
        }
        public IEnumerable<DbDataRecord> Measurements_ReadSeries(Int64 idMeasurement, DateTime? startDate, DateTime? endDate)
        {
            return new Entities.Measurements().ReadSeries(idMeasurement, startDate, endDate);
        }
        public IEnumerable<DbDataRecord> Measurements_ReadSeries(Int64 idMeasurement, DateTime? startDate, DateTime? endDate, Entities.Measurements.GroupingType groupingType, Entities.Measurements.AggregateType aggregateType)
        {
            return new Entities.Measurements().ReadSeries(idMeasurement, startDate, endDate, groupingType, aggregateType);
        }
        public IEnumerable<DbDataRecord> Measurements_ReadMaxMinDate(Int64 idMeasurement)
        {
            return new Entities.Measurements().ReadMaxMinDate(idMeasurement);
        }
        public IEnumerable<DbDataRecord> Measurements_ReadOperateValue(Int64 idMeasurement, Boolean isCumulative, DateTime startDate, DateTime endDate)
        {
            return new Entities.Measurements().ReadOperateValue(idMeasurement, isCumulative, startDate, endDate);
        }
        public IEnumerable<DbDataRecord> Measurements_ReadByFacilityForScope(Int64 idScope, Int64 IdFacility, Int64 IdActivity, String IdLanguage)
        {
            return new Entities.Measurements().ReadByFacilityForScope(idScope, IdFacility, IdActivity, IdLanguage);
        }
        public IEnumerable<DbDataRecord> Measurements_ReadByActivityAndProcess(Int64 IdActivity, Int64 IdProcess, String IdLanguage)
        {
            return new Entities.Measurements().ReadByActivityAndProcess(IdActivity, IdProcess, IdLanguage);
        }
        #endregion


        #region Write Functions


        public Int64 Measurements_Create(Int64 idDevice, Int64 idIndicator, Int64 idTimeUnitFrequency, Int32 frequency, 
            Int64 idMeasurementUnit, Boolean isRegressive, Boolean isRelevant, String source, String frequencyAtSource,
            Decimal uncertainty, Int64 idQuality, Int64 idMethodology)
        {
            return new Entities.Measurements().Create(idDevice, idIndicator, idTimeUnitFrequency, frequency, idMeasurementUnit, isRegressive, isRelevant, source, frequencyAtSource, uncertainty, idQuality, idMethodology);
        }
        public void Measurements_Delete(Int64 idMeasurement)
        {
            new Entities.Measurements().Delete(idMeasurement);
        }
        public void Measurements_Update(Int64 idMeasurement, Int64 idMeasurementDevice, Int64 idIndicator, 
            Int64 idMeasurementUnit, Int64 idTimeUnitFrequency, Int32 frequency, Boolean isRelevant, String source, 
            String frequencyAtSource,Decimal uncertainty, Int64 idQuality, Int64 idMethodology)
        {
            new Entities.Measurements().Update(idMeasurement, idMeasurementDevice, idIndicator, idMeasurementUnit, idTimeUnitFrequency, frequency, isRelevant, source, frequencyAtSource, uncertainty, idQuality, idMethodology);
        }

        #endregion
        #endregion
        # region Measurements_LG

        #region Read Functions
        public IEnumerable<DbDataRecord> Measurements_LG_ReadAll(Int64 idMeasurement)
        {
            return new Entities.Measurements_LG().ReadAll(idMeasurement);
        }
        public IEnumerable<DbDataRecord> Measurements_LG_ReadById(Int64 idMeasurement, String idLanguage)
        {
            return new Entities.Measurements_LG().ReadById(idMeasurement, idLanguage);
        }
        #endregion

        #region Write Functions
        public void Measurements_LG_Create(Int64 idMeasurement, String idLanguage, String name, String Description)
        {
            new Entities.Measurements_LG().Create(idMeasurement, idLanguage, name, Description);
        }
        public void Measurements_LG_Delete(Int64 idMeasurement, String idLanguage)
        {
            new Entities.Measurements_LG().Delete(idMeasurement, idLanguage);
        }
        public void Measurements_LG_Delete(Int64 idMeasurement)
        {
            new Entities.Measurements_LG().Delete(idMeasurement);
        }
        public void Measurements_LG_Update(Int64 idMeasurement, String idLanguage, String name, String Description)
        {
            new Entities.Measurements_LG().Update(idMeasurement, idLanguage, name, Description);
        }
        #endregion
        #endregion
        # region MeasurementUnits

        #region Read Functions
        public IEnumerable<DbDataRecord> MeasurementUnits_ReadAll(Int64 idMagnitud, String idLanguage)
        {
            return new Entities.MeasurementUnits().ReadAll(idMagnitud, idLanguage);
        }
        public IEnumerable<DbDataRecord> MeasurementUnits_ReadById(Int64 idMeasurementUnit, String idLanguage)
        {
            return new Entities.MeasurementUnits().ReadById(idMeasurementUnit, idLanguage);
        }
        #endregion

        #region Write Functions
        public Int64 MeasurementUnits_Create(Int64 numerator, Int64 denominator, Int64 exponent, Decimal constant, Boolean isPattern, Int64 idMagnitud, String name, String description, String idLanguage, Int64 idLogPerson)
        {
            return new Entities.MeasurementUnits().Create(numerator, denominator, exponent, constant, isPattern, idMagnitud, name, description, idLanguage, idLogPerson);
        }
        public void MeasurementUnits_Delete(Int64 idMeasurementUnit, Int64 idLogPerson)
        {
            new Entities.MeasurementUnits().Delete(idMeasurementUnit, idLogPerson);
        }
        public void MeasurementUnits_Delete(Int64 IdMagnitud)
        {
            new Entities.MeasurementUnits().Delete(IdMagnitud);
        }
        public void MeasurementUnits_Update(Int64 idMeasurementUnit, Int64 numerator, Int64 denominator, Int64 exponent, Decimal constant, Boolean isPattern, Int64 idMagnitud, String idLanguage, String name, String Description, Int64 idLogPerson)
        {
            new Entities.MeasurementUnits().Update(idMeasurementUnit, numerator, denominator, exponent, constant, isPattern, idMagnitud, idLanguage, name, Description, idLogPerson);
        }
        #endregion
        #endregion
        # region MeasurementUnits_LG

        #region Read Functions
        public IEnumerable<DbDataRecord> MeasurementUnits_LG_ReadAll(Int64 idMeasurementUnit)
        {
            return new Entities.MeasurementUnits_LG().ReadAll(idMeasurementUnit);
        }
        public IEnumerable<DbDataRecord> MeasurementUnits_LG_ReadById(Int64 idMeasurementUnit, String idLanguage)
        {
            return new Entities.MeasurementUnits_LG().ReadById(idMeasurementUnit, idLanguage);
        }
        #endregion

        #region Write Functions
        public void MeasurementUnits_LG_Create(Int64 idMeasurementUnit, String idLanguage, String name, String description, Int64 idLogPerson)
        {
            new Entities.MeasurementUnits_LG().Create(idMeasurementUnit, idLanguage, name, description, idLogPerson);
        }
        public void MeasurementUnits_LG_Delete(Int64 idMeasurementUnit, String idLanguage, Int64 idLogPerson)
        {
            new Entities.MeasurementUnits_LG().Delete(idMeasurementUnit, idLanguage, idLogPerson);
        }
        public void MeasurementUnits_LG_Update(Int64 idMeasurementUnit, String idLanguage, String name, String description, Int64 idLogPerson)
        {
            new Entities.MeasurementUnits_LG().Update(idMeasurementUnit, idLanguage, name, description, idLogPerson);
        }
        #endregion
        #endregion

        # region ParameterGroups

        #region Read Functions
        public IEnumerable<DbDataRecord> ParameterGroups_ReadAll(Int64 idIndicator, String idLanguage)
        {
            return new Entities.ParameterGroups().ReadAll(idIndicator, idLanguage);
        }
        public IEnumerable<DbDataRecord> ParameterGroups_ReadByMeasurement(Int64 idMeasurement, String idLanguage)
        {
            return new Entities.ParameterGroups().ReadByMeasurement(idMeasurement, idLanguage);
        }
        public IEnumerable<DbDataRecord> ParameterGroups_ReadById(Int64 idParameterGroup, String idLanguage)
        {
            return new Entities.ParameterGroups().ReadById(idParameterGroup, idLanguage);
        }
        #endregion

        #region Write Functions
        public Int64 ParameterGroups_Create(Int64 idIndicator, String name, String description, String idLanguage, Int64 idLogPerson)
        {
            return new Entities.ParameterGroups().Create(idIndicator, name, description, idLanguage, idLogPerson);
        }
        public void ParameterGroups_Delete(Int64 idParameterGroup, Int64 idLogPerson)
        {
            new Entities.ParameterGroups().Delete(idParameterGroup, idLogPerson);
        }
        public void ParameterGroups_Delete(Int64 idIndicator)
        {
            new Entities.ParameterGroups().Delete(idIndicator);
        }
        public void ParameterGroups_Update(Int64 idParameterGroup, Int64 idIndicator, String idLanguage, String name, String Description, Int64 idLogPerson)
        {
            new Entities.ParameterGroups().Update(idParameterGroup, idIndicator, idLanguage, name, Description, idLogPerson);
        }
        #endregion

        #region Write Functions relationship measurement
        public void ParameterGroups_Create(Int64 idMeasurement, Int64 idParameterGroup, Int64 idIndicator)
        {
            new Entities.ParameterGroups().Create(idMeasurement, idParameterGroup, idIndicator);
        }
        public void ParameterGroups_DeleteByPGP(Int64 idParameterGroup, Int64 idIndicator)
        {
            new Entities.ParameterGroups().DeleteByPGP(idParameterGroup, idIndicator);
        }
        public void ParameterGroups_DeleteByMeasurement(Int64 idMeasurement)
        {
            new Entities.ParameterGroups().DeleteByMeasurement(idMeasurement);
        }


        #endregion
        #endregion
        # region ParameterGroups_LG

        #region Read Functions
        public IEnumerable<DbDataRecord> ParameterGroups_LG_ReadAll(Int64 idParameterGroup)
        {
            return new Entities.ParameterGroups_LG().ReadAll(idParameterGroup);
        }
        public IEnumerable<DbDataRecord> ParameterGroups_LG_ReadById(Int64 idParameterGroup, String idLanguage)
        {
            return new Entities.ParameterGroups_LG().ReadById(idParameterGroup, idLanguage);
        }
        #endregion

        #region Write Functions
        public void ParameterGroups_LG_Create(Int64 idIndicator, Int64 idParameterGroup, String idLanguage, String name, String Description, Int64 idLogPerson)
        {
            new Entities.ParameterGroups_LG().Create(idIndicator, idParameterGroup, idLanguage, name, Description, idLogPerson);
        }
        public void ParameterGroups_LG_Delete(Int64 idParameterGroup, String idLanguage, Int64 idLogPerson)
        {
            new Entities.ParameterGroups_LG().Delete(idParameterGroup, idLanguage, idLogPerson);
        }
        public void ParameterGroups_LG_Update(Int64 idIndicator, Int64 idParameterGroup, String idLanguage, String name, String Description, Int64 idLogPerson)
        {
            new Entities.ParameterGroups_LG().Update(idIndicator, idParameterGroup, idLanguage, name, Description, idLogPerson);
        }
        #endregion
        #endregion
        # region ParameterRanges


        #region Write Functions
        public Int64 ParameterRanges_Add(Int64 idParameter, Double lowValue, Double highValue)
        {
            return new Entities.ParameterRanges().Add(idParameter, lowValue, highValue);
        }
        public void ParameterRanges_Remove(Int64 idParameterRange)
        {
            new Entities.ParameterRanges().Remove(idParameterRange);
        }
        public void ParameterRanges_RemoveByParameter(Int64 idParameter)
        {
            new Entities.ParameterRanges().RemoveByParameter(idParameter);
        }
        public void ParameterRanges_Update(Int64 idParameterRange, Int64 idParameter, Double lowValue, Double highValue)
        {
            new Entities.ParameterRanges().Update(idParameterRange, idParameter, lowValue, highValue);
        }
        #endregion

        #region Read Functions
        public IEnumerable<DbDataRecord> ParameterRanges_ReadById(Int64 idParameter, Int64 idParameterRange)
        {
            return new Entities.ParameterRanges().ReadById(idParameter, idParameterRange);
        }
        public IEnumerable<DbDataRecord> ParameterRanges_ReadAll(Int64 idParameter)
        {
            return new Entities.ParameterRanges().ReadAll(idParameter);
        }
        public Int64 ParameterRanges_Validate(Int64 idParameterGroup, Double value)
        {
            return new Entities.ParameterRanges().Validate(idParameterGroup, value);
        }
        public Int64 ParameterRanges_ValidateMinRange(Int64 idParameterGroup, Double lowValue, Double highValue)
        {
            return new Entities.ParameterRanges().ValidateMinRange(idParameterGroup, lowValue, highValue);
        }
        #endregion
        #endregion
        # region Parameters

        #region Read Functions
        public IEnumerable<DbDataRecord> Parameters_ReadAll(Int64 idParameterGroup, String idLanguage)
        {
            return new Entities.Parameters().ReadAll(idParameterGroup, idLanguage);
        }
        public IEnumerable<DbDataRecord> Parameters_ReadById(Int64 idParameter, String idLanguage)
        {
            return new Entities.Parameters().ReadById(idParameter, idLanguage);
        }
        #endregion

        #region Write Functions
        public Int64 Parameters_Create(Int64 idParameterGroup, Int64 idIndicator, String description, String sign, Boolean raiseException, String idLanguage, Int64 idLogPerson)
        {
            return new Entities.Parameters().Create(idParameterGroup, idIndicator, description, sign, raiseException, idLanguage, idLogPerson);
        }
        public void Parameters_Delete(Int64 idParameter, Int64 idLogPerson)
        {
            new Entities.Parameters().Delete(idParameter, idLogPerson);
        }
        public void Parameters_Update(Int64 idParameter, Int64 idParameterGroup, Int64 idIndicator, String idLanguage, String Description, String sign, Boolean raiseException, Int64 idLogPerson)
        {
            new Entities.Parameters().Update(idParameter, idParameterGroup, idIndicator, idLanguage, Description, sign, raiseException, idLogPerson);
        }
        #endregion
        #endregion
        # region Parameters_LG

        #region Read Functions
        public IEnumerable<DbDataRecord> Parameters_LG_ReadAll(Int64 idParameter)
        {
            return new Entities.Parameters_LG().ReadAll(idParameter);
        }
        public IEnumerable<DbDataRecord> Parameters_LG_ReadById(Int64 idParameter, String idLanguage)
        {
            return new Entities.Parameters_LG().ReadById(idParameter, idLanguage);
        }
        #endregion

        #region Write Functions
        public void Parameters_LG_Create(Int64 idParameter, String idLanguage, String Description, Int64 idLogPerson)
        {
            new Entities.Parameters_LG().Create(idParameter, idLanguage, Description, idLogPerson);
        }
        public void Parameters_LG_Delete(Int64 idParameter, String idLanguage, Int64 idLogPerson)
        {
            new Entities.Parameters_LG().Delete(idParameter, idLanguage, idLogPerson);
        }
        public void Parameters_LG_Update(Int64 idParameter, String idLanguage, String Description, Int64 idLogPerson)
        {
            new Entities.Parameters_LG().Update(idParameter, idLanguage, Description, idLogPerson);
        }
        #endregion
        #endregion

        #region ConfigurationExcelFiles
        #region Read Functions
        public IEnumerable<DbDataRecord> ConfigurationExcelFiles_ReadById(Int64 IdExcelFile)
        {
            return new Entities.ConfigurationExcelFiles().ReadById(IdExcelFile);
        }
        public IEnumerable<DbDataRecord> ConfigurationExcelFiles_ReadByPerson(Int64 IdPerson)
        {
            return new Entities.ConfigurationExcelFiles().ReadByPerson(IdPerson);
        }
        public IEnumerable<DbDataRecord> ConfigurationExcelFiles_ReadAll()
        {
            return new Entities.ConfigurationExcelFiles().ReadAll();
        }
        #endregion

        #region Write Functions
        public Int64 ConfigurationExcelFiles_Create(String Name, String StartIndexOfDataRows, String StartIndexOfDataCols, Boolean IsDataRows, String IndexStartDate, String IndexEndDate)
        {
            return new Entities.ConfigurationExcelFiles().Create(Name,StartIndexOfDataRows,StartIndexOfDataCols,IsDataRows,IndexStartDate, IndexEndDate);
        }
        public void ConfigurationExcelFiles_Delete(Int64 IdExcelFile)
        {
            new Entities.ConfigurationExcelFiles().Delete(IdExcelFile);
        }
        public void ConfigurationExcelFiles_Update(Int64 IdExcelFile, String Name, String StartIndexOfDataRows, String StartIndexOfDataCols, Boolean IsDataRows, String IndexStartDate, String IndexEndDate)
        {
            new Entities.ConfigurationExcelFiles().Update(IdExcelFile, Name, StartIndexOfDataRows, StartIndexOfDataCols, IsDataRows, IndexStartDate, IndexEndDate);
        }
        #endregion
        #endregion
        #region Relationship
        public IEnumerable<DbDataRecord> ConfigurationExcelFiles_ReadByFile(Int64 IdExcelFile)
        {
            return new Entities.ConfigurationExcelFiles().ReadByFile(IdExcelFile);
        }
        public void ConfigurationExcelFiles_CreateRelationship(Int64 IdExcelFile, Int64 IdMeasurement, String IndexValue, String IndexDate)
        {
            new Entities.ConfigurationExcelFiles().CreateRelationship(IdExcelFile, IdMeasurement, IndexValue, IndexDate);
        }
        public void ConfigurationExcelFiles_DeleteRelationship(Int64 IdExcelFile)
        {
            new Entities.ConfigurationExcelFiles().DeleteRelationship(IdExcelFile);
        }
        public void ConfigurationExcelFiles_DeleteRelationshipByMeasurement(Int64 IdMeasurement)
        {
            new Entities.ConfigurationExcelFiles().DeleteRelationshipByMeasurement(IdMeasurement);
        } 
        #endregion

        #endregion
        /// <summary>
        /// Constructor del acceso a datos del performance assessment
        /// </summary>
        public PerformanceAssessments()
        { }



    }
}
