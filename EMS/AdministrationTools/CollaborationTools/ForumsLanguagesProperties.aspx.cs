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
using Condesus.EMS.WebUI.MasterControls;
using Condesus.EMS.Business.CT.Entities;

namespace Condesus.EMS.WebUI.CT
{
    public partial class ForumsLanguagesProperties : BaseProperties
    {
        #region Internal Properties
            private Int64 _IdEntity
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdForum") ? base.NavigatorGetTransferVar<Int64>("IdForum") : 0;
                }
            }
            private String _IdEntityLanguage
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdLanguage") ? base.NavigatorGetTransferVar<String>("IdLanguage") : String.Empty;
                }
            }
            private Int64 _IdProcess
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdProcess") ? base.NavigatorGetTransferVar<Int64>("IdProcess") : Convert.ToInt64(GetPKfromNavigator("IdProcess"));
                }
            }
            private Forum _Entity = null;
            private Forum Entity
            {
                get
                {
                    if (_Entity == null)
                    {
                        _Entity = EMSLibrary.User.CollaborationTools.Map.Forum(_IdEntity);
                    }

                    return _Entity;
                }

                set { _Entity = value; }
            }
            private Forum_LG _EntityLanguage = null;
            private Forum_LG EntityLanguage
            {
                get
                {
                    if (_EntityLanguage == null)
                    {
                        _EntityLanguage = Entity.LanguagesOptions.Item(_IdEntityLanguage);
                    }

                    return _EntityLanguage;
                }

                set { _EntityLanguage = value; }
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

                if (!Page.IsPostBack)
                {
                    //Inicializo el Form
                    if (EntityLanguage == null)
                        { Add(); }
                    else
                        { LoadData(); } //Edit.

                    //Setea el nombre del process.
                    lblProcessValue.Text = EMSLibrary.User.ProcessFramework.Map.Process(_IdProcess).LanguageOption.Title;
                    //Form
                    base.SetContentTableRowsCss(tblContentForm);
                    this.txtForum.Focus();
                }
            }
            protected override void SetPagetitle()
            {
                base.PageTitle = (Entity != null) ? Entity.LanguageOption.Name + " - " + Global.Languages[_IdEntityLanguage].Name : GetLocalResourceObject("PageDescription").ToString();
            }
        #endregion

        #region Private Methods
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonListManage.AccountingActivity_LG_Title;
                lblDescription.Text = Resources.CommonListManage.Description;
                lblLanguage.Text = Resources.CommonListManage.Language;
                lblForum.Text = Resources.CommonListManage.Name;
                lblProcess.Text = Resources.CommonListManage.Process;
                rfv1.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
            }
            private void Add()
            {
                base.StatusBar.Clear();

                //Inicializa el Formulario
                txtForum.Text = String.Empty;
                txtDescription.Text = String.Empty;
                lblLanguageValue.Text = Global.Languages[_IdEntityLanguage].Name;
            }
            private void LoadData()
            {
                //carga los datos en pantalla
                txtForum.Text = EntityLanguage.Name;
                txtDescription.Text = EntityLanguage.Description;
                lblLanguageValue.Text = Global.Languages[_IdEntityLanguage].Name;
            }
        #endregion

        #region Page Events
            protected void btnSave_Click(object sender, EventArgs e)
            {
                try
                {
                    if (EntityLanguage == null)
                    {
                        //Alta
                        EntityLanguage = Entity.LanguagesOptions.Create(_IdEntityLanguage, txtForum.Text, txtDescription.Text);
                    }
                    else
                    {
                        //Modificacion
                        Entity.LanguagesOptions.Update(_IdEntityLanguage, txtForum.Text, txtDescription.Text);
                    }
                    base.NavigateBack();

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
