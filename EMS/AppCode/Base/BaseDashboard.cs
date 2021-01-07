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

using EBPA = Condesus.EMS.Business.PA.Entities;

namespace Condesus.EMS.WebUI.Dashboard
{
    //public class BaseDashboard : BasePage
    public partial class BaseDashboard : BasePage
    {
        #region Gadgets

        private Table BuildTable(String id, String cssClass)
        {
            Table _container = new Table();
            _container.ID = id;
            _container.CssClass = cssClass; 
            return _container;
        }
        private TableCell BuildTableCell(String cssClass)
        {
            TableCell _td = new TableCell();
            _td.CssClass = cssClass;
            return _td;
        }
        private TableRow BuildContent(System.Web.UI.Control control)
        {
            TableRow _tr = new TableRow();
            TableCell _tdColContent = BuildTableCell("ColContent");

            _tdColContent.Controls.Add(control);

            _tr.Controls.Add(_tdColContent);
            return _tr;
        }

        private GridBoundColumn CreateBoundColumn(String headerText, String dataField, String uniqueName, String sortExpression, Boolean visible)
        {
            GridBoundColumn _boundColumn = new GridBoundColumn();

            _boundColumn.HeaderText = headerText;
            _boundColumn.DataField = dataField;
            _boundColumn.UniqueName = uniqueName;
            _boundColumn.SortExpression = sortExpression;
            _boundColumn.Visible = visible;

            return _boundColumn;
        }
        private GridTemplateColumn CreateTemplateColumn(System.Web.UI.ITemplate iTemplate, String headerText)
        {
            GridTemplateColumn _rdgTempCol = new GridTemplateColumn();
            _rdgTempCol.ItemTemplate = iTemplate;
            _rdgTempCol.HeaderText = headerText;
            _rdgTempCol.Resizable = false;
            _rdgTempCol.HeaderStyle.Width = Unit.Pixel(13);
            _rdgTempCol.ItemStyle.Width = Unit.Pixel(13);

            return _rdgTempCol;
        }
        private GridButtonColumn CreateButtonColumn(String imageUrl,GridButtonColumnType buttonType,String commandName, String uniqueName)
        {
            GridButtonColumn _buttonColumn = new GridButtonColumn();
            
            _buttonColumn.HeaderText = String.Empty;
            _buttonColumn.Text = uniqueName;
            _buttonColumn.Visible = true;
            _buttonColumn.HeaderStyle.Width = Unit.Pixel(13);
            _buttonColumn.ItemStyle.Width = Unit.Pixel(13);
            _buttonColumn.ImageUrl = imageUrl;
            _buttonColumn.ButtonType = buttonType;
            _buttonColumn.CommandName = commandName;
            _buttonColumn.UniqueName = uniqueName;

            return _buttonColumn;
        }

        #endregion

        //protected void InjectRemoveDock()
        //{   //Esta funcion se ejecuta al hacer click sobre un item del menuRad
        //    System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

        //    _sbBuffer.Append(OpenHtmlJavaScript());

        //    _sbBuffer.Append("function _removeDock()                                            \n");
        //    _sbBuffer.Append("{                                                                 \n");
        //    _sbBuffer.Append("      Sys.Application.remove_load(_removeDock);                   \n");
        //    _sbBuffer.Append("      $find('{0}').undock();                                      \n");
        //    _sbBuffer.Append("      $get('{1}').appendChild($get('{0}'));                       \n");
        //    _sbBuffer.Append("      $find('{0}').doPostBack('DockPositionChanged');             \n");
        //    _sbBuffer.Append("}                                                                 \n");
        //    _sbBuffer.Append("Sys.Application.add_load(_removeDock);                            \n");

        //    _sbBuffer.Append(CloseHtmlJavaScript());

        //    InjectJavascript("JS_removeDock", _sbBuffer.ToString());
        //}
        //protected void InjectAddDock()
        //{   //Esta funcion se ejecuta al hacer click sobre un item del menuRad
        //    System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

        //    _sbBuffer.Append(OpenHtmlJavaScript());

