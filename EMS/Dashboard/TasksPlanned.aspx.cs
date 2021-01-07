using System;
using System.Collections;
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

namespace Condesus.EMS.WebUI.Dashboard
{
    public partial class TasksPlanned : BasePage
    {

        #region Internal Properties
            RadGrid _RgdNextExecutionsGrid;
        #endregion

        #region Page Event
            protected void Page_Init(object sender, EventArgs e)
            {
                CreateExecutionsGrid(ref _RgdNextExecutionsGrid, "Next");
                rcSelectDay.SelectedDateChanged += new Telerik.Web.UI.Calendar.SelectedDateChangedEventHandler(rcSelectDay_SelectedDateChanged);
                InitializeNextExecutionsGrid();
                InitializeCalendar();
                //CheckSecurity();
            }
            protected void Page_Load(object sender, EventArgs e)
            {
                if (!Page.IsPostBack)
                {
                    base.SetNavigator();

                    rcSelectDay.SelectedDate = DateTime.Now;
                }
            }
            protected override void SetPagetitle()
            {
                base.PageTitle = GetLocalResourceObject("PageTitle").ToString();

            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = GetLocalResourceObject("PageTitleSubTitle").ToString();
            }

            #region Calendar
            protected void rcSelectDay_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
                {
                    if (rcSelectDay.SelectedDate == DateTime.MinValue)
                    {
                        rcSelectDay.SelectedDate = DateTime.Now;
                    }

                    _RgdNextExecutionsGrid.CurrentPageIndex = 0;
                    LoadGrid();
                }
            #endregion

            #region Grid Methods
                protected void _RgdNextExecutionsGrid_SortCommand(object source, Telerik.Web.UI.GridSortCommandEventArgs e)
                {
                    _RgdNextExecutionsGrid.DataSource = GetExecutions("Next");
                    _RgdNextExecutionsGrid.MasterTableView.Rebind();
                }
                protected void _RgdNextExecutionsGrid_PageIndexChanged(object source, GridPageChangedEventArgs e)
                {
                    _RgdNextExecutionsGrid.DataSource = GetExecutions("Next");
                    _RgdNextExecutionsGrid.MasterTableView.Rebind();
                }
                protected void _RgdNextExecutionsGrid_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
                {
                    _RgdNextExecutionsGrid.DataSource = GetExecutions("Next");
                }
            #endregion

        #endregion

