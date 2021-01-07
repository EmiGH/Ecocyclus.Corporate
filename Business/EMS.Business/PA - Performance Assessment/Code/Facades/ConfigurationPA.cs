using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Data;
using System.Transactions;

namespace Condesus.EMS.Business.PA.Entities
{
    public class ConfigurationPA : ISecurity
    {
        #region Internal Properties
        private Credential _Credential;
        #endregion
        #region External Properties
        internal Credential Credential
        {
            get { return _Credential; }
        }
        #endregion

        internal ConfigurationPA(Credential credential)
        {
            _Credential = credential;
        }

        #region ConfigurationExcelFile
        public PA.Entities.ConfigurationExcelFile ConfigurationExcelFile(Int64 idExcelFile)
        {
            //Realiza las validaciones de autorizacion 
            return new PA.Collections.ConfigurationExcelFiles(_Credential).Item(idExcelFile);
        }
        public Dictionary<Int64, PA.Entities.ConfigurationExcelFile> ConfigurationExcelFiles()
        {
            //Realiza las validaciones de autorizacion 
            return new PA.Collections.ConfigurationExcelFiles(_Credential).Items();
        }
        public PA.Entities.ConfigurationExcelFile ConfigurationExcelFileAdd(String name, String StartIndexOfDataRows, String StartIndexOfDataCols, Boolean ValuesInRows, String IndexStartDate, String IndexEndDate, Dictionary<String, Entities.ConfigurationAsociationMeasurementExcelFile> measurements)
        {
            //Realiza las validaciones de autorizacion 
            return new PA.Collections.ConfigurationExcelFiles(_Credential).Add(name, StartIndexOfDataRows, StartIndexOfDataCols, ValuesInRows, IndexStartDate, IndexEndDate, measurements);
        }
        public void Remove(Int64 idExcelFile)
        {
            //Realiza las validaciones de autorizacion 
            new PA.Collections.ConfigurationExcelFiles(_Credential).Remove(idExcelFile);
        }
        #endregion

        #region Methodologies
        public PA.Entities.Methodology Methodology(Int64 idMethodology)
        {
            //Realiza las validaciones de autorizacion 
            //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.View);
            return new PA.Collections.Methodologies(_Credential).Item(idMethodology);
        }
        public Dictionary<Int64, PA.Entities.Methodology> Methodologies()
        {
            //Realiza las validaciones de autorizacion 
            //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.View);
            return new PA.Collections.Methodologies(_Credential).Items();
        }
        public PA.Entities.Methodology MethodologyAdd(KC.Entities.Resource resource, String methodName, String methodType, String description)
        {
            //Realiza las validaciones de autorizacion 
            //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
            return new PA.Collections.Methodologies(_Credential).Create(resource, methodName, methodType, description);
        }
        public void Remove(Entities.Methodology methodology )
        {
            //Realiza las validaciones de autorizacion 
            //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
            new PA.Collections.Methodologies(_Credential).Delete(methodology);
        }
        #endregion

        #region Qualities
        public PA.Entities.Quality Quality(Int64 idQuality)
        {
            //Realiza las validaciones de autorizacion 
            //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.View);
            return new PA.Collections.Qualities(_Credential).Item(idQuality);
        }
        public Dictionary<Int64, PA.Entities.Quality> Qualities()
        {
            //Realiza las validaciones de autorizacion 
            //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.View);
            return new PA.Collections.Qualities(_Credential).Items();
        }
        public PA.Entities.Quality QualityAdd(String name, String description)
        {
            //Realiza las validaciones de autorizacion 
            //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
            return new PA.Collections.Qualities(_Credential).Create(name, description);
        }
        public void Remove(Entities.Quality quality)
        {
            //Realiza las validaciones de autorizacion 
            //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
            new PA.Collections.Qualities(_Credential).Delete(quality);
        }
        #endregion

        #region ConstantClassifications
        public PA.Entities.ConstantClassification ConstantClassification(Int64 idConstantClassification)
        {
            //Realiza las validaciones de autorizacion 
            //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.View);
            return new PA.Collections.ConstantClassifications(this).Item(idConstantClassification);
        }
        public Dictionary<Int64, PA.Entities.ConstantClassification> ConstantClassifications()
        {
            //Realiza las validaciones de autorizacion 
            //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.View);
            return new PA.Collections.ConstantClassifications(this).Items();
        }
        public PA.Entities.ConstantClassification ConstantClassificationAdd(Entities.ConstantClassification parentConstantClassification, String name, String description)
        {
            //Realiza las validaciones de autorizacion 
            //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
            return new PA.Collections.ConstantClassifications(this).Create(parentConstantClassification, name, description);
        }
        public void Remove(Entities.ConstantClassification constantClassification)
        {
            //Realiza las validaciones de autorizacion 
            //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
            new PA.Collections.ConstantClassifications(this).Delete(constantClassification);
        }
        #endregion

