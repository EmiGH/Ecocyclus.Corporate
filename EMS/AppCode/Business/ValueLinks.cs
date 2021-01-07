using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Condesus.EMS.WebUI.Business
{
    public partial class Entities : Base
    {
        private String ValueLink(String idEntity, String entityName, String entityNameGrid, String entityNameContextInfo, String entityNameContextElement)
        {
            String _valueLink = idEntity
                  + "&EntityName=" + entityName
                  + "&EntityNameGrid=" + entityNameGrid
                  + "&EntityNameContextInfo=" + entityNameContextInfo
                  + "&EntityNameContextElement=" + entityNameContextElement;
            return _valueLink;
        }

        #region Public Methods (Aca estan los metodos que via Reflection nos permite evitar el SWITCH)

        #region Directory Service

        public String Applicability_ValueLink(Dictionary<String, Object> param)
        {
            String _valueLink = ValueLink("&Applicability=" + param["Applicability"].ToString(),
                Common.ConstantsEntitiesName.DS.Applicability,
                Common.ConstantsEntitiesName.DS.Applicabilities,
                String.Empty,
                String.Empty);

            return _valueLink;
        }

        public String ContactMessengerApplication_ValueLink(Dictionary<String, Object> param)
        {
            String _valueLink = ValueLink("&Application=" + param["Application"].ToString(),
                Common.ConstantsEntitiesName.DS.ContactMessengerApplication,
                Common.ConstantsEntitiesName.DS.ContactMessengerApplications,
                String.Empty,
                String.Empty);

            return _valueLink;
        }

        public String ContactMessengerProvider_ValueLink(Dictionary<String, Object> param)
        {
            String _valueLink = ValueLink("&Provider=" + param["Provider"].ToString(),
                Common.ConstantsEntitiesName.DS.ContactMessengerProvider,
                Common.ConstantsEntitiesName.DS.ContactMessengerProviders,
                String.Empty,
                String.Empty);

            return _valueLink;
        }

        public String ContactType_ValueLink(Dictionary<String, Object> param)
        {
            String _valueLink = ValueLink("&IdContactType=" + param["IdContactType"].ToString(),
                           Common.ConstantsEntitiesName.DS.ContactType,
                           Common.ConstantsEntitiesName.DS.ContactTypes,
                           String.Empty,
                           String.Empty);

            return _valueLink;
        }

        public String FunctionalArea_ValueLink(Dictionary<String, Object> param)
        {
            String _valueLink = ValueLink("&IdOrganization=" + param["IdOrganization"].ToString() + 
                            "&IdFunctionalArea=" + param["IdFunctionalArea"].ToString(),
                            Common.ConstantsEntitiesName.DS.FunctionalArea,
                            Common.ConstantsEntitiesName.DS.FunctionalAreas,
                            Common.ConstantsEntitiesName.DS.Organization,
                            Common.ConstantsEntitiesName.DS.Organization);

            return _valueLink;
        }

        public String FunctionalPosition_ValueLink(Dictionary<String, Object> param)
        {
            String _valueLink = ValueLink("&IdOrganization=" + param["IdOrganization"].ToString() +
                "&IdPosition=" + param["IdPosition"].ToString() +
                "&IdFunctionalArea=" + param["IdFunctionalArea"].ToString(),
                Common.ConstantsEntitiesName.DS.FunctionalPosition,
                Common.ConstantsEntitiesName.DS.FunctionalPositions,
                Common.ConstantsEntitiesName.DS.Organization,
                Common.ConstantsEntitiesName.DS.Organization);

            return _valueLink;
        }

        public String GeographicArea_ValueLink(Dictionary<String, Object> param)
        {
            String _valueLink = ValueLink("&IdGeographicArea=" + param["IdGeographicArea"].ToString(),
                            Common.ConstantsEntitiesName.DS.GeographicArea,
                            Common.ConstantsEntitiesName.DS.GeographicAreas,
                            Common.ConstantsEntitiesName.DS.Organization,
                            Common.ConstantsEntitiesName.DS.Organization);

            return _valueLink;
        }

        public String GeographicFunctionalArea_ValueLink(Dictionary<String, Object> param)
        {
            String _valueLink = ValueLink("&IdOrganization=" + param["IdOrganization"].ToString() +
                            "&IdGeographicArea=" + param["IdGeographicArea"].ToString() +
                            "&IdFunctionalArea=" + param["IdFunctionalArea"].ToString(),
                            Common.ConstantsEntitiesName.DS.GeographicFunctionalArea,
                            Common.ConstantsEntitiesName.DS.GeographicFunctionalAreas,
                            Common.ConstantsEntitiesName.DS.Organization,
                            Common.ConstantsEntitiesName.DS.Organization);

            return _valueLink;
        }

        public String JobTitle_ValueLink(Dictionary<String, Object> param)
        {
            String _valueLink = ValueLink("&IdOrganization=" + param["IdOrganization"].ToString() +
                "&IdGeographicArea=" + param["IdGeographicArea"].ToString() +
                "&IdPosition=" + param["IdPosition"].ToString() +
                "&IdFunctionalArea=" + param["IdFunctionalArea"].ToString(),
                Common.ConstantsEntitiesName.DS.JobTitle,
                Common.ConstantsEntitiesName.DS.JobTitles,
                Common.ConstantsEntitiesName.DS.JobTitle,
                Common.ConstantsEntitiesName.DS.Organization);

            return _valueLink;
        }

        public String Organization_ValueLink(Dictionary<String, Object> param)
        {
            String _valueLink = ValueLink("&IdOrganization=" + param["IdOrganization"].ToString(),
                            Common.ConstantsEntitiesName.DS.Organization,
                            Common.ConstantsEntitiesName.DS.Organizations,
                            Common.ConstantsEntitiesName.DS.Organization,
                            Common.ConstantsEntitiesName.DS.Organization);

            return _valueLink;
        }

        public String OrganizationClassification_ValueLink(Dictionary<String, Object> param)
        {
            String _valueLink = ValueLink("&IdOrganizationClassification=" + param["IdOrganizationClassification"].ToString(),
                            Common.ConstantsEntitiesName.DS.OrganizationClassification,
                            Common.ConstantsEntitiesName.DS.OrganizationClassifications,
                            Common.ConstantsEntitiesName.DS.OrganizationClassification,
                            String.Empty);

            return _valueLink;
        }

        public String OrganizationRelationshipType_ValueLink(Dictionary<String, Object> param)
        {
            String _valueLink = ValueLink("&IdOrganizationRelationshipType=" + param["IdOrganizationRelationshipType"].ToString(),
               Common.ConstantsEntitiesName.DS.OrganizationRelationshipType,
               Common.ConstantsEntitiesName.DS.OrganizationRelationshipTypes,
               String.Empty,
               String.Empty);

            return _valueLink;
        }

        public String Person_ValueLink(Dictionary<String, Object> param)
        {
            String _valueLink = ValueLink("&IdOrganization=" + param["IdOrganization"].ToString() +
                "&IdPerson=" + param["IdPerson"].ToString(),
                Common.ConstantsEntitiesName.DS.Person,
                Common.ConstantsEntitiesName.DS.People,
                Common.ConstantsEntitiesName.DS.Person,
                Common.ConstantsEntitiesName.DS.Person);

            return _valueLink;
        }

        public String Position_ValueLink(Dictionary<String, Object> param)
        {
            String _valueLink = ValueLink("&IdOrganization=" + param["IdOrganization"].ToString() + 
                            "&IdPosition=" + param["IdPosition"].ToString(),
                            Common.ConstantsEntitiesName.DS.Position,
                            Common.ConstantsEntitiesName.DS.Positions,
                            Common.ConstantsEntitiesName.DS.Organization,
                            Common.ConstantsEntitiesName.DS.Organization);

            return _valueLink;
        }

        public String SalutationType_ValueLink(Dictionary<String, Object> param)
        {
            String _valueLink = ValueLink("&IdSalutationType=" + param["IdSalutationType"].ToString(),
                           Common.ConstantsEntitiesName.DS.SalutationType,
                           Common.ConstantsEntitiesName.DS.SalutationTypes,
                           String.Empty,
                           String.Empty);

            return _valueLink;
        }

        public String Sector_ValueLink(Dictionary<String, Object> param)
        {
            String _valueLink = ValueLink("&IdOrganization=" + param["IdOrganization"].ToString() +
                                "&IdFacility="+ param["IdFacility"].ToString() +
                                "&IdSector="+ param["IdParentSector"].ToString(), //Utiliza el parentSector, para luego mostrar el sector.
                            Common.ConstantsEntitiesName.DS.Sector,
                            Common.ConstantsEntitiesName.DS.Sectors,
                            Common.ConstantsEntitiesName.DS.Facility,
                            String.Empty);

            return _valueLink;
        }

        public String FacilityType_ValueLink(Dictionary<String, Object> param)
        {
            String _valueLink = ValueLink("&IdFacilityType=" + param["IdFacilityType"].ToString(),
                            Common.ConstantsEntitiesName.DS.FacilityType,
                            Common.ConstantsEntitiesName.DS.FacilityTypes,
                            String.Empty,
                            String.Empty);

            return _valueLink;
        }

        #endregion

        #region Improvement Actions

        public String ProjectClassification_ValueLink(Dictionary<String, Object> param)
        {
            String _valueLink = ValueLink("&IdProjectClassification=" + param["IdProjectClassification"].ToString(),
                Common.ConstantsEntitiesName.IA.ProjectClassification,
                Common.ConstantsEntitiesName.IA.ProjectClassifications,
                String.Empty,
                String.Empty);

            return _valueLink;
        }

        #endregion

        #region Knowledge Collaboration

        public String KCResource_ValueLink(Dictionary<String, Object> param)
        {
            String _valueLink = ValueLink("&IdResource=" + param["IdResource"].ToString(),
                Common.ConstantsEntitiesName.KC.Resource,
                Common.ConstantsEntitiesName.KC.Resources,
                Common.ConstantsEntitiesName.KC.Resource,
                String.Empty);

            return _valueLink;
        }

        public String ResourceClassification_ValueLink(Dictionary<String, Object> param)
        {
            String _valueLink = ValueLink("&IdResourceClassification=" + param["IdResourceClassification"].ToString(),
                Common.ConstantsEntitiesName.KC.ResourceClassification,
                Common.ConstantsEntitiesName.KC.ResourceClassifications,
                Common.ConstantsEntitiesName.KC.ResourceClassification,
                String.Empty);

            return _valueLink;
        }

        public String ResourceType_ValueLink(Dictionary<String, Object> param)
        {
            String _valueLink = ValueLink("&IdResourceType=" + param["IdResourceType"].ToString(),
                Common.ConstantsEntitiesName.KC.ResourceType,
                Common.ConstantsEntitiesName.KC.ResourceTypes,
                String.Empty,
                String.Empty);

            return _valueLink;
        }

        #endregion

        #region Performance Assessment
            public String Calculation_ValueLink(Dictionary<String, Object> param)
            {
                String _valueLink = ValueLink("&IdCalculation=" + param["IdCalculation"].ToString(),
                    Common.ConstantsEntitiesName.PA.Calculation,
                    Common.ConstantsEntitiesName.PA.Calculations,
                    Common.ConstantsEntitiesName.PA.Calculation,
                    String.Empty);

                return _valueLink;
            }
            public String CalculationScenarioType_ValueLink(Dictionary<String, Object> param)
            {
                String _valueLink = ValueLink("&IdCalculation=" + param["IdCalculation"].ToString() + "&IdScenarioType=" + param["IdScenarioType"].ToString(),
                    Common.ConstantsEntitiesName.PA.CalculationScenarioType,
                    Common.ConstantsEntitiesName.PA.CalculationScenarioTypes,
                    String.Empty,
                    String.Empty);

                return _valueLink;
            }
            public String Formula_ValueLink(Dictionary<String, Object> param)
            {
                String _valueLink = ValueLink("&IdFormula=" + param["IdFormula"].ToString(),
                    Common.ConstantsEntitiesName.PA.Formula,
                    Common.ConstantsEntitiesName.PA.Formulas,
                    String.Empty,
                    String.Empty);

                return _valueLink;
            }
            public String Indicator_ValueLink(Dictionary<String, Object> param)
            {
                String _valueLink = ValueLink("&IdIndicator=" + param["IdIndicator"].ToString(),
                    Common.ConstantsEntitiesName.PA.Indicator,
                    Common.ConstantsEntitiesName.PA.Indicators,
                    Common.ConstantsEntitiesName.PA.Indicator,
                    Common.ConstantsEntitiesName.PA.Indicator);

                return _valueLink;
            }
            public String IndicatorClassification_ValueLink(Dictionary<String, Object> param)
            {
                String _valueLink = ValueLink("&IdIndicatorClassification=" + param["IdIndicatorClassification"].ToString(),
                    Common.ConstantsEntitiesName.PA.IndicatorClassification,
                    Common.ConstantsEntitiesName.PA.IndicatorClassifications,
                    Common.ConstantsEntitiesName.PA.IndicatorClassification,
                    String.Empty);

                return _valueLink;
            }
            public String Measurement_ValueLink(Dictionary<String, Object> param)
            {
                String _valueLink = ValueLink("&IdMeasurement=" + param["IdMeasurement"].ToString(),
                    Common.ConstantsEntitiesName.PA.Measurement,
                    Common.ConstantsEntitiesName.PA.Measurements,
                    String.Empty,
                    String.Empty);

                return _valueLink;
            }
            public String MeasurementDevice_ValueLink(Dictionary<String, Object> param)
            {
                String _valueLink = ValueLink("&IdMeasurementDevice=" + param["IdMeasurementDevice"].ToString(),
                    Common.ConstantsEntitiesName.PA.MeasurementDevice,
                    Common.ConstantsEntitiesName.PA.MeasurementDevices,
                    String.Empty,
                    String.Empty);

                return _valueLink;
            }               
            public String MeasurementDeviceType_ValueLink(Dictionary<String, Object> param)
            {
                String _valueLink = ValueLink("&IdMeasurementDeviceType=" + param["IdMeasurementDeviceType"].ToString(),
                    Common.ConstantsEntitiesName.PA.MeasurementDeviceType,
                    Common.ConstantsEntitiesName.PA.MeasurementDeviceTypes,
                    String.Empty,
                    String.Empty);

                return _valueLink;
            }               
            public String MeasurementUnit_ValueLink(Dictionary<String, Object> param)
            {
                String _valueLink = ValueLink("&IdMeasurementUnit=" + param["IdMeasurementUnit"].ToString()
                    + "&IdMagnitud=" + param["IdMagnitud"].ToString(),
                    Common.ConstantsEntitiesName.PA.MeasurementUnit,
                    Common.ConstantsEntitiesName.PA.MeasurementUnits,
                    String.Empty,
                    String.Empty);

                return _valueLink;
            }
            public String Parameter_ValueLink(Dictionary<String, Object> param)
            {
                String _valueLink = ValueLink("&IdIndicator=" + param["IdIndicator"].ToString() +
                    "&IdParameter=" + param["IdParameter"].ToString() +
                    "&IdParameterGroup=" + param["IdParameterGroup"].ToString(),
                    Common.ConstantsEntitiesName.PA.Parameter,
                    Common.ConstantsEntitiesName.PA.Parameters,
                    Common.ConstantsEntitiesName.PA.Parameter,
                    Common.ConstantsEntitiesName.PA.Indicator);

                return _valueLink;
            }
            public String ParameterGroup_ValueLink(Dictionary<String, Object> param)
            {
                String _valueLink = ValueLink("&IdIndicator=" + param["IdIndicator"].ToString() +
                    "&IdParameterGroup=" + param["IdParameterGroup"].ToString(),
                    Common.ConstantsEntitiesName.PA.ParameterGroup,
                    Common.ConstantsEntitiesName.PA.ParameterGroups,
                    Common.ConstantsEntitiesName.PA.ParameterGroup,
                    Common.ConstantsEntitiesName.PA.Indicator);

                return _valueLink;
            }
            public String ConstantClassification_ValueLink(Dictionary<String, Object> param)
            {
                String _valueLink = ValueLink("&IdConstantClassification=" + param["IdConstantClassification"].ToString(),
                    Common.ConstantsEntitiesName.PA.ConstantClassification,
                    Common.ConstantsEntitiesName.PA.ConstantClassifications,
                    String.Empty,
                    String.Empty);

                return _valueLink;
            }
            public String Quality_ValueLink(Dictionary<String, Object> param)
            {
                String _valueLink = ValueLink("&IdQuality=" + param["IdQuality"].ToString(),
                    Common.ConstantsEntitiesName.PA.Quality,
                    Common.ConstantsEntitiesName.PA.Qualities,
                    String.Empty,
                    String.Empty);

                return _valueLink;
            }
            public String Methodology_ValueLink(Dictionary<String, Object> param)
            {
                String _valueLink = ValueLink("&IdMethodology=" + param["IdMethodology"].ToString(),
                    Common.ConstantsEntitiesName.PA.Methodology,
                    Common.ConstantsEntitiesName.PA.Methodologies,
                    String.Empty,
                    String.Empty);

                return _valueLink;
            }        
        #endregion

        #region Process Framework

        public String ExtendedPropertyClassification_ValueLink(Dictionary<String, Object> param)
        {
            String _valueLink = ValueLink("&IdExtendedPropertyClassification=" + param["IdExtendedPropertyClassification"].ToString(),
                Common.ConstantsEntitiesName.PF.ExtendedPropertyClassification,
                Common.ConstantsEntitiesName.PF.ExtendedPropertyClassifications,
                String.Empty,
                String.Empty);

            return _valueLink;
        }

        public String ParticipationType_ValueLink(Dictionary<String, Object> param)
        {
            String _valueLink = ValueLink("&IdParticipationType=" + param["IdParticipationType"].ToString(),
                Common.ConstantsEntitiesName.PF.ParticipationType,
                Common.ConstantsEntitiesName.PF.ParticipationTypes,
                String.Empty,
                String.Empty);

            return _valueLink;
        }

        public String ProcessClassification_ValueLink(Dictionary<String, Object> param)
        {
            String _valueLink = ValueLink("&IdProcessClassification=" + param["IdProcessClassification"].ToString(),
                Common.ConstantsEntitiesName.PF.ProcessClassification,
                Common.ConstantsEntitiesName.PF.ProcessClassifications,
                Common.ConstantsEntitiesName.PF.ProcessClassification,
                String.Empty);

            return _valueLink;
        }

        public String ProcessGroupProcess_ValueLink(Dictionary<String, Object> param)
        {
            String _valueLink = ValueLink("&IdProcess=" + param["IdProcess"].ToString(),
                Common.ConstantsEntitiesName.PF.ProcessGroupProcess,
                Common.ConstantsEntitiesName.PF.ProcessGroupProcesses,
                Common.ConstantsEntitiesName.PF.ProcessGroupProcess,
                Common.ConstantsEntitiesName.PF.ProcessGroupProcess);

            return _valueLink;
        }
        public String ProcessTaskMeasurement_ValueLink(Dictionary<String, Object> param)
        {
            String _valueLink = ValueLink("&IdProcess=" + param["IdProcess"].ToString(),
                Common.ConstantsEntitiesName.PF.ProcessTaskMeasurement,
                Common.ConstantsEntitiesName.PF.ProcessTaskMeasurements,
                Common.ConstantsEntitiesName.PF.ProcessTaskMeasurement,
                Common.ConstantsEntitiesName.PF.ProcessGroupProcess);

            return _valueLink;
        }

        public String TimeUnit_ValueLink(Dictionary<String, Object> param)
        {
            String _valueLink = ValueLink("&IdTimeUnit=" + param["IdTimeUnit"].ToString(),
                Common.ConstantsEntitiesName.PF.TimeUnit,
                Common.ConstantsEntitiesName.PF.TimeUnits,
                String.Empty,
                String.Empty);

            return _valueLink;
        }

        #endregion

        #region Risk Management

        public String RiskClassification_ValueLink(Dictionary<String, Object> param)
        {
            String _valueLink = ValueLink("&IdRiskClassification=" + param["IdRiskClassification"].ToString(),
                Common.ConstantsEntitiesName.RM.RiskClassification,
                Common.ConstantsEntitiesName.RM.RiskClassifications,
                String.Empty,
                String.Empty);

            return _valueLink;
        }

        #endregion

        #region Security

        public String RoleType_ValueLink(Dictionary<String, Object> param)
        {
            String _valueLink = ValueLink("&IdRoleType=" + param["IdRoleType"].ToString(),
                Common.ConstantsEntitiesName.SS.RoleType,
                Common.ConstantsEntitiesName.SS.RoleTypes,
                String.Empty,
                String.Empty);

            return _valueLink;
        }

        #endregion

        #endregion
    }
}
