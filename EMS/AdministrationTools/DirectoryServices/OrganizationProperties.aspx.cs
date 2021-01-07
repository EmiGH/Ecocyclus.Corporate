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
using System.Linq;

namespace Condesus.EMS.WebUI.DS
{
    public partial class OrganizationProperties : BaseProperties
    {
        #region Internal Properties
            private RadComboBox _RdcResourceCatalog;
            private RadTreeView _RtvResourceCatalog;
            private Int64 _IdOrganization
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdOrganization") ? base.NavigatorGetTransferVar<Int64>("IdOrganization") : 0;
                }
            }
            private Organization _Entity = null;
            private Organization Entity
            {
                get
                {
                    try
                    {
                        if (_Entity == null)
                            _Entity = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization);

                        return _Entity;
                    }
                    catch { return null; }
                }
                set { _Entity = value; }
            }
            private RadTreeView _RtvOrganizationClassification;
            private ArrayList _OrganizationClassificationAux //Estructura interna para guardar los id de clasificacion que son seleccionados.
            {
                get
                {
                    if (ViewState["OrganizationClassificationAux"] == null)
                    {
                        ViewState["OrganizationClassificationAux"] = new ArrayList();
                    }
                    return (ArrayList)ViewState["OrganizationClassificationAux"];
                }
                set { ViewState["OrganizationClassificationAux"] = value; }
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
                AddTreeViewOrganizationClassifications();
            }
            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);
                AddComboResourceCatalogues();

                if (!Page.IsPostBack)
                {
                    //Inicializo el Form
                    if (Entity == null)
                        { Add(); }
                    else
                        { LoadData(); } //Edit.

                    //Form
                    base.SetContentTableRowsCss(tblContentForm);
                    //Realiza la carga de los datos de Clasificaciones, para que se puedan usar.
                    LoadDataOrganizationClassification();
                }
                
                ////Menu de Seguridad
                //base.BuildPropertySecuritySystemMenu(Entity, new RadMenuEventHandler(rmnuSecuritySystem_ItemClick));
            }
            protected override void SetPagetitle()
            {
                base.PageTitle = (Entity != null) ? Entity.CorporateName : Resources.CommonListManage.Organization;
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }
        #endregion

        #region Private Methods
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonListManage.Organization;
                lblClassifications.Text = Resources.CommonListManage.OrganizationClassification;
                lblCorporateName.Text = Resources.CommonListManage.CorporateName;
                lblFiscalIdentification.Text = Resources.CommonListManage.FiscalIdentification;
                lblResourceCatalog.Text = Resources.CommonListManage.Picture;
                rfv1.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
            }
            private void LoadData()
            {
                //Cargo los textbox con los datos
                txtName.Text = _Entity.Name;
                txtFiscalId.Text = _Entity.FiscalIdentification;
                txtCorporateName.Text = _Entity.CorporateName;
                txtName.ReadOnly = false;
                txtFiscalId.ReadOnly = false;
                txtCorporateName.ReadOnly = false;
                //Carga la estructura paralela con las clasificaciones que tiene el indicador.
                LoadStructOrganizationClassificationAux();
                SetResourceCatalog();
            }
            private void Add()
            {
                base.StatusBar.Clear();
                
                //_IdOrganization = 0;
                txtCorporateName.Text = String.Empty;
                txtFiscalId.Text = String.Empty;
                txtName.Text = String.Empty;
                txtCorporateName.ReadOnly = false;
                txtFiscalId.ReadOnly = false;
                txtName.ReadOnly = false;
            }
            private void AddTreeViewOrganizationClassifications()
            {
                //Setea los datos en el DataTable de base, para que luego se carge la grilla.
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                BuildGenericDataTable(Common.ConstantsEntitiesName.DS.OrganizationClassifications, _params);

                //Arma tree con todos los roots.
                phOrganizationClassifications.Controls.Clear();
                _RtvOrganizationClassification = base.BuildHierarchicalListManageContent(Common.ConstantsEntitiesName.DS.OrganizationClassifications, "Form");
                //Ya tengo el Tree le attacho los Handlers
                _RtvOrganizationClassification.NodeExpand += new RadTreeViewEventHandler(_RtvOrganizationClassification_NodeExpand);
                _RtvOrganizationClassification.NodeCreated += new RadTreeViewEventHandler(_RtvOrganizationClassification_NodeCreated);
                _RtvOrganizationClassification.NodeCheck += new RadTreeViewEventHandler(_RtvOrganizationClassification_NodeCheck);
                phOrganizationClassifications.Controls.Add(_RtvOrganizationClassification);
            }
            /// <summary>
            /// Este metodo se encarga solamente de cargar los datos dentro del TreeView que ya debe existir en la pagina (que se crea en el init)
            /// </summary>
            private void LoadDataOrganizationClassification()
            {
                _RtvOrganizationClassification.Nodes.Clear();
                //Con el tree ya armado, ahora hay que llenarlo con datos.
                base.LoadGenericTreeView(ref _RtvOrganizationClassification, Common.ConstantsEntitiesName.DS.OrganizationClassifications, Common.ConstantsEntitiesName.DS.OrganizationClassification, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, true, String.Empty, false, false);
            }
            private void LoadStructOrganizationClassificationAux()
            {
                //Carga de forma inicial todos los id de clasificacion que ya estan cargados en este proyecto.
                _OrganizationClassificationAux = new ArrayList();
                Dictionary<Int64, Condesus.EMS.Business.DS.Entities.OrganizationClassification> _organizationClassifications = new Dictionary<Int64, Condesus.EMS.Business.DS.Entities.OrganizationClassification>();
                if (_IdOrganization != 0)
                {
                    _organizationClassifications = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).Classifications;
                }
                //Ahora recorre todas las clasificaciones que ya tiene asiganadas el indicador, y los guarda en la estructura interna (ArrayList).
                foreach (Condesus.EMS.Business.DS.Entities.OrganizationClassification _item in _organizationClassifications.Values)
                {
                    _OrganizationClassificationAux.Add(_item.IdOrganizationClassification);
                }
            }
            private void AddComboResourceCatalogues()
            {
                String _filterExpression = String.Empty;
                //Combo de ResourceCatalog
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                AddComboWithTreeElementMaps(ref phResourceCatalog, ref _RdcResourceCatalog, ref _RtvResourceCatalog,
                    Common.ConstantsEntitiesName.KC.ResourceClassifications, Common.ConstantsEntitiesName.KC.ResourceCatalogues, _params, false, true, false, ref _filterExpression,
                    new RadTreeViewEventHandler(rtvResource_NodeExpand),
                    Resources.Common.ComboBoxNoDependency, false);

            }
            private void SetResourceCatalog()
            {
                //Seteamos la resourceCatalog...
                //Realiza el seteo del parent en el Combo-Tree.
                if (Entity.Pictures.Count > 0)
                {
                    Condesus.EMS.Business.KC.Entities.Resource _resource = EMSLibrary.User.KnowledgeCollaboration.Map.Resource(Entity.Pictures.First().Value.IdResource);
                    String _keyValuesElement = "IdResource=" + _resource.IdResource.ToString();
                    if (_resource.Classifications.Count > 0)
                    {
                        String _keyValuesClassification = "IdResourceClassification=" + _resource.Classifications.First().Value.IdResourceClassification;
                        SelectItemTreeViewParentElementMaps(_keyValuesClassification, _keyValuesElement, ref _RtvResourceCatalog, ref _RdcResourceCatalog, Common.ConstantsEntitiesName.KC.ResourceClassification, Common.ConstantsEntitiesName.KC.ResourceClassificationChildren, Common.ConstantsEntitiesName.KC.ResourceCatalogues);
                        //SelectItemTreeViewParentElementMaps(_keyValuesClassification, _keyValuesElement, ref _RtvResourceCatalog, ref _RdcResourceCatalog, Common.ConstantsEntitiesName.KC.ResourceClassification, Common.ConstantsEntitiesName.KC.ResourceClassificationChildren, Common.ConstantsEntitiesName.KC.Resources);
                    }
                    else
                    {
                        SelectItemTreeViewParent(_keyValuesElement, ref _RtvResourceCatalog, ref _RdcResourceCatalog, Common.ConstantsEntitiesName.KC.ResourceCatalog, Common.ConstantsEntitiesName.KC.ResourceCatalogues);
                    }
                }
            }
        #endregion

        #region Page Events
            void _RtvOrganizationClassification_NodeExpand(object sender, RadTreeNodeEventArgs e)
            {
                base.NodeExpand(sender, e, Common.ConstantsEntitiesName.DS.OrganizationClassificationsChildren, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, true, String.Empty);
            }
            void _RtvOrganizationClassification_NodeCreated(object sender, RadTreeNodeEventArgs e)
            {
                Int64 _idOrganizationClassification = Convert.ToInt64(GetKeyValue(e.Node.Value, "IdOrganizationClassification"));
                if (_OrganizationClassificationAux.Contains(_idOrganizationClassification))
                {
                    e.Node.Checked = true;
                }
                else
                {
                    e.Node.Checked = false;
                }
                //Si el usuario no tiene permisos de manage sobre la Clasificacion que viene (que se crea), no puede seleccionarla para asociarla.
                String _permissionType = e.Node.Attributes["PermissionType"].ToString();
                if (_permissionType != Common.Constants.PermissionManageName)
                {
                    e.Node.Checkable = false;
                }
            }
            void _RtvOrganizationClassification_NodeCheck(object sender, RadTreeNodeEventArgs e)
            {
                RadTreeNode _node = e.Node;

                //Obtiene el Id del nodo checkeado
                Int64 _idOrganizationClass = Convert.ToInt64(GetKeyValue(_node.Value, "IdOrganizationClassification"));
                if (_OrganizationClassificationAux.Contains(_idOrganizationClass))
                {
                    if (!_node.Checked)
                    {
                        _OrganizationClassificationAux.Remove(_idOrganizationClass);
                    }
                }
                else
                {
                    _OrganizationClassificationAux.Add(_idOrganizationClass);
                }
            }
            protected void rtvResource_NodeExpand(object sender, RadTreeNodeEventArgs e)
            {
                //Limpio los hijos, para no duplicar al abrir y cerrar.
                e.Node.Nodes.Clear();
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                _params = GetKeyValues(e.Node.Value);

                //Primero lo hace sobre las Clasificaciones Hijas...
                //Actualiza el Data table con los hijos, para poder asociarlas al nodo expadido.
                BuildGenericDataTable(Common.ConstantsEntitiesName.KC.ResourceClassificationChildren, _params);
                if (DataTableListManage.ContainsKey(Common.ConstantsEntitiesName.KC.ResourceClassificationChildren))
                {
                    foreach (DataRow _drRecord in DataTableListManage[Common.ConstantsEntitiesName.KC.ResourceClassificationChildren].Rows)
                    {
                        RadTreeNode _node = SetElementMapsNodeTreeView(_drRecord, Common.ConstantsEntitiesName.KC.ResourceClassificationChildren, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, String.Empty, String.Empty, String.Empty);
                        e.Node.Nodes.Add(_node);
                        //Como es un Mapa de Elementos, indica true para que cargue los elementos debajo de la classificacion.
                        SetExpandMode(_node, Common.ConstantsEntitiesName.KC.ResourceClassificationChildren, true, true);
                    }
                }

                //Ahora lo hace sobre los Elementos Hijos de la Clasificacion expandida.
                BuildGenericDataTable(Common.ConstantsEntitiesName.KC.ResourceCatalogues, _params);
                if (DataTableListManage.ContainsKey(Common.ConstantsEntitiesName.KC.ResourceCatalogues))
                {
                    foreach (DataRow _drRecord in DataTableListManage[Common.ConstantsEntitiesName.KC.ResourceCatalogues].Rows)
                    {
                        RadTreeNode _node = SetElementMapsNodeTreeView(_drRecord, Common.ConstantsEntitiesName.KC.ResourceCatalogues, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, String.Empty, String.Empty, String.Empty);
                        e.Node.Nodes.Add(_node);
                    }
                }
            }
            protected void btnSave_Click(object sender, EventArgs e)
            {
                try
                {
                    Dictionary<Int64, OrganizationClassification> _organizationClassifications = new Dictionary<Int64, OrganizationClassification>();
                    //Aca recorre el ArrayList con los id que fueron chequeado, y contruye un Dictionary, para pasar al ADD.
                    foreach (Int64 _item in _OrganizationClassificationAux)
                    {
                        _organizationClassifications.Add(_item, EMSLibrary.User.DirectoryServices.Map.OrganizationClassification(_item));
                    }
                    //Si el key obtenido no llega a exister devuelve null.
                    Int64 _idResourceCatalog = Convert.ToInt64((_RtvResourceCatalog.SelectedNode == null ? 0 : GetKeyValue(_RtvResourceCatalog.SelectedNode.Value, "IdResource")));    //Si queda en cero 0, quiere decir que no asocia catalogos.
                    Condesus.EMS.Business.KC.Entities.ResourceCatalog _resourceCatalog = (Condesus.EMS.Business.KC.Entities.ResourceCatalog)EMSLibrary.User.KnowledgeCollaboration.Map.Resource(_idResourceCatalog);

                    if (Entity == null)
                    {
                        //Alta
                        Entity = EMSLibrary.User.DirectoryServices.Map.OrganizationsAdd(txtCorporateName.Text, txtName.Text, txtFiscalId.Text, _resourceCatalog, _organizationClassifications);
                    }
                    else
                    {
                        //Modificacion
                        Entity.Modify(txtCorporateName.Text, txtName.Text, txtFiscalId.Text, _resourceCatalog, _organizationClassifications);
                        //base.NavigatorAddTransferVar("IdOrganization", Entity.IdOrganization);
                        //base.Navigate(HttpContext.Current.Request.Url.AbsolutePath, Entity.CorporateName + " " + Resources.Common.Edit, Condesus.WebUI.Navigation.DeleteType.DeleteModifiedEntity);
                    }
                    base.NavigatorAddTransferVar("IdOrganization", Entity.IdOrganization);
                    String _pkValues = "IdOrganization=" + Entity.IdOrganization.ToString();
                    base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);
                    base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.DS.Organization);
                    base.NavigatorAddTransferVar("EntityNameContextInfo", Common.ConstantsEntitiesName.DS.Organization);
                    //Navigate(GetPageViewerByEntity(Common.ConstantsEntitiesName.DS.Organization), Resources.CommonListManage.Organization + " " + Entity.CorporateName, Condesus.WebUI.Navigation.DeleteType.DeleteModifiedEntity);
                    String _entityPropertyName = String.Concat(Entity.CorporateName);
                    NavigatePropertyEntity(Common.ConstantsEntitiesName.DS.Organization, _entityPropertyName, Condesus.WebUI.Navigation.NavigateMenuAction.Add);

                    base.StatusBar.ShowMessage(Resources.Common.SaveOK);
                }
                catch (Exception ex)
                {
                    base.StatusBar.ShowMessage(ex);
                }
            }
            //protected void rmnuSecuritySystem_ItemClick(object sender, RadMenuEventArgs e)
            //{
            //    String _pkValues = String.Empty;
            //    switch (e.Item.Value)
            //    {
            //        case "rmiSSJobTitles":
            //            base.NavigatorAddTransferVar("EntityNameGrid", "RightJobTitleOrganizations");
            //            base.NavigatorAddTransferVar("EntityName", "RightJobTitleOrganization");
            //            //base.NavigatorAddTransferVar("EntityNameToRemove", "RightJobTitleOrganizationRemove");
            //            base.NavigatorAddTransferVar("PageTitle", txtCorporateName.Text);
                        
            //            _pkValues = "IdOrganization=" + _IdOrganization.ToString();
            //            base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);
            //            base.Navigate("~/Managers/ListManageAndView.aspx", "RightJobTitleOrganizations");

            //            break;
            //        case "rmiSSPerson":
            //            base.NavigatorAddTransferVar("EntityNameGrid", "RightPersonOrganizations");
            //            base.NavigatorAddTransferVar("EntityName", "RightPersonOrganization");
            //            //base.NavigatorAddTransferVar("EntityNameToRemove", "RightPersonOrganizationRemove");
            //            base.NavigatorAddTransferVar("PageTitle", txtCorporateName.Text);

            //            _pkValues = "IdOrganization=" + _IdOrganization.ToString();
            //            base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);
            //            base.Navigate("~/Managers/ListManageAndView.aspx", "RightJobTitleOrganizations");

            //            break;
            //        default:
            //            break;
            //    }//fin Switch
            //}//fin evento
        #endregion
    }
}
