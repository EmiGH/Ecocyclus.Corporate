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
using Condesus.EMS.WebUI.MasterControls;
using Condesus.EMS.Business.PF.Entities;


namespace Condesus.EMS.WebUI.PM
{
    public partial class ProcessTaskExecutionCalibrationsProperties : BaseProperties
    {
        #region Internal Properties
            private Int64 _IdTask
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdTask") ? base.NavigatorGetTransferVar<Int64>("IdTask") : Convert.ToInt64(GetPKfromNavigator("IdTask"));
                }
            }
            private Int64 _IdExecution
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdExecution") ? base.NavigatorGetTransferVar<Int64>("IdExecution") : Convert.ToInt64(GetPKfromNavigator("IdExecution"));
                }
            }
            private ProcessTaskExecution _Entity = null;
            private ProcessTaskExecution Entity
            {
                get
                {
                    try
                    {
                        if (_Entity == null)
                        {
                            _Entity = ((Condesus.EMS.Business.PF.Entities.ProcessTask)EMSLibrary.User.ProcessFramework.Map.Process(_IdTask)).ProcessTaskExecution(_IdExecution);
                        }
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

                if ((Entity == null) || (Entity.GetType().Name != Common.ConstantsEntitiesName.PF.ProcessTaskExecutionCalibration))
                {
                    //Le paso el Save a la MasterContentToolbar
                    EventHandler _saveEventHandler = new EventHandler(btnSave_Click);
                    //esta pagina, tiene un FileUpload, entonces el boton save, tiene que ser PostbackTrigger
                    FwMasterPage.ContentNavigatorToolbarFileActionAdd(_saveEventHandler, MasterFwContentToolbarAction.Save, false);
                }

                customvEndDate.ClientValidationFunction = "ValidateDateTimeRangeCalibration";
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
                        Add();
                    else
                        LoadData(); //Edit.

                    //Form
                    base.SetContentTableRowsCss(tblContentForm);
                    lblLanguageValue.Text = Global.DefaultLanguage.Name;
                    lblTaskNameValue.Text = EMSLibrary.User.ProcessFramework.Map.Process(_IdTask).LanguageOption.Title;
                }
                base.InjectValidateDateTimePicker(rdtValidationStart.ClientID, rdtValidationEnd.ClientID, "Calibration");
            }
            protected override void SetPagetitle()
            {
                if (Entity != null)
                {
                    String _title = Entity.ProcessTask.LanguageOption.Title;
                    base.PageTitle = _title;
                }
                else
                {
                    base.PageTitle = Resources.CommonListManage.ProcessTaskExecutionCalibration;
                }
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }
        #endregion

        #region Private Methods
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonListManage.ProcessTaskExecutionCalibration;
                lblComment.Text = Resources.CommonListManage.Comment;
                lblDate.Text = Resources.CommonListManage.Date;
                lblFile.Text = Resources.CommonListManage.FileUploadCalibration;
                lblIdExecution.Text = Resources.CommonListManage.IdExecution;
                lblLanguage.Text = Resources.CommonListManage.Language;
                lblPost.Text = Resources.CommonListManage.Post;
                lblTaskName.Text = Resources.CommonListManage.ProcessTask;
                lblValidationEnd.Text = Resources.CommonListManage.ValidThrough;
                lblValidationStart.Text = Resources.CommonListManage.ValidFrom;
                customvEndDate.ErrorMessage = Resources.ConstantMessage.ValidationDateFromTo;
            }
            private void Add()
            {
                base.StatusBar.Clear();
                txtComment.Text = String.Empty;
            }
            private void LoadData()
            {
                txtComment.Text = Entity.Comment;
                lblDateValue.Text = Entity.Date.ToString();
                if (Entity.Post != null)
                {
                    lblPostValue.Text = Entity.Post.Person.LastName + ", " + Entity.Post.Person.FirstName + " - " + Entity.Post.JobTitle.Name();
                }
                if (Entity.GetType().Name == Common.ConstantsEntitiesName.PF.ProcessTaskExecutionCalibration)
                {
                    rdtValidationStart.SelectedDate = Convert.ToDateTime(((ProcessTaskExecutionCalibration)Entity).ValidationStart);
                    rdtValidationEnd.SelectedDate = Convert.ToDateTime(((ProcessTaskExecutionCalibration)Entity).ValidationEnd);
                    fileUploadCalibration.Visible = false;
                    txtComment.ReadOnly = true;
                    rdtValidationStart.Enabled = false;
                    rdtValidationEnd.Enabled = false;
                }
            }
        #endregion

        #region Page Events
            protected void btnSave_Click(object sender, EventArgs e)
            {
                try
                {
                    System.IO.Stream _fileStream;

                    //Se trae el primer post que tiene asociado esta persona. por eso utiliza el "[0]".
                    Condesus.EMS.Business.DS.Entities.Post _post = EMSLibrary.User.Person.Posts.First();
                    Condesus.EMS.Business.PA.Entities.MeasurementDevice _measurementDevice = ((Condesus.EMS.Business.PF.Entities.ProcessTaskCalibration)EMSLibrary.User.ProcessFramework.Map.Process(_IdTask)).MeasurementDevice;
                    Byte[] _attach = null;

                    if (Entity == null)
                    {
                        //Es un ADD
                        DateTime dateExecution = DateTime.Now;
                        Entity = ((Condesus.EMS.Business.PF.Entities.ProcessTaskCalibration)EMSLibrary.User.ProcessFramework.Map.Process(_IdTask)).ProcessTaskExecutionsAdd(_post, dateExecution, txtComment.Text, _attach, Convert.ToDateTime(rdtValidationStart.SelectedDate), Convert.ToDateTime(rdtValidationEnd.SelectedDate), _measurementDevice);

                        
                    }
                    else
                    {
                        //La ejecucion madre ya existe, debe ingresar la ejecucion especifica, ExecutionMeasurements
                        DateTime dateExecution = DateTime.Now;
                        Int64 _idFile = 0;
                        if (fileUploadCalibration.HasFile)
                        {
                            String _fileNameCalibration = fileUploadCalibration.FileName;

                            _fileStream = fileUploadCalibration.FileContent;

                            Byte[] _fileCalibration = fileUploadCalibration.FileBytes;
                            _fileStream.Read(_fileCalibration, 0, Convert.ToInt32(_fileStream.Length));

                            Entity = ((Condesus.EMS.Business.PF.Entities.ProcessTaskCalibration)EMSLibrary.User.ProcessFramework.Map.Process(_IdTask)).ProcessTaskExecutionsAdd(_post, Entity, ref _idFile, _fileNameCalibration, _fileCalibration, txtComment.Text, Convert.ToDateTime(rdtValidationStart.SelectedDate), Convert.ToDateTime(rdtValidationEnd.SelectedDate), _measurementDevice);
                        }
                        else
                        {
                            Entity = ((Condesus.EMS.Business.PF.Entities.ProcessTaskCalibration)EMSLibrary.User.ProcessFramework.Map.Process(_IdTask)).ProcessTaskExecutionsAdd(_post, Entity, txtComment.Text, _attach, Convert.ToDateTime(rdtValidationStart.SelectedDate), Convert.ToDateTime(rdtValidationEnd.SelectedDate), _measurementDevice);
                        }
                    }
                    base.NavigatorAddTransferVar("IdProcess", Entity.ProcessTask.IdProcess);
                    base.NavigatorAddTransferVar("IdExecution", Entity.IdExecution);

                    String _pkValues = "IdProcess=" + Entity.ProcessTask.IdProcess.ToString()
                        + "& IdExecution=" + Entity.IdExecution.ToString();
                    base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);
                    base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.PF.ProcessTaskExecution);
                    //base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.PF.ProcessTaskExecutionCalibration);
                    base.NavigatorAddTransferVar("EntityNameContextInfo", Common.ConstantsEntitiesName.PF.ProcessTaskCalibration);
                    base.NavigatorAddTransferVar("EntityNameContextElement", Common.ConstantsEntitiesName.PF.ProcessGroupProcess);
                    //Navigate("~/MainInfo/ListViewer.aspx", Resources.CommonListManage.ProcessTaskExecutionCalibration + " " + Entity.ProcessTask.LanguageOption.Title, Condesus.WebUI.Navigation.DeleteType.DeleteModifiedEntity);
                    String _entityPropertyName = String.Concat(Entity.ProcessTask.LanguageOption.Title);
                    NavigatePropertyEntity(Common.ConstantsEntitiesName.PF.ProcessTaskExecution, _entityPropertyName, Condesus.WebUI.Navigation.NavigateMenuAction.Add);

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
