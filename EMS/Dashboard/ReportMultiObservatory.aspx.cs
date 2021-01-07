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
using Telerik.Charting;
using Condesus.EMS.WebUI.ManagementTools.ProcessesMap;
using System.Linq;
using System.Globalization;
using System.Threading;
using Condesus.EMS.WebUI.MasterControls;
using Condesus.EMS.Business.PF.Entities;
using Condesus.EMS.Business.PA.Entities;
using Condesus.EMS.Business.NT.Entities;
using Condesus.EMS.Business.DS.Entities;
using System.Text;


namespace Condesus.EMS.WebUI
{
    public partial class ReportMultiObservatory : BaseProperties
    {
        #region Internal Properties
            private RadGrid _RgdMasterGridMultiObservatory;
            private String _EntityNameGRC = String.Empty;
        #endregion

        #region PageLoad & Init
            protected override void InitializeHandlers()
            {
                base.InitializeHandlers();
                btnList.Click += new EventHandler(btnList_Click);
            }

            
            protected override void OnPreInit(EventArgs e)
            {
                base.OnPreInit(e);

                //Registro Mis Custom MenuPanels
                RegisterCustomMenuPanels();
            }
            protected override void OnInit(EventArgs e)
            {
                base.OnInit(e);
                LoadTextLabels();
                InitListViewerGrid();
            }
            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);

                if (!Page.IsPostBack)
                {   //Form
                    base.SetContentTableRowsCss(tblContentForm);
                }

                //if (ManageEntityParams.ContainsKey("StartDate"))
                //{ ManageEntityParams.Remove("StartDate"); }
                //ManageEntityParams.Add("StartDate", rdtFrom.SelectedDate);
                //if (ManageEntityParams.ContainsKey("EndDate"))
                //{ ManageEntityParams.Remove("EndDate"); }
                //ManageEntityParams.Add("EndDate", rdtThrough.SelectedDate);

