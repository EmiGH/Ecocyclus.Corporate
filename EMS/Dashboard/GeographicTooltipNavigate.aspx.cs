using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Condesus.WebUI.Navigation;

namespace Condesus.EMS.WebUI.Dashboard
{
    public partial class GeographicTooltipNavigate : BasePage
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
        private String _GTNLayerView
        {
            get { return Convert.ToString(ViewState["LayerView"]); }
            set { ViewState["LayerView"] = value; }
        }
        private String _GTNNavigateType
        {
            get { return Convert.ToString(ViewState["NavigateType"]); }
            set { ViewState["NavigateType"] = value; }
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

                if (Request.QueryString["IsReport"] != null)
                {
                    _GTNIsReport= Convert.ToBoolean(Request.QueryString["IsReport"]);
                }
                if (Request.QueryString["LayerView"] != null)
                {
                    _GTNLayerView = Convert.ToString(Request.QueryString["LayerView"]);
                }
                if (Request.QueryString["NavigateType"] != null)
                {
                    _GTNNavigateType = Convert.ToString(Request.QueryString["NavigateType"]);
                }
            }
            //Si no es un reporte, navega al viewer
            if (!_GTNIsReport)
            {
                if (String.IsNullOrEmpty(_GTNLayerView))
                {
                    NavigateViewer();
                }
                else
                {
                    NavigateToLayer();
                }
            }
            else
            {
                NavigateToReport();
            }
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

            String _entityClassName = String.Concat(_GTNText, " [", GetValueFromGlobalResource("CommonListManage", _GTNEntityName), "]", " [", Resources.Common.mnuView, "]");
            Navigate(GetPageViewerByEntity(_GTNEntityName), _entityClassName, _menuArgs);
        }
        private void NavigateToReport()
        {
            Int64 _idProcess = Convert.ToInt64(Request.QueryString["IdEntity"]);
            base.NavigatorClearTransferVars();

            //Al ser un reporte, debe navegar ahi...
            var argsColl = new Dictionary<String, String>();
            argsColl.Add("Reports", "ReportTransformationByScope");
            argsColl.Add("IdProcess", _idProcess.ToString());

            //Se pasa el id del Process para poder hacer los filtros del reporte!!!
            NavigatorAddTransferVar("IdProcess", _idProcess);

            NavigateMenuEventArgs _menuArgs = new NavigateMenuEventArgs(NavigateMenuType.GlobalMenu, argsColl);
            //Navigate("~/Dashboard/ReportTransformationByScope.aspx", Resources.CommonListManage.Report, _menuArgs);
            Navigate("~/Dashboard/FilterRptCalculationsOfTransformation.aspx", Resources.CommonListManage.Report, _menuArgs);
        }
        private void NavigateToLayer()
        {
            //Al ser un reporte, debe navegar ahi...
            var argsColl = new Dictionary<String, String>();
            argsColl.Add("LayerView", _GTNLayerView);

            NavigatorAddPkEntityIdTransferVar("PkCompost", _GTNPkCompost.Replace("|", "&"));
            NavigatorAddTransferVar("LayerView", _GTNLayerView);
            NavigatorAddTransferVar("EntityName", _GTNEntityName);
            NavigatorAddTransferVar("EntityNameGrid", _GTNEntityNameGrid);
            NavigatorAddTransferVar("EntityNameContextInfo", _GTNEntityNameContextInfo);
            NavigatorAddTransferVar("EntityNameContextElement", _GTNEntityNameContextElement);
            NavigatorAddTransferVar("NavigateType", _GTNNavigateType);

            String _args = "ContextInfoNavigation_" + _GTNPkCompost;
            NavigateMenuEventArgs _menuArgs = GetNavigateMenuEventArgs(NavigateMenuType.ContextInfoMenu, _args);

            Navigate("~/Dashboard/GeographicDashboard.aspx", Resources.Common.GeographicDashboard, _menuArgs);
        }
    }
}
