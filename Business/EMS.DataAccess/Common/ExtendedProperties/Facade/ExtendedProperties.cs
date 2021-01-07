using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess
{
    public class ExtendedProperties
    {
        # region Public Properties

        # region ExtendedProperties

        #region Read Functions
        public IEnumerable<DbDataRecord> ExtendedProperties_ReadAll(String idLanguage)
        {
            return new Entities.ExtendedProperties().ReadAll(idLanguage);
        }
        public IEnumerable<DbDataRecord> ExtendedProperties_ReadByClassification(Int64 idExtendedPropertyClassification, String idLanguage)
        {
            return new Entities.ExtendedProperties().ReadByClassification(idExtendedPropertyClassification, idLanguage);
        }
        public IEnumerable<DbDataRecord> ExtendedProperties_ReadById(Int64 idExtendedProperty, String idLanguage)
        {
            return new Entities.ExtendedProperties().ReadById(idExtendedProperty, idLanguage);
        }

        #endregion

        #region Write Functions

        public Int64 ExtendedProperties_Create(Int64 idExtendedPropertyClassification, String idLanguage, String name, String description, Int64 idLogPerson)
        {
            return new Entities.ExtendedProperties().Create(idExtendedPropertyClassification, idLanguage, name, description, idLogPerson);
        }
        public void ExtendedProperties_Delete(Int64 idExtendedProperty, Int64 idLogPerson)
        {
            new Entities.ExtendedProperties().Delete(idExtendedProperty, idLogPerson);
        }
        public void ExtendedProperties_Update(Int64 idExtendedProperty, Int64 idExtendedPropertyClassification, String idLanguage, String name, String description, Int64 idLogPerson)
        {
            new Entities.ExtendedProperties().Update(idExtendedProperty, idExtendedPropertyClassification, idLanguage, name, description, idLogPerson);
        }
        #endregion
        #endregion

        # region ExtendedProperties_LG

        #region Write Functions
        public void ExtendedProperties_LG_Create(Int64 idExtendedProperty, String idLanguage, String name, String description, Int64 idLogPerson)
        {
            new Entities.ExtendedProperties_LG().Create(idExtendedProperty, idLanguage, name, description, idLogPerson);
        }
        public void ExtendedProperties_LG_Delete(Int64 idExtendedProperty, String idLanguage, Int64 idLogPerson)
        {
            new Entities.ExtendedProperties_LG().Delete(idExtendedProperty, idLanguage, idLogPerson);
        }
        public void ExtendedProperties_LG_Update(Int64 idExtendedProperty, String idLanguage, String name, String description, Int64 idLogPerson)
        {
            new Entities.ExtendedProperties_LG().Update(idExtendedProperty, idLanguage, name, description, idLogPerson);
        }
        #endregion

        #region Read Functions
        public IEnumerable<DbDataRecord> ExtendedProperties_LG_ReadAll(Int64 idExtendedProperty)
        {
            return new Entities.ExtendedProperties_LG().ReadAll(idExtendedProperty);
        }
        public IEnumerable<DbDataRecord> ExtendedProperties_LG_ReadById(Int64 idExtendedProperty, String idLanguage)
        {
            return new Entities.ExtendedProperties_LG().ReadById(idExtendedProperty, idLanguage);
        }
        #endregion
        #endregion

        # region ExtendedPropertyClassifications

        #region Read Functions

        public IEnumerable<DbDataRecord> ExtendedPropertyClassifications_ReadAll(String idLanguage)
        {
            return new Entities.ExtendedPropertyClassifications().ReadAll(idLanguage);
        }
        public IEnumerable<DbDataRecord> ExtendedPropertyClassifications_ReadById(Int64 idExtendedPropertyClassification, String idLanguage)
        {
            return new Entities.ExtendedPropertyClassifications().ReadById(idExtendedPropertyClassification, idLanguage);
        }
        #endregion

        #region Write Functions
        public Int64 ExtendedPropertyClassifications_Create(String idLanguage, String name, String description, Int64 idLogPerson)
        {
            return new Entities.ExtendedPropertyClassifications().Create(idLanguage, name, description, idLogPerson);
        }
        public void ExtendedPropertyClassifications_Delete(Int64 idExtendedPropertyClassification, Int64 idLogPerson)
        {
            new Entities.ExtendedPropertyClassifications().Delete(idExtendedPropertyClassification, idLogPerson);
        }
        public void ExtendedPropertyClassifications_Update(Int64 idExtendedPropertyClassification, String idLanguage, String name, String description, Int64 idLogPerson)
        {
            new Entities.ExtendedPropertyClassifications().Update(idExtendedPropertyClassification, idLanguage, name, description, idLogPerson);
        }
        #endregion
        #endregion

        # region ExtendedPropertyClassifications_LG

        #region Write Functions

        public void ExtendedPropertyClassifications_LG_Create(Int64 idExtendedPropertyClassification, String idLanguage, String name, String description, Int64 idLogPerson)
        {
            new Entities.ExtendedPropertyClassifications_LG().Create(idExtendedPropertyClassification, idLanguage, name, description, idLogPerson);
        }
        public void ExtendedPropertyClassifications_LG_Delete(Int64 idExtendedPropertyClassification, String idLanguage, Int64 idLogPerson)
        {
            new Entities.ExtendedPropertyClassifications_LG().Delete(idExtendedPropertyClassification, idLanguage, idLogPerson);
        }
        public void ExtendedPropertyClassifications_LG_Update(Int64 idExtendedPropertyClassification, String idLanguage, String name, String description, Int64 idLogPerson)
        {
            new Entities.ExtendedPropertyClassifications_LG().Update(idExtendedPropertyClassification, idLanguage, name, description, idLogPerson);
        }
        #endregion

        #region Read Functions
        public IEnumerable<DbDataRecord> ExtendedPropertyClassifications_LG_ReadAll(Int64 idExtendedPropertyClassification)
        {
            return new Entities.ExtendedPropertyClassifications_LG().ReadAll(idExtendedPropertyClassification);
        }
        public IEnumerable<DbDataRecord> ExtendedPropertyClassifications_LG_ReadById(Int64 idExtendedPropertyClassification, String idLanguage)
        {
            return new Entities.ExtendedPropertyClassifications_LG().ReadById(idExtendedPropertyClassification, idLanguage);
        }
        #endregion
        #endregion

        # region ProcessExtendedProperties

        #region Read Function
        public IEnumerable<DbDataRecord> ProcessExtendedProperties_ReadAll(Int64 idProcess)
            {
                return new Entities.ProcessExtendedProperties().ReadAll(idProcess);
            }
        public IEnumerable<DbDataRecord> ProcessExtendedProperties_ReadById(Int64 idProcess, Int64 idExtendedProperty)
            {
                return new Entities.ProcessExtendedProperties().ReadById(idProcess, idExtendedProperty);
            }
        #endregion


        #region Write Functions
        public void ProcessExtendedProperties_Create(Int64 idExtendedProperty, Int64 idProcess, String value)
            {
                new Entities.ProcessExtendedProperties().Create(idExtendedProperty, idProcess, value);
            }
        public void ProcessExtendedProperties_Delete(Int64 idExtendedProperty, Int64 idProcess)
            {
                new Entities.ProcessExtendedProperties().Delete(idExtendedProperty, idProcess);            
            }
        public void ProcessExtendedProperties_Delete(Int64 idProcess)
        {
            new Entities.ProcessExtendedProperties().Delete(idProcess);
        }
        public void ProcessExtendedProperties_Update(Int64 idExtendedProperty, Int64 idProcess, String value)
            {
                new Entities.ProcessExtendedProperties().Update(idExtendedProperty, idProcess, value);
            }
        #endregion
            #endregion

        # region IndicatorExtendedProperties

        #region Read Function
        public IEnumerable<DbDataRecord> IndicatorExtendedProperties_ReadAll(Int64 idIndicator)
        {
            return new Entities.IndicatorExtendedProperties().ReadAll(idIndicator);
        }
        public IEnumerable<DbDataRecord> IndicatorExtendedProperties_ReadById(Int64 idIndicator, Int64 idExtendedProperty)
        {
            return new Entities.IndicatorExtendedProperties().ReadById(idIndicator, idExtendedProperty);
        }
        #endregion


        #region Write Functions
        public void IndicatorExtendedProperties_Create(Int64 idExtendedProperty, Int64 idIndicator, String value)
        {
            new Entities.IndicatorExtendedProperties().Create(idExtendedProperty, idIndicator, value);
        }
        public void IndicatorExtendedProperties_Delete(Int64 idExtendedProperty, Int64 idIndicator)
        {
            new Entities.IndicatorExtendedProperties().Delete(idExtendedProperty, idIndicator);
        }
        public void IndicatorExtendedProperties_Delete(Int64 idIndicator)
        {
            new Entities.IndicatorExtendedProperties().Delete(idIndicator);
        }
        public void IndicatorExtendedProperties_Update(Int64 idExtendedProperty, Int64 idIndicator, String value)
        {
            new Entities.IndicatorExtendedProperties().Update(idExtendedProperty, idIndicator, value);
        }
        #endregion
        #endregion

        # region ResourceExtendedProperties

        #region Read Function
        public IEnumerable<DbDataRecord> ResourceExtendedProperties_ReadAll(Int64 idResource)
        {
            return new Entities.ResourceExtendedProperties().ReadAll(idResource);
        }
        public IEnumerable<DbDataRecord> ResourceExtendedProperties_ReadById(Int64 idResource, Int64 idExtendedProperty)
        {
            return new Entities.ResourceExtendedProperties().ReadById(idResource, idExtendedProperty);
        }
        #endregion


        #region Write Functions
        public void ResourceExtendedProperties_Create(Int64 idExtendedProperty, Int64 idResource, String value)
        {
            new Entities.ResourceExtendedProperties().Create(idExtendedProperty, idResource, value);
        }
        public void ResourceExtendedProperties_Delete(Int64 idExtendedProperty, Int64 idResource)
        {
            new Entities.ResourceExtendedProperties().Delete(idExtendedProperty, idResource);
        }
        public void ResourceExtendedProperties_Delete(Int64 idResource)
        {
            new Entities.ResourceExtendedProperties().Delete(idResource);
        }
        public void ResourceExtendedProperties_Update(Int64 idExtendedProperty, Int64 idResource, String value)
        {
            new Entities.ResourceExtendedProperties().Update(idExtendedProperty, idResource, value);
        }
        #endregion
        #endregion

        # region OrganizationExtendedProperties

        #region Read Function
        public IEnumerable<DbDataRecord> OrganizationExtendedProperties_ReadAll(Int64 idOrganization)
        {
            return new Entities.OrganizationExtendedProperties().ReadAll(idOrganization);
        }
        public IEnumerable<DbDataRecord> OrganizationExtendedProperties_ReadById(Int64 idOrganization, Int64 idExtendedProperty)
        {
            return new Entities.OrganizationExtendedProperties().ReadById(idOrganization, idExtendedProperty);
        }
        #endregion


        #region Write Functions
        public void OrganizationExtendedProperties_Create(Int64 idExtendedProperty, Int64 idOrganization, String value)
        {
            new Entities.OrganizationExtendedProperties().Create(idExtendedProperty, idOrganization, value);
        }
        public void OrganizationExtendedProperties_Delete(Int64 idExtendedProperty, Int64 idOrganization)
        {
            new Entities.OrganizationExtendedProperties().Delete(idExtendedProperty, idOrganization);
        }
        public void OrganizationExtendedProperties_Delete(Int64 idOrganization)
        {
            new Entities.OrganizationExtendedProperties().Delete(idOrganization);
        }
        public void OrganizationExtendedProperties_Update(Int64 idExtendedProperty, Int64 idOrganization, String value)
        {
            new Entities.OrganizationExtendedProperties().Update(idExtendedProperty, idOrganization, value);
        }
        #endregion
        #endregion

        # region FormulaExtendedProperties

        #region Read Function
        public IEnumerable<DbDataRecord> FormulaExtendedProperties_ReadAll(Int64 idFormula)
        {
            return new Entities.FormulaExtendedProperties().ReadAll(idFormula);
        }
        public IEnumerable<DbDataRecord> FormulaExtendedProperties_ReadById(Int64 idFormula, Int64 idExtendedProperty)
        {
            return new Entities.FormulaExtendedProperties().ReadById(idFormula, idExtendedProperty);
        }
        #endregion


        #region Write Functions
        public void FormulaExtendedProperties_Create(Int64 idExtendedProperty, Int64 idFormula, String value)
        {
            new Entities.FormulaExtendedProperties().Create(idExtendedProperty, idFormula, value);
        }
        public void FormulaExtendedProperties_Delete(Int64 idExtendedProperty, Int64 idFormula)
        {
            new Entities.FormulaExtendedProperties().Delete(idExtendedProperty, idFormula);
        }
        public void FormulaExtendedProperties_Delete(Int64 idFormula)
        {
            new Entities.FormulaExtendedProperties().Delete(idFormula);
        }
        public void FormulaExtendedProperties_Update(Int64 idExtendedProperty, Int64 idFormula, String value)
        {
            new Entities.FormulaExtendedProperties().Update(idExtendedProperty, idFormula, value);
        }
        #endregion
        #endregion

        # region CalculateExtendedProperties

        #region Read Function
        public IEnumerable<DbDataRecord> CalculateExtendedProperties_ReadAll(Int64 idCalculate)
        {
            return new Entities.CalculateExtendedProperties().ReadAll(idCalculate);
        }
        public IEnumerable<DbDataRecord> CalculateExtendedProperties_ReadById(Int64 idCalculate, Int64 idExtendedProperty)
        {
            return new Entities.CalculateExtendedProperties().ReadById(idCalculate, idExtendedProperty);
        }
        #endregion


        #region Write Functions
        public void CalculateExtendedProperties_Create(Int64 idExtendedProperty, Int64 idCalculate, String value)
        {
            new Entities.CalculateExtendedProperties().Create(idExtendedProperty, idCalculate, value);
        }
        public void CalculateExtendedProperties_Delete(Int64 idExtendedProperty, Int64 idCalculate)
        {
            new Entities.CalculateExtendedProperties().Delete(idExtendedProperty, idCalculate);
        }
        public void CalculateExtendedProperties_Delete(Int64 idCalculate)
        {
            new Entities.CalculateExtendedProperties().Delete(idCalculate);
        }
        public void CalculateExtendedProperties_Update(Int64 idExtendedProperty, Int64 idCalculate, String value)
        {
            new Entities.CalculateExtendedProperties().Update(idExtendedProperty, idCalculate, value);
        }
        #endregion
        #endregion

        # region ParameterGroupExtendedProperties

        #region Read Function
        public IEnumerable<DbDataRecord> ParameterGroupExtendedProperties_ReadAll(Int64 idParameterGroup)
        {
            return new Entities.ParameterGroupExtendedProperties().ReadAll(idParameterGroup);
        }
        public IEnumerable<DbDataRecord> ParameterGroupExtendedProperties_ReadById(Int64 idParameterGroup, Int64 idExtendedProperty)
        {
            return new Entities.ParameterGroupExtendedProperties().ReadById(idParameterGroup, idExtendedProperty);
        }
        #endregion


        #region Write Functions
        public void ParameterGroupExtendedProperties_Create(Int64 idExtendedProperty, Int64 idParameterGroup, String value)
        {
            new Entities.ParameterGroupExtendedProperties().Create(idExtendedProperty, idParameterGroup, value);
        }
        public void ParameterGroupExtendedProperties_Delete(Int64 idExtendedProperty, Int64 idParameterGroup)
        {
            new Entities.ParameterGroupExtendedProperties().Delete(idExtendedProperty, idParameterGroup);
        }
        public void ParameterGroupExtendedProperties_Delete(Int64 idParameterGroup)
        {
            new Entities.ParameterGroupExtendedProperties().Delete(idParameterGroup);
        }
        public void ParameterGroupExtendedProperties_Update(Int64 idExtendedProperty, Int64 idParameterGroup, String value)
        {
            new Entities.ParameterGroupExtendedProperties().Update(idExtendedProperty, idParameterGroup, value);
        }
        #endregion
        #endregion
        #endregion
            /// <summary>
        /// Constructor del acceso a datos del mapa de procesos
        /// </summary>
        public ExtendedProperties()
        { 
        }
    }
}
