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
using System.Linq;

using Condesus.EMS.WebUI.MasterControls;
using Condesus.EMS.Business.PA.Entities;

namespace Condesus.EMS.WebUI.ManagementTools.PerformanceAssessment
{
    public partial class CalculationEstimatesProperties : BaseProperties
    {
        #region Internal Properties

        private Int64 _ParentEntityId
        {
            get
            {
                return base.NavigatorContainsTransferVar("IdCalculation") ? base.NavigatorGetTransferVar<Int64>("IdCalculation") : Convert.ToInt64(GetPKfromNavigator("IdCalculation"));
            }
        }
        private Calculation _ParentEntity = null;
        private Calculation ParentEntity
        {
            get
            {
                try
                {
                    if (_ParentEntity == null)
                    {
                        _ParentEntity = EMSLibrary.User.PerformanceAssessments.Configuration.Calculation(Convert.ToInt64(_ParentEntityId));
                    }
                    return _ParentEntity;
                }
                catch
                {
                    return null;
                }
            }
            set { _ParentEntity = value; }
        }
        private Int64 _IdEntity
        {
            get
            {
                return base.NavigatorContainsTransferVar("IdEstimated") ? base.NavigatorGetTransferVar<Int64>("IdEstimated") : 0;
            }
        }
        private CalculationEstimated _Entity = null;
        private CalculationEstimated Entity
        {
            get
            {
                try
                {
                    if (_Entity == null)
                    {
                        _Entity = ParentEntity.CalculationEstimated(Convert.ToInt64(_IdEntity));
                    }
                    return _Entity;
                }
                catch
                {
                    return null;
                }
            }
            set { _Entity = value; }
        }

        private RadComboBox _RdcProcessClassification;
        private RadTreeView _RtvProcessClassification;
        private CompareValidator _CvProcessClassification;

        private RadComboBox _RdcCalculationScenarioType;
        private CompareValidator _CvCalculationScenarioType;

        #endregion

        #region PageLoad & Init
            protected override void InitializeHandlers()
            {
                base.InitializeHandlers();
                //Le paso el Save a la MasterContentToolbar
                EventHandler _saveEventHandler = new EventHandler(btnSave_Click);
                FwMasterPage.ContentNavigatorToolbarFileActionAdd(_saveEventHandler, MasterFwContentToolbarAction.Save, true);

                base.InjectValidateDatePicker(rdtEstimatedFromDate.ClientID, rdtEstimatedToDate.ClientID, "EstimatedRange");
                cvEstimatedEndDate.EnableClientScript = true;
                cvEstimatedEndDate.ClientValidationFunction = "ValidateDateTimeRangeEstimatedRange";
            }
            protected override void OnInit(EventArgs e)
            {
                base.OnInit(e);
                LoadTextLabels();
            }
            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);
                AddTreeViewProcessClassifications();
                AddComboCalculationScenarioType();

