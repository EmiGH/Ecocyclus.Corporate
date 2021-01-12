using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Configuration;
using System.Threading;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using Telerik.Web.UI;
using System.Linq;
using Condesus.EMS.Business.EP.Entities;
using Condesus.EMS.Business.DS.Entities;

namespace Condesus.EMS.WebUI
{
    public partial class BasePage : System.Web.UI.Page
    {
        #region Internal Properties
        private EventHandler _GridLinkButtonClick;
        private RadTreeView _HierarchicalTreeViewInCombo = null;
        private Boolean _IsFilterHierarchy;

        //private String _EntityNameHierarchical;
        //private String _EntityNameHierarchicalChildren;

        private String _EntityNameToRemove;
        private String _SelectedValueDefaultComboBox = String.Empty;
        private Panel _FilterPanel = null;
        private String _EntityNameComboFilter;
        private String _EntityNameChildrenComboFilter;

        //private String _FilterExpressionGrid = String.Empty;
        //private Dictionary<String, Object> _ManageEntityParams = new Dictionary<String, Object>();
        //private Dictionary<String, DataTable> _DataTableListManage = new Dictionary<String, DataTable>();
        private long _IdOrganizationJT
        {
            get { return Convert.ToInt64(ViewState["IdOrganizationJT"]); }
            set { ViewState["IdOrganizationJT"] = value.ToString(); }
        }
        private long _IdOrganization
        {
            get { return Convert.ToInt64(ViewState["_IdOrganization"]); }
            set { ViewState["_IdOrganization"] = value.ToString(); }
        }
        #endregion

        #region External Properties
        public EventHandler GridLinkButtonClick
        {
            get { return _GridLinkButtonClick; }
            set { _GridLinkButtonClick = value; }
        }
        public Boolean IsFilterHierarchy
        {
            get { return _IsFilterHierarchy; }
            set { _IsFilterHierarchy = value; }
        }
        protected String EntityNameComboFilter
        {
            get { return _EntityNameComboFilter; }
            set { _EntityNameComboFilter = value; }
        }
        protected String EntityNameChildrenComboFilter
        {
            get { return _EntityNameChildrenComboFilter; }
            set { _EntityNameChildrenComboFilter = value; }
        }
        protected String SelectedValueDefaultComboBox
        {
            get { return _SelectedValueDefaultComboBox; }
            set { _SelectedValueDefaultComboBox = value; }
        }
        protected String EntityNameToRemove
        {
            get { return _EntityNameToRemove; }
            set { _EntityNameToRemove = value; }
        }
        //protected String EntityNameHierarchicalChildren
        //{
        //    get { return _EntityNameHierarchicalChildren; }
        //    set { _EntityNameHierarchicalChildren = value; }
        //}
        //protected String EntityNameHierarchical
        //{
        //    get { return _EntityNameHierarchical; }
        //    set { _EntityNameHierarchical = value; }
        //}

        //Persistentes en SESSION
        protected String FilterExpressionGrid
        {
            //get { return _FilterExpressionGrid; }
            //set { _FilterExpressionGrid = value; }
            get
            {
                if (Session["FilterExpressionGrid"] == null)
                {
                    Session["FilterExpressionGrid"] = String.Empty;
                }
                return (String)Session["FilterExpressionGrid"];
            }
            set
            { Session["FilterExpressionGrid"] = value; }
        }
        protected Dictionary<String, DataTable> DataTableListManage
        {
            //get { return _DataTableListManage; }
            //set { _DataTableListManage = value; }
            get
            {
                if (Session["DataTableListManage"] == null)
                {
                    Session["DataTableListManage"] = new Dictionary<String, DataTable>();
                }
                return (Dictionary<String, DataTable>)Session["DataTableListManage"];

            }
            set { Session["DataTableListManage"] = value; }
        }
        protected Dictionary<String, Object> ManageEntityParams
        {
            get
            {
                if (Session["ManageEntityParams"] == null)
                {
                    Session["ManageEntityParams"] = new Dictionary<String, Object>();
                }
                return (Dictionary<String, Object>)Session["ManageEntityParams"];
            }
            set { Session["ManageEntityParams"] = value; }
            //get { return _ManageEntityParams; }
            //set { _ManageEntityParams = value; }
        }
        protected Boolean IsOperator()
        {
            //Si hay una tarea asociada al post, retorna true (es operador!)
            foreach (Post _post in EMSLibrary.User.Person.Posts)
            {
                if ((_post.ProcessTaskOperator != null) && (_post.ProcessTaskOperator.Count > 0))
                {
                    return true;
                }
            }
            //Si llega hasta aca, no es operador!
            return false;
        }
        protected Boolean IsOperatorOnly()
        {
            Boolean _operatorOnly = false;
            if (ConfigurationManager.AppSettings["OperatorOnly"] != null)
            {
                _operatorOnly = Convert.ToBoolean(ConfigurationManager.AppSettings["OperatorOnly"].ToString());
            }
            return _operatorOnly;
        }

        #endregion

        #region PageTitle



        protected String PageTitle
        {
            set
            {
                ((Condesus.EMS.WebUI.EMS)this.Master).PageTitle = value;
            }
        }

        protected String PageTitleSubTitle
        {
            set
            {
                ((Condesus.EMS.WebUI.EMS)this.Master).PageTitleSubTitle = value;
            }
        }

        protected virtual void SetPagetitle()
        {
            this.PageTitle = "Undefined";
        }

        protected virtual void SetPageTileSubTitle()
        {
            this.PageTitleSubTitle = "[-]";
        }

        protected String GetTitleSectionValueFromGlobalResource(String className, String key)
        {
            try
            {
                Object _bufRetString = GetGlobalResourceObject(className, key);

                if (_bufRetString != null)
                    return _bufRetString.ToString();
            }
            catch { }

            //return this.GetType().Name;
            return String.Empty;
        }

        protected String GetTitleSectionValueFromLocalResource(String key)
        {
            try
            {
                Object _bufRetString = GetLocalResourceObject(key);

                if (_bufRetString != null)
                    return _bufRetString.ToString();
            }
            catch { }

            //return this.GetType().Name;
            return "Undefined";
        }

        #endregion

        protected Pnyx.WebControls.PnyxStatusBar StatusBar
        {
            get { return ((Condesus.EMS.WebUI.EMS)this.Master).StatusBar; }
        }
        protected Condesus.EMS.Business.EMS EMSLibrary
        {
            get
            {
                //if (Session["EMSLibrary"] == null)
                //{
                //    Session["EMSLibrary"] = String.Empty;
                //}
                return (Condesus.EMS.Business.EMS)Session["EMSLibrary"];
            }
            set
            { Session["EMSLibrary"] = value; }
        }
        protected Int64 IdOrganization
        {
            get
            {
                //object o = Session["IdOrganization"];
                object o = EMSLibrary.User.Person.Organization.IdOrganization;
                if (o != null)
                    //return Convert.ToInt64(Session["IdOrganization"]);
                    return EMSLibrary.User.Person.Organization.IdOrganization;
                throw new Exception("No organization defined");
            }
        }
        //protected Int64 IdOrganizationDefaultUser
        //{
        //    get
        //    {
        //        return EMSLibrary.User.Person.Organization.IdOrganization;
        //    }
        //}

        private SiteMapNode OnSiteMapResolve(object sender, SiteMapResolveEventArgs args)
        {
            SiteMapProvider provider = sender as SiteMapProvider; // Recuperamos el SiteMapPath
            return provider.FindSiteMapNode(HttpContext.Current.Request.CurrentExecutionFilePath); // Lo devolvemos de acuerdo a la pagina actual.
        }

        #region Content Forms
        protected void SetContentTableRowsCss(HtmlTable tblContentForm)
        {
            String _css = "TRPar";

            foreach (HtmlTableRow _tr in tblContentForm.Rows)
            {
                _tr.Attributes["class"] = _css;
                AlternateRowClass(ref _css);
            }
        }
        private void AlternateRowClass(ref String cssClass)
        {
            cssClass = (cssClass == "TRPar") ? "TRImpar" : "TRPar";
        }
        #endregion

        #region Navigator

        protected void SetNavigator()
        {
            Common.Location _location = new Condesus.EMS.WebUI.Common.Location(Page.AppRelativeVirtualPath);


            ((Common.Navigator)Session["Navigator"]).Current = _location;
        }
        protected void SetNavigator(String[,] param)
        {
            Common.Location _location = new Condesus.EMS.WebUI.Common.Location(Page.AppRelativeVirtualPath);

            for (int i = 0; i < param.Length / 2; i++)
            {
                _location.Add(param[i, 0], param[i, 1]);
            }

            ((Common.Navigator)Session["Navigator"]).Current = _location;
        }
        protected void Back()
        {
            Common.Location _location = ((Common.Navigator)Session["Navigator"]).Previous;
            foreach (DictionaryEntry _item in _location.Items)
            {
                Context.Items.Add(_item.Key, _item.Value);
            }
            ((Common.Navigator)Session["Navigator"]).Back();

            Server.Transfer(_location.Url);
        }
        protected void Back(Boolean redirect)
        {
            Common.Location _location = ((Common.Navigator)Session["Navigator"]).Previous;
            foreach (DictionaryEntry _item in _location.Items)
            {
                Context.Items.Add(_item.Key, _item.Value);
            }
            ((Common.Navigator)Session["Navigator"]).Back();

            if (redirect)
            {
                Response.Redirect(_location.Url);
            }
            else
            {
                Server.Transfer(_location.Url);
            }
        }
        protected void ValidateClearXMLTreeViewGlobalMenu(String entityName)
        {
            //Como viene de un ADD o un Delete, y es una element MAP, entonces, limpia el tree menu global!
            switch (entityName)
            {
                case Common.ConstantsEntitiesName.PF.ProcessClassification:
                case Common.ConstantsEntitiesName.PF.Process:
                case Common.ConstantsEntitiesName.PF.ProcessGroupProcess:
                case Common.ConstantsEntitiesName.DS.OrganizationClassification:
                case Common.ConstantsEntitiesName.DS.Organization:
                case Common.ConstantsEntitiesName.PA.IndicatorClassification:
                case Common.ConstantsEntitiesName.PA.Indicator:
                case Common.ConstantsEntitiesName.IA.ProjectClassification:
                case Common.ConstantsEntitiesName.KC.ResourceClassification:
                case Common.ConstantsEntitiesName.KC.Resource:
                    _TreeViewGlobalMenuXML = String.Empty;
                    break;
            }
        }

        #endregion

        #region PopUp Helper
        // <Private - Summary>
        // Setea la funcionalidad de PopUpHelper a todos los Labels del Sistema (incluidos los Headers de las Grillas)
        // El seteo es "automatico" (lo llama el Onload de Base) 
        // y la busqueda del Resource es "automatica" (si existe le da funcionalidad si no lo ignora)
        private void BuildHelperInterface()
        {
            Boolean _needsPopupFunctionality = false;
            Label _lblControl = null;
            RadGrid _radControl = null;

            foreach (Control _c in this.Page.Controls)
            {
                if (_c is Label)
                {
                    _lblControl = (Label)_c;
                    SetTooltipLabel(_lblControl, ref _needsPopupFunctionality);
                }
                if (_c is RadGrid)
                {
                    _radControl = (RadGrid)_c;
                    SetTooltipRadGrid(_radControl, ref _needsPopupFunctionality);
                }

                if (_c.Controls.Count > 0)
                    BuildHelperInterfaceChilds(_c, ref _needsPopupFunctionality);
            }

            //Si encontro un control que necesita mostrar un PopUp, inyecta la funcionalidad.
            if (_needsPopupFunctionality)
                InyectPopupHelper();
        }

        //Funcion Recursiva de SetLabelHelperResource()
        private void BuildHelperInterfaceChilds(Control child, ref Boolean needsPopupFunctionality)
        {
            Label _lblControl = null;
            RadGrid _radControl = null;

            foreach (Control _c in child.Controls)
            {
                if (_c is Label)
                {
                    _lblControl = (Label)_c;
                    SetTooltipLabel(_lblControl, ref needsPopupFunctionality);
                }
                if (_c is RadGrid)
                {
                    _radControl = (RadGrid)_c;
                    SetTooltipRadGrid(_radControl, ref needsPopupFunctionality);
                }

                if (_c.Controls.Count > 0)
                    BuildHelperInterfaceChilds(_c, ref needsPopupFunctionality);
            }
        }

        //Le da la funcionalidad de PopUp al Label
        private void SetTooltipLabel(Label labelControl, ref Boolean needsPopupFunctionality)
        {
            //Si el label no tiene ID, lo saltea.
            if (labelControl.ID != null)
            {
                String _helperText = GetHelperText(labelControl.ID);

                //Si no lo encuentra en el Resource, no lo inserta.
                if (_helperText != null)
                {
                    //Como lo encontro, entonces le pone el texto.
                    labelControl.Attributes["onmouseover"] = "DoPupupHelper(this, '" + _helperText + "');";
                    labelControl.Attributes["onmouseout"] = "HidePopupHelper();";
                    labelControl.Style["cursor"] = "help";

                    //Con que un Label necesite PopUp, se inyecta el Javascript con la funcionalidad
                    needsPopupFunctionality = true;
                }
            }
        }

        //Le da la funcionalidad de PopUp al Header e las Columnas de un radGrid
        //La key del Resource : 'idGrilla' (grid+Entidad) + 'UniqueName'
        private void SetTooltipRadGrid(RadGrid radControl, ref Boolean needsPopupFunctionality)
        {
            foreach (GridColumn _gridCol in radControl.Columns)
            {
                String _helperText = GetHelperText(radControl.ID + _gridCol.UniqueName.Replace(" ", ""));
                if (_helperText != null)
                {
                    _gridCol.HeaderText = "<span onmouseover=\"DoPupupHelper(this, '" + _helperText + "')\" onmouseout=\"HidePopupHelper()\">" + _gridCol.HeaderText + "</span>";
                    needsPopupFunctionality = true;
                }
            }
        }

        //Obtiene el Texto del resource con la KEY formada con el nombre del control mas nombre pagina.
        //Se concatena el id del control mas el nombre de la pagina en forma pura EJ.: lbltitleorganizationsmanage o lblsubtitlelogin
        private String GetHelperText(String key)
        {
            Object _objBuffer = GetGlobalResourceObject("CommonHelper", key.ToLower() + this.GetType().BaseType.Name.ToLower());

            if (_objBuffer != null)
                return _objBuffer.ToString();

            return null;
        }
        #endregion

