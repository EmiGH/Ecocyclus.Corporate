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
using Telerik.Web.UI;
using Dundas.Charting.WebControl;

using EBPE = Condesus.EMS.Business.PF.Entities;
using EBPAE = Condesus.EMS.Business.PA.Entities;
using Condesus.EMS.Business.EP.Entities;

namespace Condesus.EMS.WebUI.Dashboard
{
    public partial class ProyectBuyerSummary : BaseDashboard
    {
        #region Internal Properties
        //Estructura interna para guardar los totales
        private Decimal _EstimationTotal
        {
            get { return (Decimal)ViewState["EstimationTotal"]; }
            set { ViewState["EstimationTotal"] = value; }
        }
        private Decimal _CalculationTotal
        {
            get { return (Decimal)ViewState["CalculationTotal"]; }
            set { ViewState["CalculationTotal"] = value; }
        }
        private Decimal _CertificatedTotal
        {
            get { return (Decimal)ViewState["CertificatedTotal"]; }
            set { ViewState["CertificatedTotal"] = value; }
        }

        EBPE.ProcessGroupProcess _ProcessRoot = null;

        private Int64 _IdEntity
        {
            get { return Convert.ToInt64(ViewState["IdProcess"]); }
            set { ViewState["IdProcess"] = value.ToString(); }
        }
        //private Int64 _IdEntity
        //{
        //    get
        //    {
        //        return base.NavigatorContainsTransferVar("IdProcess") ? base.NavigatorGetTransferVar<Int64>("IdProcess") : 0;
        //    }
        //}
        private Dundas.Charting.WebControl.Series _ChartSeriesForecasted
        {
            get { return chtIndicator.Series["PDD ERs"]; }
        }
        private Dundas.Charting.WebControl.Series _ChartSeriesCalculated
        {
            get { return chtIndicator.Series["Current ERs"]; }
        }
        private Dundas.Charting.WebControl.Series _ChartSeriesVerificated
        {
            get { return chtIndicator.Series["Issued ERs"]; }
        }



        #endregion

        #region Load & Init

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializaHandlers();
        }
        private void InitializaHandlers()
        {
            imgBtnRefreshChart.Click += new ImageClickEventHandler(imgBtnRefreshChart_Click);
            imgBtnResetChart.Click += new ImageClickEventHandler(imgBtnResetChart_Click);
        }

        private void InitializeMainEntity()
        {
            if (_ProcessRoot == null)
                _ProcessRoot = (Condesus.EMS.Business.PF.Entities.ProcessGroupProcess)EMSLibrary.User.ProcessFramework.Map.Process(_IdEntity);

            if (_ProcessRoot == null)
                throw new Exception("The page could not instantiate the Main Entity");

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!Page.IsPostBack)
            {
                _EstimationTotal = 0;
                _CertificatedTotal = 0;
                _CalculationTotal = 0;

                if (Request.QueryString["IdProcess"] != null)
                {
                    _IdEntity = Convert.ToInt64(Request.QueryString["IdProcess"]);
                }

                InitializeMainEntity();

                //Main Data
                InitializeMainData();
                
                LoadChartData(null, null);
            }

            InitializeMainEntity();

            //Load de los demas Controles Dinamicos
            BuildControls();

