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
    public partial class ContactMessengersProvidersProperties : BaseProperties
    {
        #region Internal Properties

        private String _IdEntity
        {
            get
            {
                return base.NavigatorContainsTransferVar("Provider") ? base.NavigatorGetTransferVar<String>("Provider") : String.Empty;
            }
        }
        private ContactMessengerProvider _Entity = null;
        private ContactMessengerProvider Entity
        {
            get
            {
                if (_Entity == null)
                    _Entity = EMSLibrary.User.DirectoryServices.Configuration.ContactMessengerProvider(_IdEntity);

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
                LoadTextLabels();
            }
            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);

                if (!Page.IsPostBack)
                {
                    //Inicializo el Form
                    if (Entity == null)
                        Add();

                    //Form
                    base.SetContentTableRowsCss(tblContentForm);
                    this.txtProvider.Focus();
                }
            }
            //Setea el Titulo de la Pagina
            //(como no puedo pasar el Objeto como una 'Interface', lo tengo que hacer localmente)
            protected override void SetPagetitle()
            {
                base.PageTitle = (Entity != null) ? Entity.Provider : Resources.CommonListManage.ContactMessengerProvider;
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }
        #endregion

        #region Private Methods
        private void LoadTextLabels()
        {
            Page.Title = Resources.CommonListManage.ContactMessengerProvider;
            lblProvider.Text = Resources.CommonListManage.Provider;
            rfv1.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
        }
        private void Add()
        {
            base.StatusBar.Clear();

            //Inicializa el Formulario
            txtProvider.Text = String.Empty;
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
                        Entity = EMSLibrary.User.DirectoryServices.Configuration.ContactMessengersProvidersAdd(txtProvider.Text);
                        base.NavigatorAddTransferVar("Provider", Entity.Provider);
                        //base.Navigate(HttpContext.Current.Request.Url.AbsolutePath, Entity.Provider + " " + Resources.Common.Edit);

                        String _pkValues = "Provider=" + Entity.Provider;
                        base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);

                        base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.DS.ContactMessengerProvider);
                        if (Convert.ToString(GetPKfromNavigator("ParentEntity"))!="0")
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
                        String _entityPropertyName = String.Concat(Entity.Provider);
                        NavigatePropertyEntity(Common.ConstantsEntitiesName.DS.ContactMessengerProvider, _entityPropertyName, Condesus.WebUI.Navigation.NavigateMenuAction.Add);
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