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
using Condesus.WebUI.Navigation;

using EBPA = Condesus.EMS.Business.PA.Entities;

namespace Condesus.EMS.WebUI
{
    //public class BaseListManage : BasePage
    public partial class BasePage : System.Web.UI.Page
    {
        #region Internal Properties
            private String _EntityNameGrid;
            private Panel _SearchPanel = null;
            private RadGrid _MasterGrid = null;
            private RadGrid _ViewerGrid = null;
            private Dictionary<String, KeyValuePair<String, Type>> _SearchColumns = new Dictionary<String, KeyValuePair<String, Type>>();
            private Boolean _IsGridPageIndexChanged = false;
            private Boolean _IsReturnFromDelete = false;
        #endregion

        #region External Properties
            protected String EntityNameGrid
            {
                get { return _EntityNameGrid; }
                set { _EntityNameGrid = value; }
            }
            protected Boolean IsGridPageIndexChanged
            {
                get { return _IsGridPageIndexChanged; }
                set { _IsGridPageIndexChanged = value; }
            }
            protected Boolean IsReturnFromDelete
            {
                get { return _IsReturnFromDelete; }
                set { _IsReturnFromDelete = value; }
            }
        #endregion

        #region Search
            protected Panel BuildSearchContent(Panel pnlSearch)
            {
                String _imagePath = "~/Skins/Images/";

                _SearchPanel = pnlSearch;
                
                Panel _retContainer = new Panel();
              
                if (_SearchColumns.Count > 0)
                {
                    Table _tblContentForm = new Table();
                    _tblContentForm.CssClass = "ContentSearch";
                    _tblContentForm.CellSpacing = 0;
                    _tblContentForm.CellPadding = 0;
                    _tblContentForm.ID = "tblContentSearch";

                    /* Each row of the search panel */
                    TableRow _currentRow = new TableRow();

                    TableCell _tblCell = new TableCell();
                    _tblCell.ID = "tblSearch";
                    _tblCell.CssClass = "Header";

                    LinkButton _lnkCaption = new LinkButton();
                    _lnkCaption.ID = "lblSearchTitle";
                    _lnkCaption.Text = Resources.Common.lblSearch;
                    _lnkCaption.CssClass = "Link";
                    _lnkCaption.OnClientClick = "HideShowSearchContent(this, event)";
                    _tblCell.Controls.Add(_lnkCaption);
                    _currentRow.Cells.Add(_tblCell);


                    LinkButton _lnkSearch = new LinkButton();
                    _lnkSearch.ID = "lnkSearch";
                    _lnkSearch.CssClass = "LinkSearch";
                    _lnkSearch.ToolTip = Resources.Common.lnkSeachToolTip;
                    _lnkSearch.Text = Resources.Common.lnkSeach;
                    _lnkSearch.Click += new EventHandler(btnSearch_Click);
                    _lnkSearch.Style.Add("display", "none");

                    _tblCell.Controls.Add(_lnkSearch);
                    _currentRow.Cells.Add(_tblCell);

                    _tblContentForm.Controls.Add(_currentRow);

                    TableRow _searchContent = new TableRow();
                    _searchContent.ID = "searchContent";
                    _searchContent.Style.Add("display", "none");

                    TableCell _tblCell1 = new TableCell();


                    Panel _pnlContentFormSearch = new Panel();
                    _pnlContentFormSearch.CssClass = "Form";

                    Table _tblContentFormSearchContainer = new Table();
                    _tblContentFormSearchContainer.CellSpacing = 0;
                    _tblContentFormSearchContainer.CellPadding = 0;

                   //TableRow _currentRow1 = new TableRow();

                    /* How many fields in each row */
                    Int16 _searchCellsPerRow = 4;
                    Int16 _currentCellsInRow = 1;

                    /* Search Controls for each column */
                    TextBox _txtSearchField;
                    Label _lblSearchField;

                    /* First Fields Row */

                    _currentRow = new TableRow();
                    foreach (KeyValuePair<String, KeyValuePair<String, Type>> _searchColumn in _SearchColumns)
                    {
                     

                        if (_currentCellsInRow > _searchCellsPerRow)
                        {
                            _currentCellsInRow=1;
                            _tblContentFormSearchContainer.Rows.Add(_currentRow);
                            _currentRow = new TableRow();
                        }
                        else
                        {
                            _currentCellsInRow += 1;
                        }

                        _tblCell = new TableCell();
                        
                        _lblSearchField = new Label();
                        _lblSearchField.ID = "lbl" + _searchColumn.Key;
                        _lblSearchField.Text = _searchColumn.Value.Key;
                        _tblCell.Controls.Add(_lblSearchField);
                        
                        _txtSearchField = new TextBox();
                        _txtSearchField.ID = "txt" + _searchColumn.Key;
                        _txtSearchField.Attributes.Add("ColumnName", _searchColumn.Key);
                        _txtSearchField.Attributes.Add("ColumnType", _searchColumn.Value.Value.ToString());
                        _tblCell.Controls.Add(_txtSearchField);
                        
                        _currentRow.Cells.Add(_tblCell);
                        
                    }
                    _tblContentFormSearchContainer.Rows.Add(_currentRow);
                    _pnlContentFormSearch.Controls.Add(_tblContentFormSearchContainer);
                    _tblCell1.Controls.Add(_pnlContentFormSearch);
                    _searchContent.Controls.Add(_tblCell1);
 

                    _tblContentForm.Rows.Add(_searchContent);
    
                    _retContainer.Controls.Add(_tblContentForm);

                    


                }
                    return _retContainer;
                
            }
    
