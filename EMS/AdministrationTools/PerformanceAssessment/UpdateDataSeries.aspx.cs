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
using Condesus.EMS.WebUI.MasterControls;
using System.Text;
using System.IO;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Condesus.EMS.WebUI.AdministrationTools.PerformanceAssessment
{
    public partial class UpdateDataSeries : BaseProperties
    {
        #region Properties
            public RadGrid rgdMasterGrid;
            private Int64 _IdMeasurement
            {
                get
                {
                    Int64 _id = base.NavigatorContainsTransferVar("IdMeasurement") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdMeasurement")) : Convert.ToInt64(GetPKfromNavigator("IdMeasurement"));
                    //Si viene por otro lado, no trae el idmeasurement, y si el de process, entonces lo busco...
                    if (_id == 0)
                    {
                        if (_Measurement == null)
                        {
                            _id = base.NavigatorContainsTransferVar("IdTask") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdTask")) : Convert.ToInt64(GetPKfromNavigator("IdTask"));
                            _Measurement = ((ProcessTaskMeasurement)EMSLibrary.User.ProcessFramework.Map.Process(_id)).Measurement;
                        }

                        _id = Measurement.IdMeasurement;
                    }

                    return _id;
                }
            }
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
            private Measurement _Measurement = null;
            private Measurement Measurement
            {
                get
                {
                    if (_Measurement == null)
                    {
                        _Measurement = EMSLibrary.User.PerformanceAssessments.Configuration.Measurement(_IdMeasurement);
                    }

                    return _Measurement;
                }

                set { _Measurement = value; }
            }
        #endregion

        #region Load Information
            protected override void InitializeHandlers()
            {
                base.InitializeHandlers();

                //Le paso el Save a la MasterContentToolbar
                EventHandler _saveEventHandler = new EventHandler(btnSave_Click);
                FwMasterPage.ContentNavigatorToolbarFileActionAdd(_saveEventHandler, MasterFwContentToolbarAction.Save, true);
            }
            protected override void OnInit(EventArgs e)
            {
                base.OnInit(e);
                LoadTextLabels();
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
            }
            protected override void SetPagetitle()
            {
                base.PageTitle = Resources.CommonListManage.UpdateDataSeries;
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }
        #endregion

        #region Private Methods
            private void LoadData()
            {
                Measurement_LG _measurement_LG = Measurement.LanguageOption;
                MeasurementDevice _measurementDevice = Measurement.Device;
                MeasurementUnit _measurementUnit = Measurement.MeasurementUnit;

                lblMeasurementValue.Text = Common.Functions.ReplaceIndexesTags(_measurement_LG.Name);
                lblIndicatorValue.Text = Common.Functions.ReplaceIndexesTags(Measurement.Indicator.LanguageOption.Name);

                foreach (ParameterGroup _item in Measurement.ParameterGroups.Values)
                {
                    lblParameterValue.Text = _item.LanguageOption.Name + "<br/>";
                }

                if (_measurementDevice != null)
                {
                    lblDeviceValue.Text = String.Concat(_measurementDevice.Brand, " ", _measurementDevice.Model, " (", _measurementDevice.SerialNumber, ")");
                }
                else
                {
                    lblDeviceValue.Text = Resources.Common.NotUsed;
                }
                lblMeasurementUnitValue.Text = String.Concat(_measurementUnit.Magnitud.LanguageOption.Name, " - ", _measurementUnit.LanguageOption.Name);

                String _activity = Resources.CommonListManage.NoActivity;
                if (((ProcessTaskMeasurement)Measurement.ProcessTask).AccountingActivity != null)
                {
                    _activity = ((ProcessTaskMeasurement)Measurement.ProcessTask).AccountingActivity.LanguageOption.Name;
                }
                lblActivityValue.Text = _activity;
            }
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonListManage.IndicatorSeries;
                lblDevice.Text = Resources.CommonListManage.Device;
                lblIndicator.Text = Resources.CommonListManage.Indicator;
                lblMeasurement.Text = Resources.CommonListManage.Measurement;
                lblMeasurementUnit.Text = Resources.CommonListManage.MeasurementUnit;
                lblParameter.Text = Resources.CommonListManage.Parameter;
                lblActivity.Text = Resources.CommonListManage.AccountingActivity;
            }
            private DataTable BuildDataTableOfDataSeries()
            {
                DataTable _dt = new DataTable();
                _dt.TableName = "Root";
                _dt.Columns.Add("IdProcess", System.Type.GetType("System.Int64"));
                _dt.Columns.Add("IdExecution", System.Type.GetType("System.Int64"));
                _dt.Columns.Add("IdMeasurement", System.Type.GetType("System.Int64"));
                _dt.Columns.Add("IdExecutionMeasurement", System.Type.GetType("System.Int64"));
                _dt.Columns.Add("MeasurementDate", System.Type.GetType("System.String"));
                _dt.Columns.Add("MeasurementValue", System.Type.GetType("System.String"));
                _dt.Columns.Add("StartDate", System.Type.GetType("System.String"));
                _dt.Columns.Add("EndDate", System.Type.GetType("System.String"));
                _dt.Columns.Add("IsCumulative", System.Type.GetType("System.Boolean"));

                return _dt;
            }
            private DataTable BuildDataTableOfDataSeriesUpdated()
            {
                DataTable _dt = new DataTable();
                _dt.TableName = "Root";
                _dt.Columns.Add("IdProcess", System.Type.GetType("System.Int64"));
                _dt.Columns.Add("IdExecution", System.Type.GetType("System.Int64"));
                _dt.Columns.Add("IdMeasurement", System.Type.GetType("System.Int64"));
                _dt.Columns.Add("IdExecutionMeasurement", System.Type.GetType("System.Int64"));
                _dt.Columns.Add("MeasurementDate", System.Type.GetType("System.DateTime"));
                _dt.Columns.Add("MeasurementValue", System.Type.GetType("System.Double"));
                _dt.Columns.Add("StartDate", System.Type.GetType("System.DateTime"));
                _dt.Columns.Add("EndDate", System.Type.GetType("System.DateTime"));
                _dt.Columns.Add("IsCumulative", System.Type.GetType("System.Boolean"));

                return _dt;
            }
            private DataTable ReturnDataGrid()
            {
                //Crea el DataTable para luego devolverlo y asociarlo a la grilla.
                DataTable _dt = BuildDataTableOfDataSeries();

                Int64 _idMeasurement = _IdMeasurement;
                Boolean _isCummulative = Measurement.Indicator.IsCumulative;

                List<Condesus.EMS.Business.PA.Entities.MeasurementPoint> _measurementPoints = Measurement.Series();
                foreach (Condesus.EMS.Business.PA.Entities.MeasurementPoint _measurementPoint in _measurementPoints)
                {
                    _dt.Rows.Add(_measurementPoint.IdProcess,
                        _measurementPoint.IdExecution,
                        _idMeasurement,
                        _measurementPoint.IdExecutionMeasurement,
                        _measurementPoint.MeasureDate, 
                        _measurementPoint.MeasureValue,
                        _measurementPoint.StartDate,
                        _measurementPoint.EndDate,
                        _isCummulative);
                }

                //Setea el nombre de la entidad que se selecciona para ver la serie de datos
                lblEntitySeries.Text = Resources.CommonListManage.DataSeriesOf + " " + Common.Functions.ReplaceIndexesTags(Measurement.LanguageOption.Name);
                _dtDataSeries = _dt;

                return _dtDataSeries;
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


                //Crea y agrega las columnas de tipo Bound
                boundColumn = new GridBoundColumn();
                //boundColumn.HeaderStyle.Width = Unit.Pixel(150);
                boundColumn.DataField = "StartDate";
                boundColumn.DataType = System.Type.GetType("System.DateTime");
                boundColumn.HeaderText = Resources.CommonListManage.StartDate;
                boundColumn.ItemStyle.Width = Unit.Pixel(150);
                gridTableViewDetails.Columns.Add(boundColumn);


                //Crea y agrega las columnas de tipo Bound
                boundColumn = new GridBoundColumn();
                //boundColumn.HeaderStyle.Width = Unit.Pixel(150);
                boundColumn.DataField = "EndDate";
                boundColumn.DataType = System.Type.GetType("System.DateTime");
                boundColumn.HeaderText = Resources.CommonListManage.EndDate;
                boundColumn.ItemStyle.Width = Unit.Pixel(150);
                gridTableViewDetails.Columns.Add(boundColumn);

            }
            // Get data from the RadGrid and save it into the DataTable
            private DataTable saveDataInDataTable()
            {
                DataTable _dtAux = BuildDataTableOfDataSeriesUpdated();

                for (int i = 0; i < rdgDataSeries.MasterTableView.Items.Count; i++)
                {
                    GridDataItem item = rdgDataSeries.MasterTableView.Items[i];

                    Int64 _idProcess = Convert.ToInt64(((TextBox)item["IdProcess"].Controls[0]).Text);
                    Int64 _idExecution= Convert.ToInt64(((TextBox)item["IdExecution"].Controls[0]).Text);
                    Int64 _idMeasurement = Convert.ToInt64(((TextBox)item["IdMeasurement"].Controls[0]).Text);
                    Int64 _idExecutionMeasurement = Convert.ToInt64(((TextBox)item["IdExecutionMeasurement"].Controls[0]).Text);
                    DateTime _measurementDate = Convert.ToDateTime(((TextBox)item["MeasurementDate"].Controls[0]).Text);
                    Double _measurementValue = Convert.ToDouble(((TextBox)item["MeasurementValue"].Controls[0]).Text);
                    DateTime _startDate = Convert.ToDateTime(((TextBox)item["StartDate"].Controls[0]).Text);
                    DateTime _endDate = Convert.ToDateTime(((TextBox)item["EndDate"].Controls[0]).Text);
                    Boolean _isCumulative = ((CheckBox)item["IsCumulative"].Controls[0]).Checked;

                    _dtAux.Rows.Add(_idProcess, _idExecution, _idMeasurement, _idExecutionMeasurement, _measurementDate, _measurementValue, _startDate, _endDate, _isCumulative);
                }

                return _dtAux;
            }
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
                    }
                }
                protected void rdgDataSeries_ItemDataBound(object sender, GridItemEventArgs e)
                {
                    if (e.Item is GridHeaderItem)
                    {
                        GridHeaderItem headerItem = (GridHeaderItem)e.Item;
                        for (int i = 2; i < headerItem.Cells.Count; i++)
                        {
                            headerItem.Cells[i].Text = GetGlobalResourceObject("CommonListManage", headerItem.Cells[i].Text).ToString();
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

                                if ((column.UniqueName == "IdProcess")
                                    || (column.UniqueName == "IdExecution")
                                    || (column.UniqueName == "IdMeasurement")
                                    || (column.UniqueName == "IsCumulative")
                                    || (column.UniqueName == "IdExecutionMeasurement"))
                                {
                                    column.Display = false;
                                }
                                else
                                {
                                    if ((column.UniqueName == "MeasurementDate")
                                        || (column.UniqueName == "StartDate")
                                        || (column.UniqueName == "EndDate"))
                                    {
                                        //RadDatePicker _rdp = (RadDatePicker)item[column.UniqueName].Controls[0];
                                        //_rdp.Enabled = false;
                                        TextBox _tx = (TextBox)item[column.UniqueName].Controls[0];
                                        _tx.ReadOnly = true;
                                        _tx.Enabled = false;

                                    }
                                    else
                                    {
                                        TextBox textBox = (item[column.UniqueName].Controls[0]) as TextBox;
                                        if (textBox != null)
                                        {
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

                    //Ejecuta la actualizacion masiva de la serie de Datos.
                    Measurement.UpdateDataSeries(_dtDataSeries);

                    //Termino el proceso y sin errores, por una pagina para atras.
                    base.NavigateBack();

                    base.StatusBar.ShowMessage(Resources.Common.SaveOK);
                }
                catch (Exception ex)
                {
                    base.StatusBar.ShowMessage(ex);
                }
            }

        #endregion
    }
}