        #region JavaScript
        protected String OpenHtmlJavaScript()
        {
            return "<script type=\"text/javascript\"> \n";
        }
        protected String CloseHtmlJavaScript()
        {
            return "</script>";
        }
        protected String InsertBreakPointJavaScript()
        {
            return "debugger; \n";
        }
        protected void InjectJavascript(String key, String jsScript)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), key, jsScript);
        }
        protected void InjectAlert(String mensaje)
        {
            System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

            _sbBuffer.Append(OpenHtmlJavaScript());
            _sbBuffer.Append("alert('" + mensaje + "');");
            _sbBuffer.Append(CloseHtmlJavaScript());

            InjectJavascript("JS_SHOW_MENU", _sbBuffer.ToString());
        }
        protected void InjectPostBack(String btnTransferAddClientID)
        {
            System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

            _sbBuffer.Append(OpenHtmlJavaScript());
            //Inicializacion de Variables PageRequestManager
            //_sbBuffer.Append("window.onload = InitializePostBackAjax;                                                               \n");

            //_sbBuffer.Append("window.attachEvent('onload', InitializePostBackAjax);                                         \n");
            _sbBuffer.Append("  var prm;                                                                                      \n");
            _sbBuffer.Append("  var postBackElement;                                                                          \n");
            _sbBuffer.Append("  var _IEXPLORER = 'Microsoft Internet Explorer';                                             \n");
            _sbBuffer.Append("  var _FIREFOX = 'Netscape';                                                                  \n");
            _sbBuffer.Append("  var _BrowserName = navigator.appName;                                                       \n");

            _sbBuffer.Append("  if (_BrowserName == _IEXPLORER)                                                             \n");
            _sbBuffer.Append("  {   //IE and Opera                                                                          \n");
            _sbBuffer.Append("      window.attachEvent('onload', InitializePostBackAjax);                                \n");
            _sbBuffer.Append("  }                                                                                           \n");
            _sbBuffer.Append("  else                                                                                        \n");
            _sbBuffer.Append("  {   //FireFox                                                                               \n");
            _sbBuffer.Append("      document.addEventListener('DOMContentLoaded', InitializePostBackAjax, false);        \n");
            _sbBuffer.Append("  }                                                                                           \n");

            _sbBuffer.Append("function InitializePostBackAjax()                                                                     \n");
            _sbBuffer.Append("{                                                                                             \n");
            _sbBuffer.Append("  prm = Sys.WebForms.PageRequestManager.getInstance();                                        \n");
            _sbBuffer.Append("  prm.add_initializeRequest(InitializeRequest);                                               \n");
            _sbBuffer.Append("}                                                                                             \n");
            //Handlers          
            _sbBuffer.Append("function InitializeRequest(sender, args)                                                      \n");
            _sbBuffer.Append("{                                                                                             \n");
            _sbBuffer.Append("  postBackElement = args.get_postBackElement();                                               \n");
            //Si es un LinkButton Attacheado a la Grilla (sino puede ser cualquier evento de la Grilla (paginador etc)
            _sbBuffer.Append("  if (document.getElementById('radMenuItemClicked').value== 'Language')                       \n");
            _sbBuffer.Append("  {                                                                                           \n");
            _sbBuffer.Append("      args.set_cancel(true);                                                                  \n");
            //Dispara el OnClick del btnHidden emulando un post back normal por sobre el async que dispara el Boton de la Grilla
            _sbBuffer.Append("      DoNormalPostBack(document.getElementById('" + btnTransferAddClientID + "'));\n");
            _sbBuffer.Append("  }                                                                                           \n");
            _sbBuffer.Append("}                                                                                             \n");
            //El Handler que Cancela el PostBack "Asyncronic" del UpdatePanel, termina haciendo un PostBack Normal
            _sbBuffer.Append("function DoNormalPostBack(btnPostBack)                                                        \n");
            _sbBuffer.Append("{                                                                                             \n");
            _sbBuffer.Append("  document.getElementById('globalUprog').style.display = 'block';                             \n");
            _sbBuffer.Append("  btnPostBack.click();                                                                        \n");
            _sbBuffer.Append("}                                                                                             \n");

            _sbBuffer.Append(CloseHtmlJavaScript());

            InjectJavascript("JS_PostBack", _sbBuffer.ToString());
        }
        /// <summary>
        /// Falta documentar: General Option de rad menu, pincha en el server transfer, los selection al ser Context, no
        /// </summary>
        /// <param name="hdn_childRequestClientID"></param>
        /// <param name="btnPostBackClientID"></param>
        /// <param name="uProgressTabConainerClientID"></param>
        protected void InjectTabsPostBack(String hdn_childRequestClientID, String btnPostBackClientID, String uProgressTabConainerClientID)
        {
            System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

            _sbBuffer.Append(OpenHtmlJavaScript());
            //Inicializacion de Variables PageRequestManager
            //_sbBuffer.Append("window.onload = InitializeTabAjax;                                                          \n");
            //_sbBuffer.Append("window.attachEvent('onload', InitializeTabAjax);                                              \n");
            _sbBuffer.Append("var prm;                                                                                      \n");
            _sbBuffer.Append("var postBackElement;                                                                          \n");
            _sbBuffer.Append("  var _IEXPLORER = 'Microsoft Internet Explorer';                                             \n");
            _sbBuffer.Append("  var _FIREFOX = 'Netscape';                                                                  \n");
            _sbBuffer.Append("  var _BrowserName = navigator.appName;                                                       \n");

            _sbBuffer.Append("  if (_BrowserName == _IEXPLORER)                                                             \n");
            _sbBuffer.Append("  {   //IE and Opera                                                                          \n");
            _sbBuffer.Append("      window.attachEvent('onload', InitializeTabAjax);                                \n");
            _sbBuffer.Append("  }                                                                                           \n");
            _sbBuffer.Append("  else                                                                                        \n");
            _sbBuffer.Append("  {   //FireFox                                                                               \n");
            _sbBuffer.Append("      document.addEventListener('DOMContentLoaded', InitializeTabAjax, false);        \n");
            _sbBuffer.Append("  }                                                                                           \n");

            _sbBuffer.Append("function InitializeTabAjax()                                                                  \n");
            _sbBuffer.Append("{                                                                                             \n");
            _sbBuffer.Append("  prm = Sys.WebForms.PageRequestManager.getInstance();                                        \n");
            _sbBuffer.Append("  prm.add_initializeRequest(InitializeRequest);                                               \n");
            _sbBuffer.Append("  prm.add_endRequest(EndRequest);                                                             \n");
            _sbBuffer.Append("}                                                                                             \n");
            //Handlers          
            _sbBuffer.Append("function InitializeRequest(sender, args)                                                      \n");
            _sbBuffer.Append("{                                                                                             \n");
            _sbBuffer.Append("  postBackElement = args.get_postBackElement();                                               \n");
            _sbBuffer.Append("}                                                                                             \n");
            _sbBuffer.Append("function EndRequest(sender, args)                                                             \n");
            _sbBuffer.Append("{                                                                                             \n");
            _sbBuffer.Append("   if (document.getElementById('" + hdn_childRequestClientID + "').value == 'true')           \n");
            _sbBuffer.Append("      DoNormalPostBack(document.getElementById('" + btnPostBackClientID + "'));               \n");
            _sbBuffer.Append("}                                                                                             \n");
            _sbBuffer.Append("function DoNormalPostBack(btnPostBack)                                                        \n");
            _sbBuffer.Append("{                                                                                             \n");
            _sbBuffer.Append("   $get('" + uProgressTabConainerClientID + "').style.display = 'block';                      \n");
            _sbBuffer.Append("   btnPostBack.click();                                                                       \n");
            _sbBuffer.Append("}                                                                                             \n");

            _sbBuffer.Append(CloseHtmlJavaScript());

            InjectJavascript("JS_TabPostBack", _sbBuffer.ToString());
        }
        protected void InjectCheckIndexesTags()
        {   //Esta funcion se ejecuta al hacer click sobre un item del menuRad
            System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

            _sbBuffer.Append(OpenHtmlJavaScript());

            _sbBuffer.Append("function CheckIndexesTags(source, args)                       \n");
            _sbBuffer.Append("{                                                             \n");
            _sbBuffer.Append("  var _count=0;                                               \n");
            _sbBuffer.Append("  var _pos = args.Value.indexOf('__');                        \n");
            _sbBuffer.Append("  while (_pos > -1)                                           \n");
            _sbBuffer.Append("  {                                                           \n");
            _sbBuffer.Append("      _count += 1;                                            \n");
            _sbBuffer.Append("      _pos = args.Value.indexOf('__', _pos+1);                \n");
            _sbBuffer.Append("  }                                                           \n");
            _sbBuffer.Append("  _pos = args.Value.indexOf('--');                            \n");
            _sbBuffer.Append("  while (_pos > -1)                                           \n");
            _sbBuffer.Append("  {                                                           \n");
            _sbBuffer.Append("      _count += 1;                                            \n");
            _sbBuffer.Append("      _pos = args.Value.indexOf('--', _pos+1);                \n");
            _sbBuffer.Append("  }                                                           \n");
            _sbBuffer.Append("  if ((_count % 2) == 0)                                      \n");
            _sbBuffer.Append("  {                                                           \n");
            _sbBuffer.Append("      args.IsValid = true;                                    \n");
            _sbBuffer.Append("      return;                                                 \n");
            _sbBuffer.Append("  }                                                           \n");
            _sbBuffer.Append("   args.IsValid = false;                                      \n");
            _sbBuffer.Append("}                                                             \n");

            _sbBuffer.Append(CloseHtmlJavaScript());

            InjectJavascript("JS_CheckIndexesTags", _sbBuffer.ToString());
        }
        //protected void InjectOnClientNodeClicking(String radComboBoxClientID, String radTreeViewClientID, String entity, String radComboWithTree)
        //{
        //    System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

        //    _sbBuffer.Append(OpenHtmlJavaScript());
        //    //_sbBuffer.Append("window.attachEvent('onload', InitOnClientNodeClickingJSGlobalVars" + entity + ");                                         \n");
        //    _sbBuffer.Append("  var _IEXPLORER = 'Microsoft Internet Explorer';                                             \n");
        //    _sbBuffer.Append("  var _FIREFOX = 'Netscape';                                                                  \n");
        //    _sbBuffer.Append("  var _BrowserName = navigator.appName;                                                       \n");

        //    _sbBuffer.Append("  if (_BrowserName == _IEXPLORER)                                                             \n");
        //    _sbBuffer.Append("  {   //IE and Opera                                                                          \n");
        //    _sbBuffer.Append("      window.attachEvent('onload', InitOnClientNodeClickingJSGlobalVars" + entity + ");                                \n");
        //    _sbBuffer.Append("  }                                                                                           \n");
        //    _sbBuffer.Append("  else                                                                                        \n");
        //    _sbBuffer.Append("  {   //FireFox                                                                               \n");
        //    _sbBuffer.Append("      document.addEventListener('DOMContentLoaded', InitOnClientNodeClickingJSGlobalVars" + entity + ", false);        \n");
        //    _sbBuffer.Append("  }                                                                                           \n");
        //    _sbBuffer.Append("var ComboBoxTreeview" + entity + " = null;                                                            \n");

        //    _sbBuffer.Append("function InitOnClientNodeClickingJSGlobalVars" + entity + "()                                         \n");
        //    _sbBuffer.Append("{                                                                                                     \n");
        //    _sbBuffer.Append("  ComboBoxTreeview" + entity + "  = document.getElementById('" + radComboWithTree + "');              \n");
        //    _sbBuffer.Append("  ComboBoxTreeview" + entity + ".onclick = StopPropagation;                                           \n");
        //    _sbBuffer.Append("}                                                                                                     \n");

        //    _sbBuffer.Append("function OnClientNodeClicking" + entity + "(sender, args)                                             \n");
        //    _sbBuffer.Append("{                                                                                                     \n");
        //    //Obtiene el menu
        //    _sbBuffer.Append("  var comboBox = $find('" + radComboBoxClientID + "');                                                \n");
        //    //Obtiene el nodo seleccionado en el TreeView
        //    _sbBuffer.Append("  var node = args.get_node();                                                                         \n");
        //    //Setea el texto del combo con el nodo del tree
        //    _sbBuffer.Append("  comboBox.set_text(node.get_text());                                                                 \n");
        //    //_sbBuffer.Append("  comboBox.set_value(node.get_value());                                                               \n");
        //    _sbBuffer.Append("  comboBox.trackChanges();                                                                            \n");
        //    _sbBuffer.Append("  comboBox.get_items().getItem(0).set_value(node.get_value());                                         \n");
        //    _sbBuffer.Append("  comboBox.commitChanges();                                                                           \n");
        //    //Oculta el combo y su tree.-
        //    _sbBuffer.Append("  comboBox.hideDropDown();                                                                            \n");
        //    _sbBuffer.Append("}                                                                                                     \n");

        //    //Este es el codigo del StopPropagation del click del div que contiene todo esto.
        //    _sbBuffer.Append("function StopPropagation(e)                                                                           \n");
        //    _sbBuffer.Append("{                                                                                                     \n");
        //    _sbBuffer.Append("  if(!e)                                                                                              \n");
        //    _sbBuffer.Append("  {                                                                                                   \n");
        //    _sbBuffer.Append("      e = window.event;                                                                               \n");
        //    _sbBuffer.Append("  }                                                                                                   \n");
        //    _sbBuffer.Append("  e.cancelBubble = true;                                                                              \n");
        //    _sbBuffer.Append("}                                                                                                     \n");

        //    _sbBuffer.Append("function OnClientDropDownOpenedHandler(sender, eventArgs)                                             \n");
        //    _sbBuffer.Append("{                                                                                                     \n");
        //    _sbBuffer.Append("  var tree = sender.get_items().getItem(0).findControl('" + radTreeViewClientID + "');                \n");
        //    _sbBuffer.Append("  var selectedNode = tree.get_selectedNode();                                                         \n");
        //    _sbBuffer.Append("  if (selectedNode)                                                                                   \n");
        //    _sbBuffer.Append("  {                                                                                                   \n");
        //    _sbBuffer.Append("      selectedNode.scrollIntoView();                                                                  \n");
        //    _sbBuffer.Append("  }                                                                                                   \n");
        //    _sbBuffer.Append("}                                                                                                     \n");

        //    //// change the marginTop of the drop down to be that of the flyout  
        //    //_sbBuffer.Append("          var dropDownWrapper = sender.get_dropDownElement().parentNode;  \n");
        //    //_sbBuffer.Append("          if (!$telerik.isIE) {  \n");
        //    //_sbBuffer.Append("              dropDownWrapper.style.marginTop = document.body.parentNode.scrollTop + 'px;';  \n");
        //    //_sbBuffer.Append("          }  \n");
        //    //// change the position to be fixed to the flyout  
        //    //_sbBuffer.Append("          dropDownWrapper.style.position = 'fixed';  \n");


        //    //_sbBuffer.Append("  var tree = document.getElementById('" + radTreeViewClientID + "');                                                    \n");
        //    ////_sbBuffer.Append("  var tree = $find('" + radTreeViewClientID + "');                                                    \n");
        //    //_sbBuffer.Append("  var selectedNode = tree.get_selectedNode();                                                         \n");
        //    //_sbBuffer.Append("  if (selectedNode)                                                                                   \n");
        //    //_sbBuffer.Append("  {                                                                                                   \n");
        //    //_sbBuffer.Append("      selectedNode.scrollIntoView();                                                                  \n");
        //    //_sbBuffer.Append("  }                                                                                                   \n");

        //    _sbBuffer.Append(CloseHtmlJavaScript());

        //    InjectJavascript("JS_BeforeClientClick" + entity, _sbBuffer.ToString());
        //}
        protected void InjectOnClientNodeClicking(String radComboBoxClientID, String radTreeViewClientID, String entity, String radComboWithTree)
        {
            System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

            _sbBuffer.Append(OpenHtmlJavaScript());
            //_sbBuffer.Append("window.attachEvent('onload', InitOnClientNodeClickingJSGlobalVars" + entity + ");                                         \n");
            _sbBuffer.Append("  var _IEXPLORER = 'Microsoft Internet Explorer';                                             \n");
            _sbBuffer.Append("  var _FIREFOX = 'Netscape';                                                                  \n");
            _sbBuffer.Append("  var _BrowserName = navigator.appName;                                                       \n");

            _sbBuffer.Append("  if (_BrowserName == _IEXPLORER)                                                             \n");
            _sbBuffer.Append("  {   //IE and Opera                                                                          \n");
            _sbBuffer.Append("      window.attachEvent('onload', InitOnClientNodeClickingJSGlobalVars" + radTreeViewClientID + ");                                \n");
            _sbBuffer.Append("  }                                                                                           \n");
            _sbBuffer.Append("  else                                                                                        \n");
            _sbBuffer.Append("  {   //FireFox                                                                               \n");
            _sbBuffer.Append("      document.addEventListener('DOMContentLoaded', InitOnClientNodeClickingJSGlobalVars" + radTreeViewClientID + ", false);        \n");
            _sbBuffer.Append("  }                                                                                           \n");
            _sbBuffer.Append("var ComboBoxTreeview" + radTreeViewClientID + " = null;                                                            \n");

            _sbBuffer.Append("function InitOnClientNodeClickingJSGlobalVars" + radTreeViewClientID + "()                                         \n");
            _sbBuffer.Append("{                                                                                                     \n");
            _sbBuffer.Append("  ComboBoxTreeview" + radTreeViewClientID + "  = document.getElementById('" + radComboWithTree + "');              \n");
            _sbBuffer.Append("  ComboBoxTreeview" + radTreeViewClientID + ".onclick = StopPropagation;                                           \n");
            _sbBuffer.Append("}                                                                                                     \n");

            _sbBuffer.Append("function OnClientNodeClicking" + radTreeViewClientID + "(sender, args)                                             \n");
            _sbBuffer.Append("{                                                                                                     \n");
            //Obtiene el menu
            _sbBuffer.Append("  var comboBox = $find('" + radComboBoxClientID + "');                                                \n");
            //Obtiene el nodo seleccionado en el TreeView
            _sbBuffer.Append("  var node = args.get_node();                                                                         \n");
            //Setea el texto del combo con el nodo del tree
            _sbBuffer.Append("  comboBox.set_text(node.get_text());                                                                 \n");
            //_sbBuffer.Append("  comboBox.set_value(node.get_value());                                                               \n");
            _sbBuffer.Append("  comboBox.trackChanges();                                                                            \n");
            _sbBuffer.Append("  comboBox.get_items().getItem(0).set_value(node.get_value());                                         \n");
            _sbBuffer.Append("  comboBox.commitChanges();                                                                           \n");
            //Oculta el combo y su tree.-
            _sbBuffer.Append("  comboBox.hideDropDown();                                                                            \n");
            _sbBuffer.Append("}                                                                                                     \n");

            //Este es el codigo del StopPropagation del click del div que contiene todo esto.
            _sbBuffer.Append("function StopPropagation(e)                                                                           \n");
            _sbBuffer.Append("{                                                                                                     \n");
            _sbBuffer.Append("  if(!e)                                                                                              \n");
            _sbBuffer.Append("  {                                                                                                   \n");
            _sbBuffer.Append("      e = window.event;                                                                               \n");
            _sbBuffer.Append("  }                                                                                                   \n");
            _sbBuffer.Append("  e.cancelBubble = true;                                                                              \n");
            _sbBuffer.Append("}                                                                                                     \n");

            _sbBuffer.Append("function OnClientDropDownOpenedHandler" + radTreeViewClientID + "(sender, eventArgs)                                             \n");
            _sbBuffer.Append("{                                                                                                     \n");
            _sbBuffer.Append("  var tree = sender.get_items().getItem(0).findControl('" + radTreeViewClientID + "');                \n");
            _sbBuffer.Append("  var selectedNode = tree.get_selectedNode();                                                         \n");
            _sbBuffer.Append("  if (selectedNode)                                                                                   \n");
            _sbBuffer.Append("  {                                                                                                   \n");
            _sbBuffer.Append("      selectedNode.scrollIntoView();                                                                  \n");
            _sbBuffer.Append("  }                                                                                                   \n");
            _sbBuffer.Append("}                                                                                                     \n");

            _sbBuffer.Append(CloseHtmlJavaScript());

            InjectJavascript("JS_BeforeClientClick" + radTreeViewClientID, _sbBuffer.ToString());
        }

        protected void InjectGlobalUpdateProgress()
        {
            System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

            _sbBuffer.Append(OpenHtmlJavaScript());
            //Inicializacion de Variables PageRequestManager
            _sbBuffer.Append("var prmGlobalUprog=null;                                                                              \n");
            //_sbBuffer.Append("window.onload = InitializeAjaxGlobalUprog;                                                          \n");

            //_sbBuffer.Append("window.attachEvent('onload', InitializeAjaxGlobalUprog);                                              \n");

            //Constantes para identificar al navegador y variable publica para saber cual es el navegador actual.
            _sbBuffer.Append("  var _IEXPLORER = 'Microsoft Internet Explorer';                                             \n");
            _sbBuffer.Append("  var _FIREFOX = 'Netscape';                                                                  \n");
            _sbBuffer.Append("  var _BrowserName = navigator.appName;                                                       \n");

            _sbBuffer.Append("  if (_BrowserName == _IEXPLORER)                                                             \n");
            _sbBuffer.Append("  {   //IE and Opera                                                                          \n");
            _sbBuffer.Append("      window.attachEvent('onload', InitializeAjaxGlobalUprog);                                \n");
            _sbBuffer.Append("  }                                                                                           \n");
            _sbBuffer.Append("  else                                                                                        \n");
            _sbBuffer.Append("  {   //FireFox                                                                               \n");
            _sbBuffer.Append("      document.addEventListener('DOMContentLoaded', InitializeAjaxGlobalUprog, false);        \n");
            _sbBuffer.Append("  }                                                                                           \n");

            //Initialize
            _sbBuffer.Append("function InitializeAjaxGlobalUprog()                                                                  \n");
            _sbBuffer.Append("{                                                                                                     \n");
            //Add div Loading
            _sbBuffer.Append("  var divLoading = document.createElement('div');                                                     \n");
            _sbBuffer.Append("  divLoading.id = 'globalUprog';                                                                      \n");
            _sbBuffer.Append("  divLoading.className = 'Loading';                                                                   \n");
            _sbBuffer.Append("  divLoading.style.display='none';                                                                    \n");
            _sbBuffer.Append("  document.body.appendChild(divLoading);                                                              \n");
            //Add Handlers          
            _sbBuffer.Append("  prmGlobalUprog = Sys.WebForms.PageRequestManager.getInstance();                                     \n");
            _sbBuffer.Append("  prmGlobalUprog.add_initializeRequest(InitializeGlobalUprog);                                        \n");
            //_sbBuffer.Append("  prmGlobalUprog.add_pageLoading(InitializeGlobalUprog);                                            \n");
            //_sbBuffer.Append("  prmGlobalUprog.add_pageLoading(InitializeGlobalUprog);                                            \n");
            //_sbBuffer.Append("  prmGlobalUprog.add_pageLoaded(EndGlobalUprog);                                                    \n");

            _sbBuffer.Append("  prmGlobalUprog.add_endRequest(EndGlobalUprog);                                                      \n");
            _sbBuffer.Append("}                                                                                                     \n");

            //Handlres Methods
            _sbBuffer.Append("function InitializeGlobalUprog(sender, args)                                                          \n");
            _sbBuffer.Append("{                                                                                                     \n");
            _sbBuffer.Append("  document.getElementById('globalUprog').style.display = 'block';                                     \n");
            _sbBuffer.Append("  if ($get('FWMasterGlobalUpdateProgress') != null)                                                   \n");
            _sbBuffer.Append("  {                                                                                                   \n");
            _sbBuffer.Append("      $get('FWMasterGlobalUpdateProgress').style.display = 'block';                                   \n");
            _sbBuffer.Append("  }                                                                                                   \n");
            _sbBuffer.Append("}                                                                                                     \n");
            _sbBuffer.Append("function EndGlobalUprog(sender, args)                                                                 \n");
            _sbBuffer.Append("{                                                                                                     \n");
            _sbBuffer.Append("  document.getElementById('globalUprog').style.display = 'none';                                      \n");
            _sbBuffer.Append("  if ($get('FWMasterGlobalUpdateProgress') != null)                                                   \n");
            _sbBuffer.Append("  {                                                                                                   \n");
            _sbBuffer.Append("      $get('FWMasterGlobalUpdateProgress').style.display = 'none';                                    \n");
            _sbBuffer.Append("  }                                                                                                   \n");
            _sbBuffer.Append("}                                                                                                     \n");

            _sbBuffer.Append(CloseHtmlJavaScript());

            InjectJavascript("JS_GlobalUprogress", _sbBuffer.ToString());
        }
        protected void InjectValidateItemChecked(String nameListManageClientID)
        {
            System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

            _sbBuffer.Append(OpenHtmlJavaScript());

            _sbBuffer.Append("function ValidateItemChecked()                                                                                        \n");
            _sbBuffer.Append("{                                                                                                                     \n");
            _sbBuffer.Append("  var arrayInput = document.getElementById('" + nameListManageClientID + "').getElementsByTagName('INPUT');           \n");
            _sbBuffer.Append("  for(i = 0; i < arrayInput.length; i++)                                                                              \n");
            _sbBuffer.Append("  {                                                                                                                   \n");
            _sbBuffer.Append("      if (IsCheckBox(arrayInput[i]) && arrayInput[i].checked) return true;                                            \n");
            _sbBuffer.Append("  }                                                                                                                   \n");
            _sbBuffer.Append("  return false;                                                                                                       \n");
            _sbBuffer.Append("}                                                                                                                     \n");
            //Verifica que sea de tipo checkbox
            _sbBuffer.Append("function IsCheckBox(chk)                                                                                              \n");
            _sbBuffer.Append("{                                                                                                                     \n");
            _sbBuffer.Append("  if(chk.type == 'checkbox')                                                                                          \n");
            _sbBuffer.Append("  {                                                                                                                   \n");
            _sbBuffer.Append("      return true;                                                                                                    \n");
            _sbBuffer.Append("  }                                                                                                                   \n");
            _sbBuffer.Append("  else                                                                                                                \n");
            _sbBuffer.Append("  {                                                                                                                   \n");
            _sbBuffer.Append("      return false;                                                                                                   \n");
            _sbBuffer.Append("  }                                                                                                                   \n");
            _sbBuffer.Append("}                                                                                                                     \n");

            _sbBuffer.Append(CloseHtmlJavaScript());

            InjectJavascript("JS_ValidateItemCHK", _sbBuffer.ToString());

        }
        protected void InjectValidateDateTimePicker(String radDTPStartClientID, String radDTPEndClientID, String entity)
        {
            System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

            _sbBuffer.Append(OpenHtmlJavaScript());

            _sbBuffer.Append("function ValidateDateTimeRange" + entity + "(sender, e)                                       \n");
            _sbBuffer.Append("{                                                                                             \n");
            _sbBuffer.Append("    var _startDate = document.getElementById('" + radDTPStartClientID + "');                  \n");
            _sbBuffer.Append("    var _endDate = document.getElementById('" + radDTPEndClientID + "');                      \n");
            //Con esto obtiene los valores individuales de la fecha fin
            _sbBuffer.Append("    var value = _endDate.value.split('-');                                                    \n");
            _sbBuffer.Append("    var _year = value[0];                                                                     \n");
            _sbBuffer.Append("    var _month = value[1];                                                                    \n");
            _sbBuffer.Append("    var _day = value[2];                                                                      \n");
            _sbBuffer.Append("    var _hour = value[3];                                                                     \n");
            _sbBuffer.Append("    var _minute = value[4];                                                                   \n");
            _sbBuffer.Append("    var _second = value[5];                                                                   \n");
            //se guarda la fecha completa en tipo fecha.
            _sbBuffer.Append("    var _valueEndDate = new Date(_year, _month, _day, _hour, _minute, _second);               \n");
            //Ahora obtiene los valores individuales de la fecha inicio
            _sbBuffer.Append("    value = _startDate.value.split('-');                                                      \n");
            _sbBuffer.Append("    _year = value[0];                                                                         \n");
            _sbBuffer.Append("    _month = value[1];                                                                        \n");
            _sbBuffer.Append("    _day = value[2];                                                                          \n");
            _sbBuffer.Append("    _hour = value[3];                                                                         \n");
            _sbBuffer.Append("    _minute = value[4];                                                                       \n");
            _sbBuffer.Append("    _second = value[5];                                                                       \n");
            //guarda la fecha inicio en tipo fecha.
            _sbBuffer.Append("    var _valueStartDate = new Date(_year, _month, _day, _hour, _minute, _second);             \n");
            //finalmente compara las fechas-
            _sbBuffer.Append("    if (_valueStartDate >= _valueEndDate)                                                     \n");
            _sbBuffer.Append("    {                                                                                         \n");
            _sbBuffer.Append("        e.IsValid = false;                                                                    \n");
            _sbBuffer.Append("    }                                                                                         \n");
            _sbBuffer.Append("}                                                                                             \n");

            _sbBuffer.Append(CloseHtmlJavaScript());

            InjectJavascript("JS_ValidateDateTimePickerRange" + entity, _sbBuffer.ToString());
        }
        protected void InjectValidateDatePicker(String radDTPStartClientID, String radDTPEndClientID, String entity)
        {
            System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

            _sbBuffer.Append(OpenHtmlJavaScript());

            _sbBuffer.Append("function ValidateDateTimeRange" + entity + "(sender, e)                                       \n");
            _sbBuffer.Append("{                                                                                             \n");
            _sbBuffer.Append("    var _startDate = document.getElementById('" + radDTPStartClientID + "');                  \n");
            _sbBuffer.Append("    var _endDate = document.getElementById('" + radDTPEndClientID + "');                      \n");
            //Con esto obtiene los valores individuales de la fecha fin
            _sbBuffer.Append("    var value = _endDate.value.split('-');                                                    \n");
            _sbBuffer.Append("    var _year = value[0];                                                                     \n");
            _sbBuffer.Append("    var _month = value[1];                                                                    \n");
            _sbBuffer.Append("    var _day = value[2];                                                                      \n");
            //se guarda la fecha completa en tipo fecha.
            _sbBuffer.Append("    var _valueEndDate = new Date(_year, _month, _day, 00, 00, 00);                                        \n");
            //Ahora obtiene los valores individuales de la fecha inicio
            _sbBuffer.Append("    value = _startDate.value.split('-');                                                      \n");
            _sbBuffer.Append("    _year = value[0];                                                                         \n");
            _sbBuffer.Append("    _month = value[1];                                                                        \n");
            _sbBuffer.Append("    _day = value[2];                                                                          \n");
            //guarda la fecha inicio en tipo fecha.
            _sbBuffer.Append("    var _valueStartDate = new Date(_year, _month, _day, 00, 00, 00);                          \n");
            //finalmente compara las fechas-
            _sbBuffer.Append("    if (_valueStartDate >= _valueEndDate)                                                     \n");
            _sbBuffer.Append("    {                                                                                         \n");
            _sbBuffer.Append("        e.IsValid = false;                                                                    \n");
            _sbBuffer.Append("    }                                                                                         \n");
            _sbBuffer.Append("}                                                                                             \n");

            _sbBuffer.Append(CloseHtmlJavaScript());

            InjectJavascript("JS_ValidateDateTimePickerRange" + entity, _sbBuffer.ToString());
        }
        private void InyectPopupHelper()
        {
            System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

            _sbBuffer.Append(OpenHtmlJavaScript());

            //_sbBuffer.Append("window.attachEvent('onload', InitializePopUpObjects);                                         \n");
            _sbBuffer.Append("  var _IEXPLORER = 'Microsoft Internet Explorer';                                             \n");
            _sbBuffer.Append("  var _FIREFOX = 'Netscape';                                                                  \n");
            _sbBuffer.Append("  var _BrowserName = navigator.appName;                                                       \n");

            _sbBuffer.Append("  if (_BrowserName == _IEXPLORER)                                                             \n");
            _sbBuffer.Append("  {   //IE and Opera                                                                          \n");
            _sbBuffer.Append("      window.attachEvent('onload', InitializePopUpObjects);                                \n");
            _sbBuffer.Append("  }                                                                                           \n");
            _sbBuffer.Append("  else                                                                                        \n");
            _sbBuffer.Append("  {   //FireFox                                                                               \n");
            _sbBuffer.Append("      document.addEventListener('DOMContentLoaded', InitializePopUpObjects, false);        \n");
            _sbBuffer.Append("  }                                                                                           \n");

            _sbBuffer.Append("function InitializePopUpObjects() {                                                           \n");
            _sbBuffer.Append("    var popupHelper = document.createElement('div');                                          \n");
            _sbBuffer.Append("    popupHelper.id = 'divPopupHelper';                                                        \n");
            //_sbBuffer.Append("    popupHelper.className = 'PopupHelper';                                                    \n");
            _sbBuffer.Append("    popupHelper.style.display = 'none';                                                       \n");
            _sbBuffer.Append("    document.body.appendChild(popupHelper);                                                   \n");
            _sbBuffer.Append("}                                                                                             \n");

            _sbBuffer.Append("function DoPupupHelper(oLabel ,helperText) {                                                  \n");
            _sbBuffer.Append("    var popupHelper = $get('divPopupHelper');                                                 \n");
            _sbBuffer.Append("    var lblBounds = Sys.UI.DomElement.getBounds(oLabel);                                      \n");
            _sbBuffer.Append("    var x = lblBounds.x;                                                                      \n");
            _sbBuffer.Append("    var y = lblBounds.y;                                                                      \n");
            _sbBuffer.Append("    popupHelper.innerHTML = helperText;                                                       \n");
            _sbBuffer.Append("    popupHelper.style.left = String(x + 25) + 'px';                                                          \n");
            _sbBuffer.Append("    popupHelper.style.top = String(y + 20) + 0px';                                                           \n");

            _sbBuffer.Append("    popupHelper.style.paddingTop = popupHelper.style.paddingBottom = '4px';                   \n");
            _sbBuffer.Append("    popupHelper.style.paddingLeft = popupHelper.style.paddingRight = '10px';                  \n");
            _sbBuffer.Append("    popupHelper.style.backgroundColor = 'White';                                              \n");
            _sbBuffer.Append("    popupHelper.style.border = 'solid 1px #307DB3';                                           \n");
            _sbBuffer.Append("    popupHelper.style.zIndex = '1000';                                                        \n");

            _sbBuffer.Append("    popupHelper.style.position = 'absolute';                                                  \n");
            _sbBuffer.Append("    popupHelper.style.display = 'block';                                                      \n");
            _sbBuffer.Append("}                                                                                             \n");

            _sbBuffer.Append("function HidePopupHelper() {                                                                  \n");
            _sbBuffer.Append("    var popupHelper = $get('divPopupHelper');                                                 \n");
            _sbBuffer.Append("    popupHelper.innerHTML = '';                                                               \n");
            _sbBuffer.Append("    popupHelper.style.display = 'none';                                                       \n");
            _sbBuffer.Append("    popupHelper.style.left = '0px';                                                           \n");
            _sbBuffer.Append("    popupHelper.style.top = '0px';                                                            \n");
            _sbBuffer.Append("}                                                                                             \n");

            _sbBuffer.Append(CloseHtmlJavaScript());
            InjectJavascript("JS_POPUP_HELPER", _sbBuffer.ToString());
        }
        /// <summary>
        /// Este metodo revisa si el panel de menu esta dokeado, entonces cambia el class del boton.
        /// </summary>
        private void InjectSetClassButtonModule()
        {
            System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

            _sbBuffer.Append(OpenHtmlJavaScript());

            _sbBuffer.Append("function SetClassButtonModule()                                                                               \n");
            _sbBuffer.Append("{                                                                                                             \n");
            _sbBuffer.Append("    if (MenuIsDocked())                                                                                       \n");
            _sbBuffer.Append("    {                                                                                                         \n");
            _sbBuffer.Append("       var btnModule = $get('ctl00_btnGlobalToolbarGlobalNavigatorShowMenu');                                 \n");
            _sbBuffer.Append("       btnModule.className = 'GlobalToolbarModuleOpen';                                                       \n");
            _sbBuffer.Append("    }                                                                                                         \n");
            _sbBuffer.Append("}                                                                                                             \n");

            _sbBuffer.Append(CloseHtmlJavaScript());

            InjectJavascript("JS_FW_SetClassButtonModule", _sbBuffer.ToString());
        }
        /// <summary>
        /// Este metodo inyecta una funcion JavaScript, para verificar los validators de la pagina en modo cliente.
        /// </summary>
        private void InjectCheckClientValidatorPage()
        {
            //Esta funcion se ejecuta al hacer click sobre un item del menuRad
            System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

            _sbBuffer.Append(OpenHtmlJavaScript());

            //******************************************************
            //Este metodo vuelve a habilitar los validator de cada pageView.
            _sbBuffer.Append("function CheckClientValidatorPage()                                                                   \n");
            _sbBuffer.Append("{                                                                                                     \n");
            _sbBuffer.Append("      // Comprobamos si se está haciendo una validación                                               \n");
            _sbBuffer.Append("      if (typeof(Page_ClientValidate) == 'function')                                                  \n");
            _sbBuffer.Append("      {                                                                                               \n");
            _sbBuffer.Append("          // Si se está haciendo una validación, volver si ésta da resultado false                    \n");
            _sbBuffer.Append("          if (Page_ClientValidate() == false)                                                         \n");
            _sbBuffer.Append("          {                                                                                           \n");
            _sbBuffer.Append("              return false;                                                                           \n");
            _sbBuffer.Append("          }                                                                                           \n");
            _sbBuffer.Append("      }                                                                                               \n");
            _sbBuffer.Append("      return true;                                                                                    \n");
            _sbBuffer.Append("}                                                                                                     \n");

            _sbBuffer.Append(CloseHtmlJavaScript());

            InjectJavascript("JS_CheckClientValidatorPage", _sbBuffer.ToString());
        }
        protected void InjectRowContextMenu(String rmnSelectionClientID, String entity)
        {
            //Esta funcion es la encargada de mostrar el menu de opciones cuando se realiza click derecho sobre un registro en la grilla.
            System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

            _sbBuffer.Append(OpenHtmlJavaScript());

            _sbBuffer.Append("function RowContextMenu" + entity + "(sender, eventArgs)                                              \n");
            _sbBuffer.Append("{                                                                                                     \n");
            //Accede al menu
            _sbBuffer.Append("      var MyMenu = $find('" + rmnSelectionClientID + "');                                             \n");
            _sbBuffer.Append("      var evt = eventArgs.get_domEvent();                                                             \n");
            _sbBuffer.Append("      if(evt.target.tagName == 'INPUT' || evt.target.tagName == 'A')                                  \n");
            _sbBuffer.Append("      {                                                                                               \n");
            _sbBuffer.Append("          return;                                                                                     \n");
            _sbBuffer.Append("      }                                                                                               \n");
            _sbBuffer.Append("      var index = eventArgs.get_itemIndexHierarchical();                                              \n");
            //Marca como seleccionado el row
            _sbBuffer.Append("      sender.get_masterTableView().selectItem(sender.get_masterTableView().get_dataItems()[index].get_element(), true); \n");
            //Muestra el menu
            _sbBuffer.Append("      MyMenu.show(evt);                                                                                 \n");
            _sbBuffer.Append("      evt.cancelBubble = true;                                                                          \n");
            _sbBuffer.Append("      evt.returnValue = false;                                                                          \n");
            _sbBuffer.Append("      if (evt.stopPropagation)                                                                          \n");
            _sbBuffer.Append("      {                                                                                               \n");
            _sbBuffer.Append("          evt.stopPropagation();                                                                        \n");
            _sbBuffer.Append("          evt.preventDefault();                                                                         \n");
            _sbBuffer.Append("      }                                                                                               \n");
            _sbBuffer.Append("}                                                                                                     \n");

            _sbBuffer.Append(CloseHtmlJavaScript());

            InjectJavascript("JS_RowContextMenu" + entity, _sbBuffer.ToString());
        }

        protected void InjectContextMenuSelectionOnClientShowing(String listID, Boolean isRadGridList)
        {
            System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();
            _sbBuffer.Append(OpenHtmlJavaScript());

            _sbBuffer.Append("function rmnSelection_OnClientShowing(sender, args)                                           \n");
            _sbBuffer.Append("{                                                                                             \n");
            _sbBuffer.Append("    var items = sender.get_items();                                                           \n");
            _sbBuffer.Append("     for (var i=0; i<items.get_count(); i++)                                                  \n");
            _sbBuffer.Append("     {                                                                                        \n");
            _sbBuffer.Append("         var item = items.getItem(i);                                                         \n");
            _sbBuffer.Append("         item.set_visible(true);                                                         \n");
            //Indica si se busca el permiso sobre una grilla o sobre un TrewView.
            if (isRadGridList)
            {
                _sbBuffer.Append("         var grid = $find('" + listID + "');                                              \n");
                //_sbBuffer.Append("         var grid = document.getElementById('" + listID + "');                     \n");
                _sbBuffer.Append("         var MasterTable = grid.get_masterTableView();                                    \n");
                _sbBuffer.Append("         var selectedRows = MasterTable.get_selectedItems();                              \n");
                _sbBuffer.Append("         var row = selectedRows[0];                                                       \n");
                _sbBuffer.Append("         var _permissionType = MasterTable.getCellByColumnUniqueName(row, 'PermissionType').innerText;                         \n");
            }
            else
            {
                _sbBuffer.Append("         var treeView = $find('" + listID + "');                                          \n");
                //_sbBuffer.Append("         var treeView = document.getElementById('" + listID + "');                 \n");
                _sbBuffer.Append("         var selectedNode = treeView.get_selectedNode();                                  \n");
                _sbBuffer.Append("         var _permissionType = selectedNode.get_attributes().getAttribute('PermissionType')  \n");
            }
            _sbBuffer.Append("         //Si el item del menu es edit, delete, CreateException o ejecutar Calculo, debe verificar la seguridad                  \n");
            _sbBuffer.Append("         if ((item.get_value() == 'rmiEdit') || (item.get_value() == 'rmiDelete') || (item.get_value() == 'rmiCreateException') || (item.get_value() == 'rmiCompute'))            \n");
            _sbBuffer.Append("         {                                                                                    \n");
            _sbBuffer.Append("              if (_permissionType == 'View')                                                  \n");
            _sbBuffer.Append("              {                                                                               \n");
            _sbBuffer.Append("                  //Como el usuario no tiene permiso para hacer esta accion, se oculta        \n");
            _sbBuffer.Append("                  item.set_visible(false);                                                    \n");
            _sbBuffer.Append("              }                                                                               \n");
            _sbBuffer.Append("              else                                                                            \n");
            _sbBuffer.Append("              {                                                                               \n");
            _sbBuffer.Append("                  //Como el usuario tiene permiso para hacer esta accion, se visualiza        \n");
            _sbBuffer.Append("                  item.set_visible(true);                                                     \n");
            _sbBuffer.Append("              }                                                                               \n");
            _sbBuffer.Append("         }                                                                                    \n");

            _sbBuffer.Append("         //Aca verifica si se puede crear una excepcion o no.                                 \n");
            _sbBuffer.Append("         if (item.get_value() == 'rmiCreateException')                                        \n");
            _sbBuffer.Append("         {                                                                                    \n");
            _sbBuffer.Append("              var _executed = MasterTable.getCellByColumnUniqueName(row, 'Post').innerText;   \n");
            _sbBuffer.Append("              if (_executed == ' ') //No esta ejecutada aun, entonces no se puede crear una excepcion.\n");
            _sbBuffer.Append("              {                                                                               \n");
            _sbBuffer.Append("                  item.set_visible(false);                                                    \n");
            _sbBuffer.Append("              }                                                                               \n");
            _sbBuffer.Append("         }                                                                                    \n");

            //State 1 = Opened
            //State 2 = In Treatment
            //State 3 = Closed
            _sbBuffer.Append("         //Aca verifica los datos para las excepciones                                        \n");
            _sbBuffer.Append("         if (item.get_value() == 'rmiCloseException')                                         \n");
            _sbBuffer.Append("         {                                                                                    \n");
            _sbBuffer.Append("              var _exceptionState = MasterTable.getCellByColumnUniqueName(row, 'IdExecutionState').innerText;                         \n");
            _sbBuffer.Append("              if (_exceptionState == 3) //Closed                                              \n");
            _sbBuffer.Append("              {                                                                               \n");
            _sbBuffer.Append("                  item.set_visible(false);                                                    \n");
            _sbBuffer.Append("              }                                                                               \n");
            _sbBuffer.Append("         }                                                                                    \n");
            _sbBuffer.Append("         if (item.get_value() == 'rmiTreatException')                                         \n");
            _sbBuffer.Append("         {                                                                                    \n");
            _sbBuffer.Append("              var _exceptionState = MasterTable.getCellByColumnUniqueName(row, 'IdExecutionState').innerText;                         \n");
            _sbBuffer.Append("              if ((_exceptionState == 3) || (_exceptionState == 2)) //Closed o In Treatment                                        \n");
            _sbBuffer.Append("              {                                                                               \n");
            _sbBuffer.Append("                  item.set_visible(false);                                                    \n");
            _sbBuffer.Append("              }                                                                               \n");
            _sbBuffer.Append("         }                                                                                    \n");

            //Agregamos esto para los showChart y ShowSeries
            _sbBuffer.Append("         if (item.get_value() == 'rmiShowChart')                                         \n");
            _sbBuffer.Append("         {                                                                                    \n");
            _sbBuffer.Append("              var _transformation = selectedNode.get_value().toLowerCase().indexOf('idtransformation');                         \n");
            _sbBuffer.Append("              if (_transformation == -1)                                                     \n");
            _sbBuffer.Append("              {                                                                                    \n");
            _sbBuffer.Append("                  item.set_visible(false);                                                                                    \n");
            _sbBuffer.Append("              }                                                                                    \n");
            _sbBuffer.Append("              else                                                                            \n");
            _sbBuffer.Append("              {                                                                               \n");
            _sbBuffer.Append("                  item.set_visible(true);                                                     \n");
            _sbBuffer.Append("              }                                                                               \n");
            _sbBuffer.Append("         }                                                                                    \n");
            _sbBuffer.Append("         if (item.get_value() == 'rmiShowSeries')                                         \n");
            _sbBuffer.Append("         {                                                                                    \n");
            _sbBuffer.Append("              var _transformation = selectedNode.get_value().toLowerCase().indexOf('idtransformation');                         \n");
            _sbBuffer.Append("              if (_transformation == -1)                                                     \n");
            _sbBuffer.Append("              {                                                                                    \n");
            _sbBuffer.Append("                  item.set_visible(false);                                                                                    \n");
            _sbBuffer.Append("              }                                                                                    \n");
            _sbBuffer.Append("              else                                                                            \n");
            _sbBuffer.Append("              {                                                                               \n");
            _sbBuffer.Append("                  item.set_visible(true);                                                     \n");
            _sbBuffer.Append("              }                                                                               \n");
            _sbBuffer.Append("         }                                                                                    \n");
            _sbBuffer.Append("    }                                                                                         \n");
            _sbBuffer.Append("}                                                                                             \n");

            _sbBuffer.Append(CloseHtmlJavaScript());

            InjectJavascript("JS_onClientShowing", _sbBuffer.ToString());
        }
        protected void InjectShowContextMenuTreeView(String rmnSelectionClientID)
        {
            //Esta funcion es la encargada de mostrar el menu de opciones cuando se realiza click derecho sobre un registro en la grilla.
            //Parametros, <index> es el indice del registro donde esta parado, <e> es el evento
            System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

            _sbBuffer.Append(OpenHtmlJavaScript());

            _sbBuffer.Append("function ShowContextMenuTreeView(sender, args)                                                             \n");
            _sbBuffer.Append("{                                                                                                     \n");
            //Accede al menu
            _sbBuffer.Append("      var MyMenu = $find('" + rmnSelectionClientID + "');                                             \n");
            _sbBuffer.Append("      var evt = args.get_domEvent();                                                             \n");
            _sbBuffer.Append("      if(evt.target.tagName == 'INPUT' || evt.target.tagName == 'A')                                  \n");
            _sbBuffer.Append("      {                                                                                               \n");
            _sbBuffer.Append("          return;                                                                                     \n");
            _sbBuffer.Append("      }                                                                                               \n");
            //Obtiene el tree node y despues le da el selected al nodo.
            _sbBuffer.Append("      var treeNode = args.get_node();                                                                 \n");
            _sbBuffer.Append("      treeNode.set_selected(true);                                                                    \n");
            //Marca como seleccionado el row
            //Muestra el menu
            _sbBuffer.Append("      MyMenu.show(evt);                                                                                 \n");
            _sbBuffer.Append("      evt.cancelBubble = true;                                                                          \n");
            _sbBuffer.Append("      evt.returnValue = false;                                                                          \n");
            _sbBuffer.Append("      if (evt.stopPropagation)                                                                          \n");
            _sbBuffer.Append("      {                                                                                               \n");
            _sbBuffer.Append("          evt.stopPropagation();                                                                        \n");
            _sbBuffer.Append("          evt.preventDefault();                                                                         \n");
            _sbBuffer.Append("      }                                                                                               \n");
            _sbBuffer.Append("}                                                                                                     \n");

            _sbBuffer.Append(CloseHtmlJavaScript());

            InjectJavascript("JS_RowContextMenu", _sbBuffer.ToString());
        }
        protected void InjectClientSelectRow(String rgdMasterGridClientID)
        {
            //Esta funcion es la encargada de mostrar el menu de opciones cuando se selecciona el campo de seleccion en la grilla
            //Parametros    <e> event
            //          <idGridRow> el indice del row en donde esta parado
            //          <idGridTable> el id de la tabla donde esta parado(por si es jerarquica)
            System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

            _sbBuffer.Append(OpenHtmlJavaScript());

            _sbBuffer.Append("function ClientSelectRow" + rgdMasterGridClientID + "(e, idGridRow, idGridTable, result)                                               \n");
            _sbBuffer.Append("{                                                                                                         \n");
            _sbBuffer.Append("  var grid = $find('" + rgdMasterGridClientID + "');                                                      \n");
            //Selecciona el row de la grilla.
            _sbBuffer.Append("  var rowControl = grid.get_masterTableView().get_dataItems()[idGridRow].get_element();                   \n");
            _sbBuffer.Append("  grid.get_masterTableView().selectItem(rowControl, true);                                                \n");
            _sbBuffer.Append("  return result;                                                                                          \n");
            _sbBuffer.Append("}                                                                                                         \n");

            _sbBuffer.Append(CloseHtmlJavaScript());

            InjectJavascript("JS_ClientSelectRow" + rgdMasterGridClientID, _sbBuffer.ToString());
        }
        protected void InjectShowMenu(String rmnSelectionClientID, String rgdMasterGridClientID)
        {
            //Esta funcion es la encargada de mostrar el menu de opciones cuando se selecciona el campo de seleccion en la grilla
            //Parametros    <e> event
            //          <idGridRow> el indice del row en donde esta parado
            //          <idGridTable> el id de la tabla donde esta parado(por si es jerarquica)
            System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

            _sbBuffer.Append(OpenHtmlJavaScript());

            _sbBuffer.Append("function ShowMenu" + rgdMasterGridClientID + "(e, idGridRow, idGridTable)                                                          \n");
            _sbBuffer.Append("{                                                                                                      \n");
            _sbBuffer.Append("  var grid = $find('" + rgdMasterGridClientID + "');                              \n");
            //Selecciona el row de la grilla.
            _sbBuffer.Append("  var rowControl = grid.get_masterTableView().get_dataItems()[idGridRow].get_element();      \n");
            _sbBuffer.Append("  grid.get_masterTableView().selectItem(rowControl, true);                                  \n");
            //Obtiene el menu
            _sbBuffer.Append("  var menu = $find('" + rmnSelectionClientID + "');                                                            \n");
            _sbBuffer.Append("  if ( (!e.relatedTarget) || (!menu.IsChildOf(menu.DomElement, e.relatedTarget)) )                    \n");
            _sbBuffer.Append("  {                                                                                                   \n");
            //Muestra el menu.
            _sbBuffer.Append("      menu.show(e);                                                                                   \n");
            _sbBuffer.Append("  }                                                                                                   \n");
            _sbBuffer.Append("  e.cancelBubble = true;                                                                              \n");
            _sbBuffer.Append("  if (e.stopPropagation)                                                                              \n");
            _sbBuffer.Append("  {                                                                                                   \n");
            _sbBuffer.Append("      e.stopPropagation();                                                                            \n");
            _sbBuffer.Append("      e.preventDefault();                                                                             \n");
            _sbBuffer.Append("  }                                                                                                   \n");
            _sbBuffer.Append("}                                                                                                     \n");

            _sbBuffer.Append(CloseHtmlJavaScript());

            InjectJavascript("JS_ShowMenu", _sbBuffer.ToString());
        }
        protected void InjectRmnSelectionItemClickHandler(String entity, String treeID, Boolean isTreeView)
        {   //Esta funcion se ejecuta al hacer click sobre un item del menuRad
            System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

            _sbBuffer.Append(OpenHtmlJavaScript());

            _sbBuffer.Append("function rmnSelection" + entity + "_OnClientItemClickedHandler(sender, eventArgs)                                 \n");
            _sbBuffer.Append("{                                                                                                                 \n");
            if (isTreeView)
            {
                _sbBuffer.Append("         var treeView = $find('" + treeID + "');                                                              \n");
                _sbBuffer.Append("         var selectedNode = treeView.get_selectedNode();                                                      \n");
            }
            _sbBuffer.Append("  document.getElementById('radMenuClickedId" + entity + "').value = 'Selection';                                  \n");
            _sbBuffer.Append("  if (eventArgs.get_item().get_value() == 'rmiDelete')                                                            \n");
            _sbBuffer.Append("  {                                                                                                               \n");
            _sbBuffer.Append("      sender.hide();                                                                                              \n");
            _sbBuffer.Append("      var modalPopupBehavior = $find('programmaticModalPopupBehavior" + entity + "');                             \n");
            _sbBuffer.Append("      modalPopupBehavior.show();                                                                                  \n");
            _sbBuffer.Append("      eventArgs.set_cancel(true);                                                                                 \n");
            _sbBuffer.Append("  }                                                                                                               \n");
            _sbBuffer.Append("  else                                                                                                            \n");
            _sbBuffer.Append("  {                                                                                                               \n");
            //Agregamos esto para los showChart y ShowSeries
            _sbBuffer.Append("      if (eventArgs.get_item().get_value() == 'rmiShowChart')                                                     \n");
            _sbBuffer.Append("      {                                                                                                           \n");
            _sbBuffer.Append("          var getData = new QueryData(selectedNode.get_value().replace(' ', '').replace(' ', '').replace(' ', ''), true); \n");
            _sbBuffer.Append("          var _transformation = getData['IdTransformation'][0];                                                   \n");
            _sbBuffer.Append("          var _measurement = getData['IdMeasurement'][0];                                                   \n");
            _sbBuffer.Append("          sender.hide();                                                                                              \n");
            _sbBuffer.Append("          eventArgs.set_cancel(true);                                                                                 \n");
            _sbBuffer.Append("          return ShowTransformationChart(event, _measurement, _transformation);                                                                      \n");
            _sbBuffer.Append("      }                                                                                                           \n");
            //_sbBuffer.Append("      else                                                                                                        \n");
            //_sbBuffer.Append("      {                                                                                                           \n");
            //_sbBuffer.Append("          if (eventArgs.get_item().get_value() == 'rmiShowSeries')                                                \n");
            //_sbBuffer.Append("          {                                                                                                       \n");
            //_sbBuffer.Append("              var getData = new QueryData(selectedNode.get_value().replace(' ', '').replace(' ', '').replace(' ', ''), true); \n");
            //_sbBuffer.Append("              var _transformation = getData['IdTransformation'][0];                                               \n");
            //_sbBuffer.Append("              var _measurement = getData['IdMeasurement'][0];                                                   \n");
            //_sbBuffer.Append("              sender.hide();                                                                                              \n");
            //_sbBuffer.Append("              eventArgs.set_cancel(true);                                                                                 \n");
            ////_sbBuffer.Append("              return ShowTransformationSeries(event, _measurement, _transformation);                                                   \n");
            //_sbBuffer.Append("              return ShowSeries(event, _measurement);                                                   \n");
            //_sbBuffer.Append("          }                                                                                                       \n");
            //_sbBuffer.Append("          else                                                                                                    \n");
            //_sbBuffer.Append("          {                                                                                                       \n");
            //_sbBuffer.Append("              $get('FWMasterGlobalUpdateProgress').style.display = 'block';                                       \n");
            //_sbBuffer.Append("          }                                                                                                       \n");
            //_sbBuffer.Append("      }                                                                                                           \n");
            _sbBuffer.Append("  }                                                                                                               \n");
            _sbBuffer.Append("}                                                                                                                 \n");

            _sbBuffer.Append(CloseHtmlJavaScript());

            InjectJavascript("JS_RmnSelectionItemClickHandler", _sbBuffer.ToString());
        }
        /// <summary>
        /// Este metodo inyecta el JS para atrapar el Click en un Menu en Delete y mostrar el PopUp
        /// </summary>
        /// <param name="manage">Indica si el menu esta en un Manage o en Property</param>
        /// <param name="entity">Indica el nombre de la entidad, por si hay varios menu en una misma pagina</param>
        protected void InjectRmnOptionOnClientItemClicking(Boolean manage, String entity)
        {   //Esta funcion se ejecuta al hacer click sobre un item del menuRad
            //Para atrapar el Delete y mostrar el PopUp del Delete
            System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

            _sbBuffer.Append(OpenHtmlJavaScript());

            _sbBuffer.Append("function rmnOption" + entity + "_OnClientItemClicking(sender, eventArgs)                              \n");
            _sbBuffer.Append("{                                                                                                     \n");
            _sbBuffer.Append("      if (eventArgs.get_item().get_value() == 'rmiDelete')                                            \n");
            _sbBuffer.Append("      {                                                                                               \n");
            //_sbBuffer.Append("          StopEvent(eventArgs);   //window.event.returnValue = false;                                                           \n");
            //Como esta dentro de un manage, debe usar esta variable oculta para saber si borra muchos o uno.
            _sbBuffer.Append("          document.getElementById('radMenuClickedId" + entity + "').value = 'Option';                 \n");
            //Aca hace la verifica si hay al menos un registro chequeado, sino muestra una alerta.
            _sbBuffer.Append("          if ('False' == '" + manage + "')                                                            \n");
            _sbBuffer.Append("          {                                                                                           \n");
            _sbBuffer.Append("              var modalPopupBehavior = $find('programmaticModalPopupBehavior" + entity + "');         \n");
            _sbBuffer.Append("              modalPopupBehavior.show();                                                              \n");
            _sbBuffer.Append("              eventArgs.set_cancel(true);                                                             \n");
            _sbBuffer.Append("          }                                                                                           \n");
            _sbBuffer.Append("          else                                                                                        \n");
            _sbBuffer.Append("          {                                                                                           \n");
            _sbBuffer.Append("              if (ValidateItemChecked())                                                              \n");
            _sbBuffer.Append("              {                                                                                       \n");
            _sbBuffer.Append("                  var modalPopupBehavior = $find('programmaticModalPopupBehavior" + entity + "');     \n");
            _sbBuffer.Append("                  modalPopupBehavior.show();                                                          \n");
            _sbBuffer.Append("                  eventArgs.set_cancel(true);                                                         \n");
            _sbBuffer.Append("              }                                                                                       \n");
            _sbBuffer.Append("              else                                                                                    \n");
            _sbBuffer.Append("              {                                                                                       \n");
            _sbBuffer.Append("                  alert('" + Resources.Common.NoRecordSelectedToDelete + "');                         \n");
            _sbBuffer.Append("              }                                                                                       \n");
            _sbBuffer.Append("          }                                                                                           \n");
            _sbBuffer.Append("          StopEvent(eventArgs);   //window.event.returnValue = false;                                                           \n");
            _sbBuffer.Append("      }                                                                                               \n");
            _sbBuffer.Append("}                                                                                                     \n");

            _sbBuffer.Append(CloseHtmlJavaScript());

            InjectJavascript("JS_RmnOptionItemClickHandler", _sbBuffer.ToString());
            //FwMasterPage.JavaScriptManager.InjectJavascript("JS_RmnOptionItemClickHandler", _sbBuffer.ToString(), true);
        }
        protected void InjectContentToolbarBtnHandler(String rmnOptionClientID)
        {   //Esta funcion se ejecuta al hacer click sobre un item del menuRad
            System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

            _sbBuffer.Append(OpenHtmlJavaScript());

            _sbBuffer.Append("function ContentToolbarBtn" + rmnOptionClientID + "_OnMouseOver(toolbarBtn, e)                \n");
            _sbBuffer.Append("{                                                                               \n");
            _sbBuffer.Append("    var _menu = $get('" + rmnOptionClientID + "');                              \n");
            _sbBuffer.Append("    var _position = Sys.UI.DomElement.getBounds(toolbarBtn);                    \n");
            _sbBuffer.Append("                                                                                \n");
            _sbBuffer.Append("    _menu.style.display = 'block';                                              \n");
            _sbBuffer.Append("    var _menuWidth = Sys.UI.DomElement.getBounds(_menu).width;                  \n");
            _sbBuffer.Append("    _menu.style.left = (_position.x - _menuWidth + 24) + 'px';                  \n");
            _sbBuffer.Append("    _menu.style.top = _position.y + 'px';                                       \n");
            _sbBuffer.Append("                                                                                \n");
            _sbBuffer.Append("    StopEvent(e);     //window.event.returnValue = false;                                           \n");
            _sbBuffer.Append("}                                                                               \n");

            _sbBuffer.Append("var _CloseMenu" + rmnOptionClientID + " = false;                                                         \n");
            _sbBuffer.Append("var timeOutFuncId" + rmnOptionClientID + ";                                                              \n");

            _sbBuffer.Append("function OnMenuMouseOver" + rmnOptionClientID + "(sender, eventArgs) {                                   \n");
            _sbBuffer.Append("    _CloseMenu" + rmnOptionClientID + " = false;                                                         \n");
            _sbBuffer.Append("}                                                                               \n");

            _sbBuffer.Append("function OnClientMouseOutHandler" + rmnOptionClientID + "(sender, eventArgs) {                           \n");
            _sbBuffer.Append("    _CloseMenu" + rmnOptionClientID + " = true;                                                          \n");
            _sbBuffer.Append("    CloseMenu" + rmnOptionClientID + "();                                                                \n");
            _sbBuffer.Append("}                                                                               \n");

            _sbBuffer.Append("function CloseMenu" + rmnOptionClientID + "()                                                            \n");
            _sbBuffer.Append("{                                                                               \n");
            _sbBuffer.Append("    timeOutFuncId" + rmnOptionClientID + " = window.setTimeout(function() {                              \n");
            _sbBuffer.Append("      if (_CloseMenu" + rmnOptionClientID + " == true) {                                                 \n");
            _sbBuffer.Append("          var _menu = $get('" + rmnOptionClientID + "');                        \n");
            _sbBuffer.Append("          _menu.style.display = 'none';                                         \n");
            _sbBuffer.Append("          _menu.style.left = '-200px';                                          \n");
            _sbBuffer.Append("          _menu.style.top = '0px';                                              \n");
            _sbBuffer.Append("          window.clearTimeout(timeOutFuncId" + rmnOptionClientID + ");                                   \n");
            _sbBuffer.Append("          timeOutFuncId" + rmnOptionClientID + " = null;                                                 \n");
            _sbBuffer.Append("      }                                                                         \n");
            _sbBuffer.Append("    }, 100);                                                                   \n");
            _sbBuffer.Append("}                                                                               \n");

            _sbBuffer.Append(CloseHtmlJavaScript());

            //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "JS_RmnOptionItemClickHandler", _sbBuffer.ToString(), true);
            InjectJavascript("FW_ContentToolbarBtn_JS" + rmnOptionClientID, _sbBuffer.ToString());
        }
        protected void InjectOnClientClickTabGraphicMode()
        {
            //Esta funcion se ejecuta al hacer click sobre un item del menuRad
            System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

            _sbBuffer.Append(OpenHtmlJavaScript());

            //******************************************************
            //Este metodo vuelve a habilitar los validator de cada pageView.
            _sbBuffer.Append("function OnClientTabSelectedGraphicMode(sender, args)                                                     \n");
            _sbBuffer.Append("{                                                                                                         \n");
            _sbBuffer.Append("      //Si hacen click sobre el tab de mapa, Ejecuta la carga del mapa.                                   \n");
            _sbBuffer.Append("      if (args.get_tab().get_index() == 0)                                                                \n");
            _sbBuffer.Append("      {                                                                                                   \n");
            _sbBuffer.Append("          GUnload();                                                                                       \n");
            _sbBuffer.Append("          GetMap();                                                                                       \n");
            _sbBuffer.Append("      }                                                                                                   \n");
            //_sbBuffer.Append("      return false;                                                                                       \n");
            _sbBuffer.Append("}                                                                                                         \n");

            _sbBuffer.Append(CloseHtmlJavaScript());

            InjectJavascript("JS_OnClientTabSelectedGraphicMode", _sbBuffer.ToString());
        }
        protected void InjectShowFile(String gridClientId)
        {
            System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

            _sbBuffer.Append("function ShowFile(e, resourceType, urlName, idResource, idResourceFile)                                                          \n");
            _sbBuffer.Append("{                                                                                                     \n");
            //Abre una nueva ventana con el archivo.
            _sbBuffer.Append("      var _IEXPLORER = 'Microsoft Internet Explorer';                                             \n");
            _sbBuffer.Append("      var _FIREFOX = 'Netscape';                                                                  \n");
            _sbBuffer.Append("      var _BrowserName = navigator.appName;                                                       \n");
            //_sbBuffer.Append("      StopEvent(e);                                                                                   \n");
            
            _sbBuffer.Append("      if (resourceType.indexOf('DOC') >= 0)                                                                          \n");
            _sbBuffer.Append("      {                                                                                                     \n");
            _sbBuffer.Append("          if (_BrowserName == _IEXPLORER)                                                             \n");
            _sbBuffer.Append("          {   //IE and Opera                                                                          \n");
            _sbBuffer.Append("              var newWindow = window.open('http://" + Request.ServerVariables["HTTP_HOST"] + "/ManagementTools/KnowledgeCollaboration/FilesViewer.aspx?IdResource=' + idResource + '&IdResourceFile=' + idResourceFile, 'Reports', 'toolbar=yes,location=yes,resizable=yes,status=yes,menubar=yes,scrollbars=yes');      \n");
            _sbBuffer.Append("          }                                                                                           \n");
            _sbBuffer.Append("          else                                                                                        \n");
            _sbBuffer.Append("          {   //FireFox                                                                               \n");
            _sbBuffer.Append("              var newWindow = window.parent.open('http://" + Request.ServerVariables["HTTP_HOST"] + "/ManagementTools/KnowledgeCollaboration/FilesViewer.aspx?IdResource=' + idResource + '&IdResourceFile=' + idResourceFile, 'Reports', 'toolbar=yes,location=yes,resizable=yes,status=yes,menubar=yes,scrollbars=yes');      \n");
            _sbBuffer.Append("          }                                                                                           \n");
            //_sbBuffer.Append("          var newWindow = window.open('http://" + Request.ServerVariables["HTTP_HOST"] + "/ManagementTools/KnowledgeCollaboration/FilesViewer.aspx?IdResource=' + idResource + '&IdResourceFile=' + idResourceFile, 'Reports', 'toolbar=yes,location=yes,resizable=yes,status=yes,menubar=yes,scrollbars=yes');     \n");
            //_sbBuffer.Append("          newWindow.focus();                                                                                \n");
            ////frena la ejecucion de todo y queda esperando a la confirmacion en el popup.
            //_sbBuffer.Append("          StopEvent(e);     //window.event.returnValue = false;                                                                   \n");
            _sbBuffer.Append("      }                                                                                                       \n");
            _sbBuffer.Append("      else                                                                                                             \n");
            _sbBuffer.Append("      {                                                                                                     \n");
            _sbBuffer.Append("          if (_BrowserName == _IEXPLORER)                                                             \n");
            _sbBuffer.Append("          {   //IE and Opera                                                                          \n");
            _sbBuffer.Append("              var newWindow = window.open('http://' + urlName, 'Reports', 'toolbar=yes,location=yes,resizable=yes,status=yes,menubar=yes,scrollbars=yes');      \n");
            _sbBuffer.Append("          }                                                                                           \n");
            _sbBuffer.Append("          else                                                                                        \n");
            _sbBuffer.Append("          {   //FireFox                                                                               \n");
            _sbBuffer.Append("              var newWindow = window.parent.open('http://' + urlName, 'Reports', 'toolbar=yes,location=yes,resizable=yes,status=yes,menubar=yes,scrollbars=yes');      \n");
            _sbBuffer.Append("          }                                                                                           \n");
            //_sbBuffer.Append("          var newWindow = window.open('http://' + urlName, 'Reports', 'toolbar=yes,location=yes,resizable=yes,status=yes,menubar=yes,scrollbars=yes');     \n");
            //_sbBuffer.Append("          newWindow.focus();                                                                                \n");
            ////frena la ejecucion de todo y queda esperando a la confirmacion en el popup.
            //_sbBuffer.Append("          StopEvent(e);     //window.event.returnValue = false;                                                                   \n");
            _sbBuffer.Append("      }                                                                                                       \n");
            _sbBuffer.Append("      newWindow.focus();                                                                                \n");
            //frena la ejecucion de todo y queda esperando a la confirmacion en el popup.
            _sbBuffer.Append("      StopEvent(e);     //window.event.returnValue = false;                                                                   \n");
            _sbBuffer.Append("}                                                                                                             \n");

            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(UpdatePanel), "JSShowFile", _sbBuffer.ToString(), true);
        }
        protected void InjectShowFileAttach(String gridClientId)
        {
            System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

            _sbBuffer.Append("function ShowFileAttach(e, idProcess, idTask, idExecution)                                                          \n");
            _sbBuffer.Append("{                                                                                                     \n");
            //Abre una nueva ventana con el archivo.
            _sbBuffer.Append("      var _IEXPLORER = 'Microsoft Internet Explorer';                                             \n");
            _sbBuffer.Append("      var _FIREFOX = 'Netscape';                                                                  \n");
            _sbBuffer.Append("      var _BrowserName = navigator.appName;                                                       \n");
            //_sbBuffer.Append("      StopEvent(e);                                                                                   \n");

            _sbBuffer.Append("      if (_BrowserName == _IEXPLORER)                                                             \n");
            _sbBuffer.Append("      {   //IE and Opera                                                                          \n");
            _sbBuffer.Append("          var newWindow = window.open('http://" + Request.ServerVariables["HTTP_HOST"] + "/AdministrationTools/ProcessesFramework/ShowFileAttach.aspx?IdProcess=' + idProcess + '&IdTask=' + idTask + '&IdExecution=' + idExecution, 'Reports', 'toolbar=yes,location=yes,resizable=yes,status=yes,menubar=yes,scrollbars=yes');      \n");
            _sbBuffer.Append("      }                                                                                           \n");
            _sbBuffer.Append("      else                                                                                        \n");
            _sbBuffer.Append("      {   //FireFox                                                                               \n");
            _sbBuffer.Append("          var newWindow = window.parent.open('http://" + Request.ServerVariables["HTTP_HOST"] + "/AdministrationTools/ProcessesFramework/ShowFileAttach.aspx?IdProcess=' + idProcess + '&IdTask=' + idTask + '&IdExecution=' + idExecution, 'Reports', 'toolbar=yes,location=yes,resizable=yes,status=yes,menubar=yes,scrollbars=yes');      \n");
            _sbBuffer.Append("      }                                                                                           \n");
            _sbBuffer.Append("      newWindow.focus();                                                                                \n");
            //frena la ejecucion de todo y queda esperando a la confirmacion en el popup.
            _sbBuffer.Append("      StopEvent(e);     //window.event.returnValue = false;                                                                   \n");
            _sbBuffer.Append("}                                                                                                             \n");

            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(UpdatePanel), "JSShowFileAttach", _sbBuffer.ToString(), true);
        }

        protected void InjectShowSeries()
        {
            System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();
            //Esta funcion es la encargada de hacer el show de las series de datos
            _sbBuffer.Append("function ShowSeries(sender, e) {                                                                       \n");
            _sbBuffer.Append("  var _strRequestParam;                                                                                         \n");
            _sbBuffer.Append("  _strRequestParam = 'PkCompost=' + sender.attributes['PkCompost'].value; //El PK, viene separado en pipe |,  para que no falle el tranfer a esta pagina.\n");
            _sbBuffer.Append("  _strRequestParam += '&EntityName=' + sender.attributes['EntityName'].value                                    \n");
            _sbBuffer.Append("  _strRequestParam += '&EntityNameGrid=' + sender.attributes['EntityNameGrid'].value;                           \n");
            _sbBuffer.Append("  _strRequestParam += '&EntityNameContextInfo=' + sender.attributes['EntityNameContextInfo'].value;             \n");
            _sbBuffer.Append("  _strRequestParam += '&EntityNameContextElement=' + sender.attributes['EntityNameContextElement'].value;       \n");
            _sbBuffer.Append("  _strRequestParam += '&Text=' + sender.attributes['Text'].value;                                               \n");
            _sbBuffer.Append("  _strRequestParam += '&';                                                                                      \n");

            _sbBuffer.Append("  //Pagina de transicion entre el link del tooltip del mapa y el Viewer.                                        \n");
            _sbBuffer.Append("  window.open('../Managers/IndicatorSeriesNavigate.aspx?' + _strRequestParam, '_parent');          \n");

            _sbBuffer.Append("  StopEvent(e);                                                                                                 \n");
            _sbBuffer.Append("}                                                                                                             \n");


            //_sbBuffer.Append("function ShowSeries(e, idMeasurement)                                                                         \n");
            //_sbBuffer.Append("{                                                                                                             \n");
            //_sbBuffer.Append("  var _IEXPLORER = 'Microsoft Internet Explorer';                                             \n");
            //_sbBuffer.Append("  var _FIREFOX = 'Netscape';                                                                  \n");
            //_sbBuffer.Append("  var _BrowserName = navigator.appName;                                                       \n");

            //_sbBuffer.Append("  StopEvent(e);                                                                                   \n");
            //_sbBuffer.Append("  if (_BrowserName == _IEXPLORER)                                                             \n");
            //_sbBuffer.Append("  {   //IE and Opera                                                                          \n");
            //_sbBuffer.Append("      var newWindow = window.open('http://" + Request.ServerVariables["HTTP_HOST"] + "/ManagementTools/PerformanceAssessment/IndicatorSeries.aspx?idMeasurement=' + idMeasurement, 'IndicatorSeries', 'toolbar=yes,location=yes,resizable=yes,status=yes,menubar=yes,scrollbars=yes');      \n");
            //_sbBuffer.Append("  }                                                                                           \n");
            //_sbBuffer.Append("  else                                                                                        \n");
            //_sbBuffer.Append("  {   //FireFox                                                                               \n");
            //_sbBuffer.Append("      var newWindow = window.parent.open('http://" + Request.ServerVariables["HTTP_HOST"] + "/ManagementTools/PerformanceAssessment/IndicatorSeries.aspx?idMeasurement=' + idMeasurement, 'IndicatorSeries', 'toolbar=yes,location=yes,resizable=yes,status=yes,menubar=yes,scrollbars=yes');      \n");
            //_sbBuffer.Append("  }                                                                                           \n");

            //_sbBuffer.Append("  //Abre una nueva ventana con el reporte.                                                                    \n");
            ////_sbBuffer.Append("  var newWindow = window.open('http://" + Request.ServerVariables["HTTP_HOST"] + "/ManagementTools/PerformanceAssessment/IndicatorSeries.aspx?idMeasurement=' + idMeasurement, 'IndicatorSeries', 'toolbar=yes,location=yes,resizable=yes,status=yes,menubar=yes,scrollbars=yes');      \n");
            //_sbBuffer.Append("  newWindow.focus();                                                                                          \n");
            //_sbBuffer.Append("  //frena la ejecucion de todo y queda esperando a la confirmacion en el popup.                               \n");
            //_sbBuffer.Append("  StopEvent(e);     //window.event.returnValue = false;                                                                           \n");
            //_sbBuffer.Append("}                                                                                                             \n");

            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(UpdatePanel), "JSShowSeries", _sbBuffer.ToString(), true);

        }
        protected void InjectShowSeriesOLD()
        {
            System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();
            //Esta funcion es la encargada de hacer el show de las series de datos
            _sbBuffer.Append("function ShowSeries(e, idMeasurement)                                                                         \n");
            _sbBuffer.Append("{                                                                                                             \n");
            _sbBuffer.Append("  var _IEXPLORER = 'Microsoft Internet Explorer';                                             \n");
            _sbBuffer.Append("  var _FIREFOX = 'Netscape';                                                                  \n");
            _sbBuffer.Append("  var _BrowserName = navigator.appName;                                                       \n");

            //_sbBuffer.Append("  StopEvent(e);                                                                                   \n");
            _sbBuffer.Append("  if (_BrowserName == _IEXPLORER)                                                             \n");
            _sbBuffer.Append("  {   //IE and Opera                                                                          \n");
            _sbBuffer.Append("      var newWindow = window.open('http://" + Request.ServerVariables["HTTP_HOST"] + "/ManagementTools/PerformanceAssessment/IndicatorSeries.aspx?idMeasurement=' + idMeasurement, 'IndicatorSeries', 'toolbar=yes,location=yes,resizable=yes,status=yes,menubar=yes,scrollbars=yes');      \n");
            _sbBuffer.Append("  }                                                                                           \n");
            _sbBuffer.Append("  else                                                                                        \n");
            _sbBuffer.Append("  {   //FireFox                                                                               \n");
            _sbBuffer.Append("      var newWindow = window.parent.open('http://" + Request.ServerVariables["HTTP_HOST"] + "/ManagementTools/PerformanceAssessment/IndicatorSeries.aspx?idMeasurement=' + idMeasurement, 'IndicatorSeries', 'toolbar=yes,location=yes,resizable=yes,status=yes,menubar=yes,scrollbars=yes');      \n");
            _sbBuffer.Append("  }                                                                                           \n");

            _sbBuffer.Append("  //Abre una nueva ventana con el reporte.                                                                    \n");
            //_sbBuffer.Append("  var newWindow = window.open('http://" + Request.ServerVariables["HTTP_HOST"] + "/ManagementTools/PerformanceAssessment/IndicatorSeries.aspx?idMeasurement=' + idMeasurement, 'IndicatorSeries', 'toolbar=yes,location=yes,resizable=yes,status=yes,menubar=yes,scrollbars=yes');      \n");
            _sbBuffer.Append("  newWindow.focus();                                                                                          \n");
            _sbBuffer.Append("  //frena la ejecucion de todo y queda esperando a la confirmacion en el popup.                               \n");
            _sbBuffer.Append("  StopEvent(e);     //window.event.returnValue = false;                                                                           \n");
            _sbBuffer.Append("}                                                                                                             \n");

            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(UpdatePanel), "JSShowSeries", _sbBuffer.ToString(), true);

        }
        //protected void InjectShowTransformationSeries()
        //{
        //    System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();
        //    //Esta funcion es la encargada de hacer el show de las series de datos
        //    _sbBuffer.Append("function ShowTransformationSeries(e, idMeasurement, idTransformation)                                                                         \n");
        //    _sbBuffer.Append("{                                                                                                             \n");
        //    _sbBuffer.Append("  var _IEXPLORER = 'Microsoft Internet Explorer';                                             \n");
        //    _sbBuffer.Append("  var _FIREFOX = 'Netscape';                                                                  \n");
        //    _sbBuffer.Append("  var _BrowserName = navigator.appName;                                                       \n");
        //    _sbBuffer.Append("  //frena la ejecucion de todo y queda esperando a la confirmacion en el popup.                               \n");
        //    _sbBuffer.Append("  StopEvent(e);                                                                                   \n");
        //    _sbBuffer.Append("  //Abre una nueva ventana con el reporte.                                                                    \n");
        //    _sbBuffer.Append("  if (_BrowserName == _IEXPLORER)                                                             \n");
        //    _sbBuffer.Append("  {   //IE and Opera                                                                          \n");
        //    _sbBuffer.Append("      var newWindow = window.open('http://" + Request.ServerVariables["HTTP_HOST"] + "/ManagementTools/PerformanceAssessment/TransformationSeries.aspx?idTransformation=' + idTransformation + '&idMeasurement=' + idMeasurement, 'TransformationSeries', 'toolbar=yes,location=yes,resizable=yes,status=yes,menubar=yes,scrollbars=yes');      \n");
        //    _sbBuffer.Append("  }                                                                                           \n");
        //    _sbBuffer.Append("  else                                                                                        \n");
        //    _sbBuffer.Append("  {   //FireFox                                                                               \n");
        //    _sbBuffer.Append("      var newWindow = window.parent.open('http://" + Request.ServerVariables["HTTP_HOST"] + "/ManagementTools/PerformanceAssessment/TransformationSeries.aspx?idTransformation=' + idTransformation + '&idMeasurement=' + idMeasurement, 'TransformationSeries', 'toolbar=yes,location=yes,resizable=yes,status=yes,menubar=yes,scrollbars=yes');      \n");
        //    _sbBuffer.Append("  }                                                                                           \n");
        //    _sbBuffer.Append("  newWindow.focus();                                                                                          \n");
        //    _sbBuffer.Append("  //frena la ejecucion de todo y queda esperando a la confirmacion en el popup.                               \n");
        //    _sbBuffer.Append("  StopEvent(e);     //window.event.returnValue = false;                                                                           \n");
        //    _sbBuffer.Append("}                                                                                                             \n");

        //    ScriptManager.RegisterClientScriptBlock(this.Page, typeof(UpdatePanel), "JSShowTransformationSeries", _sbBuffer.ToString(), true);

        //}
        protected void InjectShowTransformationChart()
        {
            System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();
            //Esta funcion es la encargada de hacer el show de las series de datos
            _sbBuffer.Append("function ShowTransformationChart(e, idMeasurement, idTransformation)                                                                         \n");
            _sbBuffer.Append("{                                                                                                             \n");
            _sbBuffer.Append("  var _IEXPLORER = 'Microsoft Internet Explorer';                                             \n");
            _sbBuffer.Append("  var _FIREFOX = 'Netscape';                                                                  \n");
            _sbBuffer.Append("  var _BrowserName = navigator.appName;                                                       \n");
            _sbBuffer.Append("  //frena la ejecucion de todo y queda esperando a la confirmacion en el popup.                               \n");
            //_sbBuffer.Append("  StopEvent(e);                                                                                   \n");
            _sbBuffer.Append("  //Abre una nueva ventana con el reporte.                                                                    \n");
            _sbBuffer.Append("  if (_BrowserName == _IEXPLORER)                                                             \n");
            _sbBuffer.Append("  {   //IE and Opera                                                                          \n");
            _sbBuffer.Append("      var newWindow = window.open('http://" + Request.ServerVariables["HTTP_HOST"] + "/ManagementTools/PerformanceAssessment/TransformationChart.aspx?idTransformation=' + idTransformation + '&idMeasurement=' + idMeasurement, 'TransformationChart', 'toolbar=yes,location=yes,resizable=yes,status=yes,menubar=yes,scrollbars=yes');      \n");
            _sbBuffer.Append("  }                                                                                           \n");
            _sbBuffer.Append("  else                                                                                        \n");
            _sbBuffer.Append("  {   //FireFox                                                                               \n");
            _sbBuffer.Append("      var newWindow = window.parent.open('http://" + Request.ServerVariables["HTTP_HOST"] + "/ManagementTools/PerformanceAssessment/TransformationChart.aspx?idTransformation=' + idTransformation + '&idMeasurement=' + idMeasurement, 'TransformationChart', 'toolbar=yes,location=yes,resizable=yes,status=yes,menubar=yes,scrollbars=yes');      \n");
            _sbBuffer.Append("  }                                                                                           \n");
            _sbBuffer.Append("  newWindow.focus();                                                                                          \n");
            _sbBuffer.Append("  //frena la ejecucion de todo y queda esperando a la confirmacion en el popup.                               \n");
            _sbBuffer.Append("  StopEvent(e);     //window.event.returnValue = false;                                                                           \n");
            _sbBuffer.Append("}                                                                                                             \n");

            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(UpdatePanel), "JSShowTransformationChart", _sbBuffer.ToString(), true);

        }

        protected void InjectShowChart()
        {
            System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();
            //Esta funcion es la encargada de hacer el show de las series de datos
            _sbBuffer.Append("function ShowChart(e, idMeasurement)                                                                          \n");
            _sbBuffer.Append("{                                                                                                             \n");
            _sbBuffer.Append("  var _IEXPLORER = 'Microsoft Internet Explorer';                                             \n");
            _sbBuffer.Append("  var _FIREFOX = 'Netscape';                                                                  \n");
            _sbBuffer.Append("  var _BrowserName = navigator.appName;                                                       \n");

            //_sbBuffer.Append("  StopEvent(e);                                                                                   \n");
            _sbBuffer.Append("  if (_BrowserName == _IEXPLORER)                                                             \n");
            _sbBuffer.Append("  {   //IE and Opera                                                                          \n");
            _sbBuffer.Append("      var newWindow = window.open('http://" + Request.ServerVariables["HTTP_HOST"] + "/ManagementTools/PerformanceAssessment/MeasurementChart.aspx?idMeasurement=' + idMeasurement, 'IndicatorSeries', 'toolbar=yes,location=yes,resizable=yes,status=yes,menubar=yes,scrollbars=yes');      \n");
            _sbBuffer.Append("  }                                                                                           \n");
            _sbBuffer.Append("  else                                                                                        \n");
            _sbBuffer.Append("  {   //FireFox                                                                               \n");
            _sbBuffer.Append("      var newWindow = window.parent.open('http://" + Request.ServerVariables["HTTP_HOST"] + "/ManagementTools/PerformanceAssessment/MeasurementChart.aspx?idMeasurement=' + idMeasurement, 'IndicatorSeries', 'toolbar=yes,location=yes,resizable=yes,status=yes,menubar=yes,scrollbars=yes');      \n");
            _sbBuffer.Append("  }                                                                                           \n");
            //_sbBuffer.Append("  //Abre una nueva ventana con el reporte.                                                                    \n");
            //_sbBuffer.Append("  var newWindow = window.open('http://" + Request.ServerVariables["HTTP_HOST"] + "/ManagementTools/PerformanceAssessment/MeasurementChart.aspx?idMeasurement=' + idMeasurement, 'IndicatorSeries', 'toolbar=yes,location=yes,resizable=yes,status=yes,menubar=yes,scrollbars=yes');      \n");
            _sbBuffer.Append("  newWindow.focus();                                                                                          \n");
            _sbBuffer.Append("  //frena la ejecucion de todo y queda esperando a la confirmacion en el popup.                               \n");
            _sbBuffer.Append("  StopEvent(e);     //window.event.returnValue = false;                                                                           \n");
            _sbBuffer.Append("}                                                                                                             \n");

            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(UpdatePanel), "JSShowChart", _sbBuffer.ToString(), true);

        }
        protected void InjectPostBack()
        {
            System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

            _sbBuffer.Append(OpenHtmlJavaScript());
            //Inicializacion de Variables PageRequestManager
            //_sbBuffer.Append("window.onload = InitializePostBackAjax;                                                               \n");

            //_sbBuffer.Append("window.attachEvent('onload', InitializePostBackAjax);                                         \n");
            _sbBuffer.Append("  var prm;                                                                                      \n");
            _sbBuffer.Append("  var postBackElement;                                                                          \n");
            _sbBuffer.Append("  var _IEXPLORER = 'Microsoft Internet Explorer';                                             \n");
            _sbBuffer.Append("  var _FIREFOX = 'Netscape';                                                                  \n");
            _sbBuffer.Append("  var _BrowserName = navigator.appName;                                                       \n");

            _sbBuffer.Append("  if (_BrowserName == _IEXPLORER)                                                             \n");
            _sbBuffer.Append("  {   //IE and Opera                                                                          \n");
            _sbBuffer.Append("      window.attachEvent('onload', InitializePostBackAjax);                                \n");
            _sbBuffer.Append("  }                                                                                           \n");
            _sbBuffer.Append("  else                                                                                        \n");
            _sbBuffer.Append("  {   //FireFox                                                                               \n");
            _sbBuffer.Append("      document.addEventListener('DOMContentLoaded', InitializePostBackAjax, false);        \n");
            _sbBuffer.Append("  }                                                                                           \n");

            _sbBuffer.Append("function InitializePostBackAjax()                                                             \n");
            _sbBuffer.Append("{                                                                                             \n");
            _sbBuffer.Append("  prm = Sys.WebForms.PageRequestManager.getInstance();                                        \n");
            _sbBuffer.Append("  prm.add_initializeRequest(InitializeRequest);                                               \n");
            _sbBuffer.Append("}                                                                                             \n");
            //Handlers          
            _sbBuffer.Append("function InitializeRequest(sender, args)                                                      \n");
            _sbBuffer.Append("{                                                                                             \n");
            _sbBuffer.Append("  postBackElement = args.get_postBackElement();                                               \n");
            //Dispara el OnClick del btnHidden emulando un post back normal por sobre el async que dispara el Boton de la Grilla
            _sbBuffer.Append("  DoNormalPostBack();                                                                         \n");
            _sbBuffer.Append("}                                                                                             \n");
            //El Handler que Cancela el PostBack "Asyncronic" del UpdatePanel, termina haciendo un PostBack Normal
            _sbBuffer.Append("function DoNormalPostBack()                                                                   \n");
            _sbBuffer.Append("{                                                                                             \n");
            _sbBuffer.Append("  document.getElementById('FWMasterGlobalUpdateProgress').style.display = 'block';            \n");
            _sbBuffer.Append("}                                                                                             \n");

            _sbBuffer.Append(CloseHtmlJavaScript());

            InjectJavascript("JS_PostBack", _sbBuffer.ToString());
        }
        protected void InjectShowReportToPrintProjectBuyerSummary()
        {
            System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

            _sbBuffer.Append(OpenHtmlJavaScript());

            _sbBuffer.Append("function ShowReportToPrintProjectBuyerSummary(e, idProject)                                                                                        \n");
            _sbBuffer.Append("{                                                                                                                                   \n");
            //Abre una nueva ventana con el reporte.
            _sbBuffer.Append("  var _IEXPLORER = 'Microsoft Internet Explorer';                                             \n");
            _sbBuffer.Append("  var _FIREFOX = 'Netscape';                                                                  \n");
            _sbBuffer.Append("  var _BrowserName = navigator.appName;                                                       \n");

            //_sbBuffer.Append("  StopEvent(e);                                                                                   \n");
            _sbBuffer.Append("  if (_BrowserName == _IEXPLORER)                                                             \n");
            _sbBuffer.Append("  {   //IE and Opera                                                                          \n");
            _sbBuffer.Append("      var newWindow = window.open(" + Convert.ToChar(34) + "http://" +Request.ServerVariables["HTTP_HOST"].ToString() + (!string.IsNullOrEmpty(Request.ApplicationPath) ? Request.ApplicationPath : "") +  "/Dashboard/ProyectBuyerSummary.aspx?IdProcess=" + Convert.ToChar(34) + " + idProject, 'BuyerReports', 'toolbar=yes,location=yes,resizable=yes,status=yes,menubar=yes,scrollbars=yes');      \n");
            _sbBuffer.Append("  }                                                                                           \n");
            _sbBuffer.Append("  else                                                                                        \n");
            _sbBuffer.Append("  {   //FireFox                                                                               \n");
            _sbBuffer.Append("      var newWindow = window.parent.open(" + Convert.ToChar(34) + "http://" +Request.ServerVariables["HTTP_HOST"].ToString() + (!string.IsNullOrEmpty(Request.ApplicationPath) ? Request.ApplicationPath : "") +  "/Dashboard/ProyectBuyerSummary.aspx?IdProcess=" + Convert.ToChar(34) + " + idProject, 'BuyerReports', 'toolbar=yes,location=yes,resizable=yes,status=yes,menubar=yes,scrollbars=yes');      \n");
            _sbBuffer.Append("  }                                                                                           \n");
            //_sbBuffer.Append("  var newWindow = window.open(" + Convert.ToChar(34) + "http://" +Request.ServerVariables["HTTP_HOST"].ToString() + (!string.IsNullOrEmpty(Request.ApplicationPath) ? Request.ApplicationPath : "") +  "/Dashboard/ProyectBuyerSummary.aspx?IdProcess=" + Convert.ToChar(34) + " + idProject, 'BuyerReports', 'toolbar=yes,location=yes,resizable=yes,status=yes,menubar=yes,scrollbars=yes');                     \n");
            _sbBuffer.Append("  newWindow.focus();                                                                                                                  \n");
            //frena la ejecucion de todo y queda esperando a la confirmacion en el popup.
            _sbBuffer.Append("  StopEvent(e);     //window.event.returnValue = false;                                                                                               \n");
            _sbBuffer.Append("}                                                                                                                                     \n");

            _sbBuffer.Append(CloseHtmlJavaScript());

            InjectJavascript("JS_ShowReportToPrint", _sbBuffer.ToString());
        }
        protected void InjectOpenWindowDialogPickUpCoords(String inputPoints, String inputDrawModeType, String pnlCoords, String queryString)
        {
            System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

            _sbBuffer.Append(OpenHtmlJavaScript());

            _sbBuffer.Append("function openWin() {                                                                              \n");
            //Si viene vacio, no pasa parametros!!!
            if (String.IsNullOrEmpty(queryString))
            {
                _sbBuffer.Append("  var oWnd = radopen('DialogPickUpCoords.aspx', 'RadWindow1');                                    \n");
            }
            else
            {
                _sbBuffer.Append("  var oWnd = radopen('DialogPickUpCoords.aspx?ObjectQS=" + queryString + "', 'RadWindow1');                                    \n");
            }
            _sbBuffer.Append("  oWnd.setSize(780, 600);                                                                         \n");
            _sbBuffer.Append("  oWnd.Center();                                                                                  \n");
            _sbBuffer.Append("  oWnd.argument;                                                                                  \n");
            _sbBuffer.Append("  //set a function to be called when RadWindow is closed                                          \n");
            _sbBuffer.Append("  oWnd.add_close(OnClientClose);                                                                  \n");
            _sbBuffer.Append("}                                                                                                 \n");
            _sbBuffer.Append("function OnClientClose(oWnd) {                                                                    \n");
            _sbBuffer.Append("  //get the transferred arguments                                                                 \n");
            _sbBuffer.Append("  //Si hay cambios, hago algo...sino queda todo como esta.                                        \n");
            _sbBuffer.Append("  if (oWnd.argument != null) {                                                                    \n");
            _sbBuffer.Append("      if (oWnd.argument.NoChange != true) {                                                       \n");
            _sbBuffer.Append("          var _pnlCoords = document.getElementById('" + pnlCoords + "');                          \n");
            _sbBuffer.Append("          var _points = oWnd.argument.inputCoords;                                                \n");
            _sbBuffer.Append("          var _drawMode = oWnd.argument.drawModeType;                                             \n");
            _sbBuffer.Append("          document.getElementById('" + inputPoints + "').value = _points;                         \n");
            _sbBuffer.Append("          document.getElementById('" + inputDrawModeType + "').value = _drawMode;                 \n");
            _sbBuffer.Append("          //Si no hay nada seleccionado, pone el mensaje de vacio, sino muestra las coords.       \n");
            _sbBuffer.Append("          if (_points!='') {                                                                      \n");
            _sbBuffer.Append("              _pnlCoords.innerHTML = '" + Resources.ConstantMessage.SelectedCoords + " <br />' + _points;   \n");
            _sbBuffer.Append("          }                                                                                       \n");
            _sbBuffer.Append("          else {                                                                                  \n");
            _sbBuffer.Append("              _pnlCoords.innerHTML = '" + Resources.ConstantMessage.GeoCodeNotSelected + "';       \n");
            _sbBuffer.Append("          }                                                                                       \n");
            _sbBuffer.Append("      }                                                                                           \n");
            _sbBuffer.Append("  }                                                                                               \n");
            _sbBuffer.Append("}                                                                                                 \n");

            _sbBuffer.Append(CloseHtmlJavaScript());

            InjectJavascript("JS_OpenWindowPickUpCoords", _sbBuffer.ToString());
        }
        protected void InjectCheckContainVariableBaseinFormula()
        {   //Esta funcion se ejecuta al hacer click sobre un item del menuRad
            System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

            _sbBuffer.Append(OpenHtmlJavaScript());

            _sbBuffer.Append("function CheckContainVariableBaseinFormula(source, args)      \n");
            _sbBuffer.Append("{                                                             \n");
            _sbBuffer.Append("  //Si existe esta palabra esta ok, sino es invalida.         \n");
            _sbBuffer.Append("  if (args.Value.toLowerCase().indexOf('base') > -1)          \n");
            _sbBuffer.Append("  {                                                           \n");
            _sbBuffer.Append("      args.IsValid = true;                                    \n");
            _sbBuffer.Append("      return;                                                 \n");
            _sbBuffer.Append("  }                                                           \n");
            _sbBuffer.Append("   args.IsValid = false;                                      \n");
            _sbBuffer.Append("}                                                             \n");

            _sbBuffer.Append(CloseHtmlJavaScript());

            InjectJavascript("JS_CheckContainVariableBaseinFormula", _sbBuffer.ToString());
        }

        protected void InjectGoogleMapRegisterKey()
        {
            System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

            String _domain = Request.ServerVariables["HTTP_HOST"].ToString();
            String _googleMapKey = "ABQIAAAANQLnMX4Nim90_s2JOpDbCRTkfaMaODalt1Pv_z__s-8wEzonLRTmbU9_eRUNS7JmELwwOd9JFY95IQ";

            if (_domain.ToLower().Contains("ghree.com.ar"))
            {
                _googleMapKey = "ABQIAAAANQLnMX4Nim90_s2JOpDbCRTNroapOrRmTBWmhvkEqdzPFzKOsxS_EwOiZxMfX9kJOaHeWuZleHAIYA";
            }
            if (_domain.ToLower().Contains("ghree.com"))
            {
                _googleMapKey = "ABQIAAAANQLnMX4Nim90_s2JOpDbCRQBgjl3LfI56EyQmRCcJwkpeEZucBTY8wmK3Xd1fN6_U2VbLxKBc77IkA";
            }
            if (_domain.ToLower().Contains("ghree.info"))
            {
                _googleMapKey = "ABQIAAAANQLnMX4Nim90_s2JOpDbCRSG3BrgE5WGpU6M9xjBkkPno0ghXxT7VfG4oTYUy3Zt7KCXcVwYEIJPbw";
            }
            if (_domain.ToLower().Contains("ghree.org"))
            {
                _googleMapKey = "ABQIAAAANQLnMX4Nim90_s2JOpDbCRR1ItsQ4TDVvP6p9L2fxuKKjm45BRQzpg8ngwsl8O9XubaQ9g1UEKfmsg";
            }
            if (_domain.ToLower().Contains("ghree.net"))
            {
                _googleMapKey = "ABQIAAAANQLnMX4Nim90_s2JOpDbCRRGMFSOrepjFVLmO9rODdCypLEfABTKLoxq7Bfc0azrqq_79v6bxWvSSw";
            }
            if (_domain.ToLower().Contains("condesus.com.ar"))
            {
                //Volvemos a dejar la key vieja.....
                //_googleMapKey = "AIzaSyCdkS_VdztrLbaC0Ei17vYDYcdA_o7MjWE";
                _googleMapKey = "ABQIAAAANQLnMX4Nim90_s2JOpDbCRTkfaMaODalt1Pv_z__s-8wEzonLRTmbU9_eRUNS7JmELwwOd9JFY95IQ";
                //                "ABQIAAAANQLnMX4Nim90_s2JOpDbCRTkfaMaODalt1Pv_z__s-8wEzonLRTmbU9_eRUNS7JmELwwOd9JFY95IQ"

                //_sbBuffer.Append("<script src=" + Convert.ToChar(34) + "https://www.google.com/jsapi?key=" + _googleMapKey + Convert.ToChar(34) + " type=\"text/javascript\"> \n");
            }
//            else
//            {
                _sbBuffer.Append("<script src=" + Convert.ToChar(34) + "http://maps.google.com/maps?file=api&v=1&key=" + _googleMapKey + Convert.ToChar(34) + " type=\"text/javascript\"> \n");
//            }

                _sbBuffer.Append(CloseHtmlJavaScript());

                InjectJavascript("JS_GoogleMapKeyRegister", _sbBuffer.ToString());
        }

        #endregion

        #region Build Generic ComboBox
        /// <summary>
        /// Este metodo agrega los textos inciales en el combo en base a los parametros indicados.
        /// </summary>
        /// <param name="_rcbCombo">El combo sobre el que se desea insertar</param>
        /// <param name="showAll">Muestra la opcion de TODOS</param>
        /// <param name="showSelectItem">Muestra la opcion de Seleccione uno</param>
        /// <param name="showNoDependency">Muestra la opcion de Sin dependencia</param>
        private void SetInitialTextInComboBox(RadComboBox _rcbCombo, String entityID, Boolean showAll, Boolean showSelectItem, Boolean showNoDependency)
        {
            //Inserta los valores por defecto, dependiendo de lo solicitado.
            RadComboBoxItem _rcbItem = null;
            if (showNoDependency)
            {
                _rcbItem = new RadComboBoxItem(Resources.Common.ComboBoxNoDependency, Common.Constants.ComboBoxNoDependencyValue);
                _rcbCombo.Items.Add(_rcbItem);
            }
            if (showSelectItem)
            {
                _rcbItem = new RadComboBoxItem(GetComboBoxSelectItemText(entityID), Common.Constants.ComboBoxSelectItemValue);
                _rcbCombo.Items.Add(_rcbItem);
                if (!ManageEntityParams.ContainsKey("SelectItem"))
                { ManageEntityParams.Add("SelectItem", "-1"); }
            }
            if (showAll)
            {
                _rcbItem = new RadComboBoxItem(Resources.Common.ComboBoxAll, Common.Constants.ComboBoxShowAllValue);
                _rcbCombo.Items.Add(_rcbItem);
            }
        }
        /// <summary>
        /// Este metodo agrega los textos inciales en el TreeView en base a los parametros indicados.
        /// </summary>
        /// <param name="_rtvTreeInCombo">El TreeView sobre el que se desea insertar</param>
        /// <param name="showAll">Muestra la opcion de TODOS</param>
        /// <param name="showSelectItem">Muestra la opcion de Seleccione uno</param>
        /// <param name="showNoDependency">Muestra la opcion de Sin dependencia</param>
        protected void SetInitialTextInTreeView(RadTreeView _rtvTreeInCombo, String entityID, Boolean showAll, Boolean showSelectItem, Boolean showNoDependency)
        {
            //Inserta los valores por defecto, dependiendo de lo solicitado.
            RadTreeNode _node = null;
            if (showNoDependency)
            {
                _node = new RadTreeNode(Resources.Common.ComboBoxNoDependency, Common.Constants.ComboBoxNoDependencyValue);
                _node.Selected = true;
                _rtvTreeInCombo.Nodes.Add(_node);
            }
            if (showSelectItem)
            {
                _node = new RadTreeNode(GetComboBoxSelectItemText(entityID), Common.Constants.ComboBoxSelectItemValue);
                _node.Selected = true;
                _rtvTreeInCombo.Nodes.Add(_node);
                if (!ManageEntityParams.ContainsKey("SelectItem"))
                { ManageEntityParams.Add("SelectItem", "-1"); }
            }
            if (showAll)
            {
                _node = new RadTreeNode(Resources.Common.ComboBoxAll, Common.Constants.ComboBoxShowAllValue);
                _rtvTreeInCombo.Nodes.Add(_node);
            }
        }

        /// <summary>
        /// Construye el combo y le carga los datos indicados
        /// </summary>
        /// <param name="entityID">Nombre de la entidad</param>
        /// <param name="showAll">Indica si muestra el mensaje de Todos los registros</param>
        /// <param name="showSelectItem">Indica si muestra el mensaje de Seleccione un...</param>
        /// <param name="showNoDependency">Inidca si muestra el mensaje de Sin dependencia</param>
        /// <returns></returns>
        protected RadComboBox BuildComboBox(String entityID, Boolean showAll, Boolean showSelectItem, Boolean showNoDependency, String selectedValue, Boolean autoPostBack, Boolean autoSize)
        {
            //Contruye el ComboBox que retorna con los valores cargados.
            RadComboBox _rcbCombo = new RadComboBox();

            RadComboBoxItem _rcbItem = null;

            _rcbCombo.EnableViewState = true;
            _rcbCombo.AutoPostBack = autoPostBack;
            _rcbCombo.CausesValidation = false;
            _rcbCombo.EnableEmbeddedSkins = false;

            _rcbCombo.ID = "rdc" + entityID;
            //Si se indica que el combo debe tener un ancho automatico, lo ponemos en 100%
            if (autoSize)
            {
                _rcbCombo.Width = Unit.Percentage(100);
            }
            else
            {
                //Sino siempre fijo en 343px
                _rcbCombo.Width = Unit.Pixel(343);
                _rcbCombo.DropDownWidth = Unit.Pixel(343); //Importante, debe ser el mismo valor que el width del combo.
            } 
            //Limpia
            _rcbCombo.Items.Clear();
            //Setea los textos iniciales que se quieren mostrar en el combo.
            SetInitialTextInComboBox(_rcbCombo, entityID, showAll, showSelectItem, showNoDependency);

            //Carga el Combo.
            foreach (DataRow _drRecord in DataTableListManage[entityID].Rows)
            {
                //Obtiene el nombre a mostrar y el Key Value.
                String _rcbText = Common.Functions.RemoveIndexesTags(GetTextDisplayInComboBox(_drRecord, entityID));
                String _rcbValue = GetKeyValueToDisplay(_drRecord, entityID);

                _rcbItem = new RadComboBoxItem(_rcbText, _rcbValue);
                _rcbCombo.Items.Add(_rcbItem);

            }
            //Si se indica el item seleccionado por defecto, lo selecciona.
            if (selectedValue != String.Empty)
            {
                _rcbCombo.SelectedValue = selectedValue;
                //ya que esta indicando una seleccion por defecto, se arman los EntityParams.
                ManageEntityParams = GetKeyValues(selectedValue);
            }

            return _rcbCombo;
        }

        #region Combo With Tree View
        protected RadComboBox BuildComboBoxWithTreeView(RadTreeView rtvHierarchicalInCombo, String entityID, Boolean showAll, Boolean showSelectItem, Boolean showNoDependency, String selectedValue, PlaceHolder phComboWithTreeView, Boolean autoSize)
        {
            //Construye el ComboBox que retorna con los valores cargados.
            RadComboBox _rcbCombo = new RadComboBox();
            RadComboBoxItem _rcbItem = null;

            _rcbCombo.AutoPostBack = false;
            _rcbCombo.EnableViewState = true;
            _rcbCombo.EnableLoadOnDemand = false;
            _rcbCombo.EnableEmbeddedSkins = false;
            //_rcbCombo.Skin = "EMS";
            _rcbCombo.ID = "rdc" + entityID;
            //Si se indica que el combo debe tener un ancho automatico, lo ponemos en 100%
            if (autoSize)
            {
                _rcbCombo.Width = Unit.Percentage(100);
            }
            else
            {
                //Sino siempre fijo en 343px
                _rcbCombo.Width = Unit.Pixel(343);
                _rcbCombo.DropDownWidth = Unit.Pixel(343); //Importante, debe ser el mismo valor que el width del combo.
            }
            _rcbCombo.Height = Unit.Pixel(200);
            //Limpia
            _rcbCombo.Items.Clear();

            //Setea los textos iniciales que se quieren mostrar en el combo.
            //SetInitialTextInComboBox(_rcbCombo, entityID, showAll, showSelectItem, showNoDependency);
            //Al combo le mete el TreeView.
            _rcbCombo.ItemTemplate = new ComboWithTreeTemplate(UpdatePanelUpdateMode.Always, rtvHierarchicalInCombo, "up" + rtvHierarchicalInCombo.ID, phComboWithTreeView);

            //Mete uno vacio para que salga el tree......
            _rcbItem = new RadComboBoxItem(selectedValue);
            _rcbItem.Selected = true;
            _rcbCombo.Items.Add(_rcbItem);

            return _rcbCombo;
        }
        /// <summary>
        /// Este metodo publico permite obtener el texto indicado para cada entidad del tipo "Select a item..."
        /// </summary>
        /// <param name="rdcCombo">Combo donde termina mostrando el texto</param>
        /// <param name="entityID">Nombre de la entidad que debe ir a buscar para indicar el texto completo a mostrar</param>
        /// <returns>Un<c>String</c></returns>
        protected String GetInitialTextComboWithTreeView(RadComboBox rdcCombo, String entityID)
        {
            //Arma el texto para mostrar en combo por defecto.
            String _selectedValue = String.Empty;
            if ((rdcCombo != null) && (!String.IsNullOrEmpty(rdcCombo.Text)))
            { _selectedValue = rdcCombo.Text; }
            else
            { _selectedValue = GetComboBoxSelectItemText(entityID); }

            return _selectedValue;
        }
        #endregion
        #endregion

        #region TreeView In Combo
        protected RadTreeView BuildHierarchicalInComboContent(String entityGenericHierarchical)
        {
            _HierarchicalTreeViewInCombo = new RadTreeView();
            //Prepara la grilla...
            InitHierarchicalTreeViewInCombo(_HierarchicalTreeViewInCombo, entityGenericHierarchical);

            return _HierarchicalTreeViewInCombo;
        }
        /// <summary>
        /// Metodo publica que realiza la carga de un TreeView, utilizando el DataTable ya cargado.
        /// </summary>
        /// <param name="rtvTreeView">Indica el control del TreeView sobre el cual se realiza la carga</param>
        internal void LoadGenericTreeView(ref RadTreeView rtvTreeView, String entityGenericHierarchical, String singleEntityName, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable displayIn, Boolean isCheckable, String filterExpression, Boolean isLoadElementMap, Boolean checkOrganizationHasPost)
        {
            //Hace un clone del DataTable, por el filter para que quede ordenado como manda el Adapter
            //DataRow[] _dataRow = DataTableListManage[entityGenericHierarchical].Select(filterExpression);
            //DataTable _dt = DataTableListManage[entityGenericHierarchical].Clone();
            //for (int i = 0; i < _dataRow.Length; i++)
            //{
            //    _dt.ImportRow(_dataRow[i]);
            //}
            //foreach (DataRow _drRecord in DataTableListManage[entityGenericHierarchical].Select(filterExpression))

            //El TreeView no tiene Search, con lo cual, saco el filterExpression!
            foreach (DataRow _drRecord in DataTableListManage[entityGenericHierarchical].Rows)
            {
                RadTreeNode _node = SetGenericNodeTreeView(_drRecord, entityGenericHierarchical, displayIn, isCheckable, isLoadElementMap, checkOrganizationHasPost);
                //Con esto se carga el PermissionType como atributo...
                try
                {
                    if (_drRecord["PermissionType"] != null)
                    { _node.Attributes.Add("PermissionType", _drRecord["PermissionType"].ToString()); }
                }
                catch //Si no existe la columna, asume que es view...no podra dar altas...
                { _node.Attributes.Add("PermissionType", Common.Constants.PermissionViewName); }

                _node.Attributes.Add("SingleEntityName", singleEntityName);

                rtvTreeView.Nodes.Add(_node);
            }
        }
        /// <summary>
        /// Este metodo arma el nodo para el tree generico
        /// </summary>
        /// <param name="drRecord">Indica el registro para insertar en el nodo</param>
        /// <returns>Un<c>RadTreeNode</c></returns>
        internal RadTreeNode SetGenericNodeTreeView(DataRow drRecord, String entityID, Common.Constants.ExtendedPropertiesColumnDataTable displayIn, Boolean isCheckable, Boolean isLoadElementMap, Boolean checkOrganizationHasPost)
        {
            RadTreeNode _node = new RadTreeNode();

            _node.Text = Common.Functions.ReplaceIndexesTags(GetTextDisplayInTreeView(drRecord, entityID, displayIn));
            _node.Value = GetKeyValueToDisplay(drRecord, entityID);
            _node.Checkable = isCheckable;
            _node.PostBack = true;
            //_node.ContextMenuID = "rmnSelection";
            SetExpandMode(_node, entityID, isLoadElementMap, checkOrganizationHasPost);

            return _node;
        }

        #region Propiedades del Tree View
        /// <summary>
        /// Metodo que arma las caracteristicas principales del TreeView
        /// </summary>
        /// <param name="rtvMasterHierarchicalListManage">Indica el control del TreeView sobre el cual se realiza la configuracion de las caracteristicas</param>
        private void InitHierarchicalTreeViewInCombo(RadTreeView rtvHierarchicalTreeViewInCombo, String entityGenericHierarchical)
        {
            rtvHierarchicalTreeViewInCombo.ID = "rtvHierarchicalTreeViewInCombo" + entityGenericHierarchical;
            rtvHierarchicalTreeViewInCombo.EnableTheming = true;
            rtvHierarchicalTreeViewInCombo.EnableViewState = true;
            rtvHierarchicalTreeViewInCombo.CheckBoxes = false;
            rtvHierarchicalTreeViewInCombo.ShowLineImages = false;
            rtvHierarchicalTreeViewInCombo.AllowNodeEditing = false;
            rtvHierarchicalTreeViewInCombo.CausesValidation = false;
            rtvHierarchicalTreeViewInCombo.Skin = "ComboTreeView";
            rtvHierarchicalTreeViewInCombo.EnableEmbeddedSkins = false;

            //Crea los metodos del TreeView (Server).
            //rtvHierarchicalTreeViewInCombo.NodeExpand += new RadTreeViewEventHandler(rtvHierarchicalTreeViewInCombo_NodeExpand);
        }
        /// <summary>
        /// Este metodo verifica y arma el Expand para el nodo.
        /// </summary>
        /// <param name="rtvNode">Indica el nodo a verificar y para asociarle mas hijos</param>
        protected void SetExpandMode(RadTreeNode rtvNode, String entityIDHasChildren, Boolean isLoadElementMap, Boolean checkOrganizationHasPost)
        {
            //Busca todos los KeyValues del nodo actual y verifica si tiene hijos.
            Dictionary<String, Object> _params = new Dictionary<String, Object>();
            _params = GetKeyValues(rtvNode.Value);
            //Si es elementMap, tendra que cargar elementos debajo de las clasificaciones...
            _params.Add("IsLoadElementMap", isLoadElementMap);
            _params.Add("HasPost", checkOrganizationHasPost);

            SetExpandMode(rtvNode, entityIDHasChildren, _params);

            //if (entityIDHasChildren.Contains(Common.Constants.SubFixMethodHierarchicalChildren))
            //{
            //    entityIDHasChildren = entityIDHasChildren.Replace(Common.Constants.SubFixMethodHierarchicalChildren, Common.Constants.SubFixMethodHierarchicalHasChildren);
            //}
            //else
            //{
            //    entityIDHasChildren = entityIDHasChildren + Common.Constants.SubFixMethodHierarchicalHasChildren;
            //}
            ////Aca verifica si este nodo tiene hijos para asociarle en el arbol.
            //if (HasChildren(entityIDHasChildren, _params))
            //{ rtvNode.ExpandMode = TreeNodeExpandMode.ServerSide; }
        }
        /// <summary>
        /// Este metodo verifica y arma el Expand para el nodo.
        /// </summary>
        /// <param name="rtvNode">Indica el nodo a verificar y para asociarle mas hijos</param>
        protected void SetExpandMode(RadTreeNode rtvNode, String entityIDHasChildren, Boolean isLoadElementMap, Boolean showOnlyCatalog, Boolean checkOrganizationHasPost)
        {
            //Busca todos los KeyValues del nodo actual y verifica si tiene hijos.
            Dictionary<String, Object> _params = new Dictionary<String, Object>();
            _params = GetKeyValues(rtvNode.Value);
            //Si es elementMap, tendra que cargar elementos debajo de las clasificaciones...
            _params.Add("IsLoadElementMap", isLoadElementMap);
            //Si es showOnlyCatalog, solamente debe cargar los resourceCatalogs...
            _params.Add("ShowOnlyCatalog", showOnlyCatalog);

            SetExpandMode(rtvNode, entityIDHasChildren, _params);
        }
        private void SetExpandMode(RadTreeNode rtvNode, String entityIDHasChildren, Dictionary<String, Object> parameters)
        {
            if (entityIDHasChildren.Contains(Common.Constants.SubFixMethodHierarchicalChildren))
            {
                entityIDHasChildren = entityIDHasChildren.Replace(Common.Constants.SubFixMethodHierarchicalChildren, Common.Constants.SubFixMethodHierarchicalHasChildren);
            }
            else
            {
                entityIDHasChildren = entityIDHasChildren + Common.Constants.SubFixMethodHierarchicalHasChildren;
            }
            //Aca verifica si este nodo tiene hijos para asociarle en el arbol.
            if (HasChildren(entityIDHasChildren, parameters))
            { rtvNode.ExpandMode = TreeNodeExpandMode.ServerSide; }
        }
        #endregion

        #region Tree View
            /// <summary>
            /// Metodo privado que permite ir expandiendo cada nodo para llegar al selected.
            /// </summary>
            /// <param name="sender">Se envia el TreeView</param>
            /// <param name="e">NodeEventArgs</param>
            /// <param name="entityNameChildren">Nombre para el metodo que obtiene los childrens</param>
            internal void NodeExpand(object sender, Telerik.Web.UI.RadTreeNodeEventArgs e, String entityNameChildren, String filterExpression, Common.Constants.ExtendedPropertiesColumnDataTable displayIn, Boolean isCheckable, String alternateEntityIDforIcon)
            {
                if (e.Node != null)
                {
                    e.Node.Nodes.Clear();

                    Dictionary<String, Object> _params = new Dictionary<String, Object>();
                    _params = GetKeyValues(e.Node.Value);

                    //Actualiza el Data table con los hijos, para poder asociarlas al nodo expadido.
                    BuildGenericDataTable(entityNameChildren, _params);
                    foreach (DataRow _drRecord in DataTableListManage[entityNameChildren].Select(filterExpression))
                    {
                        RadTreeNode _node = SetGenericNodeTreeView(_drRecord, entityNameChildren, displayIn, isCheckable, false, false);
                        try
                        {
                            if (_drRecord["PermissionType"] != null)
                            { _node.Attributes.Add("PermissionType", _drRecord["PermissionType"].ToString()); }
                        }
                        catch //Si no existe la columna, asume que es view...no podra dar altas...
                        { _node.Attributes.Add("PermissionType", Common.Constants.PermissionViewName); }

                        e.Node.Nodes.Add(_node);
                        e.Node.Expanded = true;

                        //Obtiene el EntityName limpio para buscarlo en el resource...
                        String _entityNameforIcons;
                        _entityNameforIcons = entityNameChildren.Replace("Children", String.Empty).Replace("Roots", String.Empty);
                        //Si viene el alternativo, uso ese....
                        if (!String.IsNullOrEmpty(alternateEntityIDforIcon))
                        {
                            _entityNameforIcons = alternateEntityIDforIcon.Replace("Children", String.Empty).Replace("Roots", String.Empty);
                        }
                        _node.CssClass = (GetGlobalResourceObject("IconsByEntity", _entityNameforIcons) != null) ? GetGlobalResourceObject("IconsByEntity", _entityNameforIcons).ToString() : String.Empty;

                        SetExpandMode(_node, entityNameChildren, false, false);
                    }
                }
            }
            internal void NodeExpand(object sender, Telerik.Web.UI.RadTreeNodeEventArgs e, String entityNameClassificationChildren, String entityNameElementChildren, String filterExpression, Common.Constants.ExtendedPropertiesColumnDataTable displayIn, Boolean isCheckable, String alternateEntityIDforIcon)
            {
                if (e.Node != null)
                {
                    e.Node.Nodes.Clear();

                    Dictionary<String, Object> _params = new Dictionary<String, Object>();
                    _params = GetKeyValues(e.Node.Value);

                    //Vamos por las clasificaciones (Carga las clasificaciones dentro de esta clasificacion)
                    //Actualiza el Data table con los hijos, para poder asociarlas al nodo expadido.
                    BuildGenericDataTable(entityNameClassificationChildren, _params);
                    foreach (DataRow _drRecord in DataTableListManage[entityNameClassificationChildren].Select(filterExpression))
                    {
                        RadTreeNode _node = SetGenericNodeTreeView(_drRecord, entityNameClassificationChildren, displayIn, isCheckable, false, false);
                        try
                        {
                            if (_drRecord["PermissionType"] != null)
                            { _node.Attributes.Add("PermissionType", _drRecord["PermissionType"].ToString()); }
                        }
                        catch //Si no existe la columna, asume que es view...no podra dar altas...
                        { _node.Attributes.Add("PermissionType", Common.Constants.PermissionViewName); }

                        e.Node.Nodes.Add(_node);
                        e.Node.Expanded = true;

                        //Obtiene el EntityName limpio para buscarlo en el resource...
                        String _entityNameforIcons;
                        _entityNameforIcons = entityNameClassificationChildren.Replace("Children", String.Empty).Replace("Roots", String.Empty);
                        //Si viene el alternativo, uso ese....
                        if (!String.IsNullOrEmpty(alternateEntityIDforIcon))
                        {
                            _entityNameforIcons = alternateEntityIDforIcon.Replace("Children", String.Empty).Replace("Roots", String.Empty);
                        }
                        _node.CssClass = (GetGlobalResourceObject("IconsByEntity", _entityNameforIcons) != null) ? GetGlobalResourceObject("IconsByEntity", _entityNameforIcons).ToString() : String.Empty;

                        SetExpandMode(_node, entityNameClassificationChildren, false, false);
                    }

                    //Vamos por el Elemento (Carga todos los elementos para la clasificacion seleccionada)
                    //Actualiza el Data table con los hijos, para poder asociarlas al nodo expadido.
                    BuildGenericDataTable(entityNameElementChildren, _params);
                    foreach (DataRow _drRecord in DataTableListManage[entityNameElementChildren].Select(filterExpression))
                    {
                        RadTreeNode _node = SetGenericNodeTreeView(_drRecord, entityNameElementChildren, displayIn, isCheckable, false, false);
                        _node.Expanded = false;
                        _node.ExpandMode = TreeNodeExpandMode.ClientSide;
                        try
                        {
                            if (_drRecord["PermissionType"] != null)
                            { _node.Attributes.Add("PermissionType", _drRecord["PermissionType"].ToString()); }
                        }
                        catch //Si no existe la columna, asume que es view...no podra dar altas...
                        { _node.Attributes.Add("PermissionType", Common.Constants.PermissionViewName); }

                        e.Node.Nodes.Add(_node);
                        e.Node.Expanded = false; //Como son elementos, no tiene expand.

                        //Obtiene el EntityName limpio para buscarlo en el resource...
                        String _entityNameforIcons;
                        _entityNameforIcons = entityNameElementChildren.Replace("Children", String.Empty).Replace("Roots", String.Empty);
                        //Si viene el alternativo, uso ese....
                        if (!String.IsNullOrEmpty(alternateEntityIDforIcon))
                        {
                            _entityNameforIcons = alternateEntityIDforIcon.Replace("Children", String.Empty).Replace("Roots", String.Empty);
                        }
                        _node.CssClass = (GetGlobalResourceObject("IconsByEntity", _entityNameforIcons) != null) ? GetGlobalResourceObject("IconsByEntity", _entityNameforIcons).ToString() : String.Empty;

                        SetExpandMode(_node, entityNameElementChildren, false, false);
                    }
                }
            }

            /// <summary>
            /// Este metodo publico, permite seleccionar el item dentro de un TreeView.
            /// </summary>
            /// <param name="selectedValue">KeyValues del item al que se desea encontrar su familia</param>
            /// <param name="rtvTreeView">TreeView</param>
            /// <param name="rcbCombo">Combo contenedor del TreeView</param>
            /// <param name="entityID">Nombre entidad que esta representada en el TreeView</param>
            /// <param name="entityChildrenID">Nombre del metodo publico para acceder a los children</param>
            protected void SelectItemTreeView(String selectedValue, ref RadTreeView rtvTreeView, String entityID, String entityChildrenID)
            {
                RadTreeNode _node = null;
                //Busca el nodo que debe quedar seleccionado.
                _node = GetNodeSelected(selectedValue, ref rtvTreeView, entityID, entityChildrenID);
                //Ya tenemos el nodo, ahora setea el texto y el valor en el combo contenedor.
                if (_node != null)
                {
                    _node.Selected = true;
                }
            }
            /// <summary>
            /// Este metodo privado, permite obtener el Nodo de un Tree para que quede seleccionado.
            /// </summary>
            /// <param name="selectedValue">KeyValues del item al que se desea encontrar su familia</param>
            /// <param name="rtvTreeView">TreeView</param>
            /// <param name="rcbCombo">Combo contenedor del TreeView</param>
            /// <param name="entityID">Nombre entidad que esta representada en el TreeView</param>
            /// <param name="entityChildrenID">Nombre del metodo publico para acceder a los children</param>
            internal RadTreeNode GetNodeSelected(String selectedValue, ref RadTreeView rtvTreeView, String entityID, String entityChildrenID)
            {
                RadTreeNode _node = null;
                Dictionary<String, Object> _parameters = GetKeyValues(selectedValue);

                //Primero trata de obtener el nodo con el value esperado.
                _node = rtvTreeView.FindNodeByValue(selectedValue);
                //Si no lo encuentra, entonces tiene que obtener todo el arbol genealogico de la entidad esperada (selectedValue)
                if (_node == null)
                {
                    Stack<String> _parents = new Stack<String>();
                    //Obtiene con el factory toda la familia de la entidad esperada.
                    _parents = GetFamilyFromChild(entityID + "Family", _parameters);
                    //recorre cada uno y va expandiendo el tree, para dejar el padre seleccionado.
                    while (_parents.Count > 0)
                    {
                        String _parent = _parents.Pop();
                        _node = rtvTreeView.FindNodeByValue(_parent.ToString());
                        NodeExpand(rtvTreeView, new Telerik.Web.UI.RadTreeNodeEventArgs(_node), entityChildrenID, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, false, String.Empty);
                    }
                }
                return _node;
            }
            /// <summary>
            /// Este metodo publico, permite seleccionar el item parent dentro de un Combo con TreeView Element Maps.
            /// </summary>
            /// <param name="selectedValue">KeyValues del item al que se desea encontrar su familia</param>
            /// <param name="rtvTreeView">TreeView</param>
            /// <param name="rcbCombo">Combo contenedor del TreeView</param>
            /// <param name="entityID">Nombre entidad que esta representada en el TreeView</param>
            /// <param name="entityChildrenID">Nombre del metodo publico para acceder a los children</param>
            protected void SelectItemTreeViewParentElementMaps(String selectedValueClassification, String selectedValueElement, ref RadTreeView rtvTreeView, String entityID, String entityChildrenID, String entityElementChildrenID)
            {
                RadTreeNode _nodeClass = null;
                RadTreeNode _nodeElement = null;
                Dictionary<String, Object> _parameters = GetKeyValues(selectedValueClassification);

                //Primero trata de obtener el nodo de la Classificacion con el value esperado.
                _nodeClass = rtvTreeView.FindNodeByValue(selectedValueClassification);
                //Si no lo encuentra, entonces tiene que obtener todo el arbol genealogico de la entidad esperada (selectedValue)
                if (_nodeClass == null)
                {
                    Stack<String> _parents = new Stack<String>();
                    //Obtiene con el factory toda la familia de la entidad esperada.
                    _parents = GetFamilyFromChild(entityID + "Family", _parameters);
                    //recorre cada uno y va expandiendo el tree, para dejar el padre seleccionado.
                    while (_parents.Count > 0)
                    {
                        String _parent = _parents.Pop();
                        _nodeClass = rtvTreeView.FindNodeByValue(_parent.ToString());
                        NodeExpand(rtvTreeView, new Telerik.Web.UI.RadTreeNodeEventArgs(_nodeClass), entityChildrenID, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, false, String.Empty);
                    }
                }

                //Revisa si encontro al nodo de Classification, para ahora buscar el elemento.
                if (_nodeClass != null)
                {
                    //como encontro la clasificacion, debe expandirla para obtener los elementos...
                    NodeExpand(rtvTreeView, new Telerik.Web.UI.RadTreeNodeEventArgs(_nodeClass), entityElementChildrenID, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, false, String.Empty);
                    //entonces ahora busca el elemento...
                    Dictionary<String, Object> _parametersElement = GetKeyValues(selectedValueElement);
                    //Primero trata de obtener el nodo de la Classificacion con el value esperado.
                    _nodeElement = rtvTreeView.FindNodeByValue(selectedValueElement);
                }
                //Ahora finalmente, vemos si se encontro el nodo del elemento, que es el que realmente se necesita...
                //Ya tenemos el nodo, ahora setea el texto y el valor en el combo contenedor.
                if (_nodeElement != null)
                {
                    _nodeElement.Selected = true;
                }
            }
            protected void SelectItemTreeViewElementMapsGlobalMenu(String selectedValueClassification, String selectedValueElement, ref RadTreeView rtvTreeView, String entityID, String entityChildrenID, String entityElementChildrenID)
            {
                RadTreeNode _nodeClass = null;
                RadTreeNode _nodeElement = null;
                Dictionary<String, Object> _parameters = GetKeyValues(selectedValueClassification);

                //Primero trata de obtener el nodo de la Classificacion con el value esperado.
                _nodeClass = rtvTreeView.Nodes[0].Nodes.FindNodeByValue(selectedValueClassification);
                //Si no lo encuentra, entonces tiene que obtener todo el arbol genealogico de la entidad esperada (selectedValue)
                if (_nodeClass == null)
                {
                    Stack<String> _parents = new Stack<String>();
                    //Obtiene con el factory toda la familia de la entidad esperada.
                    _parents = GetFamilyFromChild(entityID + "Family", _parameters);
                    //recorre cada uno y va expandiendo el tree, para dejar el padre seleccionado.
                    while (_parents.Count > 0)
                    {
                        String _parent = _parents.Pop();
                        _nodeClass = rtvTreeView.Nodes[0].Nodes.FindNodeByValue(_parent.ToString());
                        NodeExpand(rtvTreeView, new Telerik.Web.UI.RadTreeNodeEventArgs(_nodeClass), entityChildrenID, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, false, String.Empty);
                    }
                }

                //Revisa si encontro al nodo de Classification, para ahora buscar el elemento.
                if (_nodeClass != null)
                {
                    //como encontro la clasificacion, debe expandirla para obtener los elementos...
                    NodeExpand(rtvTreeView, new Telerik.Web.UI.RadTreeNodeEventArgs(_nodeClass), entityChildrenID, entityElementChildrenID, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, false, String.Empty);
                    //entonces ahora busca el elemento...
                    //Dictionary<String, Object> _parametersElement = GetKeyValues(selectedValueElement);
                    //Primero trata de obtener el nodo de la Classificacion con el value esperado.
                    _nodeElement = _nodeClass.Nodes.FindNodeByValue(selectedValueElement);
                }
                //Ahora finalmente, vemos si se encontro el nodo del elemento, que es el que realmente se necesita...
                //Ya tenemos el nodo, ahora setea el texto y el valor en el combo contenedor.
                if (_nodeElement != null)
                {
                    _nodeElement.Selected = true;
                    _nodeElement.Expanded = false;
                }
            }
        #endregion

        #region Events
        //protected void rtvHierarchicalTreeViewInCombo_NodeExpand(object sender, RadTreeNodeEventArgs e)
        //{
        //    Dictionary<String, Object> _params = new Dictionary<String, Object>();
        //    _params = GetKeyValues(e.Node.Value);

        //    //Actualiza el Data table con los hijos, para poder asociarlas al nodo expadido.
        //    BuildGenericDataTable(_EntityNameHierarchicalChildren, _params);
        //    foreach (DataRow _drRecord in DataTableListManage[_EntityNameHierarchicalChildren].Rows)
        //    {
        //        RadTreeNode _node = SetNodeTreeViewManage(_drRecord, _EntityNameHierarchicalChildren, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo);
        //        e.Node.Nodes.Add(_node);
        //        SetExpandMode(_node, _EntityNameHierarchicalChildren);
        //    }
        //}
        #endregion
        #endregion

        #region Methods for Manage Primary Keys
        /// <summary>
        /// Este metodo obtiene cuales son las claves primarias del DataTable y las devuelve en una lista
        /// </summary>
        /// <returns>Un<c>List</c> con todas las claves</returns>
        private List<String> GetPrimaryKeyColumnsToDisplay(String entityID)
        {
            List<String> _columnsToDisplay = new List<String>();
            // Crea el array de DataColumn.
            DataColumn[] _primaryKeyColumns;
            //Obtiene la lista de columnas definidas como PrimaryKey.
            _primaryKeyColumns = DataTableListManage[entityID].PrimaryKey;
            //Define un array de String para insertarle los nombre de las columnas
            String[] _pkColumnName = new String[_primaryKeyColumns.Length];
            //Recorre todas las columnas y las agrega en el List
            for (int i = 0; i < _primaryKeyColumns.Length; i++)
            {
                _columnsToDisplay.Add(_primaryKeyColumns[i].ColumnName);
            }

            return _columnsToDisplay;
        }
        /// <summary>
        /// Este metodo construye el "Value" para agregarselo a un control.Value (node.value o combo.value)
        /// </summary>
        /// <param name="drRecord">Registro indicado del DataTable</param>
        /// <returns>Un<c>String</c> con el value armado. Ej:(IdOrganization=2,Id=3)</returns>
        protected String GetKeyValueToDisplay(DataRow drRecord, String entityID)
        {
            String _buff = String.Empty;
            foreach (String _columnKeyToValue in GetPrimaryKeyColumnsToDisplay(entityID))
            {
                _buff += _columnKeyToValue + "=" + drRecord[_columnKeyToValue].ToString() + "& ";
            }

            if (_buff != String.Empty)
            {
                return _buff.Substring(0, _buff.Length - 2);
            }
            else
            {
                return _buff;
            }
        }
        /// <summary>
        /// Metodo privado que permite obtener cuales son las columnas a mostrar en un TreeView
        /// </summary>
        /// <param name="entityID">Nombre de la Entidad para acceder el DataTable</param>
        /// <returns>Una lista de String</returns>
        private List<String> GetColumnsToDisplayInTreeView(String entityID, Common.Constants.ExtendedPropertiesColumnDataTable displayIn)
        {
            List<String> _columnsToDisplay = new List<String>();
            foreach (DataColumn _column in DataTableListManage[entityID].Columns)
            {
                //if ((Boolean)_column.ExtendedProperties[Common.Constants.ExtendedPropertiesColumnDataTable.DisplayManage])
                if ((Boolean)_column.ExtendedProperties[displayIn])
                {
                    _columnsToDisplay.Add(_column.ColumnName);
                }
            }
            return _columnsToDisplay;
        }
        /// <summary>
        /// Este metodo permite obtener el texto que debe mostrarse en el nodo de un TreeView.
        /// </summary>
        /// <param name="drRecord">Registros del DataTable, para luego acceder directamente con el nombre del campo</param>
        /// <param name="entityID">Nombre de la Entidad para acceder el DataTable</param>
        /// <returns>Un<c>String</c></returns>
        internal String GetTextDisplayInTreeView(DataRow drRecord, String entityID, Common.Constants.ExtendedPropertiesColumnDataTable displayIn)
        {
            String _buff = String.Empty;
            foreach (String _columnToDisplay in GetColumnsToDisplayInTreeView(entityID, displayIn))
            {
                _buff += drRecord[_columnToDisplay].ToString() + " ";
            }
            return _buff.Trim();
        }
        #endregion

        #region Internal Methods for Combo
        /// <summary>
        /// Este metodo Obtiene cuales son las columnas que se deben mostrar en un combo y lo devuelve en una lista.
        /// </summary>
        /// <returns>Un<c>List</c> de string</returns>
        private List<String> GetColumnsToDisplayInComboBox(String entityID)
        {
            List<String> _columnsToDisplay = new List<String>();
            foreach (DataColumn _column in DataTableListManage[entityID].Columns)
            {
                if ((Boolean)_column.ExtendedProperties[Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo])
                {
                    _columnsToDisplay.Add(_column.ColumnName);
                }
            }
            return _columnsToDisplay;
        }
        /// <summary>
        /// Este metodo arma el texto que se debe mostrar en un ComboBox
        /// </summary>
        /// <param name="drRecord">Indica el registro sobre el que se va a trabajar</param>
        /// <returns>Un<c>String</c> con el texto a mostrar</returns>
        private String GetTextDisplayInComboBox(DataRow drRecord, String entityID)
        {
            String _buff = String.Empty;
            foreach (String _columnToDisplay in GetColumnsToDisplayInComboBox(entityID))
            {
                _buff += drRecord[_columnToDisplay].ToString() + " ";
            }
            return _buff;
        }
        /// <summary>
        /// Retorna el texto del mensaje a mostar para la seleccion de un item determinado
        /// </summary>
        /// <param name="key">Indica el nombre de la entidad a mostrar</param>
        /// <returns>Un<c>String</c> con el texto a mostrar</returns>
        internal String GetComboBoxSelectItemText(String key)
        {
            Object _objBuffer = GetGlobalResourceObject("CommonListManage", Common.Constants.ComboBoxSelectItemPrefix.ToLower() + key.ToLower());

            if (_objBuffer != null)
                return _objBuffer.ToString();

            //Por defaul, si alguien no cargo el mensaje en el resource...retorno esto.
            return Resources.Common.ComboBoxSelectItemDefaultPrefix;
        }
        #endregion

        #region External Methods Parse Key Values (Node.Values o Combo.Values)
        /// <summary>
        /// Este metodo devuelve todos los Key Value que tiene el TreeView
        /// </summary>
        /// <param name="keyValues">Recibe un string del node.Value</param>
        /// <returns>Un<c>Dictionary con todos los KeyValues de un nodo</c></returns>
        protected Dictionary<String, Object> GetKeyValues(String keyValues)
        {
            //Arma el buffer a retornar
            Dictionary<String, Object> _buffer = new Dictionary<String, Object>();
            //separa todos los parametros
            String[] _keyValues = null;
            //Si es null, no tiene nada que hacer.
            if (keyValues != null)
            {
                //Si viene con el & o la coma...es lo mismo.
                if (keyValues.Contains('&'))
                {
                    _keyValues = keyValues.Split('&');
                }
                else
                {
                    _keyValues = keyValues.Split(',');
                }
                //recorre por parametro los key y su valor.
                for (int i = 0; i < _keyValues.Count(); i++)
                {
                    //Obtiene el key y su valor
                    if (_keyValues[i] != String.Empty)
                    {
                        String _key = _keyValues[i].Split('=')[0];
                        String _value = _keyValues[i].Split('=')[1];
                        //Si la key ya existe, la borra y vuelve a cargarla (esto es para que se quede con la ultima cargada.)
                        if (_buffer.ContainsKey(_key.Trim()))
                        {
                            //si existe, borra y vuelve a crear.
                            _buffer.Remove(_key.Trim());
                            _buffer.Add(_key.Trim(), _value.Trim());
                        }
                        else
                        {
                            //y lo inserta en el buffer. si no existe.
                            _buffer.Add(_key.Trim(), _value.Trim());
                        }
                    }
                }
            }
            //Retorna el diccionario con todos los valores
            return _buffer;
        }
        /// <summary>
        /// Este metodo retorna el valor para un Key indicado
        /// </summary>
        /// <param name="keyValues">indica el string node.Value</param>
        /// <param name="key">indica la clave a buscar en el nodo</param>
        /// <returns></returns>
        protected Object GetKeyValue(String keyValues, String key)
        {
            //Obtengo todos los KeyValues y ahora accedo al Key solicitado. y lo retorna.
            if (GetKeyValues(keyValues).ContainsKey(key))
            { return GetKeyValues(keyValues)[key]; }
            else
            //Si no esta la clave solicitada, devuelve null.
            { return null; }
        }
        /// <summary>
        /// Este metodo se encarga de obtener las PK del Navigator y de ahi sacar la Key solicitada y entregar el Int64
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Un<c>Int64</c></returns>
        protected Object GetPKfromNavigator(String key)
        {
            //Verifica que haya PK en el Navigator
            if (NavigatorContainsPkTransferVar("PkCompost"))
            {
                //Revisa que exista la key solicitada dentro del PK.
                if (GetKeyValues(NavigatorGetPkEntityIdTransferVar<String>("PkCompost")).ContainsKey(key))
                {
                    //Obtiene las PK desde el Navigator, y de ahi solo obtiene el Key solicitado.
                    return GetKeyValue(NavigatorGetPkEntityIdTransferVar<String>("PkCompost"), key);
                }
            }

            //Como tampoco existe en el PKEntity..., retorna 0
            return null;
        }
        #endregion

        #region Factories
        /// <summary>
        /// Este metodo generico, permite la carga de un DataTable, en base al nombre del metodo que se le pasa y sus parametros
        /// </summary>
        /// <param name="methodName">Nombre del metodo publico que ira a buscar por Reflection</param>
        /// <param name="param">Parametros que necesite el metodo</param>
        public void BuildGenericDataTable(String methodName, Dictionary<String, Object> param)
        {
            DataTable _dt = (DataTable)new Condesus.EMS.WebUI.Business.Collections(methodName).Execute(param);
            //DataTable _dt = (DataTable)new Condesus.EMS.WebUI.Business.Collections(_EMSLibrary, methodName).Execute(param);
            if (DataTableListManage.ContainsKey(methodName))
            {
                DataTableListManage.Remove(methodName);
                DataTableListManage.Add(methodName, _dt);
            }
            else
            {
                DataTableListManage.Add(methodName, _dt);
            }
        }
        /// <summary>
        /// Este metodo permite la ejecucion generica para identificar si un Objeto puntual tiene hijos asociados.
        /// </summary>
        /// <param name="methodName">Nombre del metodo publico que ira a buscar por Reflection</param>
        /// <param name="param">Parametros que necesite el metodo</param>
        /// <returns>Un<c>Boolean</c></returns>
        public Boolean HasChildren(String methodName, Dictionary<String, Object> param)
        {
            return (Boolean)new Condesus.EMS.WebUI.Business.Collections(methodName).Execute(param);
            //return (Boolean)new Condesus.EMS.WebUI.Business.Collections(_EMSLibrary, methodName).Execute(param);
        }
        /// <summary>
        /// Este metodo permite ejecutar cualquier metodo de una entidad en forma generica, que no retorne nada. (previamente debe haber sido programado en Entities.cs)
        /// </summary>
        /// <param name="methodName">Nombre del metodo publico que ira a buscar por Reflection</param>
        /// <param name="param">Parametros que necesite el metodo</param>
        public void ExecuteGenericMethodEntity(String methodName, Dictionary<String, Object> param)
        {
            new Condesus.EMS.WebUI.Business.Entities(methodName).Execute(param);
            //new Condesus.EMS.WebUI.Business.Entities(_EMSLibrary, methodName).Execute(param);
        }
        /// <summary>
        /// Este metodo permite ejecutar el metodo por Reflection de una entidad especifica y setea todos los parametros
        /// para poder navegar a la Manage que le corresponde, por eso retorna la direccion URL a la que debe navegar.
        /// </summary>
        /// <param name="methodName">Nombre del metodo publico que ira a buscar por Reflection</param>
        /// <param name="param">Parametros que necesite el metodo</param>
        public String GetParameterToManager(String methodName, Dictionary<String, Object> param)
        {
            return (String)new Condesus.EMS.WebUI.Business.Entities(methodName).Execute(param);
        }
        /// <summary>
        /// Este metodo permite la ejecucion generica para obtener toda la familia de un hijo.
        /// </summary>
        /// <param name="methodName">Nombre del metodo publico que ira a buscar por Reflection</param>
        /// <param name="param">Parametros que necesite el metodo</param>
        /// <returns>Un<c>Boolean</c></returns>
        public Stack<String> GetFamilyFromChild(String methodName, Dictionary<String, Object> param)
        {
            return (Stack<String>)new Condesus.EMS.WebUI.Business.Collections(methodName).Execute(param);
        }
        /// <summary>
        /// Este metodo permite la ejecucion generica para identificar si un Objeto puntual tiene Lenguaje asociado.
        /// </summary>
        /// <param name="methodName">Nombre del metodo publico de LGs que ira a buscar por Reflection</param>
        /// <returns>Un<c>Boolean</c></returns>
        public Boolean HasLanguage(String methodName)
        {
            return (Boolean)new Condesus.EMS.WebUI.Business.Collections(methodName).HasLanguages();
        }
        /// <summary>
        /// Este metodo retorna una dictionary con las opciones de menu disponible para el Usuario y para la Entidad indicada.
        /// </summary>
        /// <param name="methodName">Nombre del metodo publico de la Entidad que ira a buscar por Reflection</param>
        /// <returns>Un<c>Dictionary</c></returns>
        public Dictionary<String, KeyValuePair<String, Boolean>> GetOptionMenuByEntity(String methodName, Dictionary<String, Object> param, Boolean isViewer)
        {
            try
            {
                //Si no tiene el parametro lo agrega, sino lo actualiza.
                if (param.ContainsKey("IsViewer"))
                { param.Remove("IsViewer"); }

                param.Add("IsViewer", isViewer);
                //Al methodName, le saco el "_LG", ya que para las de lenguaje no es necesari tener un menu especifico, alcanza con el de la entidad principal.
                //A todos los entitiy name que se reciben se le concatena el _MenuOption.
                return (Dictionary<String, KeyValuePair<String, Boolean>>)new Condesus.EMS.WebUI.Business.Entities(methodName.Replace("_LG", String.Empty)).Execute(param);
            }
            //Si no existe no entrega menu.
            catch { return new Dictionary<String, KeyValuePair<String, Boolean>>(); }
        }
        /// <summary>
        /// Este metodo retorna un Dictionary con el nombre de la entidad a visualizar y sus parametros. (key=value)
        /// </summary>
        /// <param name="methodName">Nombre del metodo publico de la Entidad que ira a buscar por Reflection</param>
        /// <returns>Un<c>Dictionary</c></returns>
        public String[,] GetEntityRelated(String methodName, Dictionary<String, Object> param)
        {
            try
            {
                //A todos los entitiy name que se reciben se le concatena el _MenuOption.
                return (String[,])new Condesus.EMS.WebUI.Business.ViewerEntityRelated(methodName).Execute(param);
            }
            //Si no existe no entrega menu.
            catch { return new String[0, 0]; }
        }
        /// <summary>
        /// Este metodo retorna un Dictionary de CatalogDoc.
        /// </summary>
        /// <param name="methodName">Nombre del metodo publico de la Entidad que ira a buscar por Reflection</param>
        /// <returns>Un<c>Dictionary</c></returns>
        public Dictionary<Int64, Condesus.EMS.Business.KC.Entities.CatalogDoc> GetPicturesByObject(String methodName, Dictionary<String, Object> param)
        {
            try
            {
                return (Dictionary<Int64, Condesus.EMS.Business.KC.Entities.CatalogDoc>)new Condesus.EMS.WebUI.Business.Collections(methodName).Execute(param);
            }
            catch { return new Dictionary<Int64, Condesus.EMS.Business.KC.Entities.CatalogDoc>(); }
        }
        /// <summary>
        /// Este metodo retorna un Dictionary de ExtendedPropertyValue.
        /// </summary>
        /// <param name="methodName">Nombre del metodo publico de la Entidad que ira a buscar por Reflection</param>
        /// <returns>Un<c>Dictionary</c></returns>
        public ExtendedPropertyValue GetExtendedPropertyValueByObject(String methodName, Dictionary<String, Object> param)
        {
            try
            {
                return (ExtendedPropertyValue)new Condesus.EMS.WebUI.Business.Collections(methodName).Execute(param);
            }
            catch { return null; }
        }

        /// <summary>
        /// Extrae el ContextInfoEntityName (el nombre "propio" de la entidad seteada en el adapter) para el Titulo del ContextInfo Path
        /// </summary>
        /// <param name="EntityNameGrid">El nombre de la DataTable que hostea el ListManager</param>
        /// <returns>El Nombre Propio de la Entidad Seleccionada de la Grilla</returns>
        protected String GetContextInfoCaption(String entityNameGrid, RadGrid rgdListManage)
        {
            String _retString = String.Empty;
            GridItem _selectedItem = (rgdListManage.SelectedItems.Count > 0) ? rgdListManage.SelectedItems[0] : null;

            if (_selectedItem == null)
                return _retString; //o throw Exception...

            try
            {
                if (DataTableListManage.ContainsKey(entityNameGrid))
                {
                    DataTable _dt = DataTableListManage[entityNameGrid];
                    GridDataItem _dataItem = (GridDataItem)_selectedItem;

                    var _entityNames = from e in _dt.Columns.Cast<DataColumn>()
                                       where (Boolean)e.ExtendedProperties[Common.Constants.ExtendedPropertiesColumnDataTable.IsContextMenuCaption]
                                       select e.ColumnName;

                    if (_entityNames.Any())
                    {
                        if (_entityNames.Count() == 1)
                        {
                            _retString = _dataItem[_entityNames.First()].Text;
                        }
                        else
                        {
                            foreach (String _s in _entityNames)
                                _retString = String.Concat(_retString, ", ", _dataItem[_s].Text);

                            if (_retString.StartsWith(", "))
                                _retString = _retString.Remove(0, 2);
                        }
                    }
                }
            }
            catch
            {
                return _retString;
            }
            return _retString;
        }
        /// <summary>
        /// Extrae el ContextInfoEntityName (el nombre "propio" de la entidad seteada en el adapter) para el Titulo del ContextInfo Path
        /// </summary>
        /// <param name="EntityNameGrid">El nombre de la DataTable que hostea el ListManager</param>
        /// <returns>El Nombre Propio de la Entidad Seleccionada de la Grilla</returns>
        protected String GetContextInfoCaption(String entityNameGrid, RadTreeView rtvListManage)
        {
            String _retString = String.Empty;
            RadTreeNode _selectedItem = (rtvListManage.SelectedNodes.Count > 0) ? rtvListManage.SelectedNodes[0] : null;

            if (_selectedItem == null)
                return _retString; //o throw Exception...

            _retString = _selectedItem.Text;

            return _retString;
        }
        #endregion

        #region Resource Helpers
            protected String GetValueFromGlobalResource(String className, String key)
            {
                try
                {
                    Object _bufRetString = GetGlobalResourceObject(className, key);

                    if (_bufRetString != null)
                        return _bufRetString.ToString();
                }
                catch { }

                //return this.GetType().Name;
                return String.Empty;
            }
            protected String GetValueFromLocalResource(String key)
            {
                try
                {
                    Object _bufRetString = GetLocalResourceObject(key);

                    if (_bufRetString != null)
                        return _bufRetString.ToString();
                }
                catch { }

                //return this.GetType().Name;
                //return "Undefined";
                return String.Empty;
            }
        #endregion

    }
}
