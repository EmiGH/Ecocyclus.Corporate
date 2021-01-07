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
using Condesus.EMS.WebUI.MasterControls;
using EBPE = Condesus.EMS.Business.PF.Entities;
using EBPAE = Condesus.EMS.Business.PA.Entities;
using Condesus.WebUI.Navigation;
using Condesus.EMS.Business.KC.Entities;
using Condesus.EMS.Business.PF.Entities;

namespace Condesus.EMS.WebUI.Dashboard
{
    public partial class GeographicTooltips : BasePage
    {
        #region Internal Properties
            private Boolean _TabChartVisible = false;
            private Int64 _IdEntity
            {
                get { return Convert.ToInt64(ViewState["IdEntity"]); }
                set { ViewState["IdEntity"] = value; }
            }
            private String  _EntityObject
            {
                get { return Convert.ToString(ViewState["EntityObject"]); }
                set { ViewState["EntityObject"] = value; }
            }
            private Int64 _IdProcess
            {
                get { return Convert.ToInt64(ViewState["IdProcess"]); }
                set { ViewState["IdProcess"] = value; }
            }
            private Int64 _IdOrganization
            {
                get { return Convert.ToInt64(ViewState["IdOrganization"]); }
                set { ViewState["IdOrganization"] = value; }
            }
            private Boolean _IsManageFacility
            {
                get { return Convert.ToBoolean(ViewState["IsManageFacility"]); }
                set { ViewState["IsManageFacility"] = value; }
            }
            private Boolean _IsDashBoardMonitoring
            {
                get { return Convert.ToBoolean(ViewState["IsDashBoardMonitoring"]); }
                set { ViewState["IsDashBoardMonitoring"] = value; }
            }      
            private const Int16 _TAB_MAIN_DATA = 0;
            private const Int16 _TAB_GRAPHIC = 1;
            private const Int16 _TAB_CHART_GEI = 2;
            private const Int16 _TAB_CHART_LOCALPOLLUTANTS = 3;
            private const Int16 _TAB_EXTENDED_INFO = 4;
            private const Int16 _TAB_FORUM_INFO = 5;
            private const Int16 _TAB_STATISTIC_INFO = 6;
            private const Int16 _TAB_REPORT_INFO = 7;
            private const Int16 _TAB_MEASUREMENT_INFO = 8;
            private const Int16 _TAB_SECTOR_INFO = 9;
            private const Int16 _TAB_PROCESS_ASOCIATED_INFO = 10;
        #endregion

        #region PageLoad & Init
            protected override void OnInit(EventArgs e)
            {
                base.OnInit(e);
            }
            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);

                if (Request.QueryString["IdEntity"] != null)
                {
                    _IdEntity = Convert.ToInt64(Request.QueryString["IdEntity"]);
                }
                if (Request.QueryString["EntityObject"] != null)
                {
                    _EntityObject = Convert.ToString(Request.QueryString["EntityObject"]);
                }
                if (Request.QueryString["IdProcess"] != null)
                {
                    _IdProcess = Convert.ToInt64(Request.QueryString["IdProcess"]);
                }
                if (Request.QueryString["IdOrganization"] != null)
                {
                    _IdOrganization= Convert.ToInt64(Request.QueryString["IdOrganization"]);
                }
                if (Request.QueryString["IsManageFacility"] != null)
                {
                    _IsManageFacility = Convert.ToBoolean(Request.QueryString["IsManageFacility"]);
                }
                if (Request.QueryString["IsDashBoardMonitoring"] != null)
                {
                    _IsDashBoardMonitoring = Convert.ToBoolean(Request.QueryString["IsDashBoardMonitoring"]);
                }

