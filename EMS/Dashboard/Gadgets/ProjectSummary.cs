using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Condesus.WebUI.Navigation;

namespace Condesus.EMS.WebUI.Dashboard
{
    //public class ProjectSummary
    public partial class BaseDashboard : BasePage
    {
        #region Generic Method

        private DataTable LoadProjectSummaryGrid()
        {
            DataTable _dt = new DataTable();
            _dt.TableName = "Root";
            _dt.Columns.Add("IdProject");
            _dt.Columns.Add("ProjectTitle");
            _dt.Columns.Add("Category");
            _dt.Columns.Add("Completed");
            _dt.Columns.Add("CampaignStartDate");
            _dt.Columns.Add("State");

            //set primary keys.
            DataColumn[] keys = new DataColumn[1];
            DataColumn column = new DataColumn();
            column = _dt.Columns["IdProject"];
            keys[0] = column;
            _dt.PrimaryKey = keys;


            foreach (Condesus.EMS.Business.PF.Entities.ProcessGroupProcess _processGroupProcess in EMSLibrary.User.Dashboard.ProcessGroupProcess.Values)
            {
                    String _category = String.Empty;
                    Condesus.EMS.Business.EP.Entities.ExtendedPropertyValue _extendedProperty;
                    _extendedProperty = _processGroupProcess.ExtendedPropertyValue(Convert.ToInt64(ConfigurationManager.AppSettings["IdExtendedPropertyCategory"]));
                    if (_extendedProperty != null)
                    { _category = _extendedProperty.Value.ToString(); }

                    _dt.Rows.Add(_processGroupProcess.IdProcess,
                                   Common.Functions.ReplaceIndexesTags(_processGroupProcess.LanguageOption.Title),
                                   Common.Functions.ReplaceIndexesTags(_category),
                                   _processGroupProcess.Completed,
                                   _processGroupProcess.CurrentCampaignStartDate.ToShortDateString(),
                                   _processGroupProcess.State);
            }
            return _dt;
        }
        private void LoadProjectSummaryChildrenNodes(DataTable _dt, Condesus.EMS.Business.PF.Entities.ProcessClassification processClassification)
        {
            foreach (Condesus.EMS.Business.PF.Entities.ProcessClassification _processClassification in processClassification.ChildrenClassifications.Values)
            {
                //Load children nodes
                LoadProjectSummaryChildrenNodes(_dt, _processClassification);
            }
            foreach (Condesus.EMS.Business.PF.Entities.ProcessGroupProcess _processRoot in processClassification.ChildrenElements.Values)
            {
                _dt.Rows.Add(_processRoot.IdProcess,
                                            _processRoot.LanguageOption.Title,
                                            _processRoot.Completed,
                                            _processRoot.State);
            }
        }
        private RadContextMenu CreateMenuSelectionProjectSummary()
        {
            //Menu de Seleccion
            RadContextMenu _projectSummaryMenuSelection = new RadContextMenu();

            _projectSummaryMenuSelection.ItemClick += new RadMenuEventHandler(_projectSummaryMenuSelection_ItemClick);
            _projectSummaryMenuSelection.OnClientItemClicking = "rmnSelectionProjectSummary_OnClientItemClickedHandler";

            RadMenuItem rmiView = new RadMenuItem(Resources.Common.mnuView);
            rmiView.Value = "View";
            _projectSummaryMenuSelection.Items.Add(rmiView);

            return _projectSummaryMenuSelection;
        }
        protected Table BuildProjectSummary()
        {
            //Estructura HTML
            Table _container = BuildTable("tblContentForm", "ContentListDashboardGrid");

            //RadContextMenu _projectSummaryMenuSelection = CreateMenuSelectionProjectSummary();

            RadGrid _projectSummaryGrid = new RadGrid();

            _projectSummaryGrid.ID = "ProjectSummaryGrid";

            _projectSummaryGrid.SortCommand += new GridSortCommandEventHandler(_projectSummaryGrid_SortCommand);
            _projectSummaryGrid.NeedDataSource += new GridNeedDataSourceEventHandler(_projectSummaryGrid_NeedDataSource);
            _projectSummaryGrid.PageIndexChanged += new GridPageChangedEventHandler(_projectSummaryGrid_PageIndexChanged);
            _projectSummaryGrid.ItemDataBound += new GridItemEventHandler(_projectSummaryGrid_ItemDataBound);
            _projectSummaryGrid.ItemCommand += new GridCommandEventHandler(_projectSummaryGrid_ItemCommand);

            _projectSummaryGrid.MasterTableView.DataKeyNames = new String[] { "IdProject" };
            _projectSummaryGrid.AutoGenerateColumns = false;
            _projectSummaryGrid.AllowPaging = true;
            _projectSummaryGrid.AllowSorting = true;
            _projectSummaryGrid.Width = Unit.Percentage(100);
            _projectSummaryGrid.PageSize = 10;
            _projectSummaryGrid.Skin = "EMS";
            _projectSummaryGrid.EnableEmbeddedSkins = false;
            _projectSummaryGrid.PagerStyle.AlwaysVisible = true;
            //_projectSummaryGrid.PagerStyle.Mode = GridPagerMode.NextPrevAndNumeric;
            //Seteos para la paginacion de la grilla, ahora es culturizable.
            _projectSummaryGrid.PagerStyle.Mode = GridPagerMode.NextPrevAndNumeric;
            //_projectSummaryGrid.MasterTableView.PagerStyle.Mode = GridPagerMode.NextPrevAndNumeric;

            //_projectSummaryGrid.MasterTableView.PagerStyle.NextPagesToolTip = Resources.Common.GridPagerStyleNextPagesToolTip;    // "Next Pages";
            //_projectSummaryGrid.MasterTableView.PagerStyle.NextPageText = Resources.Common.GridPagerStyleNextPageText;    // "&gt;";
            //_projectSummaryGrid.MasterTableView.PagerStyle.NextPageToolTip = Resources.Common.GridPagerStyleNextPageToolTip;  // "Next Page";
            //_projectSummaryGrid.MasterTableView.PagerStyle.PagerTextFormat = Resources.Common.GridPagerStylePagerTextFormat;  // "Change page: {4} &nbsp;Displaying page {0} of {1}, items {2} to {3} of {5}.";
            //_projectSummaryGrid.MasterTableView.PagerStyle.PrevPagesToolTip = Resources.Common.GridPagerStylePrevPagesToolTip;    // "Previous Pages";
            //_projectSummaryGrid.MasterTableView.PagerStyle.PrevPageText = Resources.Common.GrdiPagerStylePrevPageText;    // "&lt;";
            //_projectSummaryGrid.MasterTableView.PagerStyle.PrevPageToolTip = Resources.Common.GridPagerStylePrevPageToolTip;  // "Previous Page";

            _projectSummaryGrid.PagerStyle.NextPagesToolTip = Resources.Common.GridPagerStyleNextPagesToolTip; //"Next Pages";
            _projectSummaryGrid.PagerStyle.NextPageText = Resources.Common.GridPagerStyleNextPageText;    //"&gt;";
            _projectSummaryGrid.PagerStyle.NextPageToolTip = Resources.Common.GridPagerStyleNextPageToolTip;  // "Next Page";
            _projectSummaryGrid.PagerStyle.PagerTextFormat = Resources.Common.GridPagerStylePagerTextFormat;  // "Change page: {4} &nbsp;Displaying page {0} of {1}, items {2} to {3} of {5}.";
            _projectSummaryGrid.PagerStyle.PrevPagesToolTip = Resources.Common.GridPagerStylePrevPagesToolTip;    // "Previous Pages";
            _projectSummaryGrid.PagerStyle.PrevPageText = Resources.Common.GrdiPagerStylePrevPageText;    // "&lt;";
            _projectSummaryGrid.PagerStyle.PrevPageToolTip = Resources.Common.GridPagerStylePrevPageToolTip;  // "Previous Page";

            GridTemplateColumn _viewTemplateColumn = CreateTemplateColumn(new MyTemplateLinkButton("View"), GetLocalResourceObject("View").ToString());
            //GridButtonColumn _viewButtonColumn = CreateButtonColumn("~/RadControls/Grid/Skins/EMS/SortMenuGrid.gif",GridButtonColumnType.LinkButton,"Select","View");
            GridBoundColumn _idProjectBoundColumn = CreateBoundColumn(GetLocalResourceObject("Id").ToString(), "IdProject", "IdProject", "IdProject", false);
            GridBoundColumn _projectTitleBoundColumn = CreateBoundColumn(GetLocalResourceObject("Title").ToString(), "ProjectTitle", "ProjectTitle", "ProjectTitle", true);
            GridBoundColumn _categoryBoundColumn = CreateBoundColumn(GetLocalResourceObject("Category").ToString(), "Category", "Category", "Category", true);
            GridBoundColumn _completedBoundColumn = CreateBoundColumn(GetLocalResourceObject("Completed").ToString(), "Completed", "Completed", "Completed", true);
            GridBoundColumn _campaignStartDateBoundColumn = CreateBoundColumn(GetLocalResourceObject("CampaignStartDate").ToString(), "CampaignStartDate", "CampaignStartDate", "CampaignStartDate", true);
            _campaignStartDateBoundColumn.DataType = System.Type.GetType("System.DateTime");
            GridBoundColumn _stateBoundColumn = CreateBoundColumn(GetLocalResourceObject("Status").ToString(), "State", "State", "State", true);
            GridTemplateColumn _reportTemplateColumn = CreateTemplateColumn(new MyTemplateReport("Report"), GetLocalResourceObject("Report").ToString());

            _projectSummaryGrid.MasterTableView.Columns.Add(_viewTemplateColumn);
            _projectSummaryGrid.MasterTableView.Columns.Add(_idProjectBoundColumn);
            _projectSummaryGrid.MasterTableView.Columns.Add(_projectTitleBoundColumn);
            _projectSummaryGrid.MasterTableView.Columns.Add(_categoryBoundColumn);
            _projectSummaryGrid.MasterTableView.Columns.Add(_completedBoundColumn);
            _projectSummaryGrid.MasterTableView.Columns.Add(_campaignStartDateBoundColumn);
            _projectSummaryGrid.MasterTableView.Columns.Add(_stateBoundColumn);
            _projectSummaryGrid.MasterTableView.Columns.Add(_reportTemplateColumn);

            //TableRow _trMenu = BuildContent(_projectSummaryMenuSelection);
            TableRow _trGrid = BuildContent(_projectSummaryGrid);

            //_container.Controls.Add(_trMenu);
            _container.Controls.Add(_trGrid);

            //base.InjectRmnSelectionItemClickHandler("ProjectSummary");
            InjectShowReportProjectSummary();

            //InjectShowMenuProjectSummary(_projectSummaryMenuSelection.ClientID, _projectSummaryGrid.ClientID);
            //InjectShowMenuProyectSummary(_projectSummaryMenuSelection.ClientID); //496

            //InjectRowContextMenuProjectSummary(_projectSummaryMenuSelection.ClientID);
            //base.InjectRowContextMenu(_projectSummaryMenuSelection.ClientID);

            return _container;
        }

