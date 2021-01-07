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
    public partial class URLsLanguagesProperties : BaseProperties
    {
        #region Internal Properties

        private String _ParentEntity
        {
            get
            {
                //object _o = ViewState["ParentEntity"];
                //if (_o != null)
                //    return (String)ViewState["ParentEntity"];

                //return String.Empty;
                return base.NavigatorContainsTransferVar("ParentEntity") ? base.NavigatorGetTransferVar<String>("ParentEntity") : String.Empty;
            }

            //set
            //{
            //    ViewState["ParentEntity"] = value;
            //}
        }
        private Int64 _IdOrganization
        {
            get
            {
                //object _o = ViewState["IdOrganization"];
                //if (_o != null)
                //    return (Int64)ViewState["IdOrganization"];

                //return 0;
                return base.NavigatorContainsTransferVar("IdOrganization") ? base.NavigatorGetTransferVar<Int64>("IdOrganization") : 0;
            }

            //set
            //{
            //    ViewState["IdOrganization"] = value;
            //}
        }
        private Int64 _IdPerson
        {
            get
            {
                //object _o = ViewState["IdPerson"];
                //if (_o != null)
                //    return (Int64)ViewState["IdPerson"];

                //return 0;
                return base.NavigatorContainsTransferVar("IdPerson") ? base.NavigatorGetTransferVar<Int64>("IdPerson") : 0;
            }

            //set
            //{
            //    ViewState["IdOrganization"] = value;
            //}
        }

        private Int64 _IdEntity
        {
            get
            {
                return base.NavigatorContainsTransferVar("IdContactURL") ? base.NavigatorGetTransferVar<Int64>("IdContactURL") : 0;
            }
        }
        private String _IdEntityLanguage
        {
            get
            {
                return base.NavigatorContainsTransferVar("IdLanguage") ? base.NavigatorGetTransferVar<String>("IdLanguage") : String.Empty;
            }
        }

        private ContactURL _Entity = null;
        private ContactURL Entity
        {
            get
            {
                switch (_ParentEntity)
                {
                    case "Person":
                        _Entity = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).Person(_IdPerson).ContactURL(_IdEntity);
                        break;
                    case "Organization":
                        _Entity = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).ContactURL(_IdEntity);
                        break;
                }

                return _Entity;
            }

            set { _Entity = value; }
        }
        private ContactURL_LG _EntityLanguage = null;
        private ContactURL_LG EntityLanguage
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

                //InitFkVars();

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
                base.PageTitle = (Entity != null) ? Entity.LanguageOption.Name + " - " + Global.Languages[_IdEntityLanguage].Name : Resources.CommonListManage.ContactUrl;
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }
        #endregion

        #region Private Methods
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonListManage.ContactUrl_LG_Title;
                lblDescription.Text = Resources.CommonListManage.Description;
                lblLanguage.Text = Resources.CommonListManage.Language;
                lblName.Text = Resources.CommonListManage.Name;
                lblUrl.Text = Resources.CommonListManage.URL;
                rfv1.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
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
                    EntityLanguage = Entity.LanguagesOptions.Add(_IdEntityLanguage, txtName.Text, txtDescription.Text);
                }
                else
                {
                    //Modificacion
                    EntityLanguage = Entity.LanguagesOptions.Modify(_IdEntityLanguage, txtName.Text, txtDescription.Text);
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

