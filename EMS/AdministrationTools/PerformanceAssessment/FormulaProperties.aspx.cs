using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using Telerik.Web.UI;
using EMS_PA = Condesus.EMS.Business.PA;
using Condesus.EMS.Business.PA.Entities;
using Condesus.EMS.WebUI.MasterControls;

namespace Condesus.EMS.WebUI.PA
{
    public partial class FormulaProperties : BaseProperties
    {
        #region Internal Properties
            private RadComboBox _RdcResource;
            private RadTreeView _RtvResource;
            public String _msgSelectIndicatorAndMeasurementUnit = String.Empty;
            private CompareValidator _CvMeasurementUnit;
            private RadComboBox _RdcMeasurementUnit;
            private CompareValidator _CvIndicator;
            private RadComboBox _RdcIndicator;
            private RadTreeView _RtvIndicator;
            private Formula _Entity = null;
            private Formula Entity
            {
                get
                {
                    try
                    {
                        if (_Entity == null)
                            _Entity = EMSLibrary.User.PerformanceAssessments.Configuration.Formula(_IdFormula);

                        return _Entity;
                    }
                    catch { return null; }
                }

                set { _Entity = value; }
            }
            private Int64 _IdFormula
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdFormula") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdFormula")) : 0;
                }
            }
            private RadGrid _RadMasterGrid;
            private RadComboBox _DdlIndicatorClassification;
            private RadTreeView _RtvIndicatorClassification;
            public long _IdIndicatorClassification
            {
                get { return Convert.ToInt64(ViewState["IdIndicatorClassification"]); }
                set { ViewState["IdIndicatorClassification"] = value.ToString(); }
            }
            private const String _PARAMS_GRID_NAME = "radParamsGrid";
        #endregion

        #region PageLoad & Init
            protected override void InitializeHandlers()
            {
                base.InitializeHandlers();
                //Le paso el Save a la MasterContentToolbar
                EventHandler _saveEventHandler = new EventHandler(btnSave_Click);
                FwMasterPage.ContentNavigatorToolbarFileActionAdd(_saveEventHandler, MasterFwContentToolbarAction.Save, true);

                ddlSPSchema.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(ddlSPSchema_SelectedIndexChanged);
                aspDdlPopUpIndicatorClassification.SelectedIndexChanged += new EventHandler(aspDdlPopUpIndicatorClassification_SelectedIndexChanged);
                aspDdlPopUpIndicator.SelectedIndexChanged += new EventHandler(aspDdlPopUpIndicator_SelectedIndexChanged);
            }
            protected override void OnInit(EventArgs e)
            {
                base.OnInit(e);
                LoadTextLabels();
                base.InjectCheckIndexesTags();
            }
            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);
                AddComboIndicators();
                AddComboMeasurementUnits();
                AddComboResourceVersionable();

                if (!Page.IsPostBack)
                {
                    //Los Combos
                    InitSPSchemaCombo();
                    InitPopUps();

                    if (Entity == null)
                        { Add(); }
                    else
                        { LoadData(); }

                    //Form
                    base.SetContentTableRowsCss(tblContentForm);
                    base.SetContentTableRowsCss(tblContentFormPopup);

                    this.txtName.Focus();
                }

                //Tiene que inyectar la Grilla de Params de la Formula
                BuildFormulaParamsGrid();
            }
            protected override void SetPagetitle()
            {
                try
                {
                    base.PageTitle = (Entity != null) ? Entity.LanguageOption.Name : Resources.CommonListManage.Formula;
                }
                catch { base.PageTitle = String.Empty; }
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }
        #endregion

        #region Private Methods
            private void AddComboResourceVersionable()
            {
                String _filterExpression = String.Empty;
                //Combo de ResourceCatalog
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                AddComboWithTreeElementMaps(ref phResourceCatalog, ref _RdcResource, ref _RtvResource,
                    Common.ConstantsEntitiesName.KC.ResourceClassifications, Common.ConstantsEntitiesName.KC.ResourceVersions, _params, false, true, false, ref _filterExpression,
                    new RadTreeViewEventHandler(rtvResource_NodeExpand),
                    Resources.Common.ComboBoxNoDependency, false);

            }
            private void SetResourceVersionable()
            {
                //Seteamos la resourceCatalog...
                //Realiza el seteo del parent en el Combo-Tree.
                if (Entity.ResourceVersion!=null)
                {
                    Condesus.EMS.Business.KC.Entities.Resource _resource = EMSLibrary.User.KnowledgeCollaboration.Map.Resource(Entity.ResourceVersion.IdResource);
                    String _keyValuesElement = "IdResource=" + _resource.IdResource.ToString();
                    if (_resource.Classifications.Count > 0)
                    {
                        String _keyValuesClassification = "IdResourceClassification=" + _resource.Classifications.First().Value.IdResourceClassification;
                        SelectItemTreeViewParentElementMaps(_keyValuesClassification, _keyValuesElement, ref _RtvResource, ref _RdcResource, Common.ConstantsEntitiesName.KC.ResourceClassification, Common.ConstantsEntitiesName.KC.ResourceClassificationChildren, Common.ConstantsEntitiesName.KC.ResourceVersions);
                        //SelectItemTreeViewParentElementMaps(_keyValuesClassification, _keyValuesElement, ref _RtvResourceCatalog, ref _RdcResourceCatalog, Common.ConstantsEntitiesName.KC.ResourceClassification, Common.ConstantsEntitiesName.KC.ResourceClassificationChildren, Common.ConstantsEntitiesName.KC.Resources);
                    }
                    else
                    {
                        SelectItemTreeViewParent(_keyValuesElement, ref _RtvResource, ref _RdcResource, Common.ConstantsEntitiesName.KC.Resource, Common.ConstantsEntitiesName.KC.ResourceVersions);
                    }
                }
            }
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonListManage.Formula;
                _msgSelectIndicatorAndMeasurementUnit = Resources.ConstantMessage.msgSelectIndicatorAndMeasurementUnit;
                lblClassification.Text = Resources.CommonListManage.Indicator;
                lblDescription.Text = Resources.CommonListManage.Description;
                lblIDFormula.Text = Resources.CommonListManage.IdFormula;
                lblLiteralFormula.Text = Resources.CommonListManage.LiteralFormula;
                lblMagnitud.Text = Resources.CommonListManage.Magnitud;
                lblMeasurementUnit.Text = Resources.CommonListManage.MeasurementUnit;
                lblPopUpIndicator.Text = Resources.CommonListManage.Indicator;
                lblPopUpIndicatorClassification.Text = Resources.CommonListManage.IndicatorClassification;
                lblPopUpMeasurementUnit.Text = Resources.CommonListManage.MeasurementUnit;
                lblSPSchema.Text = Resources.CommonListManage.SPSchema;
                lblTitleFormulaParams.Text = Resources.CommonListManage.Parameters;
                rfv1.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
                rfv2.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
            }
            private void AddComboIndicators()
            {
                String _filterExpression = String.Empty;
                //Combo de Indicator
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                AddComboWithTreeElementMaps(ref phIndicator, ref _RdcIndicator, ref _RtvIndicator,
                    Common.ConstantsEntitiesName.PA.IndicatorClassifications, Common.ConstantsEntitiesName.PA.IndicatorsRoots, _params, false, true, false, ref _filterExpression,
                    new RadTreeViewEventHandler(rtvIndicators_NodeExpand),
                    new RadTreeViewEventHandler(rtvIndicators_NodeClick),
                    Resources.Common.ComboBoxNoDependency, false);

                //ValidatorRequiredField(Common.ConstantsEntitiesName.PA.Indicators, phIndicatorValidator, ref _CvIndicator, _RdcIndicator, Resources.ConstantMessage.SelectAIndicator);
            }
            private void AddComboMeasurementUnits()
            {
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                if (_RtvIndicator.SelectedNode != null)
                {
                    if (GetKeyValue(_RtvIndicator.SelectedNode.Value, "IdIndicator") != null)
                    {
                        Int64 _idIndicator = Convert.ToInt64(GetKeyValue(_RtvIndicator.SelectedNode.Value, "IdIndicator"));
                        _params.Add("IdMagnitud", EMSLibrary.User.PerformanceAssessments.Map.Indicator(_idIndicator).Magnitud.IdMagnitud);
                    }
                }
                AddCombo(phMeasurementUnit, ref _RdcMeasurementUnit, Common.ConstantsEntitiesName.PA.MeasurementUnits, String.Empty, _params, false, true, false, false, false);
            }
            private void SetIndicator()
            {
                //Seteamos la organizacion...
                //Realiza el seteo del parent en el Combo-Tree.
                Indicator _indicator = EMSLibrary.User.PerformanceAssessments.Map.Indicator(Entity.Indicator.IdIndicator);
                String _keyValuesElement = "IdIndicator=" + _indicator.IdIndicator.ToString();
                if (_indicator.Classifications.Count > 0)
                {
                    String _keyValuesClassification = "IdIndicatorClassification=" + _indicator.Classifications.First().Value.IdIndicatorClassification;
                    SelectItemTreeViewParentElementMaps(_keyValuesClassification, _keyValuesElement, ref _RtvIndicator, ref _RdcIndicator, Common.ConstantsEntitiesName.PA.IndicatorClassification, Common.ConstantsEntitiesName.PA.IndicatorClassificationChildren, Common.ConstantsEntitiesName.PA.Indicators);
                }
                else
                {
                    SelectItemTreeViewParent(_keyValuesElement, ref _RtvIndicator, ref _RdcIndicator, Common.ConstantsEntitiesName.PA.Indicator, Common.ConstantsEntitiesName.PA.Indicators);
                }
            }
            private void SetMeasurementUnit()
            {
                _RdcMeasurementUnit.SelectedValue = "IdMeasurementUnit=" + Entity.MeasurementUnit.IdMeasurementUnit.ToString() + "& IdMagnitud=" + Entity.Indicator.Magnitud.IdMagnitud.ToString();
            }
            private void Add()
            {
                //Activo los textbox
                txtName.ReadOnly = false;
                txtDescription.ReadOnly = false;
                txtLiteralFormula.ReadOnly = false;
                lblMagnitudValue.Text = String.Empty;

                //limpio los textbox por si hay datos
                lblIdFormulaValue.Text = String.Empty;
                txtDescription.Text = String.Empty;
                txtName.Text = String.Empty;
                txtLiteralFormula.Text = String.Empty;
            }
            private void LoadData()
            {
                //Inputs
                lblIdFormulaValue.Text = Entity.IdFormula.ToString();
                txtDescription.Text = Entity.LanguageOption.Desciption;
                txtName.Text = Entity.LanguageOption.Name;
                txtLiteralFormula.Text = Entity.LiteralFormula;

                SetIndicator();
                _RdcIndicator.Enabled = false;
                //Despues de setear el indicador, carga los 2 combos que dependen de indicator.
                AddComboMeasurementUnits();
                SetMeasurementUnit();
                _RdcMeasurementUnit.Enabled = false;

                SetResourceVersionable();

                lblMagnitudValue.Text = Entity.Indicator.Magnitud.LanguageOption.Name;

                ddlSPSchema.FindItemByValue(Entity.SchemaSP.Name).Selected = true;
                ddlSPSchema.Enabled = false;
            }
        #endregion

        #region Page Events
            //Evento para el Expand del Combo con Tree pero ElementMaps
            protected void rtvIndicators_NodeExpand(object sender, RadTreeNodeEventArgs e)
            {
                //Limpio los hijos, para no duplicar al abrir y cerrar.
                e.Node.Nodes.Clear();
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                _params = GetKeyValues(e.Node.Value);

                //Primero lo hace sobre las Clasificaciones Hijas...
                //Actualiza el Data table con los hijos, para poder asociarlas al nodo expadido.
                BuildGenericDataTable(Common.ConstantsEntitiesName.PA.IndicatorClassificationChildren, _params);
                if (DataTableListManage.ContainsKey(Common.ConstantsEntitiesName.PA.IndicatorClassificationChildren))
                {
                    foreach (DataRow _drRecord in DataTableListManage[Common.ConstantsEntitiesName.PA.IndicatorClassificationChildren].Rows)
                    {
                        RadTreeNode _node = SetElementMapsNodeTreeView(_drRecord, Common.ConstantsEntitiesName.PA.IndicatorClassificationChildren, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, String.Empty, String.Empty, String.Empty);
                        e.Node.Nodes.Add(_node);
                        //Como es un Mapa de Elementos, indica true para que cargue los elementos debajo de la classificacion.
                        SetExpandMode(_node, Common.ConstantsEntitiesName.PA.IndicatorClassificationChildren, true, false);
                    }
                }

                //Ahora lo hace sobre los Elementos Hijos de la Clasificacion expandida.
                BuildGenericDataTable(Common.ConstantsEntitiesName.PA.Indicators, _params);
                if (DataTableListManage.ContainsKey(Common.ConstantsEntitiesName.PA.Indicators))
                {
                    foreach (DataRow _drRecord in DataTableListManage[Common.ConstantsEntitiesName.PA.Indicators].Rows)
                    {
                        RadTreeNode _node = SetElementMapsNodeTreeView(_drRecord, Common.ConstantsEntitiesName.PA.Indicators, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, String.Empty, String.Empty, String.Empty);
                        e.Node.Nodes.Add(_node);
                    }
                }
            }
            protected void rtvIndicators_NodeClick(object sender, RadTreeNodeEventArgs e)
            {
                if (_RtvIndicator.SelectedNode != null)
                {
                    if (GetKeyValue(_RtvIndicator.SelectedNode.Value, "IdIndicator") != null)
                    {
                        Int64 _idIndicator = Convert.ToInt64(GetKeyValue(_RtvIndicator.SelectedNode.Value, "IdIndicator"));
                        //Muestra el nombre de la magnitud del indicador...
                        lblMagnitudValue.Text = EMSLibrary.User.PerformanceAssessments.Map.Indicator(_idIndicator).Magnitud.LanguageOption.Name;
                    }
                }
                //Carga...Measurement Unit..
                AddComboMeasurementUnits();
            }
        #endregion

        #region Combos
            protected void LoadSPShema(ref RadComboBox rcbCombo)
            {
                RadComboBoxItem _li = new RadComboBoxItem(Resources.CommonListManage.SPSCHEMA_SelectUnit, "-1");
                rcbCombo.Items.Add(_li);

                foreach (Condesus.EMS.Business.PA.Entities.FormulaStoredProcedure _spFormula in EMSLibrary.User.PerformanceAssessments.Configuration.FormulaStoredProcedures())
                {
                    _li = new RadComboBoxItem(_spFormula.Name, _spFormula.Name);
                    rcbCombo.Items.Add(_li);
                }

                rcbCombo.SelectedValue = "-1";
            }

        private void InitSPSchemaCombo()
        {
            LoadSPShema(ref ddlSPSchema);
        }
        //private void InitMeasurementUnitCombo()
        //{
        //    ddlMeasurementUnit.Enabled = false;
        //}
        //private void InitIndicatorCombo()
        //{
        //    ddlIndicator.Enabled = false;
        //}

        //private void InitIndicatorClassificationCombo()
        //{
        //    base.LoadTreeView(ref _RtvIndicatorClassification, -1, "IndicatorClassification", false);
        //    RadTreeNode _node = _RtvIndicatorClassification.FindNodeByValue(_IdIndicatorClassification.ToString());
        //    _node.Selected = true;

        //    _DdlIndicatorClassification.Items[0].Value = _node.Value;
        //    _DdlIndicatorClassification.Items[0].Text = _node.Text;
        //    _DdlIndicatorClassification.Items[0].Selected = true;
        //}

        //private void InitIndicatorClassificationCombo2()
        //{
        //    ddlIndicatorClassification.Items.Clear();
        //    RadComboBoxItem _li = new RadComboBoxItem(Resources.ArchivoTemp.IndicatorsManage_SelectClassification, "-1");
        //    ddlIndicatorClassification.Items.Add(_li);

        //    //Meto los Indicadores del Clasification Seleccionado en la Formula
        //    foreach (Condesus.EMS.Business.PA.Entities.IndicatorClassification _classification in EMSLibrary.User.PerformanceAssessments.Map.IndicatorClassifications().Values)
        //    {
        //        _li = new RadComboBoxItem(_classification.LanguageOption.Name, _classification.IdIndicatorClassification.ToString());
        //        ddlIndicatorClassification.Items.Add(_li);

        //        if (_classification.ChildrenClassifications.Count > 0)
        //            AddIndicatorClassificationComboClassChilds(_classification, "");
        //    }
        //}

        //private void AddIndicatorClassificationComboClassChilds(Condesus.EMS.Business.PA.Entities.IndicatorClassification _child, String nivel)
        //{
        //    nivel += "   ";

        //    foreach (Condesus.EMS.Business.PA.Entities.IndicatorClassification _childClassif in _child.ChildrenClassifications.Values)
        //    {
        //        RadComboBoxItem _li = new RadComboBoxItem(nivel + _childClassif.LanguageOption.Name, _childClassif.IdIndicatorClassification.ToString());
        //        ddlIndicatorClassification.Items.Add(_li);

        //        if (_childClassif.ChildrenClassifications.Count > 0)
        //            AddIndicatorClassificationComboClassChilds(_childClassif, nivel);
        //    }
        //}

        //CombosPopUp
        private void InitPopUps()
        {
            //Indicator Classification
            aspDdlPopUpIndicatorClassification.Items.Clear();
            ListItem _li = new ListItem(Resources.CommonListManage.comboboxselectitemindicatorclassifications, "-1");
            aspDdlPopUpIndicatorClassification.Items.Add(_li);

            //Meto los Indicadores del Clasification Seleccionado en la Formula
            foreach (Condesus.EMS.Business.PA.Entities.IndicatorClassification _classification in EMSLibrary.User.PerformanceAssessments.Map.IndicatorClassifications().Values)
            {
                _li = new ListItem(Common.Functions.RemoveIndexesTags(_classification.LanguageOption.Name), _classification.IdIndicatorClassification.ToString());
                aspDdlPopUpIndicatorClassification.Items.Add(_li);

                if (_classification.ChildrenClassifications.Count > 0)
                    AddClassChilds(_classification, "");
            }

            //Indicador
            ResetComboIndicators();

            //Measurement Unit
            ResetComboMeasurementUnits();

        }

        private void AddClassChilds(Condesus.EMS.Business.PA.Entities.IndicatorClassification _child, String nivel)
        {
            nivel += "   ";

            foreach (Condesus.EMS.Business.PA.Entities.IndicatorClassification _childClassif in _child.ChildrenClassifications.Values)
            {
                ListItem _li = new ListItem(nivel + Common.Functions.RemoveIndexesTags(_childClassif.LanguageOption.Name), _childClassif.IdIndicatorClassification.ToString());
                aspDdlPopUpIndicatorClassification.Items.Add(_li);

                if (_childClassif.ChildrenClassifications.Count > 0)
                    AddClassChilds(_childClassif, nivel);
            }
        }

        void aspDdlPopUpIndicatorClassification_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResetComboIndicators();
            ResetComboMeasurementUnits();

            Int64 _idIndicatorClasif = Convert.ToInt64(aspDdlPopUpIndicatorClassification.SelectedValue);

            if (_idIndicatorClasif > 0)
            {
                Dictionary<Int64, Condesus.EMS.Business.PA.Entities.Indicator> _indicators = EMSLibrary.User.PerformanceAssessments.Map.IndicatorClassification(_idIndicatorClasif).ChildrenElements;
                foreach (Condesus.EMS.Business.PA.Entities.Indicator _indicator in _indicators.Values)
                {
                    ListItem _li = new ListItem(Common.Functions.RemoveIndexesTags(_indicator.LanguageOption.Name), _indicator.IdIndicator.ToString());
                    aspDdlPopUpIndicator.Items.Add(_li);
                }
            }

            aspDdlPopUpIndicator.Enabled = (aspDdlPopUpIndicator.Items.Count > 1) ? true : false;
        }

        void aspDdlPopUpIndicator_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResetComboMeasurementUnits();

            if (aspDdlPopUpIndicator.SelectedValue != "-1")
            {
                EMS_PA.Entities.Indicator _indicator = EMSLibrary.User.PerformanceAssessments.Map.Indicator(Convert.ToInt64(aspDdlPopUpIndicator.SelectedValue));

                if (_indicator != null)
                {
                    Dictionary<Int64, Condesus.EMS.Business.PA.Entities.MeasurementUnit> _measurementUnits = _indicator.Magnitud.MeasurementUnits;
                    foreach (Condesus.EMS.Business.PA.Entities.MeasurementUnit _measurementUnit in _measurementUnits.Values)
                    {
                        ListItem _li = new ListItem(_measurementUnit.LanguageOption.Name, _measurementUnit.IdMeasurementUnit.ToString());
                        aspDdlPopUpMeasurementUnit.Items.Add(_li);
                    }
                }

                aspDdlPopUpMeasurementUnit.Enabled = (aspDdlPopUpMeasurementUnit.Items.Count > 1) ? true : false;
            }
        }

        private void ResetComboIndicators()
        {
            aspDdlPopUpIndicator.Items.Clear();

            ListItem _li = new ListItem(Resources.CommonListManage.comboboxselectitemindicators, "-1");
            aspDdlPopUpIndicator.Items.Add(_li);

            aspDdlPopUpIndicator.SelectedValue = "-1";
            aspDdlPopUpIndicator.Enabled = false;
        }

        private void ResetComboMeasurementUnits()
        {
            aspDdlPopUpMeasurementUnit.Items.Clear();

            ListItem _li = new ListItem(Resources.CommonListManage.comboboxselectitemmeasurementunits, "-1");
            aspDdlPopUpMeasurementUnit.Items.Add(_li);

            aspDdlPopUpMeasurementUnit.SelectedValue = "-1";
            aspDdlPopUpMeasurementUnit.Enabled = false;
        }
        #endregion

        #region RadGrid
            private void BuildFormulaParamsGrid()
            {
                pnlFormulaParams.Controls.Clear();
                RadGrid _paramGrid = BuildParameterGrid();
                pnlFormulaParams.Controls.Add(_paramGrid);
            }
            private DataTable LoadParamGrid()
            {
                //RadGrid _paramGrid = (RadGrid)up_MainData.ContentTemplateContainer.FindControl("pnlFormulaParams").FindControl(_PARAMS_GRID_NAME);
                DataTable _dt = GetParamGridDataTable();
                //EMS_PA.Entities.Formula _formula = EMSLibrary.User.PerformanceAssessments.Configuration.Formula(_IdFormula);
                Formula _formula = Entity;

                if (_formula != null && _formula.FormulaParameters.Count > 0)
                {
                    foreach (EMS_PA.Entities.FormulaParameter _formulaParam in _formula.FormulaParameters.Values)
                        _dt.Rows.Add(Common.Functions.ReplaceIndexesTags(_formulaParam.ParameterName), _formulaParam.PositionParameter, _formulaParam.Indicator.IdIndicator, _formulaParam.MeasurementUnit.IdMeasurementUnit);
                }
                else
                {
                    //Me da los Paramteros del Esquema Seleccionado
                    var _spScheme = from s in EMSLibrary.User.PerformanceAssessments.Configuration.FormulaStoredProcedures()
                                    where s.Name == ddlSPSchema.SelectedValue
                                    select s.StoredProcedureParameters;

                    if (_spScheme.Count() > 0)
                    {
                        foreach (var _iListSFPP in _spScheme)
                            foreach (EMS_PA.Entities.FormulaStoredProcedureParameter _spParam in _iListSFPP)
                            {
                                if ((_spParam.Name != "IdOrganization") && (_spParam.Name != "FilterStartDate") && (_spParam.Name != "FilterEndDate") && (_spParam.Name != "ResultOut"))
                                {
                                    //if (ddlIndicator.SelectedValue != "-1" && ddlMagnitud.SelectedValue != "-1" && ddlMeasurementUnit.SelectedValue != "-1")
                                    //    _dt.Rows.Add(Common.Functions.ReplaceIndexesTags(_spParam.Name), _spParam.Order, ddlIndicator.SelectedValue, ddlMeasurementUnit.SelectedValue);
                                    //else
                                    //Ahora Siempre me la Limpia
                                    _dt.Rows.Add(Common.Functions.ReplaceIndexesTags(_spParam.Name), _spParam.Order, "0", "0");
                                }
                            }
                    }
                }

                return _dt;
            }
            private DataTable GetParamGridDataTable()
            {
                DataTable _retDt = new DataTable("Root");
                _retDt.Columns.Add("paramName");
                _retDt.Columns.Add("paramPosition");
                _retDt.Columns.Add("idIndicator");
                _retDt.Columns.Add("idMeasurementUnit");

                return _retDt;
            }
            private RadGrid BuildParameterGrid()
            {
                _RadMasterGrid = new RadGrid();
                _RadMasterGrid.ID = _PARAMS_GRID_NAME;

                #region Propiedades y Handlers
                _RadMasterGrid.Skin = "EMS";
                _RadMasterGrid.EnableEmbeddedSkins = false;
                _RadMasterGrid.AllowPaging = false;
                _RadMasterGrid.AllowSorting = false;
                _RadMasterGrid.Width = Unit.Percentage(100);
                _RadMasterGrid.AutoGenerateColumns = false;
                _RadMasterGrid.GridLines = System.Web.UI.WebControls.GridLines.None;
                _RadMasterGrid.ShowStatusBar = false;
                _RadMasterGrid.PageSize = 50;
                _RadMasterGrid.AllowMultiRowSelection = false;
                _RadMasterGrid.PagerStyle.AlwaysVisible = false;
                _RadMasterGrid.PagerStyle.Mode = GridPagerMode.NextPrevAndNumeric;
                _RadMasterGrid.MasterTableView.Width = Unit.Percentage(99);
                _RadMasterGrid.EnableViewState = false;

                //Crea los metodos de la grilla (Server)
                _RadMasterGrid.NeedDataSource += new GridNeedDataSourceEventHandler(_RadMasterGrid_NeedDataSource);
                _RadMasterGrid.ItemDataBound += new GridItemEventHandler(_RadMasterGrid_ItemDataBound);
                _RadMasterGrid.SortCommand += new GridSortCommandEventHandler(_RadMasterGrid_SortCommand);
                _RadMasterGrid.PageIndexChanged += new GridPageChangedEventHandler(_RadMasterGrid_PageIndexChanged);
                _RadMasterGrid.ItemDataBound += new GridItemEventHandler(_RadMasterGrid_ItemDataBound);

                //Crea los metodos de la grilla (Cliente)
                _RadMasterGrid.ClientSettings.AllowExpandCollapse = false;
                //_RadMasterGrid.ClientSettings.EnableClientKeyValues = true;
                _RadMasterGrid.AllowMultiRowSelection = false;
                _RadMasterGrid.ClientSettings.Selecting.AllowRowSelect = true;
                _RadMasterGrid.ClientSettings.Selecting.EnableDragToSelectRows = false;
                _RadMasterGrid.MasterTableView.HierarchyLoadMode = GridChildLoadMode.Client;

                //Define los atributos de la MasterGrid
                _RadMasterGrid.MasterTableView.Name = "gridMaster";
                _RadMasterGrid.MasterTableView.DataKeyNames = new string[] { "paramName" };
                _RadMasterGrid.MasterTableView.EnableViewState = false;
                _RadMasterGrid.MasterTableView.CellPadding = 0;
                _RadMasterGrid.MasterTableView.CellSpacing = 0;
                _RadMasterGrid.MasterTableView.GridLines = System.Web.UI.WebControls.GridLines.None;
                _RadMasterGrid.MasterTableView.GroupsDefaultExpanded = false;
                _RadMasterGrid.MasterTableView.AllowMultiColumnSorting = false;
                _RadMasterGrid.MasterTableView.HierarchyLoadMode = GridChildLoadMode.Client;
                _RadMasterGrid.MasterTableView.ExpandCollapseColumn.Resizable = false;
                _RadMasterGrid.MasterTableView.ExpandCollapseColumn.HeaderStyle.Width = Unit.Pixel(20);
                _RadMasterGrid.MasterTableView.RowIndicatorColumn.Visible = false;
                _RadMasterGrid.MasterTableView.RowIndicatorColumn.HeaderStyle.Width = Unit.Pixel(20);
                _RadMasterGrid.MasterTableView.EditMode = GridEditMode.InPlace;

                _RadMasterGrid.HeaderStyle.Font.Bold = false;
                _RadMasterGrid.HeaderStyle.Font.Italic = false;
                _RadMasterGrid.HeaderStyle.Font.Overline = false;
                _RadMasterGrid.HeaderStyle.Font.Strikeout = false;
                _RadMasterGrid.HeaderStyle.Font.Underline = false;
                _RadMasterGrid.HeaderStyle.Wrap = true;
                #endregion

                //Crea las columnas para la MasterGrid.
                DefineColumnsGrid(_RadMasterGrid.MasterTableView);

                return _RadMasterGrid;
            }
            private void DefineColumnsGrid(GridTableView gridTableViewDetails)
            {
                //Add columns bound
                GridBoundColumn boundColumn;

                //Columnas que no se ven...
                //Crea y agrega las columnas de tipo Bound
                boundColumn = new GridBoundColumn();
                boundColumn.DataField = "paramName";
                boundColumn.HeaderText = "Name";
                boundColumn.Display = true;
                gridTableViewDetails.Columns.Add(boundColumn);

                boundColumn = new GridBoundColumn();
                boundColumn.DataField = "paramPosition";
                boundColumn.HeaderText = "Position";
                boundColumn.Display = true;
                gridTableViewDetails.Columns.Add(boundColumn);


                boundColumn = new GridBoundColumn();
                boundColumn.DataField = "idIndicator";
                boundColumn.HeaderText = "Indicator";
                boundColumn.Display = false;
                gridTableViewDetails.Columns.Add(boundColumn);

                boundColumn = new GridBoundColumn();
                boundColumn.DataField = "idMeasurementUnit";
                boundColumn.HeaderText = "Measurement";
                boundColumn.Display = false;
                gridTableViewDetails.Columns.Add(boundColumn);


                GridTemplateColumn template;
                //EMS_PA.Entities.Formula _formula = EMSLibrary.User.PerformanceAssessments.Configuration.Formula(_IdFormula);
                Formula _formula = Entity;
                Boolean _canEdit = true;
                if (_formula != null)
                    _canEdit = false;

                template = new GridTemplateColumn();
                template.UniqueName = "cboIndicatorColumn";
                template.ItemTemplate = new PopUpTemplate("Indicator", _canEdit);
                template.HeaderText = "Indicator";
                template.AllowFiltering = false;
                gridTableViewDetails.Columns.Add(template);

                template = new GridTemplateColumn();
                template.UniqueName = "cboMeasurementUnitColumn";
                template.ItemTemplate = new PopUpTemplate("MeasurementUnit", _canEdit);
                template.HeaderText = "Measurement Unit";
                template.AllowFiltering = false;
                gridTableViewDetails.Columns.Add(template);

            }

            #region GridEvents
                void _RadMasterGrid_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
                {
                    RadGrid handler = (RadGrid)source;
                    handler.DataSource = LoadParamGrid();
                }
                void _RadMasterGrid_ItemDataBound(object sender, GridItemEventArgs e)
                {
                    if (e.Item is GridDataItem)
                    {
                        //PopUpIndicator
                        HiddenField _lblIndicatorId = (HiddenField)e.Item.FindControl("Indicator_idValue");
                        Label _lblIndicatorValue = (Label)e.Item.FindControl("Indicator_txtValue");

                        Int64 _idIndicator = Convert.ToInt64(((GridDataItem)e.Item)["idIndicator"].Text);
                        EMS_PA.Entities.Indicator _indicator = EMSLibrary.User.PerformanceAssessments.Map.Indicator(_idIndicator);
                        if (_indicator != null)
                        {
                            _lblIndicatorId.Value = _indicator.IdIndicator.ToString();
                            _lblIndicatorValue.Text = Common.Functions.ReplaceIndexesTags(_indicator.LanguageOption.Name);
                        }

                        //PopUpMeasurement
                        HiddenField _lblMeasurementId = (HiddenField)e.Item.FindControl("MeasurementUnit_idValue");
                        Label _lblMeasurementValue = (Label)e.Item.FindControl("MeasurementUnit_txtValue");

                        Int64 _idMeasurementUnit = Convert.ToInt64(((GridDataItem)e.Item)["idMeasurementUnit"].Text);
                        EMS_PA.Entities.MeasurementUnit _measurementUnit = null;
                        if (_indicator != null)
                            _measurementUnit = EMSLibrary.User.PerformanceAssessments.Configuration.Magnitud(_indicator.Magnitud.IdMagnitud).MeasurementUnit(_idMeasurementUnit);

                        if (_measurementUnit != null)
                        {
                            _lblMeasurementId.Value = _measurementUnit.IdMeasurementUnit.ToString();
                            _lblMeasurementValue.Text = Common.Functions.ReplaceIndexesTags(_measurementUnit.LanguageOption.Name);
                        }
                    }
                }
                void _RadMasterGrid_PageIndexChanged(object source, GridPageChangedEventArgs e)
                {
                    RadGrid handler = (RadGrid)source;
                    handler.DataSource = LoadParamGrid();
                    handler.MasterTableView.Rebind();
                }
                void _RadMasterGrid_SortCommand(object source, GridSortCommandEventArgs e)
                {
                    RadGrid handler = (RadGrid)source;
                    handler.DataSource = LoadParamGrid();
                    handler.MasterTableView.Rebind();
                }
            #endregion
        #endregion

        #region Eventos
        //private void Navigator()
        //{
        //    String[,] _param = new String[,] { { "ItemOptionSelected", _ItemOptionSelected }, { "IdFormula", _IdFormula.ToString() } };

        //    base.SetNavigator(_param);
        //}

        //void ddlIndicatorClassification_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        //{
        //    //base.LoadIndicators(Convert.ToInt64(_RtvIndicatorClassification.SelectedNode.Value), ref ddlIndicator);
        //    base.LoadIndicators(Convert.ToInt64(ddlIndicatorClassification.SelectedValue), ref ddlIndicator);

        //    ddlIndicator.Enabled = (ddlIndicator.Items.Count > 1) ? true : false;
        //}
        //void ddlIndicator_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        //{
        //    Condesus.EMS.Business.PA.Entities.Magnitud _magnitud = EMSLibrary.User.PerformanceAssessments.Map.Indicator(Convert.ToInt64(ddlIndicator.SelectedValue)).Magnitud;

        //    lblMagnitudValue.Text = _magnitud.LanguageOption.Name;

        //    Int64 _idMagnitud = _magnitud.IdMagnitud;
        //    base.LoadMeasurementUnit(_idMagnitud, ref ddlMeasurementUnit);
        //    ddlMeasurementUnit.Enabled = true;
        //}

        ////void ddlMagnitud_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        ////{
        ////    Int64 _idMagnitud = EMSLibrary.User.PerformanceAssessments.Map.Indicator(Convert.ToInt64(ddlIndicator.SelectedValue)).Magnitud.IdMagnitud;
        ////    base.LoadMeasurementUnit(_idMagnitud, ref ddlMeasurementUnit);

        ////    ddlMeasurementUnit.Enabled = (ddlMeasurementUnit.Items.Count > 1) ? true : false;
        ////}
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
                        SetExpandMode(_node, Common.ConstantsEntitiesName.KC.ResourceClassificationChildren, true, false);
                    }
                }

                //Ahora lo hace sobre los Elementos Hijos de la Clasificacion expandida.
                BuildGenericDataTable(Common.ConstantsEntitiesName.KC.ResourceVersions, _params);
                if (DataTableListManage.ContainsKey(Common.ConstantsEntitiesName.KC.ResourceVersions))
                {
                    foreach (DataRow _drRecord in DataTableListManage[Common.ConstantsEntitiesName.KC.ResourceVersions].Rows)
                    {
                        RadTreeNode _node = SetElementMapsNodeTreeView(_drRecord, Common.ConstantsEntitiesName.KC.ResourceVersions, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, String.Empty, String.Empty, String.Empty);
                        e.Node.Nodes.Add(_node);
                    }
                }
            }

        void ddlSPSchema_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                //lblMessage.Text = String.Empty;
                #region Old Formula
                //if (_IdFormula > 0 && _ItemOptionSelected == "EDIT")
                //{
                //    EMS_PA.Entities.Formula _formula = EMSLibrary.User.PerformanceAssessments.Configuration.Formula(_IdFormula);
                //    if (_formula.Calculations(IdOrganization).Count > 0)
                //    {
                //        //((RadComboBox)o).SelectedItem = ((RadComboBox)o).FindItemByText(e.OldText); 
                //        throw new Exception("You can not change the 'SP Schema' on a formula that has asociated Calculations.");
                //    }
                //    else
                //    {
                //        if (_formula.FormulaParameters.Count > 0)
                //            DeleteFormulaParams(_formula);

                //        BuildFormulaParamsGrid();
                //    }
                //}
                #endregion

                //Esto esta "Validado" en el LoadData, ya que el ddl esta inhabilitado. X Seguridad vuelvo a VALIDAR
                //if (_IdFormula == 0 && _ItemOptionSelected == "ADD")
                if (Entity==null)
                    BuildFormulaParamsGrid();
                else
                    throw new Exception("Security: Action not Allowed");

            }
            catch (Exception ex)
            {
                base.StatusBar.ShowMessage(ex);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //Inicializo valores del Save

                //Indicador
                //Int64 _idIndicator = Convert.ToInt64(ddlIndicator.SelectedValue);
                //EMS_PA.Entities.Indicator _indicator = EMSLibrary.User.PerformanceAssessments.Map.Indicator(_idIndicator);
                //MeasurementUnit
                //Int64 _idMagnitud = Convert.ToInt64(ddlMagnitud.SelectedValue);
                //Condesus.EMS.Business.PA.Entities.Magnitud _magnitud = EMSLibrary.User.PerformanceAssessments.Map.Indicator(Convert.ToInt64(ddlIndicator.SelectedValue)).Magnitud;

                //Int64 _idMeasurementUnit = Convert.ToInt64(ddlMeasurementUnit.SelectedValue);
                //EMS_PA.Entities.MeasurementUnit _measurementUnit = EMSLibrary.User.PerformanceAssessments.Configuration.Magnitud(_idMagnitud).MeasurementUnit(_idMeasurementUnit);

                //Obtiene Indicator
                Indicator _indicator = EMSLibrary.User.PerformanceAssessments.Map.Indicator(Convert.ToInt64(GetKeyValue(_RtvIndicator.SelectedNode.Value, "IdIndicator")));
                //Obtiene Measurement Unit
                MeasurementUnit _measurementUnit = _indicator.Magnitud.MeasurementUnit(Convert.ToInt64(GetKeyValue(_RdcMeasurementUnit.SelectedValue, "IdMeasurementUnit")));
                Int64 _idMagnitud = _indicator.Magnitud.IdMagnitud;
                //Obtiene el Esquema SP
                String _SPScheme = ddlSPSchema.SelectedValue;

                //Validacion de Combos
                if (_SPScheme == "-1" || _indicator == null || _measurementUnit == null)
                {
                    String _exceptionMessage = String.Empty;
                    if (_SPScheme == "-1")
                        _exceptionMessage = "You must select a SP Scheme";
                    if (_indicator == null)
                        _exceptionMessage = "You must select an Indicator";
                    if (_measurementUnit == null)
                        _exceptionMessage = "You must select a Measurement Unit";

                    throw new Exception(_exceptionMessage);
                }

                //Me traigo la grilla y la recorro guardando Parametros
                DataTable _parameters = GetformulaParameters(); // GetParamGridDataTable();

                //Si el key obtenido no llega a exister devuelve null.
                Int64 _idResource = Convert.ToInt64((_RtvResource.SelectedNode == null ? 0 : GetKeyValue(_RtvResource.SelectedNode.Value, "IdResource")));    //Si queda en cero 0, quiere decir que no asocia catalogos.
                Condesus.EMS.Business.KC.Entities.ResourceVersion _resourceVersion = (Condesus.EMS.Business.KC.Entities.ResourceVersion)EMSLibrary.User.KnowledgeCollaboration.Map.Resource(_idResource);

                //Guardo y me traigo la Formula generada para guardar el Id y agregarle Parametros
                if (Entity == null)
                {
                    //Es un ADD
                    Entity = EMSLibrary.User.PerformanceAssessments.Configuration.FormulaAdd(txtLiteralFormula.Text, 
                        _SPScheme, _indicator,
                       _measurementUnit, txtName.Text,
                       txtDescription.Text, _parameters, _resourceVersion);
                }
                else
                {
                    //Guardo lo editado y tambien los cambios a sus Parametros (Solo se puede modificar String literalFormula, String name, String description)
                    Entity.Modify(txtLiteralFormula.Text, _SPScheme, _indicator,
                                     _measurementUnit, txtName.Text, txtDescription.Text,
                                     _parameters, _resourceVersion);
                }
                base.NavigatorAddTransferVar("IdFormula", Entity.IdFormula);

                String _pkValues = "IdFormula=" + Entity.IdFormula.ToString();
                base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);

                base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.PA.Formula);
                //base.NavigatorAddTransferVar("EntityNameContextInfo", String.Empty);
                base.NavigatorAddTransferVar("EntityNameContextInfo", String.Empty);
                base.NavigatorAddTransferVar("EntityNameContextElement", String.Empty);
                //Navigate("~/MainInfo/ListViewer.aspx", Resources.CommonListManage.Formula + " " + Entity.LanguageOption.Name, Condesus.WebUI.Navigation.DeleteType.DeleteModifiedEntity);
                String _entityPropertyName = String.Concat(Entity.LanguageOption.Name);
                NavigatePropertyEntity(Common.ConstantsEntitiesName.PA.Formula, _entityPropertyName, Condesus.WebUI.Navigation.NavigateMenuAction.Add);

                base.StatusBar.ShowMessage(Resources.Common.SaveOK);
            }
            catch (Exception ex)
            {
                String _outMessage = ex.Message;
                //Valido el PK Name (Deberia haber un metodo en Bussines (bool Exists(Name)) que me diga si ya existe este PK
                if (ex.Message.Contains("Cannot insert duplicate key row in object"))
                    _outMessage = "A Formula with this name allready exists. The Name of the Formula must be Unique.";
            
                base.StatusBar.ShowMessage(ex);
            }
            //BuildFormulaParamsGrid();
        }

        private DataTable GetformulaParameters()
        {
            DataTable _parameters = new DataTable();
            _parameters.Columns.Add("ParameterName");
            _parameters.Columns.Add("PositionParameter");
            _parameters.Columns.Add("IdIndicator");
            _parameters.Columns.Add("IdMeasurementUnit");

            //Control _bufParamsGrid = up_MainData.ContentTemplateContainer.FindControl(_PARAMS_GRID_NAME);
            if (_RadMasterGrid!=null)
            //if (_bufParamsGrid != null && _bufParamsGrid is RadGrid)
            {
                #region Old Formula
                //EMS_PA.Entities.Formula _formula = EMSLibrary.User.PerformanceAssessments.Configuration.Formula(_IdFormula);

                ////Si la Formula ya tenia Parametros (estoy editando) al no tener un Modify, limpio todas y las vuelvo a insertar
                //if (_formula.FormulaParameters.Count > 0)
                //    DeleteFormulaParams(_formula);
                #endregion

                //RadGrid _paramsGrid = (RadGrid)_bufParamsGrid;
                RadGrid _paramsGrid = _RadMasterGrid;
                //Recorro los parametros de la grilla y salvo los parametros de la formula
                foreach (GridDataItem _item in _paramsGrid.MasterTableView.Items)
                {
                    //Grid Vars
                    String _paramName = _item["paramName"].Text;
                    Int64 _paramPosition = Convert.ToInt64(_item["paramPosition"].Text);

                    String _paramIndicatorIdValue = ((HiddenField)_item.FindControl("Indicator_idValue")).Value;
                    String _paramMeasurementUnitValue = ((HiddenField)_item.FindControl("MeasurementUnit_idValue")).Value;
                    if (_paramIndicatorIdValue == String.Empty) { _paramIndicatorIdValue = "0"; }
                    if (_paramMeasurementUnitValue == String.Empty) { _paramMeasurementUnitValue = "0"; }
                    Int64 _idParamIndicator = Convert.ToInt64(_paramIndicatorIdValue);
                    Int64 _idParamMeasurementUnit = Convert.ToInt64(_paramMeasurementUnitValue);

                    if (!(_idParamIndicator > 0 && _idParamMeasurementUnit > 0))
                        throw new Exception("All the Parameters must have an Indicator and a Measurement Unit");

                    //Objetos FormulaParams
                    EMS_PA.Entities.Indicator _paramIndicator = EMSLibrary.User.PerformanceAssessments.Map.Indicator(_idParamIndicator);
                    EMS_PA.Entities.MeasurementUnit _paramMeasurementUnit = null;

                    if (_paramIndicator != null)
                        _paramMeasurementUnit = EMSLibrary.User.PerformanceAssessments.Configuration.Magnitud(_paramIndicator.Magnitud.IdMagnitud).MeasurementUnit(_idParamMeasurementUnit);

                    if (_paramIndicator == null || _paramMeasurementUnit == null)
                        throw new Exception("All the Parameters must have an Indicator and a Measurement Unit");

                    _parameters.Rows.Add(_paramName, _paramPosition, _paramIndicator.IdIndicator, _paramMeasurementUnit.IdMeasurementUnit);
                }
            }

            return _parameters;
        }

        private void DeleteFormulaParams(Condesus.EMS.Business.PA.Entities.Formula formula)
        {
            foreach (var _formulaParam in formula.FormulaParameters.Values)
                formula.FormulaParameterRemove(_formulaParam.PositionParameter);
        }

        //void rmnOption_ItemClick(object sender, RadMenuEventArgs e)
        //{
        //    //Para que funcione el truquito del Transfer, necesito que el Menu raisee un evento.
        //    //En el caso de Formula, no teiene add ni delete desde Properties (Emiliano)
        //    //Por lo tanto aca solo me engancho al handler para que haga un Asyncpostback
        //    return;
        //}

        //protected void btnTransferAdd_Click(object sender, EventArgs e)
        //{
        //    Navigator();

        //    Context.Items.Add("IdFormula", _IdFormula);
        //    Server.Transfer("~/PA/FormulasLanguages.aspx");
        //}

        //protected void btnBack_Click(object sender, EventArgs e)
        //{
        //    base.Back();
        //}
        #endregion
    }

    public class PopUpTemplate : ITemplate
    {
        protected HiddenField _hdnValue;
        protected Label _lblText;
        protected LinkButton _btnDoPopUp;
        protected Boolean _canEdit;

        private String _typeOf;

        public PopUpTemplate(String typeOfEntity, Boolean canEdit)
        {
            _typeOf = typeOfEntity;
            _canEdit = canEdit;
        }
        public void InstantiateIn(System.Web.UI.Control container)
        {
            _hdnValue = new HiddenField();
            _hdnValue.ID = _typeOf + "_idValue";

            _lblText = new Label();
            _lblText.ID = _typeOf + "_txtValue";
            _lblText.Attributes["class"] = "contentformspanTitle";
            _lblText.Text = "&nbsp;&nbsp;";
            _lblText.Style["text-align"] = "left";
            _lblText.Width = Unit.Percentage(80);

            _btnDoPopUp = new LinkButton();
            _btnDoPopUp.ID = _typeOf + "_btn";
            _btnDoPopUp.Attributes["name"] = _typeOf + "_EditButton";
            _btnDoPopUp.Attributes["onclick"] = "DoPopUp(this, '" + _typeOf + "', event);";
            _btnDoPopUp.Style["text-align"] = "right";
            _btnDoPopUp.Style["display"] = (_canEdit) ? "block" : "none";
            //_btnDoPopUp.Width = Unit.Percentage(100);

            _btnDoPopUp.CausesValidation = false;
            _btnDoPopUp.Text = "Add";

            container.Controls.Add(_hdnValue);
            container.Controls.Add(_lblText);
            container.Controls.Add(_btnDoPopUp);
        }
    }

}
