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
using System.IO;
using System.Linq;
using System.ComponentModel;
using System.Globalization;
using System.Threading;
using System.Drawing;
using Telerik.Charting;
using Condesus.EMS.WebUI.MasterControls;
using Condesus.EMS.Business.PF.Entities;
using Condesus.EMS.Business.PA.Entities;
using System.Text;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Condesus.EMS.WebUI.PM
{
    public partial class ProcessTaskExecutionMeasurementsProperties : BaseProperties
    {
        #region Internal Properties
            private Int64 _IdTask
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdTask") ? base.NavigatorGetTransferVar<Int64>("IdTask") : Convert.ToInt64(GetPKfromNavigator("IdTask"));
                }
            }
            private Int64 _IdExecution
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdExecution") ? base.NavigatorGetTransferVar<Int64>("IdExecution") : Convert.ToInt64(GetPKfromNavigator("IdExecution"));
                }
            }
            private ProcessTaskExecution _Entity = null;
            private ProcessTaskExecution Entity
            {
                get
                {
                    try
                    {
                        if (_Entity == null)
                        {
                            _Entity = ((Condesus.EMS.Business.PF.Entities.ProcessTask)EMSLibrary.User.ProcessFramework.Map.Process(_IdTask)).ProcessTaskExecution(_IdExecution);
                        }
                        return _Entity;
                    }
                    catch { return null; }
                }
                set { _Entity = value; }
            }
            private Measurement _Measurement = null;
            private Measurement Measurement 
            {
                get
                {
                    try
                    {
                        _Measurement = ((ProcessTaskMeasurement)EMSLibrary.User.ProcessFramework.Map.Process(_IdTask)).Measurement;
                        return _Measurement;
                    }
                    catch { return null; }
                }
                set { _Measurement = value; }
            }

            private RadGrid _rgdMasterGridListViewerMainData;
            private DataTable _dtDataSeries
            {
                get
                {
                    object _o = Session["dtDataSeries"];
                    if (_o != null)
                        return (DataTable)Session["dtDataSeries"];

                    return new DataTable();
                }

                set
                {
                    Session["dtDataSeries"] = value;
                }
            }
        #endregion

        #region PageLoad & Init
            protected override void InitializeHandlers()
            {
                base.InitializeHandlers();

                if ((Entity == null) || (Entity.GetType().Name != Common.ConstantsEntitiesName.PF.ProcessTaskExecutionMeasurement))
                {
                    //Le paso el Save a la MasterContentToolbar
                    EventHandler _saveEventHandler = new EventHandler(btnSave_Click);
                    //esta pagina, tiene un FileUpload, entonces el boton save, tiene que ser PostbackTrigger
                    FwMasterPage.ContentNavigatorToolbarFileActionAdd(_saveEventHandler, MasterFwContentToolbarAction.Save, false);
                }
            }
            protected override void OnInit(EventArgs e)
            {
                base.OnInit(e);
                LoadTextLabels();
            }
            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);

                if (!Page.IsPostBack)
                {
                    //Inicializo el Form
                    if (Entity == null)
                        { Add(); }
                    else
                        { LoadData(); }//Edit.

                    //Form
                    base.SetContentTableRowsCss(tblContentForm);
                    LoadMeasurementChartT();
                }

                LoadListViewerMainData();

            }
            protected override void SetPagetitle()
            {
                if (Entity != null)
                {
                    String _title = Entity.ProcessTask.LanguageOption.Title;
                    base.PageTitle = _title;
                }
                else
                {
                    base.PageTitle = Resources.CommonListManage.ProcessTaskExecutionMeasurement;
                }
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }
        #endregion

        #region Private Methods
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonListManage.ProcessTaskExecutionMeasurement;
                lblComment.Text = Resources.CommonListManage.Comment;
                lblLastMeasurementDate.Text = Resources.CommonListManage.LastMeasurementDate;
                lblLastMeasurementValueTitle.Text = Resources.CommonListManage.LastMeasurementValue;
                lblMeasurementAttach.Text = Resources.CommonListManage.MeasurementAttach;

                lblLastValue.Text = Resources.CommonListManage.LastValue;
                lblValidityPeriod.Text = Resources.CommonListManage.ValidityPeriod;
                lblValidityPeriod1.Text = Resources.CommonListManage.ValidityPeriod;
                lblMeasureValue.Text = Resources.CommonListManage.MeasureValue;

                lblLastMeasurementStartDate.Text = Resources.CommonListManage.StartDate;
                lblLastMeasurementEndDate.Text = Resources.CommonListManage.EndDate;
            }
            private void Add()
            {
                base.StatusBar.Clear();
                txtComment.Text = String.Empty;
            }
            private void LoadData()
            {
                txtComment.Text = Entity.Comment;
                //lblDateValue.Text = Entity.Date.ToString();
                //if (Entity.Post != null)
                //{
                //    lblPostValue.Text = Entity.Post.Person.LastName + ", " + Entity.Post.Person.FirstName + " - " + Entity.Post.JobTitle.Name();
                //}
                
                
                //if ((Entity.GetType().Name == Common.ConstantsEntitiesName.PF.ProcessTaskExecutionMeasurement) || (EMSLibrary.User.ProcessFramework.Map.Process(_IdTask).GetType().Name == Common.ConstantsEntitiesName.PF.ProcessTaskExecutionMeasurement))
                //{
                    //Condesus.EMS.Business.PF.Entities.ProcessTaskExecutionMeasurement _ptem = (Condesus.EMS.Business.PF.Entities.ProcessTaskExecutionMeasurement)Entity;
                    Condesus.EMS.Business.PF.Entities.ProcessTaskExecutionMeasurement _ptem = ((Condesus.EMS.Business.PF.Entities.ProcessTask)EMSLibrary.User.ProcessFramework.Map.Process(_IdTask)).ProcessTaskExecutionMeasurementLast();

                    if (_ptem != null)
                    {
                        lblLastMeasurementDateValue.Text = _ptem.MeasureDate.ToString();
                        lblLastMeasurementValue.Text = _ptem.MeasureValue.ToString();
                        lblLastMeasurementStartDateValue.Text = _ptem.MeasureStartDate.ToString();
                        lblLastMeasurementEndDateValue.Text = _ptem.MeasureEndDate.ToString();
                    }

                    //txtComment.ReadOnly = true;
                //}

                //Condesus.EMS.Business.PA.Entities.MeasurementDevice _measurementDevice = null;

                //if (Entity.ProcessTask.GetType().Name == "ProcessTaskDataRecovery")
                //{
                //    _measurementDevice = ((Condesus.EMS.Business.PF.Entities.ProcessTaskDataRecovery)Entity.ProcessTask).ProcessTaskMeasurementToRecovery.Measurement.Device;
                //}
                //else
                //{
                //    _measurementDevice = ((Condesus.EMS.Business.PF.Entities.ProcessTaskMeasurement)Entity.ProcessTask).Measurement.Device;
                //}

                //if (_measurementDevice != null)
                //{
                //    lblDeviceName.Text = _measurementDevice.Brand + " - " + _measurementDevice.Model + " - " + _measurementDevice.SerialNumber;
                //    lblDeviceStartDateValue.Text = _measurementDevice.CalibrationStartDate().ToString();
                //    lblDeviceEndDateValue.Text = _measurementDevice.CalibrationEndDate().ToString();
                //}
            }
            private void SetCalibrationMessageLabel()
            {
                base.StatusBar.ShowMessage(Resources.Common.NotCalibration);
            }

            private void LoadListViewerMainData()
            {
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                _params.Add("IdMeasurement", Measurement.IdMeasurement);
                //Setea los datos en el DataTable de base, para que luego se carge la grilla.
                BuildGenericDataTable(Common.ConstantsEntitiesName.PA.Measurement, _params);

                _rgdMasterGridListViewerMainData = base.BuildListViewerContent(Common.ConstantsEntitiesName.PA.Measurement);
                pnlListViewerMainData.Controls.Add(_rgdMasterGridListViewerMainData);

                base.InjectClientSelectRow(_rgdMasterGridListViewerMainData.ClientID);
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

            private void LoadMeasurementChartT()
            {
                //Seteo la cultura en USA, para poder trabajar con la fecha en MM/DD/YYYY HH:mm:ss
                CultureInfo _cultureUSA = new CultureInfo("en-US");
                //Me guarda la actual, para luego volver a esta...
                CultureInfo _currentCulture = CultureInfo.CurrentCulture;
                //Seta la cultura estandard
                Thread.CurrentThread.CurrentCulture = _cultureUSA;

                //Setea y arma una paleta de colores para las referencias
                SetChartCustomPalette(chartMeasurementSeries);

                List<MeasurementPoint> series = Measurement.Series();
                if (series.Count > 0)
                {
                    var _lnqMeasurementSeries = from s in series
                              select new
                                {
                                    date = s.MeasureDate.ToShortDateString(),
                                    value = s.MeasureValue
                                };

                    Int32 _interval = (Int32)(series.Count / 22);
                    _interval = (_interval == 0) ? 1 : _interval;

                    chartMeasurementSeries.PlotArea.XAxis.MaxValue = _interval;
                    chartMeasurementSeries.PlotArea.XAxis.MinValue = _interval;
                    chartMeasurementSeries.PlotArea.XAxis.Step = _interval;
                    chartMeasurementSeries.PlotArea.XAxis.LabelStep = _interval;    // 6;

                    // Set a query to database.
                    chartMeasurementSeries.DataSource = _lnqMeasurementSeries;
                    chartMeasurementSeries.DataBind();

                    // Set additional chart properties and settings.
                    chartMeasurementSeries.ChartTitle.TextBlock.Text = Resources.Common.DataSeries;
                    chartMeasurementSeries.ChartTitle.Visible = true;
                    chartMeasurementSeries.SeriesOrientation = ChartSeriesOrientation.Vertical;
                    for (int i = 0; i < chartMeasurementSeries.Series.Count; i++)
                    {
                        chartMeasurementSeries.Series[i].Type = ChartSeriesType.Spline;
                        chartMeasurementSeries.Series[i].Appearance.LegendDisplayMode = ChartSeriesLegendDisplayMode.Nothing;
                    }

                    chartMeasurementSeries.Legend.Appearance.GroupNameFormat = "#VALUE";

                    chartMeasurementSeries.Legend.Visible = false;
                    chartMeasurementSeries.Appearance.Visible = false;
                }

                //Vuelve a la cultura original...
                Thread.CurrentThread.CurrentCulture = _currentCulture;
            }
            protected void chartMeasurementSeries_ItemDataBound(object sender, ChartItemDataBoundEventArgs e)
            {
                ////Obtiene el activity y el value de los datos que vienen
                ////String _measurementDate = e.SeriesItem.Parent.Name;
                //String _value = Common.Functions.CustomEMSRound((Decimal)e.SeriesItem.YValue);
                //e.SeriesItem.Parent.Type = ChartSeriesType.Spline;
                ////Setea en la leyenda y el tooltip con los datos.
                //e.SeriesItem.ActiveRegion.Tooltip = _value;
                ////e.SeriesItem.Name = _measurementDate;
                e.SeriesItem.Label.Visible = false;
            }

            #region RadGrid Excel
                private DataTable BuildDataTableOfDataSeries()
                {
                    DataTable _dt = new DataTable();
                    _dt.TableName = "Root";
                    _dt.Columns.Add("MeasurementDate", System.Type.GetType("System.String"));
                    _dt.Columns.Add("MeasurementValue", System.Type.GetType("System.String"));
                    _dt.Columns.Add("StartDate", System.Type.GetType("System.String"));
                    _dt.Columns.Add("EndDate", System.Type.GetType("System.String"));

                    return _dt;
                }
                private DataTable BuildDataTableOfDataSeriesUpdated()
                {
                    DataTable _dt = new DataTable();
                    _dt.TableName = "Root";
                    _dt.Columns.Add("MeasurementDate", System.Type.GetType("System.DateTime"));
                    _dt.Columns.Add("MeasurementValue", System.Type.GetType("System.String"));
                    _dt.Columns.Add("StartDate", System.Type.GetType("System.DateTime"));
                    _dt.Columns.Add("EndDate", System.Type.GetType("System.DateTime"));

                    return _dt;
                }
                private DataTable ReturnDataGrid()
                {
                    //Crea el DataTable para luego devolverlo y asociarlo a la grilla.
                    DataTable _dt = BuildDataTableOfDataSeries();

                    //ProcessTaskMeasurement _processTaskMeasurement = ((Condesus.EMS.Business.PF.Entities.ProcessTaskMeasurement)EMSLibrary.User.ProcessFramework.Map.Process(_IdTask));

                    DateTime _nextDateExecution = Common.Functions.CalculateNextDate(Entity.ProcessTask.TimeUnitInterval, Entity.ProcessTask.Interval, Entity.Date);
                    DateTime _startDate = Entity.Date;
                    DateTime _endDate = DateTime.MinValue;

                    //Si la medicion es Regresiva, entonces la fecha de la Ejecucion es la Final
                    if (_Measurement.IsRegressive)
                    {
                        _endDate = Entity.Date;
                        _startDate = DateTime.MinValue;

                        while (_endDate < _nextDateExecution)
                        {
                            //Al ser Regresiva, tengo que ir para atras...y la fecha de la medicion es la misma que la fecha Fin
                            _startDate = Common.Functions.CalculateNextDate(_Measurement.TimeUnitFrequency, -(_Measurement.Frequency), _endDate);

                            _dt.Rows.Add(_endDate, String.Empty, _startDate, _endDate);

                            _endDate = Common.Functions.CalculateNextDate(_Measurement.TimeUnitFrequency, _Measurement.Frequency, _endDate);
                        }
                    }
                    else
                    {
                        while (_endDate < _nextDateExecution)
                        {
                            _endDate = Common.Functions.CalculateNextDate(_Measurement.TimeUnitFrequency, _Measurement.Frequency, _startDate);

                            _dt.Rows.Add(_startDate, String.Empty, _startDate, _endDate);

                            _startDate = _endDate;
                        }
                    }


                    _dtDataSeries = _dt;

                    return _dtDataSeries;
                }
                //private void DefineColumns(GridTableView gridTableViewDetails)
                //{
                //    //Add columns bound
                //    GridBoundColumn boundColumn;

                //    //Crea y agrega las columnas de tipo Bound
                //    boundColumn = new GridBoundColumn();
                //    //boundColumn.HeaderStyle.Width = Unit.Pixel(150);
                //    boundColumn.DataField = "Date";
                //    boundColumn.DataType = System.Type.GetType("System.DateTime");
                //    boundColumn.HeaderText = Resources.CommonListManage.MeasurementDate;
                //    boundColumn.ItemStyle.Width = Unit.Pixel(150);
                //    gridTableViewDetails.Columns.Add(boundColumn);

                //    //Crea y agrega las columnas de tipo Bound
                //    boundColumn = new GridBoundColumn();
                //    boundColumn.DataField = "Value";
                //    boundColumn.DataType = System.Type.GetType("System.String");
                //    boundColumn.HeaderText = Resources.CommonListManage.Value + " [" + _Measurement.MeasurementUnit.LanguageOption.Name + "]";
                //    boundColumn.ItemStyle.Width = Unit.Pixel(150);
                //    gridTableViewDetails.Columns.Add(boundColumn);

                //    //Crea y agrega las columnas de tipo Bound
                //    boundColumn = new GridBoundColumn();
                //    //boundColumn.HeaderStyle.Width = Unit.Pixel(150);
                //    boundColumn.DataField = "StartDate";
                //    boundColumn.DataType = System.Type.GetType("System.DateTime");
                //    boundColumn.HeaderText = Resources.CommonListManage.StartDate;
                //    boundColumn.ItemStyle.Width = Unit.Pixel(150);
                //    gridTableViewDetails.Columns.Add(boundColumn);

                //    //Crea y agrega las columnas de tipo Bound
                //    boundColumn = new GridBoundColumn();
                //    //boundColumn.HeaderStyle.Width = Unit.Pixel(150);
                //    boundColumn.DataField = "EndDate";
                //    boundColumn.DataType = System.Type.GetType("System.DateTime");
                //    boundColumn.HeaderText = Resources.CommonListManage.EndDate;
                //    boundColumn.ItemStyle.Width = Unit.Pixel(150);
                //    gridTableViewDetails.Columns.Add(boundColumn);
                //}
                // Get data from the RadGrid and save it into the DataTable
                private DataTable saveDataInDataTable()
                {
                    DataTable _dtAux = BuildDataTableOfDataSeriesUpdated();

                    Boolean _isEmpty = false;
                    for (int i = 0; i < rdgDataSeries.MasterTableView.Items.Count; i++)
                    {
                        GridDataItem item = rdgDataSeries.MasterTableView.Items[i];

                        //Si al menos hay un dato vacio...corto todo y devuelvo un DT vacio!
                        if (String.IsNullOrEmpty(((TextBox)item["MeasurementValue"].Controls[0]).Text))
                        {
                            _isEmpty = true;
                            return new DataTable();
                        }
                        DateTime _measurementDate = Convert.ToDateTime(((TextBox)item["MeasurementDate"].Controls[0]).Text);
                        Double _measurementValue = Convert.ToDouble(((TextBox)item["MeasurementValue"].Controls[0]).Text);
                        DateTime _startDate = Convert.ToDateTime(((TextBox)item["StartDate"].Controls[0]).Text);
                        DateTime _endDate = Convert.ToDateTime(((TextBox)item["EndDate"].Controls[0]).Text);

                        _dtAux.Rows.Add(_measurementDate, _measurementValue, _startDate, _endDate);
                    }

                    //Si al menos hay un hueco... entonces devuelve vacio!!
                    if (_isEmpty)
                    {
                        _dtAux = new DataTable();
                    }

                    return _dtAux;
                }
            #endregion
        #endregion

        #region Page Events
            #region RadGrid Excel
                protected void rdgDataSeries_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
                {
                    rdgDataSeries.DataSource = ReturnDataGrid(); // GridSource;
                }
                protected void rdgDataSeries_DataBinding(object sender, EventArgs e)
                {
                    for (int i = 0; i < _dtDataSeries.Rows.Count; i++)
                    {
                        rdgDataSeries.EditIndexes.Add(i);
                        rdgDataSeries.Focus();
                    }
                }
                protected void rdgDataSeries_ItemDataBound(object sender, GridItemEventArgs e)
                {
                    if (e.Item is GridHeaderItem)
                    {
                        GridHeaderItem headerItem = (GridHeaderItem)e.Item;
                        for (int i = 2; i < headerItem.Cells.Count; i++)
                        {
                            if (headerItem.Cells[i].Text == "MeasurementDate")
                            {
                                headerItem.Cells[i].Text = Resources.CommonListManage.MeasurementDate;
                            }
                            if (headerItem.Cells[i].Text == "MeasurementValue")
                            {
                                headerItem.Cells[i].Text = Resources.CommonListManage.Value + " [" + _Measurement.MeasurementUnit.LanguageOption.Name + "]";
                            }
                            if (headerItem.Cells[i].Text == "StartDate")
                            {
                                headerItem.Cells[i].Text = Resources.CommonListManage.StartDate;
                            }
                            if (headerItem.Cells[i].Text == "EndDate")
                            {
                                headerItem.Cells[i].Text = Resources.CommonListManage.EndDate;
                            }
                            //headerItem.Cells[i].Text = GetGlobalResourceObject("CommonListManage", headerItem.Cells[i].Text).ToString();
                        }
                    }
                }
                protected void rdgDataSeries_PreRender(object sender, EventArgs e)
                {
                    rdgDataSeries.Attributes.Add("onkeydown", "onKeyDown(this,event);");
                    int itemsCount = 0;
                    int columnsCount = 0;
                    StringBuilder builder = new StringBuilder();
                    // Attach the event handlers to the client side events of the TextBoxes. 
                    foreach (GridDataItem item in rdgDataSeries.MasterTableView.Items)
                    {
                        if (item is GridDataItem)
                        {
                            columnsCount = 0;
                            for (int i = 2; i < rdgDataSeries.MasterTableView.RenderColumns.Length; i++)
                            {
                                GridColumn column = rdgDataSeries.MasterTableView.RenderColumns[i];
                                column.HeaderText = GetGlobalResourceObject("CommonListManage", column.UniqueName).ToString();

                                if (_Measurement.Indicator.IsCumulative)
                                {
                                    if ((column.UniqueName == "MeasurementDate")
                                        || (column.UniqueName == "StartDate")
                                        || (column.UniqueName == "EndDate"))
                                    {
                                        TextBox _tx = (TextBox)item[column.UniqueName].Controls[0];
                                        _tx.ReadOnly = true;
                                        _tx.Enabled = false;
                                    }
                                    else
                                    {
                                        TextBox textBox = (item[column.UniqueName].Controls[0]) as TextBox;
                                        if (textBox != null)
                                        {
                                            //Se para solo en el Primer registro y en la columna de valor
                                            if ((i==3) && (column.UniqueName == "MeasurementValue"))
                                            {
                                                textBox.Focus();
                                                //textBox.CssClass = "doubleSelect";
                                            }

                                            textBox.Attributes.Add("ondblclick", "cellDoubleClickFunction('" + textBox.ClientID + "');");
                                            textBox.Attributes.Add("onclick", "cellClick('" + textBox.ClientID + "');");
                                        }
                                    }
                                }
                                else
                                {
                                    if ((column.UniqueName == "StartDate")
                                        || (column.UniqueName == "EndDate"))
                                    {
                                        TextBox _tx = (TextBox)item[column.UniqueName].Controls[0];
                                        _tx.ReadOnly = true;
                                        _tx.Enabled = false;
                                    }
                                    else
                                    {
                                        TextBox textBox = (item[column.UniqueName].Controls[0]) as TextBox;
                                        if (textBox != null)
                                        {
                                            //Se para solo en el Primer registro y en la columna de valor
                                            if ((i == 3) && (column.UniqueName == "MeasurementValue"))
                                            {
                                                textBox.Focus();
                                                //textBox.CssClass = "doubleSelect";
                                            }


                                            textBox.Attributes.Add("ondblclick", "cellDoubleClickFunction('" + textBox.ClientID + "');");
                                            textBox.Attributes.Add("onclick", "cellClick('" + textBox.ClientID + "');");
                                        }
                                    }

                                }

                                columnsCount++;
                            }
                            itemsCount++;
                        }
                    }
                    RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "init", "colls = " + columnsCount + ";rows=" + itemsCount + ";", true);
                }
            #endregion

            protected void btnSave_Click(object sender, EventArgs e)
            {
                try
                {
                    //Se guarda toda la grilla modificada en un DataTable
                    DataTable _dtDataSeries = saveDataInDataTable();

                    if (_dtDataSeries.Rows.Count > 0)
                    {
                        Stream _fileStream;

                        //Seteo la cultura en USA, para poder trabajar con la fecha en MM/DD/YYYY HH:mm:ss
                        CultureInfo _cultureUSA = new CultureInfo("en-US");
                        //Me guarda la actual, para luego volver a esta...
                        CultureInfo _currentCulture = CultureInfo.CurrentCulture;
                        //Seta la cultura estandard
                        Thread.CurrentThread.CurrentCulture = _cultureUSA;

                        //Se trae el primer post que tiene asociado esta persona. por eso utiliza el "[0]".
                        Condesus.EMS.Business.DS.Entities.Post _post = EMSLibrary.User.Person.Posts.First();

                        Condesus.EMS.Business.PA.Entities.MeasurementDevice _measurementDevice = null;
                        Condesus.EMS.Business.PA.Entities.MeasurementUnit _measurementUnit = null;

                        if (EMSLibrary.User.ProcessFramework.Map.Process(_IdTask).GetType().Name == "ProcessTaskDataRecovery")
                        {
                            _measurementDevice = ((Condesus.EMS.Business.PF.Entities.ProcessTaskDataRecovery)EMSLibrary.User.ProcessFramework.Map.Process(_IdTask)).ProcessTaskMeasurementToRecovery.Measurement.Device;
                            _measurementUnit = ((Condesus.EMS.Business.PF.Entities.ProcessTaskDataRecovery)EMSLibrary.User.ProcessFramework.Map.Process(_IdTask)).ProcessTaskMeasurementToRecovery.Measurement.MeasurementUnit;
                        }
                        else
                        {
                            _measurementDevice = ((Condesus.EMS.Business.PF.Entities.ProcessTaskMeasurement)EMSLibrary.User.ProcessFramework.Map.Process(_IdTask)).Measurement.Device;
                            _measurementUnit = ((Condesus.EMS.Business.PF.Entities.ProcessTaskMeasurement)EMSLibrary.User.ProcessFramework.Map.Process(_IdTask)).Measurement.MeasurementUnit;
                        }

                        //Esto es para el Attach, que va por separado al archivo de interfaz!
                        String _comment = txtComment.Text;
                        Stream _fileStreamAttach;
                        Byte[] _fileMeasurementAttachBinary = null;
                        if (fuMeasurementAttach.HasFile)
                        {
                            String _fileNameMeasurementAttach = fuMeasurementAttach.FileName;

                            _comment = _comment + "\n\r|FileName=" + _fileNameMeasurementAttach;

                            _fileStreamAttach = fuMeasurementAttach.FileContent;

                            //Obtiene el texto
                            StreamReader _streamReaderFileMeasurementAttach = new StreamReader(_fileStreamAttach);
                            String _fileMeasurementAttach = _streamReaderFileMeasurementAttach.ReadToEnd();

                            //Obtiene el binario
                            _fileMeasurementAttachBinary = fuMeasurementAttach.FileBytes;
                            _fileStreamAttach.Read(_fileMeasurementAttachBinary, 0, Convert.ToInt32(_fileStreamAttach.Length));
                        }

                        //Verifica si esta configurado, para enviar notificacion siempre que se ejecuta...
                        Boolean _sendNotificationMeasurementExecution = false;
                        if (ConfigurationManager.AppSettings["SendNotificationMeasurementExecution"] != null)
                        {
                            _sendNotificationMeasurementExecution = Convert.ToBoolean(ConfigurationManager.AppSettings["SendNotificationMeasurementExecution"].ToString());
                        }

                        if (Entity == null)
                        {
                            //Es un ADD
                            DateTime dateExecution = DateTime.Now;
                            //Esto seria una Spontanea???, bueno igual por ahora damian no agrego el DataTable...
                            //Entity = ((Condesus.EMS.Business.PF.Entities.ProcessTaskMeasurement)EMSLibrary.User.ProcessFramework.Map.Process(_IdTask)).ProcessTaskExecutionsAdd(_post, dateExecution, _comment, _fileMeasurementAttachBinary, Convert.ToDouble(txtMeasurementValue.Text), Convert.ToDateTime(rdtMeasurementDate.SelectedDate), _measurementDevice, _measurementUnit, _sendNotificationMeasurementExecution);
                        }
                        else
                        {
                            //La ejecucion madre ya existe, debe ingresar la ejecucion especifica, ExecutionMeasurements
                            DateTime dateExecution = DateTime.Now;

                            //Sin archivo...
                            ((Condesus.EMS.Business.PF.Entities.ProcessTaskMeasurement)EMSLibrary.User.ProcessFramework.Map.Process(_IdTask)).ProcessTaskExecutionsAddTVP(_post, Entity, _comment, _fileMeasurementAttachBinary, _dtDataSeries, _measurementDevice, _measurementUnit, false, _sendNotificationMeasurementExecution);
                        }

                        //Vuelve a la cultura original...
                        Thread.CurrentThread.CurrentCulture = _currentCulture;


                        base.NavigatorAddTransferVar("IdProcess", Entity.ProcessTask.IdProcess);
                        base.NavigatorAddTransferVar("IdExecution", Entity.IdExecution);

                        String _pkValues = "IdProcess=" + Entity.ProcessTask.IdProcess.ToString()
                            + "& IdExecution=" + Entity.IdExecution.ToString();
                        base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);
                        base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.PF.ProcessTaskExecution);
                        base.NavigatorAddTransferVar("EntityNameContextInfo", Common.ConstantsEntitiesName.PF.ProcessTaskMeasurement);
                        base.NavigatorAddTransferVar("EntityNameContextElement", Common.ConstantsEntitiesName.PF.ProcessGroupProcess);
                        String _entityPropertyName = String.Concat(Entity.ProcessTask.LanguageOption.Title);
                        NavigatePropertyEntity(Common.ConstantsEntitiesName.PF.ProcessTaskExecution, _entityPropertyName, Condesus.WebUI.Navigation.NavigateMenuAction.Add);

                        base.StatusBar.ShowMessage(Resources.Common.SaveOK);
                    }
                    else
                    {
                        base.StatusBar.ShowMessage(Resources.ConstantMessage.DataSeriesIncomplete, Pnyx.WebControls.PnyxStatusBar.StatusState.Warning);
                    }
                }
                catch (Exception ex)
                {
                    base.StatusBar.ShowMessage(ex);
                }

            }
        #endregion
    }
}