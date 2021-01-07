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

namespace Condesus.EMS.WebUI.ManagementTools.PerformanceAssessment
{
    public partial class CalculationChart : BasePage
    {
        #region Internal Properties
            private Condesus.EMS.Business.PA.Entities.Calculation _Calculation;
            private Int64 _IdCalculation
            {
                get
                {
                    object o = ViewState["idCalculation"];

                    if (o != null) {
                        return Convert.ToInt64(ViewState["idCalculation"]);
                    }
                    else
                    {
                        _IdCalculation = Convert.ToInt64(Request.QueryString["idCalculation"]);
                        return Convert.ToInt64(Request.QueryString["idCalculation"]);
                    }
                }
                set { ViewState["idCalculation"] = value; }
            }
        #endregion

        #region Page Events
        private void InitializeHandlers()
        {
            imgBtnRefreshChart.Click += new ImageClickEventHandler(imgBtnRefreshChart_Click);
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            LoadTextLabels();
            InitializeHandlers();
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                lblSubTitle.Text = Resources.Common.PageSubtitleProperties;

                _IdCalculation = Convert.ToInt64(Request.QueryString["idCalculation"]);

                InitializeMainEntity();

                lblCalculationNameValue.Text = _Calculation.LanguageOption.Name;
                lblCalculationDescriptionValue.Text = _Calculation.LanguageOption.Description;
                lblCalculationUnitValue.Text = _Calculation.MeasurementUnit.LanguageOption.Name;
                lblCalculationFrequencyValue.Text = "every " + _Calculation.Frequency + " " + _Calculation.TimeUnitFrequency.LanguageOption.Name;
                lblCalculationFormulaValue.Text = _Calculation.Formula.LiteralFormula;

              //Setea el measurement
                //Deberia Recibir el Process Id del Request...
                DefaultChart();


            }

            InitializeMainEntity();
            lblCalculationLastDateValue.Text = _Calculation.DateLastResult.ToString();
            lblCalculationLastValueValue.Text = _Calculation.LastResult.ToString();
        }

        private void InitializeMainEntity()
        {
            if (_Calculation == null)
            {
                _Calculation = EMSLibrary.User.PerformanceAssessments.Configuration.Calculation(_IdCalculation);
            }

        }
        #endregion

        #region Methods
        private void LoadTextLabels()
        {
            Page.Title = Resources.CommonListManage.Indicator;
            lblCalculationDescriptionLabel.Text = Resources.CommonListManage.Description;
            lblCalculationFormulaLabel.Text = Resources.CommonListManage.Formula;
            lblCalculationFrequencyLabel.Text = Resources.CommonListManage.Frequency;
            lblCalculationLastDate.Text = Resources.CommonListManage.LastDate;
            lblCalculationLastValue.Text = Resources.CommonListManage.LastValue;
            lblCalculationNameLabel.Text = Resources.CommonListManage.Calculation;
            lblCalculationUnitLabel.Text = Resources.CommonListManage.Unit;
            lblDetail.Text = Resources.CommonListManage.CalculationChartDetails;
            lblEndDate.Text = Resources.CommonListManage.EndDate;
            lblStartDate.Text = Resources.CommonListManage.StartDate;
            lblSubTitle.Text = Resources.CommonListManage.CalculationChartSubTitle;
            lblTitle.Text = Resources.CommonListManage.CalculationChartTitle;
            imgBtnRefreshChart.AlternateText = Resources.CommonListManage.RefreshChart;
        }

        #region Data Feed

