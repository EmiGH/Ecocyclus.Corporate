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

using Condesus.EMS.WebUI.MasterControls;

namespace Condesus.EMS.WebUI
{
    public partial class EMS : System.Web.UI.MasterPage
    {


        #region FW Event and EventHandler

        /// <summary>
        /// Occurs when the MasterFW controls (Toolbars, Header, StatusBar etc) raises an Event (typically when the control clicks)
        /// </summary>
        public event Navigate OnNavigation;

        /// <summary>
        /// El Boton (cualquiera del FW) que dispare Eventos "Propios" del FwMaster (controles que no hayan sido attacheados de afuera)
        /// lanzan el evento OnNavigation para todos los clientes que escuchan.
        /// </summary>
        /// <param name="btn">El boton "nativo" del FwMaster que lanzo el evento</param>
        protected void HandleToolbarClick(IButtonControl btn)
        {
            if (OnNavigation != null)
            {
                MasterFwNavigationEventArgs _e = null;

                MasterFwSender _sender = (MasterFwSender)Enum.Parse(typeof(MasterFwSender), btn.CommandName);
                String _title = btn.Text;
                String _args = btn.CommandArgument;

                switch (_sender)
                {
                    case MasterFwSender.Header:
                    case MasterFwSender.GlobalNavigator:
                    case MasterFwSender.ContentNavigator:
                    case MasterFwSender.OperativeDash:
                        _e = new MasterFwNavigationEventArgs(_sender, _title, _args);
                        break;
                    case MasterFwSender.BreadCrumb:
                        Int32 _bcIndexNavigator = Int32.Parse(_args.Split('|')[0]);
                        _e = new MasterFwNavigationEventArgs(_sender, _title, _args, _bcIndexNavigator);
                        break;
                    case MasterFwSender.ContextInfoPath:
                        Int32 _cipIndexNavigator = Int32.Parse(_args);
                        _e = new MasterFwNavigationEventArgs(_sender, _title, _args, _cipIndexNavigator);
                        break;
                }

                this.OnNavigation(this, _e);
            }
        }

        #endregion

        #region Page Load & Init

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            InitializeJavaScriptManager();
            InitializeHandlers();

            BuildMenuPanels();
            BuildToolbars();
            InitJsClientHandlers();

       }

        //Son los handlers de los controles declarados en el "Html" (es decir, no creados x Codigo)
        private void InitializeHandlers()
        {
            btnLogOff.Click += new ImageClickEventHandler(btnLogOff_Click);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            pnyxPageTitle.PageTitle = new Condesus.WebControls.PageTitle(this.PageTitle, this.PageTitleSubTitle, "", this.PageTitleIconURL);
            pnyxStatusBar.Message = Resources.Common.msgStatusPageReady;  // "Ready";

            if (!Page.IsPostBack)
            {
                //Inicializo el Estado del GlobalMenu en su hdn_Flag
                InitGlobalMenuNavigatorVisibilityStateFlag();
            }

            //Inyecta los Scripts del los Eventos que se attachearon al Manager
            InyectJavascriptManagerEventScripts();
        }
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            //Cuando me descargo, ya se el Total de todos los PlugIns que fueron inyectados al FW
            //Inyecto el valor a la Pagina para que lo use el FW en su SetBounds.

