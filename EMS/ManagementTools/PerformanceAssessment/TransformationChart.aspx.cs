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
using CEB_PA = Condesus.EMS.Business.PA;
using Condesus.EMS.Business.PA.Entities;

namespace Condesus.EMS.WebUI.ManagementTools.PerformanceAssessment
{
    public partial class TransformationChart : BasePage
    {
        #region Private Properties
            //private Condesus.EMS.Business.PA.Entities.MeasurementStatistics _Stats;
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
            private Int64 _IdTransformation
            {
                get
                {
                    object o = ViewState["IdTransformation"];
                    if (o != null)
                    {
                        return Convert.ToInt64(ViewState["IdTransformation"]);
                    }
                    else
                    {
                        _IdTransformation = Convert.ToInt64(Request.QueryString["IdTransformation"]);
                        return Convert.ToInt64(Request.QueryString["IdTransformation"]);
                    }
                }
                set { ViewState["IdTransformation"] = value; }
            }
            private String _ChartType
            {
                get { return ViewState["ChartType"].ToString(); }
                set { ViewState["ChartType"] = value; }
            }
            private int _TimeOut;
        #endregion

        #region Page Events
            private void InitializeHandlers()
            {
                //imgBtnRefreshChart.Click += new ImageClickEventHandler(imgBtnRefreshChart_Click);
                //imgBtnResetChart.Click += new ImageClickEventHandler(imgBtnResetChart_Click);
            }
            protected override void OnInit(EventArgs e)
            {
                base.OnInit(e);
                LoadTextLabels();

                _TimeOut = Server.ScriptTimeout;
                Server.ScriptTimeout = 3600;

                InitializeHandlers();
            }
            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);