        #endregion
        
        #region Events

        protected void _projectSummaryGrid_ItemCommand(object source, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Select":
                    Int64 _idProject = Convert.ToInt64(((RadGrid)source).Items[e.Item.ItemIndex]["IdProject"].Text);

                    base.NavigatorAddTransferVar("IdProcess", _idProject);
                    base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.PF.ProcessGroupProcess);
                    base.NavigatorAddTransferVar("EntityNameGrid", Common.ConstantsEntitiesName.PF.ProcessGroupProcesses);
                    base.NavigatorAddTransferVar("EntityNameContextInfo", Common.ConstantsEntitiesName.PF.Process);
                    base.NavigatorAddTransferVar("EntityNameContextElement", Common.ConstantsEntitiesName.PF.ProcessGroupProcess);

                    NavigateMenuEventArgs _menuArgs = GetNavigateMenuEventArgs(NavigateMenuType.ContextInfoMenu, "DashboardNavigation");
                    Navigate(GetPageViewerByEntity(Common.ConstantsEntitiesName.PF.ProcessGroupProcess), Common.ConstantsEntitiesName.PF.ProcessGroupProcess + " " + EMSLibrary.User.ProcessFramework.Map.ProcessGroupProcess(_idProject).LanguageOption.Title, _menuArgs);

