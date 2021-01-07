using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Telerik.Web.UI;
using EMS_PM = Condesus.EMS.Business.PF;

namespace Condesus.EMS.WebUI.ManagementTools.ProcessesMap.Controls
{
    //public partial class ImprovementActions : ProcessBaseControl
    //{
    //    RadGrid _RgdMasterGridExceptions;

    //    #region Private Events

    //    public event GRCGridMenuClick OnMenuClick;
    //    private void GRCGridMenuClick(String action, Int64 idValue, String itemOptionSelected, Int64 idTask)
    //    {
    //        if (OnMenuClick != null)
    //            OnMenuClick(this, new GRCGridMenuEventArgs(action, idValue, itemOptionSelected, idTask));
    //    }
    //    private void GRCGridMenuClick(String action, Int64 idExtendedProperty, Int64 idExtendedPropertyClasification)
    //    {
    //        if (OnMenuClick != null)
    //            OnMenuClick(this, new GRCGridMenuEventArgs(action, idExtendedProperty, idExtendedPropertyClasification));
    //    }

    //    #endregion

    //    #region Page Init & Load
    //    protected override void OnInit(EventArgs e)
    //    {
    //        base.OnInit(e);
    //        //Le pasa su TabStrip al Padre
    //        SetRadTabStrip(rtsMainTab);
    //        SetPnlTabContainer(pnlTabContainer);
    //    }

    //    protected override void InitializeHandlers()
    //    {
    //        base.InitializeHandlers();
    //        rtsMainTab.TabClick += new RadTabStripEventHandler(rtsMainTab_TabClick);
    //        rmnSelectionIAException.ItemClick +=new RadMenuEventHandler(rmnSelectionIAException_ItemClick);
    //    }

    //    protected void Page_Load(object sender, EventArgs e)
    //    {
    //        //Arma el contenido
    //        BuildTabContent();

    //        base.SetSelectedTab();

    //        base.HideTabs(rtsMainTab);
    //    }
    //    #endregion

    //    #region Methods

    //    #region Private Methods

    //    private void BuildTabContent()
    //    {
    //        pnlTabContainer.Controls.Clear();

    //        switch (_ActiveTab)
    //        {
    //            case PMTab.Exception:
    //                BuildExceptionTabContent();
    //                break;
    //        }
    //    }

    //    private String GetMenuAction(String menuItemId)
    //    {
    //        switch (menuItemId)
    //        {
    //            case "view":
    //                return "VIEW";
    //            case "edit":
    //                return "EDIT";
    //            case "close":
    //                return "CLOSE";
    //            default:
    //                return String.Empty;
    //        }
    //    }

    //    //Para menejar los estados del Menu desde el Cliente, debo "customizar" 
    //    //valores por la diferencia de Lenguajes (la variable no llega siempre igual)
    //    //Por lo tanto tengo que Chequear SI O SI por ID (ya que el String nunca lo se)
    //    //TODO: IMPORTANT!! Implementar lo mismo en los demas Controles
    //    private String SetStateFlag(Int64 exceptionState)
    //    {
    //        switch (exceptionState)
    //        {
    //            case 1:
    //                return "opened";
    //            case 2:
    //                return "treatement";
    //            case 3:
    //                return "closed";
    //            default:
    //                throw new Exception("La clave pasada como parametro no se corresponde a un estado válido.");
    //        }
    //    }

    //    private void BuildExceptionTabContent()
    //    {
    //        #region Menu de Seleccion
    //        RadMenuItem ItemExceptionView = new RadMenuItem(Resources.Common.mnuView);
    //        ItemExceptionView.Value = "view";
    //        //Common.Functions.DoRadItemSecurity(ItemExecutionView, true);
    //        rmnSelectionIAException.Items.Add(ItemExceptionView);

    //        RadMenuItem ItemExceptionEdit = new RadMenuItem(Resources.Common.mnuEdit);
    //        ItemExceptionEdit.Value = "edit";
    //        //Common.Functions.DoRadItemSecurity(ItemExecutionEdit, true);
    //        rmnSelectionIAException.Items.Add(ItemExceptionEdit);

