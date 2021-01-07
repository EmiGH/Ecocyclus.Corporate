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

using EBPA = Condesus.EMS.Business.PA.Entities;
using Condesus.EMS.WebUI.MasterControls;
using Condesus.WebUI.Navigation;


namespace Condesus.EMS.WebUI
{
    public partial class BasePage : System.Web.UI.Page
    {
        #region Internal Properties
            private Dictionary<String, Object> _ParamFactory = new Dictionary<String, Object>();    
            //private String _SelectedModuleGRC
            //{
            //    get
            //    {
            //        object _o = Session["SelectedModuleGRC"];
            //        if (_o != null)
            //            return (String)_o;

            //        return "DS";
            //    }

            //    set { Session["SelectedModuleGRC"] = value.ToString(); }
            //}
        #endregion

        #region Factory Calls Methods
            /// <summary>
            /// Este metodo permite ejecutar cualquier metodo para construir el menu, en base al nombre del metodo.
            /// </summary>
            /// <param name="methodName">Nombre del metodo publico que ira a buscar por Reflection</param>
            /// <param name="param">Parametros que necesite el metodo</param>
            protected RadTreeView BuildGenericMenu(String methodName, Dictionary<String, Object> param)
            {
                return (RadTreeView)new Condesus.EMS.WebUI.Business.Menu(methodName).Execute(param);
            }
            /// <summary>
            /// Este metodo factory para la creacion del Menu ContextInfo dependiendo de la entidad que se le indica.
            /// </summary>
            /// <param name="entityNameGRC">Nombre de la entidad para la cual hay que crear el entityNameContextInfo</param>
            /// <param name="param">Parametros que necesite el metodo</param>
            protected Boolean BuildContextInfoModuleMenu(String entityNameContextInfo, Dictionary<String, Object> param)
            {
                _ParamFactory = param;
                
                //Carga el titulo del Panel.
                Label _lblTitleContextInformationMenu = new Label();
                _lblTitleContextInformationMenu.Text = Resources.Common.ContextInformationTitlePanel;
                FwMasterPage.ContentNavigatorCustomPanelsHeaderToolbarContentAdd(Common.Constants.ContextInformationKey, _lblTitleContextInformationMenu);

                //Carga el Tree con la opciones contextuales.
                String _methodName = "BuildContextInfoMenu" + entityNameContextInfo;
                return BuildContextInfoTreeContent(_methodName);
                
            }
            /// <summary>
            /// Este metodo factory para la creacion del Menu Context ElementMaps dependiendo de la entidad que se le indica.
            /// </summary>
            /// <param name="entityNameContextElementMaps">Nombre de la entidad para la cual hay que crear el ContextElementMaps</param>
            /// <param name="param">Parametros que necesite el metodo</param>
            protected Boolean BuildContextElementMapsModuleMenu(String entityNameContextElementMaps, Dictionary<String, Object> param)
            {
                _ParamFactory = param;

                //Carga el titulo del Panel.
                Label _lblTitleContextElementMapsMenu = new Label();
                _lblTitleContextElementMapsMenu.Text = Resources.Common.ContextElementMapsTitlePanel;
                FwMasterPage.ContentNavigatorCustomPanelsHeaderToolbarContentAdd(Common.Constants.ContextElementMapsKey, _lblTitleContextElementMapsMenu);

                //Carga el Tree con la opciones contextuales.
                String _methodName = "BuildContextElementMapsMenu" + entityNameContextElementMaps;

                return BuildContextElementMapsTreeContent(_methodName);
            }
        #endregion

        #region Modules Context Information
            /// <summary>
            /// Este metodo construye el Menu contextual de accesos directo para agregarselo a los TReeView.
            /// </summary>
            /// <returns>Un<c>RadTreeViewContextMenu</c></returns>
            private RadTreeViewContextMenu BuildContextMenuContextInfoShortCut()
            {
                RadTreeViewContextMenu _rtvContextMenu = new RadTreeViewContextMenu();
                _rtvContextMenu.ID = "rtvContextMenuContextInfo";
                _rtvContextMenu.EnableEmbeddedSkins = false;
                _rtvContextMenu.Skin = "EMS";

                //Crea los items del menu contextual
                RadMenuItem _rmItemAdd = new RadMenuItem(Resources.Common.mnuAdd);
                _rmItemAdd.Value = "rmiAdd";
                //Agrega los items root del menu
                _rtvContextMenu.Items.Add(_rmItemAdd);

                return _rtvContextMenu;
            }
            /// <summary>
            /// Este metodo se encarga de construir el Tree del Content para el GRC, en base al methodName que se pasa por parametros.
            /// Termina haciendo la llamada al factory e inyecta el tree en el contentNavigatorToolbarMenuContent.
            /// </summary>
            /// <param name="methodName">Nombre del metodo para que contruya un tree para un modulo y entidad de GRC</param>
            private Boolean BuildContextInfoTreeContent(String methodName)
            {
                //Limpia todo para que vuelva a cargar...
                FwMasterPage.ContentNavigatorToolbarMenuContentClear();

                //Con el Factory hace la llamada para que se contruya el Tree en base a la seleccion del modulo
                //y ademas para que entidad se debe armar el GRC.
                RadTreeView _rtvContextInfoTree = (RadTreeView)new Condesus.EMS.WebUI.Business.MenuGRC(methodName).Execute(_ParamFactory);
                if (_rtvContextInfoTree.Nodes.Count > 0)
                {
                    _rtvContextInfoTree.OnClientContextMenuShowing = "onClientContextMenuContextInfoShowing";
                    //Agrega el ContextMenu al treeview.
                    _rtvContextInfoTree.ContextMenus.Add(BuildContextMenuContextInfoShortCut());
                    InjectOnClientContextMenuContextInfoShowing();

                    _rtvContextInfoTree.NodeClick += new RadTreeViewEventHandler(_rtvContextInfoTree_NodeClick);
                    _rtvContextInfoTree.ContextMenuItemClick += new RadTreeViewContextMenuEventHandler(rtvMenuElementMaps_ContextMenuItemClick);

                    //FwMasterPage.ContentNavigatorToolbarMenuContentAdd(_rtvContextInfoTree);
                    FwMasterPage.ContentNavigatorCustomPanelsMenuContentAdd(Common.Constants.ContextInformationKey, _rtvContextInfoTree);
                    //HAy algo en el tree, entonces devuelve true.
                    return true;
                }
                return false;
            }