        #region AccountingActivities
        public PA.Entities.AccountingActivity AccountingActivity(Int64 idActivity)
        {
            //Realiza las validaciones de autorizacion 
            //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.View);
            return new PA.Collections.AccountingActivities(_Credential).Item(idActivity);
        }
        public Dictionary<Int64, PA.Entities.AccountingActivity> AccountingActivities()
        {
            //Realiza las validaciones de autorizacion 
            //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.View);
            return new PA.Collections.AccountingActivities(_Credential).Items();
        }
        public PA.Entities.AccountingActivity AccountingActivityAdd(Entities.AccountingActivity parentActivity, String name, String description)
        {
            using (TransactionScope _transactionScope = new TransactionScope())
            {
                //Realiza las validaciones de autorizacion 
                //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
                PA.Entities.AccountingActivity _accountingActivity = new PA.Collections.AccountingActivities(_Credential).Create(parentActivity, name, description);
                _transactionScope.Complete();
                return _accountingActivity;
            }
        }
        public void Remove(Entities.AccountingActivity activity)
        {
            using (TransactionScope _transactionScope = new TransactionScope())
            {
                //Realiza las validaciones de autorizacion 
                //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
                new PA.Collections.AccountingActivities(_Credential).Delete(activity);
                _transactionScope.Complete();
            }
        }
        #endregion

        #region AccountingScenarios
        public PA.Entities.AccountingScenario AccountingScenario(Int64 idAccountingScenario)
        {
            //Realiza las validaciones de autorizacion 
            //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.View);
            return new PA.Collections.AccountingScenarios(_Credential).Item(idAccountingScenario);
        }
        public Dictionary<Int64, PA.Entities.AccountingScenario> AccountingScenarios()
        {
            //Realiza las validaciones de autorizacion 
            //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.View);
            return new PA.Collections.AccountingScenarios(_Credential).Items();
        }
        public PA.Entities.AccountingScenario AccountingScenarioAdd(String name, String description)
        {
            using (TransactionScope _transactionScope = new TransactionScope())
            {
                //Realiza las validaciones de autorizacion 
                //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
                PA.Entities.AccountingScenario _acountingScenario = new PA.Collections.AccountingScenarios(_Credential).Create(name, description);
                _transactionScope.Complete();
                return _acountingScenario;
            }
        }
        public void Remove(Entities.AccountingScenario scenario)
        {
            using (TransactionScope _transactionScope = new TransactionScope())
            {
                //Realiza las validaciones de autorizacion 
                //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
                new PA.Collections.AccountingScenarios(_Credential).Delete(scenario);
                _transactionScope.Complete();
            }
        }
        #endregion

        #region AccountingScopes
        public PA.Entities.AccountingScope AccountingScope(Int64 idAccountingScope)
        {
            //Realiza las validaciones de autorizacion 
            //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.View);
            return new PA.Collections.AccountingScopes(_Credential).Item(idAccountingScope);
        }
        public Dictionary<Int64, PA.Entities.AccountingScope> AccountingScopes()
        {
            //Realiza las validaciones de autorizacion 
            //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.View);
            return new PA.Collections.AccountingScopes(_Credential).Items();
        }
        public PA.Entities.AccountingScope AccountingScopeAdd(String name, String description)
        {
            using (TransactionScope _transactionScope = new TransactionScope())
            {
                //Realiza las validaciones de autorizacion 
                //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
                PA.Entities.AccountingScope _scope = new PA.Collections.AccountingScopes(_Credential).Create(name, description);
                _transactionScope.Complete();
                return _scope;
            }
        }
        public void Remove(Entities.AccountingScope scope)
        {
            using (TransactionScope _transactionScope = new TransactionScope())
            {
                //Realiza las validaciones de autorizacion 
                //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
                new PA.Collections.AccountingScopes(_Credential).Delete(scope);
                _transactionScope.Complete();
            }
        }
        #endregion

