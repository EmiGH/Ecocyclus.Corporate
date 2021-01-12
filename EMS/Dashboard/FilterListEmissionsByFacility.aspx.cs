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
using Condesus.EMS.Business.KC.Entities;
using System.Reflection;

namespace Condesus.EMS.WebUI.Dashboard
{
    public partial class FilterListEmissionsByFacility : BaseProperties
    {
        #region Internal Properties
        private Int64 _IdProcess
        {
            get
            {
                return base.NavigatorContainsTransferVar("IdProcess") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdProcess")) : Convert.ToInt64(GetPKfromNavigator("IdProcess"));
            }
        }
        #endregion

        #region Page Load & Init
            protected override void InitializeHandlers()
            {
                base.InitializeHandlers();

                base.InjectValidateDateTimePicker(rdtFrom.ClientID, rdtThrough.ClientID, "ReportFilter");

                customvEndDate.ClientValidationFunction = "ValidateDateTimeRangeReportFilter";
                btnList.Click += new EventHandler(btnList_Click);
            }
            protected override void OnPreInit(EventArgs e)
            {
                base.OnPreInit(e);

                //Registro Mis Custom MenuPanels
                RegisterCustomMenuPanels();
                IsGridPageIndexChanged = false;
            }
            protected override void OnInit(EventArgs e)
            {
                base.OnInit(e);
                //Arma la grilla completa
                //LoadListManage();
            }
            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);

                customvEndDate.ErrorMessage = Resources.ConstantMessage.ValidationDateFromTo;

                if (!Page.IsPostBack)
                {
                    //Form
                    base.SetContentTableRowsCss(tblContentForm);
                }