        //    _sbBuffer.Append("function _addDock()                                            \n");
        //    _sbBuffer.Append("{                                                              \n");
        //    _sbBuffer.Append("      Sys.Application.remove_load(_addDock);                   \n");
        //    _sbBuffer.Append("      $find('{1}').dock($find('{0}'));                         \n");
        //    _sbBuffer.Append("      $find('{0}').doPostBack('DockPositionChanged');          \n");
        //    _sbBuffer.Append("}                                                              \n");
        //    _sbBuffer.Append("Sys.Application.add_load(_addDock);                            \n");

        //    _sbBuffer.Append(CloseHtmlJavaScript());

        //    InjectJavascript("JS_removeDock", _sbBuffer.ToString());
        //}

        //protected Int64 IdOrganization
        //{
        //    get
        //    {
        //        //object o = Session["IdOrganization"];
        //        object o = EMSLibrary.User.Person.Organization.IdOrganization;
        //        if (o != null)
        //            //return Convert.ToInt64(Session["IdOrganization"]);
        //            return EMSLibrary.User.Person.Organization.IdOrganization;
        //        throw new Exception("No organization defined");
        //    }
        //}

        //#region ProjectSummary
        //    private Int64 _IdProyectRoleProjectLeader;
        //    public Int64 IdProyectRoleProjectLeader
        //    {
        //        get
        //        {
        //            Int64.TryParse(ConfigurationManager.AppSettings["IdProjectRoleProjectLeader"], out _IdProyectRoleProjectLeader);
        //            return _IdProyectRoleProjectLeader;
        //        }
        //    }
        //    public Boolean IsProyectLeader
        //    {
        //        get
        //        {
        //            Int64 _idProyectRoleProjectLeader;
        //            if (Int64.TryParse(ConfigurationManager.AppSettings["IdProjectRoleProjectLeader"], out _idProyectRoleProjectLeader))
        //            {
        //                //if (EMSLibrary.User.DirectoryServices.Map.Organization(EMSLibrary.User.Person.Organization.IdOrganization).Dashboard.ProcessRoleProcessLeader(_idProyectRoleProjectLeader).Count > 0)
        //                //{
        //                //    return true;
        //                //}
        //            }
        //            return false;
        //        }
        //    }
        //    public Boolean IsProyectBuyer
        //    {
        //        get
        //        {
        //            Int64 _idProyectRoleBuyer;
        //            if (Int64.TryParse(ConfigurationManager.AppSettings["IdProjectRoleBuyer"], out _idProyectRoleBuyer))
        //            {
        //                //if (EMSLibrary.User.DirectoryServices.Map.Organization(EMSLibrary.User.Person.Organization.IdOrganization).Dashboard.ProcessRoleBuyers(_idProyectRoleBuyer).Count > 0)
        //                //{
        //                //    return true;
        //                //}
        //            }
        //            return false;
        //        }
        //    }
        //    public Boolean IsTechnicalDirector
        //    {
        //        get
        //        {
        //            Int64 _idProyectRoleTechnicalDirector;
        //            if (Int64.TryParse(ConfigurationManager.AppSettings["IdProjectRoleTechnicalDirector"], out _idProyectRoleTechnicalDirector))
        //            {
        //                //if (EMSLibrary.User.DirectoryServices.Map.Organization(EMSLibrary.User.Person.Organization.IdOrganization).Dashboard.ProcessRoleTechnicalDirector(_idProyectRoleTechnicalDirector).Count > 0)
        //                //{
        //                //    return true;
        //                //}
        //            }
        //            return false;
        //        }
        //    }
        //#endregion


        //#region BuyerProjectSummary

        //protected Panel BuildBuyerProjectSummaryContent()
        //{
        //    Panel _retContainer = new Panel();
        //    _retContainer.CssClass = "contentformDashboard";

        //    RadGrid _rdgMasterGrid = new RadGrid();
        //    InitBuyerProjectSummaryGrid(_rdgMasterGrid);
        //    _rdgMasterGrid.DataSource = LoadBuyerProjectSummary();

        //    _retContainer.Controls.Add(_rdgMasterGrid);
        //    return _retContainer;
        //}

