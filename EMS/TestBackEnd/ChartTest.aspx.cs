using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.Threading;
using Telerik.Charting;
using Telerik.Web.UI;
using Condesus.WebUI.Navigation;
using Condesus.EMS.Business.KC.Entities;
using Condesus.EMS.Business.PA.Entities;
using Condesus.EMS.Business.PF.Entities;
using Condesus.EMS.WebUI.Business;

namespace Condesus.EMS.WebUI
{
    public partial class ChartTest : BaseProperties
    {
        #region Internal Properties
        #endregion

        #region Page Load & Init
            protected override void InitializeHandlers()
            {
                base.InitializeHandlers();
            }
            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);
            }
            protected override void SetPagetitle()
            {
                try
                {
                    //Busca el nombre de la pagina si es que se lo pasaron. Sino usa el nombre de la entidad.
                    String _pageTitle = base.NavigatorGetTransferVar<String>("PageTitle");
                    if (String.IsNullOrEmpty(_pageTitle))
                    {
                        base.PageTitle = "Report";
                    }
                    else
                    {
                        base.PageTitle = _pageTitle;
                    }
                }
                catch { base.PageTitle = String.Empty; }
            }
            protected override void SetPageTileSubTitle()
            {
                try
                {
                    String _pageSubTitle = base.NavigatorGetTransferVar<String>("PageSubTitle");
                    if (String.IsNullOrEmpty(_pageSubTitle))
                    {
                        base.PageTitleSubTitle = Resources.CommonListManage.lblSubtitle;
                    }
                    else
                    {
                        base.PageTitleSubTitle = _pageSubTitle;
                    }
                }
                catch
                { base.PageTitleSubTitle = String.Empty; }
            }
        #endregion

        #region Private Methods

        #endregion

        #region Page Events
            protected void OrientationList_SelectedIndexChanged(object sender, EventArgs e)
            {
                RadChart1.SeriesOrientation = (ChartSeriesOrientation)Enum.Parse(typeof(ChartSeriesOrientation), OrientationList.SelectedValue);
            }

            protected void SubtypeDropdown_SelectedIndexChanged(object sender, EventArgs e)
            {
                RadChart1.Series[0].Type = (ChartSeriesType)Enum.Parse(typeof(ChartSeriesType), SubtypeDropdown.SelectedValue);
                RadChart1.Series[1].Type = (ChartSeriesType)Enum.Parse(typeof(ChartSeriesType), SubtypeDropdown.SelectedValue);
            }
        #endregion

    }
}