            //Form
            base.SetContentTableRowsCss(tblContentForm);
            base.SetContentTableRowsCss(tblContentForm2);
            base.SetContentTableRowsCss(tblContentForm3);
            base.SetContentTableRowsCss(tblContentForm4);

        }

        private void SetLabels(String id, System.Web.UI.WebControls.Label title, System.Web.UI.WebControls.Label value)
        {
            ExtendedPropertyValue _bufExtProp = null;
            Int64 _output = 0;
            if (Int64.TryParse(ConfigurationManager.AppSettings[id], out _output))
            {
                _bufExtProp = _ProcessRoot.ExtendedPropertyValue(_output);
                if (_bufExtProp != null)
                {
                    title.Text = _bufExtProp.ExtendedProperty.LanguageOption.Name;
                    value.Text = _bufExtProp.Value;
                }
            }
        }
        private void InitializeMainData()
        {
            //'Main' Properties
            lblNameProject.Text = _ProcessRoot.LanguageOption.Title;

            //Extended Properties
            SetLabels("IdExtendedPropertyPDDName", lblPDDName, lblPDDNameValue);

            SetLabels("IdExtendedPropertyProjectName", lblProjectName, lblProjectNameValue);

            SetLabels("IdExtendedPropertyPINumber", lblPINumber, lblPINumberValue);
            SetLabels("IdExtendedPropertyProjectType", lblProjectType, lblProjectTypeValue);
            SetLabels("IdExtendedPropertyCategory", lblCategory, lblCategoryValue);
            SetLabels("IdExtendedPropertyUNFCCC", lblUNFCCC, lblUNFCCCValue);
            SetLabels("IdExtendedPropertyVerificationDOE", lblVerificationDOE, lblVerificationDOEValue);
            SetLabels("IdExtendedPropertyVerificationFrequency", lblVerificationFrequency, lblVerificationFrequencyValue);
            SetLabels("IdExtendedPropertyNextVerificationDate", lblNextVerificationDate, lblNextVerificationDateValue);
            //Project Information / Location
            SetLabels("IdExtendedPropertyCountryPB", lblCountryPB, lblCountryPBValue);
            SetLabels("IdExtendedPropertyProvince", lblProvince, lblProvinceValue);
            SetLabels("IdExtendedPropertyCompanyDescription", lblCompanyDescription, lblCompanyDescriptionValue);
            SetLabels("IdExtendedPropertyContactInformation", lblContactInformation, lblContactInformationValue);
            SetLabels("IdExtendedPropertyConsultantIntermediary", lblConsultantIntermediary, lblConsultantIntermediaryValue);
            SetLabels("IdExtendedPropertyProjectInvestor", lblProjectInvestor, lblProjectInvestorValue);
            //Brief Project Description & Status
            SetLabels("IdExtendedPropertyBriefDescription", lblBriefDescription, lblBriefDescriptionValue);
            SetLabels("IdExtendedPropertyProjectDescription", lblProjectDescription, lblProjectDescriptionValue);

            SetLabels("IdExtendedPropertyCDM", lblCDM, lblCDMValue);
            SetLabels("IdExtendedPropertyMethodologyNumber", lblMethodologyNumber, lblMethodologyNumberValue);
            SetLabels("IdExtendedPropertyCreditingPeriod", lblCreditingPeriod, lblCreditingPeriodValue);
            SetLabels("IdExtendedPropertyStatusDescription", lblStatusDescription, lblStatusDescriptionValue);
            SetLabels("IdExtendedPropertyBriefDescriptionER", lblBriefDescriptionER, lblBriefDescriptionERValue);
            SetLabels("IdExtendedPropertyUnexpectedEvents", lblUnexpectedEvents, lblUnexpectedEventsValue);
            SetLabels("IdExtendedPropertyExpectedEvents", lblExpectedEvents, lblExpectedEventsValue);
            SetLabels("IdExtendedPropertyTotalTonnes", lblTotalTonnes, lblTotalTonnesValue);

            SetLabels("IdExtendedPropertyLifeTimeER", lblLifeTime, lblLifeTimeValue);
            SetLabels("IdExtendedPropertyTotalTonnesOfCO2", lblTotalTonnesOfCO2, lblTotalTonnesOfCO2Value);

            //ER
            SetLabels("IdExtendedPropertyMGMMonitoringContact", lblMGMMonitoringContact, lblMGMMonitoringContactValue);
        }

        #endregion

        #region Methods

        private void BuildControls()
        {
            ClearContainers();

            BuildVerificationContent();
        }

        private void ClearContainers()
        {
            pnlVerifications.Controls.Clear();
        }

        private static void InitCommonGridProperties(RadGrid grid)
        {
            grid.AllowPaging = false;
            grid.AllowSorting = false;
            grid.Skin = "EMS";
            grid.EnableEmbeddedSkins = false;
            grid.Width = Unit.Percentage(100);
            grid.AutoGenerateColumns = false;
            //grid.EnableAJAX = false;
            grid.GridLines = System.Web.UI.WebControls.GridLines.None;
            grid.ShowStatusBar = false;
            grid.PageSize = 18;
            grid.AllowMultiRowSelection = false;
            //grid.EnableAJAXLoadingTemplate = true;
            //grid.LoadingTemplateTransparency = 25;
            grid.PagerStyle.AlwaysVisible = true;
            grid.PagerStyle.Mode = GridPagerMode.NextPrevAndNumeric;
            grid.MasterTableView.Width = Unit.Percentage(100);
            grid.EnableViewState = false;

            //Crea los metodos de la grilla (Cliente)
            grid.ClientSettings.AllowExpandCollapse = false;
            //grid.ClientSettings.EnableClientKeyValues = true;
            grid.AllowMultiRowSelection = false;
            grid.ClientSettings.Selecting.AllowRowSelect = true;
            grid.ClientSettings.Selecting.EnableDragToSelectRows = false;
            grid.MasterTableView.HierarchyLoadMode = GridChildLoadMode.Client;

            //Define los atributos de la MasterGrid
            grid.MasterTableView.EnableViewState = false;
            grid.MasterTableView.CellPadding = 0;
            grid.MasterTableView.CellSpacing = 0;
            grid.MasterTableView.GridLines = System.Web.UI.WebControls.GridLines.None;
            grid.MasterTableView.GroupsDefaultExpanded = false;
            grid.MasterTableView.AllowMultiColumnSorting = false;
            grid.MasterTableView.HierarchyLoadMode = GridChildLoadMode.Client;
            grid.MasterTableView.ExpandCollapseColumn.Resizable = false;
            grid.MasterTableView.ExpandCollapseColumn.HeaderStyle.Width = Unit.Pixel(20);
            grid.MasterTableView.RowIndicatorColumn.Visible = false;
            grid.MasterTableView.RowIndicatorColumn.HeaderStyle.Width = Unit.Pixel(20);
            grid.MasterTableView.EditMode = GridEditMode.InPlace;

            grid.HeaderStyle.Font.Bold = false;
            grid.HeaderStyle.Font.Italic = false;
            grid.HeaderStyle.Font.Overline = false;
            grid.HeaderStyle.Font.Strikeout = false;
            grid.HeaderStyle.Font.Underline = false;
            grid.HeaderStyle.Wrap = true;
        }


        #region Verifications

        private void BuildVerificationContent()
        {
            RadGrid _rdgVerifications = new RadGrid();

            BuildVerificationData(_rdgVerifications);

            pnlVerifications.Controls.Add(_rdgVerifications);
        }

        private void BuildVerificationData(RadGrid rgdVerifications)
        {
            #region Grilla
            rgdVerifications.ID = "rgdMasterGridVerificationClass";

            InitCommonGridProperties(rgdVerifications);

            //Crea los metodos de la grilla (Server)
            rgdVerifications.NeedDataSource += new GridNeedDataSourceEventHandler(rgdVerifications_NeedDataSource);
            //rgdVerifications.SortCommand += new GridSortCommandEventHandler(rgdVerifications_SortCommand);
            rgdVerifications.PageIndexChanged += new GridPageChangedEventHandler(rgdVerifications_PageIndexChanged);

            //Define los atributos de la MasterGrid
            rgdVerifications.MasterTableView.Name = "gridMasterVerification";

            //Crea las columnas para la MasterGrid.
            DefineColumnsProcessVerifications(rgdVerifications.MasterTableView);

            #endregion
        }

        private void DefineColumnsProcessVerifications(GridTableView gridTableViewDetails)
        {
            //Add columns bound
            GridBoundColumn boundColumn;

            boundColumn = new GridBoundColumn();
            boundColumn.DataField = "StartDate";
            boundColumn.HeaderText = "Start Date";
            gridTableViewDetails.Columns.Add(boundColumn);

            boundColumn = new GridBoundColumn();
            boundColumn.DataField = "EndDate";
            boundColumn.HeaderText = "End Date";
            gridTableViewDetails.Columns.Add(boundColumn);

            boundColumn = new GridBoundColumn();
            boundColumn.DataField = "Forecasted";
            boundColumn.HeaderText = "PDD ERs";
            gridTableViewDetails.Columns.Add(boundColumn);

            boundColumn = new GridBoundColumn();
            boundColumn.DataField = "Verification";
            boundColumn.HeaderText = "Issued";
            gridTableViewDetails.Columns.Add(boundColumn);

            boundColumn = new GridBoundColumn();
            boundColumn.DataField = "Deviation";
            boundColumn.HeaderText = "Deviation %";
            gridTableViewDetails.Columns.Add(boundColumn);
        }

        private DataTable ReturnDataGridVerifications()
        {
            DataTable _dt = new DataTable();
            _dt.TableName = "Root";
            _dt.Columns.Add("StartDate");
            _dt.Columns.Add("EndDate");
            _dt.Columns.Add("Verification");
            _dt.Columns.Add("Forecasted");
            _dt.Columns.Add("Deviation");

            EBPAE.CalculationScenarioType _scenarioType = EMSLibrary.User.PerformanceAssessments.Configuration.CalculationScenarioType(Convert.ToInt64(ConfigurationManager.AppSettings["PDDEstimated"]));
            Decimal _estimationTotal = 0;

            foreach (EBPAE.CalculationCertificated _calcCert in _ProcessRoot.AssociatedCalculations.First().Value.CalculationCertificates)
            {
                Decimal _desviation;
                if (_calcCert.CertificationDeviation(_scenarioType) == 0)
                { _desviation = 0; }
                else
                { _desviation = Math.Round((_calcCert.Value / _calcCert.CertificationDeviation(_scenarioType)) * 100, 2); }


                Decimal _verification = Math.Round(_calcCert.Value, 2);
                Decimal _estimation = Math.Round(_calcCert.CertificationDeviation(_scenarioType), 2);

                //Totales
                _estimationTotal += _calcCert.CertificationDeviation(_scenarioType);
                //_CertificatedTotal += _calcCert.Value;

                _dt.Rows.Add(_calcCert.StartDate.ToShortDateString(), _calcCert.EndDate.ToShortDateString(), _verification, _estimation, _desviation);
            }

            Decimal _deviation;
            if (_estimationTotal == 0)
            { _deviation = 0; }
            else
            { _deviation = Math.Round(_CertificatedTotal / _estimationTotal * 100, 2); }

            //_dt.Rows.Add(_ProcessRoot.CurrentCampaignStartDate.ToShortDateString(), "N/A", "N/A", "N/A", "N/A");
            _dt.Rows.Add("", "<b>Total</b>", "<b>" + Math.Round(_CertificatedTotal, 2).ToString() + "</b>", "<b>" + Math.Round(_estimationTotal, 2).ToString() + "</b>", "<b>" + _deviation.ToString() + "</b>");
            return _dt;
        }

        #region Eventos

        void rgdVerifications_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            RadGrid handler = (RadGrid)source;
            handler.DataSource = ReturnDataGridVerifications();
            handler.MasterTableView.Rebind();
        }

        void rgdVerifications_SortCommand(object source, GridSortCommandEventArgs e)
        {
            RadGrid handler = (RadGrid)source;
            handler.DataSource = ReturnDataGridVerifications();
            handler.MasterTableView.Rebind();
        }

        void rgdVerifications_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            RadGrid handler = (RadGrid)source;
            handler.DataSource = ReturnDataGridVerifications();
        }

        #endregion

        #endregion

        #endregion

        #region Chart

        void imgBtnResetChart_Click(object sender, ImageClickEventArgs e)
        {
            LoadChartData(null, null);
            radcalStartDate.SelectedDate = null;
            radcalEndDate.SelectedDate = null;
        }

        void imgBtnRefreshChart_Click(object sender, ImageClickEventArgs e)
        {
            DateTime? _startDate;
            _startDate = radcalStartDate.SelectedDate;

            DateTime? _endDate;
            _endDate = radcalEndDate.SelectedDate;

            LoadChartData(_startDate, _endDate);
        }

        private void LoadChartData(DateTime? startDate, DateTime? endDate)
        {
            if (_ProcessRoot.AssociatedCalculations.Count > 0)
            {
                Condesus.EMS.Business.PA.Entities.Calculation _calculation = _ProcessRoot.AssociatedCalculations.First().Value;
                EBPAE.CalculationScenarioType _scenarioType = EMSLibrary.User.PerformanceAssessments.Configuration.CalculationScenarioType(Convert.ToInt64(ConfigurationManager.AppSettings["PDDEstimated"]));
                List<Condesus.EMS.Business.PA.Entities.CalculationPoint> _series = _calculation.Series(startDate, endDate);


                FeedChart(_series, _ChartSeriesCalculated);
                FeedChart(_calculation.SeriesVerificated(startDate, endDate), _ChartSeriesVerificated);
                if (_scenarioType != null)
                {
                    FeedChart(_calculation.SeriesForecasted(startDate, endDate, _scenarioType), _ChartSeriesForecasted);
                }
            }
        }

        private void FeedChart(List<Condesus.EMS.Business.PA.Entities.CalculationPoint> series, Series chartSerie)
        {
            chartSerie.Points.Clear();
            chartSerie.Type = SeriesChartType.Line;
            //Int32 _interval = (Int32)(series.Count / 22);
            //_interval = (_interval == 0) ? 1 : _interval;
            //chtIndicator.ChartAreas["AreaData"].AxisX.MajorTickMark.Interval = _interval;
            //chtIndicator.ChartAreas["AreaData"].AxisX.MajorGrid.Interval = _interval;
            //chtIndicator.ChartAreas["AreaData"].AxisX.LabelStyle.Interval = _interval;
            chtIndicator.ChartAreas["AreaData"].AxisX.LabelsAutoFit = true;
            chtIndicator.ChartAreas["AreaData"].AxisY.Title = "tCO2";


            foreach (Condesus.EMS.Business.PA.Entities.CalculationPoint _point in series)
            {
                chartSerie.Points.AddXY(_point.CalculationDate, Convert.ToDouble(_point.CalculationValue));
            }
        }

        #endregion


    }


    public class KCImageTemplate : ITemplate
    {
        protected HtmlImage imgSelButton;
        private string colname;

        public KCImageTemplate(string cName)
        {
            colname = cName;
        }
        public void InstantiateIn(System.Web.UI.Control container)
        {
            imgSelButton = new HtmlImage();
            imgSelButton.ID = "imgOpenFile";
            imgSelButton.SkinID = "DocumentGrid";
            imgSelButton.Alt = "";
            container.Controls.Add(imgSelButton);
        }
    }


}