        #region Generic Method
            private void InitializeCalendar()
            {
                rcSelectDay.DateInput.Attributes.Add("onclick", "PopupAbove(event, '" + rcSelectDay.ClientID + "')");
            }
            private void DefineExecutionsColumns(GridTableView gridTableViewDetails)
            {
                //Add columns bound
                GridBoundColumn boundColumn;

                //Crea y agrega las columna de tipo template, con sus respectivos controles internos (Check e Image)
                string templateColumnName = "";
                //GridTemplateColumn templateColumn = new GridTemplateColumn();
                //templateColumn.ItemTemplate = new MyTemplateSelection(templateColumnName);
                //templateColumn.HeaderText = templateColumnName;
                //templateColumn.Resizable = false;
                //templateColumn.HeaderStyle.Width = Unit.Pixel(13);
                //templateColumn.ItemStyle.Width = Unit.Pixel(13);
                //gridTableViewDetails.Columns.Add(templateColumn);

                //Crea y agrega las columnas de tipo Bound
                boundColumn = new GridBoundColumn();
                boundColumn.HeaderStyle.Width = Unit.Pixel(150);
                boundColumn.DataField = "ProjectTitle";
                boundColumn.HeaderText = "Project";
                boundColumn.ItemStyle.Width = Unit.Pixel(150);
                gridTableViewDetails.Columns.Add(boundColumn);

                //Crea y agrega las columnas de tipo Bound
                boundColumn = new GridBoundColumn();
                boundColumn.HeaderStyle.Width = Unit.Pixel(150);
                boundColumn.DataField = "Title";
                boundColumn.HeaderText = "Title";
                boundColumn.ItemStyle.Width = Unit.Pixel(150);
                gridTableViewDetails.Columns.Add(boundColumn);

                //Crea y agrega las columnas de tipo Bound
                boundColumn = new GridBoundColumn();
                boundColumn.HeaderStyle.Width = Unit.Pixel(150);
                boundColumn.DataField = "StartDate";
                boundColumn.HeaderText = "Start Date";
                boundColumn.ItemStyle.Width = Unit.Pixel(150);
                gridTableViewDetails.Columns.Add(boundColumn);

                //Crea y agrega las columnas de tipo Bound
                boundColumn = new GridBoundColumn();
                boundColumn.HeaderStyle.Width = Unit.Pixel(150);
                boundColumn.DataField = "EndDate";
                boundColumn.HeaderText = "End Date";
                boundColumn.ItemStyle.Width = Unit.Pixel(150);
                gridTableViewDetails.Columns.Add(boundColumn);

                //Crea y agrega las columnas de tipo Bound
                boundColumn = new GridBoundColumn();
                boundColumn.HeaderStyle.Width = Unit.Pixel(150);
                boundColumn.DataField = "State";
                boundColumn.HeaderText = "State";
                boundColumn.ItemStyle.Width = Unit.Pixel(150);
                gridTableViewDetails.Columns.Add(boundColumn);

                //Crea y agrega las columnas de tipo Bound
                boundColumn = new GridBoundColumn();
                boundColumn.HeaderStyle.Width = Unit.Pixel(150);
                boundColumn.DataField = "Type";
                boundColumn.HeaderText = "Type";
                boundColumn.ItemStyle.Width = Unit.Pixel(150);
                gridTableViewDetails.Columns.Add(boundColumn);

                //Crea y agrega las columnas de tipo Bound
                boundColumn = new GridBoundColumn();
                boundColumn.DataField = "IdTask";
                boundColumn.HeaderText = "IdTask";
                boundColumn.Display = false;
                gridTableViewDetails.Columns.Add(boundColumn);

                //Crea y agrega las columnas de tipo Bound
                boundColumn = new GridBoundColumn();
                boundColumn.DataField = "IdExecution";
                boundColumn.HeaderText = "IdExecution";
                boundColumn.Display = false;
                gridTableViewDetails.Columns.Add(boundColumn);
            }
            private DataTable GetExecutions(String type)
            {
                DataTable _dt = new DataTable();
                _dt.TableName = "Root";
                _dt.Columns.Add("IdTask");
                _dt.Columns.Add("IdExecution");
                _dt.Columns.Add("ProjectTitle");
                _dt.Columns.Add("Title");
                _dt.Columns.Add("StartDate");
                _dt.Columns.Add("EndDate");
                _dt.Columns.Add("State"); //metodo de process y execution
                _dt.Columns.Add("Type");//typeof del process


                foreach (Condesus.EMS.Business.PF.Entities.ProcessTaskExecution _processTaskExecution in EMSLibrary.User.Dashboard.TasksPlanned(Convert.ToDateTime(rcSelectDay.SelectedDate)).Values)
                {
                    String _type = HttpContext.GetLocalResourceObject("/Dashboard/Dashboard.aspx", _processTaskExecution.ProcessTask.GetType().Name).ToString();

                    _dt.Rows.Add(_processTaskExecution.ProcessTask.IdProcess,
                             _processTaskExecution.IdExecution,
                             _processTaskExecution.ProcessTask.Parent.LanguageOption.Title,
                             _processTaskExecution.ProcessTask.LanguageOption.Title,
                             _processTaskExecution.Date.ToLongDateString() + " " + _processTaskExecution.Date.ToLongTimeString(),
                             _processTaskExecution.EndDate.ToLongDateString() + " " + _processTaskExecution.EndDate.ToLongTimeString(),
                             //_processTaskExecution.Date.ToString("MM/dd/yyyy HH:mm:ss"),
                             //_processTaskExecution.EndDate.ToString("MM/dd/yyyy HH:mm:ss"),
                             _processTaskExecution.State,
                             _type);
                }

                return _dt;
            }
            private void CreateExecutionsGrid(ref RadGrid radGrid, String type)
            {
                radGrid = new RadGrid();

                radGrid.MasterTableView.DataKeyNames = new string[] { "IdTask", "IdExecution" };
                radGrid.ID = "rgdExecutionsGrid" + type;
                radGrid.MasterTableView.Name = "gridExecutions" + type;
                radGrid.AllowPaging = true;
                radGrid.AllowSorting = true;
                radGrid.Width = Unit.Percentage(100);
                radGrid.AutoGenerateColumns = false;
                radGrid.GridLines = System.Web.UI.WebControls.GridLines.None;
                radGrid.ShowStatusBar = false;
                radGrid.PageSize = 10;
                radGrid.PagerStyle.AlwaysVisible = true;
                radGrid.PagerStyle.Mode = GridPagerMode.NextPrevAndNumeric;
                radGrid.EnableViewState = true;

                //Define los atributos de la MasterGrid
                radGrid.MasterTableView.EnableViewState = true;
                radGrid.MasterTableView.CellPadding = 0;
                radGrid.MasterTableView.CellSpacing = 0;
                radGrid.MasterTableView.GridLines = System.Web.UI.WebControls.GridLines.None;
                radGrid.MasterTableView.GroupsDefaultExpanded = false;
                radGrid.MasterTableView.AllowMultiColumnSorting = false;
                radGrid.MasterTableView.ExpandCollapseColumn.Resizable = false;
                radGrid.MasterTableView.ExpandCollapseColumn.HeaderStyle.Width = Unit.Pixel(20);
                radGrid.MasterTableView.RowIndicatorColumn.Visible = false;
                radGrid.MasterTableView.RowIndicatorColumn.HeaderStyle.Width = Unit.Pixel(20);


                //Crea los metodos de la grilla (Cliente)
                radGrid.ClientSettings.AllowExpandCollapse = true;
                radGrid.ClientSettings.Selecting.AllowRowSelect = true;
                radGrid.ClientSettings.Selecting.EnableDragToSelectRows = false;

                radGrid.HeaderStyle.Font.Bold = false;
                radGrid.HeaderStyle.Font.Italic = false;
                radGrid.HeaderStyle.Font.Overline = false;
                radGrid.HeaderStyle.Font.Strikeout = false;
                radGrid.HeaderStyle.Font.Underline = false;
                radGrid.HeaderStyle.Wrap = true;

                //Seteos para la paginacion de la grilla, ahora es culturizable.
                radGrid.PagerStyle.Mode = GridPagerMode.NextPrevAndNumeric;
                radGrid.MasterTableView.PagerStyle.Mode = GridPagerMode.NextPrevAndNumeric;

                radGrid.MasterTableView.PagerStyle.NextPagesToolTip = Resources.Common.GridPagerStyleNextPagesToolTip;    // "Next Pages";
                radGrid.MasterTableView.PagerStyle.NextPageText = Resources.Common.GridPagerStyleNextPageText;    // "&gt;";
                radGrid.MasterTableView.PagerStyle.NextPageToolTip = Resources.Common.GridPagerStyleNextPageToolTip;  // "Next Page";
                radGrid.MasterTableView.PagerStyle.PagerTextFormat = Resources.Common.GridPagerStylePagerTextFormat;  // "Change page: {4} &nbsp;Displaying page {0} of {1}, items {2} to {3} of {5}.";
                radGrid.MasterTableView.PagerStyle.PrevPagesToolTip = Resources.Common.GridPagerStylePrevPagesToolTip;    // "Previous Pages";
                radGrid.MasterTableView.PagerStyle.PrevPageText = Resources.Common.GrdiPagerStylePrevPageText;    // "&lt;";
                radGrid.MasterTableView.PagerStyle.PrevPageToolTip = Resources.Common.GridPagerStylePrevPageToolTip;  // "Previous Page";

                radGrid.PagerStyle.NextPagesToolTip = Resources.Common.GridPagerStyleNextPagesToolTip; //"Next Pages";
                radGrid.PagerStyle.NextPageText = Resources.Common.GridPagerStyleNextPageText;    //"&gt;";
                radGrid.PagerStyle.NextPageToolTip = Resources.Common.GridPagerStyleNextPageToolTip;  // "Next Page";
                radGrid.PagerStyle.PagerTextFormat = Resources.Common.GridPagerStylePagerTextFormat;  // "Change page: {4} &nbsp;Displaying page {0} of {1}, items {2} to {3} of {5}.";
                radGrid.PagerStyle.PrevPagesToolTip = Resources.Common.GridPagerStylePrevPagesToolTip;    // "Previous Pages";
                radGrid.PagerStyle.PrevPageText = Resources.Common.GrdiPagerStylePrevPageText;    // "&lt;";
                radGrid.PagerStyle.PrevPageToolTip = Resources.Common.GridPagerStylePrevPageToolTip;  // "Previous Page";

                //Crea las columnas para la MasterGrid.
                DefineExecutionsColumns(radGrid.MasterTableView);

                //Agrega toda la grilla dentro del panle que ya esta en el html.
                pnlTasks.Controls.Add(radGrid);
            }
            private void LoadGrid()
            {
                _RgdNextExecutionsGrid.DataSource = GetExecutions("Next");
                _RgdNextExecutionsGrid.DataBind();
            }
            private void InitializeNextExecutionsGrid()
            {
                //Crea los metodos de la grilla (Server)
                _RgdNextExecutionsGrid.NeedDataSource += new GridNeedDataSourceEventHandler(this._RgdNextExecutionsGrid_NeedDataSource);
                //_RgdNextExecutionsGrid.DetailTableDataBind += new GridDetailTableDataBindEventHandler(this._RgdNextExecutionsGrid_DetailTableDataBind);
                //_RgdNextExecutionsGrid.ItemDataBound += new GridItemEventHandler(this._RgdNextExecutionsGrid_ItemDataBound);
                //_RgdNextExecutionsGrid.ItemCommand += new GridCommandEventHandler(this._RgdNextExecutionsGrid_ItemCommand);
                //_RgdNextExecutionsGrid.ItemCreated += new GridItemEventHandler(this._RgdNextExecutionsGrid_ItemCreated);
                _RgdNextExecutionsGrid.SortCommand += new GridSortCommandEventHandler(this._RgdNextExecutionsGrid_SortCommand);
                _RgdNextExecutionsGrid.PageIndexChanged += new GridPageChangedEventHandler(this._RgdNextExecutionsGrid_PageIndexChanged);

            }

            #region TemplateColumnsClass
                private class MyTemplateSelection : ITemplate
                {
                    protected HtmlImage imgSelButton;
                    private string colname;

                    public MyTemplateSelection(string cName)
                    {
                        colname = cName;
                    }
                    public void InstantiateIn(System.Web.UI.Control container)
                    {
                        imgSelButton = new HtmlImage();
                        imgSelButton.ID = "selButton";
                        imgSelButton.Src = "~/RadControls/Grid/Skins/EMS/SortMenuGrid.gif";
                        imgSelButton.Alt = "";
                        container.Controls.Add(imgSelButton);
                    }
                }
            #endregion
        #endregion


        




        

      }
}
