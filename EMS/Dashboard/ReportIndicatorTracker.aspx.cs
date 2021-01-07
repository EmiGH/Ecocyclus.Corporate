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
using Telerik.Charting;
using Condesus.EMS.WebUI.ManagementTools.ProcessesMap;
using System.Linq;
using System.Globalization;
using System.Threading;
using Condesus.EMS.WebUI.MasterControls;
using Condesus.EMS.Business.PF.Entities;
using Condesus.EMS.Business.PA.Entities;
using Condesus.EMS.Business.NT.Entities;
using Condesus.EMS.Business.DS.Entities;
using System.Text;


namespace Condesus.EMS.WebUI
{
    public partial class ReportIndicatorTracker : BaseProperties
    {
        #region Internal Properties
            private Int64 _IdProcess
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdProcess") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdProcess")) : Convert.ToInt64(GetPKfromNavigator("IdProcess"));
                }
            }
            private RadComboBox _RdcIndicator;
            private RadTreeView _RtvIndicator;
            private RadGrid _RgdMasterGridFacilities;
            private RadGrid _RgdMasterGridDataSeries;
            private RadGrid _RgdMasterGridStats;
            private String _EntityNameGRC = String.Empty;
        #endregion

        #region PageLoad & Init
            protected override void InitializeHandlers()
            {
                base.InitializeHandlers();
            }
            protected override void OnPreInit(EventArgs e)
            {
                base.OnPreInit(e);

                //Registro Mis Custom MenuPanels
                RegisterCustomMenuPanels();
            }
            protected override void OnInit(EventArgs e)
            {
                base.OnInit(e);
                LoadTextLabels();

                _EntityNameGRC = base.NavigatorGetTransferVar<String>("EntityNameContextInfo");
                //Carga el GRC en caso de que lo hayan pasado por parametros.
                LoadGRCByEntity();
            }
            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);

                AddComboIndicators();

                if (!Page.IsPostBack)
                {   //Form
                    base.SetContentTableRowsCss(tblContentForm);
                }

                Int64 _idIndicator = 0;
                if (_RtvIndicator.SelectedNode != null)
                {
                    if (GetKeyValue(_RtvIndicator.SelectedNode.Value, "IdIndicator") != null)
                    {
                        _idIndicator = Convert.ToInt64(GetKeyValue(_RtvIndicator.SelectedNode.Value, "IdIndicator"));
                        if (ManageEntityParams.ContainsKey("StartDate"))
                        { ManageEntityParams.Remove("StartDate"); }
                        ManageEntityParams.Add("StartDate", rdtFrom.SelectedDate);
                        if (ManageEntityParams.ContainsKey("EndDate"))
                        { ManageEntityParams.Remove("EndDate"); }
                        ManageEntityParams.Add("EndDate", rdtThrough.SelectedDate);
                        if (ManageEntityParams.ContainsKey("IdIndicator"))
                        { ManageEntityParams.Remove("IdIndicator"); }
                        ManageEntityParams.Add("IdIndicator", _idIndicator);
                        if (ManageEntityParams.ContainsKey("IdProcess"))
                        { ManageEntityParams.Remove("IdProcess"); }
                        ManageEntityParams.Add("IdProcess", _IdProcess);

                        //Carga la Grilla
                        LoadGridFacilities();
                    }
                }
            }
            protected override void SetPagetitle()
            {
                try
                {
                    base.PageTitle = Resources.CommonListManage.IndicatorTracker;
                }
                catch { base.PageTitle = String.Empty; }
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }
        #endregion

        #region Private Methods
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
            }
            private void LoadTextLabels()
            {
                lblFrom.Text = Resources.CommonListManage.From;
                lblThrough.Text = Resources.CommonListManage.Through;
                lblIndicatorClassification.Text = Resources.CommonListManage.Indicator;
                lblSubTitle.Text = Resources.Common.RelatedData;
            }
            private void LoadGridFacilities()
            {
                phGridFacilities.Controls.Clear();

                //Setea los datos en el DataTable de base, para que luego se carge la grilla.
                BuildGenericDataTable(Common.ConstantsEntitiesName.RG.ReportIndicatorTracker, ManageEntityParams);
                
                _RgdMasterGridFacilities = base.BuildListViewerContent(Common.ConstantsEntitiesName.RG.ReportIndicatorTracker);
                _RgdMasterGridFacilities.SelectedIndexChanged += new EventHandler(RgdMasterGridFacilities_SelectedIndexChanged);
                _RgdMasterGridFacilities.PageSize = 10;
                _RgdMasterGridFacilities.ClientSettings.EnablePostBackOnRowClick = true;
                _RgdMasterGridFacilities.ClientSettings.Selecting.AllowRowSelect = true;

                phGridFacilities.Controls.Add(_RgdMasterGridFacilities);
            }
            //private void Charts()
            //{
            //    const double hourStep = 1 / 24.0;
            //    const double minuteStep = hourStep / 60;
            //    const double fiveMinuteStep = minuteStep * 5;

            //    double startTime = new DateTime(2008, 1, 1, 8, 0, 0, 0).ToOADate();
            //    double endTime = new DateTime(2008, 1, 1, 17, 0, 0, 0).ToOADate();

            //    chartIndicatorTracker.PlotArea.XAxis.AddRange(startTime, endTime, hourStep);

            //    Random r = new Random();
            //    ChartSeries s = chartIndicatorTracker.Series[0];

            //    for (double currentTime = startTime; currentTime < endTime; currentTime += fiveMinuteStep)
            //    {
            //        ChartSeriesItem item = new ChartSeriesItem();
            //        item.XValue = currentTime + (r.NextDouble() - 0.5) * fiveMinuteStep;
            //        item.YValue = 7065 + (r.NextDouble() - 0.5) * 90;
            //        s.Items.Add(item);
            //    }

            //    double today = DateTime.Now.Date.ToOADate();
            //    double lastMonth = DateTime.Now.Date.AddMonths(-1).ToOADate();
            //    chartIndicatorTracker.PlotArea.XAxis.AddRange(lastMonth, today, 1);
            //    s = chartIndicatorTracker.Series[0];
            //    for (double currentDate = lastMonth; currentDate <= today; currentDate++)
            //    {
            //        ChartSeriesItem item = new ChartSeriesItem();
            //        item.YValue = 7065 + (r.NextDouble() - 0.5) * 90;
            //        s.Items.Add(item);
            //    }
            //}
            //private void AddChartSeriesOri(RadChart rdChartIndicatorTracker, String measurementName, int seed)
            //{
            //    // Define chart series
            //    ChartSeries chartSeries = new ChartSeries();
            //    chartSeries.Appearance.LabelAppearance.Visible = false;
            //    chartSeries.Name = measurementName;
            //    chartSeries.Type = ChartSeriesType.Line;
            //    //chartSeries.Appearance.LineSeriesAppearance.Color = System.Drawing.Color.Red;     //.BlueViolet;
            //    ChartSeriesItem _item = new ChartSeriesItem();

            //    //double today = Convert.ToDateTime("2001/01/01").ToOADate();
            //    //double lastMonth = Convert.ToDateTime("2001/12/01").ToOADate();     // DateTime.Now.Date.AddMonths(-1).ToOADate();
            //    //chartIndicatorTracker.PlotArea.XAxis.AddRange(today, lastMonth, 1);
            //    rdChartIndicatorTracker.PlotArea.XAxis.Clear();
            //    // Define the items in the series
            //    DateTime _date = Convert.ToDateTime("2001/01/01");
            //    for (int i = 0; i < 12; i++)
            //    {
            //        Random r = new Random(seed);
            //        _item = new ChartSeriesItem();
            //        //_item.XValue = _date.ToOADate();
            //        _item.YValue = ((i + seed) * r.Next());   // (i * r.Next()) + 90;

            //        rdChartIndicatorTracker.PlotArea.XAxis.AddItem(_date.ToShortDateString());

            //        chartSeries.Items.Add(_item);

            //        _date = _date.AddMonths(1);
            //    }
            //    // visually enhance the datapoints
            //    chartSeries.Appearance.PointMark.Dimensions.AutoSize = false;
            //    chartSeries.Appearance.PointMark.Dimensions.Width = 6;
            //    chartSeries.Appearance.PointMark.Dimensions.Height = 6;
            //    chartSeries.Appearance.PointMark.FillStyle.MainColor = System.Drawing.Color.Black;
            //    chartSeries.Appearance.PointMark.Visible = true;

            //    rdChartIndicatorTracker.Series.Add(chartSeries);
            //}

            private void AddChartSeries(RadChart rdChartIndicatorTracker, String measurementName, List<MeasurementPoint> measurementPoints)
            {
                // Define chart series
                ChartSeries chartSeries = new ChartSeries();
                chartSeries.Appearance.LabelAppearance.Visible = false;
                chartSeries.Name = measurementName;
                chartSeries.Type = ChartSeriesType.Line;
                ChartSeriesItem _item = new ChartSeriesItem();

                rdChartIndicatorTracker.PlotArea.XAxis.Clear();
                if (measurementPoints.Count() > 0)
                {
                    Int32 _interval = (Int32)(measurementPoints.Count() / 22);
                    _interval = (_interval == 0) ? 1 : _interval;
                    rdChartIndicatorTracker.PlotArea.XAxis.MaxValue = _interval;
                    rdChartIndicatorTracker.PlotArea.XAxis.MinValue = _interval;
                    rdChartIndicatorTracker.PlotArea.XAxis.Step = _interval;
                    rdChartIndicatorTracker.PlotArea.XAxis.LabelStep = _interval;
                }

                foreach (MeasurementPoint _point in measurementPoints)
                {
                    _item = new ChartSeriesItem();
                    _item.YValue = _point.MeasureValue;
                    rdChartIndicatorTracker.PlotArea.XAxis.AddItem(_point.MeasureDate.ToShortDateString());

                    chartSeries.Items.Add(_item);
                }
                // visually enhance the datapoints
                chartSeries.Appearance.PointMark.Dimensions.AutoSize = false;
                chartSeries.Appearance.PointMark.Dimensions.Width = 6;
                chartSeries.Appearance.PointMark.Dimensions.Height = 6;
                chartSeries.Appearance.PointMark.FillStyle.MainColor = System.Drawing.Color.Black;
                chartSeries.Appearance.PointMark.Visible = true;

                rdChartIndicatorTracker.Series.Add(chartSeries);
            }
            private void LoadMeasurementSeries()
            {
                DataTable _dt = DataTableListManage[Common.ConstantsEntitiesName.RG.ReportIndicatorTracker];
                DateTime? _startDate = null;
                DateTime? _endDate = null;

                _startDate = rdtFrom.SelectedDate;
                _endDate = rdtThrough.SelectedDate;

                foreach (DataRow _item in _dt.Rows)
                {
                    //Ya tengo la medicion
                    Measurement _measurement = EMSLibrary.User.PerformanceAssessments.Configuration.Measurement(Convert.ToInt64(_item["IdMeasurement"]));
                    //Ahora por cada medicion, armamos una Serie en el Chart....
                    AddChartSeries(chartIndicatorTracker, _measurement.LanguageOption.Name, _measurement.Series(_startDate, _endDate));
                }
            }
            private void LoadChartIndicatorTracker()
            {
                chartIndicatorTracker.Clear();

                //Seteo la cultura en USA, para poder trabajar con la fecha en MM/DD/YYYY HH:mm:ss
                CultureInfo _cultureUSA = new CultureInfo("en-US");
                //Me guarda la actual, para luego volver a esta...
                CultureInfo _currentCulture = CultureInfo.CurrentCulture;
                //Seta la cultura estandard
                Thread.CurrentThread.CurrentCulture = _cultureUSA;

                //Setea y arma una paleta de colores para las referencias
                SetChartCustomPalette(chartIndicatorTracker);

                // Define chart and titleRadChart radChart = new RadChart();
                chartIndicatorTracker.ChartTitle.TextBlock.Text = Resources.CommonListManage.IndicatorTracker;
                chartIndicatorTracker.ChartTitle.TextBlock.Appearance.TextProperties.Color = System.Drawing.Color.Blue;

                LoadMeasurementSeries();
                // set the plot area gradient background fill
                chartIndicatorTracker.PlotArea.Appearance.FillStyle.FillType = Telerik.Charting.Styles.FillType.Gradient;
                chartIndicatorTracker.PlotArea.XAxis.Appearance.Width = 3;
                // Set text and line for Y axis
                chartIndicatorTracker.PlotArea.YAxis.AxisLabel.TextBlock.Text = "";
                chartIndicatorTracker.PlotArea.YAxis.Appearance.Width = 3;
                chartIndicatorTracker.PlotArea.XAxis.Appearance.ValueFormat = Telerik.Charting.Styles.ChartValueFormat.LongDate;
                chartIndicatorTracker.PlotArea.XAxis.Appearance.CustomFormat = "dd/MM/yyyy HH:mm:ss"; 

                //Vuelve a la cultura original...
                Thread.CurrentThread.CurrentCulture = _currentCulture;
            }
            //private void LoadChartIndicatorTracker()
            //{
            //    //Seteo la cultura en USA, para poder trabajar con la fecha en MM/DD/YYYY HH:mm:ss
            //    CultureInfo _cultureUSA = new CultureInfo("en-US");
            //    //Me guarda la actual, para luego volver a esta...
            //    CultureInfo _currentCulture = CultureInfo.CurrentCulture;
            //    //Seta la cultura estandard
            //    Thread.CurrentThread.CurrentCulture = _cultureUSA;
                
            //    //Setea y arma una paleta de colores para las referencias
            //    SetChartCustomPalette(chartIndicatorTracker);

            //    //Carga el DT
            //    BuildGenericDataTable(Common.ConstantsEntitiesName.RG.ChartIndicatorTracker, ManageEntityParams);
            //    // Set a query to database.
            //    chartIndicatorTracker.DataSource = DataTableListManage[Common.ConstantsEntitiesName.RG.ChartIndicatorTracker];
            //    chartIndicatorTracker.DataBind();

            //    // Set additional chart properties and settings.
            //    chartIndicatorTracker.ChartTitle.TextBlock.Text = Resources.Common.Evolution;
            //    chartIndicatorTracker.ChartTitle.Visible = true;
            //    chartIndicatorTracker.SeriesOrientation = ChartSeriesOrientation.Vertical;
            //    if (chartIndicatorTracker.Series.Count > 0)
            //    {
            //        //Eliminamos la Serie que toma como año, ya que no sirve!
            //        if (chartIndicatorTracker.Series[0].Name == "Year")
            //        {
            //            chartIndicatorTracker.RemoveSeriesAt(0);
            //        }
            //    }
            //    for (int i = 0; i < chartIndicatorTracker.Series.Count; i++)
            //    {
            //        if (chartIndicatorTracker.Series[i].Items.Count == 1)
            //        {
            //            chartIndicatorTracker.Series[i].Type = ChartSeriesType.Point;
            //        }
            //        else
            //        {
            //            chartIndicatorTracker.Series[i].Type = ChartSeriesType.Spline;
            //        }
            //    }

            //    //chartLineEvolution.Legend.Appearance.GroupNameFormat = "#VALUE";

            //    //Vuelve a la cultura original...
            //    Thread.CurrentThread.CurrentCulture = _currentCulture;
            //}
            private void SetChartCustomPalette(RadChart radChart)
            {

                Palette customPalette = new Palette("CustomPalette");
                PaletteItem paletteItem = new PaletteItem();
                paletteItem.MainColor = System.Drawing.Color.Green;
                customPalette.Items.Add(paletteItem);

                PaletteItem paletteItem2 = new PaletteItem();
                paletteItem2.MainColor = System.Drawing.Color.Blue;
                customPalette.Items.Add(paletteItem2);

                PaletteItem paletteItem3 = new PaletteItem();
                paletteItem3.MainColor = System.Drawing.Color.Red;
                customPalette.Items.Add(paletteItem3);

                PaletteItem paletteItem4 = new PaletteItem();
                paletteItem4.MainColor = System.Drawing.Color.Purple;
                customPalette.Items.Add(paletteItem4);

                PaletteItem paletteItem5 = new PaletteItem();
                paletteItem5.MainColor = System.Drawing.Color.Yellow;
                customPalette.Items.Add(paletteItem5);

                PaletteItem paletteItem6 = new PaletteItem();
                paletteItem6.MainColor = System.Drawing.Color.Brown;
                customPalette.Items.Add(paletteItem6);

                PaletteItem paletteItem7 = new PaletteItem();
                paletteItem7.MainColor = System.Drawing.Color.Orange;
                customPalette.Items.Add(paletteItem7);

                PaletteItem paletteItem8 = new PaletteItem();
                paletteItem8.MainColor = System.Drawing.Color.Aqua;
                customPalette.Items.Add(paletteItem8);

                PaletteItem paletteItem9 = new PaletteItem();
                paletteItem9.MainColor = System.Drawing.Color.Black;
                customPalette.Items.Add(paletteItem9);

                PaletteItem paletteItem10 = new PaletteItem();
                paletteItem10.MainColor = System.Drawing.Color.DarkSalmon;
                customPalette.Items.Add(paletteItem10);

                PaletteItem paletteItem11 = new PaletteItem();
                paletteItem11.MainColor = System.Drawing.Color.DarkViolet;
                customPalette.Items.Add(paletteItem11);

                PaletteItem paletteItem12 = new PaletteItem();
                paletteItem12.MainColor = System.Drawing.Color.LawnGreen;
                customPalette.Items.Add(paletteItem12);

                radChart.CustomPalettes.Add(customPalette);
                radChart.SeriesPalette = "CustomPalette";
            }
            //private void HideLabels(RadChart radChart)
            //{
            //    foreach (ChartSeries series in radChart.Series)
            //    {
            //        series.Appearance.LabelAppearance.Visible = false;
            //    }
            //}
            private void LoadGridDataSeries(Dictionary<String, Object> _params)
            {
                phGridDataSeries.Controls.Clear();

                if (_params.ContainsKey("IdProcess"))
                { _params.Remove("IdProcess"); }
                _params.Add("IdProcess", _IdProcess);

                if (_params.ContainsKey("StartDate"))
                { _params.Remove("StartDate"); }
                _params.Add("StartDate", rdtFrom.SelectedDate);

                if (_params.ContainsKey("EndDate"))
                { _params.Remove("EndDate"); }
                _params.Add("EndDate", rdtThrough.SelectedDate);

                BuildGenericDataTable(Common.ConstantsEntitiesName.PA.MeasurementDataSeries, _params);

                _RgdMasterGridDataSeries = base.BuildListViewerContent(Common.ConstantsEntitiesName.PA.MeasurementDataSeries);
                _RgdMasterGridDataSeries.Rebind();
                _RgdMasterGridDataSeries.MasterTableView.Rebind();

                phGridDataSeries.Controls.Add(_RgdMasterGridDataSeries);
            }
            private void LoadGridStats(Dictionary<String, Object> _params)
            {
                phGridStats.Controls.Clear();

                if (_params.ContainsKey("IdProcess"))
                { _params.Remove("IdProcess"); }
                _params.Add("IdProcess", _IdProcess);

                if (_params.ContainsKey("StartDate"))
                { _params.Remove("StartDate"); }
                _params.Add("StartDate", rdtFrom.SelectedDate);

                if (_params.ContainsKey("EndDate"))
                { _params.Remove("EndDate"); }
                _params.Add("EndDate", rdtThrough.SelectedDate);

                BuildGenericDataTable(Common.ConstantsEntitiesName.PA.StatisticsOfMeasurements, _params);

                _RgdMasterGridStats = base.BuildListViewerContent(Common.ConstantsEntitiesName.PA.StatisticsOfMeasurements);
                _RgdMasterGridStats.Rebind();
                _RgdMasterGridStats.MasterTableView.Rebind();

                phGridStats.Controls.Add(_RgdMasterGridStats);
            }
            private void LoadGRCByEntity()
            {
                //Cuando es un Add, no debe cargar el GRC!!!
                if (!String.IsNullOrEmpty(_EntityNameGRC))
                {
                    Dictionary<String, Object> _params = new Dictionary<String, Object>();
                    _params.Add("IdProcess", _IdProcess);

                    if (BuildContextInfoModuleMenu(_EntityNameGRC, _params))
                    {
                        base.BuildContextInfoShowMenuButton();
                    }
                }
            }
            private void RegisterCustomMenuPanels()
            {
                List<String> _menuPanels = new List<String>();
                _menuPanels.Add(Common.Constants.ContextInformationKey);
                _menuPanels.Add(Common.Constants.ContextElementMapsKey);

                FwMasterPage.ContentNavigatorCustomMenuPanels(_menuPanels);
            }
        #endregion

        #region Page Events
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
                Int64 _idIndicator = 0;
                if (GetKeyValue(_RtvIndicator.SelectedNode.Value, "IdIndicator") != null)
                {
                    _idIndicator = Convert.ToInt64(GetKeyValue(_RtvIndicator.SelectedNode.Value, "IdIndicator"));
                }

                if (ManageEntityParams.ContainsKey("StartDate"))
                { ManageEntityParams.Remove("StartDate"); }
                ManageEntityParams.Add("StartDate", rdtFrom.SelectedDate);

                if (ManageEntityParams.ContainsKey("EndDate"))
                { ManageEntityParams.Remove("EndDate"); }
                ManageEntityParams.Add("EndDate", rdtThrough.SelectedDate);

                if (ManageEntityParams.ContainsKey("IdIndicator"))
                { ManageEntityParams.Remove("IdIndicator"); }
                ManageEntityParams.Add("IdIndicator", _idIndicator);

                if (ManageEntityParams.ContainsKey("IdProcess"))
                { ManageEntityParams.Remove("IdProcess"); }
                ManageEntityParams.Add("IdProcess", _IdProcess);

                //Carga la Grilla
                LoadGridFacilities();
                LoadChartIndicatorTracker();
            }
            protected void RgdMasterGridFacilities_SelectedIndexChanged(object sender, EventArgs e)
            {
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                String _pkValues = BuildParamsFromListManageSelected(_RgdMasterGridFacilities);
                //Se guarda todos los parametros que estan en la seleccion de la grilla
                foreach (KeyValuePair<String, Object> _item in GetKeyValues(_pkValues))
                {
                    if (!_params.ContainsKey(_item.Key))
                    {
                        _params.Add(_item.Key, _item.Value);
                    }
                }

                LoadGridDataSeries(_params);
                LoadGridStats(_params);
            }
            //protected void lnkExportGridMeasurement_Click(object sender, EventArgs e)
            //{
            //    Dictionary<String, Object> _params = new Dictionary<String, Object>();
            //    String _pkValues = BuildParamsFromListManageSelected(_RgdMasterGridFacilities);
            //    //Se guarda todos los parametros que estan en la seleccion de la grilla
            //    foreach (KeyValuePair<String, Object> _item in GetKeyValues(_pkValues))
            //    {
            //        if (!_params.ContainsKey(_item.Key))
            //        {
            //            _params.Add(_item.Key, _item.Value);
            //        }
            //    }

            //    LoadGridDataSeries(_params);

            //    _RgdMasterGridDataSeries.ExportSettings.ExportOnlyData = true;
            //    _RgdMasterGridDataSeries.ExportSettings.IgnorePaging = true;
            //    _RgdMasterGridDataSeries.ExportSettings.OpenInNewWindow = true;

            //    ExportarExcel();

            //    //_RgdMasterGridDataSeries.Rebind();

            //    //_RgdMasterGridDataSeries.ExportSettings.ExportOnlyData = true;
            //    //_RgdMasterGridDataSeries.ExportSettings.IgnorePaging = true;
            //    //_RgdMasterGridDataSeries.ExportSettings.OpenInNewWindow = true;

            //    //_RgdMasterGridDataSeries.MasterTableView.ExportToExcel();

            //}
        //protected void chartIndicatorTracker_ItemDataBound(object sender, ChartItemDataBoundEventArgs e)
            //{
            //    //Obtiene el activity y el value de los datos que vienen
            //    String _yearName = e.SeriesItem.Parent.Name;
            //    String _value = Common.Functions.CustomEMSRound((Decimal)e.SeriesItem.YValue);
            //    e.SeriesItem.Parent.Type = ChartSeriesType.Spline;
            //    //Setea en la leyenda y el tooltip con los datos.
            //    e.SeriesItem.ActiveRegion.Tooltip = _yearName + " = " + _value;
            //    e.SeriesItem.Name = _yearName;
            //}
            //protected void charts_DataBound(object sender, EventArgs e)
            //{
            //    HideLabels((RadChart)sender);
            //}
        #endregion

    }
}
