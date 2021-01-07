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
using Condesus.EMS.Business.PF.Entities;
using Condesus.EMS.Business.GIS.Entities;
using Condesus.EMS.Business.DS.Entities;

namespace Condesus.EMS.WebUI.ManagementTools.ProcessesMap
{
    public partial class ProcessGroupProcessProperties : BaseProperties
    {
        #region Internal Properties
            private RadComboBox _RdcOrganization;
            private RadTreeView _RtvOrganization;
            private CompareValidator _CvOrganization;
            private RadComboBox _RdcResourceCatalog;
            private RadTreeView _RtvResourceCatalog;
            CompareValidator _CvResourceCatalog;
            private Int64 _IdEntity
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdProcess") ? base.NavigatorGetTransferVar<Int64>("IdProcess") : 0;
                }
            }
            private ProcessGroupProcess _Entity = null;
            private ProcessGroupProcess Entity
            {
                get
                {
                    try
                    {
                        if (_Entity == null)
                            _Entity = (ProcessGroupProcess)EMSLibrary.User.ProcessFramework.Map.Process(_IdEntity);

                        return _Entity;
                    }
                    catch
                    {
                        return null;
                    }
                }
                set { _Entity = value; }
            }
            private RadTreeView _RtvProcessClassification;
            private ArrayList _ProcessClassificationAux //Estructura interna para guardar los id de clasificacion que son seleccionados.
            {
                get
                {
                    if (ViewState["ProcessClassificationAux"] == null)
                    {
                        ViewState["ProcessClassificationAux"] = new ArrayList();
                    }
                    return (ArrayList)ViewState["ProcessClassificationAux"];
                }
                set { ViewState["ProcessClassificationAux"] = value; }
            }
            private RadComboBox _RdcGeographicArea;
            private RadTreeView _RtvGeographicArea;
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
                AddTreeViewProcessClassifications();
            }
            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);

                AddComboResourceCatalogues();
                AddComboGeographicAreas();
                AddComboOrganizations();

                if (!Page.IsPostBack)
                {
                    String _dataObjects = String.Empty;

                    //Inicializo el Form
                    if (Entity == null)
                    { Add(); 
                    }
                    else
                    {
                        LoadData(); //Edit.
                        _dataObjects = Common.ConstantsEntitiesName.PF.Process + "|IdProcess=" + Entity.IdProcess.ToString();
                    }

                    //Inyecta los JS que permiten abrir la ventana con el mapa.
                    InjectOpenWindowDialogPickUpCoords(inputPoints.ClientID, drawModeType.ClientID, pnlCoords.ClientID, _dataObjects);

                    //Form
                    base.SetContentTableRowsCss(tblContentForm);
                    lblLanguageValue.Text = Global.DefaultLanguage.Name;
                    this.txtTitle.Focus();

                    //Realiza la carga de los datos de Clasificaciones, para que se puedan usar.
                    LoadDataProcessClassification();
                }
            }
            protected override void SetPagetitle()
            {
                base.PageTitle = (Entity != null) ? Entity.LanguageOption.Title : Resources.CommonListManage.Process;
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties; ;
            }
        #endregion

        #region Private Methods
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonListManage.Process;
                lblCampaignStartDate.Text = Resources.CommonListManage.CampaignStartDate;
                lblClassifications.Text = Resources.CommonListManage.ProcessClassification;
                lblDescription.Text = Resources.CommonListManage.Description;
                lblIdentification.Text = Resources.CommonListManage.Identification;
                lblLanguage.Text = Resources.CommonListManage.Language;
                lblOrder.Text = Resources.CommonListManage.OrderNumber;
                lblPurpose.Text = Resources.CommonListManage.Purpose;
                lblResourceCatalog.Text = Resources.CommonListManage.Picture;
                lblThreshold.Text = Resources.CommonListManage.Threshold;
                lblTitle.Text = Resources.CommonListManage.Title;
                lblWeight.Text = Resources.CommonListManage.Weight;
                lblGeographicArea.Text = Resources.CommonListManage.GeographicArea;
                rfv1.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
                rfv2.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
                rfv3.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
                rfv4.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
                rfvOrder.ErrorMessage = Resources.ConstantMessage.ValidationWrongFormat;
                cv1.ErrorMessage = Resources.ConstantMessage.ValidationWrongFormat;
                cv2.ErrorMessage = Resources.ConstantMessage.ValidationWrongFormat;
                compareValidatorWeight.ErrorMessage = Resources.ConstantMessage.ValidationWrongFormat;
                rangeValidator0_100.ErrorMessage = Resources.ConstantMessage.ValidationRange0_100;
                rangeValidator2.ErrorMessage = Resources.ConstantMessage.ValidationRange0_100;
                lblCoordenates.Text = Resources.CommonListManage.Coordenates;
                pnlCoords.InnerHtml = Resources.ConstantMessage.GeoCodeNotSelected;
            }
            private void Add()
            {
                base.StatusBar.Clear();

                txtTitle.Text = String.Empty;
                txtOrder.Text = "0";
                txtPurpose.Text = String.Empty;
                txtDescription.Text = String.Empty;
                txtWeight.Text = String.Empty;
                TxtThreshold.Text = String.Empty;
                txtIdentification.Text = String.Empty;

            }
            private void LoadData()
            {
                txtTitle.Text = Entity.LanguageOption.Title;
                txtOrder.Text = Entity.OrderNumber.ToString();
                txtPurpose.Text = Entity.LanguageOption.Purpose;
                txtDescription.Text = Entity.LanguageOption.Description;
                txtWeight.Text = Entity.Weight.ToString();
                TxtThreshold.Text = Entity.Threshold.ToString();
                txtIdentification.Text = Entity.Identification;
                rdtCampaignStartDate.SelectedDate = Entity.CurrentCampaignStartDate;

                txtTwitterUser.Text = Entity.TwitterUser;
                txtFacebookUser.Text = Entity.FacebookUser;

                if (!String.IsNullOrEmpty(Entity.Coordinate))
                {
                    pnlCoords.InnerHtml = Resources.ConstantMessage.SelectedCoords + " <br />" + Entity.Coordinate;
                }

                SetResourceCatalog();
                //Carga la estructura paralela con las clasificaciones que tiene el Process.
                LoadStructProcessClassificationAux();
                //SetSite();
                SetGeographicArea();
                SetOrganization();
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

                //ValidatorRequiredField(Common.ConstantsEntitiesName.KC.ResourceCatalogues, phResourceCatalogValidator, ref _CvResourceCatalog, _RdcResourceCatalog, Resources.ConstantMessage.SelectAResourceCatalog);
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
            private void AddTreeViewProcessClassifications()
            {
                //Setea los datos en el DataTable de base, para que luego se carge la grilla.
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                BuildGenericDataTable(Common.ConstantsEntitiesName.PF.ProcessClassifications, _params);

                //Arma tree con todos los roots.
                phProcessClassifications.Controls.Clear();
                _RtvProcessClassification = base.BuildHierarchicalListManageContent(Common.ConstantsEntitiesName.PF.ProcessClassifications, "Form");
                //Ya tengo el Tree le attacho los Handlers
                _RtvProcessClassification.NodeExpand += new RadTreeViewEventHandler(RtvProcessClassification_NodeExpand);
                _RtvProcessClassification.NodeCreated += new RadTreeViewEventHandler(RtvProcessClassification_NodeCreated);
                _RtvProcessClassification.NodeCheck += new RadTreeViewEventHandler(RtvProcessClassification_NodeCheck);
                phProcessClassifications.Controls.Add(_RtvProcessClassification);
            }
            /// <summary>
            /// Este metodo se encarga solamente de cargar los datos dentro del TreeView que ya debe existir en la pagina (que se crea en el init)
            /// </summary>
            private void LoadDataProcessClassification()
            {
                _RtvProcessClassification.Nodes.Clear();
                //Con el tree ya armado, ahora hay que llenarlo con datos.
                base.LoadGenericTreeView(ref _RtvProcessClassification, Common.ConstantsEntitiesName.PF.ProcessClassifications, Common.ConstantsEntitiesName.PF.ProcessClassification, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, true, String.Empty, false, false);
            }
            private void LoadStructProcessClassificationAux()
            {
                //Carga de forma inicial todos los id de clasificacion que ya estan cargados en este proyecto.
                _ProcessClassificationAux = new ArrayList();
                Dictionary<Int64, ProcessClassification> _processClassifications = new Dictionary<Int64, ProcessClassification>();
                if (_IdEntity != 0)
                {
                    _processClassifications = EMSLibrary.User.ProcessFramework.Map.ProcessGroupProcess(_IdEntity).Classifications;
                }
                //Ahora recorre todas las clasificaciones que ya tiene asiganadas el process, y los guarda en la estructura interna (ArrayList).
                foreach (ProcessClassification _item in _processClassifications.Values)
                {
                    _ProcessClassificationAux.Add(_item.IdProcessClassification);
                }
            }
            //private void AddComboSites()
            //{
            //    String _filterExpression = String.Empty;
            //    //Combo de Organizaciones
            //    Dictionary<String, Object> _params = new Dictionary<String, Object>();
            //    AddComboTreeSites(ref phSite, ref _RdcSite, ref _RtvSite, new RadTreeViewEventHandler(rtvSite_NodeExpand));
            //}
            //private void SetSite()
            //{
            //    Facility _facility = Entity.Facility;
            //    if (_facility != null)
            //    {
            //        if (_facility.GetType().Name == Common.ConstantsEntitiesName.DS.Facility)
            //        {
            //            //Si el sitio seleccionado es un facility...
            //            //Seteamos la organizacion...
            //            //Realiza el seteo del parent en el Combo-Tree.
            //            Condesus.EMS.Business.DS.Entities.Organization _oganization = Entity.Facility.Organization;
            //            String _keyValuesElement = "IdOrganization=" + _oganization.IdOrganization.ToString() + "& IdFacility=" + _facility.IdFacility.ToString();
            //            if (_oganization.Classifications.Count > 0)
            //            {
            //                String _keyValuesClassification = "IdOrganizationClassification=" + _oganization.Classifications.First().Value.IdOrganizationClassification;
            //                SelectItemTreeViewParentElementMapsForSite(_keyValuesClassification, _keyValuesElement, ref _RtvSite, ref _RdcSite, Common.ConstantsEntitiesName.DS.OrganizationClassification, Common.ConstantsEntitiesName.DS.OrganizationClassificationsChildren, Common.ConstantsEntitiesName.DS.Organizations, false);
            //            }
            //            //Ahora busco el facility....
            //            SelectItemTreeViewParentForSite(_keyValuesElement, ref _RtvSite, ref _RdcSite, Common.ConstantsEntitiesName.DS.Facility, Common.ConstantsEntitiesName.DS.Facilities, false);
            //        }
            //    }
            //}
            private void SetGeographicArea()
            {
                if (Entity.GeographicArea != null)
                {
                    //si es un root, no debe hacer nada de esto.
                    if (Entity.GeographicArea.IdGeographicArea != 0)
                    {
                        //El orden de este KeyValue, debe ser el mismo al que se hace al momento de crear el tree, y el mismo del metodo publico del factory!!!
                        String _keyValues = "IdGeographicArea=" + Entity.GeographicArea.IdGeographicArea.ToString();
                        RadTreeView _rtvGeoArea = _RtvGeographicArea;
                        RadComboBox _rcbGeoArea = _RdcGeographicArea;
                        //Realiza el seteo del parent en el Combo-Tree.
                        SelectItemTreeViewParent(_keyValues, ref _rtvGeoArea, ref _rcbGeoArea, Common.ConstantsEntitiesName.DS.GeographicArea, Common.ConstantsEntitiesName.DS.GeographicAreaChildren);
                        _RdcGeographicArea = _rcbGeoArea;
                        _RtvGeographicArea = _rtvGeoArea;
                    }
                }
            }
            private void AddComboGeographicAreas()
            {
                String _filterExpression = String.Empty;
                //Combo de GeographicArea Parent
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                AddComboWithTree(phGeographicArea, ref _RdcGeographicArea, ref _RtvGeographicArea,
                    Common.ConstantsEntitiesName.DS.GeographicAreas, _params, false, true, false, ref _filterExpression,
                    new RadTreeViewEventHandler(rtvHierarchicalTreeViewInComboGeoArea_NodeExpand),
                    Common.Constants.ComboBoxSelectItemDefaultPrefix, false);
            }
            private void AddComboOrganizations()
            {
                String _filterExpression = String.Empty;
                //Combo de Organizaciones
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                AddComboWithTreeElementMaps(ref phOrganization, ref _RdcOrganization, ref _RtvOrganization,
                    Common.ConstantsEntitiesName.DS.OrganizationClassifications, Common.ConstantsEntitiesName.DS.OrganizationsRoots, _params, false, true, false, ref _filterExpression,
                    new RadTreeViewEventHandler(rtvOrganizations_NodeExpand),
                    Resources.Common.ComboBoxNoDependency, false);

                ValidatorRequiredField(Common.ConstantsEntitiesName.DS.OrganizationClassifications, phOrganizationValidator, ref _CvOrganization, _RdcOrganization, Resources.ConstantMessage.SelectAnOrganization);
            }
            private void SetOrganization()
            {
                if (Entity.Organization != null)
                {
                    //Seteamos la organizacion...
                    //Realiza el seteo del parent en el Combo-Tree.
                    Organization _oganization = EMSLibrary.User.DirectoryServices.Map.Organization(Entity.Organization.IdOrganization);
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
            protected void rtvHierarchicalTreeViewInComboGeoArea_NodeExpand(object sender, RadTreeNodeEventArgs e)
            {
                NodeExpand(sender, e, Common.ConstantsEntitiesName.DS.GeographicAreaChildren, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, false, String.Empty);
            }
            protected void rtvSite_NodeExpand(object sender, RadTreeNodeEventArgs e)
            {
                NodeExpandSites(sender, e, false);
            }
            void RtvProcessClassification_NodeExpand(object sender, RadTreeNodeEventArgs e)
            {
                base.NodeExpand(sender, e, Common.ConstantsEntitiesName.PF.ProcessClassificationChildren, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, true, String.Empty);
            }
            void RtvProcessClassification_NodeCreated(object sender, RadTreeNodeEventArgs e)
            {
                Int64 _idProcessClassification = Convert.ToInt64(GetKeyValue(e.Node.Value, "IdProcessClassification"));
                if (_ProcessClassificationAux.Contains(_idProcessClassification))
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
            void RtvProcessClassification_NodeCheck(object sender, RadTreeNodeEventArgs e)
            {
                RadTreeNode _node = e.Node;

                //Obtiene el Id del nodo checkeado
                Int64 _idProcessClass = Convert.ToInt64(GetKeyValue(_node.Value, "IdProcessClassification"));
                if (_ProcessClassificationAux.Contains(_idProcessClass))
                {
                    if (!_node.Checked)
                    {
                        _ProcessClassificationAux.Remove(_idProcessClass);
                    }
                }
                else
                {
                    _ProcessClassificationAux.Add(_idProcessClass);
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
                    DateTime _campaignDate = Convert.ToDateTime(rdtCampaignStartDate.SelectedDate);

                    //Obtiene el key necesario. para armar el Parent
                    //Object _obj = GetKeyValue(_RtvResourceCatalog.SelectedNode.Value, "IdResource");
                    //Si el key obtenido no llega a exister devuelve null.
                    Int64 _idResourceCatalog = Convert.ToInt64((_RtvResourceCatalog.SelectedNode == null ? 0 : GetKeyValue(_RtvResourceCatalog.SelectedNode.Value, "IdResource")));    //Si queda en cero 0, quiere decir que no asocia catalogos.

                    Dictionary<Int64, ProcessClassification> _processClassifications = new Dictionary<Int64, Condesus.EMS.Business.PF.Entities.ProcessClassification>();
                    //Aca recorre el ArrayList con los id que fueron chequeado, y contruye un Dictionary, para pasar al ADD.
                    foreach (Int64 _item in _ProcessClassificationAux)
                    {
                        _processClassifications.Add(_item, EMSLibrary.User.ProcessFramework.Map.ProcessClassification(_item));
                    }

                    Condesus.EMS.Business.KC.Entities.ResourceCatalog _resourceCatalog = (Condesus.EMS.Business.KC.Entities.ResourceCatalog)EMSLibrary.User.KnowledgeCollaboration.Map.Resource(_idResourceCatalog);

                    String _coordinate = String.Empty;
                    //Si no hay nada selecionado, entonces las coordendas quedan vacias
                    if (!String.IsNullOrEmpty(drawModeType.Value) && !String.IsNullOrEmpty(inputPoints.Value))
                    {
                        _coordinate = drawModeType.Value + ";" + inputPoints.Value;
                    }
                    else
                    {
                        if (pnlCoords.InnerHtml != Resources.ConstantMessage.GeoCodeNotSelected)
                        {
                            _coordinate = pnlCoords.InnerHtml.ToString().Replace(Resources.ConstantMessage.SelectedCoords + " <br />", String.Empty);
                        }
                    }

                    //Agregar combo de seleccion y aqui poner la seleccion
                    Int64 _idGeographicArea = Convert.ToInt64((_RtvGeographicArea.SelectedNode == null ? 0 : GetKeyValue(_RtvGeographicArea.SelectedNode.Value, "IdGeographicArea")));
                    GeographicArea _geoArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_idGeographicArea);
                    Int64 _idOrganization = Convert.ToInt64(GetKeyValue(_RtvOrganization.SelectedNode.Value, "IdOrganization"));
                    Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);

                    if (Entity == null)
                    {
                        //Alta
                        Entity = EMSLibrary.User.ProcessFramework.Map.ProcessGroupProcessAdd(Convert.ToInt16(txtWeight.Text), base.SetProcessOrder(txtOrder.Text), txtTitle.Text, txtPurpose.Text, txtDescription.Text, Convert.ToInt16(TxtThreshold.Text), txtIdentification.Text, _campaignDate, _resourceCatalog, _coordinate, _geoArea, _processClassifications, _organization, txtTwitterUser.Text, txtFacebookUser.Text);
                    }
                    else
                    {
                        //Modificacion
                        Entity.Modify(Convert.ToInt16(txtWeight.Text), base.SetProcessOrder(txtOrder.Text), txtTitle.Text, txtPurpose.Text, txtDescription.Text, Convert.ToInt16(TxtThreshold.Text), txtIdentification.Text, _campaignDate, _resourceCatalog, _coordinate, _geoArea, _processClassifications, _organization, txtTwitterUser.Text, txtFacebookUser.Text);
                    }
                    base.NavigatorAddTransferVar("IdProcess", Entity.IdProcess);

                    String _pkValues = "IdProcess=" + Entity.IdProcess.ToString();
                    base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);
                    base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.PF.ProcessGroupProcess);
                    base.NavigatorAddTransferVar("EntityNameContextInfo", Common.ConstantsEntitiesName.PF.Process);
                    base.NavigatorAddTransferVar("EntityNameContextElement", Common.ConstantsEntitiesName.PF.ProcessGroupProcess);

                    //Navigate(GetPageViewerByEntity(Common.ConstantsEntitiesName.PF.ProcessGroupProcess), Resources.CommonListManage.ProcessGroupProcess + " " + Entity.LanguageOption.Title, Condesus.WebUI.Navigation.DeleteType.DeleteModifiedEntity);
                    String _entityPropertyName = String.Concat(Entity.LanguageOption.Title);
                    NavigatePropertyEntity(Common.ConstantsEntitiesName.PF.ProcessGroupProcess, _entityPropertyName, Condesus.WebUI.Navigation.NavigateMenuAction.Add);

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