            #region Events Context Information
                /// <summary>
                /// Es el evento que se dispara al hacer click sobre un nodo del arbol de GRC, esta hecho en forma generica, para
                /// que el Tree de cualquier GRC pueda hacer la navegacion de todas maneras. (por ahora siempre se navega al Manage)
                /// </summary>
                /// <param name="sender"></param>
                /// <param name="e"></param>
                void _rtvContextInfoTree_NodeClick(object sender, RadTreeNodeEventArgs e)
                {
                    String _pageTitle = ((RadTreeView)sender).Attributes["PageTitle"];

                    //Primero saca las PK Entity que vienen como atributos generales
                    String _pkValues = _pkValues = "PageTitle=" + _pageTitle + "&";
                    _pkValues += GetPKEntityFromAtributes(((RadTreeView)sender).Attributes);
                    //Segundo saca las PK Entity que vienen como atributos en cada nodo.
                    _pkValues += GetPKEntityFromAtributes(e.Node.Attributes);
                    //Le saco el ultimo &.
                    if (!String.IsNullOrEmpty(_pkValues))
                    { _pkValues = _pkValues.Substring(0, _pkValues.Length - 1); }
                    
                    NavigatorClearTransferVars();

                    String _entityName = e.Node.Attributes["EntityName"];
                    String _entityNameGrid = e.Node.Attributes["EntityNameGrid"] == null ? String.Empty : e.Node.Attributes["EntityNameGrid"];
                    String _entityNameHierarchical = e.Node.Attributes["EntityNameHierarchical"] == null ? String.Empty : e.Node.Attributes["EntityNameHierarchical"];
                    //Por ahora estos atributos son fijos y por eso se deben usar siempre!.
                    NavigatorAddTransferVar("EntityNameGrid", _entityNameGrid);
                    NavigatorAddTransferVar("EntityName", _entityName);
                    //NavigatorAddTransferVar("EntityNameContextInfo", ((RadTreeView)sender).Attributes["EntityNameContextInfo"]);
                    NavigatorAddTransferVar("EntityNameContextInfo", e.Node.Attributes["EntityNameContextInfo"] == null ? String.Empty : e.Node.Attributes["EntityNameContextInfo"]);
                    NavigatorAddTransferVar("EntityNameContextElement", e.Node.Attributes["EntityNameContextElement"] == null ? String.Empty : e.Node.Attributes["EntityNameContextElement"]);

                    NavigatorAddTransferVar("EntityNameHierarchical", _entityNameHierarchical);
                    NavigatorAddTransferVar("EntityNameHierarchicalChildren", e.Node.Attributes["EntityNameHierarchicalChildren"] == null ? String.Empty : e.Node.Attributes["EntityNameHierarchicalChildren"]);

                    NavigatorAddTransferVar("EntityNameComboFilter", e.Node.Attributes["EntityNameComboFilter"] == null ? String.Empty : e.Node.Attributes["EntityNameComboFilter"]);
                    NavigatorAddTransferVar("IsFilterHierarchy", e.Node.Attributes["IsFilterHierarchy"] == null ? false : Convert.ToBoolean(e.Node.Attributes["IsFilterHierarchy"]));
                    NavigatorAddTransferVar("EntityNameChildrenComboFilter", e.Node.Attributes["EntityNameChildrenComboFilter"] == null ? String.Empty : e.Node.Attributes["EntityNameChildrenComboFilter"]);
                    NavigatorAddTransferVar("EntityNameMapClassification", e.Node.Attributes["EntityNameMapClassification"] == null ? String.Empty : e.Node.Attributes["EntityNameMapClassification"]);
                    NavigatorAddTransferVar("EntityNameMapClassificationChildren", e.Node.Attributes["EntityNameMapClassificationChildren"] == null ? String.Empty : e.Node.Attributes["EntityNameMapClassificationChildren"]);
                    NavigatorAddTransferVar("EntityNameMapElement", e.Node.Attributes["EntityNameMapElement"] == null ? String.Empty : e.Node.Attributes["EntityNameMapElement"]);
                    NavigatorAddTransferVar("EntityNameMapElementChildren", e.Node.Attributes["EntityNameMapElementChildren"] == null ? String.Empty : e.Node.Attributes["EntityNameMapElementChildren"]);


                    //NavigatorAddTransferVar("EntityNameToRemove", e.Node.Attributes["EntityNameToRemove"]);
                    NavigatorAddTransferVar("PageTitle", _pageTitle);

                    //Si hay PK entonces las agrego.
                    if (!String.IsNullOrEmpty(_pkValues))
                    { NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues); }
                    //Finalmente obtiene la URL y hace el navigate.
                    String _url = e.Node.Attributes["URL"];

