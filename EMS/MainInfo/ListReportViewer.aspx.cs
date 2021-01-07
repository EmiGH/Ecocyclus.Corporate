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
using System.Drawing;
using Dundas.Charting.WebControl;
using Condesus.EMS.WebUI.MasterControls;
using Condesus.EMS.Business.PA.Entities;
using Condesus.EMS.Business.PF.Entities;
using Condesus.EMS.Business.KC.Entities;
using Condesus.WebUI.Navigation;
using Condesus.EMS.Business.GIS.Entities;
using Condesus.EMS.Business.DS.Entities;

namespace Condesus.EMS.WebUI.MainInfo
{
    public partial class ListReportViewer : BasePage
    {
        #region Internal Properties
            private RadGrid _rgdMasterGridListViewerMainData;
            private Dictionary<String, RadGrid> _RgdMasterGridListManageRelated = new Dictionary<String, RadGrid>();
            private String _EntityNameGridRelatedOn = String.Empty;
            private String _EntityNameRelatedOn = String.Empty;

            private String _EntityNameGRC = String.Empty;
            private String _EntityName = String.Empty;
            private String _EntityNameContextElement = String.Empty;
            private String _PageTitleLocal = String.Empty;
            private Dictionary<Int64, CatalogDoc> _CatalogDoc
            {
                get 
                {
                    return GetPicturesByObject(_EntityName + "Pictures", ManageEntityParams);
                }
            }
            private Dundas.Charting.WebControl.Series _ChartSeries
            {
                get { return chtMeasurement.Series["Data"]; }
            }
            public String _PointsLatLong = String.Empty;
            public String _MapType = "googlemap";
            private const Int16 _TAB_PHOTO = 0;
            private const Int16 _TAB_MAP = 1;
            private const Int16 _TAB_CHART = 2;

            private RadTabStrip _RtsTabStrip = new RadTabStrip();
            private RadMultiPage _RmpMultiPage = new RadMultiPage();
        #endregion

        #region PageLoad & Init
            protected override void OnPreInit(EventArgs e)
            {
                base.OnPreInit(e);
                
                InjectGoogleMapRegisterKey();
                //Registro Mis Custom MenuPanels
                RegisterCustomMenuPanels();
            }
            protected override void OnInit(EventArgs e)
            {
                base.OnInit(e);

                //Inicializa la variable de parametros.
                ManageEntityParams = new Dictionary<String, Object>();
                //Debe recorrer las PK para saber si es un Manage de Lenguajes.
                String _pkValues = NavigatorGetPkEntityIdTransferVar<String>("PkCompost");
                //Ahora genera el Dictionary con los parametros y se lo pasa a la ManageEntityParams para que contruya la grilla como corresponde.
                ManageEntityParams = GetKeyValues(_pkValues);

                ////Se guarda todos los parametros que recibe... si es que no vienen por PK
                foreach (KeyValuePair<String, Object> _item in NavigatorTransferenceCollection)
                {
                    if (_item.Key != "EntityName")
                    {
                        if (!ManageEntityParams.ContainsKey(_item.Key))
                        {
                            ManageEntityParams.Add(_item.Key, _item.Value);
                        }
                    }
                }

                //Setea el nombre de la entidad que se va a mostrar
                _EntityName = base.NavigatorGetTransferVar<String>("EntityName");
                EntityNameGrid = base.NavigatorGetTransferVar<String>("EntityName");
                if (base.NavigatorContainsTransferVar("EntityNameContextInfo"))
                {
                    _EntityNameGRC = base.NavigatorGetTransferVar<String>("EntityNameContextInfo");
                }
                if (base.NavigatorContainsTransferVar("EntityNameContextElement"))
                {
                    //switch (_EntityName)
                    //{
                    //    case Common.ConstantsEntitiesName.PA.Measurement:
                    //    case Common.ConstantsEntitiesName.PA.KeyIndicator:
                    //    case Common.ConstantsEntitiesName.PA.Transformation:
                    //    case Common.ConstantsEntitiesName.PA.TransformationByTransformation:
                    //    case Common.ConstantsEntitiesName.PF.ProcessTaskMeasurement:
                    //        _EntityNameContextElement = String.Empty;
                    //        break;

                    //    default:
                    _EntityNameContextElement = base.NavigatorGetTransferVar<String>("EntityNameContextElement");
                    //        break;
                    //}
                }
                //Para el caso del Remove, debe ser el nombre de la entidad mas la palabra Remove!!!
                EntityNameToRemove = _EntityName + "Remove";

                //Setea los datos en el DataTable de base, para que luego se carge la grilla.
                BuildGenericDataTable(_EntityName, ManageEntityParams);
                //Arma la grilla completa
                LoadListViewerMainData();

                //LoadListViewerRelatedData();
                //Construye los Tabs para la informacion Relacionada de la entidad.
                LoadTabsForRelatedData();

                //Carga el GRC en caso de que lo hayan pasado por parametros.
                LoadGRCByEntity();
                //Carga el Menu de Context ElementMaps a la derecha.
                LoadContextElementMapsByEntity();
                //Carga el menu de opciones generales.
                LoadGeneralOptionMenu();
                //Carga el menu de Seguridad...
                LoadSecurityOptionMenu();
                //Inserta todos los manejadores de eventos que necesita la apgina
                InitializeHandlers();
            }
            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);

                rtsGraphicModes.Tabs[0].ToolTip = Resources.Common.Photo;
                rtsGraphicModes.Tabs[1].ToolTip = Resources.Common.Map;
                rtsGraphicModes.Tabs[2].ToolTip = Resources.Common.Chart;
                lblSubTitle.Text = Resources.Common.RelatedData;
                lblTitleMainData.Text = Resources.Common.MainData;

                //La primera vez que carga, lo valida esto, despues ya no.
                if (!IsPostBack)
                {
                    ValidateShowChart();
                    ValidateShowPhoto();
                }
                ValidateTabGeneral();