        #region AccountingSectors
        public PA.Entities.AccountingSector AccountingSector(Int64 idSector)
        {
            //Realiza las validaciones de autorizacion 
            //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.View);
            return new PA.Collections.AccountingSectors(_Credential).Item(idSector);
        }
        public Dictionary<Int64, PA.Entities.AccountingSector> AccountingSectors()
        {
            //Realiza las validaciones de autorizacion 
            //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.View);
            return new PA.Collections.AccountingSectors(_Credential).Items();
        }
        public PA.Entities.AccountingSector AccountingSectorAdd(Entities.AccountingSector parentSector, String name, String description)
        {
            using (TransactionScope _transactionScope = new TransactionScope())
            {
                //Realiza las validaciones de autorizacion 
                //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
                PA.Entities.AccountingSector _sector = new PA.Collections.AccountingSectors(_Credential).Create(parentSector, name, description);
                _transactionScope.Complete();
                return _sector;
            }
        }
        public void Remove(Entities.AccountingSector sector)
        {
            using (TransactionScope _transactionScope = new TransactionScope())
            {
                //Realiza las validaciones de autorizacion 
                //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
                new PA.Collections.AccountingSectors(_Credential).Delete(sector);
                _transactionScope.Complete();
            }
        }
        #endregion

        #region Calculations
        public PA.Entities.Calculation Calculation(Int64 idCalculation)
        {
            //Realiza las validaciones de autorizacion 
            //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.View);
            return new PA.Collections.Calculations(_Credential).Item(idCalculation);
        }
        public Dictionary<Int64, PA.Entities.Calculation> Calculations()
        {
            //Realiza las validaciones de autorizacion 
            //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.View);
            return new PA.Collections.Calculations(_Credential).Items();
        }
        public PA.Entities.Calculation CalculationAdd(PA.Entities.Formula formula, PA.Entities.MeasurementUnit measurementUnit, String name, String description, Int16 frequency, PF.Entities.TimeUnit timeUnitFrequency, Dictionary<Int64, PF.Entities.ProcessGroupProcess> processGroupProcesses, DataTable parameters, Boolean isRelevant)
        {
            //Realiza las validaciones de autorizacion 
            //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
            return new PA.Collections.Calculations(_Credential).Add(formula, measurementUnit, DateTime.Now, name, description, frequency, timeUnitFrequency, processGroupProcesses, parameters, isRelevant);
        }
        public void CalculationRemove(Int64 idCalculation)
        {
            //Realiza las validaciones de autorizacion 
            //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
            new PA.Collections.Calculations(_Credential).Remove(idCalculation);
        }
        #endregion

        #region Calculations parameters
        public void CalculationParametersAdd(PA.Entities.Calculation calcualtion, Int64 positionParameters, PA.Entities.Measurement measurementParameter, String parameterName)
        {
            //Realiza las validaciones de autorizacion 
            //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
            new PA.Collections.CalculationParameters(_Credential).Add(calcualtion, positionParameters, measurementParameter, parameterName);
        }

        public PA.Entities.CalculationParameter CalculationParameter(Int64 idCalculation, Int64 positionParameter, Int64 idMeasurementParameter, String parameterName)
        {
            //Realiza las validaciones de autorizacion 
            //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.View);
            return new PA.Entities.CalculationParameter(idCalculation, positionParameter, idMeasurementParameter, parameterName, _Credential);
        }
        public void CalculationParameterRemove(Int64 idCalculation, Int64 positionParameter)
        {
            //Realiza las validaciones de autorizacion 
            //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
            new PA.Collections.CalculationParameters(_Credential).Remove(idCalculation, positionParameter);
        }
        #endregion