            private String BuildFilterExpression(TextBox txtFilter)
            {
                String _filter = String.Empty;

                if (txtFilter.Text != String.Empty)
                {
                    _filter += txtFilter.Attributes["ColumnName"].ToString() + GetOperatorByColumnDataType(txtFilter.Text, System.Type.GetType(txtFilter.Attributes["ColumnType"].ToString()));
                }
                return _filter;
            }
            private void BuildFilterExpressionChild(Control panelSearch, ref String filter)
            {
                //Recorre recursivamente los controles hijos.
                foreach (Control _control in panelSearch.Controls)
                {
                    if (_control is TextBox)
                    {
                        TextBox _buff = ((TextBox)_control);
                        filter += BuildFilterExpression(_buff);
                    }
                    if (_control.Controls.Count > 0)
                    {
                        BuildFilterExpressionChild(_control, ref filter);
                    }
                }
            }
            private void FilterGrid(Control panelSearch)
            {
                String _filter = String.Empty;
                FilterExpressionGrid = String.Empty;
                foreach (Control _control in panelSearch.Controls)
                {
                    if (_control is TextBox)
                    {
                        TextBox _buff = ((TextBox)_control);
                        _filter += BuildFilterExpression(_buff);
                    }
                    if (_control.Controls.Count > 0)
                    {
                        //Si tiene controles hijos, entonces recorre los mismos en una funcion recursiva.
                        BuildFilterExpressionChild(_control, ref _filter);
                    }
                }
                if (_filter != String.Empty)
                    //Finalmente se guarda en la variable filtro el filtro a realizar.
                    { FilterExpressionGrid = _filter.Substring(0, _filter.Length - 3); }
            }
            private String GetOperatorByColumnDataType(String filterValue, Type columnDataType)
            {
                String _ret=String.Empty;

                if (filterValue != String.Empty)
                {
                    switch (columnDataType.ToString())
                    {
                        case "System.Int64":
                            _ret = " = " + filterValue + " OR ";
                            break;

                        default:
                            _ret = " like '%" + filterValue + "%' OR ";
                            break;
                    }
                }
                return _ret;
            }

            //protected void InjectHideShowSearchContent(TableRow _searchContent)
            //{
            //    System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

            //    _sbBuffer.Append(OpenHtmlJavaScript());

            //    _sbBuffer.Append("function HideShowSearchContent(sender, e)                                \n");
            //    _sbBuffer.Append("{                                                                     \n");
            //    _sbBuffer.Append("    var trSearchContent = $get('" + _searchContent.ClientID + "');    \n");
            //    _sbBuffer.Append("    var btnSearch = $get('ctl00_ContentMain_imgSearchControl');                                                                  \n");
            //    _sbBuffer.Append("    if(trSearchContent.style.display == 'none')                       \n");
            //    _sbBuffer.Append("    {                                                                 \n");
            //    _sbBuffer.Append("        trSearchContent.style.display = 'block';                       \n");
            //    _sbBuffer.Append("        btnSearch.className = 'ImagesControlSearch';                                                                 \n");
            //    _sbBuffer.Append("    }                                                                 \n");
            //    _sbBuffer.Append("    else                                                              \n");
            //    _sbBuffer.Append("    {                                                                 \n");
            //    _sbBuffer.Append("        trSearchContent.style.display = 'none';                      \n");
            //    _sbBuffer.Append("        btnSearch.className = 'ImagesControlSearchClose';                                                                 \n");
            //    _sbBuffer.Append("    }                                                                 \n");
            //    _sbBuffer.Append("                                                                      \n");
            //    _sbBuffer.Append("     StopEvent(e);     //window.event.returnValue = false;                                \n");
            //    _sbBuffer.Append("}                                                                     \n");

            //    _sbBuffer.Append(CloseHtmlJavaScript());

            //    InjectJavascript("JS_LISTMANAGE_HideShowSearchContent", _sbBuffer.ToString());
            //}

            #region Event
                protected void btnSearch_Click(object sender, EventArgs e)
                {
                    //Arma el filtro
                    FilterGrid(_SearchPanel);
                    if (String.IsNullOrEmpty(FilterExpressionGrid))
                    {
                        ((LinkButton)sender).Text = Resources.Common.lnkSeach;
                        ((LinkButton)sender).ToolTip = Resources.Common.lnkSeachToolTip;
                    }
                    else
                    {
                        ((LinkButton)sender).Text = Resources.Common.lnkSeachActive;
                        ((LinkButton)sender).ToolTip = Resources.Common.lnkSeachToolTipActive;
                    }
                    //Hace el rebind de la grilla para que se filtre con lo solicitado.
                    _MasterGrid.MasterTableView.Rebind();
                }
            #endregion
        #endregion