        //private void InitBuyerProjectSummaryGrid(RadGrid rgdMasterGrid)
        //{
        //    #region Propiedades de la Grilla
        //    rgdMasterGrid.ID = "rgdMasterGridBuyerProjectSummary";
        //    rgdMasterGrid.AllowPaging = true;
        //    rgdMasterGrid.AllowSorting = false;
        //    rgdMasterGrid.Width = Unit.Percentage(100);
        //    rgdMasterGrid.AutoGenerateColumns = false;
        //    rgdMasterGrid.GridLines = System.Web.UI.WebControls.GridLines.None;
        //    rgdMasterGrid.ShowStatusBar = false;
        //    rgdMasterGrid.PageSize = 10;
        //    rgdMasterGrid.AllowMultiRowSelection = false;
        //    rgdMasterGrid.PagerStyle.AlwaysVisible = true;
        //    rgdMasterGrid.PagerStyle.Mode = GridPagerMode.NextPrevAndNumeric;
        //    rgdMasterGrid.MasterTableView.Width = Unit.Percentage(100);
        //    rgdMasterGrid.EnableViewState = false;

        //    //Crea los metodos de la grilla (Server)
        //    rgdMasterGrid.ItemDataBound += new GridItemEventHandler(rgdMasterGrid_ItemDataBound);
        //    //rgdMasterGrid.ItemCreated += new GridItemEventHandler(rgdMasterGrid_ItemCreated);
        //    rgdMasterGrid.NeedDataSource += new GridNeedDataSourceEventHandler(rgdMasterGrid_NeedDataSource);
        //    //rgdMasterGrid.SortCommand += new GridSortCommandEventHandler(rgdMasterGrid_SortCommand);
        //    rgdMasterGrid.PageIndexChanged += new GridPageChangedEventHandler(rgdMasterGrid_PageIndexChanged);

        //    //Crea los metodos de la grilla (Cliente)
        //    rgdMasterGrid.ClientSettings.AllowExpandCollapse = false;
        //    //rgdMasterGrid.ClientSettings.EnableClientKeyValues = true;
        //    rgdMasterGrid.AllowMultiRowSelection = false;
        //    rgdMasterGrid.ClientSettings.Selecting.AllowRowSelect = true;
        //    rgdMasterGrid.ClientSettings.Selecting.EnableDragToSelectRows = false;
        //    rgdMasterGrid.MasterTableView.HierarchyLoadMode = GridChildLoadMode.Client;

        //    //Define los atributos de la MasterGrid
        //    rgdMasterGrid.MasterTableView.Name = "gridMasterBuyerProjectSummary";
        //    rgdMasterGrid.MasterTableView.DataKeyNames = new string[] { "IdProject" };
        //    rgdMasterGrid.MasterTableView.EnableViewState = false;
        //    rgdMasterGrid.MasterTableView.CellPadding = 0;
        //    rgdMasterGrid.MasterTableView.CellSpacing = 0;
        //    rgdMasterGrid.MasterTableView.GridLines = System.Web.UI.WebControls.GridLines.None;
        //    rgdMasterGrid.MasterTableView.GroupsDefaultExpanded = false;
        //    rgdMasterGrid.MasterTableView.AllowMultiColumnSorting = false;
        //    rgdMasterGrid.MasterTableView.HierarchyLoadMode = GridChildLoadMode.Client;
        //    rgdMasterGrid.MasterTableView.ExpandCollapseColumn.Resizable = false;
        //    rgdMasterGrid.MasterTableView.ExpandCollapseColumn.HeaderStyle.Width = Unit.Pixel(20);
        //    rgdMasterGrid.MasterTableView.RowIndicatorColumn.Visible = false;
        //    rgdMasterGrid.MasterTableView.RowIndicatorColumn.HeaderStyle.Width = Unit.Pixel(20);
        //    rgdMasterGrid.MasterTableView.EditMode = GridEditMode.InPlace;

