using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;
using System.IO;
using System.Linq;
using System.ComponentModel;
using System.Globalization;
using System.Threading;
using System.Drawing;
using Telerik.Charting;
using Condesus.EMS.WebUI.MasterControls;
using Condesus.EMS.Business.PF.Entities;
using Condesus.EMS.Business.PA.Entities;

namespace Condesus.EMS.WebUI.PM
{
    public partial class ShowUploadAuditPlanProperties : BaseProperties
    {
        #region Internal Properties
            private ConfigurationExcelFile _Entity = null;
            private Int64 _IdProcess
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdProcess") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdProcess")) : Convert.ToInt64(GetPKfromNavigator("IdProcess"));
                }
            }
            private String _FileNameAudit
            {
                get
                {
                    return base.NavigatorContainsTransferVar("FileNameAudit") ? Convert.ToString(base.NavigatorGetTransferVar<Object>("FileNameAudit")) : Convert.ToString(GetPKfromNavigator("FileNameAudit"));
                }
            }
        #endregion

        #region PageLoad & Init
            protected override void InitializeHandlers()
            {
                base.InitializeHandlers();

                //if ((Entity == null) || (Entity.GetType().Name != Common.ConstantsEntitiesName.PF.ProcessTaskExecutionMeasurement))
                //{
                    //Le paso el Save a la MasterContentToolbar
                    EventHandler _saveEventHandler = new EventHandler(btnSave_Click);
                    //esta pagina, tiene un FileUpload, entonces el boton save, tiene que ser PostbackTrigger
                    FwMasterPage.ContentNavigatorToolbarFileActionAdd(_saveEventHandler, MasterFwContentToolbarAction.Save, false);
                //}

                    //lnkShowFile.Click += new EventHandler(lnkShowFile_Click);
            }

            protected void lnkShowFile_Click(object sender, EventArgs e)
            {
                String[] _files;
                String _path = Server.MapPath("~/AuditPlan");
                //'Leyendo los archivos de la carpeta
                //_files = System.IO.Directory.GetFiles(_path + "\\", _FileNameAudit);

                String _filePathName = _path + "\\" + _FileNameAudit;

                //if (_files.Length > 0)
                //{

                    //Stream _fileStream;

                    //Obtiene el texto
                    //StreamReader _streamReaderFileMeasurement = new StreamReader(_filePathName);

                    FileStream _fileStream = new FileStream(_filePathName, FileMode.Open);

                    Byte[] _fileBinary = new Byte[Convert.ToInt32(_fileStream.Length)];
                    _fileStream.Read(_fileBinary, 0, Convert.ToInt32(_fileStream.Length));
                    
                    Response.Clear();
                    Response.Cache.SetCacheability(HttpCacheability.Public);
                    Response.Cache.SetLastModified(DateTime.Now);
                    Response.Buffer = true;
                    Response.ContentType = "application/excel";
                    Response.AddHeader("Content-Length", _fileStream.Length.ToString());
                    Response.AddHeader("Content-Type", "application/excel");
                    
                    String _fileExtension = System.IO.Path.GetExtension(_filePathName);

                    //Response.AddHeader("Content-Disposition", "filename=" + DateTime.Now.ToString() +  _fileExtension);
                    Response.AddHeader("Content-Disposition", "filename=" + _FileNameAudit);
                    Response.BinaryWrite(_fileBinary);

//FileSize = objStream.Length

//Dim Buffer(CInt(FileSize)) As Byte

//objStream.Read(Buffer, 0, CInt(FileSize))

//objStream.Close() 
//Response.AppendHeader("content-disposition", "attachment; filename=imagen.jpg")

//Response.BinaryWrite(Buffer)

//Response.End




                    //String _file = _streamReaderFileMeasurement.ReadToEnd();
                    //_fileStream = _streamReaderFileMeasurement.BaseStream;  // fileUploadMeasurement.FileContent;

                    //Obtiene el binario
                    //Byte[] _fileBinary = fileUploadMeasurement.FileBytes;
                    //_fileStream.Read(_fileBinary, 0, Convert.ToInt32(_fileStream.Length));


                    //Response.Clear();
                    //Response.Cache.SetCacheability(HttpCacheability.Public);
                    //Response.Cache.SetLastModified(DateTime.Now);
                    //Response.Buffer = true;
                    //Response.ContentType = "application/excel";
                    //Response.AddHeader("Content-Length", _fileStream.Length.ToString());
                    //Response.AddHeader("Content-Type", "application/excel");
                    //Response.AddHeader("Content-Disposition", "filename=" + DateTime.Now.ToString() + "." + _fileExtension);

                    //Response.BinaryWrite(_fileBinary);
                //}
            }
            protected override void OnInit(EventArgs e)
            {
                base.OnInit(e);
                LoadTextLabels();
                lnkShowFile.OnClientClick = "return ShowFileAudit(this, " + _IdProcess + ", '" + _FileNameAudit + "');";
                //lnkShowFile.Attributes.Add("onclick", "javascript:ShowFileAudit(this, " + _IdProcess + ", " + _FileNameAudit + ");");
            }
            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);


                if (!Page.IsPostBack)
                {
                    LoadData();
                    //Form
                    base.SetContentTableRowsCss(tblContentFormUploadFile);
                }
            }
            protected override void SetPagetitle()
            {
                base.PageTitle = Resources.Common.AuditPlan;
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }
        #endregion

        #region Private Methods
            private void LoadTextLabels()
            {
                Page.Title = Resources.Common.AuditPlan;
                lblFile.Text = Resources.CommonListManage.FileUploadMeasurements;
                lblName.Text = Resources.CommonListManage.Name;
                rfv1.ErrorMessage = Resources.ConstantMessage.ValidationUploadFile;
                revFileUpload.ErrorMessage = Resources.ConstantMessage.FileTypeNotAllowed;
            }
            private void ShowUploadFile()
            {
                tblContentFormUploadFile.Style.Add("display", "block");
                rfv1.Enabled = true;
            }
            private void LoadData()
            {
                String _path = Server.MapPath("~/AuditPlan");
                String[] _files;

                //El nombre del archivo puede no venir, pero si existe algo para ese process!
                if (String.IsNullOrEmpty(_FileNameAudit))
                {
                    //'Leyendo todos los archivos de la carpeta que pertenezcan al process indicado
                    _files = System.IO.Directory.GetFiles(_path + "\\", _IdProcess.ToString() + "_*.xls*");
                }
                else
                {
                    //'Leyendo el archivo de la carpeta ya indicado...
                    _files = System.IO.Directory.GetFiles(_path + "\\", _FileNameAudit);
                }

                if (_files.Length > 0)
                {
                    String _name = System.IO.Path.GetFileName(_files[0]);
                    lblNameValue.Text = _name.Replace(_IdProcess.ToString() + "_", "");
                }
                else
                {
                    lblNameValue.Text = Resources.Common.NotUsed;
                    lnkShowFile.Attributes.Add("display", "none");
                }
            }
        #endregion

        #region Page Events
            protected void btnSave_Click(object sender, EventArgs e)
            {
                try
                {
                    String _pathBulkLoad = "AuditPlan";

                    //Teniendo el archivo, debemos guardarlo en un directorio del servidor, para que luego la libreria se encargue de leerlo y subir la info a las mediciones...
                    if (fileUploadMeasurement.HasFile)
                    {

                        //1° Borramos el Excel que ya existe para ese Process
                        //AuditPlan
                        String[] _files;
                        String _path = Server.MapPath("~/AuditPlan");
                        //'Leyendo los archivos de la carpeta ‘C:\Musica’
                        _files = System.IO.Directory.GetFiles(_path + "\\", _IdProcess.ToString() + "_*.xls*");

                        foreach (String _file in _files)
                        {
                            File.Delete(_file);
                        }

                        //2° ahora guardamos el nuevo archivo que el usuario esta cargando para ese proceso.

                        //Al nombre real del archivo le concatenamos "BL" y la fecha y hora de carga (Ejemplo BL20110811182833_nombreoriginal.xls)
                        String _uniqueName = _IdProcess.ToString() + "_";
                        _pathBulkLoad = Request.PhysicalApplicationPath + _pathBulkLoad + "\\";
                        String _fileName = _uniqueName + fileUploadMeasurement.FileName;
                        //Graba el archivo en el servidor
                        fileUploadMeasurement.SaveAs(_pathBulkLoad + _fileName);

                        //3° vamos para la pagina anterior...
                        base.NavigateBack();
                    }

                    base.StatusBar.ShowMessage(Resources.Common.SaveOK);
                }
                catch (Exception ex)
                {
                    base.StatusBar.ShowMessage(ex);
                }
            }
        #endregion
    }
}