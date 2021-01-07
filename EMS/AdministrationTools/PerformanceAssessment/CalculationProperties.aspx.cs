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
using Condesus.EMS.Business.PF.Entities;
using Condesus.EMS.WebUI.MasterControls;

using EMS_PM = Condesus.EMS.Business.PF;

namespace Condesus.EMS.WebUI.ManagementTools.PerformanceAssessment
{
    public partial class CalculationProperties : BaseProperties
    {
        #region Internal Properties
            private CompareValidator _CvFormula;
            private RadComboBox _RdcFormula;
            private CompareValidator _CvMeasurementUnit;
            private RadComboBox _RdcMeasurementUnit;
            private CompareValidator _CvTimeUnitFrequency;
            private RadComboBox _RdcTimeUnitFrequency;
            private Int64 _IdCalculation
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdCalculation") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdCalculation")) : 0;
                }
            }
            private Calculation _Entity = null;
            private Calculation Entity
            {
                get
                {
                    try
                    {
                        if (_Entity == null)
                            _Entity = EMSLibrary.User.PerformanceAssessments.Configuration.Calculation(_IdCalculation);

                        return _Entity;
                    }
                    catch { return null; }
                }

                set { _Entity = value; }
            }
            private String _ItemOptionSelected
            {
                get { return Convert.ToString(ViewState["ItemOptionSelected"]); }
                set { ViewState["ItemOptionSelected"] = value.ToString(); }
            }
            private List<Int64> _SelectedRows
            {
                get
                {
                    object _o = ViewState["SelectedRows"];
                    if (_o != null)
                        return (List<Int64>)_o;

                    throw new Exception("The collection is null");
                }
                set { ViewState["SelectedRows"] = value; }
            }
            private const String _PARAMS_GRID_NAME = "radParamsGrid";
            private String _GRCItem
            {
                get
                {
                    object o = ViewState["GRCItem"];

                    if (o != null)
                        return (String)o;

                    return "";
                }

                set { ViewState["GRCItem"] = value; }
            }
            private String _ChildControl
            {
                get
                {
                    object _o = ViewState["ChildControl"];

                    if (_o != null)
                        return (String)_o;

                    return String.Empty;
                }
                set { ViewState["ChildControl"] = value; }
            }
            private String _ChildControlEntityId
            {
                get
                {
                    object _o = ViewState["ChildControlEntityId"];

                    if (_o != null)
                        return (String)_o;

                    return "0";
                }
                set { ViewState["ChildControlEntityId"] = value; }
            }
            private Boolean? _ChildControlMode
            {
                get
                {
                    object _o = ViewState["ChildControlMode"];

                    if (_o != null)
                        return (Boolean)_o;

                    return null;
                }
                set { ViewState["ChildControlMode"] = value; }
            }
            private String _EntityName;
        #endregion

        #region Page Load & Init
            protected override void InitializeHandlers()
            {
                base.InitializeHandlers();
                //Le paso el Save a la MasterContentToolbar
                EventHandler _saveEventHandler = new EventHandler(btnSave_Click);
                FwMasterPage.ContentNavigatorToolbarFileActionAdd(_saveEventHandler, MasterFwContentToolbarAction.Save, true);

                aspDdlPopUpProcessClassification.SelectedIndexChanged += new EventHandler(aspDdlPopUpProcessClassification_SelectedIndexChanged);
                aspDdlPopUpProject.SelectedIndexChanged += new EventHandler(aspDdlPopUpProject_SelectedIndexChanged);

                rgdMasterGrid.ItemDataBound += new GridItemEventHandler(rgdMasterGrid_ItemDataBound);
                rgdMasterGrid.PageIndexChanged += new GridPageChangedEventHandler(rgdMasterGrid_PageIndexChanged);
                rgdMasterGrid.NeedDataSource += new GridNeedDataSourceEventHandler(rgdMasterGrid_NeedDataSource);
            }
            protected override void OnInit(EventArgs e)
            {
                base.OnInit(e);
                LoadTextLabels();
                base.InjectCheckIndexesTags();

                rgdMasterGrid.ClientSettings.AllowExpandCollapse = false;
                rgdMasterGrid.ClientSettings.Selecting.AllowRowSelect = true;
                rgdMasterGrid.AllowMultiRowSelection = false;
                rgdMasterGrid.ClientSettings.Selecting.EnableDragToSelectRows = false;
                rgdMasterGrid.MasterTableView.HierarchyLoadMode = GridChildLoadMode.Client;
            }
            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);
                AddComboFormulas();
                AddComboMeasurementUnits();
                AddComboTimeUnitFrequency();

                if (!Page.IsPostBack)
                {
                    InitPopUps();

                    //Creo el List donde va a persistir los Proyectos Seleccionados
                    _SelectedRows = new List<Int64>();

                    //if (_Calculation == null)
                    if (Entity == null)
                        { Add(); }
                    else
                        { LoadData(); }

                    //Form
                    base.SetContentTableRowsCss(tblContentForm);
                    base.SetContentTableRowsCss(tblContentForm2);
                    base.SetContentTableRowsCss(tblContentFormPopup);

                    this.txtName.Focus();
                }

                //InitializeMainEntities();
                BuildCalculationParamsGrid();
                //BuildControls();
            }
            protected override void SetPagetitle()
            {
                try
                {
                    base.PageTitle = (Entity != null) ? Entity.LanguageOption.Name : Resources.CommonListManage.Calculation;
                }
                catch { base.PageTitle = String.Empty; }
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }
        #endregion

        #region Formulario

        #region PopUp
            private void InitPopUps()
            {
                //ProcessClassification (Falta Recursividad...)
                aspDdlPopUpProcessClassification.Items.Clear();

                Dictionary<Int64, Condesus.EMS.Business.PF.Entities.ProcessClassification> _processClassification = EMSLibrary.User.ProcessFramework.Map.ProcessClassifications();

                ListItem _li = new ListItem(Resources.CommonListManage.comboboxselectitemprocessclassifications, "-1");
                aspDdlPopUpProcessClassification.Items.Add(_li);

                foreach (Condesus.EMS.Business.PF.Entities.ProcessClassification _pc in _processClassification.Values)
                {
                    _li = new ListItem(_pc.LanguageOption.Name, _pc.IdProcessClassification.ToString());
                    aspDdlPopUpProcessClassification.Items.Add(_li);


                    if (_pc.ChildrenClassifications.Count > 0)
                        AddProcessClassificationComboClassChilds(_pc, "");
                }
                aspDdlPopUpProcessClassification.SelectedValue = "-1";


                //Project
                ResetCboProject();

                //Measurement
                ResetCboMeasurement();
            }
            private void AddProcessClassificationComboClassChilds(Condesus.EMS.Business.PF.Entities.ProcessClassification _child, String nivel)
            {
                nivel += "   ";
                foreach (Condesus.EMS.Business.PF.Entities.ProcessClassification _childClassif in _child.ChildrenClassifications.Values)
                {
                    ListItem _li = new ListItem(nivel + _childClassif.LanguageOption.Name, _childClassif.IdProcessClassification.ToString());
                    aspDdlPopUpProcessClassification.Items.Add(_li);

                    if (_childClassif.ChildrenClassifications.Count > 0)
                        AddProcessClassificationComboClassChilds(_childClassif, nivel);
                }
            }
            void aspDdlPopUpProcessClassification_SelectedIndexChanged(object sender, EventArgs e)
            {
                ResetCboProject();
                ResetCboMeasurement();

                Int64 _idProcessClasif = Convert.ToInt64(aspDdlPopUpProcessClassification.SelectedValue);

                if (_idProcessClasif > 0)
                {
                    Dictionary<Int64, Condesus.EMS.Business.PF.Entities.ProcessGroupProcess> _groupProjects = EMSLibrary.User.ProcessFramework.Map.ProcessClassification(_idProcessClasif).ChildrenElements;
                    foreach (Condesus.EMS.Business.PF.Entities.ProcessGroupProcess _project in _groupProjects.Values)
                    {
                        ListItem _li = new ListItem(_project.LanguageOption.Title, _project.IdProcess.ToString());
                        aspDdlPopUpProject.Items.Add(_li);
                    }
                }
                aspDdlPopUpProject.Enabled = (aspDdlPopUpProject.Items.Count > 1) ? true : false;

            }
            void aspDdlPopUpProject_SelectedIndexChanged(object sender, EventArgs e)
            {
                ResetCboMeasurement();

                Int64 _idProject = Convert.ToInt64(aspDdlPopUpProject.SelectedValue);
                //Lo necesito y deberia obtenerlo cuando hago click en el add x Javascript...
                Int64 _idParamIndicator = Convert.ToInt64(hdn_ParamIndicatorId.Value);

                if (_idProject > 0 && _idParamIndicator > -1)
                {
                    Dictionary<Int64, Condesus.EMS.Business.PA.Entities.Measurement> _measurements = ((Condesus.EMS.Business.PF.Entities.ProcessGroupProcess)EMSLibrary.User.ProcessFramework.Map.Process(_idProject)).MeasurementsByIndicator(_idParamIndicator);
                    foreach (Condesus.EMS.Business.PA.Entities.Measurement _measurement in _measurements.Values)
                    {
                        ListItem _li = new ListItem(_measurement.LanguageOption.Name, _measurement.IdMeasurement.ToString());
                        aspDdlPopUpMeasurement.Items.Add(_li);
                    }
                }
                aspDdlPopUpMeasurement.Enabled = (aspDdlPopUpMeasurement.Items.Count > 1) ? true : false;
            }
            private void ResetCboProject()
            {
                aspDdlPopUpProject.Items.Clear();

                ListItem _li = new ListItem(Resources.CommonListManage.comboboxselectitemprocessclassifications, "-1");
                aspDdlPopUpProject.Items.Add(_li);
                aspDdlPopUpProject.SelectedValue = "-1";
                aspDdlPopUpProject.Enabled = false;
            }
            private void ResetCboMeasurement()
            {
                aspDdlPopUpMeasurement.Items.Clear();

                //_li = new ListItem(Resources.ArchivoTemp.MeasurementDevicesProperties_SelectUnit, "-1");
                ListItem _li = new ListItem("Select a Measurement", "-1");
                aspDdlPopUpMeasurement.Items.Add(_li);
                aspDdlPopUpMeasurement.SelectedValue = "-1";
                aspDdlPopUpMeasurement.Enabled = false;
            }
        #endregion

        #region Rad ParamsGrid
            private void BuildCalculationParamsGrid()
            {
                //up_MainData.ContentTemplateContainer.FindControl("pnlFormulaParams").Controls.Clear();
                pnlFormulaParams.Controls.Clear();

                //Solo la inyecto si hay una Formula "Activa" (sino no se el contenido)
                if (Convert.ToInt64(GetKeyValue( _RdcFormula.SelectedValue, "IdFormula")) > 0)
                {
                    RadGrid _paramGrid = BuildParameterGrid();

                    //Inyecto la Grilla de Parametros al Formulario (via su Update Panel)
                    //up_MainData.ContentTemplateContainer.FindControl("pnlFormulaParams").Controls.Add(_paramGrid);
                    pnlFormulaParams.Controls.Add(_paramGrid);
                }
            }
            private RadGrid BuildParameterGrid()
            {
                RadGrid _RadMasterGrid = new RadGrid();
                _RadMasterGrid.ID = _PARAMS_GRID_NAME;

                #region Propiedades y Handlers
                _RadMasterGrid.AllowPaging = true;
                _RadMasterGrid.AllowSorting = false;
                _RadMasterGrid.Width = Unit.Percentage(100);
                _RadMasterGrid.AutoGenerateColumns = false;
                _RadMasterGrid.GridLines = System.Web.UI.WebControls.GridLines.None;
                _RadMasterGrid.ShowStatusBar = false;
                _RadMasterGrid.PageSize = 50;
                _RadMasterGrid.AllowMultiRowSelection = false;
                _RadMasterGrid.PagerStyle.AlwaysVisible = true;
                _RadMasterGrid.PagerStyle.Mode = GridPagerMode.NextPrevAndNumeric;
                _RadMasterGrid.MasterTableView.Width = Unit.Percentage(99);
                _RadMasterGrid.EnableViewState = false;
                _RadMasterGrid.Skin = "EMS";
                _RadMasterGrid.EnableEmbeddedSkins = false;

                //Crea los metodos de la grilla (Server)
                _RadMasterGrid.NeedDataSource += new GridNeedDataSourceEventHandler(_RadMasterGrid_NeedDataSource);
                _RadMasterGrid.ItemDataBound += new GridItemEventHandler(_RadMasterGrid_ItemDataBound);
                _RadMasterGrid.PageIndexChanged += new GridPageChangedEventHandler(_RadMasterGrid_PageIndexChanged);
                _RadMasterGrid.ItemDataBound += new GridItemEventHandler(_RadMasterGrid_ItemDataBound);

                //Crea los metodos de la grilla (Cliente)
                _RadMasterGrid.ClientSettings.AllowExpandCollapse = false;
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

                //Seteos para la paginacion de la grilla, ahora es culturizable.
                _RadMasterGrid.PagerStyle.Mode = GridPagerMode.NextPrevAndNumeric;
                _RadMasterGrid.MasterTableView.PagerStyle.Mode = GridPagerMode.NextPrevAndNumeric;

                _RadMasterGrid.MasterTableView.PagerStyle.NextPagesToolTip = Resources.Common.GridPagerStyleNextPagesToolTip;    // "Next Pages";
                _RadMasterGrid.MasterTableView.PagerStyle.NextPageText = Resources.Common.GridPagerStyleNextPageText;    // "&gt;";
                _RadMasterGrid.MasterTableView.PagerStyle.NextPageToolTip = Resources.Common.GridPagerStyleNextPageToolTip;  // "Next Page";
                _RadMasterGrid.MasterTableView.PagerStyle.PagerTextFormat = Resources.Common.GridPagerStylePagerTextFormat;  // "Change page: {4} &nbsp;Displaying page {0} of {1}, items {2} to {3} of {5}.";
                _RadMasterGrid.MasterTableView.PagerStyle.PrevPagesToolTip = Resources.Common.GridPagerStylePrevPagesToolTip;    // "Previous Pages";
                _RadMasterGrid.MasterTableView.PagerStyle.PrevPageText = Resources.Common.GrdiPagerStylePrevPageText;    // "&lt;";
                _RadMasterGrid.MasterTableView.PagerStyle.PrevPageToolTip = Resources.Common.GridPagerStylePrevPageToolTip;  // "Previous Page";

                _RadMasterGrid.PagerStyle.NextPagesToolTip = Resources.Common.GridPagerStyleNextPagesToolTip; //"Next Pages";
                _RadMasterGrid.PagerStyle.NextPageText = Resources.Common.GridPagerStyleNextPageText;    //"&gt;";
                _RadMasterGrid.PagerStyle.NextPageToolTip = Resources.Common.GridPagerStyleNextPageToolTip;  // "Next Page";
                _RadMasterGrid.PagerStyle.PagerTextFormat = Resources.Common.GridPagerStylePagerTextFormat;  // "Change page: {4} &nbsp;Displaying page {0} of {1}, items {2} to {3} of {5}.";
                _RadMasterGrid.PagerStyle.PrevPagesToolTip = Resources.Common.GridPagerStylePrevPagesToolTip;    // "Previous Pages";
                _RadMasterGrid.PagerStyle.PrevPageText = Resources.Common.GrdiPagerStylePrevPageText;    // "&lt;";
                _RadMasterGrid.PagerStyle.PrevPageToolTip = Resources.Common.GridPagerStylePrevPageToolTip;  // "Previous Page";

                #endregion

                //Crea las columnas para la MasterGrid.
                DefineColumnsGrid(_RadMasterGrid.MasterTableView);

                return _RadMasterGrid;
            }
            private DataTable GetParamGridDataTable()
            {
                DataTable _retDt = new DataTable("Root");
                _retDt.Columns.Add("paramName");
                _retDt.Columns.Add("paramPosition");
                _retDt.Columns.Add("idIndicator");
                _retDt.Columns.Add("indicatorName");
                _retDt.Columns.Add("idMeasurement");

                return _retDt;
            }
            private DataTable LoadParamGrid()
            {
                DataTable _dt = GetParamGridDataTable();

                //Si es un Add le paso el id Measurement en 0 a los Params (todavia no tengo el calculo para saberlo)
                if (Entity == null)
                {
                    Formula _formula = EMSLibrary.User.PerformanceAssessments.Configuration.Formula(Convert.ToInt64(GetKeyValue( _RdcFormula.SelectedValue, "IdFormula")));   // GetFormulaFromCboFormula();

                    if (_formula != null && _formula.FormulaParameters.Count > 0)
                    {
                        foreach (FormulaParameter _formulaParam in _formula.FormulaParameters.Values)
                        { _dt.Rows.Add(Common.Functions.ReplaceIndexesTags(_formulaParam.ParameterName), _formulaParam.PositionParameter, _formulaParam.Indicator.IdIndicator, Common.Functions.RemoveIndexesTags(_formulaParam.Indicator.LanguageOption.Name), 0); }
                    }
                }
                else
                {
                    foreach (EMS_PA.Entities.CalculationParameter _calculationParam in Entity.CalculationParameters.Values)
                    { _dt.Rows.Add(Common.Functions.ReplaceIndexesTags(_calculationParam.ParameterName), _calculationParam.PositionParameter, _calculationParam.Measurement.Indicator.IdIndicator, Common.Functions.RemoveIndexesTags(_calculationParam.Measurement.Indicator.LanguageOption.Name), _calculationParam.Measurement.IdMeasurement); }
                }

                return _dt;
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
                boundColumn.DataField = "indicatorName";
                boundColumn.HeaderText = "Indicator";
                boundColumn.Display = true;
                gridTableViewDetails.Columns.Add(boundColumn);

                boundColumn = new GridBoundColumn();
                boundColumn.DataField = "idMeasurement";
                boundColumn.HeaderText = "Measurement";
                boundColumn.Display = false;
                gridTableViewDetails.Columns.Add(boundColumn);

                GridTemplateColumn template;

                Boolean _canEdit = true;
                if (Entity != null)
                    { _canEdit = false; }

                template = new GridTemplateColumn();
                template.UniqueName = "cboMeasurementColumn";
                template.ItemTemplate = new PopUpTemplate("Measurement", _canEdit);
                template.HeaderText = "Measurement";
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
                        //PopUpMeasurement
                        HiddenField _lblMeasurementId = (HiddenField)e.Item.FindControl("Measurement_idValue");
                        Label _lblMeasurementValue = (Label)e.Item.FindControl("Measurement_txtValue");

                        Int64 _idMeasurement = Convert.ToInt64(((GridDataItem)e.Item)["idMeasurement"].Text);

                        EMS_PA.Entities.Measurement _measurement = EMSLibrary.User.PerformanceAssessments.Configuration.Measurement(_idMeasurement);
                        if (_measurement != null)
                        {
                            _lblMeasurementId.Value = _measurement.IdMeasurement.ToString();
                            _lblMeasurementValue.Text = Common.Functions.ReplaceIndexesTags(_measurement.LanguageOption.Name);
                        }
                    }
                }
                void _RadMasterGrid_PageIndexChanged(object source, GridPageChangedEventArgs e)
                {
                    RadGrid handler = (RadGrid)source;
                    handler.DataSource = LoadParamGrid();
                    handler.MasterTableView.Rebind();
                }
            #endregion
        #endregion

        #endregion

        #region Private Methods
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonListManage.Calculation;
                lblDescription.Text = Resources.CommonListManage.Description;
                lblFormula.Text = Resources.CommonListManage.Formula;
                lblFormulaIndicator.Text = Resources.CommonListManage.Indicator;
                lblFormulaLiteral.Text = Resources.CommonListManage.LiteralFormula;
                lblFormulaMU.Text = Resources.CommonListManage.MeasurementUnit;
                lblFormulaSp.Text = Resources.CommonListManage.SPSchema;
                lblFrequency.Text = Resources.CommonListManage.Frequency;
                lblIsRelevant.Text = Resources.CommonListManage.IsRelevant;
                lblMeasurementUnit.Text = Resources.CommonListManage.MeasurementUnit;
                lblPopUpMeasurement.Text = Resources.CommonListManage.Measurement;
                lblPopUpProcessClassification.Text = Resources.CommonListManage.ProcessClassification;
                lblPopUpProject.Text = Resources.CommonListManage.Project;
                lblTimeUnitFrequency.Text = Resources.CommonListManage.TimeUnitFrequency;
                lblTitleParams.Text = Resources.CommonListManage.Parameters;
                lblTitleProcess.Text = Resources.CommonListManage.Process;
                rfv1.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
                rfv3.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
                cv1.ErrorMessage = Resources.ConstantMessage.ValidationWrongFormat;
            }
            private void AddComboTimeUnitFrequency()
            {
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                AddCombo(phTimeUnitFrequency, ref _RdcTimeUnitFrequency, Common.ConstantsEntitiesName.PF.TimeUnits, String.Empty, _params, false, true, false, false, false);

                ValidatorRequiredField(Common.ConstantsEntitiesName.PF.TimeUnits, phTimeUnitFrequencyValidator, ref _CvTimeUnitFrequency, _RdcTimeUnitFrequency, Resources.ConstantMessage.SelectATimeUnit);
            }
            private void SetTimeUnitFrequency()
            {
                _RdcTimeUnitFrequency.SelectedValue = "IdTimeUnit=" + Entity.IdTimeUnitFrequency.ToString();
            }
            private void AddComboFormulas()
            {
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                //_params.Add("IdIndicator", _IdIndicator);
                AddCombo(phFormula, ref _RdcFormula, Common.ConstantsEntitiesName.PA.Formulas, String.Empty, _params, false, true, false, true, false);
                _RdcFormula.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(ddlFormula_SelectedIndexChanged);
                
                FwMasterPage.RegisterContentAsyncPostBackTrigger(_RdcFormula, "SelectedIndexChanged");

                ValidatorRequiredField(Common.ConstantsEntitiesName.PA.Formulas, phFormulaValidator, ref _CvFormula, _RdcFormula, Resources.ConstantMessage.SelectAFormula);
            }
            private void SetFormulas()
            {
                _RdcFormula.SelectedValue = "IdFormula=" + Entity.Formula.IdFormula.ToString();
            }
            private void AddComboMeasurementUnits()
            {
                Formula _formula;
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                if (GetKeyValue(_RdcFormula.SelectedValue, "IdFormula") != null)
                {
                    Int64 _idFormula = Convert.ToInt64(GetKeyValue(_RdcFormula.SelectedValue, "IdFormula"));
                    _formula = EMSLibrary.User.PerformanceAssessments.Configuration.Formula(_idFormula);
                    _params.Add("IdMagnitud", _formula.Indicator.Magnitud.IdMagnitud);
                }

                AddCombo(phMeasurementUnit, ref _RdcMeasurementUnit, Common.ConstantsEntitiesName.PA.MeasurementUnits, String.Empty, _params, false, true, false, false, false);

                ValidatorRequiredField(Common.ConstantsEntitiesName.PA.MeasurementUnits, phMeasurementUnitValidator, ref _CvMeasurementUnit, _RdcMeasurementUnit, Resources.ConstantMessage.SelectAMeasurementUnit);
            }
            private void SetMeasurementUnit()
            {
                _RdcMeasurementUnit.SelectedValue = "IdMeasurementUnit=" + Entity.MeasurementUnit.IdMeasurementUnit.ToString() + "& IdMagnitud=" + Entity.Formula.Indicator.Magnitud.IdMagnitud.ToString();
            }
            private void InitProcessGroupProjectsGrid()
            {
                //En un supuesto Edit, tengo que poblar la coleccion de _SelectedRows antes de construir aca para llenar los Checks de los Projects)
                rgdMasterGrid.DataSource = LoadGrid();
            }
            private DataTable LoadGrid()
            {
                DataTable _dt = new DataTable();
                _dt.TableName = "Root";
                _dt.Columns.Add("IdProcess");
                _dt.Columns.Add("Name");

                 // set primary keys.
                DataColumn[] keys = new DataColumn[1];
                DataColumn column = new DataColumn();
                column = _dt.Columns["IdProcess"];
                keys[0] = column;
                _dt.PrimaryKey = keys;

                //TODO Cambiar cuando damian nos devuelva el metodo Todos los Process
                //foreach (EMS_PM.Entities.ProcessGroupProcess _project in EMSLibrary.User.ProcessFramework.Map.ProcessGroupProcesses().Values)
                //    _dt.Rows.Add(_project.IdProcess, _project.LanguageOption.Title);

                ////1° CArga todos los que estan sin clasificaion y/o tengo permisos solo sobre el elemento y no la class..
                //foreach (ProcessGroupProcess _processGroupProcess in EMSLibrary.User.ProcessFramework.Map.ProcessGroupProcessRoots().Values)
                //{
                //    if (!_dt.Rows.Contains(_processGroupProcess.IdProcess))
                //    {
                //        _dt.Rows.Add(_processGroupProcess.IdProcess, _processGroupProcess.LanguageOption.Title);
                //    }
                //}
                ////2° recorre todas las clasificaciones y por cada clasificacion pide sus process.
                //foreach (ProcessClassification _processClassification in EMSLibrary.User.ProcessFramework.Map.ProcessClassifications().Values)
                //{
                //    foreach (ProcessGroupProcess _processGroupProcess in EMSLibrary.User.ProcessFramework.Map.ProcessGroupProcesses(_processClassification.IdProcessClassification).Values)
                //    {
                //        //Valida que ya no lo hayan cargado.
                //        if (!_dt.Rows.Contains(_processGroupProcess.IdProcess))
                //        {
                //            _dt.Rows.Add(_processGroupProcess.IdProcess, _processGroupProcess.LanguageOption.Title);
                //        }
                //    }
                //}

                foreach (ProcessGroupProcess _processGroupProcess in EMSLibrary.User.Dashboard.ProcessGroupProcess.Values)
                {
                    //Valida que ya no lo hayan cargado.
                    if (!_dt.Rows.Contains(_processGroupProcess.IdProcess))
                    {
                        _dt.Rows.Add(_processGroupProcess.IdProcess, _processGroupProcess.LanguageOption.Title);
                    }
                }

                return _dt;
            }
            private void Add()
            {
                //ddlFormula.Enabled = true;
                //ddlFormula.SelectedIndex = 0;
                pnlFormulaProperties.Visible = false;

                //Esto va a Depender de si viene de PM o no (_IdProject != 0...)
                //ddlMeasurementUnit.Enabled = true;
                //ddlMeasurementUnit.SelectedIndex = 0;

                //ddlTimeUnitFrequency.Enabled = true;
                //ddlTimeUnitFrequency.SelectedIndex = 0;

                txtName.ReadOnly = false;
                txtName.Text = String.Empty;
                txtDescription.ReadOnly = false;
                txtDescription.Text = String.Empty;
                txtFrequency.ReadOnly = false;
                txtFrequency.Text = String.Empty;
                chkIsRelevant.Enabled = true;

                //radMnuGRC.Enabled = false;
                //rmnGeneralOptions.Enabled = false;

                //btnSave.Style.Add("display", "block");
                //lblParameter.Style["display"] = "none";
                ////Seteo del menu
                //((RadMenuItem)rmnOption.FindItemByValue("rmiLanguage")).Enabled = false;
                //((RadMenuItem)rmnOption.FindItemByValue("rmiDelete")).Enabled = false;
            }
            private void LoadData()
            {
                _RdcFormula.Enabled = false;

                //Inputs
                txtDescription.Text = Entity.LanguageOption.Description;
                txtName.Text = Entity.LanguageOption.Name;
                txtFrequency.Text = Entity.Frequency.ToString();
                chkIsRelevant.Checked = Entity.IsRelevant;

                //Nunca puedo editar la Formula
                SetFormulas();
                ShowFormulaProperties(Entity.Formula);
                AddComboMeasurementUnits();
                SetMeasurementUnit();
                SetTimeUnitFrequency();

                //Inicializo los SelectedProyects en mi Coleccion
                if (_SelectedRows.Count == 0)
                {
                    foreach (ProcessGroupProcess _project in Entity.AssociatedProcess.Values)
                    { _SelectedRows.Add(_project.IdProcess); }
                }
            }
            private void ShowFormulaProperties(Formula formula)
            {
                if (formula != null)
                {
                    pnlFormulaProperties.Visible = true;

                    lblFormulaLiteralValue.Text = Common.Functions.ReplaceIndexesTags(formula.LiteralFormula);
                    lblFormulaIndicatorValue.Text = Common.Functions.ReplaceIndexesTags(formula.Indicator.LanguageOption.Name);
                    lblFormulaMUValue.Text = Common.Functions.ReplaceIndexesTags(formula.MeasurementUnit.LanguageOption.Name);
                    lblFormulaSpValue.Text = formula.SchemaSP.Name;
                }
                else
                {
                    pnlFormulaProperties.Visible = false;
                }
            }
        #endregion

        #region Eventos
            void ddlFormula_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
            {
                Formula _formula = EMSLibrary.User.PerformanceAssessments.Configuration.Formula(Convert.ToInt64(GetKeyValue(_RdcFormula.SelectedValue, "IdFormula")));  // GetFormulaFromCboFormula();

                AddComboMeasurementUnits();
                _RdcMeasurementUnit.SelectedValue = "IdMeasurementUnit=" + _formula.MeasurementUnit.IdMeasurementUnit.ToString() + "& IdMagnitud=" + _formula.Indicator.Magnitud.IdMagnitud.ToString();


                //Seteo la Informacion de Formula
                ShowFormulaProperties(_formula);

                //Reconstruyo Controles Dinamicos
                BuildCalculationParamsGrid();
            }
            void btnSave_Click(object sender, EventArgs e)
            {
                try
                {
                    //Parametros del Add
                    Formula _formula = EMSLibrary.User.PerformanceAssessments.Configuration.Formula(Convert.ToInt64(GetKeyValue(_RdcFormula.SelectedValue, "IdFormula")));
                    Int64 _idIndicator = _formula.Indicator.IdIndicator;

                    Int64 _idMeasurementUnit = Convert.ToInt64(GetKeyValue(_RdcMeasurementUnit.SelectedValue, "IdMeasurementUnit"));
                    Int64 _idMagnitud = Convert.ToInt64(GetKeyValue(_RdcMeasurementUnit.SelectedValue, "IdMagnitud"));
                    MeasurementUnit _measurementUnit = EMSLibrary.User.PerformanceAssessments.Configuration.Magnitud(_idMagnitud).MeasurementUnit(_idMeasurementUnit);

                    Int64 _idTimeUnit = Convert.ToInt64(GetKeyValue(_RdcTimeUnitFrequency.SelectedValue, "IdTimeUnit"));
                    TimeUnit _timeUnitFrequency = EMSLibrary.User.ProcessFramework.Configuration.TimeUnit(_idTimeUnit);

                    //Si nunca Pagino, tengo que salvar la pagina actual (y "unica") como si fuese una paginacion para agregarlos a la coleccion
                    SetSelectedItems();
                    var _processGroupProjects = new Dictionary<Int64, EMS_PM.Entities.ProcessGroupProcess>();
                    foreach (Int64 _idProject in _SelectedRows)
                    {
                        ProcessGroupProcess _project = (ProcessGroupProcess)EMSLibrary.User.ProcessFramework.Map.Process(_idProject);
                        _processGroupProjects.Add(_idProject, _project);
                    }

                    DataTable _parameters = new DataTable();
                    _parameters.Columns.Add("ParameterName");
                    _parameters.Columns.Add("PositionParameter");
                    _parameters.Columns.Add("IdMeasurementParameter");

                    //Me traigo la grilla y la recorro guardando Paramtros editados
                    Control _bufParamsGrid = up_MainData.ContentTemplateContainer.FindControl(_PARAMS_GRID_NAME);
                    if (_bufParamsGrid != null && _bufParamsGrid is RadGrid)
                    {
                        RadGrid _paramsGrid = (RadGrid)_bufParamsGrid;
                        //Recorro los parametros de la grilla y salvo los parametros del Calculo
                        foreach (GridDataItem _item in _paramsGrid.MasterTableView.Items)
                        {
                            //Grid Vars
                            String _paramName = _item["paramName"].Text;
                            Int64 _paramPosition = Convert.ToInt64(_item["paramPosition"].Text);

                            String _measurementIdValue = ((HiddenField)_item.FindControl("Measurement_idValue")).Value;
                            if (_measurementIdValue == String.Empty) { _measurementIdValue = "0"; }
                            Int64 _idMeasurement = Convert.ToInt64(_measurementIdValue);
                            if (!(_idMeasurement > 0))
                                throw new Exception(GetLocalResourceObject("SelectMeasurementParamPopUp").ToString()); // "All the Parameters must have a Measurement.");

                            //Lleno la DataTable con Valores de Parametros
                            _parameters.Rows.Add(_paramName, _paramPosition, _idMeasurement);
                        }
                    }

                    Boolean _isRelevant = true;
                    _isRelevant = chkIsRelevant.Checked;

                    //Se supone que no deberian haber nulls, x Validacion de parte de la Pagina, pero vuelvo a validar x Seguridad
                    if (_formula != null && _measurementUnit != null && _timeUnitFrequency != null && _processGroupProjects.Count > 0 && _parameters.Rows.Count > 0)
                    {
                        if (Entity == null)
                        {
                            //Es un ADD
                            Entity = EMSLibrary.User.PerformanceAssessments.Configuration.CalculationAdd(_formula, _measurementUnit, txtName.Text, txtDescription.Text, Convert.ToInt16(txtFrequency.Text), _timeUnitFrequency, _processGroupProjects, _parameters, _isRelevant);
                            
                            //base.NavigatorAddTransferVar("IdCalculation", Entity.IdCalculation);
                            //base.NavigatorAddTransferVar("IdIndicator", _idIndicator);

                            //String _pkValues = "IdCalculation=" + Entity.IdCalculation.ToString()
                            //    + "& IdIndicator=" + _idIndicator.ToString();
                            //base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);

                            //base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.PA.Calculation);
                            //base.NavigatorAddTransferVar("EntityNameContextInfo", String.Empty);
                            //base.NavigatorAddTransferVar("EntityNameContextElement", String.Empty);
                            //Navigate("~/MainInfo/ListViewer.aspx", Resources.CommonListManage.Calculation + " " + Entity.LanguageOption.Name);

                            ////_IdCalculation = Entity.IdCalculation;
                            ////_ItemOptionSelected = "EDIT";
                            //rgdMasterGrid.Rebind();
                            //hdn_changeParamsGridState.Value = "true";
                        }
                        else
                        {
                            //Es modify
                            Entity.Modify(_measurementUnit, txtName.Text, txtDescription.Text, Convert.ToInt16(txtFrequency.Text), _timeUnitFrequency.IdTimeUnit, _processGroupProjects, _isRelevant);

                            //base.NavigatorAddTransferVar("IdCalculation", Entity.IdCalculation);
                            //base.NavigatorAddTransferVar("IdIndicator", _idIndicator);

                            //base.Navigate(HttpContext.Current.Request.Url.AbsolutePath, Entity.LanguageOption.Name + " " + Resources.Common.Edit, Condesus.WebUI.Navigation.DeleteType.DeleteModifiedEntity);


                            //Me deberia devolver el Objeto "reconstruido". Como no lo hace, lo reconstruyo aca
                            //Recargo el Objeto Calculation con los Cambios
                            //_Calculation = EMSLibrary.User.PerformanceAssessments.Configuration.Calculation(_IdCalculation);
                        }


                        base.NavigatorAddTransferVar("IdCalculation", Entity.IdCalculation);
                        base.NavigatorAddTransferVar("IdIndicator", _idIndicator);

                        String _pkValues = "IdCalculation=" + Entity.IdCalculation.ToString()
                            + "& IdIndicator=" + _idIndicator.ToString();
                        base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);

                        base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.PA.Calculation);
                        base.NavigatorAddTransferVar("EntityNameContextInfo", String.Empty);
                        base.NavigatorAddTransferVar("EntityNameContextElement", String.Empty);
                        //Navigate("~/MainInfo/ListViewer.aspx", Resources.CommonListManage.Calculation + " " + Entity.LanguageOption.Name);

                        String _entityPropertyName = String.Concat(Entity.LanguageOption.Name);
                        NavigatePropertyEntity(Common.ConstantsEntitiesName.PA.Calculation, _entityPropertyName, Condesus.WebUI.Navigation.NavigateMenuAction.Add);
                    }
                    else
                    {
                        //Si no hay cosas seleccionadas, aca muestra mensajes!!!.
                        ValidationErrors(_formula, _measurementUnit, _timeUnitFrequency, _processGroupProjects, _parameters);
                    }
                    ////Seteo del menu  - Language (Agregar cuando haya Edit)
                    //((RadMenuItem)rmnOption.FindItemByValue("rmiLanguage")).Enabled = true;
                    //((RadMenuItem)rmnOption.FindItemByValue("rmiDelete")).Enabled = true;

                    base.StatusBar.ShowMessage(Resources.Common.SaveOK);

                    //LoadData();
                    //CheckSecurity();
                }
                catch (Exception ex)
                {
                    String _outMessage = ex.Message;
                    //Valido el PK Name (Deberia haber un metodo en Bussines (bool Exists(Name)) que me diga si ya existe este PK
                    if (ex.Message.Contains(Common.Constants.MessageDuplicateKeyContent))
                        { _outMessage = GetLocalResourceObject("DuplicateCalculationMessage").ToString(); } // "A Calculation with this name allready exists. The Name of the Calculation must be Unique.";

                    base.StatusBar.ShowMessage(ex);
                }

                //BuildCalculationParamsGrid();
            }
            private void ValidationErrors(Condesus.EMS.Business.PA.Entities.Formula formula, Condesus.EMS.Business.PA.Entities.MeasurementUnit measurementUnit, Condesus.EMS.Business.PF.Entities.TimeUnit timeUnitFrequency, Dictionary<long, Condesus.EMS.Business.PF.Entities.ProcessGroupProcess> processGroupProjects, DataTable parameters)
            {
                String _errorMessage = String.Empty;

                if (formula == null)
                    _errorMessage = Resources.ConstantMessage.SelectAFormula; // "You must select a Formula";
                if (measurementUnit == null)
                    _errorMessage = Resources.ConstantMessage.SelectAMeasurementUnit; // "You must select a Measurement Unit";
                if (timeUnitFrequency == null)
                    _errorMessage = Resources.ConstantMessage.SelectATimeUnit;    // "You must select a Time Unit";
                if (parameters.Rows.Count == 0)
                    _errorMessage = Resources.ConstantMessage.SelectAParameterMeasurement;    // "All the Parameters must have a Measurement";
                if (processGroupProjects.Count == 0)
                    _errorMessage = Resources.ConstantMessage.SelectAProcess; // "You must select at least one Project";


                throw new Exception(_errorMessage);
            }

            #region Grid
                protected void rgdMasterGrid_ItemDataBound(object sender, GridItemEventArgs e)
                {
                    //Ver los Seleccionados para persistir el estado
                    if (e.Item is GridDataItem)
                    {
                        GridDataItem _item = (GridDataItem)e.Item;

                        CheckBox _chk = (CheckBox)e.Item.FindControl("chkSelect");
                        Int64 _value = 0;
                        if (_chk != null)
                        {
                            try
                            {
                                _value = Convert.ToInt64(_item["IdProcess"].Text);
                                _chk.Checked = (_SelectedRows.Contains(_value)) ? true : false;
                            }
                            catch { }

                            //Se resolvio permitir editar Projects. (17/07/2008)
                            //_chk.Enabled = (_Calculation == null) ? true : false;
                        }
                    }
                }
                void rgdMasterGrid_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
                {
                    rgdMasterGrid.DataSource = LoadGrid();
                }
                void rgdMasterGrid_PageIndexChanged(object source, GridPageChangedEventArgs e)
                {
                    SetSelectedItems();

                    rgdMasterGrid.DataSource = LoadGrid();
                    rgdMasterGrid.MasterTableView.Rebind();
                }
                /// <summary> Persiste los estados de los CheckBoxes de la Grilla </summary>
                private void SetSelectedItems()
                {
                    for (int i = 0; i < rgdMasterGrid.MasterTableView.Items.Count; i++)
                    {
                        GridDataItem _item = (GridDataItem)rgdMasterGrid.MasterTableView.Items[i];

                        CheckBox _chk = (CheckBox)_item.FindControl("chkSelect");
                        Int64 _value = Convert.ToInt64(_item["IdProcess"].Text);

                        if (_chk != null)
                        {
                            if (_chk.Checked)
                            {
                                if (!_SelectedRows.Contains(_value))
                                    _SelectedRows.Add(_value);
                            }
                            else
                            {
                                if (_SelectedRows.Contains(_value))
                                    _SelectedRows.Remove(_value);
                            }
                        }
                    }
                }
            #endregion

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
            _btnDoPopUp.Attributes["name"] = "Measurement_EditButton";
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