                    var argsColl = new Dictionary<String, String>();
                    argsColl.Add("EntityName", _entityName);
                    argsColl.Add("EntityNameGrid", _entityNameGrid);
                    argsColl.Add("EntityNameHierarchical", _entityNameHierarchical);
                    argsColl.Add("EntityNameHierarchicalChildren", e.Node.Attributes["EntityNameHierarchicalChildren"]);
                    argsColl.Add("PkCompost", _pkValues);

                    NavigateMenuEventArgs _menuArgs = new NavigateMenuEventArgs(NavigateMenuType.ContextInfoMenu, argsColl);

                    //Busca el titulo sobre el plural (para el listado)
                    String _titleEntity = String.IsNullOrEmpty(_entityNameGrid) ? GetValueFromGlobalResource("CommonListManage", _entityNameHierarchical) : GetValueFromGlobalResource("CommonListManage", _entityNameGrid);
                    //Sino usa el de la entidad.
                    _titleEntity = String.IsNullOrEmpty(_titleEntity) ? String.Concat(" [", GetValueFromGlobalResource("CommonListManage", _entityName), "]") : String.Concat(" [", _titleEntity, "]");

                    //Si aun sigue siendo null, entonces tomo el texto del nodo y de su padre para mostrar...
                    _titleEntity = _titleEntity == " []" ? String.Concat(" [", e.Node.Text, "] [", e.Node.ParentNode.Text, "]") : _titleEntity;

                    String _titleDecorator = String.Concat(_pageTitle, _titleEntity);
                    Navigate(_url, _titleDecorator, _menuArgs);
                    //Navigate(_url, e.Node.Text, new NavigateMenuEventArgs());
                }
            #endregion
        #endregion
        
        #region Menu Context Element Maps
            /// <summary>
            /// Este metodo se encarga de construir el Tree del Content para el ContextElementMaps, en base al methodName que se pasa por parametros.
            /// Termina haciendo la llamada al factory e inyecta el tree en el contentNavigatorToolbarMenuContent.
            /// </summary>
            /// <param name="methodName">Nombre del metodo para que contruya un tree para un modulo y entidad de ContextElementMaps</param>
            private Boolean BuildContextElementMapsTreeContent(String methodName)
            {
                //Limpia todo para que vuelva a cargar...
                FwMasterPage.ContentNavigatorToolbarMenuContentClear();

                //Con el Factory hace la llamada para que se contruya el Tree en base a la seleccion del modulo
                //y ademas para que entidad se debe armar el GRC.
                RadTreeView _rtvContextElementMapsTree = new RadTreeView();
                try
                {
                    _rtvContextElementMapsTree = (RadTreeView)new Condesus.EMS.WebUI.Business.MenuGRC(methodName).Execute(_ParamFactory);
                    ////Agrega el ContextMenu al treeview.
                    //_rtvContextElementMapsTree.ContextMenus.Add(BuildContextMenuContextElementShortCut());
                    //Agrega los eventos.
                    _rtvContextElementMapsTree.NodeClick += new RadTreeViewEventHandler(rdtvContextElementMapsByProcess_NodeClick);

                    _rtvContextElementMapsTree.ContextMenuItemClick += new RadTreeViewContextMenuEventHandler(rtvMenuElementMaps_ContextMenuItemClick);
                    _rtvContextElementMapsTree.OnClientNodeClicked = "ClientClick";
                    //_rtvContextElementMapsTree.OnClientContextMenuShowing = "onClientContextMenuContextElementShowing";
                    InjectOnClientContextMenuContextElementShowing();
                    InjectOnClientContextMenuContextElementProcessShowing();
                }
                catch 
                {
                    //Deja el Tree vacio
                    return false;
                }
                InjectFunctionForPopUpProcessFramework(_rtvContextElementMapsTree.ClientID);

                //Inyecta el tree dentro del Menu Content.
                FwMasterPage.ContentNavigatorCustomPanelsMenuContentAdd(Common.Constants.ContextElementMapsKey, _rtvContextElementMapsTree);

                return true;
            }

