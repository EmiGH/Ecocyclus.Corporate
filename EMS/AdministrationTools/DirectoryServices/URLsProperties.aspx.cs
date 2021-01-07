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

using Condesus.EMS.WebUI.MasterControls;
using Condesus.EMS.Business.DS.Entities;

namespace Condesus.EMS.WebUI.DS
{
    public partial class URLsProperties : BaseProperties
    {
        #region Internal Properties

        private Int64 _IdEntity
        {
            get
            {
                return base.NavigatorContainsTransferVar("IdContactURL") ? base.NavigatorGetTransferVar<Int64>("IdContactURL") : 0;
            }
        }
        private Int64 _IdOrganization
        {
            get
            {
                object _o = ViewState["IdOrganization"];
                if (_o != null)
                    return (Int64)ViewState["IdOrganization"];

                return 0;
            }

            set
            {
                ViewState["IdOrganization"] = value;
            }
        }
        private Int64 _IdPerson
        {
            get
            {
                object _o = ViewState["IdPerson"];
                if (_o != null)
                    return (Int64)ViewState["IdPerson"];

                return 0;
            }

            set
            {
                ViewState["IdPerson"] = value;
            }
        }
        private ContactURL _Entity = null;
        private ContactURL Entity
        {
            get
            {
                try
                {
                    if (_Entity == null)
                    {
                        if (_IdPerson > 0)
                            _Entity = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).Person(_IdPerson).ContactURL(_IdEntity);
                        else
                            _Entity = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).ContactURL(_IdEntity);
                    }

                    return _Entity;
                }
                catch { return null; }
            }

