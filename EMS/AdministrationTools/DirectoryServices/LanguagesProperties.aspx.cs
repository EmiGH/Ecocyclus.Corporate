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
using Telerik.Web.UI;

using Condesus.EMS.WebUI.MasterControls;
using Condesus.EMS.Business.DS.Entities;

namespace Condesus.EMS.WebUI.DS
{
    public partial class LanguagesProperties : BaseProperties
    {
        #region Internal Properties

        private String _IdEntity
        {
            get
            {
                return base.NavigatorContainsTransferVar("IdLanguage") ? base.NavigatorGetTransferVar<String>("IdLanguage") : String.Empty;
            }
        }
        private Language _Entity = null;
        private Language Entity
        {
            get
            {
                if (_Entity == null)
                    _Entity = EMSLibrary.User.DirectoryServices.Configuration.Language(_IdEntity);

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
                    this.txtIdValue.Focus();
                }
            }
            //Setea el Titulo de la Pagina
            //(como no puedo pasar el Objeto como una 'Interface', lo tengo que hacer localmente)
            protected override void SetPagetitle()
            {
                base.PageTitle = (Entity != null) ? Entity.Name : Resources.CommonListManage.Language;
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }
        #endregion

        #region Private Methods
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonListManage.Language;
                lblDefault.Text = Resources.CommonListManage.IsDefault;
                lblEnabled.Text = Resources.CommonListManage.Enabled;
                lblID.Text = Resources.CommonListManage.IdLanguage;
                lblLanguage.Text = Resources.CommonListManage.Language;
                rfv1.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
                rfv2.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
            }
            private void Add()
            {
                base.StatusBar.Clear();

                //Inicializa el Formulario
                txtIdValue.Text = String.Empty;
                txtLanguage.Text = String.Empty;
                chkDefault.Checked = false;
                chkEnabled.Checked = false;
            }
            private void LoadData()
            {
                //carga los datos en pantalla
                txtIdValue.Text = Entity.IdLanguage.ToString();
                txtLanguage.Text = Entity.Name.ToString();
                chkDefault.Checked = Entity.IsDefault;
                chkEnabled.Checked = Entity.Enable;
                chkEnabled.Enabled = true;
                chkDefault.Enabled = false;
            }
        #endregion

        #region Page Events
            protected void btnSave_Click(object sender, EventArgs e)
            {
                try
                {
                    if (Entity == null)
                    {
                        //Alta
                        Entity = EMSLibrary.User.DirectoryServices.Configuration.LanguagesAdd(txtIdValue.Text, txtLanguage.Text, chkEnabled.Checked);
                    }
                    else
                    {
                        //Modificacion
                        Entity = EMSLibrary.User.DirectoryServices.Configuration.LanguagesModify(_IdEntity, txtLanguage.Text, chkEnabled.Checked);
                    }

                    base.NavigatorAddTransferVar("IdLanguage", Entity.IdLanguage);

                    String _pkValues = "IdLanguage=" + Entity.IdLanguage.ToString();
                    base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);

                    base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.DS.Language);
                    base.NavigatorAddTransferVar("EntityNameContextInfo", String.Empty);
                    //Navigate("~/MainInfo/ListViewer.aspx", Resources.CommonListManage.Language + " " + Entity.Name, Condesus.WebUI.Navigation.DeleteType.DeleteModifiedEntity);
                    String _entityPropertyName = String.Concat(Entity.Name);
                    NavigatePropertyEntity(Common.ConstantsEntitiesName.DS.Language, _entityPropertyName, Condesus.WebUI.Navigation.NavigateMenuAction.Add);

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