        #region List Manage
            /// <summary>
            /// Este metodo retorna los parametros armados, para cada registro de la grilla.
            /// </summary>
            /// <param name="rgdListManage"></param>
            /// <returns>Un<c>String</c></returns>
            protected String GetPKCompostFromItem(RadGrid rgdListManage, Int32 _selectedIndex)
            {
                String _params = String.Empty;
                for (int i = 0; i < rgdListManage.MasterTableView.DataKeyNames.Count(); i++)
                {
                    //Obtiene el KeyName y su Valor del registo seleccionado en la grilla.
                    String _keyName = rgdListManage.MasterTableView.DataKeyNames[i].Trim();
                    String _keyValue = rgdListManage.MasterTableView.DataKeyValues[_selectedIndex][_keyName].ToString();
                    //Finalmente concatena los key en un string de parametros.
                    _params += _keyName + "=" + _keyValue + "&";
                }

                return _params.Substring(0, _params.Length - 1); ;
            }
            /// <summary>
            /// Este metodo retorna los parametros armados, del registro seleccionado, con la estructura que esperan el resto de las paginas (Key=Value&)
            /// </summary>
            /// <param name="rgdListManage"></param>
            /// <returns>Un<c>String</c></returns>
            protected String BuildParamsFromListManageSelected(RadGrid rgdListManage)
            {
                String _params = String.Empty;
                if (rgdListManage.SelectedItems.Count > 0)  //Si no hay nada en la grilla, me salteo esto para que no de error.
                {
                    for (int i = 0; i < rgdListManage.MasterTableView.DataKeyNames.Count(); i++)
                    {
                        //Obtiene el KeyName y su Valor del registo seleccionado en la grilla.
                        Int32 _selectedIndex = rgdListManage.SelectedItems[0].ItemIndex;
                        String _keyName = rgdListManage.MasterTableView.DataKeyNames[i].Trim();
                        String _keyValue = rgdListManage.MasterTableView.DataKeyValues[_selectedIndex][_keyName].ToString();
                        //Finalmente concatena los key en un string de parametros.
                        _params += _keyName + "=" + _keyValue + "&";
                    }

                    return _params.Substring(0, _params.Length - 1);
                }
                else
                {
                    return _params;
                }
            }
            /// <summary>
            /// Este metodo retorna los parametros armados, de todos los registros chequeados, con la estructura que esperan el resto de las paginas (Key=Value&)
            /// </summary>
            /// <param name="rgdListManage"></param>
            /// <returns>Un<c>String</c></returns>
            protected String BuildParamsFromListManageChecked(RadGrid rgdListManage, GridDataItem row)
            {
                String _params = String.Empty;
                if (rgdListManage.SelectedItems.Count > 0)  //Si no hay nada en la grilla, me salteo esto para que no de error.
                {
                    for (int i = 0; i < rgdListManage.MasterTableView.DataKeyNames.Count(); i++)
                    {
                        //Obtiene el KeyName y su Valor del registo seleccionado en la grilla.
                        Int32 _checkedIndex = row.ItemIndex;
                        String _keyName = rgdListManage.MasterTableView.DataKeyNames[i].Trim();
                        String _keyValue = rgdListManage.MasterTableView.DataKeyValues[_checkedIndex][_keyName].ToString();
                        //Finalmente concatena los key en un string de parametros.
                        _params += _keyName + "=" + _keyValue + "&";
                    }

                    return _params.Substring(0, _params.Length - 1); ;
                }
                else
                {
                    return _params;
                }
            }

            protected RadGrid BuildListViewerContent(String entityName)
            {
                _ViewerGrid = new RadGrid();
                //Prepara la grilla...
                InitListViewerGrid(_ViewerGrid, entityName);
                SetDataKeyNameListManage(_ViewerGrid, entityName);
                SetGridColumnListViewer(_ViewerGrid, entityName);

                return _ViewerGrid;

                //Panel _retContainer = new Panel();
                //_retContainer.CssClass = "contentformDashboard";

                //RadGrid _rdgMasterGrid = new RadGrid();
                //Prepara la grilla...
                //InitListViewerGrid(_rdgMasterGrid, nameGridID);
                //SetDataKeyNameListManage(_rdgMasterGrid);
                //SetGridColumnListViewer(_rdgMasterGrid);

                //_retContainer.Controls.Add(_rdgMasterGrid);
                //return _retContainer;
            }
            protected RadGrid BuildListManageContent(String entityName, Boolean showImgSelect, Boolean showCheck, Boolean showOpenFile, Boolean showOpenChart, Boolean showOpenSeries, Boolean allowSearchableGrid)
            {
                //Panel _retContainer = new Panel();
                //_retContainer.CssClass = "contentformDashboard";

                _MasterGrid = new RadGrid();
                //Prepara la grilla...
                InitListManageGrid(_MasterGrid, entityName);
                SetDataKeyNameListManage(_MasterGrid, entityName);
                SetGridColumnListManage(_MasterGrid, entityName, showImgSelect, showCheck, showOpenFile, showOpenChart, showOpenSeries, allowSearchableGrid);
            
                return _MasterGrid;

                //_retContainer.Controls.Add(_MasterGrid);
                //return _retContainer;
            }

            /// <summary>
            /// Este metodo permite obtener el Atributo Nombre/titulo, etc para mostrar en los Viewers.
            /// </summary>
            /// <returns></returns>
            protected String GetPageTitleForViewer()
            {
                try
                {
                    return DataTableListManage[EntityNameGrid].ExtendedProperties["PageTitle"].ToString();
                }
                catch { return String.Empty; }
            }
            protected Boolean IsGridWithGrouping(String entityName)
            {
                //Si la entidad que viene, es una entidad para ver con Grouping en la grilla, devuelve true
                switch (entityName)
                {
                    case Common.ConstantsEntitiesName.DB.PlannedTasks:
                    case Common.ConstantsEntitiesName.DB.OpenedExceptions:
                    case Common.ConstantsEntitiesName.DB.WorkingExceptions:
                    case Common.ConstantsEntitiesName.DB.ActiveTasks:
                    case Common.ConstantsEntitiesName.DB.OverDueTasks:
                    case Common.ConstantsEntitiesName.DB.AllMyExecutions:
                    case Common.ConstantsEntitiesName.DB.ProcessTaskExecutionExecuted:
                        return true;
                        break;

                    default:
                        return false;
                        break;
                }
            }

