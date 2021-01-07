using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using Telerik.Web.UI;
using Condesus.EMS.WebUI.Controls;
using Condesus.EMS.Business.DS.Entities;
using Condesus.EMS.WebUI.MasterControls;
using Condesus.EMS.Business.Security.Entities;
using Condesus.EMS.Business.GIS.Entities;
using System.Linq;

namespace Condesus.EMS.WebUI.Security
{
    public partial class JobTitlesProperties : BaseProperties
    {
        #region Internal Properties
        String _PageTitle = String.Empty;
        CompareValidator _CvOrganization;
        CompareValidator _CvOrganizationalChart;
        CompareValidator _CvJobTitle;
        CompareValidator _CvPermission;
        //CompareValidator _CvRolType;
        //private RadComboBox _RdcRoleType;
        private RadComboBox _RdcPermission;
        private RadComboBox _RdcOrganization;
        private RadTreeView _RtvOrganization;
        private RadComboBox _RdcOrganizationalChart;
        private RadComboBox _RdcJobTitle;
        private RadTreeView _RtvJobTitle;
        private String _ParentEntity
        {
            get
            {
                return base.NavigatorContainsTransferVar("ParentEntity") ? Convert.ToString(base.NavigatorGetTransferVar<Object>("ParentEntity")) : Convert.ToString(GetPKfromNavigator("ParentEntity"));
            }
        }
        private Int64 _IdFunctionalArea
        {
            get
            {
                return base.NavigatorContainsTransferVar("IdFunctionalArea") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdFunctionalArea")) : Convert.ToInt64(GetPKfromNavigator("IdFunctionalArea"));
            }
        }
        private Int64 _IdGeographicArea
        {
            get
            {
                return base.NavigatorContainsTransferVar("IdGeographicArea") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdGeographicArea")) : Convert.ToInt64(GetPKfromNavigator("IdGeographicArea"));
            }
        }
        private Int64 _IdPosition
        {
            get
            {
                return base.NavigatorContainsTransferVar("IdPosition") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdPosition")) : Convert.ToInt64(GetPKfromNavigator("IdPosition"));
            }
        }
        private Int64 _IdOrganization
        {
            get
            {
                return base.NavigatorContainsTransferVar("IdOrganization") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdOrganization")) : Convert.ToInt64(GetPKfromNavigator("IdOrganization"));
            }
        }
        private Int64 _IdPermission
        {
            get
            {
                return base.NavigatorContainsTransferVar("IdPermission") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdPermission")) : Convert.ToInt64(GetPKfromNavigator("IdPermission"));
            }
        }
        private Int64 _IdOwnerObject
        {
            get
            {
                return base.NavigatorContainsTransferVar("IdOwnerObject") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdOwnerObject")) : Convert.ToInt64(GetPKfromNavigator("IdOwnerObject"));
            }
        }
        private String _OwnerClassName
        {
            get
            {
                return base.NavigatorContainsTransferVar("OwnerClassName") ? Convert.ToString(base.NavigatorGetTransferVar<Object>("OwnerClassName")) : Convert.ToString(GetPKfromNavigator("OwnerClassName"));
            }
        }

        private Int64 _IdOrganizationClassification
        {
            get
            {
                return base.NavigatorContainsTransferVar("IdOrganizationClassification") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdOrganizationClassification")) : Convert.ToInt64(GetPKfromNavigator("IdOrganizationClassification"));
            }
        }
        private Int64 _IdOrganizationOrg
        {
            get
            {
                return base.NavigatorContainsTransferVar("IdOrganizationOrg") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdOrganizationOrg")) : Convert.ToInt64(GetPKfromNavigator("IdOrganization"));
            }
        }
        private Int64 _IdResourceClassification
        {
            get
            {
                return base.NavigatorContainsTransferVar("IdResourceClassification") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdResourceClassification")) : Convert.ToInt64(GetPKfromNavigator("IdResourceClassification"));
            }
        }
        private Int64 _IdResource
        {
            get
            {
                return base.NavigatorContainsTransferVar("IdResource") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdResource")) : Convert.ToInt64(GetPKfromNavigator("IdResource"));
            }
        }
        private Int64 _IdProjectClassification
        {
            get
            {
                return base.NavigatorContainsTransferVar("IdProjectClassification") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdProjectClassification")) : Convert.ToInt64(GetPKfromNavigator("IdProjectClassification"));
            }
        }
        private Int64 _IdProcess
        {
            get
            {
                return base.NavigatorContainsTransferVar("IdProcess") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdProcess")) : Convert.ToInt64(GetPKfromNavigator("IdProcess"));
            }
        }
        private Int64 _IdIndicatorClassification
        {
            get
            {
                return base.NavigatorContainsTransferVar("IdIndicatorClassification") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdIndicatorClassification")) : Convert.ToInt64(GetPKfromNavigator("IdIndicatorClassification"));
            }
        }
        private Int64 _IdIndicator
        {
            get
            {
                return base.NavigatorContainsTransferVar("IdIndicator") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdIndicator")) : Convert.ToInt64(GetPKfromNavigator("IdIndicator"));
            }
        }
        private Int64 _IdProcessClassification
        {
            get
            {
                return base.NavigatorContainsTransferVar("IdProcessClassification") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdProcessClassification")) : Convert.ToInt64(GetPKfromNavigator("IdProcessClassification"));
            }
        }


