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
using System.Linq;
using Condesus.EMS.WebUI.MasterControls;
using Condesus.EMS.Business.PF.Entities;

namespace Condesus.EMS.WebUI.ManagementTools.ProcessesMap
{
    public partial class ExceptionProperties : BaseProperties
    {
        #region Internal Properties
            private String _ExceptionStateOperation
            {
                get
                {
                    return base.NavigatorContainsTransferVar("ExceptionState") ? base.NavigatorGetTransferVar<String>("ExceptionState") : String.Empty;
                }
            }
            private Int64 _IdExecution
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdExecution") ? base.NavigatorGetTransferVar<Int64>("IdExecution") : Convert.ToInt64(GetPKfromNavigator("IdExecution"));
                }
            }
            private Int64 _IdTask
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdTask") ? base.NavigatorGetTransferVar<Int64>("IdTask") : Convert.ToInt64(GetPKfromNavigator("IdTask"));
                }
            }
            private Int64 _IdException
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdException") ? base.NavigatorGetTransferVar<Int64>("IdException") : Convert.ToInt64(GetPKfromNavigator("IdException"));
                }
            }
            private Condesus.EMS.Business.IA.Entities.Exception _Entity = null;
            private Condesus.EMS.Business.IA.Entities.Exception Entity
            {
                get
                {
                    if (_Entity == null)
                    {
                        if (_IdException != 0)
                        {
                            _Entity = EMSLibrary.User.ImprovementAction.Configuration.Exception(_IdException);
                        }
                        else
                        {
                            _Entity = ((Condesus.EMS.Business.PF.Entities.ProcessTask)EMSLibrary.User.ProcessFramework.Map.Process(_IdTask)).ProcessTaskExecution(_IdExecution).Exception.First();
                        }
                    }

                    return _Entity;
                }

                set { _Entity = value; }
            }     
        #endregion

        #region PageLoad & Init

        protected override void InitializeHandlers()
        {
            base.InitializeHandlers();

            //Le paso el Save a la MasterContentToolbar
            EventHandler _saveEventHandler = new EventHandler(btnSave_Click);
            FwMasterPage.ContentNavigatorToolbarFileActionAdd(_saveEventHandler, MasterFwContentToolbarAction.Save, true);
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadTextLabels();

            //Carga la informacion de las tareas y la ejecucion.
            Condesus.EMS.Business.PF.Entities.ProcessTask _processTask = ((Condesus.EMS.Business.PF.Entities.ProcessTask)EMSLibrary.User.ProcessFramework.Map.Process(_IdTask));
            Condesus.EMS.Business.PF.Entities.ProcessTaskExecution _processTaskExecution = _processTask.ProcessTaskExecution(_IdExecution);

            String _fullNamePost = String.Empty;
            if (_processTaskExecution.Post != null)
            {
                _fullNamePost = _processTaskExecution.Post.Person.LastName
                                        + ", " + _processTaskExecution.Post.Person.FirstName
                                        + " - " + _processTaskExecution.Post.JobTitle.Name();
            }
            lblProjectValue.Text = _processTask.Parent.LanguageOption.Title;
            lblTaskNameValue.Text = _processTask.LanguageOption.Title;
            lblExecutedByValue.Text = _fullNamePost;
            lblDateValue.Text = _processTaskExecution.Date.ToLongDateString() + " " + _processTaskExecution.Date.ToLongTimeString();

            if (!Page.IsPostBack)
            {
                //Inicializo el Form
                if (Entity == null)
                    Add();
                else
                    LoadData(); //Edit.

                //Form
                base.SetContentTableRowsCss(tblContentForm);
                this.txtComment.Focus();
            }
        }

        //Setea el Titulo de la Pagina
        //(como no puedo pasar el Objeto como una 'Interface', lo tengo que hacer localmente)
        protected override void SetPagetitle()
        {
            base.PageTitle = (Entity != null) ? Entity.ExceptionType.LanguageOption.Name + " - " + Entity.ExceptionState.LanguageOption.Name + " " + Entity.ExceptionDate.ToString() : Resources.CommonListManage.Exception;
        }
        protected override void SetPageTileSubTitle()
        {
            base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
        }

        #endregion

        #region Private Methods
        private void LoadTextLabels()
        {
            lblComment.Text = Resources.CommonListManage.Comment;
            lblDate.Text = Resources.CommonListManage.Date;
            lblExceptionStates.Text = Resources.CommonListManage.State;
            lblExceptionType.Text = Resources.CommonListManage.Type;
            lblExecutedBy.Text = Resources.CommonListManage.ExecutedBy;
            lblProject.Text = Resources.CommonListManage.Process;
            lblTaskName.Text = Resources.CommonListManage.Task;
        }
        private void Add()
        {
            base.StatusBar.Clear();

            //Inicializa el Formulario
            txtComment.Text = String.Empty;
            //lblDateValue.Text = String.Empty;
            lblExceptionTypeValue.Text = Resources.CommonListManage.ExceptionTypeManual;
            lblExceptionStatesValue.Text = Resources.CommonListManage.ExceptionStateBuilding;

        }
        private void LoadData()
        {
            //carga los datos en pantalla
            txtComment.Text = Entity.Comment;
            //lblDateValue.Text = Entity.ExceptionDate.ToString();
            lblExceptionTypeValue.Text = Entity.ExceptionType.LanguageOption.Name.ToString();
            lblExceptionStatesValue.Text = Entity.ExceptionState.LanguageOption.Name.ToString();
        }

        #endregion

        #region Page Events

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Condesus.EMS.Business.PF.Entities.ProcessTaskExecution _processTaskExecution = null;
                Condesus.EMS.Business.PF.Entities.ProcessTask _processTask = ((Condesus.EMS.Business.PF.Entities.ProcessTask)EMSLibrary.User.ProcessFramework.Map.Process(_IdTask));
                
                if (Entity == null)
                {
                    //Alta
                    _processTaskExecution = _processTask.ProcessTaskExecution(_IdExecution);
                    Entity = _processTaskExecution.CreateException(txtComment.Text);
                }
                else
                {
                    //Esto es que va a cambiar el estado.
                    //Si la operacion es de close
                    if (_ExceptionStateOperation == Common.Constants.ExceptionStateCloseName)
                    {
                        Entity.Close(txtComment.Text);
                    }
                    //o para tratarla...
                    else
                    {
                        Entity.Treat(txtComment.Text);
                    }
                }


                base.NavigatorAddTransferVar("IdTask", _IdTask);
                base.NavigatorAddTransferVar("IdExecution", _IdExecution);
                base.NavigatorAddTransferVar("IdException", Entity.IdException);

                String _pkValues = "IdTask=" + _IdTask.ToString()
                     + "& IdExecution=" + _IdExecution.ToString()
                     + "& IdException=" + Entity.IdException.ToString();
                base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);

                base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.PF.ProcessTaskExecution);
                base.NavigatorAddTransferVar("EntityNameContextInfo", base.NavigatorGetTransferVar<String>("EntityNameContextInfo"));
                //Navigate("~/MainInfo/ListViewer.aspx", Resources.CommonListManage.ProcessTaskExecutionOperation + " " + Entity.ExceptionType.LanguageOption.Name + " - " + Entity.ExceptionState.LanguageOption.Name + " " + Entity.ExceptionDate.ToString());
                String _entityPropertyName = String.Concat(Entity.ExceptionType.LanguageOption.Name, " - ", Entity.ExceptionState.LanguageOption.Name, " ", Entity.ExceptionDate.ToString());
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
