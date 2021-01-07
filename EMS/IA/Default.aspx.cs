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

namespace Condesus.EMS.WebUI.IA
{
    public partial class Default : BasePage
    {
        #region Page Load & Init
            protected override void OnInit(EventArgs e)
            {
                base.OnInit(e);
            }
            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);
                //Load left menu
                Session["SelectedGlobalMenuItem"] = Condesus.EMS.WebUI.Common.GlobalMenus.Undefined;
            }
            protected override void SetPagetitle()
            {
                base.PageTitle = Resources.CommonMenu.DefaultIAPageTitle;
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.CommonMenu.AdministrationTool;
            }
        #endregion

        #region Private Methods
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonMenu.DefaultIAPageTitle;
                lblDescription.Text = Resources.CommonMenu.DefaultIADescription;
                lblOrganization.Text = Resources.CommonMenu.DefaultIAOrganization;
                lblTitleDescription.Text = Resources.CommonMenu.DefaultIATitleDescription;
            }
        #endregion

    }
}