                //Carga la Grilla
                //LoadGridMultiObservatory();
            }
            protected override void SetPagetitle()
            {
                try
                {
                    base.PageTitle = Resources.CommonListManage.MultiObservatory;
                }
                catch { base.PageTitle = String.Empty; }
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }
        #endregion

        #region Private Methods
            private void LoadTextLabels()
            {
                lblFrom.Text = Resources.CommonListManage.From;
                lblThrough.Text = Resources.CommonListManage.Through;
            }
            private void LoadGridMultiObservatory()
            {
                try
                {
                    phGridMultiObservatory.Controls.Clear();

                    //if (_params.ContainsKey("StartDate"))
                    //{ _params.Remove("StartDate"); }
                    //_params.Add("StartDate", rdtFrom.SelectedDate);

                    //if (_params.ContainsKey("EndDate"))
                    //{ _params.Remove("EndDate"); }
                    //_params.Add("EndDate", rdtThrough.SelectedDate);

                    //DateTime _startDate = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;
                    //DateTime _endDate = (DateTime)System.Data.SqlTypes.SqlDateTime.MaxValue;

                    //Seteo la cultura en USA, para poder trabajar con la fecha en MM/DD/YYYY HH:mm:ss
                    CultureInfo _cultureUSA = new CultureInfo("en-US");
                    //Me guarda la actual, para luego volver a esta...
                    CultureInfo _currentCulture = CultureInfo.CurrentCulture;
                    //Seta la cultura estandard
                    Thread.CurrentThread.CurrentCulture = _cultureUSA;

                    DateTime _startDate = DateTime.MinValue;
                    DateTime _endDate = DateTime.MaxValue;

                    if (rdtFrom.SelectedDate.HasValue)
                    { _startDate = rdtFrom.SelectedDate.Value; }
                    if (rdtThrough.SelectedDate.HasValue)
                    { _endDate = rdtThrough.SelectedDate.Value; }


                    //BuildGenericDataTable(Common.ConstantsEntitiesName.RG.ReportIndicatorTracker, _params);

                    //_RgdMasterGridMultiObservatory = base.BuildListViewerContent(Common.ConstantsEntitiesName.RG.ReportIndicatorTracker);

                    DataTable _dtvpProcess = new DataTable();
                    _dtvpProcess.Columns.Add("IdProcess", System.Type.GetType("System.Int64"));

                    String _idProcessesFilter = String.Empty;
                    foreach (ProcessGroupProcess _processGroupProcess in EMSLibrary.User.Dashboard.ProcessGroupProcess.Values)
                    {
                        _idProcessesFilter += _processGroupProcess.IdProcess.ToString() + ",";
                        _dtvpProcess.Rows.Add(_processGroupProcess.IdProcess);
                    }

                    if (!String.IsNullOrEmpty(_idProcessesFilter))
                    {
                        _idProcessesFilter = _idProcessesFilter.Substring(0, _idProcessesFilter.Length - 1);
                    }


                    _RgdMasterGridMultiObservatory.DataSource = EMSLibrary.User.Report_MultiObservatory(_dtvpProcess, _startDate, _endDate);
                    _RgdMasterGridMultiObservatory.Rebind();
                    _RgdMasterGridMultiObservatory.MasterTableView.Rebind();

                    phGridMultiObservatory.Controls.Add(_RgdMasterGridMultiObservatory);

                    //Vuelve a la cultura original...
                    Thread.CurrentThread.CurrentCulture = _currentCulture;
                }
                catch (Exception ex)
                {
                    base.StatusBar.ShowMessage(ex);
                }
            }
            private void InitListViewerGrid()
            {
                _RgdMasterGridMultiObservatory = new RadGrid();
                _RgdMasterGridMultiObservatory.ID = "_RgdMasterGridMultiObservatoryListViewerMultiObservatory";
                _RgdMasterGridMultiObservatory.AllowPaging = false;
                _RgdMasterGridMultiObservatory.AllowSorting = false;
                _RgdMasterGridMultiObservatory.Skin = "EMS";
                _RgdMasterGridMultiObservatory.Width = Unit.Percentage(100);
                _RgdMasterGridMultiObservatory.AutoGenerateColumns = true;
                _RgdMasterGridMultiObservatory.GridLines = System.Web.UI.WebControls.GridLines.None;
                _RgdMasterGridMultiObservatory.ShowStatusBar = false;
                _RgdMasterGridMultiObservatory.PageSize = 18;
                _RgdMasterGridMultiObservatory.AllowMultiRowSelection = false;
                _RgdMasterGridMultiObservatory.PagerStyle.AlwaysVisible = true;
                _RgdMasterGridMultiObservatory.MasterTableView.Width = Unit.Percentage(100);
                _RgdMasterGridMultiObservatory.EnableViewState = true;
                _RgdMasterGridMultiObservatory.EnableEmbeddedSkins = false;

                //Crea los metodos de la grilla (Server)
                //_RgdMasterGridMultiObservatory.NeedDataSource += new GridNeedDataSourceEventHandler(_RgdMasterGridMultiObservatory_NeedDataSource);
                //_RgdMasterGridMultiObservatory.SortCommand += new GridSortCommandEventHandler(_RgdMasterGridMultiObservatory_SortCommand);
                //_RgdMasterGridMultiObservatory.ItemDataBound += new GridItemEventHandler(_RgdMasterGridMultiObservatory_ItemDataBound);
                //_RgdMasterGridMultiObservatory.ItemCreated += new GridItemEventHandler(_RgdMasterGridMultiObservatory_ItemCreated);
                _RgdMasterGridMultiObservatory.ItemDataBound += new GridItemEventHandler(RgdMasterGridMultiObservatory_ItemDataBound);
                _RgdMasterGridMultiObservatory.PreRender += new EventHandler(RgdMasterGridMultiObservatory_PreRender);

                //Crea los metodos de la grilla (Cliente)
                _RgdMasterGridMultiObservatory.ClientSettings.AllowExpandCollapse = false;
                _RgdMasterGridMultiObservatory.AllowMultiRowSelection = false;
                _RgdMasterGridMultiObservatory.ClientSettings.Selecting.AllowRowSelect = true;
                _RgdMasterGridMultiObservatory.ClientSettings.Selecting.EnableDragToSelectRows = false;
                _RgdMasterGridMultiObservatory.MasterTableView.HierarchyLoadMode = GridChildLoadMode.Client;

                //Define los atributos de la MasterGrid
                _RgdMasterGridMultiObservatory.MasterTableView.Name = "gridMasterListViewerMultiObservatory";
                _RgdMasterGridMultiObservatory.MasterTableView.EnableViewState = true;
                _RgdMasterGridMultiObservatory.MasterTableView.CellPadding = 0;
                _RgdMasterGridMultiObservatory.MasterTableView.CellSpacing = 0;
                _RgdMasterGridMultiObservatory.MasterTableView.GridLines = System.Web.UI.WebControls.GridLines.None;
                _RgdMasterGridMultiObservatory.MasterTableView.GroupsDefaultExpanded = false;
                _RgdMasterGridMultiObservatory.MasterTableView.AllowMultiColumnSorting = false;
                _RgdMasterGridMultiObservatory.MasterTableView.HierarchyLoadMode = GridChildLoadMode.Client;
                _RgdMasterGridMultiObservatory.MasterTableView.ExpandCollapseColumn.Resizable = false;
                _RgdMasterGridMultiObservatory.MasterTableView.ExpandCollapseColumn.HeaderStyle.Width = Unit.Pixel(20);
                _RgdMasterGridMultiObservatory.MasterTableView.RowIndicatorColumn.Visible = false;
                _RgdMasterGridMultiObservatory.MasterTableView.RowIndicatorColumn.HeaderStyle.Width = Unit.Pixel(20);
                _RgdMasterGridMultiObservatory.MasterTableView.EditMode = GridEditMode.InPlace;

                _RgdMasterGridMultiObservatory.HeaderStyle.Font.Bold = false;
                _RgdMasterGridMultiObservatory.HeaderStyle.Font.Italic = false;
                _RgdMasterGridMultiObservatory.HeaderStyle.Font.Overline = false;
                _RgdMasterGridMultiObservatory.HeaderStyle.Font.Strikeout = false;
                _RgdMasterGridMultiObservatory.HeaderStyle.Font.Underline = false;
                _RgdMasterGridMultiObservatory.HeaderStyle.Wrap = true;
            }

            private void RegisterCustomMenuPanels()
            {
                List<String> _menuPanels = new List<String>();
                _menuPanels.Add(Common.Constants.ContextInformationKey);
                _menuPanels.Add(Common.Constants.ContextElementMapsKey);

                FwMasterPage.ContentNavigatorCustomMenuPanels(_menuPanels);
            }
        #endregion

        #region Page Events
            protected void RgdMasterGridMultiObservatory_ItemDataBound(object sender, GridItemEventArgs e)
            {
                if (e.Item is GridHeaderItem)
                {
                    GridHeaderItem headerItem = (GridHeaderItem)e.Item;
                    for (int i = 2; i < headerItem.Cells.Count; i++)
                    {
                        if (headerItem.Cells[i].Text == "ProcessName")
                        {
                            headerItem.Cells[i].Text = Resources.CommonListManage.Process;
                        }
                    }
                }
                //Recorremos los datos, para hacer el formato de los nros.
                if (e.Item is GridDataItem)
                {
                    Int32 _columnCount = ((DataRowView)e.Item.DataItem).Row.ItemArray.Count();
                    //Recorremos las columnas con valores, por eso arranca del 2, las 2 primeras columnas son el id y el nombre...
                    for (int i = 2; i < _columnCount; i++)
                    {
                        String _value = ((DataRowView)e.Item.DataItem).Row[i].ToString();
                        if (String.IsNullOrEmpty(_value))
                        {
                            ((DataRowView)e.Item.DataItem).Row[i] = String.Empty;
                        }
                        else
                        {
                            ((DataRowView)e.Item.DataItem).Row[i] = Common.Functions.CustomEMSRound(Convert.ToDecimal(_value));
                        }
                    }
                }

            }
            protected void RgdMasterGridMultiObservatory_PreRender(object sender, EventArgs e)
            {
                int itemsCount = 0;
                int columnsCount = 0;
                RadGrid _rgdReport = (RadGrid)sender;
                foreach (GridDataItem item in _rgdReport.MasterTableView.Items)
                {
                    if (item is GridDataItem)
                    {
                        columnsCount = 0;
                        for (int i = 2; i < _rgdReport.MasterTableView.RenderColumns.Length; i++)
                        {
                            GridColumn column = _rgdReport.MasterTableView.RenderColumns[i];
                            if (column.UniqueName == "IdProcess")
                            {
                                column.Display = false;
                            }
                            columnsCount++;
                        }
                        itemsCount++;
                    }
                }
            }

            protected void btnList_Click(object sender, EventArgs e)
            {
                //if (ManageEntityParams.ContainsKey("StartDate"))
                //{ ManageEntityParams.Remove("StartDate"); }
                //ManageEntityParams.Add("StartDate", rdtFrom.SelectedDate);
                //if (ManageEntityParams.ContainsKey("EndDate"))
                //{ ManageEntityParams.Remove("EndDate"); }
                //ManageEntityParams.Add("EndDate", rdtThrough.SelectedDate);

                //Carga la Grilla
                LoadGridMultiObservatory();
            }
        #endregion

    }
}
