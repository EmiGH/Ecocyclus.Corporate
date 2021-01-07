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
using Condesus.EMS.Business.DS.Entities;
using Condesus.EMS.Business.PA.Entities;
using Condesus.EMS.Business.PF.Entities;
using Condesus.EMS.WebUI.MasterControls;
using Condesus.WebUI.Navigation.State;
using Condesus.WebUI.Navigation.Status;
using Condesus.WebUI.Navigation.Transference;
using Condesus.WebUI.Navigation;

namespace Condesus.EMS.WebUI
{
    public partial class Default : BasePage
    {
        private void NavigateToOperatorDashboard()
        {
            _SelectedModuleValue = "Dashboard";
            _SelectedModuleSection = "Admin";
            //Navigator.Current.Transference.Items.MenuContextVars["ModuleValue"]="Dashboard";
            //Navigator.Current.Transference.Items.MenuContextVars["ModuleSection"] = "Map";

            String _entityName = Common.ConstantsEntitiesName.DB.ActiveTasks;
            NavigatorAddTransferVar("EntityNameGrid", Common.ConstantsEntitiesName.DB.ActiveTasks);
            NavigatorAddTransferVar("EntityName", _entityName);

            NavigatorAddTransferVar("EntityNameContextInfo", String.Empty);
            NavigatorAddTransferVar("EntityNameContextElement", String.Empty);
            NavigatorAddTransferVar("EntityNameComboFilter", String.Empty);
            NavigatorAddTransferVar("EntityNameHierarchical", String.Empty);
            NavigatorAddTransferVar("EntityNameHierarchicalChildren", String.Empty);
            NavigatorAddTransferVar("IsFilterHierarchy", false);
            NavigatorAddTransferVar("EntityNameChildrenComboFilter", String.Empty);
            NavigatorAddTransferVar("EntityNameMapClassification", String.Empty);
            NavigatorAddTransferVar("EntityNameMapClassificationChildren", String.Empty);
            NavigatorAddTransferVar("EntityNameMapElement", String.Empty);
            NavigatorAddTransferVar("EntityNameMapElementChildren", String.Empty);

            //Finalmente hace el Navigate al Manage Correspondiente.
            var argsColl = new Dictionary<String, String>();
            argsColl.Add("EntityName", _entityName);
            argsColl.Add("EntityNameGrid", Common.ConstantsEntitiesName.DB.ActiveTasks);
            argsColl.Add("EntityNameHierarchical", String.Empty);
            argsColl.Add("EntityNameHierarchicalChildren", String.Empty);

            NavigateMenuEventArgs _menuArgs = new NavigateMenuEventArgs(NavigateMenuType.GlobalMenu, argsColl);
            //Va al Manager
            String _titleDecorator = String.Concat(Resources.CommonListManage.ActiveTasks, " [", GetValueFromGlobalResource("CommonListManage", _entityName), "]");
            Navigate("~/Managers/ListManageAndView.aspx", _titleDecorator, _menuArgs);

        }
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (base.IsOperator())
            {
                //Ahora quieren que cuando se loguea un operador vaya al listado de tareas working...
                NavigateToOperatorDashboard();

                ImageButton _btnGlobalToolbarHome = (ImageButton)FwMasterPage.FindControl("btnGlobalToolbarHomeDashboardFW");
                //if (IsOperatorOnly())
                if (!EMSLibrary.User.ViewGlobalMenu)
                {
                    _btnGlobalToolbarHome.Attributes.Add("display", "none");
                    _btnGlobalToolbarHome.Visible = false;
                }

            }
            else
            {
                //Como el Usuario no es Operador, ocultamos el Boton del DashboardOperativo!!!
                ImageButton _btnGlobalToolbarOperativeDashboard = (ImageButton)FwMasterPage.FindControl("btnGlobalToolbarOperativeDashboardFW");
                _btnGlobalToolbarOperativeDashboard.Attributes.Add("display", "none");
                _btnGlobalToolbarOperativeDashboard.Visible = false;

                //Si el usuario no es operador, va al dashboard geografico
                //Response.Redirect("~/Dashboard/GeographicDashboard.aspx");
                Response.Redirect("~/Dashboard/GeographicDashboardMonitoring.aspx");
            }
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //TitleIconURL
            FwMasterPage.PageTitleIconURL = "~/Skins/Images/Icons/Dashboard.png";

        }


    }
}
