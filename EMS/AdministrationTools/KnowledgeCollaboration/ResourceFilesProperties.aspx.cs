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
using Condesus.EMS.WebUI.MasterControls;
using Condesus.EMS.Business.KC.Entities;

namespace Condesus.EMS.WebUI.ManagementTools.KnowledgeCollaboration
{
    public partial class ResourceFilesProperties : BaseProperties
    {
        #region Internal Properties
            private Condesus.EMS.Business.KC.Entities.Version _Entity = null;
            private Int64 _IdResource
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdResource") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdResource")) : Convert.ToInt64(GetPKfromNavigator("IdResource"));
                }
            }
            private Int64 _IdResourceFile
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdResourceFile") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdResourceFile")) : 0;
                }
            }
            private Condesus.EMS.Business.KC.Entities.Version Entity
            {
                get
                {
                    try
                    {
                        if (_Entity == null)
                            _Entity = ((Condesus.EMS.Business.KC.Entities.ResourceVersion)EMSLibrary.User.KnowledgeCollaboration.Map.Resource(_IdResource)).Version(_IdResourceFile);

                        return _Entity;
                    }
                    catch { return null; }
                }
                set { _Entity = value; }
            }
        #endregion

        #region PageLoad & Init
            protected override void InitializeHandlers()
            {
                base.InitializeHandlers();

                //No se puede modificar...solo agregar...
                if (Entity == null)
                {
                    //Le paso el Save a la MasterContentToolbar
                    EventHandler _saveEventHandler = new EventHandler(btnSave_Click);
                    FwMasterPage.ContentNavigatorToolbarFileActionAdd(_saveEventHandler, MasterFwContentToolbarAction.Save, false);
                }
                rbList.SelectedIndexChanged += new EventHandler(rbList_SelectedIndexChanged);
                FwMasterPage.RegisterContentAsyncPostBackTrigger(rbList, "SelectedIndexChanged");
                customvEndDate.ClientValidationFunction = "ValidateDateTimeRangeResourceDuration";
            }
            protected override void OnInit(EventArgs e)
            {
                base.OnInit(e);
                LoadTextLabels();
                base.InjectValidateDatePicker(rdtFrom.ClientID, rdtThrough.ClientID, "ResourceDuration");
            }
            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);

                if (!Page.IsPostBack)
                {
                    //Inicializo el Form
                    if (Entity == null)
                    { Add(); }
                    else
                    { LoadData(); } //Edit.

                    //Form
                    base.SetContentTableRowsCss(tblContentForm);
                    base.SetContentTableRowsCss(tblContentFormUpload);
                }
            }
            protected override void SetPagetitle()
            {
                base.PageTitle = (Entity != null) ? Entity.ResourceVersion.LanguageOption.Title : Resources.CommonListManage.ResourceVersion;
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }
        #endregion

        #region Private Methods
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonListManage.ResourceVersion;
                rbList.Items[0].Text = Resources.CommonListManage.File;
                rbList.Items[1].Text = Resources.CommonListManage.URL;
                lblCurrent.Text = Resources.CommonListManage.Current;
                lblExtension.Text = Resources.CommonListManage.Extension;
                lblFileName.Text = Resources.CommonListManage.FileName;
                lblFrom.Text = Resources.CommonListManage.From;
                lblLenght.Text = Resources.CommonListManage.FileSize;
                lblResourceFile.Text = Resources.CommonListManage.FileName;
                lblSelectedResouce.Text = Resources.CommonListManage.SelectedResource;
                lblThrough.Text = Resources.CommonListManage.Through;
                lblType.Text = Resources.CommonListManage.Type;
                lblUpload.Text = Resources.CommonListManage.FileUpload;
                lblVersion.Text = Resources.CommonListManage.Version;
                rfvDateFrom.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
                rfvDateThrough.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
                rfvFile.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
                rfvOrder.ErrorMessage = Resources.ConstantMessage.ValidationWrongFormat;
                rfvTitle.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
                rfvVersion.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
            }
            private void Add()
            {
                txtResourceFile.Text = String.Empty;
                lblExtensionValue.Text = String.Empty;
                lblFileNameValue.Text = String.Empty;
                lblLenghtValue.Text = String.Empty;
                lblTypeValue.Text = String.Empty;
            }
            private void LoadData()
            {
                rbList.Enabled = false;
                txtResourceFile.ReadOnly = true;
                fileUploadVersionable.Enabled = false;
                txtVersion.ReadOnly = true;
                chkCurrent.Enabled = false;
                rdtFrom.Enabled = false;
                rdtThrough.Enabled = false;

                Condesus.EMS.Business.KC.Entities.VersionDoc _versionFileDoc = null;
                Condesus.EMS.Business.KC.Entities.VersionURL _versionFileURL = null;

                if (Entity.GetType().Name == "VersionDoc")
                {
                    _versionFileDoc = (VersionDoc)Entity;

                    rbList.Items.FindByValue("rbFile").Selected = true;
                    rbList.Items[0].Selected = true;
                    rbList.Items[1].Selected = false;
                    tblContentFormUpload.Style.Add("display", "block");
                    lblResourceFile.Text = "File Name:";
                    rfvTitle.Enabled = false;
                    rfvFile.Enabled = true;

                    txtResourceFile.Text = _versionFileDoc.FileAttach.FileName;
                    lblExtensionValue.Text = _versionFileDoc.FileAttach.FileName.Substring(_versionFileDoc.FileAttach.FileName.LastIndexOf(".") + 1);
                    lblFileNameValue.Text = _versionFileDoc.FileAttach.FileName;
                    lblLenghtValue.Text = _versionFileDoc.DocSize;
                    lblTypeValue.Text = _versionFileDoc.DocType;
                }
                else
                {
                    _versionFileURL = (VersionURL)Entity;

                    rbList.Items.FindByValue("rbUrl").Selected = true;
                    rbList.Items[0].Selected = false;
                    rbList.Items[1].Selected = true;
                    tblContentFormUpload.Style.Add("display", "none");
                    lblResourceFile.Text = "URL:";
                    rfvTitle.Enabled = true;
                    rfvFile.Enabled = false;

                    txtResourceFile.Text = _versionFileURL.Url;
                }

                //carga los datos en pantalla
                rdtFrom.SelectedDate = Entity.ValidFrom;
                rdtThrough.SelectedDate = Entity.ValidThrough;
                txtVersion.Text = Entity.VersionNumber.ToString();
            }
            private void CompressFile(Stream streamFile)
            {
                //Byte _buffer = streamFile.Length;
                //streamFile.Read(_buffer,0,_buffer.Le)

                //    'A String object reads the file name (locally)
                //Dim FileName As String = Path.GetFileName(browseFile.Value)

                //'Stream object that reads file contents
                //Dim streamObj As Stream = browseFile.PostedFile.InputStream

                //'Allocate space in buffer according to the length of the file read
                //Dim buffer(streamObj.Length) As Byte

                //'Fill buffer
                //streamObj.Read(buffer, 0, buffer.Length)
                //streamObj.Close()

                //'File Stream object used to change the extension of a file
                //Dim compFile As System.IO.FileStream = File.Create(MapPath(Path.ChangeExtension(FileName, "zip")))

                //'GZip object that compress the file 
                //Dim zipStreamObj As New GZipStream(compFile, CompressionMode.Compress)

                //'Write to the Stream object from the buffer
                //zipStreamObj.Write(buffer, 0, buffer.Length)
                //zipStreamObj.Close()

            }
        #endregion

        #region Page Events
        protected void rbList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Como si fuera un ADD..
            //Limpia los campos para todos...
            txtResourceFile.Text = String.Empty;
            lblExtensionValue.Text = String.Empty;
            lblFileNameValue.Text = String.Empty;
            lblLenghtValue.Text = String.Empty;
            lblTypeValue.Text = String.Empty;

            if (rbList.Items.FindByValue("rbFile").Selected)
            {
                tblContentFormUpload.Style.Add("display", "block");
                lblResourceFile.Text = Resources.CommonListManage.FileName;
                rfvTitle.Enabled = false;
                rfvFile.Enabled = true;
            }
            else
            {
                tblContentFormUpload.Style.Add("display", "none");
                lblResourceFile.Text = Resources.CommonListManage.URL;
                rfvTitle.Enabled = true;
                rfvFile.Enabled = false;
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                System.IO.Stream _fileStream;
                String _name = String.Empty;

                //Condesus.EMS.Business.KC.Entities.Catalog _catalog = null;
                //carga archivo o carga URL
                if (rbList.Items.FindByValue("rbFile").Selected)
                {
                    if (fileUploadVersionable.HasFile)
                    {
                        String _fileNameCatalog = fileUploadVersionable.FileName;
                        _fileStream = fileUploadVersionable.FileContent;

                        //Obtiene el texto
                        StreamReader _streamReaderFileMeasurement = new StreamReader(_fileStream);
                        String _fileMeasurement = _streamReaderFileMeasurement.ReadToEnd();

                        //Archivo
                        String _fileName = fileUploadVersionable.FileName;  // _filePath.Substring(_filePath.LastIndexOf("\\") + 1);
                        String _fileExtension = _fileName.Substring(_fileName.LastIndexOf(".") + 1);
                        String _fileSize = _fileStream.Length.ToString();  //Request.Files[0].ContentLength.ToString();
                        String _fileType = fileUploadVersionable.PostedFile.ContentType;

                        lblFileNameValue.Text = _fileName;
                        lblExtensionValue.Text = _fileExtension;
                        lblLenghtValue.Text = _fileSize + " bytes.";
                        lblTypeValue.Text = _fileType;
                        _name = txtResourceFile.Text;   // _fileName;

                        //Obtiene el binario
                        Byte[] _fileVersionBinary = fileUploadVersionable.FileBytes;
                        _fileStream.Read(_fileVersionBinary, 0, Convert.ToInt32(_fileStream.Length));

                        if (Entity == null)
                        {
                            Entity = ((ResourceVersion)EMSLibrary.User.KnowledgeCollaboration.Map.Resource(_IdResource)).VersionAdd(DateTime.Now, Convert.ToDateTime(rdtFrom.SelectedDate), Convert.ToDateTime(rdtThrough.SelectedDate), txtVersion.Text, _fileType, _fileSize, _name, _fileVersionBinary);
                        }
                    }
                }
                else
                {
                    _name = txtResourceFile.Text.ToLower().Replace("http://", String.Empty);
                    //URL
                    Entity = ((ResourceVersion)EMSLibrary.User.KnowledgeCollaboration.Map.Resource(_IdResource)).VersionAdd(DateTime.Now, Convert.ToDateTime(rdtFrom.SelectedDate), Convert.ToDateTime(rdtThrough.SelectedDate), txtVersion.Text, txtResourceFile.Text.ToLower().Replace("http://", String.Empty));
                }

                base.NavigatorAddTransferVar("IdResource", Entity.IdResource);
                base.NavigatorAddTransferVar("IdResourceFile", Entity.IdResourceFile);

                String _pkValues = "IdResource=" + Entity.IdResource.ToString()
                    + "& IdResourceFile=" + Entity.IdResourceFile.ToString();
                base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);

                base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.KC.VersionFile);
                //base.NavigatorAddTransferVar("EntityNameContextInfo", Common.ConstantsEntitiesName.KC.Resource);
                base.NavigatorAddTransferVar("EntityNameContextInfo", Common.ConstantsEntitiesName.KC.Resource);
                base.NavigatorAddTransferVar("EntityNameContextElement", String.Empty);
                //Navigate("~/MainInfo/ListViewer.aspx", Resources.CommonListManage.ResourceVersion + " " + _name);
                String _entityPropertyName = String.Concat(Entity.ResourceVersion.LanguageOption.Title);
                NavigatePropertyEntity(Common.ConstantsEntitiesName.KC.VersionFile, _entityPropertyName, Condesus.WebUI.Navigation.NavigateMenuAction.Add);


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
