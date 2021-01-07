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

namespace Condesus.EMS.WebUI.PA
{
    public partial class Default : BaseProperties
    {
        #region Page Load & Init
            protected override void OnInit(EventArgs e)
            {
                base.OnInit(e);
                LoadTextLabels();
            }
            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);

                //Activa el dashboard, y Desactiva el Search
                ((ImageButton)FwMasterPage.FindControl("btnGlobalToolbarHomeDashboardFW")).CssClass = "GlobalToolbarDashboardOpen";
                ((ImageButton)FwMasterPage.FindControl("btnGlobalToolbarSearch")).CssClass = "GlobalToolbarSearch";
            }
            protected override void SetPagetitle()
            {
                base.PageTitle = Resources.CommonMenu.DefaultPASubTitle;
            }
        #endregion

        #region Private Methods
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonMenu.DefaultPASubTitle;
                lblDescription.Text = Resources.CommonMenu.DefaultPADescription;
                lblOrganization.Text = Resources.CommonMenu.DefaultPAOrganization;
                lblSubTitle.Text = Resources.CommonMenu.DefaultPASubTitle;
                lblTitle.Text = Resources.CommonMenu.AdministrationTool;
                lblTitleDescription.Text = Resources.CommonMenu.DefaultPATitleDescription;
            }
        #endregion
    }
}