        //    rgdMasterGrid.HeaderStyle.Font.Bold = false;
        //    rgdMasterGrid.HeaderStyle.Font.Italic = false;
        //    rgdMasterGrid.HeaderStyle.Font.Overline = false;
        //    rgdMasterGrid.HeaderStyle.Font.Strikeout = false;
        //    rgdMasterGrid.HeaderStyle.Font.Underline = false;
        //    rgdMasterGrid.HeaderStyle.Wrap = true;
        //    #endregion

        //    #region Estructura de la Grilla

        //    GridBoundColumn _rgdBoundCol = null;
        //    GridTemplateColumn _rdgTempCol = null;

        //    _rgdBoundCol = new GridBoundColumn();
        //    _rgdBoundCol.DataField = "IdProject";
        //    _rgdBoundCol.HeaderText = this.GetLocalResourceObject("IdProject").ToString();
        //    _rgdBoundCol.Display = false;
        //    rgdMasterGrid.MasterTableView.Columns.Add(_rgdBoundCol);

        //    _rgdBoundCol = new GridBoundColumn();
        //    _rgdBoundCol.DataField = "ProjectName";
        //    _rgdBoundCol.HeaderText = this.GetLocalResourceObject("ProjectName").ToString();
        //    _rgdBoundCol.ItemStyle.Width = Unit.Percentage(100);
        //    rgdMasterGrid.MasterTableView.Columns.Add(_rgdBoundCol);

        //    _rgdBoundCol = new GridBoundColumn();
        //    _rgdBoundCol.DataField = "Category";
        //    _rgdBoundCol.HeaderText = this.GetLocalResourceObject("Category").ToString();
        //    _rgdBoundCol.ItemStyle.Width = Unit.Percentage(100);
        //    rgdMasterGrid.MasterTableView.Columns.Add(_rgdBoundCol);

        //    _rgdBoundCol = new GridBoundColumn();
        //    _rgdBoundCol.DataField = "CampaignStartDate";
        //    _rgdBoundCol.HeaderText = this.GetLocalResourceObject("CampaignStartDate").ToString();
        //    _rgdBoundCol.DataType = System.Type.GetType("System.DateTime");
        //    _rgdBoundCol.ItemStyle.Width = Unit.Percentage(100);
        //    rgdMasterGrid.MasterTableView.Columns.Add(_rgdBoundCol);

        //    _rgdBoundCol = new GridBoundColumn();
        //    _rgdBoundCol.DataField = "Status";
        //    _rgdBoundCol.HeaderText = this.GetLocalResourceObject("Status").ToString();
        //    _rgdBoundCol.ItemStyle.Width = Unit.Percentage(100);
        //    rgdMasterGrid.MasterTableView.Columns.Add(_rgdBoundCol);

        //    _rgdBoundCol = new GridBoundColumn();
        //    _rgdBoundCol.DataField = "Forecasted";
        //    _rgdBoundCol.HeaderText = this.GetLocalResourceObject("Forecasted").ToString();
        //    rgdMasterGrid.MasterTableView.Columns.Add(_rgdBoundCol);

        //    _rgdBoundCol = new GridBoundColumn();
        //    _rgdBoundCol.DataField = "Calculated";
        //    _rgdBoundCol.HeaderText = this.GetLocalResourceObject("Calculated").ToString();
        //    rgdMasterGrid.MasterTableView.Columns.Add(_rgdBoundCol);

        //    _rgdBoundCol = new GridBoundColumn();
        //    _rgdBoundCol.DataField = "Deviation";
        //    _rgdBoundCol.HeaderText = this.GetLocalResourceObject("Deviation").ToString();
        //    rgdMasterGrid.MasterTableView.Columns.Add(_rgdBoundCol);

        //    _rdgTempCol = new GridTemplateColumn();
        //    _rdgTempCol.UniqueName = "TemplateColumn";
        //    _rdgTempCol.ItemTemplate = new DBImageButtonTemplate("btnProyectLink");
        //    _rdgTempCol.ItemStyle.Width = Unit.Pixel(21);
        //    TableItemStyle headerStyle = _rdgTempCol.HeaderStyle;
        //    headerStyle.Width = Unit.Pixel(21);
        //    _rdgTempCol.HeaderText = this.GetLocalResourceObject("Report").ToString();
        //    _rdgTempCol.AllowFiltering = false;
        //    rgdMasterGrid.MasterTableView.Columns.Add(_rdgTempCol);

