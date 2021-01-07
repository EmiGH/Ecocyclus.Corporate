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
using Condesus.EMS.Business.PA.Entities;
using Condesus.EMS.Business.PF.Entities;
using Condesus.WebUI.Navigation;

namespace Condesus.EMS.WebUI.ManagementTools.PerformanceAssessment
{
    public partial class IndicatorSeries : BasePage
    {
        #region Properties
            public RadGrid rgdMasterGrid;
            private Int64 _IdMeasurement
            {
                get
                {
                    object o = ViewState["idMeasurement"];
                    if (o != null)
                    {
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
        #endregion

        #region Load Information
            protected override void OnInit(EventArgs e)
            {
                base.OnInit(e);
                LoadTextLabels();
                InitializeGrid();

            }
            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);
                base.SetContentTableRowsCss(tblContentForm);

                if (!Page.IsPostBack)
                {
                    base.SetNavigator();
                    LoadData();
                }

                LoadTreeList();
                SetCssInHtmlTable(tblTreeTableReport);
            }
            private void LoadData()
            {
                Condesus.EMS.Business.PA.Entities.Measurement _measurement = EMSLibrary.User.PerformanceAssessments.Configuration.Measurement(_IdMeasurement);
                Condesus.EMS.Business.PA.Entities.Measurement_LG _measurement_LG = _measurement.LanguageOption;
                Condesus.EMS.Business.PA.Entities.MeasurementDevice _measurementDevice = _measurement.Device;
                Condesus.EMS.Business.PA.Entities.MeasurementUnit _measurementUnit = _measurement.MeasurementUnit;


                this.Title = "EMS - " + Resources.CommonListManage.Measurements;

                lblMeasurementValue.Text = Common.Functions.ReplaceIndexesTags(_measurement_LG.Name);
                lblIndicatorValue.Text = Common.Functions.ReplaceIndexesTags(_measurement.Indicator.LanguageOption.Name);

                foreach (ParameterGroup _item in _measurement.ParameterGroups.Values)
                {
                    lblParameterValue.Text = _item.LanguageOption.Name + "<br />";
                }

                if (_measurementDevice != null)
                {
                    lblDeviceValue.Text = String.Concat(_measurementDevice.Brand, " ", _measurementDevice.Model, " (", _measurementDevice.SerialNumber, ")");
                }
                else
                {
                    lblDeviceValue.Text = Resources.Common.NotUsed; //HttpContext.GetLocalResourceObject("/ManagementTools/PerformanceAssessment/IndicatorSeries.aspx", "lblDeviceValue.Text").ToString();
                }
                lblMeasurementUnitValue.Text = String.Concat(_measurementUnit.Magnitud.LanguageOption.Name, " - ", _measurementUnit.LanguageOption.Name);

                String _activity = Resources.CommonListManage.NoActivity;
                if (((ProcessTaskMeasurement)_measurement.ProcessTask).AccountingActivity != null)
                {
                    _activity = ((ProcessTaskMeasurement)_measurement.ProcessTask).AccountingActivity.LanguageOption.Name;
                }
                lblActivityValue.Text = _activity;
            }
        #endregion

        #region Private Methods
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonListManage.IndicatorSeries;
                lblTitle.Text = Resources.CommonListManage.IndicatorSeries;
                lblDetail.Text = Resources.CommonListManage.IndicatorSeriesSubTitle;
                lblDevice.Text = Resources.CommonListManage.Device;
                lblIndicator.Text = Resources.CommonListManage.Indicator;
                lblMeasurement.Text = Resources.CommonListManage.Measurement;
                lblMeasurementUnit.Text = Resources.CommonListManage.MeasurementUnit;
                lblParameter.Text = Resources.CommonListManage.Parameter;
                lblActivity.Text = Resources.CommonListManage.AccountingActivity;
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
                //rgdMasterGrid.DetailTableDataBind += new GridDetailTableDataBindEventHandler(this.rgdMasterGrid_DetailTableDataBind);
                //rgdMasterGrid.ItemDataBound += new GridItemEventHandler(this.rgdMasterGrid_ItemDataBound);
                //rgdMasterGrid.ItemCommand += new GridCommandEventHandler(this.rgdMasterGrid_ItemCommand);
                //rgdMasterGrid.ItemCreated += new GridItemEventHandler(this.rgdMasterGrid_ItemCreated);
                rgdMasterGrid.SortCommand += new GridSortCommandEventHandler(this.rgdMasterGrid_SortCommand);
                rgdMasterGrid.PageIndexChanged += new GridPageChangedEventHandler(this.rgdMasterGrid_PageIndexChanged);
                rgdMasterGrid.ItemDataBound += new GridItemEventHandler(rgdMasterGrid_ItemDataBound);

                //Crea los metodos de la grilla (Cliente)
                //rgdMasterGrid.ClientSettings.ClientEvents.OnRowContextMenu = "RowContextMenu";
                //rgdMasterGrid.ClientSettings.ClientEvents.OnRowCreated = "RowCreated";
                //rgdMasterGrid.ClientSettings.ClientEvents.OnRowSelected = "RowSelected";
                rgdMasterGrid.ClientSettings.AllowExpandCollapse = true;
                //rgdMasterGrid.ClientSettings.EnableClientKeyValues = true;
                rgdMasterGrid.ClientSettings.Selecting.AllowRowSelect = true;
                rgdMasterGrid.ClientSettings.Selecting.EnableDragToSelectRows = false;

                //Define los atributos de la MasterGrid
                rgdMasterGrid.MasterTableView.Name = "gridMaster";
                //rgdMasterGrid.MasterTableView.DataKeyNames = new string[] { "MeasureDate" };
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
                _dt.Columns.Add("MeasureValue", System.Type.GetType("System.Decimal"));
                _dt.Columns.Add("StartDate", System.Type.GetType("System.DateTime"));
                _dt.Columns.Add("EndDate", System.Type.GetType("System.DateTime"));
                _dt.Columns.Add("Status", System.Type.GetType("System.String"));

                Measurement _measurement = EMSLibrary.User.PerformanceAssessments.Configuration.Measurement(_IdMeasurement);
                List<Condesus.EMS.Business.PA.Entities.MeasurementPoint> _measurementPoints = _measurement.Series();
                foreach (Condesus.EMS.Business.PA.Entities.MeasurementPoint _measurementPoint in _measurementPoints)
                {
                    _dt.Rows.Add(_measurementPoint.MeasureDate, 
                        Common.Functions.CustomEMSRound(_measurementPoint.MeasureValue),
                        _measurementPoint.StartDate.ToShortDateString(),
                        _measurementPoint.EndDate.ToShortDateString(),
                        _measurementPoint.Sing);
                }

                //Setea el nombre de la entidad que se selecciona para ver la serie de datos
                lblEntitySeries.Text = Resources.CommonListManage.DataSeriesOf + " " + Common.Functions.ReplaceIndexesTags(_measurement.LanguageOption.Name);

                return _dt;
            }
            private void DefineColumns(GridTableView gridTableViewDetails)
            {
                //Add columns bound
                GridBoundColumn boundColumn;

                //Crea y agrega las columnas de tipo Bound
                boundColumn = new GridBoundColumn();
                //boundColumn.HeaderStyle.Width = Unit.Pixel(150);
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
            }


