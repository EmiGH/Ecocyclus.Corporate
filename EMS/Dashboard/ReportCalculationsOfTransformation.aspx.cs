using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.Threading;
using Telerik.Charting;
using Telerik.Web.UI;
using Condesus.WebUI.Navigation;
using Condesus.EMS.Business.KC.Entities;
using Condesus.EMS.Business.PA.Entities;
using Condesus.EMS.Business.PF.Entities;
using Condesus.EMS.WebUI.Business;

namespace Condesus.EMS.WebUI.Dashboard
{
    public partial class ReportCalculationsOfTransformation : BaseProperties
    {
        #region Internal Properties
            private Int64 _IdProcess;
            private String _ReportType;
            private String _Report;
            private ChartSeriesType _ChartBarType = ChartSeriesType.StackedBar;
            //private ChartSeriesType _ChartBarType// = ChartSeriesType.StackedBar;
            //{
            //    get
            //    {
            //        object o = ViewState["idMeasurement"];
            //        if (o == null)
            //        {
            //            ViewState["ChartBarType"] = ChartSeriesType.StackedBar;
            //        }

            //        return (ChartSeriesType)ViewState["ChartBarType"];
            //    }
            //    set { ViewState["ChartBarType"] = value; }
            //}

            private DateTime? _StartDate;
            private DateTime? _EndDate;
            private RadGrid _rgdMasterGridTransformationMeasurement;
            private RadComboBox _RdcSite;
            private RadTreeView _RtvSite;
            private RadComboBox _RdcOrganization;
            private RadTreeView _RtvOrganization;
            private RadComboBox _RdcAccountingScope;
            private CompareValidator _CvAccountingScope;
            private RadGrid _RgdEvolution;
            private Boolean _ShowOnlyCO2e
            {
                get
                {
                    object _o = ViewState["ShowOnlyCO2e"];
                    if (_o != null)
                        return (Boolean)ViewState["ShowOnlyCO2e"];

                    return false;
                }
                set
                {
                    ViewState["ShowOnlyCO2e"] = value;
                }
            }
            private RadComboBox _RdcScopeGeneral;
        #endregion

        #region Page Load & Init
            protected override void InitializeHandlers()
            {
                base.InitializeHandlers();

                rblOptionChartType.SelectedIndexChanged += new EventHandler(rblOptionChartType_SelectedIndexChanged);
                //lnkExport.Click += new EventHandler(lnkExport_Click);
                //lnkExportGridMeasurement.Click += new EventHandler(lnkExportGridMeasurement_Click);
            }
            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);
                LoadTextLabels();

                String _reportTypeName = String.Empty;
                String _reportName = String.Empty;

                LoadAllParameter();
                AddComboAccountingScope();
                AddComboScopeGeneral();

                //Pone como titulos la organizacion, el scope y si hay fechas que vienen del filtro.
                lblProcessValue.Text = EMSLibrary.User.ProcessFramework.Map.Process(_IdProcess).LanguageOption.Title;
                if (_Report == "GEI")
                {
                    _reportName = Resources.Common.GEI;

                    //Ocultamos las columnas de contaminantes locales que no queremos ver en este caso
                    HCT.Style.Add("display", "none");
                    HCNM.Style.Add("display", "none");
                    C2H6.Style.Add("display", "none");
                    C3H8.Style.Add("display", "none");
                    C4H10.Style.Add("display", "none");
                    CO.Style.Add("display", "none");
                    NOx.Style.Add("display", "none");
                    SOx.Style.Add("display", "none");
                    SO2.Style.Add("display", "none");
                    H2S.Style.Add("display", "none");
                    PM.Style.Add("display", "none");
                    PM10.Style.Add("display", "none");

                    Boolean _showAllColumnReportGEI = true;
                    if (ConfigurationManager.AppSettings["ShowAllColumnReportGEI"] != null)
                    {
                        _showAllColumnReportGEI = Convert.ToBoolean(ConfigurationManager.AppSettings["ShowAllColumnReportGEI"].ToString());
                    }
                    //Si esta configurado para que no salgan todas las columnas de GEI, entonces estas 3 no se muestran...
                    if (!_showAllColumnReportGEI)
                    {
                        //Las columnas no se muestra
                        PFC.Style.Add("display", "none");
                        HFC.Style.Add("display", "none");
                        SF6.Style.Add("display", "none");
                    }

                }
                else
                {
                    _reportName = Resources.Common.CL;

                    //Ocultamos las columnas de GEI que no queremos ver en este caso
                    tCO2e.Style.Add("display", "none");
                    CO2.Style.Add("display", "none");
                    CH4.Style.Add("display", "none");
                    N20.Style.Add("display", "none");
                    PFC.Style.Add("display", "none");
                    HFC.Style.Add("display", "none");
                    SF6.Style.Add("display", "none");
                }

                switch (_ReportType)
                {
                    case "GA-S-A-FT-F":
                        _reportTypeName = Resources.Common.GA_S_A_FT_F;
                        break;
                    case "GA-FT-F-S-A":
                        _reportTypeName = Resources.Common.GA_FT_F_S_A;
                        break;
                    case "S-GA-A-FT-F":
                        _reportTypeName = Resources.Common.S_GA_A_FT_F;
                        break;
                    case "S-A-FT-F":
                        _reportTypeName = Resources.Common.S_A_FT_F;
                        break;
                    case "FT-F-S-A":
                        _reportTypeName = Resources.Common.FT_F_S_A;
                        break;
                    case "O_S_A_FT_F":
                        _reportTypeName = Resources.Common.O_S_A_FT_F;
                        break;
                    case "Evolution":
                        _reportTypeName = Resources.Common.Evolution;
                        break;
                }
                lblReportFilter.Text = _reportName + "<br/>" + _reportTypeName;
                if (_StartDate != null)
                {
                    lblReportFilter.Text += "<br/><br/>" + Resources.CommonListManage.FilterDate + " " 
                        + Resources.CommonListManage.From + " " 
                        + Convert.ToDateTime(_StartDate).ToShortDateString() + " " 
                        + Resources.CommonListManage.Through + " " + Convert.ToDateTime(_EndDate).ToShortDateString();
                }


                //SetSelfHierarchyGrid();

                rtsReportCalculationsOfTransformation.TabClick += new RadTabStripEventHandler(rtsReportCalculationsOfTransformation_TabClick);
                rtsReportCalculationsOfTransformation.OnClientTabSelecting = "ShowLoading";
                rtsReportCalculationsOfTransformation.AutoPostBack = true;

                rtsCharts.TabClick += new RadTabStripEventHandler(rtsCharts_TabClick);
                rtsCharts.OnClientTabSelecting = "ShowLoading";
                rtsCharts.AutoPostBack = true;

                if (_ReportType == "Evolution")
                {
                    AddComboOrganizations();

                    pnlReportEvolution.Attributes.Add("display", "block");
                    pnlReportEvolution.Visible = true;
                    pnlReports.Attributes.Add("display", "none");
                    pnlReports.Visible = false;
                    //lnkExport.Visible = false;
                    lnkExpand.Visible = false;

                    Int64 _idOrganization = 0;
                    if (_RtvOrganization.SelectedNode != null)
                    {
                        _idOrganization = Convert.ToInt64(GetKeyValue(_RtvOrganization.SelectedNode.Value, "IdOrganization"));
                    }
                    Int64 _idScope = Convert.ToInt64(GetKeyValue(_RdcAccountingScope.SelectedValue, "IdScope"));

                    LoadEvolutionReport(_idOrganization, _idScope);
                    LoadChartEvolution(_idOrganization, _idScope);
                    LoadChartEvolutionCO2(_idOrganization, _idScope);
                    LoadChartEvolutionCH4(_idOrganization, _idScope);
                    LoadChartEvolutionN2O(_idOrganization, _idScope);
                }
                else
                {
                    AddComboSites();
                    lnkExport.Visible = true;
                    lnkExpand.Visible = true;
                }

