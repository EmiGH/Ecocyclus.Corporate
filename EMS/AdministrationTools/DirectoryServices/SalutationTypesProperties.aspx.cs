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
    public partial class SalutationTypesProperties : BaseProperties
    {
        #region Internal Properties

        private Int64 _IdSalutationType
        {
            get
            {
                return base.NavigatorContainsTransferVar("IdSalutationType") ? base.NavigatorGetTransferVar<Int64>("IdSalutationType") : 0;
            }
        }
        private SalutationType _Entity = null;
        private SalutationType Entity
        {
            get
            {
                try
                {
                    if (_Entity == null)
                        _Entity = EMSLibrary.User.DirectoryServices.Configuration.SalutationType(_IdSalutationType);

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
                    this.txtName.Focus();
                }
            }
            protected override void SetPagetitle()
            {
                base.PageTitle = (Entity != null) ? Entity.LanguageOption.Name : Resources.CommonListManage.SalutationType;
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }
        #endregion

        #region Private Methods
        private void LoadTextLabels()
        {
            Page.Title = Resources.CommonListManage.SalutationType;
            lblDescription.Text = Resources.CommonListManage.Description;
            lblLanguage.Text = Resources.CommonListManage.Language;
            lblName.Text = Resources.CommonListManage.Name;
            rfv1.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
        }
        private void Add()
        {
            base.StatusBar.Clear();

            //Inicializa el Formulario
            txtName.Text = String.Empty;
            txtDescription.Text = String.Empty;
            lblLanguageValue.Text = Global.DefaultLanguage.Name;
        }
        private void LoadData()
        {
            //carga los datos en pantalla
            txtName.Text = Entity.LanguageOption.Name;
            txtDescription.Text = Entity.LanguageOption.Description;
            lblLanguageValue.Text = Entity.LanguageOption.Language.Name;
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
                        Entity = EMSLibrary.User.DirectoryServices.Configuration.SalutationTypesAdd(Global.DefaultLanguage.IdLanguage, txtName.Text, txtDescription.Text);
                    }
                    else
                    {
                        //Modificacion
                        Entity = EMSLibrary.User.DirectoryServices.Configuration.SalutationTypesModify(Entity.IdSalutationType, Global.DefaultLanguage.IdLanguage, txtName.Text, txtDescription.Text);
                    }

                    base.NavigatorAddTransferVar("IdSalutationType", Entity.IdSalutationType);

                    String _pkValues = "IdSalutationType=" + Entity.IdSalutationType.ToString();
                    base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);

                    //base.Navigate(HttpContext.Current.Request.Url.AbsolutePath, Entity.LanguageOption.Name + " " + Resources.Common.Edit);
                    base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.DS.SalutationType);
                    base.NavigatorAddTransferVar("EntityNameContextInfo", base.NavigatorGetTransferVar<String>("EntityNameContextInfo"));
                    //Navigate("~/MainInfo/ListViewer.aspx", Resources.CommonListManage.SalutationType + " " + Entity.LanguageOption.Name, Condesus.WebUI.Navigation.DeleteType.DeleteModifiedEntity);
                    String _entityPropertyName = String.Concat(Entity.LanguageOption.Name);
                    NavigatePropertyEntity(Common.ConstantsEntitiesName.DS.SalutationType, _entityPropertyName, Condesus.WebUI.Navigation.NavigateMenuAction.Add);

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
