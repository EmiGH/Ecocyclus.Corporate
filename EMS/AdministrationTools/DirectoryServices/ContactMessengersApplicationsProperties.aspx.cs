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
using System.Collections.Generic;
using Condesus.EMS.WebUI.MasterControls;
using Condesus.EMS.Business.DS.Entities;

namespace Condesus.EMS.WebUI.DS
{
    public partial class ContactMessengersApplicationsProperties : BaseProperties
    {
        #region Internal Properties
        RadComboBox _RdcProvider;
        CompareValidator _CvProvider;
        private String _IdEntity
        {
            get
            {
                return base.NavigatorContainsTransferVar("Application") ? base.NavigatorGetTransferVar<String>("Application") : String.Empty;
            }
        }
        private String _IdProvider
        {
            get
            {
                return base.NavigatorContainsTransferVar("Provider") ? base.NavigatorGetTransferVar<String>("Provider") : String.Empty;
            }
        }

        private ContactMessengerApplication _Entity = null;
        private ContactMessengerApplication Entity
        {
            get
            {
                if (_Entity == null)
                    _Entity = EMSLibrary.User.DirectoryServices.Configuration.ContactMessengerApplication(_IdProvider, _IdEntity);

                return _Entity;
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

            LoadTextLables();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadComboProvider();

            if (!Page.IsPostBack)
            {
                //Inicializo el Form
                if (Entity == null)
                    Add();
                else
                    LoadData(); //Edit.

                //Form
                base.SetContentTableRowsCss(tblContentForm);
                this.txtApplication.Focus();
            }
        }

        //Setea el Titulo de la Pagina
        //(como no puedo pasar el Objeto como una 'Interface', lo tengo que hacer localmente)
        protected override void SetPagetitle()
        {
            base.PageTitle = (Entity != null) ? Entity.Provider + " - " + Entity.Application : Resources.CommonListManage.ContactMessengerApplication;
        }
        protected override void SetPageTileSubTitle()
        {
            base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
        }
        #endregion

        #region Private Methods
        private void LoadTextLables()
        {
            Page.Title = Resources.CommonListManage.ContactMessengerApplication;
            lblApplication.Text = Resources.CommonListManage.Application;
            lblProvider.Text = Resources.CommonListManage.Provider;
            rfv1.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
        }
        private void Add()
        {
            base.StatusBar.Clear();

            //Inicializa el Formulario
            txtApplication.Text = String.Empty;
        }
        private void LoadData()
        {
            //carga los datos en pantalla
            txtApplication.Text = Entity.Application;
            _RdcProvider.SelectedValue = "Provider=" + Entity.Provider;
        }
        private void LoadComboProvider()
        {
            Dictionary<String, Object> _params = new Dictionary<String, Object>();
            AddCombo(phProvider, ref _RdcProvider, Common.ConstantsEntitiesName.DS.ContactMessengerProviders, String.Empty, _params, false, true, false, true, false);
            ValidatorRequiredField(Common.ConstantsEntitiesName.DS.ContactMessengerProviders, phProviderValidator, ref _CvProvider, _RdcProvider, Resources.ConstantMessage.SelectAProvider);
        }

        #endregion

        #region Page Events
            protected void btnSave_Click(object sender, EventArgs e)
            {
                try
                {
                    if (Entity == null)
                    {
                        String _idProvider = GetKeyValue(_RdcProvider.SelectedValue, "Provider").ToString();

                        //Alta
                        Entity = EMSLibrary.User.DirectoryServices.Configuration.ContactMessengersApplicationsAdd(_idProvider, txtApplication.Text);
                        base.NavigatorAddTransferVar("Application", Entity.Application);
                        base.NavigatorAddTransferVar("Provider", Entity.Provider);                        

                        String _pkValues = "Application=" + Entity.Application
                        + "& Provider=" + Entity.Provider;
                        base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);
                        base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.DS.ContactMessengerApplication);
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
                        //Navigate("~/MainInfo/ListViewer.aspx", Convert.ToString(GetPKfromNavigator("PageTitle")));
                        String _entityPropertyName = String.Concat(Entity.Provider, ", ", Entity.Application);
                        NavigatePropertyEntity(Common.ConstantsEntitiesName.DS.ContactMessengerApplication, _entityPropertyName, Condesus.WebUI.Navigation.NavigateMenuAction.Add);
                    }

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