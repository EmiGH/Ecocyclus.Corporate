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
    public partial class CalculationCertificatesProperties : BaseProperties
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
                return base.NavigatorContainsTransferVar("IdCertificated") ? base.NavigatorGetTransferVar<Int64>("IdCertificated") : 0;
            }
        }
        private CalculationCertificated _Entity = null;
        private CalculationCertificated Entity
        {
            get
            {
                try
                {
                    if (_Entity == null)
                    {
                        _Entity = _ParentEntity.CalculationCertificated(Convert.ToInt64(_IdEntity));
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
        RadComboBox _RdcOrganization;
        private RadTreeView _RtvOrganization;
        CompareValidator _CvOrganization;
           
        #endregion

        #region PageLoad & Init
            protected override void InitializeHandlers()
            {
                base.InitializeHandlers();
                //Le paso el Save a la MasterContentToolbar
                EventHandler _saveEventHandler = new EventHandler(btnSave_Click);
                FwMasterPage.ContentNavigatorToolbarFileActionAdd(_saveEventHandler, MasterFwContentToolbarAction.Save, true);

                base.InjectValidateDatePicker(rdtCertificatedFromDate.ClientID, rdtCertificatedToDate.ClientID, "EstimatedRange");
                cvCertificatedEndDate.EnableClientScript = true;
                cvCertificatedEndDate.ClientValidationFunction = "ValidateDateTimeRangeEstimatedRange";
            }
            protected override void OnInit(EventArgs e)
            {
                base.OnInit(e);
                LoadTextLabels();
            }
            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);
                AddComboOrganizationDOE();
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
                base.PageTitle = (Entity != null) ? ParentEntity.LanguageOption.Name : Resources.CommonListManage.CalculationCertificated;
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }
        #endregion

        #region Private Methods
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonListManage.CalculationCertificated;
                lblCalculation.Text = Resources.CommonListManage.Calculation;
                lblCertificatedFromDate.Text = Resources.CommonListManage.From;
                lblCertificatedToDate.Text = Resources.CommonListManage.To;
                lblIdCertificated.Text = Resources.CommonListManage.IdCertificated;
                lblOrganizationDOE.Text = Resources.CommonListManage.DOE;
                lblValue.Text = Resources.CommonListManage.ValueCertificated;
                rfv2.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
                rfv1.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
                rvValue.ErrorMessage = Resources.ConstantMessage.ValidationWrongFormat;
                rfv3.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
                cvCertificatedEndDate.ErrorMessage = Resources.ConstantMessage.ValidationDateFromTo;
                cv2.ErrorMessage = Resources.ConstantMessage.ValidationWrongFormat;
                cv1.ErrorMessage = Resources.ConstantMessage.ValidationWrongFormat;
            }
            private void Add()
            {
                base.StatusBar.Clear();

                lblCalculationValue.Text = ParentEntity.LanguageOption.Name;
                lblIdCertificatedValue.Text = String.Empty;
                txtValue.Text = String.Empty;

                rdtCertificatedFromDate.SelectedDate = null;
                rdtCertificatedToDate.SelectedDate = null;
            }
            private void LoadData()
            {
                lblCalculationValue.Text = ParentEntity.LanguageOption.Name;
                lblIdCertificatedValue.Text = Entity.IdCertificated.ToString();
                txtValue.Text = Entity.Value.ToString();
                rdtCertificatedFromDate.SelectedDate = Entity.StartDate;
                rdtCertificatedToDate.SelectedDate = Entity.EndDate;

                //Seteamos la organizacion...
                //Realiza el seteo del parent en el Combo-Tree.
                Condesus.EMS.Business.DS.Entities.Organization _oganization = EMSLibrary.User.DirectoryServices.Map.Organization(Entity.IdOrganizationDOE);
                String _keyValuesElement = "IdOrganization=" + _oganization.IdOrganization.ToString();
                if (_oganization.Classifications.Count > 0)
                {
                    String _keyValuesClassification = "IdOrganizationClassification=" + _oganization.Classifications.First().Value.IdOrganizationClassification;
                    SelectItemTreeViewParentElementMaps(_keyValuesClassification, _keyValuesElement, ref _RtvOrganization, ref _RdcOrganization, Common.ConstantsEntitiesName.DS.OrganizationClassification, Common.ConstantsEntitiesName.DS.OrganizationClassificationsChildren, Common.ConstantsEntitiesName.DS.Organizations);
                }
                else
                {
                    SelectItemTreeViewParent(_keyValuesElement, ref _RtvOrganization, ref _RdcOrganization, Common.ConstantsEntitiesName.DS.Organization, Common.ConstantsEntitiesName.DS.Organizations);
                }
            }
            private void AddComboOrganizationDOE()
            {
                //Dictionary<String, Object> _params = new Dictionary<String, Object>();
                //AddCombo(phOrganization, ref _RdcOrganization, Common.ConstantsEntitiesName.DS.Organizations, String.Empty, _params, false, true, false);

                String _filterExpression = String.Empty;
                //Combo de Organizaciones
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                AddComboWithTreeElementMaps(ref phOrganization, ref _RdcOrganization, ref _RtvOrganization,
                    Common.ConstantsEntitiesName.DS.OrganizationClassifications, Common.ConstantsEntitiesName.DS.OrganizationsRoots, _params, false, true, false, ref _filterExpression,
                    new RadTreeViewEventHandler(rtvOrganizations_NodeExpand),
                    Resources.Common.ComboBoxNoDependency, false);

                ValidatorRequiredField(Common.ConstantsEntitiesName.DS.OrganizationClassifications, phOrganizationValidator, ref _CvOrganization, _RdcOrganization, Resources.ConstantMessage.SelectAnOrganization);
            }
        #endregion

        #region Page Events
        protected void rtvOrganizations_NodeExpand(object sender, RadTreeNodeEventArgs e)
        {
            //Limpio los hijos, para no duplicar al abrir y cerrar.
            e.Node.Nodes.Clear();
            Dictionary<String, Object> _params = new Dictionary<String, Object>();
            _params = GetKeyValues(e.Node.Value);

            //Primero lo hace sobre las Clasificaciones Hijas...
            //Actualiza el Data table con los hijos, para poder asociarlas al nodo expadido.
            BuildGenericDataTable(Common.ConstantsEntitiesName.DS.OrganizationClassificationsChildren, _params);
            if (DataTableListManage.ContainsKey(Common.ConstantsEntitiesName.DS.OrganizationClassificationsChildren))
            {
                foreach (DataRow _drRecord in DataTableListManage[Common.ConstantsEntitiesName.DS.OrganizationClassificationsChildren].Rows)
                {
                    RadTreeNode _node = SetElementMapsNodeTreeView(_drRecord, Common.ConstantsEntitiesName.DS.OrganizationClassificationsChildren, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, String.Empty, String.Empty, String.Empty);
                    e.Node.Nodes.Add(_node);
                    //Como es un Mapa de Elementos, indica true para que cargue los elementos debajo de la classificacion.
                    SetExpandMode(_node, Common.ConstantsEntitiesName.DS.OrganizationClassificationsChildren, true, false);
                }
            }

            //Ahora lo hace sobre los Elementos Hijos de la Clasificacion expandida.
            BuildGenericDataTable(Common.ConstantsEntitiesName.DS.Organizations, _params);
            if (DataTableListManage.ContainsKey(Common.ConstantsEntitiesName.DS.Organizations))
            {
                foreach (DataRow _drRecord in DataTableListManage[Common.ConstantsEntitiesName.DS.Organizations].Rows)
                {
                    RadTreeNode _node = SetElementMapsNodeTreeView(_drRecord, Common.ConstantsEntitiesName.DS.Organizations, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, String.Empty, String.Empty, String.Empty);
                    e.Node.Nodes.Add(_node);
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime _CertificatedFromDate = Convert.ToDateTime(rdtCertificatedFromDate.SelectedDate);
                DateTime _CertificateddToDate = Convert.ToDateTime(rdtCertificatedToDate.SelectedDate);
                Int64 _idOrganization = Convert.ToInt64(GetKeyValue(_RtvOrganization.SelectedNode.Value, "IdOrganization"));

                if (Entity == null)
                {
                    //Alta
                    Entity = ParentEntity.CalculationCertificatesAdd(_CertificatedFromDate, _CertificateddToDate, Convert.ToDecimal(txtValue.Text),_idOrganization);
                }
                else
                {
                    //Modificacion
                    Entity.Modify(ParentEntity, _CertificatedFromDate, _CertificateddToDate, Convert.ToDecimal(txtValue.Text), _idOrganization);
                }
                base.NavigatorAddTransferVar("IdCertificated", Entity.IdCertificated);

                String _pkValues = "IdCertificated=" + Entity.IdCertificated.ToString() +
                    "& IdCalculation=" + ParentEntity.IdCalculation.ToString();
                base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);
                base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.PA.CalculationCertificate);
                base.NavigatorAddTransferVar("EntityNameContextInfo", Common.ConstantsEntitiesName.PA.Calculation);
                base.NavigatorAddTransferVar("EntityNameContextElement", String.Empty);
                //Navigate("~/MainInfo/ListViewer.aspx", Resources.CommonListManage.CalculationCertificated + " " + Entity.Calculation.LanguageOption.Name, Condesus.WebUI.Navigation.DeleteType.DeleteModifiedEntity);
                String _entityPropertyName = String.Concat(Entity.Calculation.LanguageOption.Name);
                NavigatePropertyEntity(Common.ConstantsEntitiesName.PA.CalculationCertificate, _entityPropertyName, Condesus.WebUI.Navigation.NavigateMenuAction.Add);

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
