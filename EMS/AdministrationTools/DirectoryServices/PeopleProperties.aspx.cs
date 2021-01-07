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
using System.Linq;

using Condesus.EMS.Business.DS.Entities;
using Condesus.EMS.WebUI.MasterControls;

namespace Condesus.EMS.WebUI.DS
{
    public partial class PeopleProperties : BaseProperties
    {
        #region Internal Properties
            private RadComboBox _RdcResourceCatalog;
            private RadTreeView _RtvResourceCatalog;
            CompareValidator _CvResourceCatalog;
            CompareValidator _CvSalutationType;
            RadComboBox _RdcSalutationType;
            private Int64 _IdPerson
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdPerson") ? base.NavigatorGetTransferVar<Int64>("IdPerson") : 0;
                }
            }
            private Int64 _IdOrganization
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdOrganization") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdOrganization")) : Convert.ToInt64(GetPKfromNavigator("IdOrganization"));
                }
            }
            private Person _Entity = null;
            private Person Entity
            {
                get
                {
                    try
                    {
                        if (_Entity == null)
                            _Entity = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).Person(_IdPerson);

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

                AddCombos();
                AddValidators();
            }
            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);
                lblOrganizationValue.Text = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).CorporateName;
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
                }
            }
            //Setea el Titulo de la Pagina
            //(como no puedo pasar el Objeto como una 'Interface', lo tengo que hacer localmente)
            protected override void SetPagetitle()
            {
                base.PageTitle = (Entity != null) ? Entity.FirstName + ", " + Entity.LastName : Resources.CommonListManage.Person;
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }

        #endregion

        #region Private Methods
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonListManage.People;
                lblAddressingType.Text = Resources.CommonListManage.SalutationType;
                lblFirstName.Text = Resources.CommonListManage.FirstName;
                lblLastName.Text = Resources.CommonListManage.LastName;
                lblNickName.Text = Resources.CommonListManage.NickName;
                lblOrganization.Text = Resources.CommonListManage.Organization;
                lblPosName.Text = Resources.CommonListManage.PosName;
                lblResourceCatalog.Text = Resources.CommonListManage.Picture;
                rfv1.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
                rfv2.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
            }
            private void LoadData()
            {
                base.PageTitle = Entity.FirstName + ' ' + Entity.LastName;

                //Cargo los textbox con los datos
                txtFirstName.Text = Entity.FirstName;
                txtLastName.Text = Entity.LastName;
                txtNickName.Text = Entity.NickName;
                txtPosName.Text = Entity.PosName;
                _RdcSalutationType.SelectedValue = "IdSalutationType=" + Entity.SalutationType.IdSalutationType.ToString();
                SetResourceCatalog();
            }
            private void Add()
            {
                base.StatusBar.Clear();

                txtFirstName.Text = String.Empty;
                txtLastName.Text = String.Empty;
                txtNickName.Text = String.Empty;
                txtPosName.Text = String.Empty;
            }
            private void AddCombos()
            {
                //AddComboOrganizations();
                AddComboSalutationType();
            }
            private void AddValidators()
            {
                //ValidatorRequiredField(Common.ConstantsEntitiesName.DS.OrganizationClassifications, fValidator, ref _CvOrganization, _RdcOrganization, Resources.ConstantMessage.SelectAnOrganization);
                ValidatorRequiredField(Common.ConstantsEntitiesName.DS.SalutationTypes, phSalutationTypeValidator, ref _CvSalutationType, _RdcSalutationType, Resources.ConstantMessage.SelectASalutationType);
            }
            private void AddComboSalutationType()
            {
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                AddCombo(phSalutationType, ref _RdcSalutationType, Common.ConstantsEntitiesName.DS.SalutationTypes, String.Empty, _params, false, true, false, false, false);
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
                    Int64 _idSalutationType = Convert.ToInt64(GetKeyValue(_RdcSalutationType.SelectedValue, "IdSalutationType"));
                    //Int64 _idOrganization = Convert.ToInt64(GetKeyValue(_RtvOrganization.SelectedNode.Value, "IdOrganization"));
                    SalutationType _salutationType = EMSLibrary.User.DirectoryServices.Configuration.SalutationType(_idSalutationType);
                    //Si el key obtenido no llega a exister devuelve null.
                    Int64 _idResourceCatalog = Convert.ToInt64((_RtvResourceCatalog.SelectedNode == null ? 0 : GetKeyValue(_RtvResourceCatalog.SelectedNode.Value, "IdResource")));    //Si queda en cero 0, quiere decir que no asocia catalogos.
                    Condesus.EMS.Business.KC.Entities.ResourceCatalog _resourceCatalog = (Condesus.EMS.Business.KC.Entities.ResourceCatalog)EMSLibrary.User.KnowledgeCollaboration.Map.Resource(_idResourceCatalog);

                    if (Entity == null)
                    {
                        //Alta
                        Entity = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).PeopleAdd(_salutationType, txtLastName.Text, txtFirstName.Text, txtPosName.Text, txtNickName.Text, _resourceCatalog);
                    }
                    else
                    {
                        //Modificacion
                        Entity.Modify(_salutationType, txtLastName.Text, txtFirstName.Text, txtPosName.Text, txtNickName.Text, _resourceCatalog);
                        //EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).PeopleModify(_IdPerson, _idSalutationType, txtLastName.Text, txtFirstName.Text, txtPosName.Text, txtNickName.Text);
                        //base.NavigatorAddTransferVar("IdOrganization", Entity.IdOrganization);
                        //base.NavigatorAddTransferVar("IdPerson", Entity.IdPerson);
                        //base.Navigate(HttpContext.Current.Request.Url.AbsolutePath, Entity.LastName.ToString() + ", " + Entity.FirstName.ToString() + " " + Resources.Common.Edit, Condesus.WebUI.Navigation.DeleteType.DeleteModifiedEntity);
                    }


                    base.NavigatorAddTransferVar("IdOrganization", Entity.Organization.IdOrganization);
                    base.NavigatorAddTransferVar("IdPerson", Entity.IdPerson);

                    String _pkValues = "IdOrganization=" + Entity.Organization.IdOrganization.ToString()
                        + "& IdPerson=" + Entity.IdPerson.ToString();
                    base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);
                    base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.DS.Person);
                    base.NavigatorAddTransferVar("EntityNameContextInfo", Common.ConstantsEntitiesName.DS.Person);
                    //Navigate(GetPageViewerByEntity(Common.ConstantsEntitiesName.DS.Person), Resources.CommonListManage.Person + " " + Entity.LastName.ToString() + ", " + Entity.FirstName.ToString(), Condesus.WebUI.Navigation.DeleteType.DeleteModifiedEntity);
                    String _entityPropertyName = String.Concat(Entity.FullName);
                    NavigatePropertyEntity(Common.ConstantsEntitiesName.DS.Person, _entityPropertyName, Condesus.WebUI.Navigation.NavigateMenuAction.Add);

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