                if (!Page.IsPostBack)
                {
                    if (_Report == "GEI")
                    {
                        //rtsCharts.Tabs[0].Attributes.Add("display", "block");
                        //rtsCharts.Tabs[0].Visible = true;
                        //rtsCharts.SelectedIndex = 0;
                        //rmpCharts.SelectedIndex = 0;
                        //rpvScopes.Selected = true;
                        //rpvScopes.Attributes.Add("display", "block");
                        //rpvActivities.Selected = false;
                    }
                    else
                    {
                        rtsCharts.Tabs[0].Attributes.Add("display", "none");
                        rtsCharts.Tabs[0].Visible = false;
                        rtsCharts.SelectedIndex = 1;
                        rmpCharts.SelectedIndex = 1;
                        rpvScopes.Selected = false;
                        rpvScopes.Attributes.Add("display", "none");
                        rpvActivities.Selected = true;
                    }

                    if (_ReportType == "Evolution")
                    {
                        //pnlReportEvolution.Attributes.Add("display", "block");
                        //pnlReportEvolution.Visible = true;
                        //pnlReports.Attributes.Add("display", "none");
                        //pnlReports.Visible = false;

                        //Int64 _idOrganization = 0;
                        //if (_RtvOrganization.SelectedNode != null)
                        //{
                        //    _idOrganization = Convert.ToInt64(GetKeyValue(_RtvOrganization.SelectedNode.Value, "IdOrganization"));
                        //}
                        //LoadEvolutionReport(_idOrganization);
                        //LoadChartEvolution(_idOrganization);
                    }
                    else
                    {
                        pnlReportEvolution.Attributes.Add("display", "none");
                        pnlReportEvolution.Visible = false;
                        pnlReports.Attributes.Add("display", "block");
                        pnlReports.Visible = true;
                        //Carga la info en el tab de RelatedData seleccionado.(Por defecto la primera vez viene el 1° tab seleccionado)
                        if (rtsReportCalculationsOfTransformation.SelectedTab != null)
                        {
                            LoadDataByTabSelected(rtsReportCalculationsOfTransformation.SelectedTab.Value);
                        }
                    }
                }
            }
            protected override void SetPagetitle()
            {
                try
                {
                    //Busca el nombre de la pagina si es que se lo pasaron. Sino usa el nombre de la entidad.
                    String _pageTitle = base.NavigatorGetTransferVar<String>("PageTitle");
                    if (String.IsNullOrEmpty(_pageTitle))
                    {
                        base.PageTitle = "Report";
                    }
                    else
                    {
                        base.PageTitle = _pageTitle;
                    }
                }
                catch { base.PageTitle = String.Empty; }
            }
            protected override void SetPageTileSubTitle()
            {
                try
                {
                    String _pageSubTitle = base.NavigatorGetTransferVar<String>("PageSubTitle");
                    if (String.IsNullOrEmpty(_pageSubTitle))
                    {
                        base.PageTitleSubTitle = Resources.CommonListManage.lblSubtitle;
                    }
                    else
                    {
                        base.PageTitleSubTitle = _pageSubTitle;
                    }
                }
                catch
                { base.PageTitleSubTitle = String.Empty; }
            }
        #endregion

        #region Private Methods
            private void AddComboSites()
            {
                String _filterExpression = String.Empty;
                //Combo de Organizaciones
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                AddComboTreeSites(ref phFacility, ref _RdcSite, ref _RtvSite, new RadTreeViewEventHandler(rtvSite_NodeExpand));

                _RtvSite.NodeClick += new RadTreeViewEventHandler(_RtvSite_NodeClick);
            }
            private void AddComboOrganizations()
            {
                String _filterExpression = String.Empty;
                //Combo de Organizaciones
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                AddComboWithTreeElementMaps(ref phOrganization, ref _RdcOrganization, ref _RtvOrganization,
                    Common.ConstantsEntitiesName.DS.OrganizationClassifications, Common.ConstantsEntitiesName.DS.OrganizationsRoots, _params, false, true, false, ref _filterExpression,
                    new RadTreeViewEventHandler(rtvOrganizations_NodeExpand),
                    new RadTreeViewEventHandler(rtvOrganizations_NodeClick),
                    Resources.Common.ComboBoxNoDependency, false);
            }
            private void AddComboAccountingScope()
            {
                Dictionary<String, Object> _params = new Dictionary<String, Object>();

                String _selectedValue = String.Empty;
                AddCombo(phAccountingScope, ref _RdcAccountingScope, Common.ConstantsEntitiesName.PA.AccountingScopes, _selectedValue, _params, false, true, false, false, false);

                ValidatorRequiredField(Common.ConstantsEntitiesName.PA.AccountingScopes, phAccountingScopeValidator, ref _CvAccountingScope, _RdcAccountingScope, Resources.ConstantMessage.SelectAAccountingScope);
            }
            private void AddComboScopeGeneral()
            {
                Dictionary<String, Object> _params = new Dictionary<String, Object>();

                String _selectedValue = String.Empty;
                AddCombo(phScopeGeneral, ref _RdcScopeGeneral, Common.ConstantsEntitiesName.PA.AccountingScopes, _selectedValue, _params, false, true, false, true, false);
                _RdcScopeGeneral.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(_RdcScopeGeneral_SelectedIndexChanged);
            }
            private void LoadEvolutionReport(Int64 idOrganization, Int64 idScope)
            {
                phEvolutionGrid.Controls.Clear();

                if (ManageEntityParams.ContainsKey("IdOrganization"))
                {
                    ManageEntityParams.Remove("IdOrganization");
                }
                ManageEntityParams.Add("IdOrganization", idOrganization);
                if (ManageEntityParams.ContainsKey("IdScope"))
                { ManageEntityParams.Remove("IdScope"); }
                ManageEntityParams.Add("IdScope", idScope);

                BuildGenericDataTable("ReportEvolution", ManageEntityParams);

                Boolean _showAllColumnReportGEI = true;
                if (ConfigurationManager.AppSettings["ShowAllColumnReportGEI"] != null)
                {
                    _showAllColumnReportGEI = Convert.ToBoolean(ConfigurationManager.AppSettings["ShowAllColumnReportGEI"].ToString());
                }

                //Si esta el DataTable lo carga en la grilla
                if (DataTableListManage.ContainsKey("ReportEvolution"))
                {
                    _RgdEvolution = base.BuildListManageContent("ReportEvolution", false, false, false, false, false, false);
                    _RgdEvolution.AllowSorting = false;
                    _RgdEvolution.AllowPaging = false;

                    for (int i = 0; i < _RgdEvolution.Columns.Count; i++)
                    {
                        if (_Report == "GEI")
                        {
                            if (_RgdEvolution.Columns[i].UniqueName == "HCT" || _RgdEvolution.Columns[i].UniqueName == "HCNM"
                                || _RgdEvolution.Columns[i].UniqueName == "C2H6" || _RgdEvolution.Columns[i].UniqueName == "C3H8"
                                || _RgdEvolution.Columns[i].UniqueName == "C4H10" || _RgdEvolution.Columns[i].UniqueName == "CO"
                                || _RgdEvolution.Columns[i].UniqueName == "NOx" || _RgdEvolution.Columns[i].UniqueName == "SOx"
                                || _RgdEvolution.Columns[i].UniqueName == "SO2" || _RgdEvolution.Columns[i].UniqueName == "H2S"
                                || _RgdEvolution.Columns[i].UniqueName == "PM" || _RgdEvolution.Columns[i].UniqueName == "PM10"
                                || _RgdEvolution.Columns[i].UniqueName == "HCT_Deviation" || _RgdEvolution.Columns[i].UniqueName == "HCNM_Deviation"
                                || _RgdEvolution.Columns[i].UniqueName == "C2H6_Deviation" || _RgdEvolution.Columns[i].UniqueName == "C3H8_Deviation"
                                || _RgdEvolution.Columns[i].UniqueName == "C4H10_Deviation" || _RgdEvolution.Columns[i].UniqueName == "CO_Deviation"
                                || _RgdEvolution.Columns[i].UniqueName == "NOx_Deviation" || _RgdEvolution.Columns[i].UniqueName == "SOx_Deviation"
                                || _RgdEvolution.Columns[i].UniqueName == "SO2_Deviation" || _RgdEvolution.Columns[i].UniqueName == "H2S_Deviation"
                                || _RgdEvolution.Columns[i].UniqueName == "PM_Deviation" || _RgdEvolution.Columns[i].UniqueName == "PM10_Deviation")
                            {
                                //Las columnas no se muestra
                                _RgdEvolution.Columns[i].Visible = false;
                            }
                            //Si esta configurado para que no salgan todas las columnas de GEI, entonces estas 3 no se muestran...
                            if (!_showAllColumnReportGEI)
                            {
                                if (_RgdEvolution.Columns[i].UniqueName == "PFC" || _RgdEvolution.Columns[i].UniqueName == "HFC"
                                || _RgdEvolution.Columns[i].UniqueName == "SF6" || _RgdEvolution.Columns[i].UniqueName == "PFC_Deviation"
                                || _RgdEvolution.Columns[i].UniqueName == "HFC_Deviation" || _RgdEvolution.Columns[i].UniqueName == "SF6_Deviation")
                                {
                                    //Las columnas no se muestra
                                    _RgdEvolution.Columns[i].Visible = false;
                                }
                            }
                        }
                        else
                        {
                            if (_RgdEvolution.Columns[i].UniqueName == "tCO2e" || _RgdEvolution.Columns[i].UniqueName == "CO2"
                                || _RgdEvolution.Columns[i].UniqueName == "CH4" || _RgdEvolution.Columns[i].UniqueName == "N2O"
                                || _RgdEvolution.Columns[i].UniqueName == "PFC" || _RgdEvolution.Columns[i].UniqueName == "HFC"
                                || _RgdEvolution.Columns[i].UniqueName == "SF6"
                                || _RgdEvolution.Columns[i].UniqueName == "tCO2e_Deviation" || _RgdEvolution.Columns[i].UniqueName == "CO2_Deviation"
                                || _RgdEvolution.Columns[i].UniqueName == "CH4_Deviation" || _RgdEvolution.Columns[i].UniqueName == "N2O_Deviation"
                                || _RgdEvolution.Columns[i].UniqueName == "PFC_Deviation" || _RgdEvolution.Columns[i].UniqueName == "HFC_Deviation"
                                || _RgdEvolution.Columns[i].UniqueName == "SF6_Deviation")
                            {
                                //Las columnas no se muestra
                                _RgdEvolution.Columns[i].Visible = false;
                            }
                        }
                    }
                }
                phEvolutionGrid.Controls.Add(_RgdEvolution);
            }
            private void LoadChartEvolution(Int64 idOrganization, Int64 idScope)
            {
                //Seteo la cultura en USA, para poder trabajar con la fecha en MM/DD/YYYY HH:mm:ss
                CultureInfo _cultureUSA = new CultureInfo("en-US");
                //Me guarda la actual, para luego volver a esta...
                CultureInfo _currentCulture = CultureInfo.CurrentCulture;
                //Seta la cultura estandard
                Thread.CurrentThread.CurrentCulture = _cultureUSA;

                //Setea y arma una paleta de colores para las referencias
                SetChartCustomPalette(chartLineEvolution);

                if (ManageEntityParams.ContainsKey("IdOrganization"))
                { ManageEntityParams.Remove("IdOrganization"); }
                ManageEntityParams.Add("IdOrganization", idOrganization);
                if (ManageEntityParams.ContainsKey("IdScope"))
                { ManageEntityParams.Remove("IdScope"); }
                ManageEntityParams.Add("IdScope", idScope);


                //chartLineEvolution.RemoveAllSeries();
                //Carga el DT
                BuildGenericDataTable(Common.ConstantsEntitiesName.RG.ChartEvolution, ManageEntityParams);
                // Set a query to database.
                chartLineEvolution.DataSource = DataTableListManage[Common.ConstantsEntitiesName.RG.ChartEvolution];
                chartLineEvolution.DataBind();

                // Set additional chart properties and settings.
                chartLineEvolution.ChartTitle.TextBlock.Text = Resources.Common.Evolution;
                chartLineEvolution.ChartTitle.Visible = true;
                chartLineEvolution.SeriesOrientation = ChartSeriesOrientation.Vertical;
                if (chartLineEvolution.Series.Count > 0)
                {
                    //Eliminamos la Serie que toma como año, ya que no sirve!
                    if (chartLineEvolution.Series[0].Name == "Year")
                    {
                        chartLineEvolution.RemoveSeriesAt(0);
                    }
                }
                for (int i = 0; i < chartLineEvolution.Series.Count; i++)
                {
                    if (chartLineEvolution.Series[i].Items.Count == 1)
                    {
                        chartLineEvolution.Series[i].Type = ChartSeriesType.Point;
                    }
                    else
                    {
                        chartLineEvolution.Series[i].Type = ChartSeriesType.Spline;
                    }
                }

                //chartLineEvolution.Legend.Appearance.GroupNameFormat = "#VALUE";

                //Vuelve a la cultura original...
                Thread.CurrentThread.CurrentCulture = _currentCulture;
            }
            private void LoadChartEvolutionCO2(Int64 idOrganization, Int64 idScope)
            {
                //Seteo la cultura en USA, para poder trabajar con la fecha en MM/DD/YYYY HH:mm:ss
                CultureInfo _cultureUSA = new CultureInfo("en-US");
                //Me guarda la actual, para luego volver a esta...
                CultureInfo _currentCulture = CultureInfo.CurrentCulture;
                //Seta la cultura estandard
                Thread.CurrentThread.CurrentCulture = _cultureUSA;

                //Setea y arma una paleta de colores para las referencias
                SetChartCustomPalette(chartLineEvolutionCO2);

                if (ManageEntityParams.ContainsKey("IdOrganization"))
                { ManageEntityParams.Remove("IdOrganization"); }
                ManageEntityParams.Add("IdOrganization", idOrganization);
                if (ManageEntityParams.ContainsKey("IdScope"))
                { ManageEntityParams.Remove("IdScope"); }
                ManageEntityParams.Add("IdScope", idScope);

                //chartLineEvolutionCO2.RemoveAllSeries();
                //Carga el DT
                BuildGenericDataTable(Common.ConstantsEntitiesName.RG.ChartEvolutionCO2, ManageEntityParams);
                // Set a query to database.
                chartLineEvolutionCO2.DataSource = DataTableListManage[Common.ConstantsEntitiesName.RG.ChartEvolutionCO2];
                chartLineEvolutionCO2.DataBind();

                // Set additional chart properties and settings.
                chartLineEvolutionCO2.ChartTitle.TextBlock.Text = Resources.Common.Evolution;
                chartLineEvolutionCO2.ChartTitle.Visible = true;
                chartLineEvolutionCO2.SeriesOrientation = ChartSeriesOrientation.Vertical;
                if (chartLineEvolutionCO2.Series.Count > 0)
                {                //Eliminamos la Serie que toma como año, ya que no sirve!
                    if (chartLineEvolutionCO2.Series[0].Name == "Year")
                    {
                        chartLineEvolutionCO2.RemoveSeriesAt(0);
                    }
                }
                for (int i = 0; i < chartLineEvolutionCO2.Series.Count; i++)
                {
                    if (chartLineEvolutionCO2.Series[i].Items.Count == 1)
                    {
                        chartLineEvolutionCO2.Series[i].Type = ChartSeriesType.Point;
                    }
                    else
                    {
                        chartLineEvolutionCO2.Series[i].Type = ChartSeriesType.Spline;
                    }
                }

                //chartLineEvolution.Legend.Appearance.GroupNameFormat = "#VALUE";

                //Vuelve a la cultura original...
                Thread.CurrentThread.CurrentCulture = _currentCulture;
            }
            private void LoadChartEvolutionCH4(Int64 idOrganization, Int64 idScope)
            {
                //Seteo la cultura en USA, para poder trabajar con la fecha en MM/DD/YYYY HH:mm:ss
                CultureInfo _cultureUSA = new CultureInfo("en-US");
                //Me guarda la actual, para luego volver a esta...
                CultureInfo _currentCulture = CultureInfo.CurrentCulture;
                //Seta la cultura estandard
                Thread.CurrentThread.CurrentCulture = _cultureUSA;

                //Setea y arma una paleta de colores para las referencias
                SetChartCustomPalette(chartLineEvolutionCH4);

                if (ManageEntityParams.ContainsKey("IdOrganization"))
                { ManageEntityParams.Remove("IdOrganization"); }
                ManageEntityParams.Add("IdOrganization", idOrganization);
                if (ManageEntityParams.ContainsKey("IdScope"))
                { ManageEntityParams.Remove("IdScope"); }
                ManageEntityParams.Add("IdScope", idScope);

                //chartLineEvolutionCH4.RemoveAllSeries();
                //Carga el DT
                BuildGenericDataTable(Common.ConstantsEntitiesName.RG.ChartEvolutionCH4, ManageEntityParams);
                // Set a query to database.
                chartLineEvolutionCH4.DataSource = DataTableListManage[Common.ConstantsEntitiesName.RG.ChartEvolutionCH4];
                chartLineEvolutionCH4.DataBind();

                // Set additional chart properties and settings.
                chartLineEvolutionCH4.ChartTitle.TextBlock.Text = Resources.Common.Evolution;
                chartLineEvolutionCH4.ChartTitle.Visible = true;
                chartLineEvolutionCH4.SeriesOrientation = ChartSeriesOrientation.Vertical;
                if (chartLineEvolutionCH4.Series.Count > 0)
                {//Eliminamos la Serie que toma como año, ya que no sirve!
                    if (chartLineEvolutionCH4.Series[0].Name == "Year")
                    {
                        chartLineEvolutionCH4.RemoveSeriesAt(0);
                    }
                }
                for (int i = 0; i < chartLineEvolutionCH4.Series.Count; i++)
                {
                    if (chartLineEvolutionCH4.Series[i].Items.Count == 1)
                    {
                        chartLineEvolutionCH4.Series[i].Type = ChartSeriesType.Point;
                    }
                    else
                    {
                        chartLineEvolutionCH4.Series[i].Type = ChartSeriesType.Spline;
                    }
                }

                //chartLineEvolution.Legend.Appearance.GroupNameFormat = "#VALUE";

                //Vuelve a la cultura original...
                Thread.CurrentThread.CurrentCulture = _currentCulture;
            }
            private void LoadChartEvolutionN2O(Int64 idOrganization, Int64 idScope)
            {
                //Seteo la cultura en USA, para poder trabajar con la fecha en MM/DD/YYYY HH:mm:ss
                CultureInfo _cultureUSA = new CultureInfo("en-US");
                //Me guarda la actual, para luego volver a esta...
                CultureInfo _currentCulture = CultureInfo.CurrentCulture;
                //Seta la cultura estandard
                Thread.CurrentThread.CurrentCulture = _cultureUSA;

                //Setea y arma una paleta de colores para las referencias
                SetChartCustomPalette(chartLineEvolutionN2O);

                if (ManageEntityParams.ContainsKey("IdOrganization"))
                { ManageEntityParams.Remove("IdOrganization"); }
                ManageEntityParams.Add("IdOrganization", idOrganization);
                if (ManageEntityParams.ContainsKey("IdScope"))
                { ManageEntityParams.Remove("IdScope"); }
                ManageEntityParams.Add("IdScope", idScope);

                //chartLineEvolutionN2O.RemoveAllSeries();
                //Carga el DT
                BuildGenericDataTable(Common.ConstantsEntitiesName.RG.ChartEvolutionN2O, ManageEntityParams);
                // Set a query to database.
                chartLineEvolutionN2O.DataSource = DataTableListManage[Common.ConstantsEntitiesName.RG.ChartEvolutionN2O];
                chartLineEvolutionN2O.DataBind();

                // Set additional chart properties and settings.
                chartLineEvolutionN2O.ChartTitle.TextBlock.Text = Resources.Common.Evolution;
                chartLineEvolutionN2O.ChartTitle.Visible = true;
                chartLineEvolutionN2O.SeriesOrientation = ChartSeriesOrientation.Vertical;
                if (chartLineEvolutionN2O.Series.Count > 0)
                {//Eliminamos la Serie que toma como año, ya que no sirve!
                    if (chartLineEvolutionN2O.Series[0].Name == "Year")
                    {
                        chartLineEvolutionN2O.RemoveSeriesAt(0);
                    }
                }

                for (int i = 0; i < chartLineEvolutionN2O.Series.Count; i++)
                {
                    if (chartLineEvolutionN2O.Series[i].Items.Count == 1)
                    {
                        chartLineEvolutionN2O.Series[i].Type = ChartSeriesType.Point;
                    }
                    else
                    {
                        chartLineEvolutionN2O.Series[i].Type = ChartSeriesType.Spline;
                    }
                }

                //chartLineEvolution.Legend.Appearance.GroupNameFormat = "#VALUE";

                //Vuelve a la cultura original...
                Thread.CurrentThread.CurrentCulture = _currentCulture;
            }

            //private void LoadListManage()
            //{
            //    BuildGenericDataTable("ReportAG_S_A_FT_F", ManageEntityParams);

            //    //Si esta el DataTable lo carga en la grilla
            //    if (DataTableListManage.ContainsKey("ReportAG_S_A_FT_F"))
            //    {
            //        rgdHierarchy.DataSource = DataTableListManage["ReportAG_S_A_FT_F"];
            //        rgdHierarchy.Rebind();

            //        //Como no hay registros, deshabilita el tab
            //        if (DataTableListManage["ReportAG_S_A_FT_F"].Rows.Count == 0)
            //        {
            //            rtsReportCalculationsOfTransformation.Enabled = false;
            //        }
            //    }
            //}
            //private void SetSelfHierarchyGrid()
            //{
            //    if (Assembly.GetAssembly(typeof(ScriptManager)).FullName.IndexOf("3.5") != -1)
            //    {
            //        rgdHierarchy.MasterTableView.FilterExpression = @"it[""IdParent""] = Convert.DBNull";
            //    }
            //    else
            //    {
            //        rgdHierarchy.MasterTableView.FilterExpression = "IdParent IS NULL";
            //    }
            //}
            private void LoadTextLabels()
            {
                rtsReportCalculationsOfTransformation.Tabs[0].Text = Resources.CommonListManage.Report;
                rtsReportCalculationsOfTransformation.Tabs[1].Text = Resources.Common.Chart;
                lnkPrint.Text = Resources.Common.Print;
                lnkExpand.Text = Resources.Common.Expand;
                lnkExport.Text = Resources.Common.Export;
                lblModeBar.Text = Resources.Common.ChartSeriesType;
                rtsCharts.Tabs[0].Text = Resources.Common.ChartScopes;
                rtsCharts.Tabs[1].Text = Resources.Common.ChartActivities;
                rtsCharts.Tabs[2].Text = Resources.Common.ChartGeographicalAreas;
                rtsCharts.Tabs[3].Text = Resources.Common.ChartFacilityTypes;
                rtsCharts.Tabs[4].Text = Resources.Common.ChartFacilities;

                rblOptionChartType.Items[0].Text = Resources.Common.ChartSeriesTypeStackedBar;
                rblOptionChartType.Items[1].Text = Resources.Common.ChartSeriesTypeStackedBar100;

                lblFacility.Text = Resources.CommonListManage.Facility;
                lblOrganization.Text = Resources.CommonListManage.Organization;
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
            private void LoadChartPieScopeByIndicator()
            {
                //Setea y arma una paleta de colores para las referencias
                SetChartCustomPalette(chartTotalScopeByIndicator);
                //Carga el DT
                BuildGenericDataTable(Common.ConstantsEntitiesName.RG.ChartPieScopeByIndicator, ManageEntityParams);

                // Set a query to database.
                chartTotalScopeByIndicator.Series[0].DefaultLabelValue = "#Y";
                chartTotalScopeByIndicator.Series[0].Appearance.LegendDisplayMode = ChartSeriesLegendDisplayMode.ItemLabels;
                chartTotalScopeByIndicator.DataSource = DataTableListManage[Common.ConstantsEntitiesName.RG.ChartPieScopeByIndicator];
                chartTotalScopeByIndicator.DataBind();

                // Set additional chart properties and settings.
                chartTotalScopeByIndicator.ChartTitle.TextBlock.Text = Resources.CommonListManage.TotalScopeByIndicator;
                chartTotalScopeByIndicator.ChartTitle.Visible = true;
            }
            private void LoadChartPieScopeByFacility(Int64 idFacility)
            {
                //Seteo la cultura en USA, para poder trabajar con la fecha en MM/DD/YYYY HH:mm:ss
                CultureInfo _cultureUSA = new CultureInfo("en-US");
                //Me guarda la actual, para luego volver a esta...
                CultureInfo _currentCulture = CultureInfo.CurrentCulture;
                //Seta la cultura estandard
                Thread.CurrentThread.CurrentCulture = _cultureUSA;

                //Setea y arma una paleta de colores para las referencias
                //SetChartCustomPalette(chartBarActivityByScope1AndFacility);
                if (ManageEntityParams.ContainsKey("IdFacility"))
                { ManageEntityParams.Remove("IdFacility"); }
                ManageEntityParams.Add("IdFacility", idFacility);

                //Setea y arma una paleta de colores para las referencias
                SetChartCustomPalette(chartTotalScopeByFacility);
                //Carga el DT
                BuildGenericDataTable(Common.ConstantsEntitiesName.RG.ChartPieScopeByFacility, ManageEntityParams);

                // Set a query to database.
                chartTotalScopeByFacility.Series[0].DefaultLabelValue = "#Y";
                chartTotalScopeByFacility.Series[0].Appearance.LegendDisplayMode = ChartSeriesLegendDisplayMode.ItemLabels;
                chartTotalScopeByFacility.DataSource = DataTableListManage[Common.ConstantsEntitiesName.RG.ChartPieScopeByFacility];
                chartTotalScopeByFacility.DataBind();

                // Set additional chart properties and settings.
                chartTotalScopeByFacility.ChartTitle.TextBlock.Text = Resources.CommonListManage.TotalScopeByFacility;
                chartTotalScopeByFacility.ChartTitle.Visible = true;

                //Vuelve a la cultura original...
                Thread.CurrentThread.CurrentCulture = _currentCulture;
            }

            private void LoadChartBarFacilityTypeByScope(Int64 idScope)
            {
                //Setea y arma una paleta de colores para las referencias
                SetChartCustomPalette(chartBarFacilityTypeByScope1);
                if (ManageEntityParams.ContainsKey("IdScope"))
                { ManageEntityParams.Remove("IdScope"); }
                ManageEntityParams.Add("IdScope", idScope);

                //Carga el DT
                if (_Report == "GEI")
                {
                    BuildGenericDataTable(Common.ConstantsEntitiesName.RG.ChartBarFacilityTypeByScopeGEI, ManageEntityParams);
                    // Set a query to database.
                    chartBarFacilityTypeByScope1.DataSource = DataTableListManage[Common.ConstantsEntitiesName.RG.ChartBarFacilityTypeByScopeGEI];
                }
                else
                {
                    BuildGenericDataTable(Common.ConstantsEntitiesName.RG.ChartBarFacilityTypeByScopeCL, ManageEntityParams);
                    // Set a query to database.
                    chartBarFacilityTypeByScope1.DataSource = DataTableListManage[Common.ConstantsEntitiesName.RG.ChartBarFacilityTypeByScopeCL];
                }
                chartBarFacilityTypeByScope1.DataBind();

                if (EMSLibrary.User.PerformanceAssessments.Configuration.AccountingScope(idScope) != null)
                {
                    // Set additional chart properties and settings.
                    chartBarFacilityTypeByScope1.ChartTitle.TextBlock.Text = Resources.CommonListManage.ChartBarGasesFacilityTypeByScope + " " + EMSLibrary.User.PerformanceAssessments.Configuration.AccountingScope(idScope).LanguageOption.Name;
                }
                chartBarFacilityTypeByScope1.ChartTitle.Visible = true;
                chartBarFacilityTypeByScope1.SeriesOrientation = ChartSeriesOrientation.Vertical;
                for (int i = 0; i < chartBarFacilityTypeByScope1.Series.Count; i++)
                {
                    chartBarFacilityTypeByScope1.Series[i].Type = _ChartBarType;    // ChartSeriesType.StackedBar;
                }

                chartBarFacilityTypeByScope1.Legend.Appearance.GroupNameFormat = "#VALUE";
            }

            private void LoadChartBarActivityByScope(Int64 idScope)
            {
                //Seteo la cultura en USA, para poder trabajar con la fecha en MM/DD/YYYY HH:mm:ss
                CultureInfo _cultureUSA = new CultureInfo("en-US");
                //Me guarda la actual, para luego volver a esta...
                CultureInfo _currentCulture = CultureInfo.CurrentCulture;
                //Seta la cultura estandard
                Thread.CurrentThread.CurrentCulture = _cultureUSA;

                //Setea y arma una paleta de colores para las referencias
                SetChartCustomPalette(chartBarActivityByScope1);
                if (ManageEntityParams.ContainsKey("IdScope"))
                { ManageEntityParams.Remove("IdScope"); }
                ManageEntityParams.Add("IdScope", idScope);

                //Carga el DT
                if (_Report == "GEI")
                {   
                    BuildGenericDataTable(Common.ConstantsEntitiesName.RG.ChartBarActivityByScopeGEI, ManageEntityParams);
                    // Set a query to database.
                    chartBarActivityByScope1.DataSource = DataTableListManage[Common.ConstantsEntitiesName.RG.ChartBarActivityByScopeGEI];
                }
                else
                {
                    BuildGenericDataTable(Common.ConstantsEntitiesName.RG.ChartBarActivityByScopeCL, ManageEntityParams);
                    // Set a query to database.
                    chartBarActivityByScope1.DataSource = DataTableListManage[Common.ConstantsEntitiesName.RG.ChartBarActivityByScopeCL];
                }

                chartBarActivityByScope1.DataBind();
                if (EMSLibrary.User.PerformanceAssessments.Configuration.AccountingScope(idScope) != null)
                {
                    // Set additional chart properties and settings.
                    chartBarActivityByScope1.ChartTitle.TextBlock.Text = Resources.CommonListManage.ChartBarGasesActivityByScope + " " + EMSLibrary.User.PerformanceAssessments.Configuration.AccountingScope(idScope).LanguageOption.Name;
                }
                chartBarActivityByScope1.ChartTitle.Visible = true;
                chartBarActivityByScope1.SeriesOrientation = ChartSeriesOrientation.Vertical;
                for (int i = 0; i < chartBarActivityByScope1.Series.Count; i++)
                {
                    chartBarActivityByScope1.Series[i].Type = _ChartBarType;    // ChartSeriesType.StackedBar;
                }

                chartBarActivityByScope1.Legend.Appearance.GroupNameFormat = "#VALUE";

                //Vuelve a la cultura original...
                Thread.CurrentThread.CurrentCulture = _currentCulture;
            }

            private void LoadChartBarStateByScope(Int64 idScope)
            {
                //Setea y arma una paleta de colores para las referencias
                SetChartCustomPalette(chartBarStateByScope1);
                if (ManageEntityParams.ContainsKey("IdScope"))
                { ManageEntityParams.Remove("IdScope"); }
                ManageEntityParams.Add("IdScope", idScope);

                //Carga el DT
                if (_Report == "GEI")
                {
                    BuildGenericDataTable(Common.ConstantsEntitiesName.RG.ChartBarStateByScopeGEI, ManageEntityParams);
                    // Set a query to database.
                    chartBarStateByScope1.DataSource = DataTableListManage[Common.ConstantsEntitiesName.RG.ChartBarStateByScopeGEI];
                }
                else
                {
                    BuildGenericDataTable(Common.ConstantsEntitiesName.RG.ChartBarStateByScopeCL, ManageEntityParams);
                    // Set a query to database.
                    chartBarStateByScope1.DataSource = DataTableListManage[Common.ConstantsEntitiesName.RG.ChartBarStateByScopeCL];
                }

                chartBarStateByScope1.DataBind();

                if (EMSLibrary.User.PerformanceAssessments.Configuration.AccountingScope(idScope) != null)
                {   // Set additional chart properties and settings.
                    chartBarStateByScope1.ChartTitle.TextBlock.Text = Resources.CommonListManage.ChartBarGasesStateByScope + " " + EMSLibrary.User.PerformanceAssessments.Configuration.AccountingScope(idScope).LanguageOption.Name;
                }
                chartBarStateByScope1.ChartTitle.Visible = true;
                chartBarStateByScope1.SeriesOrientation = ChartSeriesOrientation.Vertical;
                for (int i = 0; i < chartBarStateByScope1.Series.Count; i++)
                {
                    chartBarStateByScope1.Series[i].Type = _ChartBarType;    // ChartSeriesType.StackedBar;
                }

                chartBarStateByScope1.Legend.Appearance.GroupNameFormat = "#VALUE";
            }

            private void LoadChartBarActivityByScopeAndFacility(Int64 idScope, Int64 idOrganization, Int64 idFacility)
            {
                //Setea y arma una paleta de colores para las referencias
                SetChartCustomPalette(chartBarActivityByScope1AndFacility);
                if (ManageEntityParams.ContainsKey("IdScope"))
                { ManageEntityParams.Remove("IdScope"); }
                ManageEntityParams.Add("IdScope", idScope);
                
                if (ManageEntityParams.ContainsKey("IdFacility"))
                { ManageEntityParams.Remove("IdFacility"); }
                ManageEntityParams.Add("IdFacility", idFacility);

                //Carga el DT
                if (_Report == "GEI")
                {
                    BuildGenericDataTable(Common.ConstantsEntitiesName.RG.ChartBarActivityByScopeAndFacilityGEI, ManageEntityParams);
                    // Set a query to database.
                    chartBarActivityByScope1AndFacility.DataSource = DataTableListManage[Common.ConstantsEntitiesName.RG.ChartBarActivityByScopeAndFacilityGEI];
                }
                else
                {
                    BuildGenericDataTable(Common.ConstantsEntitiesName.RG.ChartBarActivityByScopeAndFacilityCL, ManageEntityParams);
                    // Set a query to database.
                    chartBarActivityByScope1AndFacility.DataSource = DataTableListManage[Common.ConstantsEntitiesName.RG.ChartBarActivityByScopeAndFacilityCL];
                }

                chartBarActivityByScope1AndFacility.DataBind();

                if (EMSLibrary.User.PerformanceAssessments.Configuration.AccountingScope(idScope) != null)
                {                // Set additional chart properties and settings.
                    chartBarActivityByScope1AndFacility.ChartTitle.TextBlock.Text = Resources.CommonListManage.ChartBarGasesActivityByScope + " " + EMSLibrary.User.PerformanceAssessments.Configuration.AccountingScope(idScope).LanguageOption.Name;
                }
                chartBarActivityByScope1AndFacility.ChartTitle.Visible = true;
                chartBarActivityByScope1AndFacility.SeriesOrientation = ChartSeriesOrientation.Vertical;
                for (int i = 0; i < chartBarActivityByScope1AndFacility.Series.Count; i++)
                {
                    chartBarActivityByScope1AndFacility.Series[i].Type = _ChartBarType;    // ChartSeriesType.StackedBar;
                }

                chartBarActivityByScope1AndFacility.Legend.Appearance.GroupNameFormat = "#VALUE";
            }

            //private void LoadListChartTotalActivityByGas()
            //{
            //    //Setea y arma una paleta de colores para las referencias
            //    SetChartCustomPalette(chartTotalActivityByGas);
            //    //Carga el DT
            //    BuildGenericDataTable(Common.ConstantsEntitiesName.RPT.ChartTransformationTotalActivityByGas, ManageEntityParams);

            //    // Set a query to database.
            //    //chartTotalActivityByGas.Series[0].DefaultLabelValue = "#Y";
            //    //chartTotalActivityByGas.Series[0].Appearance.LegendDisplayMode = ChartSeriesLegendDisplayMode.ItemLabels;
            //    chartTotalActivityByGas.DataSource = DataTableListManage[Common.ConstantsEntitiesName.RPT.ChartTransformationTotalActivityByGas];
            //    chartTotalActivityByGas.DataBind();

            //    // Set additional chart properties and settings.
            //    chartTotalActivityByGas.ChartTitle.TextBlock.Text = Resources.CommonListManage.TotalActivityChartbyGas;
            //    chartTotalActivityByGas.ChartTitle.Visible = true;
            //    chartTotalActivityByGas.SeriesOrientation = ChartSeriesOrientation.Vertical;
            //    //for (int i = 0; i < chartTotalActivityByGas.Series.Count; i++)
            //    //{
            //    //    chartTotalActivityByGas.Series[i].Type = ChartSeriesType.StackedBar;
            //    //}

            //    //chartTotalActivityByGas.DataGroupColumn = "Quarter";
            //    //chartTotalActivityByGas.PlotArea.XAxis.DataLabelsColumn = "Year";
            //    chartTotalActivityByGas.DataGroupColumn = "AccountingActivity";
            //    chartTotalActivityByGas.PlotArea.XAxis.DataLabelsColumn = "Name";
            //    //chartTotalActivityByGas.Legend.Appearance.GroupNameFormat = "#NAME: #VALUE";
            //    chartTotalActivityByGas.Legend.Appearance.GroupNameFormat = "#VALUE";
            //}
            //private void LoadListChartTotaltnCO2ByActivity()
            //{
            //    //Setea y arma una paleta de colores para las referencias
            //    SetChartCustomPalette(chartTotaltnCO2);
            //    //Carga el DT
            //    BuildGenericDataTable(Common.ConstantsEntitiesName.RPT.ChartTransformationTotaltnCO2ByActivity, ManageEntityParams);

            //    // Set a query to database.
            //    chartTotaltnCO2.Series[0].DefaultLabelValue = "#Y";
            //    chartTotaltnCO2.Series[0].Appearance.LegendDisplayMode = ChartSeriesLegendDisplayMode.ItemLabels;
            //    chartTotaltnCO2.DataSource = DataTableListManage[Common.ConstantsEntitiesName.RPT.ChartTransformationTotaltnCO2ByActivity];
            //    chartTotaltnCO2.DataBind();

            //    // Set additional chart properties and settings.
            //    chartTotaltnCO2.ChartTitle.TextBlock.Text = Resources.CommonListManage.TotaltnCO2ChartbyActivity;
            //    chartTotaltnCO2.ChartTitle.Visible = true;
            //}
            //private void LoadGridTransformationMeasurement()
            //{
            //    pnlTransformationMeasurement.Controls.Clear();

            //    BuildGenericDataTable(Common.ConstantsEntitiesName.RPT.ReportTransformationMeasurement, ManageEntityParams);

            //    _rgdMasterGridTransformationMeasurement = base.BuildListManageContent(Common.ConstantsEntitiesName.RPT.ReportTransformationMeasurement, false, false, false, false, false, false);
            //    _rgdMasterGridTransformationMeasurement.AllowPaging = false;
            //    _rgdMasterGridTransformationMeasurement.AllowSorting = false;

            //    pnlTransformationMeasurement.Controls.Add(_rgdMasterGridTransformationMeasurement);
            //}
            private void LoadAllParameter()
            {
                //Carga el DataTable.
                Int64 _idIndicator_tnCO2e;
                Int64 _idIndicator_CO2;
                Int64 _idIndicator_CH4;
                Int64 _idIndicator_N2O;
                Int64 _idIndicator_PFC;
                Int64 _idIndicator_HFC;
                Int64 _idIndicator_SF6;

                Int64 _idIndicator_HCNM;
                Int64 _idIndicator_HCT;
                Int64 _idIndicator_CO;
                Int64 _idIndicator_NOx;
                Int64 _idIndicator_SOx;
                Int64 _idIndicator_MP;
                Int64 _idIndicator_SO2;
                Int64 _idIndicator_H2S;
                Int64 _idIndicator_MP10;
                Int64 _idIndicator_C2H6;
                Int64 _idIndicator_C3H8;
                Int64 _idIndicator_C4H10;

                String _states;

                #region Parameter QueryString
                     if (Request.QueryString["IdProcess"] != null)
                    {
                        _IdProcess = Convert.ToInt64(Request.QueryString["IdProcess"]);
                    }
                    if (Request.QueryString["StartDate"] != null)
                    {
                        if (Request.QueryString["StartDate"] != "")
                        {
                            _StartDate = Convert.ToDateTime(Request.QueryString["StartDate"]);
                        }
                    }
                    if (Request.QueryString["EndDate"] != null)
                    {
                        if (Request.QueryString["StartDate"] != "")
                        {
                            _EndDate = Convert.ToDateTime(Request.QueryString["EndDate"]);
                        }
                    }
                    if (Request.QueryString["ReportType"] != null)
                    {
                        if (Request.QueryString["ReportType"] != "")
                        {
                            _ReportType = Request.QueryString["ReportType"].ToString();
                        }
                    }
                    if (Request.QueryString["Report"] != null)
                    {
                        if (Request.QueryString["Report"] != "")
                        {
                            _Report = Request.QueryString["Report"].ToString();
                        }
                    }
                #endregion

                #region Indicator GAS Config
                    Int64.TryParse(ConfigurationManager.AppSettings["IdIndicator_tnCO2e"], out _idIndicator_tnCO2e);
                    Int64.TryParse(ConfigurationManager.AppSettings["IdIndicator_CO2"], out _idIndicator_CO2);
                    Int64.TryParse(ConfigurationManager.AppSettings["IdIndicator_CH4"], out _idIndicator_CH4);
                    Int64.TryParse(ConfigurationManager.AppSettings["IdIndicator_N2O"], out _idIndicator_N2O);
                    Int64.TryParse(ConfigurationManager.AppSettings["IdIndicator_PFC"], out _idIndicator_PFC);
                    Int64.TryParse(ConfigurationManager.AppSettings["IdIndicator_HFC"], out _idIndicator_HFC);
                    Int64.TryParse(ConfigurationManager.AppSettings["IdIndicator_SF6"], out _idIndicator_SF6);

                    Int64.TryParse(ConfigurationManager.AppSettings["IdIndicator_HCNM"], out _idIndicator_HCNM);
                    Int64.TryParse(ConfigurationManager.AppSettings["IdIndicator_HCT"], out _idIndicator_HCT);
                    Int64.TryParse(ConfigurationManager.AppSettings["IdIndicator_CO"], out _idIndicator_CO);
                    Int64.TryParse(ConfigurationManager.AppSettings["IdIndicator_NOx"], out _idIndicator_NOx);
                    Int64.TryParse(ConfigurationManager.AppSettings["IdIndicator_SOx"], out _idIndicator_SOx);
                    Int64.TryParse(ConfigurationManager.AppSettings["IdIndicator_PM"], out _idIndicator_MP);
                    Int64.TryParse(ConfigurationManager.AppSettings["IdIndicator_SO2"], out _idIndicator_SO2);
                    Int64.TryParse(ConfigurationManager.AppSettings["IdIndicator_H2S"], out _idIndicator_H2S);
                    Int64.TryParse(ConfigurationManager.AppSettings["IdIndicator_PM10"], out _idIndicator_MP10);
                    Int64.TryParse(ConfigurationManager.AppSettings["IdIndicator_C2H6"], out _idIndicator_C2H6);
                    Int64.TryParse(ConfigurationManager.AppSettings["IdIndicator_C3H8"], out _idIndicator_C3H8);
                    Int64.TryParse(ConfigurationManager.AppSettings["IdIndicator_C4H10"], out _idIndicator_C4H10);

                #endregion

                #region State of Geographic Area Config
                    _states = ConfigurationManager.AppSettings["States"].ToString();
                #endregion

                #region Add Manage Entity Params
                    if (ManageEntityParams.ContainsKey("States"))
                    { ManageEntityParams.Remove("States"); }
                    ManageEntityParams.Add("States", _states);

                    if (ManageEntityParams.ContainsKey("IdProcess"))
                    { ManageEntityParams.Remove("IdProcess"); }
                    ManageEntityParams.Add("IdProcess", _IdProcess);

                    if (ManageEntityParams.ContainsKey("IdIndicator_tnCO2e"))
                    { ManageEntityParams.Remove("IdIndicator_tnCO2e"); }
                    ManageEntityParams.Add("IdIndicator_tnCO2e", _idIndicator_tnCO2e);

                    if (ManageEntityParams.ContainsKey("IdIndicator_CO2"))
                    { ManageEntityParams.Remove("IdIndicator_CO2"); }
                    ManageEntityParams.Add("IdIndicator_CO2", _idIndicator_CO2);

                    if (ManageEntityParams.ContainsKey("IdIndicator_CH4"))
                    { ManageEntityParams.Remove("IdIndicator_CH4"); }
                    ManageEntityParams.Add("IdIndicator_CH4", _idIndicator_CH4);

                    if (ManageEntityParams.ContainsKey("IdIndicator_N2O"))
                    { ManageEntityParams.Remove("IdIndicator_N2O"); }
                    ManageEntityParams.Add("IdIndicator_N2O", _idIndicator_N2O);

                    if (ManageEntityParams.ContainsKey("IdIndicator_PFC"))
                    { ManageEntityParams.Remove("IdIndicator_PFC"); }
                    ManageEntityParams.Add("IdIndicator_PFC", _idIndicator_PFC);

                    if (ManageEntityParams.ContainsKey("IdIndicator_HFC"))
                    { ManageEntityParams.Remove("IdIndicator_HFC"); }
                    ManageEntityParams.Add("IdIndicator_HFC", _idIndicator_HFC);

                    if (ManageEntityParams.ContainsKey("IdIndicator_SF6"))
                    { ManageEntityParams.Remove("IdIndicator_SF6"); }
                    ManageEntityParams.Add("IdIndicator_SF6", _idIndicator_SF6);

                    if (ManageEntityParams.ContainsKey("IdIndicator_HCNM"))
                    { ManageEntityParams.Remove("IdIndicator_HCNM"); }
                    ManageEntityParams.Add("IdIndicator_HCNM", _idIndicator_HCNM);

                    if (ManageEntityParams.ContainsKey("IdIndicator_HCT"))
                    { ManageEntityParams.Remove("IdIndicator_HCT"); }
                    ManageEntityParams.Add("IdIndicator_HCT", _idIndicator_HCT);

                    if (ManageEntityParams.ContainsKey("IdIndicator_CO"))
                    { ManageEntityParams.Remove("IdIndicator_CO"); }
                    ManageEntityParams.Add("IdIndicator_CO", _idIndicator_CO);

                    if (ManageEntityParams.ContainsKey("IdIndicator_NOx"))
                    { ManageEntityParams.Remove("IdIndicator_NOx"); }
                    ManageEntityParams.Add("IdIndicator_NOx", _idIndicator_NOx);

                    if (ManageEntityParams.ContainsKey("IdIndicator_SOx"))
                    { ManageEntityParams.Remove("IdIndicator_SOx"); }
                    ManageEntityParams.Add("IdIndicator_SOx", _idIndicator_SOx);

                    if (ManageEntityParams.ContainsKey("IdIndicator_PM"))
                    { ManageEntityParams.Remove("IdIndicator_PM"); }
                    ManageEntityParams.Add("IdIndicator_PM", _idIndicator_MP);

                    if (ManageEntityParams.ContainsKey("IdIndicator_SO2"))
                    { ManageEntityParams.Remove("IdIndicator_SO2"); }
                    ManageEntityParams.Add("IdIndicator_SO2", _idIndicator_SO2);

                    if (ManageEntityParams.ContainsKey("IdIndicator_H2S"))
                    { ManageEntityParams.Remove("IdIndicator_H2S"); }
                    ManageEntityParams.Add("IdIndicator_H2S", _idIndicator_H2S);

                    if (ManageEntityParams.ContainsKey("IdIndicator_PM10"))
                    { ManageEntityParams.Remove("IdIndicator_PM10"); }
                    ManageEntityParams.Add("IdIndicator_PM10", _idIndicator_MP10);

                    if (ManageEntityParams.ContainsKey("IdIndicator_C2H6"))
                    { ManageEntityParams.Remove("IdIndicator_C2H6"); }
                    ManageEntityParams.Add("IdIndicator_C2H6", _idIndicator_C2H6);

                    if (ManageEntityParams.ContainsKey("IdIndicator_C3H8"))
                    { ManageEntityParams.Remove("IdIndicator_C3H8"); }
                    ManageEntityParams.Add("IdIndicator_C3H8", _idIndicator_C3H8);

                    if (ManageEntityParams.ContainsKey("IdIndicator_C4H10"))
                    { ManageEntityParams.Remove("IdIndicator_C4H10"); }
                    ManageEntityParams.Add("IdIndicator_C4H10", _idIndicator_C4H10);

                    if (ManageEntityParams.ContainsKey("StartDate"))
                    { ManageEntityParams.Remove("StartDate"); }
                    ManageEntityParams.Add("StartDate", _StartDate);

                    if (ManageEntityParams.ContainsKey("EndDate"))
                    { ManageEntityParams.Remove("EndDate"); }
                    ManageEntityParams.Add("EndDate", _EndDate);

                #endregion
            }
            private void RegisterCustomMenuPanels()
            {
                List<String> _menuPanels = new List<String>();
                _menuPanels.Add(Common.Constants.ContextInformationKey);
                _menuPanels.Add(Common.Constants.ContextElementMapsKey);

                FwMasterPage.ContentNavigatorCustomMenuPanels(_menuPanels);
            }
            private void HideExpandColumnRecursive(GridTableView tableView)
            {
                try
                {
                    GridItem[] nestedViewItems = tableView.GetItems(GridItemType.NestedView);
                    foreach (GridNestedViewItem nestedViewItem in nestedViewItems)
                    {
                        foreach (GridTableView nestedView in nestedViewItem.NestedTableViews)
                        {
                            if (nestedView.Items.Count == 0)
                            {

                                TableCell cell = nestedView.ParentItem["ExpandColumn"];
                                cell.CssClass = "ExpandColumn";
                                cell.Controls[0].Visible = false;
                                nestedViewItem.Visible = false;
                            }
                            if (nestedView.HasDetailTables)
                            {
                                HideExpandColumnRecursive(nestedView);
                                TableCell cell = nestedView.ParentItem["ExpandColumn"];
                                cell.CssClass = "ExpandColumn";
                            }
                        }
                    }
                }
                catch { }
            }
            private void LoadDataByTabSelected(String tabValue)
            {
                //Dependiendo el tab seleccionado, carga su informacion.
                switch (tabValue)
                {
                    case "Grid":
                        //LoadListManage();
                        switch (_ReportType)
                        {
                            case "GA-S-A-FT-F":
                                LoadGridReport_GA_S_A_FT_F();
                                break;
                            case "GA-FT-F-S-A":
                                LoadGridReport_GA_FT_F_S_A();
                                break;
                            case "S-GA-A-FT-F":
                                LoadGridReport_S_GA_A_FT_F();
                                break;

                            case "S-A-FT-F":
                                LoadGridReport_S_A_FT_F();
                                break;

                            case "FT-F-S-A":
                                LoadGridReport_FT_F_S_A();
                                break;
                            case "O_S_A_FT_F":
                                LoadGridReport_O_S_A_FT_F();
                                break;
                        }
                        SetCssInHtmlTable(tblTreeTableReport);
                        lnkExpand.Style.Add("display", "block");
                        //pnlOptionChartType.Style.Add("display", "none");
                        pnlOptionChartType.CssClass = "pnlOptionChartTypeNone";
                        lnkExport.Style.Add("display", "block");
                        //lnkExportGridMeasurement.Style.Add("display", "none");
                        break;

                    case "Charts":
                        //Seteo la cultura en USA, para poder trabajar con la fecha en MM/DD/YYYY HH:mm:ss
                        CultureInfo _cultureUSA = new CultureInfo("en-US");
                        //Me guarda la actual, para luego volver a esta...
                        CultureInfo _currentCulture = CultureInfo.CurrentCulture;
                        //Seta la cultura estandard
                        Thread.CurrentThread.CurrentCulture = _cultureUSA;

                        if (_Report == "GEI")
                        {
                            //LoadChartPieScopeByIndicator();
                            LoadDataChartByTabSelected("Scopes");
                        }
                        else
                        {
                            LoadDataChartByTabSelected("Activities");
                        }
                        //SAca el link de Expand que pertenece al repote
                        lnkExpand.Style.Add("display", "none");
                        lnkExport.Attributes.Add("display", "none");

                        //Vuelve a la cultura original...
                        Thread.CurrentThread.CurrentCulture = _currentCulture;

                        break;
                }
            }
            private void LoadDataChartByTabSelected(String tabValue)
            {
                Int64 _idScope = Convert.ToInt64(GetKeyValue(_RdcScopeGeneral.SelectedValue, "IdScope"));

                _ChartBarType = ChartSeriesType.StackedBar;
                //pnlOptionChartType.Style.Add("display", "block");
                pnlOptionChartType.CssClass = "pnlOptionChartType";
                //Dependiendo el tab seleccionado, carga su informacion.
                switch (tabValue)
                {
                    case "Scopes":
                        LoadChartPieScopeByIndicator();
                        //pnlOptionChartType.Style.Add("display", "none");
                        pnlOptionChartType.CssClass = "pnlOptionChartTypeNone";
                        break;
                    case "FacilityTypes":
                        LoadChartBarFacilityTypeByScope(_idScope);
                        //trOptionFacility.Style.Add("display", "none");
                        break;
                    case "Activities":
                        LoadChartBarActivityByScope(_idScope);
                        //trOptionFacility.Style.Add("display", "none");
                        break;
                    case "GeographicalAreas":
                        LoadChartBarStateByScope(_idScope);
                        //trOptionFacility.Style.Add("display", "none");
                        break;
                    case "Facilities":
                        if (EMSLibrary.User.PerformanceAssessments.Configuration.AccountingScope(_idScope) != null)
                        {   //No hace nada porque espera seleccion del combo!!...
                            chartBarActivityByScope1AndFacility.ChartTitle.TextBlock.Text = Resources.CommonListManage.ChartBarGasesActivityByScope + " " + EMSLibrary.User.PerformanceAssessments.Configuration.AccountingScope(_idScope).LanguageOption.Name + " - " + Resources.CommonListManage.Facility;
                        }
                        chartBarActivityByScope1AndFacility.ChartTitle.Visible = true;

                        chartTotalScopeByFacility.ChartTitle.TextBlock.Text = Resources.CommonListManage.TotalScopeByFacility;
                        chartTotalScopeByFacility.ChartTitle.Visible = true;
                        
                        //trOptionFacility.Style.Add("display", "");

                        break;
                }
                //SAca el link de Expand que pertenece al repote
                lnkExpand.Style.Add("display", "none");
                lnkExport.Style.Add("display", "none");
            }
            private void ConfigureExport(RadGrid grid)
            {
                grid.ExportSettings.ExportOnlyData = true;
                grid.ExportSettings.IgnorePaging = true;
                grid.ExportSettings.OpenInNewWindow = true;
            }
            private void SetCssInHtmlTable(HtmlTable tblContentForm)
            {
                String _css = "trPar";

                foreach (HtmlTableRow _tr in tblContentForm.Rows)
                {
                    _tr.Attributes["class"] = _tr.Attributes["class"] + " " + _css;
                    AlternateRowClassHtmlTable(ref _css);
                }
            }
            private void AlternateRowClassHtmlTable(ref String cssClass)
            {
                cssClass = (cssClass == "trPar") ? "trImpar" : "trPar";
            }

            private Boolean ValidateRowCeroByReport(String result_tCO2e, String result_CO2, String result_CH4, String result_N2O, String result_PFC,
                    String result_HFC, String result_SF6, String result_HCT, String result_HCNM, String result_C2H6,
                    String result_C3H8, String result_C4H10, String result_CO, String result_NOx, String result_SOx,
                    String result_SO2, String result_H2S, String result_PM, String result_PM10)
            {
                //if (_Report == "GEI")
                //{
                //    if ((result_tCO2e != "0.") || (result_CO2 != "0.") ||
                //    (result_CH4 != "0.") || (result_N2O != "0.") ||
                //    (result_PFC != "0.") || (result_HFC != "0.") ||
                //    (result_SF6 != "0."))
                //    {
                //        return true;
                //    }
                //}
                //else
                //{
                //    if ((result_HCT != "0.") ||
                //    (result_HCNM != "0.") || (result_C2H6 != "0.") ||
                //    (result_C3H8 != "0.") || (result_C4H10 != "0.") ||
                //    (result_CO != "0.") || (result_NOx != "0.") ||
                //    (result_SOx != "0.") || (result_SO2 != "0.") ||
                //    (result_H2S != "0.") || (result_PM != "0.") ||
                //    (result_PM10 != "0."))
                //    {
                //        return true;
                //    }
                //}

                //return false;
                return true;
            }
            private void AddTableRow(Int64 id, Int64? idParent, String title,
                    String result_tCO2e, String result_CO2, String result_CH4, String result_N2O, String result_PFC,
                    String result_HFC, String result_SF6, String result_HCT, String result_HCNM, String result_C2H6,
                    String result_C3H8, String result_C4H10, String result_CO, String result_NOx, String result_SOx,
                    String result_SO2, String result_H2S, String result_PM, String result_PM10)
            {

                //if ((result_tCO2e != "0.") || (result_CO2 != "0.") ||
                    //(result_CH4 != "0.") || (result_N2O != "0.") ||
                    //(result_PFC != "0.") || (result_HFC != "0.") ||
                    //(result_SF6 != "0.") || (result_HCT != "0.") ||
                    //(result_HCNM != "0.") || (result_C2H6 != "0.") ||
                    //(result_C3H8 != "0.") || (result_C4H10 != "0.") ||
                    //(result_CO != "0.") || (result_NOx != "0.") ||
                    //(result_SOx != "0.") || (result_SO2 != "0.") ||
                    //(result_H2S != "0.") || (result_PM != "0.") ||
                    //(result_PM10 != "0."))

                if (ValidateRowCeroByReport(result_tCO2e, result_CO2, result_CH4, result_N2O, result_PFC, result_HFC, result_SF6, result_HCT, result_HCNM, 
                    result_C2H6, result_C3H8, result_C4H10, result_CO, result_NOx, result_SOx, result_SO2, result_H2S, result_PM, result_PM10))
                {
                    const String prefixNODE = "node_";
                    const String prefixNODE_CHILD = "child-of-ctl00_ContentPopUp_node_";

                    //Creamos 1 Registro en la tabla
                    HtmlTableRow _tr = new HtmlTableRow();
                    HtmlTableCell _td = new HtmlTableCell();
                    Label _lblCaption = new Label();

                    //Indica el Identificador del registro
                    _tr.ID = prefixNODE + id.ToString();
                    //Si viene un parent, quiere decir que es un hijo...y hay que poner este class...
                    if (idParent != null)
                    {
                        _tr.Attributes.Add("class", prefixNODE_CHILD + idParent.ToString());
                    }
                    //_tr.Attributes.Add("onclick", "javascript:SelectingRow(this);");

                    //Columna del Check
                    //CheckBox _chkRow = new CheckBox();
                    //_chkRow.Attributes.Add("onclick", "javascript:SelectingRow(this);");
                    //_td = new HtmlTableCell();
                    //_td.Attributes.Add("class", "DCGEI");
                    //_td.Controls.Add(_chkRow);
                    //_tr.Cells.Add(_td);

                    //Carga las columnas
                    //Columna Titulo
                    _lblCaption.Text = title;
                    _td.Controls.Add(_lblCaption);
                    _tr.Cells.Add(_td);

                    if (_Report == "GEI")
                    {
                        //Ocultamos las columnas de contaminantes locales que no queremos ver en este caso
                        //HCT.Style.Add("display", "none");
                        //HCNM.Style.Add("display", "none");
                        //C2H6.Style.Add("display", "none");
                        //C3H8.Style.Add("display", "none");
                        //C4H10.Style.Add("display", "none");
                        //CO.Style.Add("display", "none");
                        //NOx.Style.Add("display", "none");
                        //SOx.Style.Add("display", "none");
                        //SO2.Style.Add("display", "none");
                        //H2S.Style.Add("display", "none");
                        //PM.Style.Add("display", "none");
                        //PM10.Style.Add("display", "none");

                        HCT.Visible = false;
                        HCNM.Visible = false;
                        C2H6.Visible = false;
                        C3H8.Visible = false;
                        C4H10.Visible = false;
                        CO.Visible = false;
                        NOx.Visible = false;
                        SOx.Visible = false;
                        SO2.Visible = false;
                        H2S.Visible = false;
                        PM.Visible = false;
                        PM10.Visible = false;

                        //Columna tCO2e
                        _lblCaption = new Label();
                        _lblCaption.Text = result_tCO2e == "0." ? "0" : result_tCO2e;
                        _td = new HtmlTableCell();
                        _td.Attributes.Add("class", "DCGEI");
                        _td.Controls.Add(_lblCaption);
                        _tr.Cells.Add(_td);

                        //Columna CO2
                        _lblCaption = new Label();
                        _lblCaption.Text = result_CO2 == "0." ? "0" : result_CO2;
                        _td = new HtmlTableCell();
                        _td.Attributes.Add("class", "DCGEI");
                        _td.Controls.Add(_lblCaption);
                        _tr.Cells.Add(_td);

                        //Columna CH4
                        _lblCaption = new Label();
                        _lblCaption.Text = result_CH4 == "0." ? "0" : result_CH4;
                        _td = new HtmlTableCell();
                        _td.Attributes.Add("class", "DCGEI");
                        _td.Controls.Add(_lblCaption);
                        _tr.Cells.Add(_td);

                        //Columna N2O
                        _lblCaption = new Label();
                        _lblCaption.Text = result_N2O == "0." ? "0" : result_N2O;
                        _td = new HtmlTableCell();
                        _td.Attributes.Add("class", "DCGEI");
                        _td.Controls.Add(_lblCaption);
                        _tr.Cells.Add(_td);

                        Boolean _showAllColumnReportGEI = true;
                        if (ConfigurationManager.AppSettings["ShowAllColumnReportGEI"] != null)
                        {
                            _showAllColumnReportGEI = Convert.ToBoolean(ConfigurationManager.AppSettings["ShowAllColumnReportGEI"].ToString());
                        }
                        //Si esta configurado para que no salgan todas las columnas de GEI, entonces estas 3 no se muestran...
                        if (!_showAllColumnReportGEI)
                        {
                            //Las columnas no se muestra
                            PFC.Visible = false;
                            HFC.Visible = false;
                            SF6.Visible = false;
                        }
                        else
                        {
                            //Columna PFC
                            _lblCaption = new Label();
                            _lblCaption.Text = result_PFC == "0." ? "0" : result_PFC;
                            _td = new HtmlTableCell();
                            _td.Attributes.Add("class", "DCGEI");
                            _td.Controls.Add(_lblCaption);
                            _tr.Cells.Add(_td);

                            //Columna HFC
                            _lblCaption = new Label();
                            _lblCaption.Text = result_HFC == "0." ? "0" : result_HFC;
                            _td = new HtmlTableCell();
                            _td.Attributes.Add("class", "DCGEI");
                            _td.Controls.Add(_lblCaption);
                            _tr.Cells.Add(_td);

                            //Columna SF6
                            _lblCaption = new Label();
                            _lblCaption.Text = result_SF6 == "0." ? "0" : result_SF6;
                            _td = new HtmlTableCell();
                            _td.Attributes.Add("class", "DCGEI");
                            _td.Controls.Add(_lblCaption);
                            _tr.Cells.Add(_td);
                        }
                    }
                    else
                    {
                        //Ocultamos las columnas de GEI que no queremos ver en este caso
                        //tCO2e.Style.Add("display", "none");
                        //CO2.Style.Add("display", "none");
                        //CH4.Style.Add("display", "none");
                        //N20.Style.Add("display", "none");
                        //PFC.Style.Add("display", "none");
                        //HFC.Style.Add("display", "none");
                        //SF6.Style.Add("display", "none");

                        tCO2e.Visible = false;
                        CO2.Visible = false;
                        CH4.Visible = false;
                        N20.Visible = false;
                        PFC.Visible = false;
                        HFC.Visible = false;
                        SF6.Visible = false;

                        //Columna HCT
                        _lblCaption = new Label();
                        _lblCaption.Text = result_HCT == "0." ? "0" : result_HCT;
                        _td = new HtmlTableCell();
                        _td.Attributes.Add("class", "DCCL");
                        _td.Controls.Add(_lblCaption);
                        _tr.Cells.Add(_td);

                        //Columna HCNM
                        _lblCaption = new Label();
                        _lblCaption.Text = result_HCNM == "0." ? "0" : result_HCNM;
                        _td = new HtmlTableCell();
                        _td.Attributes.Add("class", "DCCL");
                        _td.Controls.Add(_lblCaption);
                        _tr.Cells.Add(_td);

                        //Columna C2H6
                        _lblCaption = new Label();
                        _lblCaption.Text = result_C2H6 == "0." ? "0" : result_C2H6;
                        _td = new HtmlTableCell();
                        _td.Attributes.Add("class", "DCCL");
                        _td.Controls.Add(_lblCaption);
                        _tr.Cells.Add(_td);

                        //Columna C3H8
                        _lblCaption = new Label();
                        _lblCaption.Text = result_C3H8 == "0." ? "0" : result_C3H8;
                        _td = new HtmlTableCell();
                        _td.Attributes.Add("class", "DCCL");
                        _td.Controls.Add(_lblCaption);
                        _tr.Cells.Add(_td);

                        //Columna C4H10
                        _lblCaption = new Label();
                        _lblCaption.Text = result_C4H10 == "0." ? "0" : result_C4H10;
                        _td = new HtmlTableCell();
                        _td.Attributes.Add("class", "DCCL");
                        _td.Controls.Add(_lblCaption);
                        _tr.Cells.Add(_td);

                        //Columna CO
                        _lblCaption = new Label();
                        _lblCaption.Text = result_CO == "0." ? "0" : result_CO;
                        _td = new HtmlTableCell();
                        _td.Attributes.Add("class", "DCCL");
                        _td.Controls.Add(_lblCaption);
                        _tr.Cells.Add(_td);

                        //Columna NOx
                        _lblCaption = new Label();
                        _lblCaption.Text = result_NOx == "0." ? "0" : result_NOx;
                        _td = new HtmlTableCell();
                        _td.Attributes.Add("class", "DCCL");
                        _td.Controls.Add(_lblCaption);
                        _tr.Cells.Add(_td);

                        //Columna SOx
                        _lblCaption = new Label();
                        _lblCaption.Text = result_SOx == "0." ? "0" : result_SOx;
                        _td = new HtmlTableCell();
                        _td.Attributes.Add("class", "DCCL");
                        _td.Controls.Add(_lblCaption);
                        _tr.Cells.Add(_td);

                        //Columna SO2
                        _lblCaption = new Label();
                        _lblCaption.Text = result_SO2 == "0." ? "0" : result_SO2;
                        _td = new HtmlTableCell();
                        _td.Attributes.Add("class", "DCCL");
                        _td.Controls.Add(_lblCaption);
                        _tr.Cells.Add(_td);

                        //Columna H2S
                        _lblCaption = new Label();
                        _lblCaption.Text = result_H2S == "0." ? "0" : result_H2S;
                        _td = new HtmlTableCell();
                        _td.Attributes.Add("class", "DCCL");
                        _td.Controls.Add(_lblCaption);
                        _tr.Cells.Add(_td);

                        //Columna PM
                        _lblCaption = new Label();
                        _lblCaption.Text = result_PM == "0." ? "0" : result_PM;
                        _td = new HtmlTableCell();
                        _td.Attributes.Add("class", "DCCL");
                        _td.Controls.Add(_lblCaption);
                        _tr.Cells.Add(_td);

                        //Columna PM10
                        _lblCaption = new Label();
                        _lblCaption.Text = result_PM10 == "0." ? "0" : result_PM10;
                        _td = new HtmlTableCell();
                        _td.Attributes.Add("class", "DCCL");
                        _td.Controls.Add(_lblCaption);
                        _tr.Cells.Add(_td);
                    }

                    //Insertamos el Registro en la Tabla
                    tblTreeTableReport.Controls.Add(_tr);
                }
            }

            #region Reporte GA_S_A_FT_F
                private void LoadGridReport_GA_S_A_FT_F()
                {
                    DateTime _startDate = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;
                    DateTime _endDate = (DateTime)System.Data.SqlTypes.SqlDateTime.MaxValue;
                    if (ManageEntityParams["StartDate"] != null)
                    { _startDate = Convert.ToDateTime(ManageEntityParams["StartDate"]); }
                    if (ManageEntityParams["EndDate"] != null)
                    { _endDate = Convert.ToDateTime(ManageEntityParams["EndDate"]); }

                    Int64 _idProcess = Convert.ToInt64(ManageEntityParams["IdProcess"]);
                    Int64 _idIndicator_tnCO2e = Convert.ToInt64(ManageEntityParams["IdIndicator_tnCO2e"]);
                    Int64 _idIndicator_CO2 = Convert.ToInt64(ManageEntityParams["IdIndicator_CO2"]);
                    Int64 _idIndicator_CH4 = Convert.ToInt64(ManageEntityParams["IdIndicator_CH4"]);
                    Int64 _idIndicator_N2O = Convert.ToInt64(ManageEntityParams["IdIndicator_N2O"]);
                    Int64 _idIndicator_PFC = Convert.ToInt64(ManageEntityParams["IdIndicator_PFC"]);
                    Int64 _idIndicator_HFC = Convert.ToInt64(ManageEntityParams["IdIndicator_HFC"]);
                    Int64 _idIndicator_SF6 = Convert.ToInt64(ManageEntityParams["IdIndicator_SF6"]);
                    Int64 _idIndicator_HCNM = Convert.ToInt64(ManageEntityParams["IdIndicator_HCNM"]);
                    Int64 _idIndicator_HCT = Convert.ToInt64(ManageEntityParams["IdIndicator_HCT"]);
                    Int64 _idIndicator_CO = Convert.ToInt64(ManageEntityParams["IdIndicator_CO"]);
                    Int64 _idIndicator_NOx = Convert.ToInt64(ManageEntityParams["IdIndicator_NOx"]);
                    Int64 _idIndicator_SOx = Convert.ToInt64(ManageEntityParams["IdIndicator_SOx"]);
                    Int64 _idIndicator_PM = Convert.ToInt64(ManageEntityParams["IdIndicator_PM"]);
                    Int64 _idIndicator_SO2 = Convert.ToInt64(ManageEntityParams["IdIndicator_SO2"]);
                    Int64 _idIndicator_H2S = Convert.ToInt64(ManageEntityParams["IdIndicator_H2S"]);
                    Int64 _idIndicator_PM10 = Convert.ToInt64(ManageEntityParams["IdIndicator_PM10"]);
                    Int64 _idIndicator_C2H6 = Convert.ToInt64(ManageEntityParams["IdIndicator_C2H6"]);
                    Int64 _idIndicator_C3H8 = Convert.ToInt64(ManageEntityParams["IdIndicator_C3H8"]);
                    Int64 _idIndicator_C4H10 = Convert.ToInt64(ManageEntityParams["IdIndicator_C4H10"]);

                    
                    List<Condesus.EMS.Business.RG.IColumnsReport> _listGA = ((ProcessGroupProcess)EMSLibrary.User.ProcessFramework.Map.Process(_idProcess)).Report_GA_S_A_FT_F.GA(_idIndicator_tnCO2e, _idIndicator_CO2,
                        _idIndicator_CH4, _idIndicator_N2O, _idIndicator_PFC, _idIndicator_HFC, _idIndicator_SF6, _idIndicator_HCT,
                        _idIndicator_HCNM, _idIndicator_C2H6, _idIndicator_C3H8, _idIndicator_C4H10, _idIndicator_CO, _idIndicator_NOx,
                        _idIndicator_SOx, _idIndicator_SO2, _idIndicator_H2S, _idIndicator_PM, _idIndicator_PM10, _startDate, _endDate);

                    Int64 _id = 1;
                    
                    //Empezamos con los ROOT de Area Geografica
                    foreach (Condesus.EMS.Business.RG.IColumnsReport _icolumns in _listGA)
                    {
                        AddTableRow(_id, null, _icolumns.Name, Common.Functions.CustomEMSRound(_icolumns.Result_tCO2e),
                            Common.Functions.CustomEMSRound(_icolumns.Result_CO2),
                            Common.Functions.CustomEMSRound(_icolumns.Result_CH4),
                            Common.Functions.CustomEMSRound(_icolumns.Result_N2O),
                            Common.Functions.CustomEMSRound(_icolumns.Result_PFC),
                            Common.Functions.CustomEMSRound(_icolumns.Result_HFC),
                            Common.Functions.CustomEMSRound(_icolumns.Result_SF6),
                            Common.Functions.CustomEMSRound(_icolumns.Result_HCT),
                            Common.Functions.CustomEMSRound(_icolumns.Result_HCNM),
                            Common.Functions.CustomEMSRound(_icolumns.Result_C2H6),
                            Common.Functions.CustomEMSRound(_icolumns.Result_C3H8),
                            Common.Functions.CustomEMSRound(_icolumns.Result_C4H10),
                            Common.Functions.CustomEMSRound(_icolumns.Result_CO),
                            Common.Functions.CustomEMSRound(_icolumns.Result_NOx),
                            Common.Functions.CustomEMSRound(_icolumns.Result_SOx),
                            Common.Functions.CustomEMSRound(_icolumns.Result_SO2),
                            Common.Functions.CustomEMSRound(_icolumns.Result_H2S),
                            Common.Functions.CustomEMSRound(_icolumns.Result_PM),
                            Common.Functions.CustomEMSRound(_icolumns.Result_PM10));

                        Int64 _idParent = _id;
                        _id++;
                        
                        //Cargo el Scope de esta Area Geografica
                        GA_S_A_FT_F_GetScopes(_icolumns, _idParent, ref _id);

                        //Ahora le pido los hijos de esta Area Geografica
                        GA_S_A_FT_F_GetGeographicAreaChild(_icolumns, ref _id, _idParent);

                    }
                }
                private void GA_S_A_FT_F_GetScopes(Condesus.EMS.Business.RG.IColumnsReport geoArea, Int64 idGAFather, ref Int64 id)
                {
                    //Despues de cargar el arbol de Area Geografica, me quedo con la ultima Area y desde ahi cuelgo los Scopes
                    List<Condesus.EMS.Business.RG.IColumnsReport> _scopes = geoArea.Items();
                    foreach (Condesus.EMS.Business.RG.IColumnsReport _item in _scopes)
                    {
                        AddTableRow(id, idGAFather, _item.Name,
                            Common.Functions.CustomEMSRound(_item.Result_tCO2e),
                            Common.Functions.CustomEMSRound(_item.Result_CO2),
                            Common.Functions.CustomEMSRound(_item.Result_CH4),
                            Common.Functions.CustomEMSRound(_item.Result_N2O),
                            Common.Functions.CustomEMSRound(_item.Result_PFC),
                            Common.Functions.CustomEMSRound(_item.Result_HFC),
                            Common.Functions.CustomEMSRound(_item.Result_SF6),
                            Common.Functions.CustomEMSRound(_item.Result_HCT),
                            Common.Functions.CustomEMSRound(_item.Result_HCNM),
                            Common.Functions.CustomEMSRound(_item.Result_C2H6),
                            Common.Functions.CustomEMSRound(_item.Result_C3H8),
                            Common.Functions.CustomEMSRound(_item.Result_C4H10),
                            Common.Functions.CustomEMSRound(_item.Result_CO),
                            Common.Functions.CustomEMSRound(_item.Result_NOx),
                            Common.Functions.CustomEMSRound(_item.Result_SOx),
                            Common.Functions.CustomEMSRound(_item.Result_SO2),
                            Common.Functions.CustomEMSRound(_item.Result_H2S),
                            Common.Functions.CustomEMSRound(_item.Result_PM),
                            Common.Functions.CustomEMSRound(_item.Result_PM10));

                        Int64 _idScopeFather = id;
                        id++;

                        GA_S_A_FT_F_GetActivities(_item, _idScopeFather, ref id);
                    }
                }
                private void GA_S_A_FT_F_GetActivities(Condesus.EMS.Business.RG.IColumnsReport scope, Int64 idScopeFather, ref Int64 id)
                {
                    var _activities = from aa in scope.Items()
                                      orderby aa.Name ascending
                                      select aa;
                    foreach (Condesus.EMS.Business.RG.IColumnsReport _activity in _activities)
                    {
                        //El scope es el padre de todas las actividades que se cargan aca...
                        AddTableRow(id, idScopeFather, _activity.Name,
                            Common.Functions.CustomEMSRound(_activity.Result_tCO2e),
                            Common.Functions.CustomEMSRound(_activity.Result_CO2),
                            Common.Functions.CustomEMSRound(_activity.Result_CH4),
                            Common.Functions.CustomEMSRound(_activity.Result_N2O),
                            Common.Functions.CustomEMSRound(_activity.Result_PFC),
                            Common.Functions.CustomEMSRound(_activity.Result_HFC),
                            Common.Functions.CustomEMSRound(_activity.Result_SF6),
                            Common.Functions.CustomEMSRound(_activity.Result_HCT),
                            Common.Functions.CustomEMSRound(_activity.Result_HCNM),
                            Common.Functions.CustomEMSRound(_activity.Result_C2H6),
                            Common.Functions.CustomEMSRound(_activity.Result_C3H8),
                            Common.Functions.CustomEMSRound(_activity.Result_C4H10),
                            Common.Functions.CustomEMSRound(_activity.Result_CO),
                            Common.Functions.CustomEMSRound(_activity.Result_NOx),
                            Common.Functions.CustomEMSRound(_activity.Result_SOx),
                            Common.Functions.CustomEMSRound(_activity.Result_SO2),
                            Common.Functions.CustomEMSRound(_activity.Result_H2S),
                            Common.Functions.CustomEMSRound(_activity.Result_PM),
                            Common.Functions.CustomEMSRound(_activity.Result_PM10));

                        Int64 _idParent = id;
                        id++;

                        //Cargo los facility Types de esta Actividad
                        GA_S_A_FT_F_GetFacilityTypes(_activity, _idParent, ref id);

                        //Con esto cargo todos los hijos de esta actividad...
                        GA_S_A_FT_F_GetActivitiesChild(_activity, ref id, _idParent);
                    }
                }
                private void GA_S_A_FT_F_GetFacilityTypes(Condesus.EMS.Business.RG.IColumnsReport activity, Int64? idActivityFather, ref Int64 id)
                {
                    List<Condesus.EMS.Business.RG.IColumnsReport> _facilities = activity.Items();
                    var _facilityTypes = from aa in _facilities
                                      orderby aa.Name ascending
                                      select aa;
                    foreach (Condesus.EMS.Business.RG.IColumnsReport _item in _facilityTypes)
                    {
                        AddTableRow(id, idActivityFather, _item.Name,
                            Common.Functions.CustomEMSRound(_item.Result_tCO2e),
                            Common.Functions.CustomEMSRound(_item.Result_CO2),
                            Common.Functions.CustomEMSRound(_item.Result_CH4),
                            Common.Functions.CustomEMSRound(_item.Result_N2O),
                            Common.Functions.CustomEMSRound(_item.Result_PFC),
                            Common.Functions.CustomEMSRound(_item.Result_HFC),
                            Common.Functions.CustomEMSRound(_item.Result_SF6),
                            Common.Functions.CustomEMSRound(_item.Result_HCT),
                            Common.Functions.CustomEMSRound(_item.Result_HCNM),
                            Common.Functions.CustomEMSRound(_item.Result_C2H6),
                            Common.Functions.CustomEMSRound(_item.Result_C3H8),
                            Common.Functions.CustomEMSRound(_item.Result_C4H10),
                            Common.Functions.CustomEMSRound(_item.Result_CO),
                            Common.Functions.CustomEMSRound(_item.Result_NOx),
                            Common.Functions.CustomEMSRound(_item.Result_SOx),
                            Common.Functions.CustomEMSRound(_item.Result_SO2),
                            Common.Functions.CustomEMSRound(_item.Result_H2S),
                            Common.Functions.CustomEMSRound(_item.Result_PM),
                            Common.Functions.CustomEMSRound(_item.Result_PM10));

                        Int64 _idActivityFather = id;
                        id++;

                        GA_S_A_FT_F_GetFacilities(_item, _idActivityFather, ref id);
                    }
                }
                private void GA_S_A_FT_F_GetFacilities(Condesus.EMS.Business.RG.IColumnsReport facilityType, Int64 idFacilityTypeFather, ref Int64 id)
                {
                    var _facilities = from aa in facilityType.Items()
                                      orderby aa.Name ascending
                                      select aa;
                    foreach (Condesus.EMS.Business.RG.IColumnsReport _facility in _facilities)
                    {
                        //El scope es el padre de todas las actividades que se cargan aca...
                        AddTableRow(id, idFacilityTypeFather, _facility.Name,
                            Common.Functions.CustomEMSRound(_facility.Result_tCO2e),
                            Common.Functions.CustomEMSRound(_facility.Result_CO2),
                            Common.Functions.CustomEMSRound(_facility.Result_CH4),
                            Common.Functions.CustomEMSRound(_facility.Result_N2O),
                            Common.Functions.CustomEMSRound(_facility.Result_PFC),
                            Common.Functions.CustomEMSRound(_facility.Result_HFC),
                            Common.Functions.CustomEMSRound(_facility.Result_SF6),
                            Common.Functions.CustomEMSRound(_facility.Result_HCT),
                            Common.Functions.CustomEMSRound(_facility.Result_HCNM),
                            Common.Functions.CustomEMSRound(_facility.Result_C2H6),
                            Common.Functions.CustomEMSRound(_facility.Result_C3H8),
                            Common.Functions.CustomEMSRound(_facility.Result_C4H10),
                            Common.Functions.CustomEMSRound(_facility.Result_CO),
                            Common.Functions.CustomEMSRound(_facility.Result_NOx),
                            Common.Functions.CustomEMSRound(_facility.Result_SOx),
                            Common.Functions.CustomEMSRound(_facility.Result_SO2),
                            Common.Functions.CustomEMSRound(_facility.Result_H2S),
                            Common.Functions.CustomEMSRound(_facility.Result_PM),
                            Common.Functions.CustomEMSRound(_facility.Result_PM10));

                        Int64 _idParent = id;
                        id++;
                        //Con esto cargo todos los hijos de esta actividad...
                        GA_S_A_FT_F_GetFacilitiesChild(_facility, ref id, _idParent);

                        GA_S_A_FT_F_GetIndicators(_facility, _idParent, ref id);
                    }
                }
                private void GA_S_A_FT_F_GetGeographicAreaChild(Condesus.EMS.Business.RG.IColumnsReport _item, ref Int64 id, Int64? idGeoAreaParent)
                {
                    //Ahora por cada Area, cargo las hijas
                    var _geographicAreas = from geoarea in _item.Child()
                                           orderby geoarea.Name ascending
                                           select geoarea;
                    foreach (Condesus.EMS.Business.RG.IColumnsReport _geographicArea in _geographicAreas)
                    {
                        //El scope es el padre de todas las actividades que se cargan aca...
                        AddTableRow(id, idGeoAreaParent, _geographicArea.Name,
                            Common.Functions.CustomEMSRound(_geographicArea.Result_tCO2e),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_CO2),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_CH4),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_N2O),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_PFC),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_HFC),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_SF6),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_HCT),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_HCNM),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_C2H6),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_C3H8),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_C4H10),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_CO),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_NOx),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_SOx),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_SO2),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_H2S),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_PM),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_PM10));

                        Int64 _idParent = id;
                        id++;

                        //Cargo el Scope de esta Area Geografica
                        GA_S_A_FT_F_GetScopes(_geographicArea, _idParent, ref id);

                        //Ahora le pido los hijos de esta Area Geografica
                        GA_S_A_FT_F_GetGeographicAreaChild(_geographicArea, ref id, _idParent);
                    }
                }
                private void GA_S_A_FT_F_GetActivitiesChild(Condesus.EMS.Business.RG.IColumnsReport _item, ref Int64 id, Int64? idScopeFather)
                {
                    //Ahora por cada Scope, cargo las actividades
                    var _activities = from aa in _item.Child()
                                      //where aa.Result != 0
                                      orderby aa.Name ascending
                                      select aa;
                    foreach (Condesus.EMS.Business.RG.IColumnsReport _activity in _activities)
                    {
                        //El scope es el padre de todas las actividades que se cargan aca...
                        AddTableRow(id, idScopeFather, _activity.Name,
                            Common.Functions.CustomEMSRound(_activity.Result_tCO2e),
                            Common.Functions.CustomEMSRound(_activity.Result_CO2),
                            Common.Functions.CustomEMSRound(_activity.Result_CH4),
                            Common.Functions.CustomEMSRound(_activity.Result_N2O),
                            Common.Functions.CustomEMSRound(_activity.Result_PFC),
                            Common.Functions.CustomEMSRound(_activity.Result_HFC),
                            Common.Functions.CustomEMSRound(_activity.Result_SF6),
                            Common.Functions.CustomEMSRound(_activity.Result_HCT),
                            Common.Functions.CustomEMSRound(_activity.Result_HCNM),
                            Common.Functions.CustomEMSRound(_activity.Result_C2H6),
                            Common.Functions.CustomEMSRound(_activity.Result_C3H8),
                            Common.Functions.CustomEMSRound(_activity.Result_C4H10),
                            Common.Functions.CustomEMSRound(_activity.Result_CO),
                            Common.Functions.CustomEMSRound(_activity.Result_NOx),
                            Common.Functions.CustomEMSRound(_activity.Result_SOx),
                            Common.Functions.CustomEMSRound(_activity.Result_SO2),
                            Common.Functions.CustomEMSRound(_activity.Result_H2S),
                            Common.Functions.CustomEMSRound(_activity.Result_PM),
                            Common.Functions.CustomEMSRound(_activity.Result_PM10));

                        Int64? _idParent = id;
                        id++;

                        //Cargo los facility Types de esta Actividad
                        GA_S_A_FT_F_GetFacilityTypes(_activity, _idParent, ref id);

                        //Carga los hijos
                        GA_S_A_FT_F_GetActivitiesChild(_activity, ref id, _idParent);
                    }
                }
                private void GA_S_A_FT_F_GetFacilitiesChild(Condesus.EMS.Business.RG.IColumnsReport _item, ref Int64 id, Int64? idFacilityTypeFather)
                {
                    //Ahora por cada Scope, cargo las actividades
                    var _facilities = from aa in _item.Child()
                                      //where aa.Result != 0
                                      orderby aa.Name ascending
                                      select aa;
                    foreach (Condesus.EMS.Business.RG.IColumnsReport _facility in _facilities)
                    {
                        //El scope es el padre de todas las actividades que se cargan aca...
                        AddTableRow(id, idFacilityTypeFather, _facility.Name,
                            Common.Functions.CustomEMSRound(_facility.Result_tCO2e),
                            Common.Functions.CustomEMSRound(_facility.Result_CO2),
                            Common.Functions.CustomEMSRound(_facility.Result_CH4),
                            Common.Functions.CustomEMSRound(_facility.Result_N2O),
                            Common.Functions.CustomEMSRound(_facility.Result_PFC),
                            Common.Functions.CustomEMSRound(_facility.Result_HFC),
                            Common.Functions.CustomEMSRound(_facility.Result_SF6),
                            Common.Functions.CustomEMSRound(_facility.Result_HCT),
                            Common.Functions.CustomEMSRound(_facility.Result_HCNM),
                            Common.Functions.CustomEMSRound(_facility.Result_C2H6),
                            Common.Functions.CustomEMSRound(_facility.Result_C3H8),
                            Common.Functions.CustomEMSRound(_facility.Result_C4H10),
                            Common.Functions.CustomEMSRound(_facility.Result_CO),
                            Common.Functions.CustomEMSRound(_facility.Result_NOx),
                            Common.Functions.CustomEMSRound(_facility.Result_SOx),
                            Common.Functions.CustomEMSRound(_facility.Result_SO2),
                            Common.Functions.CustomEMSRound(_facility.Result_H2S),
                            Common.Functions.CustomEMSRound(_facility.Result_PM),
                            Common.Functions.CustomEMSRound(_facility.Result_PM10));

                        Int64? _idParent = id;
                        id++;

                        GA_S_A_FT_F_GetFacilitiesChild(_facility, ref id, _idParent);

                        GA_S_A_FT_F_GetIndicators(_facility, _idParent, ref id);
                    }
                }
                private void GA_S_A_FT_F_GetIndicators(Condesus.EMS.Business.RG.IColumnsReport facility, Int64? idFacilityFather, ref Int64 id)
                {
                    var _indicators = from aa in facility.Items()
                                      orderby aa.Name ascending
                                      select aa;
                    foreach (Condesus.EMS.Business.RG.IColumnsReport _indicator in _indicators)
                    {
                        //El Facility es el padre de todos los indicadores que se cargan aca...
                        AddTableRow(id, idFacilityFather, _indicator.Name,
                            Common.Functions.CustomEMSRound(_indicator.Result_tCO2e),
                            Common.Functions.CustomEMSRound(_indicator.Result_CO2),
                            Common.Functions.CustomEMSRound(_indicator.Result_CH4),
                            Common.Functions.CustomEMSRound(_indicator.Result_N2O),
                            Common.Functions.CustomEMSRound(_indicator.Result_PFC),
                            Common.Functions.CustomEMSRound(_indicator.Result_HFC),
                            Common.Functions.CustomEMSRound(_indicator.Result_SF6),
                            Common.Functions.CustomEMSRound(_indicator.Result_HCT),
                            Common.Functions.CustomEMSRound(_indicator.Result_HCNM),
                            Common.Functions.CustomEMSRound(_indicator.Result_C2H6),
                            Common.Functions.CustomEMSRound(_indicator.Result_C3H8),
                            Common.Functions.CustomEMSRound(_indicator.Result_C4H10),
                            Common.Functions.CustomEMSRound(_indicator.Result_CO),
                            Common.Functions.CustomEMSRound(_indicator.Result_NOx),
                            Common.Functions.CustomEMSRound(_indicator.Result_SOx),
                            Common.Functions.CustomEMSRound(_indicator.Result_SO2),
                            Common.Functions.CustomEMSRound(_indicator.Result_H2S),
                            Common.Functions.CustomEMSRound(_indicator.Result_PM),
                            Common.Functions.CustomEMSRound(_indicator.Result_PM10));

                        Int64 _idParent = id;
                        id++;
                    }
                }
            #endregion

            #region Reporte GA_FT_F_S_A
                private void LoadGridReport_GA_FT_F_S_A()
                {
                    DateTime _startDate = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;
                    DateTime _endDate = (DateTime)System.Data.SqlTypes.SqlDateTime.MaxValue;
                    if (ManageEntityParams["StartDate"] != null)
                    { _startDate = Convert.ToDateTime(ManageEntityParams["StartDate"]); }
                    if (ManageEntityParams["EndDate"] != null)
                    { _endDate = Convert.ToDateTime(ManageEntityParams["EndDate"]); }

                    Int64 _idProcess = Convert.ToInt64(ManageEntityParams["IdProcess"]);
                    Int64 _idIndicator_tnCO2e = Convert.ToInt64(ManageEntityParams["IdIndicator_tnCO2e"]);
                    Int64 _idIndicator_CO2 = Convert.ToInt64(ManageEntityParams["IdIndicator_CO2"]);
                    Int64 _idIndicator_CH4 = Convert.ToInt64(ManageEntityParams["IdIndicator_CH4"]);
                    Int64 _idIndicator_N2O = Convert.ToInt64(ManageEntityParams["IdIndicator_N2O"]);
                    Int64 _idIndicator_PFC = Convert.ToInt64(ManageEntityParams["IdIndicator_PFC"]);
                    Int64 _idIndicator_HFC = Convert.ToInt64(ManageEntityParams["IdIndicator_HFC"]);
                    Int64 _idIndicator_SF6 = Convert.ToInt64(ManageEntityParams["IdIndicator_SF6"]);
                    Int64 _idIndicator_HCNM = Convert.ToInt64(ManageEntityParams["IdIndicator_HCNM"]);
                    Int64 _idIndicator_HCT = Convert.ToInt64(ManageEntityParams["IdIndicator_HCT"]);
                    Int64 _idIndicator_CO = Convert.ToInt64(ManageEntityParams["IdIndicator_CO"]);
                    Int64 _idIndicator_NOx = Convert.ToInt64(ManageEntityParams["IdIndicator_NOx"]);
                    Int64 _idIndicator_SOx = Convert.ToInt64(ManageEntityParams["IdIndicator_SOx"]);
                    Int64 _idIndicator_PM = Convert.ToInt64(ManageEntityParams["IdIndicator_PM"]);
                    Int64 _idIndicator_SO2 = Convert.ToInt64(ManageEntityParams["IdIndicator_SO2"]);
                    Int64 _idIndicator_H2S = Convert.ToInt64(ManageEntityParams["IdIndicator_H2S"]);
                    Int64 _idIndicator_PM10 = Convert.ToInt64(ManageEntityParams["IdIndicator_PM10"]);
                    Int64 _idIndicator_C2H6 = Convert.ToInt64(ManageEntityParams["IdIndicator_C2H6"]);
                    Int64 _idIndicator_C3H8 = Convert.ToInt64(ManageEntityParams["IdIndicator_C3H8"]);
                    Int64 _idIndicator_C4H10 = Convert.ToInt64(ManageEntityParams["IdIndicator_C4H10"]);


                    List<Condesus.EMS.Business.RG.IColumnsReport> _listGA = ((ProcessGroupProcess)EMSLibrary.User.ProcessFramework.Map.Process(_idProcess)).Report_GA_FT_F_S_A.GA(_idIndicator_tnCO2e, _idIndicator_CO2,
                        _idIndicator_CH4, _idIndicator_N2O, _idIndicator_PFC, _idIndicator_HFC, _idIndicator_SF6, _idIndicator_HCT,
                        _idIndicator_HCNM, _idIndicator_C2H6, _idIndicator_C3H8, _idIndicator_C4H10, _idIndicator_CO, _idIndicator_NOx,
                        _idIndicator_SOx, _idIndicator_SO2, _idIndicator_H2S, _idIndicator_PM, _idIndicator_PM10, _startDate, _endDate);

                    Int64 _id = 1;

                    //Empezamos con los ROOT de Area Geografica
                    foreach (Condesus.EMS.Business.RG.IColumnsReport _icolumns in _listGA)
                    {
                        AddTableRow(_id, null, _icolumns.Name, 
                            Common.Functions.CustomEMSRound(_icolumns.Result_tCO2e),
                            Common.Functions.CustomEMSRound(_icolumns.Result_CO2),
                            Common.Functions.CustomEMSRound(_icolumns.Result_CH4),
                            Common.Functions.CustomEMSRound(_icolumns.Result_N2O),
                            Common.Functions.CustomEMSRound(_icolumns.Result_PFC),
                            Common.Functions.CustomEMSRound(_icolumns.Result_HFC),
                            Common.Functions.CustomEMSRound(_icolumns.Result_SF6),
                            Common.Functions.CustomEMSRound(_icolumns.Result_HCT),
                            Common.Functions.CustomEMSRound(_icolumns.Result_HCNM),
                            Common.Functions.CustomEMSRound(_icolumns.Result_C2H6),
                            Common.Functions.CustomEMSRound(_icolumns.Result_C3H8),
                            Common.Functions.CustomEMSRound(_icolumns.Result_C4H10),
                            Common.Functions.CustomEMSRound(_icolumns.Result_CO),
                            Common.Functions.CustomEMSRound(_icolumns.Result_NOx),
                            Common.Functions.CustomEMSRound(_icolumns.Result_SOx),
                            Common.Functions.CustomEMSRound(_icolumns.Result_SO2),
                            Common.Functions.CustomEMSRound(_icolumns.Result_H2S),
                            Common.Functions.CustomEMSRound(_icolumns.Result_PM),
                            Common.Functions.CustomEMSRound(_icolumns.Result_PM10));

                        Int64 _idParent = _id;
                        _id++;

                        //Cargo el Facility Type
                        GA_FT_F_S_A_GetFacilityTypes(_icolumns, _idParent, ref _id);

                        //Ahora le pido los hijos de esta Area Geografica
                        GA_FT_F_S_A_GetGeographicAreaChild(_icolumns, ref _id, _idParent);

                    }
                }
                private void GA_FT_F_S_A_GetScopes(Condesus.EMS.Business.RG.IColumnsReport facility, Int64? idGAFather, ref Int64 id)
                {
                    //Despues de cargar el arbol de Area Geografica, me quedo con la ultima Area y desde ahi cuelgo los Scopes
                    List<Condesus.EMS.Business.RG.IColumnsReport> _scopes = facility.Items();
                    foreach (Condesus.EMS.Business.RG.IColumnsReport _item in _scopes)
                    {
                        AddTableRow(id, idGAFather, _item.Name,
                            Common.Functions.CustomEMSRound(_item.Result_tCO2e),
                            Common.Functions.CustomEMSRound(_item.Result_CO2),
                            Common.Functions.CustomEMSRound(_item.Result_CH4),
                            Common.Functions.CustomEMSRound(_item.Result_N2O),
                            Common.Functions.CustomEMSRound(_item.Result_PFC),
                            Common.Functions.CustomEMSRound(_item.Result_HFC),
                            Common.Functions.CustomEMSRound(_item.Result_SF6),
                            Common.Functions.CustomEMSRound(_item.Result_HCT),
                            Common.Functions.CustomEMSRound(_item.Result_HCNM),
                            Common.Functions.CustomEMSRound(_item.Result_C2H6),
                            Common.Functions.CustomEMSRound(_item.Result_C3H8),
                            Common.Functions.CustomEMSRound(_item.Result_C4H10),
                            Common.Functions.CustomEMSRound(_item.Result_CO),
                            Common.Functions.CustomEMSRound(_item.Result_NOx),
                            Common.Functions.CustomEMSRound(_item.Result_SOx),
                            Common.Functions.CustomEMSRound(_item.Result_SO2),
                            Common.Functions.CustomEMSRound(_item.Result_H2S),
                            Common.Functions.CustomEMSRound(_item.Result_PM),
                            Common.Functions.CustomEMSRound(_item.Result_PM10));

                        Int64 _idScopeFather = id;
                        id++;

                        GA_FT_F_S_A_GetActivities(_item, _idScopeFather, ref id);
                    }
                }
                private void GA_FT_F_S_A_GetActivities(Condesus.EMS.Business.RG.IColumnsReport scope, Int64 idScopeFather, ref Int64 id)
                {
                    var _activities = from aa in scope.Items()
                                      orderby aa.Name ascending
                                      select aa;
                    foreach (Condesus.EMS.Business.RG.IColumnsReport _activity in _activities)
                    {
                        //El scope es el padre de todas las actividades que se cargan aca...
                        AddTableRow(id, idScopeFather, _activity.Name,
                            Common.Functions.CustomEMSRound(_activity.Result_tCO2e),
                            Common.Functions.CustomEMSRound(_activity.Result_CO2),
                            Common.Functions.CustomEMSRound(_activity.Result_CH4),
                            Common.Functions.CustomEMSRound(_activity.Result_N2O),
                            Common.Functions.CustomEMSRound(_activity.Result_PFC),
                            Common.Functions.CustomEMSRound(_activity.Result_HFC),
                            Common.Functions.CustomEMSRound(_activity.Result_SF6),
                            Common.Functions.CustomEMSRound(_activity.Result_HCT),
                            Common.Functions.CustomEMSRound(_activity.Result_HCNM),
                            Common.Functions.CustomEMSRound(_activity.Result_C2H6),
                            Common.Functions.CustomEMSRound(_activity.Result_C3H8),
                            Common.Functions.CustomEMSRound(_activity.Result_C4H10),
                            Common.Functions.CustomEMSRound(_activity.Result_CO),
                            Common.Functions.CustomEMSRound(_activity.Result_NOx),
                            Common.Functions.CustomEMSRound(_activity.Result_SOx),
                            Common.Functions.CustomEMSRound(_activity.Result_SO2),
                            Common.Functions.CustomEMSRound(_activity.Result_H2S),
                            Common.Functions.CustomEMSRound(_activity.Result_PM),
                            Common.Functions.CustomEMSRound(_activity.Result_PM10));

                        Int64 _idParent = id;
                        id++;

                        //Con esto cargo todos los hijos de esta actividad...
                        GA_FT_F_S_A_GetActivitiesChild(_activity, ref id, _idParent);

                        GA_FT_F_S_A_GetIndicators(_activity, _idParent, ref id);
                    }
                }
                private void GA_FT_F_S_A_GetFacilityTypes(Condesus.EMS.Business.RG.IColumnsReport geoarea, Int64? idActivityFather, ref Int64 id)
                {
                    List<Condesus.EMS.Business.RG.IColumnsReport> _fTypes = geoarea.Items();
                    var _facilityTypes = from aa in _fTypes
                                         orderby aa.Name ascending
                                         select aa;
                    foreach (Condesus.EMS.Business.RG.IColumnsReport _item in _facilityTypes)
                    {
                        AddTableRow(id, idActivityFather, _item.Name,
                            Common.Functions.CustomEMSRound(_item.Result_tCO2e),
                            Common.Functions.CustomEMSRound(_item.Result_CO2),
                            Common.Functions.CustomEMSRound(_item.Result_CH4),
                            Common.Functions.CustomEMSRound(_item.Result_N2O),
                            Common.Functions.CustomEMSRound(_item.Result_PFC),
                            Common.Functions.CustomEMSRound(_item.Result_HFC),
                            Common.Functions.CustomEMSRound(_item.Result_SF6),
                            Common.Functions.CustomEMSRound(_item.Result_HCT),
                            Common.Functions.CustomEMSRound(_item.Result_HCNM),
                            Common.Functions.CustomEMSRound(_item.Result_C2H6),
                            Common.Functions.CustomEMSRound(_item.Result_C3H8),
                            Common.Functions.CustomEMSRound(_item.Result_C4H10),
                            Common.Functions.CustomEMSRound(_item.Result_CO),
                            Common.Functions.CustomEMSRound(_item.Result_NOx),
                            Common.Functions.CustomEMSRound(_item.Result_SOx),
                            Common.Functions.CustomEMSRound(_item.Result_SO2),
                            Common.Functions.CustomEMSRound(_item.Result_H2S),
                            Common.Functions.CustomEMSRound(_item.Result_PM),
                            Common.Functions.CustomEMSRound(_item.Result_PM10));

                        Int64 _idActivityFather = id;
                        id++;

                        GA_FT_F_S_A_GetFacilities(_item, _idActivityFather, ref id);
                    }
                }
                private void GA_FT_F_S_A_GetFacilities(Condesus.EMS.Business.RG.IColumnsReport facilityType, Int64 idFacilityTypeFather, ref Int64 id)
                {
                    var _facilities = from aa in facilityType.Items()
                                      orderby aa.Name ascending
                                      select aa;
                    foreach (Condesus.EMS.Business.RG.IColumnsReport _facility in _facilities)
                    {
                        //El scope es el padre de todas las actividades que se cargan aca...
                        AddTableRow(id, idFacilityTypeFather, _facility.Name,
                            Common.Functions.CustomEMSRound(_facility.Result_tCO2e),
                            Common.Functions.CustomEMSRound(_facility.Result_CO2),
                            Common.Functions.CustomEMSRound(_facility.Result_CH4),
                            Common.Functions.CustomEMSRound(_facility.Result_N2O),
                            Common.Functions.CustomEMSRound(_facility.Result_PFC),
                            Common.Functions.CustomEMSRound(_facility.Result_HFC),
                            Common.Functions.CustomEMSRound(_facility.Result_SF6),
                            Common.Functions.CustomEMSRound(_facility.Result_HCT),
                            Common.Functions.CustomEMSRound(_facility.Result_HCNM),
                            Common.Functions.CustomEMSRound(_facility.Result_C2H6),
                            Common.Functions.CustomEMSRound(_facility.Result_C3H8),
                            Common.Functions.CustomEMSRound(_facility.Result_C4H10),
                            Common.Functions.CustomEMSRound(_facility.Result_CO),
                            Common.Functions.CustomEMSRound(_facility.Result_NOx),
                            Common.Functions.CustomEMSRound(_facility.Result_SOx),
                            Common.Functions.CustomEMSRound(_facility.Result_SO2),
                            Common.Functions.CustomEMSRound(_facility.Result_H2S),
                            Common.Functions.CustomEMSRound(_facility.Result_PM),
                            Common.Functions.CustomEMSRound(_facility.Result_PM10));

                        Int64 _idParent = id;
                        id++;

                        //Cargo el Scope de esta Area Geografica
                        GA_FT_F_S_A_GetScopes(_facility, _idParent, ref id);

                        //Con esto cargo todos los hijos de esta actividad...
                        GA_FT_F_S_A_GetFacilitiesChild(_facility, ref id, _idParent);
                    }
                }
                private void GA_FT_F_S_A_GetGeographicAreaChild(Condesus.EMS.Business.RG.IColumnsReport _item, ref Int64 id, Int64? idGeoAreaParent)
                {
                    //Ahora por cada Area, cargo las hijas
                    var _geographicAreas = from geoarea in _item.Child()
                                           orderby geoarea.Name ascending
                                           select geoarea;
                    foreach (Condesus.EMS.Business.RG.IColumnsReport _geographicArea in _geographicAreas)
                    {
                        //El scope es el padre de todas las actividades que se cargan aca...
                        AddTableRow(id, idGeoAreaParent, _geographicArea.Name,
                            Common.Functions.CustomEMSRound(_geographicArea.Result_tCO2e),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_CO2),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_CH4),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_N2O),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_PFC),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_HFC),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_SF6),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_HCT),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_HCNM),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_C2H6),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_C3H8),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_C4H10),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_CO),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_NOx),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_SOx),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_SO2),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_H2S),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_PM),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_PM10));

                        Int64 _idParent = id;
                        id++;

                        //Cargo el Facility Type de esta Area Geografica
                        GA_FT_F_S_A_GetFacilityTypes(_geographicArea, _idParent, ref id);

                        //Ahora le pido los hijos de esta Area Geografica
                        GA_FT_F_S_A_GetGeographicAreaChild(_geographicArea, ref id, _idParent);
                    }
                }
                private void GA_FT_F_S_A_GetActivitiesChild(Condesus.EMS.Business.RG.IColumnsReport _item, ref Int64 id, Int64? idScopeFather)
                {
                    //Ahora por cada Scope, cargo las actividades
                    var _activities = from aa in _item.Child()
                                      //where aa.Result != 0
                                      orderby aa.Name ascending
                                      select aa;
                    foreach (Condesus.EMS.Business.RG.IColumnsReport _activity in _activities)
                    {
                        //El scope es el padre de todas las actividades que se cargan aca...
                        AddTableRow(id, idScopeFather, _activity.Name,
                            Common.Functions.CustomEMSRound(_activity.Result_tCO2e),
                            Common.Functions.CustomEMSRound(_activity.Result_CO2),
                            Common.Functions.CustomEMSRound(_activity.Result_CH4),
                            Common.Functions.CustomEMSRound(_activity.Result_N2O),
                            Common.Functions.CustomEMSRound(_activity.Result_PFC),
                            Common.Functions.CustomEMSRound(_activity.Result_HFC),
                            Common.Functions.CustomEMSRound(_activity.Result_SF6),
                            Common.Functions.CustomEMSRound(_activity.Result_HCT),
                            Common.Functions.CustomEMSRound(_activity.Result_HCNM),
                            Common.Functions.CustomEMSRound(_activity.Result_C2H6),
                            Common.Functions.CustomEMSRound(_activity.Result_C3H8),
                            Common.Functions.CustomEMSRound(_activity.Result_C4H10),
                            Common.Functions.CustomEMSRound(_activity.Result_CO),
                            Common.Functions.CustomEMSRound(_activity.Result_NOx),
                            Common.Functions.CustomEMSRound(_activity.Result_SOx),
                            Common.Functions.CustomEMSRound(_activity.Result_SO2),
                            Common.Functions.CustomEMSRound(_activity.Result_H2S),
                            Common.Functions.CustomEMSRound(_activity.Result_PM),
                            Common.Functions.CustomEMSRound(_activity.Result_PM10));

                        Int64? _idParent = id;
                        id++;

                        //Carga los hijos
                        GA_FT_F_S_A_GetActivitiesChild(_activity, ref id, _idParent);
                        
                        GA_FT_F_S_A_GetIndicators(_activity, _idParent, ref id);
                    }
                }
                private void GA_FT_F_S_A_GetFacilitiesChild(Condesus.EMS.Business.RG.IColumnsReport _item, ref Int64 id, Int64? idFacilityTypeFather)
                {
                    //Ahora por cada Scope, cargo las actividades
                    var _facilities = from aa in _item.Child()
                                      //where aa.Result != 0
                                      orderby aa.Name ascending
                                      select aa;
                    foreach (Condesus.EMS.Business.RG.IColumnsReport _facility in _facilities)
                    {
                        //El scope es el padre de todas las actividades que se cargan aca...
                        AddTableRow(id, idFacilityTypeFather, _facility.Name,
                            Common.Functions.CustomEMSRound(_facility.Result_tCO2e),
                            Common.Functions.CustomEMSRound(_facility.Result_CO2),
                            Common.Functions.CustomEMSRound(_facility.Result_CH4),
                            Common.Functions.CustomEMSRound(_facility.Result_N2O),
                            Common.Functions.CustomEMSRound(_facility.Result_PFC),
                            Common.Functions.CustomEMSRound(_facility.Result_HFC),
                            Common.Functions.CustomEMSRound(_facility.Result_SF6),
                            Common.Functions.CustomEMSRound(_facility.Result_HCT),
                            Common.Functions.CustomEMSRound(_facility.Result_HCNM),
                            Common.Functions.CustomEMSRound(_facility.Result_C2H6),
                            Common.Functions.CustomEMSRound(_facility.Result_C3H8),
                            Common.Functions.CustomEMSRound(_facility.Result_C4H10),
                            Common.Functions.CustomEMSRound(_facility.Result_CO),
                            Common.Functions.CustomEMSRound(_facility.Result_NOx),
                            Common.Functions.CustomEMSRound(_facility.Result_SOx),
                            Common.Functions.CustomEMSRound(_facility.Result_SO2),
                            Common.Functions.CustomEMSRound(_facility.Result_H2S),
                            Common.Functions.CustomEMSRound(_facility.Result_PM),
                            Common.Functions.CustomEMSRound(_facility.Result_PM10));

                        Int64? _idParent = id;
                        id++;

                        //Cargo el Scope de esta Area Geografica
                        GA_FT_F_S_A_GetScopes(_facility, _idParent, ref id);

                        //Carga los facilities hijos
                        GA_FT_F_S_A_GetFacilitiesChild(_facility, ref id, _idParent);
                    }
                }
                private void GA_FT_F_S_A_GetIndicators(Condesus.EMS.Business.RG.IColumnsReport activity, Int64? idActivityFather, ref Int64 id)
                {
                    var _indicators = from aa in activity.Items()
                                      orderby aa.Name ascending
                                      select aa;
                    foreach (Condesus.EMS.Business.RG.IColumnsReport _indicator in _indicators)
                    {
                        //El scope es el padre de todas las actividades que se cargan aca...
                        AddTableRow(id, idActivityFather, _indicator.Name,
                            Common.Functions.CustomEMSRound(_indicator.Result_tCO2e),
                            Common.Functions.CustomEMSRound(_indicator.Result_CO2),
                            Common.Functions.CustomEMSRound(_indicator.Result_CH4),
                            Common.Functions.CustomEMSRound(_indicator.Result_N2O),
                            Common.Functions.CustomEMSRound(_indicator.Result_PFC),
                            Common.Functions.CustomEMSRound(_indicator.Result_HFC),
                            Common.Functions.CustomEMSRound(_indicator.Result_SF6),
                            Common.Functions.CustomEMSRound(_indicator.Result_HCT),
                            Common.Functions.CustomEMSRound(_indicator.Result_HCNM),
                            Common.Functions.CustomEMSRound(_indicator.Result_C2H6),
                            Common.Functions.CustomEMSRound(_indicator.Result_C3H8),
                            Common.Functions.CustomEMSRound(_indicator.Result_C4H10),
                            Common.Functions.CustomEMSRound(_indicator.Result_CO),
                            Common.Functions.CustomEMSRound(_indicator.Result_NOx),
                            Common.Functions.CustomEMSRound(_indicator.Result_SOx),
                            Common.Functions.CustomEMSRound(_indicator.Result_SO2),
                            Common.Functions.CustomEMSRound(_indicator.Result_H2S),
                            Common.Functions.CustomEMSRound(_indicator.Result_PM),
                            Common.Functions.CustomEMSRound(_indicator.Result_PM10));

                        Int64 _idParent = id;
                        id++;
                    }
                }
            #endregion

            #region Reporte S_GA_A_FT_F
                private void LoadGridReport_S_GA_A_FT_F()
                {
                    DateTime _startDate = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;
                    DateTime _endDate = (DateTime)System.Data.SqlTypes.SqlDateTime.MaxValue;
                    if (ManageEntityParams["StartDate"] != null)
                    { _startDate = Convert.ToDateTime(ManageEntityParams["StartDate"]); }
                    if (ManageEntityParams["EndDate"] != null)
                    { _endDate = Convert.ToDateTime(ManageEntityParams["EndDate"]); }

                    Int64 _idProcess = Convert.ToInt64(ManageEntityParams["IdProcess"]);
                    Int64 _idIndicator_tnCO2e = Convert.ToInt64(ManageEntityParams["IdIndicator_tnCO2e"]);
                    Int64 _idIndicator_CO2 = Convert.ToInt64(ManageEntityParams["IdIndicator_CO2"]);
                    Int64 _idIndicator_CH4 = Convert.ToInt64(ManageEntityParams["IdIndicator_CH4"]);
                    Int64 _idIndicator_N2O = Convert.ToInt64(ManageEntityParams["IdIndicator_N2O"]);
                    Int64 _idIndicator_PFC = Convert.ToInt64(ManageEntityParams["IdIndicator_PFC"]);
                    Int64 _idIndicator_HFC = Convert.ToInt64(ManageEntityParams["IdIndicator_HFC"]);
                    Int64 _idIndicator_SF6 = Convert.ToInt64(ManageEntityParams["IdIndicator_SF6"]);
                    Int64 _idIndicator_HCNM = Convert.ToInt64(ManageEntityParams["IdIndicator_HCNM"]);
                    Int64 _idIndicator_HCT = Convert.ToInt64(ManageEntityParams["IdIndicator_HCT"]);
                    Int64 _idIndicator_CO = Convert.ToInt64(ManageEntityParams["IdIndicator_CO"]);
                    Int64 _idIndicator_NOx = Convert.ToInt64(ManageEntityParams["IdIndicator_NOx"]);
                    Int64 _idIndicator_SOx = Convert.ToInt64(ManageEntityParams["IdIndicator_SOx"]);
                    Int64 _idIndicator_PM = Convert.ToInt64(ManageEntityParams["IdIndicator_PM"]);
                    Int64 _idIndicator_SO2 = Convert.ToInt64(ManageEntityParams["IdIndicator_SO2"]);
                    Int64 _idIndicator_H2S = Convert.ToInt64(ManageEntityParams["IdIndicator_H2S"]);
                    Int64 _idIndicator_PM10 = Convert.ToInt64(ManageEntityParams["IdIndicator_PM10"]);
                    Int64 _idIndicator_C2H6 = Convert.ToInt64(ManageEntityParams["IdIndicator_C2H6"]);
                    Int64 _idIndicator_C3H8 = Convert.ToInt64(ManageEntityParams["IdIndicator_C3H8"]);
                    Int64 _idIndicator_C4H10 = Convert.ToInt64(ManageEntityParams["IdIndicator_C4H10"]);

                    //Arrancamos con los Scopes
                    List<Condesus.EMS.Business.RG.IColumnsReport> _listScope = ((ProcessGroupProcess)EMSLibrary.User.ProcessFramework.Map.Process(_idProcess)).Report_S_GA_A_FT_F.S(_idIndicator_tnCO2e, _idIndicator_CO2,
                        _idIndicator_CH4, _idIndicator_N2O, _idIndicator_PFC, _idIndicator_HFC, _idIndicator_SF6, _idIndicator_HCT,
                        _idIndicator_HCNM, _idIndicator_C2H6, _idIndicator_C3H8, _idIndicator_C4H10, _idIndicator_CO, _idIndicator_NOx,
                        _idIndicator_SOx, _idIndicator_SO2, _idIndicator_H2S, _idIndicator_PM, _idIndicator_PM10, _startDate, _endDate);

                    Int64 _id = 1;

                    //Empezamos con los ROOT de Area Geografica
                    foreach (Condesus.EMS.Business.RG.IColumnsReport _icolumns in _listScope)
                    {
                        AddTableRow(_id, null, _icolumns.Name, 
                            Common.Functions.CustomEMSRound(_icolumns.Result_tCO2e),
                            Common.Functions.CustomEMSRound(_icolumns.Result_CO2),
                            Common.Functions.CustomEMSRound(_icolumns.Result_CH4),
                            Common.Functions.CustomEMSRound(_icolumns.Result_N2O),
                            Common.Functions.CustomEMSRound(_icolumns.Result_PFC),
                            Common.Functions.CustomEMSRound(_icolumns.Result_HFC),
                            Common.Functions.CustomEMSRound(_icolumns.Result_SF6),
                            Common.Functions.CustomEMSRound(_icolumns.Result_HCT),
                            Common.Functions.CustomEMSRound(_icolumns.Result_HCNM),
                            Common.Functions.CustomEMSRound(_icolumns.Result_C2H6),
                            Common.Functions.CustomEMSRound(_icolumns.Result_C3H8),
                            Common.Functions.CustomEMSRound(_icolumns.Result_C4H10),
                            Common.Functions.CustomEMSRound(_icolumns.Result_CO),
                            Common.Functions.CustomEMSRound(_icolumns.Result_NOx),
                            Common.Functions.CustomEMSRound(_icolumns.Result_SOx),
                            Common.Functions.CustomEMSRound(_icolumns.Result_SO2),
                            Common.Functions.CustomEMSRound(_icolumns.Result_H2S),
                            Common.Functions.CustomEMSRound(_icolumns.Result_PM),
                            Common.Functions.CustomEMSRound(_icolumns.Result_PM10));

                        Int64 _idParent = _id;
                        _id++;

                        //Ahora le pido los hijos de esta Area Geografica
                        S_GA_A_FT_F_GetGeographicArea(_icolumns, ref _id, _idParent);

                    }
                }
                private void S_GA_A_FT_F_GetGeographicArea(Condesus.EMS.Business.RG.IColumnsReport _item, ref Int64 id, Int64? idGeoAreaParent)
                {
                    //Ahora por cada Scope pido las areas geograficas y por cada AG pido sus hijas
                    var _geographicAreas = from geoarea in _item.Items()
                                           orderby geoarea.Name ascending
                                           select geoarea;
                    foreach (Condesus.EMS.Business.RG.IColumnsReport _geographicArea in _geographicAreas)
                    {
                        //El scope es el padre de todas las actividades que se cargan aca...
                        AddTableRow(id, idGeoAreaParent, _geographicArea.Name,
                            Common.Functions.CustomEMSRound(_geographicArea.Result_tCO2e),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_CO2),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_CH4),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_N2O),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_PFC),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_HFC),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_SF6),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_HCT),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_HCNM),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_C2H6),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_C3H8),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_C4H10),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_CO),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_NOx),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_SOx),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_SO2),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_H2S),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_PM),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_PM10));

                        Int64 _idParent = id;
                        id++;

                        //Cargo las Activities de esta Area Geografica
                        S_GA_A_FT_F_GetActivities(_geographicArea, _idParent, ref id);

                        //Ahora le pido los hijos de esta Area Geografica
                        S_GA_A_FT_F_GetGeographicAreaChild(_geographicArea, ref id, _idParent);
                    }
                }
                private void S_GA_A_FT_F_GetActivities(Condesus.EMS.Business.RG.IColumnsReport scope, Int64 idScopeFather, ref Int64 id)
                {
                    var _activities = from aa in scope.Items()
                                      orderby aa.Name ascending
                                      select aa;
                    foreach (Condesus.EMS.Business.RG.IColumnsReport _activity in _activities)
                    {
                        //El scope es el padre de todas las actividades que se cargan aca...
                        AddTableRow(id, idScopeFather, _activity.Name,
                            Common.Functions.CustomEMSRound(_activity.Result_tCO2e),
                            Common.Functions.CustomEMSRound(_activity.Result_CO2),
                            Common.Functions.CustomEMSRound(_activity.Result_CH4),
                            Common.Functions.CustomEMSRound(_activity.Result_N2O),
                            Common.Functions.CustomEMSRound(_activity.Result_PFC),
                            Common.Functions.CustomEMSRound(_activity.Result_HFC),
                            Common.Functions.CustomEMSRound(_activity.Result_SF6),
                            Common.Functions.CustomEMSRound(_activity.Result_HCT),
                            Common.Functions.CustomEMSRound(_activity.Result_HCNM),
                            Common.Functions.CustomEMSRound(_activity.Result_C2H6),
                            Common.Functions.CustomEMSRound(_activity.Result_C3H8),
                            Common.Functions.CustomEMSRound(_activity.Result_C4H10),
                            Common.Functions.CustomEMSRound(_activity.Result_CO),
                            Common.Functions.CustomEMSRound(_activity.Result_NOx),
                            Common.Functions.CustomEMSRound(_activity.Result_SOx),
                            Common.Functions.CustomEMSRound(_activity.Result_SO2),
                            Common.Functions.CustomEMSRound(_activity.Result_H2S),
                            Common.Functions.CustomEMSRound(_activity.Result_PM),
                            Common.Functions.CustomEMSRound(_activity.Result_PM10));

                        Int64 _idParent = id;
                        id++;

                        //Cargo los facility Types de esta Actividad
                        S_GA_A_FT_F_GetFacilityTypes(_activity, _idParent, ref id);

                        //Con esto cargo todos los hijos de esta actividad...
                        S_GA_A_FT_F_GetActivitiesChild(_activity, ref id, _idParent);
                    }
                }
                private void S_GA_A_FT_F_GetFacilityTypes(Condesus.EMS.Business.RG.IColumnsReport activity, Int64? idActivityFather, ref Int64 id)
                {
                    List<Condesus.EMS.Business.RG.IColumnsReport> _facilities = activity.Items();
                    var _facilityTypes = from aa in _facilities
                                         orderby aa.Name ascending
                                         select aa;
                    foreach (Condesus.EMS.Business.RG.IColumnsReport _item in _facilityTypes)
                    {
                        AddTableRow(id, idActivityFather, _item.Name,
                            Common.Functions.CustomEMSRound(_item.Result_tCO2e),
                            Common.Functions.CustomEMSRound(_item.Result_CO2),
                            Common.Functions.CustomEMSRound(_item.Result_CH4),
                            Common.Functions.CustomEMSRound(_item.Result_N2O),
                            Common.Functions.CustomEMSRound(_item.Result_PFC),
                            Common.Functions.CustomEMSRound(_item.Result_HFC),
                            Common.Functions.CustomEMSRound(_item.Result_SF6),
                            Common.Functions.CustomEMSRound(_item.Result_HCT),
                            Common.Functions.CustomEMSRound(_item.Result_HCNM),
                            Common.Functions.CustomEMSRound(_item.Result_C2H6),
                            Common.Functions.CustomEMSRound(_item.Result_C3H8),
                            Common.Functions.CustomEMSRound(_item.Result_C4H10),
                            Common.Functions.CustomEMSRound(_item.Result_CO),
                            Common.Functions.CustomEMSRound(_item.Result_NOx),
                            Common.Functions.CustomEMSRound(_item.Result_SOx),
                            Common.Functions.CustomEMSRound(_item.Result_SO2),
                            Common.Functions.CustomEMSRound(_item.Result_H2S),
                            Common.Functions.CustomEMSRound(_item.Result_PM),
                            Common.Functions.CustomEMSRound(_item.Result_PM10));

                        Int64 _idActivityFather = id;
                        id++;

                        S_GA_A_FT_F_GetFacilities(_item, _idActivityFather, ref id);
                    }
                }
                private void S_GA_A_FT_F_GetFacilities(Condesus.EMS.Business.RG.IColumnsReport facilityType, Int64 idFacilityTypeFather, ref Int64 id)
                {
                    var _facilities = from aa in facilityType.Items()
                                      orderby aa.Name ascending
                                      select aa;
                    foreach (Condesus.EMS.Business.RG.IColumnsReport _facility in _facilities)
                    {
                        //El scope es el padre de todas las actividades que se cargan aca...
                        AddTableRow(id, idFacilityTypeFather, _facility.Name,
                            Common.Functions.CustomEMSRound(_facility.Result_tCO2e),
                            Common.Functions.CustomEMSRound(_facility.Result_CO2),
                            Common.Functions.CustomEMSRound(_facility.Result_CH4),
                            Common.Functions.CustomEMSRound(_facility.Result_N2O),
                            Common.Functions.CustomEMSRound(_facility.Result_PFC),
                            Common.Functions.CustomEMSRound(_facility.Result_HFC),
                            Common.Functions.CustomEMSRound(_facility.Result_SF6),
                            Common.Functions.CustomEMSRound(_facility.Result_HCT),
                            Common.Functions.CustomEMSRound(_facility.Result_HCNM),
                            Common.Functions.CustomEMSRound(_facility.Result_C2H6),
                            Common.Functions.CustomEMSRound(_facility.Result_C3H8),
                            Common.Functions.CustomEMSRound(_facility.Result_C4H10),
                            Common.Functions.CustomEMSRound(_facility.Result_CO),
                            Common.Functions.CustomEMSRound(_facility.Result_NOx),
                            Common.Functions.CustomEMSRound(_facility.Result_SOx),
                            Common.Functions.CustomEMSRound(_facility.Result_SO2),
                            Common.Functions.CustomEMSRound(_facility.Result_H2S),
                            Common.Functions.CustomEMSRound(_facility.Result_PM),
                            Common.Functions.CustomEMSRound(_facility.Result_PM10));

                        Int64 _idParent = id;
                        id++;
                        //Con esto cargo todos los hijos de esta actividad...
                        S_GA_A_FT_F_GetFacilitiesChild(_facility, ref id, _idParent);

                        S_GA_A_FT_F_GetIndicators(_facility, _idParent, ref id);
                    }
                }
                private void S_GA_A_FT_F_GetGeographicAreaChild(Condesus.EMS.Business.RG.IColumnsReport _item, ref Int64 id, Int64? idGeoAreaParent)
                {
                    //Ahora por cada Area, cargo las hijas
                    var _geographicAreas = from geoarea in _item.Child()
                                           orderby geoarea.Name ascending
                                           select geoarea;
                    foreach (Condesus.EMS.Business.RG.IColumnsReport _geographicArea in _geographicAreas)
                    {
                        //El scope es el padre de todas las actividades que se cargan aca...
                        AddTableRow(id, idGeoAreaParent, _geographicArea.Name,
                            Common.Functions.CustomEMSRound(_geographicArea.Result_tCO2e),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_CO2),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_CH4),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_N2O),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_PFC),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_HFC),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_SF6),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_HCT),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_HCNM),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_C2H6),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_C3H8),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_C4H10),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_CO),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_NOx),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_SOx),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_SO2),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_H2S),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_PM),
                            Common.Functions.CustomEMSRound(_geographicArea.Result_PM10));

                        Int64 _idParent = id;
                        id++;

                        //Cargo las Activities de esta Area Geografica
                        S_GA_A_FT_F_GetActivities(_geographicArea, _idParent, ref id);

                        //Ahora le pido los hijos de esta Area Geografica
                        S_GA_A_FT_F_GetGeographicAreaChild(_geographicArea, ref id, _idParent);
                    }
                }
                private void S_GA_A_FT_F_GetActivitiesChild(Condesus.EMS.Business.RG.IColumnsReport _item, ref Int64 id, Int64? idScopeFather)
                {
                    //Ahora por cada Scope, cargo las actividades
                    var _activities = from aa in _item.Child()
                                      //where aa.Result != 0
                                      orderby aa.Name ascending
                                      select aa;
                    foreach (Condesus.EMS.Business.RG.IColumnsReport _activity in _activities)
                    {
                        //El scope es el padre de todas las actividades que se cargan aca...
                        AddTableRow(id, idScopeFather, _activity.Name,
                            Common.Functions.CustomEMSRound(_activity.Result_tCO2e),
                            Common.Functions.CustomEMSRound(_activity.Result_CO2),
                            Common.Functions.CustomEMSRound(_activity.Result_CH4),
                            Common.Functions.CustomEMSRound(_activity.Result_N2O),
                            Common.Functions.CustomEMSRound(_activity.Result_PFC),
                            Common.Functions.CustomEMSRound(_activity.Result_HFC),
                            Common.Functions.CustomEMSRound(_activity.Result_SF6),
                            Common.Functions.CustomEMSRound(_activity.Result_HCT),
                            Common.Functions.CustomEMSRound(_activity.Result_HCNM),
                            Common.Functions.CustomEMSRound(_activity.Result_C2H6),
                            Common.Functions.CustomEMSRound(_activity.Result_C3H8),
                            Common.Functions.CustomEMSRound(_activity.Result_C4H10),
                            Common.Functions.CustomEMSRound(_activity.Result_CO),
                            Common.Functions.CustomEMSRound(_activity.Result_NOx),
                            Common.Functions.CustomEMSRound(_activity.Result_SOx),
                            Common.Functions.CustomEMSRound(_activity.Result_SO2),
                            Common.Functions.CustomEMSRound(_activity.Result_H2S),
                            Common.Functions.CustomEMSRound(_activity.Result_PM),
                            Common.Functions.CustomEMSRound(_activity.Result_PM10));

                        Int64? _idParent = id;
                        id++;

                        //Cargo los facility Types de esta Actividad
                        S_GA_A_FT_F_GetFacilityTypes(_activity, _idParent, ref id);

                        //Carga los hijos
                        S_GA_A_FT_F_GetActivitiesChild(_activity, ref id, _idParent);
                    }
                }
                private void S_GA_A_FT_F_GetFacilitiesChild(Condesus.EMS.Business.RG.IColumnsReport _item, ref Int64 id, Int64? idFacilityTypeFather)
                {
                    //Ahora por cada Scope, cargo las actividades
                    var _facilities = from aa in _item.Child()
                                      //where aa.Result != 0
                                      orderby aa.Name ascending
                                      select aa;
                    foreach (Condesus.EMS.Business.RG.IColumnsReport _facility in _facilities)
                    {
                        //El scope es el padre de todas las actividades que se cargan aca...
                        AddTableRow(id, idFacilityTypeFather, _facility.Name,
                            Common.Functions.CustomEMSRound(_facility.Result_tCO2e),
                            Common.Functions.CustomEMSRound(_facility.Result_CO2),
                            Common.Functions.CustomEMSRound(_facility.Result_CH4),
                            Common.Functions.CustomEMSRound(_facility.Result_N2O),
                            Common.Functions.CustomEMSRound(_facility.Result_PFC),
                            Common.Functions.CustomEMSRound(_facility.Result_HFC),
                            Common.Functions.CustomEMSRound(_facility.Result_SF6),
                            Common.Functions.CustomEMSRound(_facility.Result_HCT),
                            Common.Functions.CustomEMSRound(_facility.Result_HCNM),
                            Common.Functions.CustomEMSRound(_facility.Result_C2H6),
                            Common.Functions.CustomEMSRound(_facility.Result_C3H8),
                            Common.Functions.CustomEMSRound(_facility.Result_C4H10),
                            Common.Functions.CustomEMSRound(_facility.Result_CO),
                            Common.Functions.CustomEMSRound(_facility.Result_NOx),
                            Common.Functions.CustomEMSRound(_facility.Result_SOx),
                            Common.Functions.CustomEMSRound(_facility.Result_SO2),
                            Common.Functions.CustomEMSRound(_facility.Result_H2S),
                            Common.Functions.CustomEMSRound(_facility.Result_PM),
                            Common.Functions.CustomEMSRound(_facility.Result_PM10));

                        Int64? _idParent = id;
                        id++;

                        S_GA_A_FT_F_GetFacilitiesChild(_facility, ref id, _idParent);
                        
                        S_GA_A_FT_F_GetIndicators(_facility, _idParent, ref id);
                    }
                }
                private void S_GA_A_FT_F_GetIndicators(Condesus.EMS.Business.RG.IColumnsReport facility, Int64? idFacilityFather, ref Int64 id)
                {
                    var _indicators = from aa in facility.Items()
                                      orderby aa.Name ascending
                                      select aa;
                    foreach (Condesus.EMS.Business.RG.IColumnsReport _indicator in _indicators)
                    {
                        //El scope es el padre de todas las actividades que se cargan aca...
                        AddTableRow(id, idFacilityFather, _indicator.Name,
                            Common.Functions.CustomEMSRound(_indicator.Result_tCO2e),
                            Common.Functions.CustomEMSRound(_indicator.Result_CO2),
                            Common.Functions.CustomEMSRound(_indicator.Result_CH4),
                            Common.Functions.CustomEMSRound(_indicator.Result_N2O),
                            Common.Functions.CustomEMSRound(_indicator.Result_PFC),
                            Common.Functions.CustomEMSRound(_indicator.Result_HFC),
                            Common.Functions.CustomEMSRound(_indicator.Result_SF6),
                            Common.Functions.CustomEMSRound(_indicator.Result_HCT),
                            Common.Functions.CustomEMSRound(_indicator.Result_HCNM),
                            Common.Functions.CustomEMSRound(_indicator.Result_C2H6),
                            Common.Functions.CustomEMSRound(_indicator.Result_C3H8),
                            Common.Functions.CustomEMSRound(_indicator.Result_C4H10),
                            Common.Functions.CustomEMSRound(_indicator.Result_CO),
                            Common.Functions.CustomEMSRound(_indicator.Result_NOx),
                            Common.Functions.CustomEMSRound(_indicator.Result_SOx),
                            Common.Functions.CustomEMSRound(_indicator.Result_SO2),
                            Common.Functions.CustomEMSRound(_indicator.Result_H2S),
                            Common.Functions.CustomEMSRound(_indicator.Result_PM),
                            Common.Functions.CustomEMSRound(_indicator.Result_PM10));

                        Int64 _idParent = id;
                        id++;
                    }
                }
            #endregion

            #region Reporte S_A_FT_F
                private void LoadGridReport_S_A_FT_F()
                {
                    DateTime _startDate = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;
                    DateTime _endDate = (DateTime)System.Data.SqlTypes.SqlDateTime.MaxValue;
                    if (ManageEntityParams["StartDate"] != null)
                    { _startDate = Convert.ToDateTime(ManageEntityParams["StartDate"]); }
                    if (ManageEntityParams["EndDate"] != null)
                    { _endDate = Convert.ToDateTime(ManageEntityParams["EndDate"]); }

                    Int64 _idProcess = Convert.ToInt64(ManageEntityParams["IdProcess"]);
                    Int64 _idIndicator_tnCO2e = Convert.ToInt64(ManageEntityParams["IdIndicator_tnCO2e"]);
                    Int64 _idIndicator_CO2 = Convert.ToInt64(ManageEntityParams["IdIndicator_CO2"]);
                    Int64 _idIndicator_CH4 = Convert.ToInt64(ManageEntityParams["IdIndicator_CH4"]);
                    Int64 _idIndicator_N2O = Convert.ToInt64(ManageEntityParams["IdIndicator_N2O"]);
                    Int64 _idIndicator_PFC = Convert.ToInt64(ManageEntityParams["IdIndicator_PFC"]);
                    Int64 _idIndicator_HFC = Convert.ToInt64(ManageEntityParams["IdIndicator_HFC"]);
                    Int64 _idIndicator_SF6 = Convert.ToInt64(ManageEntityParams["IdIndicator_SF6"]);
                    Int64 _idIndicator_HCNM = Convert.ToInt64(ManageEntityParams["IdIndicator_HCNM"]);
                    Int64 _idIndicator_HCT = Convert.ToInt64(ManageEntityParams["IdIndicator_HCT"]);
                    Int64 _idIndicator_CO = Convert.ToInt64(ManageEntityParams["IdIndicator_CO"]);
                    Int64 _idIndicator_NOx = Convert.ToInt64(ManageEntityParams["IdIndicator_NOx"]);
                    Int64 _idIndicator_SOx = Convert.ToInt64(ManageEntityParams["IdIndicator_SOx"]);
                    Int64 _idIndicator_PM = Convert.ToInt64(ManageEntityParams["IdIndicator_PM"]);
                    Int64 _idIndicator_SO2 = Convert.ToInt64(ManageEntityParams["IdIndicator_SO2"]);
                    Int64 _idIndicator_H2S = Convert.ToInt64(ManageEntityParams["IdIndicator_H2S"]);
                    Int64 _idIndicator_PM10 = Convert.ToInt64(ManageEntityParams["IdIndicator_PM10"]);
                    Int64 _idIndicator_C2H6 = Convert.ToInt64(ManageEntityParams["IdIndicator_C2H6"]);
                    Int64 _idIndicator_C3H8 = Convert.ToInt64(ManageEntityParams["IdIndicator_C3H8"]);
                    Int64 _idIndicator_C4H10 = Convert.ToInt64(ManageEntityParams["IdIndicator_C4H10"]);


                    List<Condesus.EMS.Business.RG.IColumnsReport> _listS = ((ProcessGroupProcess)EMSLibrary.User.ProcessFramework.Map.Process(_idProcess)).Report_S_A_FT_F.S(_idIndicator_tnCO2e, _idIndicator_CO2,
                        _idIndicator_CH4, _idIndicator_N2O, _idIndicator_PFC, _idIndicator_HFC, _idIndicator_SF6, _idIndicator_HCT,
                        _idIndicator_HCNM, _idIndicator_C2H6, _idIndicator_C3H8, _idIndicator_C4H10, _idIndicator_CO, _idIndicator_NOx,
                        _idIndicator_SOx, _idIndicator_SO2, _idIndicator_H2S, _idIndicator_PM, _idIndicator_PM10, _startDate, _endDate);

                    Int64 _id = 1;

                    //Empezamos con los ROOT de Scopes
                    foreach (Condesus.EMS.Business.RG.IColumnsReport _icolumns in _listS)
                    {
                        AddTableRow(_id, null, _icolumns.Name, 
                            Common.Functions.CustomEMSRound(_icolumns.Result_tCO2e),
                            Common.Functions.CustomEMSRound(_icolumns.Result_CO2),
                            Common.Functions.CustomEMSRound(_icolumns.Result_CH4),
                            Common.Functions.CustomEMSRound(_icolumns.Result_N2O),
                            Common.Functions.CustomEMSRound(_icolumns.Result_PFC),
                            Common.Functions.CustomEMSRound(_icolumns.Result_HFC),
                            Common.Functions.CustomEMSRound(_icolumns.Result_SF6),
                            Common.Functions.CustomEMSRound(_icolumns.Result_HCT),
                            Common.Functions.CustomEMSRound(_icolumns.Result_HCNM),
                            Common.Functions.CustomEMSRound(_icolumns.Result_C2H6),
                            Common.Functions.CustomEMSRound(_icolumns.Result_C3H8),
                            Common.Functions.CustomEMSRound(_icolumns.Result_C4H10),
                            Common.Functions.CustomEMSRound(_icolumns.Result_CO),
                            Common.Functions.CustomEMSRound(_icolumns.Result_NOx),
                            Common.Functions.CustomEMSRound(_icolumns.Result_SOx),
                            Common.Functions.CustomEMSRound(_icolumns.Result_SO2),
                            Common.Functions.CustomEMSRound(_icolumns.Result_H2S),
                            Common.Functions.CustomEMSRound(_icolumns.Result_PM),
                            Common.Functions.CustomEMSRound(_icolumns.Result_PM10));

                        Int64 _idParent = _id;
                        _id++;

                        //Cargo las ACtivities para el Scope
                        S_A_FT_F_GetActivities(_icolumns, _idParent, ref _id);
                    }
                }
                private void S_A_FT_F_GetActivities(Condesus.EMS.Business.RG.IColumnsReport scope, Int64 idScopeFather, ref Int64 id)
                {
                    var _activities = from aa in scope.Items()
                                      orderby aa.Name ascending
                                      select aa;
                    foreach (Condesus.EMS.Business.RG.IColumnsReport _activity in _activities)
                    {
                        //El scope es el padre de todas las actividades que se cargan aca...
                        AddTableRow(id, idScopeFather, _activity.Name,
                            Common.Functions.CustomEMSRound(_activity.Result_tCO2e),
                            Common.Functions.CustomEMSRound(_activity.Result_CO2),
                            Common.Functions.CustomEMSRound(_activity.Result_CH4),
                            Common.Functions.CustomEMSRound(_activity.Result_N2O),
                            Common.Functions.CustomEMSRound(_activity.Result_PFC),
                            Common.Functions.CustomEMSRound(_activity.Result_HFC),
                            Common.Functions.CustomEMSRound(_activity.Result_SF6),
                            Common.Functions.CustomEMSRound(_activity.Result_HCT),
                            Common.Functions.CustomEMSRound(_activity.Result_HCNM),
                            Common.Functions.CustomEMSRound(_activity.Result_C2H6),
                            Common.Functions.CustomEMSRound(_activity.Result_C3H8),
                            Common.Functions.CustomEMSRound(_activity.Result_C4H10),
                            Common.Functions.CustomEMSRound(_activity.Result_CO),
                            Common.Functions.CustomEMSRound(_activity.Result_NOx),
                            Common.Functions.CustomEMSRound(_activity.Result_SOx),
                            Common.Functions.CustomEMSRound(_activity.Result_SO2),
                            Common.Functions.CustomEMSRound(_activity.Result_H2S),
                            Common.Functions.CustomEMSRound(_activity.Result_PM),
                            Common.Functions.CustomEMSRound(_activity.Result_PM10));

                        Int64 _idParent = id;
                        id++;

                        //Cargo los facility Types de esta Actividad
                        S_A_FT_F_GetFacilityTypes(_activity, _idParent, ref id);

                        //Con esto cargo todos los hijos de esta actividad...
                        S_A_FT_F_GetActivitiesChild(_activity, ref id, _idParent);
                    }
                }
                private void S_A_FT_F_GetFacilityTypes(Condesus.EMS.Business.RG.IColumnsReport activity, Int64? idActivityFather, ref Int64 id)
                {
                    List<Condesus.EMS.Business.RG.IColumnsReport> _facilities = activity.Items();
                    var _facilityTypes = from aa in _facilities
                                         orderby aa.Name ascending
                                         select aa;
                    foreach (Condesus.EMS.Business.RG.IColumnsReport _item in _facilityTypes)
                    {
                        AddTableRow(id, idActivityFather, _item.Name,
                            Common.Functions.CustomEMSRound(_item.Result_tCO2e),
                            Common.Functions.CustomEMSRound(_item.Result_CO2),
                            Common.Functions.CustomEMSRound(_item.Result_CH4),
                            Common.Functions.CustomEMSRound(_item.Result_N2O),
                            Common.Functions.CustomEMSRound(_item.Result_PFC),
                            Common.Functions.CustomEMSRound(_item.Result_HFC),
                            Common.Functions.CustomEMSRound(_item.Result_SF6),
                            Common.Functions.CustomEMSRound(_item.Result_HCT),
                            Common.Functions.CustomEMSRound(_item.Result_HCNM),
                            Common.Functions.CustomEMSRound(_item.Result_C2H6),
                            Common.Functions.CustomEMSRound(_item.Result_C3H8),
                            Common.Functions.CustomEMSRound(_item.Result_C4H10),
                            Common.Functions.CustomEMSRound(_item.Result_CO),
                            Common.Functions.CustomEMSRound(_item.Result_NOx),
                            Common.Functions.CustomEMSRound(_item.Result_SOx),
                            Common.Functions.CustomEMSRound(_item.Result_SO2),
                            Common.Functions.CustomEMSRound(_item.Result_H2S),
                            Common.Functions.CustomEMSRound(_item.Result_PM),
                            Common.Functions.CustomEMSRound(_item.Result_PM10));

                        Int64 _idActivityFather = id;
                        id++;

                        S_A_FT_F_GetFacilities(_item, _idActivityFather, ref id);
                    }
                }
                private void S_A_FT_F_GetFacilities(Condesus.EMS.Business.RG.IColumnsReport facilityType, Int64 idFacilityTypeFather, ref Int64 id)
                {
                    var _facilities = from aa in facilityType.Items()
                                      orderby aa.Name ascending
                                      select aa;
                    foreach (Condesus.EMS.Business.RG.IColumnsReport _facility in _facilities)
                    {
                        //El scope es el padre de todas las actividades que se cargan aca...
                        AddTableRow(id, idFacilityTypeFather, _facility.Name,
                            Common.Functions.CustomEMSRound(_facility.Result_tCO2e),
                            Common.Functions.CustomEMSRound(_facility.Result_CO2),
                            Common.Functions.CustomEMSRound(_facility.Result_CH4),
                            Common.Functions.CustomEMSRound(_facility.Result_N2O),
                            Common.Functions.CustomEMSRound(_facility.Result_PFC),
                            Common.Functions.CustomEMSRound(_facility.Result_HFC),
                            Common.Functions.CustomEMSRound(_facility.Result_SF6),
                            Common.Functions.CustomEMSRound(_facility.Result_HCT),
                            Common.Functions.CustomEMSRound(_facility.Result_HCNM),
                            Common.Functions.CustomEMSRound(_facility.Result_C2H6),
                            Common.Functions.CustomEMSRound(_facility.Result_C3H8),
                            Common.Functions.CustomEMSRound(_facility.Result_C4H10),
                            Common.Functions.CustomEMSRound(_facility.Result_CO),
                            Common.Functions.CustomEMSRound(_facility.Result_NOx),
                            Common.Functions.CustomEMSRound(_facility.Result_SOx),
                            Common.Functions.CustomEMSRound(_facility.Result_SO2),
                            Common.Functions.CustomEMSRound(_facility.Result_H2S),
                            Common.Functions.CustomEMSRound(_facility.Result_PM),
                            Common.Functions.CustomEMSRound(_facility.Result_PM10));

                        Int64 _idParent = id;
                        id++;
                        //Con esto cargo todos los hijos de esta actividad...
                        S_A_FT_F_GetFacilitiesChild(_facility, ref id, _idParent);

                        S_A_FT_F_GetIndicators(_facility, _idParent, ref id);
                    }
                }
                private void S_A_FT_F_GetActivitiesChild(Condesus.EMS.Business.RG.IColumnsReport _item, ref Int64 id, Int64? idScopeFather)
                {
                    //Ahora por cada Scope, cargo las actividades
                    var _activities = from aa in _item.Child()
                                      //where aa.Result != 0
                                      orderby aa.Name ascending
                                      select aa;
                    foreach (Condesus.EMS.Business.RG.IColumnsReport _activity in _activities)
                    {
                        //El scope es el padre de todas las actividades que se cargan aca...
                        AddTableRow(id, idScopeFather, _activity.Name,
                            Common.Functions.CustomEMSRound(_activity.Result_tCO2e),
                            Common.Functions.CustomEMSRound(_activity.Result_CO2),
                            Common.Functions.CustomEMSRound(_activity.Result_CH4),
                            Common.Functions.CustomEMSRound(_activity.Result_N2O),
                            Common.Functions.CustomEMSRound(_activity.Result_PFC),
                            Common.Functions.CustomEMSRound(_activity.Result_HFC),
                            Common.Functions.CustomEMSRound(_activity.Result_SF6),
                            Common.Functions.CustomEMSRound(_activity.Result_HCT),
                            Common.Functions.CustomEMSRound(_activity.Result_HCNM),
                            Common.Functions.CustomEMSRound(_activity.Result_C2H6),
                            Common.Functions.CustomEMSRound(_activity.Result_C3H8),
                            Common.Functions.CustomEMSRound(_activity.Result_C4H10),
                            Common.Functions.CustomEMSRound(_activity.Result_CO),
                            Common.Functions.CustomEMSRound(_activity.Result_NOx),
                            Common.Functions.CustomEMSRound(_activity.Result_SOx),
                            Common.Functions.CustomEMSRound(_activity.Result_SO2),
                            Common.Functions.CustomEMSRound(_activity.Result_H2S),
                            Common.Functions.CustomEMSRound(_activity.Result_PM),
                            Common.Functions.CustomEMSRound(_activity.Result_PM10));

                        Int64? _idParent = id;
                        id++;

                        //Cargo los facility Types de esta Actividad
                        S_A_FT_F_GetFacilityTypes(_activity, _idParent, ref id);

                        //Carga los hijos
                        S_A_FT_F_GetActivitiesChild(_activity, ref id, _idParent);
                    }
                }
                private void S_A_FT_F_GetFacilitiesChild(Condesus.EMS.Business.RG.IColumnsReport _item, ref Int64 id, Int64? idFacilityTypeFather)
                {
                    //Ahora por cada Scope, cargo las actividades
                    var _facilities = from aa in _item.Child()
                                      //where aa.Result != 0
                                      orderby aa.Name ascending
                                      select aa;
                    foreach (Condesus.EMS.Business.RG.IColumnsReport _facility in _facilities)
                    {
                        //El scope es el padre de todas las actividades que se cargan aca...
                        AddTableRow(id, idFacilityTypeFather, _facility.Name,
                            Common.Functions.CustomEMSRound(_facility.Result_tCO2e),
                            Common.Functions.CustomEMSRound(_facility.Result_CO2),
                            Common.Functions.CustomEMSRound(_facility.Result_CH4),
                            Common.Functions.CustomEMSRound(_facility.Result_N2O),
                            Common.Functions.CustomEMSRound(_facility.Result_PFC),
                            Common.Functions.CustomEMSRound(_facility.Result_HFC),
                            Common.Functions.CustomEMSRound(_facility.Result_SF6),
                            Common.Functions.CustomEMSRound(_facility.Result_HCT),
                            Common.Functions.CustomEMSRound(_facility.Result_HCNM),
                            Common.Functions.CustomEMSRound(_facility.Result_C2H6),
                            Common.Functions.CustomEMSRound(_facility.Result_C3H8),
                            Common.Functions.CustomEMSRound(_facility.Result_C4H10),
                            Common.Functions.CustomEMSRound(_facility.Result_CO),
                            Common.Functions.CustomEMSRound(_facility.Result_NOx),
                            Common.Functions.CustomEMSRound(_facility.Result_SOx),
                            Common.Functions.CustomEMSRound(_facility.Result_SO2),
                            Common.Functions.CustomEMSRound(_facility.Result_H2S),
                            Common.Functions.CustomEMSRound(_facility.Result_PM),
                            Common.Functions.CustomEMSRound(_facility.Result_PM10));

                        Int64? _idParent = id;
                        id++;

                        S_A_FT_F_GetFacilitiesChild(_facility, ref id, _idParent);

                        S_A_FT_F_GetIndicators(_facility, _idParent, ref id);
                    }
                }
                private void S_A_FT_F_GetIndicators(Condesus.EMS.Business.RG.IColumnsReport facility, Int64? idFacilityFather, ref Int64 id)
                {
                    var _indicators = from aa in facility.Items()
                                      orderby aa.Name ascending
                                      select aa;
                    foreach (Condesus.EMS.Business.RG.IColumnsReport _indicator in _indicators)
                    {
                        //El scope es el padre de todas las actividades que se cargan aca...
                        AddTableRow(id, idFacilityFather, _indicator.Name,
                            Common.Functions.CustomEMSRound(_indicator.Result_tCO2e),
                            Common.Functions.CustomEMSRound(_indicator.Result_CO2),
                            Common.Functions.CustomEMSRound(_indicator.Result_CH4),
                            Common.Functions.CustomEMSRound(_indicator.Result_N2O),
                            Common.Functions.CustomEMSRound(_indicator.Result_PFC),
                            Common.Functions.CustomEMSRound(_indicator.Result_HFC),
                            Common.Functions.CustomEMSRound(_indicator.Result_SF6),
                            Common.Functions.CustomEMSRound(_indicator.Result_HCT),
                            Common.Functions.CustomEMSRound(_indicator.Result_HCNM),
                            Common.Functions.CustomEMSRound(_indicator.Result_C2H6),
                            Common.Functions.CustomEMSRound(_indicator.Result_C3H8),
                            Common.Functions.CustomEMSRound(_indicator.Result_C4H10),
                            Common.Functions.CustomEMSRound(_indicator.Result_CO),
                            Common.Functions.CustomEMSRound(_indicator.Result_NOx),
                            Common.Functions.CustomEMSRound(_indicator.Result_SOx),
                            Common.Functions.CustomEMSRound(_indicator.Result_SO2),
                            Common.Functions.CustomEMSRound(_indicator.Result_H2S),
                            Common.Functions.CustomEMSRound(_indicator.Result_PM),
                            Common.Functions.CustomEMSRound(_indicator.Result_PM10));

                        Int64 _idParent = id;
                        id++;
                    }
                }
            #endregion

            #region Reporte FT_F_S_A
                private void LoadGridReport_FT_F_S_A()
                {
                    DateTime _startDate = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;
                    DateTime _endDate = (DateTime)System.Data.SqlTypes.SqlDateTime.MaxValue;
                    if (ManageEntityParams["StartDate"] != null)
                    { _startDate = Convert.ToDateTime(ManageEntityParams["StartDate"]); }
                    if (ManageEntityParams["EndDate"] != null)
                    { _endDate = Convert.ToDateTime(ManageEntityParams["EndDate"]); }

                    Int64 _idProcess = Convert.ToInt64(ManageEntityParams["IdProcess"]);
                    Int64 _idIndicator_tnCO2e = Convert.ToInt64(ManageEntityParams["IdIndicator_tnCO2e"]);
                    Int64 _idIndicator_CO2 = Convert.ToInt64(ManageEntityParams["IdIndicator_CO2"]);
                    Int64 _idIndicator_CH4 = Convert.ToInt64(ManageEntityParams["IdIndicator_CH4"]);
                    Int64 _idIndicator_N2O = Convert.ToInt64(ManageEntityParams["IdIndicator_N2O"]);
                    Int64 _idIndicator_PFC = Convert.ToInt64(ManageEntityParams["IdIndicator_PFC"]);
                    Int64 _idIndicator_HFC = Convert.ToInt64(ManageEntityParams["IdIndicator_HFC"]);
                    Int64 _idIndicator_SF6 = Convert.ToInt64(ManageEntityParams["IdIndicator_SF6"]);
                    Int64 _idIndicator_HCNM = Convert.ToInt64(ManageEntityParams["IdIndicator_HCNM"]);
                    Int64 _idIndicator_HCT = Convert.ToInt64(ManageEntityParams["IdIndicator_HCT"]);
                    Int64 _idIndicator_CO = Convert.ToInt64(ManageEntityParams["IdIndicator_CO"]);
                    Int64 _idIndicator_NOx = Convert.ToInt64(ManageEntityParams["IdIndicator_NOx"]);
                    Int64 _idIndicator_SOx = Convert.ToInt64(ManageEntityParams["IdIndicator_SOx"]);
                    Int64 _idIndicator_PM = Convert.ToInt64(ManageEntityParams["IdIndicator_PM"]);
                    Int64 _idIndicator_SO2 = Convert.ToInt64(ManageEntityParams["IdIndicator_SO2"]);
                    Int64 _idIndicator_H2S = Convert.ToInt64(ManageEntityParams["IdIndicator_H2S"]);
                    Int64 _idIndicator_PM10 = Convert.ToInt64(ManageEntityParams["IdIndicator_PM10"]);
                    Int64 _idIndicator_C2H6 = Convert.ToInt64(ManageEntityParams["IdIndicator_C2H6"]);
                    Int64 _idIndicator_C3H8 = Convert.ToInt64(ManageEntityParams["IdIndicator_C3H8"]);
                    Int64 _idIndicator_C4H10 = Convert.ToInt64(ManageEntityParams["IdIndicator_C4H10"]);


                    List<Condesus.EMS.Business.RG.IColumnsReport> _listFT = ((ProcessGroupProcess)EMSLibrary.User.ProcessFramework.Map.Process(_idProcess)).Report_FT_F_S_A.FT(_idIndicator_tnCO2e, _idIndicator_CO2,
                        _idIndicator_CH4, _idIndicator_N2O, _idIndicator_PFC, _idIndicator_HFC, _idIndicator_SF6, _idIndicator_HCT,
                        _idIndicator_HCNM, _idIndicator_C2H6, _idIndicator_C3H8, _idIndicator_C4H10, _idIndicator_CO, _idIndicator_NOx,
                        _idIndicator_SOx, _idIndicator_SO2, _idIndicator_H2S, _idIndicator_PM, _idIndicator_PM10, _startDate, _endDate);

                    Int64 _id = 1;

                    //Empezamos con los ROOT de Area Geografica
                    foreach (Condesus.EMS.Business.RG.IColumnsReport _icolumns in _listFT)
                    {
                        AddTableRow(_id, null, _icolumns.Name, 
                            Common.Functions.CustomEMSRound(_icolumns.Result_tCO2e),
                            Common.Functions.CustomEMSRound(_icolumns.Result_CO2),
                            Common.Functions.CustomEMSRound(_icolumns.Result_CH4),
                            Common.Functions.CustomEMSRound(_icolumns.Result_N2O),
                            Common.Functions.CustomEMSRound(_icolumns.Result_PFC),
                            Common.Functions.CustomEMSRound(_icolumns.Result_HFC),
                            Common.Functions.CustomEMSRound(_icolumns.Result_SF6),
                            Common.Functions.CustomEMSRound(_icolumns.Result_HCT),
                            Common.Functions.CustomEMSRound(_icolumns.Result_HCNM),
                            Common.Functions.CustomEMSRound(_icolumns.Result_C2H6),
                            Common.Functions.CustomEMSRound(_icolumns.Result_C3H8),
                            Common.Functions.CustomEMSRound(_icolumns.Result_C4H10),
                            Common.Functions.CustomEMSRound(_icolumns.Result_CO),
                            Common.Functions.CustomEMSRound(_icolumns.Result_NOx),
                            Common.Functions.CustomEMSRound(_icolumns.Result_SOx),
                            Common.Functions.CustomEMSRound(_icolumns.Result_SO2),
                            Common.Functions.CustomEMSRound(_icolumns.Result_H2S),
                            Common.Functions.CustomEMSRound(_icolumns.Result_PM),
                            Common.Functions.CustomEMSRound(_icolumns.Result_PM10));

                        Int64 _idParent = _id;
                        _id++;

                        //Cargo el Facility
                        FT_F_S_A_GetFacilities(_icolumns, _idParent, ref _id);
                    }
                }
                private void FT_F_S_A_GetScopes(Condesus.EMS.Business.RG.IColumnsReport facility, Int64? idGAFather, ref Int64 id)
                {
                    //Despues de cargar el arbol de Area Geografica, me quedo con la ultima Area y desde ahi cuelgo los Scopes
                    List<Condesus.EMS.Business.RG.IColumnsReport> _scopes = facility.Items();
                    foreach (Condesus.EMS.Business.RG.IColumnsReport _item in _scopes)
                    {
                        AddTableRow(id, idGAFather, _item.Name,
                            Common.Functions.CustomEMSRound(_item.Result_tCO2e),
                            Common.Functions.CustomEMSRound(_item.Result_CO2),
                            Common.Functions.CustomEMSRound(_item.Result_CH4),
                            Common.Functions.CustomEMSRound(_item.Result_N2O),
                            Common.Functions.CustomEMSRound(_item.Result_PFC),
                            Common.Functions.CustomEMSRound(_item.Result_HFC),
                            Common.Functions.CustomEMSRound(_item.Result_SF6),
                            Common.Functions.CustomEMSRound(_item.Result_HCT),
                            Common.Functions.CustomEMSRound(_item.Result_HCNM),
                            Common.Functions.CustomEMSRound(_item.Result_C2H6),
                            Common.Functions.CustomEMSRound(_item.Result_C3H8),
                            Common.Functions.CustomEMSRound(_item.Result_C4H10),
                            Common.Functions.CustomEMSRound(_item.Result_CO),
                            Common.Functions.CustomEMSRound(_item.Result_NOx),
                            Common.Functions.CustomEMSRound(_item.Result_SOx),
                            Common.Functions.CustomEMSRound(_item.Result_SO2),
                            Common.Functions.CustomEMSRound(_item.Result_H2S),
                            Common.Functions.CustomEMSRound(_item.Result_PM),
                            Common.Functions.CustomEMSRound(_item.Result_PM10));

                        Int64 _idScopeFather = id;
                        id++;

                        FT_F_S_A_GetActivities(_item, _idScopeFather, ref id);
                    }
                }
                private void FT_F_S_A_GetActivities(Condesus.EMS.Business.RG.IColumnsReport scope, Int64 idScopeFather, ref Int64 id)
                {
                    var _activities = from aa in scope.Items()
                                      orderby aa.Name ascending
                                      select aa;
                    foreach (Condesus.EMS.Business.RG.IColumnsReport _activity in _activities)
                    {
                        //El scope es el padre de todas las actividades que se cargan aca...
                        AddTableRow(id, idScopeFather, _activity.Name,
                            Common.Functions.CustomEMSRound(_activity.Result_tCO2e),
                            Common.Functions.CustomEMSRound(_activity.Result_CO2),
                            Common.Functions.CustomEMSRound(_activity.Result_CH4),
                            Common.Functions.CustomEMSRound(_activity.Result_N2O),
                            Common.Functions.CustomEMSRound(_activity.Result_PFC),
                            Common.Functions.CustomEMSRound(_activity.Result_HFC),
                            Common.Functions.CustomEMSRound(_activity.Result_SF6),
                            Common.Functions.CustomEMSRound(_activity.Result_HCT),
                            Common.Functions.CustomEMSRound(_activity.Result_HCNM),
                            Common.Functions.CustomEMSRound(_activity.Result_C2H6),
                            Common.Functions.CustomEMSRound(_activity.Result_C3H8),
                            Common.Functions.CustomEMSRound(_activity.Result_C4H10),
                            Common.Functions.CustomEMSRound(_activity.Result_CO),
                            Common.Functions.CustomEMSRound(_activity.Result_NOx),
                            Common.Functions.CustomEMSRound(_activity.Result_SOx),
                            Common.Functions.CustomEMSRound(_activity.Result_SO2),
                            Common.Functions.CustomEMSRound(_activity.Result_H2S),
                            Common.Functions.CustomEMSRound(_activity.Result_PM),
                            Common.Functions.CustomEMSRound(_activity.Result_PM10));

                        Int64 _idParent = id;
                        id++;

                        //Con esto cargo todos los hijos de esta actividad...
                        FT_F_S_A_GetActivitiesChild(_activity, ref id, _idParent);
                        
                        //Con esto traemos los indicadores
                        FT_F_S_A_GetIndicators(_activity, _idParent, ref id);
                    }
                }
                private void FT_F_S_A_GetFacilities(Condesus.EMS.Business.RG.IColumnsReport facilityType, Int64 idFacilityTypeFather, ref Int64 id)
                {
                    var _facilities = from aa in facilityType.Items()
                                      orderby aa.Name ascending
                                      select aa;
                    foreach (Condesus.EMS.Business.RG.IColumnsReport _facility in _facilities)
                    {
                        //El scope es el padre de todas las actividades que se cargan aca...
                        AddTableRow(id, idFacilityTypeFather, _facility.Name,
                            Common.Functions.CustomEMSRound(_facility.Result_tCO2e),
                            Common.Functions.CustomEMSRound(_facility.Result_CO2),
                            Common.Functions.CustomEMSRound(_facility.Result_CH4),
                            Common.Functions.CustomEMSRound(_facility.Result_N2O),
                            Common.Functions.CustomEMSRound(_facility.Result_PFC),
                            Common.Functions.CustomEMSRound(_facility.Result_HFC),
                            Common.Functions.CustomEMSRound(_facility.Result_SF6),
                            Common.Functions.CustomEMSRound(_facility.Result_HCT),
                            Common.Functions.CustomEMSRound(_facility.Result_HCNM),
                            Common.Functions.CustomEMSRound(_facility.Result_C2H6),
                            Common.Functions.CustomEMSRound(_facility.Result_C3H8),
                            Common.Functions.CustomEMSRound(_facility.Result_C4H10),
                            Common.Functions.CustomEMSRound(_facility.Result_CO),
                            Common.Functions.CustomEMSRound(_facility.Result_NOx),
                            Common.Functions.CustomEMSRound(_facility.Result_SOx),
                            Common.Functions.CustomEMSRound(_facility.Result_SO2),
                            Common.Functions.CustomEMSRound(_facility.Result_H2S),
                            Common.Functions.CustomEMSRound(_facility.Result_PM),
                            Common.Functions.CustomEMSRound(_facility.Result_PM10));

                        Int64 _idParent = id;
                        id++;

                        //Cargo el Scope de esta Area Geografica
                        FT_F_S_A_GetScopes(_facility, _idParent, ref id);

                        //Con esto cargo todos los hijos de esta actividad...
                        FT_F_S_A_GetFacilitiesChild(_facility, ref id, _idParent);
                    }
                }
                private void FT_F_S_A_GetActivitiesChild(Condesus.EMS.Business.RG.IColumnsReport _item, ref Int64 id, Int64? idScopeFather)
                {
                    //Ahora por cada Scope, cargo las actividades
                    var _activities = from aa in _item.Child()
                                      //where aa.Result != 0
                                      orderby aa.Name ascending
                                      select aa;
                    foreach (Condesus.EMS.Business.RG.IColumnsReport _activity in _activities)
                    {
                        //El scope es el padre de todas las actividades que se cargan aca...
                        AddTableRow(id, idScopeFather, _activity.Name,
                            Common.Functions.CustomEMSRound(_activity.Result_tCO2e),
                            Common.Functions.CustomEMSRound(_activity.Result_CO2),
                            Common.Functions.CustomEMSRound(_activity.Result_CH4),
                            Common.Functions.CustomEMSRound(_activity.Result_N2O),
                            Common.Functions.CustomEMSRound(_activity.Result_PFC),
                            Common.Functions.CustomEMSRound(_activity.Result_HFC),
                            Common.Functions.CustomEMSRound(_activity.Result_SF6),
                            Common.Functions.CustomEMSRound(_activity.Result_HCT),
                            Common.Functions.CustomEMSRound(_activity.Result_HCNM),
                            Common.Functions.CustomEMSRound(_activity.Result_C2H6),
                            Common.Functions.CustomEMSRound(_activity.Result_C3H8),
                            Common.Functions.CustomEMSRound(_activity.Result_C4H10),
                            Common.Functions.CustomEMSRound(_activity.Result_CO),
                            Common.Functions.CustomEMSRound(_activity.Result_NOx),
                            Common.Functions.CustomEMSRound(_activity.Result_SOx),
                            Common.Functions.CustomEMSRound(_activity.Result_SO2),
                            Common.Functions.CustomEMSRound(_activity.Result_H2S),
                            Common.Functions.CustomEMSRound(_activity.Result_PM),
                            Common.Functions.CustomEMSRound(_activity.Result_PM10));

                        Int64? _idParent = id;
                        id++;

                        //Carga los hijos
                        FT_F_S_A_GetActivitiesChild(_activity, ref id, _idParent);

                        //Con esto traemos los indicadores
                        FT_F_S_A_GetIndicators(_activity, _idParent, ref id);
                    }
                }
                private void FT_F_S_A_GetFacilitiesChild(Condesus.EMS.Business.RG.IColumnsReport _item, ref Int64 id, Int64? idFacilityTypeFather)
                {
                    //Ahora por cada Scope, cargo las actividades
                    var _facilities = from aa in _item.Child()
                                      //where aa.Result != 0
                                      orderby aa.Name ascending
                                      select aa;
                    foreach (Condesus.EMS.Business.RG.IColumnsReport _facility in _facilities)
                    {
                        //El scope es el padre de todas las actividades que se cargan aca...
                        AddTableRow(id, idFacilityTypeFather, _facility.Name,
                            Common.Functions.CustomEMSRound(_facility.Result_tCO2e),
                            Common.Functions.CustomEMSRound(_facility.Result_CO2),
                            Common.Functions.CustomEMSRound(_facility.Result_CH4),
                            Common.Functions.CustomEMSRound(_facility.Result_N2O),
                            Common.Functions.CustomEMSRound(_facility.Result_PFC),
                            Common.Functions.CustomEMSRound(_facility.Result_HFC),
                            Common.Functions.CustomEMSRound(_facility.Result_SF6),
                            Common.Functions.CustomEMSRound(_facility.Result_HCT),
                            Common.Functions.CustomEMSRound(_facility.Result_HCNM),
                            Common.Functions.CustomEMSRound(_facility.Result_C2H6),
                            Common.Functions.CustomEMSRound(_facility.Result_C3H8),
                            Common.Functions.CustomEMSRound(_facility.Result_C4H10),
                            Common.Functions.CustomEMSRound(_facility.Result_CO),
                            Common.Functions.CustomEMSRound(_facility.Result_NOx),
                            Common.Functions.CustomEMSRound(_facility.Result_SOx),
                            Common.Functions.CustomEMSRound(_facility.Result_SO2),
                            Common.Functions.CustomEMSRound(_facility.Result_H2S),
                            Common.Functions.CustomEMSRound(_facility.Result_PM),
                            Common.Functions.CustomEMSRound(_facility.Result_PM10));

                        Int64? _idParent = id;
                        id++;

                        //Cargo el Scope de esta Area Geografica
                        FT_F_S_A_GetScopes(_facility, _idParent, ref id);

                        //Carga los facilities hijos
                        FT_F_S_A_GetFacilitiesChild(_facility, ref id, _idParent);
                    }
                }
                private void FT_F_S_A_GetIndicators(Condesus.EMS.Business.RG.IColumnsReport activity, Int64? idActivityFather, ref Int64 id)
                {
                    var _indicators = from aa in activity.Items()
                                      orderby aa.Name ascending
                                      select aa;
                    foreach (Condesus.EMS.Business.RG.IColumnsReport _indicator in _indicators)
                    {
                        //El scope es el padre de todas las actividades que se cargan aca...
                        AddTableRow(id, idActivityFather, _indicator.Name,
                            Common.Functions.CustomEMSRound(_indicator.Result_tCO2e),
                            Common.Functions.CustomEMSRound(_indicator.Result_CO2),
                            Common.Functions.CustomEMSRound(_indicator.Result_CH4),
                            Common.Functions.CustomEMSRound(_indicator.Result_N2O),
                            Common.Functions.CustomEMSRound(_indicator.Result_PFC),
                            Common.Functions.CustomEMSRound(_indicator.Result_HFC),
                            Common.Functions.CustomEMSRound(_indicator.Result_SF6),
                            Common.Functions.CustomEMSRound(_indicator.Result_HCT),
                            Common.Functions.CustomEMSRound(_indicator.Result_HCNM),
                            Common.Functions.CustomEMSRound(_indicator.Result_C2H6),
                            Common.Functions.CustomEMSRound(_indicator.Result_C3H8),
                            Common.Functions.CustomEMSRound(_indicator.Result_C4H10),
                            Common.Functions.CustomEMSRound(_indicator.Result_CO),
                            Common.Functions.CustomEMSRound(_indicator.Result_NOx),
                            Common.Functions.CustomEMSRound(_indicator.Result_SOx),
                            Common.Functions.CustomEMSRound(_indicator.Result_SO2),
                            Common.Functions.CustomEMSRound(_indicator.Result_H2S),
                            Common.Functions.CustomEMSRound(_indicator.Result_PM),
                            Common.Functions.CustomEMSRound(_indicator.Result_PM10));

                        Int64 _idParent = id;
                        id++;
                    }
                }
            #endregion

            #region Reporte O_S_A_FT_F
                private void LoadGridReport_O_S_A_FT_F()
                {
                    DateTime _startDate = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;
                    DateTime _endDate = (DateTime)System.Data.SqlTypes.SqlDateTime.MaxValue;
                    if (ManageEntityParams["StartDate"] != null)
                    { _startDate = Convert.ToDateTime(ManageEntityParams["StartDate"]); }
                    if (ManageEntityParams["EndDate"] != null)
                    { _endDate = Convert.ToDateTime(ManageEntityParams["EndDate"]); }

                    Int64 _idProcess = Convert.ToInt64(ManageEntityParams["IdProcess"]);
                    Int64 _idIndicator_tnCO2e = Convert.ToInt64(ManageEntityParams["IdIndicator_tnCO2e"]);
                    Int64 _idIndicator_CO2 = Convert.ToInt64(ManageEntityParams["IdIndicator_CO2"]);
                    Int64 _idIndicator_CH4 = Convert.ToInt64(ManageEntityParams["IdIndicator_CH4"]);
                    Int64 _idIndicator_N2O = Convert.ToInt64(ManageEntityParams["IdIndicator_N2O"]);
                    Int64 _idIndicator_PFC = Convert.ToInt64(ManageEntityParams["IdIndicator_PFC"]);
                    Int64 _idIndicator_HFC = Convert.ToInt64(ManageEntityParams["IdIndicator_HFC"]);
                    Int64 _idIndicator_SF6 = Convert.ToInt64(ManageEntityParams["IdIndicator_SF6"]);
                    Int64 _idIndicator_HCNM = Convert.ToInt64(ManageEntityParams["IdIndicator_HCNM"]);
                    Int64 _idIndicator_HCT = Convert.ToInt64(ManageEntityParams["IdIndicator_HCT"]);
                    Int64 _idIndicator_CO = Convert.ToInt64(ManageEntityParams["IdIndicator_CO"]);
                    Int64 _idIndicator_NOx = Convert.ToInt64(ManageEntityParams["IdIndicator_NOx"]);
                    Int64 _idIndicator_SOx = Convert.ToInt64(ManageEntityParams["IdIndicator_SOx"]);
                    Int64 _idIndicator_PM = Convert.ToInt64(ManageEntityParams["IdIndicator_PM"]);
                    Int64 _idIndicator_SO2 = Convert.ToInt64(ManageEntityParams["IdIndicator_SO2"]);
                    Int64 _idIndicator_H2S = Convert.ToInt64(ManageEntityParams["IdIndicator_H2S"]);
                    Int64 _idIndicator_PM10 = Convert.ToInt64(ManageEntityParams["IdIndicator_PM10"]);
                    Int64 _idIndicator_C2H6 = Convert.ToInt64(ManageEntityParams["IdIndicator_C2H6"]);
                    Int64 _idIndicator_C3H8 = Convert.ToInt64(ManageEntityParams["IdIndicator_C3H8"]);
                    Int64 _idIndicator_C4H10 = Convert.ToInt64(ManageEntityParams["IdIndicator_C4H10"]);


                    List<Condesus.EMS.Business.RG.IColumnsReport> _listO = ((ProcessGroupProcess)EMSLibrary.User.ProcessFramework.Map.Process(_idProcess)).Report_O_S_A_FT_F.O(_idIndicator_tnCO2e, _idIndicator_CO2,
                        _idIndicator_CH4, _idIndicator_N2O, _idIndicator_PFC, _idIndicator_HFC, _idIndicator_SF6, _idIndicator_HCT,
                        _idIndicator_HCNM, _idIndicator_C2H6, _idIndicator_C3H8, _idIndicator_C4H10, _idIndicator_CO, _idIndicator_NOx,
                        _idIndicator_SOx, _idIndicator_SO2, _idIndicator_H2S, _idIndicator_PM, _idIndicator_PM10, _startDate, _endDate);

                    Int64 _id = 1;

                    //Empezamos con los ROOT de Organizaciones
                    foreach (Condesus.EMS.Business.RG.IColumnsReport _icolumns in _listO)
                    {
                        AddTableRow(_id, null, _icolumns.Name, Common.Functions.CustomEMSRound(_icolumns.Result_tCO2e),
                            Common.Functions.CustomEMSRound(_icolumns.Result_CO2),
                            Common.Functions.CustomEMSRound(_icolumns.Result_CH4),
                            Common.Functions.CustomEMSRound(_icolumns.Result_N2O),
                            Common.Functions.CustomEMSRound(_icolumns.Result_PFC),
                            Common.Functions.CustomEMSRound(_icolumns.Result_HFC),
                            Common.Functions.CustomEMSRound(_icolumns.Result_SF6),
                            Common.Functions.CustomEMSRound(_icolumns.Result_HCT),
                            Common.Functions.CustomEMSRound(_icolumns.Result_HCNM),
                            Common.Functions.CustomEMSRound(_icolumns.Result_C2H6),
                            Common.Functions.CustomEMSRound(_icolumns.Result_C3H8),
                            Common.Functions.CustomEMSRound(_icolumns.Result_C4H10),
                            Common.Functions.CustomEMSRound(_icolumns.Result_CO),
                            Common.Functions.CustomEMSRound(_icolumns.Result_NOx),
                            Common.Functions.CustomEMSRound(_icolumns.Result_SOx),
                            Common.Functions.CustomEMSRound(_icolumns.Result_SO2),
                            Common.Functions.CustomEMSRound(_icolumns.Result_H2S),
                            Common.Functions.CustomEMSRound(_icolumns.Result_PM),
                            Common.Functions.CustomEMSRound(_icolumns.Result_PM10));

                        Int64 _idParent = _id;
                        _id++;

                        //Cargo el Scope de esta Organizacion
                        O_S_A_FT_F_GetScopes(_icolumns, _idParent, ref _id);
                    }
                }
                private void O_S_A_FT_F_GetScopes(Condesus.EMS.Business.RG.IColumnsReport organization, Int64 idOFather, ref Int64 id)
                {
                    //Despues de cargar el arbol de Area Geografica, me quedo con la ultima Area y desde ahi cuelgo los Scopes
                    List<Condesus.EMS.Business.RG.IColumnsReport> _scopes = organization.Items();
                    foreach (Condesus.EMS.Business.RG.IColumnsReport _item in _scopes)
                    {
                        AddTableRow(id, idOFather, _item.Name,
                            Common.Functions.CustomEMSRound(_item.Result_tCO2e),
                            Common.Functions.CustomEMSRound(_item.Result_CO2),
                            Common.Functions.CustomEMSRound(_item.Result_CH4),
                            Common.Functions.CustomEMSRound(_item.Result_N2O),
                            Common.Functions.CustomEMSRound(_item.Result_PFC),
                            Common.Functions.CustomEMSRound(_item.Result_HFC),
                            Common.Functions.CustomEMSRound(_item.Result_SF6),
                            Common.Functions.CustomEMSRound(_item.Result_HCT),
                            Common.Functions.CustomEMSRound(_item.Result_HCNM),
                            Common.Functions.CustomEMSRound(_item.Result_C2H6),
                            Common.Functions.CustomEMSRound(_item.Result_C3H8),
                            Common.Functions.CustomEMSRound(_item.Result_C4H10),
                            Common.Functions.CustomEMSRound(_item.Result_CO),
                            Common.Functions.CustomEMSRound(_item.Result_NOx),
                            Common.Functions.CustomEMSRound(_item.Result_SOx),
                            Common.Functions.CustomEMSRound(_item.Result_SO2),
                            Common.Functions.CustomEMSRound(_item.Result_H2S),
                            Common.Functions.CustomEMSRound(_item.Result_PM),
                            Common.Functions.CustomEMSRound(_item.Result_PM10));

                        Int64 _idScopeFather = id;
                        id++;

                        O_S_A_FT_F_GetActivities(_item, _idScopeFather, ref id);
                    }
                }
                private void O_S_A_FT_F_GetActivities(Condesus.EMS.Business.RG.IColumnsReport scope, Int64 idScopeFather, ref Int64 id)
                {
                    var _activities = from aa in scope.Items()
                                      orderby aa.Name ascending
                                      select aa;
                    foreach (Condesus.EMS.Business.RG.IColumnsReport _activity in _activities)
                    {
                        //El scope es el padre de todas las actividades que se cargan aca...
                        AddTableRow(id, idScopeFather, _activity.Name,
                            Common.Functions.CustomEMSRound(_activity.Result_tCO2e),
                            Common.Functions.CustomEMSRound(_activity.Result_CO2),
                            Common.Functions.CustomEMSRound(_activity.Result_CH4),
                            Common.Functions.CustomEMSRound(_activity.Result_N2O),
                            Common.Functions.CustomEMSRound(_activity.Result_PFC),
                            Common.Functions.CustomEMSRound(_activity.Result_HFC),
                            Common.Functions.CustomEMSRound(_activity.Result_SF6),
                            Common.Functions.CustomEMSRound(_activity.Result_HCT),
                            Common.Functions.CustomEMSRound(_activity.Result_HCNM),
                            Common.Functions.CustomEMSRound(_activity.Result_C2H6),
                            Common.Functions.CustomEMSRound(_activity.Result_C3H8),
                            Common.Functions.CustomEMSRound(_activity.Result_C4H10),
                            Common.Functions.CustomEMSRound(_activity.Result_CO),
                            Common.Functions.CustomEMSRound(_activity.Result_NOx),
                            Common.Functions.CustomEMSRound(_activity.Result_SOx),
                            Common.Functions.CustomEMSRound(_activity.Result_SO2),
                            Common.Functions.CustomEMSRound(_activity.Result_H2S),
                            Common.Functions.CustomEMSRound(_activity.Result_PM),
                            Common.Functions.CustomEMSRound(_activity.Result_PM10));

                        Int64 _idParent = id;
                        id++;

                        //Cargo los facility Types de esta Actividad
                        O_S_A_FT_F_GetFacilityTypes(_activity, _idParent, ref id);

                        //Con esto cargo todos los hijos de esta actividad...
                        O_S_A_FT_F_GetActivitiesChild(_activity, ref id, _idParent);
                    }
                }
                private void O_S_A_FT_F_GetFacilityTypes(Condesus.EMS.Business.RG.IColumnsReport activity, Int64? idActivityFather, ref Int64 id)
                {
                    List<Condesus.EMS.Business.RG.IColumnsReport> _facilities = activity.Items();
                    var _facilityTypes = from aa in _facilities
                                         orderby aa.Name ascending
                                         select aa;
                    foreach (Condesus.EMS.Business.RG.IColumnsReport _item in _facilityTypes)
                    {
                        AddTableRow(id, idActivityFather, _item.Name,
                            Common.Functions.CustomEMSRound(_item.Result_tCO2e),
                            Common.Functions.CustomEMSRound(_item.Result_CO2),
                            Common.Functions.CustomEMSRound(_item.Result_CH4),
                            Common.Functions.CustomEMSRound(_item.Result_N2O),
                            Common.Functions.CustomEMSRound(_item.Result_PFC),
                            Common.Functions.CustomEMSRound(_item.Result_HFC),
                            Common.Functions.CustomEMSRound(_item.Result_SF6),
                            Common.Functions.CustomEMSRound(_item.Result_HCT),
                            Common.Functions.CustomEMSRound(_item.Result_HCNM),
                            Common.Functions.CustomEMSRound(_item.Result_C2H6),
                            Common.Functions.CustomEMSRound(_item.Result_C3H8),
                            Common.Functions.CustomEMSRound(_item.Result_C4H10),
                            Common.Functions.CustomEMSRound(_item.Result_CO),
                            Common.Functions.CustomEMSRound(_item.Result_NOx),
                            Common.Functions.CustomEMSRound(_item.Result_SOx),
                            Common.Functions.CustomEMSRound(_item.Result_SO2),
                            Common.Functions.CustomEMSRound(_item.Result_H2S),
                            Common.Functions.CustomEMSRound(_item.Result_PM),
                            Common.Functions.CustomEMSRound(_item.Result_PM10));

                        Int64 _idActivityFather = id;
                        id++;

                        O_S_A_FT_F_GetFacilities(_item, _idActivityFather, ref id);
                    }
                }
                private void O_S_A_FT_F_GetFacilities(Condesus.EMS.Business.RG.IColumnsReport facilityType, Int64 idFacilityTypeFather, ref Int64 id)
                {
                    var _facilities = from aa in facilityType.Items()
                                      orderby aa.Name ascending
                                      select aa;
                    foreach (Condesus.EMS.Business.RG.IColumnsReport _facility in _facilities)
                    {
                        //El scope es el padre de todas las actividades que se cargan aca...
                        AddTableRow(id, idFacilityTypeFather, _facility.Name,
                            Common.Functions.CustomEMSRound(_facility.Result_tCO2e),
                            Common.Functions.CustomEMSRound(_facility.Result_CO2),
                            Common.Functions.CustomEMSRound(_facility.Result_CH4),
                            Common.Functions.CustomEMSRound(_facility.Result_N2O),
                            Common.Functions.CustomEMSRound(_facility.Result_PFC),
                            Common.Functions.CustomEMSRound(_facility.Result_HFC),
                            Common.Functions.CustomEMSRound(_facility.Result_SF6),
                            Common.Functions.CustomEMSRound(_facility.Result_HCT),
                            Common.Functions.CustomEMSRound(_facility.Result_HCNM),
                            Common.Functions.CustomEMSRound(_facility.Result_C2H6),
                            Common.Functions.CustomEMSRound(_facility.Result_C3H8),
                            Common.Functions.CustomEMSRound(_facility.Result_C4H10),
                            Common.Functions.CustomEMSRound(_facility.Result_CO),
                            Common.Functions.CustomEMSRound(_facility.Result_NOx),
                            Common.Functions.CustomEMSRound(_facility.Result_SOx),
                            Common.Functions.CustomEMSRound(_facility.Result_SO2),
                            Common.Functions.CustomEMSRound(_facility.Result_H2S),
                            Common.Functions.CustomEMSRound(_facility.Result_PM),
                            Common.Functions.CustomEMSRound(_facility.Result_PM10));

                        Int64 _idParent = id;
                        id++;
                        //Con esto cargo todos los hijos de esta actividad...
                        O_S_A_FT_F_GetFacilitiesChild(_facility, ref id, _idParent);

                        O_S_A_FT_F_GetIndicators(_facility, _idParent, ref id);
                    }
                }
                private void O_S_A_FT_F_GetActivitiesChild(Condesus.EMS.Business.RG.IColumnsReport _item, ref Int64 id, Int64? idScopeFather)
                {
                    //Ahora por cada Scope, cargo las actividades
                    var _activities = from aa in _item.Child()
                                      //where aa.Result != 0
                                      orderby aa.Name ascending
                                      select aa;
                    foreach (Condesus.EMS.Business.RG.IColumnsReport _activity in _activities)
                    {
                        //El scope es el padre de todas las actividades que se cargan aca...
                        AddTableRow(id, idScopeFather, _activity.Name,
                            Common.Functions.CustomEMSRound(_activity.Result_tCO2e),
                            Common.Functions.CustomEMSRound(_activity.Result_CO2),
                            Common.Functions.CustomEMSRound(_activity.Result_CH4),
                            Common.Functions.CustomEMSRound(_activity.Result_N2O),
                            Common.Functions.CustomEMSRound(_activity.Result_PFC),
                            Common.Functions.CustomEMSRound(_activity.Result_HFC),
                            Common.Functions.CustomEMSRound(_activity.Result_SF6),
                            Common.Functions.CustomEMSRound(_activity.Result_HCT),
                            Common.Functions.CustomEMSRound(_activity.Result_HCNM),
                            Common.Functions.CustomEMSRound(_activity.Result_C2H6),
                            Common.Functions.CustomEMSRound(_activity.Result_C3H8),
                            Common.Functions.CustomEMSRound(_activity.Result_C4H10),
                            Common.Functions.CustomEMSRound(_activity.Result_CO),
                            Common.Functions.CustomEMSRound(_activity.Result_NOx),
                            Common.Functions.CustomEMSRound(_activity.Result_SOx),
                            Common.Functions.CustomEMSRound(_activity.Result_SO2),
                            Common.Functions.CustomEMSRound(_activity.Result_H2S),
                            Common.Functions.CustomEMSRound(_activity.Result_PM),
                            Common.Functions.CustomEMSRound(_activity.Result_PM10));

                        Int64? _idParent = id;
                        id++;

                        //Cargo los facility Types de esta Actividad
                        O_S_A_FT_F_GetFacilityTypes(_activity, _idParent, ref id);

                        //Carga los hijos
                        O_S_A_FT_F_GetActivitiesChild(_activity, ref id, _idParent);
                    }
                }
                private void O_S_A_FT_F_GetFacilitiesChild(Condesus.EMS.Business.RG.IColumnsReport _item, ref Int64 id, Int64? idFacilityTypeFather)
                {
                    //Ahora por cada Scope, cargo las actividades
                    var _facilities = from aa in _item.Child()
                                      //where aa.Result != 0
                                      orderby aa.Name ascending
                                      select aa;
                    foreach (Condesus.EMS.Business.RG.IColumnsReport _facility in _facilities)
                    {
                        //El scope es el padre de todas las actividades que se cargan aca...
                        AddTableRow(id, idFacilityTypeFather, _facility.Name,
                            Common.Functions.CustomEMSRound(_facility.Result_tCO2e),
                            Common.Functions.CustomEMSRound(_facility.Result_CO2),
                            Common.Functions.CustomEMSRound(_facility.Result_CH4),
                            Common.Functions.CustomEMSRound(_facility.Result_N2O),
                            Common.Functions.CustomEMSRound(_facility.Result_PFC),
                            Common.Functions.CustomEMSRound(_facility.Result_HFC),
                            Common.Functions.CustomEMSRound(_facility.Result_SF6),
                            Common.Functions.CustomEMSRound(_facility.Result_HCT),
                            Common.Functions.CustomEMSRound(_facility.Result_HCNM),
                            Common.Functions.CustomEMSRound(_facility.Result_C2H6),
                            Common.Functions.CustomEMSRound(_facility.Result_C3H8),
                            Common.Functions.CustomEMSRound(_facility.Result_C4H10),
                            Common.Functions.CustomEMSRound(_facility.Result_CO),
                            Common.Functions.CustomEMSRound(_facility.Result_NOx),
                            Common.Functions.CustomEMSRound(_facility.Result_SOx),
                            Common.Functions.CustomEMSRound(_facility.Result_SO2),
                            Common.Functions.CustomEMSRound(_facility.Result_H2S),
                            Common.Functions.CustomEMSRound(_facility.Result_PM),
                            Common.Functions.CustomEMSRound(_facility.Result_PM10));

                        Int64? _idParent = id;
                        id++;

                        O_S_A_FT_F_GetFacilitiesChild(_facility, ref id, _idParent);

                        O_S_A_FT_F_GetIndicators(_facility, _idParent, ref id);
                    }
                }
                private void O_S_A_FT_F_GetIndicators(Condesus.EMS.Business.RG.IColumnsReport facility, Int64? idFacilityFather, ref Int64 id)
                {
                    var _indicators = from aa in facility.Items()
                                      orderby aa.Name ascending
                                      select aa;
                    foreach (Condesus.EMS.Business.RG.IColumnsReport _indicator in _indicators)
                    {
                        //El scope es el padre de todas las actividades que se cargan aca...
                        AddTableRow(id, idFacilityFather, _indicator.Name,
                            Common.Functions.CustomEMSRound(_indicator.Result_tCO2e),
                            Common.Functions.CustomEMSRound(_indicator.Result_CO2),
                            Common.Functions.CustomEMSRound(_indicator.Result_CH4),
                            Common.Functions.CustomEMSRound(_indicator.Result_N2O),
                            Common.Functions.CustomEMSRound(_indicator.Result_PFC),
                            Common.Functions.CustomEMSRound(_indicator.Result_HFC),
                            Common.Functions.CustomEMSRound(_indicator.Result_SF6),
                            Common.Functions.CustomEMSRound(_indicator.Result_HCT),
                            Common.Functions.CustomEMSRound(_indicator.Result_HCNM),
                            Common.Functions.CustomEMSRound(_indicator.Result_C2H6),
                            Common.Functions.CustomEMSRound(_indicator.Result_C3H8),
                            Common.Functions.CustomEMSRound(_indicator.Result_C4H10),
                            Common.Functions.CustomEMSRound(_indicator.Result_CO),
                            Common.Functions.CustomEMSRound(_indicator.Result_NOx),
                            Common.Functions.CustomEMSRound(_indicator.Result_SOx),
                            Common.Functions.CustomEMSRound(_indicator.Result_SO2),
                            Common.Functions.CustomEMSRound(_indicator.Result_H2S),
                            Common.Functions.CustomEMSRound(_indicator.Result_PM),
                            Common.Functions.CustomEMSRound(_indicator.Result_PM10));

                        Int64 _idParent = id;
                        id++;
                    }
                }
            #endregion

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
            protected void rtvOrganizations_NodeClick(object sender, RadTreeNodeEventArgs e)
            {
                Int64 _idOrganization = 0;
                _idOrganization = Convert.ToInt64(GetKeyValue(e.Node.Value, "IdOrganization"));
                Int64 _idScope = Convert.ToInt64(GetKeyValue(_RdcAccountingScope.SelectedValue, "IdScope"));

                LoadEvolutionReport(_idOrganization, _idScope);
                LoadChartEvolution(_idOrganization, _idScope);
                LoadChartEvolutionCO2(_idOrganization, _idScope);
                LoadChartEvolutionCH4(_idOrganization, _idScope);
                LoadChartEvolutionN2O(_idOrganization, _idScope);
            }
            protected void rblOptionChartType_SelectedIndexChanged(object sender, EventArgs e)
            {
                Int64 _idScope = Convert.ToInt64(GetKeyValue(_RdcScopeGeneral.SelectedValue, "IdScope"));

                if (((RadioButtonList)sender).SelectedValue == "Stacked")
                {
                    _ChartBarType = ChartSeriesType.StackedBar;
                }
                else
                {
                    _ChartBarType = ChartSeriesType.StackedBar100;
                }

                switch (rtsCharts.SelectedTab.Value)
                {
                    case "FacilityTypes":
                        LoadChartBarFacilityTypeByScope(_idScope);
                        break;
                    case "Activities":
                        LoadChartBarActivityByScope(_idScope);
                        break;
                    case "GeographicalAreas":
                        LoadChartBarStateByScope(_idScope);
                        break;
                }
            }
            protected void rtvSite_NodeExpand(object sender, RadTreeNodeEventArgs e)
            {
                NodeExpandSites(sender, e, true);
            }
            //protected void _RdcSite_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
            //{

            //    Int64 _idFacility = Convert.ToInt64(GetKeyValue(_RdcSite.SelectedValue, "IdFacility"));
            //    Int64 _idOrganization = Convert.ToInt64(GetKeyValue(_RdcSite.SelectedValue, "IdOrganization"));

            //    LoadChartBarActivityByScope1AndFacility(_idOrganization, _idFacility);
            //    LoadChartBarActivityByScope2AndFacility(_idOrganization, _idFacility);
            //}
            protected void _RtvSite_NodeClick(object sender, RadTreeNodeEventArgs e)
            {
                Int64 _idFacility = 0;
                if (_RtvSite.SelectedNode.Value.Contains("IdSector"))
                {
                    _idFacility = Convert.ToInt64(GetKeyValue(_RtvSite.SelectedNode.Value, "IdSector"));
                }
                else
                {
                    _idFacility = Convert.ToInt64(GetKeyValue(_RtvSite.SelectedNode.Value, "IdFacility"));
                }
                Int64 _idOrganization = Convert.ToInt64(GetKeyValue(_RtvSite.SelectedNode.Value, "IdOrganization"));
                Int64 _idScope = Convert.ToInt64(GetKeyValue(_RdcScopeGeneral.SelectedValue, "IdScope"));

                LoadChartBarActivityByScopeAndFacility(_idScope, _idOrganization, _idFacility);

                LoadChartPieScopeByFacility(_idFacility);
            }
            //protected void rgdHierarchy_ColumnCreated(object sender, GridColumnCreatedEventArgs e)
            //{
            //    if (e.Column.IsBoundToFieldName("Id") || e.Column.IsBoundToFieldName("IdParent"))
            //    {
            //        //Las columnas con id no se muestra
            //        e.Column.Visible = false;
            //    }
            //    else if (e.Column.IsBoundToFieldName("Name"))
            //    {
            //        //Son las 3 columnas que tienen texto, etonces se setea esto
            //        e.OwnerTableView.HorizontalAlign = HorizontalAlign.Left;
            //        e.Column.ItemStyle.CssClass = "rgdHierarchyColumn";
            //    }
            //    else if (e.Column is GridBoundColumn)
            //    {
            //        //El resto de las columnas
            //        e.Column.HeaderStyle.Width = Unit.Pixel(95);
            //        e.Column.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            //        e.Column.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            //        e.Column.ItemStyle.CssClass = "rgdHierarchyColumn";
            //    }
            //}
            //protected void rgdHierarchy_ItemCreated(object sender, GridItemEventArgs e)
            //{
            //    if ((e.Item is GridHeaderItem && e.Item.OwnerTableView != rgdHierarchy.MasterTableView) || (e.Item is GridNoRecordsItem))
            //    {
            //        e.Item.Style["display"] = "none";
            //    }
            //    if (e.Item is GridNestedViewItem)
            //    {
            //        e.Item.Style["display"] = "none";
            //    }
            //}
            //protected void rgdHierarchy_PreRender(object sender, EventArgs e)
            //{
            //    HideExpandColumnRecursive(rgdHierarchy.MasterTableView);
            //}
            protected void _RdcScopeGeneral_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
            {
                Int64 _idScope = Convert.ToInt64(GetKeyValue(e.Value, "IdScope"));

                switch (rtsCharts.SelectedTab.Value)
                {
                    case "FacilityTypes":
                        LoadChartBarFacilityTypeByScope(_idScope);
                        break;
                    case "Activities":
                        LoadChartBarActivityByScope(_idScope);
                        break;
                    case "GeographicalAreas":
                        LoadChartBarStateByScope(_idScope);
                        break;
                    case "Facilities":
                        Int64 _idFacility = 0;
                        if (_RtvSite.SelectedNode != null)
                        {
                            if (_RtvSite.SelectedNode.Value.Contains("IdSector"))
                            {
                                _idFacility = Convert.ToInt64(GetKeyValue(_RtvSite.SelectedNode.Value, "IdSector"));
                            }
                            else
                            {
                                _idFacility = Convert.ToInt64(GetKeyValue(_RtvSite.SelectedNode.Value, "IdFacility"));
                            }
                            Int64 _idOrganization = Convert.ToInt64(GetKeyValue(_RtvSite.SelectedNode.Value, "IdOrganization"));

                            LoadChartBarActivityByScopeAndFacility(_idScope, _idOrganization, _idFacility);
                        }
                        break;
                }
            }

            protected void chartLineEvolution_ItemDataBound(object sender, ChartItemDataBoundEventArgs e)
            {
                //Obtiene el activity y el value de los datos que vienen
                String _yearName = e.SeriesItem.Parent.Name;
                String _value = Common.Functions.CustomEMSRound((Decimal)e.SeriesItem.YValue);
                e.SeriesItem.Parent.Type = ChartSeriesType.Spline;
                //Setea en la leyenda y el tooltip con los datos.
                e.SeriesItem.ActiveRegion.Tooltip = _yearName + " = " + _value;
                e.SeriesItem.Name = _yearName;
            }

            protected void chartBarFacilityTypeByScope1_ItemDataBound(object sender, ChartItemDataBoundEventArgs e)
            {
                //Obtiene el activity y el value de los datos que vienen
                String _facilityTypeName = e.SeriesItem.Parent.Name;
                String _value = Common.Functions.CustomEMSRound((Decimal)e.SeriesItem.YValue);
                e.SeriesItem.Parent.Type = _ChartBarType;    // ChartSeriesType.StackedBar;
                //Setea en la leyenda y el tooltip con los datos.
                e.SeriesItem.ActiveRegion.Tooltip = _facilityTypeName + " = " + _value;
                e.SeriesItem.Name = _facilityTypeName;
            }
            protected void chartBarFacilityTypeByScope2_ItemDataBound(object sender, ChartItemDataBoundEventArgs e)
            {
                //Obtiene el activity y el value de los datos que vienen
                String _facilityTypeName = e.SeriesItem.Parent.Name;
                String _value = Common.Functions.CustomEMSRound((Decimal)e.SeriesItem.YValue);
                e.SeriesItem.Parent.Type = _ChartBarType;    // ChartSeriesType.StackedBar;
                //Setea en la leyenda y el tooltip con los datos.
                e.SeriesItem.ActiveRegion.Tooltip = _facilityTypeName + " = " + _value;
                e.SeriesItem.Name = _facilityTypeName;
            }

            protected void chartBarActivityByScope1_ItemDataBound(object sender, ChartItemDataBoundEventArgs e)
            {
                //Obtiene el activity y el value de los datos que vienen
                String _activity = e.SeriesItem.Parent.Name;
                String _value = Common.Functions.CustomEMSRound((Decimal)e.SeriesItem.YValue);
                e.SeriesItem.Parent.Type = _ChartBarType;    // ChartSeriesType.StackedBar;
                //Setea en la leyenda y el tooltip con los datos.
                e.SeriesItem.ActiveRegion.Tooltip = _activity + " = " + _value;
                e.SeriesItem.Name = _activity;
            }
            protected void chartBarActivityByScope2_ItemDataBound(object sender, ChartItemDataBoundEventArgs e)
            {
                //Obtiene el activity y el value de los datos que vienen
                String _activity = e.SeriesItem.Parent.Name;
                String _value = Common.Functions.CustomEMSRound((Decimal)e.SeriesItem.YValue);
                e.SeriesItem.Parent.Type = _ChartBarType;    // ChartSeriesType.StackedBar;
                //Setea en la leyenda y el tooltip con los datos.
                e.SeriesItem.ActiveRegion.Tooltip = _activity + " = " + _value;
                e.SeriesItem.Name = _activity;
            }

            protected void chartBarStateByScope1_ItemDataBound(object sender, ChartItemDataBoundEventArgs e)
            {
                //Obtiene el activity y el value de los datos que vienen
                String _state = e.SeriesItem.Parent.Name;
                String _value = Common.Functions.CustomEMSRound((Decimal)e.SeriesItem.YValue);
                e.SeriesItem.Parent.Type = _ChartBarType;    // ChartSeriesType.StackedBar;
                //Setea en la leyenda y el tooltip con los datos.
                e.SeriesItem.ActiveRegion.Tooltip = _state + " = " + _value;
                e.SeriesItem.Name = _state;
            }
            protected void chartBarStateByScope2_ItemDataBound(object sender, ChartItemDataBoundEventArgs e)
            {
                //Obtiene el activity y el value de los datos que vienen
                String _state = e.SeriesItem.Parent.Name;
                String _value = Common.Functions.CustomEMSRound((Decimal)e.SeriesItem.YValue);
                e.SeriesItem.Parent.Type = _ChartBarType;    // ChartSeriesType.StackedBar;
                //Setea en la leyenda y el tooltip con los datos.
                e.SeriesItem.ActiveRegion.Tooltip = _state + " = " + _value;
                e.SeriesItem.Name = _state;
            }

            protected void chartBarActivityByScope1AndFacility_ItemDataBound(object sender, ChartItemDataBoundEventArgs e)
            {
                //Obtiene el activity y el value de los datos que vienen
                String _activity = e.SeriesItem.Parent.Name;
                String _value = Common.Functions.CustomEMSRound((Decimal)e.SeriesItem.YValue);
                e.SeriesItem.Parent.Type = _ChartBarType;    // ChartSeriesType.StackedBar;
                //Setea en la leyenda y el tooltip con los datos.
                e.SeriesItem.ActiveRegion.Tooltip = _activity + " = " + _value;
                e.SeriesItem.Name = _activity;
            }
            protected void chartBarActivityByScope2AndFacility_ItemDataBound(object sender, ChartItemDataBoundEventArgs e)
            {
                //Obtiene el activity y el value de los datos que vienen
                String _activity = e.SeriesItem.Parent.Name;
                String _value = Common.Functions.CustomEMSRound((Decimal)e.SeriesItem.YValue);
                e.SeriesItem.Parent.Type = _ChartBarType;    // ChartSeriesType.StackedBar;
                //Setea en la leyenda y el tooltip con los datos.
                e.SeriesItem.ActiveRegion.Tooltip = _activity + " = " + _value;
                e.SeriesItem.Name = _activity;
            }

            protected void chartTotalScopeByIndicator_ItemDataBound(object sender, ChartItemDataBoundEventArgs e)
            {
                Int16 _indexColumnName = 0;
                Int16 _indexColumnValue = 1;
                Int16 _indexColumnPercentageValue = 2;
                //Obtiene el activity y el value de los datos que vienen
                String _scopeName = ((System.Data.DataRowView)(e.DataItem)).Row.Table.Rows[e.SeriesItem.Index][_indexColumnName].ToString();
                String _value = ((System.Data.DataRowView)(e.DataItem)).Row.Table.Rows[e.SeriesItem.Index][_indexColumnValue].ToString();
                String _percentageValue = ((System.Data.DataRowView)(e.DataItem)).Row.Table.Rows[e.SeriesItem.Index][_indexColumnPercentageValue].ToString();
                //Setea en la leyenda y el tooltip con los datos.
                e.SeriesItem.ActiveRegion.Tooltip = _scopeName + " =" + _percentageValue + "%";
                e.SeriesItem.Name = _scopeName + " = " + Common.Functions.CustomEMSRound(Convert.ToDecimal(_value)) + " tCO2e";
            }
            protected void chartTotalScopeByFacility_ItemDataBound(object sender, ChartItemDataBoundEventArgs e)
            {
                Int16 _indexColumnName = 0;
                Int16 _indexColumnValue = 1;
                Int16 _indexColumnPercentageValue = 2;
                //Obtiene el activity y el value de los datos que vienen
                String _scopeName = ((System.Data.DataRowView)(e.DataItem)).Row.Table.Rows[e.SeriesItem.Index][_indexColumnName].ToString();
                String _value = ((System.Data.DataRowView)(e.DataItem)).Row.Table.Rows[e.SeriesItem.Index][_indexColumnValue].ToString();
                String _percentageValue = ((System.Data.DataRowView)(e.DataItem)).Row.Table.Rows[e.SeriesItem.Index][_indexColumnPercentageValue].ToString();
                //Setea en la leyenda y el tooltip con los datos.
                e.SeriesItem.ActiveRegion.Tooltip = _scopeName + " =" + _percentageValue + "%";
                e.SeriesItem.Name = _scopeName + " = " + Common.Functions.CustomEMSRound(Convert.ToDecimal(_value)) + " tCO2e";
            }
            protected void chartTotalGas_ItemDataBound(object sender, ChartItemDataBoundEventArgs e)
            {
                //Obtiene el activity y el value de los datos que vienen
                String _scopeName = e.SeriesItem.Parent.Name;
                String _value = e.SeriesItem.YValue.ToString();
                e.SeriesItem.Parent.Type = ChartSeriesType.Pie;
                //Setea en la leyenda y el tooltip con los datos.
                e.SeriesItem.ActiveRegion.Tooltip = _scopeName + " = " + _value;
                e.SeriesItem.Name = _scopeName;
            }
            protected void HideLabels(RadChart radChart)
            {
                foreach (ChartSeries series in radChart.Series)
                {
                    series.Appearance.LabelAppearance.Visible = false;
                }
            }
            //protected void chartTotalScopeByIndicator_DataBound(object sender, EventArgs e)
            //{
            //    HideLabels((RadChart)sender);
            //    //HideLabels(chartTotalActivityByGas);
            //}
            //protected void chartTotalActivityByGas_DataBound(object sender, EventArgs e)
            //{
            //    HideLabels((RadChart)sender);
            //    //HideLabels(chartTotalActivityByGas);
            //}
            protected void charts_DataBound(object sender, EventArgs e)
            {
                HideLabels((RadChart)sender);
            }
            protected void rtsReportCalculationsOfTransformation_TabClick(object sender, RadTabStripEventArgs e)
            {
                LoadDataByTabSelected(e.Tab.Value);
            }
            protected void rtsCharts_TabClick(object sender, RadTabStripEventArgs e)
            {
                LoadDataChartByTabSelected(e.Tab.Value);
            }
            protected void lnkExportGridMeasurement_Click(object sender, EventArgs e)
            {   //Tengo que volver a construir la grilla plana.
                if (_ReportType == "Evolution")
                {
                    ConfigureExport(_RgdEvolution);

                    _RgdEvolution.MasterTableView.ExportToExcel();  
                }
                else
                {
                    LoadDataByTabSelected(rtsReportCalculationsOfTransformation.SelectedTab.Value);

                    Response.Clear();
                    Response.ContentType = "application/ms-excel";
                    Response.AddHeader("content-disposition", "attachment;filename=excelimage.xls");
                    System.IO.StringWriter sw = new System.IO.StringWriter();
                    HtmlTextWriter hw = new HtmlTextWriter(sw);
                    tblTreeTableReport.RenderControl(hw);
                    Response.Write(sw.ToString());
                    Response.End();
                }
            }

            //protected void lnkExport_Click(object sender, EventArgs e)
            //{
            //    Response.Clear();
            //    Response.ContentType = "application/ms-excel";
            //    Response.AddHeader("content-disposition", "attachment;filename=excelimage.xls");
            //    System.IO.StringWriter sw = new System.IO.StringWriter();
            //    HtmlTextWriter hw = new HtmlTextWriter(sw);
            //    tblTreeTableReport.RenderControl(hw);
            //    Response.Write(sw.ToString());
            //    Response.End();

            //}
            //protected void lnkExportGridMeasurement_Click(object sender, EventArgs e)
            //{   //Tengo que volver a construir la grilla plana.
            //    LoadGridTransformationMeasurement();

            //    ConfigureExport(_rgdMasterGridTransformationMeasurement);

            //    _rgdMasterGridTransformationMeasurement.MasterTableView.ExportToExcel();  
            //}
        #endregion

    }
}
