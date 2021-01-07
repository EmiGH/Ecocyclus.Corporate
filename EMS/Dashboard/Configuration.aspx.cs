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

namespace Condesus.EMS.WebUI.Dashboard
{
    public partial class Configuration : BasePage
    {
        private String _PageTitleLocal = String.Empty;

        protected override void SetPagetitle()
        {
            _PageTitleLocal = Resources.Common.Dashboard;
            base.PageTitle = _PageTitleLocal;
        }
        protected override void SetPageTileSubTitle()
        {
            base.PageTitleSubTitle = Resources.Common.AdministrationTools;
        }

    }
}
