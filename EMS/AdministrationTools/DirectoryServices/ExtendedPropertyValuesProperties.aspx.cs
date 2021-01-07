using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;
using Condesus.EMS.WebUI.MasterControls;
using Condesus.EMS.Business.DS.Entities;
using Condesus.EMS.Business.EP.Entities;

namespace Condesus.EMS.WebUI.AdministrationTools.DirectoryServices
{
    public partial class ExtendedPropertyValuesProperties : BaseProperties
    {
        #region Internal Properties
            CompareValidator _CvExtendedPropertyClassification;
            CompareValidator _CvExtendedProperty;
            private String _LocalEntityName
            {
                get
                {
                    return base.NavigatorContainsTransferVar("EntityName") ? Convert.ToString(base.NavigatorGetTransferVar<Object>("EntityName")) : Convert.ToString(GetPKfromNavigator("EntityName"));
                }
            }
            private ExtendedPropertyValue _Entity = null;
            private ExtendedPropertyValue Entity
            {
                get
                {
                    try
                    {
                        Dictionary<String, Object> _params = new Dictionary<String, Object>();
                        //Por las dudas que venga por PK
                        _params = GetKeyValues(NavigatorGetPkEntityIdTransferVar<String>("PkCompost"));
                        //Ahora recorre el resto
                        //Se guarda todos los parametros que recibe... si es que no vienen por PK
                        foreach (KeyValuePair<String, Object> _item in NavigatorTransferenceCollection)
                        {
                            if (!_params.ContainsKey(_item.Key))
                            {
                                _params.Add(_item.Key, _item.Value);
                            }
                        }
                        if (_Entity == null)
                            _Entity = GetExtendedPropertyValueByObject(_LocalEntityName + "Values", _params);
                        
                        return _Entity;
                    }
                    catch { return null; }
                }

                set { _Entity = value; }
            }
            private RadComboBox _RdcExtendedPropertyClassification;
            private RadComboBox _RdcExtendedProperty;
        #endregion

        #region PageLoad & Init
            protected override void InitializeHandlers()
            {
                base.InitializeHandlers();

                //Le paso el Save a la MasterContentToolbar
                EventHandler _saveEventHandler = new EventHandler(btnSave_Click);
                FwMasterPage.ContentNavigatorToolbarFileActionAdd(_saveEventHandler, MasterFwContentToolbarAction.Save, true);
            }
            protected override void OnInit(EventArgs e)
            {
                base.OnInit(e);
                LoadTextLabels();
                AddCombos();
                AddValidators();
            }
            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);

