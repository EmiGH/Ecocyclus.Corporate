using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using Telerik.Web.UI;

namespace Condesus.EMS.WebUI.Dashboard
{
    public partial class ExceptionsClosed : BasePage
    {
        #region Internal Properties
        #endregion

        #region Page Event
            protected void Page_Load(object sender, EventArgs e)
            {
                if (!Page.IsPostBack)
                {
                    base.SetNavigator();

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

            #region Grid Methods
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
                _dt.Columns.Add("IdException");
                _dt.Columns.Add("Date");
                _dt.Columns.Add("Source");
                _dt.Columns.Add("Status");

                //Dictionary<Int64, Condesus.EMS.Business.IA.Entities.Exception> _exceptions = EMSLibrary.User.DirectoryServices.Map.Organization(IdOrganization).Dashboard.ProcessExceptionClose;

                //foreach (Condesus.EMS.Business.IA.Entities.Exception _exception in _exceptions.Values)
                //{
                //    _dt.Rows.Add(_exception.IdException, _exception.ExceptionDate, _exception.AssociateTask().LanguageOption.Title, _exception.ExceptionState.LanguageOption.Name);
                //}
                return _dt;
            }
        #endregion





    }
}
