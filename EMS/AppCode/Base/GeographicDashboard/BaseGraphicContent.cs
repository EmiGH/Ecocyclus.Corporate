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
using System.Reflection;
using System.Linq;
using Condesus.EMS.WebUI.MasterControls;
using Condesus.EMS.Business.PF.Entities;
using Condesus.EMS.Business.PA.Entities;
using Condesus.EMS.Business.DS.Entities;
using Condesus.EMS.Business.KC.Entities;
using Dundas.Charting.WebControl;
using Telerik.Charting;

namespace Condesus.EMS.WebUI
{
    public partial class BasePage : System.Web.UI.Page
    {
        #region Internal Properties
            private Dictionary<String, Object> _Params = new Dictionary<String, Object>();
            private String _EntityObject = String.Empty;
            private Image _ImgShowSlide = new Image();
            private ImageButton _BtnPrevPicture = new ImageButton();
            private ImageButton _BtnNextPicture = new ImageButton();
            private Dictionary<Int64, CatalogDoc> _CatalogDoc
            {
                get
                {
                    return GetPicturesByObject(_EntityObject + "Pictures", _Params);
                }
            }
            private Int32 _ImagePosition
            {
                get { return Convert.ToInt32(ViewState["ImagePosition"]); }
                set { ViewState["ImagePosition"] = value; }
            }
            private Dundas.Charting.WebControl.Series _ChartSeries
            {
                get { return _ChartMeasurement.Series["Data"]; }
            }
            private Dundas.Charting.WebControl.Chart _ChartMeasurement;
        #endregion

        #region Image Gallery Methods
            private Panel BuildContentPhotos()
            {
                Panel _pnlContent = new Panel();
                _pnlContent.CssClass = "div";

                _ImgShowSlide.ID = "imgShowSlide";

                HtmlGenericControl _lineBreak = new HtmlGenericControl("br");


                _BtnPrevPicture.ID = "btnPrevPicture";
                _BtnPrevPicture.CommandArgument = "-1";
                _BtnPrevPicture.CssClass = "Back";
                _BtnPrevPicture.Click += new ImageClickEventHandler(PagerPicture_Click);

                _BtnNextPicture.ID = "btnNextPicture";
                _BtnNextPicture.CommandArgument = "1";
                _BtnNextPicture.CssClass = "Next";
                _BtnNextPicture.Click += new ImageClickEventHandler(PagerPicture_Click);

                _pnlContent.Controls.Add(_ImgShowSlide);
                _pnlContent.Controls.Add(_lineBreak);
                _pnlContent.Controls.Add(_BtnPrevPicture);
                _pnlContent.Controls.Add(_BtnNextPicture);

                SetImageViewerContent(0);
                SetPagerStatus(0);

                return _pnlContent;
            }
            private void SetImageViewerContent(Int32 index)
            {
                Int64 _idResource = -1;
                Int64 _idResourceFile = -1;
                try
                {
                    if (_CatalogDoc.Count == 0)
                    {
                        _ImgShowSlide.ImageUrl = "~/Skins/Images/NoImagesAvailable.gif";
                        _ImagePosition = 0;
                        return;
                    }

                    _idResource = _CatalogDoc.ElementAt(index).Value.IdResource;
                    _idResourceFile = _CatalogDoc.ElementAt(index).Value.IdResourceFile;
                }
                catch (IndexOutOfRangeException ex)
                {
                    _idResource = _CatalogDoc.ElementAt(_CatalogDoc.Count - 1).Value.IdResource;
                    _idResourceFile = _CatalogDoc.ElementAt(_CatalogDoc.Count - 1).Value.IdResourceFile;
                    _ImagePosition = (_CatalogDoc.Count - 1);
                }

                _ImgShowSlide.ImageUrl = "~/ManagementTools/KnowledgeCollaboration/FilesViewer.aspx?IdResource=" + _idResource.ToString() + "&IdResourceFile=" + _idResourceFile.ToString();
            }
            private void SetPagerStatus(Int32 index)
            {
                _BtnPrevPicture.Enabled = true;
                _BtnNextPicture.Enabled = true;

                if (index == 0)
                    _BtnPrevPicture.Enabled = false;
                if (index == _CatalogDoc.Count - 1 || index > _CatalogDoc.Count - 1)
                    _BtnNextPicture.Enabled = false;

                //Si no hay ninguna foto en la coleccion
                if (_CatalogDoc.Count == 0)
                    _BtnPrevPicture.Enabled = _BtnNextPicture.Enabled = false;

                _BtnPrevPicture.ImageUrl = "~/Skins/Images/Trans.gif";
                _BtnNextPicture.ImageUrl = "~/Skins/Images/Trans.gif";
            }
            #region Events
                protected void PagerPicture_Click(object sender, ImageClickEventArgs e)
                {
                    IButtonControl _pagerButton = (IButtonControl)sender;

                    //Muevo la Posicion
                    Int32 _index = _ImagePosition;
                    _index += Int32.Parse(_pagerButton.CommandArgument);
                    _ImagePosition = _index;

                    //Con el Index Rearmo la Pantalla con la foto nueva
                    SetPagerStatus(_index);

                    SetImageViewerContent(_index);
                }
            #endregion
        #endregion