                //Inyecta la funcion JS que verifica los Validator de la pagina, esta funcion es global para el sistema. (cualquiera lo puede usar.)
                InjectShowReportCalculation(_IdProcess, rdtFrom.ClientID, rdtThrough.ClientID);
            }
            protected override void SetPagetitle()
            {
                try
                {
                    //Busca el nombre de la pagina si es que se lo pasaron. Sino usa el nombre de la entidad.
                    String _pageTitle = base.NavigatorGetTransferVar<String>("PageTitle");
                    if (String.IsNullOrEmpty(_pageTitle))
                    {
                        base.PageTitle = "Report";
                    }
                    else
                    {
                        base.PageTitle = _pageTitle;
                    }
                }
                catch { base.PageTitle = String.Empty; }
            }
            protected override void SetPageTileSubTitle()
            {
                try
                {
                    String _pageSubTitle = base.NavigatorGetTransferVar<String>("PageSubTitle");
                    if (String.IsNullOrEmpty(_pageSubTitle))
                    {
                        base.PageTitleSubTitle = Resources.CommonListManage.lblSubtitle;
                    }
                    else
                    {
                        base.PageTitleSubTitle = _pageSubTitle;
                    }
                }
                catch
                { base.PageTitleSubTitle = String.Empty; }
            }
        #endregion

        #region Private Methods
            private void RegisterCustomMenuPanels()
            {
                List<String> _menuPanels = new List<String>();
                _menuPanels.Add(Common.Constants.ContextInformationKey);
                _menuPanels.Add(Common.Constants.ContextElementMapsKey);

                FwMasterPage.ContentNavigatorCustomMenuPanels(_menuPanels);
            }
            protected void InjectShowReportCalculation(Int64 idProcess, String dtStartDate, String dtEndDate)
            {
                System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

                _sbBuffer.Append(OpenHtmlJavaScript());

                _sbBuffer.Append("function ShowEmissionsByFacility(e)                                                                 \n");
                _sbBuffer.Append("{                                                                                                 \n");
                _sbBuffer.Append("  //Ejecuta la validacion de los Validator en la pagina                                           \n");
                _sbBuffer.Append("  if (CheckClientValidatorPage())                                                                 \n");
                _sbBuffer.Append("  {                                                                                               \n");
                _sbBuffer.Append("      //No hay validator activos                                                                  \n");
                //Abre una nueva ventana con el reporte.
                _sbBuffer.Append("      var _IEXPLORER = 'Microsoft Internet Explorer';                                             \n");
                _sbBuffer.Append("      var _FIREFOX = 'Netscape';                                                                  \n");
                _sbBuffer.Append("      var _BrowserName = navigator.appName;                                                       \n");
                _sbBuffer.Append("      var datePickerStart = document.getElementById('" + dtStartDate + "');                       \n");
                _sbBuffer.Append("      var datePickerEnd = document.getElementById('" + dtEndDate + "');                           \n");

                _sbBuffer.Append("      var _idProcess = " + idProcess + "                                                          \n");
                _sbBuffer.Append("      var _startDate = datePickerStart.value;                                                     \n");
                _sbBuffer.Append("      var _endDate = datePickerEnd.value;                                                         \n");

                //_sbBuffer.Append(" debugger;                                                                                        \n");
                _sbBuffer.Append("      if (_BrowserName == _IEXPLORER)                                                             \n");
                _sbBuffer.Append("      {   //IE and Opera                                                                          \n");
                _sbBuffer.Append("          var newWindow = window.open(" + Convert.ToChar(34) + "http://" +Request.ServerVariables["HTTP_HOST"].ToString() + (!string.IsNullOrEmpty(Request.ApplicationPath) ? Request.ApplicationPath : "") +  "/Managers/ListEmissionsByFacility.aspx?IdProcess=" + Convert.ToChar(34) + " + _idProcess + '&StartDate=' + _startDate + '&EndDate=' + _endDate, 'Reports', 'toolbar=yes,location=yes,resizable=yes,status=yes,menubar=yes,scrollbars=yes');      \n");
                _sbBuffer.Append("      }                                                                                           \n");
                _sbBuffer.Append("      else                                                                                        \n");
                _sbBuffer.Append("      {   //FireFox                                                                               \n");
                _sbBuffer.Append("          var newWindow = window.parent.open(" + Convert.ToChar(34) + "http://" +Request.ServerVariables["HTTP_HOST"].ToString() + (!string.IsNullOrEmpty(Request.ApplicationPath) ? Request.ApplicationPath : "") +  "/Managers/ListEmissionsByFacility.aspx?IdProcess=" + Convert.ToChar(34) + " + _idProcess + '&StartDate=' + _startDate + '&EndDate=' + _endDate, 'Reports', 'toolbar=yes,location=yes,resizable=yes,status=yes,menubar=yes,scrollbars=yes');      \n");
                _sbBuffer.Append("      }                                                                                           \n");
                _sbBuffer.Append("      newWindow.focus();                                                                          \n");
                //Con esto hago que la ventana quede en tamaño completo de la pantalla.
                _sbBuffer.Append("      newWindow.moveTo(0, 0);                                                                     \n");
                _sbBuffer.Append("      newWindow.resizeTo(screen.availWidth, screen.availHeight);                                  \n");
                //frena la ejecucion de todo y queda esperando a la confirmacion en el popup.
                _sbBuffer.Append("      StopEvent(e);     //window.event.returnValue = false;                                       \n");
                _sbBuffer.Append("      return false;     //window.event.returnValue = false;                                       \n");
                _sbBuffer.Append("  }                                                                                               \n");
                _sbBuffer.Append("}                                                                                                 \n");

                _sbBuffer.Append(CloseHtmlJavaScript());

                InjectJavascript("JS_ShowReportCalculation", _sbBuffer.ToString());
            }
        #endregion

        #region Page Events
            protected void btnList_Click(object sender, EventArgs e)
            {

                //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('http://" + Request.ServerVariables["HTTP_HOST"] + "/Dashboard/ReportCalculationsOfTransformation.aspx?IdOrganization='" + _IdOrganization + "'&IdScope='" + _IdScope + "'&StartDate='" + rdtFrom.SelectedDate + "'&EndDate='" + rdtThrough.SelectedDate + ", 'Reports', 'toolbar=yes,location=yes,resizable=yes,status=yes,menubar=yes,scrollbars=yes');", true);
            }
        #endregion
    }
}