    //        //RadMenuItem ItemExceptionClose = new RadMenuItem("Close");
    //        //ItemExceptionClose.ID = "RadMenuItem3";
    //        //ItemExceptionClose.Value = "close";
    //        //Common.Functions.DoRadItemSecurity(ItemExecutionException, true);
    //        //rmnSelectionIAException.Items.Add(ItemExceptionClose);
    //        #endregion

    //        #region Grilla
    //        pnlTabContainer.Controls.Add(new LiteralControl("<div class='contentform'>"));

    //        _RgdMasterGridExceptions = new RadGrid();
    //        _RgdMasterGridExceptions.ID = "rgdMasterGrid";
    //        _RgdMasterGridExceptions.AllowPaging = true;
    //        _RgdMasterGridExceptions.AllowSorting = true;
    //        _RgdMasterGridExceptions.Width = Unit.Percentage(100);
    //        _RgdMasterGridExceptions.AutoGenerateColumns = false;
    //        _RgdMasterGridExceptions.GridLines = System.Web.UI.WebControls.GridLines.None;
    //        _RgdMasterGridExceptions.ShowStatusBar = false;
    //        _RgdMasterGridExceptions.PageSize = 18;
    //        _RgdMasterGridExceptions.AllowMultiRowSelection = false;
    //        _RgdMasterGridExceptions.PagerStyle.AlwaysVisible = true;
    //        _RgdMasterGridExceptions.PagerStyle.Mode = GridPagerMode.NextPrevAndNumeric;
    //        _RgdMasterGridExceptions.MasterTableView.Width = Unit.Percentage(100);
    //        _RgdMasterGridExceptions.EnableViewState = false;

    //        //Crea los metodos de la grilla (Server)
    //        _RgdMasterGridExceptions.NeedDataSource += new GridNeedDataSourceEventHandler(this.rgdMasterGrid_NeedDataSource);
    //        //_RgdMasterGridExceptions.ItemCreated += new GridItemEventHandler(this.rgdMasterGrid_ItemCreated);
    //        _RgdMasterGridExceptions.SortCommand += new GridSortCommandEventHandler(this.rgdMasterGrid_SortCommand);
    //        _RgdMasterGridExceptions.PageIndexChanged += new GridPageChangedEventHandler(this.rgdMasterGrid_PageIndexChanged);

    //        //Crea los metodos de la grilla (Cliente)
    //        _RgdMasterGridExceptions.ClientSettings.ClientEvents.OnRowContextMenu = "RowContextMenuIAException";
    //        //rgdMasterGrid.ClientSettings.ClientEvents.OnRowSelected = "RowSelected";
    //        _RgdMasterGridExceptions.ClientSettings.AllowExpandCollapse = false;
    //        //_RgdMasterGridExceptions.ClientSettings.EnableClientKeyValues = true;
    //        _RgdMasterGridExceptions.AllowMultiRowSelection = false;
    //        _RgdMasterGridExceptions.ClientSettings.Selecting.AllowRowSelect = true;
    //        _RgdMasterGridExceptions.ClientSettings.Selecting.EnableDragToSelectRows = false;
    //        _RgdMasterGridExceptions.MasterTableView.HierarchyLoadMode = GridChildLoadMode.Client;