        private RightJobTitle _Entity = null;
        private RightJobTitle Entity
        {
            get
            {
                try
                {
                    if (_Entity == null)
                    {
                        //Post _post = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).Post(_IdGeographicArea, _IdPosition, _IdFunctionalArea, _IdPerson);
                        Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization);
                        GeographicArea _geoArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_IdGeographicArea);
                        Position _position = _organization.Position(_IdPosition);
                        FunctionalArea _funArea = _organization.FunctionalArea(_IdFunctionalArea);
                        FunctionalPosition _funPos = _organization.FunctionalPosition(_position, _funArea);
                        GeographicFunctionalArea _geoFunArea = _organization.GeographicFunctionalArea(_funArea, _geoArea);
                        JobTitle _jobTitle = _organization.JobTitle(_geoFunArea, _funPos);
                        Permission _permission = EMSLibrary.User.Security.Permission(_IdPermission);

                        switch (_ParentEntity)
                        {
                            //case Common.ConstantsEntitiesName.KC.ResourceClassification:    // "ResourceClassificationsProperties":
                            //    _Entity = EMSLibrary.User.KnowledgeCollaboration.Map.ResourceClassification(_IdResourceClassification).ReadJobTitleByID(_jobTitle, _permission);
                            //    break;
                            //case Common.ConstantsEntitiesName.KC.Resource:  // "ResourcesProperties":
                            //    _Entity = EMSLibrary.User.KnowledgeCollaboration.Map.Resource(_IdResource).ReadJobTitleByID(_jobTitle, _permission);
                            //    break;
                            //case Common.ConstantsEntitiesName.IA.ProjectClassification: // "ProjectClassificationsProperties":
                            //    _Entity = EMSLibrary.User.ImprovementAction.Map.ProjectClassification(_IdProjectClassification).ReadJobTitleByID(_jobTitle, _permission);
                            //    break;
                            case Common.ConstantsEntitiesName.PF.ProcessGroupProcess:   // "Process":
                                Condesus.EMS.Business.PF.Entities.Process _process = EMSLibrary.User.ProcessFramework.Map.Process(_IdProcess);
                                if (_process.GetType().Name == Common.ConstantsEntitiesName.PF.ProcessGroupProcess)
                                {
                                    _Entity = ((Condesus.EMS.Business.PF.Entities.ProcessGroupProcess)_process).ReadJobTitleByID(_jobTitle, _permission);
                                }
                                else
                                {
                                    //TODO: Que paso???
                                    //_rightPerson = ((Condesus.EMS.Business.PF.Entities.ProcessTask)_process).ReadPersonByID(_post, _permission);
                                }
                                break;
                            //case Common.ConstantsEntitiesName.PA.IndicatorClassification:   // "IndicatorClassificationsProperties":
                            //    _Entity = EMSLibrary.User.PerformanceAssessments.Map.IndicatorClassification(_IdIndicatorClassification).ReadJobTitleByID(_jobTitle, _permission);
                            //    break;
                            //case Common.ConstantsEntitiesName.PA.Indicator: // "Indicator":
                            //    _Entity = EMSLibrary.User.PerformanceAssessments.Map.Indicator(_IdIndicator).ReadJobTitleByID(_jobTitle, _permission);
                            //    break;
                            //case Common.ConstantsEntitiesName.PF.ProcessClassification: // "ProcessClassificationsProperties":
                            //    _Entity = EMSLibrary.User.ProcessFramework.Map.ProcessClassification(_IdProcessClassification).ReadJobTitleByID(_jobTitle, _permission);
                            //    break;
                            case Common.ConstantsEntitiesName.DS.Organization:  // "OrganizationProperties":
                                _Entity = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganizationOrg).ReadJobTitleByID(_jobTitle, _permission);
                                break;
                            //case Common.ConstantsEntitiesName.DS.OrganizationClassification:
                            //    _Entity = EMSLibrary.User.DirectoryServices.Map.OrganizationClassification(_IdOrganizationClassification).ReadJobTitleByID(_jobTitle, _permission);
                            //    break;

                            case Condesus.EMS.Business.Common.Security.MapDS:
                                _Entity = EMSLibrary.User.DirectoryServices.Map.ReadJobTitleByID(_jobTitle, _permission);
                                break;
                            case Condesus.EMS.Business.Common.Security.MapIA:
                                _Entity = EMSLibrary.User.ImprovementAction.Map.ReadJobTitleByID(_jobTitle, _permission);
                                break;
                            case Condesus.EMS.Business.Common.Security.MapKC:
                                _Entity = EMSLibrary.User.KnowledgeCollaboration.Map.ReadJobTitleByID(_jobTitle, _permission);
                                break;
                            case Condesus.EMS.Business.Common.Security.MapPA:
                                _Entity = EMSLibrary.User.PerformanceAssessments.Map.ReadJobTitleByID(_jobTitle, _permission);
                                break;
                            case Condesus.EMS.Business.Common.Security.MapPF:
                                _Entity = EMSLibrary.User.ProcessFramework.Map.ReadJobTitleByID(_jobTitle, _permission);
                                break;
                            case Condesus.EMS.Business.Common.Security.MapRM:
                                _Entity = EMSLibrary.User.RiskManagement.Map.ReadJobTitleByID(_jobTitle, _permission);
                                break;

                            case Condesus.EMS.Business.Common.Security.ConfigurationDS:
                                _Entity = EMSLibrary.User.DirectoryServices.Configuration.ReadJobTitleByID(_jobTitle, _permission);
                                break;
                            case Condesus.EMS.Business.Common.Security.ConfigurationIA:
                                _Entity = EMSLibrary.User.ImprovementAction.Configuration.ReadJobTitleByID(_jobTitle, _permission);
                                break;
                            case Condesus.EMS.Business.Common.Security.ConfigurationKC:
                                _Entity = EMSLibrary.User.KnowledgeCollaboration.Configuration.ReadJobTitleByID(_jobTitle, _permission);
                                break;
                            case Condesus.EMS.Business.Common.Security.ConfigurationPA:
                                _Entity = EMSLibrary.User.PerformanceAssessments.Configuration.ReadJobTitleByID(_jobTitle, _permission);
                                break;
                            case Condesus.EMS.Business.Common.Security.ConfigurationPF:
                                _Entity = EMSLibrary.User.ProcessFramework.Configuration.ReadJobTitleByID(_jobTitle, _permission);
                                break;
                            case Condesus.EMS.Business.Common.Security.ConfigurationRM:
                                _Entity = EMSLibrary.User.RiskManagement.Configuration.ReadJobTitleByID(_jobTitle, _permission);
                                break;

                        }
                    }
                    return _Entity;
                }
                catch { return null; }
            }

            set { _Entity = value; }
        }
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
            }
            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);

                AddCombos();
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
                try
                {
                    if ((_ParentEntity.Contains("Map")) || (_ParentEntity.Contains("Config")))
                    {
                        _PageTitle = GetGlobalResourceObject("CommonListManage", "RightJobTitle" + _ParentEntity.Replace("Configuration", "Config")).ToString();   // _ParentEntity + " : " + Resources.CommonListManage.JobTitle;
                    }
                    else
                    {
                        _PageTitle = _ParentEntity + " : " + Resources.CommonListManage.RightJobTitle;
                    }
                    base.PageTitle = (Entity != null) ? Entity.JobTitle.Name() : _PageTitle;
                }
                catch { base.PageTitle = String.Empty; }
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }
        #endregion

        #region Private Method
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonListManage.SecuritybyJobTitles;
                lblOrganization2.Text = Resources.CommonListManage.Organization;
                lblOrganizationalChart.Text = Resources.CommonListManage.OrganizationalChart;
                lblPermission.Text = Resources.CommonListManage.Permission;
                lblPost.Text = Resources.CommonListManage.Post;
                //lblRoleType.Text = Resources.CommonListManage.RoleType;
            }
            private void AddCombos()
            {
                AddComboOrganizations();
                AddComboOrganizationalCharts();
                AddComboJobTitles();
                AddComboPermissions();
                //AddComboRoleType();
            }
            private void AddComboPermissions()
            {
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                AddCombo(phPermission, ref _RdcPermission, Common.ConstantsEntitiesName.SS.Permissions, String.Empty, _params, false, true, false, false, false);
                ValidatorRequiredField(Common.ConstantsEntitiesName.SS.Permissions, phPermissionValidator, ref _CvPermission, _RdcPermission, Resources.ConstantMessage.SelectAPermission);
            }
            private void AddComboOrganizationalCharts()
            {
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                if (_RtvOrganization.SelectedNode != null)
                {
                    if (!_RtvOrganization.SelectedNode.Value.Contains("IdOrganizationClassification"))
                    {
                        _params.Add("IdOrganization", GetKeyValue(_RtvOrganization.SelectedNode.Value, "IdOrganization"));
                    }
                }
                AddCombo(phOrganizationalChart, ref _RdcOrganizationalChart, Common.ConstantsEntitiesName.DS.OrganizationalCharts, String.Empty, _params, false, true, false, true, false);
                _RdcOrganizationalChart.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(_RdcOrganizationalChart_SelectedIndexChanged);

                ValidatorRequiredField(Common.ConstantsEntitiesName.DS.OrganizationalCharts, phOrganizationalChartValidator, ref _CvOrganizationalChart, _RdcOrganizationalChart, Resources.ConstantMessage.SelectAnOrganizationalChart);
            }
            private void AddComboOrganizations()
            {
                String _filterExpression = String.Empty;
                //Combo de Organizaciones
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                AddComboWithTreeElementMaps(ref phOrganization, ref _RdcOrganization, ref _RtvOrganization,
                    Common.ConstantsEntitiesName.DS.OrganizationClassifications, Common.ConstantsEntitiesName.DS.OrganizationsRoots, _params, false, true, false, ref _filterExpression,
                    new RadTreeViewEventHandler(rtvOrganizations_NodeExpand),
                    new RadTreeViewEventHandler(rtvOrganizations_NodeClick),
                    Resources.Common.ComboBoxNoDependency, false);

                //FwMasterPage.RegisterContentAsyncPostBackTrigger(_RtvOrganization, "NodeClick");
                ValidatorRequiredField(Common.ConstantsEntitiesName.DS.OrganizationClassifications, phOrganizationValidator, ref _CvOrganization, _RdcOrganization, Resources.ConstantMessage.SelectAnOrganization);
            }
            private void AddComboJobTitles()
            {
                String _filterExpression = String.Empty;
                //Combo de GeographicArea Parent
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                if (_RtvOrganization.SelectedNode != null)
                {
                    _params.Add("IdOrganization", GetKeyValue(_RtvOrganization.SelectedNode.Value, "IdOrganization"));
                    if (GetKeyValue(_RdcOrganizationalChart.SelectedValue, "IdOrganizationalChart") != null)
                    {
                        _params.Add("IdOrganizationalChart", GetKeyValue(_RdcOrganizationalChart.SelectedValue, "IdOrganizationalChart"));
                    }
                }
                AddComboWithTree(phJobTitle, ref _RdcJobTitle, ref _RtvJobTitle,
                    Common.ConstantsEntitiesName.DS.JobTitles, _params, false, true, false, ref _filterExpression,
                    new RadTreeViewEventHandler(rtvJobTitles_NodeExpand),
                    Resources.Common.ComboBoxNoDependency, false);

                ValidatorRequiredField(Common.ConstantsEntitiesName.DS.JobTitles, phJobTitleValidator, ref _CvJobTitle, _RdcJobTitle, Resources.ConstantMessage.SelectAJobTitle);
            }
            //private void AddComboRoleType()
            //{
            //    Dictionary<String, Object> _params = new Dictionary<String, Object>();
            //    AddCombo(phRoleType, ref _RdcRoleType, Common.ConstantsEntitiesName.SS.RoleTypes, String.Empty, _params, false, true, false, false, false);
            //    ValidatorRequiredField(Common.ConstantsEntitiesName.SS.RoleType, phRoleTypeValidator, ref _CvRolType, _RdcRoleType, Resources.ConstantMessage.SelectARoleType);
            //}
            private void LoadData()
            {
                //Seteamos la organizacion...
                //Realiza el seteo del parent en el Combo-Tree.
                //Organization _oganization = EMSLibrary.User.DirectoryServices.Map.Organization(Entity.JobTitle.IdOrganization);
                //String _keyValuesElement = "IdOrganization=" + _oganization.IdOrganization.ToString();
                //if (_oganization.Classifications.Count > 0)
                //{
                //    String _keyValuesClassification = "IdOrganizationClassification=" + _oganization.Classifications.First().Value.IdOrganizationClassification;
                //    SelectItemTreeViewParentElementMaps(_keyValuesClassification, _keyValuesElement, ref _RtvOrganization, ref _RdcOrganization, Common.ConstantsEntitiesName.DS.OrganizationClassification, Common.ConstantsEntitiesName.DS.OrganizationClassificationsChildren, Common.ConstantsEntitiesName.DS.Organizations);
                //}
                //else
                //{
                //    SelectItemTreeViewParent(_keyValuesElement, ref _RtvOrganization, ref _RdcOrganization, Common.ConstantsEntitiesName.DS.Organization, Common.ConstantsEntitiesName.DS.Organizations);
                //}

                SetJobTitle();
                SetPermission();
                //SetRoleType();
                SetOrganization();

                _RdcOrganization.Enabled = false;
                _RdcOrganizationalChart.Enabled = false;
                _RdcJobTitle.Enabled = false;
                _RdcOrganization.Enabled = false;

            }
            private void SetJobTitle()
            {
                _RdcJobTitle.Style.Add("display", "none");
                _RdcOrganizationalChart.Style.Add("display", "none");
                _CvJobTitle.EnableClientScript = false;
                _CvOrganizationalChart.EnableClientScript = false;

                Label _lblJobTitleName = new Label();
                _lblJobTitleName.Text = Entity.JobTitle.Name();
                phJobTitle.Controls.Add(_lblJobTitleName);

                Label _lblOrganizationalChart = new Label();
                _lblOrganizationalChart.Text = Resources.Common.Unavailable;
                phOrganizationalChart.Controls.Add(_lblOrganizationalChart);

            }
            private void SetPermission()
            {
                _RdcPermission.SelectedValue = "IdPermission=" + Entity.Permission.IdPermission.ToString();
            }
            //private void SetRoleType()
            //{
            //    _RdcRoleType.SelectedValue = "IdRoleType=" + Entity.RoleType.IdRoleType.ToString();
            //}
            private void SetOrganization()
            {
                _RdcOrganization.Style.Add("display", "none");
                _CvOrganization.EnableClientScript = false;

                Label _lblOrganization = new Label();
                _lblOrganization.Text = Entity.JobTitle.Organization.CorporateName;
                phOrganization.Controls.Add(_lblOrganization);
            }
            private void Add()
            {
                base.StatusBar.Clear();
            }
        #endregion

        #region Page Events
        //Evento para el Expand del Combo con Tree pero ElementMaps
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
        protected void rtvOrganizations_NodeClick(object sender, RadTreeNodeEventArgs e)
        {
            AddComboOrganizationalCharts();
        }
        protected void rtvJobTitles_NodeExpand(object sender, RadTreeNodeEventArgs e)
        {
            NodeExpand(sender, e, Common.ConstantsEntitiesName.DS.JobTitleChildren, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, false, String.Empty);
        }
        void _RdcOrganizationalChart_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            AddComboJobTitles();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Int64 _idFunctionalArea = 0;
                Int64 _idGeographicArea = 0;
                Int64 _idPosition = 0;
                Int64 _idOrganization;  // = Convert.ToInt64(GetKeyValue(_RtvOrganization.SelectedNode.Value, "IdOrganization"));

                Int64 _idPermission = Convert.ToInt64(GetKeyValue(_RdcPermission.SelectedValue, "IdPermission"));
                Permission _permission= null;   // = EMSLibrary.User.Security.Permission(_idPermission);

                //Int64 _idRoleType = Convert.ToInt64(GetKeyValue(_RdcRoleType.SelectedValue, "IdRoleType"));
                //Condesus.EMS.Business.Security.Entities.RoleType _roleType = EMSLibrary.User.Security.RoleType(_idRoleType);
                String _pkValues = String.Empty;
                //String _pageTitle = String.Empty;
                //Condesus.EMS.Business.Security.Entities.RightJobTitle _rightJobTitle = null;

                if (Entity == null)
                {
                    _permission = EMSLibrary.User.Security.Permission(_idPermission);

                    _idOrganization = Convert.ToInt64(GetKeyValue(_RtvOrganization.SelectedNode.Value, "IdOrganization"));
                    Object _obj = GetKeyValue(_RtvJobTitle.SelectedNode.Value, "IdFunctionalArea");   //Si lo saco del tree, funciona!!!.
                    //Si el key obtenido no llega a exister devuelve null.
                    _idFunctionalArea = Convert.ToInt64((_obj == null ? 0 : _obj));    //Si queda en cero 0, quiere decir que esta dando trabajando con un ROOT.
                    _obj = GetKeyValue(_RtvJobTitle.SelectedNode.Value, "IdGeographicArea");   //Si lo saco del tree, funciona!!!.
                    _idGeographicArea = Convert.ToInt64((_obj == null ? 0 : _obj));    //Si queda en cero 0, quiere decir que esta dando trabajando con un ROOT.
                    _obj = GetKeyValue(_RtvJobTitle.SelectedNode.Value, "IdPosition");   //Si lo saco del tree, funciona!!!.
                    _idPosition = Convert.ToInt64((_obj == null ? 0 : _obj));    //Si queda en cero 0, quiere decir que esta dando trabajando con un ROOT.

                    Condesus.EMS.Business.DS.Entities.Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                    Condesus.EMS.Business.GIS.Entities.GeographicArea _geoArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_idGeographicArea);
                    Condesus.EMS.Business.DS.Entities.Position _position = _organization.Position(_idPosition);
                    Condesus.EMS.Business.DS.Entities.FunctionalArea _funArea = _organization.FunctionalArea(_idFunctionalArea);
                    FunctionalPosition _funPos = _organization.FunctionalPosition(_position, _funArea);
                    GeographicFunctionalArea _geoFunArea = _organization.GeographicFunctionalArea(_funArea, _geoArea);

                    Condesus.EMS.Business.DS.Entities.JobTitle _jobTitle = _organization.JobTitle(_geoFunArea, _funPos);

                    //JobTitle _jobTitle = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization).JobTitle(_idGeographicArea, _idPosition, _idFunctionalArea);

                    switch (_ParentEntity)
                    {
                        //case Common.ConstantsEntitiesName.KC.ResourceClassification:    // "ResourceClassificationsProperties":
                        //    Entity = EMSLibrary.User.KnowledgeCollaboration.Map.ResourceClassification(_IdResourceClassification).SecurityJobTitleAdd(_jobTitle, _permission, _roleType);
                        //    //_rightJobTitle = EMSLibrary.User.KnowledgeCollaboration.Map.ResourceClassification(_IdResourceClassification).ReadJobTitleByID(_jobTitle, _permission);
                        //    base.NavigatorAddTransferVar("IdResourceClassification", _IdResourceClassification);
                        //    base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightJobTitleResourceClassification);
                        //    _pkValues = "IdResourceClassification=" + _IdResourceClassification.ToString();
                        //    break;
                        //case Common.ConstantsEntitiesName.KC.Resource:  // "ResourcesProperties":
                        //    Entity = EMSLibrary.User.KnowledgeCollaboration.Map.Resource(_IdResource).SecurityJobTitleAdd(_jobTitle, _permission, _roleType);
                        //    //_rightJobTitle = EMSLibrary.User.KnowledgeCollaboration.Map.Resource(_IdResource).ReadJobTitleByID(_jobTitle, _permission);
                        //    base.NavigatorAddTransferVar("IdResource", _IdResource);
                        //    base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightJobTitleResource);
                        //    _pkValues = "IdResource=" + _IdResource.ToString();
                        //    break;
                        //case Common.ConstantsEntitiesName.IA.ProjectClassification: // "ProjectClassificationsProperties":
                        //    Entity = EMSLibrary.User.ImprovementAction.Map.ProjectClassification(_IdProjectClassification).SecurityJobTitleAdd(_jobTitle, _permission, _roleType);
                        //    //_rightJobTitle = EMSLibrary.User.ImprovementAction.Map.ProjectClassification(_IdProjectClassification).ReadJobTitleByID(_jobTitle, _permission);
                        //    base.NavigatorAddTransferVar("IdProjectClassification", _IdProjectClassification);
                        //    base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightJobTitleProjectClassification);
                        //    _pkValues = "IdProjectClassification=" + _IdProjectClassification.ToString();
                        //    break;
                        case Common.ConstantsEntitiesName.PF.ProcessGroupProcess:   // "ProcessTest":
                            Condesus.EMS.Business.PF.Entities.Process _process = EMSLibrary.User.ProcessFramework.Map.Process(_IdProcess);
                            base.NavigatorAddTransferVar("IdProcess", _IdProcess);
                            base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightJobTitleProcess);
                            _pkValues = "IdProcess=" + _IdProcess.ToString();
                            if (_process.GetType().Name == Common.ConstantsEntitiesName.PF.ProcessGroupProcess)    //"Condesus.EMS.Business.PF.Entities.ProcessGroupProcess")
                            {
                                Entity = ((Condesus.EMS.Business.PF.Entities.ProcessGroupProcess)_process).SecurityJobTitleAdd(_jobTitle, _permission);
                                //_rightJobTitle = ((Condesus.EMS.Business.PF.Entities.ProcessGroupProcess)_process).ReadJobTitleByID(_jobTitle, _permission);
                            }
                            else
                            {
                                //TODO: Que paso???
                                //((Condesus.EMS.Business.PF.Entities.ProcessTask)_process).SecurityJobTitleAdd(_jobTitle, _permission);
                            }
                            break;
                        //case Common.ConstantsEntitiesName.PA.IndicatorClassification:   // "IndicatorClassificationsProperties":
                        //    Entity = EMSLibrary.User.PerformanceAssessments.Map.IndicatorClassification(_IdIndicatorClassification).SecurityJobTitleAdd(_jobTitle, _permission, _roleType);
                        //    //_rightJobTitle = EMSLibrary.User.PerformanceAssessments.Map.IndicatorClassification(_IdIndicatorClassification).ReadJobTitleByID(_jobTitle, _permission);
                        //    base.NavigatorAddTransferVar("IdIndicatorClassification", _IdIndicatorClassification);
                        //    base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightJobTitleIndicatorClassification);
                        //    _pkValues = "IdIndicatorClassification=" + _IdIndicatorClassification.ToString();
                        //    break;
                        //case Common.ConstantsEntitiesName.PA.Indicator: // "Indicator":
                        //    Entity = EMSLibrary.User.PerformanceAssessments.Map.Indicator(_IdIndicator).SecurityJobTitleAdd(_jobTitle, _permission, _roleType);
                        //    //_rightJobTitle = EMSLibrary.User.PerformanceAssessments.Map.Indicator(_IdIndicator).ReadJobTitleByID(_jobTitle, _permission);
                        //    base.NavigatorAddTransferVar("IdIndicator", _IdIndicator);
                        //    base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightJobTitleIndicator);
                        //    _pkValues = "IdIndicator=" + _IdIndicator.ToString();
                        //    break;
                        //case Common.ConstantsEntitiesName.PF.ProcessClassification: // "ProcessClassificationsProperties":
                        //    Entity = EMSLibrary.User.ProcessFramework.Map.ProcessClassification(_IdProcessClassification).SecurityJobTitleAdd(_jobTitle, _permission, _roleType);
                        //    //_rightJobTitle = EMSLibrary.User.ProcessFramework.Map.ProcessClassification(_IdProcessClassification).ReadJobTitleByID(_jobTitle, _permission);
                        //    base.NavigatorAddTransferVar("IdProcessClassification", _IdProcessClassification);
                        //    base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightJobTitleProcessClassification);
                        //    _pkValues = "IdProcessClassification=" + _IdProcessClassification.ToString();
                        //    break;
                        case Common.ConstantsEntitiesName.DS.Organization:  // "OrganizationProperties":
                            Entity = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganizationOrg).SecurityJobTitleAdd(_jobTitle, _permission);
                            //_rightJobTitle = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganizationOrg).ReadJobTitleByID(_jobTitle, _permission);
                            base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightJobTitleOrganization);
                            _pkValues = "IdOrganizationOrg=" + _IdOrganizationOrg.ToString();
                            break;
                        //case Common.ConstantsEntitiesName.DS.OrganizationClassification:
                        //    Entity = EMSLibrary.User.DirectoryServices.Map.OrganizationClassification(_IdOrganizationClassification).SecurityJobTitleAdd(_jobTitle, _permission, _roleType);
                        //    //_rightJobTitle = EMSLibrary.User.DirectoryServices.Map.OrganizationClassification(_IdOrganizationClassification).ReadJobTitleByID(_jobTitle, _permission);
                        //    base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightJobTitleOrganizationClassification);
                        //    _pkValues = "IdOrganizationClassification=" + _IdOrganizationClassification.ToString();
                        //    break;

                        case Condesus.EMS.Business.Common.Security.MapDS:
                            Entity = EMSLibrary.User.DirectoryServices.Map.SecurityJobTitleAdd(_jobTitle, _permission);
                            //_rightJobTitle = EMSLibrary.User.DirectoryServices.Map.ReadJobTitleByID(_jobTitle, _permission);
                            base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightJobTitleMapDS);
                            break;
                        case Condesus.EMS.Business.Common.Security.MapIA:
                            Entity = EMSLibrary.User.ImprovementAction.Map.SecurityJobTitleAdd(_jobTitle, _permission);
                            //_rightJobTitle = EMSLibrary.User.ImprovementAction.Map.ReadJobTitleByID(_jobTitle, _permission);
                            base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightJobTitleMapIA);
                            break;
                        case Condesus.EMS.Business.Common.Security.MapKC:
                            Entity = EMSLibrary.User.KnowledgeCollaboration.Map.SecurityJobTitleAdd(_jobTitle, _permission);
                            //_rightJobTitle = EMSLibrary.User.KnowledgeCollaboration.Map.ReadJobTitleByID(_jobTitle, _permission);
                            base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightJobTitleMapKC);
                            break;
                        case Condesus.EMS.Business.Common.Security.MapPA:
                            Entity = EMSLibrary.User.PerformanceAssessments.Map.SecurityJobTitleAdd(_jobTitle, _permission);
                            //_rightJobTitle = EMSLibrary.User.PerformanceAssessments.Map.ReadJobTitleByID(_jobTitle, _permission);
                            base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightJobTitleMapPA);
                            break;
                        case Condesus.EMS.Business.Common.Security.MapPF:
                            Entity = EMSLibrary.User.ProcessFramework.Map.SecurityJobTitleAdd(_jobTitle, _permission);
                            //_rightJobTitle = EMSLibrary.User.ProcessFramework.Map.ReadJobTitleByID(_jobTitle, _permission);
                            base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightJobTitleMapPF);
                            break;
                        case Condesus.EMS.Business.Common.Security.MapRM:
                            Entity = EMSLibrary.User.RiskManagement.Map.SecurityJobTitleAdd(_jobTitle, _permission);
                            //_rightJobTitle = EMSLibrary.User.RiskManagement.Map.ReadJobTitleByID(_jobTitle, _permission);
                            base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightJobTitleMapRM);
                            break;
                        case Condesus.EMS.Business.Common.Security.ConfigurationDS:
                            Entity = EMSLibrary.User.DirectoryServices.Configuration.SecurityJobTitleAdd(_jobTitle, _permission);
                            //_rightJobTitle = EMSLibrary.User.DirectoryServices.Configuration.ReadJobTitleByID(_jobTitle, _permission);
                            base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightJobTitleConfigurationDS);
                            break;
                        case Condesus.EMS.Business.Common.Security.ConfigurationIA:
                            Entity = EMSLibrary.User.ImprovementAction.Configuration.SecurityJobTitleAdd(_jobTitle, _permission);
                            //_rightJobTitle = EMSLibrary.User.ImprovementAction.Configuration.ReadJobTitleByID(_jobTitle, _permission);
                            base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightJobTitleConfigurationIA);
                            break;
                        case Condesus.EMS.Business.Common.Security.ConfigurationKC:
                            Entity = EMSLibrary.User.KnowledgeCollaboration.Configuration.SecurityJobTitleAdd(_jobTitle, _permission);
                            //_rightJobTitle = EMSLibrary.User.KnowledgeCollaboration.Configuration.ReadJobTitleByID(_jobTitle, _permission);
                            base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightJobTitleConfigurationKC);
                            break;
                        case Condesus.EMS.Business.Common.Security.ConfigurationPA:
                            Entity = EMSLibrary.User.PerformanceAssessments.Configuration.SecurityJobTitleAdd(_jobTitle, _permission);
                            //_rightJobTitle = EMSLibrary.User.PerformanceAssessments.Configuration.ReadJobTitleByID(_jobTitle, _permission);
                            base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightJobTitleConfigurationPA);
                            break;
                        case Condesus.EMS.Business.Common.Security.ConfigurationPF:
                            Entity = EMSLibrary.User.ProcessFramework.Configuration.SecurityJobTitleAdd(_jobTitle, _permission);
                            //_rightJobTitle = EMSLibrary.User.ProcessFramework.Configuration.ReadJobTitleByID(_jobTitle, _permission);
                            base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightJobTitleConfigurationPF);
                            break;
                        case Condesus.EMS.Business.Common.Security.ConfigurationRM:
                            Entity = EMSLibrary.User.RiskManagement.Configuration.SecurityJobTitleAdd(_jobTitle, _permission);
                            //_rightJobTitle = EMSLibrary.User.RiskManagement.Configuration.ReadJobTitleByID(_jobTitle, _permission);
                            base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightJobTitleConfigurationRM);
                            break;

                    }
                }
                else
                {
                    //Si el key obtenido no llega a exister devuelve null.
                    _idFunctionalArea = _IdFunctionalArea;
                    _idGeographicArea = _IdGeographicArea;
                    _idPosition = _IdPosition;
                    _idOrganization = Entity.JobTitle.Organization.IdOrganization;

                    //Condesus.EMS.Business.DS.Entities.Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                    //Condesus.EMS.Business.DS.Entities.GeographicArea _geoArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_idGeographicArea);
                    //Condesus.EMS.Business.DS.Entities.Position _position = _organization.Position(_idPosition);
                    //Condesus.EMS.Business.DS.Entities.FunctionalArea _funArea = _organization.FunctionalArea(_idFunctionalArea);
                    //FunctionalPosition _funPos = _organization.FunctionalPosition(_position, _funArea);
                    //GeographicFunctionalArea _geoFunArea = _organization.GeographicFunctionalArea(_funArea, _geoArea);
                    //JobTitle _jobTitle = _organization.JobTitle(_geoFunArea, _funPos);

                    JobTitle _jobTitle = Entity.JobTitle;
                    _permission = EMSLibrary.User.Security.Permission(_idPermission);

                    //JobTitle _jobTitle = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization).JobTitle(_idGeographicArea, _idPosition, _idFunctionalArea);

                    switch (_ParentEntity)
                    {
                        //case Common.ConstantsEntitiesName.KC.ResourceClassification:    // "ResourceClassificationsProperties":
                        //    Entity = EMSLibrary.User.KnowledgeCollaboration.Map.ResourceClassification(_IdResourceClassification).SecurityJobTitleModify(Entity, _jobTitle, _permission, _roleType);
                        //    //_rightJobTitle = EMSLibrary.User.KnowledgeCollaboration.Map.ResourceClassification(_IdResourceClassification).ReadJobTitleByID(_jobTitle, _permission);
                        //    base.NavigatorAddTransferVar("IdResourceClassification", _IdResourceClassification);
                        //    base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightJobTitleResourceClassification);
                        //    _pkValues = "IdResourceClassification=" + _IdResourceClassification.ToString();
                        //    break;
                        //case Common.ConstantsEntitiesName.KC.Resource:  // "ResourcesProperties":
                        //    Entity = EMSLibrary.User.KnowledgeCollaboration.Map.Resource(_IdResource).SecurityJobTitleModify(Entity, _jobTitle, _permission, _roleType);
                        //    //_rightJobTitle = EMSLibrary.User.KnowledgeCollaboration.Map.Resource(_IdResource).ReadJobTitleByID(_jobTitle, _permission);
                        //    base.NavigatorAddTransferVar("IdResource", _IdResource);
                        //    base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightJobTitleResource);
                        //    _pkValues = "IdResource=" + _IdResource.ToString();
                        //    break;
                        //case Common.ConstantsEntitiesName.IA.ProjectClassification: // "ProjectClassificationsProperties":
                        //    Entity = EMSLibrary.User.ImprovementAction.Map.ProjectClassification(_IdProjectClassification).SecurityJobTitleModify(Entity, _jobTitle, _permission, _roleType);
                        //    //_rightJobTitle = EMSLibrary.User.ImprovementAction.Map.ProjectClassification(_IdProjectClassification).ReadJobTitleByID(_jobTitle, _permission);
                        //    base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightJobTitleProjectClassification);
                        //    base.NavigatorAddTransferVar("IdProjectClassification", _IdProjectClassification);
                        //    _pkValues = "IdProjectClassification=" + _IdProjectClassification.ToString();
                        //    break;
                        case Common.ConstantsEntitiesName.PF.ProcessGroupProcess:   // "ProcessTest":
                            Condesus.EMS.Business.PF.Entities.Process _process = EMSLibrary.User.ProcessFramework.Map.Process(_IdProcess);
                            base.NavigatorAddTransferVar("IdProcess", _IdProcess);
                            base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightJobTitleProcess);
                            _pkValues = "IdProcess=" + _IdProcess.ToString();
                            if (_process.GetType().Name == Common.ConstantsEntitiesName.PF.ProcessGroupProcess)
                            {
                                Entity = ((Condesus.EMS.Business.PF.Entities.ProcessGroupProcess)_process).SecurityJobTitleModify(Entity, _jobTitle, _permission);
                                //_rightJobTitle = ((Condesus.EMS.Business.PF.Entities.ProcessGroupProcess)_process).ReadJobTitleByID(_jobTitle, _permission);
                            }
                            else
                            {
                                //TODO: Que paso???
                                //((Condesus.EMS.Business.PF.Entities.ProcessTask)_process).SecurityJobTitleModify(_jobTitle, _permission);
                            }
                            break;
                        //case Common.ConstantsEntitiesName.PA.IndicatorClassification:   // "IndicatorClassificationsProperties":
                        //    Entity = EMSLibrary.User.PerformanceAssessments.Map.IndicatorClassification(_IdIndicatorClassification).SecurityJobTitleModify(Entity, _jobTitle, _permission, _roleType);
                        //    //_rightJobTitle = EMSLibrary.User.PerformanceAssessments.Map.IndicatorClassification(_IdIndicatorClassification).ReadJobTitleByID(_jobTitle, _permission);
                        //    base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightJobTitleIndicatorClassification);
                        //    base.NavigatorAddTransferVar("IdIndicatorClassification", _IdIndicatorClassification);
                        //    _pkValues = "IdIndicatorClassification=" + _IdIndicatorClassification.ToString();
                        //    break;
                        //case Common.ConstantsEntitiesName.PA.Indicator: // "Indicator":
                        //    Entity = EMSLibrary.User.PerformanceAssessments.Map.Indicator(_IdIndicator).SecurityJobTitleModify(Entity, _jobTitle, _permission, _roleType);
                        //    //_rightJobTitle = EMSLibrary.User.PerformanceAssessments.Map.Indicator(_IdIndicator).ReadJobTitleByID(_jobTitle, _permission);
                        //    base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightJobTitleIndicator);
                        //    base.NavigatorAddTransferVar("IdIndicator", _IdIndicator);
                        //    _pkValues = "IdIndicator=" + _IdIndicator.ToString();
                        //    break;
                        //case Common.ConstantsEntitiesName.PF.ProcessClassification: // "ProcessClassificationsProperties":
                        //    Entity = EMSLibrary.User.ProcessFramework.Map.ProcessClassification(_IdProcessClassification).SecurityJobTitleModify(Entity, _jobTitle, _permission, _roleType);
                        //    //_rightJobTitle = EMSLibrary.User.ProcessFramework.Map.ProcessClassification(_IdProcessClassification).ReadJobTitleByID(_jobTitle, _permission);
                        //    base.NavigatorAddTransferVar("IdProcessClassification", _IdProcessClassification);
                        //    base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightJobTitleProcessClassification);
                        //    _pkValues = "IdProcessClassification=" + _IdProcessClassification.ToString();
                        //    break;
                        case Common.ConstantsEntitiesName.DS.Organization:  // "OrganizationProperties":
                            Entity = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganizationOrg).SecurityJobTitleModify(Entity, _jobTitle, _permission);
                            //_rightJobTitle = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganizationOrg).ReadJobTitleByID(_jobTitle, _permission);
                            base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightJobTitleOrganization);
                            _pkValues = "IdOrganizationOrg=" + _IdOrganizationOrg.ToString();
                            break;
                        //case Common.ConstantsEntitiesName.DS.OrganizationClassification:
                        //    Entity = EMSLibrary.User.DirectoryServices.Map.OrganizationClassification(_IdOrganizationClassification).SecurityJobTitleModify(Entity, _jobTitle, _permission, _roleType);
                        //    //_rightJobTitle = EMSLibrary.User.DirectoryServices.Map.OrganizationClassification(_IdOrganizationClassification).ReadJobTitleByID(_jobTitle, _permission);
                        //    base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightJobTitleOrganizationClassification);
                        //    _pkValues = "IdOrganizationClassification=" + _IdOrganizationClassification.ToString();
                        //    break;

                        case Condesus.EMS.Business.Common.Security.MapDS:
                            Entity = EMSLibrary.User.DirectoryServices.Map.SecurityJobTitleModify(Entity, _jobTitle, _permission);
                            //_rightJobTitle = EMSLibrary.User.DirectoryServices.Map.ReadJobTitleByID(_jobTitle, _permission);
                            base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightJobTitleMapDS);
                            break;
                        case Condesus.EMS.Business.Common.Security.MapIA:
                            Entity = EMSLibrary.User.ImprovementAction.Map.SecurityJobTitleModify(Entity, _jobTitle, _permission);
                            //_rightJobTitle = EMSLibrary.User.ImprovementAction.Map.ReadJobTitleByID(_jobTitle, _permission);
                            base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightJobTitleMapIA);
                            break;
                        case Condesus.EMS.Business.Common.Security.MapKC:
                            Entity = EMSLibrary.User.KnowledgeCollaboration.Map.SecurityJobTitleModify(Entity, _jobTitle, _permission);
                            //_rightJobTitle = EMSLibrary.User.KnowledgeCollaboration.Map.ReadJobTitleByID(_jobTitle, _permission);
                            base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightJobTitleMapKC);
                            break;
                        case Condesus.EMS.Business.Common.Security.MapPA:
                            Entity = EMSLibrary.User.PerformanceAssessments.Map.SecurityJobTitleModify(Entity, _jobTitle, _permission);
                            //_rightJobTitle = EMSLibrary.User.PerformanceAssessments.Map.ReadJobTitleByID(_jobTitle, _permission);
                            base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightJobTitleMapPA);
                            break;
                        case Condesus.EMS.Business.Common.Security.MapPF:
                            Entity = EMSLibrary.User.ProcessFramework.Map.SecurityJobTitleModify(Entity, _jobTitle, _permission);
                            //_rightJobTitle = EMSLibrary.User.ProcessFramework.Map.ReadJobTitleByID(_jobTitle, _permission);
                            base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightJobTitleMapPF);
                            break;
                        case Condesus.EMS.Business.Common.Security.MapRM:
                            Entity = EMSLibrary.User.RiskManagement.Map.SecurityJobTitleModify(Entity, _jobTitle, _permission);
                            //_rightJobTitle = EMSLibrary.User.RiskManagement.Map.ReadJobTitleByID(_jobTitle, _permission);
                            base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightJobTitleMapRM);
                            break;
                        case Condesus.EMS.Business.Common.Security.ConfigurationDS:
                            Entity = EMSLibrary.User.DirectoryServices.Configuration.SecurityJobTitleModify(Entity, _jobTitle, _permission);
                            //_rightJobTitle = EMSLibrary.User.DirectoryServices.Configuration.ReadJobTitleByID(_jobTitle, _permission);
                            base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightJobTitleConfigurationDS);
                            break;
                        case Condesus.EMS.Business.Common.Security.ConfigurationIA:
                            Entity = EMSLibrary.User.ImprovementAction.Configuration.SecurityJobTitleModify(Entity, _jobTitle, _permission);
                            //_rightJobTitle = EMSLibrary.User.ImprovementAction.Configuration.ReadJobTitleByID(_jobTitle, _permission);
                            base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightJobTitleConfigurationIA);
                            break;
                        case Condesus.EMS.Business.Common.Security.ConfigurationKC:
                            Entity = EMSLibrary.User.KnowledgeCollaboration.Configuration.SecurityJobTitleModify(Entity, _jobTitle, _permission);
                            //_rightJobTitle = EMSLibrary.User.KnowledgeCollaboration.Configuration.ReadJobTitleByID(_jobTitle, _permission);
                            base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightJobTitleConfigurationKC);
                            break;
                        case Condesus.EMS.Business.Common.Security.ConfigurationPA:
                            Entity = EMSLibrary.User.PerformanceAssessments.Configuration.SecurityJobTitleModify(Entity, _jobTitle, _permission);
                            //_rightJobTitle = EMSLibrary.User.PerformanceAssessments.Configuration.ReadJobTitleByID(_jobTitle, _permission);
                            base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightJobTitleConfigurationPA);
                            break;
                        case Condesus.EMS.Business.Common.Security.ConfigurationPF:
                            Entity = EMSLibrary.User.ProcessFramework.Configuration.SecurityJobTitleModify(Entity, _jobTitle, _permission);
                            //_rightJobTitle = EMSLibrary.User.ProcessFramework.Configuration.ReadJobTitleByID(_jobTitle, _permission);
                            base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightJobTitleConfigurationPF);
                            break;
                        case Condesus.EMS.Business.Common.Security.ConfigurationRM:
                            Entity = EMSLibrary.User.RiskManagement.Configuration.SecurityJobTitleModify(Entity, _jobTitle, _permission);
                            //_rightJobTitle = EMSLibrary.User.RiskManagement.Configuration.ReadJobTitleByID(_jobTitle, _permission);
                            base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightJobTitleConfigurationRM);
                            break;
                    }
                }


                base.NavigatorAddTransferVar("IdPermission", _idPermission);
                //base.NavigatorAddTransferVar("IdOwnerObject", Entity.Permission.OwnerPermission.IdObject);
                //base.NavigatorAddTransferVar("OwnerClassName", Entity.Permission.OwnerPermission.ClassName);
                //base.NavigatorAddTransferVar("IdOwnerObject", _rightJobTitle.Permission.OwnerPermission.IdObject);
                //base.NavigatorAddTransferVar("OwnerClassName", _rightJobTitle.Permission.OwnerPermission.ClassName);
                base.NavigatorAddTransferVar("ParentEntity", _ParentEntity);
                //+ Posts..
                base.NavigatorAddTransferVar("IdOrganization", _idOrganization);
                base.NavigatorAddTransferVar("IdFunctionalArea", _idFunctionalArea);
                base.NavigatorAddTransferVar("IdGeographicArea", _idGeographicArea);
                base.NavigatorAddTransferVar("IdPosition", _idPosition);

                _pkValues += "& IdFunctionalArea=" + _idFunctionalArea.ToString()
                    + "& IdGeographicArea=" + _idGeographicArea.ToString()
                    + "& IdPosition=" + _idPosition.ToString()
                    + "& IdPermission=" + _idPermission.ToString()
                    //+ "& IdOwnerObject=" + Entity.Permission.OwnerPermission.IdObject.ToString()
                    //+ "& OwnerClassName=" + Entity.Permission.OwnerPermission.ClassName
                    + "& ParentEntity=" + _ParentEntity.ToString();

                base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);
                //Navigate("~/MainInfo/ListViewer.aspx", _PageTitle, Condesus.WebUI.Navigation.DeleteType.DeleteModifiedEntity);
                String _entityPropertyName = String.Concat(Entity.JobTitle.Name());
                NavigatePropertyEntity(Entity.GetType().Name, _entityPropertyName, Condesus.WebUI.Navigation.NavigateMenuAction.Add);


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