        private void LoadData()
        {
            FeedChart(_Calculation.Calculate());
        }
        private void LoadData(DateTime startDate, DateTime endDate)
        {
            FeedChart(_Calculation.Calculate(startDate, endDate));
        }
        private void FeedChart(Decimal value)
        {
            Double _value = (Double)value;

            dndGauge.BorderWidth = 0;
            Dundas.Gauges.WebControl.GaugeLabel _label = dndGauge.Labels.Add("Value");
            _label.Location.X = 5;
            _label.Location.Y = 5;
            _label.Size.Width = 90;
            _label.Size.Height = 20;

            // Set label text and alignment.
            _label.Text = _value.ToString() + " " + _Calculation.MeasurementUnit.LanguageOption.Name;
            _label.TextAlignment = ContentAlignment.MiddleCenter;

            // Set label text font and appearance.
            _label.Font = new Font("Arial", 10);
            _label.TextColor = Color.DarkGray;

            // Set label background and border color.
            _label.BackColor = Color.AliceBlue;
            _label.BackShadowOffset = 0;
            _label.BorderWidth = 1;
            _label.BorderColor = Color.CornflowerBlue;
            
            //Frame
            Dundas.Gauges.WebControl.BackFrame _frame = new Dundas.Gauges.WebControl.BackFrame();
            _frame.FrameStyle = Dundas.Gauges.WebControl.BackFrameStyle.Simple;
            _frame.BackGradientType = Dundas.Gauges.WebControl.GradientType.None;
            _frame.BackColor = Color.AliceBlue;
            _frame.FrameColor = Color.AliceBlue;
            _frame.FrameGradientType = Dundas.Gauges.WebControl.GradientType.None;
            _frame.BorderWidth = 0;
            _frame.BorderColor = Color.AliceBlue;

            _frame.FrameShape = Dundas.Gauges.WebControl.BackFrameShape.Rectangular;
            dndGauge.LinearGauges["Calculation"].BackFrame = _frame;

            //Scale
            Dundas.Gauges.WebControl.LinearScale _scale = new Dundas.Gauges.WebControl.LinearScale();
            _scale.Minimum = 0;
            _scale.Maximum = Math.Ceiling(_value + _value*0.5);
            
            Font a = new Font(new FontFamily("Arial"),5);
            _scale.LabelStyle.Font = a;
            _scale.LabelStyle.Placement = Dundas.Gauges.WebControl.Placement.Outside;
            _scale.LabelStyle.ShowEndLabels = true;

            _scale.FillColor = Color.Gray;
            _scale.FillGradientEndColor = Color.Gray;
            _scale.FillGradientType = Dundas.Gauges.WebControl.GradientType.None;

            Dundas.Gauges.WebControl.LinearMinorTickMark _minorTick = new Dundas.Gauges.WebControl.LinearMinorTickMark();
            _minorTick.Placement = Dundas.Gauges.WebControl.Placement.Cross;
            _minorTick.FillColor = Color.Gray;
            _minorTick.BorderColor = Color.Gray;
            _minorTick.Width = 1;
            _minorTick.Interval = Math.Ceiling(_value / 12);
            _scale.MinorTickMark = _minorTick;
            
            Dundas.Gauges.WebControl.LinearMajorTickMark _majorTick = new Dundas.Gauges.WebControl.LinearMajorTickMark();
            _majorTick.Placement = Dundas.Gauges.WebControl.Placement.Cross;
            _majorTick.Interval = Math.Ceiling(_value / 3);
            _majorTick.FillColor = Color.Gray;
            _majorTick.BorderColor = Color.Gray;
            _majorTick.Width = 3;
            _scale.MajorTickMark = _majorTick;

            dndGauge.LinearGauges["Calculation"].Scales.Add(_scale);

            //Pointer
            Dundas.Gauges.WebControl.LinearPointer _pointer = new Dundas.Gauges.WebControl.LinearPointer();
            _pointer.Name = "Default";
            _pointer.Type = Dundas.Gauges.WebControl.LinearPointerType.Marker;
            _pointer.Width = 3;
            _pointer.MarkerStyle = Dundas.Gauges.WebControl.MarkerStyle.Triangle;
            _pointer.Value = _value;
            _pointer.FillColor = Color.Red;
            _pointer.BorderColor = Color.Red;

            dndGauge.LinearGauges["Calculation"].Pointers.Add(_pointer);

            //Dundas.Gauges.WebControl.LinearRange _range = new Dundas.Gauges.WebControl.LinearRange();
            //_range.StartWidth = 2;
            //_range.EndWidth = 10;
            //_range.FillColor = Color.White;
            //_range.FillGradientEndColor = Color.Cyan;
            //_range.FillGradientType = Dundas.Gauges.WebControl.RangeGradientType.StartToEnd;
            //_range.StartValue = 0;
            //_range.EndValue = _value + _value * 0.5;
            //dndGauge.LinearGauges["Calculation"].Ranges.Add(_range);
            

        }
     
        #endregion

        private void RefreshChart()
        {
         
            //Set date options
            DateTime _startDate = DateTime.MinValue; ;
            if (radcalStartDate.SelectedDate != null)
            {
                _startDate = (DateTime)radcalStartDate.SelectedDate;    
            }

            DateTime _endDate = DateTime.MaxValue; ;
            if (radcalEndDate.SelectedDate != null)
            {
                _endDate = (DateTime)radcalEndDate.SelectedDate;
            }

            if (_startDate != DateTime.MinValue && _endDate != DateTime.MaxValue)
            {
                LoadData(_startDate, _endDate);    
            }
            else
            {
                LoadData();
            }
            
        }
        private void DefaultChart()
        {
            radcalEndDate.SelectedDate = null;

            LoadData();
        }

        #endregion

        #region Events

        void imgBtnResetChart_Click(object sender, ImageClickEventArgs e)
        {
            DefaultChart();
        }
        void imgBtnRefreshChart_Click(object sender, ImageClickEventArgs e)
        {
            RefreshChart();
        }
        protected void chtIndicator_CommandFired(object sender, CommandFiredArgs e)
        {

        }

        #endregion

    }
}