        #region Chart Methods
            private void LoadMeasurementChart(Int64 idEntity)
            {
                _ChartMeasurement.Visible = true;
                Measurement _measurement = EMSLibrary.User.PerformanceAssessments.Configuration.Measurement(idEntity);
                List<Condesus.EMS.Business.PA.Entities.MeasurementPoint> series;
                //Si la frequencia, de menor al dia, entonces se agrupa por dia
                if (_measurement.TimeUnitFrequency >= 3)
                {
                    series = _measurement.Series(null, null, Measurement.GroupingType.Day, Measurement.AggregateType.Avg);
                }
                else
                {
                    //Sino muestra todo como viene.
                    series= _measurement.Series();
                }

                _ChartSeries.Color = System.Drawing.Color.Blue;
                _ChartSeries.Type = SeriesChartType.Line;

                if (series.Count > 0)
                {
                    _ChartSeries.Points.Clear();
                    Int32 _interval = (Int32)(series.Count / 22);
                    _interval = (_interval == 0) ? 1 : _interval;
                    _ChartMeasurement.ChartAreas["AreaData"].AxisX.MajorTickMark.Interval = _interval;
                    _ChartMeasurement.ChartAreas["AreaData"].AxisX.MajorGrid.Interval = _interval;
                    _ChartMeasurement.ChartAreas["AreaData"].AxisX.LabelStyle.Interval = _interval;
                    _ChartMeasurement.ChartAreas["AreaData"].AxisX.LabelsAutoFit = true;

                    foreach (Condesus.EMS.Business.PA.Entities.MeasurementPoint _point in series)
                    {
                        _ChartSeries.Points.AddXY(_point.MeasureDate.ToString(), Convert.ToDouble(_point.MeasureValue));
                    }
                }
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
            private void LoadChartBarActivityByScope1AndFacility(RadChart radChart, Int64 idFacility, Int64 idProcess)
            {
                //Setea y arma una paleta de colores para las referencias
                SetChartCustomPalette(radChart);
                if (ManageEntityParams.ContainsKey("IdScope"))
                { ManageEntityParams.Remove("IdScope"); }
                ManageEntityParams.Add("IdScope", 1);

                if (ManageEntityParams.ContainsKey("IdProcess"))
                { ManageEntityParams.Remove("IdProcess"); }
                ManageEntityParams.Add("IdProcess", idProcess);

                if (ManageEntityParams.ContainsKey("IdFacility"))
                { ManageEntityParams.Remove("IdFacility"); }
                ManageEntityParams.Add("IdFacility", idFacility);

                ProcessGroupProcess _process = (ProcessGroupProcess)EMSLibrary.User.ProcessFramework.Map.Process(idProcess);
                DateTime _startDate = _process.CurrentCampaignStartDate;
                DateTime _endDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());

                if (ManageEntityParams.ContainsKey("StartDate"))
                { ManageEntityParams.Remove("StartDate"); }
                ManageEntityParams.Add("StartDate", _startDate);

                if (ManageEntityParams.ContainsKey("EndDate"))
                { ManageEntityParams.Remove("EndDate"); }
                ManageEntityParams.Add("EndDate", _endDate);
                
                //Obtiene los indicadores
                LoadAllParameter();

                //Carga el DT
                //if (_Report == "GEI")
                //{
                    BuildGenericDataTable(Common.ConstantsEntitiesName.RG.ChartBarActivityByScopeAndFacilityGEI, ManageEntityParams);
                    // Set a query to database.
                    radChart.DataSource = DataTableListManage[Common.ConstantsEntitiesName.RG.ChartBarActivityByScopeAndFacilityGEI];
                //}
                //else
                //{
                //    BuildGenericDataTable(Common.ConstantsEntitiesName.RG.ChartBarActivityByScopeAndFacilityCL, ManageEntityParams);
                //    // Set a query to database.
                //    radChart.DataSource = DataTableListManage[Common.ConstantsEntitiesName.RG.ChartBarActivityByScopeAndFacilityCL];
                //}

                radChart.DataBind();

                Condesus.EMS.Business.GIS.Entities.Site _site = EMSLibrary.User.GeographicInformationSystem.Site(idFacility);
                // Set additional chart properties and settings.
                radChart.ChartTitle.TextBlock.Text = Resources.CommonListManage.ChartBarGasesActivityByScope + " " + EMSLibrary.User.PerformanceAssessments.Configuration.AccountingScope(1).LanguageOption.Name;
                radChart.ChartTitle.Visible = true;
                radChart.SeriesOrientation = ChartSeriesOrientation.Horizontal;
                for (int i = 0; i < radChart.Series.Count; i++)
                {
                    radChart.Series[i].Type = ChartSeriesType.StackedBar100;
                }

                radChart.Legend.Appearance.GroupNameFormat = "#VALUE";
            }
            private void LoadChartBarActivityByScope1AndFacilityLC(RadChart radChart, Int64 idFacility, Int64 idProcess)
            {
                //Setea y arma una paleta de colores para las referencias
                SetChartCustomPalette(radChart);
                if (ManageEntityParams.ContainsKey("IdScope"))
                { ManageEntityParams.Remove("IdScope"); }
                ManageEntityParams.Add("IdScope", 1);

                if (ManageEntityParams.ContainsKey("IdProcess"))
                { ManageEntityParams.Remove("IdProcess"); }
                ManageEntityParams.Add("IdProcess", idProcess);

                if (ManageEntityParams.ContainsKey("IdFacility"))
                { ManageEntityParams.Remove("IdFacility"); }
                ManageEntityParams.Add("IdFacility", idFacility);

                ProcessGroupProcess _process = (ProcessGroupProcess)EMSLibrary.User.ProcessFramework.Map.Process(idProcess);
                DateTime _startDate = _process.CurrentCampaignStartDate;
                DateTime _endDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());

