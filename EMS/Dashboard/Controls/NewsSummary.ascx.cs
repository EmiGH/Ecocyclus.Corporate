using System;
using System.Collections;
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

namespace Condesus.EMS.WebUI.Dashboard.Controls
{
    public partial class NewsSummary : System.Web.UI.UserControl
    {
        #region Internal Properties
        #endregion

        #region Page Event
            protected void Page_Load(object sender, EventArgs e)
            {
                if (!Page.IsPostBack)
                {
                    
                }
            }
            
            #region Grid Events
                protected void rgdMasterGrid_SortCommand(object source, Telerik.Web.UI.GridSortCommandEventArgs e)
                {
                    rgdMasterGrid.MasterTableView.Rebind();
                }
                protected void rgdMasterGrid_PageIndexChanged(object source, GridPageChangedEventArgs e)
                {
                    rgdMasterGrid.MasterTableView.Rebind();
                }
                protected void rgdMasterGrid_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
                {
                    //Carga todos los ROOT en la grilla.
                    rgdMasterGrid.DataSource = LoadGrid();
                }
            #endregion 
        #endregion

        #region Generic Method
            private DataTable LoadGrid()
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

    }
}