            #region Propiedades de la Grilla
                private void InitListManageGrid(RadGrid rgdMasterGrid, String entityName)
                {
                    rgdMasterGrid.ID = "rgdMasterGridListManage" + entityName;
                    rgdMasterGrid.AllowPaging = true;
                    rgdMasterGrid.AllowSorting = true;
                    rgdMasterGrid.Skin = "EMS";
                    rgdMasterGrid.EnableEmbeddedSkins = false;
                    rgdMasterGrid.Width = Unit.Percentage(100);
                    rgdMasterGrid.AutoGenerateColumns = false;
                    rgdMasterGrid.GridLines = System.Web.UI.WebControls.GridLines.None;
                    rgdMasterGrid.ShowStatusBar = false;
                    rgdMasterGrid.PageSize = 18;
                    rgdMasterGrid.AllowMultiRowSelection = false;
                    rgdMasterGrid.PagerStyle.AlwaysVisible = true;
                    rgdMasterGrid.MasterTableView.Width = Unit.Percentage(100);
                    rgdMasterGrid.EnableViewState = true;

                    //Crea los metodos de la grilla (Server)
                    rgdMasterGrid.NeedDataSource += new GridNeedDataSourceEventHandler(rgdMasterGrid_NeedDataSource);
                    rgdMasterGrid.PageIndexChanged += new GridPageChangedEventHandler(rgdMasterGrid_PageIndexChanged);
                    rgdMasterGrid.SortCommand += new GridSortCommandEventHandler(rgdMasterGrid_SortCommand);

                    //Crea los metodos de la grilla (Cliente)
                    rgdMasterGrid.ClientSettings.AllowExpandCollapse = false;
                    rgdMasterGrid.AllowMultiRowSelection = false;
                    rgdMasterGrid.ClientSettings.Selecting.AllowRowSelect = true;
                    rgdMasterGrid.ClientSettings.Selecting.EnableDragToSelectRows = false;
                    rgdMasterGrid.MasterTableView.HierarchyLoadMode = GridChildLoadMode.Client;
                    rgdMasterGrid.MasterTableView.SortExpressions.AllowMultiColumnSorting = true;

                    //Define los atributos de la MasterGrid
                    rgdMasterGrid.MasterTableView.Name = "gridMasterListManage" + entityName;
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
                    rgdMasterGrid.MasterTableView.EditMode = GridEditMode.InPlace;
                    rgdMasterGrid.MasterTableView.NoMasterRecordsText = Resources.Common.NoRecordsToDisplay;

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

                }
                private void InitListViewerGrid(RadGrid rgdMasterGrid, String nameGridID)
                {
                    rgdMasterGrid.ID = "rgdMasterGridListViewer" + nameGridID;
                    rgdMasterGrid.AllowPaging = false;
                    rgdMasterGrid.AllowSorting = false;
                    rgdMasterGrid.Skin = "EMS";
                    rgdMasterGrid.Width = Unit.Percentage(100);
                    rgdMasterGrid.AutoGenerateColumns = false;
                    rgdMasterGrid.GridLines = System.Web.UI.WebControls.GridLines.None;
                    rgdMasterGrid.ShowStatusBar = false;
                    rgdMasterGrid.PageSize = 18;
                    rgdMasterGrid.AllowMultiRowSelection = false;
                    rgdMasterGrid.PagerStyle.AlwaysVisible = true;
                    rgdMasterGrid.MasterTableView.Width = Unit.Percentage(100);
                    rgdMasterGrid.EnableViewState = true;
                    rgdMasterGrid.EnableEmbeddedSkins = false;

                    //Crea los metodos de la grilla (Server)
                    //rgdMasterGrid.ItemCreated += new GridItemEventHandler(rgdMasterGrid_ItemCreated);

                    rgdMasterGrid.NeedDataSource += new GridNeedDataSourceEventHandler(rgdMasterGrid_NeedDataSource);
                    rgdMasterGrid.SortCommand += new GridSortCommandEventHandler(rgdMasterGrid_SortCommand);
                    rgdMasterGrid.ItemDataBound += new GridItemEventHandler(rgdMasterGrid_ItemDataBound);
                    rgdMasterGrid.ItemCreated += new GridItemEventHandler(rgdMasterGrid_ItemCreated);

                    //Crea los metodos de la grilla (Cliente)
                    rgdMasterGrid.ClientSettings.AllowExpandCollapse = false;
                    rgdMasterGrid.AllowMultiRowSelection = false;
                    rgdMasterGrid.ClientSettings.Selecting.AllowRowSelect = true;
                    rgdMasterGrid.ClientSettings.Selecting.EnableDragToSelectRows = false;
                    rgdMasterGrid.MasterTableView.HierarchyLoadMode = GridChildLoadMode.Client;

                    //Define los atributos de la MasterGrid
                    rgdMasterGrid.MasterTableView.Name = "gridMasterListViewer" + nameGridID;
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
                    rgdMasterGrid.MasterTableView.EditMode = GridEditMode.InPlace;
                    
                    rgdMasterGrid.HeaderStyle.Font.Bold = false;
                    rgdMasterGrid.HeaderStyle.Font.Italic = false;
                    rgdMasterGrid.HeaderStyle.Font.Overline = false;
                    rgdMasterGrid.HeaderStyle.Font.Strikeout = false;
                    rgdMasterGrid.HeaderStyle.Font.Underline = false;
                    rgdMasterGrid.HeaderStyle.Wrap = true;
                }
            #endregion

