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

namespace Condesus.EMS.WebUI.ManagementTools.Dashboard
{
    public partial class Map : BasePage
    {
        //#region Internal Properties
        private String _PageTitleLocal = String.Empty;
        //#endregion

        //#region PageLoad & Init
        //    protected override void OnLoad(EventArgs e)
        //    {
        //        base.OnLoad(e);
        //        //Si el usuario tiene permisos para acceder al config como manage, entonces carga el menu de seguridad
        //        if (EMSLibrary.User.DirectoryServices.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
        //        {
        //            LoadSecurityOptionMenu();
        //        }
        //    }
        protected override void SetPagetitle()
        {
            _PageTitleLocal = Resources.Common.Dashboard;
            base.PageTitle = _PageTitleLocal;
        }
        protected override void SetPageTileSubTitle()
        {
            base.PageTitleSubTitle = Resources.Common.ManagementTools;
        }
        //#endregion

        //#region Private Method
        //    private void LoadSecurityOptionMenu()
        //    {
        //        var _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
        //        _itemsMenu.Add("rmiSSJobTitles", new KeyValuePair<String, Boolean>(Resources.Common.mnuSSJobTitle, true));
        //        _itemsMenu.Add("rmiSSPerson", new KeyValuePair<String, Boolean>(Resources.Common.mnuSSPerson, true));

        //        RadMenu _rmnuSecuritySystem = BuildSecuritySystemMenu(_itemsMenu);
        //        _rmnuSecuritySystem.ItemClick += new RadMenuEventHandler(rmnuSecuritySystem_ItemClick);
        //    }
        //    private void SecuritySystemJobTitlesNavigate()
        //    {
        //        String _entityNameGrid = String.Empty;
        //        String _entityName = String.Empty;
        //        String _pkValues = String.Empty;
        //        String _pageTitle = String.Empty;

        //        switch (_SelectedModuleSection)
        //        {
        //            case "Map":
        //                _pkValues = "ParentEntity=" + Condesus.EMS.Business.Common.Security.MapDS;
        //                _pageTitle = _PageTitleLocal + " - " + Resources.Common.RightJobTitle;
        //                _entityNameGrid = Common.ConstantsEntitiesName.SS.RightJobTitleMapsDS;
        //                _entityName = Common.ConstantsEntitiesName.SS.RightJobTitleMapDS;
        //                break;
        //            case "Admin":
        //                _pkValues = "ParentEntity=" + Condesus.EMS.Business.Common.Security.ConfigurationDS;
        //                _pageTitle = _PageTitleLocal + " - " + Resources.Common.RightJobTitle;
        //                _entityNameGrid = Common.ConstantsEntitiesName.SS.RightJobTitleConfigurationsDS;
        //                _entityName = Common.ConstantsEntitiesName.SS.RightJobTitleConfigurationDS;
        //                break;
        //        }
        //        base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);
        //        base.NavigatorAddTransferVar("PageTitle", _pageTitle);
        //        base.NavigatorAddTransferVar("EntityNameGrid", _entityNameGrid);
        //        base.NavigatorAddTransferVar("EntityName", _entityName);

        //        base.Navigate("~/Managers/ListManageAndView.aspx", _entityNameGrid);
        //    }
        //    private void SecuritySystemPersonNavigate()
        //    {
        //        String _entityNameGrid = String.Empty;
        //        String _entityName = String.Empty;
        //        String _pkValues = String.Empty;
        //        String _pageTitle = String.Empty;

        //        switch (_SelectedModuleSection)
        //        {
        //            case "Map":
        //                _pkValues = "ParentEntity=" + Condesus.EMS.Business.Common.Security.MapDS;
        //                _pageTitle = _PageTitleLocal + " - " + Resources.Common.RightPerson;
        //                _entityNameGrid = Common.ConstantsEntitiesName.SS.RightPersonMapsDS;
        //                _entityName = Common.ConstantsEntitiesName.SS.RightPersonMapDS;
        //                break;
        //            case "Admin":
        //                _pkValues = "ParentEntity=" + Condesus.EMS.Business.Common.Security.ConfigurationDS;
        //                _pageTitle = _PageTitleLocal + " - " + Resources.Common.RightPerson;
        //                _entityNameGrid = Common.ConstantsEntitiesName.SS.RightPersonConfigurationsDS;
        //                _entityName = Common.ConstantsEntitiesName.SS.RightPersonConfigurationDS;
        //                break;
        //        }
        //        base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);
        //        base.NavigatorAddTransferVar("PageTitle", _pageTitle);
        //        base.NavigatorAddTransferVar("EntityNameGrid", _entityNameGrid);
        //        base.NavigatorAddTransferVar("EntityName", _entityName);

        //        base.Navigate("~/Managers/ListManageAndView.aspx", _entityNameGrid);
        //    }
        //#endregion

        //#region Page Event
        //    protected void rmnuSecuritySystem_ItemClick(object sender, RadMenuEventArgs e)
        //    {
        //        switch (e.Item.Value)
        //        {
        //            case "rmiSSJobTitles":
        //                SecuritySystemJobTitlesNavigate();
        //                break;

        //            case "rmiSSPerson":
        //                SecuritySystemPersonNavigate();
        //                break;

        //            default:
        //                break;
        //        }//fin Switch
        //    }//fin evento
        //#endregion

    }
}
