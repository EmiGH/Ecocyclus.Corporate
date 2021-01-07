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
using Condesus.EMS.Business.GIS.Entities;
using System.Linq;


namespace Condesus.EMS.WebUI.AdministrationTools.DirectoryServices
{
    public partial class FacilitiesProperties : BaseProperties
    {
        #region Internal Properties
            private RadComboBox _RdcResourceCatalog;
            private RadTreeView _RtvResourceCatalog;
            private CompareValidator _CvResourceCatalog;
            private Facility _Entity = null;
            private Int64 _IdFacility
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdFacility") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdFacility")) : 0;   // Convert.ToInt64(GetPKfromNavigator("IdFacility"));
                }
            }
            private Int64 _IdOrganization
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdOrganization") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdOrganization")) : Convert.ToInt64(GetPKfromNavigator("IdOrganization"));
                }
            }
            private Facility Entity
            {
                get
                {
                    try
                    {
                        if (_Entity == null)
                            _Entity = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).Facility(_IdFacility);

                        return _Entity;
                    }
                    catch { return null; }
                }

                set { _Entity = value; }
            }
            CompareValidator _CvFacilityType;
            private RadComboBox _RdcFacilityType;
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
                AddCombos();
            }
            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);

                AddComboGeographicAreas();

                if (!Page.IsPostBack)
                {
                    String _dataObjects = String.Empty;
                    //Inicializo el Form
                    if (Entity == null)
                    { Add(); }
                    else
                    {
                        LoadData();  //Edit.
                        _dataObjects = Common.ConstantsEntitiesName.DS.Facility + "|IdOrganization=" + Entity.IdOrganization.ToString() + ",IdFacility=" + Entity.IdFacility.ToString();
                    }
                    //Inyecta los JS que permiten abrir la ventana con el mapa.
                    InjectOpenWindowDialogPickUpCoords(inputPoints.ClientID, drawModeType.ClientID, pnlCoords.ClientID, _dataObjects);

                    //Form
                    base.SetContentTableRowsCss(tblContentForm);
                }
            }
            protected override void SetPagetitle()
            {
                base.PageTitle = (Entity != null) ? Entity.LanguageOption.Name : Resources.CommonListManage.Facility;
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }
        #endregion

        #region Private Methods
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonListManage.Facility;
                lblFacilityType.Text = Resources.CommonListManage.FacilityType;
                lblDescription.Text = Resources.CommonListManage.Description;
                lblFacility.Text = Resources.CommonListManage.Facility;
                lblLanguage.Text = Resources.CommonListManage.Language;
                lblActive.Text = Resources.CommonListManage.Active;
                rfv1.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
                lblCoordenates.Text = Resources.CommonListManage.Coordenates;
                pnlCoords.InnerHtml = Resources.ConstantMessage.GeoCodeNotSelected;
            }
            private void Add()
            {
                base.StatusBar.Clear();

                lblLanguageValue.Text = Global.DefaultLanguage.Name;
                txtName.Text = String.Empty;
                txtDescription.Text = String.Empty;
                txtName.ReadOnly = false;
                txtDescription.ReadOnly = false;
            }
            private void LoadData()
            {
                Condesus.EMS.Business.GIS.Entities.Site_LG _facility_LG = _Entity.LanguagesOptions.Item(Global.DefaultLanguage.IdLanguage);

                base.PageTitle = _facility_LG.Name;

                if (!String.IsNullOrEmpty(Entity.Coordinate))
                {
                    pnlCoords.InnerHtml = Resources.ConstantMessage.SelectedCoords + " <br />" + Entity.Coordinate;

                    String _drawModeType = Entity.Coordinate.Split(';')[0];
                    drawModeType.Value = _drawModeType;
                    inputPoints.Value = Entity.Coordinate.Replace(_drawModeType + ";", String.Empty);
                }
                chkActive.Checked = Entity.Active;
                txtName.Text = _facility_LG.Name;
                txtDescription.Text = _facility_LG.Description;
                lblLanguageValue.Text = Entity.LanguageOption.Language.Name;
                txtName.ReadOnly = false;
                txtDescription.ReadOnly = false;
                SetResourceCatalog();
                SetFacilityType();
                SetGeographicArea();
            }
            private void AddCombos()
            {
                AddComboFacilityTypes();
                AddComboResourceCatalogues();
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
                if (Entity.GetType().Name == Common.ConstantsEntitiesName.DS.Facility)
                {
                    _RdcResourceCatalog.Enabled = true;
                    if (((Condesus.EMS.Business.GIS.Entities.Facility)Entity).Pictures.Count > 0)
                    {
                        Condesus.EMS.Business.KC.Entities.Resource _resource = EMSLibrary.User.KnowledgeCollaboration.Map.Resource(((Condesus.EMS.Business.GIS.Entities.Facility)Entity).Pictures.First().Value.IdResource);
                        String _keyValuesElement = "IdResource=" + _resource.IdResource.ToString();
                        if (_resource.Classifications.Count > 0)
                        {
                            String _keyValuesClassification = "IdResourceClassification=" + _resource.Classifications.First().Value.IdResourceClassification;
                            SelectItemTreeViewParentElementMaps(_keyValuesClassification, _keyValuesElement, ref _RtvResourceCatalog, ref _RdcResourceCatalog, Common.ConstantsEntitiesName.KC.ResourceClassification, Common.ConstantsEntitiesName.KC.ResourceClassificationChildren, Common.ConstantsEntitiesName.KC.ResourceCatalogues);
                        }
                        else
                        {
                            SelectItemTreeViewParent(_keyValuesElement, ref _RtvResourceCatalog, ref _RdcResourceCatalog, Common.ConstantsEntitiesName.KC.ResourceCatalog, Common.ConstantsEntitiesName.KC.ResourceCatalogues);
                        }
                    }
                }
            }
            private void AddComboFacilityTypes()
            {
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                AddCombo(phFacilityType, ref _RdcFacilityType, Common.ConstantsEntitiesName.DS.FacilityTypes, String.Empty, _params, false, true, false, false, false);
            
                ValidatorRequiredField(Common.ConstantsEntitiesName.DS.FacilityTypes, phFacilityTypeValidator, ref _CvFacilityType, _RdcFacilityType, Resources.ConstantMessage.SelectAMagnitud);
            }
            private void SetFacilityType()
            {
                _RdcFacilityType.SelectedValue = "IdFacilityType=" + Entity.FacilityType.IdFacilityType.ToString();
            }
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
        #endregion

        #region Page Events
            protected void rtvHierarchicalTreeViewInComboGeoArea_NodeExpand(object sender, RadTreeNodeEventArgs e)
            {
                NodeExpand(sender, e, Common.ConstantsEntitiesName.DS.GeographicAreaChildren, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, false, String.Empty);
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
                    //Si el key obtenido no llega a exister devuelve null.
                    Int64 _idResourceCatalog = Convert.ToInt64((_RtvResourceCatalog.SelectedNode == null ? 0 : GetKeyValue(_RtvResourceCatalog.SelectedNode.Value, "IdResource")));    //Si queda en cero 0, quiere decir que no asocia catalogos.
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
                    Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization);
                    //Seleccion del Facility Type
                    Int64 _idFacilityType = Convert.ToInt64(GetKeyValue(_RdcFacilityType.SelectedValue, "IdFacilityType"));
                    FacilityType _facilityType = EMSLibrary.User.GeographicInformationSystem.FacilityType(_idFacilityType);

                    Int64 _idGeographicArea = Convert.ToInt64((_RtvGeographicArea.SelectedNode == null ? 0 : GetKeyValue(_RtvGeographicArea.SelectedNode.Value, "IdGeographicArea")));
                    GeographicArea _geoArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_idGeographicArea);

                    if (Entity == null)
                    {
                        //Alta
                        Entity = _organization.FacilityAdd(_coordinate, txtName.Text, txtDescription.Text, _resourceCatalog, _facilityType, _geoArea, chkActive.Checked);
                    }
                    else
                    {
                        //Modificacion
                        Entity.Modify(_coordinate, txtName.Text, txtDescription.Text, _resourceCatalog, _facilityType, _geoArea, chkActive.Checked);
                    }

                    base.NavigatorAddTransferVar("IdOrganization", _IdOrganization);
                    base.NavigatorAddTransferVar("IdFacility", Entity.IdFacility);
                    String _pkValues = "IdOrganization=" + _IdOrganization.ToString()
                        + "& IdFacility=" + Entity.IdFacility.ToString();

                    base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);
                    base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.DS.Facility);
                    base.NavigatorAddTransferVar("EntityNameContextInfo", Common.ConstantsEntitiesName.DS.Organization);

                    String _entityPropertyName = String.Concat(Entity.LanguageOption.Name);
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