            #region Estructura de la Grilla
                private void SetDataKeyNameListManage(RadGrid rgdMasterGrid, String entityName)
                {
                    // Crea el array de DataColumn.
                    DataColumn[] _primaryKeyColumns;
                    ////Si es un Viewer, y ademas viene con nombre de entidad, entonces usa el nombre de la entidad para acceder al Dictionary.
                    //String _nameGridIdViewer = "rgdMasterGridListViewer";
                    //if ((rgdMasterGrid.ID.Contains(_nameGridIdViewer)) && (rgdMasterGrid.ID.Length > _nameGridIdViewer.Length))
                    //{
                    //    String _entityName = rgdMasterGrid.ID.Substring(_nameGridIdViewer.Length);
                    //    //Obtiene la lista de columnas definidas como PrimaryKey.
                    //    _primaryKeyColumns = DataTableListManage[_entityName].PrimaryKey;
                    //}
                    //else
                    //{
                    //    //Obtiene la lista de columnas definidas como PrimaryKey.
                    //    _primaryKeyColumns = DataTableListManage[_EntityNameGrid].PrimaryKey;
                    //}
                    _primaryKeyColumns = DataTableListManage[entityName].PrimaryKey;

                    //Define un array de String para insertarle los nombre de las columnas
                    String[] _pkColumnName = new String[_primaryKeyColumns.Length];
                    //Recorre todas las columnas y las agrega al array de string
                    for (int i = 0; i < _primaryKeyColumns.Length; i++)
                    {
                        _pkColumnName[i] = _primaryKeyColumns[i].ColumnName;
                    }
                    //Finalmente asigna el key en la grilla.
                    rgdMasterGrid.MasterTableView.DataKeyNames = _pkColumnName;
                }
                private void SetGridColumnListManage(RadGrid rgdMasterGrid, String entityName, Boolean showImgSelect, Boolean showCheck, Boolean showOpenFile, Boolean showOpenChart, Boolean showOpenSeries, Boolean allowSearchableGrid)
                {
                    GridBoundColumn _rgdBoundCol = null;
                    GridTemplateColumn _rdgTempCol = null;
                    GridButtonColumn _rgdButtonCol = null;
                    GridBinaryImageColumn _rgdBinaryImageCol = null;

                    #region Template Columns 
                        //Crea y agrega las columna de tipo template

                        if (showImgSelect)
                        {
                            //Icono de seleccion (abre menu)
                            String _templateColumnName = "";
                            _rdgTempCol = new GridTemplateColumn();
                            _rdgTempCol.ItemTemplate = new MyTemplateSelection(_templateColumnName);
                            _rdgTempCol.HeaderText = _templateColumnName;
                            _rdgTempCol.Resizable = false;
                            _rdgTempCol.HeaderStyle.Width = Unit.Pixel(13);
                            _rdgTempCol.ItemStyle.Width = Unit.Pixel(13);
                            rgdMasterGrid.MasterTableView.Columns.Add(_rdgTempCol);
                        }
                        if (showCheck)
                        {
                            //Check item para el delete
                            String _templateColumnNameCheck = "";
                            _rdgTempCol = new GridTemplateColumn();
                            _rdgTempCol.ItemTemplate = new MyTemplateSelectionItemCheck(_templateColumnNameCheck);
                            _rdgTempCol.ItemStyle.Width = Unit.Pixel(21);
                            _rdgTempCol.Resizable = false;
                            _rdgTempCol.HeaderStyle.Width = Unit.Pixel(21);
                            rgdMasterGrid.MasterTableView.Columns.Add(_rdgTempCol);
                        }
                    #endregion

                    #region Column of DataTable
                        foreach (DataColumn _column in DataTableListManage[entityName].Columns)
                        {
                            //Si esta columna esta definida para que salga ordenada, lo hace...
                            if ((Boolean)_column.ExtendedProperties[Common.Constants.ExtendedPropertiesColumnDataTable.IsSortedBy])
                            {
                                GridSortExpression _gridSortExpression = new GridSortExpression();
                                _gridSortExpression.FieldName = _column.ColumnName;
                                GridSortOrder _enumOrderType = (GridSortOrder)_column.ExtendedProperties[Common.Constants.ExtendedPropertiesColumnDataTable.SortOrderType];
                                _gridSortExpression.SetSortOrder(_enumOrderType.ToString());

                                rgdMasterGrid.MasterTableView.SortExpressions.AllowMultiColumnSorting = true;
                                rgdMasterGrid.MasterTableView.SortExpressions.AddSortExpression(_gridSortExpression);
                            }
                            //Si esta indicado que es una celda de tipo Link...
                            if ((Boolean)_column.ExtendedProperties[Common.Constants.ExtendedPropertiesColumnDataTable.IsCellLink])
                            {
                                _rgdButtonCol = new GridButtonColumn();
                                _rgdButtonCol.DataTextField = _column.ColumnName;
                                _rgdButtonCol.HeaderText = _column.Caption;
                                String commandArgs = "EntityName=" + _column.ExtendedProperties[Common.Constants.ExtendedPropertiesColumnDataTable.EntityName].ToString()
                                    + "&EntityNameGrid=" + _column.ExtendedProperties[Common.Constants.ExtendedPropertiesColumnDataTable.EntityNameGrid].ToString()
                                    + "&EntityNameContextInfo=" + _column.ExtendedProperties[Common.Constants.ExtendedPropertiesColumnDataTable.EntityNameContextInfo].ToString()
                                    + "&EntityNameContextElement=" + _column.ExtendedProperties[Common.Constants.ExtendedPropertiesColumnDataTable.EntityNameContextElement].ToString();

                                _rgdButtonCol.CommandArgument = commandArgs;
                                _rgdButtonCol.Display = (Boolean)_column.ExtendedProperties[Common.Constants.ExtendedPropertiesColumnDataTable.DisplayManage];
                                rgdMasterGrid.MasterTableView.Columns.Add(_rgdButtonCol);
                            }
                            else
                            {
                                if (((Boolean)_column.ExtendedProperties[Common.Constants.ExtendedPropertiesColumnDataTable.IsBinaryImage]))
                                {
                                    TableItemStyle _headerStyle;
                                    GridTemplateColumn _rdgTempColBinaryImage = new GridTemplateColumn();
                                    _rdgTempColBinaryImage.UniqueName = "Image";
                                    _rdgTempColBinaryImage.ItemTemplate = new MyTemplateBinaryImage(Resources.CommonListManage.Image);
                                    _headerStyle = _rdgTempCol.HeaderStyle;
                                    _rdgTempColBinaryImage.HeaderText = String.Empty;   // Resources.CommonListManage.Image;
                                    _rdgTempColBinaryImage.AllowFiltering = false;
                                    rgdMasterGrid.MasterTableView.Columns.Add(_rdgTempColBinaryImage);
                                }
                                else
                                {
                                    //Sino hace lo de siempre...
                                    _rgdBoundCol = new GridBoundColumn();
                                    _rgdBoundCol.DataField = _column.ColumnName;
                                    _rgdBoundCol.UniqueName = _column.ColumnName;
                                    _rgdBoundCol.HeaderText = _column.Caption;
                                    _rgdBoundCol.Display = (Boolean)_column.ExtendedProperties[Common.Constants.ExtendedPropertiesColumnDataTable.DisplayManage];
                                    rgdMasterGrid.MasterTableView.Columns.Add(_rgdBoundCol);
                                    //Si se indica que la grilla a construir tiene search...
                                    if (allowSearchableGrid)
                                    {
                                        if ((Boolean)_column.ExtendedProperties[Common.Constants.ExtendedPropertiesColumnDataTable.IsSearchable])
                                        {
                                            _SearchColumns.Add(_column.ColumnName, new KeyValuePair<String, Type>(_column.Caption, _column.DataType));
                                        }
                                    }
                                }
                            }
                        }
                    #endregion

                    #region Template Column Open PopUp (Open File, Chart, Series)
                        if (showOpenFile)
                        {
                            TableItemStyle _headerStyle;
                            _rdgTempCol = new GridTemplateColumn();
                            _rdgTempCol.UniqueName = "OpenFile";
                            _rdgTempCol.ItemTemplate = new MyTemplateReport(Resources.CommonListManage.OpenFile);
                            _rdgTempCol.ItemStyle.Width = Unit.Pixel(21);
                            _headerStyle = _rdgTempCol.HeaderStyle;
                            _headerStyle.Width = Unit.Pixel(21);
                            _rdgTempCol.HeaderText = Resources.CommonListManage.OpenFile;
                            _rdgTempCol.AllowFiltering = false;
                            rgdMasterGrid.MasterTableView.Columns.Add(_rdgTempCol);
                        }
                        if (showOpenChart)
                        {
                            TableItemStyle _headerStyle;
                            _rdgTempCol = new GridTemplateColumn();
                            _rdgTempCol.UniqueName = "TemplateColumnChart";
                            _rdgTempCol.ItemTemplate = new DBImageButtonTemplate("btnChartLink");
                            _rdgTempCol.ItemStyle.Width = Unit.Pixel(21);
                            _headerStyle = _rdgTempCol.HeaderStyle;
                            _headerStyle.Width = Unit.Pixel(21);
                            _rdgTempCol.HeaderText = Resources.CommonListManage.OpenChart;  // "Chart";
                            _rdgTempCol.AllowFiltering = false;
                            rgdMasterGrid.MasterTableView.Columns.Add(_rdgTempCol);

                        }
                        if (showOpenSeries)
                        {
                            TableItemStyle _headerStyle;
                            _rdgTempCol = new GridTemplateColumn();
                            _rdgTempCol.UniqueName = "TemplateColumnSeries";
                            _rdgTempCol.ItemTemplate = new DBImageButtonTemplate("btnSeriesLink");
                            _rdgTempCol.ItemStyle.Width = Unit.Pixel(21);
                            _headerStyle = _rdgTempCol.HeaderStyle;
                            _headerStyle.Width = Unit.Pixel(21);
                            _rdgTempCol.HeaderText = Resources.CommonListManage.OpenSeries;     // "Series";
                            _rdgTempCol.AllowFiltering = false;
                            rgdMasterGrid.MasterTableView.Columns.Add(_rdgTempCol);
                        }
                    #endregion
                }
                private void SetGridColumnListViewer(RadGrid rgdMasterGrid, String entityName)
                {
                    GridBoundColumn _rgdBoundCol = null;
                    //foreach (DataColumn _column in DataTableListManage[_EntityNameGrid].Columns)
                    foreach (DataColumn _column in DataTableListManage[entityName].Columns)
                    {
                        _rgdBoundCol = new GridBoundColumn();

                        if (_column.ColumnName == "Value")
                        {
                            GridButtonColumn _rgdButtonCol = new GridButtonColumn();
                            _rgdButtonCol.UniqueName = _column.ColumnName;
                            _rgdButtonCol.DataTextField = _column.ColumnName;
                            _rgdButtonCol.HeaderText = _column.Caption;
                            _rgdButtonCol.Display = (Boolean)_column.ExtendedProperties[Common.Constants.ExtendedPropertiesColumnDataTable.DisplayManage];
                            
                            rgdMasterGrid.MasterTableView.Columns.Add(_rgdButtonCol);
                        }
                        else
                        {
                            _rgdBoundCol.UniqueName = _column.ColumnName;
                            _rgdBoundCol.DataField = _column.ColumnName;
                            _rgdBoundCol.HeaderText = _column.Caption;
                            _rgdBoundCol.Display = (Boolean)_column.ExtendedProperties[Common.Constants.ExtendedPropertiesColumnDataTable.DisplayManage];

                            rgdMasterGrid.MasterTableView.Columns.Add(_rgdBoundCol);
                        }
                        

                        ////Si esta indicado que es una celda de tipo Link...
                        //if ((Boolean)_column.ExtendedProperties[Common.Constants.ExtendedPropertiesColumnDataTable.IsCellLink])
                        //{
                        //    _rgdBoundCol.DataFormatString = "<div style='text-decoration: underline;'>{0}</div>";
                        //}
                        //_rgdBoundCol.ItemStyle.Width = Unit.Percentage(100);
                    }
                    //La ultima columna debe quedar sin 100%....
                    //rgdMasterGrid.MasterTableView.Columns[rgdMasterGrid.MasterTableView.Columns.Count - 1].ItemStyle.Width = Unit.Empty;
                }
            #endregion
            
