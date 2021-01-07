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
using Condesus.EMS.Business.PF.Entities;

namespace Condesus.EMS.WebUI.ManagementTools.ProcessesMap
{
    public partial class ParticipationProperties : BaseProperties
    {
        #region Internal Properties

        private Int64 _IdOrganization2
        {
            get
            {
                return base.NavigatorContainsTransferVar("IdOrganization") ? base.NavigatorGetTransferVar<Int64>("IdOrganization") : Convert.ToInt64(GetPKfromNavigator("IdOrganization"));
            }
        }
        private Int64 _IdParticipationType
        {
            get
            {
                return base.NavigatorContainsTransferVar("IdParticipationType") ? base.NavigatorGetTransferVar<Int64>("IdParticipationType") : 0;
            }
        }
        private Int64 _IdProject
        {
            get
            {
                return base.NavigatorContainsTransferVar("IdProcess") ? base.NavigatorGetTransferVar<Int64>("IdProcess") : Convert.ToInt64(GetPKfromNavigator("IdProcess"));
            }
        }
        private Boolean _ProcessParticipation
        {
            get
            {
                return base.NavigatorContainsTransferVar("ProcessParticipation") ? true : false;
            }
        }

        private ProcessParticipation _Entity = null;
        private ProcessParticipation Entity
        {
            get
            {
                try
                {
                    if (_Entity == null)
                    {
                        _Entity = ((Condesus.EMS.Business.PF.Entities.ProcessGroupProcess)EMSLibrary.User.ProcessFramework.Map.Process(_IdProject)).ProcessParticipation(_IdOrganization2, _IdParticipationType);
                    }

                    return _Entity;
                }
                catch { return null; }
            }

            set { _Entity = value; }
        }

        private RadComboBox _RdcParticipationType;
        private CompareValidator _CvParticipationType;
        
        private RadComboBox _RdcOrganization;
        private RadTreeView _RtvOrganization;
        private CompareValidator _CvOrganization;

        private RadComboBox _RdcProject;
        private RadTreeView _RtvProject;
        private CompareValidator _CvProject;

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
                        Add();
                    else
                        LoadData(); //Edit.