    //        //Define los atributos de la MasterGrid
    //        _RgdMasterGridExceptions.MasterTableView.Name = "gridMaster";
    //        _RgdMasterGridExceptions.MasterTableView.DataKeyNames = new string[] { "IdException", "ExceptionStateFlag" };
    //        _RgdMasterGridExceptions.MasterTableView.EnableViewState = false;
    //        _RgdMasterGridExceptions.MasterTableView.CellPadding = 0;
    //        _RgdMasterGridExceptions.MasterTableView.CellSpacing = 0;
    //        _RgdMasterGridExceptions.MasterTableView.GridLines = System.Web.UI.WebControls.GridLines.None;
    //        _RgdMasterGridExceptions.MasterTableView.GroupsDefaultExpanded = false;
    //        _RgdMasterGridExceptions.MasterTableView.AllowMultiColumnSorting = false;
    //        _RgdMasterGridExceptions.MasterTableView.HierarchyLoadMode = GridChildLoadMode.Client;
    //        _RgdMasterGridExceptions.MasterTableView.ExpandCollapseColumn.Resizable = false;
    //        _RgdMasterGridExceptions.MasterTableView.ExpandCollapseColumn.HeaderStyle.Width = Unit.Pixel(20);
    //        _RgdMasterGridExceptions.MasterTableView.RowIndicatorColumn.Visible = false;
    //        _RgdMasterGridExceptions.MasterTableView.RowIndicatorColumn.HeaderStyle.Width = Unit.Pixel(20);

    //        _RgdMasterGridExceptions.HeaderStyle.Font.Bold = false;
    //        _RgdMasterGridExceptions.HeaderStyle.Font.Italic = false;
    //        _RgdMasterGridExceptions.HeaderStyle.Font.Overline = false;
    //        _RgdMasterGridExceptions.HeaderStyle.Font.Strikeout = false;
    //        _RgdMasterGridExceptions.HeaderStyle.Font.Underline = false;
    //        _RgdMasterGridExceptions.HeaderStyle.Wrap = true;

    //        //Crea las columnas para la MasterGrid.
    //        DefineColumns(_RgdMasterGridExceptions.MasterTableView);

    //        //Agrega toda la grilla dentro del panle que ya esta en el html.
    //        pnlTabContainer.Controls.Add(_RgdMasterGridExceptions);
    //        pnlTabContainer.Controls.Add(new LiteralControl("</div>"));
    //        #endregion
    //    }

    //    #region RGDMasterGridExecutions
    //    protected void rgdMasterGrid_SortCommand(object source, GridSortCommandEventArgs e)
    //    {
    //        RadGrid handler = (RadGrid)source;
    //        handler.DataSource = ReturnDataGrid();
    //        handler.MasterTableView.Rebind();
    //    }
    //    protected void rgdMasterGrid_PageIndexChanged(object source, GridPageChangedEventArgs e)
    //    {
    //        RadGrid handler = (RadGrid)source;
    //        handler.DataSource = ReturnDataGrid();
    //        handler.MasterTableView.Rebind();
    //    }
    //    protected void rgdMasterGrid_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    //    {
    //        RadGrid handler = (RadGrid)source;
    //        //Carga todos los ROOT en la grilla.
    //        handler.DataSource = ReturnDataGrid();
    //    }

    //    private DataTable ReturnDataGrid()
    //    {
    //        //Crea el DataTable para luego devolverlo y asociarlo a la grilla.
    //        DataTable _dt = new DataTable();
    //        _dt.TableName = "Root";
    //        _dt.Columns.Add("IdException");
    //        _dt.Columns.Add("ExceptionDate");
    //        _dt.Columns.Add("ExceptionType");
    //        _dt.Columns.Add("ExceptionState");
    //        _dt.Columns.Add("ExceptionStateFlag");

    //        if (_IdProcess != 0)
    //        {
    //            IDictionary<Int64, Condesus.EMS.Business.IA.Entities.Exception> _processExceptions = EMSLibrary.User.ProcessFramework.Map.Process(_IdProcess).Exceptions;
                
    //            //Agrega los datos al DataTable
    //            if(_processExceptions != null)
    //                foreach (Condesus.EMS.Business.IA.Entities.Exception _processException in _processExceptions.Values)
    //                    _dt.Rows.Add(_processException.IdException, _processException.ExceptionDate, _processException.ExceptionType.LanguageOption.Name, _processException.ExceptionState.LanguageOption.Name, SetStateFlag(_processException.ExceptionState.IdExceptionState));
    //        }
    //        return _dt;
    //    }

