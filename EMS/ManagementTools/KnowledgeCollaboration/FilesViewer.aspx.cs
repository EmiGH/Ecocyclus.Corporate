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

namespace Condesus.EMS.WebUI.ManagementTools.KnowledgeCollaboration
{
    public partial class FilesViewer : BasePage
    {
        #region Internal Properties
        public String swfFileName = "";
        public String mediaFileName = "";
        public String typeFile = "";

        private String _FileNameFlash = String.Empty;
        private Int64 _IdResource
        {
            get { return Convert.ToInt64(ViewState["IdResource"]); }
            set { ViewState["IdResource"] = value; }
        }
        private Int64 _IdResourceFile
        {
            get { return Convert.ToInt64(ViewState["IdResourceFile"]); }
            set { ViewState["IdResourceFile"] = value; }
        }
        #endregion
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Page.Title = Resources.CommonListManage.FileViewer;
            if (!IsPostBack)
            {
                if (Request.QueryString["IdResource"] != null)
                {
                    _IdResource = Convert.ToInt64(Request.QueryString["IdResource"]);
                    _IdResourceFile = Convert.ToInt64(Request.QueryString["IdResourceFile"]);

                }
                try
                {
                    if (EMSLibrary.User.KnowledgeCollaboration.Map.Resource(_IdResource).GetType().Name == "ResourceVersion")
                    {
                        Condesus.EMS.Business.KC.Entities.Version _resourceFile = ((Condesus.EMS.Business.KC.Entities.ResourceVersion)EMSLibrary.User.KnowledgeCollaboration.Map.Resource(_IdResource)).Version(_IdResourceFile);

                        if (_resourceFile.GetType().Name == "VersionDoc")
                        {
                            Condesus.EMS.Business.KC.Entities.VersionDoc _resourceFileDoc = (Condesus.EMS.Business.KC.Entities.VersionDoc)_resourceFile;

                            String _fileName = _resourceFileDoc.FileAttach.FileName;
                            String _fileExtension = _fileName.Substring(_fileName.LastIndexOf(".") + 1);
                            BinaryWriter _binaryWriter = null;
                            FileStream _fileStreamWriter;

                            switch (_resourceFileDoc.DocType)
                            {
                                case "application/x-shockwave-flash":
                                    pnlFlashViewer.Visible = true;
                                    //Arma un nombre unico.
                                    //_FileNameFlash = "flash" + DateTime.Now.ToFileTime().ToString() + ".swf";
                                    //Por defecto lo creamos en el mismo directorio de esta pagina.
                                    _fileStreamWriter = new FileStream(Condesus.EMS.WebUI.Global._ServerPathMediaFiles, FileMode.Create);
                                    _binaryWriter = new BinaryWriter(_fileStreamWriter);
                                    _binaryWriter.Write(_resourceFileDoc.FileAttach.FileStream);
                                    _fileStreamWriter.Close();

                                    //Esto lo setea para que lo tome el embedded de la pagina.
                                    swfFileName = "./" + Session.SessionID.ToString() + "_flash.swf";    // +_FileNameFlash;
                                    break;

                                case "video/x-ms-wmv":
                                case "audio/mpeg":
                                case "audio/x-ms-wma":
                                case "application/octet-stream":
                                    pnlMediaViewer.Visible = true;
                                    //Arma un nombre unico.
                                    //_FileNameFlash = "flash" + DateTime.Now.ToFileTime().ToString() + ".swf";
                                    //Por defecto lo creamos en el mismo directorio de esta pagina.
                                    _fileStreamWriter = new FileStream(Condesus.EMS.WebUI.Global._ServerPathMediaFiles + Session.SessionID.ToString() + "_media.wmv", FileMode.Create);
                                    _binaryWriter = new BinaryWriter(_fileStreamWriter);
                                    _binaryWriter.Write(_resourceFileDoc.FileAttach.FileStream);
                                    _fileStreamWriter.Close();

                                    _fileStreamWriter = new FileStream(Condesus.EMS.WebUI.Global._ServerPathMediaFiles + Session.SessionID.ToString() + "_media.smi", FileMode.Create);
                                    _binaryWriter = new BinaryWriter(_fileStreamWriter);
                                    _binaryWriter.Write(_resourceFileDoc.FileAttach.FileStream);
                                    _fileStreamWriter.Close();


                                    mediaFileName = Session.SessionID.ToString() + "_media.wmv";
                                    typeFile = _resourceFileDoc.DocType;
                                    //slStreamingMedia.MediaSource = "./video.wma";
                                    break;

                                default:
                                    Response.Clear();
                                    Response.Cache.SetCacheability(HttpCacheability.Public);
                                    Response.Cache.SetLastModified(DateTime.Now);
                                    Response.Buffer = true;
                                    Response.ContentType = _resourceFileDoc.DocType;
                                    Response.AddHeader("Content-Length", _resourceFileDoc.DocSize.ToString());
                                    Response.AddHeader("Content-Type", _resourceFileDoc.DocType);
                                    Response.AddHeader("Content-Disposition", "filename=" + DateTime.Now.ToString() + "." + _fileExtension);

                                    Response.BinaryWrite(_resourceFileDoc.FileAttach.FileStream);
                                    break;
                            }


                            //if (_resourceFileDoc.DocType == "application/x-shockwave-flash")
                            //{
                            //    //Arma un nombre unico.
                            //    //_FileNameFlash = "flash" + DateTime.Now.ToFileTime().ToString() + ".swf";
                            //    FileStream _fileStreamWriter;
                            //    //Por defecto lo creamos en el mismo directorio de esta pagina.
                            //    _fileStreamWriter = new FileStream(Server.MapPath("~/ManagementTools/KnowledgeCollaboration/flash.swf"), FileMode.Create);
                            //    BinaryWriter _binaryWriter = new BinaryWriter(_fileStreamWriter);
                            //    _binaryWriter.Write(_resourceFileDoc.FileAttach.FileStream);
                            //    _fileStreamWriter.Close();

                            //    //Esto lo setea para que lo tome el embedded de la pagina.
                            //    swfFileName = "./flash.swf";    // +_FileNameFlash;
                            //}
                            //else
                            //{
                            //    if (_resourceFileDoc.DocType == "application/octet-stream")
                            //    {
                            //        //Arma un nombre unico.
                            //        //_FileNameFlash = "flash" + DateTime.Now.ToFileTime().ToString() + ".swf";
                            //        FileStream _fileStreamWriter;
                            //        //Por defecto lo creamos en el mismo directorio de esta pagina.
                            //        _fileStreamWriter = new FileStream(Server.MapPath("~/ManagementTools/KnowledgeCollaboration/video.flv"), FileMode.Create);
                            //        BinaryWriter _binaryWriter = new BinaryWriter(_fileStreamWriter);
                            //        _binaryWriter.Write(_resourceFileDoc.FileAttach.FileStream);
                            //        _fileStreamWriter.Close();

                            //        //Esto lo setea para que lo tome el embedded de la pagina.
                            //        swfFileName = "./video.flv";    // +_FileNameFlash;
                            //    }
                            //    else
                            //    {
                            //        Response.Clear();
                            //        Response.Cache.SetCacheability(HttpCacheability.Public);
                            //        Response.Cache.SetLastModified(DateTime.Now);
                            //        Response.Buffer = true;
                            //        Response.ContentType = _resourceFileDoc.DocType;
                            //        Response.AddHeader("Content-Length", _resourceFileDoc.DocSize.ToString());
                            //        Response.AddHeader("Content-Type", _resourceFileDoc.DocType);
                            //        Response.AddHeader("Content-Disposition", "filename=" + DateTime.Now.ToString() + "." + _fileExtension);

                            //        Response.BinaryWrite(_resourceFileDoc.FileAttach.FileStream);
                            //    }
                            //}
                        }
                    }
                    else
                    {
                        Condesus.EMS.Business.KC.Entities.Catalog _resourceFile = ((Condesus.EMS.Business.KC.Entities.ResourceCatalog)EMSLibrary.User.KnowledgeCollaboration.Map.Resource(_IdResource)).Catalog(_IdResourceFile);

                        if (_resourceFile.GetType().Name == "CatalogDoc")
                        {
                            Condesus.EMS.Business.KC.Entities.CatalogDoc _resourceFileDoc = (Condesus.EMS.Business.KC.Entities.CatalogDoc)_resourceFile;
                            //_resourceFile.DocType;
                            //_resourceFile.FileAttach.FileStream;

                            String _fileName = _resourceFileDoc.FileAttach.FileName;
                            String _fileExtension = _fileName.Substring(_fileName.LastIndexOf(".") + 1);

                            Response.Clear();
                            Response.Buffer = true;
                            Response.ContentType = _resourceFileDoc.DocType;
                            Response.AddHeader("Content-Type", _resourceFileDoc.DocType);
                            Response.AddHeader("Content-Disposition", "filename=" + DateTime.Now.ToString() + "." + _fileExtension);

                            //Response.BinaryWrite(CType(dr("Contenido"), Byte()))
                            Response.BinaryWrite(_resourceFileDoc.FileAttach.FileStream);

                        }
                        //else
                        //{
                        //    Condesus.EMS.Business.KC.Entities.ResourceFileURL _resourceFileURL = (Condesus.EMS.Business.KC.Entities.ResourceFileURL)_resourceFile;

                        //    _resourceFileURL.Url;

                        //}


                    }
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