            #region Events
                protected void rgdMasterGrid_ItemDataBound(object sender, GridItemEventArgs e)
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
                protected void rgdMasterGrid_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
                {
                    //ItemCreated(sender, e);
                    if (e.Item is Telerik.Web.UI.GridDataItem)
                    {
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

                        HtmlImage oimg = (HtmlImage)e.Item.FindControl("selButton");
                        if (!(oimg == null))
                        {
                            String _clientID = ((RadGrid)sender).ClientID;
                            oimg.Attributes["onclick"] = string.Format("return ShowMenu" + _clientID + "(event, " + e.Item.ItemIndex + ", '" + e.Item.Parent.Parent.UniqueID + "');");
                          //oimg.Attributes["onclick"] = string.Format("return ShowMenu(event, " + e.Item.ItemIndex + ", '" + e.Item.Parent.Parent.UniqueID + "');");
                        }
                        foreach (GridColumn _col in ((RadGrid)sender).Columns)
                        {
                            if ((_col.GetType().Name == "GridButtonColumn") && (_GridLinkButtonClick != null))
                            {
                                //Si la columna es de Tipo GridButtonColumn...
                                Boolean _execPostback = true;   //Se usa para que el JS retorne True si queremos que ejecute el evento en el Server.

                                GridDataItem dataItem = (GridDataItem)e.Item;
                                LinkButton lbtNavigateView = (LinkButton)dataItem[_col.UniqueName].Controls[0];
                                lbtNavigateView.Click += new EventHandler(_GridLinkButtonClick);

                                if (_col.UniqueName == "Value")
                                {
                                    String _valueLink = String.Empty;
                                    try
                                    {
                                        _valueLink = ((DataRowView)e.Item.DataItem).Row["KeyValueLink"].ToString();
                                    }
                                    catch { }

                                    if (!_valueLink.Contains("EntityName"))
                                    {
                                        //Esto es un View, y este registro no es Link.
                                        _execPostback = false; //Deshabilitamos el postback del linkbutton.
                                        //Saca el subrayado del texto.
                                        lbtNavigateView.Style.Add("text-decoration", "none");
                                        //dataItem.Style.Add("text-decoration", "none");
                                    }
                                }
                                RadGrid _rgdGridViewer = (RadGrid)sender;
                                //Agrega el evento Cliente JS, para que seleccione el registro y continue o no, con el PostBack.
                                lbtNavigateView.Attributes["onclick"] = string.Format("return ClientSelectRow" + _rgdGridViewer.ClientID + "(event, " + e.Item.ItemIndex + ", '" + e.Item.Parent.Parent.UniqueID + "', " + _execPostback.ToString().ToLower() + ");");
                            }
                            else
                            {
                                if (_col.UniqueName == "Value")
                                {
                                    GridDataItem dataItem = (GridDataItem)e.Item;
                                    if (dataItem[_col.UniqueName].Controls.Count > 0)
                                    {
                                        LinkButton lbtNavigateView = (LinkButton)dataItem[_col.UniqueName].Controls[0];
                                        //Saca el subrayado del texto.
                                        lbtNavigateView.Style.Add("text-decoration", "none");
                                    }
                                }
                            }
                        }
                    }
                }
                protected void rgdMasterGrid_SortCommand(object sender, Telerik.Web.UI.GridSortCommandEventArgs e)
                {
                    RadGrid _rgdMasterGrid = (RadGrid)sender;
                    _rgdMasterGrid.MasterTableView.Rebind();
                }
                protected void rgdMasterGrid_PageIndexChanged(object sender, GridPageChangedEventArgs e)
                {
                    IsGridPageIndexChanged = true;
                    RadGrid _rgdMasterGrid = (RadGrid)sender;
                    _rgdMasterGrid.MasterTableView.Rebind();
                }
                protected void rgdMasterGrid_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
                {
                    RadGrid _rgdMasterGrid = (RadGrid)sender;

                    //Si es un Viewer, y ademas viene con nombre de entidad, entonces usa el nombre de la entidad para acceder al Dictionary.
                    String _nameGridIdViewer = "rgdMasterGridListViewer";
                    String _nameGridIdManager = "rgdMasterGridListManage";
                    
                    //Ya sea manage o view, saca de ahi el nombre de la entidad.
                    if (((_rgdMasterGrid.ID.Contains(_nameGridIdViewer)) && (_rgdMasterGrid.ID.Length > _nameGridIdViewer.Length)) || ((_rgdMasterGrid.ID.Contains(_nameGridIdManager)) && (_rgdMasterGrid.ID.Length > _nameGridIdManager.Length)))
                    {
                        String _entityName = _rgdMasterGrid.ID.Substring(_nameGridIdViewer.Length);
                        try
                        {
                            DataRow[] _dataRow = DataTableListManage[_entityName].Select(FilterExpressionGrid);
                            DataTable _dt = DataTableListManage[_entityName].Clone();
                            for (Int64 i = 0; i < _dataRow.Length; i++)
                            {
                                _dt.ImportRow(_dataRow[i]);
                            }

                            _rgdMasterGrid.DataSource = _dt;
                            //_rgdMasterGrid.DataSource = DataTableListManage[_entityName].Select(FilterExpressionGrid);
                        }
                        catch
                        {   //Si no existe el campo de busqueda, carga sin filtro
                            _rgdMasterGrid.DataSource = DataTableListManage[_entityName];
                        }
                    }
                    else
                    {
                        //Ya sea un viewer comun o cualquier otra cosa, es el STD.
                        try
                        {
                            DataRow[] _dataRow = DataTableListManage[EntityNameGrid].Select(FilterExpressionGrid);
                            DataTable _dt = DataTableListManage[EntityNameGrid].Clone();
                            for (Int64 i = 0; i < _dataRow.Length; i++)
                            {
                                _dt.ImportRow(_dataRow[i]);
                            }
                            _rgdMasterGrid.DataSource = _dt;
                            //_rgdMasterGrid.DataSource = DataTableListManage[EntityNameGrid].Select(FilterExpressionGrid);
                        }
                        catch
                        {   //Si no existe el campo de busqueda, carga sin filtro
                            _rgdMasterGrid.DataSource = DataTableListManage[EntityNameGrid];
                        }
                    }
                }
            #endregion
        #endregion