        //    ////Icono de seleccion (abre menu)
        //    //String _templateColumnName = "TemplateColumnPrint";
        //    //_rdgTempCol = new GridTemplateColumn();
        //    //_rdgTempCol.ItemTemplate = new MyTemplateSelection(_templateColumnName);
        //    //_rdgTempCol.HeaderText = _templateColumnName;
        //    //_rdgTempCol.Resizable = false;
        //    //_rdgTempCol.HeaderStyle.Width = Unit.Pixel(13);
        //    //_rdgTempCol.ItemStyle.Width = Unit.Pixel(13);
        //    //rgdMasterGrid.MasterTableView.Columns.Add(_rdgTempCol);

        //    #endregion
        //}

        //private DataTable LoadBuyerProjectSummary()
        //{
        //    DataTable _dt = new DataTable();
        //    _dt.TableName = "Root";
        //    _dt.Columns.Add("IdProject");
        //    _dt.Columns.Add("ProjectName");
        //    _dt.Columns.Add("Category");
        //    _dt.Columns.Add("CampaignStartDate");
        //    _dt.Columns.Add("Status");
        //    _dt.Columns.Add("Forecasted");
        //    _dt.Columns.Add("Calculated");
        //    _dt.Columns.Add("Deviation");

        //    //TODO: Rehacer con lo nuevo de Roles y el nuevo DASHBOARD!!!

        //    //Int64 _idProyectRoleBuyer = -1;
        //    //Int64 _idProyectRoleTechnicalDirector = -1;
        //    ////List<Condesus.EMS.Business.PF.Entities.ProcessRole> _projectRoles = null;

        //    //Int64.TryParse(ConfigurationManager.AppSettings["IdProjectRoleTechnicalDirector"], out _idProyectRoleTechnicalDirector);
        //    //Int64.TryParse(ConfigurationManager.AppSettings["IdProjectRoleBuyer"], out _idProyectRoleBuyer);

        //    //_projectRoles = EMSLibrary.User.DirectoryServices.Map.Organization(EMSLibrary.User.Person.Organization.IdOrganization).Dashboard.ProcessRoleBuyersAndTechnicalDirector(_idProyectRoleBuyer, _idProyectRoleTechnicalDirector);

        //    //if (_projectRoles != null)
        //    //{
        //    //    //Totals
        //    //    Decimal _estimationTotal = 0;
        //    //    Decimal _verificationTotal = 0;

        //    //    foreach (Condesus.EMS.Business.PF.Entities.ProcessRole _projectRole in _projectRoles)
        //    //    {
        //    //        Decimal _estimated = _projectRole.ProcessGroupProcess.TotalEstimatesByScenario(EMSLibrary.User.PerformanceAssessments.Configuration.CalculationScenarioType(Convert.ToInt64(ConfigurationManager.AppSettings["PDDEstimated"])));
        //    //        Decimal _current = _projectRole.ProcessGroupProcess.FirstCalculationResult();
        //    //        Decimal _state;
        //    //        if (_estimated == 0)
        //    //        { _state = 0; }
        //    //        else
        //    //        { _state = Math.Round((_current / _estimated * 100), 2); }


        //    //        //Totals
        //    //        _estimationTotal += _estimated;
        //    //        _verificationTotal += _current;

        //    //        //Busca las propiedades extendidas de categoria y status.
        //    //        String _category = String.Empty;
        //    //        String _status = String.Empty;
        //    //        Condesus.EMS.Business.PF.Entities.ProcessExtendedProperty _extendedProperty;
        //    //        _extendedProperty = _projectRole.ProcessGroupProcess.ProcessExtendedProperty(Convert.ToInt64(ConfigurationManager.AppSettings["IdExtendedPropertyCategory"]));
        //    //        if (_extendedProperty != null)
        //    //        { _category = _extendedProperty.Value.ToString(); }

