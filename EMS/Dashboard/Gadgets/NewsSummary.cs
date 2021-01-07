using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Condesus.EMS.WebUI.Dashboard
{
    //public class NewsSummary
    public partial class BaseDashboard : BasePage
    {
        #region Generic Method
        private DataTable LoadNewsSummaryGrid()
        {
            DataTable _dt = new DataTable();
            _dt.TableName = "Root";
            _dt.Columns.Add("IdProject");
            _dt.Columns.Add("ProjectTitle");
            _dt.Columns.Add("Date");
            _dt.Columns.Add("Comment");

            return _dt;
        }
        #endregion

        protected RadGrid BuildNewsSummary()
        {
            //Estructura HTML

            RadGrid _newsSummaryGrid = new RadGrid();

            _newsSummaryGrid.SortCommand += new GridSortCommandEventHandler(_newsSummaryGrid_SortCommand);
            _newsSummaryGrid.NeedDataSource += new GridNeedDataSourceEventHandler(_newsSummaryGrid_NeedDataSource);
            _newsSummaryGrid.PageIndexChanged += new GridPageChangedEventHandler(_newsSummaryGrid_PageIndexChanged);

            _newsSummaryGrid.MasterTableView.DataKeyNames = new String[] { "IdProject" };
            _newsSummaryGrid.AutoGenerateColumns = false;
            //_newsSummaryGrid.AllowPaging = true;
            _newsSummaryGrid.AllowPaging = false;
            _newsSummaryGrid.AllowSorting = true;
            _newsSummaryGrid.Width = Unit.Percentage(100);
            _newsSummaryGrid.PageSize = 10;
            _newsSummaryGrid.Skin = "EMS";
            _newsSummaryGrid.EnableEmbeddedSkins = false;
            _newsSummaryGrid.PagerStyle.AlwaysVisible = true;

            //_newsSummaryGrid.PagerStyle.Mode = GridPagerMode.NextPrev;
            ////Seteos para la paginacion de la grilla, ahora es culturizable.
            //_newsSummaryGrid.PagerStyle.Mode = GridPagerMode.NextPrevAndNumeric;
            //_newsSummaryGrid.MasterTableView.PagerStyle.Mode = GridPagerMode.NextPrevAndNumeric;

            //_newsSummaryGrid.MasterTableView.PagerStyle.NextPagesToolTip = Resources.Common.GridPagerStyleNextPagesToolTip;    // "Next Pages";
            //_newsSummaryGrid.MasterTableView.PagerStyle.NextPageText = Resources.Common.GridPagerStyleNextPageText;    // "&gt;";
            //_newsSummaryGrid.MasterTableView.PagerStyle.NextPageToolTip = Resources.Common.GridPagerStyleNextPageToolTip;  // "Next Page";
            //_newsSummaryGrid.MasterTableView.PagerStyle.PagerTextFormat = Resources.Common.GridPagerStylePagerTextFormat;  // "Change page: {4} &nbsp;Displaying page {0} of {1}, items {2} to {3} of {5}.";
            //_newsSummaryGrid.MasterTableView.PagerStyle.PrevPagesToolTip = Resources.Common.GridPagerStylePrevPagesToolTip;    // "Previous Pages";
            //_newsSummaryGrid.MasterTableView.PagerStyle.PrevPageText = Resources.Common.GrdiPagerStylePrevPageText;    // "&lt;";
            //_newsSummaryGrid.MasterTableView.PagerStyle.PrevPageToolTip = Resources.Common.GridPagerStylePrevPageToolTip;  // "Previous Page";

            //_newsSummaryGrid.PagerStyle.NextPagesToolTip = Resources.Common.GridPagerStyleNextPagesToolTip; //"Next Pages";
            //_newsSummaryGrid.PagerStyle.NextPageText = Resources.Common.GridPagerStyleNextPageText;    //"&gt;";
            //_newsSummaryGrid.PagerStyle.NextPageToolTip = Resources.Common.GridPagerStyleNextPageToolTip;  // "Next Page";
            //_newsSummaryGrid.PagerStyle.PagerTextFormat = Resources.Common.GridPagerStylePagerTextFormat;  // "Change page: {4} &nbsp;Displaying page {0} of {1}, items {2} to {3} of {5}.";
            //_newsSummaryGrid.PagerStyle.PrevPagesToolTip = Resources.Common.GridPagerStylePrevPagesToolTip;    // "Previous Pages";
            //_newsSummaryGrid.PagerStyle.PrevPageText = Resources.Common.GrdiPagerStylePrevPageText;    // "&lt;";
            //_newsSummaryGrid.PagerStyle.PrevPageToolTip = Resources.Common.GridPagerStylePrevPageToolTip;  // "Previous Page";


            GridBoundColumn _idProjectBoundColumn = CreateBoundColumn(GetLocalResourceObject("Id").ToString(), "IdProject", "IdProject", "IdProject", false);
            GridBoundColumn _projectTitleBoundColumn = CreateBoundColumn(GetLocalResourceObject("Title").ToString(), "ProjectTitle", "ProjectTitle", "ProjectTitle", true);
            GridBoundColumn _dateBoundColumn = CreateBoundColumn(GetLocalResourceObject("Date").ToString(), "Date", "Date", "Date", true);
            GridBoundColumn _commentBoundColumn = CreateBoundColumn(GetLocalResourceObject("Comment").ToString(), "Comment", "Comment", "Comment", true);

            _newsSummaryGrid.MasterTableView.Columns.Add(_idProjectBoundColumn);
            _newsSummaryGrid.MasterTableView.Columns.Add(_projectTitleBoundColumn);
            _newsSummaryGrid.MasterTableView.Columns.Add(_dateBoundColumn);
            _newsSummaryGrid.MasterTableView.Columns.Add(_commentBoundColumn);

            return _newsSummaryGrid;
        }
        
        #region Events

        protected void _newsSummaryGrid_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            ((RadGrid)source).MasterTableView.Rebind();
        }
        protected void _newsSummaryGrid_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            ((RadGrid)source).DataSource = LoadNewsSummaryGrid();
        }
        protected void _newsSummaryGrid_SortCommand(object source, GridSortCommandEventArgs e)
        {
            ((RadGrid)source).MasterTableView.Rebind();
        }

        #endregion
    }
}
