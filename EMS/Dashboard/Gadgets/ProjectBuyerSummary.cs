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
    //public class ProjectBuyerSummary
    public partial class BaseDashboard : BasePage
    {
        #region Generic Method

        private DataTable LoadProjectBuyerSummaryGrid()
        {
            DataTable _dt = new DataTable();
            _dt.TableName = "Root";
            _dt.Columns.Add("IdProject");
            _dt.Columns.Add("ProjectName");
            _dt.Columns.Add("Category");
            _dt.Columns.Add("CampaignStartDate");
            _dt.Columns.Add("Status");
            _dt.Columns.Add("Forecasted");
            _dt.Columns.Add("Calculated");
            _dt.Columns.Add("Deviation");

            //Totals
            Decimal _estimationTotal = 0;
            Decimal _verificationTotal = 0;

            //foreach (Condesus.EMS.Business.PM.Entities.ProjectRole _projectRole in _projectRoles)
            foreach (Condesus.EMS.Business.PF.Entities.ProcessGroupProcess _processGroupProcess in EMSLibrary.User.Dashboard.ProcessGroupProcess.Values)
            {
                Decimal _estimated = _processGroupProcess.TotalEstimatesByScenario(EMSLibrary.User.PerformanceAssessments.Configuration.CalculationScenarioType(Convert.ToInt64(ConfigurationManager.AppSettings["PDDEstimated"])));
                Decimal _current = _processGroupProcess.FirstCalculationResult();
                Decimal _state;
                if (_estimated == 0)
                { _state = 0; }
                else
                { _state = Math.Round((_current / _estimated * 100), 2); }


                //Totals
                _estimationTotal += _estimated;
                _verificationTotal += _current;

                //Busca las propiedades extendidas de categoria y status.
                String _category = String.Empty;
                String _status = String.Empty;
                Condesus.EMS.Business.EP.Entities.ExtendedPropertyValue _extendedProperty;
                _extendedProperty = _processGroupProcess.ExtendedPropertyValue(Convert.ToInt64(ConfigurationManager.AppSettings["IdExtendedPropertyCategory"]));
                if (_extendedProperty != null)
                { _category = _extendedProperty.Value.ToString(); }

                _extendedProperty = _processGroupProcess.ExtendedPropertyValue(Convert.ToInt64(ConfigurationManager.AppSettings["IdExtendedPropertyProjectStatus"]));
                if (_extendedProperty != null)
                { _status = _extendedProperty.Value.ToString(); }

                _dt.Rows.Add(_processGroupProcess.IdProcess,
                               Common.Functions.ReplaceIndexesTags(_processGroupProcess.LanguageOption.Title),
                               Common.Functions.ReplaceIndexesTags(_category),
                               _processGroupProcess.CurrentCampaignStartDate.ToShortDateString(),
                               _status,
                               Math.Round(_estimated, 2),
                               Math.Round(_current, 2),
                               _state);
            }

            Decimal _deviationTotal;
            if (_estimationTotal == 0)
            { _deviationTotal = 0; }
            else
            { _deviationTotal = Math.Round(_verificationTotal / _estimationTotal * 100, 2); }

            _dt.Rows.Add(-1, "<b>Total</b>", "", "", "", "<b>" + Math.Round(_estimationTotal, 2).ToString() + "</b>", "<b>" + Math.Round(_verificationTotal, 2).ToString() + "</b>", "<b>" + _deviationTotal.ToString() + "</b>");

            return _dt;
        }
        protected Table BuildProjectBuyerSummary()
        {
            //Estructura HTML
            Table _container = BuildTable("tblContentForm", "ContentListDashboardGrid");

            RadGrid _projectBuyerSummaryGrid = new RadGrid();

            _projectBuyerSummaryGrid.ID = "ProjectBuyerSummaryGrid";

            _projectBuyerSummaryGrid.SortCommand += new GridSortCommandEventHandler(_projectBuyerSummaryGrid_SortCommand);
            _projectBuyerSummaryGrid.NeedDataSource += new GridNeedDataSourceEventHandler(_projectBuyerSummaryGrid_NeedDataSource);
            _projectBuyerSummaryGrid.PageIndexChanged += new GridPageChangedEventHandler(_projectBuyerSummaryGrid_PageIndexChanged);
            _projectBuyerSummaryGrid.ItemDataBound += new GridItemEventHandler(_projectBuyerSummaryGrid_ItemDataBound);
            _projectBuyerSummaryGrid.ItemCommand += new GridCommandEventHandler(_projectBuyerSummaryGrid_ItemCommand);

            _projectBuyerSummaryGrid.MasterTableView.DataKeyNames = new String[] { "IdProject" };
            _projectBuyerSummaryGrid.AutoGenerateColumns = false;
            _projectBuyerSummaryGrid.AllowPaging = true;
            _projectBuyerSummaryGrid.AllowSorting = true;
            _projectBuyerSummaryGrid.Width = Unit.Percentage(100);
            _projectBuyerSummaryGrid.PageSize = 10;
            _projectBuyerSummaryGrid.Skin = "EMS";
            _projectBuyerSummaryGrid.EnableEmbeddedSkins = false;
            _projectBuyerSummaryGrid.PagerStyle.AlwaysVisible = true;
            _projectBuyerSummaryGrid.PagerStyle.Mode = GridPagerMode.NextPrevAndNumeric;
            ////Seteos para la paginacion de la grilla, ahora es culturizable.
            _projectBuyerSummaryGrid.PagerStyle.Mode = GridPagerMode.NextPrevAndNumeric;
            //_projectBuyerSummaryGrid.MasterTableView.PagerStyle.Mode = GridPagerMode.NextPrevAndNumeric;

            //_projectBuyerSummaryGrid.MasterTableView.PagerStyle.NextPagesToolTip = Resources.Common.GridPagerStyleNextPagesToolTip;    // "Next Pages";
            //_projectBuyerSummaryGrid.MasterTableView.PagerStyle.NextPageText = Resources.Common.GridPagerStyleNextPageText;    // "&gt;";
            //_projectBuyerSummaryGrid.MasterTableView.PagerStyle.NextPageToolTip = Resources.Common.GridPagerStyleNextPageToolTip;  // "Next Page";
            //_projectBuyerSummaryGrid.MasterTableView.PagerStyle.PagerTextFormat = Resources.Common.GridPagerStylePagerTextFormat;  // "Change page: {4} &nbsp;Displaying page {0} of {1}, items {2} to {3} of {5}.";
            //_projectBuyerSummaryGrid.MasterTableView.PagerStyle.PrevPagesToolTip = Resources.Common.GridPagerStylePrevPagesToolTip;    // "Previous Pages";
            //_projectBuyerSummaryGrid.MasterTableView.PagerStyle.PrevPageText = Resources.Common.GrdiPagerStylePrevPageText;    // "&lt;";
            //_projectBuyerSummaryGrid.MasterTableView.PagerStyle.PrevPageToolTip = Resources.Common.GridPagerStylePrevPageToolTip;  // "Previous Page";

            _projectBuyerSummaryGrid.PagerStyle.NextPagesToolTip = Resources.Common.GridPagerStyleNextPagesToolTip; //"Next Pages";
            _projectBuyerSummaryGrid.PagerStyle.NextPageText = Resources.Common.GridPagerStyleNextPageText;    //"&gt;";
            _projectBuyerSummaryGrid.PagerStyle.NextPageToolTip = Resources.Common.GridPagerStyleNextPageToolTip;  // "Next Page";
            _projectBuyerSummaryGrid.PagerStyle.PagerTextFormat = Resources.Common.GridPagerStylePagerTextFormat;  // "Change page: {4} &nbsp;Displaying page {0} of {1}, items {2} to {3} of {5}.";
            _projectBuyerSummaryGrid.PagerStyle.PrevPagesToolTip = Resources.Common.GridPagerStylePrevPagesToolTip;    // "Previous Pages";
            _projectBuyerSummaryGrid.PagerStyle.PrevPageText = Resources.Common.GrdiPagerStylePrevPageText;    // "&lt;";
            _projectBuyerSummaryGrid.PagerStyle.PrevPageToolTip = Resources.Common.GridPagerStylePrevPageToolTip;  // "Previous Page";

            GridTemplateColumn _viewTemplateColumn = CreateTemplateColumn(new MyTemplateLinkButton("View"), GetLocalResourceObject("View").ToString());
            GridBoundColumn _idProjectBoundColumn = CreateBoundColumn(GetLocalResourceObject("Id").ToString(), "IdProject", "IdProject", "IdProject", false);
            GridBoundColumn _projectNameBoundColumn = CreateBoundColumn(GetLocalResourceObject("Title").ToString(), "ProjectName", "ProjectName", "ProjectName", true);
            GridBoundColumn _categoryBoundColumn = CreateBoundColumn(GetLocalResourceObject("Category").ToString(), "Category", "Category", "Category", true);
            GridBoundColumn _campaignStartDateBoundColumn = CreateBoundColumn(GetLocalResourceObject("CampaignStartDate").ToString(), "CampaignStartDate", "CampaignStartDate", "CampaignStartDate", true);
            _campaignStartDateBoundColumn.DataType = System.Type.GetType("System.DateTime");
            GridBoundColumn _statusBoundColumn = CreateBoundColumn(GetLocalResourceObject("Status").ToString(), "Status", "Status", "Status", true);
            GridBoundColumn _forecastedBoundColumn = CreateBoundColumn(GetLocalResourceObject("Forecasted").ToString(), "Forecasted", "Forecasted", "Forecasted", true);
            GridBoundColumn _calculatedBoundColumn = CreateBoundColumn(GetLocalResourceObject("Calculated").ToString(), "Calculated", "Calculated", "Calculated", true);
            GridBoundColumn _deviationBoundColumn = CreateBoundColumn(GetLocalResourceObject("Deviation").ToString(), "Deviation", "Deviation", "Deviation", true);
            GridTemplateColumn _reportTemplateColumn = CreateTemplateColumn(new MyTemplateReport("Report"), GetLocalResourceObject("Report").ToString());

            _projectBuyerSummaryGrid.MasterTableView.Columns.Add(_viewTemplateColumn);
            _projectBuyerSummaryGrid.MasterTableView.Columns.Add(_idProjectBoundColumn);
            _projectBuyerSummaryGrid.MasterTableView.Columns.Add(_projectNameBoundColumn);
            _projectBuyerSummaryGrid.MasterTableView.Columns.Add(_categoryBoundColumn);
            _projectBuyerSummaryGrid.MasterTableView.Columns.Add(_campaignStartDateBoundColumn);
            _projectBuyerSummaryGrid.MasterTableView.Columns.Add(_statusBoundColumn);
            _projectBuyerSummaryGrid.MasterTableView.Columns.Add(_forecastedBoundColumn);
            _projectBuyerSummaryGrid.MasterTableView.Columns.Add(_calculatedBoundColumn);
            _projectBuyerSummaryGrid.MasterTableView.Columns.Add(_deviationBoundColumn);
            _projectBuyerSummaryGrid.MasterTableView.Columns.Add(_reportTemplateColumn);

            TableRow _trGrid = BuildContent(_projectBuyerSummaryGrid);

            _container.Controls.Add(_trGrid);

            InjectShowReportToPrintProjectBuyerSummary();

            return _container;
        }

        #endregion

        #region Events

        protected void _projectBuyerSummaryGrid_ItemCommand(object source, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Select":
                    Int64 _idProject = Convert.ToInt64(((RadGrid)source).Items[e.Item.ItemIndex]["IdProject"].Text);

                    base.NavigatorAddTransferVar("IdProcess", _idProject);

                    NavigateMenuEventArgs _menuArgs = GetNavigateMenuEventArgs(NavigateMenuType.ContextInfoMenu, "DashboardNavigation");
                    Navigate("~/Dashboard/ProyectBuyerSummaryContent.aspx", EMSLibrary.User.ProcessFramework.Map.ProcessGroupProcess(_idProject).LanguageOption.Title, _menuArgs);
                    break;
            }
        }
        protected void _projectBuyerSummaryGrid_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                String _idProject = ((DataRowView)e.Item.DataItem).Row["IdProject"].ToString();

                HtmlImage oimg = (HtmlImage)e.Item.FindControl("rptButton");
                if (!(oimg == null))
                {
                    if (_idProject == "-1")
                    {
                        oimg.Visible = false;
                    }
                    else
                    {
                        oimg.Attributes["onclick"] = string.Format("return ShowReportToPrintProjectBuyerSummary(event, " + _idProject + ");");
                        oimg.Attributes["onmouseover"] = "this.style.cursor = 'pointer'";
                    }
                }

                ImageButton oIB = (ImageButton)e.Item.FindControl("imageButton");
                if (!(oIB == null))
                {
                    if (_idProject == "-1")
                    {
                        oIB.Visible = false;
                    }
                    else
                    {
                        oIB.Attributes["idProject"] = _idProject;
                        oimg.Attributes["onmouseover"] = "this.style.cursor = 'pointer'";
                    }
                }
            }
        }
        protected void _projectBuyerSummaryGrid_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            ((RadGrid)source).MasterTableView.Rebind();
        }
        protected void _projectBuyerSummaryGrid_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            ((RadGrid)source).DataSource = LoadProjectBuyerSummaryGrid();
        }
        protected void _projectBuyerSummaryGrid_SortCommand(object source, GridSortCommandEventArgs e)
        {
            ((RadGrid)source).MasterTableView.Rebind();
        }

        #endregion

        #region Javascript

        
        #endregion
    }
}
