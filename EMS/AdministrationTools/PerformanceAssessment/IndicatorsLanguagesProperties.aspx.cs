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

namespace Condesus.EMS.WebUI.PA
{
    public partial class IndicatorsLanguagesProperties : BaseProperties
    {
        #region Internal Properties

        private Int64 _IdEntity
        {
            get
            {
                return base.NavigatorContainsTransferVar("IdIndicator") ? base.NavigatorGetTransferVar<Int64>("IdIndicator") : Convert.ToInt64(GetPKfromNavigator("IdIndicator"));
            }
        }
        private String _IdEntityLanguage
        {
            get
            {
                return base.NavigatorContainsTransferVar("IdLanguage") ? base.NavigatorGetTransferVar<String>("IdLanguage") : String.Empty;
            }
        }

        private Indicator _Entity = null;
        private Indicator Entity
        {
            get
            {
                if (_Entity == null)
                {
                    _Entity = EMSLibrary.User.PerformanceAssessments.Map.Indicator(_IdEntity);
                }

                return _Entity;
            }

            set { _Entity = value; }
        }
        private Indicator_LG _EntityLanguage = null;
        private Indicator_LG EntityLanguage
        {
            get
            {
                if (_EntityLanguage == null)
                {
                    _EntityLanguage = Entity.LanguagesOptions.Item(EMSLibrary.User.DirectoryServices.Configuration.Language(_IdEntityLanguage));
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
                base.InjectCheckIndexesTags();
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
                base.PageTitle = (Entity != null) ? Entity.LanguageOption.Name + " - " + Global.Languages[_IdEntityLanguage].Name : Resources.CommonListManage.Indicator;
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }
        #endregion

        #region Private Methods
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonListManage.Indicator_LG_Title;
                lblDescription.Text = Resources.CommonListManage.Description;
                lblLanguage.Text = Resources.CommonListManage.Language;
                lblVerificationEntities.Text = Resources.CommonListManage.Name;
                lblScope.Text = Resources.CommonListManage.Scope;
                lblDefinition.Text = Resources.CommonListManage.Definition;
                lblLimitation.Text = Resources.CommonListManage.Limitation;
                rfv1.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
                rfv2.ErrorMessage = Resources.ConstantMessage.ValidationInvalidSpecialCharacter;
            }
            private void Add()
            {
                base.StatusBar.Clear();

                //Inicializa el Formulario
                txtName.Text = String.Empty;
                txtDescription.Text = String.Empty;
                lblLanguageValue.Text = Global.Languages[_IdEntityLanguage].Name;
            }
            private void LoadData()
            {
                //carga los datos en pantalla
                txtName.Text = EntityLanguage.Name;
                txtDescription.Text = EntityLanguage.Description;
                lblLanguageValue.Text = EntityLanguage.Language.Name;
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
                    EntityLanguage = Entity.LanguagesOptions.Add(EMSLibrary.User.DirectoryServices.Configuration.Language(_IdEntityLanguage), txtName.Text, txtDescription.Text, txtScope.Text, txtLimitation.Text, txtDefinition.Text);
                }
                else
                {
                    //Modificacion
                    EntityLanguage = Entity.LanguagesOptions.Modify(EMSLibrary.User.DirectoryServices.Configuration.Language(_IdEntityLanguage), txtName.Text, txtDescription.Text, txtScope.Text, txtLimitation.Text, txtDefinition.Text);
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