            set { _Entity = value; }
        }

        RadComboBox _RdcContactType;
        CompareValidator _CvContactType;

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
                AddCombos();
                AddValidators();
            }
            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);

                if (!Page.IsPostBack)
                {
                    InitFkVars();
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
            private void InitFkVars()
            {
                //Aca intenta obtener el/los Id desde el TransferVar, si no esta ahi, entonces lo busca en las PKEntity.
                _IdOrganization = base.NavigatorContainsTransferVar("IdOrganization") ? base.NavigatorGetTransferVar<Int64>("IdOrganization") : Convert.ToInt64(GetPKfromNavigator("IdOrganization"));
                _IdPerson = NavigatorContainsTransferVar("IdPerson") ? base.NavigatorGetTransferVar<Int64>("IdPerson") : Convert.ToInt64(GetPKfromNavigator("IdPerson"));
            }
            protected override void SetPagetitle()
            {
                //Busca el nombre de la pagina si es que se lo pasaron. Sino usa el nombre de la entidad.
                String _pageTitle = Convert.ToString(GetPKfromNavigator("PageTitle"));
                if (_pageTitle != "0")
                {
                    base.PageTitle = _pageTitle;
                }
                else
                {
                    String _title = String.Empty;
                    if (Entity != null)
                    {
                        _title = Entity.LanguageOption.Name;
                    }
                    base.PageTitle = (Entity != null) ? _title : Resources.CommonListManage.ContactUrl;
                }
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }
            private void AddCombos()
            {
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                //Para cargar los contactTypes, debo setear el Applicability 3 que es URLs.
                _params.Add("IdApplicability", Condesus.EMS.Business.Common.ConstantsApplicabilities.Url);
                AddCombo(phContactType, ref _RdcContactType, Common.ConstantsEntitiesName.DS.ContactTypes, String.Empty, _params, false, true, false, false, false);
            }
            private void AddValidators()
            {
                ValidatorRequiredField(Common.ConstantsEntitiesName.DS.ContactTypes, phContactTypeValidator, ref _CvContactType, _RdcContactType, Resources.ConstantMessage.SelectAContactType);
            }
        #endregion

        #region Private Methods
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonListManage.ContactUrl;
                lblContactType.Text = Resources.CommonListManage.ContactType;
                lblDescription.Text = Resources.CommonListManage.Description;
                lblLanguage.Text = Resources.CommonListManage.Language;
                lblName.Text = Resources.CommonListManage.Name;
                lblUrl.Text = Resources.CommonListManage.URL;
                rfv1.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
                rfv2.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
            }
            private void Add()
            {
                base.StatusBar.Clear();

                //Inicializa el Formulario
                txtUrl.Text = String.Empty;
                txtDescription.Text = String.Empty;
                txtName.Text = String.Empty;
                _RdcContactType.SelectedValue = Common.Constants.ComboBoxSelectItemValue;
                lblLanguageValue.Text = Global.DefaultLanguage.Name;
            }
            private void LoadData()
            {
                //carga los datos en pantalla
                txtUrl.Text = Entity.URL;
                txtName.Text = Entity.LanguageOption.Name;
                txtDescription.Text = Entity.LanguageOption.Description;
                _RdcContactType.SelectedValue = "IdContactType=" + Entity.ContactType.IdContactType.ToString() + "& IdApplicability=" + Entity.ContactType.Applicability.ToString();
                lblLanguageValue.Text = Entity.LanguageOption.Language.Name;
            }
        #endregion

        #region Generic Methods

        private ContactURL AddEntity()
        {
            Int64 _idContactType = Convert.ToInt64(GetKeyValue(_RdcContactType.SelectedValue, "IdContactType"));
            ContactType _contactType = EMSLibrary.User.DirectoryServices.Configuration.ContactType(3, _idContactType);
            if (_IdPerson > 0)
            {
                return EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).Person(_IdPerson).ContactURLsAdd(txtUrl.Text, txtName.Text, txtDescription.Text, _contactType);
            }
            else
                return EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).ContactURLsAdd(txtUrl.Text, txtName.Text, txtDescription.Text, _contactType);

        }
        private void ModifyEntity()
        {
            Int64 _idContactType = Convert.ToInt64(GetKeyValue(_RdcContactType.SelectedValue, "IdContactType"));
            ContactType _contactType = EMSLibrary.User.DirectoryServices.Configuration.ContactType(Condesus.EMS.Business.Common.ConstantsApplicabilities.Url, _idContactType);

            if (_IdPerson > 0)
            {
                EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).Person(_IdPerson).ContactURLsModify(Entity, txtUrl.Text, txtName.Text, txtDescription.Text, _contactType);
            }
            else
                EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).ContactURLsModify(Entity, txtUrl.Text, txtName.Text, txtDescription.Text, _contactType);
        }
        private void RemoveEntity()
        {
            if (_IdPerson > 0)
            {
                EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).Person(_IdPerson).Remove(Entity);
            }
            else
                EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).Remove(Entity);
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
                        Entity = AddEntity();

                        

                    }
                    else
                    {
                        //Modificacion
                        ModifyEntity();
                    }
                    base.NavigatorAddTransferVar("IdPerson", _IdPerson);
                    base.NavigatorAddTransferVar("IdContactURL", Entity.IdContactURL);
                    base.NavigatorAddTransferVar("IdOrganization", _IdOrganization);

                    base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.DS.ContactURL);

                    String _parentEntity = String.Empty;
                    if (Convert.ToString(GetPKfromNavigator("ParentEntity")) != "0")
                    {
                        _parentEntity = Convert.ToString(GetPKfromNavigator("ParentEntity"));
                        base.NavigatorAddTransferVar("ParentEntity", _parentEntity);
                    }
                    else
                    {
                        if (base.NavigatorContainsTransferVar("ParentEntity"))
                        {
                            _parentEntity = base.NavigatorGetTransferVar<String>("ParentEntity");
                            base.NavigatorAddTransferVar("ParentEntity", base.NavigatorGetTransferVar<String>("ParentEntity"));
                        }
                    }

                    String _pkValues = "IdOrganization=" + _IdOrganization.ToString()
                        + "& IdPerson=" + _IdPerson.ToString()
                        + "& IdContactURL=" + Entity.IdContactURL.ToString()
                        + "& ParentEntity=" + _parentEntity;
                    base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);


                    base.NavigatorAddTransferVar("EntityNameContextInfo", base.NavigatorGetTransferVar<String>("EntityNameContextInfo"));
                    //Navigate("~/MainInfo/ListViewer.aspx", Entity.URL, Condesus.WebUI.Navigation.DeleteType.DeleteModifiedEntity);
                    String _entityPropertyName = String.Concat(Entity.URL);
                    NavigatePropertyEntity(Common.ConstantsEntitiesName.DS.ContactURL, _entityPropertyName, Condesus.WebUI.Navigation.NavigateMenuAction.Add);

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

