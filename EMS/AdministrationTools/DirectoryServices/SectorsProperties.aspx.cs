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
using Condesus.EMS.Business.DS.Entities;
using Condesus.EMS.Business.GIS.Entities;

namespace Condesus.EMS.WebUI.AdministrationTools.DirectoryServices
{
    public partial class SectorsProperties : BaseProperties
    {
        #region Internal Properties
            private Sector _Entity = null;
            private Int64 _IdOrganization
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdOrganization") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdOrganization")) : Convert.ToInt64(GetPKfromNavigator("IdOrganization"));
                }
            }
            private Int64 _IdFacility
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdFacility") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdFacility")) : Convert.ToInt64(GetPKfromNavigator("IdFacility"));
                }
            }
            private Int64 _IdSector
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdSector") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdSector")) : 0;
                }
            }
            private Sector Entity
            {
                get
                {
                    try
                    {
                        if (_Entity == null)
                            _Entity = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).Facility(_IdFacility).Sector(_IdSector);

                        return _Entity;
                    }
                    catch { return null; }
                }

                set { _Entity = value; }
            }
            private RadTreeView _RtvSector;
            private RadComboBox _RdcSector;
            private RadComboBox _RdcResourceCatalog;
            private RadTreeView _RtvResourceCatalog;
            private CompareValidator _CvResourceCatalog;
            String _FilterExpressionSector;
            CompareValidator _CvFacilityType;
            private RadComboBox _RdcFacilityType;
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
                Organization _organization= EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization);
                lblOrganizationValue.Text = _organization.CorporateName;
                lblFacilityValue.Text = _organization.Facility(_IdFacility).LanguageOption.Name;

                if (!Page.IsPostBack)
                {
                    String _dataObjects = String.Empty;
                    //Inicializo el Form
                    if (Entity == null)
                        { Add(); }
                    else
                    {
                        LoadData();  //Edit.
                        _dataObjects = Common.ConstantsEntitiesName.DS.Sector + "|IdOrganization=" + Entity.IdOrganization.ToString() + ",IdFacility=" + _IdFacility.ToString() + ",IdSector=" + Entity.IdFacility.ToString();
                    }
                    //Inyecta los JS que permiten abrir la ventana con el mapa.
                    InjectOpenWindowDialogPickUpCoords(inputPoints.ClientID, drawModeType.ClientID, pnlCoords.ClientID, _dataObjects);

                    //Form
                    base.SetContentTableRowsCss(tblContentForm);
                }
            }
            protected override void SetPagetitle()
            {
                base.PageTitle = (Entity != null) ? Entity.LanguageOption.Name : Resources.CommonListManage.Sector;
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }
        #endregion

        #region Private Methods
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonListManage.Sector;
                lblSector.Text = Resources.CommonListManage.Sector;
                lblOrganization.Text = Resources.CommonListManage.Organization;
                lblFacility.Text = Resources.CommonListManage.Facility;
                lblIdParent.Text = Resources.CommonListManage.Parent;
                lblLanguage.Text = Resources.CommonListManage.Language;
                lblSector.Text = Resources.CommonListManage.Sector;
                lblActive.Text = Resources.CommonListManage.Active;
                lblResourceCatalog.Text = Resources.CommonListManage.Picture;
                rfv1.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
                lblCoordenates.Text = Resources.CommonListManage.Coordenates;
                pnlCoords.InnerHtml = Resources.ConstantMessage.GeoCodeNotSelected;
                lblFacilityType.Text = Resources.CommonListManage.FacilityType;
            }
            private void Add()
            {
                base.StatusBar.Clear();

                lblLanguageValue.Text = Global.DefaultLanguage.Name;
                txtSector.Text = String.Empty;
                txtDescription.Text = String.Empty;
            }
            private void LoadData()
            {
                base.PageTitle = Entity.LanguageOption.Name;

                chkActive.Checked = Entity.Active;
                txtSector.Text = Entity.LanguagesOptions.Item(Global.DefaultLanguage.IdLanguage).Name;
                txtDescription.Text = Entity.LanguagesOptions.Item(Global.DefaultLanguage.IdLanguage).Description;
                lblLanguageValue.Text = Entity.LanguageOption.Language.Name;
                if (!String.IsNullOrEmpty(Entity.Coordinate))
                {
                    pnlCoords.InnerHtml = Resources.ConstantMessage.SelectedCoords + " <br />" + Entity.Coordinate;
                }


                //si es un root, no debe hacer nada de esto.
                Int64 _idParentSector = 0;
                if (_Entity.Parent.IdFacility != _IdFacility) //si son iguales, quiere decir que no tiene sectores superiores.
                {
                    //En este caso, quiere decir que tiene un sector superior, entonces lo mete en el objeto.
                    _idParentSector = _Entity.Parent.IdFacility; //Lo guarda aca, para seguir usandolo luego.
                    //El orden de este KeyValue, debe ser el mismo al que se hace al momento de crear el tree, y el mismo del metodo publico del factory!!!
                    String _keyValues = "IdOrganization=" + _IdOrganization.ToString() + "& IdFacility=" + _IdFacility.ToString() + "& IdSector=" + _idParentSector.ToString();
                    RadTreeView _rtvSector = _RtvSector;
                    RadComboBox _rcbSector = _RdcSector;
                    //Realiza el seteo del parent en el Combo-Tree.
                    SelectItemTreeViewParent(_keyValues, ref _rtvSector, ref _rcbSector, Common.ConstantsEntitiesName.DS.Sector, Common.ConstantsEntitiesName.DS.SectorsChildren);
                    _RdcSector = _rcbSector;
                    _RtvSector = _rtvSector;
                }

                SetFacilityType();
                SetResourceCatalog();
            }
            private void AddCombos()
            {
                AddComboSectors();
                AddComboResourceCatalogues();
                AddComboFacilityTypes();
            }
            private void AddComboSectors()
            {
                //Combo Parent
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                _params.Add("IdOrganization", _IdOrganization);
                _params.Add("IdFacility", _IdFacility);
                AddComboWithTree(phSector, ref _RdcSector, ref _RtvSector,
                    Common.ConstantsEntitiesName.DS.Sectors, _params, false, false, true, ref _FilterExpressionSector,
                    new RadTreeViewEventHandler(rtvHierarchicalTreeViewInCombo_NodeExpand),
                    Resources.Common.ComboBoxNoDependency, false);
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
                if (Entity.GetType().Name == Common.ConstantsEntitiesName.DS.Sector)
                {
                    _RdcResourceCatalog.Enabled = true;
                    if (Entity.Pictures.Count > 0)
                    {
                        Condesus.EMS.Business.KC.Entities.Resource _resource = EMSLibrary.User.KnowledgeCollaboration.Map.Resource(Entity.Pictures.First().Value.IdResource);
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
            protected void rtvHierarchicalTreeViewInCombo_NodeExpand(object sender, RadTreeNodeEventArgs e)
            {
                NodeExpand(sender, e, Common.ConstantsEntitiesName.DS.SectorsChildren, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, false, String.Empty);
            }
            protected void btnSave_Click(object sender, EventArgs e)
            {
                try
                {
                    //Obtiene el key necesario.
                    Object _obj = GetKeyValue(_RtvSector.SelectedNode.Value, "IdSector");   //Si lo saco del tree, funciona!!!.
                    //Si el key obtenido no llega a exister devuelve null.
                    Int64 _parentValue = Convert.ToInt64((_obj == null ? 0 : _obj));    //Si queda en cero 0, quiere decir que esta dando trabajando con un ROOT.
                    Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization);
                    Facility _facility = _organization.Facility(_IdFacility);

                    Sector _sectorParent = _facility.Sector(_parentValue);
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

                    Int64 _idResourceCatalog = Convert.ToInt64((_RtvResourceCatalog.SelectedNode == null ? 0 : GetKeyValue(_RtvResourceCatalog.SelectedNode.Value, "IdResource")));    //Si queda en cero 0, quiere decir que no asocia catalogos.
                    Condesus.EMS.Business.KC.Entities.ResourceCatalog _resourceCatalog = (Condesus.EMS.Business.KC.Entities.ResourceCatalog)EMSLibrary.User.KnowledgeCollaboration.Map.Resource(_idResourceCatalog);

                    //Seleccion del Facility Type
                    Int64 _idFacilityType = Convert.ToInt64(GetKeyValue(_RdcFacilityType.SelectedValue, "IdFacilityType"));
                    FacilityType _facilityType = EMSLibrary.User.GeographicInformationSystem.FacilityType(_idFacilityType);

                    if (Entity == null)
                    {
                        //Alta
                        //Un sector puede ser hijo directo de un Facility o de otro sector
                        if (_sectorParent==null)
                        {
                            //En este caso es hijo directo de facility
                            Entity = _facility.SectorAdd(_coordinate, txtSector.Text, txtDescription.Text, _resourceCatalog, _facilityType, chkActive.Checked); 
                        }
                        else
                        {
                            //En este caso es hijo de otro Sector.
                            Entity = _sectorParent.SectorAdd(_coordinate, txtSector.Text, txtDescription.Text, _resourceCatalog, _facilityType, chkActive.Checked); 
                        }
                    }
                    else
                    {   //Solo para el Modify
                        //Si el Padre es null, quiere decir que es el primer sector y su padre es un Facility. por ese motivo, por defecto va el facility
                        if (_sectorParent == null)
                        {
                            Entity.Modify((Site)Entity.Parent, _coordinate, txtSector.Text, txtDescription.Text, _facilityType, _resourceCatalog, chkActive.Checked);
                        }
                        else
                        {
                            //Modificacion de un SubSector...
                            Entity.Modify(_sectorParent, _coordinate, txtSector.Text, txtDescription.Text, _facilityType, _resourceCatalog, chkActive.Checked);
                        }
                    }
                    base.NavigatorAddTransferVar("IdOrganization", _IdOrganization);
                    base.NavigatorAddTransferVar("IdFacility", _IdFacility);
                    base.NavigatorAddTransferVar("IdSector", Entity.IdFacility);
                    base.NavigatorAddTransferVar("IdParentSector", Entity.Parent.IdFacility);

                    String _pkValues = "IdOrganization=" + _IdOrganization.ToString() +
                        "& IdFacility=" + _IdFacility.ToString() +
                        "& IdSector=" + Entity.IdFacility.ToString() +
                        "& IdParentSector=" + Entity.Parent.IdFacility.ToString();
                    base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);

                    base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.DS.Sector);
                    base.NavigatorAddTransferVar("EntityNameContextInfo", Common.ConstantsEntitiesName.DS.Organization);
                    String _entityPropertyName = String.Concat(Entity.LanguageOption.Name);
                    NavigatePropertyEntity(Common.ConstantsEntitiesName.DS.Sector, _entityPropertyName, Condesus.WebUI.Navigation.NavigateMenuAction.Add);

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