        #region Context Menu
                protected void BuildManageContextMenu(ref RadContextMenu rmiContextMenu, RadMenuEventHandler menuEventHandler, Boolean showDelete, Boolean showEdit, Boolean showView, Boolean showExecution, Boolean showCompute, Boolean showCloseException, Boolean showTreatException, Boolean showCreateException)
                {
                    //Boolean _isEnabled = (entity != null);
                    ////Si aun no existe la entidad, no debe poner nada en las opciones generales.
                    //if (_isEnabled)
                    //{
                    if (showExecution)
                    {
                        RadMenuItem _rmiExecution = new RadMenuItem(Resources.Common.mnuExecution);
                        _rmiExecution.Value = "rmiEdit";
                        Common.Functions.DoRadItemSecurity(_rmiExecution, true);
                        rmiContextMenu.Items.Add(_rmiExecution);
                    }
                    if (showEdit)
                    {
                        RadMenuItem _rmiEdit = new RadMenuItem(Resources.Common.mnuEdit);
                        _rmiEdit.Value = "rmiEdit";
                        Common.Functions.DoRadItemSecurity(_rmiEdit, true);
                        rmiContextMenu.Items.Add(_rmiEdit);
                    }
                    if (showView)
                    {
                        RadMenuItem _rmiView = new RadMenuItem(Resources.Common.mnuView);
                        _rmiView.Value = "rmiView";
                        Common.Functions.DoRadItemSecurity(_rmiView, true);
                        rmiContextMenu.Items.Add(_rmiView);
                    }

                    if (showDelete)
                    {
                        RadMenuItem _rmiDelete = new RadMenuItem(Resources.Common.mnuDelete);
                        _rmiDelete.Value = "rmiDelete";
                        Common.Functions.DoRadItemSecurity(_rmiDelete, true);
                        rmiContextMenu.Items.Add(_rmiDelete);
                    }
                    if (showCompute)
                    {
                        RadMenuItem _rmiCompute = new RadMenuItem(Resources.Common.mnuCompute);
                        _rmiCompute.Value = "rmiCompute";
                        Common.Functions.DoRadItemSecurity(_rmiCompute, true);
                        rmiContextMenu.Items.Add(_rmiCompute);
                    }
                    if (showCloseException)
                    {
                        RadMenuItem _rmiCloseException = new RadMenuItem(Resources.Common.mnuCloseException);
                        _rmiCloseException.Value = "rmiCloseException";
                        Common.Functions.DoRadItemSecurity(_rmiCloseException, true);
                        rmiContextMenu.Items.Add(_rmiCloseException);
                    } 
                    if (showTreatException)
                    {
                        RadMenuItem _rmiTreatException = new RadMenuItem(Resources.Common.mnuTreatException);
                        _rmiTreatException.Value = "rmiTreatException";
                        Common.Functions.DoRadItemSecurity(_rmiTreatException, true);
                        rmiContextMenu.Items.Add(_rmiTreatException);
                    } 
                    if (showCreateException)
                    {
                        RadMenuItem _rmiCreateException = new RadMenuItem(Resources.Common.mnuCreateException);
                        _rmiCreateException.Value = "rmiCreateException";
                        Common.Functions.DoRadItemSecurity(_rmiCreateException, true);
                        rmiContextMenu.Items.Add(_rmiCreateException);
                    }

                    rmiContextMenu.ItemClick += menuEventHandler;
                    rmiContextMenu.OnClientItemClicking = "rmnSelection_OnClientItemClickedHandler";
                    rmiContextMenu.OnClientShowing = "rmnSelection_OnClientShowing";

                    //}
                }
        #endregion

