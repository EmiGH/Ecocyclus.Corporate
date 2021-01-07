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


namespace Condesus.EMS.WebUI
{
    public partial class ReportFacilityAnalyzer : BaseProperties
    {
        #region Internal Properties
            private Int64 _IdProcess
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdProcess") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdProcess")) : Convert.ToInt64(GetPKfromNavigator("IdProcess"));
                }
            }
            private RadComboBox _RdcSite;
            private RadTreeView _RtvSite;
            private RadGrid _RgdMasterGridIndicators;
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

                AddComboSites();

                if (!Page.IsPostBack)
                {
                    //Form
                    base.SetContentTableRowsCss(tblContentForm);
                }

                Int64 _idFacility = 0;
                if (_RtvSite.SelectedNode != null)
                {
                    if (GetKeyValue(_RtvSite.SelectedNode.Value, "IdFacility") != null)
                    {
                        _idFacility = Convert.ToInt64(GetKeyValue(_RtvSite.SelectedNode.Value, "IdFacility"));
                        if (ManageEntityParams.ContainsKey("StartDate"))
                        { ManageEntityParams.Remove("StartDate"); }
                        ManageEntityParams.Add("StartDate", rdtFrom.SelectedDate);
                        if (ManageEntityParams.ContainsKey("EndDate"))
                        { ManageEntityParams.Remove("EndDate"); }
                        ManageEntityParams.Add("EndDate", rdtThrough.SelectedDate);
                        if (ManageEntityParams.ContainsKey("IdFacility"))
                        { ManageEntityParams.Remove("IdFacility"); }
                        ManageEntityParams.Add("IdFacility", _idFacility);
                        if (ManageEntityParams.ContainsKey("IdProcess"))
                        { ManageEntityParams.Remove("IdProcess"); }
                        ManageEntityParams.Add("IdProcess", _IdProcess);

                        //Carga la Grilla
                        LoadGridIndicators();
                    }
                }
            }
            protected override void SetPagetitle()
            {
                try
                {
                    base.PageTitle = Resources.CommonListManage.FacilityAnalyzer;
                }
                catch { base.PageTitle = String.Empty; }
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }
        #endregion

        #region Private Methods
            private void AddComboSites()
            {
                String _filterExpression = String.Empty;
                //Combo de Organizaciones
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                AddComboTreeSites(ref phSite, ref _RdcSite, ref _RtvSite, new RadTreeViewEventHandler(rtvSite_NodeExpand));
                _RtvSite.NodeClick += new RadTreeViewEventHandler(rtvSite_NodeClick);
            }
            private void LoadTextLabels()
            {
                lblFrom.Text = Resources.CommonListManage.From;
                lblThrough.Text = Resources.CommonListManage.Through;
                lblSite.Text = Resources.CommonListManage.Site;
                lblSubTitle.Text = Resources.Common.RelatedData;
            }
            private void LoadGridIndicators()
            {
                phGridIndicators.Controls.Clear();

                //Setea los datos en el DataTable de base, para que luego se carge la grilla.
                BuildGenericDataTable(Common.ConstantsEntitiesName.RG.ReportFacilityAnalyzer, ManageEntityParams);

                _RgdMasterGridIndicators = base.BuildListViewerContent(Common.ConstantsEntitiesName.RG.ReportFacilityAnalyzer);
                _RgdMasterGridIndicators.SelectedIndexChanged += new EventHandler(RgdMasterGridIndicators_SelectedIndexChanged);
                _RgdMasterGridIndicators.PageSize = 10;
                _RgdMasterGridIndicators.ClientSettings.EnablePostBackOnRowClick = true;
                _RgdMasterGridIndicators.ClientSettings.Selecting.AllowRowSelect = true;

                phGridIndicators.Controls.Add(_RgdMasterGridIndicators);
            }
            private void AddChartSeries(RadChart rdchartFacilityAnalyzer, String measurementName, List<MeasurementPoint> measurementPoints)
            {
                // Define chart series
                ChartSeries chartSeries = new ChartSeries();
                chartSeries.Appearance.LabelAppearance.Visible = false;
                chartSeries.Name = measurementName;
                chartSeries.Type = ChartSeriesType.Line;
                ChartSeriesItem _item = new ChartSeriesItem();

                rdchartFacilityAnalyzer.PlotArea.XAxis.Clear();
                if (measurementPoints.Count() > 0)
                {
                    Int32 _interval = (Int32)(measurementPoints.Count() / 22);
                    _interval = (_interval == 0) ? 1 : _interval;
                    rdchartFacilityAnalyzer.PlotArea.XAxis.MaxValue = _interval;
                    rdchartFacilityAnalyzer.PlotArea.XAxis.MinValue = _interval;
                    rdchartFacilityAnalyzer.PlotArea.XAxis.Step = _interval;
                    rdchartFacilityAnalyzer.PlotArea.XAxis.LabelStep = _interval;
                }

                foreach (MeasurementPoint _point in measurementPoints)
                {
                    _item = new ChartSeriesItem();
                    _item.YValue = _point.MeasureValue;
                    rdchartFacilityAnalyzer.PlotArea.XAxis.AddItem(_point.MeasureDate.ToShortDateString());

                    chartSeries.Items.Add(_item);
                }
                // visually enhance the datapoints
                chartSeries.Appearance.PointMark.Dimensions.AutoSize = false;
                chartSeries.Appearance.PointMark.Dimensions.Width = 6;
                chartSeries.Appearance.PointMark.Dimensions.Height = 6;
                chartSeries.Appearance.PointMark.FillStyle.MainColor = System.Drawing.Color.Black;
                chartSeries.Appearance.PointMark.Visible = true;

                rdchartFacilityAnalyzer.Series.Add(chartSeries);
            }
            private void LoadMeasurementSeries(Int64 idMeasurement)
            {
                //DataTable _dt = DataTableListManage[Common.ConstantsEntitiesName.RG.ReportFacilityAnalyzer];
                DateTime? _startDate = null;
                DateTime? _endDate = null;

                _startDate = rdtFrom.SelectedDate;
                _endDate = rdtThrough.SelectedDate;

                //Ya tengo la medicion
                Measurement _measurement = EMSLibrary.User.PerformanceAssessments.Configuration.Measurement(idMeasurement);
                //Ahora por cada medicion, armamos una Serie en el Chart....
                AddChartSeries(chartFacilityAnalyzer, _measurement.LanguageOption.Name, _measurement.Series(_startDate, _endDate));
            }
            private void LoadchartFacilityAnalyzer(Int64 idMeasurement)
            {
                chartFacilityAnalyzer.Clear();

                //Seteo la cultura en USA, para poder trabajar con la fecha en MM/DD/YYYY HH:mm:ss
                CultureInfo _cultureUSA = new CultureInfo("en-US");
                //Me guarda la actual, para luego volver a esta...
                CultureInfo _currentCulture = CultureInfo.CurrentCulture;
                //Seta la cultura estandard
                Thread.CurrentThread.CurrentCulture = _cultureUSA;

                //Setea y arma una paleta de colores para las referencias
                SetChartCustomPalette(chartFacilityAnalyzer);

                // Define chart and titleRadChart radChart = new RadChart();
                chartFacilityAnalyzer.ChartTitle.TextBlock.Text = Resources.CommonListManage.FacilityAnalyzer;
                chartFacilityAnalyzer.ChartTitle.TextBlock.Appearance.TextProperties.Color = System.Drawing.Color.Blue;

                LoadMeasurementSeries(idMeasurement);

                // set the plot area gradient background fill
                chartFacilityAnalyzer.PlotArea.Appearance.FillStyle.FillType = Telerik.Charting.Styles.FillType.Gradient;
                chartFacilityAnalyzer.PlotArea.XAxis.Appearance.Width = 3;
                // Set text and line for Y axis
                chartFacilityAnalyzer.PlotArea.YAxis.AxisLabel.TextBlock.Text = "";
                chartFacilityAnalyzer.PlotArea.YAxis.Appearance.Width = 3;
                chartFacilityAnalyzer.PlotArea.XAxis.Appearance.ValueFormat = Telerik.Charting.Styles.ChartValueFormat.LongDate;
                chartFacilityAnalyzer.PlotArea.XAxis.Appearance.CustomFormat = "dd/MM/yyyy HH:mm:ss"; 

                //Vuelve a la cultura original...
                Thread.CurrentThread.CurrentCulture = _currentCulture;
            }
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
            private void LoadGridDataSeries(Dictionary<String, Object> _params)
            {
                phGridDataSeries.Controls.Clear();
                _RgdMasterGridDataSeries = null;

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
            protected void rtvSite_NodeExpand(object sender, RadTreeNodeEventArgs e)
            {
                NodeExpandSites(sender, e, true);
            }
            protected void rtvSite_NodeClick(object sender, RadTreeNodeEventArgs e)
            {
                Int64 _idFacility = 0;
                if (_RtvSite.SelectedNode.Value.Contains("IdSector"))
                {
                    if (GetKeyValue(_RtvSite.SelectedNode.Value, "IdSector") != null)
                    {
                        _idFacility = Convert.ToInt64(GetKeyValue(_RtvSite.SelectedNode.Value, "IdSector"));
                    }
                }
                else
                {
                    if (GetKeyValue(_RtvSite.SelectedNode.Value, "IdFacility") != null)
                    {
                        _idFacility = Convert.ToInt64(GetKeyValue(_RtvSite.SelectedNode.Value, "IdFacility"));
                    }
                }

                if (ManageEntityParams.ContainsKey("StartDate"))
                { ManageEntityParams.Remove("StartDate"); }
                ManageEntityParams.Add("StartDate", rdtFrom.SelectedDate);

                if (ManageEntityParams.ContainsKey("EndDate"))
                { ManageEntityParams.Remove("EndDate"); }
                ManageEntityParams.Add("EndDate", rdtThrough.SelectedDate);

                if (ManageEntityParams.ContainsKey("IdFacility"))
                { ManageEntityParams.Remove("IdFacility"); }
                ManageEntityParams.Add("IdFacility", _idFacility);

                if (ManageEntityParams.ContainsKey("IdProcess"))
                { ManageEntityParams.Remove("IdProcess"); }
                ManageEntityParams.Add("IdProcess", _IdProcess);

                //Carga la Grilla
                LoadGridIndicators();
            }
            protected void RgdMasterGridIndicators_SelectedIndexChanged(object sender, EventArgs e)
            {
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                String _pkValues = BuildParamsFromListManageSelected(_RgdMasterGridIndicators);
                //Se guarda todos los parametros que estan en la seleccion de la grilla
                foreach (KeyValuePair<String, Object> _item in GetKeyValues(_pkValues))
                {
                    if (!_params.ContainsKey(_item.Key))
                    {
                        _params.Add(_item.Key, _item.Value);
                    }
                }

                LoadchartFacilityAnalyzer(Convert.ToInt64(_params["IdMeasurement"]));
                LoadGridDataSeries(_params);
                LoadGridStats(_params);
            }
        #endregion

    }
}