        //    //        _extendedProperty = _projectRole.ProcessGroupProcess.ProcessExtendedProperty(Convert.ToInt64(ConfigurationManager.AppSettings["IdExtendedPropertyProjectStatus"]));
        //    //        if (_extendedProperty != null)
        //    //        { _status = _extendedProperty.Value.ToString(); }

        //    //        _dt.Rows.Add(_projectRole.ProcessGroupProcess.IdProcess,
        //    //                       _projectRole.ProcessGroupProcess.LanguageOption.Title,
        //    //                       _category,
        //    //                       _projectRole.ProcessGroupProcess.CurrentCampaignStartDate.ToShortDateString(),
        //    //                       _status,
        //    //                       Math.Round(_estimated, 2),
        //    //                       Math.Round(_current, 2),
        //    //                       _state);
        //    //    }

        //    //    Decimal _deviationTotal;
        //    //    if (_estimationTotal == 0)
        //    //    { _deviationTotal = 0; }
        //    //    else
        //    //    { _deviationTotal = Math.Round(_verificationTotal / _estimationTotal * 100, 2); }

        //    //    _dt.Rows.Add(-1, "<b>Total</b>", "", "", "", "<b>" + Math.Round(_estimationTotal, 2).ToString() + "</b>", "<b>" + Math.Round(_verificationTotal, 2).ToString() + "</b>", "<b>" + _deviationTotal.ToString() + "</b>");
        //    //}

        //    return _dt;
        //}

        //#region Events
        //protected void rgdMasterGrid_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
        //{
        //    if (e.Item is GridDataItem)
        //    {
        //        HtmlImage oimg = (HtmlImage)e.Item.FindControl("prtButton");
        //        if (!(oimg == null))
        //        {
        //            oimg.Attributes["onclick"] = string.Format("return ShowReportToPrint(event, " + e.Item.ItemIndex + ", '" + e.Item.Parent.Parent.UniqueID + "');");
        //        }
        //    }
        //}
        //void rgdMasterGrid_ItemDataBound(object sender, GridItemEventArgs e)
        //{
        //    if (e.Item is GridDataItem)
        //    {
        //        IButtonControl _btnProyectLink = (IButtonControl)e.Item.FindControl("btnProyectLink");
        //        if (_btnProyectLink != null)
        //        {
        //            if (((System.Data.DataRowView)e.Item.DataItem).Row["IdProject"].ToString() == "-1")
        //            {
        //                ((ImageButton)_btnProyectLink).Visible = false;
        //            }
        //            else
        //            {
                        
        //                //_btnProyectLink.CommandArgument = ((System.Data.DataRowView)e.Item.DataItem).Row["IdProject"].ToString();
        //                //_btnProyectLink.Click += new EventHandler(_btnProyectLink_Click);

        //                ((ImageButton)_btnProyectLink).Attributes["onclick"] = string.Format("return ShowReportToPrint(event, " + e.Item.ItemIndex + ", '" + e.Item.Parent.Parent.UniqueID + "');");
        //            }
        //        }
        //    }
        //}

        //void _btnProyectLink_Click(object sender, EventArgs e)
        //{
        //    IButtonControl _btnLink = (IButtonControl)sender;

        //    Int64 _idProject = Int64.Parse(_btnLink.CommandArgument);

        //    Context.Items.Add("IdProject", _idProject);
        //    Server.Transfer("~/Dashboard/ProyectBuyerSummary.aspx");
        //}

        //protected void rgdMasterGrid_SortCommand(object sender, Telerik.Web.UI.GridSortCommandEventArgs e)
        //{
        //    RadGrid _rgdMasterGrid = (RadGrid)sender;
        //    _rgdMasterGrid.MasterTableView.Rebind();
        //}
        //protected void rgdMasterGrid_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        //{
        //    RadGrid _rgdMasterGrid = (RadGrid)sender;
        //    _rgdMasterGrid.MasterTableView.Rebind();
        //}
        //protected void rgdMasterGrid_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        //{
        //    RadGrid _rgdMasterGrid = (RadGrid)sender;
        //    _rgdMasterGrid.DataSource = LoadBuyerProjectSummary();
        //}
        //#endregion
        //#endregion
    }
}