        #region Measurements
        public Dictionary<Int64, PA.Entities.Measurement> Measurements()
        {
            //Realiza las validaciones de autorizacion 
            //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.View);
            return new PA.Collections.Measurements(_Credential).Items();
        }
        public PA.Entities.Measurement Measurement(Int64 idMeasurement)
        {
            //Realiza las validaciones de autorizacion 
            //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.View);
            return new PA.Collections.Measurements(_Credential).Item(idMeasurement);
        }
        public PA.Entities.Measurement MeasurementAdd(PA.Entities.MeasurementDevice measurementDevice, List<PA.Entities.ParameterGroup> parametersGroups, 
            PA.Entities.Indicator indicator, PF.Entities.TimeUnit timeUnitFrequency, Int32 frequency, PA.Entities.MeasurementUnit measurementUnit, 
            String name, String description, Boolean isCumulative, Boolean isRegressive, Boolean isRelevant, String source,String frequencyAtSource, 
            Decimal uncertainty, Entities.Quality quality, Entities.Methodology methodology)
        {
            using (TransactionScope _transactionScope = new TransactionScope())
            {
                //Realiza las validaciones de autorizacion 
                //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
                PA.Entities.Measurement _measurement =  new PA.Collections.Measurements(_Credential).Add(measurementDevice, parametersGroups, indicator, name, description, timeUnitFrequency, frequency, measurementUnit, isRegressive, isRelevant, source, frequencyAtSource, uncertainty, quality, methodology);
                _transactionScope.Complete();
                return _measurement;
            }
        }
        public void MeasurementRemove(Measurement measurement)
        {
            using (TransactionScope _transactionScope = new TransactionScope())
            {
                //Realiza las validaciones de autorizacion 
                //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
                new PA.Collections.Measurements(_Credential).Remove(measurement);
                _transactionScope.Complete();
            }
        }
        #endregion

        #region Formula
        public Dictionary<Int64, PA.Entities.Formula> Formulas()
        {
            //Realiza las validaciones de autorizacion 
            //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.View);
            return new PA.Collections.Formulas(_Credential).Items();
        }
        public PA.Entities.Formula Formula(Int64 idFormula)
        {
            //Realiza las validaciones de autorizacion 
            //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.View);
            return new PA.Collections.Formulas(_Credential).Item(idFormula);
        }
        public PA.Entities.Formula FormulaAdd(String literalFormula, String schemaSP, PA.Entities.Indicator indicator, PA.Entities.MeasurementUnit MeasurementUnit, String name, String description, DataTable parameters, KC.Entities.ResourceVersion resourceVersion)
        {
            //Realiza las validaciones de autorizacion 
            //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);

            return new PA.Collections.Formulas(_Credential).Add(DateTime.Now, literalFormula, schemaSP, indicator.IdIndicator, MeasurementUnit.IdMeasurementUnit, name, description, parameters, resourceVersion);

        }
        public void FormulaRemove(Int64 idFormula)
        {
            //Realiza las validaciones de autorizacion 
            //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
            new PA.Collections.Formulas(_Credential).Remove(idFormula);
        }
        #endregion

        #region Formula Parameters

        //public PA.Entities.FormulaParameter FormulaParameter(Int64 idFormula, Int64 PositionParameter, Int64 IdIndicator, Int64 IdMeasurementUnit, String ParameterName)
        //{
        //    return new PA.Entities.FormulaParameter(idFormula, PositionParameter , IdIndicator, IdMeasurementUnit, ParameterName, _Credential);
        //}

        //public void FormulaParameterAdd(PA.Entities.Formula formula, Int64 PositionParameter, PA.Entities.Indicator indicator, PA.Entities.MeasurementUnit MeasurementUnit, String ParameterName)
        //{
        //    new PA.Collections.FormulaParameters(_Credential).Add(formula, PositionParameter, indicator, MeasurementUnit, ParameterName);                
        //}

        //public void FormulaParameterRemove(Int64 idFormula, Int64 positionParameter)
        //{
        //    new PA.Collections.FormulaParameters(_Credential).Remove(idFormula, positionParameter);
        //}

        #endregion

        #region Formula Stored Procedures
        public List<Entities.FormulaStoredProcedure> FormulaStoredProcedures()
        {
            //Realiza las validaciones de autorizacion 
            //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
            //Devuelve la lista de Nombres de SP's que se pueden seleccionar para usar en una Formula.
            return new Collections.FormulaStoredProcedures(_Credential).Items(Common.Constants.FormulaStoredProcedureNamePrefix);
        }
        #endregion

