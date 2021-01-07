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
    public partial class ErrorGeneric : BasePage
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            try
            {
                //TitleIconURL
                FwMasterPage.PageTitleIconURL = "/Skins/Images/Icons/Error.png";

                this.Title = Resources.CommonMenu.ErrorGeneric_Title;
                lblErrorMessage.Text = "Error";

                String _id = Request.QueryString["ID"];
                if (_id == null)
                {
                    if ((Request.QueryString["Message"] == null) || (Request.QueryString["Message"] == ""))
                    {
                        lblErrorMessage.Text = Resources.Common.ErrorUnknown;
                    }
                    else
                    {
                        lblErrorMessage.Text = Request.QueryString["Message"];
                    }
                    //lblErrorMessage.Text = Request.QueryString["Message"];
                }
                else
                {
                    switch (_id)
                    {
                        case "1":
                            lblErrorMessage.Text = Resources.Common.ErrorFileNotFound;
                            break;
                        case "2":
                            lblErrorMessage.Text = Resources.Common.ErrorUnexpected;
                            break;
                        case "3":
                            lblErrorMessage.Text = Resources.Common.ErrorForbiddenAccess;
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = ex.Message;
            }
        }
        protected override void SetPagetitle()
        {
            base.PageTitle = Resources.CommonMenu.ErrorGeneric_Title;
        }
        protected override void SetPageTileSubTitle()
        {
            base.PageTitleSubTitle = Resources.Common.NotUsed;
        }


        //protected override void OnLoad(EventArgs e)
        //{
        //    base.OnLoad(e);

        //    //TitleIconURL
        //    FwMasterPage.PageTitleIconURL = "/Skins/Images/Icons/Error.png";
        //}
        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    this.Title = "EMS - " + Resources.CommonMenu.ErrorGeneric_Title;
        //    lblErrorMessage.Text = "";

        //    String _id = Request.QueryString["ID"];
        //    if (_id == null)
        //    {
        //        if (Request.QueryString["Message"] == "")
        //        {
        //            lblErrorMessage.Text = Resources.Common.ErrorUnknown;
        //        }
        //        else
        //        {
        //            lblErrorMessage.Text = Request.QueryString["Message"];        
        //        }
        //    }
        //    else
        //    {
        //        switch (_id)
        //        {
        //            case "1":
        //                lblErrorMessage.Text = Resources.Common.ErrorFileNotFound;
        //                break;
        //            case "2":
        //                lblErrorMessage.Text = Resources.Common.ErrorUnexpected;
        //                break;
        //            case "3":
        //                lblErrorMessage.Text = Resources.Common.ErrorForbiddenAccess;
        //                break;
        //            default:
        //                lblErrorMessage.Text = Resources.Common.ErrorUnknown;
        //                break;
        //        }
        //    }
        //}
    }
}
