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
using System.Xml.Linq;

namespace Condesus.EMS.WebUI.Dashboard
{
    public partial class ProjectSummary : BasePage
    {
        #region Internal Properties
            private Condesus.EMS.WebUI.Dashboard.Controls.ProjectSummary _ProjectSummary;
        #endregion

        #region Page Event
            protected void Page_Init(object sender, EventArgs e)
            {
                LoadProjectSummary();

                //CheckSecurity();
            }
            protected void Page_Load(object sender, EventArgs e)
            {
                if (!Page.IsPostBack)
                {
                    base.SetNavigator();

                }
            }
        #endregion

        #region Generic Method
            private void LoadProjectSummary()
            {
                _ProjectSummary = (Condesus.EMS.WebUI.Dashboard.Controls.ProjectSummary)(Page.LoadControl("~/Dashboard/Controls/ProjectSummary.ascx"));
                pnlSummary.Controls.Add(_ProjectSummary);
            }        
        #endregion

    }
}