            SetFWConstantPlugIns(_pluggInsTotalHeight);
        }
        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);

            if (HttpContext.Current.Response.IsRequestBeingRedirected)
            {
                SetGlobalMenuNavigatorVisibilityState();
            }
        }

        private void InitializeJavaScriptManager()
        {
            _JavaScriptManager = new JavaScriptManager();
        }

        private void InitJsClientHandlers()
        {
            //if (!smEMSMaster.IsInAsyncPostBack)
            InitFwFunctions();
        }

        private void InyectJavascriptManagerEventScripts()
        {
            HttpBrowserCapabilities _Browser = Request.Browser;
            _JavaScriptManager.AttachEvents(_Browser.Browser);
        }
        #endregion


            public String PageTitle
        {
            get { return pnyxPageTitle.Title; }
            set { pnyxPageTitle.Title = Common.Functions.ReplaceIndexesTags(value); }
        }

        public String PageTitleSubTitle
        {
            get { return pnyxPageTitle.SubTitle; }
            set { pnyxPageTitle.SubTitle = value; }
        }

        public String PageTitleIconURL
        {
            get { return pnyxPageTitle.TitleIconURL; }
            set { pnyxPageTitle.TitleIconURL = value; }
        }

        public Pnyx.WebControls.PnyxStatusBar StatusBar
        {
            get { return pnyxStatusBar; }
        }
        //Nombre Fijo del Div que contiene Content
        //Si cambia en el HTML esta acoplado en reglas css y En esta Propiedad
        public String ContentContainerClientId
        {
            get { return "divContent"; }
        }

        #region JavaScript Manager

        private JavaScriptManager _JavaScriptManager;
        public JavaScriptManager JavaScriptManager
        {
            get { return _JavaScriptManager; }
        }

        #endregion

        #region Ajax
        public ScriptManager FWScriptManager
        {
            get { return this.smEMSMaster; }
        }

        public void RegisterContentAsyncPostBackTrigger(Control item, String asyncPostBackTriggerEventName)
        {
            //Attach del control como Trigger del UP
            AsyncPostBackTrigger _upContentBodyTrigger = new AsyncPostBackTrigger();
            _upContentBodyTrigger.ControlID = item.UniqueID;
            _upContentBodyTrigger.EventName = asyncPostBackTriggerEventName;

            upContentBody.Triggers.Add(_upContentBodyTrigger);
        }

        public void RegisterContentPostBackTrigger(Control item)
        {
            //Attach del control como Trigger del UP
            PostBackTrigger _upContentBodyTrigger = new PostBackTrigger();
            _upContentBodyTrigger.ControlID = item.UniqueID;

            upContentBody.Triggers.Add(_upContentBodyTrigger);
        }
        #endregion

        #region Header
        //lblTechnicalSupport.HRef = "mailto:" + (String)ConfigurationSettings.AppSettings["customTechnicalSupportAddressTo"];

        public String HeaderUserName
        {
            set { lblUserName.Text = value; }
        }

        //TODO: Preguntar a Tomacho donde va el CompanyName en Master
        String _verdondevaestecontrolenHeader;
        public String HeaderCompanyName
        {
            set { _verdondevaestecontrolenHeader = value; }
        }

        public String HeaderProductName
        {
            set { lblProductName.Text = value; }
        }

        //OnClick="btnLogOff_Click"
        public String HeaderLogOffText
        {
            set { btnLogOff.ToolTip = value; }
        }

        public String HeaderTecnicalSupportText
        {
            set { lblTechnicalSupportDescription.Text = value; }
        }

        public String HeaderTecnicalSupportEMail
        {
            set { lblTechnicalSupport.HRef = "mailto:" + value; }
        }

        #region Events

        void btnLogOff_Click(object sender, ImageClickEventArgs e)
        {
            IButtonControl _btnLogOff = (IButtonControl)sender;
            _btnLogOff.CommandName = MasterFwSender.Header.ToString();
            _btnLogOff.CommandArgument = "LogOff";

            HandleToolbarClick(_btnLogOff);
        }

        #endregion

        #endregion

        #region Footer

        public String FooterText
        {
            set { lblFooter.Text = value; }
        }

        #endregion

        #region Plug-Ins
        /*
            Esta Contenedor Permite agregar Controles (PlugIn).
            La ubicacion es debajo del Content y Encima del Status Bar.
            ** Para que funcione, los ontroles tiene que tener un alto Fijo y declararlo al Inyectarlo al FW. **
        */

        private Int64 _pluggInsTotalHeight = 0;
        //TODO:para cuando haya que meter los plugin...
        ///// <summary>
        ///// Agrega un Custom Control (Plugin) al FW.
        ///// La ubicacion es debajo del Content y Encima del Status Bar.
        ///// Para que funcione, los ontroles tiene que tener un alto Fijo y declararlo al Inyectarlo al FW
        ///// Para los BoxModels que no utilizan bordes, especificar el ALTO TOTAL del BOXMODEL
        ///// </summary>
        ///// <param name="item">El PlugIn</param>
        ///// <param name="pixelHeight">La altura del PlugIn (Fijo e invariable) en Pixeles</param>
        //public void AddCustomPluginControl(Control item, Unit pixelHeight)
        //{
        //    //Valida Reglas
        //    if (pixelHeight.Type != UnitType.Pixel)
        //        throw new Exception("The PlugIn Height must me specified in pixels");

        //    if (pixelHeight.IsEmpty)
        //        throw new Exception("The PlugIn must have a Height");

        //    //Setea el alto Total para el calculo del Content e injecta la Constante Js a la pagina
        //    Int64 _height = (Int64)pixelHeight.Value;
        //    _pluggInsTotalHeight += _height;

        //    //Injecta el Control al 'PlugIn Container'
        //    phPlugInContainer.Controls.Add(item);
        //}

        #endregion

        #region Toolbars & MenuPanels
        //**************************
        // Toolbar y Menu Functions
        //**************************

        //Los Toolbar Aceptan una coleccion de botones. Sus "href" hacen de navegadores "directos"
        //Si a un boton le queres dar la funcionalidad de Mostrar un "SubMenu" lo tenes que registrar
        //a alguno de los paneles Disponibles (te lo da el Mismo Master, por ahora HC, en el futuro Dinamicos)

        //El Contenido de los MenuPaneles se los seta el que los consume. Esto iria de la mano de la registracion
        //de los botones, pero no esta necesariamente acoplado (podrias mostrar un menu "vacìo")

        //**************************
        //Expone el Contenedor de los menues para el Manejo de Estado
        //(No se deberia usar para agregarle Controles, El uso indebido puede romper la estructura)
        /// <summary>
        /// Devuelve el Contenedor de todos los Menus del FW.
        /// (Solo debe usarse para iterar los Menus)
        /// </summary>
        public PlaceHolder MenuContainer
        {
            get { return cphToolbarMenuPanels; }
        }


        /// <summary>
        /// Agrega un Menu Custom al FW.
        /// (El Handler y la Funcionalidad del mismo la controla el que consume la funcion. El FW solo hace de contendor.)
        /// </summary>
        /// <param name="item">El Control que contiene el Menu</param>
        public void AddCustomMenuPanel(Control item)
        {
            if (item is WebControl)
            {
                WebControl _webItem = (WebControl)item;
                //_webItem.CssClass = "GlobalNavigator";
                _webItem.Style["display"] = "none";
                _webItem.Style["position"] = "absolute";
                _webItem.Style["top"] = "0px";
                _webItem.Style["left"] = "-200px";
                _webItem.Style["z-index"] = "999999";
            }

            cphToolbarMenuPanels.Controls.Add(item);
        }

        #region Global Toolbar & ManuPanel
        #region State Handler

        /// <summary>
        /// El Estado (la visibilidad) del Global Menu en Redirects
        /// </summary>
        public DockingState GlobalNavigatorMenuDockingState
        {
            get
            {
                object _o = Session["MasterFWGlobalNavigatorMenuVisibilityState"];
                if (_o != null)
                    return (DockingState)Enum.Parse(typeof(DockingState), (String)_o);

                return DockingState.UnDocked;
            }
            set { Session["MasterFWGlobalNavigatorMenuVisibilityState"] = value.ToString(); }
        }
        /// <summary>
        /// El Estado (el tipo de breadcrumb seleccionado para ver) del BreadCrumbList en Redirects
        /// </summary>
        public String GlobalNavigatorBreadCrumbCurrentState
        {
            get
            {
                object _o = Session["GlobalNavigatorBreadCrumbCurrentState"];
                if (_o != null)
                    return Convert.ToString(_o);

                return "breadCrumbHistoryList";
            }
            set { Session["GlobalNavigatorBreadCrumbCurrentState"] = value; }
        }
        /// <summary>
        /// Inicializa el Estado del GlobalMenu en su hdn_Flag (Cuando Carga el FW)
        /// </summary>
        private void InitGlobalMenuNavigatorVisibilityStateFlag()
        {
            hdn_globalNavigatorMenuDockingState.Value = GlobalNavigatorMenuDockingState.ToString();
            _GlobalNavigator.MenuDockNavigation = GlobalNavigatorMenuDockingState;
            hdn_globalNavigatorCurrentBreadCrumbState.Value = GlobalNavigatorBreadCrumbCurrentState;
        }

        /// <summary>
        /// Setea el Estado (visible/Invisible) del Global Menu entre Redirects
        /// (Este handler solo se dispara cuando hay un Redirect)
        /// </summary>
        private void SetGlobalMenuNavigatorVisibilityState()
        {
            //GlobalNavigatorMenuDockingState = DockingState.UnDocked;

            //if (_GlobalNavigator.MenuDockNavigation == DockingState.Docked)
            GlobalNavigatorMenuDockingState = (DockingState)Enum.Parse(typeof(DockingState), hdn_globalNavigatorMenuDockingState.Value);
            GlobalNavigatorBreadCrumbCurrentState = hdn_globalNavigatorCurrentBreadCrumbState.Value;
        }

        #endregion
        //El Icono Del HomeButton
        private String _HomeImageUrl = String.Empty;
        public String HomeImageUrl
        {
            set { _HomeImageUrl = value; }
        }

        public void GlobalNavigatorHeaderToolbarContentAdd(Control item)
        {
            _GlobalNavigator.AddMenuHeaderToolbarContent(item);
        }
        public void ContentNavigatorToolbarClear()
        {
            phMasterTableContentToolbar.Controls.Clear();
        }

        public void GlobalNavigatorToolbarAdd(ImageButton button, Boolean opensMenu)
        {
            if (opensMenu)
                button.OnClientClick = RegisterToolbarButton(_GlobalNavigator);

            phMasterTableGlobalToolbar.Controls.Add(button);
        }

        public void GlobalNavigatorToolbarAdd(ImageButton button, String AsyncPostBackTriggerEventName)
        {
            GlobalNavigatorToolbarAdd(button, false);

            RegisterContentAsyncPostBackTrigger(button, AsyncPostBackTriggerEventName);
        }

        public void GlobalNavigatorToolbarMenuContentClear()
        {
            _GlobalNavigator.ClearMenuContent();
        }

        public void GlobalNavigatorToolbarMenuContentAdd(Control item)
        {
            _GlobalNavigator.AddMenuContent(item);
        }

        public void GlobalNavigatorToolbarMenuContentAdd(Control item, String AsyncPostBackTriggerEventName)
        {
            this.GlobalNavigatorToolbarMenuContentAdd(item);

            RegisterContentAsyncPostBackTrigger(item, AsyncPostBackTriggerEventName);
        }

        #endregion

        #region Content Toolbar & ManuPanel
        //#region Content Navigator Multiple
        //public void ContentNavigatorHeaderTitle(String keyItem, String title)
        //{
        //    _ContentNavigators[keyItem].Title = title;
        //}
        //public void ContentNavigatorHeaderToolbarContentAdd(String keyItem, Control item)
        //{
        //    _ContentNavigators[keyItem].AddMenuHeaderToolbarContent(item);
        //}
        //public void ContentNavigatorToolbarAdd(String keyItem, ImageButton button, Boolean opensMenu)
        //{
        //    if (opensMenu)
        //        button.OnClientClick = RegisterToolbarButton(_ContentNavigators[keyItem]);

        //    //Como en la Content Toolbar entra mas de un boton, lo convertimos en Block para que haga el <BR>
        //    button.Style["display"] = "block";

        //    phMasterTableContentToolbar.Controls.Add(button);
        //}
        //public void ContentNavigatorToolbarAdd(String keyItem, ImageButton button, String AsyncPostBackTriggerEventName)
        //{
        //    ContentNavigatorToolbarAdd(keyItem, button, false);

        //    RegisterContentAsyncPostBackTrigger(button, AsyncPostBackTriggerEventName);
        //}
        //public void ContentNavigatorToolbarMenuContentClear(String keyItem)
        //{
        //    _ContentNavigators[keyItem].ClearMenuContent();
        //}
        //public void ContentNavigatorToolbarMenuContentAdd(String keyItem, Control item)
        //{
        //    _ContentNavigators[keyItem].AddMenuContent(item);
        //}
        //public void ContentNavigatorToolbarMenuContentAdd(String keyItem, Control item, String asyncPostBackTriggerEventName)
        //{
        //    this.ContentNavigatorToolbarMenuContentAdd(keyItem, item);

        //    RegisterContentAsyncPostBackTrigger(item, asyncPostBackTriggerEventName);
        //}
        //#endregion

            public void ContentNavigatorHeaderTitle(String title)
            {
                _ContentNavigator.Title = title;
            }
            public void ContentNavigatorHeaderToolbarContentAdd(Control item)
            {
                _ContentNavigator.AddMenuHeaderToolbarContent(item);
            }
            public void ContentNavigatorToolbarAdd(ImageButton button, Boolean opensMenu)
            {
                if (opensMenu)
                    button.OnClientClick = RegisterToolbarButton(_ContentNavigator);

                //Como en la Content Toolbar entra mas de un boton, lo convertimos en Block para que haga el <BR>
                //button.Style["display"] = "block";

                phMasterTableContentToolbar.Controls.Add(button);
            }
            public void ContentNavigatorToolbarAdd(ImageButton button, String AsyncPostBackTriggerEventName)
            {
                ContentNavigatorToolbarAdd(button, false);

                RegisterContentAsyncPostBackTrigger(button, AsyncPostBackTriggerEventName);
            }
            public void ContentNavigatorToolbarMenuContentClear()
            {
                _ContentNavigator.ClearMenuContent();
            }
            public void ContentNavigatorToolbarMenuContentAdd(Control item)
            {
                _ContentNavigator.AddMenuContent(item);
            }
            public void ContentNavigatorToolbarMenuContentAdd(Control item, String asyncPostBackTriggerEventName)
            {
                this.ContentNavigatorToolbarMenuContentAdd(item);

                RegisterContentAsyncPostBackTrigger(item, asyncPostBackTriggerEventName);
            }
        //Las Acciones son Fijas (Delete, Save,...)
        //TODO: Establecer bien el template (CodeSmith) que va a generar las imagenes y todo los relacionado con esto
        //tanto para esta funcionalidad, como el FW en general.
        public void ContentNavigatorToolbarFileActionAdd(EventHandler buttonHandler, MasterFwContentToolbarAction action, Boolean isAsyncPostBack)
        {
            ImageButton _actionButton = BuildGeneralPropertyImageButtonFileAction(action);
            //Me attacho al Delegado
            _actionButton.Click += new ImageClickEventHandler(buttonHandler);
            _actionButton.OnClientClick = "return ShowMasterGlobalUpdateProgress();";

            //Si es Async se registra como tal, sino lo registra como postback normal.
            if (isAsyncPostBack)
            {
                RegisterContentAsyncPostBackTrigger(_actionButton, "Click");
            }
            else
            {
                RegisterContentPostBackTrigger(_actionButton);
            }
            phMasterTableContentToolbarFileActions.Controls.Add(_actionButton);
            //Inyecta el evento JS para que muestre el reloj cuando se presiona el boton save.
            InjectShowUpdateProgressMasterGlobal();
        }
        public void ContentNavigatorToolbarFileActionAdd(String onClientClickNameFunctionJS, MasterFwContentToolbarAction action)
        {
            ImageButton _actionButton = BuildGeneralPropertyImageButtonFileAction(action);
            //Asigna el nombre de la funcion JS para el ClientClick
            _actionButton.OnClientClick = onClientClickNameFunctionJS;

            phMasterTableContentToolbarFileActions.Controls.Add(_actionButton);
        }
        /// <summary>
        /// Este metodo contruye el ImageButton que se utiliza para las acciones de ls pagina (ej. Save, Return, etc.)
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        private ImageButton BuildGeneralPropertyImageButtonFileAction(MasterFwContentToolbarAction action)
        {
            ImageButton _actionButton = new ImageButton();
            _actionButton.ID = "MasterFWContentToolbarAction" + action.ToString();
            _actionButton.CausesValidation = true;
            _actionButton.ImageUrl = "~/Skins/Images/Trans.gif";    //Common.Functions.GetAbsolutePathAppThemes + "/Images/Icons/ContentToolbar/" + action.ToString() + ".png";
            _actionButton.CssClass = GetClassNameByAction(action);
            _actionButton.ToolTip = GetToolTipByAction(action);
            //Como en la Content Toolbar entra mas de un boton, lo convertimos en Block para que haga el <BR>
            //_actionButton.Style["display"] = "block";

            return _actionButton;
        }
        /// Limpia la ContentActionToolbar.
        /// Sirve para cuando el contenido de la Toolbar va a cambiar Dinamicamente 
        /// (entre postbacks) y no solo es para inicializar la Toolbar
        /// </summary>
        public void ContentNavigatorToolbarFileActionClear()
        {
            phMasterTableContentToolbarFileActions.Controls.Clear();
        }
        /// <summary>
        /// Este metodo en base al action que se le pasa busca cual es el stylo que debe inyectarle al ImageButton.
        /// </summary>
        /// <param name="action"></param>
        /// <returns>Un<c>String</c></returns>
        private String GetClassNameByAction(MasterFwContentToolbarAction action)
        {
            switch (action)
            {
                case MasterFwContentToolbarAction.New:
                    break;

                case MasterFwContentToolbarAction.Save:
                    return Common.ConstantsStyleClassName.cssNameButtonSave;

                case MasterFwContentToolbarAction.Delete:
                    break;

                case MasterFwContentToolbarAction.Return:
                    return Common.ConstantsStyleClassName.cssNameButtonReturn;

                case MasterFwContentToolbarAction.Next:
                    return Common.ConstantsStyleClassName.cssNameButtonNext;

                default:
                    break;
            }
            return String.Empty;
        }
        private String GetToolTipByAction(MasterFwContentToolbarAction action)
        {
            switch (action)
            {
                case MasterFwContentToolbarAction.New:
                    return GetGlobalResourceObject("MasterFW", "btnNewTooltip").ToString();

                case MasterFwContentToolbarAction.Save:
                    return GetGlobalResourceObject("MasterFW", "btnSaveTooltip").ToString();

                case MasterFwContentToolbarAction.Delete:
                    return GetGlobalResourceObject("MasterFW", "btnDeleteTooltip").ToString();

                case MasterFwContentToolbarAction.Return:
                    return GetGlobalResourceObject("MasterFW", "btnReturnTooltip").ToString();

                case MasterFwContentToolbarAction.Next:
                    return GetGlobalResourceObject("MasterFW", "btnNextTooltip").ToString();

                default:
                    break;
            }
            return String.Empty;
        }
        public void ContentNavigatorSetCustomMenuWidth(Int32 menuPixelWidth)
        {
            _ContentNavigator.Style["width"] = menuPixelWidth.ToString() + "px";
        }

        #endregion

        #region Custom MenuPanels
        private ToolbarMenuPanel GetCustomMenuPanel(String menuPanelId)
            {
                if (_ContentNavigatorCustomMenuPanels.ContainsKey(menuPanelId))
                    return _ContentNavigatorCustomMenuPanels[menuPanelId];

                throw new MasterFrameWorkException("El panel a que hace referencia no fue registrado");
            }
            List<String> _ContentNavigatorCustomMenuPanelsKeys = new List<String>();
            public void ContentNavigatorCustomMenuPanels(List<String> _menuPanels)
            {
                if (_menuPanels != null && _menuPanels.Count > 0)
                    _ContentNavigatorCustomMenuPanelsKeys = _menuPanels;
            }
            public void ContentNavigatorCustomPanelsToolbarOpenMenuAdd(String menuPanelId, ImageButton button)
            {
                MasterControls.ToolbarMenuPanel _customMenuPanel = GetCustomMenuPanel(menuPanelId);
                //Agregado over para abrir Custom panel
                button.Attributes.Add("onmouseover", "HandleMenuAction(this,'" + _customMenuPanel.ClientID + "', event);");

                button.OnClientClick = RegisterToolbarButton(_customMenuPanel);

                //Como en la Content Toolbar entra mas de un boton, lo convertimos en Block para que haga el <BR>
                //button.Style["display"] = "block";

                phMasterTableContentToolbar.Controls.Add(button);
            }
            public void ContentNavigatorCustomPanelsMenuContentClear()
            {
                foreach (ToolbarMenuPanel _customPanel in _ContentNavigatorCustomMenuPanels.Values)
                    _customPanel.ClearMenuContent();
            }
            public void ContentNavigatorCustomPanelsMenuContentAdd(String menuPanelId, Control item)
            {
                MasterControls.ToolbarMenuPanel _customMenuPanel = GetCustomMenuPanel(menuPanelId);
                _customMenuPanel.AddMenuContent(item);
            }
            public void ContentNavigatorCustomPanelsSetCustomMenuWidth(String menuPanelId, Int32 menuPixelWidth)
            {
                MasterControls.ToolbarMenuPanel _customMenuPanel = GetCustomMenuPanel(menuPanelId);
                _customMenuPanel.Style["width"] = menuPixelWidth.ToString() + "px";
            }
            public void ContentNavigatorCustomPanelsHeaderTitle(String menuPanelId, String title)
            {
                MasterControls.ToolbarMenuPanel _customMenuPanel = GetCustomMenuPanel(menuPanelId);
                _customMenuPanel.Title = title;
            }
            public void ContentNavigatorCustomPanelsHeaderToolbarContentAdd(String menuPanelId, Control item)
            {
                MasterControls.ToolbarMenuPanel _customMenuPanel = GetCustomMenuPanel(menuPanelId);
                _customMenuPanel.AddMenuHeaderToolbarContent(item);
            }
        #endregion

        #endregion

        #region Toolbars & Menu Panels Builders
        MasterControls.ToolbarMenuPanel _ContentNavigator;
        MasterControls.ToolbarMenuPanel _GlobalNavigator;
        private Dictionary<String, MasterControls.ToolbarMenuPanel> _ContentNavigatorCustomMenuPanels;


        private void BuildMenuPanels()
        {
            cphToolbarMenuPanels.Controls.Clear();

            //El orden en que se insertan los panel da la prioridad en el z-index
            //El ultimo tiene mayor z
            BuildContentToolbarMenuPanel();
            BuildGlobalToolbarMenuPanel();
        }
        private void BuildToolbars()
        {
               

            phMasterTableContentToolbar.Controls.Clear();
            phMasterTableGlobalToolbar.Controls.Clear();
            phMasterTableContentToolbarFileActions.Controls.Clear();

            //Content FW Default Buttons
            /* 
              Agregar Botones x Default del Content Toolbar Aca.  
            */

            //Global FW default Buttons
            /* 
              Agregar Botones x Default del Global Toolbar Aca.
            */
            //Esto seria un Boton "Fijo" del FW en el Toolbar Global (Ejemplo: Home)
            //En este caso, el FW dispara el evento para afuera, y el Proyecto navega al Home (porque solo el sabe donde esta...)
            ImageButton btnGlobalToolbarHome = new ImageButton();
            btnGlobalToolbarHome.AlternateText = GetGlobalResourceObject("MasterFW", "btnGlobalToolbarHomeTooltip").ToString();
            btnGlobalToolbarHome.ToolTip = GetGlobalResourceObject("MasterFW", "btnGlobalToolbarHomeTooltip").ToString();
            btnGlobalToolbarHome.CausesValidation = false;
            btnGlobalToolbarHome.ID = "btnGlobalToolbarHomeDashboardFW";
            //btnGlobalToolbarHome.BorderWidth = (1);
            btnGlobalToolbarHome.CommandName = MasterFwSender.GlobalNavigator.ToString();
            //TODO: Setear uno x Default que se setea con el Template (CodeSmith) y exponer una propiedad para poder cambiarlo
            //despues, x Config, se setea en el Xml (HomeButton = "true/false" etc)
            if (String.IsNullOrEmpty(_HomeImageUrl))
            {
                btnGlobalToolbarHome.ImageUrl = "~/Skins/Images/Trans.gif"; //"/Images/Icons/GlobalToolbar/Dashboard.png";
            }
            else
            {
                btnGlobalToolbarHome.ImageUrl = _HomeImageUrl;
            }
            btnGlobalToolbarHome.CssClass = "GlobalToolbarDashboard";

            btnGlobalToolbarHome.CommandArgument = "Home";
            btnGlobalToolbarHome.Click += new ImageClickEventHandler(btnGlobalToolbarHome_Click);
            btnGlobalToolbarHome.OnClientClick = "javascript:ChangeCssClassHome(this);";

            //PersistControlState(btnGlobalToolbarHome);

            GlobalNavigatorToolbarAdd(btnGlobalToolbarHome, false);


            //Agregamos un boton para acceder al Dashboard Operativo!!!
            ImageButton btnGlobalToolbarOperativeDash = new ImageButton();
            btnGlobalToolbarOperativeDash.AlternateText = GetGlobalResourceObject("MasterFW", "btnGlobalToolbarOperativeDashTooltip").ToString();
            btnGlobalToolbarOperativeDash.ToolTip = GetGlobalResourceObject("MasterFW", "btnGlobalToolbarOperativeDashTooltip").ToString();
            btnGlobalToolbarOperativeDash.CausesValidation = false;
            btnGlobalToolbarOperativeDash.ID = "btnGlobalToolbarOperativeDashboardFW";
            //btnGlobalToolbarHome.BorderWidth = (1);
            btnGlobalToolbarOperativeDash.CommandName = MasterFwSender.OperativeDash.ToString();
            //TODO: Setear uno x Default que se setea con el Template (CodeSmith) y exponer una propiedad para poder cambiarlo
            //despues, x Config, se setea en el Xml (HomeButton = "true/false" etc)
            if (String.IsNullOrEmpty(_HomeImageUrl))
            {
                btnGlobalToolbarOperativeDash.ImageUrl = "~/Skins/Images/Trans.gif"; //"/Images/Icons/GlobalToolbar/Dashboard.png";
            }
            else
            {
                btnGlobalToolbarOperativeDash.ImageUrl = _HomeImageUrl;
            }
            btnGlobalToolbarOperativeDash.CssClass = "GlobalToolbarOperativeDash";

            btnGlobalToolbarOperativeDash.CommandArgument = "Home";
            btnGlobalToolbarOperativeDash.Click += new ImageClickEventHandler(btnGlobalToolbarHome_Click);
            btnGlobalToolbarOperativeDash.OnClientClick = "javascript:ChangeCssClassHome(this);";

            //PersistControlState(btnGlobalToolbarHome);

            //NO INSERTAMOS MAS EL ICONO DEL DASHBOARD OPERATIVO!!! 28-03-12
            btnGlobalToolbarOperativeDash.Attributes.Add("display", "none");
            btnGlobalToolbarOperativeDash.Visible = false;
            GlobalNavigatorToolbarAdd(btnGlobalToolbarOperativeDash, false);

        }

        private void BuildGlobalToolbarMenuPanel()
        {
            #region Html
            //<div class="MainMenu" id="globalNavigator" style="display: none;">
            //    <div class="TitleModule">
            //        Process Framework
            //    </div>
            //    <div class="IconsMaphover"></div>
            //    <div class="IconsAdmin"></div>
            //    <div id="GlobalNavigatorPanelTreeViewContainer" class="PanelTreeview"></div>
            //</div>
            #endregion

            _GlobalNavigator = new MasterControls.ToolbarMenuPanel(true, true);
            _GlobalNavigator.ID = "masterGlobalNavigator";
            //Por ahora, por "Reglas del Marco del Fw" es fijo -> ([top = 0] + [Header = 60] + (1px Offset ie) = 71)
            _GlobalNavigator.Top = Unit.Pixel(41);
            //Para Ubicar el Panel
            _GlobalNavigator.DockingZone = MasterControls.DockingZone.Left;

            //El DockingNavifation, para persistir o no el estado del menu entre Redirects
            //_GlobalNavigator.DockingNavigationOnClientClick = "alert(sender)";

            cphToolbarMenuPanels.Controls.Add(_GlobalNavigator);

            _GlobalNavigator.DockingNavigationOnClientClick = "DockGlobalMenuContent(this, event);";

            //Injecta su Javascript para altura Dinamica
            //TODO: Que el que injecta la funcion padre recorra todos los paneles que se registraron
            //y se attache a todas estas funciones...
            //if (!Page.IsPostBack)
            {
                String _jsDynamicHeight = String.Empty;
                _jsDynamicHeight += "function SetGlobalNavigatorBounds(contentHeight) {                                                                     \n";
                _jsDynamicHeight += "   var _globalNavigator = document.getElementById('" + _GlobalNavigator.ClientID + "');                                \n";
                _jsDynamicHeight += "   //InnerContainer                                                                                                    \n";
                //_jsDynamicHeight += "   var _innerContainer = document.getElementById('" + _GlobalNavigator.InnerContainerClientId + "');                 \n";
                _jsDynamicHeight += "   var _innerContainer = _globalNavigator.children[1];                                                                 \n";
                _jsDynamicHeight += "                                                                                                                       \n";
                _jsDynamicHeight += "   contentHeight += _BREAD_CRUMB;                                                                                      \n";
                _jsDynamicHeight += "   contentHeight += _PAGE_TITLE;                                                                                       \n";
                _jsDynamicHeight += "   contentHeight += _FOOTER;                                                                                           \n";
                _jsDynamicHeight += "   contentHeight += 6;  //ieOffset                                                                                     \n";
                _jsDynamicHeight += "                                                                                                                       \n";
                //_jsDynamicHeight += "   //Si esta Dockeado                                                                                                  \n";
                //_jsDynamicHeight += "   if($get('" + hdn_globalNavigatorMenuDockingState.ClientID + "').value == '" + DockingState.Docked.ToString() + "')  \n";
                //_jsDynamicHeight += "      contentHeight += _MENU_DOCKING_HEIGHT;                                                                           \n";
                _jsDynamicHeight += "                                                                                                                       \n";
                _jsDynamicHeight += "   //Seteo Alto del menu (y sus hijos)                                                                                 \n";
                _jsDynamicHeight += "   _globalNavigator.style.height = contentHeight + 'px';                                                               \n";
                _jsDynamicHeight += "                                                                                                                       \n";
                _jsDynamicHeight += "   if((contentHeight - 70) > 0)                                                                                        \n";
                _jsDynamicHeight += "      _innerContainer.style.height = (contentHeight - 70) + 'px';                                                      \n";
                _jsDynamicHeight += "   else                                                                                                                \n";
                _jsDynamicHeight += "      _innerContainer.style.height = '0px';                                                                            \n";
                _jsDynamicHeight += "                                                                                                                       \n";
                _jsDynamicHeight += "   //if(_globalNavigator.style.display == 'block')                                                                     \n";
                _jsDynamicHeight += "       SetMenuPanelLeftPosition(_globalNavigator);                                                                     \n";
                _jsDynamicHeight += "}                                                                                                                      \n";

                _JavaScriptManager.InjectJavascript("JS_SetGlobalNavigatorHeight", _jsDynamicHeight, true);
            }
        }

        private void BuildContentToolbarMenuPanel()
        {
            #region Html
            //<div class="ContentMainMenu" id="contentNavigator" style="display: none;">
            //    <div class="TitleModule">
            //        Process Framework</div>
            //    <div class="PanelTreeview">
            //    </div>
            //</div>
            #endregion


            #region Fw Default Panels
                //AjaxPanel
                _ContentNavigator = new MasterControls.ToolbarMenuPanel(true, false);
                _ContentNavigator.ID = "masterContentNavigator";
                //Por ahora, por "Reglas del Marco del Fw" es fijo -> ([top = 0] + [Header = 60] + [BreadCrumb = 24] + [PageTitle = 60] + (3px Offset ie) = 162)
                _ContentNavigator.Top = Unit.Pixel(135);
                //Para Ubicar el Panel
                _ContentNavigator.DockingZone = MasterControls.DockingZone.Right;
                _ContentNavigator.Title = "";

                cphToolbarMenuPanels.Controls.Add(_ContentNavigator);
            #endregion

            #region Custom Panels
            _ContentNavigatorCustomMenuPanels = new Dictionary<String, ToolbarMenuPanel>();
            MasterControls.ToolbarMenuPanel _CustomContentNavigator = null;
            foreach (String _customPanelID in _ContentNavigatorCustomMenuPanelsKeys)
            {
                _CustomContentNavigator = new MasterControls.ToolbarMenuPanel(true, false);
                _CustomContentNavigator.ID = "masterCustomContentPanel_" + _customPanelID;

                //Por ahora, por "Reglas del Marco del Fw" es fijo -> ([top = 0] + [Header = 60] + [BreadCrumb = 24] + [PageTitle = 60] + (3px Offset ie) = 162)
                _CustomContentNavigator.Top = Unit.Pixel(135);
                //Para Ubicar el Panel
                _CustomContentNavigator.DockingZone = MasterControls.DockingZone.Right;
                _CustomContentNavigator.Title = "";

                cphToolbarMenuPanels.Controls.Add(_CustomContentNavigator);
                _ContentNavigatorCustomMenuPanels.Add(_customPanelID, _CustomContentNavigator);
            }
            #endregion

            #region JavaScript
            //Injecta altura Dinamica para todos los paneles (FW y Registrados)
            String _jsDynamicHeight = String.Empty;
            _jsDynamicHeight += "function SetContentNavigatorBounds(contentHeight) {                                                                \n";
            _jsDynamicHeight += "   var _contentNavigator = document.getElementById('" + _ContentNavigator.ClientID + "');                          \n";
            _jsDynamicHeight += "   //InnerContainer                                                                                                \n";
            _jsDynamicHeight += "   var _innerContainer = _contentNavigator.children[1];                                                            \n";
            _jsDynamicHeight += "                                                                                                                   \n";
            _jsDynamicHeight += "   contentHeight += _PLUGIN_HEIGHT;                                                                                \n";
            _jsDynamicHeight += "                                                                                                                   \n";
            _jsDynamicHeight += "   //Seteo Alto del menu (y sus hijos)                                                                             \n";
            _jsDynamicHeight += "   _contentNavigator.style.height = contentHeight + 'px';                                                          \n";
            _jsDynamicHeight += "                                                                                                                   \n";
            _jsDynamicHeight += "   if((contentHeight - 65) > 0)                                                                                    \n";
            _jsDynamicHeight += "   {                                                                                                               \n";
            _jsDynamicHeight += "      _innerContainer.style.height = (contentHeight - 65) + 'px';                                                  \n";
            _jsDynamicHeight += "   }                                                                                                               \n";
            _jsDynamicHeight += "   else                                                                                                            \n";
            _jsDynamicHeight += "   {                                                                                                               \n";
            _jsDynamicHeight += "      _innerContainer.style.height = '0px';                                                                        \n";
            _jsDynamicHeight += "   }                                                                                                               \n";
            _jsDynamicHeight += "                                                                                                                   \n";
            _jsDynamicHeight += "   if(_contentNavigator.style.display == 'block')                                                                  \n";
            _jsDynamicHeight += "       SetMenuPanelLeftPosition(_contentNavigator);                                                                \n";
            _jsDynamicHeight += "                                                                                                                   \n";
            _jsDynamicHeight += "                                                                                                                   \n";
            //Los Custom
            foreach (var _customPanel in _ContentNavigatorCustomMenuPanels)
            {
                //El objeto MenuPanel
                String _jsVarNameContainer = "_contentNavigator" + _customPanel.Key;
                String _jsVarNameInnerContainer = "_innerContainer" + _jsVarNameContainer;
                _jsDynamicHeight += "   var " + _jsVarNameContainer + " = document.getElementById('" + _customPanel.Value.ClientID + "');           \n";
                //Su InnerContainer
                _jsDynamicHeight += "   var " + _jsVarNameInnerContainer + " = " + _jsVarNameContainer + ".children[1];                             \n";
                //Seteo Alto del menu (y sus hijos)
                _jsDynamicHeight += "   " + _jsVarNameContainer + ".style.height = contentHeight + 'px';                                            \n";
                _jsDynamicHeight += "                                                                                                               \n";
                _jsDynamicHeight += "   if((contentHeight - 65) > 0)                                                                                \n";
                _jsDynamicHeight += "      " + _jsVarNameInnerContainer + ".style.height = (contentHeight - 65) + 'px';                             \n";
                _jsDynamicHeight += "   else                                                                                                        \n";
                _jsDynamicHeight += "      " + _jsVarNameInnerContainer + ".style.height = '0px';                                                   \n";
                _jsDynamicHeight += "                                                                                                               \n";
                _jsDynamicHeight += "   if(" + _jsVarNameContainer + ".style.display == 'block')                                                    \n";
                _jsDynamicHeight += "       SetMenuPanelLeftPosition(" + _jsVarNameContainer + ");                                                  \n";
            }
            _jsDynamicHeight += "                                                                                                                   \n";
            _jsDynamicHeight += "}                                                                                                                  \n";

            _JavaScriptManager.InjectJavascript("JS_SetContentNavigatorHeight", _jsDynamicHeight, true);
            #endregion


            //_ContentNavigator = new MasterControls.ToolbarMenuPanel(true, false);
            //_ContentNavigator.ID = "masterContentNavigator";
            ////Por ahora, por "Reglas del Marco del Fw" es fijo -> ([top = 0] + [Header = 41] + [BreadCrumb = 30] + [PageTitle = 60] + (4px Offset ie) = 135)
            //_ContentNavigator.Top = Unit.Pixel(135);
            ////Para Ubicar el Panel
            //_ContentNavigator.DockingZone = MasterControls.DockingZone.Right;

            //_ContentNavigator.Title = "Process FrameWork Test";
            //cphToolbarMenuPanels.Controls.Add(_ContentNavigator);

            //Injecta su Javascript para altura Dinamica
            //TODO: Que el que injecta la funcion padre recorra todos los paneles que se registraron
            //y se attache a todas estas funciones...
            //if (!Page.IsPostBack)
            //{
            //    String _jsDynamicHeight = String.Empty;
            //    _jsDynamicHeight += "function SetContentNavigatorBounds(contentHeight) {                                                                \n";

            //    _jsDynamicHeight += "   var _contentNavigator = document.getElementById('" + _ContentNavigator.ClientID + "');                          \n";
            //    _jsDynamicHeight += "   //InnerContainer                                                                                                \n";
            //    //_jsDynamicHeight += "   var _innerContainer = document.getElementById('" + _ContentNavigator.InnerContainerClientId + "');              \n";
            //    _jsDynamicHeight += "   var _innerContainer = _contentNavigator.children[1];                                                            \n";
            //    _jsDynamicHeight += "   //Seteo Alto del menu (y sus hijos)                                                                             \n";
            //    _jsDynamicHeight += "   _contentNavigator.style.height = contentHeight + 'px';                                                          \n";
            //    _jsDynamicHeight += "   if((contentHeight - 65) > 0)                                                                                    \n";
            //    _jsDynamicHeight += "      _innerContainer.style.height = (contentHeight - 65) + 'px';                                                  \n";
            //    _jsDynamicHeight += "   else                                                                                                            \n";
            //    _jsDynamicHeight += "      _innerContainer.style.height = '0px';                                                                        \n";
            //    _jsDynamicHeight += "                                                                                                                   \n";
            //    _jsDynamicHeight += "   if(_contentNavigator.style.display == 'block')                                                                  \n";
            //    _jsDynamicHeight += "       SetMenuPanelLeftPosition(_contentNavigator);                                                                \n";
            //    _jsDynamicHeight += "}                                                                                                                  \n";

            //    _JavaScriptManager.InjectJavascript("JS_SetContentNavigatorHeight", _jsDynamicHeight, true);
            //}
            
        }

        #region Events

        void btnGlobalToolbarHome_Click(object sender, ImageClickEventArgs e)
        {
            HandleToolbarClick((IButtonControl)sender);
        }

        #endregion

        #endregion

        #region BreadCrumb
        //Tiene un Indice (puntero) y un Titulo. Todos los eventos pasan por el handler y Disparan el evento 'OnNavigation' del FwMasterPage
        //public void BuildBreadCrumbHistoryList(Dictionary<KeyValuePair<Int32, String>, String> breadCrumbList, Int32 currentIndex)
        //{
        //    phBreadCrumbHistoryList.Controls.Clear();

        //    Int32? _lastIndex = null;

        //    if (breadCrumbList != null)
        //    {
        //        _lastIndex = breadCrumbList.Count - 1;

        //        foreach (var _kvPairBreadCrumb in breadCrumbList)
        //        {
        //            Int32 _itemIndex = _kvPairBreadCrumb.Key.Key;
        //            String _itemArgs = _kvPairBreadCrumb.Key.Value;
        //            String _itemText = _kvPairBreadCrumb.Value;

        //            if (_itemIndex == currentIndex)
        //            {
        //                lnkBreadCrumbCurrent.Text = _itemText;
        //                Label _lblCurrent = new Label();
        //                _lblCurrent.ID = "lblBcCurrent" + _itemIndex.ToString();
        //                _lblCurrent.Text = _itemText;

        //                phBreadCrumbHistoryList.Controls.Add(_lblCurrent);
        //            }
        //            else
        //            {
        //                LinkButton _tmpButton = new LinkButton();
        //                _tmpButton.ID = "btnBC" + _itemIndex.ToString();
        //                _tmpButton.CausesValidation = false;
        //                _tmpButton.Text = _itemText;
        //                _tmpButton.CommandArgument = _itemIndex.ToString() + "|" + _itemArgs; ;
        //                _tmpButton.CommandName = MasterFwSender.BreadCrumb.ToString();
        //                _tmpButton.Click += new EventHandler(BreadCrumbHistoryListBtn_Click);

        //                phBreadCrumbHistoryList.Controls.Add(_tmpButton);
        //            }
        //        }
        //    }

        //    SetBreadCrumbNavigatorState(currentIndex, _lastIndex);
        //}
        public void BuildBreadCrumbHistoryList(List<BreadCrumbItem> breadCrumbList)
        {
            phBreadCrumbHistoryList.Controls.Clear();

            Int32? _lastIndex = null;
            Int32 _currentIndex = 0;

            if (breadCrumbList != null)
            {
                _lastIndex = breadCrumbList.Count - 1;

                foreach (BreadCrumbItem _bcItem in breadCrumbList)
                {
                    if (_bcItem.IsCurrent)
                    {
                        _currentIndex = _bcItem.ItemIndex;
                        lnkBreadCrumbCurrent.Text = Common.Functions.ReplaceIndexesTags(_bcItem.ItemTitle);

                        Label _lblCurrent = GetCurrentLabel("lblBC_Current", _bcItem.ItemIndex, Common.Functions.ReplaceIndexesTags(_bcItem.ItemTitle));
                        //_lblCurrent.Text = String.Concat(_bcItem.Indentation, _lblCurrent.Text);
                        SetBreadCrumbIndentationLevel(_lblCurrent, _bcItem.ItemIndentationLevel);
                        if (_bcItem.IsContextInfoPathItem)
                            SetBreadCrumbContextInfoItemStyle(_lblCurrent);

                        phBreadCrumbHistoryList.Controls.Add(_lblCurrent);
                    }
                    else
                    {
                        LinkButton _bcButton = GetListLinkButton("btnBC", _bcItem.ItemIndex, _bcItem.ItemTitle, MasterFwSender.BreadCrumb, new EventHandler(BreadCrumbHistoryListBtn_Click));
                        //_bcButton.Text = String.Concat(_bcItem.Indentation, _bcButton.Text);
                        SetBreadCrumbIndentationLevel(_bcButton, _bcItem.ItemIndentationLevel);
                        if (_bcItem.IsContextInfoPathItem)
                            SetBreadCrumbContextInfoItemStyle(_bcButton);



                        phBreadCrumbHistoryList.Controls.Add(_bcButton);
                    }
                }
            }

            SetBreadCrumbNavigatorState(_currentIndex, _lastIndex);
        }

        private void SetBreadCrumbNavigatorState(Int32 currentIndex, Int32? lastIndex)
        {
            String _imagePath = "~/Skins/Images/";

            //Los botones estan deshabilitados por default. Segun el estado del Listado se habilitan
            //btnBreadCrumbBack.ImageUrl = _imagePath + "BackDisabled.png";
            btnBreadCrumbBack.CssClass = "breadCrumbNavigateButtonBackDisabled";
            btnBreadCrumbBack.CommandName = MasterFwSender.BreadCrumb.ToString();
            btnBreadCrumbBack.Enabled = false;
            btnBreadCrumbBack.CausesValidation = false;
            btnBreadCrumbBack.ToolTip = GetGlobalResourceObject("MasterFW", "btnBreadCrumbBackTooltip").ToString();

            //btnBreadCrumbNext.ImageUrl = _imagePath + "FowardDisabled.png";
            btnBreadCrumbNext.CssClass = "breadCrumbNavigateButtonNextDisabled";
            btnBreadCrumbNext.CommandName = MasterFwSender.BreadCrumb.ToString();
            btnBreadCrumbNext.Enabled = false;
            btnBreadCrumbNext.CausesValidation = false;
            btnBreadCrumbNext.ToolTip = GetGlobalResourceObject("MasterFW", "btnBreadCrumbNextTooltip").ToString();

            btnBreadCrumbHistory.ToolTip = GetGlobalResourceObject("MasterFW", "btnBreadCrumbHistoryTooltip").ToString();

            btnBreadCrumbBack.Click += new ImageClickEventHandler(btnBreadCrumbNavigator_Click);
            btnBreadCrumbNext.Click += new ImageClickEventHandler(btnBreadCrumbNavigator_Click);

            //Si hay items en el HistoryList y si no es uno solo
            if (lastIndex.HasValue && lastIndex.Value > 0)
            {
                if (currentIndex >= 1)
                {
                    btnBreadCrumbBack.ImageUrl = _imagePath + "Trans.gif";
                    btnBreadCrumbBack.CssClass = "breadCrumbNavigateButtonBack";
                    btnBreadCrumbBack.Enabled = true;
                    btnBreadCrumbBack.CommandArgument = (currentIndex - 1).ToString();
                }

                if (currentIndex < lastIndex.Value)
                {
                    btnBreadCrumbNext.ImageUrl = _imagePath + "Trans.gif";
                    btnBreadCrumbNext.CssClass = "breadCrumbNavigateButtonNext";
                    btnBreadCrumbNext.CommandArgument = (currentIndex + 1).ToString();
                    btnBreadCrumbNext.Enabled = true;
                }
            }
        }

        void btnBreadCrumbNavigator_Click(object sender, ImageClickEventArgs e)
        {
            HandleToolbarClick((IButtonControl)sender);
        }

        #region Events

        /// <summary>
        /// El click desde un Item del BreadCrumb History List (del listado desplegable)
        /// </summary>
        void BreadCrumbHistoryListBtn_Click(object sender, EventArgs e)
        {
            HandleToolbarClick((IButtonControl)sender);
        }

        #endregion

        #endregion

        #region Context Info Path

        public void BuildContextInfoPath(List<ContextInfoPathItem> contextInfoPathItems)
        {
            phContextInfoPath.Controls.Clear();

            if (contextInfoPathItems != null)
            {
                if (contextInfoPathItems.Count == 0)
                {
                    Label _lblCurrent = GetCurrentLabel("lbl_CIP_NoItems", 0,  GetLocalResourceObject("NoItemsAvailable").ToString());
                    phContextInfoPath.Controls.Add(_lblCurrent);

                    return;
                }

                foreach (ContextInfoPathItem _ci in contextInfoPathItems)
                {
                    if (_ci.IsCurrent)
                    {
                        Label _lblCurrent = GetCurrentLabel("lbl_CIP_Current", _ci.ItemIndex, _ci.ItemTitle);
                        phContextInfoPath.Controls.Add(_lblCurrent);
                    }
                    else
                    {
                        LinkButton _cipButton = GetListLinkButton("btnCIP", _ci.ItemIndex, _ci.ItemTitle, MasterFwSender.ContextInfoPath, new EventHandler(ContextInfoPathBtn_Click));
                        phContextInfoPath.Controls.Add(_cipButton);
                    }
                }
            }
        }

        #region Events

        /// <summary>
        /// El click desde un Item del BreadCrumb History List (del listado desplegable)
        /// </summary>
        void ContextInfoPathBtn_Click(object sender, EventArgs e)
        {
            HandleToolbarClick((IButtonControl)sender);
        }

        #endregion

        #endregion

        #region HelperMethods
            private Label GetCurrentLabel(String controlName, Int32 index, String text)
            {
                Label _lblCurrent = new Label();
                _lblCurrent.ID = String.Concat(controlName, index.ToString());
                _lblCurrent.Text = text;
                return _lblCurrent;
            }
            private LinkButton GetListLinkButton(String controlName, Int32 index, String text, MasterFwSender listType, EventHandler clickEventHandler)
            {
                LinkButton _retButton = new LinkButton();
                _retButton.ID = String.Concat(controlName, index.ToString());
                _retButton.CausesValidation = false;
                _retButton.Text = text;
                _retButton.CommandArgument = index.ToString();
                _retButton.CommandName = listType.ToString();
                _retButton.Click += clickEventHandler;

                return _retButton;
            }
            private void SetBreadCrumbIndentationLevel(WebControl bcItem, Int32 level)
            {
                bcItem.Style["padding-left"] = (level*8).ToString() + "px";
            }
            private void SetBreadCrumbContextInfoItemStyle(WebControl bcItem)
            {
                bcItem.Style["color"] = "blue";
                bcItem.Style["font-size"] = "10px";
            }
        #endregion

        #region JavaScript -Marco Cliente-
        //Los javascripts Propios del Marco de la MasterPage del FW Cliente.

        private void InitFwFunctions()
        {
            _JavaScriptManager.AttachEvent("load", "FW_OnLoad");
            _JavaScriptManager.AttachEvent("resize", "SetFwBounds");

            SetFWOnLoadHandler();
            SetFWConstants();
            SetFwBounds();
            SetFWDockingFunctions();
            InjectAjaxHandlers();
            HandleMenuAction();
            SetGlobalNavigatorMenuState();
            SetFWBreadCrumbListHadlers();
        }

        private void SetFWBreadCrumbListHadlers()
        {
            System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

            _sbBuffer.Append("function GetCurrentBreadCrumbListId()                                                                     \n");
            _sbBuffer.Append("{                                                                                                         \n");
            _sbBuffer.Append("    return document.getElementById('" + hdn_globalNavigatorCurrentBreadCrumbState.ClientID + "').value;     \n");
            _sbBuffer.Append("}                                                                                                         \n");

            _sbBuffer.Append("function SetCurrentBreadCrumbListId(listId)                                                               \n");
            _sbBuffer.Append("{                                                                                                         \n");
            _sbBuffer.Append("   document.getElementById('" + hdn_globalNavigatorCurrentBreadCrumbState.ClientID + "').value = listId;  \n");
            _sbBuffer.Append("}                                                                                                         \n");

            _JavaScriptManager.InjectJavascript("JS_FW_BreadCrumbListHadlers", _sbBuffer.ToString(), true);
        }

        /// <summary>
        /// Define la funcion OnLoad para las Funciones que son 'funcionalmente dependientes'.
        /// </summary>
        //_JavaScriptManager.AttachEvent("load", "SetGlobalMenuNavigatorVisibilityState");
        //_JavaScriptManager.AttachEvent("load", "SetFwBounds");
        //_JavaScriptManager.AttachEvent("load", "InitializeMasterFwAjaxHandlers");
        private void SetFWOnLoadHandler()
        {
            System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

            _sbBuffer.Append("function FW_OnLoad()                                                                                              \n");
            _sbBuffer.Append("{                                                                                                                 \n");
            _sbBuffer.Append("    SetGlobalMenuNavigatorVisibilityState();                                                                      \n");
            _sbBuffer.Append("    SetFwBounds();                                                                                                \n");
            _sbBuffer.Append("    InitializeMasterFwAjaxHandlers();                                                                             \n");
            _sbBuffer.Append("    SetClassButtonModule();                                                                                       \n");
            _sbBuffer.Append("}                                                                                                                 \n");

            _JavaScriptManager.InjectJavascript("JS_FW_ONLOAD", _sbBuffer.ToString(), true);
        }
 
        private void SetFWConstants()
        {
            System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

            //FW Bounds constants
            //*******************************************************************************************************************************
            //Medidas de Offset
            //*******************
            //WIDTH
            _sbBuffer.Append("var _GLOBAL_TOOLBAR = 30;                                                                                     \n");
            //Separator x2 (son los separadores a la Izq u Derecha del Marco
            _sbBuffer.Append("var _SEPARATORS = 12;                                                                                         \n");
            _sbBuffer.Append("var _CONTENT_TOOLBAR = 30;                                                                                    \n");
            _sbBuffer.Append("var _OFFSET_IEXPLORER_WIDTH = 4;                                                                              \n");
            //HEIGHT
            _sbBuffer.Append("var _HEADER = 38;                                                                                             \n");
            _sbBuffer.Append("var _BREAD_CRUMB = 30;                                                                                        \n");
            _sbBuffer.Append("var _PAGE_TITLE = 60;                                                                                         \n");
            _sbBuffer.Append("var _STATUS_BAR = 30;                                                                                         \n");
            _sbBuffer.Append("var _FOOTER = 27;                                                                                             \n");
            _sbBuffer.Append("var _OFFSET_IEXPLORER_HEIGHT = 14;                                                                             \n");

            //Medidas de Doqueo
            //******************* (Lo que le sumo/resto al ancho Td que simula el Docking. 
            //                     Las Dimensiones de este Td son FIJAS y no dinamicas x incomp. del getBounds con el Td)
            //El ancho del DockingZone al estar dockeado (El ancho del menu x Regla Css ".ContentNavigator") 
            _sbBuffer.Append("var _MENU_DOCKING_WIDTH = 307;                                                                                \n");
            //El ancho del DockingZone al estar desdockeado (el ancho del Docking zone x Regla .css ".masterTableContentSeparation")
            _sbBuffer.Append("var _MENU_DOCKING_WIDTH_UNDOCKED = 3;                                                                         \n");
            //ie offset de los margenes derechos e izq del MenuPanel + un Borde de 1px
            _sbBuffer.Append("var _MENU_DOCKING_WIDTH_IEOFFSET = 31;                                                                        \n");
            //Lo que le sumo/resto a la altura del menu al estar Doqueado/Desdoqueado 
            //para que no me quede espacio vacìo en el footer
            //(Esta altura es = al StatusBar height)
            _sbBuffer.Append("var _MENU_DOCKING_HEIGHT = 24;                                                                                \n");

            _JavaScriptManager.InjectJavascript("JS_FW_CONSTANTS", _sbBuffer.ToString(), true);
        }

        private void SetFWConstantPlugIns(Int64 pluginsHeight)
        {
            System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

            //Si no se inyectaron Pluging le doy un pixel al contenido
            if (pluginsHeight == 0)
                pluginsHeight++;

            //HEIGHT
            _sbBuffer.Append("var _PLUGIN_HEIGHT = " + pluginsHeight.ToString() + ";                                                        \n");

            _JavaScriptManager.InjectJavascript("JS_FW_CONSTANTS_PLUGINS", _sbBuffer.ToString(), true);
        }
        /// <summary>
        /// Setea los Height Dinamicos del Marco de Master (FW Cliente)
        /// </summary>
        //Observacion:
        //Hay que tener cuidado con el Resize, ya que hay browsers que lo disparan en otro evento causando un Loop infinito 
        //Traquear los Bounds y si son iguales, matar el evento.
        private void SetFwBounds()
        {
            System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

            _sbBuffer.Append("function SetFwBounds() {                                                                                  \n");
            _sbBuffer.Append("   var content = document.getElementById('divContent');                                                   \n");
            _sbBuffer.Append("   var _contentHeight = document.body.clientHeight;                                                       \n");
            _sbBuffer.Append("   var _contentWidth = document.body.clientWidth;                                                         \n");
            _sbBuffer.Append("                                                                                                          \n");
            _sbBuffer.Append("   //Width                                                                                                \n");
            _sbBuffer.Append("   _contentWidth -= _GLOBAL_TOOLBAR;                                                                      \n");
            _sbBuffer.Append("   _contentWidth -= _SEPARATORS;                                                                          \n");
            _sbBuffer.Append("   _contentWidth -= _CONTENT_TOOLBAR;                                                                     \n");
            _sbBuffer.Append("   _contentWidth -= _OFFSET_IEXPLORER_WIDTH;                                                              \n");
            _sbBuffer.Append("                                                                                                          \n");
            _sbBuffer.Append("   if(_contentWidth > 0)                                                                                  \n");
            _sbBuffer.Append("      content.style.width = _contentWidth.toString() + 'px';                                              \n");
            _sbBuffer.Append("   else                                                                                                   \n");
            _sbBuffer.Append("      content.style.width = '0px';                                                                        \n");
            _sbBuffer.Append("                                                                                                          \n");
            _sbBuffer.Append("   //Height                                                                                               \n");
            _sbBuffer.Append("   _contentHeight -= _HEADER;                                                                             \n");
            _sbBuffer.Append("   _contentHeight -= _BREAD_CRUMB;                                                                        \n");
            _sbBuffer.Append("   _contentHeight -= _PAGE_TITLE;                                                                         \n");
            _sbBuffer.Append("   _contentHeight -= _STATUS_BAR;                                                                         \n");
            _sbBuffer.Append("   _contentHeight -= _FOOTER;                                                                             \n");
            _sbBuffer.Append("   _contentHeight -= _OFFSET_IEXPLORER_HEIGHT;                                                            \n");
            _sbBuffer.Append("                                                                                                          \n");
            _sbBuffer.Append("   if(_contentHeight > 0)                                                                                 \n");
            _sbBuffer.Append("      content.style.height = _contentHeight.toString() + 'px';                                            \n");
            _sbBuffer.Append("   else                                                                                                   \n");
            _sbBuffer.Append("      content.style.height = '0px';                                                                       \n");
            _sbBuffer.Append("                                                                                                          \n");
            _sbBuffer.Append("   SetNavigatorHeights();                                                                                 \n");
            _sbBuffer.Append("                                                                                                          \n");
            _sbBuffer.Append("   //Si esta Dockeado añdade/quita - height/width al Content y a los Paneles                              \n");
            _sbBuffer.Append("   if(MenuIsDocked())                                                                                     \n");
            _sbBuffer.Append("       DockMenu();                                                                                        \n");
            _sbBuffer.Append("}                                                                                                         \n");

            _JavaScriptManager.InjectJavascript("JS_FW_BOUNDS", _sbBuffer.ToString(), true);
        }

        private void SetFWDockingFunctions()
        {
            System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

            //String _dockingPinPath = Common.Functions.GetAbsolutePathAppThemes + "/Images/Icons/GlobalNavigator/";

            _sbBuffer.Append("function DockGlobalMenuContent(sender, e) {                                                                          \n");
            //El Menu Esta Doqueado, lo voy a 'DesDoquear'
            _sbBuffer.Append("  if (MenuIsDocked()) {                                                                                           \n");
            _sbBuffer.Append("      UnDockMenu()                                                                                                \n");
            //Cambia la imagen del DockingPin            
            _sbBuffer.Append("      sender.className = 'globalNavigatorButtonUnDocked'                 \n");
            //_sbBuffer.Append("      sender.setAttribute('src','" + _dockingPinPath + DockingState.UnDocked.ToString() + ".png')                 \n");
            _sbBuffer.Append("      $get('" + hdn_globalNavigatorMenuDockingState.ClientID + "').value='" + DockingState.UnDocked.ToString() + "';  \n");
            _sbBuffer.Append("  } else {                                                                                                        \n");
            //El Menu Esta UnDocked, lo voy a 'Doquear'
            _sbBuffer.Append("      DockMenu();                                                                                                 \n");
            //Cambia la imagen del DockingPin    
            _sbBuffer.Append("      sender.className = 'globalNavigatorButtonDocked'                 \n");
            //_sbBuffer.Append("      sender.setAttribute('src','" + _dockingPinPath + DockingState.Docked.ToString() + ".png')                   \n");
            _sbBuffer.Append("      $get('" + hdn_globalNavigatorMenuDockingState.ClientID + "').value='" + DockingState.Docked.ToString() + "';    \n");
            _sbBuffer.Append("  }                                                                                                               \n");
            _sbBuffer.Append("                                                                                                                  \n");
            _sbBuffer.Append("   StopEvent(e);     //window.event.returnValue = false;                                                                              \n");
            _sbBuffer.Append("}                                                                                                                 \n");

            //Funcion para Averiguar el Estado "Docking" del Menu
            _sbBuffer.Append("function MenuIsDocked()                                                                                            \n");
            _sbBuffer.Append("{                                                                                                                  \n");
            _sbBuffer.Append("    if($get('" + hdn_globalNavigatorMenuDockingState.ClientID + "').value == '" + DockingState.Docked.ToString() + "') \n");
            _sbBuffer.Append("    {                                                                                                              \n");
            _sbBuffer.Append("        return true;                                                                                               \n");
            _sbBuffer.Append("    } else {                                                                                                       \n");
            _sbBuffer.Append("        return false;                                                                                              \n");
            _sbBuffer.Append("    }                                                                                                              \n");
            _sbBuffer.Append("}                                                                                                                  \n");

            _sbBuffer.Append("function DockMenu()                                                                                                                   \n");
            _sbBuffer.Append("{                                                                                                                                     \n");
            _sbBuffer.Append("    //Objetos involucrados en el Docking                                                                                              \n");
            _sbBuffer.Append("    //El Td que simula el Docking                                                                                                     \n");
            _sbBuffer.Append("    var globalNavigatorDockingZone = document.getElementById('globalNavigatorDockingZone');                                           \n");
            _sbBuffer.Append("    var content = document.getElementById('divContent');                                                                              \n");
            _sbBuffer.Append("    var globalNavigatorMenuPanel = document.getElementById('" + _GlobalNavigator.ClientID + "');                                      \n");
            _sbBuffer.Append("                                                                                                                                      \n");
            _sbBuffer.Append("    //Medidas de los Objetos                                                                                                          \n");
            _sbBuffer.Append("    var contentBounds = Sys.UI.DomElement.getBounds(content);                                                                         \n");
            _sbBuffer.Append("    var menuPanelBounds = Sys.UI.DomElement.getBounds(globalNavigatorMenuPanel);                                                      \n");
            _sbBuffer.Append("                                                                                                                                      \n");
            _sbBuffer.Append("    var contentWidth = contentBounds.width - _MENU_DOCKING_WIDTH - _MENU_DOCKING_WIDTH_IEOFFSET + _MENU_DOCKING_WIDTH_UNDOCKED;       \n");
            _sbBuffer.Append("    if(contentWidth > 0)                                                                                                              \n");
            _sbBuffer.Append("      content.style.width = contentWidth + 'px';                                                                                      \n");
            _sbBuffer.Append("    else                                                                                                                              \n");
            _sbBuffer.Append("      content.style.width = '0px';                                                                                                    \n");
            _sbBuffer.Append("                                                                                                                                      \n");
            _sbBuffer.Append("    globalNavigatorDockingZone.style.width = (_MENU_DOCKING_WIDTH + _MENU_DOCKING_WIDTH_IEOFFSET) + 'px';                             \n");
            _sbBuffer.Append("    //globalNavigatorMenuPanel.style.height = (menuPanelBounds.height + _MENU_DOCKING_HEIGHT) + 'px';                                   \n");
            _sbBuffer.Append("}                                                                                                                                     \n");

            _sbBuffer.Append("function UnDockMenu()                                                                                                                 \n");
            _sbBuffer.Append("{                                                                                                                                     \n");
            _sbBuffer.Append("    //Objetos involucrados en el Docking                                                                                              \n");
            _sbBuffer.Append("    var globalNavigatorDockingZone = document.getElementById('globalNavigatorDockingZone');                                           \n");
            _sbBuffer.Append("    var content = document.getElementById('divContent');                                                                              \n");
            _sbBuffer.Append("    var globalNavigatorMenuPanel = document.getElementById('" + _GlobalNavigator.ClientID + "');                                      \n");
            _sbBuffer.Append("                                                                                                                                      \n");
            _sbBuffer.Append("    //Medidas de los Objetos                                                                                                          \n");
            _sbBuffer.Append("    var contentBounds = Sys.UI.DomElement.getBounds(content);                                                                         \n");
            _sbBuffer.Append("    var menuPanelBounds = Sys.UI.DomElement.getBounds(globalNavigatorMenuPanel);                                                      \n");
            _sbBuffer.Append("                                                                                                                                      \n");
            _sbBuffer.Append("    content.style.width = (contentBounds.width+_MENU_DOCKING_WIDTH+_MENU_DOCKING_WIDTH_IEOFFSET-_MENU_DOCKING_WIDTH_UNDOCKED) + 'px'; \n");
            //El Width del Td que simula el Doqueo al estar UnDocked 
            //(estos valores son fijos y por estilo ya que el Bounds no levanta dimensiones del TD)
            _sbBuffer.Append("    globalNavigatorDockingZone.style.width = _MENU_DOCKING_WIDTH_UNDOCKED + 'px';                                                     \n");
            //El Alto del MenuPanel UnDocked (verifica que no de un numero negativo al restar)
            //_sbBuffer.Append("    var globalNavigatorMenuPanelHeight = menuPanelBounds.height - _MENU_DOCKING_HEIGHT;                                               \n");
            //_sbBuffer.Append("    if(globalNavigatorMenuPanelHeight > 0)                                                                                            \n");
            //_sbBuffer.Append("      globalNavigatorMenuPanel.style.height = (menuPanelBounds.height - _MENU_DOCKING_HEIGHT) + 'px';                                 \n");
            //_sbBuffer.Append("    else                                                                                                                              \n");
            //_sbBuffer.Append("      globalNavigatorMenuPanel.style.height = '0px';                                                                                  \n");
            _sbBuffer.Append("}                                                                                                                                     \n");


            _JavaScriptManager.InjectJavascript("JS_FW_DOCKING", _sbBuffer.ToString(), true);
        }

        /// <summary>
        /// Esta funcion se Dispara en el Load de la Pagina y setea el Estado del GlobalNavigatorMenu del Redirect Anterior
        /// persistiendo su estado entre Redirects.
        /// </summary>
        private void SetGlobalNavigatorMenuState()
        {
            System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

            String _globalMenuVisibility = (GlobalNavigatorMenuDockingState == DockingState.Docked) ? "block" : "none";

            _sbBuffer.Append("function SetGlobalMenuNavigatorVisibilityState() {                                                            \n");
            _sbBuffer.Append("   var _globalNavigator = document.getElementById('" + _GlobalNavigator.ClientID + "');                       \n");
            _sbBuffer.Append("   _globalNavigator.style.display = '" + _globalMenuVisibility + "';                                          \n");
            _sbBuffer.Append("}                                                                                                             \n");

            _JavaScriptManager.InjectJavascript("JS_FW_GlobalMenuNavigatorVisibilityState", _sbBuffer.ToString(), true);
        }

        /// <summary>
        /// Los Handlers del 'Ciclo de Vida' de -Ajax-
        /// </summary>
        private void InjectAjaxHandlers()
        {
            System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

            _sbBuffer.Append("var prm;                                                                                                              \n");
            _sbBuffer.Append("var postBackElement;                                                                                                  \n");
            _sbBuffer.Append("                                                                                                                      \n");
            _sbBuffer.Append("function InitializeMasterFwAjaxHandlers()                                                                             \n");
            _sbBuffer.Append("{                                                                                                                     \n");
            _sbBuffer.Append("    prm = Sys.WebForms.PageRequestManager.getInstance();                                                              \n");
            _sbBuffer.Append("    prm.add_initializeRequest(MasterFwInitializeRequest);                                                             \n");
            _sbBuffer.Append("    prm.add_endRequest(MasterFwEndRequest);                                                                           \n");
            _sbBuffer.Append("}                                                                                                                     \n");

            _sbBuffer.Append("function MasterFwInitializeRequest(sender, args)                                                                      \n");
            _sbBuffer.Append("{                                                                                                                     \n");
            _sbBuffer.Append("    postBackElement = args.get_postBackElement();                                                                     \n");
            _sbBuffer.Append("    PersistMenuPanelsDisplayState('init');                                                                            \n");
            _sbBuffer.Append("}                                                                                                                     \n");

            _sbBuffer.Append("function MasterFwEndRequest(sender, args) {                                                                           \n");
            _sbBuffer.Append("    PersistMenuPanelsDisplayState('end');                                                                             \n");
            _sbBuffer.Append("    SetFwBounds();                                                                                                    \n");
            _sbBuffer.Append("}                                                                                                                     \n");

            //En el futuro, Los MenuPanels van a ser dinamico, por lo tanto esta funcion tambien
            _sbBuffer.Append("var _globalMenuPanelState;                                                                                            \n");
            _sbBuffer.Append("var _contentMenuPanelState;                                                                                           \n");
            //CustomMenuPanels
            foreach (String _customPanelName in _ContentNavigatorCustomMenuPanels.Keys)
                _sbBuffer.Append("var _contentMenuPanel" + _customPanelName + "State;                                                                   \n");

            _sbBuffer.Append("function PersistMenuPanelsDisplayState(requestState) {                                                                \n");
            _sbBuffer.Append("    if(requestState == 'init') {                                                                                      \n");
            _sbBuffer.Append("        _globalMenuPanelState = $get('" + _GlobalNavigator.ClientID + "').style.display;                              \n");
            _sbBuffer.Append("        _contentMenuPanelState = $get('" + _ContentNavigator.ClientID + "').style.display;                            \n");
            //CustomMenuPanels
            foreach (var _customPanel in _ContentNavigatorCustomMenuPanels)
                _sbBuffer.Append("        _contentMenuPanel" + _customPanel.Key + "State = $get('" + _customPanel.Value.ClientID + "').style.display;   \n");
            _sbBuffer.Append("    }                                                                                                                 \n");
            _sbBuffer.Append("    if(requestState == 'end') {                                                                                       \n");
            _sbBuffer.Append("        $get('" + _GlobalNavigator.ClientID + "').style.display = _globalMenuPanelState;                              \n");
            _sbBuffer.Append("        $get('" + _ContentNavigator.ClientID + "').style.display = _contentMenuPanelState;                            \n");
            foreach (var _customPanel in _ContentNavigatorCustomMenuPanels)
                _sbBuffer.Append("        $get('" + _customPanel.Value.ClientID + "').style.display = _contentMenuPanel" + _customPanel.Key + "State;   \n");
            _sbBuffer.Append("    }                                                                                                                 \n");
            _sbBuffer.Append("}                                                                                                                     \n");

            _JavaScriptManager.InjectJavascript("JS_FW_AJAX_HANDLERS", _sbBuffer.ToString(), true);
        }

        //private void InjectAjaxHandlers()
        //{
        //    System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

        //    _sbBuffer.Append("var prm;                                                                                              \n");
        //    _sbBuffer.Append("var postBackElement;                                                                                  \n");
        //    _sbBuffer.Append("                                                                                                      \n");
        //    _sbBuffer.Append("function InitializeMasterFwAjaxHandlers()                                                             \n");
        //    _sbBuffer.Append("{                                                                                                     \n");
        //    _sbBuffer.Append("    prm = Sys.WebForms.PageRequestManager.getInstance();                                              \n");
        //    _sbBuffer.Append("    prm.add_initializeRequest(MasterFwInitializeRequest);                                             \n");
        //    _sbBuffer.Append("    prm.add_endRequest(MasterFwEndRequest);                                                           \n");
        //    _sbBuffer.Append("}                                                                                                     \n");

        //    _sbBuffer.Append("function MasterFwInitializeRequest(sender, args)                                                      \n");
        //    _sbBuffer.Append("{                                                                                                     \n");
        //    _sbBuffer.Append("    postBackElement = args.get_postBackElement();                                                     \n");
        //    _sbBuffer.Append("    PersistMenuPanelsDisplayState('init');                                                            \n");
        //    _sbBuffer.Append("}                                                                                                     \n");

        //    _sbBuffer.Append("function MasterFwEndRequest(sender, args) {                                                           \n");
        //    _sbBuffer.Append("    PersistMenuPanelsDisplayState('end');                                                             \n");
        //    _sbBuffer.Append("    SetFwBounds();                                                                                    \n");
        //    _sbBuffer.Append("}                                                                                                     \n");

        //    //En el futuro, Los MenuPanels van a ser dinamico, por lo tanto esta funcion tambien
        //    _sbBuffer.Append("var _globalMenuPanelState;                                                                            \n");

        //    _sbBuffer.Append("function PersistMenuPanelsDisplayState(requestState) {                                                \n");

        //    //Con esto puedo recorrer todos los Key y voy armando el JS con los distintos paneles...
        //    System.Text.StringBuilder _sbBuffDeclareVar = new System.Text.StringBuilder();
        //    System.Text.StringBuilder _sbBuffAssignVar = new System.Text.StringBuilder();
        //    System.Text.StringBuilder _sbBuffSetDisplay = new System.Text.StringBuilder();
        //    foreach (String _keyItem in KeysContentNavigator)
        //    {
        //        //Todas las declaraciones de variables para el menuPanel....
        //        _sbBuffDeclareVar.Append("var _contentMenuPanelState" + _keyItem + ";                                                                       \n");
        //        //Todas las asiginaciones
        //        _sbBuffAssignVar.Append("_contentMenuPanelState" + _keyItem + " = $get('" + _ContentNavigators[_keyItem].ClientID + "').style.display;            \n");
        //        //Todos los seteo de display...
        //        _sbBuffSetDisplay.Append("$get('" + _ContentNavigators[_keyItem].ClientID + "').style.display = _contentMenuPanelState" + _keyItem + ";            \n");
        //    }
        //    //_sbBuffer.Append("    var _contentMenuPanelState;                                                                       \n");
        //    _sbBuffer.Append(_sbBuffDeclareVar);
        //    _sbBuffer.Append("    if(requestState == 'init') {                                                                      \n");
        //    _sbBuffer.Append("        _globalMenuPanelState = $get('" + _GlobalNavigator.ClientID + "').style.display;              \n");
        //    //_sbBuffer.Append("        _contentMenuPanelState" + _keyItem + " = $get('" + _ContentNavigators[_keyItem].ClientID + "').style.display;            \n");
        //    _sbBuffer.Append(_sbBuffAssignVar);
        //    _sbBuffer.Append("    }                                                                                                 \n");
        //    _sbBuffer.Append("    if(requestState == 'end') {                                                                       \n");
        //    _sbBuffer.Append("        $get('" + _GlobalNavigator.ClientID + "').style.display = _globalMenuPanelState;              \n");
        //    //_sbBuffer.Append("        $get('" + _ContentNavigators[_keyItem].ClientID + "').style.display = _contentMenuPanelState" + _keyItem + ";            \n");
        //    _sbBuffer.Append(_sbBuffSetDisplay);
        //    _sbBuffer.Append("    }                                                                                                 \n");
        //    _sbBuffer.Append("}                                                                                                     \n");

        //    _JavaScriptManager.InjectJavascript("JS_FW_AJAX_HANDLERS", _sbBuffer.ToString(), true);
        //}

        /// <summary>
        /// Handler de los eventos Click de los Toolbars
        /// </summary>
        private void HandleMenuAction()
        {
            System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

            _sbBuffer.Append("function HandleMenuAction(menuButton, idMenuPanel, e) {                                                           \n");
            _sbBuffer.Append("    //Si el GlobalNavigatorMenu esta Dockeado, no hace nada.                                                  \n");
            _sbBuffer.Append("    if(idMenuPanel == '" + _GlobalNavigator.ClientID + "' && MenuIsDocked()) {                                \n");
            _sbBuffer.Append("        StopEvent(e);     //window.event.returnValue = false; return;                                                             \n");
            _sbBuffer.Append("        return;                                                             \n");
            _sbBuffer.Append("    }                                                                                                         \n");
            _sbBuffer.Append("                                                                                                              \n");
            _sbBuffer.Append("    var _menuPanel = document.getElementById(idMenuPanel);                                                    \n");
            _sbBuffer.Append("                                                                                                              \n");
            _sbBuffer.Append("    if (_menuPanel.style.display != 'block') {                                                                \n");
            _sbBuffer.Append("        SelectedLinkButton(menuButton);                                                                       \n");
            _sbBuffer.Append("        CloseAllPanels(_menuPanel, idMenuPanel);                                                              \n");
            _sbBuffer.Append("        _menuPanel.style.display = 'block';                                                                   \n");
            _sbBuffer.Append("        SetMenuPanelLeftPosition(_menuPanel);                                                                 \n");
            _sbBuffer.Append("    } else {                                                                                                  \n");
            _sbBuffer.Append("        UnselectedLinkButton(menuButton, '');                                                                 \n");
            _sbBuffer.Append("        _menuPanel.style.display = 'none';                                                                    \n");
            _sbBuffer.Append("    }                                                                                                         \n");
            _sbBuffer.Append("                                                                                                              \n");
            _sbBuffer.Append("    StopEvent(e);     //window.event.returnValue = false;                                                     \n");
            _sbBuffer.Append("}                                                                                                             \n");
            _sbBuffer.Append("function CloseAllPanels(menuButton, idMenuPanel) {                                                            \n");
            _sbBuffer.Append("    //Si el GlobalNavigatorMenu esta Dockeado, no hace nada.                                                  \n");
            _sbBuffer.Append("    if(idMenuPanel == '" + _GlobalNavigator.ClientID + "') {                                                  \n");
            _sbBuffer.Append("        return;                                                                                               \n");
            _sbBuffer.Append("    }                                                                                                         \n");
            _sbBuffer.Append("                                                                                                              \n");
            //Los Fijos del FW
            _sbBuffer.Append("    $get('" + _ContentNavigator.ClientID + "').style.display = 'none';                                            \n");
            foreach (MasterControls.ToolbarMenuPanel _customPanel in _ContentNavigatorCustomMenuPanels.Values)
                _sbBuffer.Append("    $get('" + _customPanel.ClientID + "').style.display = 'none';                                             \n");
            _sbBuffer.Append("}                                                                                                             \n");
            

            _JavaScriptManager.InjectJavascript("JS_FW_HANDLE_MENU_ACTION", _sbBuffer.ToString(), true);
        }
        private void InjectShowUpdateProgressMasterGlobal()
        {
            System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

            _sbBuffer.Append("function ShowMasterGlobalUpdateProgress()                                                                 \n");
            _sbBuffer.Append("{                                                                                                         \n");
            //Muestra el reloj al terminar de cargar el iFrame.
            _sbBuffer.Append("      document.getElementById('FWMasterGlobalUpdateProgress').style.display = 'block';                    \n");
            _sbBuffer.Append("      //Ejecuta la validacion de los Validator en la pagina                                               \n");
            _sbBuffer.Append("      if (!CheckClientValidatorPage())                                                                    \n");
            _sbBuffer.Append("      {                                                                                                   \n");
            _sbBuffer.Append("          //Si hay validaciones, entonces cancelo el evento                                               \n");
            _sbBuffer.Append("          document.getElementById('FWMasterGlobalUpdateProgress').style.display = 'none';                 \n");
            _sbBuffer.Append("          return false;                                                                                   \n");
            _sbBuffer.Append("      }                                                                                                   \n");
            _sbBuffer.Append("      //En caso de que no hay validator activos, retorna true para que se ejecute el PostBack             \n");
            _sbBuffer.Append("      return true;                                                                                        \n");
            _sbBuffer.Append("}                                                                                                         \n");

            _JavaScriptManager.InjectJavascript("JS_ShowMasterGlobalUpdateProgress", _sbBuffer.ToString(), true);
        }

        /// <summary>
        /// Registra el boton de la Toolbar para su interaccion con los Menues
        /// </summary>
        /// <param name="menuPanelId">El menu al cual registrarse</param>
        private String RegisterToolbarButton(Panel menuPanel)
        {
            if (menuPanel == null)
                throw new Exception("El panel al cual intenta attachear el Boton no fue instanciado.");

            return "HandleMenuAction(this,'" + menuPanel.ClientID + "', event);";
        }

        #endregion
    }
}
