﻿using System;
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
using Condesus.WebUI.Navigation;

namespace Condesus.EMS.WebUI.DS
{
    public partial class Default : BasePage
    {
        #region PageLoad & Init
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

                //Si el usuario tiene permisos para acceder al config como manage, entonces carga el menu de seguridad
                if ((EMSLibrary.User.DirectoryServices.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey)) || (EMSLibrary.User.DirectoryServices.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey)))
                {
                    LoadSecurityOptionMenu();
                }
            }
            protected override void SetPagetitle()
            {
                base.PageTitle = Resources.CommonMenu.DefaultDSSubTitle;
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.CommonMenu.AdministrationTool;
            }
        #endregion

        #region Private Method
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonMenu.DefaultDSSubTitle;
                lblDescription.Text = Resources.CommonMenu.DefaultDSDescription;
                lblOrganization.Text = Resources.CommonMenu.DefaultDSOrganization;
                lblSubTitle.Text = Resources.CommonMenu.DefaultDSSubTitle;
                lblTitle.Text = Resources.CommonMenu.AdministrationTool;
                lblTitleDescription.Text = Resources.CommonMenu.DefaultDSTitleDescription;
            }
            private void LoadSecurityOptionMenu()
            {
                var _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
                _itemsMenu.Add("rmiSSJobTitles", new KeyValuePair<String, Boolean>(Resources.Common.mnuSSJobTitle, true));
                _itemsMenu.Add("rmiSSPerson", new KeyValuePair<String, Boolean>(Resources.Common.mnuSSPerson, true));

                //RadMenu _rmnuSecuritySystem = BuildSecuritySystemMenu(_itemsMenu);
                RadMenu _rmnuSecuritySystem = BuildSecuritySystemMenu(_itemsMenu);
                _rmnuSecuritySystem.ItemClick += new RadMenuEventHandler(rmnuSecuritySystem_ItemClick);
            }
            private void SecuritySystemJobTitlesNavigate()
            {
                String _entityNameGrid = String.Empty;
                String _entityName = String.Empty;
                String _pkValues = String.Empty;
                String _pageTitle = String.Empty;

                switch (_SelectedModuleSection)
                {
                    case "Map":
                        _pkValues = "ParentEntity=" + Condesus.EMS.Business.Common.Security.MapDS;
                        _pageTitle = Condesus.EMS.Business.Common.Security.MapDS + " - Right Job Title";
                        _entityNameGrid = Common.ConstantsEntitiesName.SS.RightJobTitleMapsDS;
                        _entityName = Common.ConstantsEntitiesName.SS.RightJobTitleMapDS;
                        break;
                    case "Admin":
                        _pkValues = "ParentEntity=" + Condesus.EMS.Business.Common.Security.ConfigurationDS;
                        _pageTitle = Condesus.EMS.Business.Common.Security.ConfigurationDS + " - Right Job Title";
                        _entityNameGrid = Common.ConstantsEntitiesName.SS.RightJobTitleConfigurationsDS;
                        _entityName = Common.ConstantsEntitiesName.SS.RightJobTitleConfigurationDS;
                        break;
                }
                base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);
                base.NavigatorAddTransferVar("PageTitle", _pageTitle);
                base.NavigatorAddTransferVar("EntityNameGrid", _entityNameGrid);
                base.NavigatorAddTransferVar("EntityName", _entityName);

                NavigateMenuEventArgs _menuArgs = GetNavigateMenuEventArgs(NavigateMenuType.ContextInfoMenu, "ContextInfoNavigation");
                base.Navigate("~/Managers/ListManageAndView.aspx", GetValueFromGlobalResource("CommonListManage", _entityName), _menuArgs);
            }
            private void SecuritySystemPersonNavigate()
            {
                String _entityNameGrid = String.Empty;
                String _entityName = String.Empty;
                String _pkValues = String.Empty;
                String _pageTitle = String.Empty;

                switch (_SelectedModuleSection)
                {
                    case "Map":
                        _pkValues = "ParentEntity=" + Condesus.EMS.Business.Common.Security.MapDS;
                        _pageTitle = Condesus.EMS.Business.Common.Security.MapDS + " - Right Post";
                        _entityNameGrid = Common.ConstantsEntitiesName.SS.RightPersonMapsDS;
                        _entityName = Common.ConstantsEntitiesName.SS.RightPersonMapDS;
                        break;
                    case "Admin":
                        _pkValues = "ParentEntity=" + Condesus.EMS.Business.Common.Security.ConfigurationDS;
                        _pageTitle = Condesus.EMS.Business.Common.Security.ConfigurationDS + " - Right Post";
                        _entityNameGrid = Common.ConstantsEntitiesName.SS.RightPersonConfigurationsDS;
                        _entityName = Common.ConstantsEntitiesName.SS.RightPersonConfigurationDS;
                        break;
                }
                base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);
                base.NavigatorAddTransferVar("PageTitle", _pageTitle);
                base.NavigatorAddTransferVar("EntityNameGrid", _entityNameGrid);
                base.NavigatorAddTransferVar("EntityName", _entityName);

                NavigateMenuEventArgs _menuArgs = GetNavigateMenuEventArgs(NavigateMenuType.ContextInfoMenu, "ContextInfoNavigation");
                base.Navigate("~/Managers/ListManageAndView.aspx", GetValueFromGlobalResource("CommonListManage", _entityName), _menuArgs);
            }
        #endregion

        #region Page Event
            protected void rmnuSecuritySystem_ItemClick(object sender, RadMenuEventArgs e)
            {
                switch (e.Item.Value)
                {
                    case "rmiSSJobTitles":
                        SecuritySystemJobTitlesNavigate();
                        break;

                    case "rmiSSPerson":
                        SecuritySystemPersonNavigate();
                        break;

                    default:
                        break;
                }//fin Switch
            }//fin evento
        #endregion
    }
}
