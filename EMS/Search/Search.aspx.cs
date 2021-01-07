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

namespace Condesus.EMS.WebUI.Search
{
    public partial class Search : BaseProperties
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

                //Desactiva el dashboard, y activa el Search
                ((ImageButton)FwMasterPage.FindControl("btnGlobalToolbarHomeDashboardFW")).CssClass = "GlobalToolbarDashboard";
                ((ImageButton)FwMasterPage.FindControl("btnGlobalToolbarSearch")).CssClass = "GlobalToolbarSearchOpen";
            }
            protected override void SetPagetitle()
            {
                base.PageTitle = Resources.CommonMenu.SearchPageTitle;
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.CommonMenu.SearchPageSubtitle;
            }
        #endregion

        #region Private Methods
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonMenu.SearchPageDescription;
            }
        #endregion

        //Setea el Titulo de la Pagina
        //(como no puedo pasar el Objeto como una 'Interface', lo tengo que hacer localmente)

        //Setea el Titulo de la Pagina
        //(como no puedo pasar el Objeto como una 'Interface', lo tengo que hacer localmente)

    }


}