                if (rtsGraphicModes.SelectedTab != null)
                {
                    if (_EntityName == Common.ConstantsEntitiesName.DS.GeographicArea)
                    {
                        rtsGraphicModes.Tabs[1].Selected = true;
                    }
                    LoadDataByTabSelected(rtsGraphicModes.SelectedTab.Index);
                }
                //Carga la info en el tab de RelatedData seleccionado.(Por defecto la primera vez viene el 1° tab seleccionado)
                if (_RtsTabStrip.SelectedTab != null)
                {
                    _EntityNameGridRelatedOn = _RtsTabStrip.SelectedTab.Attributes["EntityNameGrid"];
                    _EntityNameRelatedOn = _RtsTabStrip.SelectedTab.Attributes["EntityName"];
                    LoadRelatedData(_EntityNameGridRelatedOn);
                }
                
            }
            protected override void SetPagetitle()
            {
                //Busca el nombre de la pagina si es que se lo pasaron. Sino usa el nombre de la entidad.
                _PageTitleLocal = base.NavigatorGetTransferVar<String>("PageTitle");
                if (String.IsNullOrEmpty(_PageTitleLocal))
                {
                    //el metodo GetPageTitleForViewer, obtiene el nombre del dato que se esta mostrando...
                    String _titleEntityName = GetGlobalResourceObject("CommonListManage", _EntityName) != null ? GetGlobalResourceObject("CommonListManage", _EntityName).ToString() : String.Empty;
                    _PageTitleLocal = _titleEntityName + ": " + GetPageTitleForViewer();
                    base.PageTitle = _PageTitleLocal;
                }
                else
                {
                    base.PageTitle = _PageTitleLocal;
                }
            }
            protected override void SetPageTileSubTitle()
            {
                try
                {
                    String _pageSubTitle = base.NavigatorGetTransferVar<String>("PageSubTitle");
                    if (String.IsNullOrEmpty(_pageSubTitle))
                    {
                        base.PageTitleSubTitle = Resources.Common.PageSubtitleView;
                    }
                    else
                    {
                        base.PageTitleSubTitle = _pageSubTitle;
                    }
                }
                catch { base.PageTitleSubTitle = String.Empty; }
            }
        #endregion

        #region Private Methods
            private void ValidateShowChart()
            {
                switch (_EntityName)
                {
                        //El process no tiene chart aca
                    //case Common.ConstantsEntitiesName.PF.ProcessGroupProcess:
                    //    rtsGraphicModes.Tabs[2].Enabled = true;
                    //    rtsGraphicModes.Tabs[2].Style.Add("display", "block");
                    //    break;

                    case Common.ConstantsEntitiesName.PA.Measurement:
                    case Common.ConstantsEntitiesName.PA.KeyIndicator:
                    case Common.ConstantsEntitiesName.PF.ProcessTaskMeasurement:
                    case Common.ConstantsEntitiesName.PA.Transformation:
                    case Common.ConstantsEntitiesName.PA.TransformationByTransformation:
                        rtsGraphicModes.Tabs[2].Selected = true;
                        rtsGraphicModes.Tabs[2].Style.Add("display", "block");
                        rpvChart.Selected = true;
                        rmpGraphicModes.SelectedIndex = 2;
                        //al ser una medicion, no muestra fotos, y selecciona el mapa.
                        rpvPictures.Selected = false;
                        ////Selecciona el mapa
                        //rtsGraphicModes.Tabs[1].Selected = true;
                        //rpvMap.Selected = true;
                        break;

                    default:
                        //Si la entidad no tiene chart, no habilita el TAB.
                        rtsGraphicModes.Tabs[2].Enabled = false;
                        rtsGraphicModes.Tabs[2].Style.Add("display", "none");
                        break;

                }
            }
            private void ValidateShowPhoto()
            {
                switch (_EntityName)
                {
                    //Si la entidad tiene Fotos, habilita el tab
                    case Common.ConstantsEntitiesName.PF.Process:
                    case Common.ConstantsEntitiesName.PF.ProcessGroupProcess:
                    case Common.ConstantsEntitiesName.DS.Organization:
                    case Common.ConstantsEntitiesName.DS.Person:
                    case Common.ConstantsEntitiesName.DS.Facility:
                    case Common.ConstantsEntitiesName.DS.Sector:
                    case Common.ConstantsEntitiesName.PA.MeasurementDevice:
                        rtsGraphicModes.Tabs[0].Enabled = true;
                        rtsGraphicModes.Tabs[0].Style.Add("display", "block");
                        rtsGraphicModes.Tabs[0].SelectedIndex = 0;
                        rmpGraphicModes.SelectedIndex = 0;
                        rpvPictures.Selected = true;
                        break;

                    default:
                        //Si la entidad no tiene Foto, no habilita el TAB.
                        rtsGraphicModes.Tabs[0].Enabled = false;
                        rtsGraphicModes.Tabs[0].Style.Add("display", "none");
                        break;

                }
            }
            private void ValidateTabGeneral()
            {
                switch (_EntityName)
                {
                    //Si la entidad tiene Fotos, habilita el tab
                    case Common.ConstantsEntitiesName.PF.Process:
                    case Common.ConstantsEntitiesName.PF.ProcessGroupProcess:
                    case Common.ConstantsEntitiesName.DS.Organization:
                    case Common.ConstantsEntitiesName.DS.Person:
                    case Common.ConstantsEntitiesName.DS.Facility:
                    case Common.ConstantsEntitiesName.DS.Sector:
                    case Common.ConstantsEntitiesName.PA.MeasurementDevice:
                        rtsGraphicModes.Tabs[0].Enabled = true;
                        rtsGraphicModes.Tabs[0].Style.Add("display", "block");
                        rtsGraphicModes.Tabs[0].SelectedIndex = 0;
                        rmpGraphicModes.SelectedIndex = 0;
                        rpvPictures.Selected = true;
                        break;

                    default:
                        //Si la entidad no tiene Foto, no habilita el TAB.
                        rtsGraphicModes.Tabs[0].Enabled = false;
                        rtsGraphicModes.Tabs[0].Style.Add("display", "none");
                        break;

                }
            }
            #region GIS Methods
                private void LoadGeographicData()
                {
                    _MapType = ConfigurationManager.AppSettings["MAP_Type_Vendor"];

                    String _className = String.Empty;
                    String _idObject = String.Empty;

                    String _strGeoData = GetGeographicData();

                    //Si realmente hay coordenadas guardadas se mete
                    if (!String.IsNullOrEmpty(_strGeoData))
                    {
                        //Si no hay datos geograficos, no habilita el TAB.
                        rtsGraphicModes.Tabs[1].Enabled = true;
                        rtsGraphicModes.Tabs[1].Style.Add("display", "block");
                        if (rtsGraphicModes.Tabs[0].Enabled == false)
                        {
                            rtsGraphicModes.SelectedIndex = 1;
                            rpvMap.Selected = true;
                            rmpGraphicModes.SelectedIndex = 1;
                        }

                        _PointsLatLong += _strGeoData;
                    }
                    else
                    {
                        //Si no hay datos geograficos, no habilita el TAB.
                        rtsGraphicModes.Tabs[1].Enabled = false;
                        rtsGraphicModes.Tabs[1].Style.Add("display", "none");
                        rpvMap.Selected = false;

                        switch (_EntityName)
                        {
                            case Common.ConstantsEntitiesName.PA.Measurement:
                            case Common.ConstantsEntitiesName.PA.KeyIndicator:
                            case Common.ConstantsEntitiesName.PF.ProcessTaskMeasurement:
                            case Common.ConstantsEntitiesName.PA.Transformation:
                            case Common.ConstantsEntitiesName.PA.TransformationByTransformation:
                                rpvChart.Selected = true;
                                rtsGraphicModes.SelectedIndex = 2;
                                rmpGraphicModes.SelectedIndex = 2;
                                break;
                        }

                    }
                }
                private String GetGeographicData()
                {
                    String _strGeoData = String.Empty;
                    String _className = String.Empty;
                    String _idObject = String.Empty;
                    Site _site = null;

                    switch (_EntityName)
                    {
                        case Common.ConstantsEntitiesName.PF.ProcessGroupProcess:
                            //_gisObject = EMSLibrary.GisReadByID("ProcessGroupProcess", Convert.ToInt64(ManageEntityParams["IdProcess"]));
                            _className = Common.ConstantsEntitiesName.PF.ProcessGroupProcess;
                            _idObject = Convert.ToString(ManageEntityParams["IdProcess"]);
                            ProcessGroupProcess _processGroupProcess = EMSLibrary.User.ProcessFramework.Map.ProcessGroupProcess(Convert.ToInt64(_idObject));
                            //Los process pueden tener Facility o GeographicArea o ambos!...
                            //_site = _processGroupProcess.Facility;
                            //if ((_site != null) && (!String.IsNullOrEmpty(_site.Coordinate)))

                            //Solo nos importa mostrar el punto Geografico sin mirar Area Geografica.
                            if (!String.IsNullOrEmpty(_processGroupProcess.Coordinate))
                            {
                                _strGeoData += _className + "|" + _idObject + "|" + _processGroupProcess.Coordinate.Replace(";", "|").Substring(0, _processGroupProcess.Coordinate.Length - 1) + ";";
                            }
                            //GeographicArea _geographicArea = _processGroupProcess.GeographicArea;
                            //if ((_geographicArea != null) && (!String.IsNullOrEmpty(_geographicArea.Coordinate)))
                            //{
                            //    _strGeoData += _className + "|" + _idObject + "|" + _geographicArea.Coordinate.Replace(";", "|").Substring(0, _geographicArea.Coordinate.Length - 1) + ";";
                            //}
                            break;
                        case Common.ConstantsEntitiesName.DS.Organization:
                            _className = Common.ConstantsEntitiesName.DS.Organization;
                            _idObject = Convert.ToString(ManageEntityParams["IdOrganization"]);
                            Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(Convert.ToInt64(ManageEntityParams["IdOrganization"]));
                            foreach (Facility _facility in _organization.Facilities.Values)
                            {
                                if (!String.IsNullOrEmpty(_facility.Coordinate))
                                {
                                    _strGeoData += _className + "|" + _idObject + "|" + _facility.Coordinate.Replace(";", "|").Substring(0, _facility.Coordinate.Length - 1) + ";";
                                }
                            }
                            break;
                        case Common.ConstantsEntitiesName.DS.Facility:
                            _className = Common.ConstantsEntitiesName.DS.Facility;
                            _idObject = Convert.ToString(ManageEntityParams["IdFacility"]);
                            _site = EMSLibrary.User.GeographicInformationSystem.Site(Convert.ToInt64(ManageEntityParams["IdFacility"]));
                            if ((_site != null) && (!String.IsNullOrEmpty(_site.Coordinate)))
                            {
                                _strGeoData += _className + "|" + _idObject + "|" + _site.Coordinate.Replace(";", "|").Substring(0, _site.Coordinate.Length - 1) + ";";
                            }
                            break;
                        case Common.ConstantsEntitiesName.DS.Sector:
                            _className = Common.ConstantsEntitiesName.DS.Facility;
                            _idObject = Convert.ToString(ManageEntityParams["IdSector"]);
                            _site = EMSLibrary.User.GeographicInformationSystem.Site(Convert.ToInt64(ManageEntityParams["IdSector"]));
                            if ((_site != null) && (!String.IsNullOrEmpty(_site.Coordinate)))
                            {
                                _strGeoData += _className + "|" + _idObject + "|" + _site.Coordinate.Replace(";", "|").Substring(0, _site.Coordinate.Length - 1) + ";";
                            }
                            break;
                        case Common.ConstantsEntitiesName.PA.KeyIndicator:
                        case Common.ConstantsEntitiesName.PA.Measurement:
                        case Common.ConstantsEntitiesName.PF.ProcessTaskMeasurement:
                        case Common.ConstantsEntitiesName.PA.Transformation:
                        case Common.ConstantsEntitiesName.PA.TransformationByTransformation:
                            //Si se visualiza una medicion, muestro en el mapa el facility al que pertenece la tarea.
                            _site = EMSLibrary.User.PerformanceAssessments.Configuration.Measurement(Convert.ToInt64(ManageEntityParams["IdMeasurement"])).ProcessTask.Site;
                            _className = Common.ConstantsEntitiesName.DS.Facility;
                            if ((_site != null) && (!String.IsNullOrEmpty(_site.Coordinate)))
                            {
                                _idObject = _site.IdFacility.ToString();
                                _strGeoData += _className + "|" + _idObject + "|" + _site.Coordinate.Replace(";", "|").Substring(0, _site.Coordinate.Length - 1) + ";";
                            }

                            break;
                            
                        case Common.ConstantsEntitiesName.DS.GeographicArea:
                            _className = Common.ConstantsEntitiesName.DS.GeographicArea;
                            _idObject = Convert.ToString(ManageEntityParams["IdGeographicArea"]);
                            GeographicArea _geoArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(Convert.ToInt64(ManageEntityParams["IdGeographicArea"]));
                            if (!String.IsNullOrEmpty(_geoArea.Coordinate))
                            {
                                _strGeoData += _className + "|" + _idObject + "|" + _geoArea.Coordinate.Replace(";", "|").Substring(0, _geoArea.Coordinate.Length - 1) + ";";
                            }
                            rpvMap.Selected = true;
                            break;
                    }

                    return _strGeoData;
                }
            #endregion

            private void LoadDataByTabSelected(int indexTabSelected)
            {
                LoadGeographicData();

                switch (indexTabSelected)
                {
                    //El Tab del mapa se carga en JS en el OnLoad de la pagina.
                    case _TAB_MAP:
                    //    LoadGeographicData();
                        rtsGraphicModes.Tabs[_TAB_MAP].Selected = true;
                        rtsGraphicModes.Tabs[_TAB_MAP].Style.Add("display", "block");
                        rpvMap.Selected = true;
                        rmpGraphicModes.SelectedIndex = 1;
                        break;
                    case _TAB_PHOTO:
                        //Init Image Viewer
                        SetImageViewerContent(0);
                        SetPagerStatus(0);

                        rtsGraphicModes.Tabs[_TAB_PHOTO].Selected = true;
                        rtsGraphicModes.Tabs[_TAB_PHOTO].Style.Add("display", "block");
                        rpvPictures.Selected = true;
                        rmpGraphicModes.SelectedIndex = 0;

                        break;
                    case _TAB_CHART:
                        LoadChart();
                        
                        rtsGraphicModes.Tabs[2].Selected = true;
                        rtsGraphicModes.Tabs[2].Style.Add("display", "block");
                        rpvChart.Selected = true;
                        rmpGraphicModes.SelectedIndex = 2;
                        break;
                }
            }
            private void LoadMeasurementChart()
            {
                try
                {

                    chtMeasurement.Visible = true;
                    List<Condesus.EMS.Business.PA.Entities.MeasurementPoint> series = null;
                    if ((ManageEntityParams.ContainsKey("IdTransformation")) && (Convert.ToInt64(ManageEntityParams["IdTransformation"])>0))
                    {
                        CalculateOfTransformation _calculateOfTransformation = EMSLibrary.User.PerformanceAssessments.Configuration.Measurement(Convert.ToInt64(ManageEntityParams["IdMeasurement"])).Transformation(Convert.ToInt64(ManageEntityParams["IdTransformation"]));
                        series = _calculateOfTransformation.Series();
                    }
                    else
                    {
                        Measurement _measurement = EMSLibrary.User.PerformanceAssessments.Configuration.Measurement(Convert.ToInt64(ManageEntityParams["IdMeasurement"]));
                        series = _measurement.Series();
                    }
                    _ChartSeries.Color = Color.Blue;
                    _ChartSeries.Type = SeriesChartType.Line;

                    if (series.Count > 0)
                    {
                        _ChartSeries.Points.Clear();
                        Int32 _interval = (Int32)(series.Count / 22);
                        _interval = (_interval == 0) ? 1 : _interval;
                        chtMeasurement.ChartAreas["AreaData"].AxisX.MajorTickMark.Interval = _interval;
                        chtMeasurement.ChartAreas["AreaData"].AxisX.MajorGrid.Interval = _interval;
                        chtMeasurement.ChartAreas["AreaData"].AxisX.LabelStyle.Interval = _interval;
                        chtMeasurement.ChartAreas["AreaData"].AxisX.LabelsAutoFit = true;

                        //Limpia los puntos existentes
                        _ChartSeries.Points.Clear();

                        foreach (Condesus.EMS.Business.PA.Entities.MeasurementPoint _point in series)
                        {
                            _ChartSeries.Points.AddXY(_point.MeasureDate.ToString(), Convert.ToDouble(_point.MeasureValue));
                        }
                    }
                    //else
                    //{
                    //    //No hay datos para el chart, no habilita el TAB.
                    //    rtsGraphicModes.Tabs[2].Enabled = false;
                    //    rtsGraphicModes.Tabs[2].Style.Add("display", "none");
                    //}
                }
                catch (Exception ex)
                {
                    base.StatusBar.ShowMessage(ex);
                }
            }
            //private void LoadPerformanceChart()
            //{
            //    try
            //    {
            //        chtProcessTotals.Visible = true;
            //        Dictionary<String, KeyValuePair<String, Decimal>> _performanceSeries = new Dictionary<String, KeyValuePair<String, Decimal>>();
            //        CalculationScenarioType _scenarioType = EMSLibrary.User.PerformanceAssessments.Configuration.CalculationScenarioType(Convert.ToInt64(ConfigurationManager.AppSettings["PDDEstimated"]));
            //        ProcessGroupProcess _processRoot = EMSLibrary.User.ProcessFramework.Map.ProcessGroupProcess(Convert.ToInt64(ManageEntityParams["IdProcess"]));
            //        //1° Hay que obtener todas las fechas de certificacion que hay.
            //        Dictionary<Int64, DateTime[]> _issuedPeriods = _processRoot.GetIssuedPeriods();
            //        if (_issuedPeriods.Count > 0)
            //        {
            //            chtProcessTotals.Series["Issued"].Type = SeriesChartType.Column;
            //            chtProcessTotals.Series["PDD"].Type = SeriesChartType.Column;
            //            chtProcessTotals.Series["Calculated"].Type = SeriesChartType.Column;
            //            Decimal _totalIssued = 0;
            //            Decimal _totalPDD = 0;
            //            Decimal _totalCalculated = 0;

            //            //Limpia los puntos existentes
            //            chtProcessTotals.Series["Issued"].Points.Clear();
            //            chtProcessTotals.Series["PDD"].Points.Clear();
            //            chtProcessTotals.Series["Calculated"].Points.Clear();

            //            //2° Por cada fecha recorro y me quedo con el periodo y el valor.
            //            foreach (KeyValuePair<Int64, DateTime[]> _period in _issuedPeriods)
            //            {
            //                //3° Con esto obtengo el inicio-fin
            //                DateTime _startDate = _period.Value[0];   //Inicial
            //                DateTime _endDate = _period.Value[1];     //final

            //                String _xValue = _startDate.ToShortDateString() + " - " + _endDate.ToShortDateString();
            //                Decimal _yValueIssued = _processRoot.AssociatedCalculations.First().Value.CalculationCertificated(_period.Key).Value;
            //                Decimal _yValuePDD = _processRoot.AssociatedCalculations.First().Value.CalculationCertificated(_period.Key).CertificationDeviation(_scenarioType);
            //                Decimal _yValueCalculated = _processRoot.AssociatedCalculations.First().Value.Calculate(_startDate, _endDate);

            //                chtProcessTotals.Series["Issued"].Points.AddXY(_xValue, _yValueIssued);
            //                chtProcessTotals.Series["PDD"].Points.AddXY(_xValue, _yValuePDD);
            //                chtProcessTotals.Series["Calculated"].Points.AddXY(_xValue, _yValueCalculated);

            //                _totalIssued = _totalIssued + _yValueIssued;
            //                _totalPDD = _totalPDD + _yValuePDD;
            //                _totalCalculated = _totalCalculated + _yValueCalculated;
            //            }
            //            chtProcessTotals.Series["Issued"].Points.AddXY("Total", _totalIssued);
            //            chtProcessTotals.Series["PDD"].Points.AddXY("Total", _totalPDD);
            //            chtProcessTotals.Series["Calculated"].Points.AddXY("Total", _totalCalculated);
            //        }
            //        //else
            //        //{
            //        //    //No hay datos para el chart, no habilita el TAB.
            //        //    rtsGraphicModes.Tabs[2].Enabled = false;
            //        //    rtsGraphicModes.Tabs[2].Style.Add("display", "none");
            //        //}
            //    }
            //    catch (Exception ex)
            //    {
            //        base.StatusBar.ShowMessage(ex);
            //    }

            //}
            private void LoadChart()
            {
                switch (_EntityName)
                {
                    //case Common.ConstantsEntitiesName.PF.ProcessGroupProcess:
                    //    LoadPerformanceChart();
                    //    break;

                    case Common.ConstantsEntitiesName.PA.KeyIndicator:
                    case Common.ConstantsEntitiesName.PA.Measurement:
                    case Common.ConstantsEntitiesName.PF.ProcessTaskMeasurement:
                    case Common.ConstantsEntitiesName.PA.Transformation:
                    case Common.ConstantsEntitiesName.PA.TransformationByTransformation:
                        LoadMeasurementChart();
                        break;

                    default:
                        //Si la entidad no tiene chart, no habilita el TAB.
                        rtsGraphicModes.Tabs[2].Enabled = false;
                        rtsGraphicModes.Tabs[2].Style.Add("display", "none");
                        break;

                }
            }
            //private void LoadMap()
            //{
            //    String _strGisData = GetGeographicData();

            //    if (!String.IsNullOrEmpty(_strGisData))
            //    {
            //        String[] _separatorRow = new String[] { ";" };
            //        String[] _points = _strGisData.Split(_separatorRow, StringSplitOptions.RemoveEmptyEntries);
            //        msveMap.ZoomLevel = 12;
            //        msveMap.MapStyle = Microsoft.Live.ServerControls.VE.MapStyle.Road;
            //        msveMap.MapMode = Microsoft.Live.ServerControls.VE.MapMode.Mode2D;//.Mode3D;
            //        Microsoft.Live.ServerControls.VE.Shape _shape = new Microsoft.Live.ServerControls.VE.Shape();
            //        Microsoft.Live.ServerControls.VE.LatLongWithAltitude _latlongitem = null;

            //        Double _latitud = 0;
            //        Double _longitud = 0;

            //        System.Globalization.CultureInfo _cultureInfo = System.Globalization.CultureInfo.CurrentCulture;
            //        String _decimalSeparator = _cultureInfo.NumberFormat.NumberDecimalSeparator;

            //        for (int i = 0; i < _points.Length; i++)
            //        {
            //            _latitud = Convert.ToDouble(_points[i].Split('|')[0]);
            //            _longitud = Convert.ToDouble(_points[i].Split('|')[1]);

            //            _latlongitem = new Microsoft.Live.ServerControls.VE.LatLongWithAltitude();
            //            _latlongitem.Latitude = Convert.ToDouble(_latitud.ToString().Replace(".", _decimalSeparator));
            //            _latlongitem.Longitude = Convert.ToDouble(_longitud.ToString().Replace(".", _decimalSeparator));
            //            _shape.Points.Add(_latlongitem);
            //        }
            //        Microsoft.Live.ServerControls.VE.Color _shpcolor = new Microsoft.Live.ServerControls.VE.Color(255, 0, 0, 0);

            //        if (_points.Length > 1)
            //        { _shape.Type = Microsoft.Live.ServerControls.VE.ShapeType.Polygon; }
            //        else
            //        { _shape.Type = Microsoft.Live.ServerControls.VE.ShapeType.Pushpin; }

            //        _shape.FillColor = _shpcolor;
            //        _shape.Points.Add(_latlongitem);
            //        msveMap.AddShape(_shape);

            //        msveMap.Center.Latitude = _latitud;
            //        msveMap.Center.Longitude = _longitud;
            //    }
            //    else
            //    {
            //        //Si no hay datos geograficos, no habilita el TAB.
            //        rtsGraphicModes.Tabs[1].Enabled = false;
            //        msveMap.Enabled = false;
            //    }


            //}
            private void InitializeHandlers()
            {
                btnPrevPicture.Click += new ImageClickEventHandler(PagerPicture_Click);
                btnNextPicture.Click += new ImageClickEventHandler(PagerPicture_Click);
                btnOkDelete.Click += new EventHandler(btnOkDelete_Click);
                GridLinkButtonClick = new EventHandler(GridLinkButtonClick_Click);
                _rgdMasterGridListViewerMainData.ItemDataBound += new GridItemEventHandler(rgdMasterGridListViewer_ItemDataBound);

                rtsGraphicModes.OnClientTabSelecting = "ShowLoading";
                rtsGraphicModes.AutoPostBack = true;

                InjectShowReportToPrintProjectBuyerSummary();
            }
            private void SetImageViewerContent(Int32 index)
            {
                Int64 _idResource = -1;
                Int64 _idResourceFile = -1;
                try
                {
                    
                    //if (_ProcessRoot.Pictures.Count == 0)
                    if (_CatalogDoc.Count == 0)
                    {
                        imgShowSlide.ImageUrl = "~/Skins/Images/NoImagesAvailable.gif";
                        hdn_ImagePosition.Value = "0";
                        return;
                    }

                    _idResource = _CatalogDoc.ElementAt(index).Value.IdResource;
                    _idResourceFile = _CatalogDoc.ElementAt(index).Value.IdResourceFile;
                    //_idResource = _ProcessRoot.Pictures.ElementAt(index).Value.IdResource;
                    //_idResourceFile = _ProcessRoot.Pictures.ElementAt(index).Value.IdResourceFile;
                }
                catch (IndexOutOfRangeException ex)
                {
                    _idResource = _CatalogDoc.ElementAt(_CatalogDoc.Count - 1).Value.IdResource;
                    _idResourceFile = _CatalogDoc.ElementAt(_CatalogDoc.Count - 1).Value.IdResourceFile;
                    hdn_ImagePosition.Value = (_CatalogDoc.Count - 1).ToString();

                    //_idResource = _ProcessRoot.Pictures.ElementAt(_ProcessRoot.Pictures.Count - 1).Value.IdResource;
                    //_idResourceFile = _ProcessRoot.Pictures.ElementAt(_ProcessRoot.Pictures.Count - 1).Value.IdResourceFile;
                    //hdn_ImagePosition.Value = (_ProcessRoot.Pictures.Count - 1).ToString();
                }

                imgShowSlide.ImageUrl = "~/ManagementTools/KnowledgeCollaboration/FilesViewer.aspx?IdResource=" + _idResource.ToString() + "&IdResourceFile=" + _idResourceFile.ToString();
            }
            private void SetPagerStatus(Int32 index)
            {
                btnPrevPicture.Enabled = true;
                btnNextPicture.Enabled = true;

                if (index == 0)
                    btnPrevPicture.Enabled = false;
                if (index == _CatalogDoc.Count - 1 || index > _CatalogDoc.Count - 1)
                    btnNextPicture.Enabled = false;

                //Si no hay ninguna foto en la coleccion
                if (_CatalogDoc.Count == 0)
                    btnPrevPicture.Enabled = btnNextPicture.Enabled = false;

                btnPrevPicture.ImageUrl = "~/Skins/Images/Trans.gif";
                btnNextPicture.ImageUrl = "~/Skins/Images/Trans.gif";
            }
            private void LoadListViewerMainData()
            {
                _rgdMasterGridListViewerMainData = base.BuildListViewerContent(_EntityName);
                pnlListViewerMainData.Controls.Add(_rgdMasterGridListViewerMainData);

                base.InjectClientSelectRow(_rgdMasterGridListViewerMainData.ClientID);
            }
            //private void LoadListViewerRelatedData()
            //{
            //    Boolean _showImgSelect = false;
            //    Boolean _showCheck = false;
            //    Boolean _allowSearchableGrid = false;
            //    Boolean _showOpenFile = false;
            //    Boolean _showOpenChart = false;
            //    Boolean _showOpenSeries = false;
            //    String[,] _relatedDataEntityNames = GetEntityRelated(_EntityName, ManageEntityParams);

            //    base.InjectRowContextMenu(rmnSelection.ClientID);

            //    for (int i = 0; i < _relatedDataEntityNames.GetLength(0); i++)
            //    {
            //        String _entityGrid = _relatedDataEntityNames[i, 0];

            //        BuildGenericDataTable(_entityGrid, ManageEntityParams);
            //        //Si se tiene que mostrar un file, entonces agrega la columna de openfile.
            //        if ((_entityGrid.Contains(Common.ConstantsEntitiesName.KC.ResourceVersion)) || (_entityGrid.Contains(Common.ConstantsEntitiesName.PF.ProcessResource)))
            //            { _showOpenFile = true; }
            //        //Si se esta visualizando un key indicator, entonces agrega las columnas showchart y showseries
            //        if (_entityGrid.Contains(Common.ConstantsEntitiesName.PA.KeyIndicator))
            //        {
            //            _showOpenChart = true;
            //            _showOpenSeries = true;
            //        }
            //        RadGrid _RgdMasterGridRelated = base.BuildListManageContent(_entityGrid, _showImgSelect, _showCheck, _showOpenFile, _showOpenChart, _showOpenSeries, _allowSearchableGrid);
            //        base.PersistControlState(_RgdMasterGridRelated);
            //        _RgdMasterGridRelated.ItemCreated += new GridItemEventHandler(rgdMasterGrid_ItemCreated);
            //        _RgdMasterGridRelated.ClientSettings.ClientEvents.OnRowContextMenu = "RowContextMenu";
            //        base.InjectClientSelectRow(_RgdMasterGridRelated.ClientID);
            //        base.InjectRmnSelectionItemClickHandler(_entityGrid);
            //        //Si tiene el open file, agrego el evento e inyecto el JS.
            //        if (_showOpenFile)
            //        {
            //            _RgdMasterGridRelated.ItemDataBound += new GridItemEventHandler(RgdMasterGridListManage_ItemDataBound);
            //            InjectShowFile(_RgdMasterGridRelated.ClientID);
            //            //vuelve a la normalidad..
            //            _showOpenFile = false;
            //        }
            //        //Si tiene el ShowChart o ShowSeries, agrego el evento e inyecto el JS.
            //        if ((_showOpenChart) || (_showOpenSeries))
            //        {
            //            _RgdMasterGridRelated.ItemDataBound += new GridItemEventHandler(RgdMasterGridListManageKeyIndicator_ItemDataBound);
            //            InjectShowChart();
            //            InjectShowSeries();
            //            //vuelve a la normalidad..
            //            _showOpenChart = false;
            //            _showOpenSeries = false;
            //        }

            //        System.Web.UI.WebControls.Label _lblCaption = new System.Web.UI.WebControls.Label();
            //        _lblCaption.ID = "lblGridTitle" + i.ToString();
            //        _lblCaption.Text = _relatedDataEntityNames[i, 1]; //Accede al titulo.
            //        _lblCaption.CssClass = "Label";

            //        System.Web.UI.WebControls.Label _lblSpace = new System.Web.UI.WebControls.Label();
            //        _lblSpace.ID = "lblGridSpace" + i.ToString();
            //        _lblSpace.Text = "&nbsp;";
            //        _lblSpace.CssClass = "Label";

            //        Panel _pnlContentGrid = new Panel();
            //        _pnlContentGrid.ID = "pnlContentGrid" + i.ToString();
            //        _pnlContentGrid.Controls.Add(_lblCaption);
            //        _pnlContentGrid.Controls.Add(_lblSpace);
            //        _pnlContentGrid.Controls.Add(_RgdMasterGridRelated);
            //        _pnlContentGrid.Controls.Add(_lblSpace);
            //        _pnlContentGrid.Controls.Add(_lblSpace);

            //        pnlListViewerRelatedData.Controls.Add(_pnlContentGrid);
            //    }
            //}
            private void LoadGeneralOptionMenu()
            {
                //Si llega a ser un viewer de Ejecuciones, no debe tener menu de opciones generales.
                if (!_EntityName.Contains("ProcessTaskExecution"))
                {
                    BuildPropertyGeneralOptionsMenu(_EntityName, new RadMenuEventHandler(rmnuGeneralOption_ItemClick), false);
                }
            }
            private void LoadGRCByEntity()
            {
                //TODO: Por ahora lo preguntamos aca...pero despues lo deberiamos hacer en otro lado, me parece!!
                //Cuando es un Add, no debe cargar el GRC!!!
                if (!String.IsNullOrEmpty(_EntityNameGRC))
                {
                    //Dictionary<String, Object> _param = new Dictionary<String, Object>();
                    //_param.Add("IdOrganization", _IdOrganization);
                    //_param.Add("PageTitle", txtCorporateName.Text);
                    ManageEntityParams.Concat(GetKeyValues(NavigatorGetPkEntityIdTransferVar<String>("PkCompost")));
                    if (BuildContextInfoModuleMenu(_EntityNameGRC, ManageEntityParams))
                    {
                        base.BuildContextInfoShowMenuButton();
                    }
                }
            }
            private void LoadContextElementMapsByEntity()
            {
                //Si tiene una entidad, entonces contruye el Context element Maps de la derecha
                if (!String.IsNullOrEmpty(_EntityNameContextElement))
                {
                    ManageEntityParams.Concat(GetKeyValues(NavigatorGetPkEntityIdTransferVar<String>("PkCompost")));
                    if (BuildContextElementMapsModuleMenu(_EntityNameContextElement, ManageEntityParams))
                    {
                        base.BuildContextElementMapsShowMenuButton();
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
            private void LoadSecurityOptionMenu()
            {
                //Menu de Seguridad
                base.BuildPropertySecuritySystemMenu(_EntityName, new RadMenuEventHandler(rmnuSecuritySystem_ItemClick));
            }
            private void BuildTableTabs()
            {
                
                
                
            }
            private void LoadTabsForRelatedData()
            {
                //Obtiene el Array con la entidad y titulo a mostrar...
                String[,] _relatedDataEntityNames = GetEntityRelated(_EntityName, ManageEntityParams);
                String _entityGrid = String.Empty;
                String _entityName = String.Empty;

                //Si hay al menos un dato para mostrar, construye el tab... y sigue
                if (_relatedDataEntityNames.GetLength(0) > 0)
                {
                    //Inyecta el ContextMenu para las grillas
                    //base.InjectRowContextMenu(rmnSelection.ClientID, String.Empty);
                    pnlListViewerRelatedData.Controls.Clear();

                    String _multiPageID = "rmpRelatedData";

                   //Arma el TabStrip

                   
                    _RtsTabStrip = BuildTabStrip(_multiPageID);  // BuildTabStripExtendedInfo();
                    _RtsTabStrip.Align = TabStripAlign.Right;
                    //Mete el evento click en server para que carga en cada Tab la grilla y el evento cliente para que muestre el Loading...
                    _RtsTabStrip.TabClick += new RadTabStripEventHandler(_RtsTabStrip_TabClick);
                    _RtsTabStrip.OnClientTabSelecting = "ShowLoading";

                    //Attach del control como Trigger del UP para que el GoogleMap funcione...!!!!
                    PostBackTrigger _uPanelGraphicModesPostBackTrigger = new PostBackTrigger();
                    _uPanelGraphicModesPostBackTrigger.ControlID = _RtsTabStrip.UniqueID;
                    uPanelGraphicModes.Triggers.Add(_uPanelGraphicModesPostBackTrigger);

                    //Construye el MultiPage
                    _RmpMultiPage = BuildMultiPage(_multiPageID);

                    //Recorre las Entidades relacionadas para la entidad que se esta visualizando y arma cada Tab.
                    for (int i = 0; i < _relatedDataEntityNames.GetLength(0); i++)
                    {
                        _entityGrid = _relatedDataEntityNames[i, 0]; //Accede al nombre de entindad (con este contruye la grilla)
                        _entityName = _relatedDataEntityNames[i, 4];    //Accede al nombre de la entidad singular, para el view o edit.
                        String _cssClassName = _relatedDataEntityNames[i, 1]; //Accede al cssClass.
                        String _cssClassSelectedName = _relatedDataEntityNames[i, 2]; //Accede al cssClassSelected.
                        String _textTooltip = _relatedDataEntityNames[i, 3]; //Accede al titulo. para visualizar como tooltip en el tab

                        //Inyecta el JS para el manejo del menu de seleccion....
                        base.InjectRowContextMenu(rmnSelection.ClientID, String.Empty);
                        base.InjectRmnSelectionItemClickHandler(String.Empty, String.Empty, false);

                        //Construye los tabs a mostrar
                        RadTab _radTab = BuildTab(_textTooltip, _cssClassName, _cssClassSelectedName);
                        //Le agrega como atributo el EntityName para que en el evento click construya la grilla.
                        _radTab.Attributes.Add("EntityNameGrid", _entityGrid);
                        _radTab.Attributes.Add("EntityName", _entityName);

                        //Construye una grilla vacia, para inyectar el JS 
                        //se usa esta constante y se hace aqui, ya que las grillas se construyen on demand; entonces no se puede inyectar un JS en un postback, solo en la carga inicial de la pagina...
                        base.InjectClientSelectRow("ctl00_ContentMain_rgdMasterGridListManage" + _entityGrid);
                        base.InjectShowMenu(rmnSelection.ClientID, "ctl00_ContentMain_rgdMasterGridListManage" + _entityGrid);

                        //Agrega el TAb al TabStrip.
                        _RtsTabStrip.Tabs.Add(_radTab);

                        //Agrega el Tab y el MultiPage al panel a retornar que es lo que se inyectara en la pagina.
                        pnlTabStrip.Controls.Add(_RtsTabStrip);
                        pnlListViewerRelatedData.Controls.Add(_RmpMultiPage);
                    }
                }
            }
            private void LoadRelatedData(String entityGrid)
            {
                //Y ahora construye un PageView con su contenido...
                RadPageView _rpvPageView = BuildPageView("rpvPageView_" + entityGrid, true);
                if (!String.IsNullOrEmpty(entityGrid))
                {
                    //Cargo la grilla con el nombre de la entidad indicada
                    if (_RgdMasterGridListManageRelated.ContainsKey(entityGrid))
                    {
                        _RgdMasterGridListManageRelated.Remove(entityGrid);
                    }
                    _RgdMasterGridListManageRelated.Add(entityGrid, LoadGridRelatedData(entityGrid));
                    _RgdMasterGridListManageRelated[entityGrid].ClientSettings.ClientEvents.OnRowContextMenu = "RowContextMenu";

                    //Finalmente agrega la grilla al pageView...
                    _rpvPageView.Controls.Add(_RgdMasterGridListManageRelated[entityGrid]);

                    //Arma los items del menu.
                    LoadMenuSelectionForGridRelated();
                }

                //Limpia los PageViewer del MultiPage...
                _RmpMultiPage.PageViews.Clear();
                //Agrega el PageView al MultiPage.
                _RmpMultiPage.PageViews.Add(_rpvPageView);
            }
            private RadGrid LoadGridRelatedData(String entityGrid)
            {
                //Construye la grilla para la entidad indicada.
                Boolean _showImgSelect = true;
                Boolean _showCheck = false;
                Boolean _allowSearchableGrid = false;
                Boolean _showOpenFile = false;
                Boolean _showOpenChart = false;
                Boolean _showOpenSeries = false;

                //Si no existe el ParentEntity lo agrego con el nombre de la entidad general que esta visualizando...
                if (ManageEntityParams.ContainsKey("ParentEntity"))
                {
                    ManageEntityParams.Remove("ParentEntity");
                    ManageEntityParams.Add("ParentEntity", _EntityName);
                }
                else
                {
                    ManageEntityParams.Add("ParentEntity", _EntityName);
                }

                BuildGenericDataTable(entityGrid, ManageEntityParams);
                //Si se tiene que mostrar un file, entonces agrega la columna de openfile.
                if ((entityGrid.Contains(Common.ConstantsEntitiesName.KC.ResourceVersion)) || (entityGrid.Contains(Common.ConstantsEntitiesName.PF.ProcessResource)))
                { _showOpenFile = true; }
                //Si se esta visualizando un key indicator, entonces agrega las columnas showchart y showseries
                if (entityGrid.Contains(Common.ConstantsEntitiesName.PA.KeyIndicator))
                {
                    _showOpenChart = true;
                    _showOpenSeries = true;
                }
                RadGrid _RgdMasterGridRelated = base.BuildListManageContent(entityGrid, _showImgSelect, _showCheck, _showOpenFile, _showOpenChart, _showOpenSeries, _allowSearchableGrid);
                base.PersistControlState(_RgdMasterGridRelated);
                _RgdMasterGridRelated.ItemCreated += new GridItemEventHandler(rgdMasterGrid_ItemCreated);

                //Si tiene el open file, agrego el evento e inyecto el JS.
                if (_showOpenFile)
                {
                    _RgdMasterGridRelated.ItemDataBound += new GridItemEventHandler(RgdMasterGridListManage_ItemDataBound);
                    InjectShowFile(_RgdMasterGridRelated.ClientID);
                    //vuelve a la normalidad..
                    _showOpenFile = false;
                }
                //Si tiene el ShowChart o ShowSeries, agrego el evento e inyecto el JS.
                if ((_showOpenChart) || (_showOpenSeries))
                {
                    _RgdMasterGridRelated.ItemDataBound += new GridItemEventHandler(RgdMasterGridListManageKeyIndicator_ItemDataBound);
                    InjectShowChart();
                    InjectShowSeries();
                    //vuelve a la normalidad..
                    _showOpenChart = false;
                    _showOpenSeries = false;
                }
                else
                {   //Esto es para poder disparar el DataSeries desde la visualizacion de un Measurement o Tarea de Medicion...
                    switch (_EntityName)
                    {
                        case Common.ConstantsEntitiesName.PA.Measurement:
                        case Common.ConstantsEntitiesName.PA.KeyIndicator:
                        case Common.ConstantsEntitiesName.PF.ProcessTaskMeasurement:
                        case Common.ConstantsEntitiesName.PA.Transformation:
                        case Common.ConstantsEntitiesName.PA.TransformationByTransformation:
                            InjectShowChart(); 
                            InjectShowSeries();
                            break;
                    }
                }

                return _RgdMasterGridRelated;
            }
            private void LoadMenuSelectionForGridRelated()
            {
                //Arma los items del menu.
                Boolean _showDelete = true;
                Boolean _showEdit = true;
                Boolean _showView = true;
                Boolean _showExecution = false;
                Boolean _showCompute = false;
                Boolean _showCloseException = false;
                Boolean _showTreatException = false;
                Boolean _showCreateException = false;

                rmnSelection.Items.Clear();
                //POr ahora lo hago para la entidad Execution...despues vemos si es necesario meterlo en un Base por Reflection
                switch (_EntityNameRelatedOn)
                {

                    case "Component":
                    case "Log":
                    case "AnalysisCenter":
                    //case Common.ConstantsEntitiesName.PA.ParameterGroup:
                    case Common.ConstantsEntitiesName.PA.Measurement:
                    case Common.ConstantsEntitiesName.PA.MeasurementStatistical:
                        _showDelete = false;
                        _showEdit = false;
                        _showView = false;
                        break;

                    case Common.ConstantsEntitiesName.IA.Exception:
                        _showCloseException = true;
                        _showTreatException = true;
                        _showEdit = false;
                        _showDelete = false;
                        break;

                    case Common.ConstantsEntitiesName.PF.ProcessTaskExecution:
                    case Common.ConstantsEntitiesName.PF.ProcessTaskExecutionCalibration:
                    case Common.ConstantsEntitiesName.PF.ProcessTaskExecutionOperation:
                    case Common.ConstantsEntitiesName.PF.ProcessTaskExecutionMeasurement:
                        _showDelete = false;
                        _showEdit = false;
                        //Como son ejecuciones, puedo ejecutar o crear excepcion, pero para crear una excepcion debe ya estar ejecutada.
                        _showExecution = true;
                        _showCreateException = true;
                        break;

                    case Common.ConstantsEntitiesName.DB.FinishedTasks:
                        _showDelete = false;
                        _showView = false;
                        _showEdit = false;
                        _showCreateException = true;
                        break;
                    case Common.ConstantsEntitiesName.DB.ClosedExceptions:
                        _showDelete = false;
                        _showView = false;
                        _showEdit = false;
                        break;

                    case Common.ConstantsEntitiesName.DB.OpenedExceptions:
                    case Common.ConstantsEntitiesName.DB.WorkingExceptions:
                        _showDelete = false;
                        _showView = false;
                        _showEdit = false;
                        _showExecution = true;
                        _showCloseException = true;
                        _showTreatException = true;
                        break;

                    case Common.ConstantsEntitiesName.DB.PlannedTasks:
                    case Common.ConstantsEntitiesName.DB.ActiveTasks:
                    case Common.ConstantsEntitiesName.DB.OverDueTasks:
                    case Common.ConstantsEntitiesName.DB.AllMyExecutions:
                    case Common.ConstantsEntitiesName.DB.ProcessTaskExecutionExecuted:
                        _showDelete = false;
                        _showView = false;
                        _showEdit = false;
                        _showExecution = true;
                        break;

                    //Todas las entidades que no tienen EDIT
                    case Common.ConstantsEntitiesName.DS.Applicability:
                    case Common.ConstantsEntitiesName.PF.TimeUnit:
                        _showEdit = false;
                        _showDelete = false;
                        break;

                    case Common.ConstantsEntitiesName.PA.Calculations:
                    case Common.ConstantsEntitiesName.PA.Calculation:
                        _showCompute = true;
                        break;
                }
                BuildManageContextMenu(ref rmnSelection, new RadMenuEventHandler(rmnSelection_ItemClick), _showDelete, _showEdit, _showView, _showExecution, _showCompute, _showCloseException, _showTreatException, _showCreateException);
                //InjectContextMenuSelectionOnClientShowing(_RgdMasterGridListManageRelated[_EntityNameGridRelatedOn].ClientID, true);
                base.InjectContextMenuSelectionOnClientShowing("ctl00_ContentMain_rgdMasterGridListManage" + _EntityNameGridRelatedOn, true);

                //Si existe un Delete...hay que sacarlo...
                if (rmnSelection.Items.FindItemByValue("rmiDelete") != null)
                {
                    rmnSelection.Items.FindItemByValue("rmiDelete").Visible = false;
                }

            }
        #endregion

        #region Page Events
            protected void rmnSelection_ItemClick(object sender, Telerik.Web.UI.RadMenuEventArgs e)
            {
                String _actionTitleDecorator = GetActionTitleDecorator(e.Item);
                String _entityPropertyName = GetContextInfoCaption(EntityNameGrid, _RgdMasterGridListManageRelated[_EntityNameGridRelatedOn]);
                String _urlProperties = String.Empty;

                switch (e.Item.Value)
                {
                    case "rmiView":  //VIEW
                        FilterExpressionGrid = String.Empty;
                        //Setea los parametros en el Navigate.
                        base.BuildNavigateParamsFromListManageSelected(_RgdMasterGridListManageRelated[_EntityNameGridRelatedOn]);

                        //Aca vuelve a enviar el PKCompost, y ademas el DataKeyName de la grilla lo mete ahi, para que los use el GRC.
                        String _pkCompost = NavigatorGetPkEntityIdTransferVar<String>("PkCompost")
                            + "&" + BuildParamsFromListManageSelected(_RgdMasterGridListManageRelated[_EntityNameGridRelatedOn]);
                        NavigatorAddPkEntityIdTransferVar("PkCompost", _pkCompost);

                        if (_RgdMasterGridListManageRelated[_EntityNameGridRelatedOn].MasterTableView.Columns.FindByUniqueNameSafe("EntityName") != null)
                        {
                            base.NavigatorAddTransferVar("EntityName", _RgdMasterGridListManageRelated[_EntityNameGridRelatedOn].MasterTableView.Items[_RgdMasterGridListManageRelated[_EntityNameGridRelatedOn].SelectedItems[0].ItemIndex].Cells[_RgdMasterGridListManageRelated[_EntityNameGridRelatedOn].Columns.FindByUniqueNameSafe("EntityName").OrderIndex].Text);
                        }
                        else
                        {
                            base.NavigatorAddTransferVar("EntityName", _EntityNameRelatedOn.Replace("_LG", String.Empty));
                        }
                        base.NavigatorAddTransferVar("EntityNameGrid", _EntityNameGridRelatedOn.Replace("_LG", String.Empty));
                        base.NavigatorAddTransferVar("EntityNameContextInfo", _EntityNameGRC);
                        base.NavigatorAddTransferVar("EntityNameContextElement", _EntityNameContextElement);

                        String _entityName = _EntityNameRelatedOn.Replace("_LG", String.Empty);
                        String _url = GetPageViewerByEntity(_entityName);
                        //NavigateEntity(_url, NavigateMenuAction.View);

                        NavigateEntity(_url, _entityName, _entityPropertyName, _actionTitleDecorator, NavigateMenuAction.View);

                        break;

                    case "rmiEdit":  //EDIT
                        FilterExpressionGrid = String.Empty;

                        base.BuildNavigateParamsFromListManageSelected(_RgdMasterGridListManageRelated[_EntityNameGridRelatedOn]);
                        if (NavigatorContainsPkTransferVar("PkCompost"))
                        {
                            base.NavigatorAddPkEntityIdTransferVar("PkCompost", NavigatorGetPkEntityIdTransferVar<String>("PkCompost"));
                        }

                        _urlProperties = GetGlobalResourceObject("URLEntityProperties", _EntityNameRelatedOn).ToString();
                        _actionTitleDecorator = GetActionTitleDecorator(e.Item);

                        //NavigateEntity(_urlProperties, _actionTitleDecorator, NavigateMenuAction.Edit);
                        NavigateEntity(_urlProperties, _EntityNameRelatedOn, _entityPropertyName, _actionTitleDecorator, NavigateMenuAction.Edit);
                        break;

                    //Para el caso de estar viendo Calculos, se puede ejecutar!!!
                    case "rmiCompute":
                        try
                        {
                            Int64 _idCalculation = Convert.ToInt64(GetKeyValue(BuildParamsFromListManageSelected(_RgdMasterGridListManageRelated[_EntityNameGridRelatedOn]), "IdCalculation"));
                            Condesus.EMS.Business.PA.Entities.Calculation _calculation = EMSLibrary.User.PerformanceAssessments.Configuration.Calculation(_idCalculation);
                            Decimal _yValueCalculated = _calculation.Calculate();

                            FilterExpressionGrid = String.Empty;
                            //Setea los datos en el DataTable de base, para que luego se carge la grilla.
                            BuildGenericDataTable(EntityNameGrid, ManageEntityParams);
                            //Recarga la grilla
                            _RgdMasterGridListManageRelated[_EntityNameGridRelatedOn].Rebind();
                        }
                        catch (Exception ex)
                        {
                            //Mostrar en el Status Bar....
                            base.StatusBar.ShowMessage(ex);
                        }
                        break;

                    case "rmiCreateException":
                        FilterExpressionGrid = String.Empty;

                        base.NavigatorAddTransferVar("ExceptionState", Common.Constants.ExceptionStateCreateName);
                        //Redireccionar a la pagina que se utiliza para agregar nuevos registros del mismo tipo
                        base.BuildNavigateParamsFromListManageSelected(_RgdMasterGridListManageRelated[_EntityNameGridRelatedOn]);
                        if (NavigatorContainsPkTransferVar("PkCompost"))
                        {
                            base.NavigatorAddPkEntityIdTransferVar("PkCompost", NavigatorGetPkEntityIdTransferVar<String>("PkCompost"));
                        }

                        _urlProperties = GetGlobalResourceObject("URLEntityProperties", _EntityNameRelatedOn).ToString();

                        NavigateEntity(_urlProperties, _EntityNameRelatedOn, _entityPropertyName, _actionTitleDecorator, NavigateMenuAction.Edit);
                        break;

                    case "rmiCloseException":
                        FilterExpressionGrid = String.Empty;

                        base.NavigatorAddTransferVar("ExceptionState", Common.Constants.ExceptionStateCloseName);
                        //Redireccionar a la pagina que se utiliza para agregar nuevos registros del mismo tipo
                        base.BuildNavigateParamsFromListManageSelected(_RgdMasterGridListManageRelated[_EntityNameGridRelatedOn]);
                        if (NavigatorContainsPkTransferVar("PkCompost"))
                        {
                            base.NavigatorAddPkEntityIdTransferVar("PkCompost", NavigatorGetPkEntityIdTransferVar<String>("PkCompost"));
                        }

                        _urlProperties = GetGlobalResourceObject("URLEntityProperties", _EntityNameRelatedOn).ToString();

                        NavigateEntity(_urlProperties, _EntityNameRelatedOn, _entityPropertyName, _actionTitleDecorator, NavigateMenuAction.Edit);
                        break;

                    case "rmiTreatException":
                        FilterExpressionGrid = String.Empty;

                        base.NavigatorAddTransferVar("ExceptionState", Common.Constants.ExceptionStateTreatName);
                        //Redireccionar a la pagina que se utiliza para agregar nuevos registros del mismo tipo
                        base.BuildNavigateParamsFromListManageSelected(_RgdMasterGridListManageRelated[_EntityNameGridRelatedOn]);
                        if (NavigatorContainsPkTransferVar("PkCompost"))
                        {
                            base.NavigatorAddPkEntityIdTransferVar("PkCompost", NavigatorGetPkEntityIdTransferVar<String>("PkCompost"));
                        }

                        _urlProperties = GetGlobalResourceObject("URLEntityProperties", _EntityNameRelatedOn).ToString();

                        NavigateEntity(_urlProperties, _EntityNameRelatedOn, _entityPropertyName, _actionTitleDecorator, NavigateMenuAction.Edit);
                        break;

                    default:  //DELETE se ejecuta en el btnOkDelete_Click()
                        break;
                }
            }
            protected void _RtsTabStrip_TabClick(object sender, RadTabStripEventArgs e)
            {
                //Construye la grilla en el tab seleccionado.
                _EntityNameGridRelatedOn = e.Tab.Attributes["EntityNameGrid"];
                _EntityNameRelatedOn = e.Tab.Attributes["EntityName"];
                LoadRelatedData(_EntityNameGridRelatedOn);
            }
            protected void PagerPicture_Click(object sender, ImageClickEventArgs e)
            {
                IButtonControl _pagerButton = (IButtonControl)sender;

                //Muevo la Posicion
                Int32 _index = Int32.Parse(hdn_ImagePosition.Value);
                _index += Int32.Parse(_pagerButton.CommandArgument);
                hdn_ImagePosition.Value = _index.ToString();

                //Con el Index Rearmo la Pantalla con la foto nueva
                SetPagerStatus(_index);

                SetImageViewerContent(_index);
            }
            protected void GridLinkButtonClick_Click(object sender, EventArgs e)
            {
                //Debo acceder a la grilla...
                //Como puede haber muchas grillas, entonces la busco como padre el linkButton.
                RadGrid _rgdViewers = (RadGrid)((LinkButton)sender).Parent.Parent.Parent.Parent.Parent;

                //Setea los parametros en el Navigate.
                BuildNavigateParamsFromListManageSelected(_rgdViewers);

                //Aca vuelve a enviar el PKCompost, y ademas el DataKeyName de la grilla lo mete ahi, para que los use el GRC.
                String _pkCompost = NavigatorGetPkEntityIdTransferVar<String>("PkCompost")
                    + "&" + BuildParamsFromListManageSelected(_rgdViewers);
                NavigatorAddPkEntityIdTransferVar("PkCompost", _pkCompost);

                //String _paramsToNavigate = _pkCompost;
                String _paramsToNavigate = ((LinkButton)sender).CommandArgument; //_pkCompost;
                if (String.IsNullOrEmpty(_paramsToNavigate))
                {
                    //Si viene vacio, entonces uso lo que tiene cargado pkCompost...
                    _paramsToNavigate = _pkCompost;
                }
                String _entityName = Convert.ToString(GetKeyValue(_paramsToNavigate, "EntityName"));
                NavigatorAddTransferVar("EntityName", _entityName);
                NavigatorAddTransferVar("EntityNameGrid", Convert.ToString(GetKeyValue(_paramsToNavigate, "EntityNameGrid")));
                NavigatorAddTransferVar("EntityNameContextInfo", Convert.ToString(GetKeyValue(_paramsToNavigate, "EntityNameContextInfo")));
                NavigatorAddTransferVar("EntityNameContextElement", Convert.ToString(GetKeyValue(_paramsToNavigate, "EntityNameContextElement")));

                String _url = GetPageViewerByEntity(_entityName);
                IButtonControl _lnkButton = (IButtonControl)sender;
                String _entityPropertyName = _lnkButton.Text;
                NavigateEntity(_url, _entityName, _entityPropertyName, NavigateMenuType.ListManagerMenu);
            }
            protected void rmnuGeneralOption_ItemClick(object sender, Telerik.Web.UI.RadMenuEventArgs e)
            {
                String _urlProperties = String.Empty;
                String _actionTitleDecorator = GetActionTitleDecorator(e.Item);

                switch (e.Item.Value)
                {
                    case "rmiAdd": //ADD
                        //Redireccionar a la pagina que se utiliza para agregar nuevos registros del mismo tipo.
                        base.NavigatorClearTransferVars();
                        if (NavigatorContainsPkTransferVar("PkCompost"))
                        {
                            base.NavigatorAddPkEntityIdTransferVar("PkCompost", NavigatorGetPkEntityIdTransferVar<String>("PkCompost"));
                        }
                        if (base.NavigatorContainsTransferVar("ParentEntity"))
                        {
                            base.NavigatorAddTransferVar("ParentEntity", NavigatorGetTransferVar<String>("ParentEntity"));
                        }
                        base.NavigatorAddTransferVar("EntityNameContextInfo", _EntityNameGRC);
                        base.NavigatorAddTransferVar("EntityNameContextElement", _EntityNameContextElement);

                        _urlProperties = GetGlobalResourceObject("URLEntityProperties", _EntityName).ToString();
                        //Navigate(_urlProperties, _EntityName);
                        //NavigateEntity(_urlProperties, _actionTitleDecorator, NavigateMenuAction.Add);
                        NavigateEntity(_urlProperties, _EntityName, _actionTitleDecorator, NavigateMenuAction.Add);
                        break;

                    case "rmiEdit":  //EDIT
                        //Redireccionar a la pagina que se utiliza para agregar nuevos registros del mismo tipo
                        String _pkValues = NavigatorGetPkEntityIdTransferVar<String>("PkCompost");
                        //Si quedaron variables que no estaban en el PKCompost, las leo del NavigatorTransferenceColl..
                        foreach (KeyValuePair<String, Object> _item in NavigatorTransferenceCollection)
                        {
                            if (_item.Key.Substring(0, 2) == "Id")
                            {
                                if (String.IsNullOrEmpty(_pkValues))
                                //Si esta vacio, solo pone el key...
                                { _pkValues += _item.Key + "=" + _item.Value; }
                                else
                                //Si ya tiene un dato, entonces concatena el separador...
                                { _pkValues += "&" + _item.Key + "=" + _item.Value; }
                            }
                        }
                        BuildNavigateParamsFromSelectedValue(_pkValues);

                        _urlProperties = GetGlobalResourceObject("URLEntityProperties", _EntityName).ToString();
                        //Navigate(_urlProperties, EntityNameGrid + " " + e.Item.Text);
                        //NavigateEntity(_urlProperties, _actionTitleDecorator, NavigateMenuAction.Edit);
                        NavigateEntity(_urlProperties, _EntityName, GetPageTitleForViewer(), _actionTitleDecorator, NavigateMenuAction.Edit);
                        break;

                    case "rmiLanguage":
                        String _entityName = _EntityName.Replace("_LG", String.Empty) + "_LG";
                        base.NavigatorAddTransferVar("EntityName", _entityName);
                        base.NavigatorAddTransferVar("EntityNameGrid", _EntityName.Replace("_LG", String.Empty) + "_LG");
                        base.NavigatorAddTransferVar("IsFilterHierarchy", false);

                        //Se concatenan las PK con el Key = Values, si hay mas, el separador es el "&"
                        base.NavigatorAddPkEntityIdTransferVar("PkCompost", NavigatorGetPkEntityIdTransferVar<String>("PkCompost"));
                        //Navigate("~/Managers/ListManageAndView.aspx", _EntityName + " " + Resources.Common.Languages);
                        //NavigateEntity("~/Managers/ListManageAndView.aspx", _entityName, _actionTitleDecorator, NavigateMenuType.ContextInfoMenu);
                        NavigateEntity("~/Managers/ListManageAndView.aspx", _entityName, GetPageTitleForViewer(), _actionTitleDecorator, NavigateMenuType.ContextInfoMenu);
                        break;

                    default:
                        break;
                }
            }
            protected void rmnuSecuritySystem_ItemClick(object sender, RadMenuEventArgs e)
            {
                String _pkValues = String.Empty;
                //Igual para todos...
                if (NavigatorContainsPkTransferVar("PkCompost"))
                {
                    _pkValues = NavigatorGetPkEntityIdTransferVar<String>("PkCompost");
                    if (GetKeyValue(_pkValues, "ParentEntity") == null)
                    {
                        _pkValues += "&ParentEntity=" + _EntityName;
                        //base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);
                    }
                }
                else
                {
                    _pkValues = "ParentEntity=" + _EntityName;
                    //base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);
                }
                //Si quedaron variables que no estaban en el PKCompost, las leo del NavigatorTransferenceColl..
                foreach (KeyValuePair<String, Object> _item in NavigatorTransferenceCollection)
                {
                    if (_item.Key.Substring(0, 2) == "Id")
                    {
                        if (String.IsNullOrEmpty(_pkValues))
                        //Si esta vacio, solo pone el key...
                        { _pkValues += _item.Key + "=" + _item.Value; }
                        else
                        //Si ya tiene un dato, entonces concatena el separador...
                        { _pkValues += "&" + _item.Key + "=" + _item.Value; }
                    }
                }
                base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);

                switch (e.Item.Value)
                {
                    case "rmiSSJobTitles":
                        SecuritySystemJobTitlesNavigate();
                        break;

                    case "rmiSSPerson":
                        SecuritySystemPersonNavigate();
                        break;

                    default:
                        break;
                }//fin Switch
            }//fin evento
            private void SecuritySystemJobTitlesNavigate()
            {
                String _entityNameGrid = String.Empty;
                String _entityName = String.Empty;
                String _pageTitle = _PageTitleLocal + " - Right JobTitle";
                String _navigateEntity = String.Empty;
                String _actionTitleDecorator = _pageTitle;

                switch (_EntityName)
                {
                    case Common.ConstantsEntitiesName.DS.OrganizationClassification:
                        _entityNameGrid = Common.ConstantsEntitiesName.SS.RightJobTitleOrganizationClassifications;
                        _entityName = Common.ConstantsEntitiesName.SS.RightJobTitleOrganizationClassification;
                        _navigateEntity = Common.ConstantsEntitiesName.SS.RightJobTitleOrganizationClassifications;
                        break;
                    case Common.ConstantsEntitiesName.DS.Organization:
                        _entityNameGrid = Common.ConstantsEntitiesName.SS.RightJobTitleOrganizations;
                        _entityName = Common.ConstantsEntitiesName.SS.RightJobTitleOrganization;
                        _navigateEntity = Common.ConstantsEntitiesName.SS.RightJobTitleOrganizations;
                        break;
                    case Common.ConstantsEntitiesName.PF.ProcessClassification:
                        _entityNameGrid = Common.ConstantsEntitiesName.SS.RightJobTitleProcessClassifications;
                        _entityName = Common.ConstantsEntitiesName.SS.RightJobTitleProcessClassification;
                        _navigateEntity = Common.ConstantsEntitiesName.SS.RightJobTitleProcessClassifications;
                        break;
                    case Common.ConstantsEntitiesName.PF.ProcessGroupProcess:
                        _entityNameGrid = Common.ConstantsEntitiesName.SS.RightJobTitleProcesses;
                        _entityName = Common.ConstantsEntitiesName.SS.RightJobTitleProcess;
                        _navigateEntity = Common.ConstantsEntitiesName.SS.RightJobTitleProcesses;
                        break;
                    case Common.ConstantsEntitiesName.PA.IndicatorClassification:
                        _entityNameGrid = Common.ConstantsEntitiesName.SS.RightJobTitleIndicatorClassifications;
                        _entityName = Common.ConstantsEntitiesName.SS.RightJobTitleIndicatorClassification;
                        _navigateEntity = Common.ConstantsEntitiesName.SS.RightJobTitleIndicatorClassifications;
                        break;
                    case Common.ConstantsEntitiesName.PA.Indicator:
                        _entityNameGrid = Common.ConstantsEntitiesName.SS.RightJobTitleIndicators;
                        _entityName = Common.ConstantsEntitiesName.SS.RightJobTitleIndicator;
                        _navigateEntity = Common.ConstantsEntitiesName.SS.RightJobTitleIndicators;
                        break;
                    case Common.ConstantsEntitiesName.KC.ResourceClassification:
                        _entityNameGrid = Common.ConstantsEntitiesName.SS.RightJobTitleResourceClassifications;
                        _entityName = Common.ConstantsEntitiesName.SS.RightJobTitleResourceClassification;
                        _navigateEntity = Common.ConstantsEntitiesName.SS.RightJobTitleResourceClassifications;
                        break;
                    case Common.ConstantsEntitiesName.KC.Resource:
                        _entityNameGrid = Common.ConstantsEntitiesName.SS.RightJobTitleResources;
                        _entityName = Common.ConstantsEntitiesName.SS.RightJobTitleResource;
                        _navigateEntity = Common.ConstantsEntitiesName.SS.RightJobTitleResources;
                        break;
                    case Common.ConstantsEntitiesName.IA.ProjectClassification:
                        _entityNameGrid = Common.ConstantsEntitiesName.SS.RightJobTitleProjectClassifications;
                        _entityName = Common.ConstantsEntitiesName.SS.RightJobTitleProjectClassification;
                        _navigateEntity = Common.ConstantsEntitiesName.SS.RightJobTitleProjectClassifications;
                        break;
                    case Common.ConstantsEntitiesName.RM.RiskClassification:
                        _entityNameGrid = Common.ConstantsEntitiesName.SS.RightJobTitleRiskClassifications;
                        _entityName = Common.ConstantsEntitiesName.SS.RightJobTitleRiskClassification;
                        _navigateEntity = Common.ConstantsEntitiesName.SS.RightJobTitleRiskClassifications;
                        break;
                }

                base.NavigatorAddTransferVar("EntityNameGrid", _entityNameGrid);
                base.NavigatorAddTransferVar("EntityName", _entityName);
                base.NavigatorAddTransferVar("PageTitle", _pageTitle);

                //base.Navigate("~/Managers/ListManageAndView.aspx", _navigateEntity);
                NavigateEntity("~/Managers/ListManageAndView.aspx", _entityName, _actionTitleDecorator, NavigateMenuType.ContextInfoMenu);
            }
            private void SecuritySystemPersonNavigate()
            {
                String _entityNameGrid = String.Empty;
                String _entityName = String.Empty;
                String _pageTitle = _PageTitleLocal + " - Right Post";
                String _navigateEntity = String.Empty;
                String _actionTitleDecorator = _pageTitle;

                switch (_EntityName)
                {
                    case Common.ConstantsEntitiesName.DS.OrganizationClassification:
                        _entityNameGrid = Common.ConstantsEntitiesName.SS.RightPersonOrganizationClassifications;
                        _entityName = Common.ConstantsEntitiesName.SS.RightPersonOrganizationClassification;
                        _navigateEntity = Common.ConstantsEntitiesName.SS.RightPersonOrganizationClassifications;
                        break;
                    case Common.ConstantsEntitiesName.DS.Organization:
                        _entityNameGrid = Common.ConstantsEntitiesName.SS.RightPersonOrganizations;
                        _entityName = Common.ConstantsEntitiesName.SS.RightPersonOrganization;
                        _navigateEntity = Common.ConstantsEntitiesName.SS.RightPersonOrganizations;
                        break;
                    case Common.ConstantsEntitiesName.PF.ProcessClassification:
                        _entityNameGrid = Common.ConstantsEntitiesName.SS.RightPersonProcessClassifications;
                        _entityName = Common.ConstantsEntitiesName.SS.RightPersonProcessClassification;
                        _navigateEntity = Common.ConstantsEntitiesName.SS.RightPersonProcessClassifications;
                        break;
                    case Common.ConstantsEntitiesName.PF.ProcessGroupProcess:
                        _entityNameGrid = Common.ConstantsEntitiesName.SS.RightPersonProcesses;
                        _entityName = Common.ConstantsEntitiesName.SS.RightPersonProcess;
                        _navigateEntity = Common.ConstantsEntitiesName.SS.RightPersonProcesses;
                        break;
                    case Common.ConstantsEntitiesName.PA.IndicatorClassification:
                        _entityNameGrid = Common.ConstantsEntitiesName.SS.RightPersonIndicatorClassifications;
                        _entityName = Common.ConstantsEntitiesName.SS.RightPersonIndicatorClassification;
                        _navigateEntity = Common.ConstantsEntitiesName.SS.RightPersonIndicatorClassifications;
                        break;
                    case Common.ConstantsEntitiesName.PA.Indicator:
                        _entityNameGrid = Common.ConstantsEntitiesName.SS.RightPersonIndicators;
                        _entityName = Common.ConstantsEntitiesName.SS.RightPersonIndicator;
                        _navigateEntity = Common.ConstantsEntitiesName.SS.RightPersonIndicators;
                        break;
                    case Common.ConstantsEntitiesName.KC.ResourceClassification:
                        _entityNameGrid = Common.ConstantsEntitiesName.SS.RightPersonResourceClassifications;
                        _entityName = Common.ConstantsEntitiesName.SS.RightPersonResourceClassification;
                        _navigateEntity = Common.ConstantsEntitiesName.SS.RightPersonResourceClassifications;
                        break;
                    case Common.ConstantsEntitiesName.KC.Resource:
                        _entityNameGrid = Common.ConstantsEntitiesName.SS.RightPersonResources;
                        _entityName = Common.ConstantsEntitiesName.SS.RightPersonResource;
                        _navigateEntity = Common.ConstantsEntitiesName.SS.RightPersonResources;
                        break;
                    case Common.ConstantsEntitiesName.IA.ProjectClassification:
                        _entityNameGrid = Common.ConstantsEntitiesName.SS.RightPersonProjectClassifications;
                        _entityName = Common.ConstantsEntitiesName.SS.RightPersonProjectClassification;
                        _navigateEntity = Common.ConstantsEntitiesName.SS.RightPersonProjectClassifications;
                        break;
                    case Common.ConstantsEntitiesName.RM.RiskClassification:
                        _entityNameGrid = Common.ConstantsEntitiesName.SS.RightPersonRiskClassifications;
                        _entityName = Common.ConstantsEntitiesName.SS.RightPersonRiskClassification;
                        _navigateEntity = Common.ConstantsEntitiesName.SS.RightPersonRiskClassifications;
                        break;
                }

                base.NavigatorAddTransferVar("EntityNameGrid", _entityNameGrid);
                base.NavigatorAddTransferVar("EntityName", _entityName);
                base.NavigatorAddTransferVar("PageTitle", _pageTitle);

                //base.Navigate("~/Managers/ListManageAndView.aspx", _navigateEntity);
                NavigateEntity("~/Managers/ListManageAndView.aspx", _entityName, _actionTitleDecorator, NavigateMenuType.ContextInfoMenu);
            }
            protected void btnOkDelete_Click(object sender, EventArgs e)
            {
                try
                {
                    //Se borra el elemento seleccionado.
                    ExecuteGenericMethodEntity(EntityNameToRemove, ManageEntityParams);

                    //Mostrar en el Status Bar
                    base.StatusBar.ShowMessage(Resources.Common.DeleteOK);

                    String _pkValues = String.Empty;
                    //Redireccionar a la pagina que se utiliza para agregar nuevos registros del mismo tipo.
                    if (NavigatorContainsPkTransferVar("PkCompost"))
                    {
                        _pkValues = NavigatorGetPkEntityIdTransferVar<String>("PkCompost") + "&";
                    }
                    if (base.NavigatorContainsTransferVar("ParentEntity"))
                    {
                        _pkValues += "ParentEntity=" + NavigatorGetTransferVar<String>("ParentEntity");
                    }
                    base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);

                    //Como es un Delete, hay que limpiar el XML del tree global, si es una entidad mapa(se valida internamente)
                    ValidateClearXMLTreeViewGlobalMenu(_EntityName);

                    //Navega al Manage.
                    //Ejecuta la Navegacion a la PAgina del MAnage que le corresponde segun la Entidad indicada, 
                    //TAmbien arma todos los PArametros necesarios para pasarle al Manage.
                   Navigate(GetParameterToManager(_EntityName, new Dictionary<String, Object>()), _EntityName, Condesus.WebUI.Navigation.DeleteType.DeleteModifiedEntity);
                }
                catch (Exception ex)
                {
                    //Mostrar en el Status Bar....
                    base.StatusBar.ShowMessage(ex);
                }
                //oculta el popup.
                this.mpelbDelete.Hide();
            }
            protected void RgdMasterGridListManage_ItemDataBound(object sender, GridItemEventArgs e)
            {
                if (e.Item is GridDataItem)
                {
                    String _resourceType = ((DataRowView)e.Item.DataItem).Row["ResourceType"].ToString();
                    String _urlName = ((DataRowView)e.Item.DataItem).Row["Name"].ToString();
                    Int64 _idResource = Convert.ToInt64(((DataRowView)e.Item.DataItem).Row["IdResource"]);
                    Int64 _idResourceFile = Convert.ToInt64(((DataRowView)e.Item.DataItem).Row["IdResourceFile"]);

                    HtmlImage oimg2 = (HtmlImage)e.Item.FindControl("rptButton");
                    if (!(oimg2 == null))
                    {
                        if (_idResourceFile > 0)
                        {
                            oimg2.Attributes["onclick"] = string.Format("return ShowFile(event, '" + _resourceType + "','" + _urlName + "'," + _idResource + ", " + _idResourceFile + ");");
                            oimg2.Attributes["onmouseover"] = "this.style.cursor = 'pointer'";
                        }
                        else
                        {
                            //No quiero que salga el icono, pero que no pierda el estilo la celda.
                            oimg2.Attributes["class"] = String.Empty;
                            //oimg2.Visible = false;
                        }
                    }

                    #region Exception Measurement Out Of Range (Color Row)
                        try
                        {
                            String _measurementStatus = ((DataRowView)e.Item.DataItem).Row["Status"].ToString();
                            if (_measurementStatus.ToLower() == "true")
                            {
                                e.Item.ForeColor = System.Drawing.Color.Red;
                            }
                        }
                        catch { }
                    #endregion

                }
            }
            protected void RgdMasterGridListManageKeyIndicator_ItemDataBound(object sender, GridItemEventArgs e)
            {
                if (e.Item is GridDataItem)
                {
                    String _idMeasurement = ((DataRowView)e.Item.DataItem).Row["IdMeasurement"].ToString();

                    ImageButton oimg = (ImageButton)e.Item.FindControl("btnChartLink");
                    if (!(oimg == null))
                    {
                        oimg.Attributes["onclick"] = string.Format("return ShowChart(event, " + _idMeasurement + ");");
                        oimg.Attributes["onmouseover"] = "this.style.cursor = 'pointer'";
                    }

                    ImageButton oimg2 = (ImageButton)e.Item.FindControl("btnSeriesLink");
                    if (!(oimg2 == null))
                    {
                        oimg2.Attributes["onclick"] = string.Format("return ShowSeries(event,  " + _idMeasurement + ");");
                        oimg2.Attributes["onmouseover"] = "this.style.cursor = 'pointer'";
                    }

                    #region Exception Measurement Out Of Range (Color Row)
                        try
                        {
                            String _measurementStatus = ((DataRowView)e.Item.DataItem).Row["Status"].ToString();
                            if (_measurementStatus.ToLower() == "true")
                            {
                                e.Item.ForeColor = System.Drawing.Color.Red;
                            }
                        }
                        catch { }
                    #endregion

                }
            }
            protected void rgdMasterGridListViewer_ItemDataBound(object sender, GridItemEventArgs e)
            {
                if (e.Item is GridDataItem)
                {
                    foreach (GridColumn column in e.Item.OwnerTableView.RenderColumns)
                    {
                        if (column.UniqueName == "Value")
                        {
                            if (String.IsNullOrEmpty(((LinkButton)((GridDataItem)e.Item)["Value"].Controls[0]).Text))
                            {
                                ((LinkButton)((GridDataItem)e.Item)["Value"].Controls[0]).Text = "&nbsp;";
                            }
                        }
                    }
                }
            }
        #endregion
    }
}