                    //Form
                    base.SetContentTableRowsCss(tblContentForm);
                    this.txtComment.Focus();
                }
            }
            protected override void SetPagetitle()
            {
                base.PageTitle = (Entity != null) ? Entity.Organization.Name + " - " + Entity.ParticipationType.LanguageOption.Name : Resources.CommonListManage.Participation;
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }
        #endregion

        #region Private Methods
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonListManage.Participation;
                lblComment.Text = Resources.CommonListManage.Comment;
                lblOrganization.Text = Resources.CommonListManage.Organization;
                lblProject.Text = Resources.CommonListManage.Process;
                lblRelationshipType.Text = Resources.CommonListManage.ParticipationType;
            }
            private void AddCombos()
            {
                AddComboParticipationTypes();
                if (_ProcessParticipation)
                {
                    AddComboProjects();
                }
                else
                {
                    if (_IdProject > 0)
                    {
                        AddComboOrganizations();
                    }
                    else
                    {
                        AddComboProjects();
                    }
                }
            }
            private void AddComboParticipationTypes()
            {
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                String _selectedValue = String.Empty;
                AddCombo(phParticipationType, ref _RdcParticipationType, Common.ConstantsEntitiesName.PF.ParticipationTypes, _selectedValue, _params, false, true, false, false, false);
                ValidatorRequiredField(Common.ConstantsEntitiesName.PF.ParticipationTypes, phParticipationTypeValidator, ref _CvParticipationType, _RdcParticipationType, Resources.ConstantMessage.SelectAParticipationType);
            }
            private void AddComboOrganizations()
            {
                lblOrganization.Visible = true;
                String _filterExpression = String.Empty;
                //Combo de Organizaciones
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                AddComboWithTreeElementMaps(ref phOrganization, ref _RdcOrganization, ref _RtvOrganization,
                    Common.ConstantsEntitiesName.DS.OrganizationClassifications, Common.ConstantsEntitiesName.DS.OrganizationsRoots, _params, false, true, false, ref _filterExpression,
                    new RadTreeViewEventHandler(rtvOrganizations_NodeExpand),
                    Resources.Common.ComboBoxNoDependency, false);

                ValidatorRequiredField(Common.ConstantsEntitiesName.DS.OrganizationClassifications, phOrganizationValidator, ref _CvOrganization, _RdcOrganization, Resources.ConstantMessage.SelectAnOrganization);

            }
            private void AddComboProjects()
            {
                lblProject.Visible = true;
                String _filterExpression = String.Empty;
                //Combo de Organizaciones
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                AddComboWithTreeElementMaps(ref phProject, ref _RdcProject, ref _RtvProject,
                    Common.ConstantsEntitiesName.PF.ProcessClassifications, Common.ConstantsEntitiesName.PF.ProcessGroupProcessesRoots, _params, false, true, false, ref _filterExpression,
                    new RadTreeViewEventHandler(rtvProjects_NodeExpand),
                    Resources.Common.ComboBoxNoDependency, false);

                ValidatorRequiredField(Common.ConstantsEntitiesName.PF.ProcessClassifications, phProjectValidator, ref _CvProject, _RdcProject, Resources.ConstantMessage.SelectAProject);

            }
            private void Add()
            {
                base.StatusBar.Clear();

                //Inicializa el Formulario
                txtComment.Text = String.Empty;
                _RdcParticipationType.SelectedValue = Common.Constants.ComboBoxSelectItemValue;
                if (_IdProject > 0)
                {
                    _RdcOrganization.SelectedValue = Common.Constants.ComboBoxSelectItemValue;
                }
                else
                {
                    _RdcProject.SelectedValue = Common.Constants.ComboBoxSelectItemValue;
                }
            }
            private void LoadData()
            {
                //carga los datos en pantalla
                _RdcParticipationType.SelectedValue = "IdParticipationType=" + Entity.ParticipationType.IdParticipationType.ToString();
                if (_ProcessParticipation)
                {
                    _RdcProject.SelectedValue = "IdProcess=" + Entity.IdProcess.ToString();
                }
                else
                {
                    _RdcOrganization.SelectedValue = "IdOrganization=" + Entity.Organization.IdOrganization.ToString();
                }
                txtComment.Text = Entity.Comment;
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
            protected void rtvProjects_NodeExpand(object sender, RadTreeNodeEventArgs e)
            {
                //Limpio los hijos, para no duplicar al abrir y cerrar.
                e.Node.Nodes.Clear();
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                _params = GetKeyValues(e.Node.Value);

                //Primero lo hace sobre las Clasificaciones Hijas...
                //Actualiza el Data table con los hijos, para poder asociarlas al nodo expadido.
                BuildGenericDataTable(Common.ConstantsEntitiesName.PF.ProcessClassificationChildren, _params);
                if (DataTableListManage.ContainsKey(Common.ConstantsEntitiesName.PF.ProcessClassificationChildren))
                {
                    foreach (DataRow _drRecord in DataTableListManage[Common.ConstantsEntitiesName.PF.ProcessClassificationChildren].Rows)
                    {
                        RadTreeNode _node = SetElementMapsNodeTreeView(_drRecord, Common.ConstantsEntitiesName.PF.ProcessClassificationChildren, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, String.Empty, String.Empty, String.Empty);
                        e.Node.Nodes.Add(_node);
                        //Como es un Mapa de Elementos, indica true para que cargue los elementos debajo de la classificacion.
                        SetExpandMode(_node, Common.ConstantsEntitiesName.PF.ProcessClassificationChildren, true, false);
                    }
                }

                //Ahora lo hace sobre los Elementos Hijos de la Clasificacion expandida.
                BuildGenericDataTable(Common.ConstantsEntitiesName.PF.ProcessGroupProcesses, _params);
                if (DataTableListManage.ContainsKey(Common.ConstantsEntitiesName.PF.ProcessGroupProcesses))
                {
                    foreach (DataRow _drRecord in DataTableListManage[Common.ConstantsEntitiesName.PF.ProcessGroupProcesses].Rows)
                    {
                        RadTreeNode _node = SetElementMapsNodeTreeView(_drRecord, Common.ConstantsEntitiesName.PF.ProcessGroupProcesses, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, String.Empty, String.Empty, String.Empty);
                        e.Node.Nodes.Add(_node);
                    }
                }
            }

            protected void btnSave_Click(object sender, EventArgs e)
            {
                try
                {
                    Int64 _idOrganization2;
                    Int64 _idProject;
                    Int64 _idParticipationType = Convert.ToInt64(GetKeyValue(_RdcParticipationType.SelectedValue, "IdParticipationType"));

                    if (_ProcessParticipation)
                    {
                        _idProject = Convert.ToInt64(GetKeyValue(_RtvProject.SelectedNode.Value, "IdProcess"));
                        _idOrganization2 = _IdOrganization2;
                    }
                    else
                    {
                        if (_IdProject > 0)
                        {
                            _idOrganization2 = Convert.ToInt64(GetKeyValue(_RtvOrganization.SelectedNode.Value, "IdOrganization"));
                            _idProject = _IdProject;
                        }
                        else
                        {
                            _idProject = Convert.ToInt64(GetKeyValue(_RtvProject.SelectedNode.Value, "IdProcess"));
                            _idOrganization2 = _IdOrganization2;
                        }
                    }

                    if (Entity == null)
                    {
                        //Alta
                        Entity = ((Condesus.EMS.Business.PF.Entities.ProcessGroupProcess)EMSLibrary.User.ProcessFramework.Map.Process(_idProject)).ProcessParticipationAdd(_idOrganization2, _idParticipationType, txtComment.Text);
                    }
                    else
                    {
                        //Modificacion
                        ((Condesus.EMS.Business.PF.Entities.ProcessGroupProcess)EMSLibrary.User.ProcessFramework.Map.Process(_IdProject)).ProcessParticipation(_idOrganization2, _idParticipationType).Modify(txtComment.Text);
                    }
                    base.NavigatorAddTransferVar("IdParticipationType", Entity.ParticipationType.IdParticipationType);
                    base.NavigatorAddTransferVar("IdProcess", Entity.ProcessGroupProcess.IdProcess);
                    base.NavigatorAddTransferVar("IdOrganization", Entity.Organization.IdOrganization);

                    String _pkValues = "IdParticipationType=" + Entity.ParticipationType.IdParticipationType.ToString()
                            + "& IdProcess=" + Entity.ProcessGroupProcess.IdProcess.ToString()
                            + "& IdOrganization=" + Entity.Organization.IdOrganization.ToString();
                    base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);
                    base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.PF.ProcessParticipation);
                    base.NavigatorAddTransferVar("EntityNameContextInfo", base.NavigatorGetTransferVar<String>("EntityNameContextInfo"));
                    //Navigate("~/MainInfo/ListViewer.aspx", Resources.CommonListManage.ProcessParticipation + " " + Entity.Organization.Name + " - " + Entity.ParticipationType.LanguageOption.Name, Condesus.WebUI.Navigation.DeleteType.DeleteModifiedEntity);
                    String _entityPropertyName = String.Concat(Entity.Organization.CorporateName, " ", Entity.ParticipationType.LanguageOption.Name, " ", Entity.ProcessGroupProcess.LanguageOption.Title);
                    NavigatePropertyEntity(Common.ConstantsEntitiesName.PF.ProcessParticipation, _entityPropertyName, Condesus.WebUI.Navigation.NavigateMenuAction.Add);

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