        #region Magnitudes
        public Dictionary<Int64, Entities.Magnitud> Magnitudes()
        {
            //Realiza las validaciones de autorizacion 
            //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.View);
            return new Collections.Magnitudes(_Credential).Items();
        }
        public Entities.Magnitud Magnitud(Int64 idMagnitud)
        {
            //Realiza las validaciones de autorizacion 
            //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.View);
            return new Collections.Magnitudes(_Credential).Item(idMagnitud);
        }
        public Entities.Magnitud MagnitudAdd(String name)
        {
            using (TransactionScope _transactionScope = new TransactionScope())
            {
                //Realiza las validaciones de autorizacion 
                //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
                Entities.Magnitud _magnitud = new Collections.Magnitudes(_Credential).Add(name);
                _transactionScope.Complete();
                return _magnitud;
            }
        }
        public void Remove(Magnitud magnitud)
        {
            using (TransactionScope _transactionScope = new TransactionScope())
            {
                //Realiza las validaciones de autorizacion 
                //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
                new Collections.Magnitudes(_Credential).Remove(magnitud);
                _transactionScope.Complete();
            }
        }
        #endregion

        #region Measurement Device Type
        public Dictionary<Int64, Entities.MeasurementDeviceType> MeasurementDeviceTypes()
        {
            //Realiza las validaciones de autorizacion 
            //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.View);
            return new Collections.MeasurementDeviceTypes(_Credential).Items();
        }
        public Entities.MeasurementDeviceType MeasurementDeviceType(Int64 idMeasurementDeviceType)
        {
            //Realiza las validaciones de autorizacion 
            //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.View);
            return new Collections.MeasurementDeviceTypes(_Credential).Item(idMeasurementDeviceType);
        }
        public Entities.MeasurementDeviceType MeasurementDeviceTypeAdd(String name, String description)
        {
            using (TransactionScope _transactionScope = new TransactionScope())
            {
                //Realiza las validaciones de autorizacion 
                //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
                Entities.MeasurementDeviceType _deviceType = new Collections.MeasurementDeviceTypes(_Credential).Add(name, description);
                _transactionScope.Complete();
                return _deviceType;
            }
        }
        //TODO: poner borrado en cascada para los measurementdevices
        public void MeasurementDeviceTypeRemove(Entities.MeasurementDeviceType measurementDeviceType)
        {
            using (TransactionScope _transactionScope = new TransactionScope())
            {
                //Realiza las validaciones de autorizacion 
                //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
                new Collections.MeasurementDeviceTypes(_Credential).Remove(measurementDeviceType);
                _transactionScope.Complete();
            }
        }
        #endregion

        #region Measurement Device
        public MeasurementDevice MeasurementDevice(Int64 idMeasurementDevice)
        {
            return new Collections.MeasurementDevices(_Credential).Item(idMeasurementDevice);
        }
        /// <summary>
        /// Devuelve todos lo devices 
        /// </summary>
        /// <returns></returns>
        public Dictionary<Int64, MeasurementDevice> MeasurementDevices()
        {
            return new Collections.MeasurementDevices(_Credential).Items(); 
        }
        #endregion

        #region CalculationScenarioTypes
        public Dictionary<Int64, PA.Entities.CalculationScenarioType> CalculationScenarioTypes()
        {
            //Realiza las validaciones de autorizacion 
            //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.View);
            return new PA.Collections.CalculationScenarioTypes(_Credential).Items();
        }
        public PA.Entities.CalculationScenarioType CalculationScenarioType(Int64 idScenarioType)
        {
            //Realiza las validaciones de autorizacion 
            //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.View);
            return new PA.Collections.CalculationScenarioTypes(_Credential).Item(idScenarioType);
        }
        public PA.Entities.CalculationScenarioType CalculationScenarioTypeAdd(String name, String description, Dictionary<Int64, PF.Entities.ProcessClassification> processClassifications)
        {

            //Realiza las validaciones de autorizacion 
            //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
            using (TransactionScope _transactionScope = new TransactionScope())
            {
                PA.Entities.CalculationScenarioType _calculationScenarioType = new PA.Collections.CalculationScenarioTypes(_Credential).Add(name, description);

                //Recorre para todas las clasificaciones e inserta una por una.
                foreach (PF.Entities.ProcessClassification _processClassification in processClassifications.Values)
                {
                    new PA.Collections.CalculationScenarioTypeProcessClassifications(_Credential).Add(_calculationScenarioType.IdScenarioType, _processClassification.IdProcessClassification);
                }
                _transactionScope.Complete();

                return _calculationScenarioType;
            }
        }
        public void CalculationScenarioTypeRemove(Int64 idScenarioType)
        {
            //Realiza las validaciones de autorizacion 
            //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
            using (TransactionScope _transactionScope = new TransactionScope())
            {
                //Primero se borra la asociacion.
                new PA.Collections.CalculationScenarioTypeProcessClassifications(_Credential).RemoveRelatedClassification(idScenarioType);

                //Despues se borra el type.
                new PA.Collections.CalculationScenarioTypes(_Credential).Remove(idScenarioType);

                _transactionScope.Complete();
            }
        }
        #endregion