                InitializeHandlers();
                InjectOnClientTabToolTipInfoSelecting(rtsToolTipInformation.ClientID);
                LoadInfo();
            }
            protected void InitializeHandlers()
            {
                rtsToolTipInformation.OnClientTabSelecting = "rtsToolTipInformation_OnClientTabSelecting";
                rtsToolTipInformation.AutoPostBack = true;
            }
        #endregion
        
        #region Private Method
            protected void InjectOnClientTabToolTipInfoSelecting(String rmpWizzardTask)
            {
                //Esta funcion se ejecuta al hacer click sobre un item del menuRad
                System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

                _sbBuffer.Append(OpenHtmlJavaScript());

                //******************************************************
                //Este metodo vuelve a habilitar los validator de cada pageView.
                _sbBuffer.Append("function rtsToolTipInformation_OnClientTabSelecting(sender, args)                                         \n");
                _sbBuffer.Append("{                                                                                                         \n");
                _sbBuffer.Append("      document.getElementById('GeoToolTipGlobalUpdateProgress').style.display = 'block';           \n");
                _sbBuffer.Append("      //return false;                                                                                       \n");
                _sbBuffer.Append("}                                                                                                         \n");

                _sbBuffer.Append(CloseHtmlJavaScript());

                InjectJavascript("JS_OnClientTabToolTipInformationSelecting", _sbBuffer.ToString());
            }
            private void LoadInfo()
            {
                String _tabTitleMainInfo = Resources.Common.tabMainInfo;
                String _tabTitleExtendedProperty = Resources.Common.tabProperties;
                String _tabTitleForum = Resources.CommonListManage.Forum;
                String _tabTitleStatistics= Resources.CommonListManage.Statistics;
                String _tabTitleReport = Resources.Common.tabReports;
                String _tabTitleMeasurement = Resources.CommonListManage.Measurements;
                String _tabTitleSector = Resources.CommonListManage.Sectors;
                String _tabTitleProcessAsociated = Resources.CommonListManage.Processes;

                String _tabTitleGraphic = Resources.Common.tabPhotos;
                String _tabTitleChartLocalPollutants = Resources.Common.ChartCL;
                Boolean _tabExtendedPropertyIsVisible = false;
                Boolean _tabForumIsVisible = false;
                Boolean _tabStatisticIsVisible = false; 
                Boolean _tabReportIsVisible = false;
                Boolean _tabMeasurementIsVisible = false;
                Boolean _tabSectorIsVisible = false;
                Boolean _tabChartGEIIsVisible = false; 
                Boolean _tabChartLCIsVisible = false;
                Boolean _tabProcessAsociatedIsVisible = false;

                switch (_EntityObject)
                {
                    case Common.ConstantsEntitiesName.DS.Facility:
                        if (_IsManageFacility)
                        {   //Al ser el manage de Facility no tiene que mostrar las mediciones y se muestran los sectores
                            _tabMeasurementIsVisible = false;
                            _tabSectorIsVisible = true;
                            _tabChartLCIsVisible = false;
                            _TabChartVisible = false;
                            _tabChartGEIIsVisible = false;
                            _tabProcessAsociatedIsVisible = false;
                        }
                        else
                        {
                            if (_IsDashBoardMonitoring)
                            {
                                _tabMeasurementIsVisible = false;
                                _tabSectorIsVisible = false;
                                _tabChartLCIsVisible = false;
                                _TabChartVisible = false;
                                _tabChartGEIIsVisible = false;
                                _tabProcessAsociatedIsVisible = true;
                            }
                            else
                            {
                                //En el Dashboard de facility se muestran las mediciones
                                _tabMeasurementIsVisible = true;
                                _tabChartLCIsVisible = true;
                                //Muestra un chart, no fotos.
                                //_tabTitleGraphic = Resources.Common.ChartGEI;
                                //Es un measurement, se debe cargar el CHART.
                                _TabChartVisible = false;
                                _tabChartGEIIsVisible = true;
                                _tabProcessAsociatedIsVisible = false;
                            }
                        }
                        break;
                    case Common.ConstantsEntitiesName.PA.Measurement:
                        //Muestra un chart, no fotos.
                        _tabTitleGraphic = Resources.Common.tabChart;
                        //Es un measurement, se debe cargar el CHART.
                        _TabChartVisible = true;
                        _tabStatisticIsVisible = true;
                        _tabExtendedPropertyIsVisible = false;
                        _tabForumIsVisible = false;
                        _tabReportIsVisible = false;
                        _tabChartGEIIsVisible = false;
                        _tabChartLCIsVisible = false;
                        break;
                    case Common.ConstantsEntitiesName.PF.ProcessGroupProcess:
                    case Common.ConstantsEntitiesName.PF.Process:
                        _tabStatisticIsVisible = false;
                        _tabExtendedPropertyIsVisible = true;
                        _tabForumIsVisible = true;
                        _tabReportIsVisible = true;
                        _tabChartGEIIsVisible = false;
                        _tabChartLCIsVisible = false;
                        rtsToolTipInformation.Tabs[_TAB_REPORT_INFO].Attributes.Add("onclick", "javascript:NavigateToContentReport(this, event, " + Request.QueryString["IdEntity"] + ");");
                        break;
                    default:
                        break;
                }

                LoadDataByTabSelected(rtsToolTipInformation.SelectedTab.Index);

                //Ahora carga los textos al tab.
                rtsToolTipInformation.Tabs[_TAB_MAIN_DATA].ToolTip = _tabTitleMainInfo;
                rtsToolTipInformation.Tabs[_TAB_GRAPHIC].ToolTip = _tabTitleGraphic;
                rtsToolTipInformation.Tabs[_TAB_CHART_GEI].ToolTip = Resources.Common.ChartGEI;
                rtsToolTipInformation.Tabs[_TAB_CHART_LOCALPOLLUTANTS].ToolTip = _tabTitleChartLocalPollutants;
                rtsToolTipInformation.Tabs[_TAB_EXTENDED_INFO].ToolTip = _tabTitleExtendedProperty;
                rtsToolTipInformation.Tabs[_TAB_FORUM_INFO].ToolTip = _tabTitleForum;
                rtsToolTipInformation.Tabs[_TAB_STATISTIC_INFO].ToolTip = _tabTitleStatistics;
                rtsToolTipInformation.Tabs[_TAB_REPORT_INFO].ToolTip = _tabTitleReport;
                rtsToolTipInformation.Tabs[_TAB_MEASUREMENT_INFO].ToolTip = _tabTitleMeasurement;
                rtsToolTipInformation.Tabs[_TAB_SECTOR_INFO].ToolTip = _tabTitleSector;
                rtsToolTipInformation.Tabs[_TAB_PROCESS_ASOCIATED_INFO].ToolTip = _tabTitleProcessAsociated;

                //Descomentar, cuando tengamos acomodado los estilos
                rtsToolTipInformation.Tabs[_TAB_MAIN_DATA].Text = _tabTitleMainInfo;
                rtsToolTipInformation.Tabs[_TAB_GRAPHIC].Text = _tabTitleGraphic;
                rtsToolTipInformation.Tabs[_TAB_CHART_GEI].Text = Resources.Common.ChartGEI;
                rtsToolTipInformation.Tabs[_TAB_CHART_LOCALPOLLUTANTS].Text = _tabTitleChartLocalPollutants;
                rtsToolTipInformation.Tabs[_TAB_EXTENDED_INFO].Text = _tabTitleExtendedProperty;
                rtsToolTipInformation.Tabs[_TAB_FORUM_INFO].Text = _tabTitleForum;
                rtsToolTipInformation.Tabs[_TAB_STATISTIC_INFO].Text = _tabTitleStatistics;
                rtsToolTipInformation.Tabs[_TAB_REPORT_INFO].Text = _tabTitleReport;
                rtsToolTipInformation.Tabs[_TAB_MEASUREMENT_INFO].Text = _tabTitleMeasurement;
                rtsToolTipInformation.Tabs[_TAB_SECTOR_INFO].Text = _tabTitleSector;
                rtsToolTipInformation.Tabs[_TAB_PROCESS_ASOCIATED_INFO].Text = _tabTitleProcessAsociated;

                //Indica si Agrega el MoreInfo o no.
                rtsToolTipInformation.Tabs[_TAB_EXTENDED_INFO].Visible = _tabExtendedPropertyIsVisible;
                rtsToolTipInformation.Tabs[_TAB_FORUM_INFO].Visible = false;// _tabForumIsVisible;
                rtsToolTipInformation.Tabs[_TAB_STATISTIC_INFO].Visible = _tabStatisticIsVisible;
                rtsToolTipInformation.Tabs[_TAB_REPORT_INFO].Visible = _tabReportIsVisible;
                rtsToolTipInformation.Tabs[_TAB_MEASUREMENT_INFO].Visible = _tabMeasurementIsVisible;
                rtsToolTipInformation.Tabs[_TAB_SECTOR_INFO].Visible = _tabSectorIsVisible;
                rtsToolTipInformation.Tabs[_TAB_CHART_LOCALPOLLUTANTS].Visible = _tabChartLCIsVisible;
                rtsToolTipInformation.Tabs[_TAB_CHART_GEI].Visible = _tabChartGEIIsVisible;
                rtsToolTipInformation.Tabs[_TAB_PROCESS_ASOCIATED_INFO].Visible = _tabProcessAsociatedIsVisible;
            }
            private void LoadDataByTabSelected(int indexTabSelected)
            {
                switch (indexTabSelected)
                {
                    case _TAB_MAIN_DATA:
                        LoadMainData();
                        break;
                    case _TAB_GRAPHIC:
                        LoadGraphicContent();
                        break;
                    case _TAB_CHART_LOCALPOLLUTANTS:
                        LoadChartLocalPollutantContent();
                        break;
                    case _TAB_CHART_GEI:
                        LoadChartGEIContent();
                        break;
                    case _TAB_EXTENDED_INFO:
                        pnlExtendedInfo.Controls.Clear();
                        pnlExtendedInfo.Controls.Add(BuildProcessExtendedProperties(_IdEntity));
                        rmpGeoToolTip.SelectedIndex = 4;
                        rpvMoreInfo.Selected = true;
                        break;
                    case _TAB_FORUM_INFO:
                        pnlExtendedInfo.Controls.Clear(); 
                        pnlExtendedInfo.Controls.Add(BuildProcessForumTopics(_IdEntity));
                        rmpGeoToolTip.SelectedIndex = 4;
                        rpvMoreInfo.Selected = true;
                        break;
                    case _TAB_STATISTIC_INFO:
                        pnlExtendedInfo.Controls.Clear();
                        pnlExtendedInfo.Controls.Add(BuildMeasurementStats(_IdEntity));
                        rmpGeoToolTip.SelectedIndex = 4;
                        rpvMoreInfo.Selected = true;
                        break;
                    case _TAB_REPORT_INFO:
                        //No hace nada...lo hace el JS y despues el GeographicTooltipNavigate.
                        break;
                    case _TAB_MEASUREMENT_INFO:
                        pnlExtendedInfo.Controls.Clear();
                        pnlExtendedInfo.Controls.Add(BuildMeasurements(_IdProcess, _IdEntity));
                        rmpGeoToolTip.SelectedIndex = 4;
                        rpvMoreInfo.Selected = true;
                        break;
                    case _TAB_SECTOR_INFO:
                        pnlExtendedInfo.Controls.Clear();
                        pnlExtendedInfo.Controls.Add(BuildSectors(_IdOrganization, _IdEntity));
                        rmpGeoToolTip.SelectedIndex = 4;
                        rpvMoreInfo.Selected = true;
                        break;

                    case _TAB_PROCESS_ASOCIATED_INFO:
                        pnlExtendedInfo.Controls.Clear();
                        pnlExtendedInfo.Controls.Add(BuildProcessAsociatedToFacility(_IdEntity));
                        rmpGeoToolTip.SelectedIndex = 4;
                        rpvMoreInfo.Selected = true;
                        break;
                }
            }
            private void LoadMainData()
            {
                String _title = String.Empty;
                String _subTitle = String.Empty;
                String _classNameIcon = String.Empty;
                String _classNameTitle = String.Empty;
                Dictionary<Int64, CatalogDoc> _catalogDoc = new Dictionary<Int64, CatalogDoc>();

                //Construye el MainData
                pnlMainData.Controls.Add(BuildMainData(_EntityObject, _IdEntity, _IdProcess, ref _title, ref _subTitle, ref _classNameTitle, ref _classNameIcon, ref _catalogDoc));
                //Setea la imagen principal
                SetImageViewerContent(_catalogDoc);
                //Setea el classname del icono de la entidad.
                if (_IsDashBoardMonitoring)
                {   //Como viene del DashboardMonitoring, le sacamos la palabra clave RED y le dejamos BLUE
                    pnlIcon.CssClass = _classNameIcon.Replace("Red", "Blue");
                }
                else
                {   //Sete el mismo que devuelve el metodo
                    pnlIcon.CssClass = _classNameIcon;
                }
                lblTitle.Text = Common.Functions.ReplaceIndexesTags(_title);
                lblTitle.CssClass = _classNameTitle;
                lblSubTitle.Text = Common.Functions.ReplaceIndexesTags(_subTitle);

                //Analiza si Es un Process y si tiene que mostrar el Boton Follow
                String _language = Convert.ToString(Session["CurrentLanguage"]).Split('-')[0];
                //El boton para twitear no se si lo dejamos para todos o solo el admin...despues vemos...
                btnTweet.Attributes.Add("data-lang", _language);
                switch (_EntityObject)
                {
                    case Common.ConstantsEntitiesName.PF.ProcessGroupProcess:
                    case Common.ConstantsEntitiesName.PF.Process:
                        ProcessGroupProcess _process = (ProcessGroupProcess)EMSLibrary.User.ProcessFramework.Map.Process(_IdEntity);
                        if (!String.IsNullOrEmpty(_process.TwitterUser))
                        {
                            //Aca deberia ir a buscar al process y traer el usuario de twitter.
                            btnFollow.HRef = "https://twitter.com/" + _process.TwitterUser;
                            btnFollow.Attributes.Add("data-lang", _language);
                        }
                        else
                        {
                            btnFollow.Visible = false;
                            btnTweet.Visible = false;
                        }

                        if (!String.IsNullOrEmpty(_process.FacebookUser))
                        {
                            btnFBLike.Attributes.Add("data-href", _process.FacebookUser);
                        }
                        else
                        {
                            btnFBLike.Visible = false;
                        }
                        break;

                    default:
                        btnFollow.Visible = false;
                        btnFBLike.Visible = false;
                        btnTweet.Visible = false;
                        break;
                }
            }
            private void SetImageViewerContent(Dictionary<Int64, CatalogDoc> catalogDoc)
            {
                Int64 _idResource = -1;
                Int64 _idResourceFile = -1;
                try
                {

                    if (catalogDoc.Count == 0)
                    {
                        imgMainPhoto.ImageUrl = "~/Skins/Images/NoImagesAvailable.gif";
                        hdn_ImagePosition.Value = "0";
                        return;
                    }

                    _idResource = catalogDoc.First().Value.IdResource;
                    _idResourceFile = catalogDoc.First().Value.IdResourceFile;
                }
                catch (IndexOutOfRangeException ex)
                {
                    _idResource = catalogDoc.ElementAt(catalogDoc.Count - 1).Value.IdResource;
                    _idResourceFile = catalogDoc.ElementAt(catalogDoc.Count - 1).Value.IdResourceFile;
                    hdn_ImagePosition.Value = (catalogDoc.Count - 1).ToString();
                }

                imgMainPhoto.ImageUrl = "~/ManagementTools/KnowledgeCollaboration/FilesViewer.aspx?IdResource=" + _idResource.ToString() + "&IdResourceFile=" + _idResourceFile.ToString();
            }
            private void LoadGraphicContent()
            {
                //Se muestra el Chart o las fotos???
                if (_TabChartVisible)
                    { BuildGraphicContent(_EntityObject, _IdEntity, _IdProcess, ref chartBarActivityByScope1AndFacility, "GEI"); }
                else
                    { pnlGraphic.Controls.Add(BuildGraphicContent(_EntityObject, _IdEntity)); }
            }
            private void LoadChartLocalPollutantContent()
            {
                BuildGraphicContent(_EntityObject, _IdEntity, _IdProcess, ref chartBarActivityByScope1AndFacilityLC, "LC");
            }
            private void LoadChartGEIContent()
            {
                BuildGraphicContent(_EntityObject, _IdEntity, _IdProcess, ref chartBarActivityByScope1AndFacility, "GEI");
            }
        #endregion

    }
}