                if (!Page.IsPostBack)
                {
                    //Inicializo el Form
                    if (Entity == null)
                        Add();
                    else
                        LoadData(); //Edit.

                    //Form
                    base.SetContentTableRowsCss(tblContentForm);
                    this.txtValue.Focus();
                }
            }
            protected override void SetPagetitle()
            {
                base.PageTitle = (Entity != null) ? ParentEntity.LanguageOption.Name : Resources.CommonListManage.CalculationEstimatedForecasted;
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }
        #endregion

        #region Private 
            private void LoadTextLabels()
            {
                lblCalculation.Text = Resources.CommonListManage.Calculation;
                lblClassifications.Text = Resources.CommonListManage.ProcessClassification;
                lblEstimatedFromDate.Text = Resources.CommonListManage.From;
                lblEstimatedToDate.Text = Resources.CommonListManage.To;
                lblIdEstimated.Text = Resources.CommonListManage.IdEstimated;
                lblScenario.Text = Resources.CommonListManage.ScenarioType;
                lblValue.Text = Resources.CommonListManage.ValueCertificated;
                rfv1.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
                rfv2.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
                rfv3.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
                rvValue.ErrorMessage = Resources.ConstantMessage.ValidationWrongFormat;
                cvEstimatedEndDate.ErrorMessage = Resources.ConstantMessage.ValidationDateFromTo;
                cv1.ErrorMessage = Resources.ConstantMessage.ValidationWrongFormat;
                cv2.ErrorMessage = Resources.ConstantMessage.ValidationWrongFormat;
            }
            private void Add()
            {
                base.StatusBar.Clear();

                lblCalculationValue.Text = ParentEntity.LanguageOption.Name;
                lblIdEstimatedValue.Text = String.Empty;
                txtValue.Text = String.Empty;

                //_RdcCalculationScenarioType.SelectedValue = Common.Constants.ComboBoxSelectItemValue;

                rdtEstimatedFromDate.SelectedDate = null;
                rdtEstimatedToDate.SelectedDate = null;
            }
            private void LoadData()
            {
                lblCalculationValue.Text = ParentEntity.LanguageOption.Name;
                lblIdEstimatedValue.Text = Entity.IdEstimated.ToString();
                txtValue.Text = Entity.Value.ToString();
                rdtEstimatedFromDate.SelectedDate = Entity.StartDate;
                rdtEstimatedToDate.SelectedDate = Entity.EndDate;

                SetProcessClassification();

                AddComboCalculationScenarioType();
                //base.LoadScenarioType(ref _RdcCalculationScenarioType, _idProcessClassification);

                _RdcCalculationScenarioType.SelectedValue = "IdScenarioType=" + Entity.CalculationScenarioType.IdScenarioType.ToString();
            }
            private void SetProcessClassification()
            {
                //si es un root, no debe hacer nada de esto.
                if (Entity.CalculationScenarioType.ProcessClassification != null)
                {
                    //El orden de este KeyValue, debe ser el mismo al que se hace al momento de crear el tree, y el mismo del metodo publico del factory!!!
                    String _keyValues = "IdProcessClassification=" + Entity.CalculationScenarioType.ProcessClassification.First().Value.IdProcessClassification + "IdParentProcessClassification=" + Entity.CalculationScenarioType.ProcessClassification.First().Value.IdParentProcessClassification;
                    //Realiza el seteo del parent en el Combo-Tree.
                    SelectItemTreeViewParent(_keyValues, ref _RtvProcessClassification, ref _RdcProcessClassification, Common.ConstantsEntitiesName.PF.ProcessClassification, Common.ConstantsEntitiesName.PF.ProcessClassificationChildren);
                }
            }
            private void AddTreeViewProcessClassifications()
            {
                String _filterExpression = String.Empty;
                //Combo de GeographicArea Parent
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                AddComboWithTree(phProcessClassification, ref _RdcProcessClassification, ref _RtvProcessClassification,
                    Common.ConstantsEntitiesName.PF.ProcessClassifications, _params, false, true, false, ref _filterExpression,
                    new RadTreeViewEventHandler(RtvProcessClassification_NodeExpand),
                    Common.Constants.ComboBoxSelectItemDefaultPrefix, false);
                
                _RtvProcessClassification.NodeClick += new RadTreeViewEventHandler(RtvProcessClassification_NodeClick);

                ValidatorRequiredField(Common.ConstantsEntitiesName.PF.ProcessClassification, phProcessClassificationValidator, ref _CvProcessClassification, _RdcProcessClassification, Resources.ConstantMessage.SelectAProcessClassification);
            }
            private void AddComboCalculationScenarioType()
            {
                
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                if (_RtvProcessClassification.SelectedNode != null)
                {
                    Int64 _idProcessClassification = Convert.ToInt64(GetKeyValue(_RtvProcessClassification.SelectedNode.Value, "IdProcessClassification"));
                    _params.Add("IdProcessClassification", _idProcessClassification.ToString());
                }
                //Para cargar los contactTypes, debo setear el Applicability 2 que es Emails.
                AddCombo(phCalculationScenarioType, ref _RdcCalculationScenarioType, "CalculationScenarioTypes", String.Empty, _params, false, true, false, false, false);
                ValidatorRequiredField(Common.ConstantsEntitiesName.PA.CalculationScenarioType, phCalculationScenarioTypeValidator, ref _CvCalculationScenarioType, _RdcCalculationScenarioType, Resources.ConstantMessage.SelectACalculationScenarioType);
            }
        #endregion

        #region Page Events

        protected void RtvProcessClassification_NodeClick(object o, RadTreeNodeEventArgs e)
        {

            base.StatusBar.Clear();

            AddComboCalculationScenarioType();
            //base.LoadScenarioType(ref _RdcCalculationScenarioType, _idProcessClassification);
            //ddlProcessClassification.Text = _RtvProcessClassification.SelectedNode.Text;
        }
        void RtvProcessClassification_NodeExpand(object sender, RadTreeNodeEventArgs e)
        {
            base.NodeExpand(sender, e, Common.ConstantsEntitiesName.PF.ProcessClassificationChildren, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, false, String.Empty);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime _EstimatedFromDate = Convert.ToDateTime(rdtEstimatedFromDate.SelectedDate);
                DateTime _EstimatedToDate = Convert.ToDateTime(rdtEstimatedToDate.SelectedDate);
                Int64 _idCalculationScenarioType = Convert.ToInt64(GetKeyValue(_RdcCalculationScenarioType.SelectedValue, "IdScenarioType"));
                Condesus.EMS.Business.PA.Entities.CalculationScenarioType _calculationScenarioType = EMSLibrary.User.PerformanceAssessments.Configuration.CalculationScenarioType(_idCalculationScenarioType);

                if (Entity == null)
                {
                    //Alta
                    Entity = ParentEntity.CalculationEstimatesAdd(_EstimatedFromDate, _EstimatedToDate, _calculationScenarioType, Convert.ToDecimal(txtValue.Text));
                }
                else
                {
                    //Modificacion
                    Entity.Modify(ParentEntity, _EstimatedFromDate, _EstimatedToDate, Convert.ToDecimal(txtValue.Text), _calculationScenarioType);
                }
                base.NavigatorAddTransferVar("IdEstimated", Entity.IdEstimated);

                String _pkValues = "IdEstimated=" + Entity.IdEstimated.ToString() +
                    "& IdCalculation=" + ParentEntity.IdCalculation.ToString();
                base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);
                base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.PA.CalculationEstimate);
                base.NavigatorAddTransferVar("EntityNameContextInfo", Common.ConstantsEntitiesName.PA.Calculation);
                base.NavigatorAddTransferVar("EntityNameContextElement", String.Empty);
                //Navigate("~/MainInfo/ListViewer.aspx", Resources.CommonListManage.CalculationEstimatedForecasted + " " + Entity.Calculation.LanguageOption.Name, Condesus.WebUI.Navigation.DeleteType.DeleteModifiedEntity);
                String _entityPropertyName = String.Concat(Entity.Calculation.LanguageOption.Name);
                NavigatePropertyEntity(Common.ConstantsEntitiesName.PA.CalculationEstimate, _entityPropertyName, Condesus.WebUI.Navigation.NavigateMenuAction.Add);

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
