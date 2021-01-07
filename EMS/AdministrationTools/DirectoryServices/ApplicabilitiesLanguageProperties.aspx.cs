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
    public partial class ApplicabilitiesLanguageProperties : BaseProperties
    {
        #region Internal Properties

        private Int64 _IdEntity
        {
            get
            {
                return base.NavigatorContainsTransferVar("IdApplicability") ? base.NavigatorGetTransferVar<Int64>("IdApplicability") : 0;
            }
        }
        private String _IdEntityLanguage
        {
            get
            {
                return base.NavigatorContainsTransferVar("IdLanguage") ? base.NavigatorGetTransferVar<String>("IdLanguage") : String.Empty;
            }
        }

        private ApplicabilityContactType _Entity = null;
        private ApplicabilityContactType Entity
        {
            get
            {
                if (_Entity == null)
                {
                    _Entity = EMSLibrary.User.DirectoryServices.Configuration.ApplicabilityContactType(_IdEntity);
                }

                return _Entity;
            }

            set { _Entity = value; }
        }
        private ApplicabilityContactType_LG _EntityLanguage = null;
        private ApplicabilityContactType_LG EntityLanguage
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
            protected override void OnInit(EventArgs e)
            {
                base.OnInit(e);

                LoadTextLables();
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
                    this.txtApplicability.Focus();
                }
            }
            //Setea el Titulo de la Pagina
            //(como no puedo pasar el Objeto como una 'Interface', lo tengo que hacer localmente)
            protected override void SetPagetitle()
            {
                base.PageTitle = (Entity != null) ? Entity.LanguageOption.Name + " - " + Global.Languages[_IdEntityLanguage].Name : Resources.CommonListManage.Applicability;
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }
        #endregion

        #region Private Methods
        private void LoadTextLables()
        {
            Page.Title = Resources.CommonListManage.Applicability_LG_Title;
            lblApplicability.Text = Resources.CommonListManage.Applicability;
            lblLanguage.Text = Resources.CommonListManage.Language;
            rfv1.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
        }
        private void Add()
        {
            base.StatusBar.Clear();

            //Inicializa el Formulario
            txtApplicability.Text = String.Empty;
            lblLanguageValue.Text = Global.Languages[_IdEntityLanguage].Name;
        }
        private void LoadData()
        {
            //carga los datos en pantalla
            txtApplicability.Text = EntityLanguage.Name;
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
                    EntityLanguage = Entity.LanguagesOptions.Add(_IdEntityLanguage, txtApplicability.Text);
                }
                else
                {
                    //Modificacion
                    EntityLanguage = Entity.LanguagesOptions.Modify(_IdEntityLanguage, txtApplicability.Text);

                    //base.NavigatorAddTransferVar("IdApplicabilityContactType", Entity.IdApplicabilityContactType);
                    //base.NavigatorAddTransferVar("IdLanguage", EntityLanguage.Language.IdLanguage);
                    //base.Navigate(HttpContext.Current.Request.Url.AbsolutePath, EntityLanguage.Name + " - " + EntityLanguage.Language.Name + " " + Resources.Common.Edit, Condesus.WebUI.Navigation.DeleteType.DeleteModifiedEntity);
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
