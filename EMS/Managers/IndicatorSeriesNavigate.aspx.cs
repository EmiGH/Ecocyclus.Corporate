using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Condesus.WebUI.Navigation;

namespace Condesus.EMS.WebUI.ManagementTools.PerformanceAssessment
{
    public partial class IndicatorSeriesNavigate : BasePage
    {
        private String _GTNPkCompost
        {
            get { return Convert.ToString(ViewState["PkCompost"]); }
            set { ViewState["PkCompost"] = value; }
        }
        private String _GTNEntityName
        {
            get { return Convert.ToString(ViewState["EntityName"]); }
            set { ViewState["EntityName"] = value; }
        }
        private String _GTNEntityNameGrid
        {
            get { return Convert.ToString(ViewState["EntityNameGrid"]); }
            set { ViewState["EntityNameGrid"] = value; }
        }
        private String _GTNEntityNameContextInfo
        {
            get { return Convert.ToString(ViewState["EntityNameContextInfo"]); }
            set { ViewState["EntityNameContextInfo"] = value; }
        }
        private String _GTNEntityNameContextElement
        {
            get { return Convert.ToString(ViewState["EntityNameContextElement"]); }
            set { ViewState["EntityNameContextElement"] = value; }
        }
        private String _GTNText
        {
            get { return Convert.ToString(ViewState["Text"]); }
            set { ViewState["Text"] = value; }
        }
        private Boolean _GTNIsReport
        {
            get { return Convert.ToBoolean(ViewState["IsReport"]); }
            set { ViewState["IsReport"] = value; }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
            {
                if (Request.QueryString["PkCompost"] != null)
                {
                    _GTNPkCompost = Convert.ToString(Request.QueryString["PkCompost"]);
                }
                if (Request.QueryString["EntityName"] != null)
                {
                    _GTNEntityName = Convert.ToString(Request.QueryString["EntityName"]);
                }
                if (Request.QueryString["EntityNameGrid"] != null)
                {
                    _GTNEntityNameGrid = Convert.ToString(Request.QueryString["EntityNameGrid"]);
                }
                if (Request.QueryString["EntityNameContextInfo"] != null)
                {
                    _GTNEntityNameContextInfo = Convert.ToString(Request.QueryString["EntityNameContextInfo"]);
                }
                if (Request.QueryString["EntityNameContextElement"] != null)
                {
                    _GTNEntityNameContextElement = Convert.ToString(Request.QueryString["EntityNameContextElement"]);
                }
                if (Request.QueryString["Text"] != null)
                {
                    _GTNText = Convert.ToString(Request.QueryString["Text"]);
                }
            }
            NavigateViewer();
        }
        //Pagina de transicion entre el link del tooltip del mapa y el Viewer.
        private void NavigateViewer()
        {
            NavigatorAddPkEntityIdTransferVar("PkCompost", _GTNPkCompost.Replace("|", "&"));

            NavigatorAddTransferVar("EntityName", _GTNEntityName);
            NavigatorAddTransferVar("EntityNameGrid", _GTNEntityNameGrid);
            NavigatorAddTransferVar("EntityNameContextInfo", _GTNEntityNameContextInfo);
            NavigatorAddTransferVar("EntityNameContextElement", _GTNEntityNameContextElement);

            String _args = "ContextInfoNavigation_" + _GTNPkCompost;
            NavigateMenuEventArgs _menuArgs = GetNavigateMenuEventArgs(NavigateMenuType.ContextInfoMenu, _args);

            String _entityClassName = String.Concat(Common.Functions.ReplaceIndexesTags(_GTNText), " [", GetValueFromGlobalResource("CommonListManage", _GTNEntityName), "]", " [", Resources.Common.mnuView, "]");
            Navigate("~/Managers/IndicatorSeries.aspx", _entityClassName, _menuArgs);
        }
    }
}
