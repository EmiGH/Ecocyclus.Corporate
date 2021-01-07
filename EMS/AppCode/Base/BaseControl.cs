using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Telerik.Web.UI;

namespace Condesus.EMS.WebUI
{
    public class BaseControl : System.Web.UI.UserControl
    {
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
        //protected Condesus.EMS.Business.EMS EMSLibrary
        //{
        //    get
        //    {
        //        switch (Page.Master.GetType().Name.ToLower())
        //        {
        //            case "ems_master":
        //                //return ((Condesus.EMS.WebUI.EMS)Page.Master).EMSLibrary;
        //            case "emstest_master":
        //                return ((Condesus.EMS.WebUI.EMSTest)Page.Master).EMSLibrary;
        //            case "emspopup_master":
        //                return ((Condesus.EMS.WebUI.EMSPopUp)Page.Master).EMSLibrary;
        //        }

        //        return null;
        //    }
        //}

        protected Int64 IdOrganization
        { 
            get 
            { 
                //object o = Session["IdOrganization"];
                object o = EMSLibrary.User.Person.Organization.IdOrganization;
                if(o != null)
                    //return Convert.ToInt64(Session["IdOrganization"]);
                    return EMSLibrary.User.Person.Organization.IdOrganization;
                throw new Exception("No organization defined");
            }
        }

        #region Check Security

