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
    public partial class MessengersProperties : BaseProperties
    {
        #region Internal Properties

        private Int64 _IdEntity
        {
            get
            {
                return base.NavigatorContainsTransferVar("IdContactMessenger") ? base.NavigatorGetTransferVar<Int64>("IdContactMessenger") : 0;
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
        private ContactMessenger _Entity = null;
        private ContactMessenger Entity
        {
            get
            {
                try
                {
                    if (_Entity == null)
                    {
                        if (_IdPerson > 0)
                            _Entity = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).Person(_IdPerson).ContactMessenger(_IdEntity);
                        else
                            _Entity = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).ContactMessenger(_IdEntity);
                    }

                    return _Entity;
                }
                catch { return null; }
            }

            set { _Entity = value; }
        }

        RadComboBox _RdcContactType;
        CompareValidator _CvContactType;
        RadComboBox _RdcProvider;
        CompareValidator _CvProvider;
        RadComboBox _RdcApplication;
        CompareValidator _CvApplication;

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

                LoadComboApplications();
                if (!Page.IsPostBack)
                {
                    InitFkVars();
                    //Inicializo el Form
                    if (Entity == null)
                        { Add(); }
                    else
                        { LoadData(); } //Edit.

                    //Form
                    base.SetContentTableRowsCss(tblContentForm);
                }
            }
            private void InitFkVars()
            {
                //Aca intenta obtener el/los Id desde el TransferVar, si no esta ahi, entonces lo busca en las PKEntity.
                _IdOrganization = base.NavigatorContainsTransferVar("IdOrganization") ? base.NavigatorGetTransferVar<Int64>("IdOrganization") : Convert.ToInt64(GetPKfromNavigator("IdOrganization"));
                _IdPerson = NavigatorContainsTransferVar("IdPerson") ? base.NavigatorGetTransferVar<Int64>("IdPerson") : Convert.ToInt64(GetPKfromNavigator("IdPerson"));
            }
            //Setea el Titulo de la Pagina
            //(como no puedo pasar el Objeto como una 'Interface', lo tengo que hacer localmente)
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
                        _title = Entity.Data;
                    }
                    base.PageTitle = (Entity != null) ? _title : Resources.CommonListManage.ContactMessenger;
                }
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }
            private void AddCombos()
            {
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                AddCombo(phProvider, ref _RdcProvider, Common.ConstantsEntitiesName.DS.ContactMessengerProviders, String.Empty, _params, false, true, false, true, false);
                _RdcProvider.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(_RdcProvider_SelectedIndexChanged);
                FwMasterPage.RegisterContentAsyncPostBackTrigger(_RdcProvider, "SelectedIndexChanged");

                //Para cargar los contactTypes, debo setear el Applicability 4 que es Messenger.
                _params.Add("IdApplicability", Condesus.EMS.Business.Common.ConstantsApplicabilities.Messenger);
                AddCombo(phContactType, ref _RdcContactType, Common.ConstantsEntitiesName.DS.ContactTypes, String.Empty, _params, false, true, false, false, false);

                //LoadComboApplications();
            }
            private void AddValidators()
            {
                ValidatorRequiredField(Common.ConstantsEntitiesName.DS.ContactTypes, phContactTypeValidator, ref _CvContactType, _RdcContactType, Resources.ConstantMessage.SelectAContactType);
                ValidatorRequiredField(Common.ConstantsEntitiesName.DS.ContactMessengerProviders, phProviderValidator, ref _CvProvider, _RdcProvider, Resources.ConstantMessage.SelectAProvider);
            }
        #endregion

        #region Private Methods
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonListManage.ContactMessenger;
                lblApplication.Text = Resources.CommonListManage.Application;
                lblContactType.Text = Resources.CommonListManage.ContactType;
                lblData.Text = Resources.CommonListManage.Data;
                lblProvider.Text = Resources.CommonListManage.Provider;
                rfv1.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
            }
            private void Add()
            {
                base.StatusBar.Clear();

                //Inicializa el Formulario
                txtData.Text = String.Empty;
                _RdcContactType.SelectedValue = Common.Constants.ComboBoxSelectItemValue;
                _RdcProvider.SelectedValue = Common.Constants.ComboBoxSelectItemValue;
                _RdcApplication.SelectedValue = Common.Constants.ComboBoxSelectItemValue;
                _RdcApplication.Enabled = false;

                _RdcContactType.Enabled = true;
                _RdcProvider.Enabled = true;
            }
            private void LoadData()
            {
                //carga los datos en pantalla
                txtData.Text = Entity.Data;
                _RdcContactType.SelectedValue = "IdContactType=" + Entity.ContactType.IdContactType.ToString() + "& IdApplicability=" + Entity.ContactType.Applicability.ToString();
                _RdcProvider.SelectedValue = "Provider=" + Entity.Provider;
                //Ahora que tiene el provider seteado, debe cargar nuevamente el combo de Aplicaciones de mensajeros.
                LoadComboApplications();
                //Por ultimo setea la aplicacion en el combo.
                _RdcApplication.SelectedValue = "Provider=" + Entity.Provider + "& Application=" + Entity.Application;

                //En el edit, no se pueden modificar el contact, provider ni aplicacion....
                _RdcContactType.Enabled = false;
                _RdcProvider.Enabled = false;
                _RdcApplication.Enabled = false;
            }
            private void LoadComboApplications()
            {
                String _paramProvider = String.Empty;
                Dictionary<String, Object> _params = new Dictionary<String, Object>();

                _paramProvider = GetKeyValue(_RdcProvider.SelectedValue, "Provider") == null ? String.Empty : Convert.ToString(GetKeyValue(_RdcProvider.SelectedValue, "Provider"));
                _params.Add("Provider", _paramProvider);
                AddCombo(phApplication, ref _RdcApplication, Common.ConstantsEntitiesName.DS.ContactMessengerApplications, String.Empty, _params, false, true, false, false, false);

                ValidatorRequiredField(Common.ConstantsEntitiesName.DS.ContactMessengerApplications, phApplicationValidator, ref _CvApplication, _RdcApplication, Resources.ConstantMessage.SelectAnApplication);
            }
        #endregion

        #region Generic Methods

        private ContactMessenger AddEntity()
        {
            Int64 _idContactType = Convert.ToInt64(GetKeyValue(_RdcContactType.SelectedValue, "IdContactType"));
            String _idProvider = GetKeyValue(_RdcProvider.SelectedValue, "Provider").ToString();
            String _idApplication = GetKeyValue(_RdcApplication.SelectedValue, "Application").ToString();
            ContactType _contactType = EMSLibrary.User.DirectoryServices.Configuration.ContactType(Condesus.EMS.Business.Common.ConstantsApplicabilities.Messenger, _idContactType);

            if (_IdPerson > 0)
            {
                return EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).Person(_IdPerson).ContactMessengersAdd(_idProvider, _idApplication, txtData.Text, _contactType);
            }
            else
                return EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).ContactMessengersAdd(_idProvider, _idApplication, txtData.Text, _contactType);

        }
        private void ModifyEntity()
        {
            Int64 _idContactType = Convert.ToInt64(GetKeyValue(_RdcContactType.SelectedValue, "IdContactType"));
            String _idProvider = GetKeyValue(_RdcProvider.SelectedValue, "Provider").ToString();
            String _idApplication = GetKeyValue(_RdcApplication.SelectedValue, "Application").ToString();
            ContactType _contactType = EMSLibrary.User.DirectoryServices.Configuration.ContactType(Condesus.EMS.Business.Common.ConstantsApplicabilities.Messenger, _idContactType);

            if (_IdPerson > 0)
            {
                EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).Person(_IdPerson).ContactMessengersModify(Entity, _idProvider, _idApplication, txtData.Text, _contactType);
            }
            else
                EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).ContactMessengersModify(Entity, _idProvider, _idApplication, txtData.Text, _contactType);
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
                base.NavigatorAddTransferVar("IdContactMessenger", Entity.IdContactMessenger);
                base.NavigatorAddTransferVar("IdOrganization", _IdOrganization);

                String _pkValues = "IdPerson=" + _IdPerson.ToString() +
                    "& IdContactMessenger=" + Entity.IdContactMessenger.ToString() +
                    "& IdOrganization=" + _IdOrganization.ToString();
                base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);

                base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.DS.ContactMessenger);
                if (Convert.ToString(GetPKfromNavigator("ParentEntity")) != "0")
                {
                    base.NavigatorAddTransferVar("ParentEntity", Convert.ToString(GetPKfromNavigator("ParentEntity")));
                }
                else
                {
                    if (base.NavigatorContainsTransferVar("ParentEntity"))
                    {
                        base.NavigatorAddTransferVar("ParentEntity", base.NavigatorGetTransferVar<String>("ParentEntity"));
                    }
                }
                base.NavigatorAddTransferVar("EntityNameContextInfo", base.NavigatorGetTransferVar<String>("EntityNameContextInfo"));
                //Navigate("~/MainInfo/ListViewer.aspx", Entity.Data, Condesus.WebUI.Navigation.DeleteType.DeleteModifiedEntity);
                String _entityPropertyName = String.Concat(Entity.Data);
                NavigatePropertyEntity(Common.ConstantsEntitiesName.DS.ContactMessenger, _entityPropertyName, Condesus.WebUI.Navigation.NavigateMenuAction.Add);

                base.StatusBar.ShowMessage(Resources.Common.SaveOK);
            }
            catch (Exception ex)
            {
                base.StatusBar.ShowMessage(ex);
            }
        }
        protected void _RdcProvider_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (_RdcProvider.SelectedValue == Common.Constants.ComboBoxSelectItemValue)
            {
                _RdcApplication.Enabled = false;
            }
            else
            {
                LoadComboApplications();

                _RdcApplication.Enabled = true;
            }
        }
        #endregion
    }
}