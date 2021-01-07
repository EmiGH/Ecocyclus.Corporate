using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.IO;
using Condesus.EMS.Business.PF.Entities;

namespace Condesus.EMS.WebUI
{
    public partial class ShowFileAttach : BasePage
    {
        #region Internal Properties
            private Int64 _IdProcess
            {
                get { return Convert.ToInt64(ViewState["IdProcess"]); }
                set { ViewState["IdProcess"] = value; }
            }
            private Int64 _IdTask
            {
                get { return Convert.ToInt64(ViewState["IdTask"]); }
                set { ViewState["IdTask"] = value; }
            }
            private Int64 _IdExecution
            {
                get { return Convert.ToInt64(ViewState["IdExecution"]); }
                set { ViewState["IdExecution"] = value; }
            }
        #endregion

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Page.Title = Resources.CommonListManage.Attachments;
            if (!IsPostBack)
            {
                if (Request.QueryString["IdProcess"] != null)
                {
                    _IdProcess = Convert.ToInt64(Request.QueryString["IdProcess"]);
                    _IdTask = Convert.ToInt64(Request.QueryString["IdTask"]);
                    _IdExecution = Convert.ToInt64(Request.QueryString["IdExecution"]);
                }
                try
                {
                    ProcessTask _processTask = (ProcessTask)EMSLibrary.User.ProcessFramework.Map.Process(_IdTask);
                    ProcessTaskExecution _processTaskExecution = _processTask.ProcessTaskExecution(_IdExecution);
                    Byte[] _fileBinary = _processTaskExecution.Attachment;
                    String _fileName = DateTime.Now.ToString();
                    if (_processTaskExecution.Comment.Contains("|FileName="))
                    {
                        _fileName = _processTaskExecution.Comment.Substring(_processTaskExecution.Comment.IndexOf("|FileName=")).Replace("|FileName=", String.Empty);
                    }

                    Response.Clear();
                    Response.Cache.SetCacheability(HttpCacheability.Public);
                    Response.Cache.SetLastModified(DateTime.Now);
                    Response.Buffer = true;
                    Response.ContentType = "application/octet-stream";
                    Response.AddHeader("Content-Length", _fileBinary.Length.ToString());
                    Response.AddHeader("Content-Type", "application/octet-stream");
                    Response.AddHeader("Content-Disposition", "filename=" + _fileName);

                    Response.BinaryWrite(_fileBinary);
                }
                catch
                {
                    //Si encuentra un error al cargar los Resources...por defecto muestra la imagen "Sin Imagen"
                    Response.WriteFile("~/Skins/Images/NoImagesAvailable.gif");
                }
            }
        }
    }
}