                    break;
            }
        }
        protected void _projectSummaryGrid_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                String _idProject = ((DataRowView)e.Item.DataItem).Row["IdProject"].ToString();

                HtmlImage oimg = (HtmlImage)e.Item.FindControl("rptButton");
                if (!(oimg == null))
                {
                    oimg.Attributes["onclick"] = string.Format("return ShowReportProjectSummary(event, " + _idProject + ");");
                    oimg.Attributes["onmouseover"] = "this.style.cursor = 'pointer'";
                }

                ImageButton oIB = (ImageButton)e.Item.FindControl("imageButton");
                if (!(oIB == null))
                {
                    oIB.Attributes["idProject"] = _idProject;
                    oIB.Attributes["onmouseover"] = "this.style.cursor = 'pointer'";
                }
            }
        }
        protected void _projectSummaryGrid_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            ((RadGrid)source).MasterTableView.Rebind();
        }
        protected void _projectSummaryGrid_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            ((RadGrid)source).DataSource = LoadProjectSummaryGrid();
        }
        protected void _projectSummaryGrid_SortCommand(object source, GridSortCommandEventArgs e)
        {
            ((RadGrid)source).MasterTableView.Rebind();
        }

        protected void _projectSummaryMenuSelection_ItemClick(object sender, RadMenuEventArgs e)
        {
            int radGridClickedRowIndex;
            string UId;

            //carga la grilla seleccionada
            radGridClickedRowIndex = Convert.ToInt32(Request.Form["radGridClickedRowIndex"]);
            //carga la tabla seleccionada
            UId = Request.Form["radGridClickedTableId"];

            GridTableView tableView;
            //carga la tabla en el GridTableView 
            tableView = this.Page.FindControl(UId) as GridTableView;
            //selecciona el ragistro donde se abrio el menu
            ((tableView.Controls[0] as Table).Rows[radGridClickedRowIndex] as GridItem).Selected = true;

            switch (e.Item.Value)
            {
                case "View":  //VIEW
                    //Context.Items.Add("IdValue", rgdMasterGrid.SelectedValue);
                    ////Context.Items.Add("ItemOptionSelected", "VIEW");


                    //Int64 _idProcess = Convert.ToInt64(rgdMasterGrid.SelectedValue);
                    //Int64 _idProcessClassification = EMSLibrary.DirectoryServices.Organization(IdOrganization).ProcessGroupProjects[_idProcess].ProcessClassifications.First().Key;

                    //////Ahora debe ir al Mapa de Procesos.
                    ////Context.Items.Add("IdProcess", rgdMasterGrid.SelectedValue);
                    ////Context.Items.Add("IdProcessClassification", _idProcessClassification);
                    ////Server.Transfer("~/ManagementTools/ProcessesMap/ProcessTest.aspx");

                    //String _idProcessTransfer = "IdProcess=" + rgdMasterGrid.SelectedValue.ToString();
                    //String _idProcessClassificationTransfer = "IdProcessClassification=" + _idProcessClassification.ToString();
                    //Response.Redirect("~/ManagementTools/ProcessesMap/ProcessTest.aspx?" + _idProcessTransfer + '&' + _idProcessClassificationTransfer);

                    ////Server.Transfer("~/Dashboard/ProjectDetails.aspx");
                    break;
            }
        }

        #endregion

        #region Javascript

        protected void InjectShowReportProjectSummary()
        {
            System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

            _sbBuffer.Append(OpenHtmlJavaScript());

            _sbBuffer.Append("function ShowReportProjectSummary(e, idProject)                                                                                        \n");
            _sbBuffer.Append("{                                                                                                                                         \n");
            //Abre una nueva ventana con el reporte.
            _sbBuffer.Append("  var _IEXPLORER = 'Microsoft Internet Explorer';                                             \n");
            _sbBuffer.Append("  var _FIREFOX = 'Netscape';                                                                  \n");
            _sbBuffer.Append("  var _BrowserName = navigator.appName;                                                       \n");

            //_sbBuffer.Append("  StopEvent(e);                                                                                   \n");
            _sbBuffer.Append("  if (_BrowserName == _IEXPLORER)                                                             \n");
            _sbBuffer.Append("  {   //IE and Opera                                                                          \n");
            _sbBuffer.Append("      var newWindow = window.open(" + Convert.ToChar(34) + "http://" +Request.ServerVariables["HTTP_HOST"].ToString() + (!string.IsNullOrEmpty(Request.ApplicationPath) ? Request.ApplicationPath : "") +  "/Reports/ProjectReports.aspx?IdProject=" + Convert.ToChar(34) + " + idProject, 'Reports', 'toolbar=yes,location=yes,resizable=yes,status=yes,menubar=yes,scrollbars=yes');      \n");
            _sbBuffer.Append("  }                                                                                           \n");
            _sbBuffer.Append("  else                                                                                        \n");
            _sbBuffer.Append("  {   //FireFox                                                                               \n");
            _sbBuffer.Append("      var newWindow = window.parent.open(" + Convert.ToChar(34) + "http://" +Request.ServerVariables["HTTP_HOST"].ToString() + (!string.IsNullOrEmpty(Request.ApplicationPath) ? Request.ApplicationPath : "") +  "/Reports/ProjectReports.aspx?IdProject=" + Convert.ToChar(34) + " + idProject, 'Reports', 'toolbar=yes,location=yes,resizable=yes,status=yes,menubar=yes,scrollbars=yes');      \n");
            _sbBuffer.Append("  }                                                                                           \n");
            //_sbBuffer.Append("  var newWindow = window.open(" + Convert.ToChar(34) + "http://" +Request.ServerVariables["HTTP_HOST"].ToString() + (!string.IsNullOrEmpty(Request.ApplicationPath) ? Request.ApplicationPath : "") +  "/Reports/ProjectReports.aspx?IdProject=" + Convert.ToChar(34) + " + idProject, 'Reports', 'toolbar=yes,location=yes,resizable=yes,status=yes,menubar=yes,scrollbars=yes');                     \n");
            _sbBuffer.Append("  newWindow.focus();                                                                                                                  \n");
            //frena la ejecucion de todo y queda esperando a la confirmacion en el popup.
            _sbBuffer.Append("  StopEvent(e);     //window.event.returnValue = false;                                                                                               \n");
            _sbBuffer.Append("}                                                                                                                                     \n");

            _sbBuffer.Append(CloseHtmlJavaScript());

            InjectJavascript("JS_ShowReportProjectSummary", _sbBuffer.ToString());
        }
        #endregion
    }
}
