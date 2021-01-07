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
using Condesus.EMS.Business.PA.Entities;
using System.Linq;

namespace Condesus.EMS.WebUI.PA
{
    public partial class MeasurementDevicesProperties : BaseProperties
    {
        #region Internal Properties
            private RadComboBox _RdcResourceCatalog;
            private RadTreeView _RtvResourceCatalog;
            CompareValidator _CvMeasurementDeviceType;
            private MeasurementDevice _Entity = null;
            private Int64 _IdMeasurementDeviceType
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdMeasurementDeviceType") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdMeasurementDeviceType")) : 0;
                }
            }
            private Int64 _IdMeasurementDevice
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdMeasurementDevice") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdMeasurementDevice")) : 0;
                }
            }
            private MeasurementDevice Entity
            {
                get
                {
                    try
                    {
                        if (_Entity == null)
                            _Entity = EMSLibrary.User.PerformanceAssessments.Configuration.MeasurementDeviceType(_IdMeasurementDeviceType).MeasurementDevice(_IdMeasurementDevice);

                        return _Entity;
                    }
                    catch { return null; }
                }

                set { _Entity = value; }
            }
            private RadComboBox _RdcMeasurementDeviceType;
            private RadTreeView _RtvMeasurementUnit;
            private ArrayList _MeasurementUnitAux //Estructura interna para guardar los id de Magnitud y Units que son seleccionados.
            {
                get
                {
                    if (ViewState["MeasurementUnitAux"] == null)
                    {
                        ViewState["MeasurementUnitAux"] = new ArrayList();
                    }
                    return (ArrayList)ViewState["MeasurementUnitAux"];
                }
                set { ViewState["MeasurementUnitAux"] = value; }
            }
            private RadComboBox _RdcSite;
            private RadTreeView _RtvSite;
        #endregion

        #region PageLoad & Init
            protected override void InitializeHandlers()
            {
                base.InitializeHandlers();

                //Delete - EventHandler y Registra el Boton Delete como Async
                //btnOkDelete.Click += new EventHandler(btnOkDelete_Click);
                //FwMasterPage.RegisterContentAsyncPostBackTrigger(btnOkDelete, "Click");

                //Le paso el Save a la MasterContentToolbar
                EventHandler _saveEventHandler = new EventHandler(btnSave_Click);
                FwMasterPage.ContentNavigatorToolbarFileActionAdd(_saveEventHandler, MasterFwContentToolbarAction.Save, true);
            }
            protected override void OnInit(EventArgs e)
            {
                base.OnInit(e);
                LoadTextLabels();
                AddTreeViewMeasurementUnits();
                AddCombos();
                AddValidators();
                base.InjectCheckIndexesTags();
            }
            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);

                if (!Page.IsPostBack)
                {
                    //Inicializo el Form
                    if (Entity == null)
                        { Add(); }
                    else
                        { LoadData(); } //Edit.

                    //Form
                    base.SetContentTableRowsCss(tblContentForm);

                    //Realiza la carga de los datos de units, para que se puedan usar.
                    LoadDataMeasurementUnit();
                }
            }
            protected override void SetPagetitle()
            {
                base.PageTitle = (Entity != null) ? Entity.FullName : Resources.CommonListManage.MeasurementDevice;
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }
        #endregion

        #region Private Methods
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonListManage.MeasurementDevice;
                lblBrand.Text = Resources.CommonListManage.Brand;
                lblCalibrationPeriodicity.Text = Resources.CommonListManage.CalibrationPeriodicity;
                lblInstallationDate.Text = Resources.CommonListManage.InstallationDate;
                lblMaintenance.Text = Resources.CommonListManage.Maintenance;
                lblMeasurementDeviceType.Text = Resources.CommonListManage.MeasurementDeviceType;
                lblMeasurementUnit.Text = Resources.CommonListManage.MeasurementUnit;
                lblModel.Text = Resources.CommonListManage.Model;
                lblReference.Text = Resources.CommonListManage.Reference;
                lblResourceCatalog.Text = Resources.CommonListManage.ResourceCatalog;
                lblSerialNumber.Text = Resources.CommonListManage.SerialNumber;
                lblSite.Text = Resources.CommonListManage.Site;
                lblUpperLimit.Text = Resources.CommonListManage.UpperLimit;
                lblLowerLimit.Text = Resources.CommonListManage.LowerLimit;
                lblUncertainty.Text = Resources.CommonListManage.Uncertainty;
                rfv1.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
                rfv2.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
                revLowerLimit.ErrorMessage = Resources.ConstantMessage.ValidationWrongFormat;
                revUncertainty.ErrorMessage = Resources.ConstantMessage.ValidationWrongFormat;
                revUpperLimit.ErrorMessage = Resources.ConstantMessage.ValidationWrongFormat;
                cv2.ErrorMessage = Resources.ConstantMessage.ValidationWrongFormat;
            }
            private void AddCombos()
            {
                AddComboMeasurementDeviceType();
                AddComboResourceCatalogues();
                AddComboSites();
            }
            private void AddValidators()
            {
                ValidatorRequiredField(Common.ConstantsEntitiesName.PA.MeasurementDeviceTypes, phMeasurementDeviceTypeValidator, ref _CvMeasurementDeviceType, _RdcMeasurementDeviceType, Resources.ConstantMessage.SelectAMeasurementDeviceType);
            }
            private void AddComboMeasurementDeviceType()
            {
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                AddCombo(phMeasurementDeviceType, ref _RdcMeasurementDeviceType, Common.ConstantsEntitiesName.PA.MeasurementDeviceTypes, String.Empty, _params, false, true, false, false, false);
            }
            private void AddTreeViewMeasurementUnits()
            {
                //Setea los datos en el DataTable de base, para que luego se carge la grilla.
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                BuildGenericDataTable(Common.ConstantsEntitiesName.PA.Magnitudes, _params);

                //Arma tree con todos los roots.
                phMeasurementUnit.Controls.Clear();
                _RtvMeasurementUnit= base.BuildHierarchicalListManageContent(Common.ConstantsEntitiesName.PA.Magnitudes, "Form");
                //Ya tengo el Tree le attacho los Handlers
                _RtvMeasurementUnit.NodeExpand += new RadTreeViewEventHandler(_RtvMeasurementUnit_NodeExpand);
                _RtvMeasurementUnit.NodeCreated += new RadTreeViewEventHandler(_RtvMeasurementUnit_NodeCreated);
                _RtvMeasurementUnit.NodeCheck += new RadTreeViewEventHandler(_RtvMeasurementUnit_NodeCheck);
                phMeasurementUnit.Controls.Add(_RtvMeasurementUnit);
            }
            /// <summary>
            /// Este metodo se encarga solamente de cargar los datos dentro del TreeView que ya debe existir en la pagina (que se crea en el init)
            /// </summary>
            private void LoadDataMeasurementUnit()
            {
                _RtvMeasurementUnit.Nodes.Clear();
                //Con el tree ya armado, ahora hay que llenarlo con datos.
                base.LoadGenericTreeView(ref _RtvMeasurementUnit, Common.ConstantsEntitiesName.PA.Magnitudes, Common.ConstantsEntitiesName.PA.Magnitud, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, true, String.Empty, false, false);
            }
            private void LoadStructMeasurementUnitAux()
            {
                //Carga de forma inicial todos los id de clasificacion que ya estan cargados en este proyecto.
                _MeasurementUnitAux = new ArrayList();
                Dictionary<Int64, Condesus.EMS.Business.PA.Entities.MeasurementUnit> _measurementUnits = new Dictionary<Int64, Condesus.EMS.Business.PA.Entities.MeasurementUnit>();
                if (_IdMeasurementDevice != 0)
                {
                    _measurementUnits = EMSLibrary.User.PerformanceAssessments.Configuration.MeasurementDeviceType(_IdMeasurementDeviceType).MeasurementDevice(_IdMeasurementDevice).MeasurementUnits;
                }
                //Ahora recorre todas las clasificaciones que ya tiene asiganadas el indicador, y los guarda en la estructura interna (ArrayList).
                foreach (Condesus.EMS.Business.PA.Entities.MeasurementUnit _item in _measurementUnits.Values)
                {
                    _MeasurementUnitAux.Add("IdMeasurementUnit=" + _item.IdMeasurementUnit.ToString() + "& IdMagnitud=" + _item.Magnitud.IdMagnitud.ToString());
                }
            }
            private void LoadData()
            {
                base.PageTitle = Entity.FullName;

                txtBrand.Text = Entity.Brand;
                txtModel.Text = Entity.Model;
                txtReference.Text = Entity.Reference;
                txtSerialNumber.Text = Entity.SerialNumber;
                txtCalibrationPeriodicity.Text = Entity.CalibrationPeriodicity;
                txtMaintenance.Text = Entity.Maintenance;
                rdtInstallationDate.SelectedDate = Entity.InstallationDate;
                txtUpperLimit.Text = Entity.UpperLimit.ToString();
                txtLowerLimit.Text = Entity.LowerLimit.ToString();
                txtUncertainty.Text = Entity.Uncertainty.ToString();

                //No se puede cambiar el type...
                _RdcMeasurementDeviceType.Enabled = false;
                SetMeasurementDeviceType();
                LoadStructMeasurementUnitAux();
                SetResourceCatalog();
                SetSite();
            }
            private void SetMeasurementDeviceType()
            {
                _RdcMeasurementDeviceType.SelectedValue = "IdMeasurementDeviceType=" + Entity.DeviceType.IdMeasurementDeviceType.ToString();
            }
            private void Add()
            {
                base.StatusBar.Clear();

                _RdcMeasurementDeviceType.Enabled = true;

                //limpio los textbox por si hay datos
                txtBrand.Text = String.Empty;
                txtModel.Text = String.Empty;
                txtReference.Text = String.Empty;
                txtSerialNumber.Text = String.Empty;
                txtCalibrationPeriodicity.Text = String.Empty;
                txtMaintenance.Text = String.Empty;
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
                if ((Entity.Pictures != null) && (Entity.Pictures.Count > 0))
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
            private void AddComboSites()
            {
                String _filterExpression = String.Empty;
                //Combo de Organizaciones
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                AddComboTreeSites(ref phSite, ref _RdcSite, ref _RtvSite, new RadTreeViewEventHandler(rtvSite_NodeExpand));
            }
            private void SetSite()
            {
                Condesus.EMS.Business.GIS.Entities.Site _site = Entity.Site;
                if (_site != null)
                {
                    if (_site.GetType().Name == Common.ConstantsEntitiesName.DS.Facility)
                    {
                        //Si el sitio seleccionado es un facility...
                        //Seteamos la organizacion...
                        //Realiza el seteo del parent en el Combo-Tree.
                        Condesus.EMS.Business.DS.Entities.Organization _oganization = Entity.Site.Organization;
                        String _keyValuesElement = "IdOrganization=" + _oganization.IdOrganization.ToString() + "& IdFacility=" + _site.IdFacility.ToString();
                        if (_oganization.Classifications.Count > 0)
                        {
                            String _keyValuesClassification = "IdOrganizationClassification=" + _oganization.Classifications.First().Value.IdOrganizationClassification;
                            SelectItemTreeViewParentElementMapsForSite(_keyValuesClassification, _keyValuesElement, ref _RtvSite, ref _RdcSite, Common.ConstantsEntitiesName.DS.OrganizationClassification, Common.ConstantsEntitiesName.DS.OrganizationClassificationsChildren, Common.ConstantsEntitiesName.DS.Organizations, true);
                        }
                        //Ahora busco el facility....
                        SelectItemTreeViewParentForSite(_keyValuesElement, ref _RtvSite, ref _RdcSite, Common.ConstantsEntitiesName.DS.Facility, Common.ConstantsEntitiesName.DS.Facilities, true);
                    }
                    else
                    {
                        //Si es un sector...se hace un poquito mas complejo, ya que puede estar muy anidado...
                        if (_site.GetType().Name == Common.ConstantsEntitiesName.DS.Sector)
                        {
                            //Casteo al sector!!!
                            Condesus.EMS.Business.GIS.Entities.Sector _sector = (Condesus.EMS.Business.GIS.Entities.Sector)_site;
                            //Tengo que obtener el facility de este sector!!!
                            while (_sector.Parent.GetType().Name == Common.ConstantsEntitiesName.DS.Sector)
                            {
                                _sector = (Condesus.EMS.Business.GIS.Entities.Sector)_sector.Parent;
                            }
                            //Al salir de este while, tengo el facility;
                            Condesus.EMS.Business.GIS.Entities.Facility _facility = (Condesus.EMS.Business.GIS.Entities.Facility)_sector.Parent;
                            //Ahora busco la organizacion y todo el arbol!
                            Condesus.EMS.Business.DS.Entities.Organization _oganization = Entity.Site.Organization;
                            String _keyValuesElement = "IdOrganization=" + _oganization.IdOrganization.ToString() + "& IdFacility=" + _facility.IdFacility.ToString() + "& IdSector=" + _site.IdFacility.ToString(); 
                            if (_oganization.Classifications.Count > 0)
                            {
                                String _keyValuesClassification = "IdOrganizationClassification=" + _oganization.Classifications.First().Value.IdOrganizationClassification;
                                SelectItemTreeViewParentElementMapsForSite(_keyValuesClassification, _keyValuesElement, ref _RtvSite, ref _RdcSite, Common.ConstantsEntitiesName.DS.OrganizationClassification, Common.ConstantsEntitiesName.DS.OrganizationClassificationsChildren, Common.ConstantsEntitiesName.DS.Organizations, true);
                            }
                            //Ahora busco el sector....
                            SelectItemTreeViewParentForSite(_keyValuesElement, ref _RtvSite, ref _RdcSite, Common.ConstantsEntitiesName.DS.Sector, Common.ConstantsEntitiesName.DS.Sectors, true);
                        }
                    }
                }
            }
        #endregion

        #region Page Events
            protected void rtvSite_NodeExpand(object sender, RadTreeNodeEventArgs e)
            {
                NodeExpandSites(sender, e, true);
            }

            //if (e.Node != null)
            //{
            //    //Limpio los hijos, para no duplicar al abrir y cerrar.
            //    e.Node.Nodes.Clear();
            //    Dictionary<String, Object> _params = new Dictionary<String, Object>();
            //    _params = GetKeyValues(e.Node.Value);

            //    String _singleEntityName = e.Node.Attributes["SingleEntityName"];
            //    switch (_singleEntityName)
            //    {
            //        case Common.ConstantsEntitiesName.DS.OrganizationClassification:    //esta expandiende una Classificacion!!!
            //            //Primero lo hace sobre las Clasificaciones Hijas...
            //            //Actualiza el Data table con los hijos, para poder asociarlas al nodo expadido.
            //            BuildGenericDataTable(Common.ConstantsEntitiesName.DS.OrganizationClassificationsChildren, _params);
            //            if (DataTableListManage.ContainsKey(Common.ConstantsEntitiesName.DS.OrganizationClassificationsChildren))
            //            {
            //                foreach (DataRow _drRecord in DataTableListManage[Common.ConstantsEntitiesName.DS.OrganizationClassificationsChildren].Rows)
            //                {
            //                    RadTreeNode _node = SetElementMapsNodeTreeView(_drRecord, Common.ConstantsEntitiesName.DS.OrganizationClassificationsChildren, Common.ConstantsEntitiesName.DS.OrganizationClassification, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, String.Empty, String.Empty, String.Empty);
            //                    e.Node.Nodes.Add(_node);
            //                    e.Node.Expanded = true;
            //                    //Como es un Mapa de Elementos, indica true para que cargue los elementos debajo de la classificacion.
            //                    SetExpandMode(_node, Common.ConstantsEntitiesName.DS.OrganizationClassificationsChildren, true);
            //                }
            //            }
            //            //Ahora lo hace sobre los Elementos Hijos de la Clasificacion expandida.
            //            BuildGenericDataTable(Common.ConstantsEntitiesName.DS.OrganizationsWithFacility, _params);
            //            if (DataTableListManage.ContainsKey(Common.ConstantsEntitiesName.DS.OrganizationsWithFacility))
            //            {
            //                foreach (DataRow _drRecord in DataTableListManage[Common.ConstantsEntitiesName.DS.OrganizationsWithFacility].Rows)
            //                {
            //                    RadTreeNode _node = SetElementMapsNodeTreeView(_drRecord, Common.ConstantsEntitiesName.DS.OrganizationsWithFacility, Common.ConstantsEntitiesName.DS.Organization, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, String.Empty, String.Empty, String.Empty);
            //                    //Si esta aca, quiere decir que tiene hijos!
            //                    _node.ExpandMode = TreeNodeExpandMode.ServerSide;
            //                    e.Node.Nodes.Add(_node);
            //                    e.Node.Expanded = true;
            //                }
            //            }
            //            break;

            //        case Common.ConstantsEntitiesName.DS.Organization:
            //            //Ahora lo hace sobre los Elementos Hijos de la Clasificacion expandida.
            //            BuildGenericDataTable(Common.ConstantsEntitiesName.DS.Facilities, _params);
            //            if (DataTableListManage.ContainsKey(Common.ConstantsEntitiesName.DS.Facilities))
            //            {
            //                foreach (DataRow _drRecord in DataTableListManage[Common.ConstantsEntitiesName.DS.Facilities].Rows)
            //                {
            //                    RadTreeNode _node = SetElementMapsNodeTreeView(_drRecord, Common.ConstantsEntitiesName.DS.Facilities, Common.ConstantsEntitiesName.DS.Facility, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, String.Empty, String.Empty, String.Empty);
            //                    e.Node.Nodes.Add(_node);
            //                    e.Node.Expanded = true;
            //                    //Como es un Mapa de Elementos, indica true para que cargue los elementos debajo de la classificacion.
            //                    SetExpandMode(_node, Common.ConstantsEntitiesName.DS.Facilities, true);
            //                }
            //            }
            //            break;

            //        case Common.ConstantsEntitiesName.DS.Facility:
            //            //Ahora lo hace sobre los Elementos Hijos de la Clasificacion expandida.
            //            BuildGenericDataTable(Common.ConstantsEntitiesName.DS.Sectors, _params);
            //            if (DataTableListManage.ContainsKey(Common.ConstantsEntitiesName.DS.Sectors))
            //            {
            //                foreach (DataRow _drRecord in DataTableListManage[Common.ConstantsEntitiesName.DS.Sectors].Rows)
            //                {
            //                    RadTreeNode _node = SetElementMapsNodeTreeView(_drRecord, Common.ConstantsEntitiesName.DS.Sectors, Common.ConstantsEntitiesName.DS.Sector, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, String.Empty, String.Empty, String.Empty);
            //                    e.Node.Nodes.Add(_node);
            //                    e.Node.Expanded = true;
            //                    //Como es un Mapa de Elementos, indica true para que cargue los elementos debajo de la classificacion.
            //                    SetExpandMode(_node, Common.ConstantsEntitiesName.DS.SectorsChildren, true);
            //                }
            //            }
            //            break;

            //        case Common.ConstantsEntitiesName.DS.Sector:
            //            //Ahora lo hace sobre los Elementos Hijos de la Clasificacion expandida.
            //            BuildGenericDataTable(Common.ConstantsEntitiesName.DS.SectorsChildren, _params);
            //            if (DataTableListManage.ContainsKey(Common.ConstantsEntitiesName.DS.SectorsChildren))
            //            {
            //                foreach (DataRow _drRecord in DataTableListManage[Common.ConstantsEntitiesName.DS.SectorsChildren].Rows)
            //                {
            //                    RadTreeNode _node = SetElementMapsNodeTreeView(_drRecord, Common.ConstantsEntitiesName.DS.SectorsChildren, Common.ConstantsEntitiesName.DS.Sector, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, String.Empty, String.Empty, String.Empty);
            //                    e.Node.Nodes.Add(_node);
            //                    e.Node.Expanded = true;
            //                    //Como es un Mapa de Elementos, indica true para que cargue los elementos debajo de la classificacion.
            //                    SetExpandMode(_node, Common.ConstantsEntitiesName.DS.SectorsChildren, true);
            //                }
            //            }
            //            break;
            //    }
            //}

            void _RtvMeasurementUnit_NodeExpand(object sender, RadTreeNodeEventArgs e)
            {
                base.NodeExpand(sender, e, Common.ConstantsEntitiesName.PA.MeasurementUnits, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, true, String.Empty);
            }
            void _RtvMeasurementUnit_NodeCreated(object sender, RadTreeNodeEventArgs e)
            {
                //Si el level es cero (0) quiere decir que es una magnitud, entonces no debe tener checkbox.
                if (e.Node.Level == 0)
                {
                    //Si es magnitud, no pone check...
                    e.Node.Checkable = false;
                }
                else
                {
                    if (_MeasurementUnitAux.Contains(e.Node.Value))
                    {
                        e.Node.Checked = true;
                    }
                    else
                    {
                        e.Node.Checked = false;
                    }
                }
            }
            void _RtvMeasurementUnit_NodeCheck(object sender, RadTreeNodeEventArgs e)
            {
                RadTreeNode _node = e.Node;

                //Obtiene el Id del nodo checkeado
                Int64 _idMeasurementUnit = Convert.ToInt64(GetKeyValue(_node.Value, "IdMeasurementUnit"));
                if (_MeasurementUnitAux.Contains(_node.Value))
                {
                    if (!_node.Checked)
                    {
                        _MeasurementUnitAux.Remove(_node.Value);
                    }
                }
                else
                {
                    _MeasurementUnitAux.Add(_node.Value);
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
                    //Se deben insertar el device con 1 o muchos units.
                    Dictionary<Int64, Condesus.EMS.Business.PA.Entities.MeasurementUnit> _measurementUnits = new Dictionary<Int64, Condesus.EMS.Business.PA.Entities.MeasurementUnit>();
                    //Aca recorre el ArrayList con los id que fueron chequeado, y contruye un Dictionary, para pasar al ADD.
                    foreach (String _item in _MeasurementUnitAux)
                    {
                        Int64 _idMagnitud = Convert.ToInt64(GetKeyValue(_item, "IdMagnitud"));
                        Int64 _idMeasurementUnit = Convert.ToInt64(GetKeyValue(_item, "IdMeasurementUnit"));
                        _measurementUnits.Add(_idMeasurementUnit, EMSLibrary.User.PerformanceAssessments.Configuration.Magnitud(_idMagnitud).MeasurementUnit(_idMeasurementUnit));
                    }

                    DateTime? _installationDate;
                    try
                    {
                        _installationDate = Convert.ToDateTime(rdtInstallationDate.SelectedDate);
                    }
                    catch (System.FormatException)
                    {
                        _installationDate = null;
                    }
                    //Si el key obtenido no llega a exister devuelve null.
                    Int64 _idResourceCatalog = Convert.ToInt64((_RtvResourceCatalog.SelectedNode == null ? 0 : GetKeyValue(_RtvResourceCatalog.SelectedNode.Value, "IdResource")));    //Si queda en cero 0, quiere decir que no asocia catalogos.
                    Condesus.EMS.Business.KC.Entities.ResourceCatalog _resourceCatalog = (Condesus.EMS.Business.KC.Entities.ResourceCatalog)EMSLibrary.User.KnowledgeCollaboration.Map.Resource(_idResourceCatalog);

                    //Agregar la opcion de seleccion de un Site...
                    Condesus.EMS.Business.GIS.Entities.Site _site = null;
                    //Si no hay nada seleccionado, queda en null
                    if (_RtvSite.SelectedNode != null)
                    {
                        String _entityNameSiteSelected = _RtvSite.SelectedNode.Attributes["SingleEntityName"];
                        Int64 _idSite = 0;
                        switch (_entityNameSiteSelected)
                        {
                            case Common.ConstantsEntitiesName.DS.Facility:
                                _idSite = Convert.ToInt64((_RtvSite.SelectedNode == null ? 0 : GetKeyValue(_RtvSite.SelectedNode.Value, "IdFacility")));    //Si queda en cero 0, quiere decir que no asocia.
                                break;
                            case Common.ConstantsEntitiesName.DS.Sector:
                                _idSite = Convert.ToInt64((_RtvSite.SelectedNode == null ? 0 : GetKeyValue(_RtvSite.SelectedNode.Value, "IdSector")));    //Si queda en cero 0, quiere decir que no asocia.
                                break;
                        }
                        _site = (Condesus.EMS.Business.GIS.Entities.Site)EMSLibrary.User.GeographicInformationSystem.Site(_idSite);
                    }
                    //Convierte los datos numericos para pasar al ADD.
                    Double _upperLimit;
                    Double _lowerLimit;
                    Double _uncertainty;
                    _upperLimit = Convert.ToDouble(String.IsNullOrEmpty(txtUpperLimit.Text) == true ? "0" : txtUpperLimit.Text);
                    _lowerLimit = Convert.ToDouble(String.IsNullOrEmpty(txtLowerLimit.Text) == true ? "0" : txtLowerLimit.Text);
                    _uncertainty = Convert.ToDouble(String.IsNullOrEmpty(txtUncertainty.Text) == true ? "0" : txtUncertainty.Text);

                    //verifica si es un ADD o un Modify
                    if (Entity == null)
                    {
                        Int64 _idMeasurementDeviceType = Convert.ToInt64(GetKeyValue(_RdcMeasurementDeviceType.SelectedValue, "IdMeasurementDeviceType"));
                        Entity = EMSLibrary.User.PerformanceAssessments.Configuration.MeasurementDeviceType(_idMeasurementDeviceType).MeasurementDeviceAdd(txtReference.Text, txtSerialNumber.Text, txtBrand.Text, txtModel.Text, txtCalibrationPeriodicity.Text, txtMaintenance.Text, _installationDate, _measurementUnits, _resourceCatalog, _site, _upperLimit, _lowerLimit, _uncertainty);
                    }
                    else
                    {
                        EMSLibrary.User.PerformanceAssessments.Configuration.MeasurementDeviceType(_IdMeasurementDeviceType).MeasurementDeviceModify(_IdMeasurementDevice, txtReference.Text, txtSerialNumber.Text, txtBrand.Text, txtModel.Text, txtCalibrationPeriodicity.Text, txtMaintenance.Text, _installationDate, _measurementUnits, _resourceCatalog, _site, _upperLimit, _lowerLimit, _uncertainty);
                        Entity = EMSLibrary.User.PerformanceAssessments.Configuration.MeasurementDeviceType(_IdMeasurementDeviceType).MeasurementDevice(_IdMeasurementDevice);
                    }

                    base.NavigatorAddTransferVar("IdMeasurementDeviceType", Entity.DeviceType.IdMeasurementDeviceType);
                    base.NavigatorAddTransferVar("IdMeasurementDevice", Entity.IdMeasurementDevice);

                    String _pkValues = "IdMeasurementDeviceType=" + Entity.DeviceType.IdMeasurementDeviceType.ToString()
                        + "& IdMeasurementDevice=" + Entity.IdMeasurementDevice.ToString();
                    base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);

                    base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.PA.MeasurementDevice);
                    base.NavigatorAddTransferVar("EntityNameContextInfo", String.Empty);
                    //Navigate("~/MainInfo/ListViewer.aspx", Resources.CommonListManage.MeasurementDevice + " " + Entity.FullName, Condesus.WebUI.Navigation.DeleteType.DeleteModifiedEntity);
                    String _entityPropertyName = String.Concat(Entity.FullName);
                    NavigatePropertyEntity(Common.ConstantsEntitiesName.PA.MeasurementDevice, _entityPropertyName, Condesus.WebUI.Navigation.NavigateMenuAction.Add);

                    base.StatusBar.ShowMessage(Resources.Common.SaveOK);
                }
                catch (Exception ex)
                {
                    base.StatusBar.ShowMessage(ex);
                }
            }
            
            //protected void btnOkDelete_Click(object sender, EventArgs e)
            //{
            //    if (Entity != null)
            //    {
            //        try
            //        {
            //            //Borra el Indicator
            //            EMSLibrary.User.PerformanceAssessments.Configuration.MeasurementDeviceType(_IdMeasurementDeviceType).MeasurementDeviceRemove(_IdMeasurementDevice);

            //            //Navega a un Add Borrando la pagina actual
            //            base.Navigate(HttpContext.Current.Request.Url.AbsolutePath, GetLocalResourceObject("PageDescription").ToString() + " " + Resources.Common.Add, Condesus.WebUI.Navigation.DeleteType.DeleteRemovedEntity);
            //            base.StatusBar.ShowMessage(Resources.Common.DeleteOK);
            //        }
            //        catch (Exception ex)
            //        {
            //            base.StatusBar.ShowMessage(ex);
            //        }
            //    }
            //    this.mpelbDeleteIndicator.Hide();
            //}
        #endregion
    }
}

