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

namespace Condesus.EMS.WebUI.DS
{
    public partial class GeographicAreasProperties : BaseProperties
    {
        #region Internal Properties
            private GeographicArea _Entity = null;
            private Int64 _IdGeographicArea
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdGeographicArea") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdGeographicArea")) : 0;
                }
            }
            private GeographicArea Entity
            {
                get
                {
                    try
                    {
                        if (_Entity == null)
                            _Entity = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_IdGeographicArea);

                        return _Entity;
                    }
                    catch { return null; }
                }

                set { _Entity = value; }
            }
            private RadComboBox _RdcGeographicArea;           
            private RadTreeView _RtvGeographicArea;
            private RadComboBox _RdcOrganization;
            private RadTreeView _RtvOrganization;
            private CompareValidator _CvOrganization;
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
                    String _dataObjects = String.Empty;
                    //Inicializo el Form
                    if (Entity == null)
                        { Add(); }
                    else
                    {
                        //Edit.
                        LoadData();
                        _dataObjects = Common.ConstantsEntitiesName.DS.GeographicArea + "|IdGeographicArea=" + Entity.IdGeographicArea.ToString();
                    }
                    //Inyecta los JS que permiten abrir la ventana con el mapa.
                    InjectOpenWindowDialogPickUpCoords(inputPoints.ClientID, drawModeType.ClientID, pnlCoords.ClientID, _dataObjects);

                    //Form
                    base.SetContentTableRowsCss(tblContentForm);                    
                    this.txtGeographicArea.Focus();
                }
            }
            //Setea el Titulo de la Pagina
            //(como no puedo pasar el Objeto como una 'Interface', lo tengo que hacer localmente)
            protected override void SetPagetitle()
            {
                base.PageTitle = (Entity != null) ? Entity.LanguageOption.Name : Resources.CommonListManage.GeographicArea;
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }
        #endregion

        #region Private Methods
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonListManage.GeographicArea;
                lblDescription.Text = Resources.CommonListManage.Description;
                lblGeographicArea.Text = Resources.CommonListManage.GeographicArea;
                lblIdParent.Text = Resources.CommonListManage.Parent;
                lblLanguage.Text = Resources.CommonListManage.Language;
                rfv1.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
                lblCoordenates.Text = Resources.CommonListManage.Coordenates;
                pnlCoords.InnerHtml = Resources.ConstantMessage.GeoCodeNotSelected;

                rbLayer.Items[0].Text = Resources.CommonListManage.Country;
                rbLayer.Items[1].Text = Resources.CommonListManage.Province;
                rbLayer.Items[2].Text = Resources.CommonListManage.Municipality;
            }
            private void ClearLocalSession()
            {
                _RdcGeographicArea = null;
                _RtvGeographicArea = null;
            }
            private void Add()
            {
                base.StatusBar.Clear();

                lblLanguageValue.Text = Global.DefaultLanguage.Name;
                txtGeographicArea.Text = String.Empty;
                txtDescription.Text = String.Empty;
                txtGeographicArea.ReadOnly = false;
                txtDescription.ReadOnly = false;
            }
            private void LoadData()
            {
                Condesus.EMS.Business.GIS.Entities.GeographicArea_LG _geographicArea_LG = _Entity.LanguagesOptions.Item(Global.DefaultLanguage.IdLanguage);

                base.PageTitle = _geographicArea_LG.Name;

                txtGeographicArea.Text = _geographicArea_LG.Name;
                txtDescription.Text = _geographicArea_LG.Description;
                lblLanguageValue.Text = Entity.LanguageOption.Language.Name;
                txtGeographicArea.ReadOnly = false;
                txtDescription.ReadOnly = false;

                if (!String.IsNullOrEmpty(Entity.Coordinate))
                {
                    pnlCoords.InnerHtml = Resources.ConstantMessage.SelectedCoords + " <br />" + Entity.Coordinate;
                }

                
                //si es un root, no debe hacer nada de esto.
                if (_Entity.IdParentGeographicArea != 0)
                {
                    //El orden de este KeyValue, debe ser el mismo al que se hace al momento de crear el tree, y el mismo del metodo publico del factory!!!
                    String _keyValues = "IdGeographicArea=" + _Entity.IdParentGeographicArea.ToString();
                    RadTreeView _rtvGeoArea = _RtvGeographicArea;
                    RadComboBox _rcbGeoArea = _RdcGeographicArea;
                    //Realiza el seteo del parent en el Combo-Tree.
                    SelectItemTreeViewParent(_keyValues, ref _rtvGeoArea, ref _rcbGeoArea, Common.ConstantsEntitiesName.DS.GeographicArea, Common.ConstantsEntitiesName.DS.GeographicAreaChildren);
                    _RdcGeographicArea = _rcbGeoArea;
                    _RtvGeographicArea = _rtvGeoArea;
                }

                SetOrganization();


                switch (Entity.Layer)
                {
                    case "Country":
                        rbLayer.Items.FindByValue("rbCountry").Selected = true;
                        break;
                    case "Province":
                        rbLayer.Items.FindByValue("rbProvince").Selected = true;
                        break;
                    case "Municipality":
                        rbLayer.Items.FindByValue("rbMunicipality").Selected = true;
                        break;
                }
            }
            private void AddCombos()
            {
                AddComboGeographicAreas();
                AddComboOrganizations();
            }
            //private void AddComboResourceCatalogues()
            //{
            //    String _filterExpression = String.Empty;
            //    //Combo de ResourceCatalog
            //    Dictionary<String, Object> _params = new Dictionary<String, Object>();
            //    AddComboWithTreeElementMaps(ref phResourceCatalog, ref _RdcResourceCatalog, ref _RtvResourceCatalog,
            //        Common.ConstantsEntitiesName.KC.ResourceClassifications, Common.ConstantsEntitiesName.KC.ResourceCatalogues, _params, false, true, false, ref _filterExpression,
            //        new RadTreeViewEventHandler(rtvResource_NodeExpand),
            //        Resources.Common.ComboBoxNoDependency, false);

            //    //if (chkAllowFacilities.Checked)
            //    //{//Solo se usa cuando es un Facility
            //    //    _RdcResourceCatalog.Enabled = true;
            //    //}
            //    //else
            //    //{ _RdcResourceCatalog.Enabled = false; }
            //}
            //private void SetResourceCatalog()
            //{
            //    //Seteamos la resourceCatalog...
            //    //Realiza el seteo del parent en el Combo-Tree.
            //    if (Entity.GetType().Name == Common.ConstantsEntitiesName.DS.GeographicAreaFacilities)
            //    {
            //        _RdcResourceCatalog.Enabled = true;
            //        if (((Condesus.EMS.Business.DS.Entities.GeographicAreaFacilities)Entity).Pictures.Count > 0)
            //        {
            //            Condesus.EMS.Business.KC.Entities.Resource _resource = EMSLibrary.User.KnowledgeCollaboration.Map.Resource(((Condesus.EMS.Business.DS.Entities.GeographicAreaFacilities)Entity).Pictures.First().Value.IdResource);
            //            String _keyValuesElement = "IdResource=" + _resource.IdResource.ToString();
            //            if (_resource.Classifications.Count > 0)
            //            {
            //                String _keyValuesClassification = "IdResourceClassification=" + _resource.Classifications.First().Value.IdResourceClassification;
            //                SelectItemTreeViewParentElementMaps(_keyValuesClassification, _keyValuesElement, ref _RtvResourceCatalog, ref _RdcResourceCatalog, Common.ConstantsEntitiesName.KC.ResourceClassification, Common.ConstantsEntitiesName.KC.ResourceClassificationChildren, Common.ConstantsEntitiesName.KC.ResourceCatalogues);
            //            }
            //            else
            //            {
            //                SelectItemTreeViewParent(_keyValuesElement, ref _RtvResourceCatalog, ref _RdcResourceCatalog, Common.ConstantsEntitiesName.KC.ResourceCatalog, Common.ConstantsEntitiesName.KC.ResourceCatalogues);
            //            }
            //        }
            //    }
            //}
            private void AddComboGeographicAreas()
            {
                String _filterExpression = String.Empty;
                //Combo de GeographicArea Parent
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                AddComboWithTree(phGeographicArea, ref _RdcGeographicArea, ref _RtvGeographicArea,
                    Common.ConstantsEntitiesName.DS.GeographicAreas, _params, false, false, true, ref _filterExpression,
                    new RadTreeViewEventHandler(rtvHierarchicalTreeViewInCombo_NodeExpand),
                    new RadTreeViewEventHandler(rtvHierarchicalTreeViewInCombo_NodeClick),
                    Resources.Common.ComboBoxNoDependency, false);
            }
            //protected void InjectClickCheckFacility(String chkFalilityName, String txtMnemoName, String rfvMnemo, String rdcResourceCatalogName)
            //{
            //    System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();
            //    _sbBuffer.Append("function chkClick() {                                                                                         \n");
            //    //Verifica que si esta chequeado el facilities, entonces debe habilitar el txtMnemo y el validator
            //    _sbBuffer.Append("  if (document.getElementById('" + chkFalilityName + "').checked) {                                           \n");
            //    _sbBuffer.Append("    document.getElementById('" + txtMnemoName + "').disabled = false;                                             \n");
            //    _sbBuffer.Append("    document.getElementById('" + rdcResourceCatalogName + "').disabled = true;                                \n");
            //    _sbBuffer.Append("    var myRvf = document.getElementById('" + rfvMnemo + "');                                              \n");
            //    _sbBuffer.Append("    ValidatorEnable(myRvf, true);                                                                             \n");
            //    _sbBuffer.Append("  }                                                                                                           \n");
            //    _sbBuffer.Append("else {                                                                                                        \n");
            //        //Como no es facility, debe inhabilitar el txtMnbemo y deshabilitar el validator.
            //    _sbBuffer.Append("    document.getElementById('" + txtMnemoName + "').value = '';                                                   \n");
            //    _sbBuffer.Append("    document.getElementById('" + txtMnemoName + "').disabled = true;                                              \n");
            //    _sbBuffer.Append("    document.getElementById('" + rdcResourceCatalogName + "').disabled = false;                               \n");
            //    _sbBuffer.Append("    var myRvf = document.getElementById('" + rfvMnemo + "');                                                  \n");
            //    _sbBuffer.Append("    ValidatorEnable(myRvf, false);                                                                            \n");
            //    _sbBuffer.Append("  }                                                                                                           \n");
            //    _sbBuffer.Append("}                                                                                                             \n");


            //    ScriptManager.RegisterClientScriptBlock(this.Page, typeof(UpdatePanel), "JSClickCheckFacility", _sbBuffer.ToString(), true);

            //}
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
                if (Entity.Government != null)
                {
                    //Seteamos la organizacion...
                    //Realiza el seteo del parent en el Combo-Tree.
                    Organization _oganization = EMSLibrary.User.DirectoryServices.Map.Organization(Entity.Government.IdOrganization);
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
            protected void rtvHierarchicalTreeViewInCombo_NodeExpand(object sender, RadTreeNodeEventArgs e)
            {
                NodeExpand(sender, e, Common.ConstantsEntitiesName.DS.GeographicAreaChildren, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, false, String.Empty);               
            }
            protected void rtvHierarchicalTreeViewInCombo_NodeClick(object sender, RadTreeNodeEventArgs e)
            {
                _RdcGeographicArea.Text = e.Node.Text;
                _RdcGeographicArea.SelectedValue = e.Node.Value;
                //Verifica la seleccion y actua en concecuencia(habilita/deshabilita check...)
                SetParentGeographicArea(e.Node.Value);
            }
            private void SetParentGeographicArea(String value)
            {
                //Obtiene el key necesario.
                Object _obj = GetKeyValue(value, "IdGeographicArea");
                //Si el key obtenido no llega a exister devuelve null.
                Int64 _idGeoArea = Convert.ToInt64((_obj == null ? 0 : _obj));

                //Si queda en cero 0, quiere decir que esta dando trabajando con un ROOT.
                if (_idGeoArea != 0)
                {
                    if (_Entity == null)
                    {
                        Condesus.EMS.Business.GIS.Entities.GeographicArea _geographicArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_idGeoArea);
                    }
                }
            }
            protected void btnSave_Click(object sender, EventArgs e)
            {
                GeographicAreasPropertiesSave();
            }
            private void GeographicAreasPropertiesSave()
            {
                try
                {
                    //Obtiene el key necesario.
                    Object _obj = GetKeyValue(_RtvGeographicArea.SelectedNode.Value, "IdGeographicArea");   //Si lo saco del tree, funciona!!!.
                    //Si el key obtenido no llega a exister devuelve null.
                    Int64 _parentValue = Convert.ToInt64((_obj == null ? 0 : _obj));    //Si queda en cero 0, quiere decir que esta dando trabajando con un ROOT.
                    GeographicArea _geoAreaParent = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_parentValue);
                    Int64 _idOrganization = Convert.ToInt64(GetKeyValue(_RtvOrganization.SelectedNode.Value, "IdOrganization"));
                    Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                    //Si el key obtenido no llega a exister devuelve null.
                    //Int64 _idResourceCatalog = Convert.ToInt64((_RtvResourceCatalog.SelectedNode == null ? 0 : GetKeyValue(_RtvResourceCatalog.SelectedNode.Value, "IdResource")));    //Si queda en cero 0, quiere decir que no asocia catalogos.
                    //Condesus.EMS.Business.KC.Entities.ResourceCatalog _resourceCatalog = (Condesus.EMS.Business.KC.Entities.ResourceCatalog)EMSLibrary.User.KnowledgeCollaboration.Map.Resource(_idResourceCatalog);

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

                    String _layer = String.Empty;
                    if (rbLayer.Items.FindByValue("rbCountry").Selected)
                    {
                        _layer = "Country";
                    }
                    else
                    {
                        if (rbLayer.Items.FindByValue("rbProvince").Selected)
                        {
                            _layer = "Province";
                        }
                        else
                        {
                            if (rbLayer.Items.FindByValue("rbMunicipality").Selected)
                            {
                                _layer = "Municipality";
                            }
                        }
                    }

                    if (Entity == null)
                    {
                        //Alta
                        Entity = EMSLibrary.User.GeographicInformationSystem.GeographicAreasAdd(_geoAreaParent, _coordinate, txtISOCode.Text, _organization, txtGeographicArea.Text, txtDescription.Text, _layer);
                    }
                    else
                    {
                        //Modificacion
                        Entity.Modify(_geoAreaParent, _coordinate, txtISOCode.Text, _organization, txtGeographicArea.Text, txtDescription.Text, _layer);
                    }

                    //base.NavigatorAddTransferVar("IdOrganization", Entity.IdOrganization);
                    base.NavigatorAddTransferVar("IdGeographicArea", Entity.IdGeographicArea);
                    base.NavigatorAddTransferVar("IdParentGeographicArea", Entity.IdParentGeographicArea);
                    String _pkValues = "IdGeographicArea=" + Entity.IdGeographicArea.ToString()
                        + "& IdParentGeographicArea=" + Entity.IdParentGeographicArea.ToString();
                    base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);
                    base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.DS.GeographicArea);
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
            //protected void rtvResource_NodeExpand(object sender, RadTreeNodeEventArgs e)
            //{
            //    //Limpio los hijos, para no duplicar al abrir y cerrar.
            //    e.Node.Nodes.Clear();
            //    Dictionary<String, Object> _params = new Dictionary<String, Object>();
            //    _params = GetKeyValues(e.Node.Value);

            //    //Primero lo hace sobre las Clasificaciones Hijas...
            //    //Actualiza el Data table con los hijos, para poder asociarlas al nodo expadido.
            //    BuildGenericDataTable(Common.ConstantsEntitiesName.KC.ResourceClassificationChildren, _params);
            //    if (DataTableListManage.ContainsKey(Common.ConstantsEntitiesName.KC.ResourceClassificationChildren))
            //    {
            //        foreach (DataRow _drRecord in DataTableListManage[Common.ConstantsEntitiesName.KC.ResourceClassificationChildren].Rows)
            //        {
            //            RadTreeNode _node = SetElementMapsNodeTreeView(_drRecord, Common.ConstantsEntitiesName.KC.ResourceClassificationChildren, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, String.Empty, String.Empty, String.Empty);
            //            e.Node.Nodes.Add(_node);
            //            //Como es un Mapa de Elementos, indica true para que cargue los elementos debajo de la classificacion.
            //            SetExpandMode(_node, Common.ConstantsEntitiesName.KC.ResourceClassificationChildren, true, true);
            //        }
            //    }

            //    //Ahora lo hace sobre los Elementos Hijos de la Clasificacion expandida.
            //    BuildGenericDataTable(Common.ConstantsEntitiesName.KC.ResourceCatalogues, _params);
            //    if (DataTableListManage.ContainsKey(Common.ConstantsEntitiesName.KC.ResourceCatalogues))
            //    {
            //        foreach (DataRow _drRecord in DataTableListManage[Common.ConstantsEntitiesName.KC.ResourceCatalogues].Rows)
            //        {
            //            RadTreeNode _node = SetElementMapsNodeTreeView(_drRecord, Common.ConstantsEntitiesName.KC.ResourceCatalogues, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, String.Empty, String.Empty, String.Empty);
            //            e.Node.Nodes.Add(_node);
            //        }
            //    }
            //}
        #endregion
    }
}