                if (ManageEntityParams.ContainsKey("StartDate"))
                { ManageEntityParams.Remove("StartDate"); }
                ManageEntityParams.Add("StartDate", _startDate);

                if (ManageEntityParams.ContainsKey("EndDate"))
                { ManageEntityParams.Remove("EndDate"); }
                ManageEntityParams.Add("EndDate", _endDate);

                //Obtiene los indicadores
                LoadAllParameter();

                //Carga el DT
                BuildGenericDataTable(Common.ConstantsEntitiesName.RG.ChartBarActivityByScopeAndFacilityCL, ManageEntityParams);
                // Set a query to database.
                radChart.DataSource = DataTableListManage[Common.ConstantsEntitiesName.RG.ChartBarActivityByScopeAndFacilityCL];
                radChart.DataBind();

                Condesus.EMS.Business.GIS.Entities.Site _site = EMSLibrary.User.GeographicInformationSystem.Site(idFacility);
                // Set additional chart properties and settings.
                radChart.ChartTitle.TextBlock.Text = Resources.CommonListManage.ChartBarGasesActivityByScope + " " + EMSLibrary.User.PerformanceAssessments.Configuration.AccountingScope(1).LanguageOption.Name;
                radChart.ChartTitle.Visible = true;
                radChart.SeriesOrientation = ChartSeriesOrientation.Horizontal;
                for (int i = 0; i < radChart.Series.Count; i++)
                {
                    radChart.Series[i].Type = ChartSeriesType.StackedBar100;
                }

                radChart.Legend.Appearance.GroupNameFormat = "#VALUE";
            }
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