            #region Events Context Element Maps
                protected void InjectFunctionForPopUpProcessFramework(String radTreeViewContext)
                {
                    System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

                    _sbBuffer.Append(OpenHtmlJavaScript());

                    //_sbBuffer.Append("//Global Vars & Handlers                                                                                          \n");
                    //_sbBuffer.Append("//Traquea la posicion del Mouse para mostrar el PopUp                                                             \n");
                    _sbBuffer.Append("var xMousePos = 0;                                                                                                \n");
                    _sbBuffer.Append("var yMousePos = 0;                                                                                                \n");
                    _sbBuffer.Append("document.onmousemove = SetMousePosition;                                                                          \n");

                    _sbBuffer.Append("  var _IEXPLORER = 'Microsoft Internet Explorer';                                             \n");
                    _sbBuffer.Append("  var _FIREFOX = 'Netscape';                                                                  \n");
                    _sbBuffer.Append("  var _BrowserName = navigator.appName;                                                       \n");

                    _sbBuffer.Append("  if (_BrowserName == _IEXPLORER)                                                             \n");
                    _sbBuffer.Append("  {   //IE and Opera                                                                          \n");
                    _sbBuffer.Append("      window.attachEvent('onload', InitializeDivInfoPopUp);                                \n");
                    _sbBuffer.Append("  }                                                                                           \n");
                    _sbBuffer.Append("  else                                                                                        \n");
                    _sbBuffer.Append("  {   //FireFox                                                                               \n");
                    _sbBuffer.Append("      document.addEventListener('DOMContentLoaded', InitializeDivInfoPopUp, false);        \n");
                    _sbBuffer.Append("  }                                                                                           \n");
                    //_sbBuffer.Append("window.attachEvent('onload', InitializeDivInfoPopUp);                                              \n");
                    //Initialize
                    _sbBuffer.Append("function InitializeDivInfoPopUp()                                                                  \n");
                    _sbBuffer.Append("{                                                                                                     \n");
                    _sbBuffer.Append("  var divNodeInfoPopUp = document.createElement('div');                                                     \n");
                    _sbBuffer.Append("  divNodeInfoPopUp.id = 'nodeInfoPopUp';                                                                      \n");
                    _sbBuffer.Append("  divNodeInfoPopUp.style.padding='10px';                                                                    \n");
                    _sbBuffer.Append("  divNodeInfoPopUp.style.position='absolute';                                                                    \n");
                    _sbBuffer.Append("  divNodeInfoPopUp.style.display='none';                                                                    \n");
                    _sbBuffer.Append("  divNodeInfoPopUp.style.backgroundColor='White';                                                                    \n");
                    _sbBuffer.Append("  divNodeInfoPopUp.style.border='solid 1px #307DB3';                                                                    \n");
                    _sbBuffer.Append("  divNodeInfoPopUp.style.zIndex='9999999';                                                                    \n");
                    _sbBuffer.Append("  document.body.appendChild(divNodeInfoPopUp);                                                              \n");
                    _sbBuffer.Append("}                                                                                                     \n");

                    _sbBuffer.Append("function ClientClick(node)                                                                                                    \n");
                    _sbBuffer.Append("{                                                                                                    \n");
                    _sbBuffer.Append("    //Esconde el PopUp si es que esta activo                                                                                                    \n");
                    _sbBuffer.Append("    HidePopUpProcces();                                                                                                    \n");
                    _sbBuffer.Append("}                                                                                                    \n");
                    
                    _sbBuffer.Append("function SetMousePosition(e)                                                                                                     \n");
                    _sbBuffer.Append("{                                                                                                    \n");
                    _sbBuffer.Append("    try {                                                                                                    \n");
                    _sbBuffer.Append("        xMousePos = GetMousePositionX(e);  //window.event.x;                                                                                                    \n");
                    _sbBuffer.Append("        yMousePos = GetMousePositionY(e);  //window.event.y;                                                                                                    \n");
                    _sbBuffer.Append("    }                                                                                                    \n");
                    _sbBuffer.Append("    catch(ex){}                                                                                                    \n");
                    _sbBuffer.Append("}                                                                                                    \n");

                    _sbBuffer.Append("function ShowPopUpProcces(img, value)                                                                 \n");
                    _sbBuffer.Append("{                                                                                                     \n");
                    _sbBuffer.Append("    var tree = $find('ctl00_rtvElementMapsProcessGroupProcesses');                                    \n");
                    _sbBuffer.Append("    var node = value._node;                                                                           \n");
                    _sbBuffer.Append("    var _entityName = node._attributes.getAttribute('EntityName');                                    \n");
                    _sbBuffer.Append("    if ((_entityName != 'FacilityType') && (_entityName != 'Facility') && (_entityName != 'Sector') && (_entityName != '') && (_entityName != 'MeasurementDeviceType') && (_entityName != 'MeasurementDevice') && (_entityName != 'Post') && (_entityName != 'AuditPlan'))                               \n");
                    _sbBuffer.Append("    {                                                                                                 \n");
                    _sbBuffer.Append("      var popup = document.getElementById('nodeInfoPopUp');                                           \n");
                    _sbBuffer.Append("      var text = GetPopupInfo(node, img);                                                             \n");
                    _sbBuffer.Append("      popup.innerHTML = text;                                                                         \n");
                    _sbBuffer.Append("      popup.style.display = 'block';                                                                  \n");
                    _sbBuffer.Append("      popup.style.left = String(xMousePos - popup.clientWidth) + 'px';                                \n");
                    _sbBuffer.Append("      popup.style.top = String(yMousePos + document.documentElement.scrollTop) + 'px';                \n");
                    _sbBuffer.Append("    }                                                                                                 \n");
                    _sbBuffer.Append("}                                                                                                     \n");

                    _sbBuffer.Append("function HidePopUpProcces(sender, args)                                                               \n");
                    _sbBuffer.Append("{                                                                                                     \n");
                    _sbBuffer.Append("    var popup = document.getElementById('nodeInfoPopUp');                                             \n");
                    _sbBuffer.Append("    popup.innerHTML = '';                                                                             \n");
                    _sbBuffer.Append("    popup.style.display = 'none';                                                                     \n");
                    _sbBuffer.Append("    popup.style.left = '0px';                                                                         \n");
                    _sbBuffer.Append("    popup.style.top = '0px';                                                                          \n");
                    _sbBuffer.Append("}                                                                                                     \n");
                    
                    _sbBuffer.Append(CloseHtmlJavaScript());

                    InjectJavascript("JS_ToolTipsProcessFramework", _sbBuffer.ToString());

                }
                /// <summary>
                /// Evento para el Click en un nodo...
                /// </summary>
                /// <param name="sender"></param>
                /// <param name="e"></param>
                void rdtvContextElementMapsByProcess_NodeClick(object sender, RadTreeNodeEventArgs e)
                {
                    //Por ahora estos atributos son fijos y por eso se deben usar siempre!.
                    String _entityName = e.Node.Attributes["EntityName"];
                    NavigatorAddTransferVar("EntityName", _entityName);
                    NavigatorAddTransferVar("EntityNameContextInfo", e.Node.Attributes["EntityNameContextInfo"]);
                    NavigatorAddTransferVar("EntityNameContextElement", e.Node.Attributes["EntityNameContextElement"]);
                    
                    //Si hay PK entonces las agrego.
                    NavigatorAddPkEntityIdTransferVar("PkCompost", e.Node.Attributes["PkCompost"]);

                    //Finalmente obtiene la URL y hace el navigate.
                    String _url = e.Node.Attributes["URL"];

                    var argsColl = new Dictionary<String, String>();
                    argsColl.Add("EntityName", _entityName);
                    argsColl.Add("EntityNameContextInfo", e.Node.Attributes["EntityNameContextInfo"]);
                    argsColl.Add("EntityNameContextElement", e.Node.Attributes["EntityNameContextElement"]);
                    argsColl.Add("PkCompost", e.Node.Attributes["PkCompost"]);

                    NavigateMenuEventArgs _menuArgs = new NavigateMenuEventArgs(NavigateMenuType.ContextInfoMenu, argsColl);
                    String _titleDecorator = String.Concat(e.Node.Text, " [", GetValueFromGlobalResource("CommonListManage", _entityName), "] [", Resources.Common.mnuView, "]");
                    Navigate(_url, _titleDecorator, _menuArgs);
                }
            #endregion
        #endregion

