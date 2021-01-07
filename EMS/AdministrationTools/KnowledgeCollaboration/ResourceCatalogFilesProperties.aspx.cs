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
    public partial class ResourceCatalogFilesProperties : BaseProperties
    {
        #region Internal Properties
            private Catalog _Entity = null;
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
            private Catalog Entity
            {
                get
                {
                    try
                    {
                        if (_Entity == null)
                            _Entity = ((Condesus.EMS.Business.KC.Entities.ResourceCatalog)EMSLibrary.User.KnowledgeCollaboration.Map.Resource(_IdResource)).Catalog(_IdResourceFile);

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
            }
            protected override void OnInit(EventArgs e)
            {
                base.OnInit(e);
                LoadTextLabels();
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
                base.PageTitle = (Entity != null) ? Entity.ResourceCatalog.LanguageOption.Title : Resources.CommonListManage.ResourceCatalogFile;
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }
        #endregion

        #region Private Methods
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonListManage.ResourceCatalogFile;
                lblExtension.Text = Resources.CommonListManage.Extension;
                lblFileName.Text = Resources.CommonListManage.FileName;
                lblLenght.Text = Resources.CommonListManage.FileSize;
                lblResourceFile.Text = Resources.CommonListManage.FileName;
                lblSelectedResouce.Text = Resources.CommonListManage.SelectedResource;
                lblType.Text = Resources.CommonListManage.Type;
                lblUpload.Text = Resources.CommonListManage.FileUpload;
                rbList.Items[0].Text = Resources.CommonListManage.File;
                rbList.Items[1].Text = Resources.CommonListManage.URL;
                rfvFile.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
                rfvTitle.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
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
                fileUploadCatalog.Enabled = false;

                Condesus.EMS.Business.KC.Entities.CatalogDoc _catalogDoc = null;
                Condesus.EMS.Business.KC.Entities.CatalogURL _catalogURL = null;

                if (Entity.GetType().Name == "CatalogDoc")
                {
                    _catalogDoc = (Condesus.EMS.Business.KC.Entities.CatalogDoc)Entity;
                    
                    rbList.Items.FindByValue("rbFile").Selected = true;
                    rbList.Items[0].Selected = true;
                    rbList.Items[1].Selected = false;
                    tblContentFormUpload.Style.Add("display", "");
                    lblResourceFile.Text = "File Name:";
                    rfvTitle.Enabled = false;
                    rfvFile.Enabled = true;

                    txtResourceFile.Text = _catalogDoc.FileAttach.FileName;
                    lblExtensionValue.Text = _catalogDoc.FileAttach.FileName.Substring(_catalogDoc.FileAttach.FileName.LastIndexOf(".") + 1);
                    lblFileNameValue.Text = _catalogDoc.FileAttach.FileName;
                    lblLenghtValue.Text = _catalogDoc.DocSize;
                    lblTypeValue.Text = _catalogDoc.DocType;
                }
                else
                {
                    _catalogURL = (Condesus.EMS.Business.KC.Entities.CatalogURL)Entity;

                    rbList.Items.FindByValue("rbUrl").Selected = true;
                    rbList.Items[0].Selected = false;
                    rbList.Items[1].Selected = true;
                    tblContentFormUpload.Style.Add("display", "none");
                    lblResourceFile.Text = "URL:";
                    rfvTitle.Enabled = true;
                    rfvFile.Enabled = false;

                    txtResourceFile.Text = _catalogURL.Url;
                }
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

        //private void InyectJavaScript()
        //{
        //    base.InjectRmnOptionItemClickHandler(rmnOption.ClientID, String.Empty, false, String.Empty);
        //}
        //private void InitializeHandlers()
        //{
        //    rmnOption.ItemClick += new RadMenuEventHandler(rmnOption_ItemClick);
        //    btnOk.Click += new EventHandler(btnOkDelete_Click);
        //    btnSave.Click += new EventHandler(btnSave_Click);
        //    rmnOption.OnClientItemClicked = "rmnOption_OnClientItemClickedHandler";
        //    //OnSelectedIndexChanged="rbList_SelectedIndexChanged"
        //    rbList.SelectedIndexChanged += new EventHandler(rbList_SelectedIndexChanged);
        //    //btnAttach.Click += new EventHandler(btnAttach_Click);

        //}

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
                    if (fileUploadCatalog.HasFile)
                    {
                        String _fileNameCatalog = fileUploadCatalog.FileName;
                        _fileStream = fileUploadCatalog.FileContent;

                        //Obtiene el texto
                        StreamReader _streamReaderFileMeasurement = new StreamReader(_fileStream);
                        String _fileMeasurement = _streamReaderFileMeasurement.ReadToEnd();
                     
                        //Archivo
                        String _fileName = fileUploadCatalog.FileName;  // _filePath.Substring(_filePath.LastIndexOf("\\") + 1);
                        String _fileExtension = _fileName.Substring(_fileName.LastIndexOf(".") + 1);
                        String _fileSize = _fileStream.Length.ToString();  //Request.Files[0].ContentLength.ToString();
                        String _fileType = fileUploadCatalog.PostedFile.ContentType;

                        lblFileNameValue.Text = _fileName;
                        lblExtensionValue.Text = _fileExtension;
                        lblLenghtValue.Text = _fileSize + " bytes.";
                        lblTypeValue.Text = _fileType;
                        _name = _fileName;

                        //Obtiene el binario
                        Byte[] _fileCatalogBinary = fileUploadCatalog.FileBytes;
                        _fileStream.Read(_fileCatalogBinary, 0, Convert.ToInt32(_fileStream.Length));

                        if (Entity == null)
                        {
                            Entity = ((Condesus.EMS.Business.KC.Entities.ResourceCatalog)EMSLibrary.User.KnowledgeCollaboration.Map.Resource(_IdResource)).CatalogAdd(DateTime.Now, _fileType, _fileSize, _fileName, _fileCatalogBinary);
                            //FinishSave(_catalog.IdResourceFile);
                        }
                    }
                }
                else
                {
                    _name = txtResourceFile.Text.ToLower().Replace("http://", String.Empty);
                    //URL
                    Entity = ((Condesus.EMS.Business.KC.Entities.ResourceCatalog)EMSLibrary.User.KnowledgeCollaboration.Map.Resource(_IdResource)).CatalogAdd(DateTime.Now, txtResourceFile.Text.ToLower().Replace("http://", String.Empty));
                    //FinishSave(_catalog.IdResourceFile);
                }

                base.NavigatorAddTransferVar("IdResource", Entity.IdResource);
                base.NavigatorAddTransferVar("IdResourceFile", Entity.IdResourceFile);

                String _pkValues = "IdResource=" + Entity.IdResource.ToString()
                    + "& IdResourceFile=" + Entity.IdResourceFile.ToString();
                base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);
                
                base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.KC.Catalog);
                //base.NavigatorAddTransferVar("EntityNameContextInfo", Common.ConstantsEntitiesName.KC.Resource);
                base.NavigatorAddTransferVar("EntityNameContextInfo", Common.ConstantsEntitiesName.KC.Resource);
                base.NavigatorAddTransferVar("EntityNameContextElement", String.Empty);
                //Navigate("~/MainInfo/ListViewer.aspx", Resources.CommonListManage.ResourceCatalog + " " + _name);
                String _entityPropertyName = String.Concat(Entity.ResourceCatalog.LanguageOption.Title);
                NavigatePropertyEntity(Common.ConstantsEntitiesName.KC.Catalog, _entityPropertyName, Condesus.WebUI.Navigation.NavigateMenuAction.Add);


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