        #region Security
            private bool EntityHasSecurity(String entityNameID)
            {
                //Si el nombre de la entidad que viene no esta en este switch, entonces no usa seguridad...
                switch (entityNameID)
                {
                    case Common.ConstantsEntitiesName.DS.OrganizationClassification:
                    case Common.ConstantsEntitiesName.DS.Organization:
                    case Common.ConstantsEntitiesName.PF.ProcessClassification:
                    case Common.ConstantsEntitiesName.PF.ProcessGroupProcess:
                    case Common.ConstantsEntitiesName.PA.IndicatorClassification:
                    case Common.ConstantsEntitiesName.PA.Indicator:
                    case Common.ConstantsEntitiesName.KC.ResourceClassification:
                    case Common.ConstantsEntitiesName.KC.Resource:
                    case Common.ConstantsEntitiesName.IA.ProjectClassification:
                    case Common.ConstantsEntitiesName.RM.RiskClassification:
                        return true;

                    default:
                        return false;
                }
            }
            protected void BuildPropertySecuritySystemMenu(String entityNameID, RadMenuEventHandler menuEventHandler)
            {
                Boolean _isEnabled = EntityHasSecurity(entityNameID);
                //Si aun no existe la entidad, no debe poner nada en el menu de Seguridad
                if (_isEnabled)
                {
                    var _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
                    _itemsMenu.Add("rmiSSJobTitles", new KeyValuePair<String, Boolean>(Resources.Common.mnuSSJobTitle, _isEnabled));
                    _itemsMenu.Add("rmiSSPerson", new KeyValuePair<String, Boolean>(Resources.Common.mnuSSPerson, _isEnabled));

                    
                    //RadMenu _rmnuSecuritySystem = BuildSecuritySystemMenu(_itemsMenu);
                    //RadMenu _rmnuSecuritySystem = BuildSecuritySystemMenu(GetOptionMenuByEntity(entityNameID + "_MenuSecurity", ManageEntityParams, true));
                    RadMenu _rmnuSecuritySystem = BuildSecuritySystemMenu(GetOptionMenuByEntity(entityNameID + "_MenuSecurity", ManageEntityParams, true));
                    _rmnuSecuritySystem.ItemClick += new RadMenuEventHandler(menuEventHandler);
                }
            }
        #endregion

        protected NavigateMenuEventArgs BuildMenuEventArgs(String entityClassName, String entityName, NavigateMenuType menuType)
        {
            var _menuArgs = GetBaseMenuArgs(entityClassName, entityName);
            return new Condesus.WebUI.Navigation.NavigateMenuEventArgs(menuType, _menuArgs);
        }
        protected NavigateMenuEventArgs BuildMenuEventArgs(String entityClassName, String entityName, NavigateMenuType menuType, NavigateMenuAction menuAction)
        {
            var _menuArgs = GetBaseMenuArgs(entityClassName, entityName);
            return new Condesus.WebUI.Navigation.NavigateMenuEventArgs(menuType, menuAction, _menuArgs);
        }
        private Dictionary<String, String> GetBaseMenuArgs(String entityClassName, String entityName)
        {
            var _menuArgs = new Dictionary<String, String>();
            _menuArgs.Add("EntityClass", entityClassName);
            _menuArgs.Add("EntityName", entityName);

            return _menuArgs;
        }

    }
}
