using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Telerik.Web.UI;
using Condesus.EMS.WebUI.MasterControls;
using Condesus.WebUI.Navigation.State;
using Condesus.WebUI.Navigation.Status;
using Condesus.WebUI.Navigation.Transference;
using Condesus.WebUI.Navigation;

namespace Condesus.EMS.WebUI
{
    public partial class BasePage : System.Web.UI.Page 
    {
        public EMS FwMasterPage
        {
            get { return (EMS)this.Master; }
        }

        //Handler de todos los 'Click events' de MasterFW
        void FwMasterPage_OnNavigation(object sender, MasterControls.MasterFwNavigationEventArgs e)
        {
            switch (e.Sender)
            {
                case MasterFwSender.Header:
                    if (e.Args == "LogOff")
                        LoggOff();
                    break;
                case MasterFwSender.ContentNavigator:
                case MasterFwSender.GlobalNavigator:
                    if (e.Args == "Home")
                        //Como se presiono el HOME, por las dudas desactiva el search. y activa el dashboard.
                        ((ImageButton)FwMasterPage.FindControl("btnGlobalToolbarSearch")).CssClass = "GlobalToolbarSearch";
                        ((ImageButton)FwMasterPage.FindControl("btnGlobalToolbarHomeDashboardFW")).CssClass = "GlobalToolbarDashboard";
                        ((ImageButton)FwMasterPage.FindControl("btnGlobalToolbarOperativeDashboardFW")).CssClass = "GlobalToolbarOperativeDash";

                        NavigateMenuEventArgs _menuArgs = GetNavigateMenuEventArgs(NavigateMenuType.GlobalMenu, "HomeModuleNavigation");
                        //if (IsOperator())
                        //{
                        //    //Si el usuario logueado es operador de al menos una tarea, va al dashboard operativo
                        //    //this.Navigate("~/Managers/TaskList.aspx", e.Title, _menuArgs);
                        //    NavigateToOperatorDashboard();
                        //}
                        //else
                        //{
                            //Si el usuario no es operador, va al dashboard geografico
                        NavigatorAddTransferVar("LayerView", "Country");
                        //this.Navigate("~/Dashboard/GeographicDashboard.aspx", e.Title, _menuArgs);
                        this.Navigate("~/Dashboard/GeographicDashboardMonitoring.aspx", e.Title, _menuArgs);
                        //}
                    break;

                case MasterFwSender.OperativeDash:
                    ((ImageButton)FwMasterPage.FindControl("btnGlobalToolbarOperativeDashboardFW")).CssClass = "GlobalToolbarOperativeDash";
                    ((ImageButton)FwMasterPage.FindControl("btnGlobalToolbarHomeDashboardFW")).CssClass = "GlobalToolbarDashboard";
                    NavigateToOperatorDashboard();
                    break;

                case MasterFwSender.BreadCrumb:
                    HandleFwBreadCrumbClick(e);
                    break;
            }
        }
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

        protected void LoggOff()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            Response.Redirect("~/Login_ghree.aspx");
        }

        //TODO: MOVER A BASE PAGE (daba Error Generic)
        #region MasterFWContent (Textos del Marco del FrameWork)

        //Inicializa los valores (x medio de Recursos, Config(AppSettings) y Libreria) de los Controles del Marco del MasterFW
        private void InitFwContent()
        {
            FwMasterPage.HeaderUserName = EMSLibrary.User.Person.FullName + " - " + EMSLibrary.User.Person.Organization.Name;
            FwMasterPage.HeaderCompanyName = EMSLibrary.User.Person.Organization.Name;
            FwMasterPage.HeaderLogOffText = GetMasterFWResourceText("lblLogOff");
            FwMasterPage.HeaderProductName = GetMasterFWResourceText("lblProductName");
            FwMasterPage.HeaderTecnicalSupportEMail = (String)ConfigurationSettings.AppSettings["customTechnicalSupportAddressTo"];
            FwMasterPage.HeaderTecnicalSupportText = GetMasterFWResourceText("lblTechnicalSupport");
            FwMasterPage.FooterText = GetMasterFWResourceText("lblFooter");
        }

        private String GetMasterFWResourceText(String key)
        {
            Object _objBuffer = GetGlobalResourceObject("MasterFW", key);

            if (_objBuffer != null)
                return _objBuffer.ToString();

            return String.Empty;
        }

        #endregion

        private void HandleNavigatorPageTitle()
        {
            if (Navigator.Current.Title == Navigator.UNDEFINED_TITLE)
                Navigator.Current.Title = FwMasterPage.PageTitle;
        }

        #region Global Toolbar & Menu

        #region ToolBar
        private void BuildGlobalToolbar()
        {
            ImageButton btnGlobalToolbarGlobalNavigatorShowMenu = new ImageButton();
            btnGlobalToolbarGlobalNavigatorShowMenu.ID = "btnGlobalToolbarGlobalNavigatorShowMenu";
            btnGlobalToolbarGlobalNavigatorShowMenu.CausesValidation = false;
            btnGlobalToolbarGlobalNavigatorShowMenu.ImageUrl = "~/Skins/Images/Trans.gif";
            btnGlobalToolbarGlobalNavigatorShowMenu.CssClass = "GlobalToolbarModule";
            btnGlobalToolbarGlobalNavigatorShowMenu.ToolTip = GetGlobalResourceObject("MasterFW", "btnGlobalToolbarGlobalNavigatorShowMenuTooltip").ToString();
            //Agregado over para abrir panel global
            btnGlobalToolbarGlobalNavigatorShowMenu.Attributes.Add("onmouseover", "javascript:ShowMenuGlobalPanel(event);");

            //Si el usuario esta configurado para ver el Menu Global, lo ponemos (Pero si es Operador, tambien lo ponemos, porque es la unica forma de acceder!)
            if ((EMSLibrary.User.ViewGlobalMenu) || IsOperator())
            {
                FwMasterPage.GlobalNavigatorToolbarAdd(btnGlobalToolbarGlobalNavigatorShowMenu, true);
            }

            ImageButton btnGlobalToolbarSearch = new ImageButton();
            btnGlobalToolbarSearch.ID = "btnGlobalToolbarSearch";
            btnGlobalToolbarSearch.CausesValidation = false;
            btnGlobalToolbarSearch.ImageUrl = "~/Skins/Images/Trans.gif";
            btnGlobalToolbarSearch.CssClass = "GlobalToolbarSearch";
            btnGlobalToolbarSearch.Click += new ImageClickEventHandler(btnGlobalToolbarSearch_Click);
            btnGlobalToolbarSearch.ToolTip = GetGlobalResourceObject("MasterFW", "btnGlobalToolbarSearchTooltip").ToString();
            btnGlobalToolbarSearch.OnClientClick = "javascript:ChangeCssClassHome(this);";
            btnGlobalToolbarSearch.Style.Add("display", "none");

            FwMasterPage.GlobalNavigatorToolbarAdd(btnGlobalToolbarSearch, false);
        }