        #region Security 15-02-2010

        #region Properties
        public Int64 IdObject
        {
            get { return 0; }
        }
        public String ClassName
        {
            get
            {
                return Common.Security.ConfigurationPA;
            }
        }
        #endregion

        #region Read
        #region Permissions
        internal Dictionary<Int64, Security.Entities.Permission> _Permissions;
        public Dictionary<Int64, Security.Entities.Permission> Permissions
        {
            get
            {
                if (_Permissions == null)
                { _Permissions = new Security.Collections.Permissions(_Credential).Items(this); }
                return _Permissions;
            }
        }
        #endregion

        //ALL
        public List<Security.Entities.RightPerson> SecurityPeople()
        {
            return new Security.Collections.Rights(_Credential).ReadPersonByObject(this);
        }

        public List<Security.Entities.RightJobTitle> SecurityJobTitles()
        {
            return new Security.Collections.Rights(_Credential).ReadJobTitleByObject(this);
        }
        //por ID
        public Security.Entities.RightJobTitle ReadJobTitleByID(DS.Entities.JobTitle jobTitle, Security.Entities.Permission permission)
        {
            return new Security.Collections.Rights(_Credential).ReadJobTitleByID(jobTitle, permission);
        }
        public Security.Entities.RightPerson ReadPersonByID(DS.Entities.Person person, Security.Entities.Permission permission)
        {
            return new Security.Collections.Rights(_Credential).ReadPersonByID(person, permission);
        }

        #endregion

        #region Write
        //Security Add
        public Security.Entities.RightPerson SecurityPersonAdd(DS.Entities.Person person, Security.Entities.Permission permission)
        {
            //Realiza las validaciones de autorizacion 
            //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
            //Alta el permiso
            Security.Entities.RightPerson _rightPerson = new Security.Collections.Rights(_Credential).Add(this, person, permission);

            return _rightPerson;
        }
        public Security.Entities.RightJobTitle SecurityJobTitleAdd(DS.Entities.JobTitle jobTitle, Security.Entities.Permission permission)
        {
            //Realiza las validaciones de autorizacion 
            //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
            //Alta el permiso
            Security.Entities.RightJobTitle _rightJobTitle = new Security.Collections.Rights(_Credential).Add(this, jobTitle, permission);

            return _rightJobTitle;
        }
        //Security Remove
        public void SecurityPersonRemove(DS.Entities.Person person, Security.Entities.Permission permission)
        {
            //Realiza las validaciones de autorizacion 
            //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
            //Borra el permiso
            new Security.Collections.Rights(_Credential).Remove(person, this, permission);
        }
        public void SecurityJobTitleRemove(DS.Entities.JobTitle jobTitle, Security.Entities.Permission permission)
        {
            //Realiza las validaciones de autorizacion 
            //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
            //Borra el permiso
            new Security.Collections.Rights(_Credential).Remove(jobTitle, this, permission);
        }
        //Security Modify
        public Security.Entities.RightPerson SecurityPersonModify(Security.Entities.RightPerson oldRightPerson, DS.Entities.Person person, Security.Entities.Permission permission)
        {
            //Realiza las validaciones de autorizacion 
            //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
            //Se borra con sus herederos
            this.SecurityPersonRemove(person, oldRightPerson.Permission);
            //se da de alta el y sus herederos
            this.SecurityPersonAdd(person, permission);

            return new Condesus.EMS.Business.Security.Entities.RightPerson(permission, person);
        }
        public Security.Entities.RightJobTitle SecurityJobTitleModify(Security.Entities.RightJobTitle oldRightJobTitle, DS.Entities.JobTitle jobTitle, Security.Entities.Permission permission)
        {
            //Realiza las validaciones de autorizacion 
            //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
            //Se borra con sus herederos
            this.SecurityJobTitleRemove(jobTitle, oldRightJobTitle.Permission);
            //se da de alta el y sus herederos
            this.SecurityJobTitleAdd(jobTitle, permission);

            return new Condesus.EMS.Business.Security.Entities.RightJobTitle(permission, jobTitle);
        }
        #endregion

        #endregion

    }
}
