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

namespace Condesus.EMS.WebUI
{
    public partial class ShowFileAudit : BasePage
    {
        #region Internal Properties
            public String swfFileName = "";
            public String mediaFileName = "";
            public String typeFile = "";
            private String _FileNameFlash = String.Empty;

            private Int64 _IdProcess
            {
                get { return Convert.ToInt64(ViewState["IdProcess"]); }
                set { ViewState["IdProcess"] = value; }
            }
            private String _FileNameAudit
            {
                get { return Convert.ToString(ViewState["FileNameAudit"]); }
                set { ViewState["FileNameAudit"] = value; }
            }
        #endregion

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Page.Title = Resources.Common.AuditPlan;
            if (!IsPostBack)
            {
                if (Request.QueryString["IdProcess"] != null)
                {
                    _IdProcess = Convert.ToInt64(Request.QueryString["IdProcess"]);
                    _FileNameAudit = Convert.ToString(Request.QueryString["FileNameAudit"]);
                }
                try
                {
                    String _path = Server.MapPath("~/AuditPlan");
                    String _filePathName = _path + "\\" + _FileNameAudit;

                    FileStream _fileStream = new FileStream(_filePathName, FileMode.Open);

                    Byte[] _fileBinary = new Byte[Convert.ToInt32(_fileStream.Length)];
                    _fileStream.Read(_fileBinary, 0, Convert.ToInt32(_fileStream.Length));

                    Response.Clear();
                    Response.Cache.SetCacheability(HttpCacheability.Public);
                    Response.Cache.SetLastModified(DateTime.Now);
                    Response.Buffer = true;
                    //application/octet-stream
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("Content-Length", _fileStream.Length.ToString());
                    Response.AddHeader("Content-Type", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

                    String _fileExtension = System.IO.Path.GetExtension(_filePathName);

                    Response.AddHeader("Content-Disposition", "filename=" + _FileNameAudit);
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
