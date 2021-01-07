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
using Condesus.EMS.Business.PA.Entities;

namespace Condesus.EMS.WebUI.AdministrationTools.PerformanceAssessment
{
    public partial class MethodologiesLanguagesProperties : BaseProperties
    {
        #region Internal Properties
            private Int64 _IdEntity
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdMethodology") ? base.NavigatorGetTransferVar<Int64>("IdMethodology") : 0;
                }
            }
            private String _IdEntityLanguage
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdLanguage") ? base.NavigatorGetTransferVar<String>("IdLanguage") : String.Empty;
                }
            }
            private Methodology _Entity = null;
            private Methodology Entity
            {
                get
                {
                    if (_Entity == null)
                    {
                        _Entity = EMSLibrary.User.PerformanceAssessments.Configuration.Methodology(_IdEntity);
                    }

                    return _Entity;
                }

                set { _Entity = value; }
            }
            private Methodology_LG _EntityLanguage = null;
            private Methodology_LG EntityLanguage
            {
                get
                {
                    if (_EntityLanguage == null)
                    {
                        _EntityLanguage = Entity.LanguagesOptions[_IdEntityLanguage];
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
                    if (EntityLanguage == null)
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
                base.PageTitle = (Entity != null) ? Entity.LanguageOption.MethodName + " - " + Global.Languages[_IdEntityLanguage].Name : Resources.CommonListManage.Methodology;
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }
        #endregion

        #region Private Methods
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonListManage.Methodology_LG_Title;
                lblDescription.Text = Resources.CommonListManage.Description;
                lblLanguage.Text = Resources.CommonListManage.Language;
                lblName.Text = Resources.CommonListManage.Name;
                lblType.Text = Resources.CommonListManage.MethodType;
                rfv1.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
                rfvType.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
            }
            private void Add()
            {
                base.StatusBar.Clear();

                //Inicializa el Formulario
                txtName.Text = String.Empty;
                txtDescription.Text = String.Empty;
                lblLanguageValue.Text = Global.Languages[_IdEntityLanguage].Name;
                txtType.Text = string.Empty;
            }
            private void LoadData()
            {
                //carga los datos en pantalla
                txtName.Text = EntityLanguage.MethodName;
                txtType.Text = EntityLanguage.MethodType;
                txtDescription.Text = EntityLanguage.Description;
                lblLanguageValue.Text = EntityLanguage.Language.Name;
            }
        #endregion

        #region Page Events
            protected void btnSave_Click(object sender, EventArgs e)
            {
                try
                {
                    if (String.IsNullOrEmpty( EntityLanguage.MethodName))
                    {
                        //Alta
                        EntityLanguage = Entity.LanguageCreate(EMSLibrary.User.DirectoryServices.Configuration.Language(_IdEntityLanguage), txtName.Text, txtType.Text, txtDescription.Text);
                    }
                    else
                    {
                        //Modificacion
                        Entity.LanguageModify(EMSLibrary.User.DirectoryServices.Configuration.Language(_IdEntityLanguage), txtName.Text, txtType.Text, txtDescription.Text);
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
