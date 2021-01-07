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
using Condesus.EMS.Business.DS.Entities;

namespace Condesus.EMS.WebUI.DS
{
    public partial class FunctionalAreasLanguagesProperties : BaseProperties
    {
        #region Internal Properties
        private Int64 _IdOrganization
        {
            get
            {
                return base.NavigatorContainsTransferVar("IdOrganization") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdOrganization")) : Convert.ToInt64(GetPKfromNavigator("IdOrganization"));
            }
        }
        private Int64 _IdEntity
        {
            get
            {
                return base.NavigatorContainsTransferVar("IdFunctionalArea") ? base.NavigatorGetTransferVar<Int64>("IdFunctionalArea") : 0;
            }
        }
        private String _IdEntityLanguage
        {
            get
            {
                return base.NavigatorContainsTransferVar("IdLanguage") ? base.NavigatorGetTransferVar<String>("IdLanguage") : String.Empty;
            }
        }

        private FunctionalArea _Entity = null;
        private FunctionalArea Entity
        {
            get
            {
                if (_Entity == null)
                {
                    _Entity = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).FunctionalArea(_IdEntity);
                }

                return _Entity;
            }

            set { _Entity = value; }
        }
        private FunctionalArea_LG _EntityLanguage = null;
        private FunctionalArea_LG EntityLanguage
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
                this.txtFunctionalAreas.Focus();
            }
        }

        //Setea el Titulo de la Pagina
        //(como no puedo pasar el Objeto como una 'Interface', lo tengo que hacer localmente)
        protected override void SetPagetitle()
        {
            base.PageTitle = (Entity != null) ? Entity.LanguageOption.Name + " - " + Global.Languages[_IdEntityLanguage].Name : Resources.CommonListManage.FunctionalArea;
        }
        protected override void SetPageTileSubTitle()
        {
            base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
        }
        #endregion

        #region Private Methods
        private void LoadTextLabels()
        {
            Page.Title = Resources.CommonListManage.FunctionalArea_LG_Title;
            lblFunctionalAreas.Text = Resources.CommonListManage.FunctionalArea;
            lblLanguage.Text = Resources.CommonListManage.Language;
            rfv1.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
        }
        private void Add()
        {
            base.StatusBar.Clear();

            //Inicializa el Formulario
            txtFunctionalAreas.Text = String.Empty;
            lblLanguageValue.Text = Global.Languages[_IdEntityLanguage].Name;
        }
        private void LoadData()
        {
            //carga los datos en pantalla
            txtFunctionalAreas.Text = EntityLanguage.Name;
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
                    EntityLanguage = Entity.LanguagesOptions.Add(_IdEntityLanguage, txtFunctionalAreas.Text);
                }
                else
                {
                    //Modificacion
                    EntityLanguage = Entity.LanguagesOptions.Modify(_IdEntityLanguage, txtFunctionalAreas.Text);
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
