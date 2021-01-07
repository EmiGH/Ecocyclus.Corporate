using System;
using System.Collections;
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

namespace Condesus.EMS.WebUI
{
    public partial class EMSPopUpReport : System.Web.UI.MasterPage
    {
        private Condesus.EMS.Business.EMS _EMSLibrary;
        public Condesus.EMS.Business.EMS EMSLibrary
        {
            get { return _EMSLibrary; }
            set { _EMSLibrary = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    _EMSLibrary = new Condesus.EMS.Business.EMS(Convert.ToString(Session["Username"]), Convert.ToString(Session["Password"]), Convert.ToString(Session["CurrentLanguage"]), Convert.ToString(Session["IPAddress"]));
                }
                catch (Exception ex)
                {
                    Context.Items.Add("Error", ex);
                    Server.Transfer("~/ErrorLogged.aspx");
                }
            }
        }


        public String HeaderProductName
        {
            set { lblProductName.Text = value; }
        }

        public String HeaderTecnicalSupportText
        {
            set { lblTechnicalSupportDescription.Text = value; }
        }

  
    }
}
