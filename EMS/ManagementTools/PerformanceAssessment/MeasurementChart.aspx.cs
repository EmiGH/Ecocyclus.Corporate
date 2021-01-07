using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Drawing;
using Dundas.Charting.WebControl;
using Telerik.Web.UI;
using CEB_PA = Condesus.EMS.Business.PA;
using Condesus.EMS.Business.PA.Entities;

namespace Condesus.EMS.WebUI.ManagementTools.PerformanceAssessment
{
    public partial class MeasurementChart : BaseProperties
    {
        #region Private Properties
            private Condesus.EMS.Business.PA.Entities.MeasurementStatistics _Stats;
            private enum _AggregateTypes { None, Avg, StdDev, StdDevP, Sum, Cummulative, Var, VarP }
            private enum _GroupingTypes { None, Hour, Day, Month, Year }
            private Dundas.Charting.WebControl.Series _ChartSeries
            {
                get { return chtIndicator.Series["Data"]; }
            }
            private Dundas.Charting.WebControl.Series _ChartSeriesAvg
            {
                get { return chtIndicator.Series["Average"]; }
            }
            private Int64 _IdMeasurement
            {
                get
                {
                    object o = ViewState["idMeasurement"];
                    if (o != null) {
                        return Convert.ToInt64(ViewState["idMeasurement"]);
                    }
                    else
                    {
                        _IdMeasurement = Convert.ToInt64(Request.QueryString["idMeasurement"]);
                        return Convert.ToInt64(Request.QueryString["idMeasurement"]);
                    }
                }
                set { ViewState["idMeasurement"] = value; }
            }
            private String _ChartType
            {
                get { return ViewState["ChartType"].ToString(); }
                set { ViewState["ChartType"] = value; }
            }
            private int _TimeOut;
            public RadGrid rgdMasterGrid;
            private RadComboBox _RdcParameterGroup;
        #endregion

        #region Page Events
            private void InitializeHandlers()
            {
                imgBtnRefreshChart.Click += new ImageClickEventHandler(imgBtnRefreshChart_Click);
                imgBtnResetChart.Click += new ImageClickEventHandler(imgBtnResetChart_Click);
            }
            protected override void OnInit(EventArgs e)
            {
                base.OnInit(e);
                LoadTextLabels();

                _TimeOut = Server.ScriptTimeout;
                Server.ScriptTimeout = 3600;

                InitializeHandlers();

                InitializeGrid();
                AddComboParameterGroups();

                lnkExport.OnClientClick = "return ExportMeasurementSeries(this, " + _IdMeasurement + ");";
            }
            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);

