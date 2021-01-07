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
using Condesus.WebUI.Navigation;

namespace Condesus.EMS.WebUI.PM
{
    public partial class Configuration : BasePage
    {
        #region Internal Properties
            private String _PageTitleLocal = String.Empty;
        #endregion

        #region PageLoad & Init
            protected override void OnInit(EventArgs e)
            {
                base.OnInit(e);
                LoadTextLabels();
            }
            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);
                LoadSecurityOptionMenu();
            }
            protected override void SetPagetitle()
            {
                _PageTitleLocal = Resources.CommonMenu.ConfigPFPageTitle;
                base.PageTitle = _PageTitleLocal;
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.CommonMenu.AdministrationTool;
            }
        #endregion

        #region Private Method
            private void LoadTextLabels()
            {
                lblDescription.Text = Resources.CommonMenu.ConfigPFDescription;
            }
            private void LoadSecurityOptionMenu()
            {
                var _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
                _itemsMenu.Add("rmiSSJobTitles", new KeyValuePair<String, Boolean>(Resources.Common.mnuSSJobTitle, true));
                _itemsMenu.Add("rmiSSPerson", new KeyValuePair<String, Boolean>(Resources.Common.mnuSSPerson, true));

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
                        _pkValues = "ParentEntity=" + Condesus.EMS.Business.Common.Security.ConfigurationPF;
                        _pageTitle = _PageTitleLocal + " - " + Resources.Common.RightJobTitle;
                        _entityNameGrid = Common.ConstantsEntitiesName.SS.RightJobTitleConfigurationsPF;
                        _entityName = Common.ConstantsEntitiesName.SS.RightJobTitleConfigurationPF;
                        break;
                    case "Admin":
                        _pkValues = "ParentEntity=" + Condesus.EMS.Business.Common.Security.ConfigurationPF;
                        _pageTitle = _PageTitleLocal + " - " + Resources.Common.RightJobTitle;
                        _entityNameGrid = Common.ConstantsEntitiesName.SS.RightJobTitleConfigurationsPF;
                        _entityName = Common.ConstantsEntitiesName.SS.RightJobTitleConfigurationPF;
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
                        _pkValues = "ParentEntity=" + Condesus.EMS.Business.Common.Security.ConfigurationPF;
                        _pageTitle = _PageTitleLocal + " - " + Resources.Common.RightPerson;
                        _entityNameGrid = Common.ConstantsEntitiesName.SS.RightPersonConfigurationsPF;
                        _entityName = Common.ConstantsEntitiesName.SS.RightPersonConfigurationPF;
                        break;
                    case "Admin":
                        _pkValues = "ParentEntity=" + Condesus.EMS.Business.Common.Security.ConfigurationPF;
                        _pageTitle = _PageTitleLocal + " - " + Resources.Common.RightPerson;
                        _entityNameGrid = Common.ConstantsEntitiesName.SS.RightPersonConfigurationsPF;
                        _entityName = Common.ConstantsEntitiesName.SS.RightPersonConfigurationPF;
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
