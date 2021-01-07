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
using System.Web.Configuration;
using System.Threading;
using System.Globalization;
using Telerik.Web.UI;

namespace Condesus.EMS.WebUI
{
    //En esta clase se encuentran los eventos del ciclo de vida de una pagina.
    public partial class BasePage : System.Web.UI.Page
    {
        #region Load & Init
            protected override void OnPreInit(EventArgs e)
            {
                try
                {
                    if (Session["CurrentLanguage"] != null)
                    {
                        if (Session["CurrentLanguage"].ToString() != "")
                        {
                            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(Session["CurrentLanguage"].ToString());
                            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Session["CurrentLanguage"].ToString());
                        }
                    }
                    else
                    {
                        if (Request.UserLanguages != null)
                        {
                            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(Request.UserLanguages[0]);
                            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Request.UserLanguages[0]);
                        }
                    }

                    //Set page theme according to user's browser
                    HttpBrowserCapabilities _Browser = Request.Browser;
                    switch (_Browser.Browser)
                    {
                        case "IE":

                            switch (_Browser.MajorVersion)
                            {
                                case 6:
                                    Page.Theme = (String)ConfigurationSettings.AppSettings["ThemeIE6"];
                                    break;
                                case 7:
                                    Page.Theme = (String)ConfigurationSettings.AppSettings["ThemeIE7"];
                                    break;
                                case 8:
                                    Page.Theme = (String)ConfigurationSettings.AppSettings["ThemeIE8"];
                                    break;
                                default:
                                    Page.Theme = (String)ConfigurationSettings.AppSettings["ThemeIEDefault"];
                                    break;
                            }
                            break;

                        case "Firefox":
                            Page.Theme = (String)ConfigurationSettings.AppSettings["ThemeFirefox"];
                            break;
                        default:
                            Page.Theme = (String)ConfigurationSettings.AppSettings["ThemeOther"];
                            break;
                    }

                }
                catch (ArgumentException)
                {
                }
                base.OnPreInit(e);
            }
            protected override void OnInit(EventArgs e)
            {
                //Facade access to business
                if (!Request.Url.LocalPath.Contains("/Login_ghree.aspx"))
                {
                    String _masterName = this.Master.GetType().Name.ToLower();

                    //_EMSLibrary = new Condesus.EMS.Business.EMS(Convert.ToString(Session["Username"]), Convert.ToString(Session["Password"]), Convert.ToString(Session["CurrentLanguage"]), Convert.ToString(Session["IPAddress"]));
                    if (EMSLibrary == null)
                    {
                        EMSLibrary = new Condesus.EMS.Business.EMS(Convert.ToString(Session["Username"]), Convert.ToString(Session["Password"]), Convert.ToString(Session["CurrentLanguage"]), Convert.ToString(Session["IPAddress"]));
                    }
                    try
                    {
                        switch (_masterName)
                        {
                            //case "emsold_master":
                            //    ((Condesus.EMS.WebUI.EMSOld)this.Master).EMSLibrary = EMSLibrary;// _EMSLibrary;
                            //    break;
                            //case "emstest_master":
                            //    ((Condesus.EMS.WebUI.EMSTest)this.Master).EMSLibrary = EMSLibrary;  // _EMSLibrary;
                            //    break;
                            case "emspopup_master":
                                ((Condesus.EMS.WebUI.EMSPopUp)this.Master).EMSLibrary = EMSLibrary; // _EMSLibrary;
                                break;
                            case "emspopupreport_master":
                                ((Condesus.EMS.WebUI.EMSPopUpReport)this.Master).EMSLibrary = EMSLibrary; // _EMSLibrary;
                                break;
                        }
                    }
                    catch (InvalidCastException) { }
                }
                base.OnInit(e);

                InjectGlobalUpdateProgress();
                //Inyecta la funcion JS que verifica si el panel de menu general, esta docking, entonces cambia el class del boton.
                InjectSetClassButtonModule();
                //Inyecta la funcion JS que verifica los Validator de la pagina, esta funcion es global para el sistema. (cualquiera lo puede usar.)
                InjectCheckClientValidatorPage();
                if ((!Request.Url.LocalPath.Contains("/Login_ghree.aspx")) && (this.Master.GetType().Name.ToLower() == "ems_master"))
                {
                    FwMasterPage.OnNavigation += new Condesus.EMS.WebUI.MasterControls.Navigate(FwMasterPage_OnNavigation);
                }
            }
            protected override void OnLoad(EventArgs e)
            {
                //Registramos el evento para resolver el SiteMapPath
                //SiteMap.SiteMapResolve += new SiteMapResolveEventHandler(this.OnSiteMapResolve);
                base.OnLoad(e);

                //Llama a la funcion que se encarga de recorrer los controles de la pagina y le pone el tooltip a los Labels.
                BuildHelperInterface();
                if ((!Request.Url.LocalPath.Contains("/Login_ghree.aspx")) && (this.Master.GetType().Name.ToLower() == "ems_master"))
                {
                    if (!this.Page.IsPostBack)
                    {
                        //Inicializa las variables del Menu
                        LoadMenuState();
                        //Inicializa los valores (x medio de Recursos, Config(AppSettings) y Libreria) de los Controles del Marco del MasterFW
                        InitFwContent();
                    }
                    //Arma Toolbars y Menues
                    BuildGlobalToolbar();
                    BuildGlobalMenu();
                    BuildContentMenu();

                    ImageButton _btnGlobalToolbarHome = (ImageButton)FwMasterPage.FindControl("btnGlobalToolbarHomeDashboardFW");
                    PersistControlState(_btnGlobalToolbarHome);

                    //if (IsOperator() && IsOperatorOnly())
                    if (IsOperator() && !EMSLibrary.User.ViewGlobalMenu)
                    {
                        _btnGlobalToolbarHome.Attributes.Add("display", "none");
                        _btnGlobalToolbarHome.Visible = false;
                    }

                    //No es Operador, entonces ocultamos el Boton del Dashboard Operativo!!
                    if (!IsOperator())
                    {   
                        ImageButton _btnGlobalToolbarOperativeDashboard = (ImageButton)FwMasterPage.FindControl("btnGlobalToolbarOperativeDashboardFW");
                        _btnGlobalToolbarOperativeDashboard.Attributes.Add("display", "none");
                        _btnGlobalToolbarOperativeDashboard.Visible = false;
                    }


                    //Arma el BreadCrumb
                    BuildBreadCrumb();
                    //Arma ContextInfoPath (Entidades Ppales)
                    BuildContextInfoPath();

                    //Setea el Titulo y el Subtitulo
                    SetPagetitle();
                    SetPageTileSubTitle();
                    //Arma el Return Button del Wizzard
                    InitNavigatorWizzard();
                }
            }

            protected override void OnLoadComplete(EventArgs e)
            {
                base.OnLoadComplete(e);
                if ((!Request.Url.LocalPath.Contains("/Login_ghree.aspx")) && (this.Master.GetType().Name.ToLower() == "ems_master"))
                {
                    if (!this.Page.IsPostBack)
                    {
                        LoadContentState();
                        //HandleNavigatorPageTitle();
                    }
                }
            }
            protected override void OnUnload(EventArgs e)
            {
                base.OnUnload(e);

                ManageEntityParams = new Dictionary<String, Object>();

                if ((this.Master != null) && (this.Master.GetType().Name.ToLower() == "ems_master"))
                {
                    //Si va a navegar a otra Pagina
                    //Los TransferenceVars las setea cada Pagina individualmentem ya que ese estado es propio y Es Pre "Navigate()", es decir, del Current
                    //La pesistencia del Content Tambien es Sobre el Current (PreviousHistory x que ya Navego)
                    //La persistencia del Contexto del Menu es sobre la Pagina a la cual se navega, ya que, el estado del menu en ese momento, esta ligado a la pagina a la cual navegar
                    if (HttpContext.Current.Response.IsRequestBeingRedirected)
                    {
                        PersistFormContentState();
                    }
                }
            }
        #endregion

    }
}