                base.SetContentTableRowsCss(tblContentForm2);
                if (!IsPostBack)
                {
                    this.Title = "EMS - " + Resources.CommonListManage.Indicator;

                    //Load Types
                    LoadAggregateType();
                    LoadGroupingType();

                    //Setea el measurement
                    //Deberia Recibir el Process Id del Request...
                    Condesus.EMS.Business.PA.Entities.Measurement _measurement = EMSLibrary.User.PerformanceAssessments.Configuration.Measurement(_IdMeasurement);
                    
                    LoadIndicatorData(_measurement);
                    DefaultChart(_measurement);
                    ShowAverageSerie(false);

                }
                Server.ScriptTimeout = _TimeOut;
            }
        #endregion

        #region Private Methods
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonListManage.MeasurementChart;
                imgBtnRefreshChart.AlternateText = Resources.CommonListManage.RefreshChart;
                imgBtnResetChart.AlternateText = Resources.CommonListManage.ResetChart;
                lblTitle.Text = Resources.CommonListManage.MeasurementChart;
                lblDetail.Text = Resources.CommonListManage.MeasurementChartSubTitle;
                lblAggregate.Text = Resources.CommonListManage.Aggregate;
                lblParameterGroup.Text = Resources.CommonListManage.ParameterGroup;
                lblAVG.Text = Resources.CommonListManage.Average;
                lblCOUNT.Text = Resources.CommonListManage.Count;
                lblDeviceLabel.Text = Resources.CommonListManage.Device;
                lblEndDate.Text = Resources.CommonListManage.EndDate;
                lblFIRST.Text = Resources.CommonListManage.FirstValue;
                lblGrouping.Text = Resources.CommonListManage.Grouping;
                lblIndicatorLabel.Text = Resources.CommonListManage.Indicator;
                lblLAST.Text = Resources.CommonListManage.LastValue;
                lblMAX.Text = Resources.CommonListManage.MaxValue;
                lblMeasurementLabel.Text = Resources.CommonListManage.Measurement;
                lblMeasurementUnit.Text = Resources.CommonListManage.MeasurementUnit;
                lblMIN.Text = Resources.CommonListManage.MinValue;
                lblPeriod.Text = Resources.CommonListManage.Period;
                lblProject.Text = Resources.CommonListManage.Project;
                lblShowAverage.Text = Resources.CommonListManage.ShowAverage;
                lblShowBands.Text = Resources.CommonListManage.ShowBands;
                lblShowFirstLast.Text = Resources.CommonListManage.ShowFirstLast;
                lblShowMaxMin.Text = Resources.CommonListManage.ShowMaxMin;
                lblStartDate.Text = Resources.CommonListManage.StartDate;
                lblSTDDEV.Text = Resources.CommonListManage.StdDevValue;
                lblSTDDEVP.Text = Resources.CommonListManage.StdDevPValue;
                lblSUM.Text = Resources.CommonListManage.Sum;
                lblVAR.Text = Resources.CommonListManage.VarValue;
                lblVARP.Text = Resources.CommonListManage.VarPValue;
            }

            #region Load Data
                private Condesus.EMS.Business.PF.Entities.ProcessGroupProcess SeekRoot(Condesus.EMS.Business.PF.Entities.Process node)
                {
                    switch (node.GetType().FullName)
                    {
                        case "Condesus.EMS.Business.PF.Entities.ProcessGroupProcess":
                            return (Condesus.EMS.Business.PF.Entities.ProcessGroupProcess)node;

                        //case "Condesus.EMS.Business.PF.Entities.ProcessGroupNode":
                        //    return SeekRoot(((Condesus.EMS.Business.PF.Entities.ProcessGroupNode)node).Parent);

                        //case "Condesus.EMS.Business.PF.Entities.ProcessGroupRootRootStandard":
                        //    return SeekRoot(((Business.PF.Entities.ProcessGroupRootRootStandard)node).ProcessParent);

                        default:
                            return null;
                    }
                }
                private void LoadIndicatorData(CEB_PA.Entities.Measurement measurement)
                {
                    //Condesus.EMS.Business.PF.Entities.ProcessGroupProcess _rootProcess = SeekRoot(((Condesus.EMS.Business.PF.Entities.ProcessTaskMeasurement)measurement.ProcessTask).Parent);
                    Condesus.EMS.Business.PF.Entities.ProcessGroupProcess _rootProcess = measurement.ProcessTask.Parent;

                    lblProjectValue.Text = _rootProcess.LanguageOption.Title;

                    CEB_PA.Entities.Indicator _indicator = measurement.Indicator;
                    CEB_PA.Entities.Indicator_LG _indicator_LG = _indicator.LanguageOption;
                    //CEB_PA.Entities.IndicatorClassification _indicatorClassification = _indicator.Classification;
                    CEB_PA.Entities.MeasurementDevice _measurementDevice = measurement.Device;
                    CEB_PA.Entities.MeasurementUnit _measurementUnit = measurement.MeasurementUnit;

                    lblMeasurementValue.Text = String.Concat(measurement.LanguageOption.Name);
                    lblIndicatorValue.Text = String.Concat(Common.Functions.RemoveIndexesTags(_indicator_LG.Name), " (", _indicator.Magnitud.LanguageOption.Name, ")");
                    if (_measurementDevice != null)
                    {
                        lblDeviceValue.Text = String.Concat(_measurementDevice.Brand, " ", _measurementDevice.Model, " (", _measurementDevice.SerialNumber, ")");
                    }
                    else
                    {
                        lblDeviceValue.Text = Resources.Common.NotUsed; // HttpContext.GetLocalResourceObject("/ManagementTools/PerformanceAssessment/MeasurementChart.aspx", "lblDeviceValue.Text").ToString();
                    }
                    lblMeasurementUnitValue.Text = String.Concat(_measurementUnit.Magnitud.LanguageOption.Name, " - ", _measurementUnit.LanguageOption.Name);
                }
                private void LoadStatistics()
                {
                    lblFirstDate.Text = _Stats.FirstDate.ToString();
                    lblLastDate.Text = _Stats.LastDate.ToString();
                    lblLastVal.Text = _Stats.LastValue.ToString();
                    lblFirstVal.Text = _Stats.FirstValue.ToString();
                    lblMinVal.Text = _Stats.MinValue.ToString();
                    lblMaxVal.Text = _Stats.MaxValue.ToString();
                    lblAvgVal.Text = _Stats.AvgValue.ToString();
                    lblCountVal.Text = _Stats.CountValue.ToString();
                    lblSumVal.Text = _Stats.SumValue.ToString();
                    lblStdDevVal.Text = _Stats.StdDevValue.ToString();
                    lblStdDevPVal.Text = _Stats.StdDevPValue.ToString();
                    lblVarVal.Text = _Stats.VarValue.ToString();
                    lblVarPVal.Text = _Stats.VarPValue.ToString();
                }

                #region Data Feed
                    private void LoadData()
                    {
                        Condesus.EMS.Business.PA.Entities.Measurement _measurement = EMSLibrary.User.PerformanceAssessments.Configuration.Measurement(_IdMeasurement);
                        FeedChart(_measurement.Series());
                    }
                    private void LoadData(DateTime? startDate, DateTime? endDate)
                    {
                        Condesus.EMS.Business.PA.Entities.Measurement _measurement = EMSLibrary.User.PerformanceAssessments.Configuration.Measurement(_IdMeasurement);
                        FeedChart(_measurement.Series(startDate, endDate));
                    }
                    private void LoadData(DateTime? startDate, DateTime? endDate, _AggregateTypes aggregate, _GroupingTypes grouping)
                    {
                        Condesus.EMS.Business.PA.Entities.Measurement _measurement = EMSLibrary.User.PerformanceAssessments.Configuration.Measurement(_IdMeasurement);
                        FeedChart(_measurement.Series(startDate, endDate, TranslateGrouping(grouping), TranslateAggregate(aggregate)));
                    }
                    private void FeedChart(List<Condesus.EMS.Business.PA.Entities.MeasurementPoint> series)
                    {
                        _ChartSeries.Points.Clear();
                        Int32 _interval = (Int32)(series.Count / 22);
                        _interval = (_interval == 0) ? 1 : _interval;
                        chtIndicator.ChartAreas["AreaData"].AxisX.MajorTickMark.Interval = _interval;
                        chtIndicator.ChartAreas["AreaData"].AxisX.MajorGrid.Interval = _interval;
                        chtIndicator.ChartAreas["AreaData"].AxisX.LabelStyle.Interval = _interval;
                        chtIndicator.ChartAreas["AreaData"].AxisX.LabelsAutoFit = true;

                        foreach (Condesus.EMS.Business.PA.Entities.MeasurementPoint _point in series)
                        {
                            _ChartSeries.Points.AddXY(GetX(_point.MeasureDate), Convert.ToDouble(_point.MeasureValue));
                        }
                    }
                #endregion

                #region Measurement Series
                    private void DefineColumns(GridTableView gridTableViewDetails)
                    {
                        //Add columns bound
                        GridBoundColumn boundColumn;

                        //Crea y agrega las columnas de tipo Bound
                        boundColumn = new GridBoundColumn();
                        boundColumn.DataField = "MeasureDate";
                        boundColumn.DataType = System.Type.GetType("System.DateTime");
                        boundColumn.HeaderText = "Date";
                        boundColumn.ItemStyle.Width = Unit.Pixel(150);
                        gridTableViewDetails.Columns.Add(boundColumn);

                        //Crea y agrega las columnas de tipo Bound
                        boundColumn = new GridBoundColumn();
                        boundColumn.DataField = "MeasureValue";
                        boundColumn.DataType = System.Type.GetType("System.String");
                        boundColumn.HeaderText = "Value";
                        boundColumn.ItemStyle.Width = Unit.Pixel(150);
                        gridTableViewDetails.Columns.Add(boundColumn);


                        //Crea y agrega las columnas de tipo Bound
                        boundColumn = new GridBoundColumn();
                        boundColumn.DataField = "StartDate";
                        boundColumn.DataType = System.Type.GetType("System.DateTime");
                        boundColumn.HeaderText = Resources.CommonListManage.StartDate;
                        boundColumn.ItemStyle.Width = Unit.Pixel(150);
                        gridTableViewDetails.Columns.Add(boundColumn);


                        //Crea y agrega las columnas de tipo Bound
                        boundColumn = new GridBoundColumn();
                        boundColumn.DataField = "EndDate";
                        boundColumn.DataType = System.Type.GetType("System.DateTime");
                        boundColumn.HeaderText = Resources.CommonListManage.EndDate;
                        boundColumn.ItemStyle.Width = Unit.Pixel(150);
                        gridTableViewDetails.Columns.Add(boundColumn);

                    }
                    private void InitializeGrid()
                    {
                        //Crea y setea los atributos de la grilla
                        rgdMasterGrid = new RadGrid();
                        rgdMasterGrid.ID = "rgdMasterGrid";
                        rgdMasterGrid.Skin = "EMS";
                        rgdMasterGrid.EnableEmbeddedSkins = false;
                        rgdMasterGrid.AllowPaging = true;
                        rgdMasterGrid.AllowSorting = true;
                        rgdMasterGrid.Width = Unit.Percentage(100);
                        rgdMasterGrid.AutoGenerateColumns = false;
                        rgdMasterGrid.GridLines = System.Web.UI.WebControls.GridLines.None;
                        rgdMasterGrid.ShowStatusBar = false;
                        rgdMasterGrid.PageSize = 18;
                        rgdMasterGrid.AllowMultiRowSelection = true;
                        rgdMasterGrid.PagerStyle.AlwaysVisible = true;
                        rgdMasterGrid.PagerStyle.Mode = GridPagerMode.NextPrevAndNumeric;
                        rgdMasterGrid.EnableViewState = true;

                        //Crea los metodos de la grilla (Server)
                        rgdMasterGrid.NeedDataSource += new GridNeedDataSourceEventHandler(this.rgdMasterGrid_NeedDataSource);
                        rgdMasterGrid.SortCommand += new GridSortCommandEventHandler(this.rgdMasterGrid_SortCommand);
                        rgdMasterGrid.PageIndexChanged += new GridPageChangedEventHandler(this.rgdMasterGrid_PageIndexChanged);
                        rgdMasterGrid.ItemDataBound += new GridItemEventHandler(rgdMasterGrid_ItemDataBound);

                        //Crea los metodos de la grilla (Cliente)
                        rgdMasterGrid.ClientSettings.AllowExpandCollapse = true;
                        rgdMasterGrid.ClientSettings.Selecting.AllowRowSelect = true;
                        rgdMasterGrid.ClientSettings.Selecting.EnableDragToSelectRows = false;

                        //Define los atributos de la MasterGrid
                        rgdMasterGrid.MasterTableView.Name = "gridMaster";
                        rgdMasterGrid.MasterTableView.EnableViewState = true;
                        rgdMasterGrid.MasterTableView.CellPadding = 0;
                        rgdMasterGrid.MasterTableView.CellSpacing = 0;
                        rgdMasterGrid.MasterTableView.GridLines = System.Web.UI.WebControls.GridLines.None;
                        rgdMasterGrid.MasterTableView.GroupsDefaultExpanded = false;
                        rgdMasterGrid.MasterTableView.AllowMultiColumnSorting = false;
                        rgdMasterGrid.MasterTableView.HierarchyLoadMode = GridChildLoadMode.Client;
                        rgdMasterGrid.MasterTableView.ExpandCollapseColumn.Resizable = false;
                        rgdMasterGrid.MasterTableView.ExpandCollapseColumn.HeaderStyle.Width = Unit.Pixel(20);
                        rgdMasterGrid.MasterTableView.RowIndicatorColumn.Visible = false;
                        rgdMasterGrid.MasterTableView.RowIndicatorColumn.HeaderStyle.Width = Unit.Pixel(20);

                        rgdMasterGrid.HeaderStyle.Font.Bold = false;
                        rgdMasterGrid.HeaderStyle.Font.Italic = false;
                        rgdMasterGrid.HeaderStyle.Font.Overline = false;
                        rgdMasterGrid.HeaderStyle.Font.Strikeout = false;
                        rgdMasterGrid.HeaderStyle.Font.Underline = false;
                        rgdMasterGrid.HeaderStyle.Wrap = true;
                        //Seteos para la paginacion de la grilla, ahora es culturizable.
                        rgdMasterGrid.PagerStyle.Mode = GridPagerMode.NextPrevAndNumeric;
                        rgdMasterGrid.MasterTableView.PagerStyle.Mode = GridPagerMode.NextPrevAndNumeric;

                        rgdMasterGrid.MasterTableView.PagerStyle.NextPagesToolTip = Resources.Common.GridPagerStyleNextPagesToolTip;    // "Next Pages";
                        rgdMasterGrid.MasterTableView.PagerStyle.NextPageText = Resources.Common.GridPagerStyleNextPageText;    // "&gt;";
                        rgdMasterGrid.MasterTableView.PagerStyle.NextPageToolTip = Resources.Common.GridPagerStyleNextPageToolTip;  // "Next Page";
                        rgdMasterGrid.MasterTableView.PagerStyle.PagerTextFormat = Resources.Common.GridPagerStylePagerTextFormat;  // "Change page: {4} &nbsp;Displaying page {0} of {1}, items {2} to {3} of {5}.";
                        rgdMasterGrid.MasterTableView.PagerStyle.PrevPagesToolTip = Resources.Common.GridPagerStylePrevPagesToolTip;    // "Previous Pages";
                        rgdMasterGrid.MasterTableView.PagerStyle.PrevPageText = Resources.Common.GrdiPagerStylePrevPageText;    // "&lt;";
                        rgdMasterGrid.MasterTableView.PagerStyle.PrevPageToolTip = Resources.Common.GridPagerStylePrevPageToolTip;  // "Previous Page";

                        rgdMasterGrid.PagerStyle.NextPagesToolTip = Resources.Common.GridPagerStyleNextPagesToolTip; //"Next Pages";
                        rgdMasterGrid.PagerStyle.NextPageText = Resources.Common.GridPagerStyleNextPageText;    //"&gt;";
                        rgdMasterGrid.PagerStyle.NextPageToolTip = Resources.Common.GridPagerStyleNextPageToolTip;  // "Next Page";
                        rgdMasterGrid.PagerStyle.PagerTextFormat = Resources.Common.GridPagerStylePagerTextFormat;  // "Change page: {4} &nbsp;Displaying page {0} of {1}, items {2} to {3} of {5}.";
                        rgdMasterGrid.PagerStyle.PrevPagesToolTip = Resources.Common.GridPagerStylePrevPagesToolTip;    // "Previous Pages";
                        rgdMasterGrid.PagerStyle.PrevPageText = Resources.Common.GrdiPagerStylePrevPageText;    // "&lt;";
                        rgdMasterGrid.PagerStyle.PrevPageToolTip = Resources.Common.GridPagerStylePrevPageToolTip;  // "Previous Page";


                        //Crea las columnas para la MasterGrid.
                        DefineColumns(rgdMasterGrid.MasterTableView);
                        //Agrega toda la grilla dentro del panle que ya esta en el html.
                        pchControls.Controls.Add(rgdMasterGrid);
                    }
                    private DataTable ReturnDataGrid()
                    {
                        //Crea el DataTable para luego devolverlo y asociarlo a la grilla.
                        DataTable _dt = new DataTable();
                        _dt.TableName = "Root";
                        _dt.Columns.Add("MeasureDate", System.Type.GetType("System.DateTime"));
                        _dt.Columns.Add("MeasureValue", System.Type.GetType("System.String"));
                        _dt.Columns.Add("StartDate", System.Type.GetType("System.DateTime"));
                        _dt.Columns.Add("EndDate", System.Type.GetType("System.DateTime"));
                        _dt.Columns.Add("Status", System.Type.GetType("System.String"));

                        Measurement _measurement = EMSLibrary.User.PerformanceAssessments.Configuration.Measurement(_IdMeasurement);
                        List<Condesus.EMS.Business.PA.Entities.MeasurementPoint> _measurementPoints = _measurement.Series();
                        foreach (Condesus.EMS.Business.PA.Entities.MeasurementPoint _measurementPoint in _measurementPoints)
                        {
                            _dt.Rows.Add(_measurementPoint.MeasureDate,
                                Common.Functions.CustomEMSRound(_measurementPoint.MeasureValue),
                                _measurementPoint.StartDate,
                                _measurementPoint.EndDate,
                                _measurementPoint.Sing);
                        }

                        //Setea el nombre de la entidad que se selecciona para ver la serie de datos
                        lblEntitySeries.Text = Resources.CommonListManage.DataSeriesOf + " " + Common.Functions.ReplaceIndexesTags(_measurement.LanguageOption.Name);

                        return _dt;
                    }
                #endregion

            #endregion

            #region Combos
                private void LoadAggregateType()
                {
                    radddlAggregate.Items.Add(new Telerik.Web.UI.RadComboBoxItem(_AggregateTypes.None.ToString()));
                    radddlAggregate.Items.Add(new Telerik.Web.UI.RadComboBoxItem(_AggregateTypes.Avg.ToString()));
                    radddlAggregate.Items.Add(new Telerik.Web.UI.RadComboBoxItem(_AggregateTypes.StdDev.ToString()));
                    radddlAggregate.Items.Add(new Telerik.Web.UI.RadComboBoxItem(_AggregateTypes.StdDevP.ToString()));
                    radddlAggregate.Items.Add(new Telerik.Web.UI.RadComboBoxItem(_AggregateTypes.Sum.ToString()));
                    radddlAggregate.Items.Add(new Telerik.Web.UI.RadComboBoxItem(_AggregateTypes.Cummulative.ToString()));
                    radddlAggregate.Items.Add(new Telerik.Web.UI.RadComboBoxItem(_AggregateTypes.Var.ToString()));
                    radddlAggregate.Items.Add(new Telerik.Web.UI.RadComboBoxItem(_AggregateTypes.VarP.ToString()));
                }
                private void LoadGroupingType()
                {
                    radddlGrouping.Items.Add(new Telerik.Web.UI.RadComboBoxItem(_GroupingTypes.None.ToString()));
                    radddlGrouping.Items.Add(new Telerik.Web.UI.RadComboBoxItem(_GroupingTypes.Hour.ToString()));
                    radddlGrouping.Items.Add(new Telerik.Web.UI.RadComboBoxItem(_GroupingTypes.Day.ToString()));
                    radddlGrouping.Items.Add(new Telerik.Web.UI.RadComboBoxItem(_GroupingTypes.Month.ToString()));
                    radddlGrouping.Items.Add(new Telerik.Web.UI.RadComboBoxItem(_GroupingTypes.Year.ToString()));
                }
            #endregion
           
            #region Filters
                private void ShowAverageSerie(Boolean show)
                {
                    if (show)
                    {
                        if (_ChartSeriesAvg.Points.Count > 0)
                        {
                            _ChartSeriesAvg.Enabled = show;
                        }
                        else
                        {
                            for (int i = 0; i < _ChartSeries.Points.Count; i++)
                            {
                                _ChartSeriesAvg.Points.AddXY(_ChartSeries.Points[i].XValue, Convert.ToDouble(lblAvgVal.Text));
                            }
                        }
                        _ChartSeriesAvg.Color = Color.Green;
                        _ChartSeriesAvg.Type = SeriesChartType.Line;

                    }
                    else
                    {
                        _ChartSeriesAvg.Enabled = show;
                    }
                }
                private void ShowFirstLastValues(Boolean show)
                {
                    if (show)
                    {
                        DataPointCollection _points = chtIndicator.Series["Data"].Points;
                        DataPoint _firstValuePoint = _points[0];
                        _firstValuePoint.Color = Color.Black;
                        _firstValuePoint.ShowLabelAsValue = true;
                        _firstValuePoint.ShowInLegend = true;
                        _firstValuePoint.MarkerStyle = MarkerStyle.Square;
                        _firstValuePoint.LegendText = _firstValuePoint.Name;

                        DataPoint _lastValuePoint = _points[_points.Count-1];
                        _lastValuePoint.Color = Color.Black;
                        _lastValuePoint.ShowLabelAsValue = true;
                        _lastValuePoint.ShowInLegend = true;
                        _lastValuePoint.MarkerStyle = MarkerStyle.Square;
                        _lastValuePoint.LegendText = _lastValuePoint.Name;
                    }
                }
                private void ShowMaxMinValues(Boolean show)
                {
                    if (show)
                    {
                        DataPoint _maxValuePoint = chtIndicator.Series["Data"].Points.FindMaxValue();
                        _maxValuePoint.Color = Color.Black;
                        _maxValuePoint.ShowLabelAsValue = true;
                        _maxValuePoint.ShowInLegend = true;
                        _maxValuePoint.MarkerStyle = MarkerStyle.Triangle;
                        _maxValuePoint.LegendText = _maxValuePoint.Name;

                        DataPoint _minValuePoint = chtIndicator.Series["Data"].Points.FindMinValue();
                        _minValuePoint.Color = Color.Black;
                        _minValuePoint.ShowLabelAsValue = true;
                        _minValuePoint.ShowInLegend = true;
                        _minValuePoint.MarkerStyle = MarkerStyle.Triangle;
                        _minValuePoint.LegendText = _minValuePoint.Name;
                    }
                }
                private void ShowBands(Boolean show, Condesus.EMS.Business.PA.Entities.Measurement _measurement, ParameterGroup _parameterGroup)
                {
                    if (show)
                    {
                        chtIndicator.ChartAreas["AreaData"].AxisY.StripLines.Clear();
                        chtIndicator.ChartAreas["AreaData"].AxisY.CustomLabels.Clear();

                        foreach (ParameterGroup _item in _measurement.ParameterGroups.Values)
                        {
                            if (_item.IdParameterGroup == _parameterGroup.IdParameterGroup)
                            {
                                foreach (Condesus.EMS.Business.PA.Entities.Parameter _parameter in _item.Parameters.Values)
                                {
                                    String _name = _parameter.LanguageOption.Description;

                                    foreach (Condesus.EMS.Business.PA.Entities.ParameterRange _parameterRange in _parameter.ParameterRanges.Values)
                                    {
                                        SetBand(_name, Convert.ToDouble(_parameterRange.LowValue), Convert.ToDouble(_parameterRange.HighValue), _parameter.Sign);
                                    }
                                }
                            }
                        }
                        //ORIGINAL!!!
                        //foreach (Condesus.EMS.Business.PA.Entities.Parameter _parameter in _measurement.ParameterGroup.Parameters.Values)
                        //{
                        //    String _name = _parameter.LanguageOption.Description;

                        //    foreach (Condesus.EMS.Business.PA.Entities.ParameterRange _parameterRange in _parameter.ParameterRanges.Values)
                        //    {
                        //        SetBand(_name, Convert.ToDouble(_parameterRange.LowValue), Convert.ToDouble(_parameterRange.HighValue), _parameter.Sign);
                        //    }
                        //}
                    }
                    else
                    {
                        chtIndicator.ChartAreas["AreaData"].AxisY.StripLines.Clear();
                        chtIndicator.ChartAreas["AreaData"].AxisY.CustomLabels.Clear();
                    }
                }
                private void SetBand(String range, Double min, Double max, String sign)
                {
                    // Create strip lines which cover the areas with filtered values
                    StripLine stripLine = new StripLine();

                    stripLine.IntervalOffset = min;
                    stripLine.StripWidth = Math.Abs(max - min);
                    String _minMaxRange = " (" + min.ToString() + " - " + max.ToString() + ')';

                    // Set X axis custom labels
                    int element;

                    switch (sign)
                    {
                        case "+":
                            stripLine.BackColor = Color.FromArgb(64, Color.Green);
                            chtIndicator.ChartAreas["AreaData"].AxisY.StripLines.Add(stripLine);
                            element = chtIndicator.ChartAreas["AreaData"].AxisY.CustomLabels.Add(min, max, range + _minMaxRange);
                            break;
                        case "=":
                            stripLine.BackColor = Color.FromArgb(64, Color.Yellow);
                            chtIndicator.ChartAreas["AreaData"].AxisY.StripLines.Add(stripLine);
                            element = chtIndicator.ChartAreas["AreaData"].AxisY.CustomLabels.Add(min, max, range + _minMaxRange);
                            break;
                        case "-":
                            stripLine.BackColor = Color.FromArgb(64, Color.Red);
                            chtIndicator.ChartAreas["AreaData"].AxisY.StripLines.Add(stripLine);
                            element = chtIndicator.ChartAreas["AreaData"].AxisY.CustomLabels.Add(min, max, range + _minMaxRange);
                            break;
                        default:
                            break;
                    }
                }
            #endregion

            private void RefreshChart(Condesus.EMS.Business.PA.Entities.Measurement measurement)
            {
                _ChartSeries.Color = Color.Blue;

                //Set date options
                DateTime? _startDate;
                _startDate = radcalStartDate.SelectedDate;

                DateTime? _endDate;
                _endDate = radcalEndDate.SelectedDate;

                //Set aggregate options
                _GroupingTypes _grouping = GetGroupingType(radddlGrouping.SelectedItem.Text);
                _AggregateTypes _aggregate = GetAggregateType(radddlAggregate.SelectedItem.Text);

                _Stats = measurement.Statistics(_startDate, _endDate);

                if (_aggregate == _AggregateTypes.None || _grouping == _GroupingTypes.None)
                    LoadData(_startDate, _endDate);
                else
                    LoadData(_startDate, _endDate, _aggregate, _grouping);
            }
            private void DefaultChart(Condesus.EMS.Business.PA.Entities.Measurement measurement)
            {
                _ChartSeries.Color = Color.Blue;
                _ChartSeries.Type = SeriesChartType.Line;

                radcalStartDate.SelectedDate = null;
                radcalEndDate.SelectedDate = null;

                //Combo en day si frecuencia es menor a dia
                if (measurement.TimeUnitFrequency > 3)
                {
                    radddlGrouping.SelectedIndex = 2;
                    radddlAggregate.SelectedIndex = 1;
                }
                else
                {
                    radddlGrouping.SelectedIndex = -1;
                    radddlAggregate.SelectedIndex = -1;
                }

                RefreshChart(measurement);

                //Statistics
                LoadStatistics();
            }

            private String GetX(DateTime xValue) 
            {
                switch (GetGroupingType(radddlGrouping.SelectedItem.Text))
                {
                    case _GroupingTypes.None:
                        return xValue.ToString();
                    case _GroupingTypes.Hour:
                        return xValue.Hour.ToString() + " " + xValue.ToShortDateString();
                    case _GroupingTypes.Day:
                        return xValue.ToShortDateString();
                    case _GroupingTypes.Month:
                        return xValue.Month.ToString() + "/" + xValue.Year.ToString();
                    case _GroupingTypes.Year:
                        return xValue.Year.ToString();
                    default:
                        return xValue.ToString();
                }
            }

            private _AggregateTypes GetAggregateType(String aggregate)
            {
                try
                {
                    return (_AggregateTypes)Enum.Parse(typeof(_AggregateTypes), aggregate);
                }
                catch
                {
                    return _AggregateTypes.None;
                }

                #region Old
                if (aggregate == _AggregateTypes.None.ToString()) return _AggregateTypes.None;
                if (aggregate == _AggregateTypes.Avg.ToString()) return _AggregateTypes.Avg;
                if (aggregate == _AggregateTypes.StdDev.ToString()) return _AggregateTypes.StdDev;
                if (aggregate == _AggregateTypes.StdDevP.ToString()) return _AggregateTypes.StdDevP;
                if (aggregate == _AggregateTypes.Sum.ToString()) return _AggregateTypes.Sum;
                if (aggregate == _AggregateTypes.Var.ToString()) return _AggregateTypes.Var;
                if (aggregate == _AggregateTypes.VarP.ToString()) return _AggregateTypes.VarP;

                return _AggregateTypes.Avg;
                #endregion
            }
            private _GroupingTypes GetGroupingType(String grouping)
            {
                try
                {
                    return (_GroupingTypes)Enum.Parse(typeof(_GroupingTypes), grouping);
                }
                catch
                {
                    return _GroupingTypes.None;
                }

                #region Old
                if (grouping == _GroupingTypes.None.ToString()) return _GroupingTypes.None;
                if (grouping == _GroupingTypes.Day.ToString()) return _GroupingTypes.Day;
                if (grouping == _GroupingTypes.Month.ToString()) return _GroupingTypes.Month;
                if (grouping == _GroupingTypes.Year.ToString()) return _GroupingTypes.Year;

                return _GroupingTypes.Day;
                #endregion
            }
     
            private Condesus.EMS.Business.PA.Entities.Measurement.GroupingType TranslateGrouping(_GroupingTypes grouping)
            {
                if (grouping == _GroupingTypes.Hour) return Condesus.EMS.Business.PA.Entities.Measurement.GroupingType.Hour;
                if (grouping == _GroupingTypes.Day) return Condesus.EMS.Business.PA.Entities.Measurement.GroupingType.Day;
                if (grouping == _GroupingTypes.Month) return Condesus.EMS.Business.PA.Entities.Measurement.GroupingType.Month;
                if (grouping == _GroupingTypes.Year) return Condesus.EMS.Business.PA.Entities.Measurement.GroupingType.Year;

                return Condesus.EMS.Business.PA.Entities.Measurement.GroupingType.Day;
            }
            private Condesus.EMS.Business.PA.Entities.Measurement.AggregateType TranslateAggregate(_AggregateTypes aggregate)
            {
                if (aggregate == _AggregateTypes.Avg) return Condesus.EMS.Business.PA.Entities.Measurement.AggregateType.Avg;
                if (aggregate == _AggregateTypes.StdDev) return Condesus.EMS.Business.PA.Entities.Measurement.AggregateType.StdDev;
                if (aggregate == _AggregateTypes.StdDevP) return Condesus.EMS.Business.PA.Entities.Measurement.AggregateType.StdDevP;
                if (aggregate == _AggregateTypes.Sum) return Condesus.EMS.Business.PA.Entities.Measurement.AggregateType.Sum;
                if (aggregate == _AggregateTypes.Cummulative) return Condesus.EMS.Business.PA.Entities.Measurement.AggregateType.SumCummulative;
                if (aggregate == _AggregateTypes.Var) return Condesus.EMS.Business.PA.Entities.Measurement.AggregateType.Var;
                if (aggregate == _AggregateTypes.VarP) return Condesus.EMS.Business.PA.Entities.Measurement.AggregateType.VarP;

                return Condesus.EMS.Business.PA.Entities.Measurement.AggregateType.Avg;
            }

            private void AddComboParameterGroups()
            {
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                Measurement _measurement = EMSLibrary.User.PerformanceAssessments.Configuration.Measurement(_IdMeasurement);

                if (_measurement != null)
                {
                    if (_measurement.Indicator != null)
                    {
                        _params.Add("IdIndicator", _measurement.Indicator.IdIndicator);
                    }
                }
                AddCombo(phParameterGroup, ref _RdcParameterGroup, Common.ConstantsEntitiesName.PA.ParameterGroups, String.Empty, _params, false, true, false, false, false);
                _RdcParameterGroup.Width = Unit.Pixel(160);
            }
        #endregion

        #region Events
            void imgBtnResetChart_Click(object sender, ImageClickEventArgs e)
            {
                chkShowAverage.Checked = false;
                chkShowBands.Checked = false;
                chkShowFirstLast.Checked = false;
                chkShowMaxMin.Checked = false;

                Condesus.EMS.Business.PA.Entities.Measurement _measurement = EMSLibrary.User.PerformanceAssessments.Configuration.Measurement(_IdMeasurement);
                DefaultChart(_measurement);
            }
            void imgBtnRefreshChart_Click(object sender, ImageClickEventArgs e)
            {
                Condesus.EMS.Business.PA.Entities.Measurement _measurement = EMSLibrary.User.PerformanceAssessments.Configuration.Measurement(_IdMeasurement);
                RefreshChart(_measurement);
                ShowMaxMinValues(chkShowMaxMin.Checked);
                ShowFirstLastValues(chkShowFirstLast.Checked);

                Int64 _idIndicator = Convert.ToInt64(GetKeyValue(_RdcParameterGroup.SelectedValue, "IdIndicator"));
                Int64 _idParameterGroup = Convert.ToInt64(GetKeyValue(_RdcParameterGroup.SelectedValue, "IdParameterGroup"));
                ParameterGroup _parameterGroup = EMSLibrary.User.PerformanceAssessments.Map.Indicator(_idIndicator).ParameterGroup(_idParameterGroup);
                ShowBands(chkShowBands.Checked, _measurement, _parameterGroup);

                ShowAverageSerie(chkShowAverage.Checked);
                LoadStatistics();
            }
            protected void chtIndicator_CommandFired(object sender, CommandFiredArgs e)
            {

            }

            #region RGDMasterGrid
                protected void rgdMasterGrid_SortCommand(object source, Telerik.Web.UI.GridSortCommandEventArgs e)
                {
                    rgdMasterGrid.MasterTableView.Rebind();
                }
                protected void rgdMasterGrid_PageIndexChanged(object source, GridPageChangedEventArgs e)
                {
                    rgdMasterGrid.MasterTableView.Rebind();
                }
                protected void rgdMasterGrid_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
                {
                    rgdMasterGrid.DataSource = ReturnDataGrid();
                }
                protected void rgdMasterGrid_ItemDataBound(object sender, GridItemEventArgs e)
                {
                    if (e.Item is GridDataItem)
                    {
                        String _measurementStatus = ((DataRowView)e.Item.DataItem).Row["Status"].ToString();
                        if (_measurementStatus.ToLower() == "true")
                        {
                            e.Item.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }
            #endregion

        #endregion

    }
}
