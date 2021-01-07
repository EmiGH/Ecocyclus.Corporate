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
using Condesus.EMS.Business.KC.Entities;

namespace Condesus.EMS.WebUI.ManagementTools.KnowledgeCollaboration
{
    public partial class ResourcesProperties : BaseProperties
    {
        #region Internal Properties
            CompareValidator _CvResourceType;
            private Condesus.EMS.Business.KC.Entities.Resource _Entity = null;
            private Int64 _IdResourceClassification
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdResourceClassification") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdResourceClassification")) : 0;
                }
            }
            private Int64 _IdResource
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdResource") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdResource")) : 0;
                }
            }
            private Condesus.EMS.Business.KC.Entities.Resource Entity
            {
                get
                {
                    try
                    {
                        if (_Entity == null)
                            _Entity = EMSLibrary.User.KnowledgeCollaboration.Map.Resource(_IdResource);

                        return _Entity;
                    }
                    catch { return null; }
                }
                set { _Entity = value; }
            }
            private RadComboBox _RdcResourceType;
            private RadTreeView _RtvResourceType;

            private RadTreeView _RtvResourceClassification;
            private ArrayList _ResourceClassificationAux //Estructura interna para guardar los id de clasificacion que son seleccionados.
        {
            get
            {
                if (ViewState["ResourceClassificationAux"] == null)
                {
                    ViewState["ResourceClassificationAux"] = new ArrayList();
                }
                return (ArrayList)ViewState["ResourceClassificationAux"];
            }
            set { ViewState["ResourceClassificationAux"] = value; }
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
                AddTreeViewResourceClassifications();
                base.InjectCheckIndexesTags();
            }
            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);

                AddCombos();
                AddValidators();

                if (!Page.IsPostBack)
                {
                    //Inicializo el Form
                    if (Entity == null)
                    { Add(); }
                    else
                    { LoadData(); } //Edit.

                    //Form
                    base.SetContentTableRowsCss(tblContentForm);
                    lblLanguageValue.Text = Global.DefaultLanguage.Name;

                    //Realiza la carga de los datos de Clasificaciones, para que se puedan usar.
                    LoadDataResourceClassification();
                }
            }
            protected override void SetPagetitle()
            {
                base.PageTitle = (Entity != null) ? Entity.LanguageOption.Title : Resources.CommonListManage.Resource;
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }
        #endregion

        #region Private Methods
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonListManage.Resource;
                lblClassifications.Text = Resources.CommonListManage.ResourceClassification;
                lblDescription.Text = Resources.CommonListManage.Description;
                lblFileName.Text = Resources.CommonListManage.CurrentFileName;
                lblFileVersion.Text = Resources.CommonListManage.Version;
                lblLanguage.Text = Resources.CommonListManage.Language;
                lblResourceFileType.Text = Resources.CommonListManage.ResourceType;
                lblResourceType.Text = Resources.CommonListManage.Type;
                lblTitle.Text = Resources.CommonListManage.Title;
                ddlResourceFileType.Items[0].Text = Resources.CommonListManage.Versionable;
                ddlResourceFileType.Items[1].Text = Resources.CommonListManage.Catalog;
                rfv1.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
                rfv2.ErrorMessage = Resources.ConstantMessage.ValidationInvalidSpecialCharacter;
                cvResourceFileType.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
            }
            private void AddCombos()
            {
                AddComboResourceTypes();
            }
            private void AddValidators()
            {
                ValidatorRequiredField(Common.ConstantsEntitiesName.KC.ResourceTypes, phResourceTypeValidator, ref _CvResourceType, _RdcResourceType, Resources.ConstantMessage.SelectAResourceType);
            }
            private void AddComboResourceTypes()
            {
                String _filterExpression = String.Empty;
                //Combo de ResourceType
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                AddComboWithTree(phResourceType, ref _RdcResourceType, ref _RtvResourceType,
                    Common.ConstantsEntitiesName.KC.ResourceTypes, _params, false, true, false, ref _filterExpression,
                    new RadTreeViewEventHandler(rtvHierarchicalTreeViewInComboResourceType_NodeExpand),
                    Common.Constants.ComboBoxSelectItemDefaultPrefix,false);
            }
            private void AddTreeViewResourceClassifications()
            {
                //Setea los datos en el DataTable de base, para que luego se carge la grilla.
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                BuildGenericDataTable(Common.ConstantsEntitiesName.KC.ResourceClassifications, _params);

                //Arma tree con todos los roots.
                phResourceClassifications.Controls.Clear();
                _RtvResourceClassification = base.BuildHierarchicalListManageContent(Common.ConstantsEntitiesName.KC.ResourceClassifications, "Form");
                //Ya tengo el Tree le attacho los Handlers
                _RtvResourceClassification.NodeExpand += new RadTreeViewEventHandler(_RtvResourceClassification_NodeExpand);
                _RtvResourceClassification.NodeCreated += new RadTreeViewEventHandler(_RtvResourceClassification_NodeCreated);
                _RtvResourceClassification.NodeCheck += new RadTreeViewEventHandler(_RtvResourceClassification_NodeCheck);
                phResourceClassifications.Controls.Add(_RtvResourceClassification);
            }
            /// <summary>
            /// Este metodo se encarga solamente de cargar los datos dentro del TreeView que ya debe existir en la pagina (que se crea en el init)
            /// </summary>
            private void LoadDataResourceClassification()
            {
                _RtvResourceClassification.Nodes.Clear();
                //Con el tree ya armado, ahora hay que llenarlo con datos.
                base.LoadGenericTreeView(ref _RtvResourceClassification, Common.ConstantsEntitiesName.KC.ResourceClassifications, Common.ConstantsEntitiesName.KC.ResourceClassification, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, true, String.Empty, false, false);
            }
            private void LoadStructResourceClassificationAux()
            {
                //Carga de forma inicial todos los id de clasificacion que ya estan cargados en este proyecto.
                _ResourceClassificationAux = new ArrayList();
                Dictionary<Int64, Condesus.EMS.Business.KC.Entities.ResourceClassification> _ResourceClassifications = new Dictionary<Int64, Condesus.EMS.Business.KC.Entities.ResourceClassification>();
                if (_IdResource != 0)
                {
                    _ResourceClassifications = Entity.Classifications;
                }
                //Ahora recorre todas las clasificaciones que ya tiene asiganadas el indicador, y los guarda en la estructura interna (ArrayList).
                foreach (Condesus.EMS.Business.KC.Entities.ResourceClassification _item in _ResourceClassifications.Values)
                {
                    _ResourceClassificationAux.Add(_item.IdResourceClassification);
                }
            }
            private void SetResourceTypes()
            {
                //si es un root, no debe hacer nada de esto.
                if (Entity.ResourceType.IdResourceType != 0)
                {
                    //El orden de este KeyValue, debe ser el mismo al que se hace al momento de crear el tree, y el mismo del metodo publico del factory!!!
                    String _keyValues = "IdResourceType=" + Entity.ResourceType.IdResourceType.ToString();
                    //Realiza el seteo del parent en el Combo-Tree.
                    SelectItemTreeViewParent(_keyValues, ref _RtvResourceType, ref _RdcResourceType, Common.ConstantsEntitiesName.KC.ResourceType, Common.ConstantsEntitiesName.KC.ResourceTypeChildren);
                }
            }
            private void LoadData()
            {
                //Contruye el LG de indicators.
                Resource_LG _resource_LG = Entity.LanguagesOptions.Item(Global.DefaultLanguage);

                txtTitle.Text = _resource_LG.Title;
                txtDescription.Text = _resource_LG.Description;

                if (Entity.GetType().Name == "ResourceVersion")
                {
                    LoadResourceFile((Condesus.EMS.Business.KC.Entities.ResourceVersion)Entity);
                }
                else
                {
                    ddlResourceFileType.SelectedValue = "ResourceCatalog";
                }

                //Setea el valor de Magnitud en el combo.
                SetResourceTypes();
                ddlResourceFileType.Enabled = false;
                //Carga la estructura paralela con las clasificaciones que tiene el indicador.
                LoadStructResourceClassificationAux();
            }
            private void LoadResourceFile(ResourceVersion resourceFile)
            {
                Condesus.EMS.Business.KC.Entities.Version _resourceFile = resourceFile.CurrentVersion;

                ddlResourceFileType.SelectedValue = "ResourceVersion";
                if (_resourceFile != null)
                {
                    if (_resourceFile.GetType().Name == "VersionURL")
                    {
                        lblFileNameValue.Text = ((Condesus.EMS.Business.KC.Entities.VersionURL)_resourceFile).Url;
                    }
                    else
                    {
                        lblFileNameValue.Text = ((Condesus.EMS.Business.KC.Entities.VersionDoc)_resourceFile).FileAttach.FileName;
                        lblFileVersionValue.Text = ((Condesus.EMS.Business.KC.Entities.VersionDoc)_resourceFile).VersionNumber.ToString();
                    }
                }
            }
            private void Add()
            {
                //limpio los textbox por si hay datos
                txtDescription.Text = String.Empty;
                txtTitle.Text = String.Empty;
                lblFileNameValue.Text = String.Empty;
                lblFileVersionValue.Text = String.Empty;
            }
        #endregion

        #region Page Events
            //Como este ya es un Combo con TRee Generico, entonces debo manejar el expand en la pagina.
            //La idea de generico es porque puedo poner cuantos yo quiera en una pagina indicandole el nombre de la entidad a cargarle.
            protected void rtvHierarchicalTreeViewInComboResourceType_NodeExpand(object sender, RadTreeNodeEventArgs e)
            {
                NodeExpand(sender, e, Common.ConstantsEntitiesName.KC.ResourceTypeChildren, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, false, String.Empty);
            }

            void _RtvResourceClassification_NodeExpand(object sender, RadTreeNodeEventArgs e)
            {
                base.NodeExpand(sender, e, Common.ConstantsEntitiesName.KC.ResourceClassificationChildren, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, true, String.Empty);
            }
            void _RtvResourceClassification_NodeCreated(object sender, RadTreeNodeEventArgs e)
            {
                Int64 _idResourceClassification = Convert.ToInt64(GetKeyValue(e.Node.Value, "IdResourceClassification"));
                if (_ResourceClassificationAux.Contains(_idResourceClassification))
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
            void _RtvResourceClassification_NodeCheck(object sender, RadTreeNodeEventArgs e)
            {
                RadTreeNode _node = e.Node;

                //Obtiene el Id del nodo checkeado
                Int64 _idIndicatorClass = Convert.ToInt64(GetKeyValue(_node.Value, "IdResourceClassification"));
                if (_ResourceClassificationAux.Contains(_idIndicatorClass))
                {
                    if (!_node.Checked)
                    {
                        _ResourceClassificationAux.Remove(_idIndicatorClass);
                    }
                }
                else
                {
                    _ResourceClassificationAux.Add(_idIndicatorClass);
                }
            }
            protected void btnSave_Click(object sender, EventArgs e)
            {
                try
                {
                    //Se deben insertar los indicadores a 1 o mas Clasificaciones
                    Dictionary<Int64, ResourceClassification> _resourceClassifications = new Dictionary<Int64, ResourceClassification>();
                    Int64 _idResourceType = Convert.ToInt64(GetKeyValue(_RtvResourceType.SelectedNode.Value, "IdResourceType"));
                    Condesus.EMS.Business.KC.Entities.ResourceType _resourceType = EMSLibrary.User.KnowledgeCollaboration.Configuration.ResourceType(_idResourceType);

                    //Aca recorre el ArrayList con los id que fueron chequeado, y contruye un Dictionary, para pasar al ADD.
                    foreach (Int64 _item in _ResourceClassificationAux)
                    {
                        _resourceClassifications.Add(_item, EMSLibrary.User.KnowledgeCollaboration.Map.ResourceClassification(_item));
                    }

                    //verifica si es un ADD o un Modify
                    if (Entity == null)
                    {
                        Entity = EMSLibrary.User.KnowledgeCollaboration.Map.ResourceAdd(_resourceType, txtTitle.Text, txtDescription.Text, ddlResourceFileType.SelectedValue, _resourceClassifications);
                    }
                    else
                    {
                        Entity.Modify(_resourceType, txtTitle.Text, txtDescription.Text, _resourceClassifications);
                    }
                    base.NavigatorAddTransferVar("IdResource", Entity.IdResource);

                    String _pkValues = "IdResource=" + Entity.IdResource.ToString();
                    base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);

                    base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.KC.Resource);
                    //base.NavigatorAddTransferVar("EntityNameContextInfo", Common.ConstantsEntitiesName.KC.Resource);
                    base.NavigatorAddTransferVar("EntityNameContextElement", String.Empty);
                    base.NavigatorAddTransferVar("EntityNameContextInfo", Common.ConstantsEntitiesName.KC.Resource);
                    //Navigate("~/MainInfo/ListViewer.aspx", Resources.CommonListManage.KCResource + " " + Entity.LanguageOption.Title, Condesus.WebUI.Navigation.DeleteType.DeleteModifiedEntity);
                    String _entityPropertyName = String.Concat(Entity.LanguageOption.Title);
                    NavigatePropertyEntity(Common.ConstantsEntitiesName.KC.Resource, _entityPropertyName, Condesus.WebUI.Navigation.NavigateMenuAction.Add);

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

