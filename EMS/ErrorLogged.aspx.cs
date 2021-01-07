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

namespace Condesus.EMS.WebUI
{
    public partial class ErrorLogged : BasePage
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //TitleIconURL
            FwMasterPage.PageTitleIconURL = "/Skins/Images/Icons/Error.png";
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "EMS - " + Resources.CommonMenu.ErrorLogged_Title;
            if (!Page.IsPostBack)
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Exception objError = (Exception)Context.Items["Error"];
                lblErrorMessage.Text = objError.Message;
                lblExtendedInformation.Text = objError.ToString();

            }
        }
    }
}