            private void AddTableRow(Int64 id, Int64? idParent, String title, String indicator, String activity, String formula, String value, String measurementUnit, String valueType, String period,
                Int64 idMeasurement, Int64 idTask, Int64 idTransformation, Int64 idConstantClassification, Int64 idConstant, String rowType)
            {

                const String prefixNODE = "node_";
                const String prefixNODE_CHILD = "child-of-ctl00_ContentPopUp_node_";
                String _keyValues = String.Empty;

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

                ////Agrega llamada a un js
                //_tr.Attributes.Add("onclick", "javascript:click();");

                //Carga las columnas
                //Columna Titulo
                _lblCaption.Text = title;
                _td.Controls.Add(_lblCaption);
                _tr.Cells.Add(_td);

                //Columna Indicator
                _lblCaption = new Label();
                _lblCaption.Text = indicator;
                _td = new HtmlTableCell();
                _td.Attributes.Add("class", "DCGEI");
                _td.Controls.Add(_lblCaption);
                _tr.Cells.Add(_td);

                //Columna Indicator
                _lblCaption = new Label();
                _lblCaption.Text = activity;
                _td = new HtmlTableCell();
                _td.Attributes.Add("class", "DCGEI");
                _td.Controls.Add(_lblCaption);
                _tr.Cells.Add(_td);

                //Columna Formula
                _lblCaption = new Label();
                _lblCaption.Text = formula;
                _td = new HtmlTableCell();
                _td.Attributes.Add("class", "DCGEI");
                _td.Controls.Add(_lblCaption);
                _tr.Cells.Add(_td);

                //Columna Value
                _lblCaption = new Label();
                _lblCaption.Text = value == "0.00" ? "0" : value + " [" + measurementUnit + "]";
                _td = new HtmlTableCell();
                _td.Attributes.Add("class", "DCGEI");
                _td.Controls.Add(_lblCaption);
                _tr.Cells.Add(_td);

                if (rowType != "Constant")
                {
                    //Columna Series
                    HtmlInputImage _img = new HtmlInputImage();
                    _img.Src = "~/Skins/Images/Trans.gif";
                    _img.Name = "imgShowSeries";
                    _img.Attributes.Add("class", "DocumentGrid");
                    _img.Attributes.Add("onmouseover", "this.style.cursor = 'pointer'");
                    //_img.Attributes.Add("onclick", "$('#ctl00_ContentPopUp_tblTreeTableReport').expandAll(); return true;");
                    _img.Attributes.Add("runat", "Server");
                    _img.ServerClick += new ImageClickEventHandler(_img_ServerClick);
                    _img.Value = "IdMeasurement=" + idMeasurement + "&IdTransformation=" + idTransformation;
                    _td = new HtmlTableCell();
                    _td.Attributes.Add("class", "DCGEI");
                    _td.Controls.Add(_img);
                    _tr.Cells.Add(_td);

                    //Para las que no son Constantes, mostramos informacion en tooltip
                    //Agregamos como Tooltip el Type y el Period para los que tienen...
                    String _tooltip = String.Empty;
                    if (valueType != "-")
                    {
                        _tooltip = valueType;
                    }
                    if (period != " - ")
                    {
                        if (!String.IsNullOrEmpty(_tooltip))
                        {
                            _tooltip += " - " + Resources.CommonListManage.Period + ": " + period;
                        }
                        else
                        {
                            _tooltip += Resources.CommonListManage.Period + ": " + period;
                        }
                    }
                    if (!String.IsNullOrEmpty(_tooltip))
                    {
                        _tr.Attributes.Add("title", _tooltip);
                    }

                }
                else
                {
                    _td = new HtmlTableCell();
                    _td.Attributes.Add("class", "DCGEI");
                    _tr.Cells.Add(_td);

                    //Indica que es constante, por eso no muestra nada....
                    _tr.Attributes.Add("title", Resources.CommonListManage.Constant);
                }


                //Columna View
                HtmlInputImage _imgView = new HtmlInputImage();
                _imgView.Src = "~/Skins/Images/Trans.gif";
                _imgView.Name = "imgShowSeries";
                _imgView.Attributes.Add("class", "DocumentGrid");
                _imgView.Attributes.Add("onmouseover", "this.style.cursor = 'pointer'");
                _imgView.Attributes.Add("runat", "Server");
                //_imgView.ServerClick += new ImageClickEventHandler(_imgView_ServerClick);

                _keyValues = "Title=" + title
                    + "&IdTask=" + idTask.ToString()
                    + "&IdMeasurement=" + idMeasurement.ToString()
                    + "&IdTransformation=" + idTransformation.ToString()
                    + "&IdConstantClassification=" + idConstantClassification.ToString()
                    + "&IdConstant=" + idConstant.ToString()
                    + "&EntityName=" + rowType;

                _imgView.Value = _keyValues;

                //Solo para este caso, que esta dentro de un iFrame, se reemplaza el & por | (pipe)
                _imgView.Attributes.Add("PkCompost", _keyValues.Replace("&", "|"));
                _imgView.Attributes.Add("Text", title);
                _imgView.Attributes.Add("EntityName", rowType);
                _imgView.Attributes.Add("EntityNameGrid", String.Empty);
                _imgView.Attributes.Add("EntityNameContextInfo", String.Empty);
                _imgView.Attributes.Add("EntityNameContextElement", String.Empty);
                _imgView.Attributes.Add("onclick", "javascript:NavigateToContent(this, event);");

                _td = new HtmlTableCell();
                _td.Attributes.Add("class", "DCGEI");
                _td.Controls.Add(_imgView);
                _tr.Cells.Add(_td);

                //Insertamos el Registro en la Tabla
                tblTreeTableReport.Controls.Add(_tr);
            }

            
            private void LoadTreeList()
            {
                Int64 _id = 1;

                //Contruye la medicion Base
                Condesus.EMS.Business.PA.Entities.Measurement _measurement = EMSLibrary.User.PerformanceAssessments.Configuration.Measurement(_IdMeasurement);
                String _valueType;
                String _period;
                Double _measureValue = 0;
                DateTime? _firsDateSeries = null;   //Esto lo necesita para pasarlo al totalmeasurement. y esperar la primer fecha cuando es cumulative.
                Condesus.EMS.Business.PA.Entities.MeasurementPoint _measurementPoint = _measurement.TotalMeasurement(ref _firsDateSeries);
                if (_measurementPoint != null)
                {
                    _measureValue = _measurementPoint.MeasureValue;
                    //Cuando es Acumulativa, entonces se guarda la fecha la primer fecha de la medicion y la ultima.
                    if (_measurement.GetType().Name == Common.ConstantsEntitiesName.PA.MeasurementExtensive)
                    {
                        _valueType = Resources.CommonListManage.Cummulative;    // "Cummulative";
                        //_period = _measurementPoint.StartDate.ToShortDateString() + " - " + _measurementPoint.EndDate.ToShortDateString();
                    }
                    else
                    {
                        _valueType = Resources.CommonListManage.NonCummulative; // "Non Cummulative";
                        //_period = _measurementPoint.MeasureDate.ToShortDateString();  //Solo la ultima fecha
                    }
                    _period = _measurementPoint.StartDate.ToShortDateString() + " - " + _measurementPoint.EndDate.ToShortDateString();

                }
                else
                {
                    //como no hay datos, solo pongo el tipo.
                    _valueType = (_measurement.GetType().Name == Common.ConstantsEntitiesName.PA.MeasurementExtensive) ? Resources.CommonListManage.Cummulative : Resources.CommonListManage.NonCummulative;
                    _period = " - ";
                    _measureValue = 0;
                }
                String _indicatorName = Common.Functions.ReplaceIndexesTags(_measurement.Indicator.LanguageOption.Name);
                String _measurementUnit = Common.Functions.ReplaceIndexesTags(_measurement.MeasurementUnit.LanguageOption.Name);
                String _activity = Resources.CommonListManage.NoActivity;
                if (((ProcessTaskMeasurement)_measurement.ProcessTask).AccountingActivity != null)
                {
                    _activity = ((ProcessTaskMeasurement)_measurement.ProcessTask).AccountingActivity.LanguageOption.Name;
                }
                //Se carga el Dato Root de la medicion Base
                AddTableRow(_id, null, 
                    Common.Functions.ReplaceIndexesTags(_measurement.LanguageOption.Name),
                    _indicatorName,
                    _activity,
                    "-",
                    Common.Functions.CustomEMSRound(_measureValue),
                    _measurementUnit,
                    _valueType, 
                    _period,
                    _measurement.IdMeasurement,
                    _measurement.ProcessTask.IdProcess,
                    0,
                    0,
                    0,
                    "Measurement");


                Int64? _idParent = _id;
                Int64? _idTransformationParent = _id;
                _id++;

                //Empezamos con las Trasnformaciones...
                foreach (Condesus.EMS.Business.PA.Entities.CalculateOfTransformation _item in _measurement.Transformations.Values)
                {
                    _firsDateSeries = null;
                    //Por ahora lo comentamos, hasta que la dll funcione
                    Condesus.EMS.Business.PA.Entities.MeasurementPoint _measurementPointTransformation = _item.TotalMeasurement(ref _firsDateSeries);
                    if (_measurementPointTransformation != null)
                    {
                        _measureValue = _measurementPointTransformation.MeasureValue;
                        _period = _measurementPoint.StartDate.ToShortDateString() + " - " + _measurementPoint.EndDate.ToShortDateString();
                        //_period = _firsDateSeries.Value.ToShortDateString() + " - " + _measurementPointTransformation.MeasureDate.ToShortDateString();
                    }
                    else
                    {
                        //como no hay datos, solo pongo el tipo.
                        _period = " - ";
                        _measureValue = 0;
                    }
                    //Carga la transformacion
                    AddTableRow(_id, _idTransformationParent,
                        Common.Functions.ReplaceIndexesTags(_item.Name),
                        Common.Functions.ReplaceIndexesTags(_item.Indicator.LanguageOption.Name),
                        _item.Activity.LanguageOption.Name,
                        _item.Formula,
                        Common.Functions.CustomEMSRound(_measureValue),
                        Common.Functions.ReplaceIndexesTags(_item.MeasurementUnit.LanguageOption.Name),
                        "-",
                        _period,
                        _measurement.IdMeasurement,
                        _measurement.ProcessTask.IdProcess,
                        _item.IdTransformation,
                        0,
                        0,
                        "Transformation");

                    _idParent = _id;
                    _id++;

                    //HAy que cargar los operadores de la transformacion...
                    LoadParameters(String.Empty, _item, ref _id, _idParent);

                    //Ahora le pido los hijos de cada transformacion...
                    GetTransformationChild(_measurement.IdMeasurement, _item, ref _id, _idParent);

                }
            }
            private void LoadParameters(String prefix, Condesus.EMS.Business.PA.Entities.CalculateOfTransformation transformation, ref Int64 id, Int64? idTransformationParent)
            {
                //Carga el listBox y tambien el dictionary interno
                foreach (KeyValuePair<String, Condesus.EMS.Business.PA.Entities.CalculateOfTransformationParameter> _item in transformation.Parameters)
                {
                    String _idLetter = _item.Key;
                    String _operandName = _item.Value.Operand.Name;
                    String _operand = String.Empty;
                    String _keyValue = String.Empty;
                    RadComboBoxItem _rdcbItem;
                    //Verifica el tipo de objeto que viene en el parametro
                    switch (_item.Value.Operand.GetType().Name)
                    {
                        case Common.ConstantsEntitiesName.PA.Measurement:
                        case Common.ConstantsEntitiesName.PA.MeasurementExtensive:
                        case Common.ConstantsEntitiesName.PA.MeasurementIntensive:
                            //Arma el string que muestra el listBox y el KeyValue, para el dictionary interno
                            Measurement _measurement = (Measurement)_item.Value.Operand;
                            String _valueType;
                            String _period;
                            Double _measureValue = 0;
                            DateTime? _firsDateSeries = null;   //Esto lo necesita para pasarlo al totalmeasurement. y esperar la primer fecha cuando es cumulative.
                            Condesus.EMS.Business.PA.Entities.MeasurementPoint _measurementPoint = _measurement.TotalMeasurement(ref _firsDateSeries);
                            if (_measurementPoint != null)
                            {
                                _measureValue = _measurementPoint.MeasureValue;
                                //Cuando es Acumulativa, entonces se guarda la fecha la primer fecha de la medicion y la ultima.
                                if (_measurement.GetType().Name == Common.ConstantsEntitiesName.PA.MeasurementExtensive)
                                {
                                    _valueType = Resources.CommonListManage.Cummulative;    // "Cummulative";
                                    //_period = _firsDateSeries.Value.ToShortDateString() + " - " + _measurementPoint.MeasureDate.ToShortDateString();
                                }
                                else
                                {
                                    _valueType = Resources.CommonListManage.NonCummulative; // "Non Cummulative";
                                    //_period = _measurementPoint.MeasureDate.ToShortDateString();  //Solo la ultima fecha
                                }
                                _period = _measurementPoint.StartDate.ToShortDateString() + " - " + _measurementPoint.EndDate.ToShortDateString();
                            }
                            else
                            {
                                //como no hay datos, solo pongo el tipo.
                                _valueType = (_measurement.GetType().Name == Common.ConstantsEntitiesName.PA.MeasurementExtensive) ? Resources.CommonListManage.Cummulative : Resources.CommonListManage.NonCummulative;
                                _period = " - ";
                                _measureValue = 0;
                            }
                            String _indicatorName = Common.Functions.ReplaceIndexesTags(_measurement.Indicator.LanguageOption.Name);
                            String _measurementUnit = Common.Functions.ReplaceIndexesTags(_measurement.MeasurementUnit.LanguageOption.Name);
                            String _activity = Resources.CommonListManage.NoActivity;
                            if (((ProcessTaskMeasurement)_measurement.ProcessTask).AccountingActivity != null)
                            {
                                _activity = ((ProcessTaskMeasurement)_measurement.ProcessTask).AccountingActivity.LanguageOption.Name;
                            }
                            AddTableRow(id, idTransformationParent,
                                _idLetter + " = " + Common.Functions.ReplaceIndexesTags(_measurement.LanguageOption.Name),
                                _indicatorName,
                                _activity,
                                "-",
                                Common.Functions.CustomEMSRound(_measureValue),
                                _measurementUnit,
                                _valueType,
                                _period,
                                _measurement.IdMeasurement,
                                _measurement.ProcessTask.IdProcess,
                                0,
                                0,
                                0,
                                "Measurement");
                            
                            id++;
                            break;

                        case Common.ConstantsEntitiesName.PA.Constant:
                            //Arma el string que muestra el listBox y el KeyValue, para el dictionary interno
                            Constant _constant = ((Constant)_item.Value.Operand);
                            AddTableRow(id, idTransformationParent,
                                _idLetter + " = " + Common.Functions.ReplaceIndexesTags(_constant.LanguageOption.Name),
                                "-",
                                "-",
                                "-",
                                Common.Functions.CustomEMSRound(Convert.ToDecimal(_constant.Value)),
                                Common.Functions.ReplaceIndexesTags(_constant.MeasurementUnit.LanguageOption.Name),
                                "-",
                                "-",
                                0,
                                0,
                                0,
                                _constant.ConstantClassification.IdConstantClassification,
                                _constant.IdConstant,
                                "Constant");

                            id++;
                            break;

                        case Common.ConstantsEntitiesName.PA.Transformation:
                        case Common.ConstantsEntitiesName.PA.CalculateOfTransformation:
                            //Arma el string que muestra el listBox y el KeyValue, para el dictionary interno
                            CalculateOfTransformation _calculateOfTransformation = ((CalculateOfTransformation)_item.Value.Operand);
                            LoadParameters(_idLetter + " = ", _calculateOfTransformation, ref id, id);

                            id++;
                            break;
                    }
                }
            }
            private void GetTransformationChild(Int64 idMeasurement, Condesus.EMS.Business.PA.Entities.CalculateOfTransformation item, ref Int64 id, Int64? idTransformationParent)
            {
                String _period;
                Double _measureValue = 0;
                DateTime? _firsDateSeries = null;   //Esto lo necesita para pasarlo al totalmeasurement. y esperar la primer fecha cuando es cumulative.
                //Ahora por cada Transformacion, cargo sus hijos
                foreach (CalculateOfTransformation _calculateOfTransformation in item.Transformations.Values)
                {
                    _firsDateSeries = null;
                    //Lo comentamos hasta que funcione la dll
                    Condesus.EMS.Business.PA.Entities.MeasurementPoint _measurementPointTransformation = _calculateOfTransformation.TotalMeasurement(ref _firsDateSeries);
                    if (_measurementPointTransformation != null)
                    {
                        _measureValue = _measurementPointTransformation.MeasureValue;
                        //_period = _firsDateSeries.Value.ToShortDateString() + " - " + _measurementPointTransformation.MeasureDate.ToShortDateString();
                        _period = _measurementPointTransformation.StartDate.ToShortDateString() + " - " + _measurementPointTransformation.EndDate.ToShortDateString();
                    }
                    else
                    {
                        //como no hay datos, solo pongo el tipo.
                        _period = " - ";
                        _measureValue = 0;
                    }
                    AddTableRow(id, idTransformationParent,
                        Common.Functions.ReplaceIndexesTags(_calculateOfTransformation.Name),
                        Common.Functions.ReplaceIndexesTags(_calculateOfTransformation.Indicator.LanguageOption.Name),
                        _calculateOfTransformation.Activity.LanguageOption.Name,
                        _calculateOfTransformation.Formula,
                        Common.Functions.CustomEMSRound(_measureValue),
                        Common.Functions.ReplaceIndexesTags(_calculateOfTransformation.MeasurementUnit.LanguageOption.Name),
                        "-",
                        _period,
                        idMeasurement,
                        _calculateOfTransformation.MeasurementOrigin.ProcessTask.IdProcess,
                        _calculateOfTransformation.IdTransformation,
                        0,
                        0,
                        "Transformation");

                    //Actualiza el Parent, para los hijos...(los parametros sigue con el mismo que viene...)
                    Int64? _idParent = id;
                    id++;

                    //HAy que cargar los operadores de la transformacion...
                    LoadParameters(String.Empty, _calculateOfTransformation, ref id, _idParent);

                    //Ahora le pido los hijos de cada transformacion...
                    GetTransformationChild(idMeasurement, _calculateOfTransformation, ref id, _idParent);
                }
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
        
        #endregion

        #region Page Events
            protected void _img_ServerClick(object sender, ImageClickEventArgs e)
            {
                String _keyValues = ((HtmlInputImage)sender).Value;

                Int64 _idMeasurement = Convert.ToInt64(GetKeyValue(_keyValues, "IdMeasurement"));
                Int64 _idTransformation = Convert.ToInt64(GetKeyValue(_keyValues, "IdTransformation"));

                //Hace la recarga de la grilla con las series de datos...
                //Crea el DataTable para luego devolverlo y asociarlo a la grilla.
                DataTable _dt = new DataTable();
                _dt.TableName = "Root";
                _dt.Columns.Add("MeasureDate", System.Type.GetType("System.DateTime"));
                _dt.Columns.Add("MeasureValue", System.Type.GetType("System.String"));
                _dt.Columns.Add("StartDate", System.Type.GetType("System.DateTime"));
                _dt.Columns.Add("EndDate", System.Type.GetType("System.DateTime"));
                _dt.Columns.Add("Status", System.Type.GetType("System.String"));

                //Si no hay id de transformacion quiere decir que es una medicion...
                if (_idTransformation == 0)
                {
                    Measurement _measurement = EMSLibrary.User.PerformanceAssessments.Configuration.Measurement(_idMeasurement);
                    List<Condesus.EMS.Business.PA.Entities.MeasurementPoint> _measurementPoints = _measurement.Series();
                    foreach (Condesus.EMS.Business.PA.Entities.MeasurementPoint _measurementPoint in _measurementPoints)
                    {
                        _dt.Rows.Add(_measurementPoint.MeasureDate, 
                            Common.Functions.CustomEMSRound(_measurementPoint.MeasureValue),
                            _measurementPoint.StartDate.ToShortDateString(),
                            _measurementPoint.EndDate.ToShortDateString(),
                            _measurementPoint.Sing);
                    }

                    //Setea el nombre de la entidad que se selecciona para ver la serie de datos
                    lblEntitySeries.Text = Resources.CommonListManage.DataSeriesOf + " " + Common.Functions.ReplaceIndexesTags(_measurement.LanguageOption.Name);
                }
                else
                {
                    CalculateOfTransformation _calculateOfTransformation = EMSLibrary.User.PerformanceAssessments.Configuration.Measurement(_idMeasurement).Transformation(_idTransformation);
                    List<Condesus.EMS.Business.PA.Entities.MeasurementPoint> _measurementTransformationPoints = _calculateOfTransformation.Series();
                    foreach (Condesus.EMS.Business.PA.Entities.MeasurementPoint _measurementTransformationPoint in _measurementTransformationPoints)
                    {
                        _dt.Rows.Add(_measurementTransformationPoint.MeasureDate, 
                            Common.Functions.CustomEMSRound(_measurementTransformationPoint.MeasureValue),
                            _measurementTransformationPoint.StartDate.ToShortDateString(),
                            _measurementTransformationPoint.EndDate.ToShortDateString(),
                            false);
                    }

                    //Setea el nombre de la entidad que se selecciona para ver la serie de datos
                    lblEntitySeries.Text = Resources.CommonListManage.DataSeriesOf + " " + Common.Functions.ReplaceIndexesTags(_calculateOfTransformation.LanguageOption.Name);
                }
                rgdMasterGrid.DataSource = _dt;
                rgdMasterGrid.Rebind();
            }
            protected void _imgView_ServerClick(object sender, ImageClickEventArgs e)
            {
                String _keyValues = ((HtmlInputImage)sender).Value;

                Int64 _idMeasurement = Convert.ToInt64(GetKeyValue(_keyValues, "IdMeasurement"));
                Int64 _idTransformation = Convert.ToInt64(GetKeyValue(_keyValues, "IdTransformation"));
                Int64 _idConstantClassification = Convert.ToInt64(GetKeyValue(_keyValues, "IdConstantClassification"));
                Int64 _idConstant = Convert.ToInt64(GetKeyValue(_keyValues, "IdConstant"));
                String _entityName = GetKeyValue(_keyValues, "EntityName").ToString();
                String _title = GetKeyValue(_keyValues, "Title").ToString();

                FilterExpressionGrid = String.Empty;
                //Setea los parametros en el Navigate.

                //Aca vuelve a enviar el PKCompost, y ademas el DataKeyName de la grilla lo mete ahi, para que los use el GRC.
                String _pkCompost = "IdMeasurement=" + _idMeasurement.ToString()
                    + "&IdTransformation=" + _idTransformation.ToString()
                    + "&IdConstantClassification=" + _idConstantClassification.ToString()
                    + "&IdConstant=" + _idConstant.ToString();
                NavigatorAddPkEntityIdTransferVar("PkCompost", _pkCompost);

                base.NavigatorAddTransferVar("IdMeasurement", _idMeasurement);
                base.NavigatorAddTransferVar("IdTransformation", _idTransformation);
                base.NavigatorAddTransferVar("IdConstantClassification", _idConstantClassification);
                base.NavigatorAddTransferVar("IdConstant", _idConstant);

                _entityName = _entityName.Replace("_LG", String.Empty);
                base.NavigatorAddTransferVar("EntityName", _entityName);
                base.NavigatorAddTransferVar("EntityNameGrid", String.Empty);
                base.NavigatorAddTransferVar("EntityNameContextInfo", String.Empty);
                base.NavigatorAddTransferVar("EntityNameContextElement", String.Empty);

                NavigateEntity(GetPageViewerByEntity(_entityName), _entityName, _title, Resources.Common.PageSubtitleView , NavigateMenuAction.View);

                //Response.Redirect(GetPageViewerByEntity(_entityName), true);
                
            }

            #region RGDMasterGrid
                protected void rgdMasterGrid_SortCommand(object source, Telerik.Web.UI.GridSortCommandEventArgs e)
                {
                    //rgdMasterGrid.DataSource = ReturnDataGrid();
                    rgdMasterGrid.MasterTableView.Rebind();
                }
                protected void rgdMasterGrid_PageIndexChanged(object source, GridPageChangedEventArgs e)
                {
                    //rgdMasterGrid.DataSource = ReturnDataGrid();
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