    //    private void DefineColumns(GridTableView gridTableViewDetails)
    //    {
    //        //Add columns bound
    //        GridBoundColumn boundColumn;

    //        //Columnas que no se ven...
    //        //Crea y agrega las columnas de tipo Bound
    //        boundColumn = new GridBoundColumn();
    //        boundColumn.DataField = "IdException";
    //        boundColumn.HeaderText = "IdException";
    //        boundColumn.Display = false;
    //        gridTableViewDetails.Columns.Add(boundColumn);

    //        //Crea y agrega las columnas de tipo Bound
    //        boundColumn = new GridBoundColumn();
    //        boundColumn.DataField = "ExceptionStateFlag";
    //        boundColumn.HeaderText = "ExceptionStateFlag";
    //        boundColumn.Display = false;
    //        gridTableViewDetails.Columns.Add(boundColumn);
            
    //        //Crea y agrega las columnas de tipo Bound
    //        boundColumn = new GridBoundColumn();
    //        boundColumn.DataField = "ExceptionDate";
    //        boundColumn.HeaderText = "Date";
    //        boundColumn.Display = true;
    //        gridTableViewDetails.Columns.Add(boundColumn);


    //        //Crea y agrega las columnas de tipo Bound
    //        boundColumn = new GridBoundColumn();
    //        boundColumn.DataField = "ExceptionType";
    //        boundColumn.HeaderText = "Origin";
    //        boundColumn.Display = true;
    //        gridTableViewDetails.Columns.Add(boundColumn);

    //        //Crea y agrega las columnas de tipo Bound
    //        boundColumn = new GridBoundColumn();
    //        boundColumn.DataField = "ExceptionState";
    //        boundColumn.HeaderText = "State";
    //        boundColumn.Display = true;
    //        gridTableViewDetails.Columns.Add(boundColumn);
    //    }
    //    #endregion

    //    #endregion

    //    #endregion

    //    #region Eventos

    //    void rtsMainTab_TabClick(object sender, RadTabStripEventArgs e)
    //    {
    //        try
    //        {
    //            _ActiveTab = (PMTab)Enum.Parse(typeof(PMTab), e.Tab.Value);
    //        }
    //        catch
    //        {
    //            throw new Exception("The Selected Tab Type isnt suported in this Control");
    //        }

    //        //Ver si estos dos pasos son necesarios ahora, por el tema del Unload del Padre y el Aviso mediante el Evento
    //        BuildTabContent();
    //        rtsMainTab.SelectedIndex = e.Tab.Index;

    //        //Lanza evento "Hacia Arriba" del Tab Click
    //        GRCTabChanged(e.Tab.Value);
    //    }

    //    protected void rmnSelectionIAException_ItemClick(object sender, Telerik.Web.UI.RadMenuEventArgs e)
    //    {
    //        int radGridClickedRowIndex;
    //        string UId;

    //        //carga la grilla seleccionada
    //        radGridClickedRowIndex = Convert.ToInt32(Request.Form["radGridClickedRowIndexIAException"]);
    //        //carga la tabla seleccionada
    //        UId = Request.Form["radGridClickedTableIdIAException"];

    //        GridTableView tableView;
    //        //carga la tabla en el GridTableView 
    //        tableView = this.Page.FindControl(UId) as GridTableView;
    //        //selecciona el ragistro donde se abrio el menu
    //        ((tableView.Controls[0] as Table).Rows[radGridClickedRowIndex] as GridItem).Selected = true;

    //        //Transfer y EventArgs
    //        Int64 idValue = Convert.ToInt64(_RgdMasterGridExceptions.SelectedValue);
    //        String ItemOptionForExecution = e.Item.Text.ToUpper();
    //        String action;
    //        action = GetMenuAction(e.Item.Value);

    //        GRCGridMenuClick(action, idValue, ItemOptionForExecution, _IdProcess);
    //    }

    //    #endregion
    //}
}