        protected void CheckSecurity(ref RadMenuItem rmiMenu,
                                            String module,
                                            String businessClass,
                                            Boolean add,
                                            Boolean modify,
                                            Boolean view,
                                            Boolean delete,
                                            Boolean language)
        {
            rmiMenu.Items.Clear();

            if (add)
            {
                ////Boolean _addItem = EMSLibrary.User.Security.Authorize(module, businessClass, "Add");

                RadMenuItem _rmiAdd = new RadMenuItem(Resources.Common.mnuAdd);
                _rmiAdd.Value = "rmiAdd";
                Common.Functions.DoRadItemSecurity(_rmiAdd, add);
                rmiMenu.Items.Add(_rmiAdd);
            }
            if (modify)
            {
                //Boolean _editItem = EMSLibrary.User.Security.Authorize(module, businessClass, "Modify");

                RadMenuItem _rmiEdit = new RadMenuItem(Resources.Common.mnuEdit);
                _rmiEdit.Value = "rmiEdit";
                Common.Functions.DoRadItemSecurity(_rmiEdit, modify);
                rmiMenu.Items.Add(_rmiEdit);
            }
            if (view)
            {
                //Boolean _viewItem = EMSLibrary.User.Security.Authorize(module, businessClass, "Item");

                //Menu de Seleccion
                RadMenuItem _rmiView = new RadMenuItem(Resources.Common.mnuView);
                _rmiView.Value = "rmiView";
                Common.Functions.DoRadItemSecurity(_rmiView, view);
                rmiMenu.Items.Add(_rmiView);
            }
            if (delete)
            {
                //Boolean _deleteItem = EMSLibrary.User.Security.Authorize(module, businessClass, "Remove");

                RadMenuItem _rmiDelete = new RadMenuItem(Resources.Common.mnuDelete);
                _rmiDelete.Value = "rmiDelete";
                Common.Functions.DoRadItemSecurity(_rmiDelete, delete);
                rmiMenu.Items.Add(_rmiDelete);
            }
            if (language)
            {
                //Boolean _languageItem = EMSLibrary.User.Security.Authorize(module, businessClass, "Item");

                RadMenuItem _rmiLanguage = new RadMenuItem(Resources.Common.mnuLanguage);
                _rmiLanguage.Value = "rmiLanguage";
                Common.Functions.DoRadItemSecurity(_rmiLanguage, true);
                rmiMenu.Items.Add(_rmiLanguage);
            }
        }
        protected void CheckSecurity(ref RadMenu rmiMenu,
                                           String module,
                                           String businessClass,
                                           Boolean add,
                                           Boolean modify,
                                           Boolean view,
                                           Boolean delete,
                                           Boolean language)
        {
            rmiMenu.Items.Clear();

            if (add)
            {
                //Boolean _addItem = EMSLibrary.User.Security.Authorize(module, businessClass, "Add");

                RadMenuItem _rmiAdd = new RadMenuItem(Resources.Common.mnuAdd);
                _rmiAdd.Value = "rmiAdd";
                Common.Functions.DoRadItemSecurity(_rmiAdd, add);
                rmiMenu.Items.Add(_rmiAdd);
            }
            if (modify)
            {
                //Boolean _editItem = EMSLibrary.User.Security.Authorize(module, businessClass, "Modify");

                RadMenuItem _rmiEdit = new RadMenuItem(Resources.Common.mnuEdit);
                _rmiEdit.Value = "rmiEdit";
                Common.Functions.DoRadItemSecurity(_rmiEdit, modify);
                rmiMenu.Items.Add(_rmiEdit);
            }
            if (view)
            {
                //Boolean _viewItem = EMSLibrary.User.Security.Authorize(module, businessClass, "Item");

                //Menu de Seleccion
                RadMenuItem _rmiView = new RadMenuItem(Resources.Common.mnuView);
                _rmiView.Value = "rmiView";
                Common.Functions.DoRadItemSecurity(_rmiView, view);
                rmiMenu.Items.Add(_rmiView);
            }
            if (delete)
            {
                //Boolean _deleteItem = EMSLibrary.User.Security.Authorize(module, businessClass, "Remove");

                RadMenuItem _rmiDelete = new RadMenuItem(Resources.Common.mnuDelete);
                _rmiDelete.Value = "rmiDelete";
                Common.Functions.DoRadItemSecurity(_rmiDelete, delete);
                rmiMenu.Items.Add(_rmiDelete);
            }
            if (language)
            {
                //Boolean _languageItem = EMSLibrary.User.Security.Authorize(module, businessClass, "Item");

                RadMenuItem _rmiLanguage = new RadMenuItem(Resources.Common.mnuLanguage);
                _rmiLanguage.Value = "rmiLanguage";
                Common.Functions.DoRadItemSecurity(_rmiLanguage, true);
                rmiMenu.Items.Add(_rmiLanguage);
            }
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

        //Si la funcion es inyectada por Sm - no hay debugger!!
        protected void InjectJavascript(String key, String jsScript, Boolean scriptManager)
        {
            if(scriptManager)
                ScriptManager.RegisterClientScriptBlock(this.Page, typeof(UpdatePanel), key, jsScript, false);
            else
                InjectJavascript(key, jsScript);
        }

        protected void InjectAlert(String mensaje)
        {
            System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

            _sbBuffer.Append(OpenHtmlJavaScript());
            _sbBuffer.Append("alert('" + mensaje + "');");
            _sbBuffer.Append(CloseHtmlJavaScript());

            InjectJavascript("JS_SHOW_MENU", _sbBuffer.ToString(),true);
        }
        protected void InjectPostBack(String btnTransferAddClientID)
        {
            System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

            _sbBuffer.Append(OpenHtmlJavaScript());
            //Inicializacion de Variables PageRequestManager
            //_sbBuffer.Append("window.onload = InitializeAjax;                                                               \n");
            _sbBuffer.Append("  var _IEXPLORER = 'Microsoft Internet Explorer';                                             \n");
            _sbBuffer.Append("  var _FIREFOX = 'Netscape';                                                                  \n");
            _sbBuffer.Append("  var _BrowserName = navigator.appName;                                                       \n");

            _sbBuffer.Append("  if (_BrowserName == _IEXPLORER)                                                             \n");
            _sbBuffer.Append("  {   //IE and Opera                                                                          \n");
            _sbBuffer.Append("      window.attachEvent('onload', InitializeAjax);                                \n");
            _sbBuffer.Append("  }                                                                                           \n");
            _sbBuffer.Append("  else                                                                                        \n");
            _sbBuffer.Append("  {   //FireFox                                                                               \n");
            _sbBuffer.Append("      document.addEventListener('DOMContentLoaded', InitializeAjax, false);        \n");
            _sbBuffer.Append("  }                                                                                           \n");
            _sbBuffer.Append("var prm;                                                                                      \n");
            _sbBuffer.Append("var postBackElement;                                                                          \n");

            _sbBuffer.Append("function InitializeAjax()                                                                     \n");
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
            _sbBuffer.Append("  btnPostBack.click();\n");
            _sbBuffer.Append("}                                                                                             \n");

            _sbBuffer.Append(CloseHtmlJavaScript());

            InjectJavascript("JS_PostBack", _sbBuffer.ToString(), true);
        }
        protected void InjectTabsPostBack(String hdn_childRequestClientID, String btnPostBackClientID, String uProgressTabConainerClientID)
        {
            System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

            _sbBuffer.Append(OpenHtmlJavaScript());
            //Inicializacion de Variables PageRequestManager
            //_sbBuffer.Append("window.onload = InitializeAjax;                                                               \n");
            _sbBuffer.Append("  var _IEXPLORER = 'Microsoft Internet Explorer';                                             \n");
            _sbBuffer.Append("  var _FIREFOX = 'Netscape';                                                                  \n");
            _sbBuffer.Append("  var _BrowserName = navigator.appName;                                                       \n");

            _sbBuffer.Append("  if (_BrowserName == _IEXPLORER)                                                             \n");
            _sbBuffer.Append("  {   //IE and Opera                                                                          \n");
            _sbBuffer.Append("      window.attachEvent('onload', InitializeAjax);                                \n");
            _sbBuffer.Append("  }                                                                                           \n");
            _sbBuffer.Append("  else                                                                                        \n");
            _sbBuffer.Append("  {   //FireFox                                                                               \n");
            _sbBuffer.Append("      document.addEventListener('DOMContentLoaded', InitializeAjax, false);        \n");
            _sbBuffer.Append("  }                                                                                           \n");
            _sbBuffer.Append("var prm;                                                                                      \n");
            _sbBuffer.Append("var postBackElement;                                                                          \n");

            _sbBuffer.Append("function InitializeAjax()                                                                     \n");
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

            InjectJavascript("JS_TabPostBack", _sbBuffer.ToString(), true);
        }
        protected void InjectRmnOptionItemClickHandler(String rmnOptionClientID, String updateProgressId, String entity)
        {   //Esta funcion se ejecuta al hacer click sobre un item del menuRad
            System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

            _sbBuffer.Append(OpenHtmlJavaScript());

            _sbBuffer.Append("function rmnOption" + entity + "_OnClientItemClickedHandler(sender, eventArgs)                        \n");
            _sbBuffer.Append("{                                                                                                     \n");
            _sbBuffer.Append("  document.getElementById('radMenuClickedId" + entity + "').value = 'Option';                         \n");
            _sbBuffer.Append("  if (eventArgs.Item.Value=='rmiAdd')                                                                 \n");
            _sbBuffer.Append("  {                                                                                                   \n");
            _sbBuffer.Append("      $get('" + rmnOptionClientID + "').style.display = 'none';                                       \n");
            _sbBuffer.Append("      $get('" + updateProgressId + "').style.display = 'block';                                       \n");
            _sbBuffer.Append("      var _itemAdd = document.getElementById('radMenuItemClickedAdd" + entity + "');                  \n");
            _sbBuffer.Append("      if (_itemAdd!=null)                                                                             \n");
            _sbBuffer.Append("      {                                                                                               \n");
            _sbBuffer.Append("          _itemAdd.value = 'Add';                                                                     \n");
            _sbBuffer.Append("      }                                                                                               \n");
            _sbBuffer.Append("  }                                                                                                   \n");
            _sbBuffer.Append("  else                                                                                                \n");
            _sbBuffer.Append("  {                                                                                                   \n");
            _sbBuffer.Append("      if (eventArgs.Item.Value=='rmiDelete')                                                          \n");
            _sbBuffer.Append("      {                                                                                               \n");
            _sbBuffer.Append("          StopEvent(eventArgs);     //window.event.returnValue = false;                                                           \n");
            //Aca hace la verifica si hay al menos un registro chequeado, sino muestra una alerta.
            _sbBuffer.Append("          if (ValidateItemCheckedControl())                                                           \n");
            _sbBuffer.Append("          {                                                                                           \n");
            _sbBuffer.Append("              var modalPopupBehavior = $find('programmaticModalPopupBehavior" + entity + "');         \n");
            _sbBuffer.Append("              modalPopupBehavior.show();                                                              \n");
            _sbBuffer.Append("          }                                                                                           \n");
            _sbBuffer.Append("          else                                                                                        \n");
            _sbBuffer.Append("          {                                                                                           \n");
            _sbBuffer.Append("              alert('" + Resources.Common.NoRecordSelectedToDelete + "');                             \n");
            _sbBuffer.Append("          }                                                                                           \n");
            _sbBuffer.Append("      }                                                                                               \n");
            _sbBuffer.Append("      else                                                                                            \n");
            _sbBuffer.Append("      {                                                                                               \n");
            _sbBuffer.Append("          if (eventArgs.Item.Value=='rmiLanguage')                                                    \n");
            _sbBuffer.Append("          {                                                                                           \n");
            _sbBuffer.Append("              $get('" + rmnOptionClientID + "').style.display = 'none';                               \n");
            _sbBuffer.Append("              $get('" + updateProgressId + "').style.display = 'block';                               \n");
            _sbBuffer.Append("              if ($get('radMenuItemClicked')!= null)                                                  \n");
            _sbBuffer.Append("              {                                                                                       \n");
            _sbBuffer.Append("                  document.getElementById('radMenuItemClicked" + entity + "').value = 'Language';     \n");
            _sbBuffer.Append("              }                                                                                       \n");
            _sbBuffer.Append("          }                                                                                           \n");
            _sbBuffer.Append("          else                                                                                        \n");
            _sbBuffer.Append("          {                                                                                           \n");
            _sbBuffer.Append("              StopEvent(eventArgs);     //window.event.returnValue = false;                                                       \n");
            _sbBuffer.Append("          }                                                                                           \n");
            _sbBuffer.Append("      }                                                                                               \n");
            _sbBuffer.Append("  }                                                                                                   \n");
            _sbBuffer.Append("}                                                                                                     \n");

            _sbBuffer.Append(CloseHtmlJavaScript());

            InjectJavascript("JS_RmnOptionItemClickHandler" + entity, _sbBuffer.ToString(), true);
        }
        protected void InjectRmnSelectionItemClickHandler(String uProgMasterGridClientID, String entity)
        {   //Esta funcion se ejecuta al hacer click sobre un item del menuRad
            System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

            _sbBuffer.Append(OpenHtmlJavaScript());

            _sbBuffer.Append("function rmnSelection" + entity + "_OnClientItemClickedHandler(sender, eventArgs)                 \n");
            _sbBuffer.Append("{                                                                                                 \n");
            _sbBuffer.Append("  document.getElementById('radMenuClickedId" + entity + "').value = 'Selection';                  \n");
            _sbBuffer.Append("  if (eventArgs.Item.Value=='rmiDelete')                                                          \n");
            _sbBuffer.Append("  {                                                                                               \n");
            _sbBuffer.Append("      StopEvent(eventArgs);     //window.event.returnValue = false;                                                           \n");
            _sbBuffer.Append("      var modalPopupBehavior = $find('programmaticModalPopupBehavior" + entity + "');             \n");
            _sbBuffer.Append("      modalPopupBehavior.show();                                                                  \n");
            _sbBuffer.Append("  }                                                                                               \n");
            _sbBuffer.Append("  else                                                                                            \n");
            _sbBuffer.Append("  {                                                                                               \n");
            _sbBuffer.Append("      $get('" + uProgMasterGridClientID + "').style.display = 'block';                            \n");
            _sbBuffer.Append("  }                                                                                               \n");
            _sbBuffer.Append("}                                                                                                 \n");

            _sbBuffer.Append(CloseHtmlJavaScript());

            InjectJavascript("JS_RmnSelectionItemClickHandler" + entity, _sbBuffer.ToString(), true);
        }
        protected void InjectRowContextMenu(String rmnSelectionClientID, String entity)
        {
            //Esta funcion es la encargada de mostrar el menu de opciones cuando se realiza click derecho sobre un registro en la grilla.
            //Parametros, <index> es el indice del registro donde esta parado, <e> es el evento
            System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

            _sbBuffer.Append(OpenHtmlJavaScript());

            _sbBuffer.Append("function RowContextMenu" + entity + "(index, e)                                                               \n");
            _sbBuffer.Append("{                                                                                                             \n");
            _sbBuffer.Append("  if(this.Rows[index].ItemType != 'NestedView')                                                               \n");
            _sbBuffer.Append("  {                                                                                                           \n");
            //Accede al menu
            _sbBuffer.Append("      var MyMenu = " + rmnSelectionClientID + ";                                                              \n");
            //Se guarda el indice del row en donde esta parado y el id de la tabla
            _sbBuffer.Append("      document.getElementById('radGridClickedRowIndex" + entity + "').value = this.Rows[index].RealIndex;     \n");
            _sbBuffer.Append("      document.getElementById('radGridClickedTableId" + entity + "').value = this.UID;                        \n");
            //Muestra el menu
            _sbBuffer.Append("      MyMenu.Show(e);                                                                                         \n");
            _sbBuffer.Append("      e.cancelBubble = true;                                                                                  \n");
            _sbBuffer.Append("      e.returnValue = false;                                                                                  \n");
            _sbBuffer.Append("      if (e.stopPropagation)                                                                                  \n");
            _sbBuffer.Append("      {                                                                                                       \n");
            _sbBuffer.Append("          e.stopPropagation();                                                                                \n");
            _sbBuffer.Append("          e.preventDefault();                                                                                 \n");
            _sbBuffer.Append("      }                                                                                                       \n");
            //Pone al registro en selected.
            _sbBuffer.Append("      this.SelectRow(this.Rows[index].Control, true);                                                         \n");
            _sbBuffer.Append("  }                                                                                                           \n");
            _sbBuffer.Append("}                                                                                                             \n");

            _sbBuffer.Append(CloseHtmlJavaScript());

            InjectJavascript("JS_RowContextMenu" + entity, _sbBuffer.ToString(), true);
        }
        protected void InjectShowMenu(String rmnSelectionClientID, String entity)
        {
            //Esta funcion es la encargada de mostrar el menu de opciones cuando se selecciona el campo de seleccion en la grilla
            //Parametros    <e> event
            //          <idGridRow> el indice del row en donde esta parado
            //          <idGridTable> el id de la tabla donde esta parado(por si es jerarquica)
            System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

            _sbBuffer.Append(OpenHtmlJavaScript());

            _sbBuffer.Append("function ShowMenu" + entity + "(e, idGridRow, idGridTable)                                            \n");
            _sbBuffer.Append("{                                                                                                     \n");
            //Obtiene el menu
            InsertBreakPointJavaScript();
            _sbBuffer.Append("  var menu = " + rmnSelectionClientID + ";                                                            \n");
            //Se guarda el indice del row posicionado y el ID de la tabla(en caso de que sea jerarquica pueden ser varios)
            _sbBuffer.Append("  document.getElementById('radGridClickedRowIndex" + entity + "').value = idGridRow;                  \n");
            _sbBuffer.Append("  document.getElementById('radGridClickedTableId" + entity + "').value = idGridTable                  \n");
            _sbBuffer.Append("  if ( (!e.relatedTarget) || (!menu.IsChildOf(menu.DomElement, e.relatedTarget)) )                    \n");
            _sbBuffer.Append("  {                                                                                                   \n");
            //Muestra el menu.
            _sbBuffer.Append("      menu.Show(e);                                                                                   \n");
            _sbBuffer.Append("  }                                                                                                   \n");
            _sbBuffer.Append("  e.cancelBubble = true;                                                                              \n");
            _sbBuffer.Append("  if (e.stopPropagation)                                                                              \n");
            _sbBuffer.Append("  {                                                                                                   \n");
            _sbBuffer.Append("      e.stopPropagation();                                                                            \n");
            _sbBuffer.Append("      e.preventDefault();                                                                             \n");
            _sbBuffer.Append("  }                                                                                                   \n");
            _sbBuffer.Append("}                                                                                                     \n");

            _sbBuffer.Append(CloseHtmlJavaScript());

            InjectJavascript("JS_ShowMenu" + entity, _sbBuffer.ToString(), true);
        }
        protected void InjectValidateItemChecked(String nameListManageClientID)
        {
            System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

            _sbBuffer.Append(OpenHtmlJavaScript());

            _sbBuffer.Append("function ValidateItemCheckedControl()                                                                                        \n");
            _sbBuffer.Append("{                                                                                                                     \n");
            _sbBuffer.Append("  var arrayInput = document.getElementById('" + nameListManageClientID + "').getElementsByTagName('INPUT');           \n");
            _sbBuffer.Append("  for(i = 0; i < arrayInput.length; i++)                                                                              \n");
            _sbBuffer.Append("  {                                                                                                                   \n");
            _sbBuffer.Append("      if (IsCheckBoxControl(arrayInput[i]) && arrayInput[i].checked) return true;                                            \n");
            _sbBuffer.Append("  }                                                                                                                   \n");
            _sbBuffer.Append("  return false;                                                                                                       \n");
            _sbBuffer.Append("}                                                                                                                     \n");
            //Verifica que sea de tipo checkbox
            _sbBuffer.Append("function IsCheckBoxControl(chk)                                                                                              \n");
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

            InjectJavascript("JS_ValidateItemCHKControl", _sbBuffer.ToString(), true);

        }
        protected void InjectBeforeClientClick(String radComboBoxClientID, String entity)
        {
            System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

            _sbBuffer.Append(OpenHtmlJavaScript());

            //_sbBuffer.Append("window.onload = InitializeBCCJSGlobalVars" + entity + ";                                                 \n");
            //_sbBuffer.Append("window.attachEvent('onload', InitializeBCCJSGlobalVarsCtrl" + entity + ");                                         \n");
            _sbBuffer.Append("  var _IEXPLORER = 'Microsoft Internet Explorer';                                             \n");
            _sbBuffer.Append("  var _FIREFOX = 'Netscape';                                                                  \n");
            _sbBuffer.Append("  var _BrowserName = navigator.appName;                                                       \n");

            _sbBuffer.Append("  if (_BrowserName == _IEXPLORER)                                                             \n");
            _sbBuffer.Append("  {   //IE and Opera                                                                          \n");
            _sbBuffer.Append("      window.attachEvent('onload', InitializeBCCJSGlobalVarsCtrl" + entity + ");                                \n");
            _sbBuffer.Append("  }                                                                                           \n");
            _sbBuffer.Append("  else                                                                                        \n");
            _sbBuffer.Append("  {   //FireFox                                                                               \n");
            _sbBuffer.Append("      document.addEventListener('DOMContentLoaded', InitializeBCCJSGlobalVarsCtrl" + entity + ", false);        \n");
            _sbBuffer.Append("  }                                                                                           \n");
            _sbBuffer.Append("var ComboBoxTreeview" + entity + " = null;                                                            \n");

            _sbBuffer.Append("function InitializeBCCJSGlobalVarsCtrl" + entity + "() {                                                    \n");
            _sbBuffer.Append("  ComboBoxTreeview" + entity + "  = document.getElementById('ComboBoxTreeview" + entity + "');        \n");
            _sbBuffer.Append("  ComboBoxTreeview" + entity + ".onclick = StopPropagationCtrl;                                           \n");
            _sbBuffer.Append("}                                                                                                     \n");

            _sbBuffer.Append("function ProcessClientClickCtrl" + entity + "(node)                                                       \n");
            _sbBuffer.Append("{                                                                                                     \n");
            //Obtiene el menu
            _sbBuffer.Append("   var combo = " + radComboBoxClientID + ";                                                            \n");
            //_sbBuffer.Append("   var combo = <%=radComboBoxClientID.ClientID %>;                                                  \n");
            //Se guarda el indice del row posicionado y el ID de la tabla(en caso de que sea jerarquica pueden ser varios)
            _sbBuffer.Append("  combo.Items[0].Value = node.Value;                                                                  \n");
            _sbBuffer.Append("  combo.Items[0].Text = node.Text;                                                                    \n");
            _sbBuffer.Append("  combo.Items[0].Select();                                                                            \n");
            _sbBuffer.Append("  combo.HideDropDown();                                                                               \n");
            _sbBuffer.Append("}                                                                                                     \n");

            _sbBuffer.Append("function StopPropagationCtrl(e)                                                                           \n");
            _sbBuffer.Append("{                                                                                                     \n");
            _sbBuffer.Append("  if(!e)                                                                                              \n");
            _sbBuffer.Append("  {                                                                                                   \n");
            _sbBuffer.Append("      e = window.event;                                                                               \n");
            _sbBuffer.Append("  }                                                                                                   \n");
            _sbBuffer.Append("  e.cancelBubble = true;                                                                              \n");
            _sbBuffer.Append("}                                                                                                     \n");

            _sbBuffer.Append(CloseHtmlJavaScript());

            InjectJavascript("JS_BeforeClientClickCtrl" + entity, _sbBuffer.ToString(), true);
        }
        protected void InjectValidateCombo(String radComboBoxClientID, String triggerButtonClientID, String triggerValue, String entity)
        {
            System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

            _sbBuffer.Append(OpenHtmlJavaScript());

            _sbBuffer.Append("function ValidateCombo" + entity + "(sender,e)                                                        \n");
            _sbBuffer.Append("{                                                                                                     \n");
            _sbBuffer.Append("  var combo = " + radComboBoxClientID + ";                                                            \n");
            _sbBuffer.Append("  var _IEXPLORER = 'Microsoft Internet Explorer';                                                     \n");
            _sbBuffer.Append("  var _FIREFOX = 'Netscape';                                                                          \n");
            _sbBuffer.Append("  var _BrowserName = navigator.appName;                                                               \n");
            _sbBuffer.Append("  if (_BrowserName == _IEXPLORER)                                                                     \n");
            _sbBuffer.Append("  {   //IE and Opera                                                                                  \n");
            _sbBuffer.Append("      if(window.event.srcElement.id == '" + triggerButtonClientID + "')                               \n");
            _sbBuffer.Append("          if(combo.SelectedItem.Value == '" + triggerValue + "')                                      \n");
            _sbBuffer.Append("              e.IsValid = false;                                                                      \n");
            _sbBuffer.Append("  }                                                                                                   \n");
            _sbBuffer.Append("  else                                                                                                \n");
            _sbBuffer.Append("  {   //FireFox                                                                                       \n");
            _sbBuffer.Append("      if(e.srcElement.id == '" + triggerButtonClientID + "')                                          \n");
            _sbBuffer.Append("          if(combo.SelectedItem.Value == '" + triggerValue + "')                                      \n");
            _sbBuffer.Append("              e.IsValid = false;                                                                      \n");
            _sbBuffer.Append("  }                                                                                                   \n");
            _sbBuffer.Append("}                                                                                                     \n");

            _sbBuffer.Append(CloseHtmlJavaScript());

            InjectJavascript("JS_ValidateCombo" + entity, _sbBuffer.ToString(), true);
        }

        #endregion
    }
}