                #region Add Manage Entity Params
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
                #endregion
            }

            protected void chartBarActivityByScope1AndFacility_ItemDataBound(object sender, ChartItemDataBoundEventArgs e)
            {
                //Obtiene el activity y el value de los datos que vienen
                String _activity = e.SeriesItem.Parent.Name;
                String _value = Common.Functions.CustomEMSRound((Decimal)e.SeriesItem.YValue);
                e.SeriesItem.Parent.Type = ChartSeriesType.StackedBar100;
                //Setea en la leyenda y el tooltip con los datos.
                e.SeriesItem.ActiveRegion.Tooltip = "CO2e -> " + _activity + " = " + _value;
                e.SeriesItem.Name = _activity;
            }
            protected void chartBarActivityByScope1AndFacilityLC_ItemDataBound(object sender, ChartItemDataBoundEventArgs e)
            {
                //Obtiene el activity y el value de los datos que vienen
                String _activity = e.SeriesItem.Parent.Name;
                String _value = Common.Functions.CustomEMSRound((Decimal)e.SeriesItem.YValue);
                e.SeriesItem.Parent.Type = ChartSeriesType.StackedBar100;
                //Setea en la leyenda y el tooltip con los datos.
                e.SeriesItem.ActiveRegion.Tooltip = _activity + " = " + _value;
                e.SeriesItem.Name = _activity;
            }
            protected void charts_DataBound(object sender, EventArgs e)
            {
                HideLabels((RadChart)sender);
            }
            protected void HideLabels(RadChart radChart)
            {
                foreach (ChartSeries series in radChart.Series)
                {
                    series.Appearance.LabelAppearance.Visible = false;
                }
            }

        #endregion

        #region Internal Methods
            //private void InitializeProcessGroupProcess(Int64 idEntity)
            //{
            //    if (_ProcessRoot == null)
            //        _ProcessRoot = (Condesus.EMS.Business.PF.Entities.ProcessGroupProcess)EMSLibrary.User.ProcessFramework.Map.Process(idEntity);

            //    if (_ProcessRoot == null)
            //        throw new Exception("The page could not instantiate the Main Entity");

            //}
        #endregion

        #region External Methods
            public Panel BuildGraphicContent(String entityObject, Int64 idEntity)
            {
                _EntityObject = entityObject;
                Panel _pnlGraphicContent = new Panel();
                _pnlGraphicContent.ID = "pnlGraphicContent";

                switch (entityObject)
                {
                    case Common.ConstantsEntitiesName.PF.ProcessGroupProcess:
                    case Common.ConstantsEntitiesName.PF.Process:
                        _Params.Add("IdProcess", idEntity);
                        _pnlGraphicContent = BuildContentPhotos();
                        break;

                    case Common.ConstantsEntitiesName.DS.Facility:
                        _Params.Add("IdFacility", idEntity);
                        _pnlGraphicContent = BuildContentPhotos();
                        break;

                    case Common.ConstantsEntitiesName.DS.GeographicArea:
                        _Params.Add("IdGeographicArea", idEntity);
                        _pnlGraphicContent = BuildContentPhotos();
                        break;

                    case Common.ConstantsEntitiesName.PA.MeasurementDevice:
                        _Params.Add("IdMeasurementDevice", idEntity);
                        _pnlGraphicContent = BuildContentPhotos();
                        break;
                    default:
                        break;
                }

                return _pnlGraphicContent;
            }
            public void BuildGraphicContent(String entityObject, Int64 idEntity, Int64 idProcess, ref RadChart chtMeasurement, String report)
            {
                _EntityObject = entityObject;
                //_ChartMeasurement = chtMeasurement;

                switch (entityObject)
                {
                    case Common.ConstantsEntitiesName.PA.Measurement:
                        //LoadMeasurementChart(idEntity);
                        break;

                    case Common.ConstantsEntitiesName.DS.Facility:
                        if (report == "GEI")
                        {
                            LoadChartBarActivityByScope1AndFacility(chtMeasurement, idEntity, idProcess);
                        }
                        else
                        {
                            LoadChartBarActivityByScope1AndFacilityLC(chtMeasurement, idEntity, idProcess);
                        }
                        break;

                    default:
                        break;
                }
            }
        #endregion

    }
}
