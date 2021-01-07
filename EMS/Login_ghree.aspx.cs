using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Threading;
using System.Globalization;
using Telerik.Web.UI;
using Condesus.EMS.WebUI.wsGhreeLicense;

namespace Condesus.EMS.WebUI
{
    public partial class Login_ghree : BasePage
    {
        #region Internal Properties
        #endregion

        #region PageLoad & Init
            protected override void OnInit(EventArgs e)
            {
                base.OnInit(e);
            }
            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);

                try
                {
                    if (!Page.IsPostBack)
                    {
                        //Me aseguro de limpiar todo antes de empezar....
                        Session.Clear();
                        Session.Abandon();
                        FormsAuthentication.SignOut();
                        //Add click event to make postback
                        btnLogin.Attributes.Add("onclick", string.Format("realPostBack(\"{0}\", \"\"); return false;", btnLogin.UniqueID));
                    }
                    Page.SetFocus(txtUserName);
                    LoadLanguage();
                    LoadTextLabels();
                    this.Title = Resources.CommonMenu.Login_Title;   
                    //this.Title = "EMS - " + Resources.CommonMenu.Login_Title;
                }
                catch (Exception ex)
                {
                    //Response.Write(ex.Message);
                    logindivMessenger.CssClass = "logindivMessenger";
                    lblFailureText.CssClass = "Label";
                    lblFailureText.Text = ex.Message;
                    txtFullNameh.Text = "";
                }
            }
        #endregion

        #region Private Method
            private void LoadLanguage()
            {
                try
                {
                    String _idLanguage = String.Empty;
                    LinkButton _lnkLanguage = new LinkButton();
                    foreach (Condesus.EMS.Business.DS.Entities.Language _language in Global.Languages.Values)
                    {
                        _lnkLanguage = new LinkButton();
                        _lnkLanguage.Text = _language.Name;
                        _lnkLanguage.ID = "lnk" + _language.IdLanguage;
                        _lnkLanguage.Attributes.Add("value", _language.IdLanguage);
                        if (_language.IsDefault)
                        {
                            if (Session["CurrentLanguage"] == null)
                            {
                                //Recuperamos la Cookie
                                HttpCookie userCultureCookie = Request.Cookies["UserSelectedCulture"];
                                if (userCultureCookie != null)
                                {
                                    _idLanguage = userCultureCookie.Value;

                                }
                                else
                                {
                                    _idLanguage = _language.IdLanguage;
                                }
                                Session["CurrentLanguage"] = _idLanguage;
                                SetCultureLogin(_idLanguage);
                            }
                        }
                        _lnkLanguage.Click += new EventHandler(_lnkLanguage_Click);
                        pnlContentLanguege.Controls.Add(_lnkLanguage);
                    }


                    LinkButton _lnk = (LinkButton)Page.FindControl("lnk" + _idLanguage);
                    if (_lnk != null)
                    {
                        _lnk.CssClass = "selected";
                    }
                }
                catch (Exception ex)
                {
                    logindivMessenger.CssClass = "logindivMessenger";
                    lblFailureText.CssClass = "Label";
                    lblFailureText.Text = ex.Message;
                    txtFullNameh.Text = "";
                }
            }
            private void LoadTextLabels()
            {
                lblUser.Text = Resources.Common.UserName;
                lblPassword.Text = Resources.Common.Password;
                ckRememberUser.Text = Resources.Common.RememberUser;
                lnkForgetPassword.Text = Resources.Common.ForgetPassword;
                lblTitleRegister.Text = Resources.Common.TitleRegister;
                txtFullName.Text = Resources.Common.FullName;
                txtEmail.Text = Resources.Common.Email;
                txtPasswordRegister.Text = Resources.Common.Password;
                btnRegister.Text = Resources.Common.Register;
                lblSlogan.Text = Resources.Common.SloganLogin;
                lblGhreeCopyRight.Text = Resources.MasterFW.lblFooter;


                // Footer //

                lnkPrivacy.Text = Resources.Common.lnkPrivacy;
                lnkTerms.Text = Resources.Common.lnkTerms;
                lnkHelpdesk.Text = Resources.Common.lnkHelpdesk;

            }
            private void SetCultureLogin(String strCulture)
            {
                //Seteo la cultura en USA, para poder trabajar con la fecha en MM/DD/YYYY HH:mm:ss
                CultureInfo _culture = new CultureInfo(strCulture);
                //Seta la cultura Seleccionada
                Thread.CurrentThread.CurrentCulture = _culture;
                Thread.CurrentThread.CurrentUICulture = _culture;

                #region Guardamos una Cookie con el lenguaje del usuario
                    HttpCookie userCultureCookie = Request.Cookies["UserSelectedCulture"];
                    if (userCultureCookie != null)
                    {
                        Request.Cookies.Remove("UserSelectedCulture");
                    }
                    // Creamos elemento HttpCookie con su nombre y su valor
                    HttpCookie addCookie = new HttpCookie("UserSelectedCulture", strCulture);
                    addCookie.Expires = DateTime.Now.AddMonths(1);
                    // Y finalmente ñadimos la cookie a nuestro usuario
                    Response.Cookies.Add(addCookie);
                #endregion
            }
        #endregion

        #region Page Event
            protected void _lnkLanguage_Click(object sender, EventArgs e)
            {
                try
                {
                    String _strCulture = ((LinkButton)sender).Attributes["value"];
                    Session["CurrentLanguage"] = _strCulture;

                    foreach (Condesus.EMS.Business.DS.Entities.Language _language in Global.Languages.Values)
                    {
                        LinkButton _lnk = (LinkButton)Page.FindControl("lnk" + _language.IdLanguage);
                        _lnk.CssClass = "";
                    }

                    ((LinkButton)sender).CssClass = "selected";

                    SetCultureLogin(_strCulture);
                    LoadTextLabels();
                }
                catch (Exception ex)
                {
                    logindivMessenger.CssClass = "logindivMessenger";
                    lblFailureText.CssClass = "Label";
                    lblFailureText.Text = ex.Message;
                    txtFullNameh.Text = "";
                }
            }
            protected void LoginButton_Click(object sender, EventArgs e)
            {
                try
                {
                    //POR AHORA LO QUITO PORQUE ME TRAE PROBLEMAS EN EL NUEVO SERVIDOR DE AZURE...
                    //String _domain = Request.ServerVariables["HTTP_HOST"].ToString();
                    //Condesus.EMS.WebUI.wsGhreeLicense.wsGhreeLicenseSoapClient _wsGhreeLicense = new wsGhreeLicenseSoapClient();
                    //if (_wsGhreeLicense.ImplementationIsEnable(_domain))
                    //{
                    Session["Username"] = txtUserName.Text;
                    Session["Password"] = txtPassword.Text;
                    ////Session["CurrentLanguage"] = ddlLanguage.SelectedValue;
                    Session["IPAddress"] = Request.UserHostAddress;

                    Condesus.EMS.Business.EMS _EMS = new Condesus.EMS.Business.EMS(Convert.ToString(Session["Username"]), Convert.ToString(Session["Password"]), Convert.ToString(Session["CurrentLanguage"]), Convert.ToString(Session["IPAddress"]));
                    FormsAuthentication.RedirectFromLoginPage(_EMS.User.Username, false);
                    //}
                    //else
                    //{
                    //    throw new Exception(Resources.ConstantMessage.LicensePeriodIsExpired);
                    //}
                }
                catch (Exception ex)
                {
                    logindivMessenger.CssClass = "logindivMessenger";
                    //imgFailureImage.CssClass = "Images";
                    lblFailureText.CssClass = "Label";
                    lblFailureText.Text = ex.Message;
                    txtFullNameh.Text = "";
                    Page.SetFocus(txtPassword);
                }
            }
        #endregion

    }
}