        private void btnGlobalToolbarSearch_Click(object sender, ImageClickEventArgs e)
        {
            //this.Navigate("~/Search.aspx", "Search");
            //Como hizo click en el search, por las dudas, desactiva el HOME y activa el Search
            ((ImageButton)FwMasterPage.FindControl("btnGlobalToolbarHomeDashboardFW")).CssClass = "GlobalToolbarDashboard";
            ((ImageButton)sender).CssClass = "GlobalToolbarSearchOpen";

            NavigateMenuEventArgs _menuArgs = GetNavigateMenuEventArgs(NavigateMenuType.GlobalMenu, "SearchModuleNavigation"); 
            this.Navigate("~/DS/Default.aspx", Resources.CommonMenu.SearchPageDescription, _menuArgs);
        }
        private void btnGlobalToolbarOperativeDashboard_Click(object sender, ImageClickEventArgs e)
        {
            //Como hizo click en el search, por las dudas, desactiva el HOME y activa el Search
            ((ImageButton)FwMasterPage.FindControl("btnGlobalToolbarHomeDashboardFW")).CssClass = "GlobalToolbarDashboard";
            ((ImageButton)sender).CssClass = "GlobalToolbarSearchOpen";

            NavigateMenuEventArgs _menuArgs = GetNavigateMenuEventArgs(NavigateMenuType.GlobalMenu, "SearchModuleNavigation");

            NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.DB.OverDueTasks);
            NavigatorAddTransferVar("EntityNameGrid", Common.ConstantsEntitiesName.DB.OverDueTasks);
            this.Navigate("~/Managers/TaskList.aspx", Resources.CommonMenu.SearchPageDescription, _menuArgs);
        }
        #endregion

        #region Menu
        #region Members
        protected String _SelectedModuleTitle
        {
            get
            {
                object _o = ViewState["SelectedModuleTitle"];
                if (_o != null)
                    return (String)_o;

                return String.Empty;
            }

            set { ViewState["SelectedModuleTitle"] = value.ToString(); }
        }
        protected String _SelectedModuleValue
        {
            get
            {
                object _o = ViewState["SelectedModuleValue"];
                if (_o != null)
                    return (String)_o;

                return String.Empty;
            }

            set { ViewState["SelectedModuleValue"] = value.ToString(); }
        }
        public String _SelectedModuleSection
        {
            get
            {
                object _o = ViewState["SelectedModuleSection"];
                if (_o != null)
                    return (String)_o;

                return String.Empty;
            }

            set { ViewState["SelectedModuleSection"] = value.ToString(); }
        }
        private String _SelectedModuleURL
        {
            get
            {
                object _o = ViewState["SelectedModuleURL"];
                if (_o != null)
                    return (String)_o;

                return String.Empty;
            }

            set { ViewState["SelectedModuleURL"] = value.ToString(); }
        }

        private String _SelectedNodeValueGlobalMenu
        {
            get
            {
                object _o = Session["SelectedNodeValueGlobalMenu"];
                if (_o != null)
                    return (String)_o;

                return String.Empty;
            }

            set { Session["SelectedNodeValueGlobalMenu"] = value.ToString(); }
        }
        private String _SelectedNodeTextGlobalMenu
        {
            get
            {
                object _o = Session["SelectedNodeTextGlobalMenu"];
                if (_o != null)
                    return (String)_o;

                return String.Empty;
            }

            set { Session["SelectedNodeTextGlobalMenu"] = value.ToString(); }
        }
        private String _SelectedNodeGlobalMenuEntityName
        {
            get
            {
                object _o = Session["SelectedNodeGlobalMenuEntityName"];
                if (_o != null)
                    return (String)_o;

                return String.Empty;
            }

            set { Session["SelectedNodeGlobalMenuEntityName"] = value.ToString(); }
        }
        private String _SelectedNodeGlobalMenuEntityNameChildren
        {
            get
            {
                object _o = Session["SelectedNodeGlobalMenuEntityNameChildren"];
                if (_o != null)
                    return (String)_o;

                return String.Empty;
            }

            set { Session["SelectedNodeGlobalMenuEntityNameChildren"] = value.ToString(); }
        }
        public String _TreeViewGlobalMenuXML
        {
            get
            {
                object _o = Session["TreeViewGlobalMenuXML"];
                if (_o != null)
                    return (String)_o;

                return String.Empty;
            }

            set { Session["TreeViewGlobalMenuXML"] = value.ToString(); }
        }
        #endregion

        protected virtual void BuildGlobalMenu()
        {
            //Estructura HTML (Control Tree)
            Table _container = new Table();
            TableRow _tr = new TableRow();
            TableCell _tdMenu = new TableCell();
            TableCell _tdIcons = new TableCell();
            _container.Style["width"] = "100%";

            //Menu del Header
            //RadMenu _rmnuHeaderToolbar = BuildGlobalMenuHeaderToolbar();
            RadMenu _rmnuHeaderToolbar = BuildGlobalMenuHeaderToolbar();

            //Botones para la funcionalidad de cambio de SubModulo (Map-Admin-etc)
            _tdIcons.Style["text-align"] = "right";
            _tdIcons.Style["width"] = "100%";
            ImageButton _btnManage = new ImageButton();
            _btnManage.ID = "btnManageGobalMenuPanel";
            _btnManage.CommandArgument = "Map";
            _btnManage.CausesValidation = false;
            _btnManage.ImageUrl = "~/Skins/Images/Trans.gif";
            _btnManage.ToolTip = GetGlobalResourceObject("MasterFW", "btnManageGobalMenuPanelTooltip").ToString();
            //_btnManage.CssClass = "globalNavigatorButtonMap";
            _btnManage.Click += new ImageClickEventHandler(btnGlobalNavigatorHeaderToolbarSubModule_Click);

            ImageButton _btnAdmin = new ImageButton();
            _btnAdmin.ID = "btnAdminGobalMenuPanel";
            _btnAdmin.CommandArgument = "Admin";
            _btnAdmin.CausesValidation = false;
            _btnAdmin.ImageUrl = "~/Skins/Images/Trans.gif";
            _btnAdmin.ToolTip = GetGlobalResourceObject("MasterFW", "btnAdminGobalMenuPanelTooltip").ToString();
            //_btnAdmin.CssClass = "globalNavigatorButtonConfigOpen";
            _btnAdmin.Click += new ImageClickEventHandler(btnGlobalNavigatorHeaderToolbarSubModule_Click);

            //Estructura (Control Tree)
            _tdMenu.Controls.Add(_rmnuHeaderToolbar);
            _tdIcons.Controls.Add(_btnManage);
            _tdIcons.Controls.Add(_btnAdmin);
            _tr.Controls.Add(_tdMenu);
            _tr.Controls.Add(_tdIcons);
            _container.Controls.Add(_tr);
            FwMasterPage.GlobalNavigatorHeaderToolbarContentAdd(_container);

            //Construye el menu de accion para el modulo seleccionado.
            //String _methodBuildMenuModule = "BuildMenuModule" + _SelectedModuleValue + _SelectedModuleSection;
            String _methodBuildMenuModule = "BuildMenuModule" + Navigator.Current.Transference.Items.MenuContextVars["ModuleValue"] + Navigator.Current.Transference.Items.MenuContextVars["ModuleSection"];
            RadTreeView _rtvMenuModule = BuildGenericMenu(_methodBuildMenuModule, new Dictionary<String, Object>());
            _rtvMenuModule.NodeClick += new RadTreeViewEventHandler(rtvMenuModule_NodeClick);
            _rtvMenuModule.ContextMenuItemClick += new RadTreeViewContextMenuEventHandler(rtvMenuElementMaps_ContextMenuItemClick);
            FwMasterPage.GlobalNavigatorToolbarMenuContentAdd(_rtvMenuModule);

            _rtvMenuModule.OnClientNodeExpanded = "onNodeExpanded";

            InjectOnClientContextMenuShowing();
            InjectDragAndDropNodeElementMap();

            InjectOnEndRequestSetTreeViewScroll(_rtvMenuModule.ClientID);
            InjectonNodeExpanded();

            SetStyleButtonMapAdmin();

            if (!String.IsNullOrEmpty(_TreeViewGlobalMenuXML))
            {
                _rtvMenuModule.LoadXml(_TreeViewGlobalMenuXML);
            }

            ////Verifica si hay algo seleccionado
            //RadTreeNode _rtNode = null;
            //if ((!String.IsNullOrEmpty(_SelectedNodeValueGlobalMenu)) && (_SelectedNodeValueGlobalMenu != "nodeConfig"))
            //{
            //    //SelectItemTreeViewParentElementMaps(_SelectedNodeValueGlobalMenu, ref _rtvMenuModule, Common.ConstantsEntitiesName.DS.GeographicArea, Common.ConstantsEntitiesName.DS.GeographicAreaChildren);
            //    Int64 _idOrganization = Convert.ToInt64(GetKeyValue(_SelectedNodeValueGlobalMenu, "IdOrganization"));
            //    Condesus.EMS.Business.DS.Entities.Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
            //    String _keyValuesElement = "IdOrganization=" + _organization.IdOrganization.ToString();
            //    if (_organization.Classifications.Count > 0)
            //    {
            //        String _keyValuesClassification = "IdOrganizationClassification=" + _organization.Classifications.First().Value.IdOrganizationClassification.ToString() + "& IdParentOrganizationClassification=" + _organization.Classifications.First().Value.IdParentOrganizationClassification.ToString();
            //        SelectItemTreeViewElementMapsGlobalMenu(_keyValuesClassification, _keyValuesElement, ref _rtvMenuModule, Common.ConstantsEntitiesName.DS.OrganizationClassification, Common.ConstantsEntitiesName.DS.OrganizationClassificationsChildren, Common.ConstantsEntitiesName.DS.Organizations);
            //    }
            //    else
            //    {
            //        SelectItemTreeView(_keyValuesElement, ref _rtvMenuModule, Common.ConstantsEntitiesName.DS.Organization, Common.ConstantsEntitiesName.DS.Organizations);
            //    }

            //    //_rtNode = _rtvMenuModule.FindNodeByValue(_SelectedNodeValueGlobalMenu);
            //}
            //else
            //{
            //    if (!String.IsNullOrEmpty(_SelectedNodeTextGlobalMenu))
            //    {
            //        _rtNode = _rtvMenuModule.FindNodeByText(_SelectedNodeTextGlobalMenu);
            //    }
            //}
            //if (_rtNode != null)
            //    { _rtNode.Selected = true; }

        }
        //private RadMenu BuildGlobalMenuHeaderToolbar()
        private RadMenu BuildGlobalMenuHeaderToolbar()
        {
            //RadMenu _rmnuHeaderToolbar = new RadMenu();
            RadMenu _rmnuHeaderToolbar = new RadMenu();
            _rmnuHeaderToolbar.ID = "rmnuHeaderToolbar";
            _rmnuHeaderToolbar.CausesValidation = false;
            _rmnuHeaderToolbar.Skin = "EMS";
            _rmnuHeaderToolbar.EnableEmbeddedSkins = false;
            _rmnuHeaderToolbar.CollapseDelay = 0;
            _rmnuHeaderToolbar.ExpandDelay = 0;
            _rmnuHeaderToolbar.ItemClick += new RadMenuEventHandler(_rmnuHeaderToolbar_ItemClick);
            
            LoadModules(_rmnuHeaderToolbar);

            return _rmnuHeaderToolbar;
        }

        //Para Desacoplar el Menu del "Proyecto", esto deberia armarse en Menu.cs (c/proyecto tiene el suyo propio)
        //private void LoadModules(RadMenu rmnuHeaderToolbar)
        private void LoadModules(RadMenu rmnuHeaderToolbar)
        {
            Boolean _operatorOnly = false;
            if (ConfigurationManager.AppSettings["OperatorOnly"] != null)
            {
                _operatorOnly = Convert.ToBoolean(ConfigurationManager.AppSettings["OperatorOnly"].ToString());
            }
            
            #region RootItem
            RadMenuItem _radmnuItemRoot = new RadMenuItem();

            _radmnuItemRoot.CssClass = "MnuItemRoot";
            _radmnuItemRoot.ExpandedCssClass = "MnuItemRootOver";
            _radmnuItemRoot.PostBack = false;

            //Si es Operador y no puede hacer otra cosa.., entonces setea el nombre en el menu....
            //if (IsOperator() && IsOperatorOnly())
            if (IsOperator() && !EMSLibrary.User.ViewGlobalMenu)
            {
                _radmnuItemRoot.Text = Resources.CommonMenu.radMnuSectionDashboard;
            }
            else
            {
                _radmnuItemRoot.Text = _SelectedModuleTitle;
            }
            rmnuHeaderToolbar.Items.Add(_radmnuItemRoot);
            #endregion

            #region Module Items
            //Items (Armar con Seguridad Integrada)
            var _modules = new Dictionary<String, KeyValuePair<String, String>>();
            try
            {
                //Si es Operador y no puede hacer otra cosa..
                //if (IsOperator() && IsOperatorOnly())
                if (IsOperator() && !EMSLibrary.User.ViewGlobalMenu)
                {
                    _modules.Add("Dashboard", new KeyValuePair<String, String>(Resources.CommonMenu.radMnuSectionDashboard, "/Dashboard/Default.aspx"));
                }
                else
                {
                    _modules.Add("DS", new KeyValuePair<String, String>(Resources.CommonMenu.radMnuModuleDS, "/DS/Default.aspx"));
                    _modules.Add("PA", new KeyValuePair<String, String>(Resources.CommonMenu.radMnuModulePA, "/PA/Default.aspx"));
                    _modules.Add("PM", new KeyValuePair<String, String>(Resources.CommonMenu.radMnuModulePF, "/PM/Default.aspx"));
                    _modules.Add("KC", new KeyValuePair<String, String>(Resources.CommonMenu.radMnuModuleKC, "/KC/Default.aspx"));
                    if (IsOperator())
                    {
                        _modules.Add("Dashboard", new KeyValuePair<String, String>(Resources.CommonMenu.radMnuSectionDashboard, "/Dashboard/Default.aspx"));
                    }
                }
            }
            catch
            {
                //Falta el Recurso
            }

            RadMenuItem _radmnuItem;
            foreach (var _moduleItem in _modules)
            {
                String _itemModule = _moduleItem.Key;
                String _itemText = _moduleItem.Value.Key;
                String _postBackUrl = _moduleItem.Value.Value;

                _radmnuItem = new RadMenuItem();
                _radmnuItem.Value = _itemModule;
                _radmnuItem.Text = _itemText;

                //El Seleccionado no deberia hacer PostBack
                _radmnuItem.PostBack = (Navigator.Current.Transference.Items.MenuContextVars["ModuleValue"] != _itemModule);
                //Lo mismo para "marcar x Css" el selecionado
                //TODO: Marcar el Modulo con Css de Seleccionado

                _radmnuItemRoot.Items.Add(_radmnuItem);
           }
            FwMasterPage.PageTitleIconURL = "/Skins/Images/Icons/" + Navigator.Current.Transference.Items.MenuContextVars["ModuleValue"] + ".png";

            #endregion
        }
        private void SetStyleButtonMapAdmin()
        {
            //Verifica que seccion esta seleccionada, y le cambia el estilo a los botones Map y Config.
            if (_SelectedModuleSection == "Map")
            {
                ((ImageButton)FwMasterPage.FindControl("btnManageGobalMenuPanel")).CssClass = "globalNavigatorButtonMapOpen";
                ((ImageButton)FwMasterPage.FindControl("btnAdminGobalMenuPanel")).CssClass = "globalNavigatorButtonConfig";
            }
            else
            {
                ((ImageButton)FwMasterPage.FindControl("btnManageGobalMenuPanel")).CssClass = "globalNavigatorButtonMap";
                ((ImageButton)FwMasterPage.FindControl("btnAdminGobalMenuPanel")).CssClass = "globalNavigatorButtonConfigOpen";
            }

            if (_SelectedModuleValue == "Dashboard")
            {
                ((ImageButton)FwMasterPage.FindControl("btnManageGobalMenuPanel")).CssClass = "globalNavigatorButtonMapHidden";
                ((ImageButton)FwMasterPage.FindControl("btnAdminGobalMenuPanel")).CssClass = "globalNavigatorButtonConfigHidden";
            }
        }
        #region Eventos

        //Handler del Menu de Modulos (DS,PM,PA Etc)
        protected void _rmnuHeaderToolbar_ItemClick(object sender, RadMenuEventArgs e)
        {
            if (e.Item.Value == "Dashboard")
            {
                ((ImageButton)FwMasterPage.FindControl("btnManageGobalMenuPanel")).CssClass = "globalNavigatorButtonMapHidden";
                ((ImageButton)FwMasterPage.FindControl("btnAdminGobalMenuPanel")).CssClass = "globalNavigatorButtonConfigHidden";
            }

            _SelectedModuleValue = e.Item.Value;
            _SelectedModuleTitle = e.Item.Text;
            //Vacia el tree del menu, para que se vuelva a cargar.
            _TreeViewGlobalMenuXML = String.Empty;

            String _url = GetNavigateMenuURL();
            String _title = GetNavigateTitle();

            NavigateMenuEventArgs _menuArgs = GetNavigateMenuEventArgs(NavigateMenuType.GlobalMenu, "ModuleNavigation");
            Navigate(_url, _title, _menuArgs);
        }

        //Handler de los Eventos de los SubModulos (Admin-Map-etc)
        void btnGlobalNavigatorHeaderToolbarSubModule_Click(object sender, ImageClickEventArgs e)
        {
            //Se guarda en que Seccion esta seleccionado (Admin o Map)
            _SelectedModuleSection = ((IButtonControl)sender).CommandArgument;
            SetStyleButtonMapAdmin();
            //Vacia el tree del menu, para que se vuelva a cargar.
            _TreeViewGlobalMenuXML = String.Empty;

            String _url = GetNavigateMenuURL();
            String _title = GetNavigateTitle();

            NavigateMenuEventArgs _menuArgs = GetNavigateMenuEventArgs(NavigateMenuType.GlobalMenu, "SubModuleNavigation");
            Navigate(_url, _title, _menuArgs);
        }

        #endregion

        #region Menu Navigation
        private String GetNavigateTitle()
        {
            return String.Concat(_SelectedModuleTitle, " ", _SelectedModuleSection);
        }

        private String GetNavigateMenuURL()
        {
            switch (_SelectedModuleSection.ToUpper())
            {
                case "ADMIN":
                    return "/" + _SelectedModuleValue + "/Configuration.aspx";
                case "MAP":
                    return "/ManagementTools/" + GetManagmentFolder() + "/Map.aspx";
                case "DASHBOARD":
                    //return "/Dashboard/Dashboard.aspx";
                    if (IsOperator())
                    {
                        //Si el usuario logueado es operador de al menos una tarea, va al dashboard operativo
                        return "~/Managers/TaskList.aspx";
                    }
                    else
                    {
                        //Si el usuario no es operador, va al dashboard geografico
                        return "/Dashboard/GeographicDashboardMonitoring.aspx"; //"/Dashboard/GeographicDashboard.aspx";
                    }

                //Son para los Clicks en un Modulo sin seccion (ahora no existen)
                //case ModuleSection.DS:
                //case ModuleSection.IA:
                //case ModuleSection.KC:
                //case ModuleSection.PA:
                //case ModuleSection.PM:
                //case ModuleSection.RM:
                //    return _SelectedModuleValue + "/Default.aspx";
                //    break;
            }

            throw new Exception("The menu could not resolve its destination path. Please contact your System Administrator.");
        }

        private String GetManagmentFolder()
        {
            var _moduleHT = new Dictionary<String, String>();
            _moduleHT.Add("DS", "DirectoryServices");
            _moduleHT.Add("PM", "ProcessesMap");
            _moduleHT.Add("PA", "PerformanceAssessment");
            _moduleHT.Add("KC", "KnowledgeCollaboration");
            _moduleHT.Add("RM", "RiskManagement");
            _moduleHT.Add("IA", "ImprovementActions");
            _moduleHT.Add("CT", "DirectoryServices");
            _moduleHT.Add("Dashboard", "Dashboard");

            return _moduleHT[_SelectedModuleValue];
        }

        
        #endregion

        #endregion

        #endregion

        #region Content Toolbar & Menu

        protected virtual void BuildContentMenu()
        {
            FwMasterPage.ContentNavigatorHeaderTitle("Content Toolbar");
        }
        
        #endregion

        #region BreadCrumb
        //private void BuildBreadCrumb()
        //{
        //    var _breadCrumbHistoryList = new Dictionary<KeyValuePair<Int32, String>, String>();

        //    Int32 _index = 0;
        //    foreach (var _location in Navigator.History)
        //    {
        //         //var _args = new KeyValuePair<Int32, String>(_index, _location.Transference.Items.GetMenuContextVarsQueryString());
        //        var _args = new KeyValuePair<Int32, String>(_index, String.Empty);
        //        _breadCrumbHistoryList.Add(_args, _location.Title);
        //        _index++;
        //    }

        //    //String _args = String.Concat(_SelectedModuleTitle, _SelectedModuleValue, _SelectedModuleSection);
        //    FwMasterPage.BuildBreadCrumbHistoryList(_breadCrumbHistoryList, Navigator.CurrentIndex);
        //}
        ////Es el Handler del Evento dsparado por el BreadCrumb del MasterFW
        //private void HandleFwBreadCrumbClick(MasterFwNavigationEventArgs e)
        //{
        //    this.Navigate(e.NavigatorIndex.Value);
        //}
        /// <summary>
        /// Constructor del BreadCrumb. 
        /// Arma el listado desde su Navigator y se lo pasa al FW para que lo "renderee" con estos datos.
        /// </summary>
        private void BuildBreadCrumb()
        {
            //var _breadCrumbHistoryList = new Dictionary<KeyValuePair<Int32, String>, String>();

            var _breadCrumbHistoryList = new List<BreadCrumbItem>();

            Int32 _index = 0;
            foreach (var _location in Navigator.History)
            {
                BreadCrumbItem _bcItem = new BreadCrumbItem(_index, String.Empty, _location.Title, _index == Navigator.CurrentIndex, _location.IsContextItem);
                _bcItem.SetIndentation(_location.Level);

                _breadCrumbHistoryList.Add(_bcItem);
                _index++;
            }

            FwMasterPage.BuildBreadCrumbHistoryList(_breadCrumbHistoryList);
        }

        //Es el Handler del Evento dsparado por el BreadCrumb del MasterFW
        private void HandleFwBreadCrumbClick(MasterFwNavigationEventArgs e)
        {
            this.Navigate(e.NavigatorIndex.Value);
        }

        /// <summary>
        /// Constructor del Context Info. (Entidades Ppales en la Navegacion)
        /// Arma el listado desde su Navigator y se lo pasa al FW para que lo "renderee" con estos datos.
        /// </summary>
        private void BuildContextInfoPath()
        {
            var _contextInfoPathItems = new List<ContextInfoPathItem>();


            //var _eContextInfoPathItems = from ci in Navigator.History
            //                            where ci.IsContextItem
            //                            select ci;
            Int32 _index = 0;
            foreach (Location _location in Navigator.GetCurrentContextInfoPath())
            {
                if (_location.IsContextItem)
                {
                    ContextInfoPathItem _ciItem = new ContextInfoPathItem(_index, _location.ContextItemTitle, _index == Navigator.CurrentIndex);
                    _contextInfoPathItems.Add(_ciItem);
                }
                _index++;
            }
            FwMasterPage.BuildContextInfoPath(_contextInfoPathItems);
        }

        //Es el Handler del Evento dsparado por el BreadCrumb del MasterFW
        private void HandleFwContextNavigatorPathClick(MasterFwNavigationEventArgs e)
        {
            this.Navigate(e.NavigatorIndex.Value);
        }

        #endregion
        
        #region Navigator
            private Condesus.WebUI.Navigation.Navigator Navigator
            {
                get
                {
                    if (Session["Condesus.WebUI.Navigator"] == null)
                    {
                        Condesus.WebUI.Navigation.Navigator _navigator = new Condesus.WebUI.Navigation.Navigator(Resources.Common.Home, HttpContext.Current.Request.Path, BuildMenuContextVarsCollection(Resources.CommonMenu.radMnuModulePF, "PM", "Map"));
                        //Condesus.WebUI.Navigation.Navigator _navigator = new Condesus.WebUI.Navigation.Navigator("Home", HttpContext.Current.Request.Path, BuildMenuContextVarsCollection(Resources.CommonMenu.radMnuModuleDS, "DS", "Map"));
                        Session["Condesus.WebUI.Navigator"] = _navigator;
                    }

                    return (Condesus.WebUI.Navigation.Navigator)Session["Condesus.WebUI.Navigator"];
                }
            }
            protected String NavigatorPageTitle
            {
                get { return Navigator.Current.Title; }
            }
            protected void Navigate(String url)
            {
                Navigate(url, Navigator.UNDEFINED_TITLE);
            }
            protected void Navigate(String url, String title)
            {
                PersistMenuContentState();
                Navigator.Navigate(title, url, GetPageMenuContextVars());
                PersistMenuContentState();

                DoNavigation(url);
            }
            protected void Navigate(String url, String title, Boolean asWizzardStartPage)
            {
                PersistMenuContentState();
                Navigator.Navigate(title, url, GetPageMenuContextVars(), asWizzardStartPage);
                PersistMenuContentState();

                DoNavigation(url);
            }
            //TODO: NAVIGATOER DELETE-MODIFY - !!!!!!!!!!!!!!!!BORRAR!!!!!!!!!!!!!!!!!!
            protected void Navigate(String url, String title, Condesus.WebUI.Navigation.DeleteType deleteType)
            {
                Navigator.Navigate(title, url, GetPageMenuContextVars(), deleteType);
                PersistMenuContentState();

                DoNavigation(url);
            }
            //Extender e Investigar
            protected void Navigate(String url, String title, NavigateMenuEventArgs args)
            {
                if (args.Args.Count == 0)
                {
                    Navigate(url, title);
                    return;
                }
                //Persisto Antes y Despues de haber movido el Puntero x que tengo que persistir cambios en el actual
                //de expanded(el objeto internamente eso lo resuelve) y tambien el estado al que voy (pasandole un menu
                //construido con los EventArgs (que simula haber sido disparado por ese menu))
                PersistMenuContentState();

                //Voy al nuevo (y simulo un click en el menu, pasandole a la pagina nueva, su menu correspondiente)
                Navigator.Navigate(title, url, GetPageMenuContextVars(), args);

                PersistMenuContentState();

                DoNavigation(url);
            }
            private void Navigate(Int32 indexNavigator)
            {
                //Se Setea Antes de haber movido el Puntero x que el estado le pertenece al actual
                PersistMenuContentState();

                Navigator.Move(indexNavigator);
                DoNavigation(Navigator.Current.Url);
            }
            //protected void Navigate(String url, String title, Condesus.WebUI.Navigation.DeleteType deleteType)
            protected void NavigateDeleted()
            {
                Condesus.WebUI.Navigation.DeleteType _deleteType = Condesus.WebUI.Navigation.DeleteType.DeleteRemovedEntity;
                Navigator.Navigate(_deleteType);

                DoNavigation(Navigator.Current.Url, true);
            }
            protected void NavigateModified()
            {
                NavigateModified(String.Empty);
            }
            //Remueve el Current y llena al Anterior con las variables Necesarias (hay que setearle las variables correctas)
            protected void NavigateModified(String title)
            {
                String _postBackUrl = Navigator.Current.Url;

                Condesus.WebUI.Navigation.DeleteType deleteType = Condesus.WebUI.Navigation.DeleteType.DeleteModifiedEntity;
                Navigator.Navigate(deleteType);

                Navigate(_postBackUrl, title);
            }
            protected void NavigateBack()
            {
                this.Navigate(Navigator.CurrentIndex - 1);
            }
            //Capaz que puede ser protected....
            private void NavigatorReloadCurrent()
            {
                DoNavigation(Navigator.Current.Url);
            }
            protected void NavigatorNavigateBack()
            {
                if (Navigator.CurrentIndex - 1 > 0)
                    Navigate(Navigator.CurrentIndex - 1);
                else
                    Navigate(0);
            }
            protected void NavigatorNavigateBack(Int32 pageCount)
            {
                if (Navigator.CurrentIndex - pageCount > 0)
                    Navigate(Navigator.CurrentIndex - pageCount);
                else
                    Navigate(0);
            }
            /// <summary>
            /// Produce la Navegacion "por libreria de Asp.Net"
            /// </summary>
            /// <param name="url">La URL de la pagina a la cual navegar</param>
            // (Si en el futuro cambia la navegacion de la libreria 'ajena', se implementa 
            //  ese cambio en este metodo; Por ejemplo: Response.Redirect x Server.Transfer)
            private void DoNavigation(String url)
            {
                DoNavigation(url, false);
            }
            private void DoNavigation(String url, Boolean endResponse)
            {
                Response.Redirect(url, endResponse);
            }

        #region Wizzard

        private void InitNavigatorWizzard()
        {
            if (Navigator.IsNavigatingWizzardBranch)
            {
                //Le paso el Save a la MasterContentToolbar
                EventHandler _navigateWizzardEventHandler = new EventHandler(btnNavigateWizzard_Click);
                FwMasterPage.ContentNavigatorToolbarFileActionAdd(_navigateWizzardEventHandler, MasterFwContentToolbarAction.Return, false);
            }
        }

        protected void btnNavigateWizzard_Click(object sender, EventArgs e)
        {
            Navigate(Navigator.WizzardStartPageReturnIndex);
        }

        #endregion

        #region Transfer

        protected TransferenceCollection NavigatorTransferenceCollection
        {
            get { return Navigator.Previous.Transference.Items; }
        }

        protected void NavigatorClearTransferVars()
        {
            Navigator.Current.Transference.Items.Clear();
        }

        protected void NavigatorAddPkEntityIdTransferVar<T>(String name, T value)
        {
            Navigator.Current.Transference.Items.SetEntityId(name, value);
        }

        protected void NavigatorAddTransferVar<T>(String name, T value)
        {
            Navigator.Current.Transference.Items.Add(name, value);
        }

        protected T NavigatorGetPkEntityIdTransferVar<T>(String name)
        {
            return (T)Navigator.Previous.Transference.Items.GetEntityId<T>(name);
        }

        protected T NavigatorGetTransferVar<T>(String name)
        {
            return (T)Navigator.Previous.Transference.Items.Get<T>(name);
        }

        protected Dictionary<String, String> NavigatorGetMenuContextVars()
        {
            return Navigator.Current.Transference.Items.MenuContextVars;
        }

        protected Boolean NavigatorContainsTransferVar(String name)
        {
            try
            {
                return Navigator.Previous.Transference.Items.ContainsKeyAndValue(name);
            }
            catch
            {
                return false;
            }
        }

        protected Boolean NavigatorContainsPkTransferVar(String name)
        {
            try
            {
                return Navigator.Previous.Transference.Items.ContainsPkKeyAndValue(name);
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        #endregion

        #region Navigator State

        #region Persist FormContent State
        /// <summary>
        /// Flagea el control para que su estado se persita.
        /// (El valor del mismo deja de ser Stateless entre "Cross-Postbacks" o Redirects)
        /// </summary>
        public void PersistControlState(WebControl control)
        {
            //Flaguea al control (con que tenga valor (cualquier valor (!= null)) es 'persistent true')
            control.Attributes[Navigator.PERSIST_CONTROL] = Navigator.PERSIST_CONTROL;
        }

        //Constructor o Handler Ppal de Persistencia del Content de la Pagina
        //Se Dispara solamente cuando hay una Navegacion (Response-Redirect)
        //El Handler esta Attacheado al evento UnLoad de la Pagina.
        private void PersistFormContentState()
        {
            PersistStatusState();
            PersistFormControlsState(this.Page);
        }

        //Persiste controles. Es recursivo de grado 1, x que la pagina en si es un 'Control'
        private void PersistFormControlsState(Control pageControls)
        {
            foreach (Control _pageCtrl in pageControls.Controls)
            {
                if (_pageCtrl is WebControl)
                    PersistFormControl((WebControl)_pageCtrl);

                if (_pageCtrl.Controls.Count > 0)
                    PersistFormControlsState(_pageCtrl);
            }
        }

        private void PersistFormControl(WebControl persistantControl)
        {
            //Primero me fijo si el Control esta flageado como Persistible
            if (persistantControl.Attributes[Navigator.PERSIST_CONTROL] != null)
            {
                //Me traigo un StateObject y persito el estado del control segun el Tipo de StateObject que me devuelve
                StateObject _persistableStateObject = StateObjectFactory.GetPersistableStateObject(persistantControl, Navigator.PreviousHistory.State.Items);
                if (_persistableStateObject != null && _persistableStateObject is IPersistable)
                {
                    ((IPersistable)_persistableStateObject).SetValue(persistantControl);
                    Navigator.PreviousHistory.State.Add(_persistableStateObject);
                }
            }
        }

        //Persiste la Status Bar
        //TODO: Manejar los Tipos de Mensajes
        private void PersistStatusState()
        {
            Navigator.PreviousHistory.Status.Result = new StatusObjectSuccess(FwMasterPage.StatusBar.Message);
        }

        #endregion

        #region Persist MenuContentState
        public void PersistMenuControlState(WebControl control)
        {
            //Flaguea al control (con que tenga valor (cualquier valor (!= null)) es 'persistent true')
            control.Attributes[Navigator.PERSIST_MENU] = Navigator.PERSIST_MENU;
        }

        /// <summary>
        /// Solo persiste el Contenido de los Menues (El contenedor especifico Expuesto x La MasterFW para todos los Menues)
        /// </summary>
        private void PersistMenuContentState()
        {
            if (FwMasterPage != null)
            {
                PersistMenuContainerState(FwMasterPage.MenuContainer);
            }
        }

        private void PersistMenuContainerState(Control menuContainer)
        {
            foreach (Control _menuCtrl in menuContainer.Controls)
            {
                if (_menuCtrl is WebControl)
                    PersistMenuControl((WebControl)_menuCtrl);

                if (_menuCtrl.Controls.Count > 0)
                    PersistMenuContainerState(_menuCtrl);
            }
        }

        private void PersistMenuControl(WebControl persistantMenu)
        {
            //Primero me fijo si el Control esta flageado como Persistible
            if (persistantMenu.Attributes[Navigator.PERSIST_MENU] != null)
            {
                //Me traigo un StateObject y persito el estado del control segun el Tipo de StateObject que me devuelve
                StateObject _persistableStateObject = StateObjectFactory.GetPersistableStateObject(persistantMenu, Navigator.Current.State.Items);
                if (_persistableStateObject != null && _persistableStateObject is IPersistable)
                {
                    ((IPersistable)_persistableStateObject).SetValue(persistantMenu);
                    Navigator.Current.State.Add(_persistableStateObject);
                }
            }
        }

        //Persiste el Estado del Menu para una Pagina especifica
        private Dictionary<String, String> GetPageMenuContextVars()
        {
            return BuildMenuContextVarsCollection(_SelectedModuleTitle, _SelectedModuleValue, _SelectedModuleSection);
        }

        private Dictionary<String, String> BuildMenuContextVarsCollection(String moduleTitle, String moduleValue, String moduleSection)
        {
            Dictionary<String, String> _menuVars = new Dictionary<String, String>();

            _menuVars.Add("ModuleTitle", moduleTitle);
            _menuVars.Add("ModuleValue", moduleValue);
            _menuVars.Add("ModuleSection", moduleSection);

            return _menuVars;
        }
        #endregion

        #region LoadState
        //El Handler esta Attacheado al evento Load de la Pagina y solo se dispara en la Inicializacion del la Pagina (!IsPostBack)
        //Recarga el estado del Formulario
        private void LoadContentState()
        {
            foreach (StateObject _stateObject in Navigator.Current.State.Items.Values)
            {

                Control _cntrl = Page.FindControl(_stateObject.Name);
                if (_cntrl != null)
                {
                    if (_cntrl is WebControl)
                    {
                        WebControl _webControl = (WebControl)_cntrl;

                        //Si sos un Menu, siempre te persistis.
                        //Si sos un FormControl, depende de que instancia sos (Seteado por el Navegador internamente)
                        //TODO: ver como obviar este If... Consultar -> P.M.
                        if (_webControl.Attributes[Navigator.PERSIST_MENU] != null
                            || (_webControl.Attributes[Navigator.PERSIST_CONTROL] != null && Navigator.PersistState))
                        {
                            ((IPersistable)_stateObject).GetValue(_webControl);
                        }
                    }
                }
                else
                {
                    //El Control ya no es parte de la pagina (o nunca lo fue: un Menu de Modulo diferente)
                    //Lo Remuevo de su State Objects (Implementar para que no Borre contenido dinamico, que se carga segun Variables de Pagina)
                    //Navigator.Current.State.Items.Remove(_stateObject.Name);
                }
            }
        }

        //Verifico que el Navegador haya sido configurado para trabajar con un Menu. (Validador -> Podria ser una funcion aparte)
        //Si Fue instanciado, seteo variables del Menu.
        private void LoadMenuState()
        {
            try
            {
                if (Navigator.Current.Transference.Items.MenuContextVars.Count > 0)
                {
                    _SelectedModuleTitle = Navigator.Current.Transference.Items.MenuContextVars["ModuleTitle"];
                    _SelectedModuleValue = Navigator.Current.Transference.Items.MenuContextVars["ModuleValue"];
                    _SelectedModuleSection = Navigator.Current.Transference.Items.MenuContextVars["ModuleSection"];
                }
                else
                    throw new Exception();
            }
            catch
            {
                throw new Exception("El Navegador no fue instanciado para navegar en un 'Contexto de Menu' o sus variables de contexto no fueron instanciadas correctamente");
            }
        }

        #endregion

        #endregion

            #region Navigate from Pages
                protected void NavigateEntity(String url, String entityName, String entityPropertyName, String actionTitleDecorator, NavigateMenuAction menuAction)
                {
                    String _entityClassName = GetValueFromGlobalResource("CommonListManage", entityName);
                    //String _entityPropertyName = (menuAction == NavigateMenuAction.Add) ? String.Empty : GetContextInfoCaption(EntityNameGrid, _RgdMasterGridListManage);
                    String _title = String.Concat(entityPropertyName.Replace(", &nbsp;", ""), " [", _entityClassName, "]", actionTitleDecorator);

                    NavigateMenuEventArgs _navArgs = BuildMenuEventArgs(_entityClassName, entityPropertyName, NavigateMenuType.ListManagerMenu, menuAction);

                    Navigate(url, _title, _navArgs);
                }
                protected void NavigateEntity(String url, String entityName, String actionTitleDecorator, NavigateMenuAction menuAction)
                {
                    String _entityClassName = GetValueFromGlobalResource("CommonListManage", entityName);
                    String _entityPropertyName = (menuAction == NavigateMenuAction.Add) ? String.Empty : GetPageTitleForViewer();
                    String _title = String.Concat(_entityPropertyName, " [", _entityClassName, "]", actionTitleDecorator);

                    NavigateMenuEventArgs _navArgs = BuildMenuEventArgs(_entityClassName, _entityPropertyName, NavigateMenuType.ListViewerMenu, menuAction);

                    Navigate(url, _title, _navArgs);
                }
                protected void NavigateEntity(String url, String entityClassName, String entityPropertyName, String actionTitleDecorator, NavigateMenuType menuType)
                {
                    String _entityName = GetValueFromGlobalResource("CommonListManage", entityClassName);
                    String _title = String.Concat(entityPropertyName, " [", _entityName, "]", actionTitleDecorator);

                    NavigateMenuEventArgs _navArgs = BuildMenuEventArgs(entityClassName, entityPropertyName, menuType);

                    Navigate(url, _title, _navArgs);
                }
                protected void NavigateEntity(String url, String entityClassName, String entityPropertyName, NavigateMenuType menuType)
                {
                    String _title = String.Concat(entityPropertyName, " [", entityClassName, "]");

                    NavigateMenuEventArgs _navArgs = BuildMenuEventArgs(entityClassName, entityPropertyName, menuType);

                    Navigate(url, _title, _navArgs);
                }
                protected void NavigateEntity(String url, String entityName, NavigateMenuAction menuAction)
                {
                    NavigateEntity(url, entityName, String.Empty, menuAction);
                }
                protected NavigateMenuEventArgs GetNavigateMenuEventArgs(NavigateMenuType menuType, String menuSection)
                {
                    var argsColl = new Dictionary<String, String>();
                    String _navKey = menuSection; //GetDictionaryKeyForNavigationByPrivateMenu(menuType);
                    argsColl.Add(_navKey, _SelectedModuleSection + _SelectedModuleValue);

                    return new NavigateMenuEventArgs(menuType, argsColl);
                }
            #endregion

        #endregion

        //#region Navigator

        //private Condesus.WebUI.Navigation.Navigator Navigator
        //{
        //    get
        //    {
        //        if (Session["Condesus.WebUI.Navigator"] == null)
        //        {
        //            //Condesus.WebUI.Navigation.Navigator _navigator = new Condesus.WebUI.Navigation.Navigator("Home", HttpContext.Current.Request.Path, BuildMenuContextVarsCollection(Resources.CommonMenu.radMnuModuleDS, "DS", "Admin"));
        //            Condesus.WebUI.Navigation.Navigator _navigator = new Condesus.WebUI.Navigation.Navigator("Home", HttpContext.Current.Request.Path, BuildMenuContextVarsCollection(Resources.CommonMenu.radMnuModuleDS, "DS", "Map"));
        //            Session["Condesus.WebUI.Navigator"] = _navigator;
        //        }

        //        return (Condesus.WebUI.Navigation.Navigator)Session["Condesus.WebUI.Navigator"];
        //    }
        //}

        //protected void Navigate(String url, String title)
        //{
        //    PersistMenuContentState();
        //    Navigator.Navigate(title, url, GetPageMenuContextVars());
        //    PersistMenuContentState();

        //    DoNavigation(url);
        //}

        //protected void Navigate(String url, String title, Condesus.WebUI.Navigation.DeleteType deleteType)
        //{
        //    Navigator.Navigate(title, url, GetPageMenuContextVars(), deleteType);
        //    PersistMenuContentState();

        //    DoNavigation(url);
        //}

        ////Extender e Investigar
        //protected void Navigate(String url, String title, NavigateMenuEventArgs args)
        //{
        //    if (args.Args.Count == 0)
        //    {
        //        Navigate(url, title);
        //        return;
        //    }
        //    //Persisto Antes y Despues de haber movido el Puntero x que tengo que persistir cambios en el actual
        //    //de expanded(el objeto internamente eso lo resuelve) y tambien el estado al que voy (pasandole un menu
        //    //construido con los EventArgs (que simula haber sido disparado por ese menu))
        //    PersistMenuContentState();

        //    //Voy al nuevo (y simulo un click en el menu, pasandole a la pagina nueva, su menu correspondiente)
        //    Navigator.Navigate(title, url, GetPageMenuContextVars(), args);
            
        //    PersistMenuContentState();

        //    DoNavigation(url);
        //}

        //private void Navigate(Int32 indexNavigator)
        //{
        //    //Se Setea Antes de haber movido el Puntero x que el estado le pertenece al actual
        //    PersistMenuContentState();

        //    Navigator.Move(indexNavigator);
        //    DoNavigation(Navigator.Current.Url);
        //}

        //protected void Navigate(String url)
        //{
        //    Navigate(url, Navigator.UNDEFINED_TITLE);
        //}
        //protected void Navigate(String url, String title, Boolean asWizzardStartPage)
        //{
        //    PersistMenuContentState();
        //    Navigator.Navigate(title, url, GetPageMenuContextVars(), asWizzardStartPage);
        //    PersistMenuContentState();

        //    DoNavigation(url);
        //}
        //protected void NavigateBack()
        //{
        //    this.Navigate(Navigator.CurrentIndex - 1);
        //}

        ///// <summary>
        ///// Produce la Navegacion "por libreria de Asp.Net"
        ///// </summary>
        ///// <param name="url">La URL de la pagina a la cual navegar</param>
        //// (Si en el futuro cambia la navegacion de la libreria 'ajena', se implementa 
        ////  ese cambio en este metodo; Por ejemplo: Response.Redirect x Server.Transfer)
        //private void DoNavigation(String url)
        //{
        //    Response.Redirect(url, false);
        //}

        //#region Wizzard
        //    private void InitNavigatorWizzard()
        //    {
        //        if (Navigator.IsNavigatingWizzardBranch)
        //        {
        //            //Le paso el Save a la MasterContentToolbar
        //            EventHandler _navigateWizzardEventHandler = new EventHandler(btnNavigateWizzard_Click);
        //            FwMasterPage.ContentNavigatorToolbarFileActionAdd(_navigateWizzardEventHandler, MasterFwContentToolbarAction.Return, false);
        //        }
        //    }
        //    protected void btnNavigateWizzard_Click(object sender, EventArgs e)
        //    {
        //        Navigate(Navigator.WizzardStartPageReturnIndex);
        //    }
        //#endregion

        //#region Transfer
        //    protected TransferenceCollection NavigatorTransferenceCollection
        //    {
        //        get { return Navigator.Previous.Transference.Items; }
        //    }
        //    protected void NavigatorClearTransferVars()
        //    {
        //        Navigator.Current.Transference.Items.Clear();
        //    }
        //    protected void NavigatorAddPkEntityIdTransferVar<T>(String name, T value)
        //    {
        //        Navigator.Current.Transference.Items.SetEntityId(name, value);
        //    }
        //    protected void NavigatorAddTransferVar<T>(String name, T value)
        //    {
        //        Navigator.Current.Transference.Items.Add(name, value);
        //    }
        //    protected T NavigatorGetPkEntityIdTransferVar<T>(String name)
        //    {
        //        return (T)Navigator.Previous.Transference.Items.GetEntityId<T>(name);
        //    }
        //    protected T NavigatorGetTransferVar<T>(String name)
        //    {
        //        return (T)Navigator.Previous.Transference.Items.Get<T>(name);
        //    }
        //    protected Dictionary<String, String> NavigatorGetMenuContextVars()
        //    {
        //        return Navigator.Current.Transference.Items.MenuContextVars;
        //    }
        //    protected Boolean NavigatorContainsTransferVar(String name)
        //    {
        //        return Navigator.Previous.Transference.Items.ContainsKeyAndValue(name);
        //    }
        //    protected Boolean NavigatorContainsPkTransferVar(String name)
        //    {
        //        return Navigator.Previous.Transference.Items.ContainsPkKeyAndValue(name);
        //    }

        //#endregion

        #region External Method Build TransferVar or Return String PKCompost
            /// <summary>
            /// Este metodo que setea los parametros armados, del registro seleccionado, 
            /// con la estructura que esperan el resto de las paginas a travez del Navigator
            /// </summary>
            /// <param name="rgdListManage"></param>
            protected void BuildNavigateParamsFromListManageSelected(RadGrid rgdListManage)
            {
                String _params = String.Empty;
                for (int i = 0; i < rgdListManage.MasterTableView.DataKeyNames.Count(); i++)
                {
                    //Obtiene el KeyName y su Valor del registo seleccionado en la grilla.
                    Int32 _selectedIndex = rgdListManage.SelectedItems[0].ItemIndex;
                    String _keyName = rgdListManage.MasterTableView.DataKeyNames[i].Trim();
                    //Con esto obtiene el Tipo de datos, para poder castear al momento de guardarlo en el Navigator.
                    Type _columnType = rgdListManage.MasterTableView.Columns.FindByUniqueName(_keyName).DataType;
                    //Obtiene el value para meterlo en el tranfer.
                    String _keyValue = rgdListManage.MasterTableView.DataKeyValues[_selectedIndex][_keyName].ToString();
                    //Finalmente agrega los key en el navigator.
                    NavigatorAddTransferVar(_keyName, Convert.ChangeType(_keyValue, _columnType));
                }
            }
            /// <summary>
            /// Este metodo se encarga de setear los parametros armados desde el String SelectedValue,
            /// con la estructura que esperan el resto de las paginas a travez del Navigator
            /// </summary>
            /// <param name="selectedValue"></param>
            protected void BuildNavigateParamsFromSelectedValue(String selectedValue)
            {
                try
                {
                    //String _params = String.Empty;
                    Dictionary<String, Object> _keys = GetKeyValues(selectedValue);
                    foreach (KeyValuePair<String, Object> _item in _keys)
                    {
                        //Por defecto se convierten en Int64, salvo IdLanguage y ahora PermissionType y PageTitle
                        if ((_item.Key == "IdLanguage") || (_item.Key == "PermissionType") || (_item.Key == "PageTitle") || (_item.Key == "Title") || (_item.Key == "ParentEntity") || (_item.Key == "EntityName"))
                        {
                            //Finalmente agrega los key en el navigator.
                            NavigatorAddTransferVar(_item.Key, _item.Value);
                        }
                        else
                        {
                            //Finalmente agrega los key en el navigator.
                            NavigatorAddTransferVar(_item.Key, Convert.ToInt64(_item.Value));
                        }
                    }
                }
                catch { }
            }
            /// <summary>
            /// Este metodo arma un string con todos los KeyValue en el formato Key=valor&
            /// </summary>
            /// <param name="values"></param>
            protected String BuildStringParamsFromValues(String values)
            {
                String _params = String.Empty;
                try
                {
                    //Convierte el String de values, en el dictionary de Key-value, y recorre
                    Dictionary<String, Object> _values = GetKeyValues(values);

                    foreach (KeyValuePair<String, Object> _item in _values)
                    {
                        //Finalmente concatena los key en un string de parametros.
                        _params += _item.Key + "=" + _item.Value + "&";
                    }
                }
                catch { return String.Empty; }

                if (String.IsNullOrEmpty(_params))
                {
                    return String.Empty;
                }
                else
                {
                    return _params.Substring(0, _params.Length - 1); ;
                }
            }
        #endregion

        //#region Navigator State

        //#region Persist FormContent State
        ///// <summary>
        ///// Flagea el control para que su estado se persita.
        ///// (El valor del mismo deja de ser Stateless entre "Cross-Postbacks" o Redirects)
        ///// </summary>
        //public void PersistControlState(WebControl control)
        //{
        //    //Flaguea al control (con que tenga valor (cualquier valor (!= null)) es 'persistent true')
        //    control.Attributes[Navigator.PERSIST_CONTROL] = Navigator.PERSIST_CONTROL;
        //}

        ////Constructor o Handler Ppal de Persistencia del Content de la Pagina
        ////Se Dispara solamente cuando hay una Navegacion (Response-Redirect)
        ////El Handler esta Attacheado al evento UnLoad de la Pagina.
        //private void PersistFormContentState()
        //{
        //    PersistStatusState();
        //    PersistFormControlsState(this.Page);
        //}

        ////Persiste controles. Es recursivo de grado 1, x que la pagina en si es un 'Control'
        //private void PersistFormControlsState(Control pageControls)
        //{
        //    foreach (Control _pageCtrl in pageControls.Controls)
        //    {
        //        if (_pageCtrl is WebControl)
        //            PersistFormControl((WebControl)_pageCtrl);

        //        if (_pageCtrl.Controls.Count > 0)
        //            PersistFormControlsState(_pageCtrl);
        //    }
        //}

        //private void PersistFormControl(WebControl persistantControl)
        //{
        //    //Primero me fijo si el Control esta flageado como Persistible
        //    if (persistantControl.Attributes[Navigator.PERSIST_CONTROL] != null)
        //    {
        //        //Me traigo un StateObject y persito el estado del control segun el Tipo de StateObject que me devuelve
        //        StateObject _persistableStateObject = StateObjectFactory.GetPersistableStateObject(persistantControl, Navigator.PreviousHistory.State.Items);
        //        if (_persistableStateObject != null && _persistableStateObject is IPersistable)
        //        {
        //            ((IPersistable)_persistableStateObject).SetValue(persistantControl);
        //            Navigator.PreviousHistory.State.Add(_persistableStateObject);
        //        }
        //    }
        //}

        ////Persiste la Status Bar
        ////TODO: Manejar los Tipos de Mensajes
        //private void PersistStatusState()
        //{
        //    Navigator.PreviousHistory.Status.Result = new StatusObjectSuccess(FwMasterPage.StatusBar.Message);
        //}

        //#endregion

        //#region Persist MenuContentState
        //public void PersistMenuControlState(WebControl control)
        //{
        //    //Flaguea al control (con que tenga valor (cualquier valor (!= null)) es 'persistent true')
        //    control.Attributes[Navigator.PERSIST_MENU] = Navigator.PERSIST_MENU;
        //}

        //private void PersistMenuContentState()
        //{
        //    PersistMenuContainerState(FwMasterPage.MenuContainer);
        //}

        //private void PersistMenuContainerState(Control menuContainer)
        //{
        //    foreach (Control _menuCtrl in menuContainer.Controls)
        //    {
        //        if (_menuCtrl is WebControl)
        //            PersistMenuControl((WebControl)_menuCtrl);

        //        if (_menuCtrl.Controls.Count > 0)
        //            PersistMenuContainerState(_menuCtrl);
        //    }
        //}

        //private void PersistMenuControl(WebControl persistantMenu)
        //{
        //    //Primero me fijo si el Control esta flageado como Persistible
        //    if (persistantMenu.Attributes[Navigator.PERSIST_MENU] != null)
        //    {
        //        //Me traigo un StateObject y persito el estado del control segun el Tipo de StateObject que me devuelve
        //        StateObject _persistableStateObject = StateObjectFactory.GetPersistableStateObject(persistantMenu, Navigator.Current.State.Items);
        //        if (_persistableStateObject != null && _persistableStateObject is IPersistable)
        //        {
        //            ((IPersistable)_persistableStateObject).SetValue(persistantMenu);
        //            Navigator.Current.State.Add(_persistableStateObject);
        //        }
        //    }
        //}

        ////Persiste el Estado del Menu para una Pagina especifica
        //private Dictionary<String, String> GetPageMenuContextVars()
        //{
        //    return BuildMenuContextVarsCollection(_SelectedModuleTitle, _SelectedModuleValue, _SelectedModuleSection);
        //}

        //private Dictionary<String, String> BuildMenuContextVarsCollection(String moduleTitle, String moduleValue, String moduleSection)
        //{
        //    Dictionary<String, String> _menuVars = new Dictionary<String, String>();

        //    _menuVars.Add("ModuleTitle", moduleTitle);
        //    _menuVars.Add("ModuleValue", moduleValue);
        //    _menuVars.Add("ModuleSection", moduleSection);

        //    return _menuVars;
        //}
        //#endregion

        //#region LoadState
        ////El Handler esta Attacheado al evento Load de la Pagina y solo se dispara en la Inicializacion del la Pagina (!IsPostBack)
        ////Recarga el estado del Formulario
        //private void LoadContentState()
        //{
        //    foreach (StateObject _stateObject in Navigator.Current.State.Items.Values)
        //    {
        //        Control _cntrl = Page.FindControl(_stateObject.Name);
        //        if (_cntrl != null)
        //        {
        //            if (_cntrl is WebControl)
        //            {
        //                WebControl _webControl = (WebControl)_cntrl;

        //                //Si sos un Menu, siempre te persistis.
        //                //Si sos un FormControl, depende de que instancia sos (Seteado por el Navegador internamente)
        //                //TODO: ver como obviar este If... Consultar -> P.M.
        //                if (_webControl.Attributes[Navigator.PERSIST_MENU] != null
        //                    || (_webControl.Attributes[Navigator.PERSIST_CONTROL] != null && Navigator.PersistState))
        //                {
        //                    ((IPersistable)_stateObject).GetValue(_webControl);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            //El Control ya no es parte de la pagina (o nunca lo fue: un Menu de Modulo diferente)
        //            //Lo Remuevo de su State Objects (Implementar para que no Borre contenido dinamico, que se carga segun Variables de Pagina)
        //            //Navigator.Current.State.Items.Remove(_stateObject.Name);
        //        }
        //    }
        //}

        ////Verifico que el Navegador haya sido configurado para trabajar con un Menu. (Validador -> Podria ser una funcion aparte)
        ////Si Fue instanciado, seteo variables del Menu.
        //private void LoadMenuState()
        //{
        //    try
        //    {
        //        if (Navigator.Current.Transference.Items.MenuContextVars.Count > 0)
        //        {
        //            _SelectedModuleTitle = Navigator.Current.Transference.Items.MenuContextVars["ModuleTitle"];
        //            _SelectedModuleValue = Navigator.Current.Transference.Items.MenuContextVars["ModuleValue"];
        //            _SelectedModuleSection = Navigator.Current.Transference.Items.MenuContextVars["ModuleSection"];
        //        }
        //        else
        //            throw new Exception();
        //    }
        //    catch
        //    {
        //        throw new Exception("El Navegador no fue instanciado para navegar en un 'Contexto de Menu' o sus variables de contexto no fueron instanciadas correctamente");
        //    }
        //}

        //#endregion

        //#endregion


        #region JavaScript
            protected void InjectOnClientContextMenuShowing()
            {
                System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();
                _sbBuffer.Append(OpenHtmlJavaScript());
                _sbBuffer.Append("function onClientContextMenuConfigShowing(sender, args)                                       \n");
                _sbBuffer.Append("{                                                                                             \n");
                _sbBuffer.Append("    var treeNode = args.get_node();                                                           \n");
                _sbBuffer.Append("    treeNode.set_selected(true);                                                              \n");
                _sbBuffer.Append("    setMenuItemsStateMenuConfig(args.get_menu().get_items(), treeNode);                       \n");
                _sbBuffer.Append("}                                                                                             \n");

                _sbBuffer.Append("function setMenuItemsStateMenuConfig(menuItems, treeNode)                                     \n");
                _sbBuffer.Append("{   //Si se hace click derecho sobre el ROOTMap, entonces oculta el Edit                      \n");
                _sbBuffer.Append("    var _permissionType = treeNode.get_attributes().getAttribute('PermissionType')            \n");
                _sbBuffer.Append("    for (var i=0; i<menuItems.get_count(); i++)                                               \n");
                _sbBuffer.Append("    {                                                                                         \n");
                _sbBuffer.Append("        var menuItem = menuItems.getItem(i);                                                  \n");
                _sbBuffer.Append("        if (menuItem.get_value() == 'rmiAdd')                                                 \n");
                _sbBuffer.Append("        {                                                                                     \n");
                _sbBuffer.Append("              var nodeValue = treeNode.get_value();                                           \n");
                _sbBuffer.Append("              if ((nodeValue.indexOf('nodeConfig') == 0) && (_permissionType == 'Manage'))                         \n");
                _sbBuffer.Append("              {   //muestra el Add                                                            \n");
                _sbBuffer.Append("                  menuItem.set_visible(true);                                                 \n");
                _sbBuffer.Append("              }                                                                               \n");
                _sbBuffer.Append("              else                                                                            \n");
                _sbBuffer.Append("              {   //oculta el Add                                                             \n");
                _sbBuffer.Append("                  menuItem.set_visible(false);                                                \n");
                _sbBuffer.Append("              }                                                                               \n");
                _sbBuffer.Append("        }                                                                                     \n");
                _sbBuffer.Append("        if (menuItem.get_value() == 'rmiSecurity')                                            \n");
                _sbBuffer.Append("        {                                                                                     \n");
                _sbBuffer.Append("              //Si el permissionType es View, no se debe mostrar el security                  \n");
                _sbBuffer.Append("              if (_permissionType == 'View')                                                  \n");
                _sbBuffer.Append("              {   //Oculta el Security                                                        \n");
                _sbBuffer.Append("                  menuItem.set_visible(false);                                                \n");
                _sbBuffer.Append("              }                                                                               \n");
                _sbBuffer.Append("              else                                                                            \n");
                _sbBuffer.Append("              {   //Muestra el Security                                                       \n");
                _sbBuffer.Append("                  menuItem.set_visible(true);                                                 \n");
                _sbBuffer.Append("              }                                                                               \n");
                _sbBuffer.Append("        }                                                                                     \n");
                _sbBuffer.Append("    }                                                                                         \n");
                _sbBuffer.Append("}                                                                                             \n");

                _sbBuffer.Append("function onClientContextMenuShowingMenuElementMap(sender, args)                               \n");
                _sbBuffer.Append("{                                                                                             \n");
                _sbBuffer.Append("    var treeNode = args.get_node();                                                           \n");
                _sbBuffer.Append("    treeNode.set_selected(true);                                                              \n");
                _sbBuffer.Append("    setMenuItemsStateMenuElementMap(args.get_menu().get_items(), treeNode);                   \n");
                _sbBuffer.Append("}                                                                                             \n");

                _sbBuffer.Append("function setMenuItemsStateMenuElementMap(menuItems, treeNode)                                 \n");
                _sbBuffer.Append("{   //Si se hace click derecho sobre el ROOTMap, entonces oculta el Edit                      \n");
                _sbBuffer.Append("    var _permissionType = treeNode.get_attributes().getAttribute('PermissionType')            \n");
                _sbBuffer.Append("    for (var i=0; i<menuItems.get_count(); i++)                                               \n");
                _sbBuffer.Append("    {                                                                                         \n");
                _sbBuffer.Append("        var menuItem = menuItems.getItem(i);                                                  \n");
                _sbBuffer.Append("        if (menuItem.get_value() == 'rmiEdit')                                                \n");
                _sbBuffer.Append("        {                                                                                     \n");
                _sbBuffer.Append("              var nodeValue = treeNode.get_value();                                           \n");
                _sbBuffer.Append("              //Si el permissionType es View, no se debe mostrar el edit                      \n");
                _sbBuffer.Append("              if ((nodeValue && nodeValue.indexOf('NodeRootTitle') == 0) || (_permissionType == 'View'))   \n");
                _sbBuffer.Append("              {   //Oculta el Edit                                                            \n");
                _sbBuffer.Append("                  menuItem.set_visible(false);                                                \n");
                _sbBuffer.Append("              }                                                                               \n");
                _sbBuffer.Append("              else                                                                            \n");
                _sbBuffer.Append("              {   //Muestra el Edit                                                           \n");
                _sbBuffer.Append("                  menuItem.set_visible(true);                                                 \n");
                _sbBuffer.Append("              }                                                                               \n");
                _sbBuffer.Append("        }                                                                                     \n");
                _sbBuffer.Append("        if (menuItem.get_value() == 'rmiAdd')                                                 \n");
                _sbBuffer.Append("        {                                                                                     \n");
                _sbBuffer.Append("              //Si el permissionType es View, no se debe mostrar el Add                       \n");
                _sbBuffer.Append("              if ((treeNode.get_attributes().getAttribute('EntityType') == 'Element')  || (_permissionType == 'View')) \n");
                _sbBuffer.Append("              {   //Oculta el Add                                                             \n");
                _sbBuffer.Append("                  menuItem.set_visible(false);                                                \n");
                _sbBuffer.Append("              }                                                                               \n");
                _sbBuffer.Append("              else                                                                            \n");
                _sbBuffer.Append("              {   //Muestra el Add                                                            \n");
                _sbBuffer.Append("                  menuItem.set_visible(true);                                                 \n");
                _sbBuffer.Append("              }                                                                               \n");
                _sbBuffer.Append("        }                                                                                     \n");
                _sbBuffer.Append("        if (menuItem.get_value() == 'rmiSecurity')                                            \n");
                _sbBuffer.Append("        {                                                                                     \n");
                _sbBuffer.Append("              switch (treeNode.get_attributes().getAttribute('SingleEntityName'))             \n");
                _sbBuffer.Append("              { //Todos los mapas, menos organization y process no tienen mas seguridad       \n");
                _sbBuffer.Append("                  case '" + Common.ConstantsEntitiesName.DS.OrganizationClassification + "':  \n");
                _sbBuffer.Append("                  case '" + Common.ConstantsEntitiesName.PF.ProcessClassification + "':       \n");
                _sbBuffer.Append("                  case '" + Common.ConstantsEntitiesName.PA.IndicatorClassification + "':     \n");
                _sbBuffer.Append("                  case '" + Common.ConstantsEntitiesName.PA.Indicator + "':                   \n");
                _sbBuffer.Append("                  case '" + Common.ConstantsEntitiesName.KC.ResourceClassification + "':      \n");
                _sbBuffer.Append("                  case '" + Common.ConstantsEntitiesName.KC.Resource + "':                    \n");
                _sbBuffer.Append("                  case '" + Common.ConstantsEntitiesName.IA.ProjectClassification + "':       \n");
                _sbBuffer.Append("                  case '" + Common.ConstantsEntitiesName.IA.Exception + "':                   \n");
                _sbBuffer.Append("                  case '" + Common.ConstantsEntitiesName.RM.RiskClassification + "':          \n");
                _sbBuffer.Append("                      _permissionType = 'View';                                               \n");
                _sbBuffer.Append("                      break;                                                                  \n");
                _sbBuffer.Append("              }                                                                               \n");
                _sbBuffer.Append("              //Si el permissionType es View, no se debe mostrar el security                  \n");
                _sbBuffer.Append("              if (_permissionType == 'View')                                                  \n");
                _sbBuffer.Append("              {   //Oculta el Security                                                        \n");
                _sbBuffer.Append("                  menuItem.set_visible(false);                                                \n");
                _sbBuffer.Append("              }                                                                               \n");
                _sbBuffer.Append("              else                                                                            \n");
                _sbBuffer.Append("              {   //Muestra el Security                                                       \n");
                _sbBuffer.Append("                  menuItem.set_visible(true);                                                 \n");
                _sbBuffer.Append("              }                                                                               \n");
                _sbBuffer.Append("        }                                                                                     \n");
                _sbBuffer.Append("    }                                                                                         \n");
                _sbBuffer.Append("}                                                                                             \n");

                _sbBuffer.Append(CloseHtmlJavaScript());

                InjectJavascript("JS_onClientContextMenuShowingMenu", _sbBuffer.ToString());
            }
            protected void InjectOnClientContextMenuContextInfoShowing()
            {
                System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();
                _sbBuffer.Append(OpenHtmlJavaScript());
                _sbBuffer.Append("function onClientContextMenuContextInfoShowing(sender, args)                                  \n");
                _sbBuffer.Append("{                                                                                             \n");
                _sbBuffer.Append("    var treeNode = args.get_node();                                                           \n");
                _sbBuffer.Append("    treeNode.set_selected(true);                                                              \n");
                _sbBuffer.Append("    setMenuItemsStateMenuContextInfo(args.get_menu().get_items(), treeNode);                  \n");
                _sbBuffer.Append("}                                                                                             \n");

                _sbBuffer.Append("function setMenuItemsStateMenuContextInfo(menuItems, treeNode)                                \n");
                _sbBuffer.Append("{  //Si se hace click derecho sobre el ROOTMap, entonces oculta el Edit                       \n");
                _sbBuffer.Append("    var _permissionType = treeNode.get_attributes().getAttribute('PermissionType')            \n");
                _sbBuffer.Append("    for (var i=0; i<menuItems.get_count(); i++)                                               \n");
                _sbBuffer.Append("    {                                                                                         \n");
                _sbBuffer.Append("        var menuItem = menuItems.getItem(i);                                                  \n");
                _sbBuffer.Append("        if (menuItem.get_value() == 'rmiAdd')                                                 \n");
                _sbBuffer.Append("        {                                                                                     \n");
                _sbBuffer.Append("              //si el atributo PostBack esta en falso, quiere decir que es un                 \n");
                _sbBuffer.Append("              //un nodo agrupador, entonces no muestra el menu.                               \n");
                _sbBuffer.Append("              //y si el Permiso que tiene es view, tambien oculta el menu.                    \n");
                _sbBuffer.Append("              if ((treeNode.get_postBack()) && (_permissionType == 'Manage'))                 \n");
                _sbBuffer.Append("              {   //muestra el Add                                                            \n");
                _sbBuffer.Append("                  menuItem.set_visible(true);                                                 \n");
                _sbBuffer.Append("              }                                                                               \n");
                _sbBuffer.Append("              else                                                                            \n");
                _sbBuffer.Append("              {   //oculta el Add                                                             \n");
                _sbBuffer.Append("                  menuItem.set_visible(false);                                                \n");
                _sbBuffer.Append("              }                                                                               \n");
                _sbBuffer.Append("        }                                                                                     \n");
                _sbBuffer.Append("    }                                                                                         \n");
                _sbBuffer.Append("}                                                                                             \n");

                _sbBuffer.Append(CloseHtmlJavaScript());

                InjectJavascript("JS_onClientContextMenuContextInfoShowingMenu", _sbBuffer.ToString());
            }
            protected void InjectOnClientContextMenuContextElementShowing()
            {
                System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();
                _sbBuffer.Append(OpenHtmlJavaScript());
                _sbBuffer.Append("function onClientContextMenuContextElementShowing(sender, args)                               \n");
                _sbBuffer.Append("{                                                                                             \n");
                _sbBuffer.Append("    var treeNode = args.get_node();                                                           \n");
                _sbBuffer.Append("    treeNode.set_selected(true);                                                              \n");
                _sbBuffer.Append("    setMenuItemsStateMenuContextElement(args.get_menu().get_items(), treeNode);               \n");
                _sbBuffer.Append("}                                                                                             \n");

                _sbBuffer.Append("function setMenuItemsStateMenuContextElement(menuItems, treeNode)                             \n");
                _sbBuffer.Append("{  //Si se hace click derecho sobre un item que no tiene seguridad lo oculta                  \n");
                _sbBuffer.Append("    var _permissionType = treeNode.get_attributes().getAttribute('PermissionType')            \n");
                _sbBuffer.Append("    for (var i=0; i<menuItems.get_count(); i++)                                               \n");
                _sbBuffer.Append("    {                                                                                         \n");
                _sbBuffer.Append("        var menuItem = menuItems.getItem(i);                                                  \n");
                _sbBuffer.Append("        if (menuItem.get_value() == 'rmiEdit')                                                \n");
                _sbBuffer.Append("        {                                                                                     \n");
                _sbBuffer.Append("              //Si el permissionType es View, no se debe mostrar el edit                      \n");
                _sbBuffer.Append("              if (_permissionType == 'View')                                                  \n");
                _sbBuffer.Append("              {   //Oculta el Edit                                                            \n");
                _sbBuffer.Append("                  menuItem.set_visible(false);                                                \n");
                _sbBuffer.Append("              }                                                                               \n");
                _sbBuffer.Append("              else                                                                            \n");
                _sbBuffer.Append("              {   //Muestra el Edit                                                           \n");
                _sbBuffer.Append("                  menuItem.set_visible(true);                                                 \n");
                _sbBuffer.Append("              }                                                                               \n");
                _sbBuffer.Append("        }                                                                                     \n");
                _sbBuffer.Append("        if (menuItem.get_value() == 'rmiAdd')                                                 \n");
                _sbBuffer.Append("        {                                                                                     \n");
                _sbBuffer.Append("              //Si el permissionType es View, no se debe mostrar el Add                       \n");
                _sbBuffer.Append("              if (_permissionType == 'View')                                                  \n");
                _sbBuffer.Append("              {   //Oculta el Add                                                             \n");
                _sbBuffer.Append("                  menuItem.set_visible(false);                                                \n");
                _sbBuffer.Append("              }                                                                               \n");
                _sbBuffer.Append("              else                                                                            \n");
                _sbBuffer.Append("              {   //Muestra el Add                                                            \n");
                _sbBuffer.Append("                  menuItem.set_visible(true);                                                 \n");
                _sbBuffer.Append("              }                                                                               \n");
                _sbBuffer.Append("        }                                                                                     \n");
                _sbBuffer.Append("        if (menuItem.get_value() == 'rmiSecurity')                                            \n");
                _sbBuffer.Append("        {                                                                                     \n");
                _sbBuffer.Append("              //si el atributo WithSecurity Existe quiere decir que se debe mostrar           \n");
                _sbBuffer.Append("              //el menu de seguridad, sino no.                                                \n");
                _sbBuffer.Append("              if ((treeNode.get_attributes().getAttribute('WithSecurity')) && (_permissionType == 'Manage'))                     \n");
                _sbBuffer.Append("              {   //muestra el Security                                                       \n");
                _sbBuffer.Append("                  menuItem.set_visible(true);                                                 \n");
                _sbBuffer.Append("              }                                                                               \n");
                _sbBuffer.Append("              else                                                                            \n");
                _sbBuffer.Append("              {   //oculta el Security                                                        \n");
                _sbBuffer.Append("                  menuItem.set_visible(false);                                                \n");
                _sbBuffer.Append("              }                                                                               \n");
                _sbBuffer.Append("        }                                                                                     \n");
                _sbBuffer.Append("    }                                                                                         \n");
                _sbBuffer.Append("}                                                                                             \n");

                _sbBuffer.Append(CloseHtmlJavaScript());

                InjectJavascript("JS_onClientContextMenuContextElementShowingMenu", _sbBuffer.ToString());
            }
            protected void InjectOnClientContextMenuContextElementProcessShowing()
            {
                System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();
                _sbBuffer.Append(OpenHtmlJavaScript());
                _sbBuffer.Append("function onClientContextMenuContextElementProcessShowing(sender, args)                                \n");
                _sbBuffer.Append("{                                                                                                     \n");
                _sbBuffer.Append("    var treeNode = args.get_node();                                                                   \n");
                _sbBuffer.Append("    treeNode.set_selected(true);                                                                      \n");
                _sbBuffer.Append("    setMenuItemsStateMenuContextElementProcess(args.get_menu().get_items(), treeNode);                \n");
                _sbBuffer.Append("}                                                                                                     \n");
                _sbBuffer.Append("function isPermissionManage(permissionType)                                                           \n");
                _sbBuffer.Append("{                                                                                                     \n");
                _sbBuffer.Append("  if (permissionType == 'Manage')                                                                     \n");
                _sbBuffer.Append("  {                                                                                                   \n");
                _sbBuffer.Append("      return true;                                                                                    \n");
                _sbBuffer.Append("  }                                                                                                   \n");
                _sbBuffer.Append("  else                                                                                                \n");
                _sbBuffer.Append("  {                                                                                                   \n");
                _sbBuffer.Append("      return false;                                                                                   \n");
                _sbBuffer.Append("  }                                                                                                   \n");
                _sbBuffer.Append("}                                                                                                     \n");

                _sbBuffer.Append("function setMenuItemsStateMenuContextElementProcess(menuItems, treeNode)                              \n");
                _sbBuffer.Append("{  //Si se hace click derecho sobre un item que no tiene seguridad lo oculta                          \n");
                _sbBuffer.Append("    var _permissionType = treeNode.get_attributes().getAttribute('PermissionType');                   \n");
                _sbBuffer.Append("    var _processType = treeNode.get_attributes().getAttribute('ProcessType');                         \n");
                _sbBuffer.Append("    var _entityName = treeNode.get_attributes().getAttribute('EntityName');                                             \n");
                _sbBuffer.Append("    var _hasSubItemAdd = false;                                                                       \n");
                _sbBuffer.Append("    for (var i=0; i<menuItems.get_count(); i++)                                                       \n");
                _sbBuffer.Append("    {                                                                                                 \n");
                _sbBuffer.Append("        var menuItem = menuItems.getItem(i);                                                          \n");
                _sbBuffer.Append("        switch (menuItem.get_value())                                                                 \n");
                _sbBuffer.Append("        {                                                                                             \n");
                _sbBuffer.Append("          case 'rmiEdit':                                                                             \n");
                _sbBuffer.Append("              if ((isPermissionManage(_permissionType)) && (_entityName != ''))                       \n");
                _sbBuffer.Append("              {   //Muestra el Edit                                                                   \n");
                _sbBuffer.Append("                  menuItem.set_visible(true);                                                         \n");
                _sbBuffer.Append("              }                                                                                       \n");
                _sbBuffer.Append("              else                                                                                    \n");
                _sbBuffer.Append("              {   //Oculta el Edit                                                                    \n");
                _sbBuffer.Append("                  menuItem.set_visible(false);                                                        \n");
                _sbBuffer.Append("              }                                                                                       \n");
                _sbBuffer.Append("              break;                                                                                  \n");
                _sbBuffer.Append("          case 'rmiAdd':                                                                              \n");
                _sbBuffer.Append("              for (var x=0; x<menuItem.get_items().get_count(); x++)                                  \n");
                _sbBuffer.Append("              {                                                                                       \n");
                _sbBuffer.Append("                  var menuSubItem = menuItem.get_items().getItem(x);                                  \n");
                _sbBuffer.Append("                  switch (menuSubItem.get_value())                                                    \n");
                _sbBuffer.Append("                  {                                                                                   \n");
                //_sbBuffer.Append("                      case 'rmItemAddNode':                                                           \n");
                //_sbBuffer.Append("                          if ((isPermissionManage(_permissionType)) && ((_processType == 'ProcessGroupNodes') || (_processType == 'ProcessGroupProcess')))                                                                          \n");
                //_sbBuffer.Append("                          {   //Muestra el Edit                                                       \n");
                //_sbBuffer.Append("                              _hasSubItemAdd = true;                                                  \n");
                //_sbBuffer.Append("                              menuSubItem.set_visible(true);                                          \n");
                //_sbBuffer.Append("                          }                                                                           \n");
                //_sbBuffer.Append("                          else                                                                        \n");
                //_sbBuffer.Append("                          {   //Oculta el Edit                                                        \n");
                //_sbBuffer.Append("                              menuSubItem.set_visible(false);                                         \n");
                //_sbBuffer.Append("                          }                                                                           \n");
                //_sbBuffer.Append("                          break;                                                                      \n");
                _sbBuffer.Append("                      case 'rmItemAddTask':                                                               \n");
                //_sbBuffer.Append("                          debugger;                                                                       \n");
                _sbBuffer.Append("                          if ((_processType == undefined) || (_processType.indexOf('ProcessTask') == -1))                                    \n");
                _sbBuffer.Append("                          {                                                                               \n");
                //_sbBuffer.Append("                            if ((isPermissionManage(_permissionType)) && (_processType == 'ProcessGroupNodes'))  \n");
                _sbBuffer.Append("                              if (isPermissionManage(_permissionType))                                    \n");
                _sbBuffer.Append("                              {   //Muestra el Edit                                                       \n");
                _sbBuffer.Append("                                  _hasSubItemAdd = true;                                                  \n");
                _sbBuffer.Append("                                  menuSubItem.set_visible(true);                                          \n");
                _sbBuffer.Append("                              }                                                                           \n");
                _sbBuffer.Append("                              else                                                                        \n");
                _sbBuffer.Append("                              {   //Oculta el Edit                                                        \n");
                _sbBuffer.Append("                                  menuSubItem.set_visible(false);                                         \n");
                _sbBuffer.Append("                              }                                                                           \n");
                _sbBuffer.Append("                          }                                                                           \n");
                _sbBuffer.Append("                          break;                                                                      \n");
                _sbBuffer.Append("                  }                                                                                   \n");
                _sbBuffer.Append("              }                                                                                       \n");
                _sbBuffer.Append("              //Si al menos inserto un add, debe mostrarlo.                                           \n");
                _sbBuffer.Append("              if (_hasSubItemAdd)                                                                     \n");
                _sbBuffer.Append("              {   //Muestra el Add                                                                    \n");
                _sbBuffer.Append("                  menuItem.set_visible(true);                                                         \n");
                _sbBuffer.Append("              }                                                                                       \n");
                _sbBuffer.Append("              else                                                                                    \n");
                _sbBuffer.Append("              {   //Oculta el Add                                                                     \n");
                _sbBuffer.Append("                  menuItem.set_visible(false);                                                        \n");
                _sbBuffer.Append("              }                                                                                       \n");
                _sbBuffer.Append("              break;                                                                                  \n");
                _sbBuffer.Append("          case 'rmiSecurity':                                                                         \n");
                _sbBuffer.Append("              if ((isPermissionManage(_permissionType)) && (_processType == 'ProcessGroupProcess'))   \n");
                _sbBuffer.Append("              {   //Muestra el Edit                                                                   \n");
                _sbBuffer.Append("                  menuItem.set_visible(true);                                                         \n");
                _sbBuffer.Append("              }                                                                                       \n");
                _sbBuffer.Append("              else                                                                                    \n");
                _sbBuffer.Append("              {   //Oculta el Edit                                                                    \n");
                _sbBuffer.Append("                  menuItem.set_visible(false);                                                        \n");
                _sbBuffer.Append("              }                                                                                       \n");
                _sbBuffer.Append("              break;                                                                                  \n");
                _sbBuffer.Append("        }                                                                                             \n");
                _sbBuffer.Append("    }                                                                                                 \n");
                _sbBuffer.Append("}                                                                                                     \n");
                _sbBuffer.Append(CloseHtmlJavaScript());

                InjectJavascript("JS_onClientContextMenuContextElementProcessShowingMenu", _sbBuffer.ToString());
            }
            protected void InjectDragAndDropNodeElementMap()
            {
                System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();
                _sbBuffer.Append(OpenHtmlJavaScript());

                _sbBuffer.Append("function onNodeDroppingElementMap(sender, args)                                                               \n");
                _sbBuffer.Append("{  //Verifica que el nodo que se selecciona, no sea de tipo Element.                                          \n");
                _sbBuffer.Append("   var target = args.get_htmlElement();                                                                       \n");
                _sbBuffer.Append("   var sourceNodes = args.get_sourceNodes();                                                                  \n");
                _sbBuffer.Append("   var destNode = args.get_destNode();                                                                        \n");
                _sbBuffer.Append("   //No hay destino... no se puede hacer nada...                                                              \n");
                _sbBuffer.Append("   if (destNode == null)                                                                                      \n");
                _sbBuffer.Append("   {                                                                                                          \n");
                _sbBuffer.Append("      target.style.cursor = 'not-allowed';                                                                    \n");
                _sbBuffer.Append("      args.set_cancel(true);                                                                                  \n");
                _sbBuffer.Append("      return;                                                                                                 \n");
                _sbBuffer.Append("   }                                                                                                          \n");

                _sbBuffer.Append("   var sourceAttibute = sourceNodes[0].get_attributes().getAttribute('EntityType');                           \n");
                _sbBuffer.Append("   var destAttibute = destNode.get_attributes().getAttribute('EntityType');                                   \n");
                _sbBuffer.Append("   //Primero verifica la seguridad del nodo, su padre y su destino                                            \n");
                _sbBuffer.Append("   var _permissionTypeNodeSource = sourceNodes[0].get_attributes().getAttribute('PermissionType')             \n");
                _sbBuffer.Append("   var _permissionTypeNodeDestination = destNode.get_attributes().getAttribute('PermissionType')              \n");
                _sbBuffer.Append("   var _permissionTypeNodeSourceParent = sourceNodes[0]._parent.get_attributes().getAttribute('PermissionType')      \n");
                _sbBuffer.Append("   if (AllowNodeDragDropByPermission(_permissionTypeNodeSource, _permissionTypeNodeDestination, _permissionTypeNodeSourceParent))     \n");
                _sbBuffer.Append("   {                                                                                                          \n");
                _sbBuffer.Append("      //Se verifica que ni el origen ni el destino sea un Element.                                            \n");
                _sbBuffer.Append("      //Solo se pueden cambiar Clasificaciones.                                                               \n");
                _sbBuffer.Append("      if ((sourceAttibute == 'Element') || (destAttibute == 'Element'))                                       \n");
                _sbBuffer.Append("      {                                                                                                       \n");
                _sbBuffer.Append("          target.style.cursor = 'default';                                                                    \n");
                _sbBuffer.Append("          args.set_cancel(true);                                                                              \n");
                _sbBuffer.Append("          return;                                                                                             \n");
                _sbBuffer.Append("      }                                                                                                       \n");
                _sbBuffer.Append("   }                                                                                                          \n");
                _sbBuffer.Append("   //No tiene permisos en los nodos para moverlos.                                                            \n");
                _sbBuffer.Append("   else                                                                                                       \n");
                _sbBuffer.Append("   {                                                                                                          \n");
                _sbBuffer.Append("      target.style.cursor = 'not-allowed';                                                                        \n");
                _sbBuffer.Append("      args.set_cancel(true);                                                                                  \n");
                _sbBuffer.Append("      return;                                                                                                 \n");
                _sbBuffer.Append("   }                                                                                                          \n");
                _sbBuffer.Append("}                                                                                                             \n");

                _sbBuffer.Append("function AllowNodeDragDropByPermission(permTypeNodeSrc, permTypeNodeDest, permTypeNodeSrcParent)              \n");
                _sbBuffer.Append("{                                                                                                             \n");
                _sbBuffer.Append("   if ((permTypeNodeSrc == 'Manage') && (permTypeNodeDest == 'Manage') && (permTypeNodeSrcParent == 'Manage'))\n");
                _sbBuffer.Append("   {                                                                                                          \n");
                _sbBuffer.Append("      return true;                                                                                            \n");
                _sbBuffer.Append("   }                                                                                                          \n");
                _sbBuffer.Append("   else                                                                                                       \n");
                _sbBuffer.Append("   {                                                                                                          \n");
                _sbBuffer.Append("      return false;                                                                                           \n");
                _sbBuffer.Append("   }                                                                                                          \n");
                _sbBuffer.Append("}                                                                                                             \n");

                _sbBuffer.Append("function onNodeDragging(sender, args)                                                                         \n");
                _sbBuffer.Append("{                                                                                                             \n");
                _sbBuffer.Append("    var target = args.get_htmlElement();                                                                      \n");
                _sbBuffer.Append("    if(!target) return;                                                                                       \n");
                _sbBuffer.Append("    var _entityType = args.get_node().get_attributes().getAttribute('EntityType');                            \n");
                _sbBuffer.Append("    var _permissionType = args.get_node().get_attributes().getAttribute('PermissionType');                    \n");
                _sbBuffer.Append("    if ((_entityType != 'Classification') || (_permissionType == 'View'))                                     \n");
                _sbBuffer.Append("    {                                                                                                         \n");
                _sbBuffer.Append("       target.style.cursor = 'not-allowed';                                                                   \n");
                _sbBuffer.Append("       return;                                                                                                \n");
                _sbBuffer.Append("    }                                                                                                         \n");
                
                _sbBuffer.Append("    if(target.className != 'rtTop Folder' &&                                                                  \n");
                _sbBuffer.Append("       target.className != 'rtMid Folder' &&                                                                  \n");
                _sbBuffer.Append("       target.className != 'rtBot Folder' &&                                                                  \n");
                _sbBuffer.Append("       target.className != 'rtIn Folder')                                                                     \n");
                _sbBuffer.Append("    {                                                                                                         \n");
                _sbBuffer.Append("       target.style.cursor = 'not-allowed';                                                                   \n");
                _sbBuffer.Append("       return;                                                                                                \n");
                _sbBuffer.Append("    }                                                                                                         \n");
                _sbBuffer.Append("}                                                                                                             \n");

                _sbBuffer.Append("function dragDropChangeCursor (sender, eventArgs)                                                             \n");
                _sbBuffer.Append("{                                                                                                             \n");
                _sbBuffer.Append("       var target = eventArgs.get_domEvent().target;                                                          \n");
                _sbBuffer.Append("       target.style.cursor = 'default';                                                                       \n");
                _sbBuffer.Append("       document.body.style.cursor = 'default';                                                                \n");
                _sbBuffer.Append("}                                                                                                             \n");

                _sbBuffer.Append("function ClientNodeDragStart(sender, eventArgs)                                                               \n");
                _sbBuffer.Append("{                                                                                                             \n");
                //Primero verifica que si el tipo no es classificacion, entonces no se puede mover.
                _sbBuffer.Append("   var sourceNode = eventArgs.get_node();                                                                     \n");
                _sbBuffer.Append("    var _entityType = sourceNode.get_attributes().getAttribute('EntityType');                                 \n");
                _sbBuffer.Append("    var target = eventArgs.get_domEvent().srcElement;                                                         \n");
                _sbBuffer.Append("    if(_entityType != 'Classification')                                                                       \n");
                _sbBuffer.Append("    {                                                                                                         \n");
                _sbBuffer.Append("       target.style.cursor = 'not-allowed';                                                                   \n");
                _sbBuffer.Append("       return;                                                                                                \n");
                _sbBuffer.Append("    }                                                                                                         \n");
                //Y ahora verifica la seguridad del class y su padre.
                _sbBuffer.Append("   //Ahora verifica la seguridad del nodo, su padre                                                           \n");
                _sbBuffer.Append("   var _permissionTypeNodeSource = sourceNode.get_attributes().getAttribute('PermissionType')                 \n");
                _sbBuffer.Append("   var _permissionTypeNodeSourceParent = sourceNode._parent.get_attributes().getAttribute('PermissionType')   \n");
                //Si no son manage el y su padre, no se pueden mover.
                _sbBuffer.Append("   if ((_permissionTypeNodeSource != 'Manage') && (_permissionTypeNodeSourceParent != 'Manage'))              \n");
                _sbBuffer.Append("   {                                                                                                          \n");
                _sbBuffer.Append("       target.style.cursor = 'not-allowed';                                                                   \n");
                _sbBuffer.Append("       return;                                                                                                \n");
                _sbBuffer.Append("   }                                                                                                          \n");
                //entonces por las dudas dejo el default.
                _sbBuffer.Append("   target.style.cursor = 'default';                                                                           \n");
                _sbBuffer.Append("}                                                                                                             \n");

                _sbBuffer.Append(CloseHtmlJavaScript());

                InjectJavascript("JS_onNodeDroppingElementMap", _sbBuffer.ToString());
            }

            protected void InjectOnEndRequestSetTreeViewScroll(String clientID)
            {
                System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();
                _sbBuffer.Append(OpenHtmlJavaScript());

                _sbBuffer.Append("  var prm = null;                                                                                         \n");
                //_sbBuffer.Append("  window.attachEvent('onload', InitializeOnEndRequest_" + clientID + ");                                  \n");
                _sbBuffer.Append("  var _IEXPLORER = 'Microsoft Internet Explorer';                                             \n");
                _sbBuffer.Append("  var _FIREFOX = 'Netscape';                                                                  \n");
                _sbBuffer.Append("  var _BrowserName = navigator.appName;                                                       \n");

                _sbBuffer.Append("  if (_BrowserName == _IEXPLORER)                                                             \n");
                _sbBuffer.Append("  {   //IE and Opera                                                                          \n");
                _sbBuffer.Append("      window.attachEvent('onload', InitializeOnEndRequest_" + clientID + ");                                \n");
                _sbBuffer.Append("  }                                                                                           \n");
                _sbBuffer.Append("  else                                                                                        \n");
                _sbBuffer.Append("  {   //FireFox                                                                               \n");
                _sbBuffer.Append("      document.addEventListener('DOMContentLoaded', InitializeOnEndRequest_" + clientID + ", false);        \n");
                _sbBuffer.Append("  }                                                                                           \n");

                _sbBuffer.Append("function InitializeOnEndRequest_" + clientID + "()                                                        \n");
                _sbBuffer.Append("{                                                                                                         \n");
                _sbBuffer.Append("   var prm = Sys.WebForms.PageRequestManager.getInstance();                                               \n");
                _sbBuffer.Append("   prm.add_endRequest(OnEndRequest_" + clientID + ");                                                     \n");
                _sbBuffer.Append("};                                                                                                        \n");

                _sbBuffer.Append("function OnEndRequest_" + clientID + "(sender,args)                                                       \n");
                _sbBuffer.Append("{                                                                                                         \n");
                _sbBuffer.Append("   var treeviewInstance = $find('" + clientID + "');                                                      \n");
                _sbBuffer.Append("   var selectedNode = treeviewInstance.get_selectedNode();                                                \n");
                _sbBuffer.Append("   if (selectedNode != null)                                                                              \n");
                _sbBuffer.Append("   {                                                                                                      \n");
                _sbBuffer.Append("      selectedNode.scrollIntoView();                                                                      \n");
                _sbBuffer.Append("   }                                                                                                      \n");
                _sbBuffer.Append("}                                                                                                         \n");

                _sbBuffer.Append(CloseHtmlJavaScript());

                InjectJavascript("JS_OnEndRequest", _sbBuffer.ToString());
            }
            protected void InjectonNodeExpanded()
            {
                System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();
                _sbBuffer.Append(OpenHtmlJavaScript());

                _sbBuffer.Append("function onNodeExpanded(sender, args)                                                                     \n");
                _sbBuffer.Append("{                                                                                                         \n");
                _sbBuffer.Append("   var selectedNode = args.get_node();                                                         \n");
                _sbBuffer.Append("   if (selectedNode != null)                                                                              \n");
                _sbBuffer.Append("   {                                                                                                      \n");
                _sbBuffer.Append("      selectedNode.scrollIntoView();                                                                      \n");
                _sbBuffer.Append("   }                                                                                                      \n");
                _sbBuffer.Append("}                                                                                                         \n");

                _sbBuffer.Append(CloseHtmlJavaScript());

                InjectJavascript("JS_onNodeExpanded", _sbBuffer.ToString());
            }


        #endregion


    }
}