        /// <summary>
        /// Este metodo se encarga de recibir una coleccion de atributos y los recorre para armar los PK Entity que se necesita pasar
        /// como parametros al Navigate en el NavigatorAddPkEntityIdTransferVar. (devuelve el String armado con key=valor&...)
        /// </summary>
        /// <param name="atributeCollection"></param>
        /// <returns>Un <c>String</c></returns>
        private String GetPKEntityFromAtributes(AttributeCollection atributeCollection)
        {
            //Buffer donde guarda el PK a pasar
            String _buff = String.Empty;
            //guarda los atributos
            IEnumerator _keys = atributeCollection.Keys.GetEnumerator();
            while (_keys.MoveNext())
            {
                String _key = (String)_keys.Current;
                //Verifica que sean Atributos de tipo PK
                if (_key.Contains("PK_"))
                {
                    _buff += _key.Replace("PK_", String.Empty) + "=" + atributeCollection[_key] + "&";
                }
            }
            return _buff;
        }
        protected void BuildPropertyGeneralOptionsMenu(String entity, RadMenuEventHandler menuEventHandler, Boolean checkItemSelectedToDelete)
        {
            //var _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
            //_itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, true));
            //_itemsMenu.Add("rmiEdit", new KeyValuePair<String, Boolean>(Resources.Common.mnuEdit, true));

            ////Se concatena el LG, para saber si debe inyectar la opcion lg o no.
            //if (HasLanguage(entity.Replace("_LG", String.Empty) + "_LG"))
            //{
            //    _itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, true));
            //}
            //_itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, true));


            //RadMenu _rmnuGeneralOption = BuildGeneralOptionMenu(checkItemSelectedToDelete, GetOptionMenuByEntity(entity + "_MenuOption", ManageEntityParams, true));
            RadMenu _rmnuGeneralOption = BuildGeneralOptionMenu(checkItemSelectedToDelete, GetOptionMenuByEntity(entity + "_MenuOption", ManageEntityParams, true));

            //RadMenu _rmnuGeneralOption = BuildGeneralOptionMenu(false, _itemsMenu);
            _rmnuGeneralOption.ItemClick += new RadMenuEventHandler(menuEventHandler);
        }

