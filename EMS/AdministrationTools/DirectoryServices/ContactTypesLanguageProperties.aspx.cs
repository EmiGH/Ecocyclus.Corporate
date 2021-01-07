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

using Condesus.EMS.WebUI.MasterControls;
using Condesus.EMS.Business.DS.Entities;

namespace Condesus.EMS.WebUI.DS
{
    public partial class ContactTypesLanguageProperties : BaseProperties
    {
        #region Internal Properties

        private Int64 _IdEntity
        {
            get
            {
                return base.NavigatorContainsTransferVar("IdContactType") ? base.NavigatorGetTransferVar<Int64>("IdContactType") : 0;
            }
        }
        private String _IdEntityLanguage
        {
            get
            {
                return base.NavigatorContainsTransferVar("IdLanguage") ? base.NavigatorGetTransferVar<String>("IdLanguage") : String.Empty;
            }
        }
        private Int64 _IdApplicability
        {
            get
            {
                object _o = ViewState["IdApplicability"];
                if (_o != null)
                    return (Int64)ViewState["IdApplicability"];

                return 0;
            }

            set
            {
                ViewState["IdApplicability"] = value;
            }
        }

        private ContactType _Entity = null;
        private ContactType Entity
        {
            get
            {
                if (_Entity == null)
                {
                    _Entity = EMSLibrary.User.DirectoryServices.Configuration.ContactType(_IdApplicability, _IdEntity);
                }

                return _Entity;
            }

            set { _Entity = value; }
        }
        private ContactType_LG _EntityLanguage = null;
        private ContactType_LG EntityLanguage
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
                InitFkVars();

                //Inicializo el Form
                if (EntityLanguage == null)
                    Add();
                else
                    LoadData(); //Edit.

                //Form
                base.SetContentTableRowsCss(tblContentForm);                
                this.txtContactType.Focus();
            }

            //Menu de General Options
            //base.BuildPropertyGeneralOptionsMenu(Entity, new RadMenuEventHandler(rmnuGeneralOption_ItemClick));
        }

        private void InitFkVars()
        {
            _IdApplicability = base.NavigatorContainsTransferVar("IdApplicability") ? base.NavigatorGetTransferVar<Int64>("IdApplicability") : 0;
        }

        //Setea el Titulo de la Pagina
        //(como no puedo pasar el Objeto como una 'Interface', lo tengo que hacer localmente)
        protected override void SetPagetitle()
        {
            base.PageTitle = (Entity != null) ? Entity.LanguageOption.Name + " - " + Global.Languages[_IdEntityLanguage].Name : Resources.CommonListManage.ContactType;
        }
        protected override void SetPageTileSubTitle()
        {
            base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
        }
        #endregion

        #region Private Methods
        private void LoadTextLables()
        {
            Page.Title = Resources.CommonListManage.ContactType_LG_Title;
            lblApplicability.Text = Resources.CommonListManage.Applicability;
            lblContactType.Text = Resources.CommonListManage.ContactType;
            lblDescription.Text = Resources.CommonListManage.Description;
            lblLanguage.Text = Resources.CommonListManage.Language;
            rfv1.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
        }
        private void Add()
        {
            base.StatusBar.Clear();

            //Inicializa el Formulario
            txtContactType.Text = String.Empty;
            txtDescription.Text = String.Empty;
            lblLanguageValue.Text = Global.Languages[_IdEntityLanguage].Name;
            SetFields();
        }
        private void LoadData()
        {
            //carga los datos en pantalla
            txtContactType.Text = EntityLanguage.Name;
            txtDescription.Text = EntityLanguage.Description;
            lblLanguageValue.Text = EntityLanguage.Language.Name;
            SetFields();
        }
        private void SetFields()
        {
            ApplicabilityContactType _applicability = EMSLibrary.User.DirectoryServices.Configuration.ApplicabilityContactType(Entity.Applicability);

            if (_applicability.LanguagesOptions.Item(_IdEntityLanguage) == null)
            {
                lblApplicabilityValue.Text = _applicability.LanguageOption.Name.ToString();
            }
            else
            {
                lblApplicabilityValue.Text = _applicability.LanguagesOptions.Item(_IdEntityLanguage).Name.ToString();
            }
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
                    EntityLanguage = Entity.LanguagesOptions.Add(_IdEntityLanguage, txtContactType.Text,txtDescription.Text);
                    

                    //base.NavigatorAddTransferVar("IdContactType", Entity.IdContactType);
                    //base.NavigatorAddTransferVar("IdLanguage", EntityLanguage.Language.IdLanguage);

                    //String _pkValues = "IdContactType=" + Entity.IdContactType.ToString() +
                    //    ", IdLanguage=" + EntityLanguage.Language.IdLanguage.ToString();
                    //base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);

                    //base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.DS.ContactType);
                    //Navigate("~/MainInfo/ListViewer.aspx", Resources.CommonListManage.ContactType + " " + EntityLanguage.Name + " - " + Global.Languages[_IdEntityLanguage].Name);
                }
                else
                {
                    //Modificacion
                    EntityLanguage = Entity.LanguagesOptions.Modify(_IdEntityLanguage, txtContactType.Text, txtDescription.Text);

                    //base.NavigatorAddTransferVar("IdContactType", Entity.IdContactType);
                    //base.NavigatorAddTransferVar("IdLanguage", EntityLanguage.Language.IdLanguage);

                    //base.Navigate(HttpContext.Current.Request.Url.AbsolutePath, EntityLanguage.Name + " - " + Global.Languages[_IdEntityLanguage].Name + " " + Resources.Common.Edit, Condesus.WebUI.Navigation.DeleteType.DeleteModifiedEntity);
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
