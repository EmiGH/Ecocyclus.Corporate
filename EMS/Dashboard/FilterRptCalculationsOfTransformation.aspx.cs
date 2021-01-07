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
    public partial class FilterRptCalculationsOfTransformation : BaseProperties
    {
        #region Internal Properties
        private Int64 _IdProcess
        {
            get
            {
                return base.NavigatorContainsTransferVar("IdProcess") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdProcess")) : Convert.ToInt64(GetPKfromNavigator("IdProcess"));
            }
        }
        private String _Report
        {
            get
            {
                return base.NavigatorContainsTransferVar("Report") ? Convert.ToString(base.NavigatorGetTransferVar<Object>("Report")) : Convert.ToString(GetPKfromNavigator("Report"));
            }
        }
        private String _ReportType
        {
            get
            {
                return base.NavigatorContainsTransferVar("ReportType") ? Convert.ToString(base.NavigatorGetTransferVar<Object>("ReportType")) : Convert.ToString(GetPKfromNavigator("ReportType"));
            }
        }
        private enum MyEnum_rblReport
        {
            GEI = 0,
            CL
        }
        private enum MyEnum_rblReportType
        {
            GA_S_A_FT_F = 0,
            GA_FT_F_S_A,
            S_GA_A_FT_F,
            S_A_FT_F,
            FT_F_S_A,
            O_S_A_FT_F,
            Evolution
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

                LoadPreFilters();
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
                InjectShowReportCalculation(_IdProcess, rdtFrom.ClientID, rdtThrough.ClientID, rblReportType.ClientID);
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
            private void HideExpandColumnRecursive(GridTableView tableView)
            {
                GridItem[] nestedViewItems = tableView.GetItems(GridItemType.NestedView);
                foreach (GridNestedViewItem nestedViewItem in nestedViewItems)
                {
                    foreach (GridTableView nestedView in nestedViewItem.NestedTableViews)
                    {
                        if (nestedView.Items.Count == 0)
                        {

                            TableCell cell = nestedView.ParentItem["ExpandColumn"];
                            cell.CssClass = "ExpandColumn";
                            cell.Controls[0].Visible = false;
                            nestedViewItem.Visible = false;
                        }
                        if (nestedView.HasDetailTables)
                        {
                            HideExpandColumnRecursive(nestedView);
                            TableCell cell = nestedView.ParentItem["ExpandColumn"];
                            cell.CssClass = "ExpandColumn";
                        }
                    }
                }
            }
            protected void InjectShowReportCalculation(Int64 idProcess, String dtStartDate, String dtEndDate, String reportType)
            {
                System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

                _sbBuffer.Append(OpenHtmlJavaScript());

                _sbBuffer.Append("function ShowReportCalculation(e)                                                                 \n");
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
                _sbBuffer.Append("      var _reportType;                                                                            \n");
                _sbBuffer.Append("      switch (true)                                                                              \n");
                _sbBuffer.Append("      {                                                                                                       \n");
                _sbBuffer.Append("          case document.getElementById('ctl00_ContentMain_rblReportType_0').checked: //GA-S-A-FT-F                                    \n");
                _sbBuffer.Append("              _reportType = 'GA-S-A-FT-F'                                                                                                   \n");
                _sbBuffer.Append("              break;                                                                                          \n");
                _sbBuffer.Append("          case document.getElementById('ctl00_ContentMain_rblReportType_1').checked: //GA-FT-F-S-A                                    \n");
                _sbBuffer.Append("              _reportType = 'GA-FT-F-S-A'                                                                                                   \n");
                _sbBuffer.Append("              break;                                                                                          \n");
                _sbBuffer.Append("          case document.getElementById('ctl00_ContentMain_rblReportType_2').checked: //S-GA-A-FT-F                                    \n");
                _sbBuffer.Append("              _reportType = 'S-GA-A-FT-F'                                                                                                   \n");
                _sbBuffer.Append("              break;                                                                                          \n");
                _sbBuffer.Append("          case document.getElementById('ctl00_ContentMain_rblReportType_3').checked: //S-A-FT-F                                    \n");
                _sbBuffer.Append("              _reportType = 'S-A-FT-F'                                                                                                   \n");
                _sbBuffer.Append("              break;                                                                                          \n");
                _sbBuffer.Append("          case document.getElementById('ctl00_ContentMain_rblReportType_4').checked: //FT-F-S-A                                    \n");
                _sbBuffer.Append("              _reportType = 'FT-F-S-A'                                                                                                   \n");
                _sbBuffer.Append("              break;                                                                                          \n");
                _sbBuffer.Append("          case document.getElementById('ctl00_ContentMain_rblReportType_5').checked: //O_S_A_FT_F                                    \n");
                _sbBuffer.Append("              _reportType = 'O_S_A_FT_F'                                                                                                   \n");
                _sbBuffer.Append("              break;                                                                                          \n");
                _sbBuffer.Append("          case document.getElementById('ctl00_ContentMain_rblReportType_6').checked: //Evolution                                    \n");
                _sbBuffer.Append("              _reportType = 'Evolution'                                                                                                   \n");
                _sbBuffer.Append("              break;                                                                                          \n");
                _sbBuffer.Append("      }                                                                                                       \n");

                _sbBuffer.Append("      var _report;                                                                            \n");
                _sbBuffer.Append("      switch (true)                                                                              \n");
                _sbBuffer.Append("      {                                                                                                       \n");
                _sbBuffer.Append("          case document.getElementById('ctl00_ContentMain_rblReport_0').checked: //GEI                                    \n");
                _sbBuffer.Append("              _report = 'GEI'                                                                                                   \n");
                _sbBuffer.Append("              break;                                                                                          \n");
                _sbBuffer.Append("          case document.getElementById('ctl00_ContentMain_rblReport_1').checked: //CL                                    \n");
                _sbBuffer.Append("              _report = 'CL'                                                                                                   \n");
                _sbBuffer.Append("              break;                                                                                          \n");
                _sbBuffer.Append("      }                                                                                                       \n");

                //_sbBuffer.Append("      StopEvent(e);                                                                                   \n");
                _sbBuffer.Append("      if (_BrowserName == _IEXPLORER)                                                             \n");
                _sbBuffer.Append("      {   //IE and Opera                                                                          \n");
                _sbBuffer.Append("          var newWindow = window.open(" + Convert.ToChar(34) + "http://" + Request.ServerVariables["HTTP_HOST"].ToString() + "/Dashboard/ReportCalculationsOfTransformation.aspx?ReportType=" + Convert.ToChar(34) + " + _reportType + '&Report=' + _report + '&IdProcess=' + _idProcess + '&StartDate=' + _startDate + '&EndDate=' + _endDate, 'Reports', 'toolbar=yes,location=yes,resizable=yes,status=yes,menubar=yes,scrollbars=yes');      \n");
                _sbBuffer.Append("      }                                                                                           \n");
                _sbBuffer.Append("      else                                                                                        \n");
                _sbBuffer.Append("      {   //FireFox                                                                               \n");
                _sbBuffer.Append("          var newWindow = window.parent.open(" + Convert.ToChar(34) + "http://" + Request.ServerVariables["HTTP_HOST"].ToString() + "/Dashboard/ReportCalculationsOfTransformation.aspx?ReportType=" + Convert.ToChar(34) + " + _reportType + '&Report=' + _report + '&IdProcess=' + _idProcess + '&StartDate=' + _startDate + '&EndDate=' + _endDate, 'Reports', 'toolbar=yes,location=yes,resizable=yes,status=yes,menubar=yes,scrollbars=yes');      \n");
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
            private void LoadPreFilters()
            {
                //Si no viene el parametro, es que se llamo en forma normal...
                if (!String.IsNullOrEmpty(_Report))
                {
                    //Vino por el Context
                    if (_Report == "GEI")
                    {
                        rblReport.Items[(Int16)MyEnum_rblReport.GEI].Selected = true;
                    }
                    else
                    {
                        rblReport.Items[(Int16)MyEnum_rblReport.CL].Selected = true;
                    }

                    switch (_ReportType)
                    {
                        case "GA_S_A_FT_F":
                            rblReportType.Items[(Int16)MyEnum_rblReportType.GA_S_A_FT_F].Selected = true;
                            break;
                        case "GA_FT_F_S_A":
                            rblReportType.Items[(Int16)MyEnum_rblReportType.GA_FT_F_S_A].Selected = true;
                            break;
                        case "S_GA_A_FT_F":
                            rblReportType.Items[(Int16)MyEnum_rblReportType.S_GA_A_FT_F].Selected = true;
                            break;
                        case "S_A_FT_F":
                            rblReportType.Items[(Int16)MyEnum_rblReportType.S_A_FT_F].Selected = true;
                            break;
                        case "FT_F_S_A":
                            rblReportType.Items[(Int16)MyEnum_rblReportType.FT_F_S_A].Selected = true;
                            break;
                        case "O_S_A_FT_F":
                            rblReportType.Items[(Int16)MyEnum_rblReportType.O_S_A_FT_F].Selected = true;
                            break;
                        case "Evolution":
                            rblReportType.Items[(Int16)MyEnum_rblReportType.Evolution].Selected = true;
                            break;
                    }

                    //Ocultamos las opciones, ya que vinieron por parametro
                    trReport.Style.Add("display", "none");
                    trReportType.Style.Add("display", "none");
                }
                else
                {
                    //Mostramos las opciones, ya que vinieron por parametro
                    trReport.Style.Add("display", "table-row");
                    trReportType.Style.Add("display", "table-row");                    
                }
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