        /// <summary>
        /// Arma el Menu de General Options para la Entidad.
        /// Se agrega a la ContentToolbar con toda su funcionalidad definida del lado del cliente.
        /// </summary>
        /// <param name="checkItemSelectedToDelete">True: Borra entidades desde un checklist. False: Borra la entidad actual. </param>
        /// <param name="itemsMenu">Los MenuItems definidos por la pagina concreta [key=el value y la key del recurso|value.key=texto x default;value.value=enabled/disabled]</param>
        /// <returns>El Objeto RadMenu (para manejar los eventos desde la pagina concreta)</returns>
        //protected RadMenu BuildGeneralOptionMenu(Boolean checkItemSelectedToDelete, Dictionary<String, KeyValuePair<String, Boolean>> itemsMenu)
        protected RadMenu BuildGeneralOptionMenu(Boolean checkItemSelectedToDelete, Dictionary<String, KeyValuePair<String, Boolean>> itemsMenu)
        {
            //Contruye el boton que termina mostrando el menu de opciones generales
            ImageButton _ibtnToolbarButtonGeneralOptions = new ImageButton();
            _ibtnToolbarButtonGeneralOptions.ID = "btnGeneralOptions";
            _ibtnToolbarButtonGeneralOptions.ToolTip = GetGlobalResourceObject("MasterFW", "btnGeneralOptionsTooltip").ToString();
            _ibtnToolbarButtonGeneralOptions.ImageUrl = "~/Skins/Images/Trans.gif";     //Common.Functions.GetAbsolutePathAppThemes + "/Images/Icons/ContentToolBar/Action.png";
            _ibtnToolbarButtonGeneralOptions.CssClass = Common.ConstantsStyleClassName.cssNameGeneralOptionMenuUnSelected;

            //Contruye el Panel conde va a estar contenido el menu
            Panel _mnuGoContainer = new Panel();
            _mnuGoContainer.ID = "mnuGoContainer";

            //Contruye el Menu y le setea las caracteristicas
            //RadMenu _rmnuGeneralOption = new RadMenu();
            RadMenu _rmnuGeneralOption = new RadMenu();
            _rmnuGeneralOption.ID = "rmnuGeneralOption";
            _rmnuGeneralOption.Skin = "FrameworkAccionMenu";
            _rmnuGeneralOption.EnableEmbeddedSkins = false;
            _rmnuGeneralOption.CausesValidation = false;
            _rmnuGeneralOption.CollapseDelay = 0;
            _rmnuGeneralOption.ExpandDelay = 0;
            _rmnuGeneralOption.Flow = ItemFlow.Vertical;
            //Seteos de configuracion
            RadMenuItemGroupSettings settings = _rmnuGeneralOption.DefaultGroupSettings;
            settings.Flow = ItemFlow.Vertical;
            settings.ExpandDirection = ExpandDirection.Down;
            
            //Dentro del GeneralOption, tiene el ADD y REMOVE.
            //Ahora viene el dictionary con los items que se tienen que agregar y con su estado
            Boolean _showButton = false;
            foreach (var _item in itemsMenu)
            {
                object _resourceName = null;
                _resourceName = GetGlobalResourceObject("Common", _item.Key);

                String _textItem = (_resourceName != null) ? _resourceName.ToString() : _item.Value.Key;
                RadMenuItem _rmiItem = new RadMenuItem(_textItem);
                _rmiItem.Value = _item.Key;
                //Si al menos un item viene en Enabled=true, entonces se muestra el boton de opciones
                //if (_item.Value.Value)
                //    { _showButton = true; }
                //_rmiItem.Enabled = _item.Value.Value;
                //_rmnuGeneralOption.Items.Add(_rmiItem);

                //Arreglo Ruben 
                //Si no tiene permisos no carga el item en el menu!!
                if (_item.Value.Value)
                {
                    _showButton = true;
                    _rmiItem.Enabled = _item.Value.Value;
                    _rmnuGeneralOption.Items.Add(_rmiItem);
                }

            }
            //Si esto queda en True, quiere decir que al menos un item se muestra en el menu. entonces se agrega el boton
            //Caso contrario, no se agrega el boton de opciones generales a la pagina.
            if (_showButton)
            {
                //Setea el evento cliente para atrapar el click
                _rmnuGeneralOption.OnClientItemClicking = "rmnOption_OnClientItemClicking";

                //Inyecta el menu dentro del Panel
                _mnuGoContainer.Controls.Add(_rmnuGeneralOption);

                //inyecta el panel dentro de la Master
                FwMasterPage.AddCustomMenuPanel(_mnuGoContainer);

                //Inyecta codigo JavaScript para atrapar el click del Delete, y le pasa un parametro, para saber si tiene que validar o no algun check sobre una grilla
                InjectRmnOptionOnClientItemClicking(checkItemSelectedToDelete, String.Empty);

                //Inyecta codigo JavaScript para el Show del menu al MouseOver sobre el boton
                InjectContentToolbarBtnHandler(_mnuGoContainer.ClientID);

                _ibtnToolbarButtonGeneralOptions.OnClientClick = "ContentToolbarBtn" + _mnuGoContainer.ClientID + "_OnMouseOver(this, event)"; //"return false;";
                //_ibtnToolbarButtonGeneralOptions.Attributes["onmouseover"] = "ContentToolbarBtn" + _mnuGoContainer.ClientID + "_OnMouseOver(this)";
                //Define eventos cliente sobre el menu.
                _rmnuGeneralOption.OnClientMouseOut = "OnClientMouseOutHandler" + _mnuGoContainer.ClientID;
                _rmnuGeneralOption.OnClientMouseOver = "OnMenuMouseOver" + _mnuGoContainer.ClientID;


                //Original!!!
                //_ibtnToolbarButtonGeneralOptions.OnClientClick = "return false;";
                //_ibtnToolbarButtonGeneralOptions.Attributes["onmouseover"] = "ContentToolbarBtn" + _mnuGoContainer.ClientID + "_OnMouseOver(this)";
                ////Define eventos cliente sobre el menu.
                //_rmnuGeneralOption.OnClientMouseOut = "OnClientMouseOutHandler" + _mnuGoContainer.ClientID;
                //_rmnuGeneralOption.OnClientMouseOver = "OnMenuMouseOver" + _mnuGoContainer.ClientID;
                //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

                //Inyecta el Boton en la Master
                FwMasterPage.ContentNavigatorToolbarAdd(_ibtnToolbarButtonGeneralOptions, false);
            }
            //Finalmente retorna el Menu.
            return _rmnuGeneralOption;
        }
        /// <summary>
        /// Arma el Menu de Seguridad y se agrega a la ContentToolbar con toda su funcionalidad definida del lado del cliente.
        /// </summary>
        /// <param name="itemsMenu">Los MenuItems definidos por la pagina concreta [key=el value y la key del recurso|value.key=texto x default;value.value=enabled/disabled]</param>
        /// <returns>El Objeto RadMenu (para manejar los eventos desde la pagina concreta)</returns>
        //protected RadMenu BuildSecuritySystemMenu(Dictionary<String, KeyValuePair<String, Boolean>> itemsMenu)
        protected RadMenu BuildSecuritySystemMenu(Dictionary<String, KeyValuePair<String, Boolean>> itemsMenu)
        {
            //Contruye el boton que termina mostrando el menu de opciones generales
            ImageButton _ibtnToolbarButtonSecurity = new ImageButton();
            _ibtnToolbarButtonSecurity.ID = "btnSecurity";
            _ibtnToolbarButtonSecurity.ToolTip = GetGlobalResourceObject("MasterFW", "btnSecurityTooltip").ToString();
            //_ibtnToolbarButtonSecurity.ImageUrl = Common.Functions.GetAbsolutePathAppThemes + "/Images/Icons/ContentToolBar/Security.png";
            _ibtnToolbarButtonSecurity.ImageUrl = "~/Skins/Images/Trans.gif";     //Common.Functions.GetAbsolutePathAppThemes + "/Images/Icons/ContentToolBar/Action.png";
            _ibtnToolbarButtonSecurity.CssClass = Common.ConstantsStyleClassName.cssNameSecurityOptionMenuUnSelected;

            //Contruye el Panel conde va a estar contenido el menu
            Panel _mnuSSContainer = new Panel();
            _mnuSSContainer.ID = "mnuSSContainer";

            //Contruye el Menu y le setea las caracteristicas
            //RadMenu _rmnuSecuritySystem = new RadMenu();
            RadMenu _rmnuSecuritySystem = new RadMenu();
            _rmnuSecuritySystem.ID = "rmnuSecuritySystem";
            _rmnuSecuritySystem.Skin = "FrameworkAccionMenu";
            _rmnuSecuritySystem.EnableEmbeddedSkins = false;
            _rmnuSecuritySystem.CausesValidation = false;
            _rmnuSecuritySystem.CollapseDelay = 0;
            _rmnuSecuritySystem.ExpandDelay = 0;
            _rmnuSecuritySystem.Flow = ItemFlow.Vertical;

            //Seteos de configuracion
            RadMenuItemGroupSettings settings = _rmnuSecuritySystem.DefaultGroupSettings;
            settings.Flow = ItemFlow.Vertical;
            settings.ExpandDirection = ExpandDirection.Down;

            //Dentro del GeneralOption, tiene el acceso a seguridad por Post o JobTitle.
            //Ahora viene el dictionary con los items que se tienen que agregar y con su estado
            Boolean _showButton = false;
            foreach (var _item in itemsMenu)
            {
                object _resourceName = null;
                _resourceName = GetGlobalResourceObject("Common", _item.Key);

                String _textItem = (_resourceName != null) ? _resourceName.ToString() : _item.Value.Key;

                RadMenuItem _rmiItem = new RadMenuItem(_textItem);
                _rmiItem.Value = _item.Key;
                //Si al menos un item viene en Enabled=true, entonces se muestra el boton de seguridad
                if (_item.Value.Value)
                    { _showButton = true; }
                _rmiItem.Enabled = _item.Value.Value;
                _rmnuSecuritySystem.Items.Add(_rmiItem);
            }
            //Si esto queda en True, quiere decir que al menos un item se muestra en el menu. entonces se agrega el boton
            //Caso contrario, no se agrega el boton de opciones generales a la pagina.
            if (_showButton)
            {
                //Inyecta el menu dentro del Panel
                _mnuSSContainer.Controls.Add(_rmnuSecuritySystem);
                //inyecta el panel dentro de la Master
                FwMasterPage.AddCustomMenuPanel(_mnuSSContainer);

                //Inyecta codigo JavaScript para el Show del menu al MouseOver sobre el boton
                InjectContentToolbarBtnHandler(_mnuSSContainer.ClientID);
                _ibtnToolbarButtonSecurity.OnClientClick = "ContentToolbarBtn" + _mnuSSContainer.ClientID + "_OnMouseOver(this, event)"; //"return false;";

                //Original !!!
                //_ibtnToolbarButtonSecurity.OnClientClick = "return false;";
                //_ibtnToolbarButtonSecurity.Attributes["onmouseover"] = "ContentToolbarBtn" + _mnuSSContainer.ClientID + "_OnMouseOver(this)";
                //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

                //Define eventos cliente sobre el menu.
                _rmnuSecuritySystem.OnClientMouseOut = "OnClientMouseOutHandler" + _mnuSSContainer.ClientID;
                _rmnuSecuritySystem.OnClientMouseOver = "OnMenuMouseOver" + _mnuSSContainer.ClientID;

                //Inyecta el Boton en la Master
                FwMasterPage.ContentNavigatorToolbarAdd(_ibtnToolbarButtonSecurity, false);
            }
            //Finalmente retorna el Menu.
            return _rmnuSecuritySystem;
        }
        /// <summary>
        /// Este metodo se encarga de Crear e Inyectar el ImageButton para el menu del GRC en el ContentNavigatorToolbar.
        /// </summary>
        protected void BuildContextInfoShowMenuButton()
        {
            ImageButton btnContentToolbarContentNavigatorShowMenuContextInfo = new ImageButton();
            btnContentToolbarContentNavigatorShowMenuContextInfo.ID = "btnContentToolbarContentNavigatorShowMenuContextInfo";
            btnContentToolbarContentNavigatorShowMenuContextInfo.ToolTip = GetGlobalResourceObject("MasterFW", "btnContentToolbarContentNavigatorShowMenuTooltip").ToString();
            btnContentToolbarContentNavigatorShowMenuContextInfo.ImageUrl = "~/Skins/Images/Trans.gif";
            btnContentToolbarContentNavigatorShowMenuContextInfo.CssClass = Common.ConstantsStyleClassName.cssNameGRCMenuUnSelected;
            
            //FwMasterPage.ContentNavigatorToolbarAdd(btnContentToolbarContentNavigatorShowMenuContextInfo, true);
            FwMasterPage.ContentNavigatorCustomPanelsToolbarOpenMenuAdd(Common.Constants.ContextInformationKey, btnContentToolbarContentNavigatorShowMenuContextInfo);
        }
        /// <summary>
        /// Este metodo se encarga de Crear e Inyectar el ImageButton para el menu del ContextElementMaps en el ContentNavigatorToolbar.
        /// </summary>
        protected void BuildContextElementMapsShowMenuButton()
        {
            ImageButton btnContentToolbarContentNavigatorShowMenuContextElementMaps = new ImageButton();
            btnContentToolbarContentNavigatorShowMenuContextElementMaps.ID = "btnContentToolbarContentNavigatorShowMenuContextElementMaps";
            btnContentToolbarContentNavigatorShowMenuContextElementMaps.ToolTip = GetGlobalResourceObject("MasterFW", "btnContentToolbarContentNavigatorShowMenuContextElementMapsTooltip").ToString();
            //btnContentToolbarContentNavigatorShowMenuContextElementMaps.ImageUrl = Common.Functions.GetAbsolutePathAppThemes + "/Images/Icons/ContentToolBar/Security.png";
            btnContentToolbarContentNavigatorShowMenuContextElementMaps.ImageUrl = "~/Skins/Images/Trans.gif";
            btnContentToolbarContentNavigatorShowMenuContextElementMaps.CssClass = Common.ConstantsStyleClassName.cssNameContextElementMapContentButton;

            FwMasterPage.ContentNavigatorCustomPanelsToolbarOpenMenuAdd(Common.Constants.ContextElementMapsKey, btnContentToolbarContentNavigatorShowMenuContextElementMaps);
        }
        protected String GetActionTitleDecorator(RadMenuItem menuItem)
        {
            return " [" + menuItem.Text + "]";
        }
    }

    public class PFTemplate : ITemplate
    {
        public PFTemplate()
        {
            
        }

        public void InstantiateIn(Control container)
        {
            PlaceHolder _phContentToolTipProcessFramework = new PlaceHolder();

            Label _lblTitle = new Label();
            _lblTitle.ID = "lblContentToolTipPFTitle";
            _lblTitle.Text = "Muestro esto porque tengo ganas";

            _phContentToolTipProcessFramework.Controls.Add(_lblTitle);
            container.Controls.Add(_phContentToolTipProcessFramework);
        }
    }
}