                base.SetContentTableRowsCss(tblContentForm2);
                if (!IsPostBack)
                {
                    this.Title = "EMS - " + Resources.CommonListManage.Indicator;

                    //Load Types
                    //LoadAggregateType();
                    //LoadGroupingType();

                    //Setea el measurement
                    //Deberia Recibir el Process Id del Request...
                    CalculateOfTransformation _calculateOfTransformation = EMSLibrary.User.PerformanceAssessments.Configuration.Measurement(_IdMeasurement).Transformation(_IdTransformation);

                    LoadIndicatorData(_calculateOfTransformation);
                    DefaultChart(_calculateOfTransformation);
                    //ShowAverageSerie(false);

                }
                Server.ScriptTimeout = _TimeOut;
            }
        #endregion

        #region Private Methods
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonListManage.MeasurementChart;
                lblBase.Text = Resources.CommonListManage.Base;
                //imgBtnRefreshChart.AlternateText = Resources.CommonListManage.RefreshChart;
                //imgBtnResetChart.AlternateText = Resources.CommonListManage.ResetChart;
                lblTitle.Text = Resources.CommonListManage.MeasurementChart;
                lblDetail.Text = Resources.CommonListManage.MeasurementChartSubTitle;
                //lblAggregate.Text = Resources.CommonListManage.Aggregate;
                //lblAVG.Text = Resources.CommonListManage.Average;
                //lblCOUNT.Text = Resources.CommonListManage.Count;
                lblDeviceLabel.Text = Resources.CommonListManage.Device;
                //lblEndDate.Text = Resources.CommonListManage.EndDate;
                //lblFIRST.Text = Resources.CommonListManage.FirstValue;
                //lblGrouping.Text = Resources.CommonListManage.Grouping;
                lblIndicatorLabel.Text = Resources.CommonListManage.Indicator;
                //lblLAST.Text = Resources.CommonListManage.LastValue;
                //lblMAX.Text = Resources.CommonListManage.MaxValue;
                lblTransformationLabel.Text = Resources.CommonListManage.Transformation;
                lblMeasurementUnit.Text = Resources.CommonListManage.MeasurementUnit;
                //lblMIN.Text = Resources.CommonListManage.MinValue;
                //lblPeriod.Text = Resources.CommonListManage.Period;
                lblProject.Text = Resources.CommonListManage.Project;
                //lblShowAverage.Text = Resources.CommonListManage.ShowAverage;
                //lblShowBands.Text = Resources.CommonListManage.ShowBands;
                //lblShowFirstLast.Text = Resources.CommonListManage.ShowFirstLast;
                //lblShowMaxMin.Text = Resources.CommonListManage.ShowMaxMin;
                //lblStartDate.Text = Resources.CommonListManage.StartDate;
                //lblSTDDEV.Text = Resources.CommonListManage.StdDevValue;
                //lblSTDDEVP.Text = Resources.CommonListManage.StdDevPValue;
                //lblSUM.Text = Resources.CommonListManage.Sum;
                //lblVAR.Text = Resources.CommonListManage.VarValue;
                //lblVARP.Text = Resources.CommonListManage.VarPValue;
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
                private void LoadIndicatorData(CalculateOfTransformation calculateOfTransformation)
                {
                    Condesus.EMS.Business.PF.Entities.ProcessGroupProcess _rootProcess = calculateOfTransformation.MeasurementOrigin.ProcessTask.Parent;

                    lblProjectValue.Text = _rootProcess.LanguageOption.Title;

                    Indicator _indicator = calculateOfTransformation.Indicator;
                    Indicator_LG _indicator_LG = _indicator.LanguageOption;
                    MeasurementDevice _measurementDevice = calculateOfTransformation.MeasurementOrigin.Device;
                    MeasurementUnit _measurementUnit = calculateOfTransformation.MeasurementUnit;

                    lblBaseValue.Text = Common.Functions.RemoveIndexesTags(calculateOfTransformation.BaseTransformer.Name);
                    lblTransformationValue.Text = String.Concat(calculateOfTransformation.LanguageOption.Name);
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
                //private void LoadStatistics()
                //{
                //    lblFirstDate.Text = _Stats.FirstDate.ToString();
                //    lblLastDate.Text = _Stats.LastDate.ToString();
                //    lblLastVal.Text = _Stats.LastValue.ToString();
                //    lblFirstVal.Text = _Stats.FirstValue.ToString();
                //    lblMinVal.Text = _Stats.MinValue.ToString();
                //    lblMaxVal.Text = _Stats.MaxValue.ToString();
                //    lblAvgVal.Text = _Stats.AvgValue.ToString();
                //    lblCountVal.Text = _Stats.CountValue.ToString();
                //    lblSumVal.Text = _Stats.SumValue.ToString();
                //    lblStdDevVal.Text = _Stats.StdDevValue.ToString();
                //    lblStdDevPVal.Text = _Stats.StdDevPValue.ToString();
                //    lblVarVal.Text = _Stats.VarValue.ToString();
                //    lblVarPVal.Text = _Stats.VarPValue.ToString();
                //}

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
                            _ChartSeries.Points.AddXY(_point.MeasureDate.ToString(), Convert.ToDouble(_point.MeasureValue));
                        }
                    }
                #endregion

            #endregion

            #region Combos
                //private void LoadAggregateType()
                //{
                //    radddlAggregate.Items.Add(new Telerik.Web.UI.RadComboBoxItem(_AggregateTypes.None.ToString()));
                //    radddlAggregate.Items.Add(new Telerik.Web.UI.RadComboBoxItem(_AggregateTypes.Avg.ToString()));
                //    radddlAggregate.Items.Add(new Telerik.Web.UI.RadComboBoxItem(_AggregateTypes.StdDev.ToString()));
                //    radddlAggregate.Items.Add(new Telerik.Web.UI.RadComboBoxItem(_AggregateTypes.StdDevP.ToString()));
                //    radddlAggregate.Items.Add(new Telerik.Web.UI.RadComboBoxItem(_AggregateTypes.Sum.ToString()));
                //    radddlAggregate.Items.Add(new Telerik.Web.UI.RadComboBoxItem(_AggregateTypes.Cummulative.ToString()));
                //    radddlAggregate.Items.Add(new Telerik.Web.UI.RadComboBoxItem(_AggregateTypes.Var.ToString()));
                //    radddlAggregate.Items.Add(new Telerik.Web.UI.RadComboBoxItem(_AggregateTypes.VarP.ToString()));
                //}
                //private void LoadGroupingType()
                //{
                //    radddlGrouping.Items.Add(new Telerik.Web.UI.RadComboBoxItem(_GroupingTypes.None.ToString()));
                //    radddlGrouping.Items.Add(new Telerik.Web.UI.RadComboBoxItem(_GroupingTypes.Hour.ToString()));
                //    radddlGrouping.Items.Add(new Telerik.Web.UI.RadComboBoxItem(_GroupingTypes.Day.ToString()));
                //    radddlGrouping.Items.Add(new Telerik.Web.UI.RadComboBoxItem(_GroupingTypes.Month.ToString()));
                //    radddlGrouping.Items.Add(new Telerik.Web.UI.RadComboBoxItem(_GroupingTypes.Year.ToString()));
                //}
            #endregion
           
            #region Filters
                //private void ShowAverageSerie(Boolean show)
                //{
                //    if (show)
                //    {
                //        if (_ChartSeriesAvg.Points.Count > 0)
                //        {
                //            _ChartSeriesAvg.Enabled = show;
                //        }
                //        else
                //        {
                //            for (int i = 0; i < _ChartSeries.Points.Count; i++)
                //            {
                //                _ChartSeriesAvg.Points.AddXY(_ChartSeries.Points[i].XValue, Convert.ToDouble(lblAvgVal.Text));
                //            }
                //        }
                //        _ChartSeriesAvg.Color = Color.Green;
                //        _ChartSeriesAvg.Type = SeriesChartType.Line;

                //    }
                //    else
                //    {
                //        _ChartSeriesAvg.Enabled = show;
                //    }
                //}
                //private void ShowFirstLastValues(Boolean show)
                //{
                //    if (show)
                //    {
                //        DataPointCollection _points = chtIndicator.Series["Data"].Points;
                //        DataPoint _firstValuePoint = _points[0];
                //        _firstValuePoint.Color = Color.Black;
                //        _firstValuePoint.ShowLabelAsValue = true;
                //        _firstValuePoint.ShowInLegend = true;
                //        _firstValuePoint.MarkerStyle = MarkerStyle.Square;
                //        _firstValuePoint.LegendText = _firstValuePoint.Name;

                //        DataPoint _lastValuePoint = _points[_points.Count-1];
                //        _lastValuePoint.Color = Color.Black;
                //        _lastValuePoint.ShowLabelAsValue = true;
                //        _lastValuePoint.ShowInLegend = true;
                //        _lastValuePoint.MarkerStyle = MarkerStyle.Square;
                //        _lastValuePoint.LegendText = _lastValuePoint.Name;
                //    }
                //}
                //private void ShowMaxMinValues(Boolean show)
                //{
                //    if (show)
                //    {
                //        DataPoint _maxValuePoint = chtIndicator.Series["Data"].Points.FindMaxValue();
                //        _maxValuePoint.Color = Color.Black;
                //        _maxValuePoint.ShowLabelAsValue = true;
                //        _maxValuePoint.ShowInLegend = true;
                //        _maxValuePoint.MarkerStyle = MarkerStyle.Triangle;
                //        _maxValuePoint.LegendText = _maxValuePoint.Name;

                //        DataPoint _minValuePoint = chtIndicator.Series["Data"].Points.FindMinValue();
                //        _minValuePoint.Color = Color.Black;
                //        _minValuePoint.ShowLabelAsValue = true;
                //        _minValuePoint.ShowInLegend = true;
                //        _minValuePoint.MarkerStyle = MarkerStyle.Triangle;
                //        _minValuePoint.LegendText = _minValuePoint.Name;
                //    }
                //}
                //private void ShowBands(Boolean show, Condesus.EMS.Business.PA.Entities.Measurement _measurement)
                //{
                //    if (show)
                //    {
                //        foreach (Condesus.EMS.Business.PA.Entities.Parameter _parameter in _measurement.ParameterGroup.Parameters.Values)
                //        {
                //            String _name = _parameter.LanguageOption.Description;

                //            foreach (Condesus.EMS.Business.PA.Entities.ParameterRange _parameterRange in _parameter.ParameterRanges.Values)
                //            {
                //                SetBand(_name, Convert.ToDouble(_parameterRange.LowValue), Convert.ToDouble(_parameterRange.HighValue), _parameter.Sign);
                //            }
                //        }
                //    }
                //    else
                //    {
                //        chtIndicator.ChartAreas["AreaData"].AxisY.StripLines.Clear();
                //        chtIndicator.ChartAreas["AreaData"].AxisY.CustomLabels.Clear();
                //    }
                //}
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

            private void RefreshChart(CalculateOfTransformation calculateOfTransformation)
            {
                _ChartSeries.Color = Color.Blue;

                //Set date options
                DateTime? _startDate = null;
                //_startDate = radcalStartDate.SelectedDate;

                DateTime? _endDate = null;
                //_endDate = radcalEndDate.SelectedDate;

                //Set aggregate options
                //_GroupingTypes _grouping = GetGroupingType(radddlGrouping.SelectedItem.Text);
                //_AggregateTypes _aggregate = GetAggregateType(radddlAggregate.SelectedItem.Text);

                //_Stats = calculateOfTransformation.Statistics(_startDate, _endDate);

                //if (_aggregate == _AggregateTypes.None || _grouping == _GroupingTypes.None)
                    LoadData(_startDate, _endDate);
                //else
                //    LoadData(_startDate, _endDate, _aggregate, _grouping);
            }
            private void DefaultChart(CalculateOfTransformation calculateOfTransformation)
            {
                _ChartSeries.Color = Color.Blue;
                _ChartSeries.Type = SeriesChartType.Line;

                //radcalStartDate.SelectedDate = null;
                //radcalEndDate.SelectedDate = null;

                //Combo en day si frecuencia es menor a dia
                //if (calculateOfTransformation.MeasurementOrigin.TimeUnitFrequency > 3)
                //{
                //    radddlGrouping.SelectedIndex = 2;
                //    radddlAggregate.SelectedIndex = 1;
                //}
                //else
                //{
                //    radddlGrouping.SelectedIndex = -1;
                //    radddlAggregate.SelectedIndex = -1;
                //}

                RefreshChart(calculateOfTransformation);

                //Statistics
                //LoadStatistics();
            }

            //private String GetX(DateTime xValue) 
            //{
            //    switch (GetGroupingType(radddlGrouping.SelectedItem.Text))
            //    {
            //        case _GroupingTypes.None:
            //            return xValue.ToString();
            //        case _GroupingTypes.Hour:
            //            return xValue.Hour.ToString() + " " + xValue.ToShortDateString();
            //        case _GroupingTypes.Day:
            //            return xValue.ToShortDateString();
            //        case _GroupingTypes.Month:
            //            return xValue.Month.ToString() + "/" + xValue.Year.ToString();
            //        case _GroupingTypes.Year:
            //            return xValue.Year.ToString();
            //        default:
            //            return xValue.ToString();
            //    }
            //}

            //private _AggregateTypes GetAggregateType(String aggregate)
            //{
            //    try
            //    {
            //        return (_AggregateTypes)Enum.Parse(typeof(_AggregateTypes), aggregate);
            //    }
            //    catch
            //    {
            //        return _AggregateTypes.None;
            //    }

            //    #region Old
            //    if (aggregate == _AggregateTypes.None.ToString()) return _AggregateTypes.None;
            //    if (aggregate == _AggregateTypes.Avg.ToString()) return _AggregateTypes.Avg;
            //    if (aggregate == _AggregateTypes.StdDev.ToString()) return _AggregateTypes.StdDev;
            //    if (aggregate == _AggregateTypes.StdDevP.ToString()) return _AggregateTypes.StdDevP;
            //    if (aggregate == _AggregateTypes.Sum.ToString()) return _AggregateTypes.Sum;
            //    if (aggregate == _AggregateTypes.Var.ToString()) return _AggregateTypes.Var;
            //    if (aggregate == _AggregateTypes.VarP.ToString()) return _AggregateTypes.VarP;

            //    return _AggregateTypes.Avg;
            //    #endregion
            //}
            //private _GroupingTypes GetGroupingType(String grouping)
            //{
            //    try
            //    {
            //        return (_GroupingTypes)Enum.Parse(typeof(_GroupingTypes), grouping);
            //    }
            //    catch
            //    {
            //        return _GroupingTypes.None;
            //    }

            //    #region Old
            //    if (grouping == _GroupingTypes.None.ToString()) return _GroupingTypes.None;
            //    if (grouping == _GroupingTypes.Day.ToString()) return _GroupingTypes.Day;
            //    if (grouping == _GroupingTypes.Month.ToString()) return _GroupingTypes.Month;
            //    if (grouping == _GroupingTypes.Year.ToString()) return _GroupingTypes.Year;

            //    return _GroupingTypes.Day;
            //    #endregion
            //}
     
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
        
        #endregion

        #region Events
            void imgBtnResetChart_Click(object sender, ImageClickEventArgs e)
            {
                //chkShowAverage.Checked = false;
                //chkShowBands.Checked = false;
                //chkShowFirstLast.Checked = false;
                //chkShowMaxMin.Checked = false;

                CalculateOfTransformation _calculateOfTransformation = EMSLibrary.User.PerformanceAssessments.Configuration.Measurement(_IdMeasurement).Transformation(_IdTransformation);
                DefaultChart(_calculateOfTransformation);
            }
            void imgBtnRefreshChart_Click(object sender, ImageClickEventArgs e)
            {
                CalculateOfTransformation _calculateOfTransformation = EMSLibrary.User.PerformanceAssessments.Configuration.Measurement(_IdMeasurement).Transformation(_IdTransformation);
                RefreshChart(_calculateOfTransformation);
                //ShowMaxMinValues(chkShowMaxMin.Checked);
                //ShowFirstLastValues(chkShowFirstLast.Checked);
                //ShowBands(chkShowBands.Checked, _measurement);
                //ShowAverageSerie(chkShowAverage.Checked);
                //LoadStatistics();
            }
            protected void chtIndicator_CommandFired(object sender, CommandFiredArgs e)
            {

            }
        #endregion

    }
}
