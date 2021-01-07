using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;
using System.Reflection;
using System.Linq;
using Condesus.EMS.WebUI.MasterControls;
using Condesus.EMS.Business.PF.Entities;
using Condesus.EMS.Business.PA.Entities;
using Condesus.EMS.Business.DS.Entities;
using Condesus.EMS.Business.KC.Entities;
using Dundas.Charting.WebControl;

namespace Condesus.EMS.WebUI
{
    public partial class BasePage : System.Web.UI.Page
    {
        #region Internal Properties
            //private RadTabStrip _RtsTabStrip = new RadTabStrip();
            //private RadMultiPage _RmpMultiPage = new RadMultiPage();
            //private Panel _PnlContent = new Panel();
            //private RadGrid _RgdMasterGridRelatedInfo = null;
        #endregion

        #region Private Methods
            protected RadTabStrip BuildTabStrip(String multiPageID)
            {
                RadTabStrip _RtsTabStrip = new RadTabStrip();

                _RtsTabStrip.ID = "rtsTabStripInformation";
                _RtsTabStrip.MultiPageID = multiPageID;
                //_RtsTabStrip.Skin = skinName;
                _RtsTabStrip.EnableEmbeddedSkins = false;
                _RtsTabStrip.CausesValidation = false;
                _RtsTabStrip.SelectedIndex = 0;
                //_RtsTabStrip.Width = Unit.Pixel(200);

                return _RtsTabStrip;
            }
            protected RadTab BuildTab(String text)
            {
                RadTab _radTab = new RadTab();
                _radTab.Text = text;

                return _radTab;
            }
            protected RadTab BuildTab(String textTooltip, String cssClassName, String cssClassSelectedName)
            {
                RadTab _radTab = new RadTab();
                _radTab.CssClass = cssClassName;
                _radTab.SelectedCssClass = cssClassSelectedName;
                _radTab.ToolTip = textTooltip;
                return _radTab;
            }
            protected RadMultiPage BuildMultiPage(String controlID)
            {
                RadMultiPage _radMultiPage = new RadMultiPage();
                _radMultiPage.ID = controlID;
                _radMultiPage.SelectedIndex = 0;

                return _radMultiPage;
            }
            protected RadPageView BuildPageView(String controlID, Boolean selected)
            {
                RadPageView _radPageView = new RadPageView();
                _radPageView.ID = controlID;
                _radPageView.Selected = selected;

                return _radPageView;
            }
        #endregion
    }
}