                AddComboExtendedProperties();
                if (!Page.IsPostBack)
                {
                    //Inicializo el Form
                    if (Entity == null)
                    { Add(); }
                    else
                    { LoadData(); } //Edit.

                    //Form
                    base.SetContentTableRowsCss(tblContentForm);
                }
            }
            protected override void SetPagetitle()
            {
                //Busca el nombre de la pagina si es que se lo pasaron. Sino usa el nombre de la entidad.
                String _pageTitle = Convert.ToString(GetPKfromNavigator("PageTitle"));
                if (_pageTitle != "0")
                {
                    base.PageTitle = _pageTitle;
                }
                else
                {
                    if (Entity != null)
                    {
                        String _title = Entity.ExtendedProperty.LanguageOption.Name;
                        base.PageTitle = _title;
                    }
                    else
                    {
                        base.PageTitle = Resources.CommonListManage.OrganizationExtendedProperty;
                    }
                }
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }
        #endregion

        #region Private Methods
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonListManage.OrganizationExtendedProperty;
                lblExtendedProperty.Text = Resources.CommonListManage.ExtendedProperty;
                lblExtendedPropertyClassification.Text = Resources.CommonListManage.ExtendedPropertyClassification;
                lblValue.Text = Resources.CommonListManage.Value;
                rfv1.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
            }
            private void Add()
            {
                base.StatusBar.Clear();

                txtValue.Text = String.Empty;
                _RdcExtendedPropertyClassification.Enabled = true;
                _RdcExtendedProperty.Enabled = false;
            }
            private void LoadData()
            {
                txtValue.Text = Entity.Value;
                SetExtendedPropertyClassification();
                AddComboExtendedProperties();
                SetExtendedProperty();
                _RdcExtendedProperty.Enabled = false;
                _RdcExtendedPropertyClassification.Enabled = false;
            }
            private void AddCombos()
            {
                AddComboExtendedPropertyClassifications();
            }
            private void AddValidators()
            {
                ValidatorRequiredField(Common.ConstantsEntitiesName.PF.ExtendedPropertyClassifications, phExtendedPropertyClassificationValidator, ref _CvExtendedPropertyClassification, _RdcExtendedPropertyClassification, Resources.ConstantMessage.SelectAExtendedPropertyClassification);
            }
            private void AddComboExtendedPropertyClassifications()
            {
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                AddCombo(phExtendedPropertyClassification, ref _RdcExtendedPropertyClassification, Common.ConstantsEntitiesName.PF.ExtendedPropertyClassifications, String.Empty, _params, false, true, false, true, false);
                _RdcExtendedPropertyClassification.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(_RdcExtendedPropertyClassification_SelectedIndexChanged);
                FwMasterPage.RegisterContentAsyncPostBackTrigger(_RdcExtendedPropertyClassification, "SelectedIndexChanged");
            }
            private void AddComboExtendedProperties()
            {
                //Combo Extended Property
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                _params.Add("IdExtendedPropertyClassification", Convert.ToInt64(GetKeyValue(_RdcExtendedPropertyClassification.SelectedValue, "IdExtendedPropertyClassification")));
                AddCombo(phExtendedProperty, ref _RdcExtendedProperty, Common.ConstantsEntitiesName.PF.ExtendedProperties, String.Empty, _params, false, true, false, false, false);

                ValidatorRequiredField(Common.ConstantsEntitiesName.PF.ExtendedProperties, phExtendedPropertyValidator, ref _CvExtendedProperty, _RdcExtendedProperty, Resources.ConstantMessage.SelectAExtendedProperty);
            }
            private void SetExtendedPropertyClassification()
            {
                _RdcExtendedPropertyClassification.SelectedValue = "IdExtendedPropertyClassification=" + Entity.ExtendedProperty.ExtendedPropertyClassification.IdExtendedPropertyClassification.ToString();
            }
            private void SetExtendedProperty()
            {
                _RdcExtendedProperty.SelectedValue = "IdExtendedProperty=" + Entity.ExtendedProperty.IdExtendedProperty.ToString() + "& IdExtendedPropertyClassification=" + Entity.ExtendedProperty.ExtendedPropertyClassification.IdExtendedPropertyClassification.ToString();
            }
            private void ExtendedPropertyValueAdd(ref String pkValues, ExtendedProperty extendedProperty, String valueText)
            {
                Int64 _idIndicator;

                switch (_LocalEntityName)
                {
                    case Common.ConstantsEntitiesName.DS.OrganizationExtendedProperty:
                        Int64 _idOrganization = base.NavigatorContainsTransferVar("IdOrganization") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdOrganization")) : Convert.ToInt64(GetPKfromNavigator("IdOrganization"));
                        EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization).ExtendedPropertyValueAdd(extendedProperty, valueText);
                        pkValues = "IdOrganization=" + _idOrganization.ToString()
                            + "&EntityName=" + Common.ConstantsEntitiesName.DS.OrganizationExtendedProperty;
                        base.NavigatorAddTransferVar("IdOrganization", _idOrganization);
                        base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.DS.OrganizationExtendedProperty);
                        break;

                    case Common.ConstantsEntitiesName.PF.ProcessExtendedProperty:
                        Int64 _idProcess = base.NavigatorContainsTransferVar("IdProcess") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdProcess")) : Convert.ToInt64(GetPKfromNavigator("IdProcess"));
                        EMSLibrary.User.ProcessFramework.Map.Process(_idProcess).ExtendedPropertyValueAdd(extendedProperty, valueText);
                        pkValues = "IdProcess=" + _idProcess.ToString()
                            + "&EntityName=" + Common.ConstantsEntitiesName.PF.ProcessExtendedProperty;
                        base.NavigatorAddTransferVar("IdProcess", _idProcess);
                        base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.PF.ProcessExtendedProperty);
                        break;

                    case Common.ConstantsEntitiesName.KC.ResourceExtendedProperty:
                        Int64 _idResource = base.NavigatorContainsTransferVar("IdResource") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdResource")) : Convert.ToInt64(GetPKfromNavigator("IdResource"));
                        EMSLibrary.User.KnowledgeCollaboration.Map.Resource(_idResource).ExtendedPropertyValueAdd(extendedProperty, valueText);
                        pkValues = "IdResource=" + _idResource.ToString()
                            + "&EntityName=" + Common.ConstantsEntitiesName.KC.ResourceExtendedProperty;
                        base.NavigatorAddTransferVar("IdResource", _idResource);
                        base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.KC.ResourceExtendedProperty);
                        break;

                    case Common.ConstantsEntitiesName.PA.IndicatorExtendedProperty:
                        _idIndicator = base.NavigatorContainsTransferVar("IdIndicator") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdIndicator")) : Convert.ToInt64(GetPKfromNavigator("IdIndicator"));
                        EMSLibrary.User.PerformanceAssessments.Map.Indicator(_idIndicator).ExtendedPropertyValueAdd(extendedProperty, valueText);
                        pkValues = "IdIndicator=" + _idIndicator.ToString()
                            + "&EntityName=" + Common.ConstantsEntitiesName.PA.IndicatorExtendedProperty;
                        base.NavigatorAddTransferVar("IdIndicator", _idIndicator);
                        base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.PA.IndicatorExtendedProperty);
                        break;

                    case Common.ConstantsEntitiesName.PA.ParameterGroupExtendedProperty:
                        _idIndicator = base.NavigatorContainsTransferVar("IdIndicator") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdIndicator")) : Convert.ToInt64(GetPKfromNavigator("IdIndicator"));
                        Int64 _idParameterGroup = base.NavigatorContainsTransferVar("IdParameterGroup") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdParameterGroup")) : Convert.ToInt64(GetPKfromNavigator("IdParameterGroup"));
                        EMSLibrary.User.PerformanceAssessments.Map.Indicator(_idIndicator).ParameterGroup(_idParameterGroup).ExtendedPropertyValueAdd(extendedProperty, valueText);
                        pkValues = "IdIndicator=" + _idIndicator.ToString()
                            + "& IdParameterGroup=" + _idParameterGroup.ToString()
                            + "&EntityName=" + Common.ConstantsEntitiesName.PA.ParameterGroupExtendedProperty;
                        base.NavigatorAddTransferVar("IdIndicator", _idIndicator);
                        base.NavigatorAddTransferVar("IdParameterGroup", _idParameterGroup);
                        base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.PA.ParameterGroupExtendedProperty);
                        break;

                    case Common.ConstantsEntitiesName.PA.FormulaExtendedProperty:
                        Int64 _idFormula = base.NavigatorContainsTransferVar("IdFormula") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdFormula")) : Convert.ToInt64(GetPKfromNavigator("IdFormula"));
                        EMSLibrary.User.PerformanceAssessments.Configuration.Formula(_idFormula).ExtendedPropertyValueAdd(extendedProperty, valueText);
                        pkValues = "IdFormula=" + _idFormula.ToString()
                            + "&EntityName=" + Common.ConstantsEntitiesName.PA.FormulaExtendedProperty;
                        base.NavigatorAddTransferVar("IdFormula", _idFormula);
                        base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.PA.FormulaExtendedProperty);
                        break;

                    case Common.ConstantsEntitiesName.PA.CalculationExtendedProperty:
                        Int64 _idCalculation = base.NavigatorContainsTransferVar("IdCalculation") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdCalculation")) : Convert.ToInt64(GetPKfromNavigator("IdCalculation"));
                        EMSLibrary.User.PerformanceAssessments.Configuration.Calculation(_idCalculation).ExtendedPropertyValueAdd(extendedProperty, valueText);
                        pkValues = "IdCalculation=" + _idCalculation.ToString()
                            + "&EntityName=" + Common.ConstantsEntitiesName.PA.CalculationExtendedProperty;
                        base.NavigatorAddTransferVar("IdCalculation", _idCalculation);
                        base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.PA.CalculationExtendedProperty);
                        break;
                }
            }
            private void ExtendedPropertyValueModify(ref String pkValues, String valueText)
            {
                Int64 _idIndicator;

                switch (_LocalEntityName)
                {
                    case Common.ConstantsEntitiesName.DS.OrganizationExtendedProperty:
                        Int64 _idOrganization = base.NavigatorContainsTransferVar("IdOrganization") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdOrganization")) : Convert.ToInt64(GetPKfromNavigator("IdOrganization"));
                        EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization).ExtendedPropertyValueModify(Entity, valueText);
                        pkValues = "IdOrganization=" + _idOrganization.ToString()
                            + "&EntityName=" + Common.ConstantsEntitiesName.DS.OrganizationExtendedProperty;
                        base.NavigatorAddTransferVar("IdOrganization", _idOrganization);
                        base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.DS.OrganizationExtendedProperty);
                        break;

                    case Common.ConstantsEntitiesName.PF.ProcessExtendedProperty:
                        Int64 _idProcess = base.NavigatorContainsTransferVar("IdProcess") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdProcess")) : Convert.ToInt64(GetPKfromNavigator("IdProcess"));
                        EMSLibrary.User.ProcessFramework.Map.Process(_idProcess).ExtendedPropertyValueModify(Entity, valueText);
                        pkValues = "IdProcess=" + _idProcess.ToString()
                            + "&EntityName=" + Common.ConstantsEntitiesName.PF.ProcessExtendedProperty;
                        base.NavigatorAddTransferVar("IdProcess", _idProcess);
                        base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.PF.ProcessExtendedProperty);
                        break;

                    case Common.ConstantsEntitiesName.KC.ResourceExtendedProperty:
                        Int64 _idResource = base.NavigatorContainsTransferVar("IdResource") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdResource")) : Convert.ToInt64(GetPKfromNavigator("IdResource"));
                        EMSLibrary.User.KnowledgeCollaboration.Map.Resource(_idResource).ExtendedPropertyValueModify(Entity, valueText);
                        pkValues = "IdResource=" + _idResource.ToString()
                            + "&EntityName=" + Common.ConstantsEntitiesName.KC.ResourceExtendedProperty;
                        base.NavigatorAddTransferVar("IdResource", _idResource);
                        base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.KC.ResourceExtendedProperty);
                        break;

                    case Common.ConstantsEntitiesName.PA.IndicatorExtendedProperty:
                        _idIndicator = base.NavigatorContainsTransferVar("IdIndicator") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdIndicator")) : Convert.ToInt64(GetPKfromNavigator("IdIndicator"));
                        EMSLibrary.User.PerformanceAssessments.Map.Indicator(_idIndicator).ExtendedPropertyValueModify(Entity, valueText);
                        pkValues = "IdIndicator=" + _idIndicator.ToString()
                            + "&EntityName=" + Common.ConstantsEntitiesName.PA.IndicatorExtendedProperty;
                        base.NavigatorAddTransferVar("IdIndicator", _idIndicator);
                        base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.PA.IndicatorExtendedProperty);
                        break;

                    case Common.ConstantsEntitiesName.PA.ParameterGroupExtendedProperty:
                        _idIndicator = base.NavigatorContainsTransferVar("IdIndicator") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdIndicator")) : Convert.ToInt64(GetPKfromNavigator("IdIndicator"));
                        Int64 _idParameterGroup = base.NavigatorContainsTransferVar("IdParameterGroup") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdParameterGroup")) : Convert.ToInt64(GetPKfromNavigator("IdParameterGroup"));
                        EMSLibrary.User.PerformanceAssessments.Map.Indicator(_idIndicator).ParameterGroup(_idParameterGroup).ExtendedPropertyValueModify(Entity, valueText);
                        pkValues = "IdIndicator=" + _idIndicator.ToString()
                            + "& IdParameterGroup=" + _idParameterGroup.ToString()
                            + "&EntityName=" + Common.ConstantsEntitiesName.PA.ParameterGroupExtendedProperty;
                        base.NavigatorAddTransferVar("IdIndicator", _idIndicator);
                        base.NavigatorAddTransferVar("IdParameterGroup", _idParameterGroup);
                        base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.PA.ParameterGroupExtendedProperty);
                        break;

                    case Common.ConstantsEntitiesName.PA.FormulaExtendedProperty:
                        Int64 _idFormula = base.NavigatorContainsTransferVar("IdFormula") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdFormula")) : Convert.ToInt64(GetPKfromNavigator("IdFormula"));
                        EMSLibrary.User.PerformanceAssessments.Configuration.Formula(_idFormula).ExtendedPropertyValueModify(Entity, valueText);
                        pkValues = "IdFormula=" + _idFormula.ToString()
                            + "&EntityName=" + Common.ConstantsEntitiesName.PA.FormulaExtendedProperty;
                        base.NavigatorAddTransferVar("IdFormula", _idFormula);
                        base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.PA.FormulaExtendedProperty);
                        break;

                    case Common.ConstantsEntitiesName.PA.CalculationExtendedProperty:
                        Int64 _idCalculation = base.NavigatorContainsTransferVar("IdCalculation") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdCalculation")) : Convert.ToInt64(GetPKfromNavigator("IdCalculation"));
                        EMSLibrary.User.PerformanceAssessments.Configuration.Calculation(_idCalculation).ExtendedPropertyValueModify(Entity, valueText);
                        pkValues = "IdCalculation=" + _idCalculation.ToString()
                            + "&EntityName=" + Common.ConstantsEntitiesName.PA.CalculationExtendedProperty;
                        base.NavigatorAddTransferVar("IdCalculation", _idCalculation);
                        base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.PA.CalculationExtendedProperty);
                        break;
                }
            }
            private void SaveEntity(ref String pkValues, ExtendedProperty extendedProperty)
            {
                if (Entity == null)
                {
                    //Alta
                    ExtendedPropertyValueAdd(ref pkValues, extendedProperty, txtValue.Text);
                }
                else
                {
                    //Modificacion
                    ExtendedPropertyValueModify(ref pkValues, txtValue.Text);
                }
            }
        #endregion

        #region Page Events
            void _RdcExtendedPropertyClassification_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
            {
                AddComboExtendedProperties();
                _RdcExtendedProperty.Enabled = true;
            }
            protected void btnSave_Click(object sender, EventArgs e)
            {
                try
                {
                    String _pkValues = String.Empty;

                    //Obtiene el key necesario.
                    Int64 _idExtendedProperty = Convert.ToInt64(GetKeyValue(_RdcExtendedProperty.SelectedValue, "IdExtendedProperty"));
                    Int64 _idExtendedPropertyClass = Convert.ToInt64(GetKeyValue(_RdcExtendedProperty.SelectedValue, "IdExtendedPropertyClassification"));

                    ExtendedProperty _extendedProperty = EMSLibrary.User.ExtendedProperties.ExtendedPropertyClassification(_idExtendedPropertyClass).ExtendedProperty(_idExtendedProperty);

                    SaveEntity(ref _pkValues, _extendedProperty);

                    //le Agrego estos 2 parametros que se necesitas en forma fija!!!
                    _pkValues += "& IdExtendedProperty=" + _idExtendedProperty.ToString()
                            + "& IdExtendedPropertyClassification=" + _idExtendedPropertyClass.ToString();

                    base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);
                    base.NavigatorAddTransferVar("IdExtendedProperty", _idExtendedProperty);
                    base.NavigatorAddTransferVar("IdExtendedPropertyClassification", _idExtendedPropertyClass);
                    base.NavigatorAddTransferVar("EntityName", _LocalEntityName);
                    base.NavigatorAddTransferVar("EntityNameContextInfo", base.NavigatorGetTransferVar<String>("EntityNameContextInfo"));


                    String _entityPropertyName = String.Concat(_extendedProperty.LanguageOption.Name);
                    NavigatePropertyEntity(_LocalEntityName, _entityPropertyName, Condesus.WebUI.Navigation.NavigateMenuAction.Add);

                    base.StatusBar.ShowMessage(Resources.Common.SaveOK);
                }
                catch (Exception ex)
                {
                    base.StatusBar.ShowMessage(ex);
                }
            }
        #endregion
    }
}
