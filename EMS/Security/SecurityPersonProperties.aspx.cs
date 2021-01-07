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
    public partial class SecurityPersonProperties : BaseProperties
    {
        #region Internal Properties
        String _PageTitle = String.Empty;
        CompareValidator _CvOrganization;
        CompareValidator _CvPerson;
        CompareValidator _CvPermission;
        private RadComboBox _RdcPerson;
        private RadComboBox _RdcPermission;
        private RadComboBox _RdcOrganization;
        private RadTreeView _RtvOrganization;
        private String _ParentEntity
        {
            get
            {
                return base.NavigatorContainsTransferVar("ParentEntity") ? Convert.ToString(base.NavigatorGetTransferVar<Object>("ParentEntity")) : Convert.ToString(GetPKfromNavigator("ParentEntity"));
            }
        }
        private Int64 _IdOrganization
        {
            get
            {
                return base.NavigatorContainsTransferVar("IdOrganization") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdOrganization")) : Convert.ToInt64(GetPKfromNavigator("IdOrganization"));
            }
        }
        private Int64 _IdPerson
        {
            get
            {
                return base.NavigatorContainsTransferVar("IdPerson") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdPerson")) : Convert.ToInt64(GetPKfromNavigator("IdPerson"));
            }
        }
        private Int64 _IdPermission
        {
            get
            {
                return base.NavigatorContainsTransferVar("IdPermission") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdPermission")) : Convert.ToInt64(GetPKfromNavigator("IdPermission"));
            }
        }
        private Int64 _IdOrganizationOrg
        {
            get
            {
                return base.NavigatorContainsTransferVar("IdOrganizationOrg") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdOrganizationOrg")) : Convert.ToInt64(GetPKfromNavigator("IdOrganization"));
            }
        }
        private Int64 _IdProcess
        {
            get
            {
                return base.NavigatorContainsTransferVar("IdProcess") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdProcess")) : Convert.ToInt64(GetPKfromNavigator("IdProcess"));
            }
        }

        private RightPerson _Entity = null;
        private RightPerson Entity
        {
            get
            {
                try
                {
                    if ((_Entity == null) || (_Entity.Person == null))
                    {
                        Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization);
                        Person _person = _organization.Person(_IdPerson);
                        Permission _permission = EMSLibrary.User.Security.Permission(_IdPermission);

                        switch (_ParentEntity)
                        {
                            #region Seguridad por Entidad (Process-Organization)
                            case Common.ConstantsEntitiesName.PF.ProcessGroupProcess:   // "Process":
                                Condesus.EMS.Business.PF.Entities.Process _process = EMSLibrary.User.ProcessFramework.Map.Process(_IdProcess);
                                if (_process.GetType().Name == Common.ConstantsEntitiesName.PF.ProcessGroupProcess)
                                {
                                    _Entity = ((Condesus.EMS.Business.PF.Entities.ProcessGroupProcess)_process).ReadPersonByID(_person, _permission);
                                }
                                break;
                            case Common.ConstantsEntitiesName.DS.Organization:  // "OrganizationProperties":
                                _Entity = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganizationOrg).ReadPersonByID(_person, _permission);
                                break;
                            #endregion

                            #region Seguridad por Mapa
                            case Condesus.EMS.Business.Common.Security.MapDS:
                                _Entity = EMSLibrary.User.DirectoryServices.Map.ReadPersonByID(_person, _permission);
                                break;
                            case Condesus.EMS.Business.Common.Security.MapIA:
                                _Entity = EMSLibrary.User.ImprovementAction.Map.ReadPersonByID(_person, _permission);
                                break;
                            case Condesus.EMS.Business.Common.Security.MapKC:
                                _Entity = EMSLibrary.User.KnowledgeCollaboration.Map.ReadPersonByID(_person, _permission);
                                break;
                            case Condesus.EMS.Business.Common.Security.MapPA:
                                _Entity = EMSLibrary.User.PerformanceAssessments.Map.ReadPersonByID(_person, _permission);
                                break;
                            case Condesus.EMS.Business.Common.Security.MapPF:
                                _Entity = EMSLibrary.User.ProcessFramework.Map.ReadPersonByID(_person, _permission);
                                break;
                            case Condesus.EMS.Business.Common.Security.MapRM:
                                _Entity = EMSLibrary.User.RiskManagement.Map.ReadPersonByID(_person, _permission);
                                break;
                            #endregion

                            #region Seguridad por Config
                            case Condesus.EMS.Business.Common.Security.ConfigurationDS:
                                _Entity = EMSLibrary.User.DirectoryServices.Configuration.ReadPersonByID(_person, _permission);
                                break;
                            case Condesus.EMS.Business.Common.Security.ConfigurationIA:
                                _Entity = EMSLibrary.User.ImprovementAction.Configuration.ReadPersonByID(_person, _permission);
                                break;
                            case Condesus.EMS.Business.Common.Security.ConfigurationKC:
                                _Entity = EMSLibrary.User.KnowledgeCollaboration.Configuration.ReadPersonByID(_person, _permission);
                                break;
                            case Condesus.EMS.Business.Common.Security.ConfigurationPA:
                                _Entity = EMSLibrary.User.PerformanceAssessments.Configuration.ReadPersonByID(_person, _permission);
                                break;
                            case Condesus.EMS.Business.Common.Security.ConfigurationPF:
                                _Entity = EMSLibrary.User.ProcessFramework.Configuration.ReadPersonByID(_person, _permission);
                                break;
                            case Condesus.EMS.Business.Common.Security.ConfigurationRM:
                                _Entity = EMSLibrary.User.RiskManagement.Configuration.ReadPersonByID(_person, _permission);
                                break;
                            #endregion
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
            //Inicializo el Form
            if ((_Entity == null) || (_Entity.Person == null))
            { Add(); }
            else
            { LoadData(); } //Edit.

            //Form
            base.SetContentTableRowsCss(tblContentForm);
        }
        protected override void SetPagetitle()
        {
            try
            {
                if ((_ParentEntity.Contains("Map")) || (_ParentEntity.Contains("Config")))
                {
                    _PageTitle = GetGlobalResourceObject("CommonListManage", "RightPerson" + _ParentEntity.Replace("Configuration", "Config")).ToString();
                }
                else
                {
                    _PageTitle = _ParentEntity + " : " + Resources.CommonListManage.RightPerson;
                }
                base.PageTitle = (Entity != null) ? Entity.Person.FullName : _PageTitle;
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
            Page.Title = Resources.CommonListManage.SecurityByPosts;
            lblOrganization2.Text = Resources.CommonListManage.Organization;
            lblPermission.Text = Resources.CommonListManage.Permission;
            lblPerson.Text = Resources.CommonListManage.Person;
        }
        private void AddCombos()
        {
            AddComboOrganizations();
            AddComboPeople();
            AddComboPermissions();
        }
        private void AddComboPeople()
        {
            Dictionary<String, Object> _params = new Dictionary<String, Object>();
            if (_RtvOrganization.SelectedNode != null)
            {
                _params = GetKeyValues(_RtvOrganization.SelectedNode.Value);
            }
            AddCombo(phPerson, ref _RdcPerson, Common.ConstantsEntitiesName.DS.People, String.Empty, _params, false, true, false, false, false);
            ValidatorRequiredField(Common.ConstantsEntitiesName.DS.People, phPersonValidator, ref _CvPerson, _RdcPerson, Resources.ConstantMessage.SelectAPerson);
        }
        private void AddComboPermissions()
        {
            Dictionary<String, Object> _params = new Dictionary<String, Object>();
            AddCombo(phPermission, ref _RdcPermission, Common.ConstantsEntitiesName.SS.Permissions, String.Empty, _params, false, true, false, false, false);
            ValidatorRequiredField(Common.ConstantsEntitiesName.SS.Permissions, phPermissionValidator, ref _CvPermission, _RdcPermission, Resources.ConstantMessage.SelectAPermission);
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

            ValidatorRequiredField(Common.ConstantsEntitiesName.DS.OrganizationClassifications, phOrganizationValidator, ref _CvOrganization, _RdcOrganization, Resources.ConstantMessage.SelectAnOrganization);
        }
        private void LoadData()
        {
            SetPermission();
            SetPerson();
            SetOrganization();

            _RdcOrganization.Enabled = false;
            _RdcPerson.Enabled = false;
        }
        private void SetPermission()
        {
            _RdcPermission.SelectedValue = "IdPermission=" + Entity.Permission.IdPermission.ToString();
        }
        private void SetPerson()
        {
            _RdcPerson.Style.Add("display", "none");
            _CvPerson.EnableClientScript = false;

            Label _lblPerson = new Label();
            _lblPerson.Text = Entity.Person.FullName;
            phPerson.Controls.Add(_lblPerson);
        }
        private void SetOrganization()
        {
            _RdcOrganization.Style.Add("display", "none");
            _CvOrganization.EnableClientScript = false;

            Label _lblOrganization = new Label();
            _lblOrganization.Text = Entity.Person.Organization.CorporateName;
            phOrganization.Controls.Add(_lblOrganization);
        }
        private void Add()
        {
            base.StatusBar.Clear();
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
            protected void rtvOrganizations_NodeClick(object sender, RadTreeNodeEventArgs e)
            {
                AddComboPeople();
            }
            protected void btnSave_Click(object sender, EventArgs e)
            {
                try
                {
                    Object _obj = null;
                    Int64 _idOrganization;
                    Int64 _idPerson;
                    Condesus.EMS.Business.DS.Entities.Post _post = null;
                    Int64 _idPermission;
                    Permission _permission;
                    String _pkValues = String.Empty;

                    _idPermission = Convert.ToInt64(GetKeyValue(_RdcPermission.SelectedValue, "IdPermission"));
                    _permission = EMSLibrary.User.Security.Permission(_idPermission);
                    Person _person;

                    //Si es un alta mira los combos, sino lee los id del entity.
                    if ((_Entity == null) || (_Entity.Person == null))
                    {
                        if (_RtvOrganization.SelectedNode != null)
                        {
                            _obj = GetKeyValue(_RtvOrganization.SelectedNode.Value, "IdOrganization");   //Si lo saco del tree, funciona!!!.
                        }
                        _idOrganization = Convert.ToInt64((_obj == null ? 0 : _obj));    //Si queda en cero 0, quiere decir que esta dando trabajando con un ROOT.
                        _idPerson = Convert.ToInt64(GetKeyValue(_RdcPerson.SelectedValue, "IdPerson"));

                        Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                        _person = _organization.Person(_idPerson);
                    }
                    else
                    {
                        _idOrganization = Entity.Person.Organization.IdOrganization;
                        _idPerson = Entity.Person.IdPerson;
                        _person = Entity.Person;
                    }


                    if ((_Entity == null) || (_Entity.Person == null))
                    {   //ES UN ADD
                        switch (_ParentEntity)
                        {
                            #region ADD Seguridad por Entidad (Process-Organization)
                            case Common.ConstantsEntitiesName.PF.ProcessGroupProcess:   // "Process":
                                Condesus.EMS.Business.PF.Entities.Process _process = EMSLibrary.User.ProcessFramework.Map.Process(_IdProcess);
                                base.NavigatorAddTransferVar("IdProcess", _IdProcess);
                                base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightPersonProcess);
                                _pkValues = "IdProcess=" + _IdProcess.ToString();
                                if (_process.GetType().Name == Common.ConstantsEntitiesName.PF.ProcessGroupProcess)
                                {
                                    Entity = ((Condesus.EMS.Business.PF.Entities.ProcessGroupProcess)_process).SecurityPersonAdd(_person, _permission);
                                }
                                break;
                            case Common.ConstantsEntitiesName.DS.Organization:  // "OrganizationProperties":
                                Entity = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganizationOrg).SecurityPersonAdd(_person, _permission);
                                base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightPersonOrganization);
                                _pkValues = "IdOrganizationOrg=" + _IdOrganizationOrg.ToString();
                                break;
                            #endregion

                            #region ADD Seguridad por Mapa
                            case Condesus.EMS.Business.Common.Security.MapDS:
                                Entity = EMSLibrary.User.DirectoryServices.Map.SecurityPersonAdd(_person, _permission);
                                base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightPersonMapDS);
                                break;
                            case Condesus.EMS.Business.Common.Security.MapIA:
                                Entity = EMSLibrary.User.ImprovementAction.Map.SecurityPersonAdd(_person, _permission);
                                base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightPersonMapIA);
                                break;
                            case Condesus.EMS.Business.Common.Security.MapKC:
                                Entity = EMSLibrary.User.KnowledgeCollaboration.Map.SecurityPersonAdd(_person, _permission);
                                base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightPersonMapKC);
                                break;
                            case Condesus.EMS.Business.Common.Security.MapPA:
                                Entity = EMSLibrary.User.PerformanceAssessments.Map.SecurityPersonAdd(_person, _permission);
                                base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightPersonMapPA);
                                break;
                            case Condesus.EMS.Business.Common.Security.MapPF:
                                Entity = EMSLibrary.User.ProcessFramework.Map.SecurityPersonAdd(_person, _permission);
                                base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightPersonMapPF);
                                break;
                            case Condesus.EMS.Business.Common.Security.MapRM:
                                Entity = EMSLibrary.User.RiskManagement.Map.SecurityPersonAdd(_person, _permission);
                                base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightPersonMapRM);
                                break;
                            #endregion

                            #region ADD Seguridad por Config
                            case Condesus.EMS.Business.Common.Security.ConfigurationDS:
                                Entity = EMSLibrary.User.DirectoryServices.Configuration.SecurityPersonAdd(_person, _permission);
                                base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightPersonConfigurationDS);
                                break;
                            case Condesus.EMS.Business.Common.Security.ConfigurationIA:
                                Entity = EMSLibrary.User.ImprovementAction.Configuration.SecurityPersonAdd(_person, _permission);
                                base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightPersonConfigurationIA);
                                break;
                            case Condesus.EMS.Business.Common.Security.ConfigurationKC:
                                Entity = EMSLibrary.User.KnowledgeCollaboration.Configuration.SecurityPersonAdd(_person, _permission);
                                base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightPersonConfigurationKC);
                                break;
                            case Condesus.EMS.Business.Common.Security.ConfigurationPA:
                                Entity = EMSLibrary.User.PerformanceAssessments.Configuration.SecurityPersonAdd(_person, _permission);
                                base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightPersonConfigurationPA);
                                break;
                            case Condesus.EMS.Business.Common.Security.ConfigurationPF:
                                Entity = EMSLibrary.User.ProcessFramework.Configuration.SecurityPersonAdd(_person, _permission);
                                base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightPersonConfigurationPF);
                                break;
                            case Condesus.EMS.Business.Common.Security.ConfigurationRM:
                                Entity = EMSLibrary.User.RiskManagement.Configuration.SecurityPersonAdd(_person, _permission);
                                base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightPersonConfigurationRM);
                                break;
                            #endregion
                        }
                    }
                    else
                    {
                        //en el modify
                        _permission = EMSLibrary.User.Security.Permission(_idPermission);

                        switch (_ParentEntity)
                        {
                            #region MODIFY Seguridad por Entidad (Process-Organization)
                            case Common.ConstantsEntitiesName.PF.ProcessGroupProcess:   // "ProcessTest":
                                Condesus.EMS.Business.PF.Entities.Process _process = EMSLibrary.User.ProcessFramework.Map.Process(_IdProcess);
                                base.NavigatorAddTransferVar("IdProcess", _IdProcess);
                                _pkValues = "IdProcess=" + _IdProcess.ToString();
                                base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightPersonProcess);
                                if (_process.GetType().Name == Common.ConstantsEntitiesName.PF.ProcessGroupProcess)
                                {
                                    Entity = ((Condesus.EMS.Business.PF.Entities.ProcessGroupProcess)_process).SecurityPersonModify(Entity, _person, _permission);
                                }
                                break;
                            case Common.ConstantsEntitiesName.DS.Organization:  // "OrganizationProperties":
                                Entity = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganizationOrg).SecurityPersonModify(Entity, _person, _permission);
                                base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightPersonOrganization);
                                _pkValues = "IdOrganizationOrg=" + _IdOrganizationOrg.ToString();
                                break;
                            #endregion

                            #region MODIFY Seguridad por Mapa
                            case Condesus.EMS.Business.Common.Security.MapDS:
                                Entity = EMSLibrary.User.DirectoryServices.Map.SecurityPersonModify(Entity, _person, _permission);
                                base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightPersonMapDS);
                                break;
                            case Condesus.EMS.Business.Common.Security.MapIA:
                                Entity = EMSLibrary.User.ImprovementAction.Map.SecurityPersonModify(Entity, _person, _permission);
                                base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightPersonMapIA);
                                break;
                            case Condesus.EMS.Business.Common.Security.MapKC:
                                Entity = EMSLibrary.User.KnowledgeCollaboration.Map.SecurityPersonModify(Entity, _person, _permission);
                                base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightPersonMapKC);
                                break;
                            case Condesus.EMS.Business.Common.Security.MapPA:
                                Entity = EMSLibrary.User.PerformanceAssessments.Map.SecurityPersonModify(Entity, _person, _permission);
                                base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightPersonMapPA);
                                break;
                            case Condesus.EMS.Business.Common.Security.MapPF:
                                Entity = EMSLibrary.User.ProcessFramework.Map.SecurityPersonModify(Entity, _person, _permission);
                                base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightPersonMapPF);
                                break;
                            case Condesus.EMS.Business.Common.Security.MapRM:
                                Entity = EMSLibrary.User.RiskManagement.Map.SecurityPersonModify(Entity, _person, _permission);
                                base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightPersonMapRM);
                                break;
                            #endregion

                            #region MODIFY Seguridad por Config
                            case Condesus.EMS.Business.Common.Security.ConfigurationDS:
                                Entity = EMSLibrary.User.DirectoryServices.Configuration.SecurityPersonModify(Entity, _person, _permission);
                                base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightPersonConfigurationDS);
                                break;
                            case Condesus.EMS.Business.Common.Security.ConfigurationIA:
                                Entity = EMSLibrary.User.ImprovementAction.Configuration.SecurityPersonModify(Entity, _person, _permission);
                                base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightPersonConfigurationIA);
                                break;
                            case Condesus.EMS.Business.Common.Security.ConfigurationKC:
                                Entity = EMSLibrary.User.KnowledgeCollaboration.Configuration.SecurityPersonModify(Entity, _person, _permission);
                                base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightPersonConfigurationKC);
                                break;
                            case Condesus.EMS.Business.Common.Security.ConfigurationPA:
                                Entity = EMSLibrary.User.PerformanceAssessments.Configuration.SecurityPersonModify(Entity, _person, _permission);
                                base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightPersonConfigurationPA);
                                break;
                            case Condesus.EMS.Business.Common.Security.ConfigurationPF:
                                Entity = EMSLibrary.User.ProcessFramework.Configuration.SecurityPersonModify(Entity, _person, _permission);
                                base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightPersonConfigurationPF);
                                break;
                            case Condesus.EMS.Business.Common.Security.ConfigurationRM:
                                Entity = EMSLibrary.User.RiskManagement.Configuration.SecurityPersonModify(Entity, _person, _permission);
                                base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.SS.RightPersonConfigurationRM);
                                break;
                            #endregion
                        }
                    }

                    base.NavigatorAddTransferVar("IdPermission", _idPermission);
                    base.NavigatorAddTransferVar("ParentEntity", _ParentEntity);
                    base.NavigatorAddTransferVar("IdOrganization", _idOrganization);
                    base.NavigatorAddTransferVar("IdPerson", _idPerson);

                    _pkValues += "& IdPerson=" + _idPerson.ToString()
                        + "& IdPermission=" + _idPermission.ToString()
                        + "& ParentEntity=" + _ParentEntity.ToString();

                    base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);
                    String _entityPropertyName = Entity.Person.FullName;
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
